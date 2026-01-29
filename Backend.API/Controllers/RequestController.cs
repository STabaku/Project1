using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Backend.API.Data;

namespace Backend.API.Controllers
{
    [Authorize(Roles = "PharmacyAdmin")]
    [ApiController]
    [Route("api/requests")]
    public class RequestsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RequestsController(AppDbContext context)
        {
            _context = context;
        }

        private int PharmacyId =>
            int.Parse(User.FindFirst("pharmacyId")!.Value);

        // GET ALL REQUESTS (FOR DASHBOARD)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.EmergencyRequests
                .Where(r => r.PharmacyId == PharmacyId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            return Ok(data);
        }

        // ACCEPT REQUEST
        
        [HttpPost("accept/{id}")]
        public async Task<IActionResult> Accept(int id)
        {
            var req = await _context.EmergencyRequests
                .FirstOrDefaultAsync(r => r.Id == id && r.PharmacyId == PharmacyId);

            if (req == null) return NotFound();

            req.Status = "Accepted";
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELIVER REQUEST
        [HttpPost("deliver/{id}")]
        public async Task<IActionResult> Deliver(int id)
        {
            var req = await _context.EmergencyRequests
                .FirstOrDefaultAsync(r => r.Id == id && r.PharmacyId == PharmacyId);

            if (req == null) return NotFound();

            req.Status = "Delivered";
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DASHBOARD STATS (OPTIONAL)
        [HttpGet("stats")]
        public async Task<IActionResult> Stats()
        {
            var q = _context.EmergencyRequests
                .Where(r => r.PharmacyId == PharmacyId);

            return Ok(new
            {
                total = await q.CountAsync(),
                pending = await q.CountAsync(r => r.Status == "Pending"),
                active = await q.CountAsync(r => r.Status == "Accepted"),
                delivered = await q.CountAsync(r => r.Status == "Delivered")
            });
        }
    }
}
