using Bzway.Module.Wechat.Entity;
using System.Collections.Generic;
using System.Linq;

public interface IWechatService
{
    #region User
    IQueryable<WechatUserGroup> GetAllTags();

    IQueryable<WechatUser> GetUsers();

    #endregion
    #region Menu


    #endregion

    #region Keyword

    IEnumerable<WechatKeyword> GetWechatResponse(string Keyword = "", SearchType SearchType = SearchType.None, string wechatId = "");
    #endregion
}