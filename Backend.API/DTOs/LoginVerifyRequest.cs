// namespace PharmacyEmergencySystem.DTOs
// {
//     public class LoginVerifyRequest
//     {
//         // public string EmailOrNumber { get; set; } = null!;
//         // public string OTP { get; set; } = null!;
//         public string Email { get; set; }
//     public string OTP { get; set; }
//     }
// }


namespace PharmacyEmergencySystem.DTOs
{
    public class LoginVerifyRequest
    {
        public required string  Email { get; set; }
        public  required string OTP { get; set; }
    }
}
