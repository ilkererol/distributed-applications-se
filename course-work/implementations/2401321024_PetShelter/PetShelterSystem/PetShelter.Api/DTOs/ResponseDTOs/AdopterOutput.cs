namespace PetShelter.Api.DTOs.ResponseDTOs
{
    /// <summary>
    /// Output model representing an adopter.
    /// </summary>
    public class AdopterOutput
    {
        /// <summary>
        /// Unique identifier of the adopter.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The adopter's first name.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;
        /// <summary>
        /// The adopter's last name.
        /// </summary>
        public string LastName { get; set; } = string.Empty;
        /// <summary>
        /// The adopter's phone number.
        /// </summary>
        public string Phone { get; set; } = string.Empty;
        /// <summary>
        /// The date the adopter registered in the system.
        /// </summary>
        public DateTime RegistrationDate { get; set; }
        /// <summary>
        /// The adopter's financial contribution.
        /// </summary>
        public decimal BudgetContribution { get; set; }
    }
}
