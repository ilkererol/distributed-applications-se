using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PetShelter.Api.Data;
using PetShelter.Api.DTOs.RequestDTOs;
using PetShelter.Api.DTOs.ResponseDTOs;
using PetShelter.Api.Models;
using PetShelter.Api.Services.Interfaces;
using System.Numerics;

namespace PetShelter.Api.Services
{
    public class AdopterService : IAdopterService
    {
        private readonly AppDbContext _context;
        private readonly IValidator<AdopterInput> _validator;

        public AdopterService(AppDbContext context, IValidator<AdopterInput> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<(IEnumerable<AdopterOutput> Items, int totalCount)> GetAllAsync(
            string? lastName,
            string? phone,
            string? sortBy,
            bool ascending,
            int page,
            int pageSize)
        {
            var query = _context.Adopters.AsQueryable();
            if (!string.IsNullOrEmpty(lastName))
            {
                query = query.Where(a => a.LastName.ToLower().Contains(lastName.ToLower()));
            }

            if (!string.IsNullOrEmpty(phone))
            {
                query = query.Where(a => a.Phone.Contains(phone));
            }

            query = sortBy?.ToLower() switch
            {
                "registrationdate" => ascending ? query.OrderBy(a => a.RegistrationDate) : query.OrderByDescending(a => a.RegistrationDate),
                "budgetcontribution" => ascending ? query.OrderBy(a => a.BudgetContribution) : query.OrderByDescending(a => a.BudgetContribution),
                _ => ascending ? query.OrderBy(a => a.Id) : query.OrderByDescending(a => a.Id)
            };

            var totalItems = await query.CountAsync();
            var adopters = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(a => new AdopterOutput
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Phone = a.Phone,
                    RegistrationDate = a.RegistrationDate,
                    BudgetContribution = a.BudgetContribution,
                }).ToListAsync();

            return (adopters, totalItems);
        }

        public async Task<AdopterOutput> GetByIdAsync (int id)
        {
            var adopter = await _context.Adopters.FindAsync(id);
            if (adopter == null)
            {
                throw new KeyNotFoundException($"Adopter with ID {id} was not found.");
            }
            return MapToOutput( adopter );
        }

        public async Task<AdopterOutput> CreateAsync(AdopterInput input)
        {
            await ValidateInputAsync(input);

            var adopter = new Adopter
            {
                FirstName = input.FirstName,
                LastName = input.LastName,
                Phone = input.Phone,
                RegistrationDate = input.RegistrationDate,
                BudgetContribution = input.BudgetContribution,
            };
            _context.Adopters.Add( adopter );
            await _context.SaveChangesAsync();

            return MapToOutput(adopter);
        }

        public async Task UpdateAsync (int id, AdopterInput input)
        {
            var adopter = await _context.Adopters.FindAsync(id);
            if (adopter == null)
            {
                throw new KeyNotFoundException($"Adopter with ID {id} was not found.");
            }

            await ValidateInputAsync(input);
            adopter.FirstName = input.FirstName;
            adopter.LastName = input.LastName;
            adopter.Phone = input.Phone;
            adopter.RegistrationDate = input.RegistrationDate;
            adopter.BudgetContribution = input.BudgetContribution;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var adopter = await _context.Adopters.FindAsync(id);
            if (adopter == null)
            {
                throw new KeyNotFoundException($"Adopter with ID {id} was not found.");
            }

            _context.Adopters.Remove( adopter );
            await _context.SaveChangesAsync();
        }

        private async Task ValidateInputAsync(AdopterInput input)
        {
            var validationResult = await _validator.ValidateAsync(input);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
        }

        private AdopterOutput MapToOutput(Adopter adopter)
        {
            return new AdopterOutput
            {
                Id = adopter.Id,
                FirstName = adopter.FirstName,
                LastName = adopter.LastName,
                Phone = adopter.Phone,
                RegistrationDate = adopter.RegistrationDate,
                BudgetContribution = adopter.BudgetContribution
            };
        }
    }
}
