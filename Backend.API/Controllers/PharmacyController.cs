using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/pharmacies")]
public class PharmacyController : ControllerBase
{
    private readonly ExternalApiService _externalApi;

    public PharmacyController(ExternalApiService externalApi)
    {
        _externalApi = externalApi;
    }

    [HttpGet("reviews")]
    public IActionResult GetReviews()
    {
        var reviews = _externalApi.GetPharmacyReviews();
        return Ok(reviews);
    }
}
