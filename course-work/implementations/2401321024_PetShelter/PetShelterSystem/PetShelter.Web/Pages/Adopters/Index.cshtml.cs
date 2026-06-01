using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetShelter.Web.DTOs.Response;
using PetShelter.Web.Services.Interfaces;

namespace PetShelter.Web.Pages.Adopters
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAdopterService _adopterService;

        public IndexModel(IAdopterService adopterService)
        {
            _adopterService = adopterService;
        }

        public List<AdopterOutput> Adopters { get; set; } = new();
        public int TotalPages { get; set; }
        public int Page { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public string? LastName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Phone { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortBy { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool Ascending { get; set; } = true;

        public async Task OnGetAsync(int page = 1)
        {
            Page = page;

            var result = await _adopterService.GetAllAsync(
                LastName, Phone, SortBy, Ascending, page, 10);

            Adopters = result.Items;
            TotalPages = (int)Math.Ceiling(result.TotalCount / 10.0);
        }
    }
}