using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Odonto.API.Services.Interface;

public interface ITokenService
{
    JwtSecurityToken GerarAccessToken(IEnumerable<Claim> claims,
        IConfiguration _config);

    string GerarRefreshToken();

    ClaimsPrincipal BuscarPrincipalPorTokenExpirado(string token,
        IConfiguration _config);
}