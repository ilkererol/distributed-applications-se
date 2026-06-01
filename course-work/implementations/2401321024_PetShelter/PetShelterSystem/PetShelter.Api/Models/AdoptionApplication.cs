using System.ComponentModel.DataAnnotations;

namespace PetShelter.Api.Models
{
    public class AdoptionApplication
    {
        public int Id { get; set; }
        public int AnimalId { get; set; }
        public int AdopterId { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string? Notes { get; set; }
        public decimal ProcessingFee { get; set; }
        public Animal Animal { get; set; } = null!;
        public Adopter Adopter { get; set; } = null!;
    }
}
