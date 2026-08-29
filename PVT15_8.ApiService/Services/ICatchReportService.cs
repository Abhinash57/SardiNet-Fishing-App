using PVT15_8.Shared.DTOs;

namespace PVT15_8.ApiService.Services;

public interface ICatchReportService
{
    Task<List<CatchReportDTO>> GetAllAsync();
    Task<CatchReportDTO?> GetByIdAsync(int id);
    Task<CatchReportDTO> CreateAsync(RequestCatchReportDTO dto);
    Task<CatchReportDTO> CreateWithImageAsync(Stream imageStream, string fileName, string? contentType, Dictionary<string, string> formData);
    Task<bool> UpdateAsync(int id, RequestCatchReportDTO dto, string userId);
    Task<bool> DeleteAsync(int id, string userId);
}