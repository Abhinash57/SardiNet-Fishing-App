using PVT15_8.Shared.DTOs;

namespace PVT15_8.ApiService.Services;

public interface IFishingLureService
{
    Task<List<FishingLureDTO>> GetAllAsync();
    Task<FishingLureDTO?> GetByIdAsync(int id);
    Task<FishingLureDTO> CreateAsync(RequestFishingLureDTO dto);
    Task<bool> UpdateAsync(int id, RequestFishingLureDTO dto);
    Task<bool> DeleteAsync(int id);
}