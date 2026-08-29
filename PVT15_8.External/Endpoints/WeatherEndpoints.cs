using Microsoft.Extensions.Caching.Distributed;
using PVT15_8.Shared.DTOs;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PVT15_8.External.Endpoints;

public static class WeatherEndpoints
{
    public static void MapWeatherEndpoints(this IEndpointRouteBuilder app)
    {
        // Stad sök
        app.MapGet("/weather/city/{city}", async (string city, HttpClient httpClient, ILogger<Program> logger) =>
        {
            if (string.IsNullOrWhiteSpace(city)) return Results.NotFound();

            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("FishingApp/1.0");
            var searchUrl = $"https://nominatim.openstreetmap.org/search?q={Uri.EscapeDataString(city)}&format=json&limit=1&addressdetails=1&countrycodes=se";
            var response = await httpClient.GetFromJsonAsync<GeocodeResult[]>(searchUrl);
            var first = response?.FirstOrDefault();

            if (first == null)
                return Results.NotFound($"Hittade ingen plats för '{city}'");

            var bestName = ExtractShortName(first) ?? first.DisplayName;
            if (string.IsNullOrEmpty(bestName) || !IsSimilarEnough(city, bestName))
                return Results.NotFound($"Hittade ingen plats för '{city}'.");

            if (!double.TryParse(first.lat, CultureInfo.InvariantCulture, out var lat) ||
                !double.TryParse(first.lon, CultureInfo.InvariantCulture, out var lon))
                return Results.BadRequest("Ogiltiga koordinater från Nominatim");

            string locationName = ExtractShortName(first) ?? city;
            return await GetForecastFromSmhi(lat, lon, locationName, httpClient, logger);
        });

        app.MapGet("/weather", async Task<IResult> (double lat, double lon, HttpClient client, IDistributedCache cache) =>
        {
            var key = $"weather:lat:{lat}:lon:{lon}";

            var cachedJson = await cache.GetStringAsync(key);

            if (!string.IsNullOrEmpty(cachedJson))
            {
                var cachedWeather = JsonSerializer.Deserialize<WeatherResultDto>(cachedJson);
                if (cachedWeather is not null)
                {
                    return TypedResults.Ok(cachedWeather);
                }
            }

            var weatherForecast = await GetForecastFromSmhi(lat, lon, client);

            if (weatherForecast is null)
            {
                return TypedResults.NotFound();
            }

            var jsonToCache = JsonSerializer.Serialize(weatherForecast);

            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
            };

            await cache.SetStringAsync(key, jsonToCache, cacheOptions);

            return TypedResults.Ok(weatherForecast);
        });
    }

    private static string? ExtractShortName(GeocodeResult? result) =>
        result?.address?.suburb ?? result?.address?.town ?? result?.address?.village ?? result?.address?.city;

    private static bool IsSimilarEnough(string searchTerm, string returnedName)
    {
        if (string.IsNullOrEmpty(returnedName)) return false;
        var search = searchTerm.Trim().ToLowerInvariant();
        var returned = returnedName.Trim().ToLowerInvariant();
        if (returned == search) return true;
        var words = returned.Split(new[] { ' ', ',', '-', '/' }, StringSplitOptions.RemoveEmptyEntries);
        if (words.Contains(search)) return true;
        if (search.Length >= 3)
            foreach (var w in words)
                if (w.StartsWith(search) && w.Length > search.Length) return true;
        return false;
    }

    private static async Task<IResult> GetForecastFromSmhi(double lat, double lon, string locationName, HttpClient client, ILogger logger)
    {
        try
        {
            var roundedLon = Math.Round(lon, 2);
            var roundedLat = Math.Round(lat, 2);
            var smhiUrl = $"https://opendata-download-metfcst.smhi.se/api/category/snow1g/version/1/geotype/point/lon/{roundedLon.ToString(CultureInfo.InvariantCulture)}/lat/{roundedLat.ToString(CultureInfo.InvariantCulture)}/data.json";

            logger.LogInformation("Calling SMHI: {Url}", smhiUrl);
            client.DefaultRequestHeaders.UserAgent.ParseAdd("FishingApp/1.0");
            var response = await client.GetAsync(smhiUrl);

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                logger.LogError("SMHI returned {StatusCode}: {Error}", response.StatusCode, errorBody);
                return response.StatusCode == System.Net.HttpStatusCode.NotFound
                    ? Results.NotFound("Koordinaterna ligger utanför SMHI:s prognosområde (troligen ute till havs eller utanför Sverige).")
                    : Results.Problem($"SMHI API error: {response.StatusCode}");
            }

            var smhiData = await response.Content.ReadFromJsonAsync<SmhiRawResponse>();
            if (smhiData?.TimeSeries == null || !smhiData.TimeSeries.Any())
                return Results.NotFound("Ingen prognos tillgänglig för denna plats.");

            var forecasts = new List<WeatherForecastDto>();
            const int maxHours = 7 * 24; // 168 hours = 7 days
            var timeSeriesToTake = smhiData.TimeSeries.Take(Math.Min(smhiData.TimeSeries.Count, maxHours));
            foreach (var ts in timeSeriesToTake)
            {
                var data = ts.Data;
                if (data == null) continue;
                forecasts.Add(new WeatherForecastDto
                {
                    Time = ts.Time,
                    TemperatureC = (int)Math.Round(data.AirTemperature),
                    WindSpeedMps = data.WindSpeed,
                    WindDirectionDeg = data.WindFromDirection,
                    PrecipMmh = data.PrecipitationAmountMean,

                    SymbolCode = data.SymbolCode,
                });
            }

            return Results.Ok(new WeatherResultDto { LocationName = locationName, Forecasts = forecasts });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Weather endpoint crashed");
            return Results.Problem($"Serverfel: {ex.Message}");
        }
    }

    private static async Task<WeatherResultDto?> GetForecastFromSmhi(double lat, double lon, HttpClient client)
    {
        try
        {
            var roundedLon = Math.Round(lon, 2);
            var roundedLat = Math.Round(lat, 2);
            var smhiUrl = $"https://opendata-download-metfcst.smhi.se/api/category/snow1g/version/1/geotype/point/lon/{roundedLon.ToString(CultureInfo.InvariantCulture)}/lat/{roundedLat.ToString(CultureInfo.InvariantCulture)}/data.json";

            client.DefaultRequestHeaders.UserAgent.ParseAdd("FishingApp/1.0");
            var response = await client.GetAsync(smhiUrl);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var smhiData = await response.Content.ReadFromJsonAsync<SmhiRawResponse>();
            if (smhiData?.TimeSeries == null || !smhiData.TimeSeries.Any())
                return null;

            var forecasts = new List<WeatherForecastDto>();
            const int maxHours = 7 * 24; // 168 timmar
            var timeSeriesToTake = smhiData.TimeSeries.Take(Math.Min(smhiData.TimeSeries.Count, maxHours));
            foreach (var ts in timeSeriesToTake)
            {
                var data = ts.Data;
                if (data == null) continue;
                forecasts.Add(new WeatherForecastDto
                {
                    Time = ts.Time,
                    TemperatureC = (int)Math.Round(data.AirTemperature),
                    WindSpeedMps = data.WindSpeed,
                    WindDirectionDeg = data.WindFromDirection,
                    PrecipMmh = data.PrecipitationAmountMean,
                    SymbolCode = data.SymbolCode,
                });
            }

            return new WeatherResultDto { Forecasts = forecasts };
        }
        catch (Exception)
        {
            return null;
        }
    }
}

public class SmhiRawResponse
{
    [JsonPropertyName("timeSeries")] public List<SmhiTimeSeries>? TimeSeries { get; set; }
}

public class SmhiTimeSeries
{
    [JsonPropertyName("time")] public string? Time { get; set; }
    [JsonPropertyName("data")] public SmhiData? Data { get; set; }
}

public class SmhiData
{
    [JsonPropertyName("air_temperature")] public double AirTemperature { get; set; }
    [JsonPropertyName("wind_from_direction")] public int WindFromDirection { get; set; }
    [JsonPropertyName("wind_speed")] public double WindSpeed { get; set; }
    [JsonPropertyName("precipitation_amount_mean")] public double PrecipitationAmountMean { get; set; }
    [JsonPropertyName("symbol_code")] public int SymbolCode { get; set; }
}

public class GeocodeResult
{
    public string? lat { get; set; }
    public string? lon { get; set; }
    public GeocodeAddress? address { get; set; }
    [JsonPropertyName("display_name")] public string? DisplayName { get; set; }
}

public class GeocodeAddress
{
    public string? city { get; set; }
    public string? town { get; set; }
    public string? suburb { get; set; }
    public string? village { get; set; }
}