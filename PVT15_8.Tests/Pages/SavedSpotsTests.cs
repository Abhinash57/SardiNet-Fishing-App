using System.Text.Json;
using Bunit;
using Moq;
using MudBlazor;
using RichardSzalay.MockHttp;
using PVT15_8.Mudweb.Pages;
using PVT15_8.Shared.DTOs;
using PVT15_8.Mudweb.Tests.Helpers;

namespace PVT15_8.Mudweb.Tests.Pages;

public class SavedSpotsTests
{
    [Fact]
    public void SavedSpots_RendersPageHeading()
    {
        using var ctx = new BunitTestContext();
        ctx.FakeAuthProvider.MarkUserAsAuthenticated();
        var cut = ctx.Render<SavedSpots>();
        Assert.Contains("Sparade platser", cut.Find("h5").TextContent);
    }

    [Fact]
    public void SavedSpots_WhenApiReturnsEmpty_ShowsEmptyStateMessage()
    {
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When("/api/spots/bookmarked")
            .Respond(HttpStatusCode.OK, "application/json", JsonSerializer.Serialize(new List<FishingSpotSmallDTO>()));

        var httpClient = new HttpClient(mockHttp) { BaseAddress = new Uri("http://localhost") };

        using var ctx = new BunitTestContext();
        ctx.Services.AddSingleton(httpClient);
        ctx.FakeAuthProvider.MarkUserAsAuthenticated();

        var cut = ctx.Render<SavedSpots>();
        cut.WaitForAssertion(() =>
            Assert.Contains("Du har inga sparade platser ännu", cut.Markup));
    }

    [Fact]
    public void SavedSpots_HttpError_ShowsSnackbarWithErrorMessage()
    {
        var failHandler = new FailingHttpMessageHandler();
        var failClient = new HttpClient(failHandler) { BaseAddress = new Uri("http://localhost") };

        using var ctx = new BunitTestContext();
        ctx.Services.AddSingleton(failClient);
        ctx.FakeAuthProvider.MarkUserAsAuthenticated();

        var cut = ctx.Render<SavedSpots>();

        cut.WaitForAssertion(() =>
            ctx.SnackbarMock.Verify(
                s => s.Add(
                    It.Is<string>(msg => msg.Contains("sparade platser")),
                    Severity.Error,
                    It.IsAny<Action<SnackbarOptions>>(),
                    It.IsAny<string>()),
                Times.AtLeastOnce));
    }
}

public class FailingHttpMessageHandler : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        throw new HttpRequestException("Simulated network failure");
    }
}