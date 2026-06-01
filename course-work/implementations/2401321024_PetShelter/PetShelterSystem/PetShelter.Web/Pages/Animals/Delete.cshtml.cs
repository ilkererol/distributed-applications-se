using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetShelter.Web.Services.Interfaces;

namespace PetShelter.Web.Pages.Animals
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly IAnimalService _animalService;

        public DeleteModel(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        public string? AnimalName { get; set; }
        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var animal = await _animalService.GetByIdAsync(id);

            if (animal == null)
            {
                ErrorMessage = "Животното не е намерено.";
                return Page();
            }

            AnimalName = animal.Name ?? animal.Species;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var success = await _animalService.DeleteAsync(id);

            if (!success)
            {
                ErrorMessage = "Грешка при изтриване.";
                return Page();
            }

            return RedirectToPage("/Animals/Index");
        }
    }
}