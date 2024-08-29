using Microsoft.AspNetCore.Identity;

namespace Odonto.Infra.Identity;

public class AppUser : IdentityUser
{
    public string Nome { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}