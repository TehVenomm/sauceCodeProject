// Decompiled with JetBrains decompiler
// Type: CoopRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class CoopRoom : MonoBehaviour
{
  public const int MAX_TEAM_CLIENT = 4;
  private SpanTimer timeCheckSpan = new SpanTimer(30f);
  private SpanTimer exploreAliveSpan = new SpanTimer(14f);
  public ChatCoopConnection chatConnection;

  public CoopRoom.ROOM_STATUS status { get; private set; }

  public List<FieldModel.SlotInfo> slotInfos { get; private set; }

  public CoopClientCollector clients { get; private set; }

  public CoopRoomPacketSender packetSender { get; private set; }

  public CoopRoomPacketReceiver packetReceiver { get; private set; }

  public bool isOwnerFirstClear { get; private set; }

  public bool isOwnerCleared { get; private set; }

  public bool forceRetire { get; private set; }

  public bool ownerRetire { get; set; }

  public bool isOfflinePlay { get; private set; }

  public int roomLeaveCnt { get; private set; }

  private void Awake()
  {
    this.clients = new CoopClientCollector();
    this.packetSender = ((Component) this).gameObject.AddComponent<CoopRoomPacketSender>();
    this.packetReceiver = ((Component) this).gameObject.AddComponent<CoopRoomPacketReceiver>();
  }

  private void Update()
  {
    this.packetReceiver.OnUpdate();
    if (QuestManager.IsValidInGameExplore() && MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameProgress>.I.IsStartTimer() && this.timeCheckSpan.IsReady())
      MonoBehaviourSingleton<CoopNetworkManager>.I.RoomTimeCheck();
    if (!QuestManager.IsValidInGameExplore() || !this.exploreAliveSpan.IsReady() || !MonoBehaviourSingleton<CoopManager>.I.coopMyClient.isPartyOwner)
      return;
    this.packetSender.SendExploreAlive();
  }

  private void Logd(string str, params object[] objs)
  {
    int num = Log.enabled ? 1 : 0;
  }

  public void Clear()
  {
    this.status = CoopRoom.ROOM_STATUS.NONE;
    this.isOfflinePlay = false;
    this.isOwnerFirstClear = false;
    this.isOwnerCleared = false;
    this.forceRetire = false;
    this.ownerRetire = false;
    this.roomLeaveCnt = 0;
    this.chatConnection = (ChatCoopConnection) null;
    this.DestroyAllClient();
  }

  public void OnStageChangeInterval()
  {
    this.SetStatus(CoopRoom.ROOM_STATUS.STAGE);
    this.roomLeaveCnt = 0;
    this.clients.ForEach((Action<CoopClient>) (c => c.OnStageChangeInterval()));
  }

  public void OnQuestSeriesInterval()
  {
    this.SetStatus(CoopRoom.ROOM_STATUS.STAGE);
    this.clients.ForEach((Action<CoopClient>) (c => c.OnQuestSeriesInterval()));
  }

  private void SetStatus(CoopRoom.ROOM_STATUS st)
  {
    this.Logd("status {0} => {1}", (object) this.status, (object) st);
    this.status = st;
  }

  public bool IsActivate() => this.status != 0;

  public bool IsStage() => this.status >= CoopRoom.ROOM_STATUS.STAGE;

  public bool IsBattle() => this.status >= CoopRoom.ROOM_STATUS.BATTLE;

  public void SetOwnerFirstClear(bool is_owner_first_clear)
  {
    this.isOwnerFirstClear = is_owner_first_clear;
    this.Logd("isOwnerFirstClear:{0}", (object) this.isOwnerFirstClear);
  }

  public void SetOwnerCleared()
  {
    this.isOwnerCleared = true;
    this.Logd("owner is cleared!");
  }

  public void Activate(List<FieldModel.SlotInfo> slot_infos)
  {
    this.Deactivate();
    this.SetSlotInfos(slot_infos);
    Coop_Model_RegisterACK registerAck = MonoBehaviourSingleton<CoopNetworkManager>.I.registerAck;
    if (registerAck != null)
    {
      this.InitStage(registerAck.ids, registerAck.stgids, registerAck.stgidxs, registerAck.stghosts);
      this.InitClients(registerAck.ids, registerAck.stgids, registerAck.stgidxs, registerAck.stghosts);
      this.SetOwnerFirstClear(registerAck.of);
    }
    this.SetStatus(CoopRoom.ROOM_STATUS.STAGE);
    MonoBehaviourSingleton<CoopManager>.I.coopMyClient.StageStart();
    if (!this.isOwnerFirstClear || !Object.op_Equality((Object) this.clients.FindPartyOwner(), (Object) null))
      return;
    this.Logd("Activate... party owner not found.");
    MonoBehaviourSingleton<CoopNetworkManager>.I.Close();
  }

  public void Deactivate() => this.Clear();

  public void SetSlotInfos(List<FieldModel.SlotInfo> slot_infos)
  {
    this.slotInfos = slot_infos;
    if (this.slotInfos == null)
      return;
    int num = 0;
    for (int count = this.slotInfos.Count; num < count; ++num)
    {
      FieldModel.SlotInfo slotInfo = this.slotInfos[num];
      CoopClient byClientId = this.clients.FindByClientId(slotInfo.userId);
      if (Object.op_Inequality((Object) byClientId, (Object) null))
        byClientId.Activate(slotInfo.userId, slotInfo.token, (CharaInfo) slotInfo.userInfo, num);
    }
  }

  private void InitClients(
    List<int> ids,
    List<int> stgids,
    List<int> stgidxs,
    List<bool> stghosts)
  {
    this.DestroyAllClient();
    int counter = 0;
    int index = 0;
    for (int count = ids.Count; index < count; ++index)
    {
      if (!this.OnJoinClient(ids[index], stgids[index], stgidxs[index], stghosts[index], counter).IsActivate())
        ++counter;
    }
  }

  public void DestroyClient(CoopClient client)
  {
    if (Object.op_Equality((Object) client, (Object) null))
      return;
    this.Logd("destory client:{0}", (object) client.clientId);
    if (CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected())
      MonoBehaviourSingleton<KtbWebSocket>.I.RemoveResendPackets(client.clientId);
    this.clients.Remove(client);
    client.Clear();
    if (client is CoopMyClient)
      return;
    this.Logd("object destory client");
    Object.Destroy((Object) ((Component) client).gameObject);
    client = (CoopClient) null;
  }

  public void DestroyAllClient()
  {
    this.clients.ForEach((Action<CoopClient>) (c => this.DestroyClient(c)));
  }

  public void DestroyAllGuestClient()
  {
    this.clients.ForEach((Action<CoopClient>) (c =>
    {
      if (c is CoopMyClient)
        return;
      this.DestroyClient(c);
    }));
  }

  private void InitStage(List<int> ids, List<int> stgids, List<int> stgidxs, List<bool> stghosts)
  {
    int id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    int stage_id = 0;
    int stage_idx = 0;
    int index1 = 0;
    for (int count = ids.Count; index1 < count; ++index1)
    {
      if (id == ids[index1])
      {
        stage_id = stgids[index1];
        stage_idx = stgidxs[index1];
        break;
      }
    }
    int host_client_id = 0;
    int index2 = 0;
    for (int count = ids.Count; index2 < count; ++index2)
    {
      if (stage_id == stgids[index2] && stghosts[index2])
      {
        host_client_id = ids[index2];
        break;
      }
    }
    MonoBehaviourSingleton<CoopManager>.I.coopStage.OnInitStage(stage_id, stage_idx, host_client_id);
  }

  private CoopClient OnJoinClient(
    int client_id,
    int stgid,
    int stgidx,
    bool stghost,
    int counter = 0)
  {
    CoopClient byClientId = this.clients.FindByClientId(client_id);
    if (Object.op_Inequality((Object) byClientId, (Object) null))
    {
      this.Logd("OnJoinClient: already join. client={0}", (object) byClientId);
      return byClientId;
    }
    CoopClient client;
    if (client_id == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
    {
      client = (CoopClient) MonoBehaviourSingleton<CoopManager>.I.coopMyClient;
      client.Init(client_id);
    }
    else
    {
      client = (CoopClient) Utility.CreateGameObjectAndComponent("CoopClient", ((Component) this).transform);
      client.Init(client_id);
      MonoBehaviourSingleton<CoopManager>.I.coopMyClient.WelcomeClient(client_id);
      if (QuestManager.IsValidInGameExplore())
      {
        if (MonoBehaviourSingleton<CoopManager>.I.isStageHost)
        {
          MonoBehaviourSingleton<CoopManager>.I.coopRoom.packetSender.SendSyncAllPortalPoint(MonoBehaviourSingleton<QuestManager>.I.GetExploreStatus().GetAllPortalData(), client_id);
          MonoBehaviourSingleton<CoopManager>.I.coopRoom.packetSender.SendSyncExploreBoss(MonoBehaviourSingleton<QuestManager>.I.GetExploreStatus(), client_id);
        }
        if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.self, (Object) null))
          MonoBehaviourSingleton<CoopManager>.I.coopRoom.packetSender.SendSyncPlayerStatus(MonoBehaviourSingleton<StageObjectManager>.I.self, client_id);
      }
      else if (MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsDefenseBattle())
      {
        if (MonoBehaviourSingleton<CoopManager>.I.isStageHost && MonoBehaviourSingleton<InGameProgress>.IsValid())
          MonoBehaviourSingleton<CoopManager>.I.coopRoom.packetSender.SendSyncDefenseBattle(MonoBehaviourSingleton<InGameProgress>.I.defenseBattleEndurance);
      }
      else if (QuestManager.IsValidInGameWaveMatch() && MonoBehaviourSingleton<CoopManager>.I.isStageHost)
        MonoBehaviourSingleton<InGameProgress>.IsValid();
    }
    client.SetStage(stgid, stgidx, stghost);
    int index = this.slotInfos.FindIndex((Predicate<FieldModel.SlotInfo>) (s => s.userId == client_id));
    if (index >= 0)
    {
      FieldModel.SlotInfo slotInfo = this.slotInfos[index];
      client.Activate(slotInfo.userId, slotInfo.token, (CharaInfo) slotInfo.userInfo, index);
    }
    else if (counter == 0)
      CoopApp.UpdateField();
    this.clients.Add(client);
    if (MonoBehaviourSingleton<CoopManager>.I.coopStage.stageId == client.stageId)
      MonoBehaviourSingleton<CoopManager>.I.coopStage.OnJoinClient(client);
    return client;
  }

  private void OnLeaveClient(CoopClient client, int stgid, int stghostid)
  {
    this.Logd("OnLeaveClient: leave client. status={0} clientId={1} userid={2} token={3}", (object) client.status, (object) client.clientId, (object) client.userId, (object) client.userToken);
    if (client.isLeave)
      return;
    if (MonoBehaviourSingleton<CoopManager>.I.coopMyClient.IsBattleEnd())
    {
      this.Logd("OnLeaveClient: already my client is clear or leave({0}).", (object) MonoBehaviourSingleton<CoopManager>.I.coopMyClient.status);
    }
    else
    {
      client.OnRoomLeaved();
      ++this.roomLeaveCnt;
      if (this.isOwnerFirstClear && client.isPartyOwner)
        this.SwitchOfflinePlay();
      else if (this.NeedsForceLeave())
      {
        this.SwitchOfflinePlay();
      }
      else
      {
        if (stgid == MonoBehaviourSingleton<CoopManager>.I.coopStage.stageId)
          MonoBehaviourSingleton<CoopManager>.I.coopStage.OnLeaveClient(client, stghostid);
        if (client is CoopMyClient)
          this.SwitchOfflinePlay();
        else
          this.DestroyClient(client);
      }
    }
  }

  public void StartBattle() => this.SetStatus(CoopRoom.ROOM_STATUS.BATTLE);

  public bool IsValidBattleComplete()
  {
    return !MonoBehaviourSingleton<KtbWebSocket>.I.IsConnected() || !this.isOwnerFirstClear || MonoBehaviourSingleton<CoopManager>.I.coopMyClient.isPartyOwner || this.isOwnerCleared;
  }

  public void SwitchOfflinePlay()
  {
    if (this.isOfflinePlay)
    {
      this.Logd("already offline.");
    }
    else
    {
      this.isOfflinePlay = true;
      this.Logd("SwitchOfflinePlay.");
      if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameProgress>.I.isEnding || QuestManager.IsValidInGameExplore() && (!MonoBehaviourSingleton<InGameProgress>.IsValid() || MonoBehaviourSingleton<InGameProgress>.I.isEnding))
        return;
      if (CoopWebSocketSingleton<KtbWebSocket>.IsValidOpen())
        MonoBehaviourSingleton<CoopNetworkManager>.I.Close(call_back: (System.Action) (() => { }));
      if (this.isOwnerFirstClear && !MonoBehaviourSingleton<CoopManager>.I.coopMyClient.isPartyOwner && !MonoBehaviourSingleton<CoopManager>.I.coopStage.isQuestClose)
        this.forceRetire = true;
      else if (this.NeedsForceLeave() && !MonoBehaviourSingleton<CoopManager>.I.coopStage.isQuestClose)
      {
        this.forceRetire = true;
        if (!PartyManager.IsValidInParty())
          return;
        Protocol.Force((System.Action) (() => MonoBehaviourSingleton<PartyManager>.I.SendLeave((Action<bool>) (is_leave => this.Logd("PartyLeave. {0}", (object) is_leave)))));
      }
      else
      {
        MonoBehaviourSingleton<CoopManager>.I.coopStage.OnSwitchOfflinePlay();
        if (PartyManager.IsValidInParty() && !InGameManager.IsReentryNotLeaveParty())
          Protocol.Force((System.Action) (() => MonoBehaviourSingleton<PartyManager>.I.SendLeave((Action<bool>) (is_leave => this.Logd("PartyLeave. {0}", (object) is_leave)))));
        this.DestroyAllGuestClient();
      }
    }
  }

  public bool OnRecvRoomJoined(Coop_Model_RoomJoined model)
  {
    if (!this.IsActivate())
      return true;
    this.OnJoinClient(model.cid, model.stgid, model.stgidx, model.stghostid == model.cid);
    return true;
  }

  public bool OnRecvRoomLeaved(Coop_Model_RoomLeaved model)
  {
    CoopClient byClientId = this.clients.FindByClientId(model.cid);
    if (Object.op_Equality((Object) byClientId, (Object) null) || byClientId.userToken != model.token)
    {
      this.Logd("OnRecvRoomLeaved: client not found. clientId={0}, client={1}", (object) model.cid, (object) byClientId);
      return true;
    }
    this.OnLeaveClient(byClientId, model.stgid, model.stghostid);
    return true;
  }

  public bool OnRecvRoomStageChanged(Coop_Model_RoomStageChanged model)
  {
    if (!this.IsActivate())
      return true;
    CoopClient byClientId = this.clients.FindByClientId(model.cid);
    if (Object.op_Equality((Object) byClientId, (Object) null))
    {
      this.Logd("OnRecvRoomStageChanged: client not found. clientId={0}", (object) model.cid);
      return true;
    }
    this.Logd("OnRecvRoomStageChanged: sid={0}, client={1}", (object) model.sid, (object) byClientId);
    byClientId.SetStage(model.stgid, model.stgidx, byClientId.clientId == model.stghostid);
    if (byClientId is CoopMyClient)
    {
      MonoBehaviourSingleton<CoopManager>.I.coopStage.OnInitStage(model.stgid, model.stgidx, model.stghostid);
      MonoBehaviourSingleton<CoopNetworkManager>.I.SetRegisterSID(model.sid);
    }
    else if (model.pstgid == MonoBehaviourSingleton<CoopManager>.I.coopStage.stageId)
      MonoBehaviourSingleton<CoopManager>.I.coopStage.OnLeaveClient(byClientId, model.pstghostid);
    else if (model.stgid == MonoBehaviourSingleton<CoopManager>.I.coopStage.stageId)
      MonoBehaviourSingleton<CoopManager>.I.coopStage.OnJoinClient(byClientId);
    this.clients.ForEach((Action<CoopClient>) (c =>
    {
      if (c.stageId == model.pstgid)
        c.SetStageHost(c.clientId == model.pstghostid);
      if (c.stageId != model.stgid)
        return;
      c.SetStageHost(c.clientId == model.stghostid);
    }));
    return true;
  }

  public bool OnRecvRoomStageRequested(Coop_Model_RoomStageRequested model)
  {
    CoopClient byClientId = this.clients.FindByClientId(model.cid);
    if (Object.op_Equality((Object) byClientId, (Object) null))
      return true;
    if (byClientId is CoopMyClient)
    {
      MonoBehaviourSingleton<CoopManager>.I.coopStage.OnRequested();
    }
    else
    {
      MonoBehaviourSingleton<CoopManager>.I.coopMyClient.WelcomeClient(model.cid);
      if (byClientId.stageId == MonoBehaviourSingleton<CoopManager>.I.coopStage.stageId)
        MonoBehaviourSingleton<CoopManager>.I.coopStage.OnRequestedClient(byClientId);
    }
    return true;
  }

  public bool OnRecvRoomStageHostChanged(Coop_Model_RoomStageHostChanged model)
  {
    int hostClientId = MonoBehaviourSingleton<CoopManager>.I.coopStage.hostClientId;
    if (model.stgid == MonoBehaviourSingleton<CoopManager>.I.coopStage.stageId)
    {
      if (model.stghostid != 0)
        CoopStageObjectUtility.TransfarOwnerForClientObjects(hostClientId, model.stghostid);
      MonoBehaviourSingleton<CoopManager>.I.coopStage.SetHostClient(model.stghostid);
    }
    this.clients.ForEach((Action<CoopClient>) (c =>
    {
      if (c.stageId != model.stgid)
        return;
      c.SetStageHost(c.clientId == model.stghostid);
    }));
    return true;
  }

  public bool OnRecvRoomTimeUpdate(Coop_Model_RoomTimeUpdate model)
  {
    this.Logd("OnRecvRoomTimeUpdate:{0}", (object) model.elapsedSec);
    if (QuestManager.IsValidInGameExplore() && MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.isAlreadyBattleStarted && MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameProgress>.I.enableLimitTime)
      MonoBehaviourSingleton<InGameProgress>.I.SetElapsedTime((float) model.elapsedSec);
    return true;
  }

  public void OnRecvSyncAllPortalPoint(Coop_Model_RoomSyncAllPortalPoint model)
  {
    if (!QuestManager.IsValidExplore())
      return;
    MonoBehaviourSingleton<QuestManager>.I.SyncExplorePortalPoint(model);
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid() || MonoBehaviourSingleton<InGameProgress>.I.portalObjectList == null)
      return;
    foreach (PortalObject portalObject in MonoBehaviourSingleton<InGameProgress>.I.portalObjectList)
      portalObject.Initialize(portalObject.portalInfo);
    MonoBehaviourSingleton<FieldManager>.I.InitPortalPointForExplore(MonoBehaviourSingleton<QuestManager>.I.GetExploreStatus());
  }

  public void OnRecvRoomUpdatePortalPoint(Coop_Model_RoomUpdatePortalPoint model)
  {
    MonoBehaviourSingleton<QuestManager>.I.UpdateExplorePortalPoint(model.pid, model.pt, model.x, model.z);
  }

  public void OnRecvSyncExploreBoss(Coop_Model_RoomSyncExploreBoss model)
  {
    if (MonoBehaviourSingleton<InGameManager>.IsValid() && (!MonoBehaviourSingleton<QuestManager>.I.IsExploreBossMap() || !MonoBehaviourSingleton<QuestManager>.I.IsBossAppearMap(model.mId)))
    {
      int bossMapId = (int) MonoBehaviourSingleton<QuestManager>.I.GetBossMapId();
      if (bossMapId != 0 && bossMapId != model.mId)
        MonoBehaviourSingleton<QuestManager>.I.UpdatePassedPortal();
    }
    MonoBehaviourSingleton<QuestManager>.I.SyncExploreBossStatus(model);
  }

  public void OnRecvSyncExploreBossMap(Coop_Model_RoomSyncExploreBossMap model)
  {
    MonoBehaviourSingleton<QuestManager>.I.SyncExploreBossMap(model);
  }

  public void OnRecvExploreBossDamage(int fromClientId, Coop_Model_RoomExploreBossDamage model)
  {
    CoopClient byClientId = this.clients.FindByClientId(fromClientId);
    if (!Object.op_Inequality((Object) byClientId, (Object) null))
      return;
    MonoBehaviourSingleton<QuestManager>.I.UpdateExploreTotalDamageToBoss(byClientId, model.dmg);
  }

  public void OnRecvExploreAlive()
  {
    if (MonoBehaviourSingleton<InGameProgress>.IsValid())
    {
      MonoBehaviourSingleton<InGameProgress>.I.ResetExploreHostDCTimer();
    }
    else
    {
      if (!QuestManager.IsValidInGameExplore())
        return;
      MonoBehaviourSingleton<QuestManager>.I.UpdateExploreHostDCTime(30f);
    }
  }

  public void OnRecvExploreAliveRequest()
  {
    if (Object.op_Equality((Object) this.packetSender, (Object) null) || !MonoBehaviourSingleton<CoopManager>.IsValid() || !MonoBehaviourSingleton<CoopManager>.I.coopMyClient.isPartyOwner)
      return;
    this.packetSender.SendExploreAlive();
  }

  public bool OnRecvExploreBossDead(Coop_Model_RoomExploreBossDead model)
  {
    MonoBehaviourSingleton<QuestManager>.I.SetExploreBossDead(model);
    if (model.dmgs != null)
    {
      foreach (Coop_Model_RoomExploreBossDead.TotalDamage dmg in model.dmgs)
        MonoBehaviourSingleton<QuestManager>.I.UpdateExploreTotalDamageToBoss(dmg.uid, dmg.dmg);
    }
    if (MonoBehaviourSingleton<QuestManager>.I.IsExploreBossMap() && MonoBehaviourSingleton<CoopManager>.I.coopMyClient.IsBattleStart())
      return true;
    CoopStage coopStage = MonoBehaviourSingleton<CoopManager>.I.coopStage;
    if (coopStage.bossBreakIDLists == null)
      coopStage.InitBossBreakIdList();
    int index = 0;
    if (QuestManager.IsValidInGame())
      index = (int) MonoBehaviourSingleton<QuestManager>.I.currentQuestSeriesIndex;
    MonoBehaviourSingleton<CoopManager>.I.coopStage.bossBreakIDLists[index] = model.breakIds;
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
      return false;
    foreach (MissionCheckBase missionCheckBase in MonoBehaviourSingleton<InGameProgress>.I.missionCheck)
    {
      if (missionCheckBase is MissionCheckDownCount missionCheckDownCount)
        missionCheckDownCount.SetCount(model.downCount);
    }
    if (!MonoBehaviourSingleton<InGameProgress>.I.BattleComplete())
      MonoBehaviourSingleton<CoopManager>.I.coopStage.SetQuestClose(true);
    return true;
  }

  public void OnRecvNotifyEncounterBoss(int fromClientId, Coop_Model_RoomNotifyEncounterBoss model)
  {
    if (QuestManager.IsValidInGameExplore() && MonoBehaviourSingleton<QuestManager>.I.IsExploreBossMap())
      return;
    if (!MonoBehaviourSingleton<QuestManager>.I.IsEncountered() && MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameProgress>.I.progressEndType == InGameProgress.PROGRESS_END_TYPE.NONE)
    {
      CoopClient byClientId = this.clients.FindByClientId(fromClientId);
      MonoBehaviourSingleton<QuestManager>.I.SetMemberEncounteredMap(model.mid);
      uint currentQuestId = MonoBehaviourSingleton<QuestManager>.I.currentQuestID;
      int mainEnemyId = Singleton<QuestTable>.I.GetQuestData(currentQuestId).GetMainEnemyID();
      string enemyName = Singleton<EnemyTable>.I.GetEnemyName((uint) mainEnemyId);
      UIInGamePopupDialog.PushOpen(StringTable.Format(STRING_CATEGORY.IN_GAME, 8000U, (object) byClientId.GetPlayerName(), (object) enemyName), false, 1.22f);
    }
    if (!QuestManager.IsValidExplore())
      return;
    MonoBehaviourSingleton<QuestManager>.I.UpdatePortalUsedFlag(model.pid);
  }

  public void OnRecvNotifyTraceBoss(int fromClientId, Coop_Model_RoomNotifyTraceBoss model)
  {
    if (!MonoBehaviourSingleton<QuestManager>.IsValid())
      return;
    ExploreStatus.TraceInfo[] bossTraceHistory = MonoBehaviourSingleton<QuestManager>.I.GetBossTraceHistory();
    if (bossTraceHistory != null && bossTraceHistory.Length != 0 && bossTraceHistory[bossTraceHistory.Length - 1].mapId == model.mid)
      return;
    bool reserve = false;
    string playerName = this.clients.FindByClientId(fromClientId).GetPlayerName();
    if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameProgress>.I.progressEndType == InGameProgress.PROGRESS_END_TYPE.NONE)
      UIInGamePopupDialog.PushOpen(StringTable.Format(STRING_CATEGORY.IN_GAME, (uint) (model.lc != 0 ? 8004 : 8003), (object) playerName), false, 1.22f);
    else if (QuestManager.IsValidExplore())
      reserve = true;
    if (!QuestManager.IsValidExplore())
      return;
    MonoBehaviourSingleton<QuestManager>.I.UpdateBossTraceHistory(model.mid, model.lc, playerName, reserve);
  }

  public void OnRecvSyncPlayerStatus(int fromClientId, Coop_Model_RoomSyncPlayerStatus model)
  {
    if (!QuestManager.IsValidExplore())
      return;
    CoopClient byClientId = this.clients.FindByClientId(fromClientId);
    if (model.hp <= 0 && Object.op_Implicit((Object) byClientId))
    {
      ExplorePlayerStatus explorePlayerStatus = MonoBehaviourSingleton<QuestManager>.I.GetExplorePlayerStatus(byClientId.userId);
      if (explorePlayerStatus != null && explorePlayerStatus.hp > 0 && MonoBehaviourSingleton<UIDeadAnnounce>.IsValid())
        MonoBehaviourSingleton<UIDeadAnnounce>.I.Announce(UIDeadAnnounce.ANNOUNCE_TYPE.DEAD, byClientId.GetPlayerName());
    }
    MonoBehaviourSingleton<QuestManager>.I.UpdateExplorePlayerStatus(byClientId, model);
  }

  public void OnRecvChatStamp(int fromClientId, Coop_Model_RoomChatStamp model)
  {
    CoopClient byClientId = this.clients.FindByClientId(fromClientId);
    if (Object.op_Equality((Object) byClientId, (Object) null))
      return;
    Player player = byClientId.GetPlayer();
    if (Object.op_Implicit((Object) player))
      player.ChatSayStamp(model.stampId);
    else if (QuestManager.IsValidInGameExplore())
    {
      ExplorePlayerStatus explorePlayerStatus = MonoBehaviourSingleton<QuestManager>.I.GetExplorePlayerStatus(model.userId);
      if (MonoBehaviourSingleton<UIInGameMessageBar>.IsValid() && ((Behaviour) MonoBehaviourSingleton<UIInGameMessageBar>.I).isActiveAndEnabled && explorePlayerStatus != null)
        MonoBehaviourSingleton<UIInGameMessageBar>.I.Announce(explorePlayerStatus.userName, model.stampId);
    }
    if (this.chatConnection == null)
      return;
    if (Object.op_Implicit((Object) player))
    {
      this.chatConnection.OnReceiveStamp(byClientId.userId, player.charaName, model.stampId);
    }
    else
    {
      if (!Object.op_Inequality((Object) byClientId, (Object) null))
        return;
      this.chatConnection.OnReceiveStamp(byClientId.userId, byClientId.GetPlayerName(), model.stampId);
    }
  }

  public void SendChatStamp(int stamp_id) => this.packetSender.SendChatStamp(stamp_id);

  public void SetChatConnection(ChatCoopConnection chat_connection)
  {
    this.chatConnection = chat_connection;
  }

  public void OnRecvMoveField(Coop_Model_RoomMoveField model)
  {
    if (!QuestManager.IsValidInGameExplore())
      return;
    MonoBehaviourSingleton<QuestManager>.I.UpdatePortalUsedFlag(model.pid);
  }

  public void SnedMoveField(int usePortalId) => this.packetSender.SendMoveField(usePortalId);

  public void SendRushRequest() => this.packetSender.SendRushRequest();

  public void OnRecvRushRequest(int fromClientId, Coop_Model_RushRequest model)
  {
    if (!MonoBehaviourSingleton<CoopManager>.I.coopMyClient.isStageHost)
      return;
    this.packetSender.SendRushRequested(fromClientId, model.requestRushIndex);
  }

  public void OnRecvRushRequested(Coop_Model_RushRequested model)
  {
    MonoBehaviourSingleton<CoopManager>.I.coopStage.OnRecvRushRequested(model);
  }

  public bool NeedsForceLeave()
  {
    if (!MonoBehaviourSingleton<QuestManager>.IsValid() || MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestType() != QUEST_TYPE.GATE && MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestType() != QUEST_TYPE.DEFENSE || MonoBehaviourSingleton<InGameManager>.I.currentJoinType != CoopClient.CLIENT_JOIN_TYPE.FROM_QUEST_LIST)
      return false;
    bool foundFromField = false;
    bool foundJoinTypeNone = false;
    this.clients.ForEach((Action<CoopClient>) (c =>
    {
      if (c.isLeave)
        return;
      if (c.joinType == CoopClient.CLIENT_JOIN_TYPE.NONE)
      {
        foundJoinTypeNone = true;
      }
      else
      {
        if (c.joinType != CoopClient.CLIENT_JOIN_TYPE.FROM_FIELD || c.isBattleRetire)
          return;
        foundFromField = true;
      }
    }));
    return !foundJoinTypeNone && !foundFromField;
  }

  public void OnRecvSyncDefenseBattle(Coop_Model_RoomSyncDefenseBattle model)
  {
    if (!MonoBehaviourSingleton<QuestManager>.IsValid())
      return;
    MonoBehaviourSingleton<QuestManager>.I.UpdateTotalDamageToEndurance(model.endurance);
  }

  public enum ROOM_STATUS
  {
    NONE,
    STAGE,
    BATTLE,
  }
}
