using Backend.API.Models;
using Backend.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // ================= MY REQUESTS =================
        [HttpGet("my-requests")]
        public async Task<IActionResult> GetMyRequests()
        {
            var userId = int.Parse(
                User.FindFirst(JwtRegisteredClaimNames.Sub)!.Value
            );

            var requests = await _context.EmergencyRequests
                .Where(r => r.ClientId == userId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            return Ok(requests);
        }

        // ================= MY PROFILE =================
        [HttpGet("me")]
        public async Task<IActionResult> GetMyDetails()
        {
            var userId = int.Parse(
                User.FindFirst(JwtRegisteredClaimNames.Sub)!.Value
            );

            var user = await _context.Users
                .Where(u => u.Id == userId)
                .Select(u => new
                {
                    u.Id,
                    u.Name,
                    u.Email,
                    u.Number,
                    u.Age,
                    u.Location,
                    u.Role,
                    u.PharmacyId
                })
                .FirstOrDefaultAsync();

            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}
