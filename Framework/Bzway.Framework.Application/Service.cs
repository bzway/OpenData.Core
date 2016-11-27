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
        protected readonly ITenant tenant;
        protected readonly IDatabase db;
        public BaseService(ILoggerFactory loggerFactory, ITenant tenant)
        {
            this.logger = loggerFactory.CreateLogger<T>();
            this.tenant = tenant;
            this.db = this.tenant.GetDatabase();
        }
        #endregion
    }
}