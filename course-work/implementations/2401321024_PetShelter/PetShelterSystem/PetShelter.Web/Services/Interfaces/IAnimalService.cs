using PetShelter.Web.DTOs.Request;
using PetShelter.Web.DTOs.Response;

namespace PetShelter.Web.Services.Interfaces
{
    public interface IAnimalService
    {
        Task<PagedResult<AnimalOutput>> GetAllAsync(
            string? species,
            bool? isVaccinated,
            string? sortBy,
            bool ascending,
            int page,
            int pageSize);

        Task<AnimalOutput?> GetByIdAsync(int id);
        Task<bool> CreateAsync(AnimalInput input);
        Task<bool> UpdateAsync(int id, AnimalInput input);
        Task<bool> DeleteAsync(int id);
    }
}