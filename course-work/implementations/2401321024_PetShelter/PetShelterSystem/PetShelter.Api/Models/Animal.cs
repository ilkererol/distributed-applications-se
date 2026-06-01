using System.ComponentModel.DataAnnotations;

namespace PetShelter.Api.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Species { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; }
        public decimal Weight { get; set; }
        public bool IsVaccinated { get; set; }
        public DateTime ArrivalDate { get; set; }
    }
}
