namespace Backend.API.Models
{
    public class Pharmacy
    {
        public int Id { get; set; }

        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string Phone { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
