using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PetShelter.Api.Data;
using PetShelter.Api.DTOs.RequestDTOs;
using PetShelter.Api.DTOs.ResponseDTOs;
using PetShelter.Api.Models;
using PetShelter.Api.Services.Interfaces;

namespace PetShelter.Api.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly AppDbContext _context;
        private readonly IValidator<ApplicationInput> _validator;

        public ApplicationService(AppDbContext context, IValidator<ApplicationInput> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<(IEnumerable<ApplicationOutput> Items, int totalCount)> GetAllAsync(
            string? species,
            DateTime? applicationDate,
            string? sortBy,
            bool ascending,
            int page,
            int pageSize)
        {
            var query = _context.AdoptionApplications
                .Include(aa => aa.Animal)
                .Include(aa => aa.Adopter)
                .AsQueryable();

            if (!string.IsNullOrEmpty(species))
            {
                query = query.Where(aa => aa.Animal.Species.ToLower().Contains(species.ToLower()));
            }

            if (applicationDate.HasValue)
            {
                query = query.Where(aa => aa.ApplicationDate.Date == applicationDate.Value.Date);
            }

            query = sortBy?.ToLower() switch
            {
                "date" => ascending ? query.OrderBy(aa => aa.ApplicationDate) : query.OrderByDescending(aa => aa.ApplicationDate),
                "fee" => ascending ? query.OrderBy(aa => aa.ProcessingFee) : query.OrderByDescending(aa => aa.ProcessingFee),
                _ => ascending ? query.OrderBy(aa => aa.Id) : query.OrderByDescending(aa => aa.Id)
            };

            var totalItems = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(aa => new ApplicationOutput
                {
                    Id = aa.Id,
                    AnimalId = aa.AnimalId,
                    Species = aa.Animal.Species,
                    AdopterId = aa.AdopterId,
                    AdopterName = aa.Adopter.FirstName + " " + aa.Adopter.LastName,
                    ApplicationDate = aa.ApplicationDate,
                    Notes = aa.Notes,
                    ProcessingFee = aa.ProcessingFee
                })
                .ToListAsync();

            return (items, totalItems);
        }

        public async Task<ApplicationOutput> GetByIdAsync(int id)
        {
            var application = await _context.AdoptionApplications
                .Include(aa => aa.Animal)
                .Include(aa => aa.Adopter)
                .FirstOrDefaultAsync(aa => aa.Id == id);

            if (application == null)
            {
                throw new KeyNotFoundException($"Application with ID {id} was not found.");
            }

            return MapToOutput(application);
        }

        public async Task<ApplicationOutput> CreateAsync(ApplicationInput input)
        {
            await ValidateInputAsync(input);

            var animalExists = await _context.Animals.AnyAsync(a => a.Id == input.AnimalId);
            var adopterExists = await _context.Adopters.AnyAsync(a => a.Id == input.AdopterId);

            if (!animalExists || !adopterExists)
            {
                throw new KeyNotFoundException("The specified Animal or Adopter does not exist.");
            }

            var application = new AdoptionApplication
            {
                AnimalId = input.AnimalId,
                AdopterId = input.AdopterId,
                ApplicationDate = input.ApplicationDate,
                Notes = input.Notes,
                ProcessingFee = input.ProcessingFee
            };

            _context.AdoptionApplications.Add(application);
            await _context.SaveChangesAsync();

            await _context.Entry(application).Reference(aa => aa.Animal).LoadAsync();
            await _context.Entry(application).Reference(aa => aa.Adopter).LoadAsync();

            return MapToOutput(application);
        }

        public async Task UpdateAsync(int id, ApplicationInput input)
        {
            var application = await _context.AdoptionApplications.FindAsync(id);
            if (application == null)
            {
                throw new KeyNotFoundException($"Application with ID {id} was not found.");
            }

            await ValidateInputAsync(input);

            var animalExists = await _context.Animals.AnyAsync(a => a.Id == input.AnimalId);
            var adopterExists = await _context.Adopters.AnyAsync(a => a.Id == input.AdopterId);

            if (!animalExists || !adopterExists)
            {
                throw new KeyNotFoundException("The specified Animal or Adopter does not exist.");
            }

            application.AnimalId = input.AnimalId;
            application.AdopterId = input.AdopterId;
            application.ApplicationDate = input.ApplicationDate;
            application.Notes = input.Notes;
            application.ProcessingFee = input.ProcessingFee;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var application = await _context.AdoptionApplications.FindAsync(id);
            if (application == null)
            {
                throw new KeyNotFoundException($"Application with ID {id} was not found.");
            }

            _context.AdoptionApplications.Remove(application);
            await _context.SaveChangesAsync();
        }

        private async Task ValidateInputAsync(ApplicationInput input)
        {
            var validationResult = await _validator.ValidateAsync(input);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
        }

        private ApplicationOutput MapToOutput(AdoptionApplication aa)
        {
            return new ApplicationOutput
            {
                Id = aa.Id,
                AnimalId = aa.AnimalId,
                Species = aa.Animal?.Species ?? "Unknown",
                AdopterId = aa.AdopterId,
                AdopterName = aa.Adopter != null
                ? aa.Adopter.FirstName + " " + aa.Adopter.LastName
                : "Unknown",
                ApplicationDate = aa.ApplicationDate,
                Notes = aa.Notes,
                ProcessingFee = aa.ProcessingFee
            };
        }
    }
}