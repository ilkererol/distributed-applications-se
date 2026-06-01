using PetShelter.Api.DTOs.RequestDTOs;
using PetShelter.Api.DTOs.ResponseDTOs;

namespace PetShelter.Api.Services.Interfaces
{

    public interface IApplicationService
    {
        Task<(IEnumerable<ApplicationOutput> Items, int totalCount)> GetAllAsync(
            string? species,
            DateTime? applicationDate,
            string? sortBy,
            bool ascending,
            int page,
            int pageSize);
        Task<ApplicationOutput> GetByIdAsync(int id);
        Task<ApplicationOutput> CreateAsync(ApplicationInput input);
        Task UpdateAsync(int id, ApplicationInput input);
        Task DeleteAsync(int id);
    }
}
