using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PetShelter.Api.Data;
using PetShelter.Api.DTOs.RequestDTOs;
using PetShelter.Api.DTOs.ResponseDTOs;
using PetShelter.Api.Models;
using PetShelter.Api.Services.Interfaces;

namespace PetShelter.Api.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly AppDbContext _context;
        private readonly IValidator<AnimalInput> _validator;

        public AnimalService(AppDbContext context, IValidator<AnimalInput> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<(IEnumerable<AnimalOutput> Items, int totalCount)> GetAllAsync(
            string? species,
            bool? isVaccinated,
            string? sortBy,
            bool ascending,
            int page,
            int pageSize)
        {
            var query = _context.Animals.AsQueryable();
            if (!string.IsNullOrEmpty(species))
            {
                query = query.Where(a => a.Species.ToLower().Contains(species.ToLower()));
            }

            if (isVaccinated.HasValue)
            {
                query = query.Where(a => a.IsVaccinated == isVaccinated.Value);
            }

            query = sortBy?.ToLower() switch
            {
                "weight" => ascending ? query.OrderBy(a => a.Weight) : query.OrderByDescending(a => a.Weight),
                "arrivaldate" => ascending ? query.OrderBy(a => a.ArrivalDate) : query.OrderByDescending(a => a.ArrivalDate),
                _ => ascending ? query.OrderBy(a => a.Name ?? "Unnamed") : query.OrderByDescending(a => a.Name ?? "Unnamed")
            };

            var totalItems = await query.CountAsync();
            var animals = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(a => new AnimalOutput
                {
                    Id = a.Id,
                    Name = a.Name,
                    Species = a.Species,
                    BirthDate = a.BirthDate,
                    Weight = a.Weight,
                    IsVaccinated = a.IsVaccinated,
                    ArrivalDate = a.ArrivalDate
                }).ToListAsync();

            return (animals , totalItems);
        }

        public async Task<AnimalOutput> GetByIdAsync (int id)
        {
            var animal = await _context.Animals.FindAsync(id);
            if (animal == null)
            {
                throw new KeyNotFoundException($"Animal with ID {id} was not found.");
            }
            return MapToOutput(animal);
        }

        public async Task<AnimalOutput> CreateAsync(AnimalInput input)
        {
            await ValidateInputAsync(input);

            var animal = new Animal
            {
                Name = input.Name,
                Species = input.Species,
                BirthDate = input.BirthDate,
                Weight = input.Weight,
                IsVaccinated=input.IsVaccinated,
                ArrivalDate = input.ArrivalDate
            };
            _context.Animals.Add(animal);
            await _context.SaveChangesAsync();

            return MapToOutput(animal);
        }

        public async Task UpdateAsync (int id, AnimalInput input)
        {
            var animal = await _context.Animals.FindAsync(id);
            if (animal == null)
            {
                throw new KeyNotFoundException($"Animal with ID {id} was not found.");
            }

            await ValidateInputAsync(input);

            animal.Name = input.Name;
            animal.Species = input.Species;
            animal.BirthDate = input.BirthDate;
            animal.Weight = input.Weight;
            animal.IsVaccinated = input.IsVaccinated;
            animal.ArrivalDate = input.ArrivalDate;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var animal = await _context.Animals.FindAsync(id);
            if (animal == null)
            {
                throw new KeyNotFoundException($"Animal with ID {id} was not found.");
            }
            _context.Animals.Remove(animal);
            await _context.SaveChangesAsync();
        }

        private async Task ValidateInputAsync(AnimalInput input)
        {
            var validationResult = await _validator.ValidateAsync(input);
            if(!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
        }

        private AnimalOutput MapToOutput(Animal animal)
        {
            return new AnimalOutput
            {
                Id = animal.Id,
                Name = animal.Name,
                Species = animal.Species,
                BirthDate = animal.BirthDate,
                Weight = animal.Weight,
                IsVaccinated = animal.IsVaccinated,
                ArrivalDate = animal.ArrivalDate
            };
        }
    }
}
