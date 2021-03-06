﻿using Bzway.Data.Core;

namespace Bzway.Framework.Core.Entity
{

    public class WechatUserGroup : BaseEntity
    {
        /// <summary>
        /// 所属公众号
        /// </summary>
        public string OfficialAccount { get; set; }

        /// <summary>
        ///微信中分组编号
        /// </summary>
        public string GroupId { get; set; }
        /// <summary>
        ///用户昵称
        /// </summary>
        public string Name { get; set; }


    }
}