using System.ComponentModel.DataAnnotations;

namespace PharmacyEmergencySystem.DTOs
{
    public class RegisterRequest
    {
        // [Required]
        // public string Name { get; set; } = null!;

        // public string? Location { get; set; }

        // public string? Number { get; set; }

        // [EmailAddress]
        // public string? Email { get; set; }

        // public int Age { get; set; }

        // [Required]
        // public string Role { get; set; } = null!;
        // public int? PharmacyId { get; set; }

         public required string Name { get; set; }
    public required string Email { get; set; }
    public required string? Number { get; set; }
    public required string Password { get; set; }
    public string Role { get; set; }
    public int? PharmacyId { get; set; }

    }
}
