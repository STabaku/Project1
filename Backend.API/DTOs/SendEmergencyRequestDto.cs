using System.ComponentModel.DataAnnotations;

namespace Backend.API.DTOs
{
    public class SendEmergencyRequestDto
    {
        [Required]
        public string MedicineName { get; set; } = null!;

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string Address { get; set; } = null!;
    }
}
