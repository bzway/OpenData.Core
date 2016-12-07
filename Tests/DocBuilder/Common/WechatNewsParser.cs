using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Bzway.Common.Share
{
    public class WechatNewsParser : INewsParser
    {
        private readonly static HtmlWeb webclient = new HtmlWeb();
        private readonly string url;
        private readonly HtmlDocument doc;
        public WechatNewsParser(string url)
        {
            this.url = url;
            this.doc = webclient.Load(url);
        }


        public string Author
        {
            get
            {
                return "金融时报";
            }
        }

        public string Content
        {
            get
            {
                var titleNode = doc.DocumentNode.SelectNodes("/html/head/title").FirstOrDefault();
                if (titleNode == null)
                {
                    return "";
                }
                return titleNode.InnerText;
            }
        }

        public string Digest
        {
            get
            {
                var descriptionNode = doc.DocumentNode.SelectNodes("/html/head/meta").Where(m => m.Attributes["name"].Value == "description").FirstOrDefault();
                if (descriptionNode == null)
                {
                    return "";
                }
                return descriptionNode.Attributes["content"].Value;
            }
        }

        public string Id
        {
            get
            {
                using (MD5 md5 = MD5.Create())
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(this.url);
                    return BitConverter.ToString(md5.ComputeHash(bytes));
                }
            }
        }

        public string ThumbMedia
        {
            get
            {
                var node = doc.DocumentNode.SelectNodes("/html/body/div[7]/div/div[1]/div/div[1]/div[3]/figure").FirstOrDefault();
                if (node == null)
                {
                    return "";
                }
                return node.GetAttributeValue("data-url", "");
            }
        }

        public string Title
        {
            get
            {

                var node = doc.DocumentNode.SelectNodes("/html/body/div[7]/div/div[1]/div/div[1]/h1").FirstOrDefault();
                if (node == null)
                {
                    return "";
                }
                return node.InnerText;
            }
        }

        public string Url
        {
            get
            {
                return this.url;
            }
        }
    }
}