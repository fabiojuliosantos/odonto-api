using System.Runtime.CompilerServices;
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

        var logger = context.HttpContext.RequestServices.GetService(typeof(ILogger<VerifyUserHasPolicyAttribute>))
                     as ILogger<VerifyUserHasPolicyAttribute>;


        if (!context.HttpContext.User.Identity.IsAuthenticated)
        {
            logger?.LogError("Usuário não autenticado!");
            context.Result = new ForbidResult();

        }
        
        if (!hasPolicy)
        {
            logger?.LogError("Usuário não possui permissão para realizar esta ação.");
            context.Result = new ForbidResult();
        }
    }
}