using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Security.Cryptography;
using System.Linq;
using System.Xml;

namespace Bzway.Wechat.MessageServer
{
    public class WechatContext
    {
        public WechatContext(HttpContext context)
        {
            this.context = context;
        }

        HttpContext context;
        XmlDocument doc;

        public string IsOk
        {
            get
            {
                string id = this.context.Request.Path.Value;
                string signature = this.context.Request.Query[""];
                string echostr = this.context.Request.Query[""];
                string timestamp = this.context.Request.Query[""];
                string nonce = this.context.Request.Query[""];
                if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(signature) || string.IsNullOrEmpty(timestamp) || string.IsNullOrEmpty(nonce))
                {
                    return "error";
                }
                if (!CheckSignature(signature, timestamp, nonce))
                {
                    return "error";
                }
                if (string.Equals(this.context.Request.Method, "get", StringComparison.CurrentCultureIgnoreCase))
                {
                    return echostr;
                }
                doc.Load(this.context.Request.Body);
                return string.Empty;
            }
        }
        bool CheckSignature(string signature, string timestamp, string nonce)
        {
            var token = string.Empty;
            var arr = new[] { token, timestamp, nonce }.OrderBy(z => z).ToArray();
            var arrString = string.Join("", arr);

            var sha1 = SHA1.Create();
            var sha1Arr = sha1.ComputeHash(Encoding.UTF8.GetBytes(arrString));
            StringBuilder enText = new StringBuilder();
            foreach (var b in sha1Arr)
            {
                enText.AppendFormat("{0:x2}", b);
            }
            var test = enText.ToString();
            return signature.Equals(enText.ToString());
        }
        public string xml { get; set; }
        public string openId { get; set; }
        public string officeId { get; set; }
    }
}