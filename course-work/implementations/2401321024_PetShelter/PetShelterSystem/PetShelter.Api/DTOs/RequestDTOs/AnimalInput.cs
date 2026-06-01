namespace PetShelter.Api.DTOs.RequestDTOs
{
    /// <summary>
    /// Input model for creating or updating an animal.
    /// </summary>
    public class AnimalInput
    {
        /// <summary>
        /// The animal's name. Maximum 15 characters.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The animal's species. Maximum 20 characters.
        /// </summary>
        public string Species { get; set; } = string.Empty;
        /// <summary>
        /// The animal's date of birth. Optional.
        /// </summary>
        public DateTime? BirthDate { get; set; }
        /// <summary>
        /// The animal's weight in kilograms. Must be greater than zero.
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
