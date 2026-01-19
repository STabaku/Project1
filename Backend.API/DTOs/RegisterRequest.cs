namespace PharmacyEmergencySystem.DTOs
{
    public class RegisterRequest
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Number { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
    }
}
