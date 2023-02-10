using System.Security.Claims;

namespace MauiTemplateEcreo.Services
{
    public interface IAuthenticationService
    {
        ClaimsPrincipal GetPrincipalFromToken(string token);
        Task<ClaimsPrincipal> SignIn(string username, string password);
    }
}