using Microsoft.EntityFrameworkCore;
using PVT15_8.Media.Data.Models;

namespace PVT15_8.Media.Data.Seed;

public static class MediaDataSeeder
{
    public static async Task SeedImagesAsync(MediaDbContext db)
    {
        var seedImagesPath = Path.Combine(AppContext.BaseDirectory, "../../../Data/SeedImages");

        if (!await db.FishSpeciesImages.AnyAsync())
        {
            var gäddaPath = Path.Combine(seedImagesPath, "gädda.avif");
            var aborrePath = Path.Combine(seedImagesPath, "aborre.avif");
            var karpPath = Path.Combine(seedImagesPath, "karp.avif");
            var gösPath = Path.Combine(seedImagesPath, "gös.avif");
            var sutarePath = Path.Combine(seedImagesPath, "sutare.avif");
            var mörtPath = Path.Combine(seedImagesPath, "mört.avif");
            var braxenPath = Path.Combine(seedImagesPath, "braxen.avif");
            var lakePath = Path.Combine(seedImagesPath, "lake.avif");
            var ålPath = Path.Combine(seedImagesPath, "ål.avif");
            var regnbågePath = Path.Combine(seedImagesPath, "regnbåge.avif");
            var rödingPath = Path.Combine(seedImagesPath, "röding.avif");
            var öringPath = Path.Combine(seedImagesPath, "öring.avif");
            var harrPath = Path.Combine(seedImagesPath, "harr.avif");
            var rudaPath = Path.Combine(seedImagesPath, "ruda.avif");
            var laxPath = Path.Combine(seedImagesPath, "lax.avif");
            var kräftaPath = Path.Combine(seedImagesPath, "kräfta.avif");
            var sarv = Path.Combine(seedImagesPath, "sarv.jpg");
            var nors = Path.Combine(seedImagesPath, "nors.jpg");
            var loja = Path.Combine(seedImagesPath, "loja.jpg");
            var defaultImg = Path.Combine(seedImagesPath, "defualt.jpg");
            var asp = Path.Combine(seedImagesPath, "asp.jpg");

            if (File.Exists(gäddaPath))
            {
                db.FishSpeciesImages.Add(new FishSpeciesImage
                {
                    Id = 1,
                    Image = await File.ReadAllBytesAsync(gäddaPath)
                });
            }

            if (File.Exists(aborrePath))
            {
                db.FishSpeciesImages.Add(new FishSpeciesImage
                {
                    Id = 2,
                    Image = await File.ReadAllBytesAsync(aborrePath)
                });
            }

            if (File.Exists(karpPath))
            {
                db.FishSpeciesImages.Add(new FishSpeciesImage
                {
                    Id = 3,
                    Image = await File.ReadAllBytesAsync(karpPath)
                });
            }

            if (File.Exists(gösPath))
            {
                db.FishSpeciesImages.Add(new FishSpeciesImage
                {
                    Id = 4,
                    Image = await File.ReadAllBytesAsync(gösPath)
                });
            }

            if (File.Exists(sutarePath))
            {
                db.FishSpeciesImages.Add(new FishSpeciesImage
                {
                    Id = 5,
                    Image = await File.ReadAllBytesAsync(sutarePath)
                });
            }

            if (File.Exists(mörtPath))
            {
                db.FishSpeciesImages.Add(new FishSpeciesImage
                {
                    Id = 6,
                    Image = await File.ReadAllBytesAsync(mörtPath)
                });
            }

            if (File.Exists(braxenPath))
            {
                db.FishSpeciesImages.Add(new FishSpeciesImage
                {
                    Id = 7,
                    Image = await File.ReadAllBytesAsync(braxenPath)
                });
            }

            if (File.Exists(lakePath))
            {
                db.FishSpeciesImages.Add(new FishSpeciesImage
                {
                    Id = 8,
                    Image = await File.ReadAllBytesAsync(lakePath)
                });
            }

            if (File.Exists(ålPath))
            {
                db.FishSpeciesImages.Add(new FishSpeciesImage
                {
                    Id = 9,
                    Image = await File.ReadAllBytesAsync(ålPath)
                });
            }

            if (File.Exists(regnbågePath))
            {
                db.FishSpeciesImages.Add(new FishSpeciesImage
                {
                    Id = 10,
                    Image = await File.ReadAllBytesAsync(regnbågePath)
                });
            }

            if (File.Exists(rödingPath))
            {
                db.FishSpeciesImages.Add(new FishSpeciesImage
                {
                    Id = 11,
                    Image = await File.ReadAllBytesAsync(rödingPath)
                });
            }

            if (File.Exists(öringPath))
            {
                db.FishSpeciesImages.Add(new FishSpeciesImage
                {
                    Id = 12,
                    Image = await File.ReadAllBytesAsync(öringPath)
                });
            }

            if (File.Exists(harrPath))
            {
                db.FishSpeciesImages.Add(new FishSpeciesImage
                {
                    Id = 13,
                    Image = await File.ReadAllBytesAsync(harrPath)
                });
            }

            if (File.Exists(rudaPath))
            {
                db.FishSpeciesImages.Add(new FishSpeciesImage
                {
                    Id = 17,
                    Image = await File.ReadAllBytesAsync(rudaPath)
                });
            }

            if (File.Exists(sarv))
            {
                db.FishSpeciesImages.Add(new FishSpeciesImage
                {
                    Id = 21,
                    Image = await File.ReadAllBytesAsync(sarv)
                });
            }

            if (File.Exists(laxPath))
            {
                db.FishSpeciesImages.Add(new FishSpeciesImage
                {
                    Id = 22,
                    Image = await File.ReadAllBytesAsync(laxPath)
                });
            }

            if (File.Exists(loja))
            {
                db.FishSpeciesImages.Add(new FishSpeciesImage
                {
                    Id = 23,
                    Image = await File.ReadAllBytesAsync(loja)
                });
            }


            if (File.Exists(kräftaPath))
            {
                db.FishSpeciesImages.Add(new FishSpeciesImage
                {
                    Id = 25,
                    Image = await File.ReadAllBytesAsync(kräftaPath)
                });
            }

            if (File.Exists(asp))
            {
                db.FishSpeciesImages.Add(new FishSpeciesImage
                {
                    Id = 26,
                    Image = await File.ReadAllBytesAsync(asp)
                });
            }

            if (File.Exists(nors))
            {
                db.FishSpeciesImages.Add(new FishSpeciesImage
                {
                    Id = 27,
                    Image = await File.ReadAllBytesAsync(nors)
                });
            }

            if (File.Exists(defaultImg))
            {
                db.FishSpeciesImages.Add(new FishSpeciesImage
                {
                    Id = 28,
                    Image = await File.ReadAllBytesAsync(defaultImg)
                });
            }

            await db.SaveChangesAsync();
        }

        if (!await db.FishingLureImages.AnyAsync())
        {
            Console.WriteLine("for real");
            var spinner = Path.Combine(seedImagesPath, "spinner1.png");
            var wobbler = Path.Combine(seedImagesPath, "wobbler1.png");
            var jig = Path.Combine(seedImagesPath, "jig1.png");
            var jerkbait = Path.Combine(seedImagesPath, "jerkbait1.png");
            var tail = Path.Combine(seedImagesPath, "twister_tail1.png");
            var spoon = Path.Combine(seedImagesPath, "sked1.png");
            var fly = Path.Combine(seedImagesPath, "flugbete1.png");
            var worm = Path.Combine(seedImagesPath, "maskbete1.png");
            var crankbait = Path.Combine(seedImagesPath, "wobbler1.png");
            var popper = Path.Combine(seedImagesPath, "popper1.png");
            var streamer = Path.Combine(seedImagesPath, "streamer1.png");
            var nymph = Path.Combine(seedImagesPath, "nymf1.png");

            Console.WriteLine("exists :O? " + File.Exists(spinner));
            Console.WriteLine(spinner);
            if (File.Exists(spinner))
            {
                db.FishingLureImages.Add(new FishingLureImage
                {
                    Id = 1,
                    Image = await File.ReadAllBytesAsync(spinner)
                });
            }

            if (File.Exists(wobbler))
            {
                db.FishingLureImages.Add(new FishingLureImage
                {
                    Id = 2,
                    Image = await File.ReadAllBytesAsync(wobbler)
                });
            }

            if (File.Exists(jig))
            {
                db.FishingLureImages.Add(new FishingLureImage
                {
                    Id = 3,
                    Image = await File.ReadAllBytesAsync(jig)
                });
            }

            if (File.Exists(jerkbait))
            {
                db.FishingLureImages.Add(new FishingLureImage
                {
                    Id = 4,
                    Image = await File.ReadAllBytesAsync(jerkbait)
                });
            }

            if (File.Exists(tail))
            {
                db.FishingLureImages.Add(new FishingLureImage
                {
                    Id = 5,
                    Image = await File.ReadAllBytesAsync(tail)
                });
            }

            if (File.Exists(spoon))
            {
                db.FishingLureImages.Add(new FishingLureImage
                {
                    Id = 6,
                    Image = await File.ReadAllBytesAsync(spoon)
                });
            }

            if (File.Exists(fly))
            {
                db.FishingLureImages.Add(new FishingLureImage
                {
                    Id = 7,
                    Image = await File.ReadAllBytesAsync(fly)
                });
            }

            if (File.Exists(worm))
            {
                db.FishingLureImages.Add(new FishingLureImage
                {
                    Id = 8,
                    Image = await File.ReadAllBytesAsync(worm)
                });
            }

            if (File.Exists(crankbait))
            {
                db.FishingLureImages.Add(new FishingLureImage
                {
                    Id = 9,
                    Image = await File.ReadAllBytesAsync(crankbait)
                });
            }

            if (File.Exists(popper))
            {
                db.FishingLureImages.Add(new FishingLureImage
                {
                    Id = 10,
                    Image = await File.ReadAllBytesAsync(popper)
                });
            }

            if (File.Exists(streamer))
            {
                db.FishingLureImages.Add(new FishingLureImage
                {
                    Id = 11,
                    Image = await File.ReadAllBytesAsync(streamer)
                });
            }

            if (File.Exists(nymph))
            {
                db.FishingLureImages.Add(new FishingLureImage
                {
                    Id = 12,
                    Image = await File.ReadAllBytesAsync(nymph)
                });
            }

            await db.SaveChangesAsync();
        }

        // Only explicitly insert IDs if your Postgres sequence allows it, 
        // otherwise let Postgres auto-increment and skip setting 'Id = X'
        await db.SaveChangesAsync();
    }
}