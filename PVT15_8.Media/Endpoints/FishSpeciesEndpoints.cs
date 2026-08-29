using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PVT15_8.Media.Data;
using PVT15_8.Media.Data.Models;

namespace PVT15_8.Media.Endpoints;

public static class FishSpeciesEndpoints
{
    public static RouteGroupBuilder MapFishSpeciesEndpoints(this RouteGroupBuilder group, IConfiguration config)
    {
        var speciesGroup = group.MapGroup("/fish-species");

        speciesGroup.MapGet("/{id:int}", async (int id, MediaDbContext db) =>
        {
            var image = await db.FishSpeciesImages.FindAsync(id);
            return image is null ? Results.NotFound() : Results.File(image.Image, "image/jpeg");
        });

        speciesGroup.MapPost("/", async ([FromForm] IFormFile file, MediaDbContext db) =>
        {
            if (file.Length == 0) return Results.BadRequest("Invalid file.");

            var entity = new FishSpeciesImage { Image = await FileHelper.GetBytesAsync(file) };
            db.FishSpeciesImages.Add(entity);
            await db.SaveChangesAsync();

            var baseUrl = config["GatewayUrl"] ?? throw new Exception("GatewayUrl not set");
            var absoluteUrl = $"{baseUrl}/media/images/fish-species/{entity.Id}";

            return Results.Created(absoluteUrl, new { Url = absoluteUrl });
        }).DisableAntiforgery();

        speciesGroup.MapDelete("/{id:int}", async (int id, MediaDbContext db) =>
        {
            var rowsAffected = await db.FishSpeciesImages.Where(x => x.Id == id).ExecuteDeleteAsync();
            return rowsAffected > 0 ? Results.NoContent() : Results.NotFound();
        });

        return group;
    }
}
