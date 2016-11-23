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
namespace Bzway.Module.Wechat.Service
{
    public class WechatServiceHelper : BaseService<WechatService>
    {
        #region ctor
        private static HttpClient webClient = new HttpClient();
        public WechatServiceHelper(ILoggerFactory loggerFactory, Site site) : base(loggerFactory, site) { }
        #endregion


        public string Post(string url, string data)
        {
            try
            {
                var mssage = webClient.PostAsync(url, null).GetAwaiter().GetResult().Content.ToString();
                return mssage;
            }
            catch (Exception ex)
            {
                this.logger.LogError("{0}", ex);
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
                this.logger.LogError("WeChatHttps.GetAccessToken{0}", ex);
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
                log.Info("返回json：" + result);
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
                log.Error("WechatServiceHelper.CreateMenu", ex);
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
                log.Error("WechatServiceHelper.GetMenu", ex);
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
                log.Error("WechatServiceHelper.DeleteMenu", ex);
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
                log.Error("WechatServiceHelper.AddConditionalMenu", ex);
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
                log.Error("WechatServiceHelper.DeleteConditionalMenu", ex);
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
                log.Error("WechatServiceHelper.GetCurrentSelfMenu", ex);
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
                log.Error("WechatServiceHelper.GetKFList", ex);
                return null;
            }
        }

        public string CustomSend(CustomSendModel custom)
        {
            try
            {
                log.Info("发送客服消息Json：" + JsonConvert.SerializeObject(custom));
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
                log.Error("WechatServiceHelper.CustomSend", ex);
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
                log.Error("WechatServiceHelper.SendMsgByUserGroup", ex);
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
                log.Info("WechatServiceHelper.SendMsgByOpenid:" + respone.result.ToString());
                if (respone.resultCode == ResultCode.Success)
                {
                    return respone.result.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                log.Error("WechatServiceHelper.SendMsgByOpenid", ex);
                return null;
            }
        }

        public JObject ViewMaterial(string data)
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
                    JObject wxJsonObj = (JObject)JsonConvert.DeserializeObject(respone.result.ToString());
                    return wxJsonObj;
                }
                return null;
            }
            catch (Exception ex)
            {
                log.Error("WechatServiceHelper.GetOnLineKFList", ex);
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
                log.Error("WechatServiceHelper.DeleteGroupMsg", ex);
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
                log.Error("WechatServiceHelper.SendTemplateMsg", ex);
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
                log.Error("WechatServiceHelper.GetJsapiTicket", ex);
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
                log.Error("WechatServiceHelper.GetCardapiTicket", ex);
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
                log.Error("WechatServiceHelper.GetUserinfo", ex);
                return null;
            }
        }

        #endregion

        #region 素材管理

        public string CreateMaterial(MaterialModel material)
        {
            try
            {
                log.Info("CreateMaterial:" + material.ToString());
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
                log.Error("WechatServiceHelper.CreateMaterial", ex);
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
                log.Error("WechatServiceHelper.GetMaterial", ex);
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
                log.Error("WechatServiceHelper.DeleteMaterial", ex);
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
                log.Error("WechatServiceHelper.UpdateMaterial", ex);
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
                log.Error("WechatServiceHelper.getMaterialCount", ex);
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
                log.Error("WechatServiceHelper.GetMaterialList", ex);
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
                log.Error("WechatServiceHelper.CreateGroup", ex);
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
                log.Error("WechatServiceHelper.GetAllGroups", ex);
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
                log.Error("WechatServiceHelper.GetMemberGroup", ex);
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
                log.Error("WechatServiceHelper.UpdateGroup", ex);
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
                log.Error("WechatServiceHelper.MoveMemberGroup", ex);
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
                log.Error("WechatServiceHelper.BatchMoveMemberGroup", ex);
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
                log.Error("WechatServiceHelper.DeleteGroup", ex);
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
                log.Error("WechatServiceHelper.UpdateRemark", ex);
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
                log.Error("WechatServiceHelper.GetDetailUserInfoList", ex);
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
                log.Error("WechatServiceHelper.GetUserInfoList", ex);
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
                log.Error("WechatServiceHelper.GetDetailUserInfo", ex);
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
                log.Error("WechatServiceHelper.CreateQRCodeTicket", ex);
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
                log.Error("WechatServiceHelper.GetShortUrl", ex);
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
                log.Error("WechatServiceHelper.GetUserSummary", ex);
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
                log.Error("WechatServiceHelper.GetUserCumulate", ex);
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
                log.Error("WechatServiceHelper.GetArticleSummary", ex);
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
                log.Error("WechatServiceHelper.GetArticleTotal", ex);
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
                log.Error("WechatServiceHelper.GetUserRead", ex);
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
                log.Error("WechatServiceHelper.GetUserReadByHour", ex);
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
                log.Error("WechatServiceHelper.GetUserShare", ex);
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
                log.Error("WechatServiceHelper.GetUserShareByHour", ex);
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
                log.Error("WechatServiceHelper.GetUpStreamMsg", ex);
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
                log.Error("WechatServiceHelper.GetUpStreamMsgByHour", ex);
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
                log.Error("WechatServiceHelper.GetUpStreamMsgByWeek", ex);
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
                log.Error("WechatServiceHelper.GetUpStreamMsgByMonth", ex);
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
                log.Error("WechatServiceHelper.GetUpStreamMsgDist", ex);
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
                log.Error("WechatServiceHelper.GetUpStreamMsgDistByWeek", ex);
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
                log.Error("WechatServiceHelper.GetUpStreamMsgDistByMonth", ex);
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
                log.Error("WechatServiceHelper.GetInterfaceSummary", ex);
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
                log.Error("WechatServiceHelper.GetInterfaceSummaryByHour", ex);
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
                log.Error("WechatServiceHelper.CreateCards", ex);
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
                log.Error("WechatServiceHelper.GetOneCardQrCode", ex);
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
                log.Error("WechatServiceHelper.GetMultipleCardQrCode", ex);
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
                log.Error("WechatServiceHelper.GetCardStatus", ex);
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
                log.Error("WechatServiceHelper.UpdateCard", ex);
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
                log.Error("WechatServiceHelper.ModifyStock", ex);
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
                log.Error("WechatServiceHelper.DeleteCard", ex);
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
                log.Error("WechatServiceHelper.DeleteCard", ex);
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
                log.Error("WechatServiceHelper.ActivateCard", ex);
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
                log.Error("WechatServiceHelper.DeleteCard", ex);
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
                log.Error("WechatServiceHelper.DecryptCard", ex);
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
                log.Error("WechatServiceHelper.CreatePoi", ex);
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
                log.Error("WechatServiceHelper.GetPoi", ex);
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
                log.Error("WechatServiceHelper.GetPoiList", ex);
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
                log.Error("WechatServiceHelper.UpdatePoi", ex);
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
                log.Error("WechatServiceHelper.DeletePoi", ex);
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
                log.Error("WechatServiceHelper.GetWxCategory", ex);
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
                log.Error("WechatServiceHelper.GetOnLineKFList", ex);
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
                log.Error("WechatServiceHelper.CreateCustom", ex);
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
                log.Error("WechatServiceHelper.InvitationCustom", ex);
                return null;
            }
        }

        public string UploadCustomHeadImg(string filePath, string accountName)
        {
            try
            {
                string result = "";
                var accessToken = GetAccessToken();
                string wxurl = string.Format("https://api.weixin.qq.com/customservice/kfaccount/uploadheadimg?access_token={0}&kf_account={1}", accessToken, accountName);
                WebClient myWebClient = new WebClient();
                myWebClient.Credentials = CredentialCache.DefaultCredentials;

                byte[] responseArray = myWebClient.UploadFile(wxurl, "POST", filePath);
                result = System.Text.Encoding.Default.GetString(responseArray, 0, responseArray.Length);
                return result.Replace("\\/", "/");
            }
            catch (Exception ex)
            {
                log.Error("WechatServiceHelper.UploadHeadImg", ex);
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
                log.Error("WechatServiceHelper.UpdateCustom", ex);
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
                log.Error("WechatServiceHelper.DeleteCustom", ex);
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
                log.Error("WechatServiceHelper.Opendialogue", ex);
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
                log.Error("WechatServiceHelper.Closedialogue", ex);
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
                log.Error("WechatServiceHelper.Getsession", ex);
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
                log.Error("WechatServiceHelper.GetMsgRecord", ex);
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
                log.Error("WechatServiceHelper.Register", ex);
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
                log.Error("WechatServiceHelper.AuditStatus", ex);
                return null;
            }
        }

        #endregion

        #region 微信连WIFI TODO



        #endregion

        #region 微信扫一扫 TODO



        #endregion

        #region 微信上传
        public string UploadImg(string filePath)
        {
            try
            {
                string result = "";
                var accessToken = GetAccessToken();
                string wxurl = "https://api.weixin.qq.com/cgi-bin/media/uploadimg?access_token=" + accessToken;
                WebClient myWebClient = new WebClient();
                myWebClient.Credentials = CredentialCache.DefaultCredentials;

                byte[] responseArray = myWebClient.UploadFile(wxurl, "POST", filePath);
                result = System.Text.Encoding.Default.GetString(responseArray, 0, responseArray.Length);
                return result.Replace("\\/", "/");
            }
            catch (Exception ex)
            {
                log.Error("WechatServiceHelper.uploadImg", ex);
                return null;
            }
        }
        /// <summary>
        /// 媒体文件在后台保存时间为3天，即3天后media_id失效。
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string Upload(string filePath, string type)
        {
            try
            {
                string result = "";
                var accessToken = GetAccessToken();
                string wxurl = "https://api.weixin.qq.com/cgi-bin/media/upload?access_token=" + accessToken + "&type=" + type;
                WebClient myWebClient = new WebClient();
                myWebClient.Credentials = CredentialCache.DefaultCredentials;

                byte[] responseArray = myWebClient.UploadFile(wxurl, "POST", filePath);
                result = System.Text.Encoding.Default.GetString(responseArray, 0, responseArray.Length);
                return result.Replace("\\/", "/");
            }
            catch (Exception ex)
            {
                log.Error("WechatServiceHelper.Upload", ex);
                return null;
            }
        }

        #endregion

        public int ConvertDateTimeInt(DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

        public string JSAPITicket
        {
            get
            {
                var jsapiTicket = CacheHelper.GetCache("jsapiTicket") != null ? CacheHelper.GetCache("jsapiTicket").ToString() : null;
                if (string.IsNullOrEmpty(jsapiTicket))
                {
                    WechatServiceHelper helper = new WechatServiceHelper(SessionHelper.CurrentOfficalAccount.AppName, SessionHelper.CurrentOfficalAccount.SecurityKey, "Backoffice", "Website");
                    GetJsapiTicketModel jsmodel = helper.GetJsapiTicket();
                    jsapiTicket = jsmodel.ticket;
                    CacheHelper.AddCache("jsapiTicket", jsapiTicket, 60);
                }
                return jsapiTicket;
            }
        }

        public string CardAPITicket
        {
            get
            {
                string cardapiTicket = CacheHelper.GetCache("cardapiTicket") != null ? CacheHelper.GetCache("cardapiTicket").ToString() : null;
                if (string.IsNullOrEmpty(cardapiTicket))
                {
                    WechatServiceHelper helper = new WechatServiceHelper(SessionHelper.CurrentOfficalAccount.AppName, SessionHelper.CurrentOfficalAccount.SecurityKey, "Backoffice", "Website");
                    GetJsapiTicketModel cardmodel = helper.GetCardapiTicket();
                    cardapiTicket = cardmodel.ticket;
                    CacheHelper.AddCache("cardapiTicket", cardapiTicket, 60);
                }
                return cardapiTicket;
            }
        }

        public WeChatSDKModel GetSDKConfig(string url)
        {
            WeChatSDKModel weChatSDKModel = new WeChatSDKModel();
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            weChatSDKModel.timeStamp = (int)(DateTime.Now - startTime).TotalSeconds;

            weChatSDKModel.nonceStr = Guid.NewGuid().ToString("N");
            string jsapi_ticket = WechatServiceHelper.JSAPITicket;// WechatHelper.JSAPITicket;
            string string1 = "jsapi_ticket=" + jsapi_ticket + "&noncestr=" + weChatSDKModel.nonceStr + "&timestamp=" + weChatSDKModel.timeStamp + "&url=" + url;
            log.Info("JSsdkConfig:" + string1);
            weChatSDKModel.signature = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(string1, "SHA1");
            weChatSDKModel.appId = SessionHelper.CurrentOfficalAccount.AppId;//System.Configuration.ConfigurationManager.AppSettings["appid"];
            return weChatSDKModel;
        }



        public string RedirectToAuthorizeUrl(string redirectUrl, string state, string scope)
        {
            //"https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope={2}&state={3}#wechat_redirect";
            string authorizeUrl = string.Format(WechatConstants.getAuthorizeUrl, this.appName, HttpUtility.UrlEncode(redirectUrl), scope, state);
            log.Warn(authorizeUrl);
            return authorizeUrl;
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
                    log.Info("GetAccessTokenByCode:state:" + state + ";responseState:" + respone.state);
                    return null;
                }
                if (respone.resultCode == ResultCode.Success)
                {
                    return JsonConvert.DeserializeObject<AccessCodeModel>(respone.result.ToString());
                }
                log.Info("GetAccessTokenByCode respone.resultCode:" + respone.resultCode + ";respone.resultMsg:" + respone.resultMsg);
            }
            catch (Exception ex)
            {
                log.Error("GetAccessTokenByCode", ex);
            }
            return null;
        }
    }
}