// Decompiled with JetBrains decompiler
// Type: ClanManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ClanManager : MonoBehaviourSingleton<ClanManager>, IHomeManager
{
  private const float SendInfoSpan = 3600f;
  private SpanTimer sendInfoSpan;
  private Queue<LoungeAnnounce.AnnounceData> clanAnnounceQueue = new Queue<LoungeAnnounce.AnnounceData>();
  private Coroutine loungeAnnounceCoroutine;

  public bool IsJumpToGacha { get; set; }

  public bool IsInitialized { get; private set; }

  public HomeCamera HomeCamera { get; private set; }

  public IHomePeople IHomePeople { get; private set; }

  public LoungeTableSet TableSet { get; private set; }

  public HomeFeatureBanner HomeFeatureBanner { get; private set; }

  public bool IsPointShopOpen { get; private set; }

  public int PointShopBannerId { get; private set; }

  public bool NeedLoungeQuestBalloonUpdate { get; private set; }

  public ClanNoticeBoardData noticeBoardData { get; private set; }

  public void SetPointShop(bool isOpen, int bannerId)
  {
    this.IsPointShopOpen = isOpen;
    this.PointShopBannerId = bannerId;
  }

  public void SetNoticeBoardData(ClanNoticeBoardData noticeBoardData, bool isSaveBoardVersion)
  {
    if (noticeBoardData == null)
      noticeBoardData = new ClanNoticeBoardData();
    this.noticeBoardData = noticeBoardData;
    if (!isSaveBoardVersion || this.noticeBoardData == null)
      return;
    PlayerPrefs.SetInt("CLAN_BOARD_READ_ID_KEY", this.noticeBoardData.version);
    PlayerPrefs.Save();
  }

  public OutGameSettingsManager.HomeScene GetSceneSetting()
  {
    return (OutGameSettingsManager.HomeScene) MonoBehaviourSingleton<OutGameSettingsManager>.I.clanScene;
  }

  protected override void Awake()
  {
    base.Awake();
    this.sendInfoSpan = new SpanTimer(3600f);
  }

  private IEnumerator Start()
  {
    while (!MonoBehaviourSingleton<StageManager>.IsValid() || MonoBehaviourSingleton<StageManager>.I.isLoading)
      yield return (object) null;
    ((Component) this).gameObject.AddComponent<ClanLvUnlockManager>();
    this.HomeCamera = ((Component) this).gameObject.AddComponent<HomeCamera>();
    this.IHomePeople = (IHomePeople) ((Component) this).gameObject.AddComponent<ClanPeople>();
    this.HomeFeatureBanner = ((Component) this).gameObject.AddComponent<HomeFeatureBanner>();
    this.TableSet = ((Component) this).gameObject.AddComponent<LoungeTableSet>();
    while (!this.HomeCamera.isInitialized || !this.IHomePeople.isInitialized || !this.TableSet.isInitialized)
      yield return (object) null;
    MonoBehaviourSingleton<ClanMatchingManager>.I.OnChangeMemberStatus += new Action<LoungeMemberStatus>(this.OnChangeMemberStatus);
    this.IsInitialized = true;
    if (this.sendInfoSpan.IsReady())
      yield return (object) this.StartCoroutine(this.SendLoungeInfoForce());
    if (MonoBehaviourSingleton<ClanMatchingManager>.IsValid())
    {
      if (MonoBehaviourSingleton<ClanMatchingManager>.I.partyData == null)
        yield break;
      bool isWait = true;
      MonoBehaviourSingleton<ClanMatchingManager>.I.SendEnterToClanBase((Action<bool>) (isSuccess => isWait = false));
      while (isWait)
        yield return (object) null;
      while (!MonoBehaviourSingleton<ClanMatchingManager>.I.IsConnected() && !(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "HomeTop" | MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "LoungeTop"))
        yield return (object) null;
    }
    yield return (object) this.StartCoroutine(this.CreateLoungePlayerFromSlotInfo());
    if (ClanMatchingManager.IsValidInClan())
      MonoBehaviourSingleton<ClanMatchingManager>.I.SendInClanBase();
  }

  private IEnumerator CreateLoungePlayerFromSlotInfo()
  {
    if (MonoBehaviourSingleton<ClanMatchingManager>.I.partyData != null && MonoBehaviourSingleton<ClanMatchingManager>.I.loungeMemberStatus != null)
    {
      List<PartyModel.SlotInfo> data = MonoBehaviourSingleton<ClanMatchingManager>.I.partyData.slotInfos;
      for (int i = 0; i < data.Count; ++i)
      {
        if (data[i].userInfo != null && data[i].userInfo.userId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
        {
          switch (MonoBehaviourSingleton<ClanMatchingManager>.I.loungeMemberStatus[data[i].userInfo.userId].GetStatus())
          {
            case LoungeMemberStatus.MEMBER_STATUS.LOUNGE:
            case LoungeMemberStatus.MEMBER_STATUS.QUEST_READY:
              this.IHomePeople.CastToLoungePeople().CreateLoungePlayer(data[i], false);
              yield return (object) null;
              continue;
            default:
              continue;
          }
        }
      }
    }
  }

  private IEnumerator CreateCharacterRoomJoined(int userId)
  {
    yield return (object) this.StartCoroutine(this.SendLoungeInfoForce());
    PartyModel.SlotInfo slotInfoByUserId = MonoBehaviourSingleton<ClanMatchingManager>.I.GetSlotInfoByUserId(userId);
    if (slotInfoByUserId != null && this.IHomePeople != null && this.IHomePeople.CastToLoungePeople().CreateLoungePlayer(slotInfoByUserId, true))
    {
      this.SetAnnounce(new LoungeAnnounce.AnnounceData(LoungeAnnounce.ANNOUNCE_TYPE.JOIN_LOUNGE, slotInfoByUserId.userInfo.name));
      if (MonoBehaviourSingleton<ClanNetworkManager>.IsValid())
        MonoBehaviourSingleton<ClanNetworkManager>.I.JoinNotification(slotInfoByUserId.userInfo);
    }
  }

  private void OnChangeMemberStatus(LoungeMemberStatus status)
  {
    int userId = status.userId;
    switch (status.GetStatus())
    {
      case LoungeMemberStatus.MEMBER_STATUS.LOUNGE:
        this.SendRoomPosition(userId);
        this.NeedLoungeQuestBalloonUpdate = true;
        this.StartCoroutine(this.CreatePlayerOnChangedStatus(userId));
        break;
      case LoungeMemberStatus.MEMBER_STATUS.QUEST_READY:
        if (!status.isHost)
          break;
        this.CreatePartyAnnounce(userId);
        break;
      case LoungeMemberStatus.MEMBER_STATUS.QUEST:
      case LoungeMemberStatus.MEMBER_STATUS.FIELD:
      case LoungeMemberStatus.MEMBER_STATUS.ARENA:
        this.IHomePeople.CastToLoungePeople().DestroyLoungePlayer(userId);
        break;
    }
  }

  private IEnumerator CreatePlayerOnChangedStatus(int userId)
  {
    yield return (object) this.SendLoungeInfoForce();
    PartyModel.SlotInfo slotInfoByUserId = MonoBehaviourSingleton<ClanMatchingManager>.I.GetSlotInfoByUserId(userId);
    this.IHomePeople.CastToLoungePeople().CreateLoungePlayer(slotInfoByUserId, true);
    this.IHomePeople.CastToLoungePeople().ChangeEquipLoungePlayer(slotInfoByUserId, true);
  }

  private void CreatePartyAnnounce(int userId)
  {
    this.NeedLoungeQuestBalloonUpdate = true;
    this.SetAnnounce(new LoungeAnnounce.AnnounceData(LoungeAnnounce.ANNOUNCE_TYPE.CREATED_PARTY, MonoBehaviourSingleton<ClanMatchingManager>.I.GetSlotInfoByUserId(userId).userInfo.name));
  }

  private void SetAnnounce(LoungeAnnounce.AnnounceData data)
  {
    if (data == null)
      return;
    this.clanAnnounceQueue.Enqueue(data);
    if (this.loungeAnnounceCoroutine != null)
      return;
    this.loungeAnnounceCoroutine = this.StartCoroutine(this.ShowAnnounce());
  }

  private IEnumerator ShowAnnounce()
  {
    ClanAnnounce announce = MonoBehaviourSingleton<UIManager>.I.clanAnnounce;
    if (Object.op_Equality((Object) announce, (Object) null))
    {
      this.loungeAnnounceCoroutine = (Coroutine) null;
    }
    else
    {
      while (this.clanAnnounceQueue.Count > 0)
      {
        LoungeAnnounce.AnnounceData data = this.clanAnnounceQueue.Dequeue();
        bool wait = true;
        announce.Play(data, (System.Action) (() => wait = false));
        while (wait)
          yield return (object) null;
        yield return (object) new WaitForSeconds(0.3f);
      }
      this.loungeAnnounceCoroutine = (Coroutine) null;
    }
  }

  private void Update()
  {
    if (this.sendInfoSpan.IsReady())
      this.StartCoroutine(this.SendLoungeInfoForce());
    if (MonoBehaviourSingleton<ClanMatchingManager>.I.partyData == null)
      return;
    List<PartyModel.SlotInfo> slotInfos = MonoBehaviourSingleton<ClanMatchingManager>.I.partyData.slotInfos;
    if (slotInfos == null)
      return;
    for (int index = 0; index < slotInfos.Count && this.IHomePeople != null; ++index)
      this.IHomePeople.CastToLoungePeople().UpdateLoungePlayersInfo(slotInfos[index]);
  }

  protected override void _OnDestroy()
  {
    MonoBehaviourSingleton<ClanMatchingManager>.I.OnChangeMemberStatus -= new Action<LoungeMemberStatus>(this.OnChangeMemberStatus);
    base._OnDestroy();
  }

  private void OnApplicationPause(bool pause)
  {
    if (pause)
      return;
    this.StartCoroutine(this.ResumeApp());
  }

  private IEnumerator ResumeApp()
  {
    while (MonoBehaviourSingleton<ClanMatchingManager>.I.isResume)
      yield return (object) null;
    while (!MonoBehaviourSingleton<LoungeWebSocket>.I.IsConnected())
      yield return (object) null;
    if (!this.CheckLeavedOnResume())
    {
      this.DestoryMembersOnResume();
      yield return (object) null;
      this.StartCoroutine(this.CreateMembersOnResume());
      yield return (object) null;
      this.ResetAllMemberAction();
    }
  }

  private bool CheckLeavedOnResume()
  {
    if (MonoBehaviourSingleton<ClanMatchingManager>.I.partyData != null)
      return false;
    MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[2]
    {
      new EventData("MAIN_MENU_CLAN", (object) null),
      new EventData("CLAN_AFK_KICKED", (object) null)
    });
    MonoBehaviourSingleton<ClanMatchingManager>.I.StopAFKCheck();
    return true;
  }

  private void DestoryMembersOnResume()
  {
    if (this.IHomePeople == null || this.IHomePeople.CastToLoungePeople().loungePlayers == null)
      return;
    for (int index = 0; index < this.IHomePeople.CastToLoungePeople().loungePlayers.Count; ++index)
    {
      int userId = this.IHomePeople.CastToLoungePeople().loungePlayers[index].GetUserId();
      if (userId != 0)
      {
        if (MonoBehaviourSingleton<ClanMatchingManager>.I.GetSlotInfoByUserId(userId) == null)
          this.IHomePeople.CastToLoungePeople().DestroyLoungePlayer(userId);
        else if (MonoBehaviourSingleton<ClanMatchingManager>.I.loungeMemberStatus != null)
        {
          switch (MonoBehaviourSingleton<ClanMatchingManager>.I.loungeMemberStatus[userId].GetStatus())
          {
            case LoungeMemberStatus.MEMBER_STATUS.QUEST:
            case LoungeMemberStatus.MEMBER_STATUS.FIELD:
              this.IHomePeople.CastToLoungePeople().DestroyLoungePlayer(userId);
              continue;
            default:
              continue;
          }
        }
      }
    }
  }

  private IEnumerator CreateMembersOnResume()
  {
    if (MonoBehaviourSingleton<ClanMatchingManager>.I.partyData != null)
    {
      List<PartyModel.SlotInfo> slots = MonoBehaviourSingleton<ClanMatchingManager>.I.partyData.slotInfos;
      for (int i = 0; i < slots.Count; ++i)
      {
        if (slots[i].userInfo != null)
        {
          int userId = slots[i].userInfo.userId;
          if (userId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id && MonoBehaviourSingleton<ClanMatchingManager>.I.loungeMemberStatus != null)
          {
            LoungeMemberStatus loungeMemberStatu = MonoBehaviourSingleton<ClanMatchingManager>.I.loungeMemberStatus[userId];
            if (loungeMemberStatu != null)
            {
              switch (loungeMemberStatu.GetStatus())
              {
                case LoungeMemberStatus.MEMBER_STATUS.LOUNGE:
                case LoungeMemberStatus.MEMBER_STATUS.QUEST_READY:
                  this.IHomePeople.CastToLoungePeople().CreateLoungePlayer(slots[i], false);
                  this.IHomePeople.CastToLoungePeople().ChangeEquipLoungePlayer(slots[i], false);
                  yield return (object) null;
                  continue;
                default:
                  continue;
              }
            }
          }
        }
      }
    }
  }

  private void ResetAllMemberAction()
  {
    if (this.IHomePeople == null)
      return;
    for (int index = 0; index < this.IHomePeople.CastToLoungePeople().loungePlayers.Count; ++index)
      this.IHomePeople.CastToLoungePeople().loungePlayers[index].ResetAction();
  }

  public void OnRecvRoomJoined(int userId)
  {
    if (userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
      return;
    this.StartCoroutine(this.CreateCharacterRoomJoined(userId));
  }

  public void OnRecvRoomLeaved(int id)
  {
    if (this.IHomePeople == null)
      return;
    if (this.IHomePeople.CastToLoungePeople().DestroyLoungePlayer(id))
      this.SetAnnounce(new LoungeAnnounce.AnnounceData(LoungeAnnounce.ANNOUNCE_TYPE.LEAVED_LOUNGE, MonoBehaviourSingleton<ClanMatchingManager>.I.GetSlotInfoByUserId(id).userInfo.name));
    if (id == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
      return;
    this.StartCoroutine(this.SendLoungeInfoForce());
  }

  public void OnRecvRoomMove(int id, Vector3 targetPos)
  {
    if (this.IHomePeople == null)
      return;
    this.IHomePeople.CastToLoungePeople().MoveLoungePlayer(id, targetPos);
  }

  public void OnRecvRoomPosition(int id, Vector3 targetPos, LOUNGE_ACTION_TYPE type)
  {
    if (this.IHomePeople == null)
      return;
    this.IHomePeople.CastToLoungePeople().SetInitialPositionLoungePlayer(id, targetPos, type);
  }

  public void OnRecvRoomAction(int cid, int aid)
  {
    if (this.IHomePeople == null)
      return;
    LoungePlayer loungePlayer = this.IHomePeople.CastToLoungePeople().GetLoungePlayer(cid);
    if (Object.op_Equality((Object) loungePlayer, (Object) null))
      return;
    loungePlayer.ResetAFKTimer();
    switch (aid)
    {
      case 1:
        loungePlayer.OnRecvSit();
        break;
      case 2:
        loungePlayer.OnRecvStandUp();
        break;
      case 4:
        loungePlayer.OnRecvToGacha();
        break;
      case 5:
        loungePlayer.OnRecvToEquip();
        break;
      case 6:
        loungePlayer.OnRecvAFK();
        break;
      default:
        loungePlayer.OnRecvNone();
        break;
    }
  }

  public void OnRecvChatMessage(int userId)
  {
    if (this.IHomePeople == null)
      return;
    LoungePlayer loungePlayer = this.IHomePeople.CastToLoungePeople().GetLoungePlayer(userId);
    if (Object.op_Equality((Object) loungePlayer, (Object) null))
      return;
    loungePlayer.ResetAFKTimer();
  }

  public void OnRecvRoomKick(int id)
  {
    if (this.IHomePeople == null)
      return;
    if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id == id)
    {
      MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[2]
      {
        new EventData("MAIN_MENU_CLAN", (object) null),
        new EventData("CLAN_KICKED", (object) null)
      });
      MonoBehaviourSingleton<ClanMatchingManager>.I.StopAFKCheck();
    }
    else
      this.IHomePeople.CastToLoungePeople().DestroyLoungePlayer(id);
  }

  public void OnRecvRoomAFKKick(int id)
  {
    if (this.IHomePeople == null)
      return;
    if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id == id)
    {
      MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[2]
      {
        new EventData("MAIN_MENU_CLAN", (object) null),
        new EventData("CLAN_AFK_KICKED", (object) null)
      });
      MonoBehaviourSingleton<ClanMatchingManager>.I.StopAFKCheck();
    }
    else
      this.IHomePeople.CastToLoungePeople().DestroyLoungePlayer(id);
  }

  private IEnumerator SendLoungeInfoForce()
  {
    bool wait = true;
    MonoBehaviourSingleton<ClanMatchingManager>.I.SendInfo((Action<bool>) (is_success => wait = false), true);
    while (wait)
      yield return (object) null;
  }

  private void SendRoomPosition(int cid)
  {
    if (this.IHomePeople == null || Object.op_Equality((Object) this.IHomePeople.selfChara, (Object) null))
      return;
    Vector3 position = this.IHomePeople.selfChara._transform.position;
    LOUNGE_ACTION_TYPE actionType = this.IHomePeople.selfChara.GetActionType();
    MonoBehaviourSingleton<LoungeNetworkManager>.I.RoomPosition(cid, position, actionType);
  }
}
