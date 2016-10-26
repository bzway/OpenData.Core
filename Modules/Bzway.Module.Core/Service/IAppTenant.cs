using Bzway.Common.Share;
using Bzway.Data.Core;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Bzway.Module.Core
{
    public interface IAppTenant
    {
        IAppTenant Default { get; set; }

        UserSite FindAppTenantByHost(string host);
    }

    public class AppTenantService : IAppTenant
    {
        private readonly ICache cache;

        public AppTenantService(ICache cache)
        {
            this.cache = cache;
        }

        public IAppTenant Default
        {
            get; set;
        }



        public UserSite FindAppTenantByHost(string host)
        {
            var list = cache.Get<List<UserSite>>("usersitelist", () =>
             {
                 return OpenDatabase.GetDatabase().Entity<UserSite>().Query().ToList().ToList();
             });
            return list.FirstOrDefault(m => m.Host.Contains(host));
        }
    }
}