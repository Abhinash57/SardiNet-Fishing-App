using PVT15_8.Shared.DTOs;
using Xunit;

namespace PVT15_8.UnitTests.DTOs;

public class RequestCatchReportDTOTests
{
    [Fact]
    public void Record_CanBeCreated()
    {
        var now = DateTime.UtcNow;
        var dto = new RequestCatchReportDTO
        {
            UserId = "user456",
            FishSpeciesId = 5,
            FishingSpotId = 2,
            FishingLureId = 8,
            CatchDate = now,
            WeightKg = 3.2,
            LengthCm = 55,
            Description = "Big pike!",
            ImageUrl = "img"
        };
        Assert.Equal("user456", dto.UserId);
        Assert.Equal(5, dto.FishSpeciesId);
        Assert.Equal(3.2, dto.WeightKg);
        Assert.Equal("Big pike!", dto.Description);
    }
}
