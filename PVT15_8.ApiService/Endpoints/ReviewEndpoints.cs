using Microsoft.AspNetCore.Mvc;
using PVT15_8.ApiService.Services;
using PVT15_8.Shared.DTOs;

namespace PVT15_8.ApiService.Endpoints;

public static class ReviewEndpoints
{
    public static void MapReviewEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/reviews")
            .WithTags("Reviews");

        group.MapGet("/", async Task<IResult> (IReviewService reviewService) =>
        {
            var reviews = await reviewService.GetAllAsync();
            return TypedResults.Ok(reviews);
        })
        .WithName("GetAllReviews");

        group.MapGet("/{id:int}", async Task<IResult> (int id, IReviewService reviewService) =>
        {
            var review = await reviewService.GetByIdAsync(id);
            return review is null ? TypedResults.NotFound() : TypedResults.Ok(review);
        })
        .WithName("GetReviewById");

        group.MapPost("/", async Task<IResult> ([FromBody] RequestReviewDTO dto, IReviewService reviewService) =>
        {
            var created = await reviewService.CreateAsync(dto);
            return TypedResults.Created($"/reviews/{created.Id}", created);
        })
        .WithName("CreateReview");

        group.MapPut("/{id:int}", async Task<IResult> (int id, RequestReviewDTO dto, IReviewService reviewService) =>
        {
            var success = await reviewService.UpdateAsync(id, dto);
            return success ? TypedResults.NoContent() : TypedResults.NotFound();
        })
        .WithName("UpdateReview");

        group.MapDelete("/{id:int}", async Task<IResult> (int id, IReviewService reviewService) =>
        {
            var success = await reviewService.DeleteAsync(id);
            return success ? TypedResults.NoContent() : TypedResults.NotFound();
        })
        .WithName("DeleteReview");
    }
}