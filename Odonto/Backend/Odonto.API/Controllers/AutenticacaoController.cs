using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Odonto.API.Models;
using Odonto.API.Services.Interface;

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
}