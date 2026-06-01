namespace PetShelter.Web.DTOs.Response
{
    public class ApplicationOutput
    {
        public int Id { get; set; }
        public int AnimalId { get; set; }
        public string Species { get; set; } = string.Empty;
        public int AdopterId { get; set; }
        public string AdopterName { get; set; } = string.Empty;
        public DateTime ApplicationDate { get; set; }
        public string? Notes { get; set; }
        public decimal ProcessingFee { get; set; }
    }
}