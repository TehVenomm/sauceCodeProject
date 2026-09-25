// Decompiled with JetBrains decompiler
// Type: LoungeMatchingManager
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
public class LoungeMatchingManager : MonoBehaviourSingleton<LoungeMatchingManager>
{
  private bool isChangeStarted;
  private Coroutine afkCoroutine;
  private string inviteValue = "";
  public Action<LoungeMemberStatus> OnChangeMemberStatus = (Action<LoungeMemberStatus>) (x => { });
  private ChatLoungeConnection connection;
  private const int RETRY_COUNT = 3;
  private const float RETRY_TIMER = 5f;
  private readonly TimeSpan AFK_KICK_TIME = TimeSpan.FromMinutes(30.0);

  public List<LoungeModel.Lounge> lounges { get; private set; }

  public List<PartyModel.Party> parties { get; private set; }

  public List<LoungeModel.SlotInfo> rallyInvite { get; private set; }

  public LoungeModel.Lounge loungeData { get; private set; }

  public LoungeModel.InviteFriendInfo inviteFriendInfo { get; private set; }

  public string InviteValue
  {
    get => this.inviteValue;
    set => this.inviteValue = value;
  }

  public LoungeConditionSettings.CreateRequestParam createRequest { get; private set; }

  public LoungeSearchSettings.SearchRequestParam searchRequest { get; private set; }

  public List<FollowLoungeMember> followLoungeMember { get; private set; }

  public LoungeModel.LoungeServer loungeServerData { get; private set; }

  public LoungeModel.RandomMatchingInfo randomMatchingInfo { get; private set; }

  public List<int> firstMetUserIds { get; private set; }

  public LoungeMemberesStatus loungeMemberStatus { get; private set; }

  public bool isOpenLounge { get; private set; }

  public bool isKicked { get; private set; }

  public bool isResume { get; private set; }

  public LoungeMatchingManager()
  {
    this.lounges = (List<LoungeModel.Lounge>) null;
    this.loungeData = (LoungeModel.Lounge) null;
    this.loungeServerData = (LoungeModel.LoungeServer) null;
  }

  protected override void Awake()
  {
    base.Awake();
    ((Component) this).gameObject.AddComponent<LoungeWebSocket>();
    ((Component) this).gameObject.AddComponent<LoungeNetworkManager>();
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
      if (this.connection == null)
        return;
      this.isResume = true;
      MonoBehaviourSingleton<ChatManager>.I.DestroyLoungeChat();
      this.StopAFKCheck();
      this.ClearLounge();
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

  public void SetOpenLounge(bool isOpen) => this.isOpenLounge = isOpen;

  private void UpdateLounge(
    LoungeModel.Lounge lounge,
    List<FollowLoungeMember> followLoungeMember,
    LoungeModel.LoungeServer loungeServer,
    LoungeModel.InviteFriendInfo inviteFriendInfo,
    List<int> firstMetUserIds)
  {
    if (this.loungeData != null && this.loungeData.status == 10 && (lounge.status == 100 || lounge.status == 105))
      this.isChangeStarted = true;
    this.inviteFriendInfo = inviteFriendInfo;
    this.firstMetUserIds = firstMetUserIds;
    this.loungeData = lounge;
    if (followLoungeMember != null)
      this.followLoungeMember = followLoungeMember;
    if (loungeServer != null)
    {
      this.loungeServerData = loungeServer;
      if (!MonoBehaviourSingleton<LoungeWebSocket>.I.IsConnected())
      {
        this.connection = MonoBehaviourSingleton<LoungeNetworkManager>.I.CreateChatConnection();
        MonoBehaviourSingleton<ChatManager>.I.CreateLoungeChat((IChatConnection) this.connection);
        this.connection.Join(0, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name);
      }
    }
    if (this.loungeMemberStatus == null)
      return;
    this.loungeMemberStatus.SyncLoungeMember(this.loungeData);
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
    MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast<Lounge_Model_RoomJoined>(model);
    this.SetLoungeMemberesStatus();
    this.AFKCheck();
  }

  private void SetLoungeMemberesStatus()
  {
    if (!MonoBehaviourSingleton<LoungeNetworkManager>.IsValid())
      return;
    this.loungeMemberStatus = new LoungeMemberesStatus(MonoBehaviourSingleton<LoungeNetworkManager>.I.registerAck.GetConvertUserInfo().Where<Party_Model_RegisterACK.UserInfo>((Func<Party_Model_RegisterACK.UserInfo, bool>) (x => this.GetSlotInfoByUserId(x.userId) != null)).ToList<Party_Model_RegisterACK.UserInfo>());
  }

  private void ClearLounge()
  {
    this.loungeData = (LoungeModel.Lounge) null;
    this.loungeServerData = (LoungeModel.LoungeServer) null;
    this.isChangeStarted = false;
    this.randomMatchingInfo = (LoungeModel.RandomMatchingInfo) null;
    this.loungeMemberStatus = (LoungeMemberesStatus) null;
    this.connection = (ChatLoungeConnection) null;
  }

  private void UpdateLoungeList(List<LoungeModel.Lounge> lounges)
  {
    this.lounges = lounges;
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SEARCH_ROOM_LIST);
  }

  private void UpdateRandomMatchingInfo(LoungeModel.RandomMatchingInfo info)
  {
    this.randomMatchingInfo = info;
  }

  public static bool IsValidNotEmptyRallyList()
  {
    return MonoBehaviourSingleton<LoungeMatchingManager>.IsValid() && MonoBehaviourSingleton<LoungeMatchingManager>.I.rallyInvite != null && MonoBehaviourSingleton<LoungeMatchingManager>.I.rallyInvite.Count > 0;
  }

  public static bool IsValidNotEmptyList()
  {
    return MonoBehaviourSingleton<LoungeMatchingManager>.IsValid() && MonoBehaviourSingleton<LoungeMatchingManager>.I.lounges != null && MonoBehaviourSingleton<LoungeMatchingManager>.I.lounges.Count > 0;
  }

  public static bool IsValidInLounge()
  {
    return MonoBehaviourSingleton<LoungeMatchingManager>.IsValid() && MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge();
  }

  public bool IsUserInLounge(int user_id)
  {
    PartyModel.SlotInfo slotInfoByUserId = this.GetSlotInfoByUserId(user_id);
    return LoungeMatchingManager.IsValidInLounge() && slotInfoByUserId != null && slotInfoByUserId.userInfo != null;
  }

  public bool IsInLounge() => this.loungeData != null;

  public string GetLoungeId() => this.loungeData == null ? "" : this.loungeData.id;

  public string GetLoungeNumber() => this.loungeData == null ? "" : this.loungeData.loungeNumber;

  public string GetInviteMessage()
  {
    return this.inviteFriendInfo == null ? "" : this.inviteFriendInfo.inviteMessage;
  }

  public string GetInviteHelpURL()
  {
    return this.inviteFriendInfo == null ? "" : this.inviteFriendInfo.linkUrl;
  }

  public PARTY_STATUS GetStatus()
  {
    return this.loungeData == null ? PARTY_STATUS.NONE : (PARTY_STATUS) this.loungeData.status;
  }

  public int GetOwnerUserId() => this.loungeData == null ? 0 : this.loungeData.ownerUserId;

  public int GetSlotIndex(int user_id)
  {
    return this.loungeData == null ? -1 : this.loungeData.slotInfos.FindIndex((Predicate<PartyModel.SlotInfo>) (s => s.userInfo != null && s.userInfo.userId == user_id));
  }

  public PartyModel.SlotInfo GetSlotInfoByIndex(int idx)
  {
    return this.loungeData != null && idx < this.loungeData.slotInfos.Count ? this.loungeData.slotInfos[idx] : (PartyModel.SlotInfo) null;
  }

  public PartyModel.SlotInfo GetSlotInfoByUserId(int user_id)
  {
    int slotIndex = this.GetSlotIndex(user_id);
    return slotIndex < 0 ? (PartyModel.SlotInfo) null : this.GetSlotInfoByIndex(slotIndex);
  }

  public int GetMemberCount()
  {
    if (this.loungeData == null)
      return 0;
    int memberCount = 0;
    for (int index = 0; index < this.loungeData.slotInfos.Count; ++index)
    {
      if (this.loungeData.slotInfos[index].userInfo != null)
        ++memberCount;
    }
    return memberCount;
  }

  public bool CheckFirstMet(int userId)
  {
    int index = 0;
    for (int count = this.firstMetUserIds.Count; index < count; ++index)
    {
      if (userId == this.firstMetUserIds[index])
        return true;
    }
    return false;
  }

  public List<int> GetMemberUserIdList(int my_userid = 0)
  {
    List<int> member_list = new List<int>();
    if (my_userid > 0)
      member_list.Add(my_userid);
    if (this.loungeData != null && this.loungeData.slotInfos != null)
      this.loungeData.slotInfos.ForEach((Action<PartyModel.SlotInfo>) (slot =>
      {
        if (slot.userInfo == null || my_userid != 0 && my_userid == slot.userInfo.userId)
          return;
        member_list.Add(slot.userInfo.userId);
      }));
    return member_list;
  }

  public static string GenerateToken() => Guid.NewGuid().ToString().Replace("-", "");

  public void SetFollowLoungeMember(List<FollowLoungeMember> _followLoungeMember)
  {
    this.followLoungeMember = _followLoungeMember;
  }

  public FollowLoungeMember GetFollowLoungeMember(int userId)
  {
    return this.followLoungeMember == null ? (FollowLoungeMember) null : this.followLoungeMember.Find((Predicate<FollowLoungeMember>) (d => d.userId == userId));
  }

  public void SendSearch(Action<bool, Error> call_back, bool saveSettings)
  {
    this.lounges = (List<LoungeModel.Lounge>) null;
    this.SetSearchRequest();
    LoungeSearchModel.RequestSendForm postData = new LoungeSearchModel.RequestSendForm();
    postData.order = this.searchRequest.order;
    postData.label = (int) this.searchRequest.label;
    postData.name = this.searchRequest.loungeName;
    if (saveSettings)
      this.SaveSearchSettings();
    Protocol.Send<LoungeSearchModel.RequestSendForm, LoungeSearchModel>(LoungeSearchModel.URL, postData, (Action<LoungeSearchModel>) (ret =>
    {
      bool flag = false;
      switch (ret.Error)
      {
        case Error.None:
        case Error.WRN_PARTY_SEARCH_NOT_FOUND_QUEST:
          if (ret.Error == Error.None)
            flag = true;
          this.UpdateLoungeList(ret.result.lounges);
          break;
      }
      call_back(flag, ret.Error);
    }));
  }

  public void SendSearchRandomMatching(Action<bool, Error> call_back)
  {
    this.ClearLounge();
    this.SetSearchRequest();
    LoungeModel.RequestSearchRandomMatching postData = new LoungeModel.RequestSearchRandomMatching();
    postData.token = LoungeMatchingManager.GenerateToken();
    postData.order = this.searchRequest.order;
    postData.label = (int) this.searchRequest.label;
    postData.name = this.searchRequest.loungeName;
    this.SaveSearchSettings();
    Protocol.Send<LoungeModel.RequestSearchRandomMatching, LoungeModel>(LoungeModel.RequestSearchRandomMatching.path, postData, (Action<LoungeModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None && ret.result.lounge != null)
      {
        flag = true;
        this.UpdateLounge(ret.result.lounge, ret.result.friend, ret.result.loungeServer, ret.result.inviteFriendInfo, ret.result.firstMetUserIds);
        if (ret.result.randomMatchingInfo != null)
          this.UpdateRandomMatchingInfo(ret.result.randomMatchingInfo);
        this.Dirty();
      }
      if (call_back == null)
        return;
      call_back(flag, ret.Error);
    }));
  }

  public void SetSearchRequest(LoungeSearchSettings.SearchRequestParam request = null)
  {
    if (request != null)
    {
      this.searchRequest = request;
    }
    else
    {
      if (this.searchRequest != null)
        return;
      this.ResetLoungeSearchRequest();
    }
  }

  public void ResetLoungeSearchRequest()
  {
    this.searchRequest = new LoungeSearchSettings.SearchRequestParam();
  }

  public void SetLoungeSearchRequestFromPrefs()
  {
    this.searchRequest = new LoungeSearchSettings.SearchRequestParam(1, (LOUNGE_LABEL) PlayerPrefs.GetInt("LOUNGE_SEARCH_LABEL_KEY", 0), PlayerPrefs.GetString("LOUNGE_SEARCH_NAME_KEY", ""));
  }

  private void SaveSearchSettings()
  {
    PlayerPrefs.SetInt("LOUNGE_SEARCH_LABEL_KEY", (int) this.searchRequest.label);
    PlayerPrefs.SetString("LOUNGE_SEARCH_NAME_KEY", this.searchRequest.loungeName);
    PlayerPrefs.Save();
  }

  public void SetLoungeCreateRequest(LoungeConditionSettings.CreateRequestParam request = null)
  {
    if (request != null)
    {
      this.createRequest = request;
    }
    else
    {
      if (this.createRequest != null)
        return;
      this.ResetLoungeCreateRequest();
    }
  }

  public void ResetLoungeCreateRequest()
  {
    this.createRequest = new LoungeConditionSettings.CreateRequestParam();
  }

  public void SetLoungeCreateRequestFromPrefs()
  {
    this.createRequest = new LoungeConditionSettings.CreateRequestParam(PlayerPrefs.GetInt("LOUNGE_CREATE_STAMP_KEY", 1), PlayerPrefs.GetInt("LOUNGE_CREATE_LEVEL_MIN_KEY", 15), PlayerPrefs.GetInt("LOUNGE_CREATE_LEVEL_MAX_KEY", Singleton<UserLevelTable>.I.GetMaxLevel()), PlayerPrefs.GetInt("LOUNGE_CREATE_CAPACITY_KEY", 8), (LOUNGE_LABEL) PlayerPrefs.GetInt("LOUNGE_CREATE_LABEL_KEY", 0), PlayerPrefs.GetInt("LOUNGE_CREATE_LOCK_KEY", 0) != 0, PlayerPrefs.GetString("LOUNGE_CREATE_NAME_KEY", ""));
  }

  public LoungeMatchingManager.LoungeSetting loungeSetting { get; private set; }

  public void SetLoungeSetting(LoungeMatchingManager.LoungeSetting setting)
  {
    this.loungeSetting = setting;
  }

  public void SendCreate(Action<bool, Error> call_back)
  {
    this.ClearLounge();
    LoungeModel.RequestCreate postData = new LoungeModel.RequestCreate();
    postData.token = LoungeMatchingManager.GenerateToken();
    postData.label = (int) this.createRequest.label;
    postData.minLv = this.createRequest.minLevel;
    postData.maxLv = this.createRequest.maxLevel;
    postData.name = this.createRequest.loungeName;
    postData.num = this.createRequest.capacity;
    postData.stampId = this.createRequest.stampId;
    postData.isLock = this.createRequest.isLock ? 1 : 0;
    this.SaveLoungeConditionSettings();
    if (this.followLoungeMember != null)
      this.followLoungeMember.Clear();
    Protocol.Send<LoungeModel.RequestCreate, LoungeModel>(LoungeModel.RequestCreate.path, postData, (Action<LoungeModel>) (ret =>
    {
      bool flag = false;
      switch (ret.Error)
      {
        case Error.None:
          flag = true;
          this.UpdateLounge(ret.result.lounge, (List<FollowLoungeMember>) null, ret.result.loungeServer, ret.result.inviteFriendInfo, ret.result.firstMetUserIds);
          this.Dirty();
          break;
      }
      call_back(flag, ret.Error);
    }));
  }

  public void SendApply(string loungeNumber, Action<bool, Error> call_back)
  {
    this.ClearLounge();
    Protocol.Send<LoungeModel.RequestApply, LoungeModel>(LoungeModel.RequestApply.path, new LoungeModel.RequestApply()
    {
      token = LoungeMatchingManager.GenerateToken(),
      loungeNumber = loungeNumber
    }, (Action<LoungeModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        this.UpdateLounge(ret.result.lounge, ret.result.friend, ret.result.loungeServer, ret.result.inviteFriendInfo, ret.result.firstMetUserIds);
        this.Dirty();
      }
      if (call_back == null)
        return;
      call_back(flag, ret.Error);
    }));
  }

  public void SendEntry(string id, Action<bool> call_back)
  {
    this.ClearLounge();
    Protocol.Send<LoungeModel.RequestEntry, LoungeModel>(LoungeModel.RequestEntry.path, new LoungeModel.RequestEntry()
    {
      token = LoungeMatchingManager.GenerateToken(),
      id = id
    }, (Action<LoungeModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        this.UpdateLounge(ret.result.lounge, ret.result.friend, ret.result.loungeServer, ret.result.inviteFriendInfo, ret.result.firstMetUserIds);
        this.Dirty();
      }
      call_back(flag);
    }));
  }

  public void SendInfo(Action<bool> call_back, bool force = false)
  {
    if (force)
      Protocol.Force((System.Action) (() => this.DoSendInfo(call_back)));
    else
      this.DoSendInfo(call_back);
  }

  private void DoSendInfo(Action<bool> call_back)
  {
    Protocol.Send<LoungeModel.RequestInfo, LoungeModel>(LoungeModel.RequestInfo.path, new LoungeModel.RequestInfo()
    {
      id = this.GetLoungeId()
    }, (Action<LoungeModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        if (ret.result != null && ret.result.lounge != null && ret.result.loungeServer != null)
        {
          flag = true;
          this.UpdateLounge(ret.result.lounge, ret.result.friend, ret.result.loungeServer, ret.result.inviteFriendInfo, ret.result.firstMetUserIds);
          this.Dirty();
        }
        else
          this.ClearLounge();
      }
      else
        this.ClearLounge();
      call_back(flag);
    }));
  }

  public void SendLeave(Action<bool> call_back)
  {
    if (this.loungeData == null)
    {
      call_back(false);
    }
    else
    {
      LoungeLeaveModel.RequestSendForm postData = new LoungeLeaveModel.RequestSendForm();
      postData.id = this.loungeData.id;
      if (this.followLoungeMember != null)
        this.followLoungeMember.Clear();
      Protocol.Send<LoungeLeaveModel.RequestSendForm, LoungeLeaveModel>(LoungeLeaveModel.URL, postData, (Action<LoungeLeaveModel>) (ret =>
      {
        bool flag = false;
        switch (ret.Error)
        {
          case Error.None:
          case Error.ERR_PARTY_NOT_FOUND_PARTY:
            this.StartCoroutine(this.DoLeave(call_back, ret));
            break;
          default:
            call_back(flag);
            break;
        }
      }));
    }
  }

  public void SendEdit(LoungeModel.RequestEdit lounge_setting, Action<bool> call_back)
  {
    if (this.loungeData == null)
    {
      call_back(false);
    }
    else
    {
      LoungeModel.RequestEdit postData = lounge_setting;
      postData.id = this.loungeData.id;
      Protocol.Send<LoungeModel.RequestEdit, LoungeModel>(LoungeModel.RequestEdit.path, postData, (Action<LoungeModel>) (ret =>
      {
        bool flag = false;
        if (ret.Error == Error.None)
        {
          flag = true;
          this.UpdateLounge(ret.result.lounge, ret.result.friend, ret.result.loungeServer, ret.result.inviteFriendInfo, ret.result.firstMetUserIds);
        }
        call_back(flag);
      }));
    }
  }

  private IEnumerator DoLeave(Action<bool> call_back, LoungeLeaveModel ret)
  {
    bool flag = false;
    switch (ret.Error)
    {
      case Error.None:
      case Error.ERR_PARTY_NOT_FOUND_PARTY:
        if (this.IsHostChange(ret.result.lounge, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id))
        {
          Lounge_Model_RoomHostChanged model = new Lounge_Model_RoomHostChanged();
          model.id = 1005;
          model.hostid = ret.result.lounge.ownerUserId;
          MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast<Lounge_Model_RoomHostChanged>(model);
          yield return (object) 0;
        }
        flag = true;
        Lounge_Model_RoomLeaved model1 = new Lounge_Model_RoomLeaved();
        model1.id = 1005;
        model1.token = LoungeMatchingManager.GenerateToken();
        model1.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
        MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast<Lounge_Model_RoomLeaved>(model1, onReceiveAck: (Func<Coop_Model_ACK, bool>) (ack => true));
        MonoBehaviourSingleton<ChatManager>.I.DestroyLoungeChat();
        this.StopAFKCheck();
        this.ClearLounge();
        this.Dirty();
        break;
    }
    call_back(flag);
  }

  private bool IsHostChange(LoungeModel.Lounge lounge, int leaveUserId)
  {
    return this.GetOwnerUserId() == leaveUserId && lounge != null && lounge.status != 30;
  }

  public void SendInviteList(Action<bool, LoungeInviteCharaInfo[]> call_back)
  {
    if (this.loungeData == null)
      call_back(false, (LoungeInviteCharaInfo[]) null);
    else
      Protocol.Send<LoungeInviteListModel.RequestSendForm, LoungeInviteListModel>(LoungeInviteListModel.URL, new LoungeInviteListModel.RequestSendForm()
      {
        id = this.loungeData.id
      }, (Action<LoungeInviteListModel>) (ret =>
      {
        bool flag = false;
        LoungeInviteCharaInfo[] loungeInviteCharaInfoArray = (LoungeInviteCharaInfo[]) null;
        if (ret.Error == Error.None)
        {
          flag = true;
          loungeInviteCharaInfoArray = ret.result.ToArray();
        }
        call_back(flag, loungeInviteCharaInfoArray);
      }));
  }

  public void SendInvite(int[] userIds, Action<bool, int[]> call_back)
  {
    if (this.loungeData == null)
    {
      call_back(false, (int[]) null);
    }
    else
    {
      LoungeInviteModel.RequestSendForm postData = new LoungeInviteModel.RequestSendForm();
      postData.id = this.loungeData.id;
      foreach (int userId in userIds)
        postData.ids.Add(userId);
      Protocol.Send<LoungeInviteModel.RequestSendForm, LoungeInviteModel>(LoungeInviteModel.URL, postData, (Action<LoungeInviteModel>) (ret =>
      {
        bool flag = false;
        if (ret.Error == Error.None)
          call_back(true, ret.result.ToArray());
        else
          call_back(flag, (int[]) null);
      }));
    }
  }

  public void SendInvitedLounge(Action<bool> call_back)
  {
    this.lounges = (List<LoungeModel.Lounge>) null;
    Protocol.Send<LoungeInvitedLoungeModel>(LoungeInvitedLoungeModel.URL, (Action<LoungeInvitedLoungeModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        this.UpdateLoungeList(ret.result.lounges);
      }
      call_back(flag);
    }));
  }

  public void SendRoomParty(Action<bool, List<PartyModel.Party>> call_back)
  {
    if (this.loungeData == null)
      return;
    Protocol.Send<LoungeRoomPartyModel.RequestSendForm, LoungeRoomPartyModel>(LoungeRoomPartyModel.URL, new LoungeRoomPartyModel.RequestSendForm()
    {
      id = int.Parse(this.loungeData.id)
    }, (Action<LoungeRoomPartyModel>) (ret =>
    {
      this.UpdateParties(ret.result.parties);
      call_back(ret.Error == Error.None, ret.result.parties);
    }));
  }

  private void UpdateParties(List<PartyModel.Party> parties) => this.parties = parties;

  public void SendRoomPartyKick(Action<bool> call_back, int kickedUserId)
  {
    Protocol.Send<LoungeModel.RequestKick, BaseModel>(LoungeModel.RequestKick.path, new LoungeModel.RequestKick()
    {
      id = this.GetLoungeId(),
      userId = kickedUserId
    }, (Action<BaseModel>) (ret =>
    {
      bool flag = ret.Error == Error.None;
      if (flag)
        MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast<Lounge_Model_RoomKick>(new Lounge_Model_RoomKick()
        {
          id = 1005,
          token = LoungeMatchingManager.GenerateToken(),
          cid = kickedUserId
        });
      call_back(flag);
    }));
  }

  public void SendRoomPartyAFKKick(int kickedUserId, Action<bool> call_back = null)
  {
    Protocol.Send<LoungeModel.RequestForceKick, LoungeModel>(LoungeModel.RequestForceKick.path, new LoungeModel.RequestForceKick()
    {
      id = this.GetLoungeId(),
      userId = kickedUserId
    }, (Action<LoungeModel>) (ret =>
    {
      bool flag = ret.Error == Error.None;
      if (flag)
      {
        if (ret.Error == Error.None)
        {
          if (ret.result.lounge.slotInfos.Find((Predicate<PartyModel.SlotInfo>) (s => s.userInfo != null && s.userInfo.userId == kickedUserId)) != null)
          {
            this.loungeMemberStatus[kickedUserId].UpdateLastExecTime(TimeManager.GetNow().ToUniversalTime());
          }
          else
          {
            if (this.IsHostChange(ret.result.lounge, kickedUserId))
              MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast<Lounge_Model_RoomHostChanged>(new Lounge_Model_RoomHostChanged()
              {
                id = 1005,
                hostid = ret.result.lounge.ownerUserId
              });
            MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast<Lounge_Model_AFK_Kick>(new Lounge_Model_AFK_Kick()
            {
              id = 1005,
              cid = kickedUserId,
              token = LoungeMatchingManager.GenerateToken()
            });
          }
          this.UpdateLounge(ret.result.lounge, ret.result.friend, ret.result.loungeServer, ret.result.inviteFriendInfo, ret.result.firstMetUserIds);
        }
        else
          call_back(flag);
      }
      call_back(flag);
    }));
  }

  public void SendSearchFollowerRoom(
    Action<bool, List<LoungeSearchFollowerRoomModel.LoungeFollowerModel>, List<int>> call_back)
  {
    Protocol.Send<LoungeSearchFollowerRoomModel>(LoungeSearchFollowerRoomModel.URL, (Action<LoungeSearchFollowerRoomModel>) (ret => call_back(ret.Error == Error.None, ret.result.lounges, ret.result.firstMetUserIds)));
  }

  public void SendIsLounge()
  {
    if (!CoopWebSocketSingleton<LoungeWebSocket>.IsValidConnected())
      return;
    Lounge_Model_MemberLounge model = new Lounge_Model_MemberLounge();
    model.id = 1005;
    model.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast<Lounge_Model_MemberLounge>(model);
  }

  public void SendInLounge()
  {
    if (!CoopWebSocketSingleton<LoungeWebSocket>.IsValidConnected())
      return;
    Lounge_Model_MemberLounge model = new Lounge_Model_MemberLounge();
    model.id = 1005;
    model.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast<Lounge_Model_MemberLounge>(model);
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
    MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast<Lounge_Model_MemberField>(model);
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
    MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast<Lounge_Model_MemberQuest>(model);
  }

  public void SendStartArena(int arenaId)
  {
    if (!CoopWebSocketSingleton<LoungeWebSocket>.IsValidConnected())
      return;
    Lounge_Model_MemberArena model = new Lounge_Model_MemberArena();
    model.id = 1005;
    model.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    model.aid = arenaId;
    MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast<Lounge_Model_MemberArena>(model);
  }

  public void Kick(int userId)
  {
    if (userId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
      this.loungeData.slotInfos.Remove(this.GetSlotInfoByUserId(userId));
    if (MonoBehaviourSingleton<LoungeManager>.IsValid())
    {
      if (userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
      {
        this.ClearLounge();
        MonoBehaviourSingleton<ChatManager>.I.DestroyLoungeChat();
        this.StopAFKCheck();
      }
      MonoBehaviourSingleton<LoungeManager>.I.OnRecvRoomKick(userId);
    }
    else
    {
      if (userId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
        return;
      this.isKicked = true;
      if (FieldManager.IsValidInGameNoQuest())
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.LOUNGE_KICKED);
      if (QuestManager.IsValidInGame())
        UIInGamePopupDialog.PushOpen(StringTable.Get(STRING_CATEGORY.IN_GAME, 140U), false, 1.4f);
      this.ClearLounge();
      MonoBehaviourSingleton<ChatManager>.I.DestroyLoungeChat();
      this.StopAFKCheck();
    }
  }

  public LoungeNetworkManager.ConnectData GetWebSockConnectData()
  {
    if (this.loungeData == null || this.loungeServerData == null)
    {
      Log.Error(LOG.WEBSOCK, "NotFound ConnectData");
      return (LoungeNetworkManager.ConnectData) null;
    }
    int id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    int slotIndex = this.GetSlotIndex(id);
    if (slotIndex < 0)
      return (LoungeNetworkManager.ConnectData) null;
    return new LoungeNetworkManager.ConnectData()
    {
      path = this.loungeServerData.wsHost,
      ports = this.loungeServerData.wsPorts,
      fromId = id,
      ackPrefix = slotIndex,
      roomId = this.loungeData.id,
      owner = this.loungeData.ownerUserId,
      ownerToken = this.loungeServerData.token,
      uid = id,
      signature = this.loungeServerData.signature
    };
  }

  public void ConnectServer()
  {
    LoungeNetworkManager.ConnectData webSockConnectData = this.GetWebSockConnectData();
    if (webSockConnectData == null)
      this.TryConnect(false, false);
    else if (!MonoBehaviourSingleton<LoungeNetworkManager>.IsValid())
      this.TryConnect(false, false);
    else
      MonoBehaviourSingleton<LoungeNetworkManager>.I.ConnectAndRegist(webSockConnectData, (Action<bool, bool>) ((is_connect, is_regist) => this.TryConnect(is_connect, is_regist)));
  }

  private void SaveLoungeConditionSettings()
  {
    PlayerPrefs.SetInt("LOUNGE_CREATE_STAMP_KEY", this.createRequest.stampId);
    PlayerPrefs.SetInt("LOUNGE_CREATE_LEVEL_MIN_KEY", this.createRequest.minLevel);
    PlayerPrefs.SetInt("LOUNGE_CREATE_LEVEL_MAX_KEY", this.createRequest.maxLevel);
    PlayerPrefs.SetInt("LOUNGE_CREATE_CAPACITY_KEY", this.createRequest.capacity);
    PlayerPrefs.SetInt("LOUNGE_CREATE_LABEL_KEY", (int) this.createRequest.label);
    PlayerPrefs.SetInt("LOUNGE_CREATE_LOCK_KEY", this.createRequest.isLock ? 1 : 0);
    PlayerPrefs.SetString("LOUNGE_CREATE_NAME_KEY", this.createRequest.loungeName);
    PlayerPrefs.Save();
  }

  public void ChangeOwner(int owenerId) => this.loungeData.ownerUserId = owenerId;

  public void CompleteKick() => this.isKicked = false;

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
      if (MonoBehaviourSingleton<LoungeNetworkManager>.IsValid())
        MonoBehaviourSingleton<LoungeNetworkManager>.I.MoveLoungeNotification(status, loungeMemberStatu);
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
      if (MonoBehaviourSingleton<LoungeNetworkManager>.IsValid())
        MonoBehaviourSingleton<LoungeNetworkManager>.I.MoveLoungeNotification(status, loungeMemberStatu);
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
      if (MonoBehaviourSingleton<LoungeNetworkManager>.IsValid())
        MonoBehaviourSingleton<LoungeNetworkManager>.I.MoveLoungeNotification(status, loungeMemberStatu);
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
      if (MonoBehaviourSingleton<LoungeNetworkManager>.IsValid())
        MonoBehaviourSingleton<LoungeNetworkManager>.I.MoveLoungeNotification(status, loungeMemberStatu);
    }
    this.AFKCheck();
  }

  private void AFKCheck()
  {
    if (this.afkCoroutine != null)
      this.StopCoroutine(this.afkCoroutine);
    this.afkCoroutine = this.StartCoroutine(this.DoAFKCheck());
  }

  private IEnumerator DoAFKCheck()
  {
    if (LoungeMatchingManager.IsValidInLounge() && this.loungeMemberStatus != null)
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
          Protocol.Force((System.Action) (() => this.SendRoomPartyAFKKick(fastest.userId, (Action<bool>) (is_sucess => wait = false))));
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

  public void SendRally(Action<bool> call_back)
  {
    Protocol.Send<LoungeRallyModel.RequestSendForm, LoungeRallyModel>(LoungeRallyModel.URL, new LoungeRallyModel.RequestSendForm()
    {
      id = MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData.id,
      pid = MonoBehaviourSingleton<PartyManager>.I.GetPartyId()
    }, (Action<LoungeRallyModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      call_back(flag);
    }));
  }

  public void GetRallyList(Action<bool> call_back)
  {
    if (!LoungeMatchingManager.IsValidInLounge())
    {
      this.rallyInvite = new List<LoungeModel.SlotInfo>();
      call_back(true);
    }
    else
    {
      this.rallyInvite = (List<LoungeModel.SlotInfo>) null;
      Protocol.Send<LoungeRallyListModel.RequestSendForm, LoungeRallyListModel>(LoungeRallyListModel.URL, new LoungeRallyListModel.RequestSendForm()
      {
        id = this.loungeData.id
      }, (Action<LoungeRallyListModel>) (ret =>
      {
        bool flag = false;
        if (ret.Error == Error.None)
        {
          this.rallyInvite = ret.result.slotInfos;
          flag = true;
        }
        if (this.rallyInvite == null)
          this.rallyInvite = new List<LoungeModel.SlotInfo>();
        if (flag && this.rallyInvite.Count > 0)
          MonoBehaviourSingleton<LoungeMatchingManager>.I.SendInfo((Action<bool>) (issuccess => call_back(issuccess)));
        else
          call_back(flag);
      }));
    }
  }

  public bool IsRallyUser(int userid)
  {
    if (this.rallyInvite == null || this.rallyInvite.Count == 0)
      return false;
    int count = this.rallyInvite.Count;
    for (int index = 0; index < count; ++index)
    {
      if (this.rallyInvite[index].userInfo.userId == userid)
        return true;
    }
    return false;
  }

  public class LoungeSetting
  {
    public bool isLock;
    public int level;
    public int total;
    public int reserveLimitLevel;

    public LoungeSetting(bool is_lock, int _level, int _total)
    {
      this.isLock = is_lock;
      this.level = _level;
      this.total = _total;
      this.reserveLimitLevel = this.level;
    }
  }
}
