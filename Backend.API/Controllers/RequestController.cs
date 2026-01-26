using Microsoft.AspNetCore.Mvc;
using Backend.API.Services;
using Backend.API.Models;

namespace Backend.API.Controllers
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
    // Set dummy user id
    request.UserId = 1; 
    request.CreatedAt = DateTime.Now;
    request.Status = "Pending";

    var saved = _requestService.Add(request);
    return Ok(saved);
}

        // PHARMACY GETS ALL REQUESTS

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_requestService.GetAll());
        }
        
    } // end class
} // end namespace
