namespace PVT15_8.Shared.DTOs;

public class FishingLureDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Type { get; set; }
    public List<FishSpeciesDTO> Species { get; set; } = [];
    public string? ImageUrl { get; set; }
    public List<LureTargetSpeciesDTO> SpeciesNames { get; set; } = [];
}

public record RequestFishingLureDTO
{
    public required string Name { get; init; }
    public required string Type { get; init; }
    public List<int> FishSpeciesIds { get; init; } = [];
    public string? ImageUrl { get; init; } 
}

public class LureTargetSpeciesDTO
{
    public required string Name { get; set; }
}