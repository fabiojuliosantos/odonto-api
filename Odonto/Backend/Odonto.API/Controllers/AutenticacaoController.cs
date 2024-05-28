using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Odonto.API.DTOs.Autenticacao;
using Odonto.API.Models;
using Odonto.API.Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Odonto.API.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class AutenticacaoController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AutenticacaoController(ITokenService tokenService, UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager, IConfiguration configuration)
    {
        _tokenService = tokenService;
        _userManager = userManager;
        _roleManager = roleManager; 
        _configuration = configuration;
    }

    [HttpPost("CreateRole")]
    public async Task<IActionResult> CreateRole(string rolename)
    {
        var roleExist = await _roleManager.RoleExistsAsync(rolename);

        if (!roleExist)
        {
            var roleResult = await _roleManager.CreateAsync(new IdentityRole(rolename));

            if (roleResult.Succeeded)
            {
                return StatusCode(StatusCodes.Status200OK,
                    new Response
                    {
                        Status = "Success",
                        Message = $"Role {rolename} adicionada com sucesso!"
                    });
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response
                {
                    Status = "Error",
                    Message = $"Ocorreu um erro ao adicionar a role: {rolename}"
                });
            }
        }

        return StatusCode(StatusCodes.Status400BadRequest, new Response
        {
            Status = "Error",
            Message = "Role já existente!"
        });
    }

    [HttpPost("AddUserToRole")]
    public async Task<IActionResult> AddUserToRole(string email, string roleName)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user != null)
        {
            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                return StatusCode(StatusCodes.Status200OK, 
                    new Response
                    {
                        Status = "Success",
                        Message = $"Usuario {user.UserName} adicionado a role {roleName} com sucesso!"
                    });
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response
                {
                    Status = "Error",
                    Message = $"Não foi possível adicionar o usuário {user.UserName} a role {roleName}!"
                });
            }
        }

        return BadRequest(new
        {
            error = "Não foi possível encontrar o usuário!"
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userManager.FindByNameAsync(model.Username!);
        if (user is not null && await _userManager.CheckPasswordAsync(user, model.Password!))
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = _tokenService.GenerateAccessToken(authClaims, _configuration);

            var refreshToken = _tokenService.GenerateRefreshToken();
            _ = int.TryParse(_configuration["JWT:RefreshTokenValiditynMinutes"],
                out int refreshTokenValidityInMinutes);
            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                Expiration = token.ValidTo
            });
            return Unauthorized("Usuário não autorizado!");
        }

        return Ok();
    }

    [HttpPost("Registrar")]
    public async Task<IActionResult> Registrar([FromBody] RegisterModel model)
    {
        var existeUsuario = await _userManager.FindByNameAsync(model.Username!);

        if (existeUsuario != null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new Response
                {
                    Status = "Error",
                    Message = "Usuário já existe!"
                });
        }

        AppUser user = new()
        {
            Email = model.Email,
            UserName = model.Username,
            SecurityStamp = Guid.NewGuid().ToString(),
        };
        var result = await _userManager.CreateAsync(user, model.Password!);
        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new Response
                {
                    Status = "Erro",
                    Message = "Ocorreu um erro ao cadastrar!"
                });
        }

        return Ok(new Response { Status = "Success", Message = "Cadastro realizado com sucesso!" });
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
    {
        if (tokenModel is null) return BadRequest("Request inválido!");

        string? accessToken = tokenModel.AccessToken ?? throw new ArgumentNullException(nameof(tokenModel));
        string? refreshToken = tokenModel.RefreshToken ?? throw new ArgumentNullException(nameof(tokenModel));

        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken!, _configuration);

        if (principal is null) return BadRequest("Access/Refresh Token Inválido!");

        string usarname = principal.Identity.Name;

        var user = await _userManager.FindByNameAsync(usarname!);

        if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            return BadRequest("Access/Refresh Token Inválido!");
        }

        var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims.ToList(), _configuration);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;

        await _userManager.UpdateAsync(user);

        return new ObjectResult(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            refreshToken = newRefreshToken
        });
    }

    [Authorize]
    [HttpPost("revoke/{username}")]
    public async Task<IActionResult> Revoke(string username)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user == null) return BadRequest("Nome de usuário inválido!");

        user.RefreshToken = null;

        await _userManager.UpdateAsync(user);

        return NoContent();
    }
}