using PetShelter.Web.DTOs.Request;
using PetShelter.Web.DTOs.Response;

namespace PetShelter.Web.Services.Interfaces
{
    public interface IApplicationService
    {
        Task<PagedResult<ApplicationOutput>> GetAllAsync(
            string? species,
            DateTime? applicationDate,
            string? sortBy,
            bool ascending,
            int page,
            int pageSize);

        Task<ApplicationOutput?> GetByIdAsync(int id);
        Task<bool> CreateAsync(ApplicationInput input);
        Task<bool> UpdateAsync(int id, ApplicationInput input);
        Task<bool> DeleteAsync(int id);
    }
}