using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PVT15_8.ApiService.Data;
using PVT15_8.ApiService.Data.Models;
using PVT15_8.Shared.DTOs;

namespace PVT15_8.ApiService.Endpoints;

public static class FishingSpeciesFishingSpotEndpoints
{
    public static void MapFishingSpeciesFishingSpotEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/fishingspeciesfishingspots")
            .WithTags("Fishing Species Fishing Spots");

        group.MapGet("/", async Task<IResult> (ServiceDbContext context) =>
        {
            var speciesSpots = await context.FishingSpeciesFishingSpots
                .Select(fs => new FishingSpeciesFishingSpotDTO
                {
                    FishSpeciesId = fs.FishSpeciesId,
                    FishingSpotId = fs.FishingSpotId,
                    FishSpeciesFrequency = fs.FishSpeciesFrequencyId
                })
                .ToListAsync();

            return TypedResults.Ok(speciesSpots);
        })
        .WithName("GetAllFishingSpeciesFishingSpots")
        .WithSummary("Get all fishing species fishing spots")
        .WithDescription("Returns a list of all fishing species fishing spots.");

        group.MapPost("/", async Task<IResult> ([FromBody] FishingSpeciesFishingSpotDTO dto, ServiceDbContext context) =>
        {
            var speciesSpot = new FishingSpeciesFishingSpot
            {
                FishSpeciesId = dto.FishSpeciesId,
                FishingSpotId = dto.FishingSpotId,
                FishSpeciesFrequencyId = dto.FishSpeciesFrequency
            };

            context.FishingSpeciesFishingSpots.Add(speciesSpot);
            await context.SaveChangesAsync();

             var result = new FishingSpeciesFishingSpotDTO
             {
                 FishSpeciesId = speciesSpot.FishSpeciesId,
                 FishingSpotId = speciesSpot.FishingSpotId,
                 FishSpeciesFrequency = speciesSpot.FishSpeciesFrequencyId
             };
             return TypedResults.Created($"/fishingspeciesfishingspots/{result.FishSpeciesId}", result);
        })
        .WithName("CreateFishingSpeciesFishingSpot")
        .WithSummary("Create a new fishing species fishing spot")
        .WithDescription("Creates a new fishing species fishing spot and returns the created entity.");

        group.MapDelete("/{fishSpeciesId:int}/{fishingSpotId:int}", async Task<IResult> (int fishSpeciesId, int fishingSpotId, ServiceDbContext context) =>
        {
            var speciesSpot = await context.FishingSpeciesFishingSpots
                .FirstOrDefaultAsync(fs => fs.FishSpeciesId == fishSpeciesId && fs.FishingSpotId == fishingSpotId);

            if (speciesSpot is null)
            {
                return TypedResults.NotFound();
            }

            context.FishingSpeciesFishingSpots.Remove(speciesSpot);
            await context.SaveChangesAsync();

             return TypedResults.NoContent();
        })
        .WithName("DeleteFishingSpeciesFishingSpot")
        .WithSummary("Delete a fishing species fishing spot")
        .WithDescription("Deletes a fishing species fishing spot by fish species ID and fishing spot ID");
    }
}