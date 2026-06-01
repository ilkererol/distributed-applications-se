namespace PetShelter.Api.DTOs.RequestDTOs
{
    /// <summary>
    /// Input model for user registration.
    /// </summary>
    public class RegisterInput
    {
        /// <summary>
        /// The desired username. Between 3 and 50 characters.
        /// </summary>
        public string Username { get; set; } = string.Empty;
        /// <summary>
        /// The user's email address. Maximum 50 characters.
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// The desired password. Between 6 and 100 characters.
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}