namespace PetShelter.Api.DTOs.ResponseDTOs
{
    /// <summary>
    /// Output model representing a shelter animal.
    /// </summary>
    public class AnimalOutput
    {
        /// <summary>
        /// Unique identifier of the animal.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The animal's name.
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// The animal's species (e.g. Dog, Cat).
        /// </summary>
        public string Species { get; set; } = string.Empty;
        /// <summary>
        /// The animal's date of birth. Null if unknown.
        /// </summary>
        public DateTime? BirthDate { get; set; }
        /// <summary>
        /// The animal's weight in kilograms.
        /// </summary>
        public decimal Weight { get; set; }
        /// <summary>
        /// Indicates whether the animal has been vaccinated.
        /// </summary>
        public bool IsVaccinated { get; set; }
        /// <summary>
        /// The date the animal arrived at the shelter.
        /// </summary>
        public DateTime ArrivalDate { get; set; }
    }
}
