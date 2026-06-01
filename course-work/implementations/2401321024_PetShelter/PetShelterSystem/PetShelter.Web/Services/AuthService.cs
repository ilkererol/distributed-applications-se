using PetShelter.Web.DTOs.Request;
using PetShelter.Web.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace PetShelter.Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public AuthService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string?> LoginAsync(LoginInput input)
        {
            var client = _httpClientFactory.CreateClient("PetShelterApi");

            var json = JsonSerializer.Serialize(input);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/auth/login", content);
            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            var tokenObj = JsonSerializer.Deserialize<JsonElement>(body, _jsonOptions);
            return tokenObj.GetProperty("token").GetString();
        }

        public async Task<bool> RegisterAsync(RegisterInput input)
        {
            var client = _httpClientFactory.CreateClient("PetShelterApi");

            var json = JsonSerializer.Serialize(input);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/auth/register", content);
            return response.IsSuccessStatusCode;
        }
    }
}