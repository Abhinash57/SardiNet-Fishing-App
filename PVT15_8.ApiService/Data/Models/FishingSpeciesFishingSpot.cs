using PVT15_8.Shared.DTOs;

namespace PVT15_8.ApiService.Data.Models;

public class FishingSpeciesFishingSpot
{
    public int FishSpeciesId { get; set; }
    public FishSpecies FishSpecies { get; set; } = null!;

    public int FishingSpotId { get; set; }
    public FishingSpot FishingSpot { get; set; } = null!;
    
    public int FishSpeciesFrequencyId { get; set; }
    public FishSpeciesFrequency Frequency { get; set; }
}
