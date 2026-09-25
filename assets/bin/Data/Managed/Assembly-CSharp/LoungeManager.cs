// Decompiled with JetBrains decompiler
// Type: LoungeManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class LoungeManager : MonoBehaviourSingleton<LoungeManager>, IHomeManager
{
  private const float SendInfoSpan = 3600f;
  private SpanTimer sendInfoSpan;
  private Queue<LoungeAnnounce.AnnounceData> loungeAnnounceQueue = new Queue<LoungeAnnounce.AnnounceData>();
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

  public void SetPointShop(bool isOpen, int bannerId)
  {
    this.IsPointShopOpen = isOpen;
    this.PointShopBannerId = bannerId;
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
      this.SetAnnounce(new LoungeAnnounce.AnnounceData(LoungeAnnounce.ANNOUNCE_TYPE.LEAVED_LOUNGE, MonoBehaviourSingleton<LoungeMatchingManager>.I.GetSlotInfoByUserId(id).userInfo.name));
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
      MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[2]
      {
        new EventData("MAIN_MENU_LOUNGE", (object) null),
        new EventData("LOUNGE_KICKED", (object) null)
      });
    else
      this.IHomePeople.CastToLoungePeople().DestroyLoungePlayer(id);
  }

  public void SetLoungeQuestBalloon(bool request) => this.NeedLoungeQuestBalloonUpdate = request;

  public OutGameSettingsManager.HomeScene GetSceneSetting()
  {
    return (OutGameSettingsManager.HomeScene) MonoBehaviourSingleton<OutGameSettingsManager>.I.loungeScene;
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
    this.HomeCamera = ((Component) this).gameObject.AddComponent<HomeCamera>();
    this.IHomePeople = (IHomePeople) ((Component) this).gameObject.AddComponent<LoungePeople>();
    this.HomeFeatureBanner = ((Component) this).gameObject.AddComponent<HomeFeatureBanner>();
    this.TableSet = ((Component) this).gameObject.AddComponent<LoungeTableSet>();
    while (!this.HomeCamera.isInitialized || !this.IHomePeople.isInitialized || !this.TableSet.isInitialized)
      yield return (object) null;
    MonoBehaviourSingleton<LoungeMatchingManager>.I.OnChangeMemberStatus += new Action<LoungeMemberStatus>(this.OnChangeMemberStatus);
    if (LoungeMatchingManager.IsValidInLounge())
      MonoBehaviourSingleton<LoungeMatchingManager>.I.SendInLounge();
    this.IsInitialized = true;
    yield return (object) this.StartCoroutine(this.SendLoungeInfoForce());
    yield return (object) this.StartCoroutine(this.CreateLoungePlayerFromSlotInfo());
    yield return (object) this.StartCoroutine(this.LoadSE());
    this.PlayWaveSound();
  }

  private IEnumerator LoadSE()
  {
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    foreach (int se_id in (int[]) Enum.GetValues(typeof (LoungeManager.SE)))
      loadingQueue.CacheSE(se_id);
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
  }

  private void PlayWaveSound()
  {
    Transform gameObject = Utility.CreateGameObject("WaveAudioObjectPos", this._transform);
    gameObject.position = MonoBehaviourSingleton<OutGameSettingsManager>.I.loungeScene.waveSoundPoint;
    SoundManager.PlayLoopSE(40000363, (DisableNotifyMonoBehaviour) null, gameObject);
  }

  private IEnumerator CreateLoungePlayerFromSlotInfo()
  {
    if (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData != null)
    {
      List<PartyModel.SlotInfo> data = MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData.slotInfos;
      for (int i = 0; i < data.Count; ++i)
      {
        if (data[i].userInfo != null && data[i].userInfo.userId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
        {
          if (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeMemberStatus == null)
            break;
          switch (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeMemberStatus[data[i].userInfo.userId].GetStatus())
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
    PartyModel.SlotInfo slotInfoByUserId = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetSlotInfoByUserId(userId);
    if (slotInfoByUserId != null && this.IHomePeople != null && this.IHomePeople.CastToLoungePeople().CreateLoungePlayer(slotInfoByUserId, true))
    {
      this.SetAnnounce(new LoungeAnnounce.AnnounceData(LoungeAnnounce.ANNOUNCE_TYPE.JOIN_LOUNGE, slotInfoByUserId.userInfo.name));
      if (MonoBehaviourSingleton<LoungeNetworkManager>.IsValid())
        MonoBehaviourSingleton<LoungeNetworkManager>.I.JoinNotification(slotInfoByUserId.userInfo);
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
    PartyModel.SlotInfo slotInfoByUserId = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetSlotInfoByUserId(userId);
    this.IHomePeople.CastToLoungePeople().CreateLoungePlayer(slotInfoByUserId, true);
    this.IHomePeople.CastToLoungePeople().ChangeEquipLoungePlayer(slotInfoByUserId, true);
  }

  private void CreatePartyAnnounce(int userId)
  {
    this.NeedLoungeQuestBalloonUpdate = true;
    this.SetAnnounce(new LoungeAnnounce.AnnounceData(LoungeAnnounce.ANNOUNCE_TYPE.CREATED_PARTY, MonoBehaviourSingleton<LoungeMatchingManager>.I.GetSlotInfoByUserId(userId).userInfo.name));
  }

  private void SetAnnounce(LoungeAnnounce.AnnounceData data)
  {
    if (data == null)
      return;
    this.loungeAnnounceQueue.Enqueue(data);
    if (this.loungeAnnounceCoroutine != null)
      return;
    this.loungeAnnounceCoroutine = this.StartCoroutine(this.ShowAnnounce());
  }

  private IEnumerator ShowAnnounce()
  {
    LoungeAnnounce announce = MonoBehaviourSingleton<UIManager>.I.loungeAnnounce;
    if (Object.op_Equality((Object) announce, (Object) null))
    {
      this.loungeAnnounceCoroutine = (Coroutine) null;
    }
    else
    {
      while (this.loungeAnnounceQueue.Count > 0)
      {
        LoungeAnnounce.AnnounceData data = this.loungeAnnounceQueue.Dequeue();
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
    if (!this.sendInfoSpan.IsReady())
      return;
    this.StartCoroutine(this.SendLoungeInfoForce());
  }

  protected override void _OnDestroy()
  {
    MonoBehaviourSingleton<LoungeMatchingManager>.I.OnChangeMemberStatus -= new Action<LoungeMemberStatus>(this.OnChangeMemberStatus);
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
    while (MonoBehaviourSingleton<LoungeMatchingManager>.I.isResume)
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
    if (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData != null)
      return false;
    MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[2]
    {
      new EventData("MAIN_MENU_LOUNGE", (object) null),
      new EventData("LOUNGE_KICKED", (object) null)
    });
    MonoBehaviourSingleton<LoungeMatchingManager>.I.StopAFKCheck();
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
        if (MonoBehaviourSingleton<LoungeMatchingManager>.I.GetSlotInfoByUserId(userId) == null)
          this.IHomePeople.CastToLoungePeople().DestroyLoungePlayer(userId);
        else if (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeMemberStatus != null)
        {
          switch (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeMemberStatus[userId].GetStatus())
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
    if (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData != null)
    {
      List<PartyModel.SlotInfo> slots = MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData.slotInfos;
      for (int i = 0; i < slots.Count; ++i)
      {
        if (slots[i].userInfo != null)
        {
          int userId = slots[i].userInfo.userId;
          if (userId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id && MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeMemberStatus != null)
          {
            LoungeMemberStatus loungeMemberStatu = MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeMemberStatus[userId];
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

  private IEnumerator SendLoungeInfoForce()
  {
    bool wait = true;
    MonoBehaviourSingleton<LoungeMatchingManager>.I.SendInfo((Action<bool>) (is_success => wait = false), true);
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

  private enum SE
  {
    WAVE = 40000363, // 0x02625B6B
  }
}
