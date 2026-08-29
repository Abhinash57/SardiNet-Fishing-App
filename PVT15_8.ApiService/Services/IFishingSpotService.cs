using PVT15_8.Shared.DTOs;

namespace PVT15_8.ApiService.Services;

public interface IFishingSpotService
{
    Task<List<FishingSpotMarkerDTO>> GetMarkersAsync();
    Task<List<FishingSpotDTO>> GetAllAsync();
    Task<FishingSpotDTO?> GetByIdAsync(int id);
    Task<FishingSpotDTO> CreateAsync(RequestFishingSpotDTO dto);
    Task<List<FishingSpotMarkerDTO>> GetFilteredMarkersAsync(FishingSpotFilterDto filter);
    Task<bool> UpdateAsync(int id, RequestFishingSpotDTO dto);
    Task<bool> DeleteAsync(int id);
}