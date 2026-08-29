using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PVT15_8.Media.Data;
using PVT15_8.Media.Data.Models;

namespace PVT15_8.Media.Endpoints;

public static class ReportCatchEndpoints
{
    public static RouteGroupBuilder MapReportCatchEndpoints(this RouteGroupBuilder group, IConfiguration config)
    {
        var catchGroup = group.MapGroup("/report-catch");

        catchGroup.MapGet("/{id:int}", async (int id, MediaDbContext db) =>
        {
            var image = await db.ReportCatchImages.FindAsync(id);
            return image is null ? Results.NotFound() : Results.File(image.Image, "image/jpeg");
        });

        catchGroup.MapPost("/", async ([FromForm] IFormFile file, MediaDbContext db) =>
        {
            if (file.Length == 0) return Results.BadRequest("Invalid file.");

            var entity = new ReportCatchImage { Image = await FileHelper.GetBytesAsync(file) };
            db.ReportCatchImages.Add(entity);
            await db.SaveChangesAsync();

            var baseUrl = config["GatewayUrl"] ?? throw new Exception("GatewayUrl not set");
            var absoluteUrl = $"{baseUrl}/media/images/report-catch/{entity.Id}";

            return Results.Created(absoluteUrl, new { Url = absoluteUrl });
        }).DisableAntiforgery();

        catchGroup.MapDelete("/{id:int}", async (int id, MediaDbContext db) =>
        {
            var rowsAffected = await db.ReportCatchImages.Where(x => x.Id == id).ExecuteDeleteAsync();
            return rowsAffected > 0 ? Results.NoContent() : Results.NotFound();
        });

        return group;
    }
}
