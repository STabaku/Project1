namespace PharmacyEmergencySystem.DTOs

{
    public class VerifyOTPRequest
    {
        public string EmailOrNumber { get; set; }
        public string OTP { get; set; }
    }
}
