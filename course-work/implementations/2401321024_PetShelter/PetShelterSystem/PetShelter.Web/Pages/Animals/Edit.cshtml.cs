using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetShelter.Web.DTOs.Request;
using PetShelter.Web.Services.Interfaces;

namespace PetShelter.Web.Pages.Animals
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IAnimalService _animalService;

        public EditModel(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        [BindProperty]
        public AnimalInput Input { get; set; } = new();

        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var animal = await _animalService.GetByIdAsync(id);

            if (animal == null)
            {
                ErrorMessage = "Животното не е намерено.";
                return Page();
            }

            Input = new AnimalInput
            {
                Name = animal.Name ?? string.Empty,
                Species = animal.Species,
                BirthDate = animal.BirthDate,
                Weight = animal.Weight,
                IsVaccinated = animal.IsVaccinated,
                ArrivalDate = animal.ArrivalDate
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var success = await _animalService.UpdateAsync(id, Input);

            if (!success)
            {
                ErrorMessage = "Грешка при обновяване.";
                return Page();
            }

            return RedirectToPage("/Animals/Index");
        }
    }
}