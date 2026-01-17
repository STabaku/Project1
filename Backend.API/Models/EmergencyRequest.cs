public class EmergencyRequest
{
    public string Medication { get; set; }
    public string Urgency { get; set; }
    public string Status { get; set; } = "Pending";
}