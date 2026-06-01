using FluentValidation;
using PetShelter.Api.DTOs.RequestDTOs;

namespace PetShelter.Api.Validators
{
    public class LoginValidator : AbstractValidator<LoginInput>
    {
        public LoginValidator()
        {
            RuleFor(u => u.Username)
                .NotEmpty().WithMessage("Username is required.");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}