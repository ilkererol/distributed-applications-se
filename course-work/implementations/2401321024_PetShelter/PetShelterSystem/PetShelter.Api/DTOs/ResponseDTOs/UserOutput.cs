namespace PetShelter.Api.DTOs.ResponseDTOs
{
    /// <summary>
    /// Output model representing a registered user.
    /// </summary>
    public class UserOutput
    {
        /// <summary>
        /// Unique identifier of the user.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The user's username.
        /// </summary>
        public string Username { get; set; } = string.Empty;
        /// <summary>
        /// The user's email address.
        /// </summary>
        public string Email { get; set; } = string.Empty;
    }
}