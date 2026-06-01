using PetShelter.Api.DTOs.RequestDTOs;
using PetShelter.Api.DTOs.ResponseDTOs;

namespace PetShelter.Api.Services.Interfaces
{
    public interface IAdopterService
    {
        Task<(IEnumerable<AdopterOutput> Items, int totalCount)> GetAllAsync(
            string? lastName,
            string? phone,
            string? sortBy,
            bool ascending,
            int page,
            int pageSize);
        Task<AdopterOutput> GetByIdAsync(int id);
        Task<AdopterOutput> CreateAsync(AdopterInput input);
        Task UpdateAsync(int id, AdopterInput input);
        Task DeleteAsync(int id);

    }
}
