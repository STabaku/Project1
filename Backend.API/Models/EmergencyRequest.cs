namespace Backend.API.Models
{
public class EmergencyRequest
{
    public int Id { get; set; }

    public int PharmacyId { get; set; }     

    public string MedicineName { get; set; } = null!;
    public int Quantity { get; set; }
    public string Address { get; set; } = null!;

    public string Status { get; set; } = "Pending"; 
    public int ClientId { get; set; } 

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
}
