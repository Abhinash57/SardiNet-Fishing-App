using Microsoft.EntityFrameworkCore;
using PVT15_8.ApiService.Data;
using PVT15_8.ApiService.Data.Models;
using PVT15_8.Shared.DTOs;

namespace PVT15_8.ApiService.Services;

public class FishingLureService : IFishingLureService
{
    private readonly ServiceDbContext _context;

    public FishingLureService(ServiceDbContext context)
    {
        _context = context;
    }

    public async Task<List<FishingLureDTO>> GetAllAsync()
    {
        return await _context.FishingLures
            .OrderBy(l => l.Name)
            .Include(l => l.FishSpecies)
            .Select(l => new FishingLureDTO
            {
                Id = l.Id,
                Name = l.Name,
                Type = l.Type,
                Species = l.FishSpecies.Select(fs => new FishSpeciesDTO
                {
                    Id = fs.Id,
                    Name = fs.Name,
                    Description = fs.Description
                }).ToList(),
                ImageUrl = l.ImageUrl
            })
            .ToListAsync();
    }

    public async Task<FishingLureDTO?> GetByIdAsync(int id)
    {
        var lure = await _context.FishingLures
            .Include(l => l.FishSpecies)
            .FirstOrDefaultAsync(l => l.Id == id);
        if (lure == null) return null;
        return new FishingLureDTO
        {
            Id = lure.Id,
            Name = lure.Name,
            Type = lure.Type,
            Species = lure.FishSpecies.Select(fs => new FishSpeciesDTO
            {
                Id = fs.Id,
                Name = fs.Name,
                Description = fs.Description
            }).ToList(),
            ImageUrl = lure.ImageUrl
        };
    }

    public async Task<FishingLureDTO> CreateAsync(RequestFishingLureDTO dto)
    {
        var lure = new FishingLure
        {
            Name = dto.Name,
            Type = dto.Type,
            ImageUrl = dto.ImageUrl
        };
        if (dto.FishSpeciesIds != null && dto.FishSpeciesIds.Any())
        {
            var species = await _context.FishSpecies
                .Where(s => dto.FishSpeciesIds.Contains(s.Id))
                .ToListAsync();
            foreach (var s in species) lure.FishSpecies.Add(s);
        }
        _context.FishingLures.Add(lure);
        await _context.SaveChangesAsync();
        return await GetByIdAsync(lure.Id) ?? throw new Exception("Failed to retrieve created lure");
    }

    public async Task<bool> UpdateAsync(int id, RequestFishingLureDTO dto)
    {
        var lure = await _context.FishingLures
            .Include(l => l.FishSpecies)
            .FirstOrDefaultAsync(l => l.Id == id);
        if (lure == null) return false;
        lure.Name = dto.Name;
        lure.Type = dto.Type;
        if (dto.ImageUrl != null)
            lure.ImageUrl = dto.ImageUrl;
        lure.FishSpecies.Clear();
        if (dto.FishSpeciesIds != null && dto.FishSpeciesIds.Any())
        {
            var newSpecies = await _context.FishSpecies
                .Where(s => dto.FishSpeciesIds.Contains(s.Id))
                .ToListAsync();
            foreach (var s in newSpecies) lure.FishSpecies.Add(s);
        }
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var lure = await _context.FishingLures.FindAsync(id);
        if (lure == null) return false;
        _context.FishingLures.Remove(lure);
        await _context.SaveChangesAsync();
        return true;
    }
}


