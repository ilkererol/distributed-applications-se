using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetShelter.Web.DTOs.Request;
using PetShelter.Web.Services.Interfaces;

namespace PetShelter.Web.Pages.Animals
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IAnimalService _animalService;

        public CreateModel(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        [BindProperty]
        public AnimalInput Input { get; set; } = new();

        public string? ErrorMessage { get; set; }

        public void OnGet()
        {
            Input.ArrivalDate = DateTime.Today;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var success = await _animalService.CreateAsync(Input);

            if (!success)
            {
                ErrorMessage = "Грешка при добавяне на животното.";
                return Page();
            }

            return RedirectToPage("/Animals/Index");
        }
    }
}