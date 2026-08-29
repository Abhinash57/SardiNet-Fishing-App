using Microsoft.EntityFrameworkCore;
using PVT15_8.ApiService.Data;
using PVT15_8.ApiService.Data.Models;
using PVT15_8.ApiService.Services;
using PVT15_8.Shared.DTOs;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using Xunit;

namespace PVT15_8.UnitTests.Services;

public class FishSpeciesServiceTests
{
    private ServiceDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<ServiceDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new ServiceDbContext(options);
    }

    [Fact]
    public async Task CreateAsync_AddsSpecies()
    {
        using var context = CreateContext();
        var service = new FishSpeciesService(context);
        var dto = new RequestFishSpeciesDTO { Name = "Gädda", Description = "Rovfisk" };
        var result = await service.CreateAsync(dto);
        Assert.NotEqual(0, result.Id);
        Assert.Equal("Gädda", result.Name);
        Assert.Equal(1, context.FishSpecies.Count());
    }

    [Fact]
    public async Task UpdateAsync_ReturnsTrue_WhenExists()
    {
        using var context = CreateContext();
        var species = new FishSpecies { Id = 1, Name = "Old", Description = "Old desc" };
        context.FishSpecies.Add(species);
        await context.SaveChangesAsync();
        var service = new FishSpeciesService(context);
        var dto = new RequestFishSpeciesDTO { Name = "New", Description = "New desc" };
        var success = await service.UpdateAsync(1, dto);
        Assert.True(success);
        var updated = await context.FishSpecies.FindAsync(1);
        Assert.Equal("New", updated!.Name);
    }

    [Fact]
    public async Task DeleteAsync_RemovesSpecies()
    {
        using var context = CreateContext();
        var species = new FishSpecies { Id = 1, Name = "DeleteMe" };
        context.FishSpecies.Add(species);
        await context.SaveChangesAsync();
        var service = new FishSpeciesService(context);
        var success = await service.DeleteAsync(1);
        Assert.True(success);
        Assert.Empty(context.FishSpecies);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllSpeciesWithSpots()
    {
        using var context = CreateContext();
        var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(4326);

        var species = new FishSpecies { Id = 1, Name = "Gädda" };
        var spot = new FishingSpot
        {
            Id = 1,
            Name = "Test Lake",
            Latitude = 59.33,
            Longitude = 18.06,
            Location = geometryFactory.CreatePoint(new Coordinate(18.06, 59.33))
        };
        context.FishSpecies.Add(species);
        context.FishingSpots.Add(spot);
        context.FishingSpeciesFishingSpots.Add(new FishingSpeciesFishingSpot
        {
            FishSpecies = species,
            FishingSpot = spot,
            FishSpeciesFrequencyId = 2
        });
        await context.SaveChangesAsync();

        var service = new FishSpeciesService(context);
        var result = await service.GetAllAsync();
        Assert.Single(result);
        Assert.Single(result[0].FishingSpots);
        Assert.Equal("Test Lake", result[0].FishingSpots[0].Name);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsSpecies_WhenExists()
    {
        using var context = CreateContext();
        var species = new FishSpecies { Id = 1, Name = "Gädda" };
        context.FishSpecies.Add(species);
        await context.SaveChangesAsync();

        var service = new FishSpeciesService(context);
        var result = await service.GetByIdAsync(1);
        Assert.NotNull(result);
        Assert.Equal("Gädda", result!.Name);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
    {
        using var context = CreateContext();
        var service = new FishSpeciesService(context);
        var result = await service.GetByIdAsync(999);
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsFalse_WhenSpeciesNotFound()
    {
        using var context = CreateContext();
        var service = new FishSpeciesService(context);
        var dto = new RequestFishSpeciesDTO { Name = "X" };
        var success = await service.UpdateAsync(999, dto);
        Assert.False(success);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsFalse_WhenSpeciesNotFound()
    {
        using var context = CreateContext();
        var service = new FishSpeciesService(context);
        var success = await service.DeleteAsync(999);
        Assert.False(success);
    }
}