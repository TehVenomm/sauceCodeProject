// Decompiled with JetBrains decompiler
// Type: GuildManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
public class GuildManager : MonoBehaviourSingleton<GuildManager>
{
  public const int NUM_GUILD_MAP = 2;
  public const int GUILD_LEVEL_REQUIRE = 15;
  public const int GUILD_CREATE_NEEDED_GEM = 15;
  public static int sDefaultEmblemIDLayer1 = 10001;
  public static int sDefaultEmblemIDLayer2 = 10011;
  public static int sDefaultEmblemIDLayer3 = 10021;
  private bool isEnterGUild;
  private string banReason;
  private GuildManager.CreateGuildRequestParam mCreateRequest = new GuildManager.CreateGuildRequestParam();
  private GuildManager.GuildSearchRequestParam mSearchRequest = new GuildManager.GuildSearchRequestParam();
  public string mSearchKeywork;
  private bool _isClanInfoCached;
  public List<FriendCharaInfo> talkUsers = new List<FriendCharaInfo>();
  public long askUpdate = -1;

  public bool IsEnterGuild
  {
    set => this.isEnterGUild = value;
    get => this.isEnterGUild;
  }

  public string BanReason
  {
    set => this.banReason = value;
  }

  public GuildManager.CreateGuildRequestParam GetCreateGuildRequestParam() => this.mCreateRequest;

  public void ClearCreateGuildRequestParam()
  {
    this.mCreateRequest = new GuildManager.CreateGuildRequestParam();
  }

  public GuildManager.GuildSearchRequestParam GetGuildSearchRequestParam() => this.mSearchRequest;

  public void ResetGuildSearchRequest()
  {
    this.mSearchRequest = new GuildManager.GuildSearchRequestParam();
  }

  public void CreateAddedGuildRequestParam(GuildStatisticInfo guildStat)
  {
    this.mCreateRequest = new GuildManager.CreateGuildRequestParam(guildStat);
  }

  public GuildModel.Guild guildData { get; private set; }

  public GuildStatisticInfo guildStatData { get; private set; }

  public GuildStatisticInfo guildChangeData { get; private set; }

  public List<GuildSearchModel.GuildSearchInfo> guilds { get; private set; }

  public List<GuildInvitedModel.GuildInvitedInfo> guildInviteList { get; private set; }

  public List<DonateInvitationInfo> donateInviteList { get; private set; }

  public GuildMemberListModel guilMemberList { get; private set; }

  public void SetGuildChangeData(GuildManager.CreateGuildRequestParam param)
  {
    if (param == null)
      return;
    this.guildChangeData = new GuildStatisticInfo();
    this.guildChangeData.emblem = param.EmblemLayerIDs;
    this.guildChangeData.clanName = param.GuildName;
    this.guildChangeData.tag = param.GuildTag;
    this.guildChangeData.description = param.GuildDescribe;
    this.guildChangeData.privacy = (int) param.GuildType;
    this.guildChangeData.min_level = param.GuildMinLevel;
  }

  private bool IsInGuild() => this.guildData != null;

  public static bool IsValidInGuild()
  {
    return MonoBehaviourSingleton<GuildManager>.IsValid() && MonoBehaviourSingleton<GuildManager>.I.IsInGuild();
  }

  public void SendCreate(List<int> inviteList, Action<bool, Error> callBack)
  {
    GuildModel.RequestCreate postData = new GuildModel.RequestCreate()
    {
      token = GuildManager.GenerateToken(),
      name = this.mCreateRequest.GuildName,
      description = this.mCreateRequest.GuildDescribe,
      tag = this.mCreateRequest.GuildTag,
      emblem = new List<int>()
    };
    postData.emblem.Add(this.mCreateRequest.EmblemLayerIDs[0]);
    postData.emblem.Add(this.mCreateRequest.EmblemLayerIDs[1]);
    postData.emblem.Add(this.mCreateRequest.EmblemLayerIDs[2]);
    postData.min_level = this.mCreateRequest.GuildMinLevel;
    postData.location = this.mCreateRequest.GuildMapID;
    postData.privacy = (int) this.mCreateRequest.GuildType;
    postData.inviteList = new List<string>();
    for (int index = 0; inviteList != null && index < inviteList.Count; ++index)
      postData.inviteList.Add(inviteList[index].ToString());
    this.SaveGuildSettings();
    Protocol.Send<GuildModel.RequestCreate, GuildModel>(GuildModel.RequestCreate.path, postData, (Action<GuildModel>) (ret =>
    {
      bool is_success = false;
      switch (ret.Error)
      {
        case Error.None:
          is_success = true;
          this.ClearCreateGuildRequestParam();
          this.UpdateGuild(ret.result.guildInfo);
          if (MonoBehaviourSingleton<ChatManager>.IsValid())
          {
            MonoBehaviourSingleton<ChatManager>.I.CreateClanChat(ret.result.chat, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.clanId, (Action<bool>) (success => callBack(is_success, ret.Error)));
            break;
          }
          callBack(is_success, ret.Error);
          break;
        case Error.WRN_PARTY_TOO_MANY_PARTIES:
          Log.Error("Guild create fall");
          callBack(is_success, ret.Error);
          break;
        default:
          callBack(is_success, ret.Error);
          break;
      }
    }));
  }

  public void SendCheckClanSetting(
    int clanId,
    string clanName,
    string clanTag,
    string clanDescription,
    Action<bool, Error> call_back)
  {
    Protocol.Send<GuildModel.RequestCreateVerify, BaseModel>(GuildModel.RequestCreateVerify.verifyPath, new GuildModel.RequestCreateVerify()
    {
      clanId = clanId,
      token = GuildManager.GenerateToken(),
      name = clanName,
      tag = clanTag,
      description = clanDescription
    }, (Action<BaseModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        call_back(true, ret.Error);
      else
        call_back(flag, ret.Error);
    }));
  }

  public void SendDelete(Action<bool, Error> call_back)
  {
    Protocol.Send<GuildModel.RequestDelete, BaseModel>(GuildModel.RequestDelete.path, new GuildModel.RequestDelete()
    {
      token = GuildManager.GenerateToken()
    }, (Action<BaseModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        this.UpdateGuild((GuildModel.Guild) null);
      }
      call_back(flag, ret.Error);
    }));
  }

  public void SendSearch(Action<bool, Error> call_back, bool saveSettings)
  {
    Protocol.Send<GuildSearchModel.RequestSearchWithKeyword, GuildSearchModel>(GuildSearchModel.RequestSearchWithKeyword.path, new GuildSearchModel.RequestSearchWithKeyword()
    {
      token = GuildManager.GenerateToken(),
      keyword = this.mSearchKeywork
    }, (Action<GuildSearchModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        this.guilds = ret.result.clanList;
      }
      call_back(flag, ret.Error);
    }));
  }

  public void SendSearchWithID(int clanId, Action<bool, Error> call_back)
  {
    Protocol.Send<GuildSearchModelWithID.RequestSearchWithID, GuildSearchModelWithID>(GuildSearchModelWithID.RequestSearchWithID.path, new GuildSearchModelWithID.RequestSearchWithID()
    {
      token = GuildManager.GenerateToken(),
      clanId = clanId
    }, (Action<GuildSearchModelWithID>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        this.guilds.Clear();
        if (ret.result.guildInfo != null)
          this.guilds.Add(ret.result.guildInfo);
      }
      call_back(flag, ret.Error);
    }));
  }

  public void SendRequestStatistic(int clan_id, Action<bool, GuildStatisticInfo> callback)
  {
    Protocol.Send<GuildStatisticModel.Form, GuildStatisticModel>(GuildStatisticModel.URL, new GuildStatisticModel.Form()
    {
      clanId = clan_id
    }, (Action<GuildStatisticModel>) (ret => callback(ErrorCodeChecker.IsSuccess(ret.Error), ret.result)));
  }

  public void SendRequestJoin(int clanId, int recommentId, Action<bool, Error> call_back)
  {
    Protocol.Send<GuildModel.RequestJoin, GuildRequestJoinModel>(GuildRequestJoinModel.URL, new GuildModel.RequestJoin()
    {
      token = GuildManager.GenerateToken(),
      clanId = clanId,
      recommendId = recommentId
    }, (Action<GuildRequestJoinModel>) (ret =>
    {
      bool is_success = false;
      if (ret.Error == Error.None)
      {
        is_success = true;
        this.guildInfos = (GuildInfoModel.GuildInfo) ret.result;
        this.UpdateGuild(this.guildInfos.guildInfo);
        if (ret.result.status == 1 && MonoBehaviourSingleton<ChatManager>.IsValid())
          MonoBehaviourSingleton<ChatManager>.I.CreateClanChat(this.guildInfos.chat, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.clanId, (Action<bool>) (success => call_back(is_success, ret.Error)));
        else
          call_back(is_success, ret.Error);
      }
      else
        call_back(is_success, ret.Error);
    }));
  }

  public void SendKick(int userId, Action<bool, Error> call_back = null)
  {
    Protocol.Send<GuildModel.RequestKick, BaseModel>(GuildModel.RequestKick.path, new GuildModel.RequestKick()
    {
      token = GuildManager.GenerateToken(),
      forUserId = userId,
      reason = this.banReason.Replace("\n", "\\n")
    }, (Action<BaseModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      call_back(flag, ret.Error);
    }));
  }

  public void SendAdminJoin(int requestId, int decision, Action<bool, Error> call_back)
  {
    Protocol.Send<GuildModel.RequestAdminJoin, BaseModel>(GuildModel.RequestAdminJoin.path, new GuildModel.RequestAdminJoin()
    {
      token = GuildManager.GenerateToken(),
      requestId = requestId,
      decision = decision
    }, (Action<BaseModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      call_back(flag, ret.Error);
    }));
  }

  public void SendRequestRequest(int clanId, int recommentId, Action<bool, Error> call_back)
  {
    Protocol.Send<GuildModel.RequestJoin, GuildRequestJoinModel>(GuildRequestJoinModel.URL, new GuildModel.RequestJoin()
    {
      token = GuildManager.GenerateToken(),
      clanId = clanId,
      recommendId = recommentId
    }, (Action<GuildRequestJoinModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      call_back(flag, ret.Error);
    }));
  }

  public void SendChangeSetting(
    GuildManager.CreateGuildRequestParam requestParam,
    Action<bool, Error> call_back)
  {
    Protocol.Send<GuildModel.GuildChangeSetting, GuildChangeSettingModel>(GuildChangeSettingModel.URL, new GuildModel.GuildChangeSetting()
    {
      name = requestParam.GuildName,
      description = requestParam.GuildDescribe,
      tag = requestParam.GuildTag,
      min_level = requestParam.GuildMinLevel,
      emblem = requestParam.EmblemLayerIDs,
      privacy = (int) requestParam.GuildType,
      location = requestParam.GuildLocation
    }, (Action<GuildChangeSettingModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        this.UpdateGuildData(ret);
      }
      call_back(flag, ret.Error);
    }));
  }

  public void SendLeave(Action<bool, Error> call_back)
  {
    Protocol.Send<GuildModel.RequestLeave, BaseModel>(GuildModel.RequestLeave.path, new GuildModel.RequestLeave()
    {
      token = GuildManager.GenerateToken()
    }, (Action<BaseModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        this.UpdateGuild((GuildModel.Guild) null);
      }
      call_back(flag, ret.Error);
    }));
  }

  public void UpdateGuild(GuildModel.Guild guild)
  {
    this.guildData = guild;
    if (this.guildData != null)
    {
      MonoBehaviourSingleton<UserInfoManager>.I.userStatus.clanId = this.guildData.clanId;
      this.GetClanStat();
      PlayerPrefs.GetInt("CLAN_ID");
      PlayerPrefs.SetInt("CLAN_ID", MonoBehaviourSingleton<UserInfoManager>.I.userStatus.clanId);
    }
    else
    {
      MonoBehaviourSingleton<UserInfoManager>.I.userStatus.clanId = -1;
      this.guildStatData = (GuildStatisticInfo) null;
      PlayerPrefs.SetInt("CLAN_ID", MonoBehaviourSingleton<UserInfoManager>.I.userStatus.clanId);
    }
  }

  private void UpdateGuildData(GuildChangeSettingModel updateData)
  {
    GuildChangeSettingModel.Result result = updateData.result;
    this.guildData.name = result.clanName;
    this.guildData.level = result.level;
    this.guildData.exp = result.exp;
    this.guildData.emblem = result.emblem;
  }

  public static bool IsValidNotEmptyGuildList()
  {
    return MonoBehaviourSingleton<GuildManager>.IsValid() && MonoBehaviourSingleton<GuildManager>.I.guilds != null && MonoBehaviourSingleton<GuildManager>.I.guilds.Count > 0;
  }

  private void SaveGuildSettings()
  {
  }

  public static string GenerateToken() => Guid.NewGuid().ToString().Replace("-", "");

  public GuildInfoModel.GuildInfo guildInfos { get; private set; }

  public void SendClanInfo(Action<bool> callBack)
  {
    if (this._isClanInfoCached)
    {
      Protocol.SendAsync<GuildInfoModel>(GuildInfoModel.URL, (Action<GuildInfoModel>) (ret =>
      {
        if (ret.Error != Error.None)
          return;
        if (ret.result.invitation)
          MonoBehaviourSingleton<UserInfoManager>.I.SetClanInviteHome();
        this.guildInfos = ret.result;
        if (this.guildInfos.guildInfo == null)
        {
          PlayerPrefs.SetInt("CLAN_ID", -1);
        }
        else
        {
          this.SetAskUpdate(long.Parse(this.guildInfos.askUpdate));
          if (MonoBehaviourSingleton<ChatManager>.IsValid())
            MonoBehaviourSingleton<ChatManager>.I.CreateClanChat(this.guildInfos.chat, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.clanId);
          this.UpdateGuild(this.guildInfos.guildInfo);
          if (!ret.result.receivable)
            return;
          GuildManager.SendDonateReceive((Action<bool>) null);
        }
      }));
      callBack(true);
    }
    else
      Protocol.Send<GuildInfoModel>(GuildInfoModel.URL, (Action<GuildInfoModel>) (ret =>
      {
        bool isSuccess = false;
        if (ret.Error == Error.None)
        {
          this._isClanInfoCached = true;
          isSuccess = true;
          if (ret.result.invitation)
            MonoBehaviourSingleton<UserInfoManager>.I.SetClanInviteHome();
          this.guildInfos = ret.result;
          if (this.guildInfos.guildInfo == null)
          {
            callBack(isSuccess);
            PlayerPrefs.SetInt("CLAN_ID", -1);
          }
          else
          {
            this.SetAskUpdate(long.Parse(this.guildInfos.askUpdate));
            if (MonoBehaviourSingleton<ChatManager>.IsValid())
              MonoBehaviourSingleton<ChatManager>.I.CreateClanChat(this.guildInfos.chat, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.clanId);
            this.UpdateGuild(this.guildInfos.guildInfo);
            if (ret.result.receivable)
              GuildManager.SendDonateReceive((Action<bool>) (receiveSuccess => callBack(isSuccess)));
            else
              callBack(isSuccess);
          }
        }
        else
          callBack(isSuccess);
      }));
  }

  public void GetClanStat(Action<bool> call_back = null)
  {
    if (this.guildData == null || this.guildData.clanId == -1)
      return;
    this.SendRequestStatistic(this.guildData.clanId, (Action<bool, GuildStatisticInfo>) ((success, info) =>
    {
      this.guildStatData = !success ? (GuildStatisticInfo) null : info;
      if (call_back == null)
        return;
      call_back(success);
    }));
  }

  public List<FriendCharaInfo> members { get; set; }

  public void SendMemberList(int clan_id, Action<bool, GuildMemberListModel> callback)
  {
    Protocol.Send<GuildMemberListModel.RequestSendForm, GuildMemberListModel>(GuildMemberListModel.URL, new GuildMemberListModel.RequestSendForm()
    {
      clanId = clan_id
    }, (Action<GuildMemberListModel>) (ret =>
    {
      bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
      this.members = ret.result.list;
      this.guilMemberList = ret;
      callback(flag, ret);
    }));
  }

  public FriendCharaInfo talkUser { private set; get; }

  public void SetTalkUser(FriendCharaInfo message_user)
  {
    if (this.talkUsers.FirstOrDefault<FriendCharaInfo>((Func<FriendCharaInfo, bool>) (o => o.userId == message_user.userId)) == null)
      this.talkUsers.Insert(0, message_user);
    this.talkUser = message_user;
  }

  public bool AddTalkUser(int userId)
  {
    if (this.talkUsers.FirstOrDefault<FriendCharaInfo>((Func<FriendCharaInfo, bool>) (o => o.userId == userId)) != null)
      return false;
    FriendCharaInfo friendCharaInfo = this.members.FirstOrDefault<FriendCharaInfo>((Func<FriendCharaInfo, bool>) (o => o.userId == userId));
    if (friendCharaInfo == null)
      return false;
    this.talkUsers.Add(friendCharaInfo);
    return true;
  }

  public void RemoveTalkUser(FriendCharaInfo message_user)
  {
    FriendCharaInfo friendCharaInfo = this.talkUsers.FirstOrDefault<FriendCharaInfo>((Func<FriendCharaInfo, bool>) (o => o.userId == message_user.userId));
    if (friendCharaInfo == null)
      return;
    this.talkUsers.Remove(friendCharaInfo);
  }

  public void UpdateTalkUser()
  {
    if (this.talkUser != null || this.talkUsers.Count <= 0)
      return;
    this.talkUser = this.talkUsers[0];
  }

  public void EmptyTalkUser() => this.talkUser = (FriendCharaInfo) null;

  public void SendClanChatLog(Action<bool, GuildChatModel> callback)
  {
    Protocol.Send<GuildChatModel>(GuildChatModel.URL, (Action<GuildChatModel>) (ret => callback(ErrorCodeChecker.IsSuccess(ret.Error), ret)));
  }

  public void SendPrivateClanChatLog(int to, Action<bool, GuildPrivateChatModel> callback)
  {
    GuildPrivateChatModel.SendForm postData = new GuildPrivateChatModel.SendForm()
    {
      toUserId = to
    };
    Protocol.Send<GuildPrivateChatModel.SendForm, GuildPrivateChatModel>(GuildPrivateChatModel.URL, postData, (Action<GuildPrivateChatModel>) (ret => callback(ErrorCodeChecker.IsSuccess(ret.Error), ret)));
  }

  public void SendClanChatPin(
    int fromUserId,
    int chatId,
    string uuid,
    int type,
    string msg,
    Action<bool, GuildChatPinModel> callback)
  {
    Protocol.Send<GuildChatPinModel.SendForm, GuildChatPinModel>(GuildChatPinModel.URL, new GuildChatPinModel.SendForm()
    {
      id = chatId,
      uuid = uuid,
      type = type,
      message = msg.Replace("\n", "\\n"),
      fromUserId = fromUserId
    }, (Action<GuildChatPinModel>) (ret => callback(ErrorCodeChecker.IsSuccess(ret.Error), ret)));
  }

  public void SendClanChatUnPin(Action<bool, GuildChatUnPinModel> callback)
  {
    Protocol.Send<GuildChatUnPinModel>(GuildChatUnPinModel.URL, (Action<GuildChatUnPinModel>) (ret =>
    {
      this.pinDonate = (DonateInfo) null;
      callback(ErrorCodeChecker.IsSuccess(ret.Error), ret);
    }));
  }

  public void SendClanChatOnlineStatus(Action<bool, List<GuildMemberChatStatus>> callback)
  {
    Protocol.Send<GuildChatOnlineStatusModel>(GuildChatOnlineStatusModel.URL, (Action<GuildChatOnlineStatusModel>) (ret => callback(ErrorCodeChecker.IsSuccess(ret.Error), ret.result.online)));
  }

  public void GetAllPinData(Action<bool, GuildGetPinModel> callback)
  {
    Protocol.Send<GuildGetPinModel>(GuildGetPinModel.URL, (Action<GuildGetPinModel>) (ret => callback(ErrorCodeChecker.IsSuccess(ret.Error), ret)));
  }

  public List<DonateInfo> donateList { get; set; }

  public double SystemTime { get; set; }

  public DonateInfo pinDonate { get; set; }

  public bool ExistsAskUpdate => true;

  public void SetAskUpdate(long value)
  {
    if (value <= 0L || value == -1L)
      return;
    this.askUpdate = value;
  }

  public void SendDonateList(Action<bool> callback)
  {
    Protocol.Send<GuildDonate.GuildDonateListModel>(GuildDonate.GuildDonateListModel.URL, (Action<GuildDonate.GuildDonateListModel>) (ret =>
    {
      bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
      this.donateList = ret.result.array;
      this.pinDonate = ret.result.pinDonate;
      this.SystemTime = Utility.DateTimeToTimestampMilliseconds(DateTime.Parse(ret.currentTime));
      callback(flag);
    }));
  }

  public void SendDonateFobbidenList(Action<bool, List<int>> callback)
  {
    Protocol.Send<GuildDonate.GuildDonateFobbidenModel>(GuildDonate.GuildDonateFobbidenModel.URL, (Action<GuildDonate.GuildDonateFobbidenModel>) (ret => callback(ErrorCodeChecker.IsSuccess(ret.Error), ret.result.array)));
  }

  public void SendDonateRequest(
    int itemId,
    string itemName,
    string msg,
    int quatity,
    Action<bool> callback)
  {
    Protocol.Send<GuildDonate.GuildDonateRequestModel.Form, GuildDonate.GuildDonateRequestModel>(GuildDonate.GuildDonateRequestModel.URL, new GuildDonate.GuildDonateRequestModel.Form()
    {
      itemId = itemId,
      itemName = itemName,
      msg = msg,
      quantity = quatity,
      nickName = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name
    }, (Action<GuildDonate.GuildDonateRequestModel>) (ret =>
    {
      bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
      if (flag)
        MonoBehaviourSingleton<UserInfoManager>.I.userStatus.nextDonationTime = DateTime.Parse(ret.result.expired);
      callback(flag);
    }));
  }

  public void SendDonateSend(int donateId, int quatity, Action<bool> callback)
  {
    Protocol.Send<GuildDonate.GuildDonateSendModel.Form, GuildDonate.GuildDonateSendModel>(GuildDonate.GuildDonateSendModel.URL, new GuildDonate.GuildDonateSendModel.Form()
    {
      id = donateId,
      quantity = quatity
    }, (Action<GuildDonate.GuildDonateSendModel>) (ret => callback(ErrorCodeChecker.IsSuccess(ret.Error))));
  }

  private static void SendDonateReceive(Action<bool> callback)
  {
    Protocol.SendAsync<GuildDonate.GuildDonateReceiveModel>(GuildDonate.GuildDonateReceiveModel.URL, (Action<GuildDonate.GuildDonateReceiveModel>) (ret =>
    {
      bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
      if (callback == null)
        return;
      callback(flag);
    }));
  }

  public void SendDonateInviteList(
    int donate_id,
    Action<bool, GuildDonate.GuildDonateInviteListModel> callback)
  {
    Protocol.Send<GuildDonate.GuildDonateInviteListModel.Form, GuildDonate.GuildDonateInviteListModel>(GuildDonate.GuildDonateInviteListModel.URL, new GuildDonate.GuildDonateInviteListModel.Form()
    {
      id = donate_id
    }, (Action<GuildDonate.GuildDonateInviteListModel>) (ret => callback(ErrorCodeChecker.IsSuccess(ret.Error), ret)));
  }

  public void SendDonateInvite(int id, int user_id, Action<bool> callback)
  {
    Protocol.Send<GuildDonate.GuildDonateInviteModel.Form, GuildDonate.GuildDonateInviteModel>(GuildDonate.GuildDonateInviteModel.URL, new GuildDonate.GuildDonateInviteModel.Form()
    {
      userId = user_id,
      id = id
    }, (Action<GuildDonate.GuildDonateInviteModel>) (ret => callback(ErrorCodeChecker.IsSuccess(ret.Error))));
  }

  public void SendDonateInvitationList(Action<bool> callback, bool isResumed = false)
  {
    if (!GuildManager.IsValidInGuild())
    {
      this.donateInviteList = new List<DonateInvitationInfo>();
      callback(true);
    }
    else
    {
      this.donateInviteList = (List<DonateInvitationInfo>) null;
      Protocol.Send<GuildDonate.GuildDonateInvitationListModel>(GuildDonate.GuildDonateInvitationListModel.URL, (Action<GuildDonate.GuildDonateInvitationListModel>) (ret =>
      {
        bool flag = false;
        if (ret.Error == Error.None)
        {
          flag = true;
          this.donateInviteList = ret.result.array;
          if (this.donateInviteList.Count > 0)
            this.FillterDonateInviteList();
        }
        else
          this.donateInviteList = new List<DonateInvitationInfo>();
        if (isResumed && this.donateInviteList.Count > 0)
          MonoBehaviourSingleton<UserInfoManager>.I.SetClanDonateInviteHome();
        callback(flag);
      }));
    }
  }

  private void FillterDonateInviteList()
  {
    double timestampSeconds = this.DateTimeToTimestampSeconds();
    int count = this.donateInviteList.Count;
    for (int index = 0; index < count; ++index)
    {
      if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id != this.donateInviteList[index].userId)
      {
        if (this.donateInviteList[index].expired / 1000.0 - timestampSeconds < 1.0)
        {
          this.donateInviteList.RemoveAt(index);
          --count;
          --index;
        }
        else if (this.donateInviteList[index].itemNum >= this.donateInviteList[index].quantity)
        {
          this.donateInviteList.RemoveAt(index);
          --count;
          --index;
        }
      }
    }
  }

  private double DateTimeToTimestampSeconds()
  {
    return (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
  }

  public static bool IsValidNotEmptyDonateInviteList()
  {
    return MonoBehaviourSingleton<GuildManager>.IsValid() && MonoBehaviourSingleton<GuildManager>.I.donateInviteList != null && MonoBehaviourSingleton<GuildManager>.I.donateInviteList.Count > 0;
  }

  public string GetInviteMessage()
  {
    return this.guildData == null ? string.Empty : this.guildData.inviteMessage;
  }

  public string GetInviteHelpURL()
  {
    return this.guildData == null || this.guildData.inviteFriendInfo == null ? string.Empty : this.guildData.inviteFriendInfo.linkUrl;
  }

  public void SendInviteList(Action<bool, GuildInviteCharaInfo[]> call_back)
  {
    if (this.guildData == null)
      call_back(false, (GuildInviteCharaInfo[]) null);
    else
      Protocol.Send<GuildInviteListModel.RequestSendForm, GuildInviteListModel>(GuildInviteListModel.URL, new GuildInviteListModel.RequestSendForm()
      {
        id = this.guildData.clanId.ToString()
      }, (Action<GuildInviteListModel>) (ret =>
      {
        bool flag = false;
        GuildInviteCharaInfo[] guildInviteCharaInfoArray = (GuildInviteCharaInfo[]) null;
        if (ret.Error == Error.None)
        {
          flag = true;
          guildInviteCharaInfoArray = ret.result.list.ToArray();
        }
        call_back(flag, guildInviteCharaInfoArray);
      }));
  }

  public void SendInvite(int[] userIds, Action<bool, int[]> call_back)
  {
    if (this.guildData == null)
    {
      call_back(false, (int[]) null);
    }
    else
    {
      GuildInviteModel.RequestSendForm postData = new GuildInviteModel.RequestSendForm();
      postData.id = this.guildData.clanId.ToString();
      foreach (int userId in userIds)
        postData.inviteList.Add(userId);
      Protocol.Send<GuildInviteModel.RequestSendForm, GuildInviteModel>(GuildInviteModel.URL, postData, (Action<GuildInviteModel>) (ret =>
      {
        bool flag = false;
        if (ret.Error == Error.None)
          call_back(true, userIds);
        else
          call_back(flag, (int[]) null);
      }));
    }
  }

  public void SendRejectInviteClan(int requestId, Action<bool> call_back)
  {
    Protocol.Send<GuildRejectInvitedModel.SendForm, GuildRejectInvitedModel>(GuildRejectInvitedModel.URL, new GuildRejectInvitedModel.SendForm()
    {
      requestId = requestId
    }, (Action<GuildRejectInvitedModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      call_back(flag);
    }));
  }

  public void SendInvitedGuild(Action<bool> callBack, bool isResumed = false)
  {
    if (GuildManager.IsValidInGuild())
    {
      this.guildInviteList = new List<GuildInvitedModel.GuildInvitedInfo>();
      if (callBack == null)
        return;
      callBack(true);
    }
    else
    {
      this.guildInviteList = (List<GuildInvitedModel.GuildInvitedInfo>) null;
      Protocol.SendAsync<GuildInvitedModel>(GuildInvitedModel.RequestInvited.path, (Action<GuildInvitedModel>) (ret =>
      {
        this.guildInviteList = ret.Error == Error.None ? ret.result.list : new List<GuildInvitedModel.GuildInvitedInfo>();
        if (!isResumed || this.guildInviteList.Count <= 0)
          return;
        MonoBehaviourSingleton<UserInfoManager>.I.SetClanInviteHome();
      }));
      if (callBack == null)
        return;
      callBack(true);
    }
  }

  public static bool IsValidNotEmptyInviteList()
  {
    return MonoBehaviourSingleton<GuildManager>.IsValid() && MonoBehaviourSingleton<GuildManager>.I.guildInviteList != null && MonoBehaviourSingleton<GuildManager>.I.guildInviteList.Count > 0;
  }

  public void SendSearchFollowerRoom(
    Action<bool, List<GuildSearchFollowerRoomModel.GuildFollowerModel>> call_back)
  {
    Protocol.Send<GuildSearchFollowerRoomModel>(GuildSearchFollowerRoomModel.URL, (Action<GuildSearchFollowerRoomModel>) (ret => call_back(ret.Error == Error.None, ret.result.list)));
  }

  public enum GUILD_TYPE
  {
    PUBLIC,
    PRIVATE,
    CLOSED,
  }

  public class CreateGuildRequestParam
  {
    public int[] EmblemLayerIDs { get; private set; }

    public string GuildName { get; private set; }

    public string GuildTag { get; private set; }

    public string GuildDescribe { get; private set; }

    public int GuildLocation { get; private set; }

    public GuildManager.GUILD_TYPE GuildType { get; private set; }

    public int GuildMinLevel { get; private set; }

    public int GuildMapID { get; private set; }

    public int[] InvitedFriendIDs { get; private set; }

    public void SetEmblemID(int layer, int id) => this.EmblemLayerIDs[layer] = id;

    public void SetGuildName(string name) => this.GuildName = name;

    public void SetGuildTag(string tag) => this.GuildTag = tag;

    public void SetGuildDescribe(string des) => this.GuildDescribe = des;

    public void SetGuildType(GuildManager.GUILD_TYPE type) => this.GuildType = type;

    public void SetGuildMinLevel(int min_level) => this.GuildMinLevel = min_level;

    public void SetGuildLocaltion(int location) => this.GuildLocation = location;

    public CreateGuildRequestParam()
    {
      this.EmblemLayerIDs = new int[3];
      this.EmblemLayerIDs[0] = -1;
      this.EmblemLayerIDs[1] = -1;
      this.EmblemLayerIDs[2] = -1;
      this.GuildType = GuildManager.GUILD_TYPE.PUBLIC;
      this.GuildMinLevel = 15;
    }

    public CreateGuildRequestParam(GuildStatisticInfo guidata)
    {
      if (guidata == null)
        return;
      this.EmblemLayerIDs = guidata.emblem;
      this.GuildName = guidata.clanName;
      this.GuildTag = guidata.tag;
      this.GuildDescribe = guidata.description;
      this.GuildType = (GuildManager.GUILD_TYPE) guidata.privacy;
      this.GuildMinLevel = guidata.min_level;
    }
  }

  public class GuildSearchRequestParam
  {
  }
}
