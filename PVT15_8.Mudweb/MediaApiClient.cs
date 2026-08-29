using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace PVT15_8.Mudweb;

public class MediaApiClient(HttpClient httpClient)
{
    public async Task<string?> UploadImageAsync(string endpointPath, IBrowserFile file, Dictionary<string, string>? additionalFormData = null)
    {
        using var content = new MultipartFormDataContent();

        var fileStream = file.OpenReadStream(maxAllowedSize: 5 * 1024 * 1024);
        var streamContent = new StreamContent(fileStream);

        streamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

        content.Add(streamContent, "file", file.Name);

        if (additionalFormData != null)
        {
            foreach (var kvp in additionalFormData)
            {
                content.Add(new StringContent(kvp.Value), kvp.Key);
            }
        }

        var response = await httpClient.PostAsync(endpointPath, content);

        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<UploadResponse>();

        return result?.Url;
    }

    public async Task<bool> DeleteImageAsync(string endpointPath)
    {
        var response = await httpClient.DeleteAsync(endpointPath);
        return response.IsSuccessStatusCode;
    }
}

public record UploadResponse(string Url);
