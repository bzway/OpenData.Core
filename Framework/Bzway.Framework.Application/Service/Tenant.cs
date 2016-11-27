using Bzway.Data.Core;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Bzway.Framework.Application.Entity;
using Microsoft.AspNetCore.Http;

namespace Bzway.Framework.Application
{
    /// <summary>
    /// GrantRequest service
    /// </summary>
    public partial class Tenant : ITenant
    {
        #region ctor
        private readonly HttpContext context;
        public Tenant(HttpContext context)
        {
            this.context = context;
        }

        #endregion

        public HttpContext GetContext()
        {
            return context;
        }

        public IDatabase GetDatabase()
        {
            return OpenDatabase.GetDatabase("", "", "");
        }

        public Site GetSite()
        {
            throw new NotImplementedException();
        }
    }
}