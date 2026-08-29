using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using PVT15_8.ApiService.Auth;
using PVT15_8.ApiService.Data;
using PVT15_8.ApiService.Data.Models;
using PVT15_8.ApiService.Data.Seed;
using PVT15_8.ApiService.Endpoints;
using PVT15_8.ApiService.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5001")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

var connectionString = builder.Configuration.GetConnectionString("api-db");
builder.Services.AddDbContext<ServiceDbContext>(options =>
    options.UseNpgsql(
        connectionString,
        o => o.UseNetTopologySuite()
    )
);

builder.Services.AddProblemDetails();

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Servers?.Clear();
        document.Servers?.Add(new Microsoft.OpenApi.OpenApiServer { Url = "/api" });
        return Task.CompletedTask;
    });
});

builder.Services.AddAuthentication(HeaderAuthenticationHandler.SchemeName)
    .AddScheme<AuthenticationSchemeOptions, HeaderAuthenticationHandler>(
        HeaderAuthenticationHandler.SchemeName, null);
builder.Services.AddAuthorization();

builder.Services.AddScoped<ICatchReportService, CatchReportService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IRecommendationService, RecommendationService>();
builder.Services.AddScoped<IFishingLureService, FishingLureService>();
builder.Services.AddScoped<IFishingSpotService, FishingSpotService>();
builder.Services.AddScoped<IFishSpeciesService, FishSpeciesService>();

var app = builder.Build();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();

    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<ServiceDbContext>();
    await db.Database.EnsureDeletedAsync();
    await db.Database.EnsureCreatedAsync();
    SeedDatabase.Seed(db, app.Configuration);
}

app.MapDefaultEndpoints();
app.MapFishingSpotEndpoints();
app.MapFishSpeciesEndpoints();
app.MapFishingLureEndpoints();
app.MapReviewEndpoints();
app.MapCatchReportEndpoints();
app.MapUserEndpoints();
app.MapRecommendationEndpoints();
app.MapBookmarkedSpotsEndpoints();

app.Run();
