using PVT15_8.Shared.DTOs;

namespace PVT15_8.ApiService.Services;

public interface IFishSpeciesService
{
    Task<List<FishSpeciesDTO>> GetAllAsync();
    Task<FishSpeciesDTO?> GetByIdAsync(int id);
    Task<FishSpeciesDTO> CreateAsync(RequestFishSpeciesDTO dto);
    Task<bool> UpdateAsync(int id, RequestFishSpeciesDTO dto);
    Task<bool> DeleteAsync(int id);
}