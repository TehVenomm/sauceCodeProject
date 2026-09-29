// Decompiled with JetBrains decompiler
// Type: CoopStage
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
public class CoopStage : MonoBehaviour
{
  private bool isStageRequested;
  private SpanTimer closeEntrySpan = new SpanTimer(5f);
  public List<List<int>> bossBreakIDLists;
  public float bossStartHpDamageRate;
  public int bossDropNormal;
  public int bossDropRare;
  public BattleUserLog battleUserLog = new BattleUserLog();
  public FieldRewardPool fieldRewardPool = new FieldRewardPool();
  public bool isAsyncLoop;
  public ChatCoopConnection chatConnection;
  protected float checkCharacterSyncTimer;
  private bool forceNextWave;
  private List<CoopStage.DefeatFieldEnemyDelivery> defeatFieldEnemyDeliveryList = new List<CoopStage.DefeatFieldEnemyDelivery>();
  private bool fieldEnemyBossEntering;
  private bool readyToPlay;
  private bool isEnterFieldEnemyBossBattle;
  private bool isInFieldEnemyBossBattle;
  private bool requestedEnemyBossAlive;
  private Coroutine specialEnemyAnnounceExist;
  private Coroutine specialEnemyAnnounceGone;
  private ENEMY_POP_TYPE currentAnnounceType;
  private bool isInFieldFishingEnemyBattle;
  private bool isPresentQuest;
  private Coop_Model_WaveMatchInfo firstWaveMatchInfo;
  private float firstWaveMatchPopSec;
  private bool firstWaveMatchSet = true;

  public CoopStagePacketSender packetSender { get; private set; }

  public CoopStagePacketReceiver packetReceiver { get; private set; }

  public int stageId { get; private set; }

  public int stageIndex { get; private set; }

  public int hostClientId { get; private set; }

  public bool isActivateStart { get; private set; }

  public bool isRecvStageInfo { get; private set; }

  public bool isEnemyExtermination { get; private set; }

  public bool isEntryClose { get; protected set; }

  public bool isQuestClose { get; protected set; }

  public bool isQuestSucceed { get; protected set; }

  public bool isHostStageResponseEnd { get; protected set; }

  public bool isRecvRushRequested { get; private set; }

  public void SetReadyToPlay() => this.readyToPlay = true;

  public void SetIsEnterFieldEnemyBossBattle(bool isEnterFieldEnemyBossBattle)
  {
    this.isEnterFieldEnemyBossBattle = isEnterFieldEnemyBossBattle;
  }

  public bool GetIsInFieldEnemyBossBattle() => this.isInFieldEnemyBossBattle;

  public bool HasFieldEnemyBossLimitTime()
  {
    return this.isInFieldEnemyBossBattle && !this.isInFieldFishingEnemyBattle;
  }

  public bool GetisInFieldFishingEnemyBattle() => this.isInFieldFishingEnemyBattle;

  public CoopStage()
  {
    this.isStageRequested = false;
    this.isRecvStageInfo = false;
  }

  private void Awake()
  {
    this.packetSender = ((Component) this).gameObject.AddComponent<CoopStagePacketSender>();
    this.packetReceiver = ((Component) this).gameObject.AddComponent<CoopStagePacketReceiver>();
  }

  private void Update()
  {
    this.packetReceiver.OnUpdate();
    this.fieldRewardPool.OnUpdate();
    if (this.closeEntrySpan.IsReady())
      this.CheckEntryClose();
    if (MonoBehaviourSingleton<CoopManager>.I.coopMyClient.IsBattleStart() && MonoBehaviourSingleton<StageObjectManager>.IsValid())
    {
      this.checkCharacterSyncTimer -= Time.deltaTime;
      if ((double) this.checkCharacterSyncTimer <= 0.0)
      {
        this.CheckCharacterSync();
        this.checkCharacterSyncTimer = MonoBehaviourSingleton<InGameSettingsManager>.I.room.checkCharacterSyncInterval;
      }
    }
    this.CheckFirstWaveMatchInfo();
  }

  private void CheckCharacterSync()
  {
    if (!this.isActivateStart || !MonoBehaviourSingleton<CoopManager>.I.coopMyClient.IsBattleStart() || !MonoBehaviourSingleton<StageObjectManager>.IsValid() || MonoBehaviourSingleton<CoopManager>.I.coopRoom.isOfflinePlay)
      return;
    this.ForEachClients((Action<CoopClient>) (c =>
    {
      if (c is CoopMyClient || !c.IsBattleStart() || c.isBattleRetire || c.IsBattleEnd() || c.isLeave)
        return;
      if (c.IsPlayerPop())
      {
        Player player = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(c.playerId) as Player;
        if (Object.op_Equality((Object) player, (Object) null))
          player = MonoBehaviourSingleton<StageObjectManager>.I.FindCache(c.playerId) as Player;
        if (Object.op_Inequality((Object) player, (Object) null))
          return;
      }
      this.packetSender.SendRequestPop(c.clientId, true, true);
    }));
    int index = 0;
    for (int count = MonoBehaviourSingleton<StageObjectManager>.I.cacheList.Count; index < count; ++index)
    {
      Player cache = MonoBehaviourSingleton<StageObjectManager>.I.cacheList[index] as Player;
      if (Object.op_Inequality((Object) cache, (Object) null) && cache.IsPuppet() && Object.op_Inequality((Object) cache.playerSender, (Object) null))
        cache.playerSender.OnLoadComplete(false);
    }
  }

  private void Logd(string str, params object[] objs)
  {
    int num = Log.enabled ? 1 : 0;
  }

  public void Clear()
  {
    this.stageId = 0;
    this.stageIndex = 0;
    this.hostClientId = 0;
    this.isActivateStart = false;
    this.isStageRequested = false;
    this.isRecvStageInfo = false;
    this.isEnemyExtermination = false;
    this.isEntryClose = false;
    this.isQuestClose = false;
    this.isQuestSucceed = false;
    this.bossBreakIDLists = (List<List<int>>) null;
    this.bossStartHpDamageRate = 0.0f;
    this.bossDropNormal = 0;
    this.bossDropRare = 0;
    this.battleUserLog.Clear();
    this.fieldRewardPool.Clear();
    this.chatConnection = (ChatCoopConnection) null;
    this.isHostStageResponseEnd = false;
    this.readyToPlay = false;
    this.requestedEnemyBossAlive = false;
    this.SetFalseEnemyBossBattleFlag();
    this.ClearAllSpecialEnemyAnnounce();
    if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
      return;
    this.checkCharacterSyncTimer = MonoBehaviourSingleton<InGameSettingsManager>.I.room.checkCharacterSyncInterval;
  }

  public void OnStageChangeInterval()
  {
    this.isActivateStart = false;
    this.isStageRequested = false;
    this.isRecvStageInfo = false;
    this.isEnemyExtermination = false;
    this.isEntryClose = false;
    this.isQuestSucceed = false;
    this.isQuestClose = false;
    this.bossBreakIDLists = (List<List<int>>) null;
    this.bossStartHpDamageRate = 0.0f;
    this.bossDropNormal = 0;
    this.bossDropRare = 0;
    this.battleUserLog.Clear();
    this.fieldRewardPool.Clear();
    this.isHostStageResponseEnd = false;
    if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
      return;
    this.checkCharacterSyncTimer = MonoBehaviourSingleton<InGameSettingsManager>.I.room.checkCharacterSyncInterval;
  }

  public void OnQuestSeriesInterval()
  {
    this.isActivateStart = false;
    this.isStageRequested = false;
    this.isRecvStageInfo = false;
    this.isEnemyExtermination = false;
    this.isHostStageResponseEnd = false;
    if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
      return;
    this.checkCharacterSyncTimer = MonoBehaviourSingleton<InGameSettingsManager>.I.room.checkCharacterSyncInterval;
  }

  public void ForEachClients(Action<CoopClient> action)
  {
    MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.ForEach((Action<CoopClient>) (c =>
    {
      if (c.stageId != this.stageId)
        return;
      action(c);
    }));
  }

  public int GetClientCount()
  {
    int count = 0;
    MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.ForEach((Action<CoopClient>) (c =>
    {
      if (c.stageId != this.stageId || !c.IsPlayingStage())
        return;
      ++count;
    }));
    return count;
  }

  public bool IsSolo() => this.GetClientCount() <= 1;

  public void OnInitStage(int stage_id, int stage_idx, int host_client_id)
  {
    this.stageId = stage_id;
    this.stageIndex = stage_idx;
    this.hostClientId = host_client_id;
    this.Logd("OnInitStage: stageId={0}, stageIndex={1}, hostClientId={2}.", (object) stage_id, (object) stage_idx, (object) host_client_id);
  }

  public void OnJoinClient(CoopClient client)
  {
    this.Logd("OnJoinClient: client={0}.", (object) client);
    if (client.isStageHost && client.clientId != this.hostClientId)
      this.SetHostClient(client.clientId);
    client.OnStageChangeInterval();
  }

  public void OnLeaveClient(CoopClient client, int host_client_id)
  {
    this.Logd("OnLeaveClient: client={0}.", (object) client);
    if (this.chatConnection != null && QuestManager.IsValidInGame() && !(client is CoopMyClient) && Object.op_Inequality((Object) client.GetPlayer(), (Object) null))
      this.chatConnection.OnReceiveNotification(StringTable.Format(STRING_CATEGORY.CHAT, 6U, (object) client.GetPlayerName()));
    client.PopCachePlayer(StageObject.COOP_MODE_TYPE.PUPPET);
    CoopStageObjectUtility.TransfarOwnerForClientObjects(client.clientId, host_client_id);
    if (this.hostClientId != host_client_id)
      this.SetHostClient(host_client_id);
    if (FieldManager.IsValidInGameNoBoss() || MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsDefenseBattle())
    {
      Player player = client.GetPlayer();
      if (Object.op_Inequality((Object) player, (Object) null) && !(player is Self))
        player.DestroyObject();
    }
    if (!(client is CoopMyClient))
      client.OnStageChangeInterval();
    if (!this.IsSolo())
      return;
    this.OnSwitchSolo();
  }

  private void OnSwitchSolo()
  {
    this.Logd("OnSwitchSolo.");
    CoopStageObjectUtility.SetCoopModeForAll(StageObject.COOP_MODE_TYPE.NONE, 0);
  }

  public void OnSwitchOfflinePlay()
  {
    this.Logd("OnSwitchOfflinePlay... my status={0}", (object) MonoBehaviourSingleton<CoopManager>.I.coopMyClient.status);
    this.ForEachClients((Action<CoopClient>) (c =>
    {
      if (c is CoopMyClient)
        return;
      c.PopCachePlayer();
      if (c.IsPlayerPop() || c.userInfo == null || c.userInfo.equipSet.Count <= 0)
        return;
      int playerId = MonoBehaviourSingleton<CoopManager>.I.GetPlayerID(c);
      if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
        return;
      MonoBehaviourSingleton<StageObjectManager>.I.CreateGuest(playerId, c.userInfo);
      c.SetPlayerID(playerId);
    }));
    if (!MonoBehaviourSingleton<CoopManager>.I.coopMyClient.IsBattleStart())
      CoopStageObjectUtility.FillNonPlayer(4, this.GetClientCount());
    CoopStageObjectUtility.SetOfflineForAll();
    if (!QuestManager.IsValidInGame())
      CoopStageObjectUtility.OnlySelf();
    CoopStageObjectUtility.ShrinkOriginalNonPlayer(4);
    if (!QuestManager.IsValidInGameDefenseBattle())
      return;
    CoopStageObjectUtility.DestroyAllNonPlayer();
  }

  public void OnSwitchHost()
  {
    if (!this.isActivateStart || FieldManager.IsValidInGameNoQuest())
      return;
    this.ForEachClients((Action<CoopClient>) (c =>
    {
      if (c is CoopMyClient)
        return;
      c.PopCachePlayer();
      if (c.IsPlayerPop() || c.isStageResponseEnd || c.userInfo == null || c.userInfo.equipSet.Count <= 0)
        return;
      int playerId = MonoBehaviourSingleton<CoopManager>.I.GetPlayerID(c);
      if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
        return;
      Player guest = MonoBehaviourSingleton<StageObjectManager>.I.CreateGuest(playerId, c.userInfo, (PlayerLoader.OnCompleteLoad) (o =>
      {
        Player player = o as Player;
        if (MonoBehaviourSingleton<CoopManager>.I.coopRoom.IsBattle())
        {
          player.ActBattleStart();
        }
        else
        {
          if (!Object.op_Inequality((Object) player.controller, (Object) null))
            return;
          player.controller.SetEnableControll(false, ControllerBase.DISABLE_FLAG.BATTLE_START);
        }
      }));
      c.SetPlayerID(playerId);
      CoopStageObjectUtility.SetAI((Character) guest);
    }));
    if (!MonoBehaviourSingleton<CoopManager>.I.coopStage.isHostStageResponseEnd)
      CoopStageObjectUtility.FillNonPlayer(4, MonoBehaviourSingleton<CoopManager>.I.coopStage.GetClientCount());
    CoopStageObjectUtility.ShrinkOriginalNonPlayer(4);
    if (QuestManager.IsValidInGameDefenseBattle())
      CoopStageObjectUtility.DestroyAllNonPlayer();
    MonoBehaviourSingleton<CoopManager>.I.coopStage.SendOriginalObjectPop(0);
  }

  public void OnPlayerPop(int player_id)
  {
  }

  public void SetHostClient(int host_client_id)
  {
    if (this.hostClientId == host_client_id)
      return;
    if (host_client_id == 0)
    {
      MonoBehaviourSingleton<CoopNetworkManager>.I.LoopBackRoomLeave();
    }
    else
    {
      CoopClient byClientId1 = MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.FindByClientId(this.hostClientId);
      CoopClient byClientId2 = MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.FindByClientId(host_client_id);
      this.Logd("SetHostClient: SwitchHost: old host={0}", (object) byClientId1);
      this.Logd("SetHostClient: SwitchHost: new host={0}", (object) byClientId2);
      if (Object.op_Inequality((Object) byClientId2, (Object) null) && byClientId2 is CoopMyClient)
      {
        this.ReSendStageInfo();
        MonoBehaviourSingleton<CoopManager>.I.coopStage.OnSwitchHost();
        if (!byClientId2.IsBattleStart() && Object.op_Inequality((Object) byClientId1, (Object) null) && byClientId1.IsBattleStart() && QuestManager.IsValidInGame())
        {
          if (MonoBehaviourSingleton<InGameManager>.I.IsRush())
          {
            if (!this.IsOtherClientProgressed())
              MonoBehaviourSingleton<CoopNetworkManager>.I.LoopBackRoomLeave();
          }
          else if (QuestManager.IsValidInGameExplore())
          {
            if (MonoBehaviourSingleton<QuestManager>.I.IsExploreBossMap())
              MonoBehaviourSingleton<CoopNetworkManager>.I.LoopBackRoomLeave();
          }
          else
            MonoBehaviourSingleton<CoopNetworkManager>.I.LoopBackRoomLeave();
        }
      }
      if (Object.op_Inequality((Object) byClientId1, (Object) null) && byClientId1.stageId == this.stageId)
        byClientId1.SetStageHost(false);
      if (Object.op_Inequality((Object) byClientId2, (Object) null) && byClientId2.stageId == this.stageId)
        byClientId2.SetStageHost(true);
      this.Logd("SetHostClient: hostClientId {0} => {1}", (object) this.hostClientId, (object) host_client_id);
      this.hostClientId = host_client_id;
    }
  }

  private bool IsWait(Func<bool> cb) => this.isAsyncLoop || !cb();

  private bool IsAsyncWait(Func<bool> cb)
  {
    if (this.isAsyncLoop)
      return true;
    return (MonoBehaviourSingleton<KtbWebSocket>.I.IsOpen() || CoopOfflineManager.IsValidActivate()) && !cb();
  }

  private bool IsGuestAsyncWait(Func<bool> cb)
  {
    if (this.isAsyncLoop)
      return true;
    return MonoBehaviourSingleton<KtbWebSocket>.I.IsOpen() && !MonoBehaviourSingleton<CoopManager>.I.isStageHost && !cb();
  }

  public IEnumerator DoActivate()
  {
    this.isActivateStart = true;
    if (MonoBehaviourSingleton<CoopOfflineManager>.IsValid())
      MonoBehaviourSingleton<CoopOfflineManager>.I.OnStageActivate();
    if (QuestManager.IsValidInGame() && !FieldManager.IsValidInGameNoBoss())
      this.InitBossBreakIdList();
    if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.self, (Object) null))
    {
      Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
      self.id = MonoBehaviourSingleton<CoopManager>.I.GetSelfID();
      if (self.record != null)
        self.record.id = self.id;
      self.SetCoopMode(StageObject.COOP_MODE_TYPE.NONE, 0);
      MonoBehaviourSingleton<CoopManager>.I.coopMyClient.SetPlayerID(self.id);
    }
    this.forceNextWave = false;
    MonoBehaviourSingleton<CoopManager>.I.coopMyClient.StageRequest();
    MonoBehaviourSingleton<CoopNetworkManager>.I.RoomStageRequest();
    while (this.IsAsyncWait((Func<bool>) (() => this.isStageRequested)))
      yield return (object) null;
    if (this.IsOtherClientProgressed())
    {
      this.ForceProgressNextWave();
    }
    else
    {
      if (MonoBehaviourSingleton<InGameManager>.I.isValidGimmickObject)
      {
        this.Logd("Activate: Gimmick wait...");
        while (this.IsGuestAsyncWait((Func<bool>) (() => this.isRecvStageInfo)))
        {
          if (this.IsOtherClientProgressed())
          {
            this.ForceProgressNextWave();
            yield break;
          }
          yield return (object) null;
        }
      }
      if (QuestManager.IsValidInGameExplore())
      {
        while (this.IsGuestAsyncWait((Func<bool>) (() => this.isRecvStageInfo)))
          yield return (object) null;
      }
      bool rushTryReentry = false;
      if (MonoBehaviourSingleton<InGameManager>.I.IsNeedInitBoss())
      {
        Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
        float waitStart = Time.time;
        while (this.IsWait((Func<bool>) (() =>
        {
          boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
          if (Object.op_Equality((Object) boss, (Object) null))
          {
            if (QuestManager.IsValidInGameExplore() && MonoBehaviourSingleton<QuestManager>.I.IsExploreBossDead())
              return true;
            float num = Time.time - waitStart;
            if (MonoBehaviourSingleton<InGameManager>.I.IsRush() && ((double) num > 15.0 || !MonoBehaviourSingleton<KtbWebSocket>.I.IsOpen()))
            {
              this.Logd("Give up Boss wait.");
              rushTryReentry = true;
              return true;
            }
            if (QuestManager.IsValidInGameSeries() && ((double) num > 15.0 || !MonoBehaviourSingleton<KtbWebSocket>.I.IsOpen()))
            {
              rushTryReentry = true;
              return true;
            }
            return QuestManager.IsValidInGameWaveMatch();
          }
          return boss.IsOriginal() || boss.IsCoopNone() ? boss.isInitialized : boss.isCoopInitialized;
        })))
        {
          if (this.IsOtherClientProgressed())
          {
            this.ForceProgressNextWave();
            yield break;
          }
          yield return (object) null;
        }
        if (Object.op_Inequality((Object) boss, (Object) null))
        {
          if (boss.IsOriginal() || boss.IsCoopNone())
            MonoBehaviourSingleton<StageObjectManager>.I.self.SetAppearPosOwner(boss._position);
          else
            MonoBehaviourSingleton<StageObjectManager>.I.self.SetAppearPosGuest(boss._position);
          if (MonoBehaviourSingleton<InGameManager>.I.IsRush() && MonoBehaviourSingleton<CoopManager>.I.isStageHost && MonoBehaviourSingleton<InGameManager>.I.isRushReentry)
            MonoBehaviourSingleton<InGameManager>.I.RestoreRushInReentry();
          if (MonoBehaviourSingleton<QuestManager>.I.IsCurrentQuestTypeSeries() && MonoBehaviourSingleton<CoopManager>.I.isStageHost && MonoBehaviourSingleton<InGameManager>.I.isSeriesReentry)
            MonoBehaviourSingleton<InGameManager>.I.RestoreSeriesInReentry();
        }
      }
      if (Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.boss, (Object) null) && this.bossBreakIDLists != null)
        this.bossBreakIDLists[(int) MonoBehaviourSingleton<QuestManager>.I.currentQuestSeriesIndex] = MonoBehaviourSingleton<StageObjectManager>.I.boss.GetBreakRegionIDList();
      if (Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.self, (Object) null))
      {
        this.Logd("Activate: Self poss wait...");
        while (this.IsWait((Func<bool>) (() =>
        {
          if (MonoBehaviourSingleton<InGameManager>.I.IsRush() && (MonoBehaviourSingleton<InGameProgress>.I.progressEndType == InGameProgress.PROGRESS_END_TYPE.FIELD_REENTRY || !MonoBehaviourSingleton<KtbWebSocket>.I.IsOpen()))
          {
            rushTryReentry = true;
            return true;
          }
          if (QuestManager.IsValidInGameSeries() && (MonoBehaviourSingleton<InGameProgress>.I.progressEndType == InGameProgress.PROGRESS_END_TYPE.FIELD_REENTRY || !MonoBehaviourSingleton<KtbWebSocket>.I.IsOpen()))
          {
            rushTryReentry = true;
            return true;
          }
          if (!QuestManager.IsValidInGameWaveMatch() || MonoBehaviourSingleton<InGameProgress>.I.progressEndType != InGameProgress.PROGRESS_END_TYPE.FIELD_REENTRY && MonoBehaviourSingleton<KtbWebSocket>.I.IsOpen())
            return MonoBehaviourSingleton<StageObjectManager>.I.self.isSetAppearPos;
          rushTryReentry = true;
          return true;
        })))
        {
          if (this.IsOtherClientProgressed())
          {
            this.ForceProgressNextWave();
            yield break;
          }
          yield return (object) null;
        }
      }
      if (rushTryReentry)
      {
        this.Logd("Rush try reentry.");
        MonoBehaviourSingleton<InGameProgress>.I.FieldReentry();
      }
      this.isPresentQuest = false;
      int eventId = Utility.GetCurrentEventID();
      if (eventId > 0)
      {
        Network.EventData eventData = MonoBehaviourSingleton<QuestManager>.I.eventList.Where<Network.EventData>((Func<Network.EventData, bool>) (e => e.eventId == eventId)).FirstOrDefault<Network.EventData>();
        if (eventData != null && eventData.eventTypeEnum == EVENT_TYPE.PRESENT_QUEST)
          this.isPresentQuest = true;
      }
      this.Logd("Activate: Start battle.");
      this.StartBattle();
    }
  }

  public bool IsPresentQuest() => this.isPresentQuest;

  public void Deactivate() => this.Clear();

  public void InitBossBreakIdList()
  {
    this.Logd("Activate: Boss break list stock.");
    if (this.bossBreakIDLists != null)
      return;
    this.bossBreakIDLists = new List<List<int>>();
    int num = 0;
    for (int currentQuestSeriesNum = MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestSeriesNum(); num < currentQuestSeriesNum; ++num)
      this.bossBreakIDLists.Add(new List<int>());
  }

  private void StartBattle()
  {
    if (MonoBehaviourSingleton<CoopManager>.I.coopMyClient.IsBattleStart())
      return;
    this.Logd("StartBattle!");
    MonoBehaviourSingleton<CoopManager>.I.coopMyClient.StartBattle();
    MonoBehaviourSingleton<CoopManager>.I.coopRoom.StartBattle();
    MonoBehaviourSingleton<CoopNetworkManager>.I.BattleStart();
    if (MonoBehaviourSingleton<CoopManager>.I.isStageHost && MonoBehaviourSingleton<InGameProgress>.IsValid())
      MonoBehaviourSingleton<InGameProgress>.I.StartTimer();
    if (MonoBehaviourSingleton<InGameProgress>.IsValid())
    {
      MonoBehaviourSingleton<InGameProgress>.I.BattleStart();
      if (InGameManager.IsReentry() && !CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected())
        MonoBehaviourSingleton<InGameProgress>.I.FieldReentry();
    }
    this.fieldRewardPool.SetFieldId(MonoBehaviourSingleton<FieldManager>.I.GetFieldId());
    int mapId = MonoBehaviourSingleton<FieldManager>.I.GetMapId();
    if (QuestManager.IsValidInGameExplore())
      mapId = MonoBehaviourSingleton<QuestManager>.I.ExploreMapIndexToId(MonoBehaviourSingleton<CoopManager>.I.coopMyClient.exploreMapIndex);
    this.fieldRewardPool.SetMapId(mapId);
    if (QuestManager.IsValidInGameExplore() && Singleton<QuestTable>.IsValid() && Singleton<EnemyTable>.IsValid())
    {
      MonoBehaviourSingleton<FieldManager>.I.SendFieldQuestMapChange(mapId, (Action<bool, Error>) ((succeeded, error) => { }));
      this.CheckBossTrace(mapId);
    }
    if (FieldManager.IsValidInGameNoQuest())
      this.SetSpecialEnemyAnounceIfNeed(mapId);
    MonoBehaviourSingleton<InGameManager>.I.ResetRushInReentry();
    MonoBehaviourSingleton<InGameManager>.I.ResetSeriesInReentry();
  }

  private void SetSpecialEnemyAnounceIfNeed(int mapId)
  {
    this.ClearAllSpecialEnemyAnnounce();
    FieldMapEnemyPopTimeZoneTable.FieldMapEnemyPopTimeZoneData resultTimeZone;
    ENEMY_POP_TYPE resultType;
    if (!Singleton<FieldMapEnemyPopTimeZoneTable>.I.TryGetEnableLastEndTime(mapId, out resultTimeZone, out resultType))
      return;
    this.currentAnnounceType = resultType;
    this.specialEnemyAnnounceGone = this.StartCoroutine(this.AnnounceSpecialEnemyGone(resultType, resultTimeZone));
    if (this.ExistSpecialEnemyOnField())
      return;
    this.specialEnemyAnnounceExist = this.StartCoroutine(this.AnnounceSpecialEnemyExist(resultType, resultTimeZone));
  }

  private void ClearAllSpecialEnemyAnnounce()
  {
    this.ClearSpecialEnemyAnnounce(ENEMY_POP_TYPE.FIELD_BOSS);
    this.ClearSpecialEnemyAnnounce(ENEMY_POP_TYPE.RARE_SPECIES);
    this.currentAnnounceType = ENEMY_POP_TYPE.NONE;
  }

  private void ClearSpecialEnemyAnnounce(ENEMY_POP_TYPE type)
  {
    if (this.currentAnnounceType != type)
      return;
    this.ClearSpecialEnemyExistAnnounce(type);
    if (this.specialEnemyAnnounceGone == null)
      return;
    this.StopCoroutine(this.specialEnemyAnnounceGone);
    this.specialEnemyAnnounceGone = (Coroutine) null;
  }

  private void ClearSpecialEnemyExistAnnounce(ENEMY_POP_TYPE type)
  {
    if (this.currentAnnounceType != type || this.specialEnemyAnnounceExist == null)
      return;
    this.StopCoroutine(this.specialEnemyAnnounceExist);
    this.specialEnemyAnnounceExist = (Coroutine) null;
  }

  private IEnumerator AnnounceSpecialEnemyExist(
    ENEMY_POP_TYPE type,
    FieldMapEnemyPopTimeZoneTable.FieldMapEnemyPopTimeZoneData timeZoneData)
  {
    while (!this.readyToPlay)
      yield return (object) null;
    yield return (object) new WaitForSeconds(1f);
    if (!this.ExistSpecialEnemyOnField())
    {
      uint id = 9000;
      if (timeZoneData.existStrId != 0U)
      {
        id = timeZoneData.existStrId;
      }
      else
      {
        switch (type)
        {
          case ENEMY_POP_TYPE.FIELD_BOSS:
            id = 9000U;
            break;
          case ENEMY_POP_TYPE.RARE_SPECIES:
            id = 9002U;
            break;
        }
      }
      UIInGamePopupDialog.PushOpen(StringTable.Format(STRING_CATEGORY.IN_GAME, id), false);
    }
  }

  private IEnumerator AnnounceSpecialEnemyGone(
    ENEMY_POP_TYPE type,
    FieldMapEnemyPopTimeZoneTable.FieldMapEnemyPopTimeZoneData timeZoneData)
  {
    DateTime result;
    if (timeZoneData.TryGetEndTime(out result))
    {
      DateTime now = TimeManager.GetNow();
      result = TimeManager.CombineDateAndTime(now, result);
      TimeSpan timeSpan = result - now;
      if (!(timeSpan <= TimeSpan.Zero))
      {
        yield return (object) new WaitForSeconds((float) timeSpan.TotalSeconds);
        while (this.ExistSpecialEnemyOnField())
          yield return (object) new WaitForSeconds(10f);
        uint id = 9001;
        if (timeZoneData.goneStrId != 0U)
        {
          id = timeZoneData.goneStrId;
        }
        else
        {
          switch (type)
          {
            case ENEMY_POP_TYPE.FIELD_BOSS:
              id = 9001U;
              break;
            case ENEMY_POP_TYPE.RARE_SPECIES:
              id = 9003U;
              break;
          }
        }
        UIInGamePopupDialog.PushOpen(StringTable.Format(STRING_CATEGORY.IN_GAME, id), false);
      }
    }
  }

  private bool ExistSpecialEnemyOnField()
  {
    if (this.isInFieldEnemyBossBattle)
      return true;
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return false;
    List<Enemy> enemyList = MonoBehaviourSingleton<StageObjectManager>.I.EnemyList;
    if (enemyList == null || enemyList.Count <= 0)
      return false;
    int index = 0;
    for (int count = enemyList.Count; index < count; ++index)
    {
      if (enemyList[index].isRareSpecies)
        return true;
    }
    return false;
  }

  private void CheckBossTrace(int mapId)
  {
    if (this.CheckBossTraceSelf(mapId))
    {
      if (MonoBehaviourSingleton<QuestManager>.I.GetReservedTraceInfo() == null)
        return;
      MonoBehaviourSingleton<QuestManager>.I.CompleteBossTracePopup();
    }
    else
      this.CheckBossTraceParty();
  }

  private bool CheckBossTraceSelf(int mapId)
  {
    switch (MonoBehaviourSingleton<QuestManager>.I.GetExploreHistoryType(mapId))
    {
      case EXPLORE_HISTORY_TYPE.LAST:
        int mainEnemyId1 = Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID).GetMainEnemyID();
        UIInGamePopupDialog.PushOpen(StringTable.Format(STRING_CATEGORY.IN_GAME, 8001U, (object) Singleton<EnemyTable>.I.GetEnemyData((uint) mainEnemyId1).name), false, 1.4f);
        MonoBehaviourSingleton<CoopManager>.I.coopRoom.packetSender.SendNotifyTraceBoss(mapId, 0);
        MonoBehaviourSingleton<QuestManager>.I.UpdateBossTraceHistory(mapId, 0, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name, false);
        return true;
      case EXPLORE_HISTORY_TYPE.SECOND_LAST:
        int mainEnemyId2 = Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID).GetMainEnemyID();
        UIInGamePopupDialog.PushOpen(StringTable.Format(STRING_CATEGORY.IN_GAME, 8002U, (object) Singleton<EnemyTable>.I.GetEnemyData((uint) mainEnemyId2).name), false, 1.4f);
        MonoBehaviourSingleton<CoopManager>.I.coopRoom.packetSender.SendNotifyTraceBoss(mapId, 1);
        MonoBehaviourSingleton<QuestManager>.I.UpdateBossTraceHistory(mapId, 1, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name, false);
        return true;
      default:
        return false;
    }
  }

  private void CheckBossTraceParty()
  {
    ExploreStatus.TraceInfo reservedTraceInfo = MonoBehaviourSingleton<QuestManager>.I.GetReservedTraceInfo();
    if (reservedTraceInfo == null)
      return;
    UIInGamePopupDialog.PushOpen(StringTable.Format(STRING_CATEGORY.IN_GAME, (uint) (reservedTraceInfo.historyType != EXPLORE_HISTORY_TYPE.LAST ? 8004 : 8003), (object) reservedTraceInfo.playerName), false, 1.22f);
    MonoBehaviourSingleton<QuestManager>.I.CompleteBossTracePopup();
  }

  public void OnRequested()
  {
    if (this.isStageRequested)
      return;
    this.isStageRequested = true;
    if (MonoBehaviourSingleton<CoopManager>.I.isStageHost)
    {
      this.Logd("OnRequested: Non player create from fill.");
      CoopStageObjectUtility.FillNonPlayer(4, this.GetClientCount());
      this.ReSendStageInfo();
      this.isHostStageResponseEnd = true;
    }
    this.Logd("OnRequested: complete...");
  }

  public void OnRequestedClient(CoopClient client)
  {
    if (this.isQuestClose)
    {
      this.Logd("OnRequestedClient: STAGE_CLOSE. isSucceed={0}, to={1}", (object) this.isQuestSucceed, (object) client.clientId);
      if (this.isQuestSucceed)
        this.packetSender.SendStageResponseEnd(CoopStage.STAGE_REQUEST_ERROR.STAGE_CLOSE_SUCCEED, client.clientId);
      else
        this.packetSender.SendStageResponseEnd(CoopStage.STAGE_REQUEST_ERROR.STAGE_CLOSE_FAILED, client.clientId);
    }
    else if (!this.isActivateStart)
    {
      this.Logd("OnRequestedClient: DEACTIVATE. to={0}", (object) client.clientId);
      this.packetSender.SendStageResponseEnd(CoopStage.STAGE_REQUEST_ERROR.DEACTIVATE, client.clientId);
    }
    else
    {
      if (!client.isSendPopOriginal)
      {
        this.SendOriginalObjectPop(client.clientId);
        client.isSendPopOriginal = true;
      }
      if (MonoBehaviourSingleton<CoopManager>.I.isStageHost && !client.isSendStageInfo)
      {
        this.Logd("OnRequestedClient: Send StageInfo. to={0}", (object) client.clientId);
        this.packetSender.SendStageInfo(client.clientId);
        client.isSendStageInfo = true;
      }
      this.Logd("OnRequestedClient: Send ResponseEnd. to={0}", (object) client.clientId);
      this.packetSender.SendStageResponseEnd(to_client_id: client.clientId);
    }
  }

  public void ReSendStageInfo()
  {
    if (!this.isActivateStart)
      return;
    this.ForEachClients((Action<CoopClient>) (c =>
    {
      if (c is CoopMyClient || !c.IsStageRequest() || c.isSendStageInfo)
        return;
      this.Logd("ReSendStageInfo: to={0}", (object) c.clientId);
      this.packetSender.SendStageInfo(c.clientId);
      c.isSendStageInfo = true;
    }));
  }

  public void SendOriginalObjectPop(int to_client_id)
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return;
    MonoBehaviourSingleton<StageObjectManager>.I.playerList.ForEach((Action<StageObject>) (o =>
    {
      Player player = o as Player;
      if (Object.op_Inequality((Object) player, (Object) null) && (player.IsOriginal() || player.IsCoopNone()))
      {
        this.packetSender.SendStagePlayerPop(player, to_client_id);
        if (player is Self)
          this.Logd("StageRequest response SelfPop({0}). to={1}", (object) player.id, (object) to_client_id);
        else
          this.Logd("StageRequest response PlayerPop({0}). to={1}", (object) player.id, (object) to_client_id);
      }
      else
      {
        if (!Object.op_Equality((Object) player, (Object) null))
          return;
        Log.Warning(LOG.COOP, "SendOriginalObjectPop, player is null. to_client_id:{0}", (object) to_client_id);
      }
    }));
    if (!MonoBehaviourSingleton<CoopManager>.IsValid() || !MonoBehaviourSingleton<CoopManager>.I.isStageHost)
      return;
    List<StageObject> allCoopObjectList = MonoBehaviourSingleton<StageObjectManager>.I.GetAllCoopObjectList();
    if (allCoopObjectList == null)
      return;
    int index = 0;
    for (int count = allCoopObjectList.Count; index < count; ++index)
    {
      if (!Object.op_Equality((Object) allCoopObjectList[index], (Object) null))
      {
        allCoopObjectList[index].SetCoopMode(StageObject.COOP_MODE_TYPE.ORIGINAL, 0);
        this.packetSender.SendObjectInfo(allCoopObjectList[index], StageObject.COOP_MODE_TYPE.MIRROR, to_client_id);
      }
    }
  }

  public bool OnRecvStagePlayerPop(Coop_Model_StagePlayerPop model, CoopPacket packet)
  {
    if (!this.isActivateStart)
      return false;
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return true;
    CoopClient byClientId = MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.FindByClientId(packet.fromClientId);
    if (Object.op_Equality((Object) byClientId, (Object) null))
      return true;
    this.Logd("Responsed PlayerPop from {0}. sid={1},userid={2},self={3}", (object) packet.fromClientId, (object) model.sid, (object) byClientId.userId, (object) model.isSelf);
    this.ForEachClients((Action<CoopClient>) (c => c.OnPlayerPop(model.sid)));
    if (model.isSelf)
    {
      if (model.charaInfo != null)
      {
        Predicate<StageObject> match = (Predicate<StageObject>) (o =>
        {
          Player player1 = o as Player;
          return Object.op_Inequality((Object) player1, (Object) null) && !player1.isDestroyWaitFlag && player1.id != model.sid && player1.createInfo != null && player1.createInfo.charaInfo != null && model.charaInfo != null && player1.createInfo.charaInfo.userId != 0 && player1.createInfo.charaInfo.userId == model.charaInfo.userId;
        });
        StageObject stageObject = MonoBehaviourSingleton<StageObjectManager>.I.playerList.Find(match);
        if (Object.op_Equality((Object) stageObject, (Object) null))
          stageObject = MonoBehaviourSingleton<StageObjectManager>.I.cacheList.Find(match);
        if (Object.op_Inequality((Object) stageObject, (Object) null))
        {
          this.Logd("OnRecvStagePlayerPop last leaved player({0}) destroy. to={1}", (object) stageObject.id, (object) byClientId.userId);
          stageObject.DestroyObject();
        }
      }
      byClientId.SetPlayerID(model.sid);
      if (model.charaInfo != null)
        byClientId.SetUserInfo(model.charaInfo);
    }
    Player player = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(model.sid) as Player;
    if (Object.op_Equality((Object) player, (Object) null))
      player = MonoBehaviourSingleton<StageObjectManager>.I.FindCache(model.sid) as Player;
    if (Object.op_Inequality((Object) player, (Object) null))
    {
      if (Object.op_Equality((Object) player, (Object) MonoBehaviourSingleton<StageObjectManager>.I.self))
      {
        this.Logd("myself player pop!");
        return true;
      }
      if (player.isLoading)
        return false;
    }
    if (Object.op_Equality((Object) player, (Object) null))
    {
      PlayerLoader.OnCompleteLoad callback = (PlayerLoader.OnCompleteLoad) (o => this.OnPopPlayerLoadComplete(player, packet.fromClientId));
      if (model.extentionInfo != null && model.extentionInfo.npcDataID != 0)
      {
        this.Logd("CreateNonPlayer. sid={0},npcDataID={1},npcLv={2},npcLvIndex={3}", (object) model.sid, (object) model.extentionInfo.npcDataID, (object) model.extentionInfo.npcLv, (object) model.extentionInfo.npcLvIndex);
        player = MonoBehaviourSingleton<StageObjectManager>.I.CreateNonPlayer(model.sid, model.extentionInfo, Vector3.zero, 0.0f, model.transferInfo, callback);
      }
      else
      {
        StageObjectManager.CreatePlayerInfo create_info = new StageObjectManager.CreatePlayerInfo();
        create_info.charaInfo = model.charaInfo;
        this.Logd("CreatePlayer. sid={0},userId={1}", (object) model.sid, (object) create_info.charaInfo.userId);
        create_info.extentionInfo = model.extentionInfo;
        player = MonoBehaviourSingleton<StageObjectManager>.I.CreatePlayer(model.sid, create_info, false, Vector3.zero, 0.0f, model.transferInfo, callback);
        if (QuestManager.IsValidInGame())
          MonoBehaviourSingleton<QuestManager>.I.resultUserCollection.AddPlayer(create_info.charaInfo);
      }
      if (Object.op_Inequality((Object) player, (Object) null))
        player.SetCoopMode(StageObject.COOP_MODE_TYPE.PUPPET, packet.fromClientId);
    }
    else
    {
      this.Logd("already player pop! sid={0}", (object) model.sid);
      MonoBehaviourSingleton<StageObjectManager>.I.RemoveCacheObject((StageObject) player);
      ((Component) player).gameObject.SetActive(true);
      player.SafeActIdle();
      player.SetCoopMode(StageObject.COOP_MODE_TYPE.PUPPET, packet.fromClientId);
      if (!player.isLoading)
        this.OnPopPlayerLoadComplete(player, packet.fromClientId);
    }
    return true;
  }

  protected void OnPopPlayerLoadComplete(Player player, int client_id)
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid() || !MonoBehaviourSingleton<KtbWebSocket>.I.IsConnected())
      return;
    if (player.IsOriginal() || player.IsCoopNone())
    {
      this.Logd("PlayerLoadComplete skip. became original in loading. player={0},client={1}", (object) player.id, (object) client_id);
      CoopStageObjectUtility.ShrinkOriginalNonPlayer(4);
      if (!QuestManager.IsValidInGameDefenseBattle())
        return;
      CoopStageObjectUtility.DestroyAllNonPlayer();
    }
    else
    {
      player.RemoveController();
      if (player.isCoopInitialized)
      {
        this.Logd("PlayerLoadComplete skip. already initilized. player={0},client={1}", (object) player.id, (object) client_id);
      }
      else
      {
        this.Logd("PlayerLoadComplete. player={0},client={1}", (object) player.id, (object) client_id);
        ((Component) player).gameObject.SetActive(false);
        MonoBehaviourSingleton<StageObjectManager>.I.AddCacheObject((StageObject) player);
        player.packetReceiver.SetFilterMode(ObjectPacketReceiver.FILTER_MODE.WAIT_INITIALIZE);
        player.playerSender.OnLoadComplete(true);
        if (MonoBehaviourSingleton<UIInGameMessageBar>.IsValid() && !(player is NonPlayer))
          MonoBehaviourSingleton<UIInGameMessageBar>.I.Announce(player.charaName, StringTable.Get(STRING_CATEGORY.IN_GAME, 0U));
        CoopStageObjectUtility.ShrinkOriginalNonPlayer(4);
        if (!QuestManager.IsValidInGameDefenseBattle())
          return;
        CoopStageObjectUtility.DestroyAllNonPlayer();
      }
    }
  }

  public bool OnRecvEnemyPop(Coop_Model_EnemyPop model) => this.PopEnemy(model, out Enemy _);

  private bool PopEnemy(Coop_Model_EnemyPop model, out Enemy outEnmey)
  {
    outEnmey = (Enemy) null;
    if (!this.isActivateStart)
      return false;
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid() || !MonoBehaviourSingleton<FieldManager>.IsValid())
      return true;
    this.Logd("OnRecvEnemyPop. sid={0},ownerClientId={1},popIndex={2}", (object) model.sid, (object) model.ownerClientId, (object) model.popIndex);
    Enemy enemy = MonoBehaviourSingleton<StageObjectManager>.I.FindEnemy(model.sid) as Enemy;
    if (Object.op_Equality((Object) enemy, (Object) null))
      enemy = MonoBehaviourSingleton<StageObjectManager>.I.FindCache(model.sid) as Enemy;
    if (Object.op_Inequality((Object) enemy, (Object) null) && enemy.isLoading)
    {
      this.Logd("OnRecvEnemyPop. enemy is loading...");
      return false;
    }
    if (QuestManager.IsValidInGameSeries() || QuestManager.IsValidInGameSeriesArena())
      return this.PopEnemyForSeries(model, enemy);
    FieldMapTable.EnemyPopTableData enemyPopData = Singleton<FieldMapTable>.I.GetEnemyPopData(MonoBehaviourSingleton<FieldManager>.I.currentMapID, model.popIndex);
    if (enemyPopData == null)
    {
      Log.Error(LOG.COOP, "CoopStage: OnRecvEnemyPop. not found field enemy pop data. mapId={0},idx={1}", (object) MonoBehaviourSingleton<FieldManager>.I.currentMapID, (object) model.popIndex);
      return true;
    }
    int num1 = (int) enemyPopData.enemyID;
    int num2 = (int) enemyPopData.enemyLv;
    QUEST_TYPE questType = QUEST_TYPE.NORMAL;
    QUEST_STYLE questStyle = QUEST_STYLE.NORMAL;
    if (num1 == 0 && QuestManager.IsValidInGame())
    {
      QuestManager i = MonoBehaviourSingleton<QuestManager>.I;
      num1 = i.GetCurrentQuestEnemyID();
      num2 = i.GetCurrentQuestEnemyLv();
      questType = i.GetCurrentQuestType();
      questStyle = i.GetCurrentQuestStyle();
    }
    if (model.ownerClientId == MonoBehaviourSingleton<CoopManager>.I.coopMyClient.clientId)
    {
      if (Object.op_Equality((Object) enemy, (Object) null))
      {
        enemy = questType != QUEST_TYPE.DEFENSE ? (questStyle != QUEST_STYLE.DEFENSE ? MonoBehaviourSingleton<StageObjectManager>.I.CreateEnemy(model.sid, Vector3.zero, 0.0f, num1, num2, enemyPopData.bossFlag, enemyPopData.bigMonsterFlag, callback: (EnemyLoader.OnCompleteLoad) (target =>
        {
          if (!MonoBehaviourSingleton<InGameRecorder>.IsValid())
            return;
          MonoBehaviourSingleton<InGameRecorder>.I.RecordEnemyHP(target.id, target.hpMax);
        })) : MonoBehaviourSingleton<StageObjectManager>.I.CreateEnemyForDefenseBattle(model.sid, num1, num2)) : MonoBehaviourSingleton<StageObjectManager>.I.CreateEnemyForDefenseBattle(model.sid, num1, num2);
        if (model.popIndex >= 0 && MonoBehaviourSingleton<CoopOfflineManager>.IsValid())
          MonoBehaviourSingleton<CoopOfflineManager>.I.OnEnemyPop(model.popIndex, model.sid);
      }
      else
      {
        MonoBehaviourSingleton<StageObjectManager>.I.RemoveCacheObject((StageObject) enemy);
        ((Component) enemy).gameObject.SetActive(true);
      }
      if (this.IsSolo())
        enemy.SetCoopMode(StageObject.COOP_MODE_TYPE.NONE, 0);
      else
        enemy.SetCoopMode(StageObject.COOP_MODE_TYPE.ORIGINAL, 0);
      if (Object.op_Equality((Object) enemy.controller, (Object) null))
        enemy.AddController<EnemyController>();
      if (enemy.isBoss && Object.op_Inequality((Object) enemy.controller, (Object) null) && !MonoBehaviourSingleton<CoopManager>.I.coopMyClient.IsBattleStart())
        enemy.controller.SetEnableControll(false, ControllerBase.DISABLE_FLAG.BATTLE_START);
      enemy.CheckFirstMadMode();
      enemy.SafeActIdle();
      enemy.enemyPopIndex = model.popIndex;
      if (!enemy.isSetAppearPos)
      {
        if (model.setPos)
        {
          enemy.SetAppearPosForce(new Vector3(model.x, 0.0f, model.z), enemyPopData);
          Debug.Log((object) ("Force Set Pos " + (object) ((Component) enemy).transform.position));
        }
        else
          enemy.SetAppearPosEnemy();
      }
      this.Logd("OnRecvEnemyPop. my owner enemy({0}). is load={1}...", (object) enemy, (object) enemy.isLoading);
    }
    else
    {
      if (Object.op_Equality((Object) enemy, (Object) null))
      {
        enemy = MonoBehaviourSingleton<StageObjectManager>.I.CreateEnemy(model.sid, Vector3.zero, 0.0f, num1, num2, enemyPopData.bossFlag, enemyPopData.bigMonsterFlag, false, callback: (EnemyLoader.OnCompleteLoad) (target => this.OnPopEnemyLoadComplete(target, model.ownerClientId)));
        enemy.SetCoopMode(StageObject.COOP_MODE_TYPE.MIRROR, model.ownerClientId);
        enemy.enemyPopIndex = model.popIndex;
        if (model.popIndex >= 0 && MonoBehaviourSingleton<CoopOfflineManager>.IsValid())
          MonoBehaviourSingleton<CoopOfflineManager>.I.OnEnemyPop(model.popIndex, model.sid);
      }
      else
      {
        enemy.SetCoopMode(StageObject.COOP_MODE_TYPE.MIRROR, model.ownerClientId);
        enemy.isCoopInitialized = false;
        enemy.SafeActIdle();
        this.OnPopEnemyLoadComplete(enemy, model.ownerClientId, false);
      }
      if (enemyPopData.enablePopY)
        enemy.onTheGround = false;
      this.Logd("OnRecvEnemyPop. other owner enemy({0}). is load={1}...", (object) enemy, (object) enemy.isLoading);
    }
    if (enemyPopData.enemyPopType == ENEMY_POP_TYPE.RARE_SPECIES)
    {
      if (MonoBehaviourSingleton<UIInGameFieldQuestWarning>.IsValid() && !this.ExistSpecialEnemyOnField())
      {
        MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.PlayRareFieldEnemy();
        MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.FadeOut(4f, 1f, (System.Action) null);
      }
      this.ClearSpecialEnemyExistAnnounce(ENEMY_POP_TYPE.RARE_SPECIES);
      enemy.isRareSpecies = true;
    }
    outEnmey = enemy;
    return true;
  }

  private bool PopEnemyForSeries(Coop_Model_EnemyPop model, Enemy enemy)
  {
    MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestSeriesIndex((uint) model.seriesIdx);
    if (model.ownerClientId != MonoBehaviourSingleton<CoopManager>.I.coopMyClient.clientId)
    {
      if (Object.op_Inequality((Object) enemy, (Object) null))
      {
        enemy.SetCoopMode(StageObject.COOP_MODE_TYPE.MIRROR, model.ownerClientId);
        enemy.isCoopInitialized = false;
        enemy.SafeActIdle();
        if (MonoBehaviourSingleton<SoundManager>.IsValid())
          SoundManager.RequestBGM(MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestBGMID());
        this.OnPopEnemyLoadComplete(enemy, model.ownerClientId, false);
        return true;
      }
      enemy = MonoBehaviourSingleton<StageObjectManager>.I.CreateEnemyForSeries(model.sid, model.seriesIdx, (EnemyLoader.OnCompleteLoad) (target =>
      {
        if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
          MonoBehaviourSingleton<InGameRecorder>.I.RecordEnemyHP(target.id, target.hpMax);
        this.OnPopEnemyLoadComplete(target, model.ownerClientId);
        if (model.seriesIdx <= 0)
          return;
        this.StartCoroutine(MonoBehaviourSingleton<StageObjectManager>.I.CreateNextEnemyForSeriesOfBattles(target));
      }));
      enemy.SetCoopMode(StageObject.COOP_MODE_TYPE.MIRROR, model.ownerClientId);
      return true;
    }
    if (Object.op_Inequality((Object) enemy, (Object) null))
    {
      MonoBehaviourSingleton<StageObjectManager>.I.RemoveCacheObject((StageObject) enemy);
      ((Component) enemy).gameObject.SetActive(true);
      if (MonoBehaviourSingleton<SoundManager>.IsValid())
        SoundManager.RequestBGM(MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestBGMID());
    }
    else
      enemy = MonoBehaviourSingleton<StageObjectManager>.I.CreateEnemyForSeries(model.sid, model.seriesIdx, (EnemyLoader.OnCompleteLoad) (target =>
      {
        if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
          MonoBehaviourSingleton<InGameRecorder>.I.RecordEnemyHP(target.id, target.hpMax);
        if (model.seriesIdx <= 0)
          return;
        ((Component) target).gameObject.SetActive(false);
        this.StartCoroutine(MonoBehaviourSingleton<StageObjectManager>.I.CreateNextEnemyForSeriesOfBattles(enemy));
      }));
    if (this.IsSolo())
      enemy.SetCoopMode(StageObject.COOP_MODE_TYPE.NONE, 0);
    else
      enemy.SetCoopMode(StageObject.COOP_MODE_TYPE.ORIGINAL, 0);
    if (Object.op_Equality((Object) enemy.controller, (Object) null))
      enemy.AddController<EnemyController>();
    if (enemy.isBoss && Object.op_Inequality((Object) enemy.controller, (Object) null) && !MonoBehaviourSingleton<CoopManager>.I.coopMyClient.IsBattleStart())
      enemy.controller.SetEnableControll(false, ControllerBase.DISABLE_FLAG.BATTLE_START);
    enemy.CheckFirstMadMode();
    enemy.SafeActIdle();
    if (!enemy.isSetAppearPos)
      enemy.SetAppearPosEnemy();
    return true;
  }

  public bool OnRecvEnemyBossPop(Coop_Model_EnemyBossPop model)
  {
    Enemy outEnmey;
    if (!this.PopEnemy((Coop_Model_EnemyPop) model, out outEnmey))
    {
      this.fieldEnemyBossEntering = false;
      return false;
    }
    if (Object.op_Equality((Object) outEnmey, (Object) null))
    {
      this.fieldEnemyBossEntering = false;
      return false;
    }
    if (Object.op_Inequality((Object) ((Component) outEnmey).gameObject, (Object) null))
      ((Component) outEnmey).gameObject.SetActive(false);
    Debug.Log((object) ("Pop Enemy " + (object) ((Component) outEnmey).transform.position));
    this.ClearSpecialEnemyExistAnnounce(ENEMY_POP_TYPE.FIELD_BOSS);
    this.StartCoroutine(this.StartEnemyBoss(model, outEnmey));
    return true;
  }

  public IEnumerator StartEnemyBoss(Coop_Model_EnemyBossPop model, Enemy enemy)
  {
    this.requestedEnemyBossAlive = false;
    if (!this.readyToPlay)
    {
      this.packetSender.SendEnemyBossAliveRequest();
      float requestTimer = 0.0f;
      while (!this.requestedEnemyBossAlive)
      {
        if ((double) requestTimer > 5.0)
        {
          this.requestedEnemyBossAlive = false;
          this.SetFalseEnemyBossBattleFlag();
          enemy.DestroyObject();
          yield break;
        }
        requestTimer += Time.deltaTime;
        yield return (object) null;
      }
      this.requestedEnemyBossAlive = false;
    }
    if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<StageObjectManager>.IsValid())
    {
      MonoBehaviourSingleton<StageObjectManager>.I.SetFieldEnemyBoss(enemy);
      this.fieldEnemyBossEntering = true;
      while (!this.readyToPlay)
        yield return (object) null;
      if (Object.op_Equality((Object) enemy, (Object) null))
        this.fieldEnemyBossEntering = false;
      else if (this.isInFieldEnemyBossBattle)
      {
        this.fieldEnemyBossEntering = false;
        ((Component) enemy).gameObject.SetActive(true);
      }
      else
      {
        bool isGimmickPop = false;
        FieldMapTable.EnemyPopTableData enemyPopData = Singleton<FieldMapTable>.I.GetEnemyPopData(MonoBehaviourSingleton<FieldManager>.I.currentMapID, model.popIndex);
        if (enemyPopData.enemyPopType == ENEMY_POP_TYPE.GIMMICK_POP || enemyPopData.enemyPopType == ENEMY_POP_TYPE.GIMMICK_POP_RARE)
        {
          if (!this.isInFieldFishingEnemyBattle)
            this.ShowFieldFishingBossStartUI(enemyPopData.enemyPopType == ENEMY_POP_TYPE.GIMMICK_POP_RARE);
          this.isInFieldFishingEnemyBattle = true;
          isGimmickPop = true;
        }
        else
        {
          this.ShowFieldEnemyBossStartUI();
          this.isInFieldFishingEnemyBattle = false;
        }
        this.isInFieldEnemyBossBattle = true;
        if (this.isEnterFieldEnemyBossBattle)
        {
          this.fieldEnemyBossEntering = false;
          this.isEnterFieldEnemyBossBattle = false;
          ((Component) enemy).gameObject.SetActive(true);
        }
        else
        {
          int limit_time = 300;
          if (enemyPopData != null)
            limit_time = enemyPopData.escapeTime;
          if (MonoBehaviourSingleton<InGameProgress>.IsValid() && !isGimmickPop)
            MonoBehaviourSingleton<InGameProgress>.I.ResetStartTimer((float) limit_time);
          yield return (object) this.StartCoroutine(this.LoadFieldEnemyEntryExitEffect());
          while (!EffectManager.ExistEffect("ef_btl_enemy_entry_01"))
            yield return (object) null;
          Transform effectTrans = EffectManager.GetEffect("ef_btl_enemy_entry_01");
          Vector3 localPosition = enemy._transform.localPosition;
          localPosition.y = StageManager.GetHeight(enemy._transform.position);
          effectTrans.localPosition = localPosition;
          ((Component) enemy).gameObject.SetActive(true);
          Debug.Log((object) ("SHOW BOSS " + (object) ((Component) enemy).transform.position));
          enemy.hitOffFlag |= StageObject.HIT_OFF_FLAG.FORCE;
          enemy.isFirstMadMode = !isGimmickPop;
          this.GoUpCharacterFromUnderGround(enemy, (System.Action) (() =>
          {
            EffectManager.ReleaseEffect(((Component) effectTrans).gameObject);
            if (!isGimmickPop)
              enemy.ActMadMode();
            enemy.hitOffFlag &= ~StageObject.HIT_OFF_FLAG.FORCE;
            this.fieldEnemyBossEntering = false;
            Debug.Log((object) ("BOSS " + (object) ((Component) enemy).transform.position));
          }));
          this.StartCoroutine(this.LoadFieldTimesUpVictoryeffect());
        }
      }
    }
  }

  private void SetFalseEnemyBossBattleFlag()
  {
    this.isInFieldEnemyBossBattle = false;
    this.isInFieldFishingEnemyBattle = false;
    this.isEnterFieldEnemyBossBattle = false;
    this.fieldEnemyBossEntering = false;
  }

  private IEnumerator LoadFieldEnemyEntryExitEffect()
  {
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    loadingQueue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enemy_entry_01");
    while (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
  }

  private IEnumerator LoadFieldTimesUpVictoryeffect()
  {
    if (!MonoBehaviourSingleton<InGameLinkResourcesFieldEnemyBoss>.IsValid() && MonoBehaviourSingleton<InGameProgress>.IsValid())
    {
      bool loadedEffectUI = false;
      MonoBehaviourSingleton<InGameProgress>.I.RealizesLinkResourcesFieldEnemyBoss((System.Action) (() => loadedEffectUI = true));
      int count = 100;
      while (loadedEffectUI && count > 0)
      {
        --count;
        yield return (object) null;
      }
    }
  }

  private IEnumerator LoadAndPlayVictoryEffect()
  {
    yield return (object) this.StartCoroutine(this.LoadFieldTimesUpVictoryeffect());
    while (!this.readyToPlay)
      yield return (object) null;
    MonoBehaviourSingleton<InGameProgress>.I.PlayFieldEnemyBossVictoryEffect();
  }

  public void ShowFieldEnemyBossStartUI()
  {
    if (MonoBehaviourSingleton<UIInGameFieldQuestWarning>.IsValid())
    {
      MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.Play(ENEMY_TYPE.NONE, isFieldBoss: true);
      MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.FadeOut(4f, 1f, (System.Action) null);
    }
    if (!MonoBehaviourSingleton<UIFieldBossInfo>.IsValid())
      return;
    MonoBehaviourSingleton<UIFieldBossInfo>.I.FadeIn(0.0f, 1f, (System.Action) null);
  }

  public void ShowFieldFishingBossStartUI(bool isRare)
  {
    if (!MonoBehaviourSingleton<UIInGameFieldQuestWarning>.IsValid())
      return;
    MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.PlayFieldFishingEnemy(isRare);
    MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.FadeOut(4f, 1f, (System.Action) null);
  }

  public bool OnRecvEnemyBossEscape(Coop_Model_EnemyBossEscape model)
  {
    this.StartCoroutine(this.EscapeEnemyBoss(model.sid));
    return true;
  }

  public void OnRecvEnemyBossAliveRequest(CoopPacket packet)
  {
    if (!this.isInFieldEnemyBossBattle)
      return;
    this.packetSender.SendEnemyBossAliveRequested(packet.fromClientId);
  }

  public void OnRecvEnemyBossAliveRequested() => this.requestedEnemyBossAlive = true;

  public void EscapeHostEnmeyBoss()
  {
    if (Object.op_Equality((Object) MonoBehaviourSingleton<StageObjectManager>.I.fieldEnemyBoss, (Object) null))
    {
      this.isInFieldEnemyBossBattle = false;
      this.isInFieldFishingEnemyBattle = false;
    }
    else
    {
      int id = MonoBehaviourSingleton<StageObjectManager>.I.fieldEnemyBoss.id;
      this.packetSender.SendEnemyBossEscape(id, true);
      this.StartCoroutine(this.EscapeEnemyBoss(id));
    }
  }

  private IEnumerator EscapeEnemyBoss(int sid)
  {
    if (this.isInFieldEnemyBossBattle)
    {
      this.isInFieldEnemyBossBattle = false;
      this.isInFieldFishingEnemyBattle = false;
      while (!this.readyToPlay)
        yield return (object) null;
      yield return (object) this.StartCoroutine(this.LoadFieldEnemyEntryExitEffect());
      while (this.fieldEnemyBossEntering)
        yield return (object) null;
      Enemy enemy = (Enemy) null;
      if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
        enemy = MonoBehaviourSingleton<StageObjectManager>.I.FindEnemy(sid) as Enemy;
      if (Object.op_Inequality((Object) enemy, (Object) null))
        enemy.PrepareVanishLocal();
      if (Object.op_Inequality((Object) enemy, (Object) null))
      {
        while (!EffectManager.ExistEffect("ef_btl_enemy_entry_01"))
          yield return (object) null;
        Transform effectTrans = EffectManager.GetEffect("ef_btl_enemy_entry_01");
        Vector3 localPosition = enemy._transform.localPosition;
        localPosition.y = StageManager.GetHeight(enemy._transform.position);
        effectTrans.localPosition = localPosition;
        this.GoDownCharacterToUnderGround(enemy, (System.Action) (() =>
        {
          EffectManager.ReleaseEffect(((Component) effectTrans).gameObject);
          this.ResetBGM();
        }));
        yield return (object) this.StartCoroutine(this.LoadFieldTimesUpVictoryeffect());
        MonoBehaviourSingleton<InGameProgress>.I.PlayFieldEnemyBossTimesUpEffect();
        enemy.OnEndEscape();
      }
    }
  }

  public bool OnRecvWaveMatchInfo(Coop_Model_WaveMatchInfo model)
  {
    if (model.no == 1)
    {
      this.firstWaveMatchInfo = model;
      this.firstWaveMatchPopSec = (float) model.popGuardSec;
    }
    this.firstWaveMatchSet = MonoBehaviourSingleton<UIWaveMatchAnnounce>.IsValid();
    if (this.firstWaveMatchSet)
      MonoBehaviourSingleton<UIWaveMatchAnnounce>.I.Announce(model);
    if (!QuestManager.IsValidInGameWaveStrategy())
      return true;
    this.DrawEachLineByPopData(model.no);
    return true;
  }

  private void DrawEachLineByPopData(int waveNo)
  {
    List<FieldMapTable.EnemyPopTableData> enemyPopList = Singleton<FieldMapTable>.I.GetEnemyPopList(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
    if (enemyPopList.IsNullOrEmpty<FieldMapTable.EnemyPopTableData>())
    {
      Log.Error(LOG.COOP, "CoopStage: OnRecvWaveMatchInfo. not found field enemy pop data. mapId={0}", (object) MonoBehaviourSingleton<FieldManager>.I.currentMapID);
    }
    else
    {
      int index = 0;
      for (int count = enemyPopList.Count; index < count; ++index)
      {
        if (enemyPopList[index].waveNo == waveNo)
          MonoBehaviourSingleton<StageObjectManager>.I.DrawWaveTargetLine(enemyPopList[index]);
      }
    }
  }

  public void SetFirstWaveMatchInfoForStageInfo(ref Coop_Model_StageInfo model)
  {
    model.firstWaveMatchInfo = this.firstWaveMatchInfo;
    model.firstWaveMatchPopSec = this.firstWaveMatchPopSec;
  }

  private void CheckFirstWaveMatchInfo()
  {
    if (this.firstWaveMatchInfo != null)
    {
      this.firstWaveMatchPopSec -= Time.deltaTime;
      if ((double) this.firstWaveMatchPopSec <= 0.0)
        this.firstWaveMatchInfo = (Coop_Model_WaveMatchInfo) null;
    }
    if (this.firstWaveMatchInfo == null || this.firstWaveMatchSet || !MonoBehaviourSingleton<UIWaveMatchAnnounce>.IsValid())
      return;
    this.firstWaveMatchInfo.popGuardSec = Mathf.FloorToInt(this.firstWaveMatchPopSec);
    MonoBehaviourSingleton<UIWaveMatchAnnounce>.I.Announce(this.firstWaveMatchInfo);
    this.firstWaveMatchSet = true;
  }

  private void ResetBGM()
  {
    if (!MonoBehaviourSingleton<FieldManager>.IsValid() || !MonoBehaviourSingleton<SoundManager>.IsValid())
      return;
    SoundManager.RequestBGM(MonoBehaviourSingleton<FieldManager>.I.GetCurrentMapBGMID());
  }

  public void GoUpCharacterFromUnderGround(Enemy enemy, System.Action OnEndAction)
  {
    if (Object.op_Inequality((Object) enemy.controller, (Object) null))
      enemy.controller.SetEnableControll(false);
    float time = 3f;
    if (this.isInFieldFishingEnemyBattle)
      time = MonoBehaviourSingleton<InGameSettingsManager>.I.fishingParam.hitEnemyMoveSec;
    this.StartCoroutine(this.SimpleMoveCharacterY((Character) enemy, -10f, StageManager.GetHeight(enemy._transform.position), time, (System.Action) (() =>
    {
      OnEndAction.SafeInvoke();
      if (!Object.op_Inequality((Object) enemy.controller, (Object) null))
        return;
      enemy.controller.SetEnableControll(true);
    })));
  }

  public void GoDownCharacterToUnderGround(Enemy enemy, System.Action OnEndAction)
  {
    if (Object.op_Inequality((Object) enemy.controller, (Object) null))
      enemy.controller.SetEnableControll(false);
    enemy.PrepareVanishLocal();
    enemy.ActIdle();
    this.StartCoroutine(this.SimpleMoveCharacterY((Character) enemy, enemy._transform.position.y, -10f, 3f, (System.Action) (() =>
    {
      enemy.VanishLocal();
      OnEndAction.SafeInvoke();
    })));
  }

  public IEnumerator SimpleMoveCharacterY(
    Character enemy,
    float from,
    float to,
    float time,
    System.Action OnEndAction)
  {
    enemy.onTheGround = false;
    Vector3 position1 = enemy._transform.position;
    position1.y = from;
    enemy._transform.position = position1;
    float speed = (to - from) / time;
    float elapsedTime = 0.0f;
    Vector3 lastPos = position1;
    while (!Object.op_Equality((Object) enemy, (Object) null) && !Object.op_Equality((Object) enemy._transform, (Object) null))
    {
      Vector3 position2 = enemy._transform.position;
      if (!MonoBehaviourSingleton<StageManager>.I.CheckPosInside(position2))
      {
        position2.x = lastPos.x;
        position2.z = lastPos.z;
      }
      else
        lastPos = position2;
      position2.y += speed * Time.deltaTime;
      enemy._transform.position = position2;
      elapsedTime += Time.deltaTime;
      if ((double) elapsedTime <= (double) time)
      {
        yield return (object) null;
      }
      else
      {
        enemy.onTheGround = true;
        OnEndAction.SafeInvoke();
        break;
      }
    }
  }

  public void OnDefeatFieldEnemyBoss()
  {
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid() || !this.isInFieldEnemyBossBattle)
      return;
    this.StartCoroutine(this.LoadAndPlayVictoryEffect());
    this.isInFieldEnemyBossBattle = false;
    this.isInFieldFishingEnemyBattle = false;
    MonoBehaviourSingleton<InGameProgress>.I.StopTimer();
    MonoBehaviourSingleton<InGameProgress>.I.SetLimitTime(0.0f);
    if (MonoBehaviourSingleton<UIFieldBossInfo>.IsValid())
      MonoBehaviourSingleton<UIFieldBossInfo>.I.FadeOut(0.0f, 1f, (System.Action) null);
    this.ResetBGM();
  }

  protected void OnPopEnemyLoadComplete(Enemy enemy, int client_id, bool init = true)
  {
    if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
      MonoBehaviourSingleton<InGameRecorder>.I.RecordEnemyHP(enemy.id, enemy.hpMax);
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid() || !MonoBehaviourSingleton<KtbWebSocket>.I.IsConnected())
      return;
    if (enemy.IsOriginal() || enemy.IsCoopNone())
    {
      this.Logd("EnemyLoadComplete skip. became original in loading. enemy={0},client={1}", (object) enemy.id, (object) client_id);
    }
    else
    {
      enemy.RemoveController();
      if (enemy.isCoopInitialized)
      {
        this.Logd("EnemyLoadComplete skip. already initilized. enemy={0},client={1}", (object) enemy.id, (object) client_id);
      }
      else
      {
        this.Logd("EnemyLoadComplete. enemy={0},client={1}", (object) enemy.id, (object) client_id);
        if (init)
        {
          ((Component) enemy).gameObject.SetActive(false);
          MonoBehaviourSingleton<StageObjectManager>.I.AddCacheObject((StageObject) enemy);
        }
        enemy.packetReceiver.SetFilterMode(ObjectPacketReceiver.FILTER_MODE.WAIT_INITIALIZE);
        enemy.enemySender.OnLoadComplete(true);
      }
    }
  }

  public bool OnRecvStageInfo(Coop_Model_StageInfo model, CoopPacket packet)
  {
    if (!this.isActivateStart)
      return false;
    this.Logd("Responsed StageInfo from {0}. elapsedTime={1},gimmicks={2}", (object) packet.fromClientId, (object) model.elapsedTime, (object) model.gimmicks.Count);
    this.isRecvStageInfo = true;
    if (MonoBehaviourSingleton<InGameProgress>.IsValid())
    {
      CoopClient byClientId = MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.FindByClientId(packet.fromClientId);
      if (Object.op_Inequality((Object) byClientId, (Object) null) && byClientId.IsBattleStart())
      {
        if ((MonoBehaviourSingleton<InGameManager>.I.IsRush() || QuestManager.IsValidInGameWaveMatch()) && (double) model.rushLimitTime >= 0.0)
          MonoBehaviourSingleton<InGameProgress>.I.SetLimitTime(model.rushLimitTime);
        MonoBehaviourSingleton<InGameProgress>.I.StartTimer(model.elapsedTime);
      }
      this.isInFieldFishingEnemyBattle = model.isInFieldFishingEnemyBattle;
      if (model.isInFieldEnemyBossBattle)
      {
        this.isEnterFieldEnemyBossBattle = true;
        if (!this.isInFieldFishingEnemyBattle)
          MonoBehaviourSingleton<InGameProgress>.I.ResetStartTimer(model.rushLimitTime, model.elapsedTime);
      }
    }
    if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
    {
      model.gimmicks.ForEach((Action<Coop_Model_StageInfo.GimmickInfo>) (gimmick =>
      {
        StageObject gimmick1 = MonoBehaviourSingleton<StageObjectManager>.I.FindGimmick(gimmick.id);
        if (!Object.op_Inequality((Object) gimmick1, (Object) null))
          return;
        ((Component) gimmick1).gameObject.SetActive(gimmick.enable);
        gimmick1.SetCoopMode(StageObject.COOP_MODE_TYPE.MIRROR, packet.fromClientId);
      }));
      model.carriableGimmickInfos.ForEach((Action<Coop_Model_StageInfo.FieldCarriableGimmickInfo>) (gimmickInfo =>
      {
        FieldCarriableGimmickObject fieldGimmickObj = MonoBehaviourSingleton<InGameProgress>.I.GetFieldGimmickObj(InGameProgress.eFieldGimmick.CarriableGimmick, gimmickInfo.pointId) as FieldCarriableGimmickObject;
        if (Object.op_Equality((Object) fieldGimmickObj, (Object) null) || fieldGimmickObj.GetId() != gimmickInfo.pointId)
          return;
        fieldGimmickObj.SetCarriableGimmickInfo(gimmickInfo);
        if (fieldGimmickObj.isCarrying || !((Component) fieldGimmickObj).gameObject.activeSelf)
          return;
        fieldGimmickObj.EndCarry();
      }));
      model.supplyGimmickInfos.ForEach((Action<Coop_Model_StageInfo.FieldSupplyGimmickInfo>) (gimmickInfo =>
      {
        FieldSupplyGimmickObject fieldGimmickObj = MonoBehaviourSingleton<InGameProgress>.I.GetFieldGimmickObj(InGameProgress.eFieldGimmick.SupplyGimmick, gimmickInfo.pointId) as FieldSupplyGimmickObject;
        if (Object.op_Equality((Object) fieldGimmickObj, (Object) null) || fieldGimmickObj.GetId() != gimmickInfo.pointId)
          return;
        fieldGimmickObj.SetSupplyGimmickInfo(gimmickInfo);
      }));
      if (!MonoBehaviourSingleton<StageObjectManager>.I.waveTargetList.IsNullOrEmpty<StageObject>() && !model.waveTargets.IsNullOrEmpty<Coop_Model_StageInfo.WaveTargetInfo>())
      {
        for (int index1 = 0; index1 < MonoBehaviourSingleton<StageObjectManager>.I.waveTargetList.Count; ++index1)
        {
          FieldWaveTargetObject waveTarget1 = MonoBehaviourSingleton<StageObjectManager>.I.waveTargetList[index1] as FieldWaveTargetObject;
          if (!Object.op_Equality((Object) waveTarget1, (Object) null))
          {
            bool flag = false;
            for (int index2 = 0; index2 < model.waveTargets.Count; ++index2)
            {
              Coop_Model_StageInfo.WaveTargetInfo waveTarget2 = model.waveTargets[index2];
              if (waveTarget1.id == waveTarget2.id)
              {
                waveTarget1.SetHp(waveTarget2.hp, waveTarget1.maxHp, true);
                flag = true;
                break;
              }
            }
            if (!flag)
              waveTarget1.SetHp(0, waveTarget1.maxHp, true);
          }
        }
      }
      if (model.firstWaveMatchInfo != null && (double) model.firstWaveMatchPopSec >= 0.0 && this.firstWaveMatchInfo == null && (double) this.firstWaveMatchPopSec <= 0.0)
      {
        this.firstWaveMatchInfo = model.firstWaveMatchInfo;
        this.firstWaveMatchPopSec = model.firstWaveMatchPopSec;
        this.firstWaveMatchSet = false;
        this.DrawEachLineByPopData(this.firstWaveMatchInfo.no);
      }
    }
    if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.self, (Object) null) && !MonoBehaviourSingleton<StageObjectManager>.I.self.isSetAppearPos)
      MonoBehaviourSingleton<StageObjectManager>.I.self.SetAppearPosGuest(model.enemyPos);
    return true;
  }

  public virtual bool OnRecvStageResponseEnd(Coop_Model_StageResponseEnd model, CoopPacket packet)
  {
    CoopClient byClientId = MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.FindByClientId(packet.fromClientId);
    if (Object.op_Equality((Object) byClientId, (Object) null))
      return true;
    if (byClientId.isStageHost)
      this.isHostStageResponseEnd = true;
    byClientId.isStageResponseEnd = true;
    CoopStage.STAGE_REQUEST_ERROR errorId = (CoopStage.STAGE_REQUEST_ERROR) model.error_id;
    this.Logd("Responsed End from {0}. isStageHost={1}. error={2}.", (object) packet.fromClientId, (object) byClientId.isStageHost, (object) errorId);
    switch (errorId)
    {
      case CoopStage.STAGE_REQUEST_ERROR.ENTRY_CLOSE:
      case CoopStage.STAGE_REQUEST_ERROR.STAGE_CLOSE_FAILED:
      case CoopStage.STAGE_REQUEST_ERROR.STAGE_CLOSE_SUCCEED:
        if (QuestManager.IsValidInGameExplore() && errorId == CoopStage.STAGE_REQUEST_ERROR.STAGE_CLOSE_SUCCEED)
        {
          this.isQuestClose = true;
          this.isQuestSucceed = true;
          return true;
        }
        if (MonoBehaviourSingleton<CoopManager>.I.coopRoom.isOwnerFirstClear)
        {
          this.isQuestClose = true;
          this.isQuestSucceed = errorId == CoopStage.STAGE_REQUEST_ERROR.STAGE_CLOSE_SUCCEED;
        }
        MonoBehaviourSingleton<CoopNetworkManager>.I.LoopBackRoomLeave();
        return true;
      case CoopStage.STAGE_REQUEST_ERROR.DEACTIVATE:
        return true;
      default:
        if (!byClientId.isSendPopOriginal)
        {
          this.SendOriginalObjectPop(packet.fromClientId);
          byClientId.isSendPopOriginal = true;
        }
        return true;
    }
  }

  public bool OnRecvRequestPop(Coop_Model_StageRequestPop model, CoopPacket packet)
  {
    if (!this.isActivateStart || !MonoBehaviourSingleton<StageObjectManager>.IsValid() || !MonoBehaviourSingleton<InGameProgress>.IsValid())
      return false;
    if (MonoBehaviourSingleton<InGameProgress>.I.progressEndType != InGameProgress.PROGRESS_END_TYPE.NONE || !model.isSelf || !model.isPlayer)
      return true;
    Player self = (Player) MonoBehaviourSingleton<StageObjectManager>.I.self;
    if (Object.op_Inequality((Object) self, (Object) null) && (self.IsOriginal() || self.IsCoopNone()))
    {
      this.packetSender.SendStagePlayerPop(self, packet.fromClientId);
      this.Logd("StageRequest response SelfPop({0}). to={1}", (object) self.id, (object) packet.fromClientId);
    }
    return true;
  }

  public void CheckEntryClose(bool force = false)
  {
    if (this.isEntryClose || !this.isActivateStart || !QuestManager.IsValidInGame())
      return;
    int reason = 0;
    if (force)
      reason = 10;
    if (MonoBehaviourSingleton<CoopManager>.I.coopMyClient.IsBattleStart() && MonoBehaviourSingleton<StageObjectManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.boss, (Object) null) && MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
    {
      Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
      int num = (int) ((double) boss.hpMax * (double) MonoBehaviourSingleton<InGameSettingsManager>.I.room.entryCloseEnemyHpRate);
      if ((boss.IsCoopNone() || boss.IsOriginal()) && boss.hp < num)
        reason = 1;
    }
    if (MonoBehaviourSingleton<CoopManager>.I.coopMyClient.IsBattleStart() && MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameSettingsManager>.IsValid() && (double) (int) ((double) MonoBehaviourSingleton<InGameProgress>.I.limitTime * (double) MonoBehaviourSingleton<InGameSettingsManager>.I.room.entryCloseTimeRate) > (double) MonoBehaviourSingleton<InGameProgress>.I.remaindTime)
      reason = 2;
    if (reason == 0)
      return;
    this.isEntryClose = true;
    MonoBehaviourSingleton<CoopNetworkManager>.I.RoomEntryClose(reason);
  }

  public void SetQuestClose(bool is_succeed)
  {
    this.Logd("SetQuestClose. is_succeed={0}", (object) is_succeed);
    this.isQuestClose = true;
    this.isQuestSucceed = is_succeed;
    if (!MonoBehaviourSingleton<CoopManager>.I.coopRoom.isOwnerFirstClear || !MonoBehaviourSingleton<CoopManager>.I.coopMyClient.isPartyOwner)
      return;
    this.packetSender.SendStageQuestClose(is_succeed);
  }

  public void SetRetireQuestClose()
  {
    this.Logd("SetRetireQuestClose. ");
    for (int idx = 0; idx < 8; ++idx)
    {
      CoopClient at = MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.GetAt(idx);
      if (Object.op_Implicit((Object) at) && !(at is CoopMyClient) && !at.isBattleRetire)
        return;
    }
    this.CheckEntryClose(true);
    this.isQuestClose = true;
    this.isQuestSucceed = false;
    this.packetSender.SendStageQuestClose(false);
  }

  public bool OnRecvQuestClose(bool is_succeed)
  {
    if (!this.isActivateStart)
      return false;
    this.Logd("OnRecvQuestClose. is_succeed={0}", (object) is_succeed);
    if (MonoBehaviourSingleton<InGameProgress>.IsValid() && !MonoBehaviourSingleton<InGameProgress>.I.isBattleStart)
    {
      this.isQuestClose = true;
      this.isQuestSucceed = is_succeed;
      MonoBehaviourSingleton<CoopManager>.I.coopRoom.SwitchOfflinePlay();
    }
    return true;
  }

  public void StageTimeup()
  {
    this.Logd("StageTimeup.");
    this.packetSender.SendStageTimeup();
  }

  public bool OnRecvStageTimeup()
  {
    this.Logd("OnRecvStageTimeup.");
    return !MonoBehaviourSingleton<InGameProgress>.IsValid() || MonoBehaviourSingleton<InGameProgress>.I.BattleTimeup();
  }

  public void SetChatConnection(ChatCoopConnection chat_connection)
  {
    this.chatConnection = chat_connection;
  }

  public void StageChat(int chara_id, int chat_id)
  {
    this.packetSender.SendStageChat(chara_id, chat_id);
  }

  public bool OnRecvStageChat(Coop_Model_StageChat model)
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return false;
    Character character = MonoBehaviourSingleton<StageObjectManager>.I.FindCharacter(model.chara_id) as Character;
    if (Object.op_Inequality((Object) character, (Object) null))
      character.ChatSay(model.chat_id);
    return true;
  }

  public void SendChatMessage(int chara_id, string message)
  {
    this.packetSender.SendChatMessage(chara_id, message);
  }

  public void SendChatStamp(int chara_id, int stamp_id)
  {
    this.packetSender.SendChatStamp(chara_id, stamp_id);
  }

  public bool OnRecvChatMessage(int fromClientId, Coop_Model_StageChatMessage model)
  {
    Character character = (Character) null;
    if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
    {
      character = MonoBehaviourSingleton<StageObjectManager>.I.FindCharacter(model.chara_id) as Character;
      if (Object.op_Inequality((Object) character, (Object) null))
        character.ChatSay(model.text);
      else if (QuestManager.IsValidInGameExplore())
      {
        ExplorePlayerStatus explorePlayerStatus = MonoBehaviourSingleton<QuestManager>.I.GetExplorePlayerStatus(model.user_id);
        if (MonoBehaviourSingleton<UIInGameMessageBar>.IsValid() && ((Behaviour) MonoBehaviourSingleton<UIInGameMessageBar>.I).isActiveAndEnabled && explorePlayerStatus != null)
          MonoBehaviourSingleton<UIInGameMessageBar>.I.Announce(explorePlayerStatus.userName, model.text);
      }
    }
    if (this.chatConnection != null)
    {
      CoopClient byClientId = MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.FindByClientId(fromClientId);
      if (Object.op_Inequality((Object) byClientId, (Object) null))
        this.chatConnection.OnReceiveMessage(model.user_id, byClientId.GetPlayerName(), model.text);
      else if (Object.op_Inequality((Object) character, (Object) null))
        this.chatConnection.OnReceiveMessage(model.user_id, character.charaName, model.text);
    }
    return true;
  }

  public bool OnRecvChatStamp(Coop_Model_StageChatStamp model)
  {
    Character character = (Character) null;
    if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
    {
      character = MonoBehaviourSingleton<StageObjectManager>.I.FindCharacter(model.chara_id) as Character;
      if (Object.op_Inequality((Object) character, (Object) null))
        character.ChatSayStamp(model.stamp_id);
    }
    if (this.chatConnection != null)
    {
      CoopClient byPlayerId = MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.FindByPlayerId(model.chara_id);
      if (Object.op_Inequality((Object) byPlayerId, (Object) null))
        this.chatConnection.OnReceiveStamp(model.user_id, byPlayerId.GetPlayerName(), model.stamp_id);
      else if (Object.op_Inequality((Object) character, (Object) null))
        this.chatConnection.OnReceiveStamp(model.user_id, character.charaName, model.stamp_id);
    }
    return true;
  }

  public bool OnRecvEnemyDefeat(Coop_Model_EnemyDefeat model)
  {
    if (!this.isActivateStart)
      return false;
    if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
    {
      Enemy enemy = MonoBehaviourSingleton<StageObjectManager>.I.FindEnemy(model.sid) as Enemy;
      if (Object.op_Inequality((Object) enemy, (Object) null))
      {
        enemy.StopForceEnemyOut();
        Vector3 position = enemy._position;
        model.x = (int) position.x;
        model.z = (int) position.z;
      }
    }
    bool IsDefeatFieldDelivery = false;
    if (MonoBehaviourSingleton<InGameManager>.IsValid())
    {
      List<InGameManager.DropDeliveryInfo> _defeatFieldEnemyDelivery = (List<InGameManager.DropDeliveryInfo>) null;
      List<InGameManager.DropDeliveryInfo> deliveryList;
      List<InGameManager.DropItemInfo> itemList;
      MonoBehaviourSingleton<InGameManager>.I.CreateDropInfoList(model, out deliveryList, out itemList);
      List<InGameManager.DropDeliveryInfo> all = deliveryList.FindAll((Predicate<InGameManager.DropDeliveryInfo>) (d => !d.IsCountUpAtDefeatFieldEnemy()));
      MonoBehaviourSingleton<InGameManager>.I.CreateDropObject(model, all, itemList);
      if (!model.dropLoungeShare)
        _defeatFieldEnemyDelivery = deliveryList.FindAll((Predicate<InGameManager.DropDeliveryInfo>) (d => d.IsCountUpAtDefeatFieldEnemy()));
      if (this.IsNeededCountDefeatFieldDelivery(_defeatFieldEnemyDelivery, model))
      {
        CoopStage.DefeatFieldEnemyDelivery fieldEnemyDelivery = new CoopStage.DefeatFieldEnemyDelivery()
        {
          rewardId = model.rewardId2,
          deliveryList = _defeatFieldEnemyDelivery
        };
        this.defeatFieldEnemyDeliveryList.Add(fieldEnemyDelivery);
        MonoBehaviourSingleton<CoopNetworkManager>.I.RewardGet(fieldEnemyDelivery.rewardId);
      }
      IsDefeatFieldDelivery = _defeatFieldEnemyDelivery != null && _defeatFieldEnemyDelivery.Count > 0;
    }
    this.fieldRewardPool.AddEnemyDefeat(model, IsDefeatFieldDelivery);
    if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
    {
      UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
      userStatus.exp = (XorInt) ((int) userStatus.exp + model.exp);
      int needExp = (int) Singleton<UserLevelTable>.I.GetLevelTable(Singleton<UserLevelTable>.I.GetMaxLevel()).needExp;
      if ((int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.exp > needExp)
        MonoBehaviourSingleton<UserInfoManager>.I.userStatus.exp = (XorInt) needExp;
      MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money += model.money;
      if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money > MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.MONEY_MAX)
        MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.MONEY_MAX;
      if ((double) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.ExpProgress01 >= 1.0)
        this.fieldRewardPool.SendFieldDrop();
    }
    if (MonoBehaviourSingleton<FieldManager>.IsValid())
    {
      bool flag = true;
      if (!TutorialStep.HasFirstDeliveryCompleted())
        flag = false;
      if (flag)
      {
        FieldMapPortalInfo pointToPortalInfo = MonoBehaviourSingleton<FieldManager>.I.GetPortalPointToPortalInfo();
        if (QuestManager.IsValidInGameExplore())
        {
          if (pointToPortalInfo != null)
          {
            int point = pointToPortalInfo.GetNowPortalPoint() + model.ppt;
            MonoBehaviourSingleton<CoopManager>.I.coopRoom.packetSender.SendUpdatePortalPoint((int) pointToPortalInfo.portalData.portalID, point, model.x, model.z);
            MonoBehaviourSingleton<QuestManager>.I.UpdateExplorePortalPoint((int) pointToPortalInfo.portalData.portalID, point, model.x, model.z);
          }
        }
        else
        {
          if (MonoBehaviourSingleton<FieldManager>.I.AddPortalPointToPortalInfo(model.ppt))
            this.fieldRewardPool.SendFieldDrop();
          if (pointToPortalInfo != null && MonoBehaviourSingleton<InGameProgress>.IsValid())
            MonoBehaviourSingleton<InGameProgress>.I.CreatePortalPoint(pointToPortalInfo, model);
        }
      }
    }
    if (model.money > 0)
    {
      Vector3 pos;
      // ISSUE: explicit constructor call
      ((Vector3) ref pos).\u002Ector((float) model.x, 0.0f, (float) model.z);
      EffectManager.OneShot("ef_btl_drop_coin_01", pos, Quaternion.identity);
      SoundManager.PlayOneShotSE(10000065, pos);
    }
    return true;
  }

  private bool IsNeededCountDefeatFieldDelivery(
    List<InGameManager.DropDeliveryInfo> _defeatFieldEnemyDelivery,
    Coop_Model_EnemyDefeat model)
  {
    return _defeatFieldEnemyDelivery != null && _defeatFieldEnemyDelivery.Count > 0 && (!GameDefine.IsFieldBossTreasureBox(model.boxType) || model.exp > 0);
  }

  public bool OnRecvRewardPickup(Coop_Model_RewardPickup model)
  {
    if (!this.isActivateStart)
      return false;
    if (model.rewardKeyId > 0)
      this.fieldRewardPool.AddRewardPickup(model);
    else
      this.fieldRewardPool.ExpireRewardPickup(model);
    if (!MonoBehaviourSingleton<InGameManager>.IsValid())
      return true;
    CoopStage.DefeatFieldEnemyDelivery fieldEnemyDelivery = this.defeatFieldEnemyDeliveryList.Find((Predicate<CoopStage.DefeatFieldEnemyDelivery>) (d => d.rewardId == model.rewardId));
    if (fieldEnemyDelivery == null)
    {
      MonoBehaviourSingleton<InGameManager>.I.DeleteDropObject(model.rewardId, model.rewardKeyId > 0);
      return true;
    }
    if (MonoBehaviourSingleton<UIDropAnnounce>.IsValid() && fieldEnemyDelivery != null)
    {
      this.defeatFieldEnemyDeliveryList.Remove(fieldEnemyDelivery);
      List<UIDropAnnounce.DropAnnounceInfo> announceInfoList = MonoBehaviourSingleton<InGameManager>.I.CreateDropAnnounceInfoList(fieldEnemyDelivery.deliveryList, new List<InGameManager.DropItemInfo>(), false);
      int index = 0;
      for (int count = announceInfoList.Count; index < count; ++index)
        MonoBehaviourSingleton<UIDropAnnounce>.I.Announce(announceInfoList[index]);
    }
    return true;
  }

  public bool OnRecvEnemyExtermination(Coop_Model_EnemyExtermination model)
  {
    this.Logd("EnemyExtermination!");
    if (QuestManager.IsValidInGameExplore())
      return true;
    if (MonoBehaviourSingleton<InGameManager>.I.IsRush())
    {
      this.isEnemyExtermination = true;
      return true;
    }
    if (!MonoBehaviourSingleton<CoopManager>.I.coopMyClient.IsBattleStart())
      MonoBehaviourSingleton<CoopNetworkManager>.I.LoopBackRoomLeave();
    else
      this.isEnemyExtermination = true;
    return true;
  }

  public bool OnRecvEventHappenQuest(Coop_Model_EventHappenQuest model)
  {
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
      return true;
    uint qId = (uint) model.qId;
    QuestInfoData.Quest.Reward[] reward = (QuestInfoData.Quest.Reward[]) null;
    if (model.rewards.Count > 0)
    {
      int n = 0;
      reward = new QuestInfoData.Quest.Reward[model.rewards.Count];
      model.rewards.ForEach((Action<List<int>>) (r => reward[n++] = new QuestInfoData.Quest.Reward(r[0], r[1], r[2])));
    }
    if (MonoBehaviourSingleton<InGameManager>.IsValid())
    {
      PortalUnlockEvent component = ((Component) MonoBehaviourSingleton<InGameManager>.I).gameObject.GetComponent<PortalUnlockEvent>();
      if (Object.op_Inequality((Object) component, (Object) null))
        Object.Destroy((Object) component);
      MonoBehaviourSingleton<InGameManager>.I.OpenAllDropObject();
    }
    MonoBehaviourSingleton<InGameProgress>.I.HappenQuestDirection(qId, reward, model.rareBossType);
    return true;
  }

  public bool OnRecvEventHappenQuestStatus(Coop_Model_EventHappenQuestStatus model)
  {
    if (MonoBehaviourSingleton<InGameManager>.IsValid())
      MonoBehaviourSingleton<InGameManager>.I.happenQuestStatusList = model.statusList;
    return true;
  }

  public void OnRecvSyncTimeRequest(Coop_Model_StageSyncTimeRequest model, int toClientId)
  {
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
      return;
    this.StartCoroutine(this.DoStageSyncTime(model, toClientId));
  }

  private IEnumerator DoStageSyncTime(Coop_Model_StageSyncTimeRequest model, int toClientId)
  {
    yield return (object) new WaitForSeconds(2f);
    this.packetSender.SendStageSyncTime(MonoBehaviourSingleton<InGameProgress>.I.GetElapsedTime(), toClientId);
  }

  public void OnRecvSyncTime(Coop_Model_StageSyncTime model)
  {
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid() || (double) model.elapsedTime < (double) MonoBehaviourSingleton<InGameProgress>.I.GetElapsedTime())
      return;
    MonoBehaviourSingleton<InGameProgress>.I.SetElapsedTime(model.elapsedTime);
  }

  public void OnRecvSyncPlayerRecord(Coop_Model_StageSyncPlayerRecord model)
  {
    if (!MonoBehaviourSingleton<InGameRecorder>.IsValid())
      return;
    MonoBehaviourSingleton<InGameRecorder>.I.ApplySyncHostData(model.rec);
  }

  public void SendSyncPlayerRecord(int toClientId, bool promise = true)
  {
    if (!MonoBehaviourSingleton<InGameRecorder>.IsValid())
      return;
    List<InGameRecorder.PlayerRecordSyncHost> syncHostData1 = MonoBehaviourSingleton<InGameRecorder>.I.CreateSyncHostData();
    int index = 0;
    for (int count = syncHostData1.Count; index < count; ++index)
      this.packetSender.SendSyncPlayerRecord(syncHostData1[index], toClientId, promise, (Func<Coop_Model_Base, bool>) (resend_model =>
      {
        Coop_Model_StageSyncPlayerRecord syncPlayerRecord = resend_model as Coop_Model_StageSyncPlayerRecord;
        if (MonoBehaviourSingleton<InGameRecorder>.IsValid() && syncPlayerRecord != null && syncPlayerRecord.rec.charaInfo != null)
        {
          InGameRecorder.PlayerRecordSyncHost syncHostData2 = MonoBehaviourSingleton<InGameRecorder>.I.CreateSyncHostData(syncPlayerRecord.rec.charaInfo.userId);
          if (syncHostData2 != null)
          {
            syncPlayerRecord.rec = syncHostData2;
            return true;
          }
        }
        return false;
      }));
  }

  public void OnRecvRushRequested(Coop_Model_RushRequested model)
  {
    if (!this.forceNextWave || this.isRecvRushRequested)
      return;
    int rushIndex = MonoBehaviourSingleton<InGameManager>.I.GetRushIndex();
    if (model.currentWaveIndex <= rushIndex)
      return;
    this.isRecvRushRequested = true;
    MonoBehaviourSingleton<InGameProgress>.I.StartTimer();
    MonoBehaviourSingleton<InGameProgress>.I.StopTimer();
    MonoBehaviourSingleton<InGameProgress>.I.SetElapsedTime(model.syncData.elapsedTime);
    this.bossBreakIDLists[0] = model.syncData.bossBreakIds;
  }

  private void ForceProgressNextWave()
  {
    this.Logd("ForceProgressNextWave current:{0}, other:{1}", (object) MonoBehaviourSingleton<InGameManager>.I.GetRushIndex(), (object) this.GetOtherRushIndex());
    this.StartCoroutine(this._ForceProgressNextWave());
  }

  private IEnumerator _ForceProgressNextWave()
  {
    this.forceNextWave = true;
    this.isRecvRushRequested = false;
    MonoBehaviourSingleton<CoopManager>.I.coopRoom.SendRushRequest();
    float time = Time.time;
    while (!this.isRecvRushRequested)
    {
      yield return (object) null;
      if ((double) Time.time - (double) time > 5.0)
      {
        this.Logd("Timeout RushRequest");
        this.bossBreakIDLists[0].Add(0);
        break;
      }
    }
    this.isRecvRushRequested = true;
    MonoBehaviourSingleton<InGameProgress>.I.BattleComplete(true);
  }

  private int GetOtherRushIndex()
  {
    int otherRushIndex = -1;
    MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.ForEach((Action<CoopClient>) (client =>
    {
      if (client.rushIndex <= otherRushIndex)
        return;
      otherRushIndex = client.rushIndex;
    }));
    return otherRushIndex;
  }

  private bool IsOtherClientProgressed()
  {
    return MonoBehaviourSingleton<InGameManager>.I.IsRush() && this.GetOtherRushIndex() > MonoBehaviourSingleton<InGameManager>.I.GetRushIndex();
  }

  public bool OnRecvStageObjectInfo(Coop_Model_StageObjectInfo model, CoopPacket packet)
  {
    if (!this.isActivateStart)
      return false;
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return true;
    CoopClient byClientId = MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.FindByClientId(packet.fromClientId);
    if (Object.op_Equality((Object) byClientId, (Object) null))
      return true;
    this.Logd("Responsed StageObjectInfo from {0}. sid={1}, CoopMode:{2}, userid={3}", (object) packet.fromClientId, (object) model.StageObjectID, (object) model.CoopModeType, (object) byClientId.userId);
    StageObject stageObject = MonoBehaviourSingleton<StageObjectManager>.I.FindObject(model.StageObjectID);
    if (Object.op_Equality((Object) stageObject, (Object) null))
      return false;
    stageObject.SetCoopMode(model.CoopModeType, packet.fromClientId);
    return true;
  }

  public void ActiveSupply(int pointId)
  {
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
      return;
    FieldSupplyGimmickObject fieldGimmickObj = MonoBehaviourSingleton<InGameProgress>.I.GetFieldGimmickObj(InGameProgress.eFieldGimmick.SupplyGimmick, pointId) as FieldSupplyGimmickObject;
    if (!Object.op_Inequality((Object) fieldGimmickObj, (Object) null))
      return;
    fieldGimmickObj.Active();
  }

  public enum STAGE_REQUEST_ERROR
  {
    NONE,
    ENTRY_CLOSE,
    STAGE_CLOSE_FAILED,
    STAGE_CLOSE_SUCCEED,
    DEACTIVATE,
  }

  private class DefeatFieldEnemyDelivery
  {
    public int rewardId;
    public List<InGameManager.DropDeliveryInfo> deliveryList;
  }
}
