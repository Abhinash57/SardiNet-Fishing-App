using Microsoft.AspNetCore.Mvc;
using PVT15_8.ApiService.Services;
using PVT15_8.Shared.DTOs;

namespace PVT15_8.ApiService.Endpoints;

public static class CatchReportEndpoints
{
    public static void MapCatchReportEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/catchreports")
            .WithTags("Catch Reports");

        group.MapGet("/", async Task<IResult> (ICatchReportService catchReportService) =>
        {
            var reports = await catchReportService.GetAllAsync();
            return TypedResults.Ok(reports);
        })
        .WithName("GetAllCatchReports");

        group.MapGet("/{id:int}", async Task<IResult> (int id, ICatchReportService catchReportService) =>
        {
            var report = await catchReportService.GetByIdAsync(id);
            return report is null ? TypedResults.NotFound() : TypedResults.Ok(report);
        })
        .WithName("GetCatchReportById");

        group.MapPost("/", async Task<IResult> ([FromBody] RequestCatchReportDTO dto, ICatchReportService catchReportService) =>
        {
            var created = await catchReportService.CreateAsync(dto);
            return TypedResults.Created($"/catchreports/{created.Id}", created);
        })
        .WithName("CreateCatchReport")
        .Accepts<RequestCatchReportDTO>("application/json");

        group.MapPost("/with-image", async Task<IResult> (HttpRequest request, ICatchReportService catchReportService) =>
        {
            if (!request.HasFormContentType)
                return TypedResults.BadRequest("Förväntade multipart/form-data.");

            var form = await request.ReadFormAsync();
            var file = form.Files.GetFile("imageFile");
            if (file == null || file.Length == 0)
                return TypedResults.BadRequest("Bild saknas.");

            var formData = new Dictionary<string, string>();
            foreach (var key in form.Keys)
                formData[key] = form[key].ToString();

            using var stream = file.OpenReadStream();
            var created = await catchReportService.CreateWithImageAsync(stream, file.FileName, file.ContentType, formData);
            return TypedResults.Created($"/catchreports/{created.Id}", created);
        })
        .WithName("CreateCatchReportWithImage")
        .Accepts<MultipartFormDataContent>("multipart/form-data");

        group.MapPut("/{id:int}", async Task<IResult> (int id, CatchReportDTO dto, ICatchReportService catchReportService) =>
        {
            var requestDto = new RequestCatchReportDTO
            {
                UserId = dto.UserId,
                FishSpeciesId = dto.FishSpeciesId,
                FishingSpotId = dto.FishingSpotId,
                FishingLureId = dto.FishingLureId ?? 0,
                CatchDate = dto.CatchDate,
                WeightKg = dto.WeightKg ?? 0,
                LengthCm = dto.LengthCm ?? 0,
                Description = dto.Description,
                ImageUrl = dto.ImageUrl
            };
            var success = await catchReportService.UpdateAsync(id, requestDto, dto.UserId);
            return success ? TypedResults.Ok() : TypedResults.Forbid();
        })
        .WithName("UpdateCatchReport");

        group.MapDelete("/{id:int}", async Task<IResult> (int id, HttpContext httpContext, ICatchReportService catchReportService) =>
        {
            var userId = httpContext.Request.Headers["User_id"].FirstOrDefault();
            if (string.IsNullOrEmpty(userId))
                return TypedResults.Unauthorized();
            var success = await catchReportService.DeleteAsync(id, userId);
            return success ? TypedResults.NoContent() : TypedResults.Forbid();
        })
        .WithName("DeleteCatchReport");
    }
}