using Microsoft.AspNetCore.Mvc;
using PharmacyEmergencySystem.DTOs;
using Backend.API.Models;
using Backend.API.Services;
using System;
using System.Threading.Tasks;

namespace Backend.API.Controllers
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

        // SIGNUP
        [HttpPost("signup")]
        public async Task<IActionResult> Signup(RegisterRequest request)
        {
            var identifier = ResolveIdentifier(request.Email, request.Number);
            if (identifier == null)
                return BadRequest("Email or phone number is required.");

            var existing =
                await _userService.GetUserByEmailOrNumberAsync(identifier);

            if (existing != null)
                return BadRequest("User already exists.");

            var otp = _otpService.GenerateOTP();

            var user = new User
            {
                Name = request.Name,
                Location = request.Location,
                Number = request.Number,
                Email = request.Email,
                Age = request.Age,
                Role = request.Role,

                OTP = otp,
                OtpExpiry = DateTime.UtcNow.AddMinutes(5),
                IsVerified = false
            };

            await _userService.AddUserAsync(user);

            Console.WriteLine($"SIGNUP OTP for {identifier}: {otp}");

            return Ok("Registered successfully. Verify OTP.");
        }

        // VERIFY SIGNUP
        [HttpPost("verify-signup")]
        public async Task<IActionResult> VerifySignup(VerifyOTPRequest request)
        {
            var user =
                await _userService.GetUserByEmailOrNumberAsync(request.EmailOrNumber);

            if (user == null)
                return BadRequest("User not found.");

            if (user.IsVerified)
                return BadRequest("User already verified.");

            if (user.OTP != request.OTP)
                return BadRequest("Invalid OTP.");

            if (user.OtpExpiry == null || user.OtpExpiry < DateTime.UtcNow)
                return BadRequest("OTP expired.");

            await _userService.VerifyUserAsync(request.EmailOrNumber);

            return Ok("Account verified successfully.");
        }

        // LOGIN (REQUEST OTP)
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user =
                await _userService.GetUserByEmailOrNumberAsync(request.EmailOrNumber);

            if (user == null)
                return BadRequest("User not found.");

            if (!user.IsVerified)
                return BadRequest("Account not verified.");

            var otp = _otpService.GenerateOTP();

            user.OTP = otp;
            user.OtpExpiry = DateTime.UtcNow.AddMinutes(5);

            await _userService.UpdateUserAsync(user);

            Console.WriteLine($"LOGIN OTP for {request.EmailOrNumber}: {otp}");

            return Ok("OTP sent for login.");
        }

        // VERIFY LOGIN
        [HttpPost("verify-login")]
        public async Task<IActionResult> VerifyLogin(VerifyOTPRequest request)
        {
            var user =
                await _userService.GetUserByEmailOrNumberAsync(request.EmailOrNumber);

            if (user == null)
                return BadRequest("User not found.");

            if (user.OTP != request.OTP)
                return BadRequest("Invalid OTP.");

            if (user.OtpExpiry == null || user.OtpExpiry < DateTime.UtcNow)
                return BadRequest("OTP expired.");

            user.OTP = null;
            user.OtpExpiry = null;

            await _userService.UpdateUserAsync(user);

            return Ok(new
            {
                message = "Login successful",
                role = user.Role
            });
        }

        // =========================
        // PRIVATE HELPER
        // =========================
        private static string? ResolveIdentifier(string? email, string? number)
        {
            if (!string.IsNullOrWhiteSpace(email))
                return email;

            if (!string.IsNullOrWhiteSpace(number))
                return number;

            return null;
        }
    }
}
