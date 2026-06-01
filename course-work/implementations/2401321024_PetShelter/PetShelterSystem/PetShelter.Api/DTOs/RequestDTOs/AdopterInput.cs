namespace PetShelter.Api.DTOs.RequestDTOs
{
    /// <summary>
    /// Input model for creating or updating an adopter.
    /// </summary>
    public class AdopterInput
    {
        /// <summary>
        /// The adopter's first name. Maximum 20 characters.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;
        /// <summary>
        /// The adopter's last name. Maximum 20 characters.
        /// </summary>
        public string LastName { get; set; } = string.Empty;
        /// <summary>
        /// The adopter's phone number. Maximum 15 characters.
        /// </summary>
        public string Phone { get; set; } = string.Empty;
        /// <summary>
        /// The date the adopter registered in the system.
        /// </summary>
        public DateTime RegistrationDate { get; set; }
        /// <summary>
        /// The adopter's financial contribution towards the adoption. Must be non-negative.
        /// </summary>
        public decimal BudgetContribution { get; set; }
    }
}
