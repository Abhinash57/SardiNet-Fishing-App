using Microsoft.AspNetCore.Identity;

namespace PVT15_8.Identity.Data.Models;

public class User : IdentityUser
{
    public string DisplayName { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public string? ProfilePictureUrl { get; set; }
}
