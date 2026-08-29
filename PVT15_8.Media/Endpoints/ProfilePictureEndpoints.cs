using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PVT15_8.Media.Data;
using PVT15_8.Media.Data.Models;

namespace PVT15_8.Media.Endpoints;

public static class ProfilePictureEndpoints
{
    public static RouteGroupBuilder MapProfilePictureEndpoints(this RouteGroupBuilder group, IConfiguration config)
    {
        var profileGroup = group.MapGroup("/profile-picture");

        profileGroup.MapGet("/{id:int}", async (int id, MediaDbContext db) =>
        {
            var image = await db.ProfilePictures.FindAsync(id);
            return image is null ? Results.NotFound() : Results.File(image.Image, "image/jpeg");
        });

        profileGroup.MapPost("/", async (HttpRequest request, [FromForm] IFormFile file, [FromForm] string userId, MediaDbContext db) =>
        {
            if (file.Length == 0 || string.IsNullOrWhiteSpace(userId)) return Results.BadRequest("Invalid file or UserId.");

            var entity = new ProfilePictureImage
            {
                UserId = userId,
                Image = await FileHelper.GetBytesAsync(file)
            };

            db.ProfilePictures.Add(entity);
            await db.SaveChangesAsync();

            var baseUrl = config["GatewayUrl"] ?? throw new Exception("GatewayUrl not set");
            var absoluteUrl = $"{baseUrl}/media/images/profile-picture/{entity.Id}";

            return Results.Created(absoluteUrl, new { Url = absoluteUrl });
        }).DisableAntiforgery();

        profileGroup.MapDelete("/{id:int}", async (int id, MediaDbContext db) =>
        {
            var rowsAffected = await db.ProfilePictures.Where(x => x.Id == id).ExecuteDeleteAsync();
            return rowsAffected > 0 ? Results.NoContent() : Results.NotFound();
        });

        return group;
    }
}
