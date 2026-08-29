using PVT15_8.Shared.DTOs;
using Xunit;

namespace PVT15_8.UnitTests.DTOs;

public class RequestFishSpeciesDTOTests
{
    [Fact]
    public void Record_CanBeCreated()
    {
        var dto = new RequestFishSpeciesDTO
        {
            Name = "Havsöring",
            Description = "Sea trout"
        };
        Assert.Equal("Havsöring", dto.Name);
        Assert.Equal("Sea trout", dto.Description);
    }
}
