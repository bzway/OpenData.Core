using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http.Features;

namespace Bzway.Site.FrontPage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var host = new WebHostBuilder()
                         .UseKestrel()
                         .UseContentRoot(Directory.GetCurrentDirectory())
                         .UseIISIntegration()
                         .UseStartup<Startup>()
                         .Build();
                host.Run();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
