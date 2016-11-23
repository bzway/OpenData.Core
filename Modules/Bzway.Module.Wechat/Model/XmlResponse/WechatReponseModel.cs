﻿using Bzway.Common.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bzway.Module.Wechat.Model
{
    public class WechatReponseModel
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
        public string MsgType { get; set; }

        public override string ToString()
        {
            this.CreateTime = DateTimeHelper.GetBaseTimeValue(DateTime.Now).ToString();
            var result = SerializationHelper.SerializationToSoap(this);
            return FormartResult(result);
        }
        private string FormartResult(string result)
        {
            result = result.Replace("<?xml version=\"1.0\"?>", "");
            var count = result.Split('>')[0].Count() + 1;
            result = "<xml>" + result.Remove(0, count);
            return result;
        }

    }
}