using FluentValidation;
using PetShelter.Api.DTOs.RequestDTOs;


namespace PetShelter.Api.Validators
{
    public class ApplicationValidator : AbstractValidator<ApplicationInput>
    {
        public ApplicationValidator()
        {
            RuleFor(aa => aa.AnimalId)
                .GreaterThan(0).WithMessage("A valid Animal ID is required.");

            RuleFor(aa => aa.AdopterId)
                .GreaterThan(0).WithMessage("A valid Adopter ID is required");


            RuleFor(aa => aa.ApplicationDate)
                .NotEmpty().WithMessage("Please type in the application date.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Please type in a valid date.");

            RuleFor(aa => aa.Notes)
                .MaximumLength(500).WithMessage("Notes can't be longer than 500 symbols.");

            RuleFor(aa => aa.ProcessingFee)
                .GreaterThan(0).WithMessage("Processing fee must be greater than 0.00.");



        }
    }
}
