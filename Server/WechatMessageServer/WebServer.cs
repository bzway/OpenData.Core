using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Bzway.Wechat.MessageServer
{
    public class WebServer
    {
        List<Func<WechatContext, string>> processorList;
        public WebServer()
        {
            this.processorList = new List<Func<WechatContext, string>>();
        }
        public WebServer Use(Func<WechatContext, string> process)
        {
            this.processorList.Add(process);
            return this;
        }

        public void Run()
        {
            Startup.ProcessorList = this.processorList;
            while (true)
            {
                try
                {
                    var host = new WebHostBuilder()
                                .UseKestrel((options) => { options.AddServerHeader = false; })
                                .UseContentRoot(Directory.GetCurrentDirectory())
                                .UseStartup<Startup>()
                                .Build();
                    host.Run();
                }
                catch { }
            }
        }

        class Startup
        {
            public static List<Func<WechatContext, string>> ProcessorList;
            public Startup(IHostingEnvironment env)
            {
            }


            public void ConfigureServices(IServiceCollection services)
            {
            }

            public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
            {
                app.UseMiddleware<MessageProcessorMiddleware>(ProcessorList);
                app.Run(async context =>
                {
                    await context.Response.WriteAsync("");
                });
            }
        }
        class MessageProcessorMiddleware
        {

            private readonly RequestDelegate next;
            private readonly List<Func<WechatContext, string>> messageProcessor;
            public MessageProcessorMiddleware(RequestDelegate next, List<Func<WechatContext, string>> messageProcessor)
            {
                this.next = next;
                this.messageProcessor = messageProcessor;
            }
            public async Task Invoke(HttpContext context)
            {
                var result = this.Process(context);
                if (result.Wait(4900))
                {
                    await context.Response.WriteAsync(result.Result);
                }
                await next(context);
            }
            Task<string> Process(HttpContext context)
            {
                WechatContext ctx = new WechatContext(context);
                Task<string> task = new Task<string>(() =>
                {
                    foreach (var item in this.messageProcessor)
                    {
                        var result = item(ctx);
                        if (!string.IsNullOrEmpty(result))
                        {
                            return result;
                        }
                    }
                    return string.Empty;
                });
                task.Start();
                return task;
            }
        }
    }
}
