using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetShelter.Web.DTOs.Request;
using PetShelter.Web.Services.Interfaces;

namespace PetShelter.Web.Pages.Adopters
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IAdopterService _adopterService;

        public CreateModel(IAdopterService adopterService)
        {
            _adopterService = adopterService;
        }

        [BindProperty]
        public AdopterInput Input { get; set; } = new();

        public string? ErrorMessage { get; set; }

        public void OnGet()
        {
            Input.RegistrationDate = DateTime.Today;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var success = await _adopterService.CreateAsync(Input);

            if (!success)
            {
                ErrorMessage = "Грешка при добавяне на осиновителя.";
                return Page();
            }

            return RedirectToPage("/Adopters/Index");
        }
    }
}