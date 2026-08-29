using Microsoft.EntityFrameworkCore;
using PVT15_8.ApiService.Data;
using PVT15_8.ApiService.Data.Models;
using PVT15_8.ApiService.Services;
using PVT15_8.Shared.DTOs;
using Xunit;

namespace PVT15_8.UnitTests.Services;

public class ReviewServiceTests
{
    private ServiceDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<ServiceDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new ServiceDbContext(options);
    }

    [Fact]
    public async Task CreateAsync_AddsReview_ReturnsDto()
    {
        using var context = CreateContext();
        var service = new ReviewService(context);
        var dto = new RequestReviewDTO
        {
            UserId = "user1",
            FishingSpotId = 1,
            Rating = 5,
            Comment = "Great spot!"
        };

        var result = await service.CreateAsync(dto);
        Assert.NotEqual(0, result.Id);
        Assert.Equal("user1", result.UserId);
        Assert.Equal(1, context.Reviews.Count());
    }

    [Fact]
    public async Task UpdateAsync_ChangesRating()
    {
        using var context = CreateContext();
        var review = new Review { Id = 1, UserId = "user1", FishingSpotId = 1, Rating = 3 };
        context.Reviews.Add(review);
        await context.SaveChangesAsync();

        var service = new ReviewService(context);
        var dto = new RequestReviewDTO { UserId = "user1", FishingSpotId = 1, Rating = 5, Comment = "Better now" };
        var success = await service.UpdateAsync(1, dto);
        Assert.True(success);
        Assert.Equal(5, context.Reviews.First().Rating);
    }

    [Fact]
    public async Task DeleteAsync_RemovesReview()
    {
        using var context = CreateContext();
        var review = new Review { Id = 1, UserId = "user1", FishingSpotId = 1, Rating = 4 };
        context.Reviews.Add(review);
        await context.SaveChangesAsync();

        var service = new ReviewService(context);
        var success = await service.DeleteAsync(1);
        Assert.True(success);
        Assert.Empty(context.Reviews);
    }
    
    [Fact]
    public async Task GetAllAsync_ReturnsAllReviews()
    {
        using var context = CreateContext();
        context.Reviews.AddRange(
            new Review { Id = 1, UserId = "u1", FishingSpotId = 1, Rating = 4 },
            new Review { Id = 2, UserId = "u2", FishingSpotId = 1, Rating = 5 }
        );
        await context.SaveChangesAsync();

        var service = new ReviewService(context);
        var reviews = await service.GetAllAsync();
        Assert.Equal(2, reviews.Count);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsReview_WhenExists()
    {
        using var context = CreateContext();
        var review = new Review { Id = 1, UserId = "u1", FishingSpotId = 1, Rating = 4 };
        context.Reviews.Add(review);
        await context.SaveChangesAsync();

        var service = new ReviewService(context);
        var result = await service.GetByIdAsync(1);
        Assert.NotNull(result);
        Assert.Equal(4, result!.Rating);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
    {
        using var context = CreateContext();
        var service = new ReviewService(context);
        var result = await service.GetByIdAsync(999);
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsFalse_WhenReviewNotFound()
    {
        using var context = CreateContext();
        var service = new ReviewService(context);
        var dto = new RequestReviewDTO { UserId = "u", FishingSpotId = 1, Rating = 5 };
        var success = await service.UpdateAsync(999, dto);
        Assert.False(success);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsFalse_WhenReviewNotFound()
    {
        using var context = CreateContext();
        var service = new ReviewService(context);
        var success = await service.DeleteAsync(999);
        Assert.False(success);
    }
}