using System;

namespace Bzway.Module.Core
{
    public interface IAppTenant
    {
        IAppTenant Default { get; set; }

        UserSite FindAppTenantByHost(string host);
    }

    public class AppTenantService : IAppTenant
    {


        public IAppTenant Default
        {
            get; set;
        }



        public UserSite FindAppTenantByHost(string host)
        {
            return new UserSite();
        }
    }
}