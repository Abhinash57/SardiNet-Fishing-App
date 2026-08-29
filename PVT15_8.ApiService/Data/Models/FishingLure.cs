namespace PVT15_8.ApiService.Data.Models;

public class FishingLure
{
    public int Id { get; set; }

    public required string Name { get; set; }
    public string? Type { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public ICollection<CatchReport> CatchReports { get; set; } = new List<CatchReport>();
    public ICollection<FishSpecies> FishSpecies { get; set; } = new List<FishSpecies>();

    public ICollection<FishingLureFishes> FishingLureFishes { get; set; } = new List<FishingLureFishes>();
}
