using System.Collections.Generic;
using Microsoft.AspNetCore.Http.Authentication;

namespace Bzway.Site.FrontPage.Models.ManageViewModels
{
    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }

        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }
}
