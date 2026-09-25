// Decompiled with JetBrains decompiler
// Type: ClanMatchingManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
public class ClanMatchingManager : MonoBehaviourSingleton<ClanMatchingManager>
{
  private bool isChangeStarted;
  public Action<LoungeMemberStatus> OnChangeMemberStatus = (Action<LoungeMemberStatus>) (x => { });
  private Coroutine afkCoroutine;
  private const int RETRY_COUNT = 3;
  private const float RETRY_TIMER = 5f;
  private Coroutine popupNoticeBoardCoroutine;
  private readonly TimeSpan AFK_KICK_TIME = TimeSpan.FromMinutes(30.0);
  private string LastReadCharacterMessageId = "1";
  public bool UsingChatConnection;
  public string CachedMessageClanId = "";
  public int chatUpdateInterval = 5;
  public string LastReadMessageId = "1";
  public int UnreadMessageCount;
  private float UnreadCountSyncFixedTime = -1f;
  protected ChatClanConnection chatConnection;
  protected List<ClanChatMessageModel> chatMessagesDESC = new List<ClanChatMessageModel>();
  private const string stamp_start = "[STAMP]";
  private static DateTime dtepoc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

  public ClanData clanData { get; private set; }

  public PartyModel.Party partyData { get; private set; }

  public List<PartyModel.Party> clanRoomParties { get; private set; }

  public ClanServer clanServerData { get; private set; }

  public LoungeMemberesStatus loungeMemberStatus { get; private set; }

  public UserClanData userClanData { get; private set; }

  public bool isKicked { get; private set; }

  public bool isResume { get; private set; }

  public bool isClanCreatedNow { get; private set; }

  public void OnCreateAnnounce() => this.isClanCreatedNow = false;

  public ClanSettings.CreateRequestParam createRequest { get; private set; }

  public ClanMatchingManager()
  {
    this.isClanCreatedNow = false;
    this.partyData = (PartyModel.Party) null;
    this.clanServerData = (ClanServer) null;
  }

  protected override void Awake()
  {
    base.Awake();
    ((Component) this).gameObject.AddComponent<LoungeWebSocket>();
    ((Component) this).gameObject.AddComponent<ClanNetworkManager>();
  }

  private void OnApplicationPause(bool pause)
  {
    if (!pause)
    {
      if (!this.isResume)
        return;
      this.ResumeConnect();
    }
    else
    {
      if (!MonoBehaviourSingleton<LoungeWebSocket>.I.IsConnected())
        return;
      this.isResume = true;
      this.StopAFKCheck();
      this.ClearClan();
    }
  }

  public void Dirty()
  {
    if (MonoBehaviourSingleton<GameSceneManager>.IsValid())
    {
      MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.RECEIVE_COOP_ROOM_UPDATE);
      if (this.isChangeStarted)
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.RECEIVE_COOP_ROOM_START);
    }
    this.AFKCheck();
  }

  private void UpdateClan(ClanData clan) => this.clanData = clan;

  public void ClearClanData() => this.clanData = (ClanData) null;

  private void UpdateParty(PartyModel.Party party, ClanServer clanServer)
  {
    if (this.partyData != null && this.partyData.status == 10 && (this.partyData.status == 100 || this.partyData.status == 105))
      this.isChangeStarted = true;
    this.partyData = party;
    if (clanServer != null)
    {
      this.clanServerData = clanServer;
      if (MonoBehaviourSingleton<ClanManager>.IsValid() && !MonoBehaviourSingleton<LoungeWebSocket>.I.IsConnected())
        this.ConnectServer();
    }
    if (this.loungeMemberStatus == null)
      return;
    this.loungeMemberStatus.SyncPartyMember(this.partyData);
  }

  public void ResumeConnect()
  {
    this.SendInfo((Action<bool>) (is_success => this.isResume = false), true);
  }

  public void TryConnect(bool connect, bool regist)
  {
    if (!(connect & regist))
      return;
    Lounge_Model_RoomJoined model = new Lounge_Model_RoomJoined();
    model.id = 1005;
    model.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    MonoBehaviourSingleton<ClanNetworkManager>.I.SendBroadcast<Lounge_Model_RoomJoined>(model);
    this.SetLoungeMemberesStatus();
    this.AFKCheck();
  }

  public bool IsConnected()
  {
    return MonoBehaviourSingleton<ClanMatchingManager>.I.loungeMemberStatus != null;
  }

  private void SetLoungeMemberesStatus()
  {
    if (!MonoBehaviourSingleton<ClanNetworkManager>.IsValid())
      return;
    this.loungeMemberStatus = new LoungeMemberesStatus(MonoBehaviourSingleton<ClanNetworkManager>.I.registerAck.GetConvertUserInfo().Where<Party_Model_RegisterACK.UserInfo>((Func<Party_Model_RegisterACK.UserInfo, bool>) (x => this.GetSlotInfoByUserId(x.userId) != null)).ToList<Party_Model_RegisterACK.UserInfo>());
  }

  public void SendRoomParty(Action<bool, List<PartyModel.Party>> call_back)
  {
    if (this.partyData == null)
      return;
    Protocol.Send<ClanRoomQuestModel>(ClanRoomQuestModel.URL, (Action<ClanRoomQuestModel>) (ret =>
    {
      this.UpdateRoomQuest(ret.result.parties);
      call_back(ret.Error == Error.None, ret.result.parties);
      if (ret.Error != Error.WRN_CLAN_NOT_JOINED || !MonoBehaviourSingleton<ClanMatchingManager>.IsValid() || MonoBehaviourSingleton<ClanMatchingManager>.I.clanData == null)
        return;
      this.Kick(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
    }));
  }

  private void ClearClan()
  {
    this.partyData = (PartyModel.Party) null;
    this.clanServerData = (ClanServer) null;
    this.loungeMemberStatus = (LoungeMemberesStatus) null;
    if (!MonoBehaviourSingleton<ClanNetworkManager>.IsValid())
      return;
    MonoBehaviourSingleton<ClanNetworkManager>.I.Close();
  }

  public static bool IsValidInClan()
  {
    return MonoBehaviourSingleton<ClanMatchingManager>.IsValid() && MonoBehaviourSingleton<ClanMatchingManager>.I.IsInClan();
  }

  public bool IsUserInClanBase(int user_id)
  {
    PartyModel.SlotInfo slotInfoByUserId = this.GetSlotInfoByUserId(user_id);
    return ClanMatchingManager.IsValidInClan() && slotInfoByUserId != null && slotInfoByUserId.userInfo != null;
  }

  public bool IsInClan() => MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsInBase();

  public string GetPartyId() => this.partyData == null ? "" : this.partyData.id;

  public int GetSlotIndex(int user_id)
  {
    return this.partyData == null ? -1 : this.partyData.slotInfos.FindIndex((Predicate<PartyModel.SlotInfo>) (s => s.userInfo != null && s.userInfo.userId == user_id));
  }

  public PARTY_STATUS GetStatus()
  {
    return this.partyData == null ? PARTY_STATUS.NONE : (PARTY_STATUS) this.partyData.status;
  }

  public PartyModel.SlotInfo GetSlotInfoByIndex(int idx)
  {
    return this.partyData != null && idx < this.partyData.slotInfos.Count ? this.partyData.slotInfos[idx] : (PartyModel.SlotInfo) null;
  }

  public PartyModel.SlotInfo GetSlotInfoByUserId(int user_id)
  {
    int slotIndex = this.GetSlotIndex(user_id);
    return slotIndex < 0 ? (PartyModel.SlotInfo) null : this.GetSlotInfoByIndex(slotIndex);
  }

  public int GetMemberCount()
  {
    if (this.partyData == null)
      return 0;
    int memberCount = 0;
    for (int index = 0; index < this.partyData.slotInfos.Count; ++index)
    {
      if (this.partyData.slotInfos[index].userInfo != null)
        ++memberCount;
    }
    return memberCount;
  }

  public static string GenerateToken() => Guid.NewGuid().ToString().Replace("-", "");

  public void SendInfo(Action<bool> call_back, bool force = false)
  {
    if (force)
      Protocol.Force((System.Action) (() => this.DoSendInfo(call_back)));
    else
      this.DoSendInfo(call_back);
  }

  private void DoSendInfo(Action<bool> call_back)
  {
    Protocol.Send<ClanBaseInfoModel>(ClanBaseInfoModel.URL, (Action<ClanBaseInfoModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        if (ret.result != null && ret.result.clanParty != null && ret.result.clanServer != null)
        {
          flag = true;
          this.UpdateParty(ret.result.clanParty, ret.result.clanServer);
          this.Dirty();
        }
        else
        {
          this.StopAFKCheck();
          this.ClearClan();
        }
      }
      else
      {
        this.StopAFKCheck();
        this.ClearClan();
      }
      call_back(flag);
    }));
  }

  public void SendEnterToClanBase(Action<bool> call_back)
  {
    this.ClearClan();
    Protocol.Send<ClanEnterBaseModel>(ClanEnterBaseModel.URL, (Action<ClanEnterBaseModel>) (ret =>
    {
      if (ret.Error != Error.None)
        return;
      this.UpdateParty(ret.result.clanParty, ret.result.clanServer);
      MonoBehaviourSingleton<ClanManager>.I.SetNoticeBoardData(ret.result.clanNoticeBoard, false);
      int num = PlayerPrefs.GetInt("CLAN_BOARD_READ_ID_KEY", -1);
      if (MonoBehaviourSingleton<GameSceneManager>.IsValid() && ret.result.clanNoticeBoard != null && ret.result.clanNoticeBoard.version > num)
        this.PopupNoticeBoard();
      call_back(true);
    }));
  }

  public void SendLeaveFromClanBase(Action<bool> call_back)
  {
    if (this.partyData == null)
      call_back(false);
    else
      Protocol.Send<ClanLeaveBaseModel>(ClanLeaveBaseModel.URL, (Action<ClanLeaveBaseModel>) (ret =>
      {
        bool flag = false;
        switch (ret.Error)
        {
          case Error.None:
          case Error.ERR_PARTY_NOT_FOUND_PARTY:
            if (MonoBehaviourSingleton<ClanManager>.IsValid())
            {
              flag = true;
              this.DoLeaveFromClanBase(call_back, ret);
            }
            call_back(flag);
            break;
          default:
            call_back(flag);
            break;
        }
      }));
  }

  private void DoLeaveFromClanBase(Action<bool> call_back, ClanLeaveBaseModel ret)
  {
    this.SendLeavePacket();
    this.StopAFKCheck();
    this.ClearClan();
    this.Dirty();
  }

  public void SendInClanBase()
  {
    if (!CoopWebSocketSingleton<LoungeWebSocket>.IsValidConnected())
      return;
    Lounge_Model_MemberLounge model = new Lounge_Model_MemberLounge();
    model.id = 1005;
    model.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    MonoBehaviourSingleton<ClanNetworkManager>.I.SendBroadcast<Lounge_Model_MemberLounge>(model);
  }

  public void SendStartField()
  {
    if (!CoopWebSocketSingleton<LoungeWebSocket>.IsValidConnected() || !FieldManager.IsValidInField())
      return;
    Lounge_Model_MemberField model = new Lounge_Model_MemberField();
    model.id = 1005;
    model.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    int.TryParse(MonoBehaviourSingleton<FieldManager>.I.GetFieldId(), out model.fid);
    model.fmid = MonoBehaviourSingleton<FieldManager>.I.GetMapId();
    if (MonoBehaviourSingleton<PartyManager>.I.IsInParty())
    {
      int.TryParse(MonoBehaviourSingleton<PartyManager>.I.GetPartyId(), out model.pid);
      model.qid = (int) MonoBehaviourSingleton<PartyManager>.I.GetQuestId();
      model.h = MonoBehaviourSingleton<PartyManager>.I.GetOwnerUserId() == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    }
    MonoBehaviourSingleton<ClanNetworkManager>.I.SendBroadcast<Lounge_Model_MemberField>(model);
  }

  public void SendStartQuest(PartyModel.Party party)
  {
    if (!CoopWebSocketSingleton<LoungeWebSocket>.IsValidConnected() || !MonoBehaviourSingleton<PartyManager>.IsValid())
      return;
    Lounge_Model_MemberQuest model = new Lounge_Model_MemberQuest();
    model.id = 1005;
    model.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    int.TryParse(party.id, out model.pid);
    model.qid = party.quest.questId;
    model.h = party.ownerUserId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    MonoBehaviourSingleton<ClanNetworkManager>.I.SendBroadcast<Lounge_Model_MemberQuest>(model);
  }

  public void SendStartArena(int arenaId)
  {
    if (!CoopWebSocketSingleton<LoungeWebSocket>.IsValidConnected())
      return;
    Lounge_Model_MemberArena model = new Lounge_Model_MemberArena();
    model.id = 1005;
    model.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    model.aid = arenaId;
    MonoBehaviourSingleton<ClanNetworkManager>.I.SendBroadcast<Lounge_Model_MemberArena>(model);
  }

  public void SetClanCreateRequest(ClanSettings.CreateRequestParam request = null)
  {
    if (request != null)
    {
      this.createRequest = request;
    }
    else
    {
      if (this.createRequest != null)
        return;
      this.ResetClanCreateRequest();
    }
  }

  public void ResetClanCreateRequest()
  {
    this.createRequest = new ClanSettings.CreateRequestParam();
  }

  public void SendCreate(Action<bool, Error> call_back)
  {
    this.ClearClan();
    Protocol.Send<ClanCreateModel.RequestSendForm, ClanCreateModel>(ClanCreateModel.URL, new ClanCreateModel.RequestSendForm()
    {
      name = this.createRequest.clanName,
      jt = this.createRequest.isLock ? 1 : 0,
      lbl = (int) this.createRequest.label,
      cmt = this.createRequest.comment,
      tag = this.createRequest.clanTag,
      crystalCL = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal
    }, (Action<ClanCreateModel>) (ret =>
    {
      bool flag = false;
      switch (ret.Error)
      {
        case Error.None:
          flag = true;
          this.UpdateParty(ret.result.clanParty, ret.result.clanServer);
          this.isClanCreatedNow = true;
          this.Dirty();
          break;
      }
      call_back(flag, ret.Error);
    }));
  }

  public int GetOwnerUserId() => this.partyData == null ? 0 : this.partyData.ownerUserId;

  public void RequestApply(ClanApplyModel.RequestSendForm send_param, Action<bool> call_back)
  {
    Protocol.Send<ClanApplyModel.RequestSendForm, ClanApplyModel>(ClanApplyModel.URL, send_param, (Action<ClanApplyModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        if (ret.result.clanParty != null && ret.result.clanServer != null)
          this.UpdateParty(ret.result.clanParty, ret.result.clanServer);
      }
      call_back(flag);
    }));
  }

  public void RequestApplyCancel(string clanId, Action<bool> call_back)
  {
    Protocol.Send<ClanApplyCancelModel.RequestSendForm, ClanApplyCancelModel>(ClanApplyCancelModel.URL, new ClanApplyCancelModel.RequestSendForm()
    {
      cId = clanId
    }, (Action<ClanApplyCancelModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      call_back(flag);
    }));
  }

  public void RequestEdit(ClanEditClanModel.RequestSendForm clanSetting, Action<bool> call_back)
  {
    ClanEditClanModel.RequestSendForm postData = clanSetting;
    Protocol.Send<ClanEditClanModel.RequestSendForm, ClanModel>(ClanEditClanModel.URL, postData, (Action<ClanModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        this.UpdateClan(ret.result);
      }
      call_back(flag);
    }));
  }

  private void SendLeavePacket()
  {
    if (!MonoBehaviourSingleton<ClanManager>.IsValid())
      return;
    Lounge_Model_RoomLeaved model = new Lounge_Model_RoomLeaved();
    model.id = 1005;
    model.token = ClanMatchingManager.GenerateToken();
    model.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    MonoBehaviourSingleton<ClanNetworkManager>.I.SendBroadcast<Lounge_Model_RoomLeaved>(model, onReceiveAck: (Func<Coop_Model_ACK, bool>) (ack =>
    {
      this.ClearClan();
      this.StopAFKCheck();
      return true;
    }));
  }

  public void RequestLeave(Action<bool> call_back)
  {
    Protocol.Send<ClanModel>(ClanLeaveModel.URL, (Action<ClanModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        this.SendLeavePacket();
      }
      call_back(flag);
    }));
  }

  public void RequestKick(ClanKickModel.RequestSendForm send_param, Action<bool> call_back)
  {
    Protocol.Send<ClanKickModel.RequestSendForm, ClanModel>(ClanKickModel.URL, send_param, (Action<ClanModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        MonoBehaviourSingleton<ClanNetworkManager>.I.SendBroadcast<Lounge_Model_RoomKick>(new Lounge_Model_RoomKick()
        {
          id = 1005,
          token = ClanMatchingManager.GenerateToken(),
          cid = send_param.uId
        });
      }
      call_back(flag);
    }));
  }

  public void RequestEditMember(
    ClanEditMemberModel.RequestSendForm send_param,
    Action<bool> call_back)
  {
    Protocol.Send<ClanEditMemberModel.RequestSendForm, ClanModel>(ClanEditMemberModel.URL, send_param, (Action<ClanModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      call_back(flag);
    }));
  }

  public void RequestEditNoticeBoard(string msg, Action<bool> call_back)
  {
    Protocol.Send<ClanNoticeBoardUpdateModel.RequestSendForm, ClanNoticeBoardUpdateModel>(ClanNoticeBoardUpdateModel.URL, new ClanNoticeBoardUpdateModel.RequestSendForm()
    {
      body = msg
    }, (Action<ClanNoticeBoardUpdateModel>) (ret =>
    {
      bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
      MonoBehaviourSingleton<ClanManager>.I.SetNoticeBoardData(ret.result.clanNoticeBoard, true);
      call_back(flag);
    }));
  }

  public void RequestNoticeBoard(Action<bool> call_back)
  {
    Protocol.Send<ClanNoticeBoardModel>(ClanNoticeBoardModel.URL, (Action<ClanNoticeBoardModel>) (ret =>
    {
      bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
      MonoBehaviourSingleton<ClanManager>.I.SetNoticeBoardData(ret.result.clanNoticeBoard, true);
      call_back(flag);
    }));
  }

  private void PopupNoticeBoard()
  {
    if (this.popupNoticeBoardCoroutine != null)
    {
      this.StopCoroutine(this.popupNoticeBoardCoroutine);
      this.popupNoticeBoardCoroutine = (Coroutine) null;
    }
    if (!this.IsAbleToPopupNoticeBoard())
      this.popupNoticeBoardCoroutine = this.StartCoroutine(this.DelayPlay());
    else
      MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[1]
      {
        new EventData("NOTICE_BOARD", (object) null)
      });
  }

  private IEnumerator DelayPlay()
  {
    int waitCount = 0;
    while (!this.IsAbleToPopupNoticeBoard() || waitCount < 3)
    {
      if (this.IsAbleToPopupNoticeBoard())
        ++waitCount;
      else
        waitCount = 0;
      yield return (object) null;
    }
    MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[1]
    {
      new EventData("NOTICE_BOARD", (object) null)
    });
  }

  private bool IsAbleToPopupNoticeBoard()
  {
    return !MonoBehaviourSingleton<UIManager>.I.IsTransitioning() && !MonoBehaviourSingleton<GameSceneManager>.I.isChangeing && !MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent() && (!MonoBehaviourSingleton<InGameProgress>.IsValid() || !MonoBehaviourSingleton<InGameProgress>.I.isGameProgressStop) && (!MonoBehaviourSingleton<InGameManager>.IsValid() || !MonoBehaviourSingleton<InGameManager>.I.isQuestHappen) && (!MonoBehaviourSingleton<InGameManager>.IsValid() || !MonoBehaviourSingleton<InGameManager>.I.isQuestFromGimmick) && (!MonoBehaviourSingleton<DeliveryManager>.IsValid() || !MonoBehaviourSingleton<DeliveryManager>.I.isNoticeNewDeliveryAtHomeScene) && (!MonoBehaviourSingleton<DeliveryManager>.IsValid() || MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableStoryDelivery() == 0U) && (!MonoBehaviourSingleton<DeliveryManager>.IsValid() || MonoBehaviourSingleton<DeliveryManager>.I.GetEventCleardDeliveryData() == null) && !MonoBehaviourSingleton<UIManager>.I.levelUp.IsWaitDelay() && !MonoBehaviourSingleton<UIManager>.I.levelUp.IsPlaying() && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "ClanScene" && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "ClanTop";
  }

  public List<ClanData> clans { get; private set; }

  public List<ClanData> scoutClans { get; private set; }

  public ClanSearchModel.RequestSendForm searchRequest { get; private set; }

  public static bool IsValidNotEmptyList()
  {
    return MonoBehaviourSingleton<ClanMatchingManager>.IsValid() && MonoBehaviourSingleton<ClanMatchingManager>.I.clans != null && MonoBehaviourSingleton<ClanMatchingManager>.I.clans.Count > 0;
  }

  public static bool IsScoutValidNotEmptyList()
  {
    return MonoBehaviourSingleton<ClanMatchingManager>.IsValid() && MonoBehaviourSingleton<ClanMatchingManager>.I.scoutClans != null && MonoBehaviourSingleton<ClanMatchingManager>.I.scoutClans.Count > 0;
  }

  public void RequestSearch(Action<bool, Error> call_back, bool saveSettings)
  {
    this.clans = (List<ClanData>) null;
    if (this.searchRequest == null)
      this.ResetSearchRequest();
    if (saveSettings)
      this.SaveSearchSettings();
    Protocol.Send<ClanSearchModel.RequestSendForm, ClanSearchModel>(ClanSearchModel.URL, this.searchRequest, (Action<ClanSearchModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        this.UpdateSearchList(ret.result);
      }
      call_back(flag, ret.Error);
    }));
  }

  public void SetSearchRequest(ClanSearchModel.RequestSendForm request)
  {
    if (request != null)
      this.searchRequest = request;
    else
      this.ResetSearchRequest();
  }

  public void ResetSearchRequest() => this.searchRequest = new ClanSearchModel.RequestSendForm();

  private void UpdateRoomQuest(List<PartyModel.Party> parties) => this.clanRoomParties = parties;

  public void LoadSearchRequestFromPrefs()
  {
    this.searchRequest = new ClanSearchModel.RequestSendForm();
    this.searchRequest.jt = PlayerPrefs.GetInt("CLAN_SEARCH_JT_KEY", -1);
    this.searchRequest.lbl = PlayerPrefs.GetInt("CLAN_SEARCH_LBL_KEY", 0);
    this.searchRequest.isCF = PlayerPrefs.GetInt("CLAN_SEARCH_ISCF_KEY", 0);
  }

  private void SaveSearchSettings()
  {
    PlayerPrefs.SetInt("CLAN_SEARCH_JT_KEY", this.searchRequest.jt);
    PlayerPrefs.SetInt("CLAN_SEARCH_LBL_KEY", this.searchRequest.lbl);
    PlayerPrefs.SetInt("CLAN_SEARCH_ISCF_KEY", this.searchRequest.isCF);
    PlayerPrefs.Save();
  }

  private void UpdateSearchList(List<ClanData> clans)
  {
    this.clans = clans;
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SEARCH_ROOM_LIST);
  }

  public void RequestDetail(string clanId, Action<ClanDetailModel.Param> call_back)
  {
    Protocol.Send<ClanDetailModel.RequestSendForm, ClanDetailModel>(ClanDetailModel.URL, new ClanDetailModel.RequestSendForm()
    {
      cId = clanId
    }, (Action<ClanDetailModel>) (ret =>
    {
      if (ret.Error != Error.None)
        return;
      if (clanId == "0")
        this.UpdateClan(ret.result.clan);
      call_back(ret.result);
    }));
  }

  public void RequestUserDetail(int uId, Action<UserClanData> call_back)
  {
    Protocol.Send<ClanUserDetailModel.RequestSendForm, ClanUserDetailModel>(ClanUserDetailModel.URL, new ClanUserDetailModel.RequestSendForm()
    {
      uId = uId
    }, (Action<ClanUserDetailModel>) (ret =>
    {
      if (ret.Error != Error.None)
        return;
      this.userClanData = ret.result;
      call_back(ret.result);
    }));
  }

  public void Kick(int userId)
  {
    if (userId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
      this.partyData.slotInfos.Remove(this.GetSlotInfoByUserId(userId));
    if (userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
    {
      this.ClearClan();
      MonoBehaviourSingleton<UserInfoManager>.I.LeaveClan();
      this.StopAFKCheck();
    }
    if (!MonoBehaviourSingleton<ClanManager>.IsValid())
      return;
    MonoBehaviourSingleton<ClanManager>.I.OnRecvRoomKick(userId);
  }

  public void AFKKick(int userId)
  {
    if (userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
    {
      this.ClearClan();
      this.StopAFKCheck();
    }
    if (!MonoBehaviourSingleton<ClanManager>.IsValid())
      return;
    MonoBehaviourSingleton<ClanManager>.I.OnRecvRoomAFKKick(userId);
  }

  public ClanNetworkManager.ConnectData GetWebSockConnectData()
  {
    if (this.partyData == null || this.clanServerData == null)
    {
      Log.Error(LOG.WEBSOCK, "NotFound ConnectData");
      return (ClanNetworkManager.ConnectData) null;
    }
    int id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    int slotIndex = this.GetSlotIndex(id);
    if (slotIndex < 0)
      return (ClanNetworkManager.ConnectData) null;
    return new ClanNetworkManager.ConnectData()
    {
      path = this.clanServerData.wsHost,
      ports = this.clanServerData.wsPorts,
      fromId = id,
      ackPrefix = slotIndex,
      roomId = this.partyData.id,
      owner = this.partyData.ownerUserId,
      ownerToken = this.clanServerData.token,
      uid = id,
      signature = this.clanServerData.signature
    };
  }

  public void ConnectServer()
  {
    ClanNetworkManager.ConnectData webSockConnectData = this.GetWebSockConnectData();
    if (webSockConnectData == null)
      this.TryConnect(false, false);
    else if (!MonoBehaviourSingleton<ClanNetworkManager>.IsValid())
      this.TryConnect(false, false);
    else
      MonoBehaviourSingleton<ClanNetworkManager>.I.ConnectAndRegist(webSockConnectData, (Action<bool, bool>) ((is_connect, is_regist) => this.TryConnect(is_connect, is_regist)));
  }

  public void OnRecvMemberMoveLounge(int userId)
  {
    if (this.loungeMemberStatus == null)
      return;
    LoungeMemberStatus loungeMemberStatu = this.loungeMemberStatus[userId];
    LoungeMemberStatus.MEMBER_STATUS status = loungeMemberStatu.GetStatus();
    if (loungeMemberStatu != null)
    {
      loungeMemberStatu.ToLounge();
      this.OnChangeMemberStatus.SafeInvoke<LoungeMemberStatus>(loungeMemberStatu);
      if (MonoBehaviourSingleton<ClanNetworkManager>.IsValid())
        MonoBehaviourSingleton<ClanNetworkManager>.I.MoveLoungeNotification(status, loungeMemberStatu);
    }
    this.AFKCheck();
  }

  public void OnRecvMemberMoveField(Lounge_Model_MemberField model)
  {
    if (this.loungeMemberStatus == null)
      return;
    LoungeMemberStatus loungeMemberStatu = this.loungeMemberStatus[model.cid];
    LoungeMemberStatus.MEMBER_STATUS status = loungeMemberStatu.GetStatus();
    if (loungeMemberStatu != null)
    {
      loungeMemberStatu.ToField(model.fid.ToString(), model.fmid, model.pid.ToString(), model.qid, model.h);
      this.OnChangeMemberStatus.SafeInvoke<LoungeMemberStatus>(loungeMemberStatu);
      if (MonoBehaviourSingleton<ClanNetworkManager>.IsValid())
        MonoBehaviourSingleton<ClanNetworkManager>.I.MoveLoungeNotification(status, loungeMemberStatu);
    }
    this.AFKCheck();
  }

  public void OnRecvMemberMoveQuest(Lounge_Model_MemberQuest model)
  {
    if (this.loungeMemberStatus == null)
      return;
    LoungeMemberStatus loungeMemberStatu = this.loungeMemberStatus[model.cid];
    LoungeMemberStatus.MEMBER_STATUS status = loungeMemberStatu.GetStatus();
    if (loungeMemberStatu != null)
    {
      loungeMemberStatu.ToQuest(model.pid.ToString(), model.qid, model.h);
      this.OnChangeMemberStatus.SafeInvoke<LoungeMemberStatus>(loungeMemberStatu);
      if (MonoBehaviourSingleton<ClanNetworkManager>.IsValid())
        MonoBehaviourSingleton<ClanNetworkManager>.I.MoveLoungeNotification(status, loungeMemberStatu);
    }
    this.AFKCheck();
  }

  public void OnRecvMemberMoveArena(Lounge_Model_MemberArena model)
  {
    if (this.loungeMemberStatus == null)
      return;
    LoungeMemberStatus loungeMemberStatu = this.loungeMemberStatus[model.cid];
    LoungeMemberStatus.MEMBER_STATUS status = loungeMemberStatu.GetStatus();
    loungeMemberStatu.ToArena(model.aid);
    if (loungeMemberStatu != null)
    {
      this.OnChangeMemberStatus.SafeInvoke<LoungeMemberStatus>(loungeMemberStatu);
      if (MonoBehaviourSingleton<ClanNetworkManager>.IsValid())
        MonoBehaviourSingleton<ClanNetworkManager>.I.MoveLoungeNotification(status, loungeMemberStatu);
    }
    this.AFKCheck();
  }

  public void DebugSendRoomPartyAFKKick(int userId)
  {
    this.SendRoomPartyAFKKick(userId, (Action<bool>) (b => GameSceneGlobalSettings.GetCurrentIHomeManager().IHomePeople.CastToLoungePeople()?.DestroyLoungePlayer(userId)));
  }

  public void SendRoomPartyAFKKick(int kickedUserId, Action<bool> call_back = null)
  {
    Protocol.Send<ClanKickBaseModel.RequestSendForm, ClanKickBaseModel>(ClanKickBaseModel.URL, new ClanKickBaseModel.RequestSendForm()
    {
      uId = kickedUserId
    }, (Action<ClanKickBaseModel>) (ret =>
    {
      bool flag = ret.Error == Error.None;
      if (flag)
      {
        if (ret.Error == Error.None)
        {
          if (ret.result.clanParty.slotInfos.Find((Predicate<PartyModel.SlotInfo>) (s => s.userInfo != null && s.userInfo.userId == kickedUserId)) != null)
            this.loungeMemberStatus[kickedUserId].UpdateLastExecTime(TimeManager.GetNow().ToUniversalTime());
          else
            MonoBehaviourSingleton<ClanNetworkManager>.I.SendBroadcast<Lounge_Model_AFK_Kick>(new Lounge_Model_AFK_Kick()
            {
              id = 1005,
              cid = kickedUserId,
              token = ClanMatchingManager.GenerateToken()
            });
          this.UpdateParty(ret.result.clanParty, (ClanServer) null);
        }
        else
          call_back(flag);
      }
      call_back(flag);
    }));
  }

  private void AFKCheck()
  {
    if (this.afkCoroutine != null)
      this.StopCoroutine(this.afkCoroutine);
    this.afkCoroutine = this.StartCoroutine(this.DoAFKCheck());
  }

  private IEnumerator DoAFKCheck()
  {
    if (ClanMatchingManager.IsValidInClan() && this.loungeMemberStatus != null)
    {
      List<LoungeMemberStatus> all = this.loungeMemberStatus.GetAll();
      if (!all.IsNullOrEmpty<LoungeMemberStatus>() && all.Where<LoungeMemberStatus>((Func<LoungeMemberStatus, bool>) (x => x.userId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)).Any<LoungeMemberStatus>())
      {
        LoungeMemberStatus fastest = all.Where<LoungeMemberStatus>((Func<LoungeMemberStatus, bool>) (x => x.userId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)).Where<LoungeMemberStatus>((Func<LoungeMemberStatus, bool>) (x => x.GetStatus() == LoungeMemberStatus.MEMBER_STATUS.LOUNGE)).OrderBy<LoungeMemberStatus, DateTime>((Func<LoungeMemberStatus, DateTime>) (x => x.lastExecTime)).FirstOrDefault<LoungeMemberStatus>();
        if (fastest != null)
        {
          double totalSeconds = (this.AFK_KICK_TIME - (TimeManager.GetNow().ToUniversalTime() - fastest.lastExecTime)).TotalSeconds;
          if (totalSeconds > 0.0)
            yield return (object) new WaitForSeconds((float) totalSeconds);
          bool wait = true;
          Protocol.Force((System.Action) (() => this.SendRoomPartyAFKKick(fastest.userId, (Action<bool>) (is_sucess =>
          {
            GameSceneGlobalSettings.GetCurrentIHomeManager().IHomePeople.CastToLoungePeople()?.DestroyLoungePlayer(fastest.userId);
            wait = false;
          }))));
          while (wait)
            yield return (object) null;
          this.AFKCheck();
        }
      }
    }
  }

  public void StopAFKCheck()
  {
    if (this.afkCoroutine == null)
      return;
    this.StopCoroutine(this.afkCoroutine);
  }

  public bool EnableClanChat
  {
    get
    {
      return MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.userClan != null && MonoBehaviourSingleton<UserInfoManager>.I.userClan.stat > 0;
    }
  }

  public event Action<ClanChatMessageModel> OnReceiveCharacterMessage;

  public ChatClanConnection GetChatConnection()
  {
    if (this.chatConnection == null)
    {
      this.chatConnection = new ChatClanConnection();
      this.LastReadMessageId = PlayerPrefs.GetString("CLAN_CHAT_LAST_READ_ID_KEY", "1");
    }
    return this.chatConnection;
  }

  public void ChatResetCache() => this.chatMessagesDESC.Clear();

  public string ChatGetLatestCacheId()
  {
    return this.chatMessagesDESC.Count > 0 ? this.chatMessagesDESC[0].id : "";
  }

  public void OnReadMessage(string messageId)
  {
    long result1 = 0;
    long result2 = 0;
    if (!long.TryParse(this.LastReadMessageId, out result1) || !long.TryParse(messageId, out result2) || result2 <= result1)
      return;
    this.LastReadMessageId = messageId;
    PlayerPrefs.SetString("CLAN_CHAT_LAST_READ_ID_KEY", this.LastReadMessageId);
    PlayerPrefs.Save();
    this.UnreadMessageCount = 0;
  }

  public void UpdateUnreadMessage()
  {
    if (Application.internetReachability == null || !this.EnableClanChat || this.UsingChatConnection || (double) this.UnreadCountSyncFixedTime > 0.0 && (double) this.UnreadCountSyncFixedTime + (double) this.chatUpdateInterval > (double) Time.fixedTime)
      return;
    this.UnreadCountSyncFixedTime = Time.fixedTime;
    this.UpdateUnreadMessageCount((System.Action) (() => this.UpdateUnreadCharacterMessage((System.Action) (() => { }))));
  }

  private void UpdateUnreadMessageCount(System.Action callback)
  {
    ClanChatMessageUpdateModel.RequestSendForm send_form = new ClanChatMessageUpdateModel.RequestSendForm();
    send_form.cLatestId = this.LastReadMessageId;
    Protocol.Try((System.Action) (() =>
    {
      this.UsingChatConnection = true;
      Protocol.Send<ClanChatMessageUpdateModel.RequestSendForm, ClanChatMessageUpdateModel>(ClanChatMessageUpdateModel.URL, send_form, (Action<ClanChatMessageUpdateModel>) (ret =>
      {
        this.UsingChatConnection = false;
        if (ret.Error == Error.None)
        {
          int num = 0;
          for (int index = 0; index < ret.result.messages.Count; ++index)
          {
            if (ret.result.messages[index].userId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
              ++num;
          }
          this.UnreadMessageCount = num;
          this.checkMessageType(ret.result.messages);
        }
        if (callback == null)
          return;
        callback();
      }));
    }));
  }

  private void UpdateUnreadCharacterMessage(System.Action callback)
  {
    ClanChatMessageUpdateModel.RequestSendForm send_form = new ClanChatMessageUpdateModel.RequestSendForm();
    send_form.cLatestId = this.LastReadCharacterMessageId;
    Protocol.Try((System.Action) (() =>
    {
      this.UsingChatConnection = true;
      Protocol.Send<ClanChatMessageUpdateModel.RequestSendForm, ClanChatMessageUpdateModel>(ClanChatMessageUpdateModel.URL, send_form, (Action<ClanChatMessageUpdateModel>) (ret =>
      {
        this.UsingChatConnection = false;
        if (ret.Error == Error.None)
        {
          this.checkMessageType(ret.result.messages);
          for (int index = ret.result.messages.Count - 1; index >= 0; --index)
          {
            if (this.OnReceiveCharacterMessage != null)
              this.OnReceiveCharacterMessage(ret.result.messages[index]);
            this.LastReadCharacterMessageId = ret.result.messages[index].id;
          }
        }
        if (callback == null)
          return;
        callback();
      }));
    }));
  }

  public void ChatMessage(string message)
  {
    if (!this.EnableClanChat)
      return;
    string str1 = "0";
    if (this.chatMessagesDESC != null && this.chatMessagesDESC.Count > 0)
      str1 = this.chatMessagesDESC[0].id;
    ClanChatPostMessageModel.RequestSendForm send_form = new ClanChatPostMessageModel.RequestSendForm();
    string str2 = message;
    if (str2.Length > 32 /*0x20*/)
      str2 = str2.Substring(0, 32 /*0x20*/);
    send_form.message = str2;
    send_form.cLatestId = str1;
    Protocol.Try((System.Action) (() =>
    {
      this.UsingChatConnection = true;
      Protocol.Send<ClanChatPostMessageModel.RequestSendForm, ClanChatPostMessageModel>(ClanChatPostMessageModel.URL, send_form, (Action<ClanChatPostMessageModel>) (ret =>
      {
        this.UsingChatConnection = false;
        if (ret.Error != Error.None)
          return;
        List<ClanChatMessageModel> chatMessagesDesc = this.chatMessagesDESC;
        this.chatMessagesDESC = ret.result.messages;
        this.chatMessagesDESC.AddRange((IEnumerable<ClanChatMessageModel>) chatMessagesDesc);
        this.chatConnection.OnAfterSendUserMessage();
      }));
    }));
  }

  public void ChatStamp(int stamp_id)
  {
    this.ChatMessage(ClanMatchingManager.convertStampIdToString(stamp_id));
  }

  public void ChatGetNewMessage(int maxDispatchNum, string latestId = "")
  {
    if (!this.EnableClanChat || this.UsingChatConnection || !string.IsNullOrEmpty(latestId) && this.dispatchEventNewMessageFromChache(maxDispatchNum, latestId))
      return;
    this.chatGetLatestMessageFromServer((System.Action) (() => this.dispatchEventNewMessageFromChache(maxDispatchNum, latestId)));
  }

  private bool dispatchEventNewMessageFromChache(int maxDispatchNum, string latestId = "")
  {
    bool flag = false;
    if (string.IsNullOrEmpty(latestId))
      flag = true;
    int num = 0;
    for (int index = this.chatMessagesDESC.Count - 1; index >= 0; --index)
    {
      if (flag)
      {
        this.dispatchEventChatReceiveNew(this.chatMessagesDESC[index]);
        ++num;
        if (num >= maxDispatchNum)
          break;
      }
      else if (this.chatMessagesDESC[index].id == latestId)
        flag = true;
    }
    return num > 0;
  }

  private void chatGetLatestMessageFromServer(System.Action callback)
  {
    if (!this.EnableClanChat)
      return;
    string str = "0";
    if (this.chatMessagesDESC != null && this.chatMessagesDESC.Count > 0)
      str = this.chatMessagesDESC[0].id;
    this.CachedMessageClanId = MonoBehaviourSingleton<UserInfoManager>.I.userClan.cId;
    ClanChatMessageUpdateModel.RequestSendForm send_form = new ClanChatMessageUpdateModel.RequestSendForm();
    send_form.cLatestId = str;
    Protocol.Try((System.Action) (() =>
    {
      this.UsingChatConnection = true;
      Protocol.Send<ClanChatMessageUpdateModel.RequestSendForm, ClanChatMessageUpdateModel>(ClanChatMessageUpdateModel.URL, send_form, (Action<ClanChatMessageUpdateModel>) (ret =>
      {
        this.UsingChatConnection = false;
        if (ret.Error != Error.None)
          return;
        int count = this.chatMessagesDESC.Count;
        List<ClanChatMessageModel> chatMessagesDesc = this.chatMessagesDESC;
        this.chatMessagesDESC = ret.result.messages;
        this.chatMessagesDESC.AddRange((IEnumerable<ClanChatMessageModel>) chatMessagesDesc);
        this.chatUpdateInterval = ret.result.updateInterval;
        if (callback == null || this.chatMessagesDESC.Count <= count)
          return;
        callback();
      }));
    }));
  }

  private void dispatchEventChatReceiveNew(ClanChatMessageModel model)
  {
    if (model.type != 1)
    {
      this.chatConnection.OnReceiveNotification(this.convertSystemMessage(model), model.id);
    }
    else
    {
      int stampId = ClanMatchingManager.convertStringToStampId(model.body);
      if (stampId >= 0)
        this.chatConnection.OnReceiveStamp(model.userId, this.convertUsername(model), stampId, model.id);
      else
        this.chatConnection.OnReceiveMessage(model.userId, this.convertUsername(model), model.body, model.id);
    }
  }

  private void checkMessageType(List<ClanChatMessageModel> messages)
  {
    if (messages.IsNullOrEmpty<ClanChatMessageModel>())
      return;
    bool flag1 = false;
    int num1 = PlayerPrefs.GetInt("CLAN_CHAT_READ_ID_KEY", -1);
    for (int index = messages.Count - 1; index >= 0; --index)
    {
      ClanChatMessageModel message = messages[index];
      int num2 = int.Parse(message.id);
      if (num2 > num1)
      {
        bool flag2 = false;
        switch ((CLAN_MESSAGE_TYPE) message.type)
        {
          case CLAN_MESSAGE_TYPE.CLAN_LEVELUP:
            flag2 = true;
            if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "ClanScene")
            {
              if (message.value > 0)
              {
                this.StartRankUp();
                break;
              }
              if (MonoBehaviourSingleton<UIManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.clanCreate, (Object) null))
              {
                MonoBehaviourSingleton<UIManager>.I.clanCreate.Play(type: UIClanCreateAnnounce.eType.LevelUp);
                this.StartRequestClanData((System.Action) (() =>
                {
                  PlayerPrefs.SetInt("CLAN_LAST_LEVEL_KEY", MonoBehaviourSingleton<ClanMatchingManager>.I.userClanData.level);
                  PlayerPrefs.Save();
                }));
                break;
              }
              break;
            }
            break;
          case CLAN_MESSAGE_TYPE.CLAN_DELIVERY_COMPLETE:
            this.StartRequestClanData();
            if (MonoBehaviourSingleton<UIAnnounceBand>.IsValid())
            {
              flag2 = true;
              MonoBehaviourSingleton<UIAnnounceBand>.I.SetAnnounce(message.body, StringTable.Get(STRING_CATEGORY.CLAN, 3U));
              break;
            }
            break;
        }
        if (flag2)
        {
          num1 = num2;
          PlayerPrefs.SetInt("CLAN_CHAT_READ_ID_KEY", num2);
          flag1 = true;
        }
      }
    }
    if (!flag1)
      return;
    PlayerPrefs.Save();
  }

  public void StartRequestClanData(System.Action cb = null)
  {
    this.StartCoroutine(this.RequestClanData(cb));
  }

  private IEnumerator RequestClanData(System.Action cb)
  {
    bool wait = true;
    Protocol.Force((System.Action) (() => this.RequestUserDetail(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, (Action<UserClanData>) (userClandata =>
    {
      this.userClanData = userClandata;
      MonoBehaviourSingleton<UserInfoManager>.I.SetUserClan(userClandata);
      wait = false;
    }))));
    while (wait)
      yield return (object) null;
    if (cb != null)
      cb();
  }

  public void StartRankUp() => this.StartCoroutine(this._RankUp());

  private IEnumerator _RankUp()
  {
    while (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() != "ClanTop")
      yield return (object) null;
    while (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
      yield return (object) null;
    MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("ClanTop", ((Component) this).gameObject, "ROOM_RANKUP");
  }

  public void ChatGetOldMessage(int maxDispatchNum, string oldestId = "")
  {
    if (!MonoBehaviourSingleton<UserInfoManager>.IsValid() || MonoBehaviourSingleton<UserInfoManager>.I.userClan == null || MonoBehaviourSingleton<UserInfoManager>.I.userClan.stat == 0 || this.UsingChatConnection || !string.IsNullOrEmpty(oldestId) && this.dispatchChatOldMessageFromChache(maxDispatchNum, oldestId))
      return;
    this.chatGetOldestMessageFromServer((System.Action) (() => this.dispatchChatOldMessageFromChache(maxDispatchNum, oldestId)));
  }

  private bool dispatchChatOldMessageFromChache(int maxDispatchNum, string oldestId = "")
  {
    bool flag = false;
    if (string.IsNullOrEmpty(oldestId))
      flag = true;
    int num = 0;
    for (int index = 0; index < this.chatMessagesDESC.Count; ++index)
    {
      if (flag)
      {
        this.dispatchEventChatReceiveOld(this.chatMessagesDESC[index]);
        ++num;
        if (num >= maxDispatchNum)
          break;
      }
      else if (this.chatMessagesDESC[index].id == oldestId)
        flag = true;
    }
    return num > 0;
  }

  private void chatGetOldestMessageFromServer(System.Action callback)
  {
    if (!MonoBehaviourSingleton<UserInfoManager>.IsValid() || MonoBehaviourSingleton<UserInfoManager>.I.userClan == null || MonoBehaviourSingleton<UserInfoManager>.I.userClan.stat == 0)
      return;
    string str = "0";
    if (this.chatMessagesDESC != null && this.chatMessagesDESC.Count > 0)
      str = this.chatMessagesDESC[this.chatMessagesDESC.Count - 1].id;
    Protocol.Try((System.Action) (() => Protocol.Send<ClanChatMessageHistoryModel.RequestSendForm, ClanChatMessageUpdateModel>(ClanChatMessageHistoryModel.URL, new ClanChatMessageHistoryModel.RequestSendForm()
    {
      fromId = str
    }, (Action<ClanChatMessageUpdateModel>) (ret =>
    {
      if (ret.Error != Error.None)
        return;
      this.chatUpdateInterval = ret.result.updateInterval;
      if (ret.result.messages.Count <= 0)
        return;
      this.chatMessagesDESC.AddRange((IEnumerable<ClanChatMessageModel>) ret.result.messages);
      if (callback == null)
        return;
      callback();
    }))));
  }

  private void dispatchEventChatReceiveOld(ClanChatMessageModel model)
  {
    if (model.type != 1)
    {
      this.chatConnection.OnReceiveNotificationOld(this.convertSystemMessage(model), model.id);
    }
    else
    {
      int stampId = ClanMatchingManager.convertStringToStampId(model.body);
      if (stampId >= 0)
        this.chatConnection.OnReceiveStampOld(model.userId, this.convertUsername(model), stampId, model.id);
      else
        this.chatConnection.OnReceiveMessageOld(model.userId, this.convertUsername(model), model.body, model.id);
    }
  }

  public static string convertStampIdToString(int stamp_id) => "[STAMP]" + stamp_id.ToString();

  public static int convertStringToStampId(string message)
  {
    if (message == null || !message.StartsWith("[STAMP]"))
      return -1;
    string s = message.Substring("[STAMP]".Length, message.Length - "[STAMP]".Length);
    if (string.IsNullOrEmpty(s))
      return -1;
    int result = -1;
    return int.TryParse(s, out result) ? result : -1;
  }

  private string convertUsername(ClanChatMessageModel model)
  {
    return this.appendDate(model.userName, model.createdAt);
  }

  private string convertSystemMessage(ClanChatMessageModel model)
  {
    return this.appendDate(model.body, model.createdAt);
  }

  private string appendDate(string txt, int createdAt)
  {
    DateTime dateTime = ClanMatchingManager.dtepoc.AddSeconds((double) (createdAt + 32400));
    return txt + dateTime.ToString(" M/dd HH:mm");
  }

  public string ConvertDateIntToString(string txt, int timeAt) => this.appendDate(txt, timeAt);

  public void SendRequestList(Action<bool, List<FriendCharaInfo>> call_back)
  {
    Protocol.Send<ClanRequestListModel>(ClanRequestListModel.URL, (Action<ClanRequestListModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      if (call_back == null)
        return;
      call_back(flag, ret.result);
    }));
  }

  public void SendAcceptRequest(int userId, Action<bool> call_back)
  {
    Protocol.Send<ClanAcceptRequestModel.RequestSendForm, ClanAcceptRequestModel>(ClanAcceptRequestModel.URL, new ClanAcceptRequestModel.RequestSendForm()
    {
      uId = userId
    }, (Action<ClanAcceptRequestModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      if (call_back == null)
        return;
      call_back(flag);
    }));
  }

  public void SendRejectRequest(int userId, Action<bool> call_back)
  {
    Protocol.Send<ClanRejectRequestModel.RequestSendForm, ClanRejectRequestModel>(ClanRejectRequestModel.URL, new ClanRejectRequestModel.RequestSendForm()
    {
      uId = userId
    }, (Action<ClanRejectRequestModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      if (call_back == null)
        return;
      call_back(flag);
    }));
  }

  public void SendSymbolEditRequest(ClanSymbolData symbol, Action<bool> call_back)
  {
    Protocol.Send<ClanSymbolEditRequestModel.RequestSendForm, ClanSymbolEditRequestModel>(ClanSymbolEditRequestModel.URL, new ClanSymbolEditRequestModel.RequestSendForm()
    {
      mark = symbol.m,
      markOption = symbol.mo,
      frame = symbol.f,
      frameOption = symbol.fo,
      pattern = symbol.p,
      patternOption = symbol.po
    }, (Action<ClanSymbolEditRequestModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      if (call_back == null)
        return;
      call_back(flag);
    }));
  }

  public void SendClanInvite(int userId, Action<bool> call_back)
  {
    Protocol.Send<ClanInviteModel.RequestSendForm, ClanInviteModel>(ClanInviteModel.URL, new ClanInviteModel.RequestSendForm()
    {
      uId = userId
    }, (Action<ClanInviteModel>) (ret =>
    {
      bool flag = false;
      switch (ret.Error)
      {
        case Error.None:
          flag = true;
          break;
        case Error.WRN_CLAN_NO_VACANCY:
          this.clanData.num = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.CLAN_MAX_MEMBER_NUM;
          break;
      }
      if (call_back != null)
        call_back(flag);
      MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_FRIEND_PARAM);
    }));
  }

  public void SendClanInviteList(Action<bool> call_back)
  {
    Protocol.Send<ClanInviteListModel>(ClanInviteListModel.URL, (Action<ClanInviteListModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      this.scoutClans = ret.result;
      MonoBehaviourSingleton<UserInfoManager>.I.SetClanScoutNum(this.scoutClans.Count);
      if (call_back == null)
        return;
      call_back(flag);
    }));
  }

  public void RemoveClanScoutList(string cId)
  {
    this.scoutClans.RemoveAll((Predicate<ClanData>) (c => c.cId.Contains(cId)));
    MonoBehaviourSingleton<UserInfoManager>.I.SetClanScoutNum(this.scoutClans.Count);
  }

  public void SendClanAcceptInvite(int cId, Action<bool> call_back)
  {
    Protocol.Send<ClanAcceptInviteModel.RequestSendForm, ClanAcceptInviteModel>(ClanAcceptInviteModel.URL, new ClanAcceptInviteModel.RequestSendForm()
    {
      cId = cId
    }, (Action<ClanAcceptInviteModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        if (ret.result.clanParty != null && ret.result.clanServer != null)
          this.UpdateParty(ret.result.clanParty, ret.result.clanServer);
      }
      call_back(flag);
    }));
  }

  public void SendClanCancelInvite(int userId, Action<bool> call_back)
  {
    Protocol.Send<ClanCancelInviteModel.RequestSendForm, ClanCancelInviteModel>(ClanCancelInviteModel.URL, new ClanCancelInviteModel.RequestSendForm()
    {
      uId = userId
    }, (Action<ClanCancelInviteModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      if (call_back != null)
        call_back(flag);
      MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_FRIEND_PARAM);
    }));
  }

  public void SendClanRejectInvite(int clanId, Action<bool> call_back)
  {
    Protocol.Send<ClanRejectInviteModel.RequestSendForm, ClanRejectInviteModel>(ClanRejectInviteModel.URL, new ClanRejectInviteModel.RequestSendForm()
    {
      cId = clanId
    }, (Action<ClanRejectInviteModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      call_back(flag);
    }));
  }
}
