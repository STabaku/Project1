using Backend.API.Models;
using Backend.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // ⚠️ OPTIONAL – vetëm për SuperAdmin (rekomandohet të mbyllet)
        [HttpGet]
        public IActionResult GetAll()
        {
            return Unauthorized("Access denied");
        }

        // GET api/users/me
        [HttpGet("me")]
        public async Task<IActionResult> GetMyDetails()
        {
            if (!HttpContext.Items.ContainsKey("UserId"))
                return Unauthorized();

            int userId = (int)HttpContext.Items["UserId"]!;

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
