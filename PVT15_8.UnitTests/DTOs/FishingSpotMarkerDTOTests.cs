using PVT15_8.Shared.DTOs;
using Xunit;

namespace PVT15_8.UnitTests.DTOs;

public class FishingSpotMarkerDTOTests
{
    [Fact]
    public void Coordinates_CanBeSet()
    {
        var dto = new FishingSpotMarkerDTO
        {
            Id = 1,
            Name = "Test Spot",
            Latitude = 59.3293,
            Longitude = 18.0686
        };
        Assert.Equal(1, dto.Id);
        Assert.Equal("Test Spot", dto.Name);
        Assert.Equal(59.3293, dto.Latitude);
        Assert.Equal(18.0686, dto.Longitude);
    }
}
