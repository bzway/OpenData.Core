using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
namespace Accentiv.Spark.Service.Wechat
{
    public class WeChatModelOne
    {
        [JsonProperty]
        public string access_token { get; set; }
        [JsonProperty]
        public int expires_in { get; set; }
        [JsonProperty]
        public string refresh_token { get; set; }
        [JsonProperty]
        public string openid { get; set; }
        [JsonProperty]
        public string scope { get; set; }
    }

    public class GetAccessTokenModel
    {
        [JsonProperty]
        public string access_token { get; set; }
        [JsonProperty]
        public int expires_in { get; set; }

    }

    public class GetJsapiTicketModel : ResponesMessage
    {
        [JsonProperty]
        public string ticket { get; set; }
        [JsonProperty]
        public string expires_in { get; set; }
    }

    public class ResponesMessage
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string errcode { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string errmsg { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string msgid { get; set; }
    }

    public class WeChatSDKModel
    {
        public string appId { get; set; }
        public int timeStamp { get; set; }
        public string nonceStr { get; set; }
        public string signature { get; set; }
    }

    public class AccessCodeModel
    {
        [JsonProperty(PropertyName = "access_token")]
        public string accessToken { get; set; }
        [JsonProperty(PropertyName = "expires_in")]
        public string expiresIn { get; set; }
        [JsonProperty(PropertyName = "refresh_token")]
        public string refreshToken { get; set; }
        public string openId { get; set; }
        public string scope { get; set; }
        public string unionId { get; set; }
    }
    public class UserInfoModel
    {
        public string openId { get; set; }
        public string nickName { get; set; }
        public string sex { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string headImgUrl { get; set; }
        public string[] privilege { get; set; }
        public string unionId { get; set; }

    }
    public class RefershAccessTokenModel
    {
        [JsonProperty(PropertyName = "access_token")]
        public string accessToken { get; set; }
        [JsonProperty(PropertyName = "expires_in")]
        public string expiresIn { get; set; }
        [JsonProperty(PropertyName = "refresh_token")]
        public string refreshToken { get; set; }
        public string openID { get; set; }
        public string scope { get; set; }
    }

}
