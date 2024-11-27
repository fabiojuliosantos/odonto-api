using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Odonto.API.DTOs.Autenticacao;
using Odonto.Application.Interfaces;
using Odonto.Infra.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Odonto.API.Controllers;

[Route("api/[Controller]")]
[ApiController]
[Produces("application/json")]

public class AutenticacaoController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AutenticacaoController> _logger;
    public AutenticacaoController(ITokenService tokenService,
                                  UserManager<AppUser> userManager,
                                  RoleManager<IdentityRole> roleManager,
                                  IConfiguration configuration)
    {
        _tokenService = tokenService;
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    /// <summary>
    /// Cadastra uma nova role para permissão de acesso dos usuários
    /// </summary>
    /// <param name="rolename">Nome da role que será cadastrada</param>
    /// <returns>Retorna uma mensagem de sucesso com a role cadastrada</returns>
    [HttpPost("CreateRole")]
    public async Task<IActionResult> CreateRole(string rolename)
    {
        var roleExist = await _roleManager.RoleExistsAsync(rolename);

        if (!roleExist)
        {
            var roleResult = await _roleManager.CreateAsync(new IdentityRole(rolename));

            if (roleResult.Succeeded)
            {
                _logger.LogInformation($"Role{rolename} criada!");
                return StatusCode(StatusCodes.Status200OK,
                    new Response
                    {
                        Status = "Success",
                        Message = $"Role {rolename} adicionada com sucesso!"
                    });
            }
            else
            {
                _logger.LogError($"Ocorreu um erro ao adicionar a role: {rolename}");
                return StatusCode(StatusCodes.Status400BadRequest, new Response
                {
                    Status = "Error",
                    Message = $"Ocorreu um erro ao adicionar a role: {rolename}"
                });
            }
        }

        _logger.LogError($"Role: {rolename} já existente.");
        return StatusCode(StatusCodes.Status400BadRequest, new Response
        {
            Status = "Error",
            Message = "Role já existente!"
        });
    }

    /// <summary>
    /// Atribui uma role ao usuário informado pelo e-mail
    /// </summary>
    /// <param name="email">E-mail do usuário que receberá a role</param>
    /// <param name="roleName">Nome da role que será atribuída ao usuário</param>
    /// <returns>retorna uma mensagem de sucesso com o nome do usuário e a role atribuída</returns>
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
    /// <summary>
    /// Realiza o login do usuário cadastrado
    /// </summary>
    /// <param name="model">Objeto com as informações do usuário para login</param>
    /// <returns>Retorna o Token JWT do usuário, Tempo de validade do token e Refresh Token</returns>
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
    /// <summary>
    /// Registra um novo usuário
    /// </summary>
    /// <param name="model">Objeto com as informações do usuário para registro</param>
    /// <returns>Mensagem de sucesso, usuário cadastrado com sucesso!</returns>
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
            Nome = model.Nome
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
    /// <summary>
    /// Gera um novo refresh token para o usuário
    /// </summary>
    /// <param name="tokenModel">Objeto com as informações do token para gerar novo refresh</param>
    /// <returns>Retorna um novo Refresh Token</returns>
    /// <exception cref="ArgumentNullException"></exception>
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

    /// <summary>
    /// Revoga o token do usuário [Endpoint Protegido]
    /// </summary>
    /// <param name="username">Nome do usuário que terá o token revogado</param>
    /// <returns>Retorna código 200 sem conteúdo</returns>
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