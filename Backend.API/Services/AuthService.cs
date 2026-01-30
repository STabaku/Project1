
// public class AuthService
// {
//     private readonly AppDbContext _context;
//     private readonly OtpService _otpService;
//     private readonly PasswordHasher<User> _hasher = new();

//     public AuthService(AppDbContext context, OtpService otpService)
//     {
//         _context = context;
//         _otpService = otpService;
//     }

//     public async Task<User?> ValidatePasswordAsync(string username, string password)
//     {
//         var user = await _context.Users
//             .FirstOrDefaultAsync(u => u.Email == username || u.Number == username);

//         if (user == null) return null;

//         var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, password);
//         return result == PasswordVerificationResult.Success ? user : null;
//     }

//     public async Task GenerateLoginOtpAsync(User user)
//     {
//         user.OTP = _otpService.GenerateOTP();
//         user.OtpExpiry = DateTime.UtcNow.AddMinutes(5);
//         await _context.SaveChangesAsync();
//     }

//     public async Task<bool> VerifyOtpAsync(User user, string otp)
//     {
//         if (user.OTP != otp) return false;
//         if (user.OtpExpiry < DateTime.UtcNow) return false;

//         user.OTP = null;
//         user.OtpExpiry = null;
//         await _context.SaveChangesAsync();
//         return true;
//     }
// }
