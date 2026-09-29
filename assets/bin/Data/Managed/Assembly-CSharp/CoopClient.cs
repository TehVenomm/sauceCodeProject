// Decompiled with JetBrains decompiler
// Type: CoopClient
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class CoopClient : MonoBehaviour
{
  public CoopClient.CLIENT_STATUS status { get; protected set; }

  public bool isLeave { get; protected set; }

  public CoopClient.CLIENT_JOIN_TYPE joinType { get; protected set; }

  public int clientId { get; protected set; }

  public int userId { get; protected set; }

  public string userToken { get; protected set; }

  public int slotIndex { get; protected set; }

  public CharaInfo userInfo { get; protected set; }

  public bool isPartyOwner { get; protected set; }

  public int loadingPer { get; protected set; }

  public int continueCount { get; protected set; }

  public int stageId { get; protected set; }

  public int seriesIndex { get; protected set; }

  public int exploreMapIndex => this.seriesIndex;

  public int rushIndex => this.seriesIndex;

  public bool isStageHost { get; protected set; }

  public int playerId { get; protected set; }

  public bool isSendPopOriginal { get; set; }

  public bool isSendStageInfo { get; set; }

  public bool isSeriesProgressEnd { get; protected set; }

  public bool isBattleRetire { get; protected set; }

  public CoopClientPacketReceiver packetReceiver { get; private set; }

  public int cachePlayerID { get; private set; }

  public List<int> cacheSubPlayerIDs { get; private set; }

  public bool isStageResponseEnd { get; set; }

  public CoopClient()
  {
    this.status = CoopClient.CLIENT_STATUS.NONE;
    this.slotIndex = -1;
    this.cacheSubPlayerIDs = new List<int>();
  }

  protected virtual void Awake()
  {
    this.packetReceiver = ((Component) this).gameObject.AddComponent<CoopClientPacketReceiver>();
  }

  protected virtual void Update() => this.packetReceiver.OnUpdate();

  protected virtual void Logd(string str, params object[] objs)
  {
    int num = Log.enabled ? 1 : 0;
  }

  public override string ToString()
  {
    return $"CoopClient[{(object) this.slotIndex}]({(object) this.status}/{this.isPartyOwner.ToString()}/{this.isStageHost.ToString()}).userId={(object) this.userId}";
  }

  public void Clear()
  {
    this.status = CoopClient.CLIENT_STATUS.NONE;
    this.isLeave = false;
    this.clientId = 0;
    this.userId = 0;
    this.userToken = "";
    this.slotIndex = -1;
    this.userInfo = (CharaInfo) null;
    this.isPartyOwner = false;
    this.loadingPer = 0;
    this.continueCount = 0;
    this.stageId = 0;
    this.seriesIndex = 0;
    this.isStageHost = false;
    this.playerId = 0;
    this.isSendPopOriginal = false;
    this.isSendStageInfo = false;
    this.isSeriesProgressEnd = false;
    this.isBattleRetire = false;
    this.cachePlayerID = 0;
    this.cacheSubPlayerIDs.Clear();
    this.isStageResponseEnd = false;
    this.joinType = CoopClient.CLIENT_JOIN_TYPE.NONE;
  }

  public void OnStageChangeInterval()
  {
    this.playerId = 0;
    this.isSendPopOriginal = false;
    this.isSendStageInfo = false;
    this.isSeriesProgressEnd = false;
    this.isBattleRetire = false;
    this.cachePlayerID = 0;
    this.cacheSubPlayerIDs.Clear();
    this.isStageResponseEnd = false;
    this.loadingPer = 0;
    this.SetStatus(CoopClient.CLIENT_STATUS.STAGE_START);
  }

  public void OnQuestSeriesInterval()
  {
    this.playerId = 0;
    this.isSendPopOriginal = false;
    this.isSendStageInfo = false;
    this.isSeriesProgressEnd = false;
    this.cachePlayerID = 0;
    this.cacheSubPlayerIDs.Clear();
    this.isStageResponseEnd = false;
    this.loadingPer = 0;
    this.SetStatus(CoopClient.CLIENT_STATUS.STAGE_START);
  }

  public virtual void Init(int client_id)
  {
    this.Clear();
    this.clientId = client_id;
  }

  public void Activate(int user_id, string token, CharaInfo user_info, int slot_index)
  {
    this.userId = user_id;
    this.userToken = token;
    this.slotIndex = slot_index;
    this.userInfo = user_info;
    this.isPartyOwner = PartyManager.IsValidInParty() && MonoBehaviourSingleton<PartyManager>.I.GetOwnerUserId() == this.userId;
    if (QuestManager.IsValidExplore())
      MonoBehaviourSingleton<QuestManager>.I.ActivateExplorePlayerStatus(this);
    this.Logd("Activate.");
  }

  public void Deactivate()
  {
    this.userId = 0;
    this.userToken = "";
    this.slotIndex = -1;
    this.userInfo = (CharaInfo) null;
    this.Logd("Deactivate.");
  }

  protected virtual void SetStatus(CoopClient.CLIENT_STATUS st)
  {
    this.Logd("status {0} => {1}", (object) this.status, (object) st);
    this.status = st;
    if (this.status != CoopClient.CLIENT_STATUS.LOADING_FINISH)
      return;
    this.loadingPer = 100;
  }

  public virtual void SetUserInfo(CharaInfo user_info) => this.userInfo = user_info;

  public virtual void SetLoadingPer(int per) => this.loadingPer = per;

  public virtual string GetPlayerName()
  {
    Player player = this.GetPlayer();
    if (Object.op_Inequality((Object) player, (Object) null))
      return player.charaName;
    return this.userInfo != null ? this.userInfo.name : "player" + (object) this.playerId;
  }

  public void SetStage(int stage_id, int series_index, bool is_host)
  {
    this.stageId = stage_id;
    this.seriesIndex = series_index;
    this.isStageHost = is_host;
  }

  public void SetStageHost(bool is_host) => this.isStageHost = is_host;

  public bool IsActivate() => this.userId > 0;

  public bool IsPlayingStage()
  {
    return this.status == CoopClient.CLIENT_STATUS.STAGE_REQUEST || this.status == CoopClient.CLIENT_STATUS.BATTLE_START;
  }

  public bool IsStageStart() => this.status >= CoopClient.CLIENT_STATUS.STAGE_START;

  public bool IsLoadingStart() => this.status >= CoopClient.CLIENT_STATUS.LOADING_START;

  public bool IsStageRequest() => this.status >= CoopClient.CLIENT_STATUS.STAGE_REQUEST;

  public bool IsBattleStart() => this.status >= CoopClient.CLIENT_STATUS.BATTLE_START;

  public bool IsBattleEnd() => this.status >= CoopClient.CLIENT_STATUS.BATTLE_END;

  public void SetPlayerID(int id)
  {
    this.Logd("playerId {0} => {1}", (object) this.playerId, (object) id);
    this.playerId = id;
  }

  public bool IsPlayerPop() => this.playerId > 0;

  public Player GetPlayer()
  {
    if (this.playerId <= 0)
      return (Player) null;
    Player player = (Player) null;
    if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
      player = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(this.playerId) as Player;
    return player;
  }

  public void SetCachePlayer(int player_id, bool sub)
  {
    if (sub)
      this.cachePlayerID = player_id;
    else
      this.cacheSubPlayerIDs.Add(player_id);
  }

  public void OnPlayerPop(int player_id)
  {
    if (this.cachePlayerID != 0 && this.cachePlayerID == player_id)
    {
      this.SetPlayerID(player_id);
      this.cachePlayerID = 0;
    }
    int index = 0;
    while (index < this.cacheSubPlayerIDs.Count)
    {
      if (this.cacheSubPlayerIDs[index] != 0 && this.cacheSubPlayerIDs[index] == player_id)
        this.cacheSubPlayerIDs.RemoveAt(index);
      else
        ++index;
    }
  }

  public void PopCachePlayer(StageObject.COOP_MODE_TYPE coop_mode = StageObject.COOP_MODE_TYPE.NONE)
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return;
    Vector3 center_pos = Vector3.zero;
    if (Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.boss, (Object) null))
      center_pos = MonoBehaviourSingleton<StageObjectManager>.I.boss._transform.position;
    if (this.cachePlayerID != 0 && !this.IsPlayerPop())
    {
      int cachePlayerId = this.cachePlayerID;
      this.cachePlayerID = 0;
      Player cache = MonoBehaviourSingleton<StageObjectManager>.I.FindCache(cachePlayerId) as Player;
      if (Object.op_Inequality((Object) cache, (Object) null))
      {
        MonoBehaviourSingleton<StageObjectManager>.I.RemoveCacheObject((StageObject) cache);
        ((Component) cache).gameObject.SetActive(true);
        this.SetPlayerID(cachePlayerId);
        cache.SetAppearPosGuest(center_pos);
        if (coop_mode != StageObject.COOP_MODE_TYPE.NONE)
          cache.SetCoopMode(coop_mode, this.clientId);
      }
    }
    int index = 0;
    for (int count = this.cacheSubPlayerIDs.Count; index < count; ++index)
    {
      Player cache = MonoBehaviourSingleton<StageObjectManager>.I.FindCache(this.cacheSubPlayerIDs[index]) as Player;
      if (Object.op_Inequality((Object) cache, (Object) null))
      {
        MonoBehaviourSingleton<StageObjectManager>.I.RemoveCacheObject((StageObject) cache);
        ((Component) cache).gameObject.SetActive(true);
        cache.SetAppearPosGuest(center_pos);
        if (coop_mode != StageObject.COOP_MODE_TYPE.NONE)
          cache.SetCoopMode(coop_mode, this.clientId);
      }
    }
    this.cacheSubPlayerIDs.Clear();
  }

  public virtual void OnRoomLeaved()
  {
    this.Logd("OnRoomLeaved.");
    bool flag = this.IsBattleEnd();
    this.isLeave = true;
    if (MonoBehaviourSingleton<CoopManager>.I.coopStage.stageId != this.stageId || this.isBattleRetire || !this.IsStageStart() || flag || this.userInfo == null || !QuestManager.IsValidInGame())
      return;
    uint id = 101;
    if (this.isPartyOwner && QuestManager.IsValidInGameExplore())
    {
      if (MonoBehaviourSingleton<InGameProgress>.IsValid())
        MonoBehaviourSingleton<InGameProgress>.I.ResetExploreHostDCTimer();
      else if (QuestManager.IsValidInGameExplore())
        MonoBehaviourSingleton<QuestManager>.I.UpdateExploreHostDCTime(30f);
    }
    UIInGamePopupDialog.PushOpen(StringTable.Format(STRING_CATEGORY.IN_GAME, id, (object) this.GetPlayerName()), false);
  }

  public virtual bool OnRecvClientStatus(Coop_Model_ClientStatus model, CoopPacket packet)
  {
    if (this.isLeave)
      MonoBehaviourSingleton<CoopManager>.I.coopMyClient.WelcomeClient(this.clientId);
    this.SetStatus((CoopClient.CLIENT_STATUS) model.status);
    this.joinType = (CoopClient.CLIENT_JOIN_TYPE) model.joinType;
    if (model.status == 5)
    {
      if (this.isStageHost && MonoBehaviourSingleton<CoopManager>.I.coopStage.isRecvStageInfo && MonoBehaviourSingleton<InGameProgress>.IsValid())
        MonoBehaviourSingleton<InGameProgress>.I.StartTimer();
      if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
      {
        List<StageObject> stageObjectList = new List<StageObject>((IEnumerable<StageObject>) MonoBehaviourSingleton<StageObjectManager>.I.cacheList);
        int index = 0;
        for (int count = stageObjectList.Count; index < count; ++index)
        {
          Player player = stageObjectList[index] as Player;
          if (!Object.op_Equality((Object) player, (Object) null) && player.coopClientId == this.clientId && player.isWaitBattleStart)
          {
            MonoBehaviourSingleton<StageObjectManager>.I.RemoveCacheObject((StageObject) player);
            ((Component) player).gameObject.SetActive(true);
            player.ActBattleStart();
          }
        }
      }
    }
    if (this.isPartyOwner && model.status == 6)
      MonoBehaviourSingleton<CoopManager>.I.coopRoom.SetOwnerCleared();
    if (MonoBehaviourSingleton<CoopManager>.I.coopRoom.NeedsForceLeave())
      MonoBehaviourSingleton<CoopNetworkManager>.I.Close();
    return true;
  }

  public virtual bool OnRecvClientLoadingProgress(Coop_Model_ClientLoadingProgress model)
  {
    this.SetLoadingPer(model.per);
    return true;
  }

  public virtual bool OnRecvClientChangeEquip(Coop_Model_ClientChangeEquip model)
  {
    if (this.userInfo != null)
    {
      this.userInfo = model.userInfo;
      if (MonoBehaviourSingleton<GameSceneManager>.IsValid())
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.RECEIVE_COOP_ROOM_UPDATE);
    }
    return true;
  }

  public virtual bool OnRecvClientBattleRetire(Coop_Model_ClientBattleRetire model)
  {
    this.isBattleRetire = true;
    if (this.isPartyOwner)
      MonoBehaviourSingleton<CoopManager>.I.coopRoom.ownerRetire = true;
    if (QuestManager.IsValidInGame() && MonoBehaviourSingleton<UIDeadAnnounce>.IsValid())
      MonoBehaviourSingleton<UIDeadAnnounce>.I.Announce(UIDeadAnnounce.ANNOUNCE_TYPE.RETIRE, this.GetPlayer());
    if (!this.isStageHost)
      CoopStageObjectUtility.TransfarOwnerForClientObjects(this.clientId, MonoBehaviourSingleton<CoopManager>.I.coopStage.hostClientId);
    if (QuestManager.IsValidInGameExplore())
      MonoBehaviourSingleton<QuestManager>.I.RemoveExplorePlayerStatus(this);
    if (MonoBehaviourSingleton<CoopManager>.I.coopRoom.NeedsForceLeave())
      MonoBehaviourSingleton<CoopNetworkManager>.I.Close();
    return true;
  }

  public virtual bool OnRecvClientSeriesProgress(Coop_Model_ClientSeriesProgress model)
  {
    this.isSeriesProgressEnd = true;
    if ((long) model.ep == (long) MonoBehaviourSingleton<QuestManager>.I.currentQuestSeriesIndex && MonoBehaviourSingleton<InGameProgress>.IsValid() && !MonoBehaviourSingleton<InGameProgress>.I.isGameProgressStop)
      MonoBehaviourSingleton<InGameProgress>.I.PortalNext(0U);
    return true;
  }

  public enum CLIENT_STATUS
  {
    NONE,
    STAGE_START,
    LOADING_START,
    LOADING_FINISH,
    STAGE_REQUEST,
    BATTLE_START,
    BATTLE_END,
  }

  public enum CLIENT_JOIN_TYPE
  {
    NONE,
    FROM_QUEST_LIST,
    FROM_FIELD,
  }
}
