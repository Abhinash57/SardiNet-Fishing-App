using Microsoft.EntityFrameworkCore;
using PVT15_8.ApiService.Data.Models;

namespace PVT15_8.ApiService.Data;

public class ServiceDbContext(DbContextOptions<ServiceDbContext> options) : DbContext(options)
{

    public DbSet<CatchReport> CatchReports => Set<CatchReport>();
    public DbSet<FishSpecies> FishSpecies => Set<FishSpecies>();
    public DbSet<FishingLure> FishingLures => Set<FishingLure>();
    public DbSet<FishingSpot> FishingSpots => Set<FishingSpot>();
    public DbSet<FishingSpeciesFishingSpot> FishingSpeciesFishingSpots => Set<FishingSpeciesFishingSpot>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<FishingLureFishes> FishingLureFishes => Set<FishingLureFishes>();
    public DbSet<BookmarkedSpots> BookmarkedSpots => Set<BookmarkedSpots>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("postgis");

        modelBuilder.Entity<FishingSpot>()
            .HasIndex(f => f.Location)
            .HasMethod("GIST");

        modelBuilder.Entity<FishingSpeciesFishingSpot>()
            .HasKey(fs => new { fs.FishSpeciesId, fs.FishingSpotId });

        modelBuilder.Entity<FishingSpeciesFishingSpot>()
            .HasOne(fs => fs.FishSpecies)
            .WithMany(f => f.FishingSpeciesFishingSpots)
            .HasForeignKey(fs => fs.FishSpeciesId);

        modelBuilder.Entity<FishingSpeciesFishingSpot>()
            .HasOne(fs => fs.FishingSpot)
            .WithMany(f => f.SpeciesFishingSpots)
            .HasForeignKey(fs => fs.FishingSpotId);

        modelBuilder.Entity<FishingLureFishes>()
            .HasKey(fl => new { fl.FishingLureId, fl.FishSpeciesId });

        modelBuilder.Entity<FishingLureFishes>()
            .HasOne(fl => fl.FishingLure)
            .WithMany(l => l.FishingLureFishes)
            .HasForeignKey(fl => fl.FishingLureId);

        modelBuilder.Entity<FishingLureFishes>()
            .HasOne(fl => fl.FishSpecies)
            .WithMany(f => f.FishingLureFishes)
            .HasForeignKey(fl => fl.FishSpeciesId);

        modelBuilder.Entity<BookmarkedSpots>()
            .HasKey(ss => new { ss.FishingSpotId, ss.UserId });
    }


}
