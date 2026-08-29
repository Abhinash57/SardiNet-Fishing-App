using PVT15_8.Shared.DTOs;
using Xunit;

namespace PVT15_8.UnitTests.DTOs;

public class RequestFishingSpotDTOTests
{
    [Fact]
    public void Record_CanBeCreated()
    {
        var dto = new RequestFishingSpotDTO
        {
            Name = "Värtan",
            Description = "Pike spot",
            Depth = 8,
            Latitude = 59.3512,
            Longitude = 18.1147,
            HasRules = true,
            IsFishingCardRequired = false,
            IsForbidden = false
        };
        Assert.Equal("Värtan", dto.Name);
        Assert.Equal(59.3512, dto.Latitude);
        Assert.Equal(18.1147, dto.Longitude);
        Assert.True(dto.HasRules);
    }
}
