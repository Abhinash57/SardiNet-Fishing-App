using PVT15_8.Shared.DTOs;
using Xunit;

namespace PVT15_8.UnitTests.DTOs;

public class RequestReviewDTOTests
{
    [Fact]
    public void Record_CanBeCreated()
    {
        var dto = new RequestReviewDTO
        {
            UserId = "u",
            FishingSpotId = 1,
            Rating = 5,
            Comment = "Nice"
        };
        Assert.Equal(5, dto.Rating);
        Assert.Equal("Nice", dto.Comment);
    }

    [Fact]
    public void Record_Equality_Works()
    {
        var dto1 = new RequestReviewDTO { UserId = "u", FishingSpotId = 1, Rating = 5 };
        var dto2 = new RequestReviewDTO { UserId = "u", FishingSpotId = 1, Rating = 5 };
        var dto3 = new RequestReviewDTO { UserId = "u", FishingSpotId = 1, Rating = 4 };
        Assert.Equal(dto1, dto2);
        Assert.NotEqual(dto1, dto3);
    }
}
