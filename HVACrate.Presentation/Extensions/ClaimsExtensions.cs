using System.Security.Claims;

namespace HVACrate.Presentation.Extensions
{
    public static class ClaimsExtensions
    {
        public static bool IsAuthenticated(this ClaimsPrincipal user) => user.Identity?.IsAuthenticated ?? false;
        public static string? GetId(this ClaimsPrincipal user) => user.FindFirstValue(ClaimTypes.NameIdentifier) ?? null;
    }
}
