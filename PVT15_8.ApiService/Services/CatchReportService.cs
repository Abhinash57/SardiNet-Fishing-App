using Microsoft.EntityFrameworkCore;
using PVT15_8.ApiService.Data;
using PVT15_8.ApiService.Data.Models;
using PVT15_8.Shared.DTOs;

namespace PVT15_8.ApiService.Services;

public class CatchReportService : ICatchReportService
{
    private readonly ServiceDbContext _context;

    public CatchReportService(ServiceDbContext context)
    {
        _context = context;
    }

    public async Task<List<CatchReportDTO>> GetAllAsync()
    {
        return await _context.CatchReports
            .Include(r => r.FishSpecies)
            .Include(r => r.FishingSpot)
            .Include(r => r.FishingLure)
            .OrderByDescending(r => r.CatchDate)
            .Select(r => MapToDto(r))
            .ToListAsync();
    }

    public async Task<CatchReportDTO?> GetByIdAsync(int id)
    {
        var report = await _context.CatchReports
            .Include(r => r.FishSpecies)
            .Include(r => r.FishingSpot)
            .Include(r => r.FishingLure)
            .FirstOrDefaultAsync(r => r.Id == id);
        return report == null ? null : MapToDto(report);
    }

    public async Task<CatchReportDTO> CreateAsync(RequestCatchReportDTO dto)
    {
        var report = new CatchReport
        {
            UserId = dto.UserId,
            FishSpeciesId = dto.FishSpeciesId,
            FishingSpotId = dto.FishingSpotId,
            FishingLureId = dto.FishingLureId,
            CatchDate = DateTime.SpecifyKind(dto.CatchDate, DateTimeKind.Utc),
            WeightKg = dto.WeightKg,
            LengthCm = dto.LengthCm,
            Description = dto.Description,
            ImageUrl = dto.ImageUrl
        };
        _context.CatchReports.Add(report);
        await _context.SaveChangesAsync();
        return MapToDto(report);
    }

    
    public async Task<CatchReportDTO> CreateWithImageAsync(Stream imageStream, string fileName, string? contentType, Dictionary<string, string> formData)
    {
        byte[] imageData;
        using (var ms = new MemoryStream())
        {
            await imageStream.CopyToAsync(ms);
            imageData = ms.ToArray();
        }

        string imageUrl = string.Empty;

        int fishSpeciesId = int.Parse(formData["fishSpeciesId"]);
        int fishingSpotId = int.Parse(formData["fishingSpotId"]);
        var catchDate = DateTime.Parse(formData["catchDate"]);
        catchDate = DateTime.SpecifyKind(catchDate, DateTimeKind.Local).ToUniversalTime();

        int? fishingLureId = formData.ContainsKey("fishingLureId") && int.TryParse(formData["fishingLureId"], out var lureId) ? lureId : null;
        double? weightKg = formData.ContainsKey("weightKg") && double.TryParse(formData["weightKg"], out var w) ? w : null;
        double? lengthCm = formData.ContainsKey("lengthCm") && double.TryParse(formData["lengthCm"], out var l) ? l : null;

        var report = new CatchReport
        {
            UserId = formData["userId"],
            FishSpeciesId = fishSpeciesId,
            FishingSpotId = fishingSpotId,
            FishingLureId = fishingLureId,
            CatchDate = catchDate,
            WeightKg = weightKg,
            LengthCm = lengthCm,
            Description = formData.GetValueOrDefault("description"),
            ImageUrl = imageUrl
        };
        _context.CatchReports.Add(report);
        await _context.SaveChangesAsync();
        return MapToDto(report);
    }

    public async Task<bool> UpdateAsync(int id, RequestCatchReportDTO dto, string userId)
    {
        var report = await _context.CatchReports.FindAsync(id);
        if (report == null || report.UserId != userId) return false;

        report.FishSpeciesId = dto.FishSpeciesId;
        report.FishingSpotId = dto.FishingSpotId;
        report.FishingLureId = dto.FishingLureId;
        report.CatchDate = DateTime.SpecifyKind(dto.CatchDate, DateTimeKind.Utc);
        report.WeightKg = dto.WeightKg;
        report.LengthCm = dto.LengthCm;
        report.Description = dto.Description;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id, string userId)
    {
        var report = await _context.CatchReports.FindAsync(id);
        if (report == null || report.UserId != userId) return false;

        _context.CatchReports.Remove(report);
        await _context.SaveChangesAsync();
        return true;
    }

    private static CatchReportDTO MapToDto(CatchReport r)
    {
        return new CatchReportDTO
        {
            Id = r.Id,
            UserId = r.UserId,
            FishSpeciesId = r.FishSpeciesId,
            FishSpeciesName = r.FishSpecies?.Name,
            FishingSpotId = r.FishingSpotId,
            FishingSpotName = r.FishingSpot?.Name ?? "Okänd plats",
            FishingLureId = r.FishingLureId,
            FishingLureName = r.FishingLure?.Name,
            FishingLureType = r.FishingLure?.Type,
            CatchDate = r.CatchDate,
            WeightKg = r.WeightKg,
            LengthCm = r.LengthCm,
            Description = r.Description,
            ImageUrl = r.ImageUrl
        };
    }
}