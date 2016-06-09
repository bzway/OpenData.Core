using Microsoft.AspNetCore.Http.Authentication;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bzway.Site.FrontPage.Controllers
{
    internal class SignInManager
    {
        internal Task<IdentityResult> PasswordSignInAsync(string email, string password, bool rememberMe, bool lockoutOnFailure)
        {
            throw new NotImplementedException();
        }

        internal Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            throw new NotImplementedException();
        }

        internal Task SignOutAsync()
        {
            throw new NotImplementedException();
        }

        internal AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl)
        {
            throw new NotImplementedException();
        }

        internal Task<IdentityResult> GetExternalLoginInfoAsync()
        {
            throw new NotImplementedException();
        }

        internal Task<IdentityResult> ExternalLoginSignInAsync(object loginProvider, object providerKey, bool isPersistent)
        {
            throw new NotImplementedException();
        }

        internal Task<bool> IsTwoFactorClientRememberedAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        internal Task<ApplicationUser> GetTwoFactorAuthenticationUserAsync()
        {
            throw new NotImplementedException();
        }

        internal IList<AuthenticationDescription> GetExternalAuthenticationSchemes()
        {
            throw new NotImplementedException();
        }

        internal Task<ApplicationUser> GetExternalLoginInfoAsync(object v)
        {
            throw new NotImplementedException();
        }

        internal Task<IdentityResult> TwoFactorSignInAsync(string provider, string code, bool rememberMe, bool rememberBrowser)
        {
            throw new NotImplementedException();
        }

        internal AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl, object v)
        {
            throw new NotImplementedException();
        }
    }
}