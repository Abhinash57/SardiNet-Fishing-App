using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using PVT15_8.ApiService.Data;
using PVT15_8.ApiService.Data.Models;
using PVT15_8.Shared.DTOs;
using System.Security.Claims;

namespace PVT15_8.ApiService.Services;

public class FishingSpotService : IFishingSpotService
{
    private readonly ServiceDbContext _context;
    private const int SRID = 4326;

    public FishingSpotService(ServiceDbContext context)
    {
        _context = context;
    }

    public async Task<List<FishingSpotMarkerDTO>> GetMarkersAsync()
    {
        return await _context.FishingSpots
            .Where(s => !s.IsDeleted)
            .Select(spot => new FishingSpotMarkerDTO
            {
                Id = spot.Id,
                Name = spot.Name,
                Latitude = spot.Latitude,
                Longitude = spot.Longitude
            })
            .ToListAsync();
    }

    public async Task<List<FishingSpotDTO>> GetAllAsync()
    {
        return await _context.FishingSpots
            .Where(s => !s.IsDeleted)
            .Include(s => s.Reviews)
            .Include(s => s.CatchReports)
                .ThenInclude(cr => cr.FishSpecies)
            .Include(s => s.CatchReports)
                .ThenInclude(cr => cr.FishingLure)
            .Select(spot => MapToDto(spot))
            .ToListAsync();
    }

    public async Task<FishingSpotDTO?> GetByIdAsync(int id)
    {
        var spot = await _context.FishingSpots
            .Include(s => s.Reviews)
            .Include(s => s.CatchReports)
                .ThenInclude(cr => cr.FishSpecies)
            .Include(s => s.CatchReports)
                .ThenInclude(cr => cr.FishingLure)
            .Include(s => s.SpeciesFishingSpots)
                .ThenInclude(sf => sf.FishSpecies)
                    .ThenInclude(fs => fs.FishingLures)
            .FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);
        return spot == null ? null : MapToDto(spot);
    }

    public async Task<FishingSpotDTO> CreateAsync(RequestFishingSpotDTO dto)
    {
        var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(SRID);
        var spot = new FishingSpot
        {
            Name = dto.Name,
            Description = dto.Description,
            Depth = dto.Depth,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
            Location = geometryFactory.CreatePoint(new Coordinate(dto.Longitude, dto.Latitude))
        };
        _context.FishingSpots.Add(spot);
        await _context.SaveChangesAsync();
        return MapToDto(spot);
    }

    public async Task<List<FishingSpotMarkerDTO>> GetFilteredMarkersAsync(FishingSpotFilterDto filter)
    {
        var query = _context.FishingSpots.Where(s => !s.IsDeleted);

        if (filter.SpeciesIds != null && filter.SpeciesIds.Any())
        {
            query = query.Where(s => s.SpeciesFishingSpots.Any(sfs => filter.SpeciesIds.Contains(sfs.FishSpeciesId)));
        }

        return await query.Select(spot => new FishingSpotMarkerDTO
        {
            Id = spot.Id,
            Name = spot.Name,
            Latitude = spot.Latitude,
            Longitude = spot.Longitude
        }).ToListAsync();
    }

    public async Task<bool> UpdateAsync(int id, RequestFishingSpotDTO dto)
    {
        var spot = await _context.FishingSpots
            .FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);
        if (spot == null) return false;

        var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(SRID);
        spot.Name = dto.Name;
        spot.Description = dto.Description;
        spot.Depth = dto.Depth;
        spot.Latitude = dto.Latitude;
        spot.Longitude = dto.Longitude;
        spot.Location = geometryFactory.CreatePoint(new Coordinate(dto.Longitude, dto.Latitude));

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var spot = await _context.FishingSpots.FindAsync(id);
        if (spot == null) return false;
        spot.IsDeleted = true;
        await _context.SaveChangesAsync();
        return true;
    }

    public static FishingSpotDTO MapToDto(FishingSpot spot)
    {
        return new FishingSpotDTO
        {
            Id = spot.Id,
            Name = spot.Name,
            Description = spot.Description,
            Depth = spot.Depth,
            Latitude = spot.Latitude,
            Longitude = spot.Longitude,
            HasRules = spot.HasRules,
            IsForbidden = spot.IsForbidden,
            IsFishingCardRequired = spot.IsFishingCardRequired,
            RecommendedLures = spot.SpeciesFishingSpots
                .Where(fs => fs.Frequency == FishSpeciesFrequency.High)
                .SelectMany(fs => fs.FishSpecies.FishingLures.Select(lure => new
                {
                    Lure = lure,
                    SpeciesName = fs.FishSpecies.Name
                }))
                .GroupBy(x => x.Lure.Id)
                .Select(g =>
                {
                    var lure = g.First().Lure;
                    return new FishingLureDTO
                    {
                        Id = lure.Id,
                        ImageUrl = lure.ImageUrl,
                        Name = lure.Name,
                        Type = lure.Type,
                        SpeciesNames = g.Select(x => new LureTargetSpeciesDTO { Name = x.SpeciesName }).ToList()
                    };
                }).ToList(),
            FishSpecies = spot.SpeciesFishingSpots.Select(fs => new FishSpeciesDTO
            {
                Name = fs.FishSpecies.Name,
                Frequency = fs.Frequency,
                Description = fs.FishSpecies.Description,
                ImageUrl = fs.FishSpecies.ImageUrl
            }).ToList(),

            Reviews = spot.Reviews.Select(r => new ReviewDTO
            {
                Id = r.Id,
                UserId = r.UserId,
                FishingSpotId = r.FishingSpotId,
                Rating = r.Rating,
                Comment = r.Comment
            }).ToList(),

            CatchReports = spot.CatchReports.Select(cr => new CatchReportDTO
            {
                Id = cr.Id,
                UserId = cr.UserId,
                FishSpeciesId = cr.FishSpeciesId,
                FishSpeciesName = cr.FishSpecies?.Name,
                FishingSpotId = cr.FishingSpotId,
                FishingSpotName = spot.Name,
                FishingLureId = cr.FishingLureId,
                FishingLureName = cr.FishingLure?.Name,
                CatchDate = cr.CatchDate,
                WeightKg = cr.WeightKg,
                LengthCm = cr.LengthCm,
                Description = cr.Description
            }).ToList()
        };
    }
}