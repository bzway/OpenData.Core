using Bzway.Common.Utility;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Bzway.Module.Wechat.Model
{
    public class WechatEventMessage
    {
        string xml { get; set; }
        public WechatEventMessage(string xml)
        {
            this.xml = xml;
        }
        public string ToUserName { get; set; }
        public string FromUserName { get; set; }
        public string MsgType { get; set; }

        [XmlIgnore]
        public DateTime CreateTime { get; set; }
        [XmlElement("CreateTime")]
        public long CreateTimeWechat
        {
            get
            {
                return DateTimeHelper.GetBaseTimeValue(this.CreateTime);
            }
            set
            {
                this.CreateTime = DateTimeHelper.ConvertToBaseTime(value);
            }
        }
        public virtual T Prase<T>()
        {
            return SerializationHelper.DeserializeObjectFromXml<T>(this.xml);
        }

    }
    [XmlRoot("xml")]
    public class TextMessage : WechatEventMessage
    {
        public string Content { get; set; }
        public TextMessage()
        {
            MsgType = "text";
        }
    }
    public enum WechatMessageType
    {

        UnSubscribe = 0,
        Subscribe = 1,
        Click = 2,
        View = 3,
        Scan = 4,

        [Description("text")]
        Text = 0,
        [Description("WeixinImage")]
        Image = 1,
        [Description("WeixinMusic")]
        Music,
        [Description("WeixinVideo")]
        Video,
        [Description("WeixinVoice")]
        Voice,
        [Description("WeixinCSR")]
        CSR,
        [Description("WeixinArticles")]
        Article,
    }
}