namespace Backend.API.Models

{
 public class User
{
    public int Id { get; set; } 

    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; }

    public string? Number { get; set; }
    public int? PharmacyId { get; set; } 

    public string? Location { get; set; }
   
  public string Role { get; set; } = "User";


    // OTP fields → MUST be nullable
    public string? OTP { get; set; }
    public DateTime? OtpExpiry { get; set; }

    public bool IsVerified { get; set; }
    public int? Age { get; set; }
}
}
