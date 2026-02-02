namespace Backend.API.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string Status { get; set; } = "Request Sent";

        public List<OrderItem> Items { get; set; } = new();
    }
}
