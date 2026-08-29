using PVT15_8.Shared.DTOs;

namespace PVT15_8.ApiService.Services;

public interface IReviewService
{
    Task<List<ReviewDTO>> GetAllAsync();
    Task<ReviewDTO?> GetByIdAsync(int id);
    Task<ReviewDTO> CreateAsync(RequestReviewDTO dto);
    Task<bool> UpdateAsync(int id, RequestReviewDTO dto);
    Task<bool> DeleteAsync(int id);
}