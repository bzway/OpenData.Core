using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Bzway.Framework.Application.Entity;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Bzway.Common.Utility;
using System.Threading;

namespace Bzway.Framework.Application
{

    public class TenantMiddleware
    {
        private static Dictionary<string, IServiceProvider> serviceProviderCache = new Dictionary<string, IServiceProvider>();
        private static object lockObject = new object();
        private readonly RequestDelegate next;
        private readonly IServiceCollection gloabServices;

        public TenantMiddleware(RequestDelegate next, IServiceCollection gloabServices)
        {
            this.next = next;
            this.gloabServices = gloabServices;
        }

        public Task Invoke(HttpContext context)
        {
            //try to get ServiceProvider for this site
            context.RequestServices = TryGetTenantServiceProvider(context);
            return next(context);
        }


        IServiceProvider TryGetTenantServiceProvider(HttpContext context)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(gloabServices);
            ServiceCollection services = new ServiceCollection();
            services.AddTransient<ITenant>(m =>
            {
                return new Tenant(context);
            });
            containerBuilder.Populate(services);
            containerBuilder.RegisterAssemblyModules();
            var container = containerBuilder.Build();
            return container.Resolve<IServiceProvider>();
        }
    }
}