﻿using Bzway.Common.Utility;
using Bzway.Data.Core;
using Bzway.Framework.Connect.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Bzway.Framework.Connect
{
    public enum LoginStatus
    {
        // Summary:
        //     Sign in was successful
        Success = 0,
        //
        // Summary:
        //     User is locked out
        LockedOut = 1,
        //
        // Summary:
        //     Sign in requires addition verification (i.e. two factor)
        RequiresVerification = 2,
        //
        // Summary:
        //     Sign in failed
        Failure = 3,
        RegisterByEmail,
        EmailNoComfirmed,
        RegisterByPhoneNumber,
        PhoneNumberNoComfirmed,
        ProfileNoComfirmed,

    }
    public class UserManager
    {
        #region ctor
        static readonly string SessionKey = "Bzway.Session";
        static readonly string PasswordKey = "BzwayIdentity";
        static readonly DateTime BaseDate = new DateTime(2010, 4, 22);


        private readonly ILogger<UserManager> logger;
        private readonly IDatabase db;
        private readonly IUserService userService;
        private readonly HttpContext context;

        public UserManager(ILoggerFactory loggerFactory, IUserService userService, HttpContext context)
        {
            this.logger = loggerFactory.CreateLogger<UserManager>();
            this.db = OpenDatabase.GetDatabase();
            this.userService = userService;
            this.context = context;
        }
        #endregion
        #region PasswordSignIn
        public Task<LoginStatus> PasswordSignInAsync(string loginName, string password, bool rememberMe = true)
        {
            Task<LoginStatus> task = new Task<LoginStatus>(() => { return this.PasswordSignIn(loginName, password); });
            task.Start();
            return task;
        }

        public LoginStatus PasswordSignIn(string loginName, string password, bool rememberMe = true)
        {
            if (RegexHelper.IsEmail(loginName))
            {
                var userEmail = userService.VerifyEmail(loginName);
                if (userEmail == null)
                {
                    userEmail = userService.RegisterByEmail(loginName, password);
                    SendEDMValidationCodeAsync(userEmail);
                    return LoginStatus.RegisterByEmail;
                }
                var user = userService.FindUserByID(userEmail.UserID);
                if (user == null)
                {
                    return LoginStatus.Failure;
                }
                if (!string.Equals(Cryptor.EncryptMD5(password), user.Password))
                {
                    return LoginStatus.Failure;
                }
                if (!userEmail.IsConfirmed)
                {
                    this.SetAuthCookies(user, LockType.Email, rememberMe);
                    return LoginStatus.EmailNoComfirmed;
                }
                this.SetAuthCookies(user, LockType.None, rememberMe);
                return LoginStatus.Success;
            }
            if (RegexHelper.IsMobileNumber(loginName))
            {
                var phoneNumber = loginName;
                var userPhone = userService.VerifyPhone(phoneNumber);
                if (userPhone == null)
                {
                    userPhone = userService.RegisterByPhoneNumber(phoneNumber, password);
                    SendSMSValidationCodeAsync(userPhone);
                    return LoginStatus.RegisterByPhoneNumber;
                }
                var user = userService.FindUserByID(userPhone.ValidateCode);
                if (user == null)
                {
                    return LoginStatus.Failure;
                }
                if (!string.Equals(Cryptor.EncryptMD5(password), user.Password))
                {
                    return LoginStatus.Failure;
                }
                if (!userPhone.IsConfirmed)
                {
                    SetAuthCookies(user, LockType.MobilePhone, rememberMe);
                    return LoginStatus.PhoneNumberNoComfirmed;
                }
                SetAuthCookies(user, LockType.None, rememberMe);
                return LoginStatus.Success;
            }
            var userName = loginName;
            var verifiedUser = userService.VerifyUser(userName, password);
            if (verifiedUser == null)
            {
                return LoginStatus.Failure;
            }
            if (!verifiedUser.IsConfirmed)
            {
                return LoginStatus.ProfileNoComfirmed;
            }
            if (verifiedUser.IsLocked)
            {
                return LoginStatus.LockedOut;
            }
            SetAuthCookies(verifiedUser, LockType.Information, rememberMe);
            return LoginStatus.Success;
        }
        #endregion
        public void RemoveAuthCookies()
        {
            //myCookie.Expires = DateTime.Now.AddDays(-1d);
            //httpContext.Response.Cookies.Add(myCookie);
            //var httpContext = HttpContext.Current;
            //HttpCookie myCookie = httpContext.Request.Cookies["OpenID"];
            //if (myCookie != null)
            //{
            //    myCookie.Expires = DateTime.Now.AddDays(-1d);
            //    httpContext.Response.Cookies.Add(myCookie);
            //}

            //httpContext.Response.Cookies.Clear();
            //httpContext.Request.Cookies.Clear();
            //this.context.Server.Abandon();
            this.context.Response.Cookies.Delete(SessionKey, new CookieOptions() { Expires = DateTime.UtcNow.AddDays(-1) });
            //this.context.Response.Cookies.Delete(SessionKey);

        }
        private void SetAuthCookies(User user, LockType lockType, bool rememberMe)
        {
            var expringTime = DateTime.UtcNow.AddMonths(rememberMe ? 1 : 0);
            var userIdentity = new UserIdentity()
            {
                ID = user.Id,
                Name = user.NickName,
                Roles = user.Roles,
                Locked = lockType,
                In = (expringTime - BaseDate).Minutes,
            };

            var token = Cryptor.EncryptAES(SerializationHelper.SerializeObjectToJson(userIdentity), PasswordKey, PasswordKey);
            userIdentity.Token = token;
            //this.context.Set<UserIdentity>(SessionKey, userIdentity);
            this.context.Response.Cookies.Append(SessionKey, token, new CookieOptions { Expires = expringTime });
        }

        public Task<bool> SendEDMValidationCodeAsync(UserEmail email, string returnUrl = "")
        {

            Task<bool> task = new Task<bool>(() => { return this.SendEDMValidationCode(email, returnUrl); });
            task.Start();
            return task;
        }
        public bool SendEDMValidationCode(UserEmail email, string returnUrl = "")
        {
            if (email == null)
            {
                var userIdentiy = this.GetCurrentUser();
                if (userIdentiy == null)
                {
                    return false;
                }
                email = this.userService.FindUserEmailsByUserID(userIdentiy.Identity.Name).FirstOrDefault();
            }
            if (email == null)
            {
                return false;
            }
            string from = "上海比之为网络科技有限公司".Localize("phone");
            string to = email.Email;
            string subject = "Email Confirm".Localize("phone");
            string body = string.Format(@"亲爱的 {0},

您在{1}的账号已创建，你即将可以通过{0}登录{1}。

请点击后面的链接完成确认: {1}/account/validation?code={2}&returnUrl={3}

谢谢您的配合!
{4}
", email.Email, context.Request.Host, email.ValidateCode, System.Net.WebUtility.UrlEncode(returnUrl), from);
            //smtpService.SendMail(from, to, subject, body);
            return true;
        }


        public Task<bool> SendSMSValidationCodeAsync(UserPhone phone, string returnUrl = "")
        {
            Task<bool> task = new Task<bool>(() => { return this.SendSMSValidationCode(phone, returnUrl); });
            task.Start();
            return task;
        }
        public bool SendSMSValidationCode(UserPhone phone, string returnUrl = "")
        {
            if (phone == null)
            {
                var userIdentiy = this.GetCurrentUser();
                if (userIdentiy == null)
                {
                    return false;
                }
                phone = this.userService.FindUserPhonesByUserID(userIdentiy.Identity.Name).FirstOrDefault();
            }
            if (phone == null)
            {
                return false;
            }
            string from = "上海比之为网络科技有限公司".Localize("phone");
            string to = phone.PhoneNumber;
            string subject = "Email Confirm".Localize("phone");
            string body = string.Format(@"亲爱的 {0},

您在{1}的账号已创建，你即将可以通过{0}登录{1}。

请点击后面的链接完成确认: {1}/account/validation?code={2}&returnUrl={3}

谢谢您的配合!
{4}
", phone.PhoneNumber, context.Request.Host, phone.ValidateCode, System.Net.WebUtility.UrlEncode(returnUrl), from);
            //smtpService.SendMail(from, to, subject, body);
            return true;
        }

        public ClaimsPrincipal GetCurrentUser()
        {
            try
            {
                var userIdentity = this.context.User;
                if (userIdentity != null)
                {
                    return userIdentity;
                }
                var data = this.context.Request.Cookies[SessionKey];
                if (string.IsNullOrEmpty(data))
                {
                    return null;
                }
                data = Cryptor.DecryptAES(data, PasswordKey, PasswordKey);
                userIdentity = SerializationHelper.DeserializeObjectJson<BzwayPrincipal>(data);
                if (userIdentity == null)
                {
                    return null;
                }
                this.context.User = userIdentity;
                return userIdentity;
            }
            catch (Exception ex)
            {
                logger.LogError("GetCurrentUser", ex);
                return null;
            }
        }

        public Task<LoginStatus> ValidateCodeAsync(string provider, string code, bool rememberMe)
        {
            Task<LoginStatus> task = new Task<LoginStatus>(() => { return this.ValidateCode(provider, code, rememberMe); });
            task.Start();
            return task;
        }

        public LoginStatus ValidateCode(string provider, string code, bool rememberMe)
        {
            if (string.Equals("email", provider, StringComparison.CurrentCultureIgnoreCase))
            {
                var userEmail = this.userService.FindUserEmailByValidationCode(code);
                if (userEmail == null)
                {
                    return LoginStatus.Failure;
                }

                var user = userService.FindUserByID(userEmail.UserID);
                if (user == null)
                {
                    return LoginStatus.Failure;
                }

                userEmail.IsConfirmed = true;
                //userEmail.ValidateCode = string.Empty;
                //userEmail.ValidateTime = DateTime.UtcNow;
                this.userService.ConfirmUserEmail(userEmail);
                SetAuthCookies(user, LockType.None, rememberMe);
                return LoginStatus.Success;
            }
            if (string.Equals("phone", provider, StringComparison.CurrentCultureIgnoreCase))
            {
                var userPhone = this.userService.FindUserPhoneByValidationCode(code);
                if (userPhone == null)
                {
                    return LoginStatus.Failure;
                }

                var user = userService.FindUserByID(userPhone.UserID);
                if (user == null)
                {
                    return LoginStatus.Failure;
                }

                userPhone.IsConfirmed = true;
                this.userService.ConfirmUserPhone(userPhone);
                SetAuthCookies(user, LockType.None, rememberMe);
                return LoginStatus.Success;
            }
            return LoginStatus.Failure;

        }

    }
}