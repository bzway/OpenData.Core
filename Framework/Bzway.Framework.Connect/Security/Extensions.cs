using Microsoft.AspNetCore.Builder;
using System.Security.Principal;

namespace Bzway.Framework.Connect
{
    public static class Extensions
    {
        public static void UsAuthorized(this IApplicationBuilder app)
        {
            app.UseMiddleware<UserIdentityMiddleware>();
        }

    }
}