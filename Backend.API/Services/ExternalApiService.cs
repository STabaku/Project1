using Backend.API.Models;
public class ExternalApiService
{
    public bool ValidateMedication(string medication)
    {
        // OpenFDA API stub
        return true;
    }

    public double CalculateDistance()
    {
        // Google Maps API stub
        return 2.5;
    }

    public string[] GetPharmacyReviews()
    {
        // Google Places API stub
        return new[] { "Excellent service", "Fast delivery" };
    }
}
