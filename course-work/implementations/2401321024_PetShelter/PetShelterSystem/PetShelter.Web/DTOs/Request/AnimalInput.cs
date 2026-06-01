namespace PetShelter.Web.DTOs.Request
{
    public class AnimalInput
    {
        public string Name { get; set; } = string.Empty;
        public string Species { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; }
        public decimal Weight { get; set; }
        public bool IsVaccinated { get; set; }
        public DateTime ArrivalDate { get; set; }
    }
}