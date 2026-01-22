namespace PharmacyEmergencySystem.Models
{
    public class EmergencyRequest
    {
        public int Id { get; set; }
        public string MedicineName { get; set; }
        public int Quantity { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }   
    }
}
