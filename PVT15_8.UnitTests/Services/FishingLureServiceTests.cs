using Microsoft.EntityFrameworkCore;
using PVT15_8.ApiService.Data;
using PVT15_8.ApiService.Data.Models;
using PVT15_8.ApiService.Services;
using PVT15_8.Shared.DTOs;
using Xunit;

namespace PVT15_8.UnitTests.Services;

public class FishingLureServiceTests
{
    private ServiceDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<ServiceDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new ServiceDbContext(options);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllLures()
    {
        using var context = CreateContext();
        context.FishingLures.AddRange(
            new FishingLure { Name = "Lure1", Type = "Spinner" },
            new FishingLure { Name = "Lure2", Type = "Wobbler" }
        );
        await context.SaveChangesAsync();

        var service = new FishingLureService(context);
        var lures = await service.GetAllAsync();
        Assert.Equal(2, lures.Count);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsCorrectLure()
    {
        using var context = CreateContext();
        var lure = new FishingLure { Id = 1, Name = "Mepps", Type = "Spinner" };
        context.FishingLures.Add(lure);
        await context.SaveChangesAsync();

        var service = new FishingLureService(context);
        var result = await service.GetByIdAsync(1);
        Assert.NotNull(result);
        Assert.Equal("Mepps", result!.Name);
    }

    [Fact]
    public async Task CreateAsync_AddsLureWithSpecies()
    {
        using var context = CreateContext();
        var species1 = new FishSpecies { Id = 1, Name = "Gädda" };
        var species2 = new FishSpecies { Id = 2, Name = "Abborre" };
        context.FishSpecies.AddRange(species1, species2);
        await context.SaveChangesAsync();

        var service = new FishingLureService(context);
        var dto = new RequestFishingLureDTO
        {
            Name = "Rapala",
            Type = "Wobbler",
            FishSpeciesIds = new List<int> { 1, 2 }
        };
        var result = await service.CreateAsync(dto);
        Assert.Equal("Rapala", result.Name);
        Assert.Equal(2, result.Species.Count);
    }

    [Fact]
    public async Task UpdateAsync_UpdatesNameAndSpecies()
    {
        using var context = CreateContext();
        var lure = new FishingLure { Id = 1, Name = "Old", Type = "OldType" };
        var species1 = new FishSpecies { Id = 1, Name = "Gädda" };
        var species2 = new FishSpecies { Id = 2, Name = "Abborre" };
        context.FishingLures.Add(lure);
        context.FishSpecies.AddRange(species1, species2);
        await context.SaveChangesAsync();

        var service = new FishingLureService(context);
        var dto = new RequestFishingLureDTO
        {
            Name = "New Name",
            Type = "New Type",
            FishSpeciesIds = new List<int> { 2 }  // only species2
        };
        var success = await service.UpdateAsync(1, dto);
        Assert.True(success);
        var updated = await context.FishingLures.Include(l => l.FishSpecies).FirstAsync(l => l.Id == 1);
        Assert.Equal("New Name", updated.Name);
        Assert.Single(updated.FishSpecies);
        Assert.Equal("Abborre", updated.FishSpecies.First().Name);
    }

    [Fact]
    public async Task DeleteAsync_RemovesLure()
    {
        using var context = CreateContext();
        var lure = new FishingLure { Id = 1, Name = "DeleteMe" };
        context.FishingLures.Add(lure);
        await context.SaveChangesAsync();

        var service = new FishingLureService(context);
        var success = await service.DeleteAsync(1);
        Assert.True(success);
        Assert.Null(await context.FishingLures.FindAsync(1));
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
    {
        using var context = CreateContext();
        var service = new FishingLureService(context);
        var result = await service.GetByIdAsync(999);
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsFalse_WhenLureNotFound()
    {
        using var context = CreateContext();
        var service = new FishingLureService(context);
        var dto = new RequestFishingLureDTO { Name = "X", Type = "Y" };
        var success = await service.UpdateAsync(999, dto);
        Assert.False(success);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsFalse_WhenLureNotFound()
    {
        using var context = CreateContext();
        var service = new FishingLureService(context);
        var success = await service.DeleteAsync(999);
        Assert.False(success);
    }
}