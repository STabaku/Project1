using Backend.API.Data;
using Backend.API.Models;

public class RequestService
{
    private readonly AppDbContext _context;

    public RequestService(AppDbContext context)
    {
        _context = context;
    }

    public EmergencyRequest Add(EmergencyRequest request)
    {
        _context.EmergencyRequests.Add(request);
        _context.SaveChanges();
        return request;
    }

    public List<EmergencyRequest> GetByPharmacy(int pharmacyId)
    {
        return _context.EmergencyRequests
            .Where(r => r.PharmacyId == pharmacyId)
            .OrderByDescending(r => r.CreatedAt)
            .ToList();
    }
}
