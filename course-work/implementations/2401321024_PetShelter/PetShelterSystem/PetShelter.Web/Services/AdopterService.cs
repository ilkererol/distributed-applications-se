using PetShelter.Web.DTOs.Request;
using PetShelter.Web.DTOs.Response;
using PetShelter.Web.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace PetShelter.Web.Services
{
    public class AdopterService : IAdopterService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public AdopterService(
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

        public async Task<PagedResult<AdopterOutput>> GetAllAsync(
            string? lastName,
            string? phone,
            string? sortBy,
            bool ascending,
            int page,
            int pageSize)
        {
            var client = CreateClient();

            var url = $"api/adopters?page={page}&pageSize={pageSize}&ascending={ascending}" +
                      (string.IsNullOrEmpty(lastName) ? "" : $"&lastName={lastName}") +
                      (string.IsNullOrEmpty(phone) ? "" : $"&phone={phone}") +
                      (string.IsNullOrEmpty(sortBy) ? "" : $"&sortBy={sortBy}");

            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return new PagedResult<AdopterOutput>();

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PagedResult<AdopterOutput>>(body, _jsonOptions)
                   ?? new PagedResult<AdopterOutput>();
        }

        public async Task<AdopterOutput?> GetByIdAsync(int id)
        {
            var client = CreateClient();
            var response = await client.GetAsync($"api/adopters/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<AdopterOutput>(body, _jsonOptions);
        }

        public async Task<bool> CreateAsync(AdopterInput input)
        {
            var client = CreateClient();
            var json = JsonSerializer.Serialize(input);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/adopters", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(int id, AdopterInput input)
        {
            var client = CreateClient();
            var json = JsonSerializer.Serialize(input);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"api/adopters/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var client = CreateClient();
            var response = await client.DeleteAsync($"api/adopters/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}