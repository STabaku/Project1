using Backend.API.Models;
using Backend.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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

       [Authorize]
[HttpGet("me")]
public async Task<IActionResult> Me()
{
    try
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
            return BadRequest("UserId claim missing");

        if (!int.TryParse(userIdClaim.Value, out int userId))
            return BadRequest("Invalid userId format");

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            return NotFound("User not found in database");

        return Ok(new
        {
            name = user.Name,
            email = user.Email,
            number = user.Number
        });
    }
    catch (Exception ex)
    {
        return StatusCode(500, ex.Message);
    }
}

    }
}
