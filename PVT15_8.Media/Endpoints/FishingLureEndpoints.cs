using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PVT15_8.Media.Data;
using PVT15_8.Media.Data.Models;

namespace PVT15_8.Media.Endpoints;

public static class FishingLureEndpoints
{
    public static RouteGroupBuilder MapFishingLureEndpoints(this RouteGroupBuilder group, IConfiguration config)
    {
        var lureGroup = group.MapGroup("/fishing-lure");

        lureGroup.MapGet("/{id:int}", async (int id, MediaDbContext db) =>
        {
            var image = await db.FishingLureImages.FindAsync(id);
            return image is null ? Results.NotFound() : Results.File(image.Image, "image/jpeg");
        });

        lureGroup.MapPost("/", async ([FromForm] IFormFile file, MediaDbContext db) =>
        {
            if (file.Length == 0) return Results.BadRequest("Invalid file.");

            var entity = new FishingLureImage { Image = await FileHelper.GetBytesAsync(file) };
            db.FishingLureImages.Add(entity);
            await db.SaveChangesAsync();

            var baseUrl = config["GatewayUrl"] ?? throw new Exception("GatewayUrl not set");
            var absoluteUrl = $"{baseUrl}/media/images/fishing-lure/{entity.Id}";

            return Results.Created(absoluteUrl, new { Url = absoluteUrl });
        }).DisableAntiforgery();

        lureGroup.MapDelete("/{id:int}", async (int id, MediaDbContext db) =>
        {
            var rowsAffected = await db.FishingLureImages.Where(x => x.Id == id).ExecuteDeleteAsync();
            return rowsAffected > 0 ? Results.NoContent() : Results.NotFound();
        });

        return group;
    }
}