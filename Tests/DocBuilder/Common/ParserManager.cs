using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Bzway.Common.Share
{
    public static class ParserManager
    {
        static Dictionary<string, Func<string, INewsParser>> list = new Dictionary<string, Func<string, INewsParser>>();
        static ParserManager()
        {
            list.Add("www.ftchinese.com", m => { return new FTNewsParser(m); });

            list.Add("mp.weixin.qq.com", m => { return new WechatNewsParser(m); });
        }
        public static INewsParser Get(string url)
        {
            Uri uri = new Uri(url);
            return list[uri.Host](url);
        }
    }
}