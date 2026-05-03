
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using AnalyzerQC.Application;
using Microsoft.AspNetCore.Http;
namespace AnalyzerQC.Infrastructure;

public sealed class UserContext(IHttpContextAccessor httpContextAccessor): IUserContext
{
    public Guid UserId =>
        httpContextAccessor
            .HttpContext?
            .User?
            .GetUserId() ?? Guid.Empty;
    public bool IsAuthenticated =>
        httpContextAccessor
            .HttpContext?
            .User?
            .Identity?
            .IsAuthenticated ?? false;
}
internal static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal? principal)
    {
        var userId = principal?.FindFirst(JwtRegisteredClaimNames.Jti);
        return Guid.TryParse(userId?.Value, out Guid parsedUserId) ? parsedUserId : Guid.Empty;
    }
}