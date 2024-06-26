using Microsoft.AspNetCore.Identity;

namespace Odonto.Infra.Identity;

public class AppUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}