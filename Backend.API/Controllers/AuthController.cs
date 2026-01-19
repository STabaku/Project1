using Microsoft.AspNetCore.Mvc;
using PharmacyEmergencySystem.DTOs;
using PharmacyEmergencySystem.Models;
using PharmacyEmergencySystem.Services;

namespace PharmacyEmergencySystem.Services
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly OtpService _otpService;

        // ✅ Constructor injection
        public AuthController(UserService userService, OtpService otpService)
        {
            _userService = userService;
            _otpService = otpService;
        }

        [HttpPost("signup")]
        public IActionResult Signup([FromBody] RegisterRequest request)
        {
            var existing = _userService.GetUserByEmailOrNumber(request.Email);
            if (existing != null) return BadRequest("User already exists.");

            var otp = _otpService.GenerateOTP();
            var user = new User
            {
                Name = request.Name,
                Location = request.Location,
                Number = request.Number,
                Gender = request.Gender,
                Age = request.Age,
                Email = request.Email,
                OTP = otp,
                IsVerified = false,
                Role = request.Role
            };

            _userService.AddUser(user);
            Console.WriteLine($"OTP for {request.Email}: {otp}"); // Display OTP in console for testing
            return Ok("User registered. Please verify OTP.");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] VerifyOTPRequest request)
        {
            var user = _userService.GetUserByEmailOrNumber(request.EmailOrNumber);
            if (user == null) return BadRequest("User not found.");

            var otp = _otpService.GenerateOTP();
            _userService.UpdateOTP(request.EmailOrNumber, otp);
            Console.WriteLine($"OTP for login {request.EmailOrNumber}: {otp}");
            return Ok("OTP sent. Please verify.");
        }

        [HttpPost("verify")]
        public IActionResult Verify([FromBody] VerifyOTPRequest request)
        {
            var user = _userService.GetUserByEmailOrNumber(request.EmailOrNumber);
            if (user == null) return BadRequest("User not found.");
            if (user.OTP != request.OTP) return BadRequest("Invalid OTP.");

            _userService.VerifyUser(request.EmailOrNumber);
            return Ok(new { message = "User verified.", role = user.Role });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // For session-less API, frontend can just clear tokens or localStorage
            return Ok("Logged out successfully.");
        }
    }
}

