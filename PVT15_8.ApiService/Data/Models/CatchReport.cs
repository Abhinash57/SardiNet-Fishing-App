namespace PVT15_8.ApiService.Data.Models;

public class CatchReport
{
    public int Id { get; set; }

    public required string UserId { get; set; }

    public int FishSpeciesId { get; set; }
    public FishSpecies? FishSpecies { get; set; }
   
    public int FishingSpotId { get; set; }
    public FishingSpot FishingSpot { get; set; } = null!;

    public int? FishingLureId { get; set; }
    public FishingLure? FishingLure { get; set; }

    public DateTime CatchDate { get; set; }

    public double? WeightKg { get; set; }
    public double? LengthCm { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; } 
}
