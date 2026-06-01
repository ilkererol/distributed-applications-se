using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetShelter.Web.DTOs.Response;
using PetShelter.Web.Services.Interfaces;

namespace PetShelter.Web.Pages.Applications
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IApplicationService _applicationService;

        public IndexModel(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        public List<ApplicationOutput> Applications { get; set; } = new();
        public int TotalPages { get; set; }
        public int Page { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public string? Species { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? ApplicationDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortBy { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool Ascending { get; set; } = true;

        public async Task OnGetAsync(int page = 1)
        {
            Page = page;

            var result = await _applicationService.GetAllAsync(
                Species, ApplicationDate, SortBy, Ascending, page, 10);

            Applications = result.Items;
            TotalPages = (int)Math.Ceiling(result.TotalCount / 10.0);
        }
    }
}