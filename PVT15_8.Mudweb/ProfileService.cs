using PVT15_8.Shared;
using PVT15_8.Shared.DTOs;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace PVT15_8.Mudweb;

public class ProfileService(HttpClient httpClient)
{
    public async Task<UserStatsDTO?> GetProfileStatsAsync()
    {
        try
        {
            var userProfile = await httpClient.GetFromJsonAsync<UserStatsDTO>("/api/profile");
            return userProfile;
        }
        catch
        {
            return null;
        }
    }

    public async Task<ProfileDTO?> GetProfileAsync()
    {
        try
        {
            var profile = await httpClient.GetFromJsonAsync<ProfileDTO>("/identity/profile");
            return profile;
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> UpdateProfileAsync(UpdateProfileRequest request)
    {
        try
        {
            var response = await httpClient.PutAsJsonAsync("/identity/profile", request);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteProfileWithDataAsync()
    {
        try
        {
            var profileTask = httpClient.DeleteAsync("/identity/profile");
            var dataTask = httpClient.DeleteAsync("/api/profile");
            await Task.WhenAll(profileTask, dataTask);

            return profileTask.Result.IsSuccessStatusCode
                   && dataTask.Result.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}