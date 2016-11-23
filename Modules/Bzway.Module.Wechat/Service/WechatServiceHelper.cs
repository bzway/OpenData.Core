using System;
using System.Collections.Generic;
using System.Linq;
using Bzway.Module.Wechat.Entity;
using Bzway.Data.Core;
using Microsoft.Extensions.Logging;
using Bzway.Framework.Application;
using Bzway.Framework.Application.Entity;
using Newtonsoft.Json;
using System.Net.Http;
using Bzway.Module.Wechat.Model;
using Accentiv.Spark.Service.Wechat;
using System.IO;
using System.Threading.Tasks;
using Bzway.Common.Utility;

namespace Bzway.Module.Wechat.Service
{
    public class WechatServiceHelper : BaseService<WechatService>
    {
        #region ctor
        private static HttpClient webClient = new HttpClient();
        public WechatServiceHelper(ILoggerFactory loggerFactory, Site site) : base(loggerFactory, site) { }
        #endregion

        public string Get(string url, string data)
        {
            return string.Empty;
        }
        public string Post(string url, string data)
        {
            try
            {
                var mssage = webClient.PostAsync(url, null).GetAwaiter().GetResult().Content.ToString();
                return mssage;
            }
            catch (Exception ex)
            {
                this.logger.LogError("{0}{0}", ex);
                return null;
            }
        }
        public string GetAccessToken()
        {
            try
            {
                return string.Empty;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WeChatHttps.GetAccessToken{0}{0}", ex);
                return null;
            }

        }

        #region 自定义菜单

        public string CreateMenu(WechatButtonMenu menu)
        {
            try
            {
                var request = new WechatPostRequest() { functionName = "createMenu", data = menu.ToString() };
                var state = Guid.NewGuid().ToString("N");
                string result = Post(state, request.ToString());

                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(result);
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.CreateMenu{0}", ex);
                return new WechatMenuResult() { errcode = "500", errmsg = "接口调用出错！详情见错误日志,WechatServiceHelper.CreateMenu" }.ToString();
            }
        }

        public string GetMenu()
        {
            try
            {
                var request = new WechatPostRequest() { functionName = "getMenuData", data = "" };

                var state = Guid.NewGuid().ToString("N");

                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Get(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetMenu{0}", ex);
                return null;
            }
        }

        public string DeleteMenu()
        {
            try
            {
                var request = new WechatPostRequest() { functionName = "deleteMenu", data = "" };

                var state = Guid.NewGuid().ToString("N");

                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Get(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.DeleteMenu{0}", ex);
                return null;
            }
        }

        public string AddConditionalMenu(WechatCustomButtonMenu menu)
        {
            try
            {
                var request = new WechatPostRequest() { functionName = "addConditionalMenu", data = menu.ToString() };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.AddConditionalMenu{0}", ex);
                return null;
            }
        }

        public string DeleteConditionalMenu(string menuId)
        {
            try
            {
                string data = "{\"menuid\":\"" + menuId + "\"}";
                var request = new WechatPostRequest() { functionName = "deleteConditionalMenu", data = data };

                var state = Guid.NewGuid().ToString("N");

                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.DeleteConditionalMenu{0}", ex);
                return null;
            }
        }

        public string GetCurrentSelfMenu()
        {
            try
            {
                var request = new WechatPostRequest() { functionName = "getCurrentSelfMenu", data = "" };

                var state = Guid.NewGuid().ToString("N");

                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Get(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetCurrentSelfMenu{0}", ex);
                return null;
            }
        }

        #endregion

        #region 消息管理

        public KFListModel GetKFList()
        {
            try
            {
                var request = new WechatPostRequest() { functionName = "getKFList", data = "" };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Get(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    var model = JsonConvert.DeserializeObject<KFListModel>(respone.result.ToString());
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetKFList{0}", ex);
                return null;
            }
        }

        public string CustomSend(CustomSendModel custom)
        {
            try
            {

                var request = new WechatPostRequest() { functionName = "customSend", data = custom.ToString() };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.CustomSend{0}", ex);
                return null;
            }
        }

        public string SendMsgByUserGroup(MassContentByGroupidModel byGroup)
        {
            try
            {
                var request = new WechatPostRequest() { functionName = "sendMsgByUserGroup", data = byGroup.ToString() };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.SendMsgByUserGroup{0}", ex);
                return null;
            }
        }

        public string SendMsgByOpenid(MassContentByOpenidModel byOpenId)
        {
            try
            {
                var request = new WechatPostRequest() { functionName = "sendMsgByOpenid", data = byOpenId.ToString() };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }

                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.SendMsgByOpenid{0}", ex);
                return null;
            }
        }

        public string ViewMaterial(string data)
        {
            try
            {
                var request = new WechatPostRequest() { functionName = "getPreview", data = data };

                var state = Guid.NewGuid().ToString("N");

                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetOnLineKFList{0}", ex);
                return null;
            }
        }

        public string DeleteGroupMsg(string msgId)
        {
            try
            {
                string data = "{\"msg_id\":\"" + msgId + "\"}";
                var request = new WechatPostRequest() { functionName = "deleteGroupMsg", data = data };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.DeleteGroupMsg{0}", ex);
                return null;
            }
        }

        public string SendTemplateMsg(TemplateMsgModel template)
        {
            try
            {
                var request = new WechatPostRequest() { functionName = "sendTemplate", data = template.ToString() };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.SendTemplateMsg{0}", ex);
                return null;
            }
        }

        #endregion

        #region 微信网页开发

        public GetJsapiTicketModel GetJsapiTicket()
        {
            try
            {
                var request = new WechatPostRequest() { functionName = "getJsapiTicket", data = "" };

                var state = Guid.NewGuid().ToString("N");

                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Get(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    var model = JsonConvert.DeserializeObject<GetJsapiTicketModel>(respone.result.ToString());
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetJsapiTicket{0}", ex);
                return null;
            }
        }

        public GetJsapiTicketModel GetCardapiTicket()
        {
            try
            {
                var request = new WechatPostRequest() { functionName = "getCardapiTicket", data = "" };

                var state = Guid.NewGuid().ToString("N");

                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Get(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    var model = JsonConvert.DeserializeObject<GetJsapiTicketModel>(respone.result.ToString());
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetCardapiTicket{0}", ex);
                return null;
            }
        }

        public UserInfoResponseModel GetUserinfo(string openId)
        {
            try
            {
                var functionName = "https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid=" + openId + "&lang=zh_CN";
                var request = new WechatPostRequest() { functionName = functionName, data = "" };

                var state = Guid.NewGuid().ToString("N");

                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Get(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    var model = JsonConvert.DeserializeObject<UserInfoResponseModel>(respone.result.ToString());
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetUserinfo{0}", ex);
                return null;
            }
        }

        #endregion

        #region 素材管理

        public string CreateMaterial(MaterialModel material)
        {
            try
            {
                var request = new WechatPostRequest() { functionName = "createMaterial", data = material.ToString() };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.CreateMaterial{0}", ex);
                return null;
            }
        }

        public MaterialResponse GetMaterial(string mediaId)
        {
            try
            {
                string data = "{\"media_id\":\"" + mediaId + "\"}";
                var request = new WechatPostRequest() { functionName = "getMaterial", data = data };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    var model = JsonConvert.DeserializeObject<MaterialResponse>(respone.result.ToString());
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetMaterial{0}", ex);
                return null;
            }
        }

        public string DeleteMaterial(string mediaId)
        {
            try
            {
                string data = "{\"media_id\":\"" + mediaId + "\"}";
                var request = new WechatPostRequest() { functionName = "deleteMaterial", data = data };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.DeleteMaterial{0}", ex);
                return null;
            }
        }

        public string UpdateMaterial(MaterialUpdateModel material)
        {
            try
            {
                var request = new WechatPostRequest() { functionName = "updateMaterial", data = material.ToString() };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.UpdateMaterial{0}", ex);
                return null;
            }
        }

        public MaterialCountResponse GetMaterialCount()
        {
            try
            {
                var request = new WechatPostRequest() { functionName = "getMaterialCount", data = "" };

                var state = Guid.NewGuid().ToString("N");

                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Get(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    var model = JsonConvert.DeserializeObject<MaterialCountResponse>(respone.result.ToString());
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.getMaterialCount{0}", ex);
                return null;
            }
        }

        public MaterialListResponse GetMaterialList(string type, int offset, int count)
        {
            try
            {
                string data = "{\"type\":\"" + type + "\",\"offset\":" + offset + ",\"count\":" + count + "}";
                var request = new WechatPostRequest() { functionName = "getMaterialList", data = data };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    var model = JsonConvert.DeserializeObject<MaterialListResponse>(respone.result.ToString());
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetMaterialList{0}", ex);
                return null;
            }
        }

        #endregion

        #region 用户管理

        public string CreateGroup(string name)
        {
            try
            {
                string data = ("{\"group\":{\"name\":\"" + name + "\"}}");
                var request = new WechatPostRequest() { functionName = "createGroup", data = data };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.CreateGroup{0}", ex);
                return null;
            }
        }

        public string GetAllGroups()
        {
            try
            {
                var request = new WechatPostRequest() { functionName = "getAllGroups", data = "" };

                var state = Guid.NewGuid().ToString("N");

                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Get(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetAllGroups{0}", ex);
                return null;
            }
        }

        public string GetMemberGroup(string openId)
        {
            try
            {
                string data = "{\"openid\":\"" + openId + "\"}";
                var request = new WechatPostRequest() { functionName = "getMemberGroup", data = data };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetMemberGroup{0}", ex);
                return null;
            }
        }

        public string UpdateGroup(int groupId, string groupName)
        {
            try
            {
                string data = "{\"group\":{\"id\":" + groupId + ",\"name\":\"" + groupName + "\"}}";
                var request = new WechatPostRequest() { functionName = "updateGroup", data = data };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.UpdateGroup{0}", ex);
                return null;
            }
        }

        public string MoveMemberGroup(string openId, int toGroupId)
        {
            try
            {
                string data = "{\"openid\":\"" + openId + "\",\"to_groupid\":" + toGroupId + "}";
                var request = new WechatPostRequest() { functionName = "moveMemberGroup", data = data };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.MoveMemberGroup{0}", ex);
                return null;
            }
        }

        public string BatchMoveMemberGroup(BatchMoveMemberGroup batchGroup)
        {
            try
            {
                var request = new WechatPostRequest() { functionName = "batchMoveMemberGroup", data = batchGroup.ToString() };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.BatchMoveMemberGroup{0}", ex);
                return null;
            }
        }

        public string DeleteGroup(int groupId)
        {
            try
            {
                string data = "{\"group\":{\"id\":" + groupId + "}}";
                var request = new WechatPostRequest() { functionName = "deleteGroup", data = data };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.DeleteGroup{0}", ex);
                return null;
            }
        }

        public string UpdateRemark(string openId, string remark)
        {
            try
            {
                string data = "{\"openid\":\"" + openId + "\",\"remark\":\"" + remark + "\"}";
                var request = new WechatPostRequest() { functionName = "updateRemark", data = data };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.UpdateRemark{0}", ex);
                return null;
            }
        }

        public string GetDetailUserInfoList(BatchGetUserInfo batchUserInfo)
        {
            try
            {
                var request = new WechatPostRequest() { functionName = "getDetailUserInfoList", data = batchUserInfo.ToString() };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetDetailUserInfoList{0}", ex);
                return null;
            }
        }

        public WeChatUserModel GetUserInfoList(string openId)
        {
            try
            {
                var functionName = "https://api.weixin.qq.com/cgi-bin/user/get?access_token={0}&next_openid=" + openId;
                var request = new WechatPostRequest() { functionName = functionName, data = "" };

                var state = Guid.NewGuid().ToString("N");

                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Get(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    var model = JsonConvert.DeserializeObject<WeChatUserModel>(respone.result.ToString());
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetUserInfoList{0}", ex);
                return null;
            }
        }

        public WeChatUserInfoModel GetDetailUserInfo(string openId, string lang)
        {
            try
            {
                var functionName = "https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid=" + openId + "&lang=" + lang;
                var request = new WechatPostRequest() { functionName = functionName, data = "" };

                var state = Guid.NewGuid().ToString("N");

                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Get(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    var model = JsonConvert.DeserializeObject<WeChatUserInfoModel>(respone.result.ToString());
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetDetailUserInfo{0}", ex);
                return null;
            }
        }

        #endregion

        #region 帐号管理

        public QRCodeResponseModel CreateQRCodeTicket(ActionModel qrCode)
        {
            try
            {
                var request = new WechatPostRequest() { functionName = "createQRCodeTicket", data = qrCode.ToString() };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    var model = JsonConvert.DeserializeObject<QRCodeResponseModel>(respone.result.ToString());
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.CreateQRCodeTicket{0}", ex);
                return null;
            }
        }

        public string GetShortUrl(ForShortUrlModel ulrModel)
        {
            try
            {
                var request = new WechatPostRequest() { functionName = "getShortUrl", data = ulrModel.ToString() };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    var model = JsonConvert.DeserializeObject<ForShortUrlModel>(respone.result.ToString());
                    return model.short_url;
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetShortUrl{0}", ex);
                return null;
            }
        }

        #endregion

        #region 数据统计

        public UserCumulateModel GetUserSummary(string start, string end)
        {
            try
            {
                string data = "{\"begin_date\": \"" + start + "\", \"end_date\": \"" + end + "\"}";
                var request = new WechatPostRequest() { functionName = "getUserSummary", data = data };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    var model = JsonConvert.DeserializeObject<UserCumulateModel>(respone.result.ToString());
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetUserSummary{0}", ex);
                return null;
            }
        }

        public UserCumulateModel GetUserCumulate(string start, string end)
        {
            try
            {
                string data = "{\"begin_date\": \"" + start + "\", \"end_date\": \"" + end + "\"}";
                var request = new WechatPostRequest() { functionName = "getUserCumulate", data = data };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    var model = JsonConvert.DeserializeObject<UserCumulateModel>(respone.result.ToString());
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetUserCumulate{0}", ex);
                return null;
            }
        }

        public ArticleSummaryModel GetArticleSummary(string start, string end)
        {
            try
            {
                string data = "{\"begin_date\": \"" + start + "\", \"end_date\": \"" + end + "\"}";
                var request = new WechatPostRequest() { functionName = "getArticleSummary", data = data };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    var model = JsonConvert.DeserializeObject<ArticleSummaryModel>(respone.result.ToString());
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetArticleSummary{0}", ex);
                return null;
            }
        }

        public ArticleTotalModel GetArticleTotal(string start, string end)
        {
            try
            {
                string data = "{\"begin_date\": \"" + start + "\", \"end_date\": \"" + end + "\"}";
                var request = new WechatPostRequest() { functionName = "getArticleTotal", data = data };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    var model = JsonConvert.DeserializeObject<ArticleTotalModel>(respone.result.ToString());
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetArticleTotal{0}", ex);
                return null;
            }
        }

        public ArticleReadModel GetUserRead(string start, string end)
        {
            try
            {
                string data = "{\"begin_date\": \"" + start + "\", \"end_date\": \"" + end + "\"}";
                var request = new WechatPostRequest() { functionName = "getUserRead", data = data };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    var model = JsonConvert.DeserializeObject<ArticleReadModel>(respone.result.ToString());
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetUserRead{0}", ex);
                return null;
            }
        }

        public ArticleReadByHourModel GetUserReadByHour(string start, string end)
        {
            try
            {
                string data = "{\"begin_date\": \"" + start + "\", \"end_date\": \"" + end + "\"}";
                var request = new WechatPostRequest() { functionName = "getUserReadHour", data = data };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    var model = JsonConvert.DeserializeObject<ArticleReadByHourModel>(respone.result.ToString());
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetUserReadByHour{0}", ex);
                return null;
            }
        }

        public ArticleShareModel GetUserShare(string start, string end)
        {
            try
            {
                string data = "{\"begin_date\": \"" + start + "\", \"end_date\": \"" + end + "\"}";
                var request = new WechatPostRequest() { functionName = "getUserShare", data = data };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    var model = JsonConvert.DeserializeObject<ArticleShareModel>(respone.result.ToString());
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetUserShare{0}", ex);
                return null;
            }
        }

        public ArticleShareByHourModel GetUserShareByHour(string start, string end)
        {
            try
            {
                string data = "{\"begin_date\": \"" + start + "\", \"end_date\": \"" + end + "\"}";
                var request = new WechatPostRequest() { functionName = "getUserShareHour", data = data };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    var model = JsonConvert.DeserializeObject<ArticleShareByHourModel>(respone.result.ToString());
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetUserShareByHour{0}", ex);
                return null;
            }
        }

        public StreamMsgModel GetUpStreamMsg(string start, string end)
        {
            try
            {
                string data = "{\"begin_date\": \"" + start + "\", \"end_date\": \"" + end + "\"}";
                var request = new WechatPostRequest() { functionName = "getUpStreamMsg", data = data };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    var model = JsonConvert.DeserializeObject<StreamMsgModel>(respone.result.ToString());
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetUpStreamMsg{0}", ex);
                return null;
            }
        }

        public StreamMsgModel GetUpStreamMsgByHour(string start, string end)
        {
            try
            {
                string data = "{\"begin_date\": \"" + start + "\", \"end_date\": \"" + end + "\"}";
                var request = new WechatPostRequest() { functionName = "getUpStreamMsgByHour", data = data };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    var model = JsonConvert.DeserializeObject<StreamMsgModel>(respone.result.ToString());
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetUpStreamMsgByHour{0}", ex);
                return null;
            }
        }

        public StreamMsgModel GetUpStreamMsgByWeek(string start, string end)
        {
            try
            {
                string data = "{\"begin_date\": \"" + start + "\", \"end_date\": \"" + end + "\"}";
                var request = new WechatPostRequest() { functionName = "getUpStreamMsgByWeek", data = data };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    var model = JsonConvert.DeserializeObject<StreamMsgModel>(respone.result.ToString());
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetUpStreamMsgByWeek{0}", ex);
                return null;
            }
        }

        public StreamMsgModel GetUpStreamMsgByMonth(string start, string end)
        {
            try
            {
                string data = "{\"begin_date\": \"" + start + "\", \"end_date\": \"" + end + "\"}";
                var request = new WechatPostRequest() { functionName = "getUpStreamMsgByMonth", data = data };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    var model = JsonConvert.DeserializeObject<StreamMsgModel>(respone.result.ToString());
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetUpStreamMsgByMonth{0}", ex);
                return null;
            }
        }

        public StreamMsgModel GetUpStreamMsgDist(string start, string end)
        {
            try
            {
                string data = "{\"begin_date\": \"" + start + "\", \"end_date\": \"" + end + "\"}";
                var request = new WechatPostRequest() { functionName = "getUpStreamMsgDist", data = data };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    var model = JsonConvert.DeserializeObject<StreamMsgModel>(respone.result.ToString());
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetUpStreamMsgDist{0}", ex);
                return null;
            }
        }

        public StreamMsgModel GetUpStreamMsgDistByWeek(string start, string end)
        {
            try
            {
                string data = "{\"begin_date\": \"" + start + "\", \"end_date\": \"" + end + "\"}";
                var request = new WechatPostRequest() { functionName = "getUpStreamMsgDistByWeek", data = data };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    var model = JsonConvert.DeserializeObject<StreamMsgModel>(respone.result.ToString());
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetUpStreamMsgDistByWeek{0}", ex);
                return null;
            }
        }

        public StreamMsgModel GetUpStreamMsgDistByMonth(string start, string end)
        {
            try
            {
                string data = "{\"begin_date\": \"" + start + "\", \"end_date\": \"" + end + "\"}";
                var request = new WechatPostRequest() { functionName = "getUpStreamMsgDistByMonth", data = data };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    var model = JsonConvert.DeserializeObject<StreamMsgModel>(respone.result.ToString());
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetUpStreamMsgDistByMonth{0}", ex);
                return null;
            }
        }

        public InterfaceSummaryModel GetInterfaceSummary(string start, string end)
        {
            try
            {
                string data = "{\"begin_date\": \"" + start + "\", \"end_date\": \"" + end + "\"}";
                var request = new WechatPostRequest() { functionName = "getInterfaceSummary", data = data };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    var model = JsonConvert.DeserializeObject<InterfaceSummaryModel>(respone.result.ToString());
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetInterfaceSummary{0}", ex);
                return null;
            }

        }

        public InterfaceSummaryModel GetInterfaceSummaryByHour(string start, string end)
        {
            try
            {
                string data = "{\"begin_date\": \"" + start + "\", \"end_date\": \"" + end + "\"}";
                var request = new WechatPostRequest() { functionName = "getInterfaceSummaryByHour", data = data };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    var model = JsonConvert.DeserializeObject<InterfaceSummaryModel>(respone.result.ToString());
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetInterfaceSummaryByHour{0}", ex);
                return null;
            }

        }

        #endregion

        #region 微信卡券

        public string CreateCards(string data)
        {
            try
            {
                var request = new WechatPostRequest() { functionName = "createCards", data = data };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.CreateCards{0}", ex);
                return null;
            }
        }

        public QRCodeCardResponse GetOneCardQrCode(OneQrCodeCard card)
        {
            try
            {
                var request = new WechatPostRequest() { functionName = "getCardQrCode", data = card.ToString() };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    var model = JsonConvert.DeserializeObject<QRCodeCardResponse>(respone.result.ToString());
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetOneCardQrCode{0}", ex);
                return null;
            }
        }

        public QRCodeCardResponse GetMultipleCardQrCode(MultipleQrCodeCard cardList)
        {
            try
            {
                var request = new WechatPostRequest() { functionName = "getCardQrCode", data = cardList.ToString() };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    var model = JsonConvert.DeserializeObject<QRCodeCardResponse>(respone.result.ToString());
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetMultipleCardQrCode{0}", ex);
                return null;
            }
        }

        public WeChatMemberCard GetCardStatus(string cardId)
        {
            try
            {
                string data = "{\"card_id\": \"" + cardId + "\"}";
                var request = new WechatPostRequest() { functionName = "getCardStatus", data = data };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    var model = JsonConvert.DeserializeObject<WeChatMemberCard>(respone.result.ToString());
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetCardStatus{0}", ex);
                return null;
            }

        }

        public string UpdateCard(string data)
        {
            try
            {
                var request = new WechatPostRequest() { functionName = "updateCard", data = data };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.UpdateCard{0}", ex);
                return null;
            }

        }

        public string ModifyStock(modifystock stock)
        {
            try
            {
                var accessToken = GetAccessToken();
                var url = "https://api.weixin.qq.com/card/modifystock?access_token=" + accessToken;
                var request = new WechatPostRequest() { functionName = url, data = stock.ToString() };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.ModifyStock{0}", ex);
                return null;
            }

        }

        public string DeleteCard(string cardId)
        {
            try
            {
                string data = "{\"card_id\": \"" + cardId + "\"}";
                var request = new WechatPostRequest() { functionName = "deleteCard", data = data };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.DeleteCard{0}", ex);
                return null;
            }
        }

        public string GrantCard(string cardId)
        {
            try
            {
                string data = "{\"card_id\": \"" + cardId + "\"}";
                var request = new WechatPostRequest() { functionName = "grantCard", data = data };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.DeleteCard{0}", ex);
                return null;
            }
        }

        public string ActivateCard(string data)
        {
            try
            {
                var request = new WechatPostRequest() { functionName = "activateCard", data = data };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.ActivateCard{0}", ex);
                return null;
            }
        }

        public string ConsumeCard(string cardId, string cardCode)
        {

            try
            {
                string data = "{ \"code\": \"" + cardCode + "\", \"card_id\": \"" + cardId + "\"}";

                var request = new WechatPostRequest() { functionName = "https://api.weixin.qq.com/card/code/consume?access_token={0}", data = data };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.DeleteCard{0}", ex);
                return null;
            }

        }

        public string DecryptCard(string encrypt_code)
        {

            try
            {
                var accessToken = GetAccessToken();
                string data = "{ \"encrypt_code\": \"" + encrypt_code + "\"}";

                var request = new WechatPostRequest() { functionName = "https://api.weixin.qq.com/card/code/decrypt?access_token={0}", data = data };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.DecryptCard{0}", ex);
                return null;
            }

        }

        #endregion

        #region 微信门店

        public string CreatePoi(PoiRequestModel model)
        {
            try
            {
                var request = new WechatPostRequest() { functionName = "createPoi", data = model.ToString() };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.CreatePoi{0}", ex);
                return null;
            }
        }
        public PoiResponseModel GetPoi(string poi_id)
        {
            try
            {
                string data = "{\"poi_id\":\"" + poi_id + "\"}";
                var request = new WechatPostRequest() { functionName = "getPoi", data = data };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return JsonConvert.DeserializeObject<PoiResponseModel>(respone.result.ToString());
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetPoi{0}", ex);
                return null;
            }
        }

        public PoiListResponseModel GetPoiList(int begin, int limit = 20)
        {
            try
            {
                string data = "{\"begin\": \"" + begin + "\", \"limit\": \"" + limit + "\"}";
                var request = new WechatPostRequest() { functionName = "getPoiList", data = data };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return JsonConvert.DeserializeObject<PoiListResponseModel>(respone.result.ToString());
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetPoiList{0}", ex);
                return null;
            }
        }

        public string UpdatePoi(UpdatePoiResponseModel model)
        {
            try
            {
                var request = new WechatPostRequest() { functionName = "updatePoi", data = model.ToString() };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.UpdatePoi{0}", ex);
                return null;
            }
        }

        public string DeletePoi(string poi_id)
        {
            try
            {
                string data = "{\"poi_id\":\"" + poi_id + "\"}";
                var request = new WechatPostRequest() { functionName = "deletePoi", data = data };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.DeletePoi{0}", ex);
                return null;
            }
        }

        public PoiCategoryResponseModel GetWxCategory()
        {
            try
            {
                var request = new WechatPostRequest() { functionName = "getWxCategory", data = "" };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return JsonConvert.DeserializeObject<PoiCategoryResponseModel>(respone.result.ToString());
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetWxCategory{0}", ex);
                return null;
            }
        }

        #endregion

        #region 微信多客服功能

        public OnLineKFModel GetOnLineKFList()
        {
            try
            {
                var request = new WechatPostRequest() { functionName = "getOnLineKFList", data = "" };

                var state = Guid.NewGuid().ToString("N");

                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Get(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    var model = JsonConvert.DeserializeObject<OnLineKFModel>(respone.result.ToString());
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetOnLineKFList{0}", ex);
                return null;
            }
        }

        public string CreateCustom(CustomModel custom)
        {
            try
            {
                var request = new WechatPostRequest() { functionName = "createCustom", data = custom.ToString() };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.CreateCustom{0}", ex);
                return null;
            }
        }

        public string InvitationCustom(CustomModel custom)
        {
            try
            {
                var request = new WechatPostRequest() { functionName = "invitationCustom", data = custom.ToString() };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.InvitationCustom{0}", ex);
                return null;
            }
        }

        public async Task<string> UploadCustomHeadImg(string filePath, string accountName)
        {
            try
            {
                var accessToken = GetAccessToken();
                string url = string.Format("https://api.weixin.qq.com/customservice/kfaccount/uploadheadimg?access_token={0}&kf_account={1}", accessToken, accountName);
                using (var formDataContent = new MultipartFormDataContent())
                {
                    formDataContent.Add(new ByteArrayContent(File.ReadAllBytes(filePath)), "files", "test");
                    HttpResponseMessage response = await webClient.PostAsync(url, formDataContent);
                    var result = await response.Content.ReadAsByteArrayAsync();
                    return Convert.ToBase64String(result);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.UploadHeadImg{0}", ex);
                return null;
            }
        }

        public string UpdateCustom(CustomModel custom)
        {
            try
            {
                var request = new WechatPostRequest() { functionName = "updateCustom", data = custom.ToString() };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {

                this.logger.LogError("WechatServiceHelper.UpdateCustom{0}", ex);
                return null;
            }

        }

        public string DeleteCustom(CustomModel custom, string AccessToken)
        {
            try
            {
                var request = new WechatPostRequest() { functionName = string.Format("https://api.weixin.qq.com/customservice/kfaccount/del?access_token={0}&kf_account={1}", AccessToken, custom.kf_account) };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Get(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.DeleteCustom{0}", ex);
                return null;
            }
        }

        public string Opendialogue(KFSessionModel session)
        {
            try
            {
                var request = new WechatPostRequest() { functionName = "opendialogue", data = session.ToString() };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.Opendialogue{0}", ex);
                return null;
            }

        }

        public string Closedialogue(KFSessionModel session)
        {
            try
            {
                var request = new WechatPostRequest() { functionName = "closedialogue", data = session.ToString() };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.Closedialogue{0}", ex);
                return null;
            }

        }

        public KFSessionResponseModel Getsession(string openId)
        {
            try
            {
                var functionName = "https://api.weixin.qq.com/customservice/kfsession/getsession?access_token={0}&openid=" + openId + "&lang=zh_CN";
                var request = new WechatPostRequest() { functionName = functionName, data = "" };

                var state = Guid.NewGuid().ToString("N");

                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Get(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    var model = JsonConvert.DeserializeObject<KFSessionResponseModel>(respone.result.ToString());
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.Getsession{0}", ex);
                return null;
            }
        }

        public MsgRecordModel GetMsgRecord(MsgRecordPostModel record)
        {
            try
            {
                var request = new WechatPostRequest() { functionName = "getMsgRecord", data = record.ToString() };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    var model = JsonConvert.DeserializeObject<MsgRecordModel>(respone.result.ToString());
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.GetMsgRecord{0}", ex);
                return null;
            }

        }

        #endregion

        #region 微信摇一摇周边

        public string Register(RegisterRequestModel model)
        {
            try
            {
                var request = new WechatPostRequest() { functionName = "register", data = model.ToString() };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Post(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.Register{0}", ex);
                return null;
            }
        }

        public AuditStatusResponseModel AuditStatus()
        {
            try
            {
                var request = new WechatPostRequest() { functionName = "auditStatus", data = "" };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Get(state, request.ToString()));
                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return JsonConvert.DeserializeObject<AuditStatusResponseModel>(respone.result.ToString());
                }
                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.AuditStatus{0}", ex);
                return null;
            }
        }

        #endregion

        #region 微信连WIFI TODO



        #endregion

        #region 微信扫一扫 TODO



        #endregion

        #region 微信上传
        public async Task<string> UploadImg(string filePath)
        {
            try
            {
                var accessToken = GetAccessToken();
                string url = "https://api.weixin.qq.com/cgi-bin/media/uploadimg?access_token=" + accessToken;
                using (var formDataContent = new MultipartFormDataContent())
                {
                    formDataContent.Add(new ByteArrayContent(File.ReadAllBytes(filePath)), "files", "test");
                    HttpResponseMessage response = await webClient.PostAsync(url, formDataContent);
                    var result = await response.Content.ReadAsByteArrayAsync();
                    return Convert.ToBase64String(result);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.uploadImg{0}", ex);
                return null;
            }
        }
        /// <summary>
        /// 媒体文件在后台保存时间为3天，即3天后media_id失效。
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<string> Upload(string filePath, string type)
        {
            try
            {
                var accessToken = GetAccessToken();
                string url = "https://api.weixin.qq.com/cgi-bin/media/upload?access_token=" + accessToken + "&type=" + type;
                using (var formDataContent = new MultipartFormDataContent())
                {
                    formDataContent.Add(new ByteArrayContent(File.ReadAllBytes(filePath)), "files", "test");
                    HttpResponseMessage response = await webClient.PostAsync(url, formDataContent);
                    var result = await response.Content.ReadAsByteArrayAsync();
                    return Convert.ToBase64String(result);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError("WechatServiceHelper.Upload{0}", ex);
                return null;
            }
        }

        #endregion


        public string JSAPITicket
        {
            get
            {
                return string.Empty;
            }
        }

        public string CardAPITicket
        {
            get
            {
                return string.Empty;
            }
        }

        public WeChatSDKModel GetSDKConfig(string url)
        {
            WeChatSDKModel weChatSDKModel = new WeChatSDKModel();
            weChatSDKModel.timeStamp = (int)DateTimeHelper.GetBaseTimeValue(DateTime.Now);

            weChatSDKModel.nonceStr = Guid.NewGuid().ToString("N");
            string jsapi_ticket = this.JSAPITicket;
            string string1 = "jsapi_ticket=" + jsapi_ticket + "&noncestr=" + weChatSDKModel.nonceStr + "&timestamp=" + weChatSDKModel.timeStamp + "&url=" + url;
            weChatSDKModel.signature = Cryptor.EncryptSHA1(string1);
            weChatSDKModel.appId = string.Empty;// AppSettings["appid"];
            return weChatSDKModel;
        }



        public string RedirectToAuthorizeUrl(string redirectUrl, string appId, string state, string scope)
        {
            return string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope={2}&state={3}#wechat_redirect", appId, redirectUrl, scope, state);
        }
        public AccessCodeModel GetAccessTokenByCode(string appId, string appSecurityKey, string code)
        {
            try
            {
                var request = new WechatPostRequest() { functionName = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", appId, appSecurityKey, code), data = string.Empty };
                var state = Guid.NewGuid().ToString("N");
                var respone = JsonConvert.DeserializeObject<WebServiceResponse>(Get(state, request.ToString()));

                if (state != respone.state)
                {
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return JsonConvert.DeserializeObject<AccessCodeModel>(respone.result.ToString());
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError("GetAccessTokenByCode{0}", ex);
            }
            return null;
        }
    }
}