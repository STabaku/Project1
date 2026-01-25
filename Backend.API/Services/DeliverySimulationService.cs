using Backend.API.Models;
public class DeliverySimulationService
{
    public async Task SimulateDeliveryAsync(Pharmacy pharmacy)
    {
        var random = new Random();
        int delay = random.Next(1000, 5000);

        await Task.Delay(delay);

        // Delivery status updated asynchronously
        Console.WriteLine($"Delivery completed by {pharmacy.Name}");
    }
}
