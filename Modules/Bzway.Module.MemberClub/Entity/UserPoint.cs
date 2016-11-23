﻿using Bzway.Data.Core;
using Bzway.Framework.Connect.Entity;
using System;
namespace Bzway.Module.MemberClub.Entity
{

    public class UserPoint : EntityBase
    {
        public string UserID { get; set; }
        public PointType Type { get; set; }
        public int Amount { get; set; }
        public string ExtensionID { get; set; }
        public string Source { get; set; }
        public DateTime ExpiringOn { get; set; }
        public string Remark { get; set; }
    }
  
}