using Microsoft.EntityFrameworkCore;
using PVT15_8.ApiService.Data;
using PVT15_8.ApiService.Data.Models;
using PVT15_8.Shared.DTOs;

namespace PVT15_8.ApiService.Services;

public class ReviewService : IReviewService
{
    private readonly ServiceDbContext _context;

    public ReviewService(ServiceDbContext context)
    {
        _context = context;
    }

    public async Task<List<ReviewDTO>> GetAllAsync()
    {
        return await _context.Reviews
            .Select(r => MapToDto(r))
            .ToListAsync();
    }

    public async Task<ReviewDTO?> GetByIdAsync(int id)
    {
        var review = await _context.Reviews.FindAsync(id);
        return review == null ? null : MapToDto(review);
    }

    public async Task<ReviewDTO> CreateAsync(RequestReviewDTO dto)
    {
        var review = new Review
        {
            UserId = dto.UserId,
            FishingSpotId = dto.FishingSpotId,
            Rating = dto.Rating,
            Comment = dto.Comment
        };
        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();
        return MapToDto(review);
    }

    public async Task<bool> UpdateAsync(int id, RequestReviewDTO dto)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review == null) return false;

        review.Rating = dto.Rating;
        review.Comment = dto.Comment;
        review.FishingSpotId = dto.FishingSpotId;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review == null) return false;

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();
        return true;
    }

    private static ReviewDTO MapToDto(Review r)
    {
        return new ReviewDTO
        {
            Id = r.Id,
            UserId = r.UserId,
            FishingSpotId = r.FishingSpotId,
            Rating = r.Rating,
            Comment = r.Comment
        };
    }
}