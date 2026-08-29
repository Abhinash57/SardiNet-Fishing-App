using PVT15_8.Shared.DTOs;
using Xunit;

namespace PVT15_8.UnitTests.DTOs;

public class CatchReportDTOTests
{
    [Fact]
    public void DefaultValues_AreSet()
    {
        var dto = new CatchReportDTO();
        Assert.Equal(0, dto.Id);
        Assert.Equal(string.Empty, dto.UserId);
        Assert.Equal(0, dto.FishSpeciesId);
        Assert.Null(dto.FishSpeciesName);
        Assert.Equal(0, dto.FishingSpotId);
        Assert.Null(dto.FishingSpotName);
        Assert.Null(dto.FishingLureId);
        Assert.Null(dto.FishingLureName);
        Assert.Null(dto.FishingLureType);
        Assert.Equal(default(DateTime), dto.CatchDate);
        Assert.Null(dto.WeightKg);
        Assert.Null(dto.LengthCm);
        Assert.Null(dto.Description);
        Assert.Null(dto.ImageUrl);
    }

    [Fact]
    public void CanSetProperties()
    {
        var now = DateTime.UtcNow;
        var dto = new CatchReportDTO
        {
            Id = 42,
            UserId = "user123",
            FishSpeciesId = 7,
            FishSpeciesName = "Gädda",
            FishingSpotId = 3,
            FishingSpotName = "Stockholms Ström",
            FishingLureId = 9,
            FishingLureName = "Rapala",
            FishingLureType = "Wobbler",
            CatchDate = now,
            WeightKg = 2.5,
            LengthCm = 65,
            Description = "Nice catch!",
            ImageUrl = "fakebase64"
        };
        Assert.Equal(42, dto.Id);
        Assert.Equal("user123", dto.UserId);
        Assert.Equal("Gädda", dto.FishSpeciesName);
        Assert.Equal("Wobbler", dto.FishingLureType);
        Assert.Equal("Nice catch!", dto.Description);
    }
}
