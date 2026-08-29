namespace PVT15_8.ApiService.Data.Models;

public class Review
{
    public int Id { get; set; }

    public required string UserId { get; set; }

    public int FishingSpotId { get; set; }
    public FishingSpot? FishingSpot { get; set; }

    public int Rating { get; set; }
    public string? Comment { get; set; }
}
