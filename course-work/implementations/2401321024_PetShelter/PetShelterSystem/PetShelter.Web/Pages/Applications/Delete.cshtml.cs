using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetShelter.Web.Services.Interfaces;

namespace PetShelter.Web.Pages.Applications
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly IApplicationService _applicationService;

        public DeleteModel(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        public string? AdopterName { get; set; }
        public string? Species { get; set; }
        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var app = await _applicationService.GetByIdAsync(id);

            if (app == null)
            {
                ErrorMessage = "Заявлението не е намерено.";
                return Page();
            }

            AdopterName = app.AdopterName;
            Species = app.Species;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var success = await _applicationService.DeleteAsync(id);

            if (!success)
            {
                ErrorMessage = "Грешка при изтриване.";
                return Page();
            }

            return RedirectToPage("/Applications/Index");
        }
    }
}