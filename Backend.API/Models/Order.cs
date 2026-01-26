namespace Backend.API.Models
{
    public class Order
    {
        public int Id { get; set; }

        // Relations
        public int UserId { get; set; }

        // Order info
        public string MedicineName { get; set; } = null!;
        public string PharmacyName { get; set; } = null!;
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; } = "Request Sent";
    }
}
