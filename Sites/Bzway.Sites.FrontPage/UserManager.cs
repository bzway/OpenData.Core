using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Bzway.Site.FrontPage.Models.ManageViewModels;

namespace Bzway.Site.FrontPage.Controllers
{
    internal class UserManager
    {
        internal Task<IdentityResult> CreateAsync(ApplicationUser user, string password = "")
        {
            throw new NotImplementedException();
        }

        internal Task<IdentityResult> AddLoginAsync(ApplicationUser user, object info)
        {
            throw new NotImplementedException();
        }

        internal Task<ApplicationUser> GetUserAsync(ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }

        internal Task<bool> HasPasswordAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        internal Task<string> GetPhoneNumberAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        internal Task<bool> GetTwoFactorEnabledAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        internal Task<IList<UserLoginInfo>> GetLoginsAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        internal Task<IdentityResult> RemoveLoginAsync(ApplicationUser user, string loginProvider, string providerKey)
        {
            throw new NotImplementedException();
        }

        internal Task<string> GenerateChangePhoneNumberTokenAsync(ApplicationUser user, string phoneNumber)
        {
            throw new NotImplementedException();
        }

        internal Task<IdentityResult> SetTwoFactorEnabledAsync(ApplicationUser user, bool v)
        {
            throw new NotImplementedException();
        }

        internal Task<IdentityResult> ConfirmEmailAsync(object user, string code)
        {
            throw new NotImplementedException();
        }

        internal Task<IdentityResult> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        internal Task<IdentityResult> FindByNameAsync(string email)
        {
            throw new NotImplementedException();
        }

        internal Task<bool> IsEmailConfirmedAsync(IdentityResult user)
        {
            throw new NotImplementedException();
        }

        internal Task<List<string>> GetValidTwoFactorProvidersAsync(object user)
        {
            throw new NotImplementedException();
        }

        internal Task<string> GenerateTwoFactorTokenAsync(ApplicationUser user, string selectedProvider)
        {
            throw new NotImplementedException();
        }

        internal Task<string> GetEmailAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        internal Task<IdentityResult> ResetPasswordAsync(IdentityResult user, string code, string password)
        {
            throw new NotImplementedException();
        }

        internal Task<IdentityResult> SetPhoneNumberAsync(ApplicationUser user, object p)
        {
            throw new NotImplementedException();
        }

        internal Task<string> GetUserIdAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        internal Task<IdentityResult> ChangePhoneNumberAsync(ApplicationUser user, string phoneNumber, string code)
        {
            throw new NotImplementedException();
        }

        internal Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        internal Task<IdentityResult> AddPasswordAsync(ApplicationUser user, string newPassword)
        {
            throw new NotImplementedException();
        }

        internal object GetUserId(ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }
    }
}