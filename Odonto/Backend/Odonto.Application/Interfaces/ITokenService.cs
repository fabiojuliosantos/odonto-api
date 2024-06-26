﻿using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Odonto.Application.Interfaces;

public interface ITokenService
{
    JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims,
        IConfiguration _config);
    string GenerateRefreshToken();

    ClaimsPrincipal GetPrincipalFromExpiredToken(string token,
        IConfiguration _config);
}
