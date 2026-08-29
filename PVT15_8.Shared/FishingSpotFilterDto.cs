namespace PVT15_8.Shared.DTOs;

public class FishingSpotFilterDto
{
    public double? MinDepth { get; set; }
    public double? MaxDepth { get; set; }
    public bool? HasRules { get; set; }
    public bool? IsFishingCardRequired { get; set; }
    public bool IncludeForbidden { get; set; } = false;
    public List<int> SpeciesIds { get; set; } = new();
}