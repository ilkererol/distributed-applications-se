namespace PetShelter.Web.DTOs.Request
{
    public class ApplicationInput
    {
        public int AnimalId { get; set; }
        public int AdopterId { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string? Notes { get; set; }
        public decimal ProcessingFee { get; set; }
    }
}