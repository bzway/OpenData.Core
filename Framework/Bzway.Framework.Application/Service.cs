using Bzway.Data.Core;
using Bzway.Framework.Application.Entity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Bzway.Framework.Application
{
    public abstract class BaseService<T>
    {
        #region ctor
        protected readonly ILogger<T> logger;
        protected readonly Site site;
        protected readonly IDatabase db;
        public BaseService(ILoggerFactory loggerFactory, Site site)
        {
            this.logger = loggerFactory.CreateLogger<T>();
            this.db = OpenDatabase.GetDatabase(site.ProviderName, site.ConnectionString, site.DatabaseName);
        }
        #endregion
    }
}