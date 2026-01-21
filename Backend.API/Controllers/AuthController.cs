using Microsoft.AspNetCore.Mvc;
using PharmacyEmergencySystem.DTOs;
using PharmacyEmergencySystem.Models;
using PharmacyEmergencySystem.Services;

namespace PharmacyEmergencySystem.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly OtpService _otpService;

        public AuthController(UserService userService, OtpService otpService)
        {
            _userService = userService;
            _otpService = otpService;
        }

        //  SIGNUP
        [HttpPost("signup")]
        public IActionResult Signup([FromBody] RegisterRequest request)
        {
            var existing = _userService.GetUserByEmailOrNumber(request.Email);
            if (existing != null)
                return BadRequest("User already exists.");

            var otp = _otpService.GenerateOTP();

            var user = new User
            {
                Name = request.Name,
                Location = request.Location,
                Number = request.Number,
                Gender = request.Gender,
                Age = request.Age,
                Email = request.Email,
                Role = request.Role,
                OTP = otp,
                OtpExpiry = DateTime.UtcNow.AddMinutes(5),
                IsVerified = false
            };

            _userService.AddUser(user);

            Console.WriteLine($"SIGNUP OTP for {request.Email}: {otp}");
            return Ok("Registered successfully. Verify OTP.");
        }

        // VERIFY SIGNUP OTP
        [HttpPost("verify-signup")]
        public IActionResult VerifySignup([FromBody] VerifyOTPRequest request)
        {
            var user = _userService.GetUserByEmailOrNumber(request.EmailOrNumber);
            if (user == null)
                return BadRequest("User not found.");

            if (user.IsVerified)
                return BadRequest("User already verified.");

            if (user.OTP != request.OTP)
                return BadRequest("Invalid OTP.");

            if (user.OtpExpiry < DateTime.UtcNow)
                return BadRequest("OTP expired.");

            _userService.VerifyUser(request.EmailOrNumber);
            return Ok("Account verified successfully.");
        }

        //  LOGIN (REQUEST OTP)
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _userService.GetUserByEmailOrNumber(request.EmailOrNumber);
            if (user == null)
                return BadRequest("User not found.");

            if (!user.IsVerified)
                return BadRequest("Account not verified.");

            var otp = _otpService.GenerateOTP();
            _userService.UpdateOTP(request.EmailOrNumber, otp);

            Console.WriteLine($"LOGIN OTP for {request.EmailOrNumber}: {otp}");
            return Ok("OTP sent for login.");
        }

        //  VERIFY LOGIN OTP
        [HttpPost("verify-login")]
        public IActionResult VerifyLogin([FromBody] VerifyOTPRequest request)
        {
            var user = _userService.GetUserByEmailOrNumber(request.EmailOrNumber);
            if (user == null)
                return BadRequest("User not found.");

            if (user.OTP != request.OTP)
                return BadRequest("Invalid OTP.");

            if (user.OtpExpiry < DateTime.UtcNow)
                return BadRequest("OTP expired.");

            return Ok(new
            {
                message = "Login successful",
                role = user.Role
            });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok("Logged out successfully.");
        }
    }
}
