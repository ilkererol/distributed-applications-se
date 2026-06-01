using PetShelter.Web.DTOs.Request;
using PetShelter.Web.DTOs.Response;
using PetShelter.Web.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace PetShelter.Web.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public ApplicationService(
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        private HttpClient CreateClient()
        {
            var client = _httpClientFactory.CreateClient("PetShelterApi");
            var token = _httpContextAccessor.HttpContext?.User
                .Claims.FirstOrDefault(c => c.Type == "JwtToken")?.Value;

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }

        public async Task<PagedResult<ApplicationOutput>> GetAllAsync(
            string? species,
            DateTime? applicationDate,
            string? sortBy,
            bool ascending,
            int page,
            int pageSize)
        {
            var client = CreateClient();

            var url = $"api/applications?page={page}&pageSize={pageSize}&ascending={ascending}" +
                      (string.IsNullOrEmpty(species) ? "" : $"&species={species}") +
                      (applicationDate.HasValue ? $"&applicationDate={applicationDate.Value:yyyy-MM-dd}" : "") +
                      (string.IsNullOrEmpty(sortBy) ? "" : $"&sortBy={sortBy}");

            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return new PagedResult<ApplicationOutput>();

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PagedResult<ApplicationOutput>>(body, _jsonOptions)
                   ?? new PagedResult<ApplicationOutput>();
        }

        public async Task<ApplicationOutput?> GetByIdAsync(int id)
        {
            var client = CreateClient();
            var response = await client.GetAsync($"api/applications/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ApplicationOutput>(body, _jsonOptions);
        }

        public async Task<bool> CreateAsync(ApplicationInput input)
        {
            var client = CreateClient();
            var json = JsonSerializer.Serialize(input);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/applications", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(int id, ApplicationInput input)
        {
            var client = CreateClient();
            var json = JsonSerializer.Serialize(input);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"api/applications/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var client = CreateClient();
            var response = await client.DeleteAsync($"api/applications/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}