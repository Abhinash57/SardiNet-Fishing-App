namespace PVT15_8.Media.Data.Models;

public abstract class ImageBase
{
    public int Id { get; set; }
    public byte[] Image { get; set; } = [];
}

public class ProfilePictureImage : ImageBase
{
    public string UserId { get; set; } = string.Empty;
}

public class ReportCatchImage : ImageBase;
public class FishSpeciesImage : ImageBase;
public class FishingLureImage : ImageBase;

public static class FileHelper
{
    public static async Task<byte[]> GetBytesAsync(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }
}
