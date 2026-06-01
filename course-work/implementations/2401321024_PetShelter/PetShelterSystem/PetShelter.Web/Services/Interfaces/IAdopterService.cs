using PetShelter.Web.DTOs.Request;
using PetShelter.Web.DTOs.Response;

namespace PetShelter.Web.Services.Interfaces
{
    public interface IAdopterService
    {
        Task<PagedResult<AdopterOutput>> GetAllAsync(
            string? lastName,
            string? phone,
            string? sortBy,
            bool ascending,
            int page,
            int pageSize);

        Task<AdopterOutput?> GetByIdAsync(int id);
        Task<bool> CreateAsync(AdopterInput input);
        Task<bool> UpdateAsync(int id, AdopterInput input);
        Task<bool> DeleteAsync(int id);
    }
}