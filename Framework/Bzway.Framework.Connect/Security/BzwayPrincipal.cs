using System.Security.Principal;
using System.Linq;
using System.Collections.Generic;
using Bzway.Common.Utility;
using System.Security.Claims;

namespace Bzway.Framework.Connect
{

    public class BzwayPrincipal : ClaimsPrincipal
    {
        private static readonly string SessionKey = "SessionKey";
        private static readonly string PasswordKey = "passwordKey";
        private readonly IDictionary<string, string> cookies;
        private UserIdentity identity;

        public BzwayPrincipal(IDictionary<string, string> cookies)
        {
            this.cookies = cookies;
        }
        public override IIdentity Identity
        {
            get
            {
                if (this.identity == null && cookies.ContainsKey(SessionKey))
                {
                    var data = cookies[SessionKey];
                    data = Cryptor.DecryptAES(data, PasswordKey, PasswordKey);
                    this.identity = SerializationHelper.DeserializeObjectJson<UserIdentity>(data);
                }
                return this.identity;
            }
        }
        public override string ToString()
        {
            return this.identity.Name;
        }
    }
}