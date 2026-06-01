using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetShelter.Web.DTOs.Request;
using PetShelter.Web.Services.Interfaces;

namespace PetShelter.Web.Pages.Adopters
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IAdopterService _adopterService;

        public EditModel(IAdopterService adopterService)
        {
            _adopterService = adopterService;
        }

        [BindProperty]
        public AdopterInput Input { get; set; } = new();

        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var adopter = await _adopterService.GetByIdAsync(id);

            if (adopter == null)
            {
                ErrorMessage = "Осиновителят не е намерен.";
                return Page();
            }

            Input = new AdopterInput
            {
                FirstName = adopter.FirstName,
                LastName = adopter.LastName,
                Phone = adopter.Phone,
                RegistrationDate = adopter.RegistrationDate,
                BudgetContribution = adopter.BudgetContribution
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var success = await _adopterService.UpdateAsync(id, Input);

            if (!success)
            {
                ErrorMessage = "Грешка при обновяване.";
                return Page();
            }

            return RedirectToPage("/Adopters/Index");
        }
    }
}