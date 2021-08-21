using System.Security.Claims;

namespace MyWallet.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetCurrentUserId(this ClaimsPrincipal target) => target.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
