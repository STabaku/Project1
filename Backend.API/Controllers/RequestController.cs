using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/requests")]
public class RequestController : ControllerBase
{
    private readonly MatchingService _matchingService;
    private readonly DeliverySimulationService _deliveryService;

    public RequestController(
        MatchingService matchingService,
        DeliverySimulationService deliveryService)
    {
        _matchingService = matchingService;
        _deliveryService = deliveryService;
    }

    [HttpPost]
    public async Task<IActionResult> SubmitRequest(EmergencyRequest request)
    {
        // Asynchronous processing
        await Task.Delay(300);

        var pharmacy = _matchingService.MatchPharmacy(request);

        _ = _deliveryService.SimulateDeliveryAsync(pharmacy);

        return Accepted("Request accepted and processing asynchronously");
    }
}
