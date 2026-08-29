namespace PVT15_8.Shared.DTOs;

public class UserStatsDTO
{
    // public string Firstname { get; set; }
    // public string LastName { get; set; }
    // public string Username { get; set; } = string.Empty; // har lokalt
    // public string Bio { get; set; } = string.Empty; // bara från Identity Service :( ** lägg till
    public int Catches { get; set; }
    public double TotalWeight { get; set; }
    public int Places { get; set; } // från SavedPlaces?
    public List<RecentActivityDto> RecentActivites { get; set; } = [];
}

public class RecentActivityDto
{
    public string? FishSpeciesName { get; set; } = string.Empty;
    public double? Weight { get; set; }
    public DateTime CatchDate { get; set; }
    public double? LengthCm { get; set; }
    public string LocationName { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
}
