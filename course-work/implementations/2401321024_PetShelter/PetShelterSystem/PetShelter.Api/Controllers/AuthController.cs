using Microsoft.AspNetCore.Mvc;
using PetShelter.Api.DTOs.RequestDTOs;
using PetShelter.Api.Services.Interfaces;

namespace PetShelter.Api.Controllers
{
    /// <summary>
    /// Handles user authentication and registration.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// Initializes a new instance of the AuthController class.
        /// </summary>
        /// <param name="authService">Service responsible for authentication operations.</param>
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Registers a new user and returns a JWT token.
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterInput input)
        {
            await _authService.RegisterAsync(input);
            return Ok();
        }

        /// <summary>
        /// Authenticates a user and returns a JWT token.
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginInput input)
        {
            var token = await _authService.LoginAsync(input);
            return Ok(new { token });
        }
    }
}
