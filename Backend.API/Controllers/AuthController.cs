using Backend.API.Data;
using Backend.API.Models;
using Backend.API.Services;
using Microsoft.AspNetCore.Identity;
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
        private readonly JwtService _jwtService;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthController(
            AppDbContext context,
            JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
            _passwordHasher = new PasswordHasher<User>();
        }

        // =========================
        // REGISTER
        // =========================
        [HttpPost("signup")]
        public async Task<IActionResult> Signup(RegisterRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Password))
                return BadRequest("Email and password required");

            if (await _context.Users.AnyAsync(u =>
                u.Email == request.Email ||
                u.Number == request.Number))
                return BadRequest("User already exists");

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Number = request.Number,
                Role = request.Role,
                PharmacyId = request.PharmacyId,
                IsVerified = true 
            };

            user.PasswordHash =
                _passwordHasher.HashPassword(user, request.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Registered successfully" });
        }

        // =========================
        // LOGIN (EMAIL OSE NUMBER + PASSWORD)
        // =========================
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.EmailOrNumber) ||
                string.IsNullOrWhiteSpace(request.Password))
                return BadRequest("Email/Number and password required");

            var user = await _context.Users.FirstOrDefaultAsync(u =>
                u.Email == request.EmailOrNumber ||
                u.Number == request.EmailOrNumber);

            if (user == null)
                return Unauthorized("Invalid credentials");

            var result = _passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                request.Password);

            if (result != PasswordVerificationResult.Success)
                return Unauthorized("Invalid credentials");

            var token = _jwtService.GenerateToken(user);

            return Ok(new
            {
                token,
                role = user.Role,
                userId = user.Id,
                pharmacyId = user.PharmacyId
            });
        }
    }
}
