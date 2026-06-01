namespace PetShelter.Api.DTOs.RequestDTOs
{
    /// <summary>
    /// Input model for user login.
    /// </summary>
    public class LoginInput
    {
        /// <summary>
        /// The user's username.
        /// </summary>
        public string Username { get; set; } = string.Empty;
        /// <summary>
        /// The user's password.
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}