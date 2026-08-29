using Microsoft.AspNetCore.Components.Authorization;
using PVT15_8.Shared;
using System.Net.Http.Json;
using System.Security.Claims;

namespace PVT15_8.Mudweb;

public class IdentityAuthenticationStateProvider(HttpClient httpClient) : AuthenticationStateProvider
{
    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());
    private AuthenticationState? _cachedState;

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (_cachedState is not null)
            return _cachedState;

        try
        {
            var response = await httpClient.GetAsync("identity/manage/info/name");

            if (response.IsSuccessStatusCode)
            {
                var info = await response.Content.ReadFromJsonAsync<UserInfo>();
                var claims = new List<Claim> {
                    new(ClaimTypes.NameIdentifier, info!.UserId),
                    new(ClaimTypes.Name, info!.Username),
                    new(ClaimTypes.Email, info.Email)
                };

                var rolesResponse = await httpClient.GetAsync("identity/manage/roles");
                if (rolesResponse.IsSuccessStatusCode)
                {
                    var roles = await rolesResponse.Content.ReadFromJsonAsync<IEnumerable<string>>();
                    if (roles is not null)
                    {
                        foreach (var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role));
                        }
                    }
                }

                var identity = new ClaimsIdentity(claims, "CookieAuth");
                _cachedState = new AuthenticationState(new ClaimsPrincipal(identity));
                return _cachedState;
            }
        }
        catch { }
        _cachedState = new AuthenticationState(_anonymous);
        return _cachedState;
    }

    public void MarkUserAsAuthenticated()
    {
        _cachedState = null;
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task LogoutAsync()
    {
        await httpClient.PostAsync("/identity/logout", null);
        _cachedState = new AuthenticationState(_anonymous);
        NotifyAuthenticationStateChanged(Task.FromResult(_cachedState));
    }
}

