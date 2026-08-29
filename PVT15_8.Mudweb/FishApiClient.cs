using PVT15_8.Shared.DTOs;
using System.Net.Http.Json;

namespace PVT15_8.Mudweb;

public class FishApiClient(HttpClient httpClient)
{
    public async Task<string> GetFishDataAsync()
    {
        var response = await httpClient.GetAsync("/api/fish");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

}
