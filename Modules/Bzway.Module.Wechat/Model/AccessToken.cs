using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Bzway.Module.Wechat..Model
{
    public class AccessToken
    {
        public string AccessTokenString { get; set; }
        public DateTime ExpiredDateTime { get; set; }
    }

}