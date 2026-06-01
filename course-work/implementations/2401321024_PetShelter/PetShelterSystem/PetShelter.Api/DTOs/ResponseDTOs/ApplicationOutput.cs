namespace PetShelter.Api.DTOs.ResponseDTOs
{
    /// <summary>
    /// Output model representing an adoption application.
    /// </summary>
    public class ApplicationOutput
    {
        /// <summary>
        /// Unique identifier of the application.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identifier of the animal being adopted.
        /// </summary>
        public int AnimalId { get; set; }
        /// <summary>
        /// Species of the animal being adopted.
        /// </summary>
        public string Species { get; set; } = string.Empty;
        /// <summary>
        /// Identifier of the adopter submitting the application.
        /// </summary>
        public int AdopterId { get; set; }
        /// <summary>
        /// Full name of the adopter.
        /// </summary>
        public string AdopterName { get; set; } = string.Empty;
        /// <summary>
        /// The date the application was submitted.
        /// </summary>
        public DateTime ApplicationDate { get; set; }
        /// <summary>
        /// Optional notes or remarks about the application.
        /// </summary>
        public string? Notes { get; set; }
        /// <summary>
        /// The processing fee paid for this application.
        /// </summary>
        public decimal ProcessingFee { get; set; }

    }
}
