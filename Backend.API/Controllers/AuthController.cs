using Backend.API.Data;
using Backend.API.Models;
using Backend.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacyEmergencySystem.DTOs;

namespace Backend.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly OtpService _otpService;
        private readonly JwtService _jwtService;

        public AuthController(
            AppDbContext context,
            OtpService otpService,
            JwtService jwtService)
        {
            _context = context;
            _otpService = otpService;
            _jwtService = jwtService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup(RegisterRequest request)
        {
            var identifier = request.Email ?? request.Number;
            if (string.IsNullOrWhiteSpace(identifier))
                return BadRequest(new { message = "Email or phone required" });

            if (await _context.Users.AnyAsync(u =>
                u.Email == identifier || u.Number == identifier))
                return BadRequest(new { message = "User already exists" });

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Number = request.Number,
                Role = request.Role,
                PharmacyId = request.PharmacyId,
                OTP = _otpService.GenerateOTP(),
                OtpExpiry = DateTime.UtcNow.AddMinutes(5),
                IsVerified = false
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            Console.WriteLine($"SIGNUP OTP: {user.OTP}");

            return Ok(new { message = "Registered successfully. Verify OTP." });
        }

        [HttpPost("verify-signup")]
        public async Task<IActionResult> VerifySignup(VerifyOTPRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u =>
                u.Email == request.EmailOrNumber ||
                u.Number == request.EmailOrNumber);

            if (user == null)
                return BadRequest(new { message = "User not found" });

            if (user.OTP != request.OTP)
                return BadRequest(new { message = "Invalid OTP" });

            if (user.OtpExpiry == null || user.OtpExpiry < DateTime.UtcNow)
                return BadRequest(new { message = "OTP expired" });

            user.IsVerified = true;
            user.OTP = null;
            user.OtpExpiry = null;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Account verified successfully" });
        }

        [HttpPost("login-request")]
        public async Task<IActionResult> LoginRequest(LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u =>
                u.Email == request.EmailOrNumber ||
                u.Number == request.EmailOrNumber);

            if (user == null)
                return BadRequest(new { message = "User not found" });

            if (!user.IsVerified)
                return BadRequest(new { message = "Account not verified" });

            user.OTP = _otpService.GenerateOTP();
            user.OtpExpiry = DateTime.UtcNow.AddMinutes(5);

            await _context.SaveChangesAsync();

            Console.WriteLine($"LOGIN OTP: {user.OTP}");

            return Ok(new { message = "OTP sent" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginVerifyRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u =>
                u.Email == request.EmailOrNumber ||
                u.Number == request.EmailOrNumber);

            if (user == null)
                return BadRequest(new { message = "User not found" });

            if (user.OTP != request.OTP)
                return BadRequest(new { message = "Invalid OTP" });

            if (user.OtpExpiry == null || user.OtpExpiry < DateTime.UtcNow)
                return BadRequest(new { message = "OTP expired" });

            user.OTP = null;
            user.OtpExpiry = null;
            await _context.SaveChangesAsync();

            var token = _jwtService.GenerateToken(user);

            return Ok(new
            {
                token,
                role = user.Role,
                pharmacyId = user.PharmacyId
            });
        }
    }
}
