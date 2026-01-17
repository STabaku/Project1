public class MatchingService
{
    public Pharmacy MatchPharmacy(EmergencyRequest request)
    {
        // Simplified matching logic (distance, availability, reliability)
        return new Pharmacy
        {
            Name = "Nearby Pharmacy",
            ReliabilityScore = 0.9,
            HasMedication = true
        };
    }
}
