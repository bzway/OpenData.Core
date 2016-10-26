﻿using OpenData.Framework.Core.Entity;
using System.Text;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Html;
using Bzway.Data.Core;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OpenData.Framework.Core
{
    public static class FrontPageHtmlHelper
    {
        /// <summary>
        /// 得到当前页面对象
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static SitePage FrontPage(this HtmlHelper helper)
        {
            return helper.ViewBag.Page;
        }
        public static HtmlString RenderBody(this HtmlHelper helper)
        {
            return new HtmlString(helper.FrontPage().Content);
        }

        //public static HtmlString RenderBlocks(this HtmlHelper helper, string blockName)
        //{
        //    SiteManager siteManager = helper.ViewContext.HttpContext.GetSiteManager();
        //    using (var db = siteManager.GetSiteDataBase())
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        foreach (var block in db.Entity<PageBlock>().Query().Where(m => m.Name, blockName, CompareType.Equal)
        //            .Where(m => m.PageId, helper.FrontPage().Id, CompareType.Equal).OrderBy(m => m.OrderBy).ToList())
        //        {
        //            var item = db.Entity<PageView>().Query().Where(m => m.Id, block.ViewId, CompareType.Equal).First();
        //            if (item == null)
        //            {
        //                continue;
        //            }
        //            switch (item.Type)
        //            {
        //                case BlockType.StaticHtml:
        //                    sb.Append(helper.Partial(item.Path));
        //                    break;
        //                case BlockType.CreateView:
        //                    sb.Append(helper.Partial(item.Path, null));
        //                    break;
        //                case BlockType.DeleteView:
        //                case BlockType.UpdateView:
        //                    var id = helper.ViewContext.HttpContext.Request.QueryString["id"];
        //                    var model = db.DynamicEntity(db[item.EntityName]).Query().Where("Id", id, CompareType.Equal).First();
        //                    sb.Append(helper.Partial(item.Path, model));
        //                    break;
        //                case BlockType.QueryView:
        //                    sb.Append(helper.Partial(item.Path));
        //                    break;
        //                default:
        //                    break;
        //            }
        //            sb.Append(item.ToString());
        //        }
        //        return new HtmlString(sb.ToString());
        //    }
        //}
    }
}