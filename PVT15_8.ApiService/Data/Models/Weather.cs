namespace PVT15_8.ApiService.Data.Models;

public class Weather
{
    public int Id { get; set; }

    public int FishingSpotId { get; set; }
    public required FishingSpot FishingSpot { get; set; }
    public DateTime Date { get; set; }

    public double Temperature { get; set; }
    public double WindSpeed { get; set; }
    public string? Conditions { get; set; }
}
