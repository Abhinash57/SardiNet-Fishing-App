using PVT15_8.Shared.DTOs;
using Xunit;

namespace PVT15_8.UnitTests.DTOs;

public class RequestFishingLureDTOTests
{
    [Fact]
    public void DefaultSpeciesIds_IsEmptyList()
    {
        var dto = new RequestFishingLureDTO { Name = "X", Type = "Y" };
        Assert.NotNull(dto.FishSpeciesIds);
        Assert.Empty(dto.FishSpeciesIds);
    }

    [Fact]
    public void CanSetSpeciesIds()
    {
        var dto = new RequestFishingLureDTO
        {
            Name = "Rapala",
            Type = "Wobbler",
            FishSpeciesIds = new List<int> { 1, 2, 3 }
        };
        Assert.Equal(3, dto.FishSpeciesIds.Count);
    }
}
