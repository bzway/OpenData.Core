﻿using Bzway.Data;
using System;

namespace Bzway.Framework.Model
{

    public class ViewSiteWechatFan : IViewBase
    {
        public string WechatID { get; set; }
        public string UserID { get; set; }
        public string OpenID { get; set; }
        public string UnionID { get; set; }

        public bool IsSubscribed { get; set; }

        public string NickName { get; set; }
        public GenderType Gender { get; set; }
        public string Country { get; set; }
        public string Proinvce { get; set; }
        public string City { get; set; }
        public string Language { get; set; }

        public string HeadImageUrl { get; set; }

        public DateTime SubscribeTime { get; set; }

        public string Remark { get; set; }

        public string Group { get; set; }
    }
}
