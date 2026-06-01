using PetShelter.Web.DTOs.Request;
using PetShelter.Web.DTOs.Response;
using PetShelter.Web.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace PetShelter.Web.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public AnimalService(
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

        public async Task<PagedResult<AnimalOutput>> GetAllAsync(
            string? species,
            bool? isVaccinated,
            string? sortBy,
            bool ascending,
            int page,
            int pageSize)
        {
            var client = CreateClient();

            var url = $"api/animals?page={page}&pageSize={pageSize}&ascending={ascending}" +
                      (string.IsNullOrEmpty(species) ? "" : $"&species={species}") +
                      (isVaccinated.HasValue ? $"&isVaccinated={isVaccinated}" : "") +
                      (string.IsNullOrEmpty(sortBy) ? "" : $"&sortBy={sortBy}");

            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return new PagedResult<AnimalOutput>();

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PagedResult<AnimalOutput>>(body, _jsonOptions)
                   ?? new PagedResult<AnimalOutput>();
        }

        public async Task<AnimalOutput?> GetByIdAsync(int id)
        {
            var client = CreateClient();
            var response = await client.GetAsync($"api/animals/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<AnimalOutput>(body, _jsonOptions);
        }

        public async Task<bool> CreateAsync(AnimalInput input)
        {
            var client = CreateClient();
            var json = JsonSerializer.Serialize(input);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/animals", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(int id, AnimalInput input)
        {
            var client = CreateClient();
            var json = JsonSerializer.Serialize(input);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"api/animals/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var client = CreateClient();
            var response = await client.DeleteAsync($"api/animals/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}