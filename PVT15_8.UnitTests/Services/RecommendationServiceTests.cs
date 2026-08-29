using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using PVT15_8.ApiService.Data;
using PVT15_8.ApiService.Data.Models;
using PVT15_8.ApiService.Services;
using Xunit;

namespace PVT15_8.UnitTests.Services;

public class RecommendationServiceTests
{
    private ServiceDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<ServiceDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new ServiceDbContext(options);
    }

    [Fact]
    public async Task GetRecommendationsAsync_ReturnsOrderedByDistance()
    {
        using var context = CreateContext();
        var service = new RecommendationService(context);
        var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(4326);

        var spot1 = new FishingSpot
        {
            Name = "Close spot",
            Latitude = 59.33,
            Longitude = 18.06,
            Location = geometryFactory.CreatePoint(new Coordinate(18.06, 59.33))
        };
        var spot2 = new FishingSpot
        {
            Name = "Far spot",
            Latitude = 59.35,
            Longitude = 18.10,
            Location = geometryFactory.CreatePoint(new Coordinate(18.10, 59.35))
        };
        await service.AddAsync(spot1);
        await service.AddAsync(spot2);

        var recommendations = await service.GetRecommendationsAsync(59.33, 18.06, take: 2);

        Assert.Equal(2, recommendations.Count);
        Assert.Equal("Close spot", recommendations[0].Name);
    }

    [Fact]
    public async Task AddAsync_SetsLocation()
    {
        using var context = CreateContext();
        var service = new RecommendationService(context);
        var spot = new FishingSpot
        {
            Name = "New spot",
            Latitude = 59.33,
            Longitude = 18.06
        };
        var added = await service.AddAsync(spot);
        Assert.NotNull(added);
        Assert.NotNull(added.Location);
        Assert.Equal(18.06, added.Location.X);
        Assert.Equal(59.33, added.Location.Y);
    }

    [Fact]
    public async Task DeleteAsync_RemovesSpot()
    {
        using var context = CreateContext();
        var service = new RecommendationService(context);
        var spot = new FishingSpot
        {
            Name = "To delete",
            Latitude = 59.33,
            Longitude = 18.06
        };
        var added = await service.AddAsync(spot);
        Assert.NotNull(added);

        var success = await service.DeleteAsync(added.Id);
        Assert.True(success);
        Assert.Empty(await service.GetAllAsync());
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllSpots()
    {
        using var context = CreateContext();
        var service = new RecommendationService(context);
        var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(4326);

        var spot1 = new FishingSpot { Name = "Spot1", Latitude = 59.33, Longitude = 18.06 };
        var spot2 = new FishingSpot { Name = "Spot2", Latitude = 59.34, Longitude = 18.07 };
        await service.AddAsync(spot1);
        await service.AddAsync(spot2);

        var all = await service.GetAllAsync();
        Assert.Equal(2, all.Count);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsFalse_WhenSpotNotFound()
    {
        using var context = CreateContext();
        var service = new RecommendationService(context);
        var success = await service.DeleteAsync(999);
        Assert.False(success);
    }

    [Fact]
    public async Task GetRecommendationsAsync_FiltersByFishSpeciesId()
    {
        using var context = CreateContext();
        var service = new RecommendationService(context);
        var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(4326);

        var species1 = new FishSpecies { Id = 1, Name = "Gädda" };
        var species2 = new FishSpecies { Id = 2, Name = "Abborre" };
        context.FishSpecies.AddRange(species1, species2);

        var spot1 = new FishingSpot
        {
            Name = "Gädda spot",
            Latitude = 59.33,
            Longitude = 18.06,
            Location = geometryFactory.CreatePoint(new Coordinate(18.06, 59.33)),
            CatchReports = new List<CatchReport>
            {
                new() { FishSpecies = species1, UserId = "u1", CatchDate = DateTime.UtcNow }
            }
        };
        var spot2 = new FishingSpot
        {
            Name = "Abborre spot",
            Latitude = 59.34,
            Longitude = 18.07,
            Location = geometryFactory.CreatePoint(new Coordinate(18.07, 59.34)),
            CatchReports = new List<CatchReport>
            {
                new() { FishSpecies = species2, UserId = "u1", CatchDate = DateTime.UtcNow }
            }
        };
        await service.AddAsync(spot1);
        await service.AddAsync(spot2);

        var recommendations = await service.GetRecommendationsAsync(59.33, 18.06, fishSpeciesId: 1, take: 5);
        Assert.Single(recommendations);
        Assert.Equal("Gädda spot", recommendations[0].Name);
    }
}