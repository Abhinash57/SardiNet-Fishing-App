namespace PVT15_8.Shared;

public class ProfileDTO
{
    public string? DisplayName { get; set; }
    public string? Bio { get; set; }
    public string? ProfilePictureUrl { get; set; }
}

public record UpdateProfileRequest(string? DisplayName, string? Bio, string? ProfilePictureUrl);

public record UserInfo(string UserId, string Username, string Email, bool IsEmailConfirmed);