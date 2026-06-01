using PetShelter.Api.DTOs.RequestDTOs;
using PetShelter.Api.DTOs.ResponseDTOs;


namespace PetShelter.Api.Services.Interfaces
{
    public interface IAnimalService
    {
        Task<(IEnumerable<AnimalOutput> Items, int totalCount)> GetAllAsync(
            string? species,
            bool? isVaccinated,
            string? sortBy,
            bool ascending,
            int page,
            int pageSize);
        Task<AnimalOutput> GetByIdAsync(int id);
        Task<AnimalOutput> CreateAsync(AnimalInput input);
        Task UpdateAsync(int id, AnimalInput input);
        Task DeleteAsync(int id);
    }
}
