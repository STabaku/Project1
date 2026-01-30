using System.ComponentModel.DataAnnotations;
using Org.BouncyCastle.Ocsp;

namespace PharmacyEmergencySystem.DTOs
{
    public class LoginRequest
    {
        // [Required]
        // public string EmailOrNumber { get; set; } = null!;
         public required string Email { get; set; } // email ose number
    public required string Password { get; set; }
    }
}
