namespace PVT15_8.ApiService.Data.Models;

public class FishSpecies
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; } 

    public ICollection<CatchReport> CatchReports { get; set; } = new List<CatchReport>();
    public ICollection<FishingLure> FishingLures { get; set; } = new List<FishingLure>();

    public ICollection<FishingSpeciesFishingSpot> FishingSpeciesFishingSpots { get; set; } = new List<FishingSpeciesFishingSpot>();
    
    public ICollection<FishingLureFishes> FishingLureFishes { get; set; } = new List<FishingLureFishes>();
}
