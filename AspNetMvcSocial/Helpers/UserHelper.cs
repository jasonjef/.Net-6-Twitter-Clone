using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMvcSocial
{
    public static class HttpContextExtensions
    {
        public static ClaimsPrincipal GetUser(this HttpContext httpContext)
        {
            var user = httpContext.User;

            return user;
        }

        public static int GetUserId(this HttpContext httpContext)
        {
            var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            return int.Parse(userId);
        }

    }
}
