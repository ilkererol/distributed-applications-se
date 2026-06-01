using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetShelter.Web.DTOs.Request;
using PetShelter.Web.DTOs.Response;
using PetShelter.Web.Services.Interfaces;

namespace PetShelter.Web.Pages.Applications
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IApplicationService _applicationService;
        private readonly IAnimalService _animalService;
        private readonly IAdopterService _adopterService;

        public CreateModel(
            IApplicationService applicationService,
            IAnimalService animalService,
            IAdopterService adopterService)
        {
            _applicationService = applicationService;
            _animalService = animalService;
            _adopterService = adopterService;
        }

        [BindProperty]
        public ApplicationInput Input { get; set; } = new();

        public List<AnimalOutput> Animals { get; set; } = new();
        public List<AdopterOutput> Adopters { get; set; } = new();
        public string? ErrorMessage { get; set; }

        public async Task OnGetAsync()
        {
            Input.ApplicationDate = DateTime.Today;
            await LoadDropdownsAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var success = await _applicationService.CreateAsync(Input);

            if (!success)
            {
                ErrorMessage = "Грешка при добавяне на заявлението.";
                await LoadDropdownsAsync();
                return Page();
            }

            return RedirectToPage("/Applications/Index");
        }

        private async Task LoadDropdownsAsync()
        {
            var animals = await _animalService.GetAllAsync(
                null, null, null, true, 1, 100);
            Animals = animals.Items;

            var adopters = await _adopterService.GetAllAsync(
                null, null, null, true, 1, 100);
            Adopters = adopters.Items;
        }
    }
}