using Bzway.Common.Utility;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Bzway.Module.Wechat.Model
{
    [Serializable]
    [XmlRoot(ElementName = "xml", Namespace = null)]
    public class WechatEventMessage : WechatMessageBase
    {
        public string Event { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "xml", Namespace = null)]
    public class WechatEventSubscribe : WechatEventMessage
    {
    }

    [Serializable]
    [XmlRoot(ElementName = "xml", Namespace = null)]
    public class WechatEventUnsubscribe : WechatEventMessage
    {
    }


    [Serializable]
    [XmlRoot(ElementName = "xml", Namespace = null)]
    public class WechatEventScanSubscribe : WechatEventMessage
    {
        public string EventKey { get; set; }
        public string Ticket { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "xml", Namespace = null)]
    public class WechatEventScan : WechatEventMessage
    {
        public string EventKey { get; set; }
        public string Ticket { get; set; }
    }


    [XmlRoot(ElementName = "xml", Namespace = null)]
    public class WechatEventLocation : WechatEventMessage
    { 
        public long Latitude { get; set; }
        public long Longitude { get; set; }
        public long Precision { get; set; }
    }


    [XmlRoot(ElementName = "xml", Namespace = null)]
    public class WechatEventClick : WechatEventMessage
    {
        public string EventKey { get; set; }
    }
}