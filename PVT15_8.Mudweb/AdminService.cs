using PVT15_8.Shared.DTOs;
using System.Net.Http.Json;

namespace PVT15_8.Mudweb;

public class AdminService(HttpClient httpClient)
{
    public async Task<FishingSpotDTO?> GetFishingSpotByIdAsync(int id)
    {
        try
        {
            return await httpClient.GetFromJsonAsync<FishingSpotDTO>($"/api/fishingspots/{id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public async Task<List<FishingLureDTO>> GetFishingLuresAsync()
    {
        try
        {
            return await httpClient.GetFromJsonAsync<List<FishingLureDTO>>("/api/fishinglures") ?? [];
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return [];
        }
    }

    public async Task<bool> PostFishingLureAsync(RequestFishingLureDTO request)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync("/api/fishinglures", request);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> PutFishingLureAsync(int id, RequestFishingLureDTO request)
    {
        try
        {
            var response = await httpClient.PutAsJsonAsync($"/api/fishinglures/{id}", request);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
    public async Task<bool> DeleteFishingLureAsync(int id)
    {
        try
        {
            var response = await httpClient.DeleteAsync($"/api/fishinglures/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
    public async Task<List<FishSpeciesDTO>> GetFishSpeciesAsync()
    {
        try
        {
            return await httpClient.GetFromJsonAsync<List<FishSpeciesDTO>>("/api/fishspecies") ?? [];
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return [];
        }
    }
    public async Task<bool> PostFishSpeciesAsync(RequestFishSpeciesDTO request)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync("/api/fishspecies", request);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
    public async Task<bool> PutFishSpeciesAsync(int id, RequestFishSpeciesDTO request)
    {
        try
        {
            var response = await httpClient.PutAsJsonAsync($"/api/fishspecies/{id}", request);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
    public async Task<bool> DeleteFishSpeciesAsync(int id)
    {
        try
        {
            var response = await httpClient.DeleteAsync($"/api/fishspecies/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> PutFishingSpotAsync(int id, RequestFishingSpotDTO request)
    {
        try
        {
            var response = await httpClient.PutAsJsonAsync($"/api/fishingspots/{id}", request);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task CreateFishingSpotMarker(FishingSpotMarkerDTO fishingSpot)
    {
        var response = await httpClient.PostAsJsonAsync("/api/fishingspots", fishingSpot);
        response.EnsureSuccessStatusCode();
    }
    public async Task DeleteFishingSpotMarker(int id)
    {
        var response = await httpClient.DeleteAsync($"/api/fishingspots/{id}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<List<FishingSpotMarkerDTO>> GetFishingSpots()
    {
        var response = await httpClient.GetAsync("/api/fishingspots");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<FishingSpotMarkerDTO>>() ?? [];
    }

    public async Task UpdateFishingSpotMarker(int id, FishingSpotMarkerDTO fishingSpot)
    {
        var response = await httpClient.PutAsJsonAsync($"/api/fishingspots/{id}", fishingSpot);
        response.EnsureSuccessStatusCode();
    }

    public async Task<List<FishingSpotMarkerDTO>> GetFilteredFishingSpotsAsync(FishingSpotFilterDto filter)
    {
        var response = await httpClient.PostAsJsonAsync("/api/fishingspots/filter", filter);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<FishingSpotMarkerDTO>>() ?? [];
    }
}