using Microsoft.AspNetCore.Mvc;
using PharmacyEmergencySystem.DTOs;
using Backend.API.Models;
using Backend.API.Services; // ✅ correct
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

        //  SIGNUP
       [HttpPost("signup")]
public async Task<IActionResult> Signup([FromBody] RegisterRequest request)
{
    if (string.IsNullOrEmpty(request.Email) && string.IsNullOrEmpty(request.Number))
        return BadRequest("Please provide an email or phone number.");

    var existing = await _userService.GetUserByEmailOrNumberAsync(request.Email ?? request.Number);
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

    await _userService.AddUserAsync(user);

    Console.WriteLine($"SIGNUP OTP for {(request.Email ?? request.Number)}: {otp}");
    return Ok("Registered successfully. Verify OTP.");
}

 // VERIFY SIGNUP OTP
        [HttpPost("verify-signup")]
        public async Task<IActionResult> VerifySignup([FromBody] VerifyOTPRequest request)
        {
            var user = await _userService.GetUserByEmailOrNumberAsync(request.EmailOrNumber);
            if (user == null)
                return BadRequest("User not found.");

            if (user.IsVerified)
                return BadRequest("User already verified.");

            if (user.OTP != request.OTP)
                return BadRequest("Invalid OTP.");

            if (user.OtpExpiry < DateTime.UtcNow)
                return BadRequest("OTP expired.");

            await _userService.VerifyUserAsync(request.EmailOrNumber);
            return Ok("Account verified successfully.");
        }

// LOGIN (REQUEST OTP)
[HttpPost("login")]
public async Task<IActionResult> Login([FromBody] LoginRequest request)
{
    var user = await _userService.GetUserByEmailOrNumberAsync(request.EmailOrNumber);
    if (user == null)
        return BadRequest("User not found.");

    if (!user.IsVerified)
        return BadRequest("Account not verified.");

    // Generate a new OTP and set expiry (5 minutes)
    var otp = _otpService.GenerateOTP();
    user.OTP = otp;
    user.OtpExpiry = DateTime.UtcNow.AddMinutes(5);
    await _userService.UpdateUserAsync(user); // make sure UpdateUserAsync saves changes

    Console.WriteLine($"LOGIN OTP for {request.EmailOrNumber}: {otp}");

    return Ok(new
    {
        message = "OTP sent for login",
        otp = otp // optional, only for testing; remove in production
    });
}

// VERIFY LOGIN OTP
[HttpPost("verify-login")]
public async Task<IActionResult> VerifyLogin([FromBody] VerifyOTPRequest request)
{
    var user = await _userService.GetUserByEmailOrNumberAsync(request.EmailOrNumber);
    if (user == null)
        return BadRequest("User not found.");

    if (user.OTP != request.OTP)
        return BadRequest("Invalid OTP.");

    if (user.OtpExpiry < DateTime.UtcNow)
        return BadRequest("OTP expired.");

    // Successful login, clear OTP so it can't be reused accidentally
    user.OTP = null;
    user.OtpExpiry = null;
    await _userService.UpdateUserAsync(user);

    return Ok(new
    {
        message = "Login successful",
        role = user.Role
    });
}}
}


