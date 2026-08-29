using GoogleMapsComponents;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using PVT15_8.Mudweb;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

builder.Services.AddTransient<CookieHandler>();

builder.Services.AddHttpClient("GatewayClient", client =>
{
    var gatewayUrl = builder.HostEnvironment.IsDevelopment()
        ? "http://localhost:5001"
        : "https://api-sardinet.monimon.org";

    client.BaseAddress = new Uri(gatewayUrl);
})
.AddHttpMessageHandler<CookieHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("GatewayClient"));
builder.Services.AddScoped<FishApiClient>();
builder.Services.AddScoped<WeatherApiClient>();
builder.Services.AddScoped<AdminService>();
builder.Services.AddScoped<RecommendationApiClient>();
builder.Services.AddScoped<ProfileService>();
builder.Services.AddScoped<MediaApiClient>();

builder.Services.AddBlazorGoogleMaps(builder.Configuration["GoogleMaps:ApiKey"]);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityAuthenticationStateProvider>();

await builder.Build().RunAsync();
