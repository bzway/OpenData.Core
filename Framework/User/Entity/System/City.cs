﻿using OpenData.Data.Core;

namespace OpenData.Framework.Core.Entity
{

    public class City : BaseEntity
    {
        public string ProinceID { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
    }
}