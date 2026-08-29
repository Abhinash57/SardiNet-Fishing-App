using PVT15_8.Shared.DTOs;
using Xunit;

namespace PVT15_8.UnitTests.DTOs;

public class ReviewDTOTests
{
    [Fact]
    public void Properties_Work()
    {
        var dto = new ReviewDTO
        {
            Id = 1,
            UserId = "user",
            FishingSpotId = 2,
            Rating = 4,
            Comment = "Great place"
        };
        Assert.Equal(1, dto.Id);
        Assert.Equal("user", dto.UserId);
        Assert.Equal(4, dto.Rating);
        Assert.Equal("Great place", dto.Comment);
    }
}
