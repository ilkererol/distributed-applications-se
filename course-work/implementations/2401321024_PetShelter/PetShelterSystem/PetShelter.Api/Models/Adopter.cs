using System.ComponentModel.DataAnnotations;

namespace PetShelter.Api.Models
{
    public class Adopter
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; }
        public decimal BudgetContribution { get; set; }

    }
}
