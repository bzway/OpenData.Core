using System;
using System.Collections.Generic;
using System.Linq;
using Bzway.Module.Wechat.Entity;
using Bzway.Data.Core;
using Microsoft.Extensions.Logging;
using Bzway.Framework.Application;
using Bzway.Framework.Application.Entity;

namespace Bzway.Module.Wechat.Service
{
    public class WechatService : BaseService<WechatService>
    {
        #region ctor
        public WechatService(ILoggerFactory loggerFactory, Site site) : base(loggerFactory, site) { }
        #endregion

        public IEnumerable<WechatKeyword> GetWechatResponse(string Keyword = "", SearchType SearchType = SearchType.None, string wechatId = "")
        {
            try
            {
                var today = DateTime.Now;
                var yesterday = today.AddDays(-1);
                var query = this.db.Entity<WechatKeyword>().Query()
                    .Where(m => (!m.FromTime.HasValue || m.FromTime.Value <= today))
                    .Where(m => (!m.EndTime.HasValue || m.EndTime.Value > yesterday));
                switch (SearchType)
                {
                    case SearchType.Equal:
                        query = query.Where(m => m.Keyword == Keyword);
                        break;
                    case SearchType.StartWith:
                        query = query.Where(m => m.Keyword.StartsWith(Keyword));
                        break;
                    case SearchType.EndWith:
                        query = query.Where(m => m.Keyword.EndsWith(Keyword));
                        break;
                    case SearchType.Contain:
                        query = query.Where(m => m.Keyword.Contains(Keyword));
                        break;

                    case SearchType.Include:
                        query = query.Where(m => Keyword.Contains(m.Keyword));
                        break;
                    default:
                        query = query.Where(m =>
                            (m.SearchType == SearchType.Equal && m.Keyword == Keyword) ||
                            (m.SearchType == SearchType.StartWith && m.Keyword.StartsWith(Keyword) ||
                            (m.SearchType == SearchType.EndWith && m.Keyword.EndsWith(Keyword)) ||
                            (m.SearchType == SearchType.Contain && m.Keyword.Contains(Keyword)) ||
                            (m.SearchType == SearchType.Include && Keyword.Contains(m.Keyword))));

                        break;
                }
                return query.ToList();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return new List<WechatKeyword>();

            }
        }
    }
}