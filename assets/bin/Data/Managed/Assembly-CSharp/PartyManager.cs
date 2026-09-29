// Decompiled with JetBrains decompiler
// Type: PartyManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class PartyManager : MonoBehaviourSingleton<PartyManager>
{
  private bool isChangeStarted;
  private string inviteValue = "";
  public bool is_repeat_quest;
  public int repeatPartyStatus;
  private int setting_cs;
  private int setting_ce;

  public List<PartyModel.Party> partys { get; private set; }

  public PartyModel.Party partyData { get; private set; }

  public PartyModel.InviteFriendInfo inviteFriendInfo { get; private set; }

  public string InviteValue
  {
    get => this.inviteValue;
    set => this.inviteValue = value;
  }

  public QuestSearchRoomCondition.SearchRequestParam searchRequest { get; private set; }

  public QuestSearchRoomCondition.SearchRequestParam searchRequestTemp { get; private set; }

  public QuestRushSearchRoomCondition.RushSearchRequestParam rushSearchRequest { get; private set; }

  public List<int> nowRushQuestIds { get; private set; }

  public List<FollowPartyMember> followPartyMember { get; private set; }

  public List<IsEquipPartyMember> isEquipPartyMember { get; private set; }

  public PartyModel.PartyServer partyServerData { get; private set; }

  public PartyModel.RandomMatchingInfo randomMatchingInfo { get; private set; }

  public QuestChallengeInfoModel.Param challengeInfo { get; private set; }

  public PartyManager()
  {
    this.partys = (List<PartyModel.Party>) null;
    this.partyData = (PartyModel.Party) null;
    this.partyServerData = (PartyModel.PartyServer) null;
  }

  protected override void Awake()
  {
    base.Awake();
    ((Component) this).gameObject.AddComponent<PartyWebSocket>();
    ((Component) this).gameObject.AddComponent<PartyNetworkManager>();
  }

  public void Dirty()
  {
    if (!MonoBehaviourSingleton<GameSceneManager>.IsValid())
      return;
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.RECEIVE_COOP_ROOM_UPDATE);
    if (!this.isChangeStarted)
      return;
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.RECEIVE_COOP_ROOM_START);
  }

  private void UpdateParty(
    PartyModel.Party party,
    List<FollowPartyMember> followPartyMember,
    PartyModel.PartyServer partyServer,
    PartyModel.InviteFriendInfo inviteFriendInfo,
    List<IsEquipPartyMember> isEquipList = null)
  {
    if (this.partyData != null && this.partyData.status == 10 && (party.status == 100 || party.status == 105))
      this.isChangeStarted = true;
    this.inviteFriendInfo = inviteFriendInfo;
    if (this.partyData == null && party != null)
      MonoBehaviourSingleton<LoungeMatchingManager>.I.SendStartQuest(party);
    this.partyData = party;
    if (followPartyMember != null)
      this.followPartyMember = followPartyMember;
    if (isEquipList != null)
      this.isEquipPartyMember = isEquipList;
    if (partyServer != null)
      this.partyServerData = partyServer;
    if (party == null || !MonoBehaviourSingleton<QuestManager>.IsValid())
      return;
    MonoBehaviourSingleton<QuestManager>.I.resultUserCollection.Init(party);
    if (followPartyMember == null)
      return;
    MonoBehaviourSingleton<QuestManager>.I.resultUserCollection.SetPartyFollowInfo(followPartyMember);
  }

  private void ClearParty()
  {
    this.repeatPartyStatus = -1;
    this.is_repeat_quest = false;
    this.partyData = (PartyModel.Party) null;
    this.partyServerData = (PartyModel.PartyServer) null;
    this.isChangeStarted = false;
    this.randomMatchingInfo = (PartyModel.RandomMatchingInfo) null;
  }

  private void UpdatePartyList(List<PartyModel.Party> partys)
  {
    this.partys = partys;
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SEARCH_ROOM_LIST);
  }

  private void UpdateRandomMatchingInfo(PartyModel.RandomMatchingInfo info)
  {
    this.randomMatchingInfo = info;
  }

  public static bool IsValidNotEmptyList()
  {
    return MonoBehaviourSingleton<PartyManager>.IsValid() && MonoBehaviourSingleton<PartyManager>.I.partys != null && MonoBehaviourSingleton<PartyManager>.I.partys.Count > 0;
  }

  public static bool IsValidInParty()
  {
    return MonoBehaviourSingleton<PartyManager>.IsValid() && MonoBehaviourSingleton<PartyManager>.I.IsInParty();
  }

  public bool IsInParty() => this.partyData != null;

  public string GetPartyId() => this.partyData == null ? "" : this.partyData.id;

  public string GetPartyNumber() => this.partyData == null ? "" : this.partyData.partyNumber;

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
    return this.partyData == null ? PARTY_STATUS.NONE : (PARTY_STATUS) this.partyData.status;
  }

  public int GetOwnerUserId() => this.partyData == null ? 0 : this.partyData.ownerUserId;

  public uint GetQuestId()
  {
    return this.partyData == null || this.partyData.quest == null ? 0U : (uint) this.partyData.quest.questId;
  }

  public int GetSlotIndex(int user_id)
  {
    return this.partyData == null ? -1 : this.partyData.slotInfos.FindIndex((Predicate<PartyModel.SlotInfo>) (s => s.userInfo != null && s.userInfo.userId == user_id));
  }

  public PartyModel.SlotInfo GetSlotInfoByIndex(int idx)
  {
    return this.partyData != null && idx < this.partyData.slotInfos.Count ? this.partyData.slotInfos[idx] : (PartyModel.SlotInfo) null;
  }

  public bool IsMaxPartyMember()
  {
    if (this.partyData == null)
      return false;
    int count = this.partyData.slotInfos.Count;
    return this.GetMemberCount() >= count;
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

  public PartyModel.SlotInfo GetSlotInfoByUserId(int user_id)
  {
    int slotIndex = this.GetSlotIndex(user_id);
    return slotIndex < 0 ? (PartyModel.SlotInfo) null : this.GetSlotInfoByIndex(slotIndex);
  }

  public bool IsEquipChangeByIndex(int idx)
  {
    PartyModel.SlotInfo slotInfoByIndex = this.GetSlotInfoByIndex(idx);
    if (this.isEquipPartyMember != null)
    {
      for (int index = 0; index < this.isEquipPartyMember.Count; ++index)
      {
        if (this.isEquipPartyMember[index].memberId == slotInfoByIndex.userInfo.userId)
          return this.isEquipPartyMember[index].isEquip;
      }
    }
    return false;
  }

  public bool IsPayingQuest()
  {
    if (this.partyData == null)
    {
      Log.Error("IsPayingQuest :: PartyData is NULL");
      return false;
    }
    return this.partyData.quest.paying != null && !this.partyData.quest.paying.free;
  }

  public List<int> GetMemberUserIdList(int my_userid = 0)
  {
    List<int> member_list = new List<int>();
    if (my_userid > 0)
      member_list.Add(my_userid);
    if (this.partyData != null && this.partyData.slotInfos != null)
      this.partyData.slotInfos.ForEach((Action<PartyModel.SlotInfo>) (slot =>
      {
        if (slot.userInfo == null || my_userid != 0 && my_userid == slot.userInfo.userId)
          return;
        member_list.Add(slot.userInfo.userId);
      }));
    return member_list;
  }

  public static string GenerateToken() => Guid.NewGuid().ToString().Replace("-", "");

  public void SetFollowPartyMember(List<FollowPartyMember> _followPartyMember)
  {
    this.followPartyMember = _followPartyMember;
  }

  public FollowPartyMember GetFollowPartyMember(int userId)
  {
    return this.followPartyMember == null ? (FollowPartyMember) null : this.followPartyMember.Find((Predicate<FollowPartyMember>) (d => d.userId == userId));
  }

  public void SendFollowAgency(List<int> send_follow_list, Action<bool> callback = null)
  {
    MonoBehaviourSingleton<FriendManager>.I.SendFollowUser(send_follow_list, (Action<Error, List<int>>) ((err, follow_list) =>
    {
      bool flag = err == Error.None && follow_list.Count > 0;
      if (flag)
      {
        bool updated = false;
        send_follow_list.ForEach((Action<int>) (userId =>
        {
          FollowPartyMember followPartyMember = this.GetFollowPartyMember(userId);
          if (followPartyMember == null || followPartyMember.following)
            return;
          followPartyMember.following = true;
          updated = true;
        }));
        if (updated)
          this.UpdateParty(this.partyData, this.followPartyMember, this.partyServerData, this.inviteFriendInfo);
      }
      if (callback == null)
        return;
      callback(flag);
    }));
  }

  public void SendUnFollowAgency(int send_unfollow_user_id, Action<bool> callback = null)
  {
    MonoBehaviourSingleton<FriendManager>.I.SendUnfollowUser(send_unfollow_user_id, (Action<bool>) (is_success =>
    {
      if (is_success)
      {
        FollowPartyMember followPartyMember = this.GetFollowPartyMember(send_unfollow_user_id);
        if (followPartyMember != null && followPartyMember.following)
        {
          followPartyMember.following = false;
          this.UpdateParty(this.partyData, this.followPartyMember, this.partyServerData, this.inviteFriendInfo);
        }
      }
      if (callback == null)
        return;
      callback(is_success);
    }));
  }

  public void SetSearchRequestTemp(
    QuestSearchRoomCondition.SearchRequestParam request)
  {
    if (request == null)
      return;
    this.searchRequestTemp = this.searchRequest;
    this.searchRequest = request;
  }

  public void ResetSearchRequestTemp()
  {
    if (this.searchRequestTemp == null)
      return;
    this.searchRequest = this.searchRequestTemp;
    this.searchRequestTemp = (QuestSearchRoomCondition.SearchRequestParam) null;
  }

  public void SetSearchRequest(
    QuestSearchRoomCondition.SearchRequestParam request = null)
  {
    if (request != null)
    {
      this.ResetSearchRequestTemp();
      this.searchRequest = request;
    }
    else
    {
      if (this.searchRequest != null)
        return;
      this.ResetSearchRequestTemp();
      this.ResetSearchRequest();
    }
  }

  public void SetRushSearchRequest(
    QuestRushSearchRoomCondition.RushSearchRequestParam request)
  {
    if (request == null)
      return;
    this.rushSearchRequest = request;
  }

  public void ResetSearchRequest()
  {
    this.searchRequest = new QuestSearchRoomCondition.SearchRequestParam();
  }

  public void ResetRushSearchRequest()
  {
    this.rushSearchRequest = new QuestRushSearchRoomCondition.RushSearchRequestParam();
  }

  public void SendSearch(Action<bool, Error> call_back, bool saveSettings)
  {
    this.partys = (List<PartyModel.Party>) null;
    this.SetSearchRequest();
    PartySearchModel.RequestSendForm postData = new PartySearchModel.RequestSendForm();
    postData.order = this.searchRequest.order;
    postData.rarityBit = this.searchRequest.rarityBit;
    postData.elementBit = this.searchRequest.elementBit;
    postData.enemyLevelMin = this.searchRequest.enemyLevelMin;
    postData.enemyLevelMax = this.searchRequest.enemyLevelMax;
    postData.isCs = this.searchRequest.isCs;
    postData.isFs = this.searchRequest.isFs;
    if (!string.IsNullOrEmpty(this.searchRequest.targetEnemySpeciesName))
      postData.enemySpecies = this.searchRequest.GetEnemySpeciesId(this.searchRequest.targetEnemySpeciesName);
    postData.questTypeBit = this.searchRequest.questTypeBit;
    if (saveSettings)
      this.SaveGachaSearchSettings();
    Protocol.Send<PartySearchModel.RequestSendForm, PartySearchModel>(PartySearchModel.URL, postData, (Action<PartySearchModel>) (ret =>
    {
      bool flag = false;
      switch (ret.Error)
      {
        case Error.None:
        case Error.WRN_PARTY_SEARCH_NOT_FOUND_QUEST:
          if (ret.Error == Error.None)
            flag = true;
          this.UpdatePartyList(ret.result.partys);
          break;
      }
      call_back(flag, ret.Error);
    }));
  }

  public void SendSearchRandomMatching(Action<bool, Error> call_back)
  {
    this.ClearParty();
    this.SetSearchRequest();
    PartyModel.RequestSearchRandomMatching postData = new PartyModel.RequestSearchRandomMatching();
    postData.token = PartyManager.GenerateToken();
    postData.order = this.searchRequest.order;
    postData.rarityBit = this.searchRequest.rarityBit;
    postData.elementBit = this.searchRequest.elementBit;
    postData.enemyLevelMin = this.searchRequest.enemyLevelMin;
    postData.enemyLevelMax = this.searchRequest.enemyLevelMax;
    postData.isCs = this.searchRequest.isCs;
    postData.isFs = this.searchRequest.isFs;
    if (!string.IsNullOrEmpty(this.searchRequest.targetEnemySpeciesName))
      postData.enemySpecies = this.searchRequest.GetEnemySpeciesId(this.searchRequest.targetEnemySpeciesName);
    postData.questTypeBit = this.searchRequest.questTypeBit;
    this.SaveGachaSearchSettings();
    Protocol.Send<PartyModel.RequestSearchRandomMatching, PartyModel>(PartyModel.RequestSearchRandomMatching.path, postData, (Action<PartyModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None && ret.result.party != null)
      {
        flag = true;
        this.UpdateParty(ret.result.party, ret.result.friend, ret.result.partyServer, ret.result.inviteFriendInfo);
        if (ret.result.randomMatchingInfo != null)
          this.UpdateRandomMatchingInfo(ret.result.randomMatchingInfo);
        this.Dirty();
      }
      if (call_back == null)
        return;
      call_back(flag, ret.Error);
    }));
  }

  private void SaveGachaSearchSettings()
  {
    PlayerPrefs.SetInt("GACHA_SEARCH_RAIRTY_KEY", this.searchRequest.rarityBit);
    PlayerPrefs.SetInt("GACHA_SEARCH_ELEMENT_KEY", this.searchRequest.elementBit);
    PlayerPrefs.SetInt("GACHA_SEARCH_PRIORITY_FS_KEY", this.searchRequest.isFs);
    PlayerPrefs.SetInt("GACHA_SEARCH_PRIORITY_CS_KEY", this.searchRequest.isCs);
    PlayerPrefs.SetInt("GACHA_SEARCH_LEVEL_MIN_KEY", this.searchRequest.enemyLevelMin);
    PlayerPrefs.SetInt("GACHA_SEARCH_LEVEL_MAX_KEY", this.searchRequest.enemyLevelMax);
    if (!string.IsNullOrEmpty(this.searchRequest.targetEnemySpeciesName))
      PlayerPrefs.SetString("GACHA_SEARCH_SPECIES_KEY", this.searchRequest.targetEnemySpeciesName);
    PlayerPrefs.Save();
  }

  public void SetSearchRequestFromPrefs()
  {
    this.searchRequest = new QuestSearchRoomCondition.SearchRequestParam();
    this.searchRequest.rarityBit = PlayerPrefs.GetInt("GACHA_SEARCH_RAIRTY_KEY", 8388607 /*0x7FFFFF*/);
    this.searchRequest.elementBit = PlayerPrefs.GetInt("GACHA_SEARCH_ELEMENT_KEY", 8388607 /*0x7FFFFF*/);
    this.searchRequest.isFs = PlayerPrefs.GetInt("GACHA_SEARCH_PRIORITY_FS_KEY", 1);
    this.searchRequest.isCs = PlayerPrefs.GetInt("GACHA_SEARCH_PRIORITY_CS_KEY", 1);
    this.searchRequest.enemyLevelMin = PlayerPrefs.GetInt("GACHA_SEARCH_LEVEL_MIN_KEY", 1);
    int num = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.PARTY_SEARCH_QUEST_LEVEL_MAX;
    if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.PARTY_SEARCH_QUEST_EXTRA_LEVEL_MAX > 0)
      num = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.PARTY_SEARCH_QUEST_EXTRA_LEVEL_MAX;
    this.searchRequest.enemyLevelMax = PlayerPrefs.GetInt("GACHA_SEARCH_LEVEL_MAX_KEY", num);
    this.searchRequest.targetEnemySpeciesName = PlayerPrefs.GetString("GACHA_SEARCH_SPECIES_KEY", (string) null);
  }

  public void SendRushSearch(Action<bool, Error> call_back, bool saveSettings)
  {
    this.partys = (List<PartyModel.Party>) null;
    if (this.rushSearchRequest == null)
      this.ResetRushSearchRequest();
    PartySearchRushModel.RequestSendForm postData = new PartySearchRushModel.RequestSendForm();
    postData.floorMinQuestId = this.rushSearchRequest.minFloorQuestId;
    postData.floorMaxQuestId = this.rushSearchRequest.maxFloorQuestId;
    if (saveSettings)
      this.SaveRushSearchSettings();
    Protocol.Send<PartySearchRushModel.RequestSendForm, PartySearchModel>(PartySearchRushModel.URL, postData, (Action<PartySearchModel>) (ret =>
    {
      bool flag = false;
      switch (ret.Error)
      {
        case Error.None:
        case Error.WRN_PARTY_SEARCH_NOT_FOUND_QUEST:
          if (ret.Error == Error.None)
            flag = true;
          this.UpdatePartyList(ret.result.partys);
          break;
      }
      call_back(flag, ret.Error);
    }));
  }

  public void SendRushSearchRandomMatching(Action<bool, Error> call_back)
  {
    this.ClearParty();
    if (this.rushSearchRequest == null)
      this.ResetRushSearchRequest();
    PartyModel.RequestSearchRushRandomMatching postData = new PartyModel.RequestSearchRushRandomMatching();
    postData.token = PartyManager.GenerateToken();
    postData.floorMinQuestId = this.rushSearchRequest.minFloorQuestId;
    postData.floorMaxQuestId = this.rushSearchRequest.maxFloorQuestId;
    this.SaveRushSearchSettings();
    Protocol.Send<PartyModel.RequestSearchRushRandomMatching, PartyModel>(PartyModel.RequestSearchRushRandomMatching.path, postData, (Action<PartyModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None && ret.result.party != null)
      {
        flag = true;
        this.UpdateParty(ret.result.party, ret.result.friend, ret.result.partyServer, ret.result.inviteFriendInfo);
        if (ret.result.randomMatchingInfo != null)
          this.UpdateRandomMatchingInfo(ret.result.randomMatchingInfo);
        this.Dirty();
      }
      if (call_back == null)
        return;
      call_back(flag, ret.Error);
    }));
  }

  private void SaveRushSearchSettings()
  {
    PlayerPrefs.SetInt("RUSH_SEARCH_MAX_QUESTID_KEY", this.rushSearchRequest.maxFloorQuestId);
    PlayerPrefs.SetInt("RUSH_SEARCH_MIN_QUESTID_KEY", this.rushSearchRequest.minFloorQuestId);
    PlayerPrefs.Save();
  }

  public void SetRushRequestFromPrefs()
  {
    this.rushSearchRequest = new QuestRushSearchRoomCondition.RushSearchRequestParam();
    this.rushSearchRequest.maxFloorQuestId = PlayerPrefs.GetInt("RUSH_SEARCH_MAX_QUESTID_KEY", 0);
    this.rushSearchRequest.minFloorQuestId = PlayerPrefs.GetInt("RUSH_SEARCH_MIN_QUESTID_KEY", 0);
  }

  public void SetNowRushQuestIds(List<int> idList) => this.nowRushQuestIds = idList;

  public void SendEventSearch(int eventId, Action<bool, Error> call_back)
  {
    this.partys = (List<PartyModel.Party>) null;
    Protocol.Send<PartySearchEventModel.RequestSendForm, PartySearchModel>(PartySearchEventModel.URL, new PartySearchEventModel.RequestSendForm()
    {
      eid = eventId
    }, (Action<PartySearchModel>) (ret =>
    {
      bool flag = false;
      switch (ret.Error)
      {
        case Error.None:
        case Error.WRN_PARTY_SEARCH_NOT_FOUND_QUEST:
          if (ret.Error == Error.None)
            flag = true;
          this.UpdatePartyList(ret.result.partys);
          break;
      }
      call_back(flag, ret.Error);
    }));
  }

  public PartyManager.PartySetting partySetting { get; private set; }

  public void SetPartySetting(PartyManager.PartySetting setting) => this.partySetting = setting;

  public void SendRandomMatching(
    int questId,
    int retryCount,
    bool isExplore,
    Action<bool, int, bool, float> call_back)
  {
    this.ClearParty();
    Protocol.Send<PartyModel.RequestRandomMatching, PartyModel>(PartyModel.RequestRandomMatching.path, new PartyModel.RequestRandomMatching()
    {
      qid = questId,
      retryCount = retryCount,
      token = PartyManager.GenerateToken(),
      ce = isExplore ? 1 : 0
    }, (Action<PartyModel>) (ret =>
    {
      bool flag1 = false;
      bool flag2 = false;
      int num1 = 0;
      float num2 = 0.0f;
      if (ret.Error == Error.None)
      {
        flag1 = true;
        num1 = ret.result.randomMatchingInfo.maxRetryCount;
        num2 = ret.result.randomMatchingInfo.waitTime;
        if (ret.result.party != null)
        {
          this.UpdateParty(ret.result.party, ret.result.friend, ret.result.partyServer, ret.result.inviteFriendInfo);
          this.Dirty();
          num1 = 0;
          flag2 = true;
        }
      }
      call_back(flag1, num1, flag2, num2);
    }));
  }

  public void SendMatching(int questId, Action<Error, bool> call_back)
  {
    this.ClearParty();
    Protocol.Send<PartyModel.RequestMatching, PartyModel>(PartyModel.RequestMatching.path, new PartyModel.RequestMatching()
    {
      qid = questId,
      token = PartyManager.GenerateToken()
    }, (Action<PartyModel>) (ret =>
    {
      bool flag = false;
      switch (ret.Error)
      {
        case Error.None:
          flag = true;
          this.UpdateParty(ret.result.party, ret.result.friend, ret.result.partyServer, ret.result.inviteFriendInfo);
          this.Dirty();
          break;
        case Error.WRN_QUEST_IS_ORDER:
          flag = false;
          break;
      }
      call_back(ret.Error, flag);
    }));
  }

  public void SendCreate(
    int questId,
    PartyManager.PartySetting party_setting,
    Action<bool> call_back)
  {
    this.ClearParty();
    PartyModel.RequestCreate postData = new PartyModel.RequestCreate();
    postData.qid = questId;
    postData.token = PartyManager.GenerateToken();
    postData.isLock = party_setting.isLock ? 1 : 0;
    postData.lv = party_setting.level;
    postData.power = party_setting.total;
    postData.cs = party_setting.cs;
    postData.ce = party_setting.ex;
    this.setting_cs = party_setting.cs;
    this.setting_ce = party_setting.ex;
    if (this.followPartyMember != null)
      this.followPartyMember.Clear();
    Protocol.Send<PartyModel.RequestCreate, PartyModel>(PartyModel.RequestCreate.path, postData, (Action<PartyModel>) (ret =>
    {
      bool flag = false;
      switch (ret.Error)
      {
        case Error.None:
          flag = true;
          MonoBehaviourSingleton<UserInfoManager>.I.repeatPartyEnable = ret.result.repeatFeatureEnable;
          this.UpdateParty(ret.result.party, (List<FollowPartyMember>) null, ret.result.partyServer, ret.result.inviteFriendInfo);
          this.Dirty();
          break;
      }
      call_back(flag);
    }));
  }

  public void SendApply(string partyNumber, Action<bool, Error> call_back, int questId = 0)
  {
    this.ClearParty();
    Protocol.Send<PartyModel.RequestApply, PartyModel>(PartyModel.RequestApply.path, new PartyModel.RequestApply()
    {
      token = PartyManager.GenerateToken(),
      partyNumber = partyNumber,
      qid = questId
    }, (Action<PartyModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        this.UpdateParty(ret.result.party, ret.result.friend, ret.result.partyServer, ret.result.inviteFriendInfo);
        MonoBehaviourSingleton<UserInfoManager>.I.repeatPartyEnable = ret.result.repeatFeatureEnable;
        this.is_repeat_quest = ret.result.repeatStatus == 1;
        this.Dirty();
      }
      if (call_back == null)
        return;
      call_back(flag, ret.Error);
    }));
  }

  public void SendEntry(string id, bool isLoungeBoard, Action<bool> call_back)
  {
    this.ClearParty();
    Protocol.Send<PartyModel.RequestEntry, PartyModel>(PartyModel.RequestEntry.path, new PartyModel.RequestEntry()
    {
      token = PartyManager.GenerateToken(),
      id = id,
      isLoungeBoard = isLoungeBoard ? 1 : 0
    }, (Action<PartyModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        this.UpdateParty(ret.result.party, ret.result.friend, ret.result.partyServer, ret.result.inviteFriendInfo);
        MonoBehaviourSingleton<UserInfoManager>.I.repeatPartyEnable = ret.result.repeatFeatureEnable;
        this.is_repeat_quest = ret.result.repeatStatus == 1;
        this.Dirty();
      }
      call_back(flag);
    }));
  }

  public void SendInfo(Action<bool> call_back)
  {
    if (this.partyData == null)
    {
      call_back(false);
    }
    else
    {
      if (MonoBehaviourSingleton<QuestManager>.IsValid())
        MonoBehaviourSingleton<QuestManager>.I.resultUserCollection.Clear();
      Protocol.Send<PartyModel.RequestInfo, PartyModel>(PartyModel.RequestInfo.path, new PartyModel.RequestInfo()
      {
        id = this.partyData.id
      }, (Action<PartyModel>) (ret =>
      {
        bool flag = false;
        if (ret.Error == Error.None)
        {
          flag = true;
          this.UpdateParty(ret.result.party, ret.result.friend, ret.result.partyServer, ret.result.inviteFriendInfo, ret.result.isEquipList);
          this.is_repeat_quest = ret.result.repeatStatus == 1;
          this.Dirty();
        }
        else
          this.ClearParty();
        call_back(flag);
      }));
    }
  }

  public void SendIsEquip(bool isEquip, Action<bool> call_back)
  {
    if (this.partyData == null)
      call_back(false);
    else
      Protocol.Send<PartyModel.RequestIsEquip, PartyModel>(PartyModel.RequestIsEquip.path, new PartyModel.RequestIsEquip()
      {
        id = this.partyData.id,
        isEquip = isEquip
      }, (Action<PartyModel>) (ret =>
      {
        bool flag = false;
        if (ret.Error == Error.None)
          flag = true;
        call_back(flag);
      }));
  }

  public void SendReady(bool enable_ready, Action<bool> call_back)
  {
    if (this.partyData == null)
    {
      call_back(false);
    }
    else
    {
      int slotIndex = this.GetSlotIndex(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
      if (slotIndex < 0)
      {
        call_back(false);
      }
      else
      {
        PARTY_PLAYER_STATUS partyPlayerStatus = enable_ready ? PARTY_PLAYER_STATUS.READY : PARTY_PLAYER_STATUS.JOINED;
        if ((PARTY_PLAYER_STATUS) this.partyData.slotInfos[slotIndex].status == partyPlayerStatus)
        {
          call_back(true);
        }
        else
        {
          this.partyData.slotInfos[slotIndex].status = (int) partyPlayerStatus;
          Protocol.Send<PartyModel.RequestReady, PartyModel>(PartyModel.RequestReady.path, new PartyModel.RequestReady()
          {
            id = this.partyData.id,
            enable = enable_ready ? 1 : 0
          }, (Action<PartyModel>) (ret =>
          {
            bool flag = false;
            if (ret.Error == Error.None)
            {
              flag = true;
              this.UpdateParty(ret.result.party, (List<FollowPartyMember>) null, (PartyModel.PartyServer) null, ret.result.inviteFriendInfo);
              this.Dirty();
            }
            call_back(flag);
          }));
        }
      }
    }
  }

  public void SendLeave(Action<bool> call_back)
  {
    if (this.partyData == null)
    {
      call_back(false);
    }
    else
    {
      PartyLeaveModel.RequestSendForm postData = new PartyLeaveModel.RequestSendForm();
      postData.id = this.partyData.id;
      if (this.followPartyMember != null)
        this.followPartyMember.Clear();
      Protocol.Send<PartyLeaveModel.RequestSendForm, PartyLeaveModel>(PartyLeaveModel.URL, postData, (Action<PartyLeaveModel>) (ret =>
      {
        bool flag = false;
        switch (ret.Error)
        {
          case Error.None:
          case Error.ERR_PARTY_NOT_FOUND_PARTY:
            flag = true;
            this.ClearParty();
            this.Dirty();
            break;
        }
        call_back(flag);
      }));
    }
  }

  public void SendEdit(PartyManager.PartySetting party_setting, Action<bool> call_back)
  {
    if (this.partyData == null)
      call_back(false);
    else
      Protocol.Send<PartyModel.RequestEdit, PartyModel>(PartyModel.RequestEdit.path, new PartyModel.RequestEdit()
      {
        id = this.partyData.id,
        isLock = party_setting.isLock ? 1 : 0,
        lv = party_setting.level,
        power = party_setting.total
      }, (Action<PartyModel>) (ret =>
      {
        bool flag = false;
        if (ret.Error == Error.None)
          flag = true;
        call_back(flag);
      }));
  }

  public void SendInviteList(Action<bool, PartyInviteCharaInfo[]> call_back)
  {
    if (this.partyData == null)
      call_back(false, (PartyInviteCharaInfo[]) null);
    else
      Protocol.Send<PartyInviteListModel.RequestSendForm, PartyInviteListModel>(PartyInviteListModel.URL, new PartyInviteListModel.RequestSendForm()
      {
        id = this.partyData.id
      }, (Action<PartyInviteListModel>) (ret =>
      {
        bool flag = false;
        PartyInviteCharaInfo[] partyInviteCharaInfoArray = (PartyInviteCharaInfo[]) null;
        if (ret.Error == Error.None)
        {
          flag = true;
          partyInviteCharaInfoArray = ret.result.ToArray();
        }
        call_back(flag, partyInviteCharaInfoArray);
      }));
  }

  public void SendInvite(int[] userIds, Action<bool, int[]> call_back)
  {
    if (this.partyData == null)
    {
      call_back(false, (int[]) null);
    }
    else
    {
      PartyInviteModel.RequestSendForm postData = new PartyInviteModel.RequestSendForm();
      postData.id = this.partyData.id;
      foreach (int userId in userIds)
        postData.ids.Add(userId);
      Protocol.Send<PartyInviteModel.RequestSendForm, PartyInviteModel>(PartyInviteModel.URL, postData, (Action<PartyInviteModel>) (ret =>
      {
        bool flag = false;
        if (ret.Error == Error.None)
          call_back(true, ret.result.ToArray());
        else
          call_back(flag, (int[]) null);
      }));
    }
  }

  public void SendInvitedParty(Action<bool> call_back, bool isResumed = false)
  {
    this.partys = (List<PartyModel.Party>) null;
    Protocol.Send<PartyInvitedPartyModel>(PartyInvitedPartyModel.URL, (Action<PartyInvitedPartyModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        this.UpdatePartyList(ret.result.partys);
        if (PartyManager.IsValidNotEmptyList() && isResumed)
          MonoBehaviourSingleton<UserInfoManager>.I.SetPartyInviteResume(true);
      }
      call_back(flag);
    }));
  }

  public PartyNetworkManager.ConnectData GetWebSockConnectData()
  {
    if (this.partyData == null || this.partyServerData == null)
      return (PartyNetworkManager.ConnectData) null;
    int id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    int slotIndex = this.GetSlotIndex(id);
    if (slotIndex < 0)
      return (PartyNetworkManager.ConnectData) null;
    return new PartyNetworkManager.ConnectData()
    {
      path = this.partyServerData.wsHost,
      ports = this.partyServerData.wsPorts,
      fromId = id,
      ackPrefix = slotIndex,
      roomId = this.partyData.id,
      owner = this.partyData.ownerUserId,
      ownerToken = this.partyServerData.token,
      uid = id,
      signature = this.partyServerData.signature
    };
  }

  public void ConnectServer(Action<bool, bool> call_back = null)
  {
    PartyNetworkManager.ConnectData webSockConnectData = this.GetWebSockConnectData();
    if (webSockConnectData == null)
    {
      if (call_back == null)
        return;
      call_back(false, false);
    }
    else if (!MonoBehaviourSingleton<PartyNetworkManager>.IsValid())
    {
      if (call_back == null)
        return;
      call_back(false, false);
    }
    else
      MonoBehaviourSingleton<PartyNetworkManager>.I.ConnectAndRegist(webSockConnectData, (Action<bool, bool>) ((is_connect, is_regist) =>
      {
        int num = is_regist ? 1 : 0;
        if (call_back == null)
          return;
        call_back(is_connect, is_regist);
      }));
  }

  public void SendGetChallengeInfo(Action<bool, Error> call_back)
  {
    if (HomeTutorialManager.ShouldRunGachaTutorial())
    {
      this.challengeInfo = new QuestChallengeInfoModel.Param();
      if (call_back == null)
        return;
      call_back(true, Error.None);
    }
    else
    {
      this.challengeInfo = (QuestChallengeInfoModel.Param) null;
      QuestChallengeInfoModel.RequestSendForm postData = new QuestChallengeInfoModel.RequestSendForm();
      Protocol.Send<QuestChallengeInfoModel.RequestSendForm, QuestChallengeInfoModel>(QuestChallengeInfoModel.URL, postData, (Action<QuestChallengeInfoModel>) (ret =>
      {
        bool flag = false;
        if (ret.Error == Error.None)
        {
          flag = true;
          this.challengeInfo = ret.result;
        }
        if (call_back == null)
          return;
        call_back(flag, ret.Error);
      }));
    }
  }

  public void SendRepeat(bool isOn, Action<bool> call_back)
  {
    if (this.partyData == null)
      call_back(false);
    else
      Protocol.Send<PartyRepeatModel.RequestSendForm, PartyRepeatModel>(PartyRepeatModel.URL, new PartyRepeatModel.RequestSendForm()
      {
        id = this.partyData.id,
        st = isOn ? 1 : 0,
        cs = this.setting_cs,
        ce = this.setting_ce
      }, (Action<PartyRepeatModel>) (ret =>
      {
        bool flag = false;
        if (ret.Error == Error.None)
        {
          flag = true;
          this.is_repeat_quest = isOn;
        }
        call_back(flag);
      }));
  }

  public void SendGetNextParty(Action<bool> call_back)
  {
    if (this.partyData == null)
      call_back(false);
    else
      Protocol.Send<PartyNextModel.RequestSendForm, PartyNextModel>(PartyNextModel.URL, new PartyNextModel.RequestSendForm()
      {
        id = this.partyData.id
      }, (Action<PartyNextModel>) (ret =>
      {
        bool flag = false;
        if (ret.Error == Error.None)
        {
          this.repeatPartyStatus = ret.result.repeatPartyStatus;
          if (ret.result.repeatPartyStatus > 0)
          {
            this.UpdateParty(ret.result.party, (List<FollowPartyMember>) null, ret.result.partyServer, ret.result.inviteFriendInfo);
            this.is_repeat_quest = ret.result.repeatStatus == 1;
          }
          flag = true;
        }
        call_back(flag);
      }));
  }

  public void UpdatePartyRepeat(
    PartyModel.Party party,
    List<FollowPartyMember> followPartyMember,
    PartyModel.PartyServer partyServer,
    PartyModel.InviteFriendInfo inviteFriendInfo,
    List<IsEquipPartyMember> isEquipList = null)
  {
    this.UpdateParty(party, followPartyMember, partyServer, inviteFriendInfo, isEquipList);
    this.Dirty();
  }

  public class PartySetting
  {
    public bool isLock;
    public int level;
    public int total;
    public int reserveLimitLevel;
    public int cs;
    public int ex;

    public PartySetting(bool is_lock, int _level, int _total, int _cs = 0, int _ex = 0)
    {
      this.isLock = is_lock;
      this.level = _level;
      this.total = _total;
      this.reserveLimitLevel = this.level;
      this.cs = _cs;
      this.ex = _ex;
    }
  }
}
