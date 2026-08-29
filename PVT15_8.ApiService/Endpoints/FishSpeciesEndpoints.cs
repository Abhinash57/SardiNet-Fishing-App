using Microsoft.AspNetCore.Mvc;
using PVT15_8.ApiService.Services;
using PVT15_8.Shared.DTOs;

namespace PVT15_8.ApiService.Endpoints;

public static class FishSpeciesEndpoints
{
    public static void MapFishSpeciesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/fishspecies")
            .WithTags("Fish Species");

        group.MapGet("/", async Task<IResult> (IFishSpeciesService fishSpeciesService) =>
        {
            var species = await fishSpeciesService.GetAllAsync();
            return TypedResults.Ok(species);
        })
        .WithName("GetAllFishSpecies");

        group.MapGet("/{id:int}", async Task<IResult> (int id, IFishSpeciesService fishSpeciesService) =>
        {
            var species = await fishSpeciesService.GetByIdAsync(id);
            return species is null ? TypedResults.NotFound() : TypedResults.Ok(species);
        })
        .WithName("GetFishSpeciesById");

        group.MapPost("/", async Task<IResult> ([FromBody] RequestFishSpeciesDTO dto, IFishSpeciesService fishSpeciesService) =>
        {
            var created = await fishSpeciesService.CreateAsync(dto);
            return TypedResults.Created($"/fishspecies/{created.Id}", created);
        })
        .RequireAuthorization(policy => policy.RequireRole("Admin"))
        .WithName("CreateFishSpecies")
        .WithSummary("Create fish species")
        .WithDescription("Adds a new fish species to the database.");

        group.MapPut("/{id:int}", async Task<IResult> (int id, RequestFishSpeciesDTO dto, IFishSpeciesService fishSpeciesService) =>
        {
            var success = await fishSpeciesService.UpdateAsync(id, dto);
            return success ? TypedResults.NoContent() : TypedResults.NotFound();
        })
        .RequireAuthorization(policy => policy.RequireRole("Admin"))
        .WithName("UpdateFishSpecies")
        .WithSummary("Update fish species")
        .WithDescription("Updates the fish species.");

        group.MapDelete("/{id:int}", async Task<IResult> (int id, IFishSpeciesService fishSpeciesService) =>
        {
            var success = await fishSpeciesService.DeleteAsync(id);
            return success ? TypedResults.NoContent() : TypedResults.NotFound();
        })
        .RequireAuthorization(policy => policy.RequireRole("Admin"))
        .WithName("DeleteFishSpecies")
        .WithSummary("Delete fish species")
        .WithDescription("Deletes the fish species.");
    }
}