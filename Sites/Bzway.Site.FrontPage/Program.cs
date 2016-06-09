using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;

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
