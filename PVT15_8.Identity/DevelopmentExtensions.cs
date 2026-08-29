using Microsoft.AspNetCore.Identity;
using PVT15_8.Identity.Data;
using PVT15_8.Identity.Data.Models;

namespace PVT15_8.Identity;

public static class DevelopmentExtensions
{
    public static async Task UseDevelopmentSettings(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<UserDbContext>();

        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        string[] roles = ["Admin"];
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        const string adminEmail = "admin@pvt15.g";
        if (await userManager.FindByEmailAsync(adminEmail) is null)
        {
            var admin = new User { DisplayName= "Admin Mårten", UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
            await userManager.CreateAsync(admin, "Hej123!");
            await userManager.AddToRoleAsync(admin, "Admin");
        }

        const string userTest = "user@test.g";
        if (await userManager.FindByEmailAsync(userTest) is null)
        {
            var user = new User { DisplayName = "Mårten", UserName = userTest, Email = userTest, EmailConfirmed = true };
            await userManager.CreateAsync(user, "Hej123!");
        }
    }
}
