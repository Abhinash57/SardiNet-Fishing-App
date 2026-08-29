using NetTopologySuite.Geometries;
using System.Text.Json.Serialization;

namespace PVT15_8.ApiService.Data.Models;

public class FishingSpot
{
    public int Id { get; set; }

    public required string Name { get; set; }
    public string? Description { get; set; }
    public double? Depth { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public bool HasRules { get; set; } = false;
    
    public bool IsForbidden { get; set; }
    public bool IsDeleted { get; set; } = false;
    public bool IsFishingCardRequired { get; set; }
    [JsonIgnore]
    public Point Location { get; set; } = null!;

    public ICollection<CatchReport> CatchReports { get; set; } = new List<CatchReport>();

    public ICollection<FishingSpeciesFishingSpot> SpeciesFishingSpots { get; set; } = new List<FishingSpeciesFishingSpot>();

    public ICollection<Review> Reviews { get; set; } = new List<Review>();

    public ICollection<Weather> Weathers { get; set; } = new List<Weather>();
}
