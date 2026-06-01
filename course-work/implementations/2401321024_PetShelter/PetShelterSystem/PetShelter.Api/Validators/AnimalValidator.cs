using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PetShelter.Api.DTOs.RequestDTOs;



namespace PetShelter.Api.Validators
{
    public class AnimalValidator : AbstractValidator<AnimalInput>
    {
        public AnimalValidator()
        {
            RuleFor(a => a.Name)
                .MaximumLength(15).WithMessage("The name can't be longer than 15 symbols");

            RuleFor(a => a.Species)
                .NotEmpty().WithMessage("Please type in the species of the animal.")
                .MaximumLength(20).WithMessage("The species name can't be longer than 20 symbols");

            RuleFor(a => a.Weight)
                .GreaterThan(0).WithMessage("Weight must be greater than 0 kg.");

            RuleFor(a => a.BirthDate)
                .LessThanOrEqualTo(DateTime.Today)
                .WithMessage("Birth date cannot be in the future.")
                .When(a => a.BirthDate.HasValue);

            RuleFor(a => a.ArrivalDate)
                .NotEmpty().WithMessage("Arrival date is required.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Arrival date cannot be in the future.");
        }
    }
}
