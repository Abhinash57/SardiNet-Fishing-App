using Microsoft.EntityFrameworkCore;
using PVT15_8.ApiService.Data;
using PVT15_8.ApiService.Data.Models;
using PVT15_8.ApiService.Services;
using PVT15_8.Shared.DTOs;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using Xunit;

namespace PVT15_8.UnitTests.Services;

public class CatchReportServiceTests
{
    private ServiceDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<ServiceDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new ServiceDbContext(options);
    }

    [Fact]
    public async Task CreateAsync_AddsReport_ReturnsDto()
    {
        using var context = CreateContext();
        var service = new CatchReportService(context);
        var dto = new RequestCatchReportDTO
        {
            UserId = "user1",
            FishSpeciesId = 1,
            FishingSpotId = 1,
            FishingLureId = 1,
            CatchDate = DateTime.UtcNow,
            WeightKg = 2.5,
            LengthCm = 50,
            Description = "Nice fish"
        };

        var result = await service.CreateAsync(dto);

        Assert.NotEqual(0, result.Id);
        Assert.Equal("user1", result.UserId);
        Assert.Equal(1, context.CatchReports.Count());
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
    {
        using var context = CreateContext();
        var service = new CatchReportService(context);
        var result = await service.GetByIdAsync(999);
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsFalse_WhenWrongUser()
    {
        using var context = CreateContext();
        var report = new CatchReport { Id = 1, UserId = "owner" };
        context.CatchReports.Add(report);
        await context.SaveChangesAsync();

        var service = new CatchReportService(context);
        var dto = new RequestCatchReportDTO
        {
            UserId = "hacker",
            FishSpeciesId = 2,
            FishingSpotId = 2,
            FishingLureId = 2,
            CatchDate = DateTime.UtcNow,
            WeightKg = 1,
            LengthCm = 30
        };
        var success = await service.UpdateAsync(1, dto, "hacker");
        Assert.False(success);
    }

    [Fact]
    public async Task DeleteAsync_RemovesReport_WhenOwned()
    {
        using var context = CreateContext();
        var report = new CatchReport { Id = 1, UserId = "owner" };
        context.CatchReports.Add(report);
        await context.SaveChangesAsync();

        var service = new CatchReportService(context);
        var success = await service.DeleteAsync(1, "owner");
        Assert.True(success);
        Assert.Empty(context.CatchReports);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllReports()
    {
        using var context = CreateContext();
        var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(4326);

        // Create required related entities
        var fishSpecies = new FishSpecies { Id = 1, Name = "Gädda" };
        var fishingSpot = new FishingSpot
        {
            Id = 1,
            Name = "Spot1",
            Latitude = 59.33,
            Longitude = 18.06,
            Location = geometryFactory.CreatePoint(new Coordinate(18.06, 59.33))
        };
        context.FishSpecies.Add(fishSpecies);
        context.FishingSpots.Add(fishingSpot);
        await context.SaveChangesAsync();

        // Now create catch reports referencing those entities
        context.CatchReports.AddRange(
            new CatchReport { Id = 1, UserId = "u1", CatchDate = DateTime.UtcNow, FishSpeciesId = 1, FishingSpotId = 1 },
            new CatchReport { Id = 2, UserId = "u2", CatchDate = DateTime.UtcNow, FishSpeciesId = 1, FishingSpotId = 1 }
        );
        await context.SaveChangesAsync();

        var service = new CatchReportService(context);
        var reports = await service.GetAllAsync();
        Assert.Equal(2, reports.Count);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsReport_WhenExists()
    {
        using var context = CreateContext();
        var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(4326);

        var fishSpecies = new FishSpecies { Id = 1, Name = "Gädda" };
        var fishingSpot = new FishingSpot
        {
            Id = 1,
            Name = "Spot1",
            Latitude = 59.33,
            Longitude = 18.06,
            Location = geometryFactory.CreatePoint(new Coordinate(18.06, 59.33))
        };
        context.FishSpecies.Add(fishSpecies);
        context.FishingSpots.Add(fishingSpot);
        await context.SaveChangesAsync();

        var report = new CatchReport
        {
            Id = 1,
            UserId = "u1",
            CatchDate = DateTime.UtcNow,
            FishSpeciesId = 1,
            FishingSpotId = 1
        };
        context.CatchReports.Add(report);
        await context.SaveChangesAsync();

        var service = new CatchReportService(context);
        var result = await service.GetByIdAsync(1);
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task UpdateAsync_Succeeds_WhenOwned()
    {
        using var context = CreateContext();
        var report = new CatchReport { Id = 1, UserId = "owner", FishSpeciesId = 1, FishingSpotId = 1 };
        context.CatchReports.Add(report);
        await context.SaveChangesAsync();

        var service = new CatchReportService(context);
        var dto = new RequestCatchReportDTO
        {
            UserId = "owner",
            FishSpeciesId = 2,
            FishingSpotId = 2,
            FishingLureId = 1,
            CatchDate = DateTime.UtcNow,
            WeightKg = 3,
            LengthCm = 60
        };
        var success = await service.UpdateAsync(1, dto, "owner");
        Assert.True(success);
        var updated = await context.CatchReports.FindAsync(1);
        Assert.Equal(2, updated!.FishSpeciesId);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsFalse_WhenReportNotFound()
    {
        using var context = CreateContext();
        var service = new CatchReportService(context);
        var success = await service.DeleteAsync(999, "any");
        Assert.False(success);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsFalse_WhenNotOwned()
    {
        using var context = CreateContext();
        var report = new CatchReport { Id = 1, UserId = "owner" };
        context.CatchReports.Add(report);
        await context.SaveChangesAsync();

        var service = new CatchReportService(context);
        var success = await service.DeleteAsync(1, "hacker");
        Assert.False(success);
        Assert.NotNull(await context.CatchReports.FindAsync(1));
    }
}