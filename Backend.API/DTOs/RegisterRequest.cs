using System.ComponentModel.DataAnnotations;

namespace PharmacyEmergencySystem.DTOs
{
    public class RegisterRequest
    {
        [Required]
        public string Name { get; set; } = null!;

        public string? Location { get; set; }

        public string? Number { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public int Age { get; set; }

        [Required]
        public string Role { get; set; } = null!;
        public int? PharmacyId { get; set; }

    }
}
