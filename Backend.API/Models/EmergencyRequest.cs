namespace Backend.API.Models
{
    public class EmergencyRequest
    {
        public int Id { get; set; }

        // Relations
        public int UserId { get; set; }

        // Request info
        public string MedicineName { get; set; } = null!;
        public int Quantity { get; set; }
        public string Address { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; } = "Pending";
    }
}
