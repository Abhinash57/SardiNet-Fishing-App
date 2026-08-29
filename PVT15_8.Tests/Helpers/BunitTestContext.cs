using Bunit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Moq;
using MudBlazor;
using MudBlazor.Services;
using PVT15_8.Mudweb;
using System.Security.Claims;

namespace PVT15_8.Mudweb.Tests.Helpers;

public class BunitTestContext : BunitContext
{
    public TestHttpMessageHandler HttpHandler { get; }
    public Mock<NavigationManager> NavigationManagerMock { get; }
    public Mock<IJSRuntime> JsRuntimeMock { get; }
    public Mock<ISnackbar> SnackbarMock { get; }
    public Mock<IDialogService> DialogServiceMock { get; }
    public AdminService RealAdminService { get; }
    public WeatherApiClient RealWeatherApiClient { get; }
    public RecommendationApiClient RealRecommendationApiClient { get; }
    public ProfileService RealProfileService { get; }
    public FishApiClient RealFishApiClient { get; }
    public FakeIdentityAuthenticationStateProvider FakeAuthProvider { get; }
    public HttpClient HttpClient { get; }

    public BunitTestContext()
    {
        Services.AddMudServices();

        Services.AddAuthorizationCore();
        Services.AddScoped<IAuthorizationPolicyProvider, DefaultAuthorizationPolicyProvider>();
        Services.AddScoped<IAuthorizationService, DefaultAuthorizationService>();
        Services.AddScoped<IAuthorizationHandlerProvider, DefaultAuthorizationHandlerProvider>();
        Services.AddScoped<IAuthorizationHandlerContextFactory, DefaultAuthorizationHandlerContextFactory>();
        Services.AddScoped<IAuthorizationEvaluator, DefaultAuthorizationEvaluator>();

        NavigationManagerMock = new Mock<NavigationManager>();
        Services.AddSingleton(NavigationManagerMock.Object);

        JsRuntimeMock = new Mock<IJSRuntime>();
        Services.AddSingleton(JsRuntimeMock.Object);

        SnackbarMock = new Mock<ISnackbar>();
        Services.AddSingleton(SnackbarMock.Object);

        DialogServiceMock = new Mock<IDialogService>();
        Services.AddSingleton(DialogServiceMock.Object);

        HttpHandler = new TestHttpMessageHandler();
        HttpClient = new HttpClient(HttpHandler) { BaseAddress = new Uri("http://localhost") };
        Services.AddSingleton(HttpClient);

        RealAdminService = new AdminService(HttpClient);
        Services.AddSingleton(RealAdminService);

        RealWeatherApiClient = new WeatherApiClient(HttpClient);
        Services.AddSingleton(RealWeatherApiClient);

        RealRecommendationApiClient = new RecommendationApiClient(HttpClient);
        Services.AddSingleton(RealRecommendationApiClient);

        RealProfileService = new ProfileService(HttpClient);
        Services.AddSingleton(RealProfileService);

        RealFishApiClient = new FishApiClient(HttpClient);
        Services.AddSingleton(RealFishApiClient);

        FakeAuthProvider = new FakeIdentityAuthenticationStateProvider(HttpClient);
        Services.AddSingleton<IdentityAuthenticationStateProvider>(FakeAuthProvider);
        Services.AddSingleton<AuthenticationStateProvider>(FakeAuthProvider);

        RenderTree.Add<CascadingAuthenticationState>();
    }
}

public class TestHttpMessageHandler : DelegatingHandler
{
    public Func<HttpRequestMessage, HttpResponseMessage>? MockResponse { get; set; }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (MockResponse != null)
        {
            return Task.FromResult(MockResponse(request));
        }

        var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
        {
            Content = new StringContent("{}", System.Text.Encoding.UTF8, "application/json")
        };
        return Task.FromResult(response);
    }
}

public class FakeIdentityAuthenticationStateProvider : IdentityAuthenticationStateProvider
{
    private AuthenticationState _authenticationState;
    private bool _markUserAsAuthenticatedCalled = false;

    public FakeIdentityAuthenticationStateProvider(HttpClient httpClient) : base(httpClient)
    {
        _authenticationState = new AuthenticationState(new ClaimsPrincipal());
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return Task.FromResult(_authenticationState);
    }

    public new void MarkUserAsAuthenticated()
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, "test-user"),
            new Claim(ClaimTypes.Name, "test@example.com")
        }, "test"));
        _authenticationState = new AuthenticationState(user);
        _markUserAsAuthenticatedCalled = true;
        NotifyAuthenticationStateChanged(Task.FromResult(_authenticationState));
    }

    public bool WasMarkUserAsAuthenticatedCalled => _markUserAsAuthenticatedCalled;
}