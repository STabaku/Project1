namespace PharmacyEmergencySystem.DTOs
{
    public class LoginVerifyRequest
    {
        public string EmailOrNumber { get; set; } = null!;
        public string OTP { get; set; } = null!;
    }
}
