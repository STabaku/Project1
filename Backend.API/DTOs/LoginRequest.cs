using System.ComponentModel.DataAnnotations;
using Org.BouncyCastle.Ocsp;

namespace PharmacyEmergencySystem.DTOs
{
   public class LoginRequest
{
    public required string EmailOrNumber { get; set; }
    public required string Password { get; set; }
}

}
