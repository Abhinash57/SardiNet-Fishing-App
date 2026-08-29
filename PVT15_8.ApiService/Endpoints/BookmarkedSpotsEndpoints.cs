using Microsoft.EntityFrameworkCore;
using PVT15_8.ApiService.Data;
using PVT15_8.ApiService.Data.Models;
using PVT15_8.Shared.DTOs;
using System.Security.Claims;

namespace PVT15_8.ApiService.Endpoints;

public static class BookmarkedSpotsEndpoints
{
    public static void MapBookmarkedSpotsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/spots/bookmarked")
            .WithTags("Bookmarked spots");

        group.MapGet("/", async Task<IResult> (ClaimsPrincipal claims, ServiceDbContext context) =>
        {
            var userId = claims.FindFirstValue(ClaimTypes.NameIdentifier);

            var bookmarks = await context.BookmarkedSpots
                .AsNoTracking()
                .Where(bs => bs.UserId == userId)
                .Select(bs => new FishingSpotSmallDTO
                {
                    Id = bs.FishingSpotId,
                    Name = bs.FishingSpot.Name,
                   Description = bs.FishingSpot.Description, 
                })
                .ToListAsync();

            return TypedResults.Ok(bookmarks);
        })
        .WithName("GetBookmarks")
        .RequireAuthorization();

        group.MapDelete("/{id:int}", async Task<IResult> (int id, ClaimsPrincipal claims, ServiceDbContext context) =>
        {
            var userId = claims.FindFirstValue(ClaimTypes.NameIdentifier);

            var bookmark = await context.BookmarkedSpots
                .FirstOrDefaultAsync(bs => bs.UserId == userId && bs.FishingSpotId == id);

            if (bookmark is null) return TypedResults.NotFound();

            context.BookmarkedSpots.Remove(bookmark);
            await context.SaveChangesAsync();

            return TypedResults.NoContent();
        })
        .WithName("RemoveBookmark")
        .RequireAuthorization();

        group.MapPost("/", async Task<IResult> (int id, ClaimsPrincipal claims, ServiceDbContext context) =>
        {
            var userId = claims.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null) return TypedResults.Unauthorized();

            var bookmark = new BookmarkedSpots { UserId = userId, FishingSpotId = id };

            context.BookmarkedSpots.Add(bookmark);
            await context.SaveChangesAsync();

            return TypedResults.NoContent();
        })
        .WithName("BookmarkSpot")
        .RequireAuthorization();

    }
}