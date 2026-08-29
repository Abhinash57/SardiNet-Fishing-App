using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.DataProtection.StackExchangeRedis;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using PVT15_8.Identity;
using PVT15_8.Identity.Data;
using PVT15_8.Identity.Data.Models;
using Scalar.AspNetCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

var connectionString = builder.Configuration.GetConnectionString("identity-db");
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddSingleton<IEmailSender<User>, IdentityEmailSender>();

builder.Services.AddIdentityApiEndpoints<User>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
})
.AddRoles<IdentityRole>()
.AddSignInManager<SignInManager<User>>()
.AddEntityFrameworkStores<UserDbContext>();

builder.AddRedisClient("auth-cache");

builder.Services.AddDataProtection()
    .SetApplicationName("PVT15_8AuthApp");

builder.Services.AddOptions<KeyManagementOptions>()
    .Configure<IConnectionMultiplexer>((options, redis) =>
    {
        options.XmlRepository = new RedisXmlRepository(() => redis.GetDatabase(), "DataProtection-Keys");
    });

builder.Services.AddAuthorization();
builder.Services.AddControllers();

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Servers?.Clear();
        document.Servers?.Add(new Microsoft.OpenApi.OpenApiServer { Url = "/identity" });
        return Task.CompletedTask;
    });
});

var mailtrapToken = builder.Configuration["Mailtrap:ApiKey"] 
    ?? throw new InvalidOperationException("Mailtrap apikey not in config");
builder.Services.AddHttpClient<IEmailSender<User>, IdentityEmailSender>(client =>
{
    client.BaseAddress = new Uri("https://send.api.mailtrap.io/");
    client.DefaultRequestHeaders.Add(HeaderNames.Authorization, $"Bearer {mailtrapToken}");
});

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

var app = builder.Build();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    await app.UseDevelopmentSettings();
}

app.MapIdentityApi<User>();
app.MapUserEndpoints();

app.Run();

