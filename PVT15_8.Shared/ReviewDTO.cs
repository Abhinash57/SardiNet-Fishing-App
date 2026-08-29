namespace PVT15_8.Shared.DTOs;

public class ReviewDTO
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int FishingSpotId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
}

public record RequestReviewDTO
{
    public required string UserId { get; init; }
    public required int FishingSpotId { get; init; }
    public required int Rating { get; init; }
    public string? Comment { get; init; }
}

