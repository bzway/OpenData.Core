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
using System.Web;

namespace Bzway.Mvc
{
    public class AggregateHtmlString : IHtmlString
    {
        List<IHtmlString> list;
        public AggregateHtmlString()
            : this(null)
        {   
        }
        public AggregateHtmlString(IEnumerable<IHtmlString> htmlStrings)
        {
            if (htmlStrings != null)
            {
                list = new List<IHtmlString>(htmlStrings);
            }
            else
            {
                list = new List<IHtmlString>();
            }
        }
        public IEnumerable<IHtmlString> HtmlStrings
        {
            get
            {
                return list;
            }
        }
        public void Add(IHtmlString htmlString)
        {
            list.Add(htmlString);
        }
        #region IHtmlString Members

        public string ToHtmlString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in HtmlStrings)
            {
                if (item != null)
                {
                    sb.Append(item.ToHtmlString());
                }
            }
            return sb.ToString();
        }

        #endregion
        public override string ToString()
        {
            return ToHtmlString();
        }
    }
}
