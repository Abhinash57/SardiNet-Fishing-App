using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using PVT15_8.ApiService.Data;
using PVT15_8.ApiService.Data.Models;
using PVT15_8.ApiService.Services;
using PVT15_8.Shared.DTOs;

namespace PVT15_8.ApiService.Endpoints;

public static class RecommendationEndpoints
{
    public const int SRID = 4326;
    public static void MapRecommendationEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/recommendations")
            .WithTags("Recommendations");

        group.MapGet("/nearby", async Task<IResult> (
            [FromQuery] double userLat,
            [FromQuery] double userLon,
            [FromServices] ServiceDbContext context,
            [FromQuery] int take = 1) =>
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(SRID);
            var userLocation = geometryFactory.CreatePoint(new Coordinate(userLon, userLat));

            var nearbySpots = await context.FishingSpots
                .Where(fs => !fs.IsForbidden && !fs.IsDeleted)
                .Include(fs => fs.Reviews)
                .Include(fs => fs.SpeciesFishingSpots)
                    .ThenInclude(fs => fs.FishSpecies)
                        .ThenInclude(fs => fs.FishingLures)
                .OrderBy(fs => fs.Location.Distance(userLocation))
                .Take(take)
                .ToListAsync();

            if (nearbySpots.Count == 0) return TypedResults.Ok(nearbySpots); // Returns an empty array gracefully 

            var resultList = nearbySpots.Select(spot =>
            {
                var dto = FishingSpotService.MapToDto(spot);

                dto.AverageRating = spot.Reviews.Count != 0
                    ? (int)Math.Round(spot.Reviews.Average(r => r.Rating))
                    : 0;

                dto.DistanceFromUserMeters = GeoCalculator.GetDistanceInMeters(
                    userLat, userLon,
                    spot.Latitude, spot.Longitude);

                return dto;
            }).ToList();

            return TypedResults.Ok(resultList);
        })
        .WithName("GetNearbySpots")
        .WithSummary("Gets a list of the closest spots");

        group.MapGet("/random", async Task<IResult> (
            [FromServices] ServiceDbContext context,
            [FromQuery] int take = 1) =>
        {
            var randomSpots = await context.FishingSpots
                .Where(fs => !fs.IsForbidden && !fs.IsDeleted)
                .Include(fs => fs.Reviews)
                .Include(fs => fs.SpeciesFishingSpots)
                    .ThenInclude(fs => fs.FishSpecies)
                        .ThenInclude(fs => fs.FishingLures)
                .OrderBy(fs => Guid.NewGuid())
                .Take(take)
                .ToListAsync();

            if (randomSpots.Count == 0) return TypedResults.Ok(randomSpots);

            var resultList = randomSpots.Select(spot =>
            {
                var dto = FishingSpotService.MapToDto(spot);

                dto.AverageRating = spot.Reviews.Count != 0
                    ? (int)Math.Round(spot.Reviews.Average(r => r.Rating))
                    : 0;

                dto.DistanceFromUserMeters = 0;

                return dto;
            }).ToList();

            return TypedResults.Ok(resultList);
        })
        .WithName("GetRandomSpots")
        .WithSummary("Gets a random list of fishing spots when location is denied");
    }
}
