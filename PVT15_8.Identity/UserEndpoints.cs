using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.WebUtilities;
using PVT15_8.Identity.Data;
using PVT15_8.Identity.Data.Models;
using PVT15_8.Shared;
using System.Security.Claims;
using System.Text;

namespace PVT15_8.Identity;

public static class UserDataEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        app.MapPost("/account/register", async (
            RegisterRequestUsername request,
            UserManager<User> userManager,
            IEmailSender<User> emailSender,
            HttpContext httpContext) =>
        {
            var user = new User
            {
                DisplayName = request.Username,
                UserName = request.Email,
                Email = request.Email,
            };

            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return Results.ValidationProblem(result.Errors
                    .GroupBy(e => e.Code)
                    .ToDictionary(g => g.Key, g => g.Select(e => e.Description).ToArray()));

            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var confirmationLink = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/identity/confirmEmail?userId={user.Id}&code={encodedToken}";

            await emailSender.SendConfirmationLinkAsync(user, user.Email, confirmationLink);

            return Results.Ok();
        });

        app.MapGet("/manage/roles", async (ClaimsPrincipal user) =>
        {
            return TypedResults.Ok(user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value));
        }).RequireAuthorization();

        app.MapPost("/logout", async (SignInManager<User> signInManager) =>
        {
            await signInManager.SignOutAsync();

            return TypedResults.Ok();
        })
        .RequireAuthorization();

        app.MapPut("/profile", async Task<IResult> (
            UpdateProfileRequest request,
            ClaimsPrincipal principal,
            UserManager<User> userManager,
            UserDbContext context) =>
        {
            var user = await userManager.GetUserAsync(principal);

            if (user is null)
                return TypedResults.NotFound("User not found.");

            if (request.Bio is not null)
                user.Bio = request.Bio;

            if (request.ProfilePictureUrl is not null)
                user.ProfilePictureUrl = request.ProfilePictureUrl;

            if (request.DisplayName is not null)
                user.DisplayName = request.DisplayName;

            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return TypedResults.BadRequest(result.Errors);

            return TypedResults.NoContent();
        })
        .RequireAuthorization();

        app.MapDelete("/profile", async Task<IResult> (
            ClaimsPrincipal principal,
            UserManager<User> userManager,
            UserDbContext context) =>
        {
            var user = await userManager.GetUserAsync(principal);

            if (user is null)
                return TypedResults.NotFound("User not found.");

            var result = await userManager.DeleteAsync(user);

            if (!result.Succeeded)
                return TypedResults.BadRequest(result.Errors);

            return TypedResults.NoContent();
        })
        .RequireAuthorization();

        app.MapGet("/profile", async Task<IResult> (ClaimsPrincipal principal, UserManager<User> userManager) =>
        {
            var user = await userManager.GetUserAsync(principal);

            return user is not null
                ? TypedResults.Ok(new ProfileDTO { DisplayName = user.DisplayName, ProfilePictureUrl = user.ProfilePictureUrl, Bio = user.Bio })
                : TypedResults.NotFound("User not found.");
        })
        .RequireAuthorization();


        app.MapGet("/manage/info/name", async Task<IResult> (ClaimsPrincipal principal, UserManager<User> userManager) =>
        {
            var user = await userManager.GetUserAsync(principal);

            return user is not null
                ? TypedResults.Ok(new UserInfo(user.Id, user.DisplayName, user.Email!, user.EmailConfirmed))
                : TypedResults.NotFound("User not found.");
        })
        .RequireAuthorization();
    }
}
