using Microsoft.AspNetCore.Mvc;
using PharmacyEmergencySystem.Models;
using PharmacyEmergencySystem.Services;

namespace PharmacyEmergencySystem.Controllers
{
    [ApiController]
    [Route("api/requests")]
    public class RequestsController : ControllerBase
    {
        private readonly RequestService _requestService;

        public RequestsController(RequestService requestService)
        {
            _requestService = requestService;
        }

        // CLIENT SENDS REQUEST
        [HttpPost("send")]
        public IActionResult Send([FromBody] EmergencyRequest request)
        {
            var saved = _requestService.Add(request);
            return Ok(saved);
        }

        // PHARMACY GETS ALL REQUESTS
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_requestService.GetAll());
        }
    }
}
