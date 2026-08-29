using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using PVT15_8.ApiService.Data;
using PVT15_8.ApiService.Data.Models;
using PVT15_8.ApiService.Services;
using PVT15_8.Shared.DTOs;
using Xunit;

namespace PVT15_8.UnitTests.Services;

public class FishingSpotServiceTests
{
    private ServiceDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<ServiceDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new ServiceDbContext(options);
    }

    [Fact]
    public async Task GetMarkersAsync_ReturnsOnlyNonDeleted()
    {
        using var context = CreateContext();
        var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(4326);
        var spot1 = new FishingSpot { Name = "Spot1", IsDeleted = false, Latitude = 1, Longitude = 1, Location = geometryFactory.CreatePoint(new Coordinate(1,1)) };
        var spot2 = new FishingSpot { Name = "Spot2", IsDeleted = true, Latitude = 2, Longitude = 2, Location = geometryFactory.CreatePoint(new Coordinate(2,2)) };
        context.FishingSpots.AddRange(spot1, spot2);
        await context.SaveChangesAsync();

        var service = new FishingSpotService(context);
        var markers = await service.GetMarkersAsync();
        Assert.Single(markers);
        Assert.Equal("Spot1", markers[0].Name);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsOnlyNonDeleted()
    {
        using var context = CreateContext();
        var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(4326);
        context.FishingSpots.AddRange(
            new FishingSpot { Name = "Active", IsDeleted = false, Latitude = 1, Longitude = 1, Location = geometryFactory.CreatePoint(new Coordinate(1,1)) },
            new FishingSpot { Name = "Deleted", IsDeleted = true, Latitude = 2, Longitude = 2, Location = geometryFactory.CreatePoint(new Coordinate(2,2)) }
        );
        await context.SaveChangesAsync();

        var service = new FishingSpotService(context);
        var spots = await service.GetAllAsync();
        Assert.Single(spots);
        Assert.Equal("Active", spots[0].Name);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenDeleted()
    {
        using var context = CreateContext();
        var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(4326);
        var spot = new FishingSpot { Id = 1, Name = "Spot", IsDeleted = true, Latitude = 1, Longitude = 1, Location = geometryFactory.CreatePoint(new Coordinate(1,1)) };
        context.FishingSpots.Add(spot);
        await context.SaveChangesAsync();

        var service = new FishingSpotService(context);
        var result = await service.GetByIdAsync(1);
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_AddsSpot_WithLocation()
    {
        using var context = CreateContext();
        var service = new FishingSpotService(context);
        var dto = new RequestFishingSpotDTO
        {
            Name = "New Spot",
            Latitude = 59.33,
            Longitude = 18.06,
            Depth = 12
        };
        var result = await service.CreateAsync(dto);
        Assert.NotEqual(0, result.Id);
        Assert.Equal("New Spot", result.Name);
        var saved = await context.FishingSpots.FindAsync(result.Id);
        Assert.NotNull(saved!.Location);
        Assert.Equal(18.06, saved.Location.X);
    }

    [Fact]
    public async Task UpdateAsync_UpdatesProperties()
    {
        using var context = CreateContext();
        var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(4326);
        var spot = new FishingSpot { Id = 1, Name = "Old", IsDeleted = false, Latitude = 1, Longitude = 1, Location = geometryFactory.CreatePoint(new Coordinate(1,1)) };
        context.FishingSpots.Add(spot);
        await context.SaveChangesAsync();

        var service = new FishingSpotService(context);
        var dto = new RequestFishingSpotDTO { Name = "Updated", Latitude = 2, Longitude = 2, Depth = 10 };
        var success = await service.UpdateAsync(1, dto);
        Assert.True(success);
        var updated = await context.FishingSpots.FindAsync(1);
        Assert.Equal("Updated", updated!.Name);
        Assert.Equal(2, updated.Latitude);
    }

    [Fact]
    public async Task DeleteAsync_SetsIsDeletedTrue()
    {
        using var context = CreateContext();
        var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(4326);
        var spot = new FishingSpot { Id = 1, Name = "ToDelete", IsDeleted = false, Latitude = 1, Longitude = 1, Location = geometryFactory.CreatePoint(new Coordinate(1,1)) };
        context.FishingSpots.Add(spot);
        await context.SaveChangesAsync();

        var service = new FishingSpotService(context);
        var success = await service.DeleteAsync(1);
        Assert.True(success);
        var deleted = await context.FishingSpots.FindAsync(1);
        Assert.True(deleted!.IsDeleted);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
    {
        using var context = CreateContext();
        var service = new FishingSpotService(context);
        var result = await service.GetByIdAsync(999);
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsFalse_WhenSpotNotFound()
    {
        using var context = CreateContext();
        var service = new FishingSpotService(context);
        var dto = new RequestFishingSpotDTO { Name = "X", Latitude = 1, Longitude = 1 };
        var success = await service.UpdateAsync(999, dto);
        Assert.False(success);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsFalse_WhenSpotNotFound()
    {
        using var context = CreateContext();
        var service = new FishingSpotService(context);
        var success = await service.DeleteAsync(999);
        Assert.False(success);
    }

    [Fact]
    public async Task GetFilteredMarkersAsync_FiltersBySpecies()
    {
        using var context = CreateContext();
        var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(4326);

        var species1 = new FishSpecies { Id = 1, Name = "Gädda" };
        var species2 = new FishSpecies { Id = 2, Name = "Abborre" };
        context.FishSpecies.AddRange(species1, species2);

        var spot1 = new FishingSpot
        {
            Name = "Spot1",
            Latitude = 1,
            Longitude = 1,
            Location = geometryFactory.CreatePoint(new Coordinate(1, 1)),
            SpeciesFishingSpots = new List<FishingSpeciesFishingSpot>
            {
                new() { FishSpecies = species1, FishSpeciesFrequencyId = 2 }
            }
        };
        var spot2 = new FishingSpot
        {
            Name = "Spot2",
            Latitude = 2,
            Longitude = 2,
            Location = geometryFactory.CreatePoint(new Coordinate(2, 2)),
            SpeciesFishingSpots = new List<FishingSpeciesFishingSpot>
            {
                new() { FishSpecies = species2, FishSpeciesFrequencyId = 1 }
            }
        };
        context.FishingSpots.AddRange(spot1, spot2);
        await context.SaveChangesAsync();

        var service = new FishingSpotService(context);
        var filter = new FishingSpotFilterDto { SpeciesIds = new List<int> { 1 } };
        var markers = await service.GetFilteredMarkersAsync(filter);
        Assert.Single(markers);
        Assert.Equal("Spot1", markers[0].Name);
    }
}