using PVT15_8.Shared.DTOs;
using Xunit;

namespace PVT15_8.UnitTests.DTOs;

public class FishSpeciesDTOTests
{
    [Fact]
    public void DefaultValues()
    {
        var dto = new FishSpeciesDTO();
        Assert.Equal(0, dto.Id);
        Assert.Equal(string.Empty, dto.Name);
        Assert.Null(dto.Description);
    }

    [Fact]
    public void CanSetProperties()
    {
        var dto = new FishSpeciesDTO
        {
            Id = 5,
            Name = "Gös",
            Description = "Popular predator"
        };
        Assert.Equal(5, dto.Id);
        Assert.Equal("Gös", dto.Name);
        Assert.Equal("Popular predator", dto.Description);
    }
}
