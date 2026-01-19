namespace PharmacyEmergencySystem.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Number { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string OTP { get; set; } // Temporary OTP
        public bool IsVerified { get; set; } = false;
        public string Role { get; set; } = "User"; // Default: User
    }
}
