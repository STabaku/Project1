using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Backend.API.Data;
using Backend.API.Models; 

namespace Backend.API.Controllers
{
    [ApiController]
    [Route("api/requests")]
    public class RequestsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RequestsController(AppDbContext context)
        {
            _context = context;
        }

        // =========================
        // CREATE REQUEST (USER)
        // =========================
        [HttpPost]
        [AllowAnonymous] 
        public async Task<IActionResult> Create([FromBody] EmergencyRequest dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var request = new EmergencyRequest
            {
                MedicineName = dto.MedicineName,
                Quantity = dto.Quantity,
                Address = dto.Address,
                PharmacyId = dto.PharmacyId,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow
            };

            _context.EmergencyRequests.Add(request);
            await _context.SaveChangesAsync();

            return Ok(request);
        }

        // =========================
        // GET ALL (PharmacyAdmin)
        // =========================
        [Authorize(Roles = "PharmacyAdmin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pharmacyId = GetPharmacyId();

            var data = await _context.EmergencyRequests
                .Where(r => r.PharmacyId == pharmacyId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            return Ok(data);
        }

        // =========================
        // ACCEPT
        // =========================
        [Authorize(Roles = "PharmacyAdmin")]
        [HttpPost("accept/{id}")]
        public async Task<IActionResult> Accept(int id)
        {
            var pharmacyId = GetPharmacyId();

            var req = await _context.EmergencyRequests
                .FirstOrDefaultAsync(r => r.Id == id && r.PharmacyId == pharmacyId);

            if (req == null)
                return NotFound();

            req.Status = "Accepted";
            await _context.SaveChangesAsync();

            return Ok();
        }

        // =========================
        // DELIVER
        // =========================
        [Authorize(Roles = "PharmacyAdmin")]
        [HttpPost("deliver/{id}")]
        public async Task<IActionResult> Deliver(int id)
        {
            var pharmacyId = GetPharmacyId();

            var req = await _context.EmergencyRequests
                .FirstOrDefaultAsync(r => r.Id == id && r.PharmacyId == pharmacyId);

            if (req == null)
                return NotFound();

            req.Status = "Delivered";
            await _context.SaveChangesAsync();

            return Ok();
        }

        // =========================
        // STATS
        // =========================
        [Authorize(Roles = "PharmacyAdmin")]
        [HttpGet("stats")]
        public async Task<IActionResult> Stats()
        {
            var pharmacyId = GetPharmacyId();

            var q = _context.EmergencyRequests
                .Where(r => r.PharmacyId == pharmacyId);

            return Ok(new
            {
                total = await q.CountAsync(),
                pending = await q.CountAsync(r => r.Status == "Pending"),
                active = await q.CountAsync(r => r.Status == "Accepted"),
                delivered = await q.CountAsync(r => r.Status == "Delivered")
            });
        }

        // =========================
        // Helper Method
        // =========================
        private int GetPharmacyId()
        {
            return int.Parse(User.FindFirst("pharmacyId")!.Value);
        }
    }
}
