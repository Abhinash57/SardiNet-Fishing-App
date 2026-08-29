using Bunit;
using Moq;
using PVT15_8.Mudweb.Pages;
using PVT15_8.Shared.DTOs;
using PVT15_8.Mudweb.Tests.Helpers;
using System.Text;
using System.Text.Json;

namespace PVT15_8.Mudweb.Tests.Pages;

public class HomeTests : IClassFixture<BunitTestContext>, IDisposable
{
    private readonly BunitTestContext _ctx;

    public HomeTests(BunitTestContext ctx)
    {
        _ctx = ctx;
        _ctx.FakeAuthProvider.MarkUserAsAuthenticated();
    }

    public void Dispose()
    {
        _ctx.HttpHandler.MockResponse = null;
        _ctx.JsRuntimeMock.Reset();
        GC.SuppressFinalize(this);
    }

    private static HttpResponseMessage CreateRawJsonResponse(string jsonString)
    {
        return new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(jsonString, Encoding.UTF8, "application/json")
        };
    }

    private void SetupMocks(FishingSpotDTO targetSpot, string? weatherJson = "null")
    {
        _ctx.JsRuntimeMock
            .Setup(x => x.InvokeAsync<Home.LocationResult>(
                It.IsAny<string>(), It.IsAny<object?[]?>()))
            .Returns(new ValueTask<Home.LocationResult>(
                new Home.LocationResult { Latitude = 59.32, Longitude = 18.06 }));

        var spotJson = JsonSerializer.Serialize(targetSpot);

        _ctx.HttpHandler.MockResponse = request =>
        {
            var path = request.RequestUri?.AbsolutePath.ToLower() ?? "";

            if (path.Contains("nearby"))
                return CreateRawJsonResponse(spotJson);

            if (path.Contains("random"))
                return CreateRawJsonResponse($"[{spotJson}]");

            if (path.Contains("weather") || path.Contains("smhi"))
                return CreateRawJsonResponse(weatherJson ?? "null");

            return CreateRawJsonResponse("{}");
        };
    }

    private void SetupMocksWithLocationDenied(FishingSpotDTO? randomSpot)
    {
        _ctx.JsRuntimeMock
            .Setup(x => x.InvokeAsync<Home.LocationResult>(
                It.IsAny<string>(), It.IsAny<object?[]?>()))
            .ThrowsAsync(new Exception("User denied geolocation"));

        _ctx.HttpHandler.MockResponse = request =>
        {
            var path = request.RequestUri?.AbsolutePath.ToLower() ?? "";

            if (path.Contains("random"))
            {
                var json = randomSpot is null
                    ? "[]"
                    : $"[{JsonSerializer.Serialize(randomSpot)}]";
                return CreateRawJsonResponse(json);
            }

            if (path.Contains("weather") || path.Contains("smhi"))
                return CreateRawJsonResponse("null");

            return CreateRawJsonResponse("{}");
        };
    }

    [Fact]
    public void Home_RendersWelcomeMessage_AuthorizedUser()
    {
        // Arrange
        SetupMocks(new FishingSpotDTO { Id = 99, Name = "Välkommen Sjö" });

        // Act
        var cut = _ctx.Render<Home>();

        // Assert
        cut.WaitForAssertion(() => Assert.Contains("Välkommen test@example.com!", cut.Markup));
    }

    [Fact]
    public void Home_ShowsNearbySpot_WhenLocationSucceeds()
    {
        // Arrange
        SetupMocks(new FishingSpotDTO { Id = 1, Name = "Nära Sjö" });

        // Act
        var cut = _ctx.Render<Home>();

        // Assert
        cut.WaitForAssertion(() => Assert.Contains("Nära Sjö", cut.Markup));
    }

    [Fact]
    public void Home_DisplaysForbiddenAlert_WhenSpotIsForbidden()
    {
        // Arrange
        SetupMocks(new FishingSpotDTO { Id = 2, Name = "Förbjuden", IsForbidden = true });

        // Act
        var cut = _ctx.Render<Home>();

        // Assert
        cut.WaitForAssertion(() =>
        {
            Assert.Contains("Det är förbjudet att fiska på den här platsen.", cut.Markup);
            Assert.DoesNotContain("särskilda regler", cut.Markup);
        });
    }

    [Fact]
    public void Home_DisplaysWarningAlerts_WhenFishingCardRequiredAndHasRules()
    {
        // Arrange
        SetupMocks(new FishingSpotDTO
        {
            Id = 3,
            Name = "Regel Sjö",
            IsForbidden = false,
            HasRules = true,
            IsFishingCardRequired = true
        });

        // Act
        var cut = _ctx.Render<Home>();

        // Assert
        cut.WaitForAssertion(() =>
        {
            Assert.Contains("särskilda regler", cut.Markup);
            Assert.Contains("Fiskekort krävs", cut.Markup);
        });
    }

    [Fact]
    public void Home_ShowsWarningAlert_WhenNoSpotsFound_AfterLocationDenied()
    {
        // Arrange
        SetupMocksWithLocationDenied(randomSpot: null);

        // Act
        var cut = _ctx.Render<Home>();

        // Assert
        cut.WaitForAssertion(() =>
            Assert.Contains("kunde tyvärr inte hitta", cut.Markup,
                StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void Home_ShowsWarningAlert_WhenNearbyReturnsEmpty_AndNoRandomFallback()
    {
        // Arrange
        _ctx.JsRuntimeMock
            .Setup(x => x.InvokeAsync<Home.LocationResult>(
                It.IsAny<string>(), It.IsAny<object?[]?>()))
            .Returns(new ValueTask<Home.LocationResult>(
                new Home.LocationResult { Latitude = 0, Longitude = 0 }));

        _ctx.HttpHandler.MockResponse = request =>
        {
            var path = request.RequestUri?.AbsolutePath.ToLower() ?? "";
            if (path.Contains("nearby") || path.Contains("random"))
                return CreateRawJsonResponse("[]");
            return CreateRawJsonResponse("null");
        };

        // Act
        var cut = _ctx.Render<Home>();

        // Assert
        cut.WaitForAssertion(() =>
            Assert.Contains("kunde tyvärr inte hitta", cut.Markup,
                StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void Home_ShowsOnlyRulesWarning_WhenHasRulesButNoCardRequired()
    {
        // Arrange
        SetupMocks(new FishingSpotDTO
        {
            Id = 4,
            Name = "Regler Sjö",
            IsForbidden = false,
            HasRules = true,
            IsFishingCardRequired = false
        });

        // Act
        var cut = _ctx.Render<Home>();

        // Assert
        cut.WaitForAssertion(() =>
        {
            Assert.Contains("särskilda regler", cut.Markup);
            Assert.DoesNotContain("Fiskekort krävs", cut.Markup);
        });
    }

    [Fact]
    public void Home_ShowsOnlyFishingCardWarning_WhenCardRequiredButNoRules()
    {
        // Arrange
        SetupMocks(new FishingSpotDTO
        {
            Id = 5,
            Name = "Korts Sjö",
            IsForbidden = false,
            HasRules = false,
            IsFishingCardRequired = true
        });

        // Act
        var cut = _ctx.Render<Home>();

        // Assert
        cut.WaitForAssertion(() =>
        {
            Assert.DoesNotContain("särskilda regler", cut.Markup);
            Assert.Contains("Fiskekort krävs", cut.Markup);
        });
    }

    [Fact]
    public void Home_ShowsNoWarnings_WhenSpotHasNeitherRulesNorCardRequired()
    {
        // Arrange
        SetupMocks(new FishingSpotDTO
        {
            Id = 6,
            Name = "Fri Sjö",
            IsForbidden = false,
            HasRules = false,
            IsFishingCardRequired = false
        });

        // Act
        var cut = _ctx.Render<Home>();

        // Assert
        cut.WaitForAssertion(() =>
        {
            Assert.DoesNotContain("särskilda regler", cut.Markup);
            Assert.DoesNotContain("Fiskekort krävs", cut.Markup);
            Assert.DoesNotContain("förbjudet", cut.Markup);
        });
    }

    [Fact]
    public void Home_ForbiddenSpot_SuppressesRulesAndCardWarnings()
    {
        // Arrange
        SetupMocks(new FishingSpotDTO
        {
            Id = 7,
            Name = "Stängt",
            IsForbidden = true,
            HasRules = true,
            IsFishingCardRequired = true
        });

        // Act
        var cut = _ctx.Render<Home>();

        // Assert
        cut.WaitForAssertion(() =>
        {
            Assert.Contains("förbjudet", cut.Markup, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("särskilda regler", cut.Markup);
            Assert.DoesNotContain("Fiskekort krävs", cut.Markup);
        });
    }

    [Fact]
    public void Home_DoesNotRenderFishCarousel_WhenNoSpeciesPresent()
    {
        // Arrange
        SetupMocks(new FishingSpotDTO
        {
            Id = 8,
            Name = "Tom Sjö",
            FishSpecies = new List<FishSpeciesDTO>()
        });

        // Act
        var cut = _ctx.Render<Home>();

        // Assert
        cut.WaitForAssertion(() => Assert.DoesNotContain("Fiskarter", cut.Markup));
    }

    [Fact]
    public void Home_DoesNotRenderLureCarousel_WhenNoLuresPresent()
    {
        // Arrange
        SetupMocks(new FishingSpotDTO
        {
            Id = 14,
            Name = "Beteslös Sjö",
            RecommendedLures = new List<FishingLureDTO>()
        });

        // Act
        var cut = _ctx.Render<Home>();

        // Assert
        cut.WaitForAssertion(() => Assert.DoesNotContain("Rekommenderade beten", cut.Markup));
    }

    [Fact]
    public void Home_RendersLureType_WhenTypeIsPresent()
    {
        // Arrange
        SetupMocks(new FishingSpotDTO
        {
            Id = 16,
            Name = "Typ Sjö",
            RecommendedLures = new List<FishingLureDTO>
            {
                new() { Name = "Masken", Type = "Naturligt bete" }
            }
        });

        // Act
        var cut = _ctx.Render<Home>();

        // Assert
        cut.WaitForAssertion(() => Assert.Contains("Naturligt bete", cut.Markup));
    }

    [Fact]
    public void Home_DoesNotRenderLureType_WhenTypeIsEmpty()
    {
        // Arrange
        SetupMocks(new FishingSpotDTO
        {
            Id = 17,
            Name = "Typfri Sjö",
            RecommendedLures = new List<FishingLureDTO>
            {
                new() { Name = "Okänt bete", Type = "" }
            }
        });

        // Act
        var cut = _ctx.Render<Home>();

        // Assert
        cut.WaitForAssertion(() =>
        {
            Assert.Contains("Okänt bete", cut.Markup);
            Assert.DoesNotContain("color:#888", cut.Markup);
        });
    }

    [Fact]
    public void Home_ShowsWeatherFallbackText_WhenWeatherApiReturnsNull()
    {
        // Arrange
        SetupMocks(
            new FishingSpotDTO { Id = 18, Name = "Väder Sjö" },
            weatherJson: "null");

        // Act
        var cut = _ctx.Render<Home>();

        // Assert
        cut.WaitForAssertion(() =>
            Assert.Contains("Kunde ej hämta väder", cut.Markup));
    }

    [Fact]
    public void Home_DoesNotShowDistance_WhenLocationIsDenied()
    {
        // Arrange
        SetupMocksWithLocationDenied(new FishingSpotDTO
        {
            Id = 21,
            Name = "Fjärran Sjö",
            DistanceFromUserMeters = 10000
        });

        // Act
        var cut = _ctx.Render<Home>();

        // Assert
        cut.WaitForAssertion(() =>
        {
            Assert.Contains("Fjärran Sjö", cut.Markup);
            Assert.DoesNotContain("km", cut.Markup);
        });
    }

    [Fact]
    public void Home_ShowsLoadingSpinner_BeforeDataArrives()
    {
        // Arrange
        var tcs = new TaskCompletionSource<Home.LocationResult>();

        _ctx.JsRuntimeMock
            .Setup(x => x.InvokeAsync<Home.LocationResult>(
                It.IsAny<string>(), It.IsAny<object?[]?>()))
            .Returns(new ValueTask<Home.LocationResult>(tcs.Task));

        // Act
        var cut = _ctx.Render<Home>();

        // Assert
        Assert.Contains("mud-progress-circular", cut.Markup,
            StringComparison.OrdinalIgnoreCase);
    }
}