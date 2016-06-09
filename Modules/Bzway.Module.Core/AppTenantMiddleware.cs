using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Bzway.Module.Core
{
   
    public class AppTenantMiddleware
    {
        private static Dictionary<string, IServiceProvider> serviceProviderCache = new Dictionary<string, IServiceProvider>();
        private static object lockObject = new object();
        private readonly RequestDelegate next;
        private readonly IServiceCollection gloabServices;

        public AppTenantMiddleware(RequestDelegate next, IServiceCollection gloabServices)
        {
            this.next = next;
            this.gloabServices = gloabServices;
        }
        public Task Invoke(HttpContext context)
        {
            //find tenant by host name
            var appTenant = context.RequestServices.GetService<IAppTenant>();
            if (appTenant == null)
            {
                throw new NotSupportedException("App Service is not registered");
            }
            var site = appTenant.FindAppTenantByHost(context.Request.Host.Host);
            //try to get ServiceProvider for this site
            context.RequestServices = TryGetTenantServiceProvider(appTenant, site);
            return next(context);
        }
  

        IServiceProvider TryGetTenantServiceProvider(IAppTenant service, UserSite site)
        {
            if (serviceProviderCache.ContainsKey(site.Name))
            {
                return serviceProviderCache[site.Name];
            }
            lock (lockObject)
            {
                if (!serviceProviderCache.ContainsKey(site.Name))
                {
                
                    var containerBuilder = new ContainerBuilder();
                    containerBuilder.Populate(gloabServices);

                    ServiceCollection services = new ServiceCollection();
                    //for testing
                    services.AddScoped<IUserSite, UserSite>((p) => { return new UserSite() { Host = new string[] { "" }, Name = Guid.NewGuid().ToString("N") }; });
              
                    containerBuilder.Populate(services);
                    //containerBuilder.RegisterModule<DefaultModule>();
                    containerBuilder.RegisterAssemblyModules();
                   
                    var container = containerBuilder.Build();
                    
                    serviceProviderCache.Add(site.Name, container.Resolve<IServiceProvider>());
                }
                return serviceProviderCache[site.Name];
            }
        }
    }
}