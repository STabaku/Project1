using Backend.API.Data;
using Backend.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.API.Controllers
{
    [ApiController]
    [Route("api/pharmacies")]
    public class PharmaciesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PharmaciesController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ GET: api/pharmacies
        // Merr vetëm farmacitë aktive
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pharmacies = await _context.Pharmacies
                .Where(p => p.IsActive)
                .ToListAsync();

            return Ok(pharmacies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pharmacy = await _context.Pharmacies
                .FirstOrDefaultAsync(p => p.Id == id && p.IsActive);

            if (pharmacy == null)
                return NotFound("Pharmacy not found");

            return Ok(pharmacy);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Pharmacy pharmacy)
        {
            pharmacy.IsActive = true;
            _context.Pharmacies.Add(pharmacy);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById),
                new { id = pharmacy.Id }, pharmacy);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Pharmacy pharmacy)
        {
            if (id != pharmacy.Id)
                return BadRequest("Id mismatch");

            var existing = await _context.Pharmacies.FindAsync(id);
            if (existing == null || !existing.IsActive)
                return NotFound("Pharmacy not found");

            existing.Name = pharmacy.Name;
            existing.Address = pharmacy.Address;
            existing.Phone = pharmacy.Phone;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var pharmacy = await _context.Pharmacies.FindAsync(id);
            if (pharmacy == null)
                return NotFound("Pharmacy not found");

            pharmacy.IsActive = false; 
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
