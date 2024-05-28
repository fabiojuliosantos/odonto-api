using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Odonto.API.AuthenticationFilter;

public class VerifyUserHasPolicyAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    private readonly string _policy;
    public VerifyUserHasPolicyAttribute(string policy)
    {
        _policy = policy;
    }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;
        var hasPolicy = user.HasClaim(ClaimTypes.Role, _policy);

        if (!context.HttpContext.User.Identity.IsAuthenticated)
        {
            context.Result = new UnauthorizedObjectResult("Usuário não autenticado!");
        }
        
        if (!hasPolicy)
        {
            context.Result = new ForbidResult("Usuário não possui permissão para realizar esta ação.");
        }
    }
}