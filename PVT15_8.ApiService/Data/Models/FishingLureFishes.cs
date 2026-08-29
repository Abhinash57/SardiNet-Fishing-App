namespace PVT15_8.ApiService.Data.Models;
public class FishingLureFishes
{
    public int FishingLureId { get; set; }
    public FishingLure FishingLure { get; set; } = null!;

    public int FishSpeciesId { get; set; }
    public FishSpecies FishSpecies { get; set; } = null!;
    


}