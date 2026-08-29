using Microsoft.AspNetCore.Mvc;
using PVT15_8.ApiService.Services;
using PVT15_8.Shared.DTOs;

namespace PVT15_8.ApiService.Endpoints;

public static class FishingLureEndpoints
{
    public static void MapFishingLureEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/fishinglures")
            .WithTags("Fishing Lures");

        group.MapGet("/", async Task<IResult> (IFishingLureService fishingLureService) =>
        {
            var lures = await fishingLureService.GetAllAsync();
            return TypedResults.Ok(lures);
        })
        .WithName("GetAllFishingLures");

        group.MapGet("/{id:int}", async Task<IResult> (int id, IFishingLureService fishingLureService) =>
        {
            var lure = await fishingLureService.GetByIdAsync(id);
            return lure is null ? TypedResults.NotFound() : TypedResults.Ok(lure);
        })
        .WithName("GetFishingLureById");

        group.MapPost("/", async Task<IResult> ([FromBody] RequestFishingLureDTO dto, IFishingLureService fishingLureService) =>
        {
            var created = await fishingLureService.CreateAsync(dto);
            return TypedResults.Created($"/fishinglures/{created.Id}", created);
        })
        .RequireAuthorization(policy => policy.RequireRole("Admin"))   
        .WithName("CreateFishingLure")
        .WithSummary("Create fishing lure")
        .WithDescription("Adds a new fishing lure to the database.");

        group.MapPut("/{id:int}", async Task<IResult> (int id, RequestFishingLureDTO dto, IFishingLureService fishingLureService) =>
        {
            var success = await fishingLureService.UpdateAsync(id, dto);
            return success ? TypedResults.NoContent() : TypedResults.NotFound();
        })
        .RequireAuthorization(policy => policy.RequireRole("Admin"))
        .WithName("UpdateFishingLure")
        .WithSummary("Update fishing lure 🎣")
        .WithDescription("Updates the fishing lure");

        group.MapDelete("/{id:int}", async Task<IResult> (int id, IFishingLureService fishingLureService) =>
        {
            var success = await fishingLureService.DeleteAsync(id);
            return success ? TypedResults.NoContent() : TypedResults.NotFound();
        })
        .RequireAuthorization(policy => policy.RequireRole("Admin"))
        .WithName("DeleteFishingLure")
        .WithSummary("Delete fishing lure 🎣");
    }
}