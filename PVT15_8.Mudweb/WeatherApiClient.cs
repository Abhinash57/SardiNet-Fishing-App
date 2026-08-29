using PVT15_8.Shared.DTOs;
using System.Globalization;
using System.Net.Http.Json;

namespace PVT15_8.Mudweb;

public class WeatherApiClient(HttpClient httpClient)
{
    public async Task<WeatherResultDto?> GetSmhiForecastByCityAsync(string city)
    {
        var response = await httpClient.GetAsync($"external/weather/city/{Uri.EscapeDataString(city)}");
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(error);
        }
        return await response.Content.ReadFromJsonAsync<WeatherResultDto>();
    }

    public async Task<WeatherResultDto?> GetSmhiForecastAsync(double lon, double lat)
    {
        var url = $"external/weather?lon={lon.ToString(CultureInfo.InvariantCulture)}&lat={lat.ToString(CultureInfo.InvariantCulture)}";
        var response = await httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(error);
        }
        return await response.Content.ReadFromJsonAsync<WeatherResultDto>();
    }
}
