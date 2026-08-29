namespace PVT15_8.Shared.DTOs;

public class CatchReportDTO
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;

    public int FishSpeciesId { get; set; }
    public string? FishSpeciesName { get; set; }

    public int FishingSpotId { get; set; }
    public string? FishingSpotName { get; set; }

    public int? FishingLureId { get; set; }
    public string? FishingLureName { get; set; }
    public string? FishingLureType { get; set; }  

    public DateTime CatchDate { get; set; }
    public double? WeightKg { get; set; }
    public double? LengthCm { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
}

public record RequestCatchReportDTO
{
    public required string UserId { get; init; }
    public required int FishSpeciesId { get; init; }
    public required int FishingSpotId { get; init; }
    public required int FishingLureId { get; init; }
    public required DateTime CatchDate { get; init; }
    public required double WeightKg { get; init; }
    public required double LengthCm { get; init; }
    public string? Description { get; init; }
    public string? ImageUrl { get; set; }
}
