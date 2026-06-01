using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetShelter.Web.Services.Interfaces;

namespace PetShelter.Web.Pages.Adopters
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly IAdopterService _adopterService;

        public DeleteModel(IAdopterService adopterService)
        {
            _adopterService = adopterService;
        }

        public string? AdopterName { get; set; }
        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var adopter = await _adopterService.GetByIdAsync(id);

            if (adopter == null)
            {
                ErrorMessage = "Осиновителят не е намерен.";
                return Page();
            }

            AdopterName = $"{adopter.FirstName} {adopter.LastName}";
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var success = await _adopterService.DeleteAsync(id);

            if (!success)
            {
                ErrorMessage = "Грешка при изтриване.";
                return Page();
            }

            return RedirectToPage("/Adopters/Index");
        }
    }
}