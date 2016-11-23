using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accentiv.Spark.Service.Wechat
{
    public class WeChatUserModel : ResponesMessage
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int total { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int count { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Openids data { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string next_openid { get; set; }
    }

    public class Openids
    {
        public List<string> openid { get; set; }
    }

    public class WeChatUserInfoModel : ResponesMessage
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int subscribe { get; set; }
        [JsonProperty]
        public string openid { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string nickname { get; set; }
        [JsonProperty]
        public int sex { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string city { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string country { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string province { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string language { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string headimgurl { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string subscribe_time { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string unionid { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string remark { get; set; }
        [JsonProperty]
        public int groupid { get; set; }
    }

}
