using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using PVT15_8.ApiService.Data;
using PVT15_8.ApiService.Data.Models;

namespace PVT15_8.ApiService.Services;

public class RecommendationService : IRecommendationService
{
    private readonly ServiceDbContext _context;
    private const int SRID = 4326;

    public RecommendationService(ServiceDbContext context)
    {
        _context = context;
    }

    public async Task<List<FishingSpot>> GetRecommendationsAsync(double userLat, double userLon, int? fishSpeciesId = null, int take = 5)
    {
        var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(SRID);
        var userLocation = geometryFactory.CreatePoint(new Coordinate(userLon, userLat));

        var query = _context.FishingSpots.AsQueryable();

        if (fishSpeciesId != null)
        {
            query = query.Where(fs => fs.CatchReports.Any(cr => cr.FishSpeciesId == fishSpeciesId));
        }

        return await query
            .OrderBy(fs => fs.Location.Distance(userLocation))
            .Take(take)
            .ToListAsync();
    }

    public async Task<FishingSpot?> AddAsync(FishingSpot spot)
    {
        var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(SRID);
        spot.Location = geometryFactory.CreatePoint(new Coordinate(spot.Longitude, spot.Latitude));
        _context.FishingSpots.Add(spot);
        await _context.SaveChangesAsync();
        return spot;
    }

    public async Task<List<FishingSpot>> GetAllAsync()
    {
        return await _context.FishingSpots.ToListAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var spot = await _context.FishingSpots.FindAsync(id);
        if (spot == null) return false;
        _context.FishingSpots.Remove(spot);
        await _context.SaveChangesAsync();
        return true;
    }
}