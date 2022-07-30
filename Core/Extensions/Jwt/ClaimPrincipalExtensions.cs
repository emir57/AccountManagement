using System.Security.Claims;

namespace Core.Extensions.Jwt
{
    public static class ClaimPrincipalExtensions
    {
        public static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string type)
        {
            return claimsPrincipal?.FindAll(type)?.Select(c => c?.Value)?.ToList();
        }

        public static string ClaimId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims(ClaimTypes.NameIdentifier).First();
        }

        public static string ClaimEmail(this ClaimsPrincipal claimsPrincipal)
        {
            string email = claimsPrincipal?.Claims(ClaimTypes.Email)?.FirstOrDefault() ?? "<Anonymous>";
            return email;
        }
    }
}
