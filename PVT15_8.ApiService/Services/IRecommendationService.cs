using PVT15_8.ApiService.Data.Models;

namespace PVT15_8.ApiService.Services;

public interface IRecommendationService
{
    Task<List<FishingSpot>> GetRecommendationsAsync(double userLat, double userLon, int? fishSpeciesId = null, int take = 5);
    Task<FishingSpot?> AddAsync(FishingSpot spot);
    Task<List<FishingSpot>> GetAllAsync();
    Task<bool> DeleteAsync(int id);
}