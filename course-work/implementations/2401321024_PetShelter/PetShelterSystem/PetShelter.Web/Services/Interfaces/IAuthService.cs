using PetShelter.Web.DTOs.Request;

namespace PetShelter.Web.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(LoginInput input);
        Task<bool> RegisterAsync(RegisterInput input);
    }
}