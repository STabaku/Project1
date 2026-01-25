
using Backend.API.Models;


namespace Backend.API.Services

{
    public class RequestService
    {
        private static List<EmergencyRequest> _requests = new();
        private static int _id = 1;

        public EmergencyRequest Add(EmergencyRequest req)
        {
            req.Id = _id++;
            req.CreatedAt = DateTime.Now;
            req.Status = "Pending";
            _requests.Add(req);
            return req;
        }

        public List<EmergencyRequest> GetAll()
        {
            return _requests.OrderByDescending(r => r.CreatedAt).ToList();
        }
    }
}
