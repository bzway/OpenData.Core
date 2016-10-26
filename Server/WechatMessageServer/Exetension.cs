using System;
using System.Threading;

namespace Bzway.Wechat.MessageServer
{
    public static class Exetension
    {
        public static WebServer UseKeyWord(this WebServer app)
        {
            app.Use(ProcessKeyWord);
            return app;
        }

        public static WebServer UseMvc(this WebServer app)
        {
            app.Use(ProcessKeyMvc);
            return app;
        }
        static string ProcessKeyWord(WechatContext context)
        {
            Console.WriteLine("Keyword");
            return "Keyword";
        }
        static string ProcessKeyMvc(WechatContext context)
        {
            return "Mvc";
        }
    }
}