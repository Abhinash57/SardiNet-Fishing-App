using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PVT15_8.ApiService.Data;
using PVT15_8.ApiService.Services;
using PVT15_8.Shared.DTOs;
using System.Security.Claims;

namespace PVT15_8.ApiService.Endpoints;

public static class FishingSpotEndpoints
{
    public static void MapFishingSpotEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/fishingspots")
            .WithTags("Fishing Spots");

        group.MapGet("/markers", async Task<IResult> (IFishingSpotService fishingSpotService) =>
        {
            var markers = await fishingSpotService.GetMarkersAsync();
            return TypedResults.Ok(markers);
        })
        .WithName("GetFishingSpotMarkers");

        group.MapGet("/", async Task<IResult> (IFishingSpotService fishingSpotService) =>
        {
            var spots = await fishingSpotService.GetAllAsync();
            return TypedResults.Ok(spots);
        })
        .WithName("GetAllFishingSpots");

        group.MapGet("/{id:int}", async Task<IResult> (int id, IFishingSpotService fishingSpotService) =>
        {
            var spot = await fishingSpotService.GetByIdAsync(id);
            return spot is null ? TypedResults.NotFound() : TypedResults.Ok(spot);
        })
        .WithName("GetFishingSpotById");

        group.MapPost("/", async Task<IResult> ([FromBody] RequestFishingSpotDTO dto, IFishingSpotService fishingSpotService) =>
        {
            var created = await fishingSpotService.CreateAsync(dto);
            return TypedResults.Created($"/fishingspots/{created.Id}", created);
        })
        .RequireAuthorization(policy => policy.RequireRole("Admin"))
        .WithName("CreateFishingSpot")
        .WithSummary("Create a new fishing spot")
        .WithDescription("Adds a new fishing spot to the database and links any provided existing rules or permits.");

        group.MapPost("/filter", async Task<IResult> ([FromBody] FishingSpotFilterDto filter, IFishingSpotService fishingSpotService) =>
        {
            var markers = await fishingSpotService.GetFilteredMarkersAsync(filter);
            return TypedResults.Ok(markers);
        })
        .WithName("GetFilteredFishingSpots")
        .Accepts<FishingSpotFilterDto>("application/json");

        group.MapPut("/{id:int}", async Task<IResult> (int id, RequestFishingSpotDTO dto, IFishingSpotService fishingSpotService) =>
        {
            var success = await fishingSpotService.UpdateAsync(id, dto);
            return success ? TypedResults.NoContent() : TypedResults.NotFound();
        })
        .RequireAuthorization(policy => policy.RequireRole("Admin"))
        .WithName("UpdateFishingSpot")
        .WithSummary("Update fishing spot")
        .WithDescription("Updates fishing spot :D");

        group.MapDelete("/{id:int}", async Task<IResult> (int id, IFishingSpotService fishingSpotService) =>
        {
            var success = await fishingSpotService.DeleteAsync(id);
            return success ? TypedResults.NoContent() : TypedResults.NotFound();
        })
        .RequireAuthorization(policy => policy.RequireRole("Admin"))
        .WithName("DeleteFishingSpot")
        .WithSummary("Delete fishing spot")
        .WithDescription("Soft deletes the fishing spot. Blub Blub 🐸");
    }
}