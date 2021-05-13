using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            // Get username from current ClaimsPrinciple
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}