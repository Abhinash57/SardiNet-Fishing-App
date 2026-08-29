using Microsoft.EntityFrameworkCore;
using PVT15_8.Media.Data.Models;

namespace PVT15_8.Media.Data;

public class MediaDbContext(DbContextOptions<MediaDbContext> options) : DbContext(options)
{
    public DbSet<ProfilePictureImage> ProfilePictures => Set<ProfilePictureImage>();
    public DbSet<FishingLureImage> FishingLureImages => Set<FishingLureImage>();
    public DbSet<FishSpeciesImage> FishSpeciesImages => Set<FishSpeciesImage>();
    public DbSet<ReportCatchImage> ReportCatchImages => Set<ReportCatchImage>();
}
