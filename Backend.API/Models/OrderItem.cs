using System.Text.Json.Serialization;

namespace Backend.API.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int? OrderId { get; set; }

        public string MedicineName { get; set; } = null!;
        public string PharmacyName { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        [JsonIgnore]
        public Order? Order { get; set; }
    }
}
