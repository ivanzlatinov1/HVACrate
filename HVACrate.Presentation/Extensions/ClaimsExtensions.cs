using HVACrate.GCommon;
using System.Security.Claims;

using static HVACrate.GCommon.GlobalConstants;

namespace HVACrate.Presentation.Extensions
{
    public static class ClaimsExtensions
    {
        public static bool IsAuthenticated(this ClaimsPrincipal user) => user.Identity?.IsAuthenticated ?? false;
        public static string GetName(this ClaimsPrincipal user) => user.Identity?.Name ?? string.Empty;
        public static Guid? GetId(this ClaimsPrincipal user) => Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty);
        public static string GetProfilePictureUrl(this ClaimsPrincipal user) => user.FindFirstValue(ProfilePictureClaimType) ?? DefaultProfilePictureUrl;
    }
}
