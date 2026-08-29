using Microsoft.EntityFrameworkCore;
using PVT15_8.ApiService.Data;
using PVT15_8.ApiService.Data.Models;
using PVT15_8.Shared.DTOs;

namespace PVT15_8.ApiService.Services;

public class FishSpeciesService : IFishSpeciesService
{
    private readonly ServiceDbContext _context;

    public FishSpeciesService(ServiceDbContext context)
    {
        _context = context;
    }

    public async Task<List<FishSpeciesDTO>> GetAllAsync()
    {
        var species = await _context.FishSpecies
            .Include(f => f.FishingSpeciesFishingSpots)
                .ThenInclude(fs => fs.FishingSpot)
            .ToListAsync();

        return species.Select(f => new FishSpeciesDTO
        {
            Id = f.Id,
            Name = f.Name,
            Description = f.Description,
            ImageUrl = f.ImageUrl,
            FishingSpots = f.FishingSpeciesFishingSpots
                .Select(fs => new FishingSpotInfo
                {
                    Id = fs.FishingSpot.Id,
                    Name = fs.FishingSpot.Name
                })
                .DistinctBy(s => s.Id)   
                .OrderBy(s => s.Name)
                .ToList()
        }).ToList();
    }


    public async Task<FishSpeciesDTO?> GetByIdAsync(int id)
    {
        var species = await _context.FishSpecies.FindAsync(id);
        return species == null ? null : new FishSpeciesDTO
        {
            Id = species.Id,
            Name = species.Name,
            Description = species.Description,
            ImageUrl = species.ImageUrl
        };
    }

    public async Task<FishSpeciesDTO> CreateAsync(RequestFishSpeciesDTO dto)
    {
        var species = new FishSpecies
        {
            Name = dto.Name,
            Description = dto.Description,
            ImageUrl = dto.ImageUrl
        };
        _context.FishSpecies.Add(species);
        await _context.SaveChangesAsync();
        return new FishSpeciesDTO
        {
            Id = species.Id,
            Name = species.Name,
            Description = species.Description,
            ImageUrl = species.ImageUrl
        };
    }

    public async Task<bool> UpdateAsync(int id, RequestFishSpeciesDTO dto)
    {
        var species = await _context.FishSpecies.FindAsync(id);
        if (species == null) return false;
        species.Name = dto.Name;
        species.Description = dto.Description;
        if (dto.ImageUrl != null)
            species.ImageUrl = dto.ImageUrl;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var species = await _context.FishSpecies.FindAsync(id);
        if (species == null) return false;
        _context.FishSpecies.Remove(species);
        await _context.SaveChangesAsync();
        return true;
    }
}