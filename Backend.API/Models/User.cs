namespace Backend.API.Models

{
 public class User
{
    public int Id { get; set; }   // NOT nullable

    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;

    // Optional fields → MUST be nullable
    public string? Number { get; set; }
    public string? Location { get; set; }
    public string? Gender { get; set; }
    public string? Role { get; set; }

    // OTP fields → MUST be nullable
    public string? OTP { get; set; }
    public DateTime? OtpExpiry { get; set; }

    public bool IsVerified { get; set; }
    public int? Age { get; set; }
}
}
