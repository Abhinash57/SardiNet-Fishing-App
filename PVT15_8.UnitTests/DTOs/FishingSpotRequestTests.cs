using PVT15_8.Shared.DTOs;
using Xunit;

namespace PVT15_8.UnitTests.DTOs;

public class FishingSpotRequestTests
{
    [Fact]
    public void Record_CanBeCreated()
    {
        var req = new FishingSpotRequest("Test", "Desc", 5.0, 18.0, 59.0);
        Assert.Equal("Test", req.Name);
        Assert.Equal("Desc", req.Description);
        Assert.Equal(5.0, req.Depth);
        Assert.Equal(18.0, req.Longitude);
        Assert.Equal(59.0, req.Latitude);
    }
}
