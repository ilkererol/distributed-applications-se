namespace PetShelter.Api.DTOs.RequestDTOs
{
    /// <summary>
    /// Input model for creating or updating an adoption application.
    /// </summary>
    public class ApplicationInput
    {
        /// <summary>
        /// The identifier of the animal being adopted.
        /// </summary>
        public int AnimalId { get; set; }
        /// <summary>
        /// The identifier of the adopter submitting the application.
        /// </summary>
        public int AdopterId { get; set; }
        /// <summary>
        /// The date the application was submitted.
        /// </summary>
        public DateTime ApplicationDate { get; set; }
        /// <summary>
        /// Optional notes or remarks about the application. Maximum 500 characters.
        /// </summary>
        public string? Notes { get; set; }
        /// <summary>
        /// The processing fee for the adoption application. Must be non-negative.
        /// </summary>
        public decimal ProcessingFee { get; set; }
    }
}
