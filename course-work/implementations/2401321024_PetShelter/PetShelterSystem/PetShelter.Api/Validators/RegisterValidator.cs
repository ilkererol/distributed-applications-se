using FluentValidation;
using PetShelter.Api.DTOs.RequestDTOs;

namespace PetShelter.Api.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterInput>
    {
        public RegisterValidator()
        {
            RuleFor(u => u.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(3).WithMessage("Username must be at least 3 symbols long.")
                .MaximumLength(50).WithMessage("Username can't be longer than 50 symbols.");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email address is required.")
                .MaximumLength(50).WithMessage("Email can't be longer than 50 symbols.");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 symbols long.")
                .MaximumLength(100).WithMessage("Password can't be longer than 100 symbols.");
        }
    }
}