using System.ComponentModel.DataAnnotations;

namespace PharmacyEmergencySystem.DTOs
{
  public class RegisterRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Number { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public int? PharmacyId { get; set; }
}

    }

