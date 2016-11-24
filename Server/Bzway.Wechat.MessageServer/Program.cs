using Bzway.Common.Collections;
using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.IO;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Bzway.Wechat.MessageServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebServer server = new WebServer();
            server.UseBaseProcess();
            server.UseMvc();
            //server.UseKeyWord();
            server.Run();
        }
    }
}