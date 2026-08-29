namespace PVT15_8.Shared.DTOs;

public record FishingSpotRequest(string Name, string? Description, double? Depth, double Longitude, double Latitude);