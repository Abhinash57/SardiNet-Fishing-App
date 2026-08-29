using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.DataProtection.StackExchangeRedis;
using Microsoft.AspNetCore.Identity;
using Scalar.AspNetCore;
using StackExchange.Redis;
using System.Security.Claims;
using Yarp.ReverseProxy.Transforms;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddOpenApi();

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .AddServiceDiscoveryDestinationResolver()
    .AddTransforms(transformBuilderContext =>
    {
        if (transformBuilderContext.Route.ClusterId == "api-cluster" 
        || transformBuilderContext.Route.ClusterId == "external-cluster"
        || transformBuilderContext.Route.ClusterId == "media-cluster")
        {
            transformBuilderContext.AddRequestTransform(context =>
            {
                var user = context.HttpContext.User;

                if (user.Identity?.IsAuthenticated == true)
                {
                    var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                              ?? user.FindFirst("sub")?.Value;

                    var roles = user.Claims
                        .Where(c => c.Type == ClaimTypes.Role || c.Type == "role")
                        .Select(c => c.Value)
                        .ToList();

                    var name = user.FindFirst(ClaimTypes.Name)?.Value;

                    if (!string.IsNullOrEmpty(name))
                    {
                        context.ProxyRequest.Headers.TryAddWithoutValidation("X-User-Name", name);
                    }

                    if (!string.IsNullOrEmpty(userId))
                    {
                        context.ProxyRequest.Headers.TryAddWithoutValidation("X-User-Id", userId);
                    }

                    if (roles.Any())
                    {
                        context.ProxyRequest.Headers.TryAddWithoutValidation("X-User-Roles", string.Join(",", roles));
                    }
                }
                return ValueTask.CompletedTask;
            });
        }
    });
builder.AddRedisClient("auth-cache");

builder.Services.AddDataProtection()
    .SetApplicationName("PVT15_8AuthApp");

builder.Services.AddOptions<KeyManagementOptions>()
    .Configure<IConnectionMultiplexer>((options, redis) =>
    {
        options.XmlRepository = new RedisXmlRepository(() => redis.GetDatabase(), "DataProtection-Keys");
    });

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://localhost:7060", "http://localhost:5289", "https://sardinet.monimon.org")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddCookie(IdentityConstants.ApplicationScheme, options =>
    {
        options.Cookie.Name = ".AspNetCore.Identity.Application";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = 401;
            return Task.CompletedTask;
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title = "PVT15_8 API";

        options.Agent = new ScalarAgentOptions
        {
            Disabled = true,
        };

        options.AddDocument(
            documentName: "identity",
            title: "Identity API",
            routePattern: "/docs/identity/openapi.json"
        );

        options.AddDocument(
            documentName: "api",
            title: "Core API",
            routePattern: "/docs/api/openapi.json",
            isDefault: true
        );

        options.AddDocument(
            documentName: "external",
            title: "External Data API",
            routePattern: "/docs/external/openapi.json"
        );

        /* inte visa i scalar
        options.AddDocument(
            documentName: "media",
            title: "Media API",
            routePattern: "/docs/media/openapi.json"
        );
        */
    });
}

app.UseRouting();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapReverseProxy();

app.MapGet("/", () => Results.Redirect("/scalar"));

app.Run();
