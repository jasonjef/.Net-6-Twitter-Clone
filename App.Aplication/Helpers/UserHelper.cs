using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace App.Application.Helpers
{
    public static class UserHelper
    {
        public static ClaimsPrincipal GetUser(this HttpContext httpContext)
        {
            var user = httpContext.User;

            return user;
        }

        public static int GetUserId(this HttpContext httpContext)
        {
            var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return int.Parse(userId);
        }
    }
}