namespace PVT15_8.ApiService.Data.Models;

public class BookmarkedSpots
{
    public required string UserId { get; set; }
    public int FishingSpotId { get; set; }
    public FishingSpot FishingSpot { get; set; } = null!;
}
