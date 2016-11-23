using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Bzway.Module.Wechat..Model
{

    public class WebServiceResponse
    {
        public string resultCode { get; set; }

        public string resultMsg { get; set; }

        public string state { get; set; }

        public object result { get; set; }
        public WebServiceResponse()
        {

        }
        public WebServiceResponse(string resultCode, string resultMsg, string state, object result = null)
        {
            this.resultCode = resultCode;
            this.result = result;
            this.resultMsg = resultMsg;
            this.state = state;
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}