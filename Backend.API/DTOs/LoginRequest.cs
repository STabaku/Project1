using System.ComponentModel.DataAnnotations;

namespace PharmacyEmergencySystem.DTOs
{
    public class LoginRequest
    {
        [Required]
        public string EmailOrNumber { get; set; } = null!;
    }
}
