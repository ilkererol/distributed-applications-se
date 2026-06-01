using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetShelter.Web.DTOs.Response;
using PetShelter.Web.Services.Interfaces;

namespace PetShelter.Web.Pages.Animals
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAnimalService _animalService;

        public IndexModel(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        public List<AnimalOutput> Animals { get; set; } = new();
        public int TotalPages { get; set; }
        public int Page { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public string? Species { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool? IsVaccinated { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortBy { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool Ascending { get; set; } = true;

        public async Task OnGetAsync(int page = 1)
        {
            Page = page;

            var result = await _animalService.GetAllAsync(
                Species, IsVaccinated, SortBy, Ascending, page, 10);

            Animals = result.Items;
            TotalPages = (int)Math.Ceiling(result.TotalCount / 10.0);
        }
    }
}