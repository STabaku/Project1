using Backend.API.Data;
using Backend.API.Models;

namespace Backend.API.Services
{
    public class RequestService
    {
        private readonly AppDbContext _context;

        public RequestService(AppDbContext context)
        {
            _context = context;
        }

        public EmergencyRequest Add(EmergencyRequest req)
        {
            _context.EmergencyRequests.Add(req);
            _context.SaveChanges();
            return req;
        }

        public List<EmergencyRequest> GetAll()
        {
            return _context.EmergencyRequests
                           .OrderByDescending(r => r.CreatedAt)
                           .ToList();
        }
    }
}

