using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;

namespace Bzway.Site.FrontPage.Controllers
{
    internal class IdentityResult
    {
        public IEnumerable<Error> Errors { get; set; }
        public bool IsLockedOut { get; set; }
        public object LoginProvider { get; set; }
        public ClaimsPrincipal Principal { get; set; }
        public object ProviderKey { get; set; }
        public bool RequiresTwoFactor { get; set; }
        public bool Succeeded { get; set; }
    }
}