namespace PVT15_8.Shared.DTOs;

public class FishSpeciesDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public FishSpeciesFrequency Frequency { get; set; }
    public List<FishingSpotInfo> FishingSpots { get; set; } = new();
    public List<FishingLureDTO> RecommendedLures { get; set; } = [];
}

public class FishingSpotInfo
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public record RequestFishSpeciesDTO
{
    public required string Name { get; init; }
    public string? Description { get; init; }
    public string? ImageUrl { get; init; }
}

public enum FishSpeciesFrequency
{
    Low,
    Medium,
    High,
}
