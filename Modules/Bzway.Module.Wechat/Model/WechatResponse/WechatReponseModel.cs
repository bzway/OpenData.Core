using Accentiv.Spark.Common.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bzway.Module.Wechat..Model
{
    public class WechatReponseModel
    {
        const string WeixinText = "WeixinText";
        const string WeixinTuWen = "WeixinTuWen";
        const string WeixinTuWenList = "WeixinTuWenList";
        const string WeixinMusic = "WeixinMusic";
        const string WeixinVoice = "WeixinVoice";
        const string WeixinCSR = "WeixinCSR";
        const string ArticleItem = "ArticleItem";
        [Serializable]
        [XmlRoot(ElementName = "xml", Namespace = null)]
        public class WechatText
        {
            /// <summary>
            /// 接收方帐号（收到的OpenID）	  
            /// </summary>
            public virtual string ToUserName { get; set; }

            /// <summary>
            /// 开发者微信号	
            /// </summary>
            public virtual string FromUserName { get; set; }

            ///<summary>
            ///消息创建时间 （整型）
            /// </summary>
            public virtual string CreateTime { get; set; }

            public override string ToString()
            {
                this.CreateTime = WechatServiceHelper.ConvertDateTimeInt(DateTime.Now).ToString();
                var result = Accentiv.Spark.Common.Serialization.SoapSerialization(this);
                return FormartResult(result);
            }

            public WechatText()
            {

            }
            public WechatText(string FromUserName, string ToUserName, string Content)
            {
                this.FromUserName = ToUserName;
                this.ToUserName = FromUserName;
                this.Content = Content;
                this.MsgType = "text";
            }

            /// <summary>
            ///text	  
            /// </summary>
            public string MsgType { get; set; }

            /// <summary>
            ///回复的消息内容（换行：在content中能够换行，微信客户端就支持换行显示）	
            /// </summary>
            public string Content { get; set; }

        }
        [XmlRoot(ElementName = "xml", Namespace = null)]
        public class WechatImage
        {
            /// <summary>
            /// 接收方帐号（收到的OpenID）	  
            /// </summary>
            public virtual string ToUserName { get; set; }

            /// <summary>
            /// 开发者微信号	
            /// </summary>
            public virtual string FromUserName { get; set; }

            ///<summary>
            ///消息创建时间 （整型）
            /// </summary>
            public virtual string CreateTime { get; set; }
            public override string ToString()
            {
                this.CreateTime = WechatServiceHelper.ConvertDateTimeInt(DateTime.Now).ToString();
                var result = Accentiv.Spark.Common.Serialization.SoapSerialization(this);
                return FormartResult(result);
            }
            public WechatImage()
            {

            }
            public WechatImage(string FromUserName, string ToUserName, string MediaId)
            {
                this.FromUserName = ToUserName;
                this.ToUserName = FromUserName;
                this.MsgType = "image";
                this.Image = new ImageModel() { MediaId = MediaId };
            }

            /// <summary>
            /// image
            /// </summary>

            public string MsgType { get; set; }

            public ImageModel Image { get; set; }

            public class ImageModel
            {
                /// <summary>
                /// 通过素材管理接口上传多媒体文件，得到的id。
                /// </summary>
                public string MediaId { get; set; }
            }
        }
        [XmlRoot(ElementName = "xml", Namespace = null)]
        public class WechatVoice
        {
            /// <summary>
            /// 接收方帐号（收到的OpenID）	  
            /// </summary>
            public virtual string ToUserName { get; set; }

            /// <summary>
            /// 开发者微信号	
            /// </summary>
            public virtual string FromUserName { get; set; }

            ///<summary>
            ///消息创建时间 （整型）
            /// </summary>
            public virtual string CreateTime { get; set; }
            public override string ToString()
            {
                this.CreateTime = WechatServiceHelper.ConvertDateTimeInt(DateTime.Now).ToString();
                var result = Accentiv.Spark.Common.Serialization.SoapSerialization(this);
                return FormartResult(result);
            }
            public WechatVoice()
            {

            }
            public WechatVoice(string FromUserName, string ToUserName, string MediaId)
            {
                this.FromUserName = ToUserName;
                this.ToUserName = FromUserName;
                this.MsgType = "voice";
                this.Voice = new VoiceModel() { MediaId = MediaId };
            }

            /// <summary>
            /// image
            /// </summary>
            public string MsgType { get; set; }

            public VoiceModel Voice { get; set; }
            public class VoiceModel
            {
                /// <summary>
                /// 通过素材管理接口上传多媒体文件，得到的id。
                /// </summary>
                public string MediaId { get; set; }
            }
        }
        [XmlRoot(ElementName = "xml", Namespace = null)]
        public class WechatVideo
        {
            /// <summary>
            /// 接收方帐号（收到的OpenID）	  
            /// </summary>
            public virtual string ToUserName { get; set; }

            /// <summary>
            /// 开发者微信号	
            /// </summary>
            public virtual string FromUserName { get; set; }

            ///<summary>
            ///消息创建时间 （整型）
            /// </summary>
            public virtual string CreateTime { get; set; }
            public override string ToString()
            {
                this.CreateTime = WechatServiceHelper.ConvertDateTimeInt(DateTime.Now).ToString();
                var result = Accentiv.Spark.Common.Serialization.SoapSerialization(this);
                return FormartResult(result);
            }
            public WechatVideo()
            {

            }
            public WechatVideo(string FromUserName, string ToUserName, string MediaId, string Title, string Description)
            {
                this.FromUserName = ToUserName;
                this.ToUserName = FromUserName;
                this.MsgType = "video";
                this.Video = new WechatVideo.VideoModel() { MediaId = MediaId, Description = "", Title = "" };
            }

            /// <summary>
            /// image
            /// </summary>
            public string MsgType { get; set; }

            public VideoModel Video { get; set; }
            public class VideoModel
            {
                /// <summary>
                /// 通过素材管理接口上传多媒体文件，得到的id。
                /// </summary>
                public string MediaId { get; set; }
                public string Title { get; set; }
                public string Description { get; set; }
            }

        }
        [XmlRoot(ElementName = "xml", Namespace = null)]
        public class WechatMusic
        {
            /// <summary>
            /// 接收方帐号（收到的OpenID）	  
            /// </summary>
            public virtual string ToUserName { get; set; }

            /// <summary>
            /// 开发者微信号	
            /// </summary>
            public virtual string FromUserName { get; set; }

            ///<summary>
            ///消息创建时间 （整型）
            /// </summary>
            public virtual string CreateTime { get; set; }
            public override string ToString()
            {
                this.CreateTime = WechatServiceHelper.ConvertDateTimeInt(DateTime.Now).ToString();
                var result = Accentiv.Spark.Common.Serialization.SoapSerialization(this);
                return FormartResult(result);
            }
            public WechatMusic()
            {

            }
            public WechatMusic(string FromUserName, string ToUserName, string MediaId, string Title, string Description, string MusicUrl, string HQMusicUrl)
            {
                this.FromUserName = ToUserName;
                this.ToUserName = FromUserName;
                this.MsgType = "music";
                this.Music = new MusicModel()
                {
                    ThumbMediaId = MediaId,
                    HQMusicUrl = HQMusicUrl,
                    MusicUrl = MusicUrl,
                    Description = Description,
                    Title = Title
                };
            }
            public string MsgType { get; set; }

            public MusicModel Music { get; set; }
            public class MusicModel
            {

                public string Title { get; set; }
                public string Description { get; set; }
                public string MusicUrl { get; set; }
                public string HQMusicUrl { get; set; }
                public string ThumbMediaId { get; set; }
            }
        }
        [XmlRoot(ElementName = "xml", Namespace = null)]
        public class WechatArticle
        {
            /// <summary>
            /// 接收方帐号（收到的OpenID）	  
            /// </summary>
            public virtual string ToUserName { get; set; }

            /// <summary>
            /// 开发者微信号	
            /// </summary>
            public virtual string FromUserName { get; set; }

            ///<summary>
            ///消息创建时间 （整型）
            /// </summary>
            public virtual string CreateTime { get; set; }
            public override string ToString()
            {
                this.CreateTime = WechatServiceHelper.ConvertDateTimeInt(DateTime.Now).ToString();
                var result = Accentiv.Spark.Common.Serialization.SoapSerialization(this);
                return FormartResult(result);
            }
            public WechatArticle()
            {

            }
            public WechatArticle(string FromUserName, string ToUserName, string Title, string Description, string Url, string PicUrl)
            {
                this.FromUserName = ToUserName;
                this.ToUserName = FromUserName;
                this.MsgType = "news";
                this.Articles = new List<ArticleModel>(8);
                this.Articles.Add(new ArticleModel()
                {
                    PicUrl = PicUrl,
                    Url = Url,
                    Description = Description,
                    Title = Title
                });
            }
            public void AddArticle(string Title, string Description, string Url, string PicUrl)
            {
                this.Articles.Add(new ArticleModel()
                {
                    PicUrl = PicUrl,
                    Url = Url,
                    Description = Description,
                    Title = Title
                });
            }
            public string MsgType { get; set; }
            public int ArticleCount { get { return this.Articles.Count; } set { } }
            [XmlArrayItem(ElementName = "item")]
            public List<ArticleModel> Articles { get; set; }

            public class ArticleModel
            {
                public string Title { get; set; }
                public string Description { get; set; }
                public string PicUrl { get; set; }
                public string Url { get; set; }
            }
        }

        public static string FormartResult(string result)
        {
            result = result.Replace("<?xml version=\"1.0\"?>", "");
            var count = result.Split('>')[0].Count() + 1;
            result = "<xml>" + result.Remove(0, count);
            return result;
        }
    }


    public class CustomerMessage
    {
        public string touser { get; set; }

        public string msgtype { get; set; }

        public news news { get; set; }
    }
}