using PetShelter.Api.DTOs.RequestDTOs;
using PetShelter.Api.DTOs.ResponseDTOs;

namespace PetShelter.Api.Services.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterInput input);
        Task<string> LoginAsync(LoginInput input);
    }
}