using Autofac.Extensions.DependencyInjection;
using Bzway.Common.Share;
using Bzway.Common.Utility;
using Bzway.Data.Core;
using Bzway.Data.JsonFile;
using Bzway.Framework.Application;
using Bzway.Framework.Connect.Authentication;
using Bzway.Module.Wechat.Interface;
using Bzway.Module.Wechat.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Bzway.Site.BackOffice
{
    public class Startup
    {
        IServiceCollection services;
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();

                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddMvc();

            // Add application services.
            services.AddTransient<IWechatApiService, WechatApiService>();
            services.AddTransient<IWechatService, WechatService>();
            services.AddTransient<ICacheManager, DefaultCache>();

            this.services = services;
            AppEngine.Current.Register<IDatabase, FileDatabase>("Default");
            return AppEngine.Current.Build(services);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseApplicationInsightsExceptionTelemetry();
            //app.UseCookieAuthentication(new CookieAuthenticationOptions() { });
            app.UseStaticFiles();
            //app.UseIdentity();
            app.UseMiddleware<TenantMiddleware>(services);
            app.UseBzwayCookieAuthentication();
            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715
            app.UseMvcWithDefaultRoute();
        }
    }
}
