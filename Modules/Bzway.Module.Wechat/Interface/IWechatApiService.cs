using Accentiv.Spark.Service.Wechat;
using Bzway.Module.Wechat.Model;

namespace Bzway.Module.Wechat.Interface
{
    public interface IWechatApiService
    {
        string CardAPITicket { get; }
        string JSAPITicket { get; }

        string ActivateCard(string data);
        string AddConditionalMenu(WechatCustomButtonMenu menu);
        AuditStatusResponseModel AuditStatus();
        string BatchMoveMemberGroup(BatchMoveMemberGroup batchGroup);
        string Closedialogue(KFSessionModel session);
        string ConsumeCard(string cardId, string cardCode);
        string CreateCards(string data);
        string CreateCustom(CustomModel custom);
        string CreateGroup(string name);
        string CreateMaterial(MaterialModel material);
        string CreateMenu(WechatButtonMenu menu);
        string CreatePoi(PoiRequestModel model);
        QRCodeResponseModel CreateQRCodeTicket(ActionModel qrCode);
        string CustomSend(CustomSendModel custom);
        string DecryptCard(string encrypt_code);
        string DeleteCard(string cardId);
        string DeleteConditionalMenu(string menuId);
        string DeleteCustom(CustomModel custom, string AccessToken);
        string DeleteGroup(int groupId);
        string DeleteGroupMsg(string msgId);
        string DeleteMaterial(string mediaId);
        string DeleteMenu();
        string DeletePoi(string poi_id);
        string GetAccessToken();
        AccessCodeModel GetAccessTokenByCode(string appId, string appSecurityKey, string code);
        string GetAllGroups();
        ArticleSummaryModel GetArticleSummary(string start, string end);
        ArticleTotalModel GetArticleTotal(string start, string end);
        GetJsapiTicketModel GetCardapiTicket();
        WeChatMemberCard GetCardStatus(string cardId);
        string GetCurrentSelfMenu();
        WeChatUserInfoModel GetDetailUserInfo(string openId, string lang);
        string GetDetailUserInfoList(BatchGetUserInfo batchUserInfo);
        InterfaceSummaryModel GetInterfaceSummary(string start, string end);
        InterfaceSummaryModel GetInterfaceSummaryByHour(string start, string end);
        GetJsapiTicketModel GetJsapiTicket();
        KFListModel GetKFList();
        MaterialResponse GetMaterial(string mediaId);
        MaterialCountResponse GetMaterialCount();
        MaterialListResponse GetMaterialList(string type, int offset, int count);
        string GetMemberGroup(string openId);
        string GetMenu();
        MsgRecordModel GetMsgRecord(MsgRecordPostModel record);
        QRCodeCardResponse GetMultipleCardQrCode(MultipleQrCodeCard cardList);
        QRCodeCardResponse GetOneCardQrCode(OneQrCodeCard card);
        OnLineKFModel GetOnLineKFList();
        PoiResponseModel GetPoi(string poi_id);
        PoiListResponseModel GetPoiList(int begin, int limit = 20);
        WeChatSDKModel GetSDKConfig(string url);
        KFSessionResponseModel Getsession(string openId);
        string GetShortUrl(ForShortUrlModel ulrModel);
        StreamMsgModel GetUpStreamMsg(string start, string end);
        StreamMsgModel GetUpStreamMsgByHour(string start, string end);
        StreamMsgModel GetUpStreamMsgByMonth(string start, string end);
        StreamMsgModel GetUpStreamMsgByWeek(string start, string end);
        StreamMsgModel GetUpStreamMsgDist(string start, string end);
        StreamMsgModel GetUpStreamMsgDistByMonth(string start, string end);
        StreamMsgModel GetUpStreamMsgDistByWeek(string start, string end);
        UserCumulateModel GetUserCumulate(string start, string end);
        UserInfoResponseModel GetUserinfo(string openId);
        WeChatUserModel GetUserInfoList(string openId);
        ArticleReadModel GetUserRead(string start, string end);
        ArticleReadByHourModel GetUserReadByHour(string start, string end);
        ArticleShareModel GetUserShare(string start, string end);
        ArticleShareByHourModel GetUserShareByHour(string start, string end);
        UserCumulateModel GetUserSummary(string start, string end);
        PoiCategoryResponseModel GetWxCategory();
        string GrantCard(string cardId);
        string InvitationCustom(CustomModel custom);
        string ModifyStock(modifystock stock);
        string MoveMemberGroup(string openId, int toGroupId);
        string Opendialogue(KFSessionModel session);
        string RedirectToAuthorizeUrl(string redirectUrl, string state, string scope);
        string Register(RegisterRequestModel model);
        string SendMsgByOpenid(MassContentByOpenidModel byOpenId);
        string SendMsgByUserGroup(MassContentByGroupidModel byGroup);
        string SendTemplateMsg(TemplateMsgModel template);
        string UpdateCard(string data);
        string UpdateCustom(CustomModel custom);
        string UpdateGroup(int groupId, string groupName);
        string UpdateMaterial(MaterialUpdateModel material);
        string UpdatePoi(UpdatePoiResponseModel model);
        string UpdateRemark(string openId, string remark);
        string Upload(string filePath, string type);
        string UploadCustomHeadImg(string filePath, string accountName);
        string UploadImg(string filePath);
        object ViewMaterial(string data);
    }
}