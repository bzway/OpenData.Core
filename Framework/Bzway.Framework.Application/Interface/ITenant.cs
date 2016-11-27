using Bzway.Data.Core;
using Bzway.Framework.Application.Entity;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Bzway.Framework.Application
{
    public interface ITenant
    {
        Site GetSite();
        IDatabase GetDatabase();

        HttpContext GetContext();
    }
}
