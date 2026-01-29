using Backend.API.Data;
using Backend.API.Models;
using System.Linq;

namespace Backend.API.Services
{
    public class MatchingService
    {
        private readonly AppDbContext _context;

        public MatchingService(AppDbContext context)
        {
            _context = context;
        }

        public Pharmacy? MatchPharmacy(EmergencyRequest request)
        {
            return _context.Pharmacies
                .Where(p => p.IsActive)
                .FirstOrDefault();
        }
    }
}
