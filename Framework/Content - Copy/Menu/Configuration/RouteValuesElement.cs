﻿#region License
// 
// Copyright (c) 2013, Bzway team
// 
// Licensed under the BSD License
// See the file LICENSE.txt for details.
// 
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace OpenData.Framework.Common.Menu.Configuration
{
    public class RouteValuesElement : ConfigurationElement
    {
        private Dictionary<string, object> attributes = new Dictionary<string, object>();


        public Dictionary<string, object> Attributes
        {
            get { return this.attributes; }
        }


        protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
        {
            attributes.Add(name, value);
            return true;
        }
    }
}
