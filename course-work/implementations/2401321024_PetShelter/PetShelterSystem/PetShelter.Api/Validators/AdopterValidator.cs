using FluentValidation;
using PetShelter.Api.DTOs.RequestDTOs;

namespace PetShelter.Api.Validators

{
    public class AdopterValidator : AbstractValidator<AdopterInput>
    {
        public AdopterValidator()
        {
            RuleFor(a => a.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(20).WithMessage("First name can't be longer than 20 symbols.");

            RuleFor(a => a.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(20).WithMessage("Last name can't be longer than 20 symbols.");

            RuleFor(a => a.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .MaximumLength(15).WithMessage("Phone number can't be longer than 15 symbols.");

            RuleFor(a => a.BudgetContribution)
                .GreaterThanOrEqualTo(0).WithMessage("The contribution can't be negative value.");

            RuleFor(a => a.RegistrationDate)
                .NotEmpty().WithMessage("Registration date is required.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Registration date can't be in the future.");
        }
    }
}
