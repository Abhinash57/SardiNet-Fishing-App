using PVT15_8.Shared.DTOs;
using Xunit;

namespace PVT15_8.UnitTests.DTOs;

public class FishingSpotDTOTests
{
    [Fact]
    public void DefaultCollections_AreEmpty()
    {
        var dto = new FishingSpotDTO();
        Assert.NotNull(dto.Reviews);
        Assert.Empty(dto.Reviews);
        Assert.NotNull(dto.CatchReports);
        Assert.Empty(dto.CatchReports);
    }

    [Fact]
    public void CanSetProperties()
    {
        var reviews = new List<ReviewDTO> { new() { Id = 1, Rating = 5 } };
        var catches = new List<CatchReportDTO> { new() { Id = 1, UserId = "u" } };
        var dto = new FishingSpotDTO
        {
            Id = 99,
            Name = "Mälaren",
            Description = "Big lake",
            Depth = 12.5,
            Latitude = 59.33,
            Longitude = 17.79,
            HasRules = true,
            IsFishingCardRequired = false,
            IsForbidden = false,
            Reviews = reviews,
            CatchReports = catches
        };
        Assert.Equal(99, dto.Id);
        Assert.Equal("Mälaren", dto.Name);
        Assert.Equal(12.5, dto.Depth);
        Assert.True(dto.HasRules);
        Assert.Single(dto.Reviews);
        Assert.Single(dto.CatchReports);
    }
}
