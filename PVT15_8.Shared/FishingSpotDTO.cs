namespace PVT15_8.Shared.DTOs;

public class FishingSpotDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public double? Depth { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public bool HasRules { get; set; }
    public bool IsFishingCardRequired { get; set; }
    public bool IsForbidden { get; set; }
    public double DistanceFromUserMeters { get; set; }
    public int AverageRating { get; set; }

    public List<ReviewDTO> Reviews { get; set; } = [];
    public List<CatchReportDTO> CatchReports { get; set; } = [];
    public List<FishSpeciesDTO> FishSpecies { get; set; } = [];
    public List<FishingLureDTO> RecommendedLures { get; set; } = [];
}

public class FishingSpotSmallDTO
{
    public int Id { get; set; }
    public string Name { set; get; } = string.Empty;
    public string? Description { set; get; }
}

public record RequestFishingSpotDTO
{
    public required string Name { get; init; }
    public string? Description { get; init; }
    public double? Depth { get; init; }
    public required double Latitude { get; init; }
    public required double Longitude { get; init; }
    public bool HasRules { get; set; }
    public bool IsFishingCardRequired { get; set; }
    public bool IsForbidden { get; set; }
}
