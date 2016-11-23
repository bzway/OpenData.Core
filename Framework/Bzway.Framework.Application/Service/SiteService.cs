using Bzway.Data.Core;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Bzway.Framework.Application.Entity;

namespace Bzway.Framework.Application
{
    /// <summary>
    /// GrantRequest service
    /// </summary>
    public partial class SiteService : ISiteService
    {
        #region ctor
        private readonly ILogger<SiteService> logger;
        private readonly IDatabase db;
        public SiteService(ILoggerFactory loggerFactory)
        {
            this.db = OpenDatabase.GetDatabase();
            this.logger = loggerFactory.CreateLogger<SiteService>();
        }
        #endregion

        public Site FindSiteByDomain(string domain)
        {
            return db.Entity<Site>().Query().Where(m => m.Domains, domain, CompareType.Contains).First();
        }
        public IEnumerable<Site> FindSiteByUserID(string userID)
        {
            List<Site> list = new List<Site>();
            foreach (var item in db.Entity<UserSite>().Query().Where(m => m.UserID, userID, CompareType.Equal).ToList())
            {
                list.AddRange(db.Entity<Site>().Query().Where(m => m.Id, item.SiteID, CompareType.Equal).ToList());
            }
            return list;
        }

        public Site FindSiteByID(string id)
        {
            return this.db.Entity<Site>().Query().Where(m => m.Id, id, CompareType.Equal).First();
        }
        public Site FindSiteByName(string siteName)
        {
            return this.db.Entity<Site>().Query().Where(m => m.Name, siteName, CompareType.Equal).First();
        }
        public void CreateOrUpdateSite(Site site, string userID)
        {
            if (!string.IsNullOrEmpty(site.Id))
            {
                this.db.Entity<Site>().Update(site);
                return;
            }
            site.Id = Guid.NewGuid().ToString("N");
            this.db.Entity<Site>().Insert(site);
            this.db.Entity<UserSite>().Insert(new UserSite() { UserID = userID, SiteID = site.Id });
        }

        public void DeleteSiteByID(string siteID)
        {
            this.db.Entity<Site>().Delete(siteID);
            var userSite = this.db.Entity<UserSite>().Query()
                .Where(m => m.SiteID, siteID, CompareType.Equal)
                .First();
            this.db.Entity<UserSite>().Delete(userSite);
        }
    }
}