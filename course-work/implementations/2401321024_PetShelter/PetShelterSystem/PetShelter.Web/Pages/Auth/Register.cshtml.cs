using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetShelter.Web.DTOs.Request;
using PetShelter.Web.Services.Interfaces;

namespace PetShelter.Web.Pages.Auth
{
    public class RegisterModel : PageModel
    {
        private readonly IAuthService _authService;

        public RegisterModel(IAuthService authService)
        {
            _authService = authService;
        }

        [BindProperty]
        public RegisterInput Input { get; set; } = new();

        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var success = await _authService.RegisterAsync(Input);

            if (!success)
            {
                ErrorMessage = "Регистрацията не бе успешна. Опитай с различни данни.";
                return Page();
            }

            SuccessMessage = "Регистрацията е успешна! Можеш да влезеш.";
            return Page();
        }
    }
}