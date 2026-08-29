using PVT15_8.Shared.DTOs;
using Xunit;

namespace PVT15_8.UnitTests.DTOs;

public class FishingLureDTOTests
{
    [Fact]
    public void DefaultSpecies_IsEmptyList()
    {
        var dto = new FishingLureDTO();
        Assert.NotNull(dto.Species);
        Assert.Empty(dto.Species);
    }

    [Fact]
    public void CanSetSpecies()
    {
        var speciesList = new List<FishSpeciesDTO>
        {
            new() { Id = 1, Name = "Gädda" },
            new() { Id = 2, Name = "Abborre" }
        };
        var dto = new FishingLureDTO
        {
            Id = 10,
            Name = "Mepps",
            Type = "Spinner",
            Species = speciesList
        };
        Assert.Equal(10, dto.Id);
        Assert.Equal("Mepps", dto.Name);
        Assert.Equal(2, dto.Species.Count);
    }
}
