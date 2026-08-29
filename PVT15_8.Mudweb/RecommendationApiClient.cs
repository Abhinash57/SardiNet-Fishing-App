using PVT15_8.Shared.DTOs;
using System.Globalization;
using System.Net.Http.Json;

namespace PVT15_8.Mudweb;

public class RecommendationApiClient(HttpClient httpClient)
{
    public async Task<List<FishingSpotDTO>> GetNerabyFishingSpotAsync(double lon, double lat, int take = 1)
    {
        var url = $"api/recommendations/nearby?userLon={lon.ToString(CultureInfo.InvariantCulture)}&userLat={lat.ToString(CultureInfo.InvariantCulture)}&take={take}";

        var response = await httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(error);
        }
        return await response.Content.ReadFromJsonAsync<List<FishingSpotDTO>>() ?? [];
    }
    public async Task<List<FishingSpotDTO>> GetRandomFishingSpotAsync(int take = 1)
    {
        return await httpClient.GetFromJsonAsync<List<FishingSpotDTO>>($"api/recommendations/random?take={take}") ?? [];
    }

}
