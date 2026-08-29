using Microsoft.EntityFrameworkCore;
using PVT15_8.ApiService.Data;
using PVT15_8.Shared.DTOs;
using System.Security.Claims;

namespace PVT15_8.ApiService.Endpoints;

public static class UserDataEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/profile")
            .WithTags("User data");

        group.MapGet("/", async Task<IResult> (ClaimsPrincipal claims, ServiceDbContext context) =>
        {
            var userId = claims.FindFirstValue(ClaimTypes.NameIdentifier);

            var catches = await context.CatchReports
                .AsNoTracking()
                .Include(c => c.FishingSpot)
                .Include(c => c.FishSpecies)
                .Where(c => c.UserId == userId)
                .OrderBy(c => c.CatchDate)
                .ToListAsync();

            UserStatsDTO profileData = new()
            {
                Places = catches.DistinctBy(cr => cr.FishingSpot.Id).Count(),
                TotalWeight = catches.Sum(cr => cr.WeightKg == null ? 0 : cr.WeightKg.Value),
                Catches = catches.Count,
                RecentActivites = catches.Take(3).Select(c => new RecentActivityDto
                {
                    CatchDate = c.CatchDate,
                    LengthCm = c.LengthCm,
                    FishSpeciesName = c.FishSpecies?.Name,
                    LocationName = c.FishingSpot.Name,
                    Weight = c.WeightKg,
                    ImageUrl = c.ImageUrl
                }).ToList(),
            };

            return TypedResults.Ok(profileData);
        })
        .WithName("GetUserProfileData")
        .WithSummary("User profile data from catch reports")
        .RequireAuthorization();

        group.MapDelete("/", async Task<IResult> (ClaimsPrincipal claims, ServiceDbContext context) =>
        {
            var userId = claims.FindFirstValue(ClaimTypes.NameIdentifier);

            var catches = await context.CatchReports
                .Where(c => c.UserId == userId)
                .ToListAsync();

            var reviews = await context.Reviews
                .Where(r => r.UserId == userId)
                .ToListAsync();

            if (catches.Count != 0)
                context.CatchReports.RemoveRange(catches);
            
            if (reviews.Count != 0)
                context.Reviews.RemoveRange(reviews);

            await context.SaveChangesAsync();

            return TypedResults.NoContent();
        }).RequireAuthorization();

    }
}
