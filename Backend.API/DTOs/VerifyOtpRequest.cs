using System.ComponentModel.DataAnnotations;

namespace PharmacyEmergencySystem.DTOs
{
    public class VerifyOTPRequest
    {
        [Required]
        public string EmailOrNumber { get; set; } = null!;

        [Required]
        public string OTP { get; set; } = null!;
    }
}
