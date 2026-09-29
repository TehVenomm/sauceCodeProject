// Decompiled with JetBrains decompiler
// Type: ExploreStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ExploreStatus
{
  private List<int> bossMapIdHistory = new List<int>();
  private List<ExploreStatus.TraceInfo> bossTraceMapIdHistory = new List<ExploreStatus.TraceInfo>();
  private List<ExplorePortalPoint> portals;
  private List<MissionCheckBase> missionCheck;
  private ExplorePlayerStatus[] playerStatuses = new ExplorePlayerStatus[8];
  private ExplorePlayerStatus selfPlayerStatus;

  public bool isHost { get; private set; }

  public PartyModel.ExploreInfo exploreInfo { get; private set; }

  public ExploreStatus.TraceInfo reservedTraceInfo { get; private set; }

  private FieldMapTable.PortalTableData lastUsePortal { get; set; }

  public ExploreBossStatus bossStatus { get; private set; }

  public bool isBossDead => this.bossStatus != null && this.bossStatus.isDead;

  public bool isEncountered { get; private set; }

  public float bossMoveRemainTime { get; private set; }

  public float hostDCRemainTime { get; private set; }

  public event System.Action onChangeExploreMemberList;

  public ExploreStatus(PartyModel.ExploreInfo exploreInfo, bool host)
  {
    this.exploreInfo = exploreInfo;
    this.isHost = host;
    this.portals = this.InitPortalPoint(exploreInfo.mapIds);
    this.hostDCRemainTime = 30f;
  }

  public void UpdateBossMap()
  {
    int count = this.GetEnabledPlayerStatusList().Count;
    for (int statusIndex = 0; statusIndex < count; ++statusIndex)
    {
      if (MonoBehaviourSingleton<QuestManager>.I.GetExploreMapId(statusIndex) == this.exploreInfo.mapIds[this.exploreInfo.mapIds.Count - 1])
        return;
    }
    int currentBossMapId = this.GetCurrentBossMapId();
    if (currentBossMapId < 0)
    {
      int index = Random.Range(0, this.exploreInfo.mapIds.Count - 1);
      int mapId = this.exploreInfo.mapIds[index];
      if (index == 0 && this.exploreInfo.mapIds.Count > 1)
        this.UpdateBossMap();
      else
        this.bossMapIdHistory.Add(mapId);
    }
    else
    {
      List<FieldMapTable.PortalTableData> portalListByMapId = Singleton<FieldMapTable>.I.GetPortalListByMapID((uint) currentBossMapId);
      if (portalListByMapId == null)
        return;
      List<int> intList = new List<int>();
      for (int index = 0; index < portalListByMapId.Count; ++index)
      {
        if (portalListByMapId[index].banEnemy != 1U)
        {
          int dstMapId = (int) portalListByMapId[index].dstMapID;
          if (dstMapId != this.exploreInfo.mapIds[0])
            intList.Add(dstMapId);
        }
      }
      if (intList.Count <= 0)
        return;
      int index1 = Random.Range(0, intList.Count);
      int num = intList[index1];
      if (this.GetBeforeBossMapId() == num && intList.Count > 1)
        this.UpdateBossMap();
      else
        this.bossMapIdHistory.Add(num);
    }
  }

  public int GetCurrentBossMapId()
  {
    return this.bossMapIdHistory == null || this.bossMapIdHistory.Count < 1 ? -1 : this.bossMapIdHistory[this.bossMapIdHistory.Count - 1];
  }

  private int GetBeforeBossMapId()
  {
    return this.bossMapIdHistory == null || this.bossMapIdHistory.Count < 2 ? -1 : this.bossMapIdHistory[this.bossMapIdHistory.Count - 2];
  }

  public EXPLORE_HISTORY_TYPE GetHistoryTypeOfMap(int mapId)
  {
    if (this.bossMapIdHistory == null)
      return EXPLORE_HISTORY_TYPE.NONE;
    int? nullable1 = new int?();
    for (int index = 0; index < this.bossMapIdHistory.Count; ++index)
    {
      if (this.bossMapIdHistory[index] == mapId)
        nullable1 = new int?(index);
    }
    if (!nullable1.HasValue)
      return EXPLORE_HISTORY_TYPE.NONE;
    int count = this.bossMapIdHistory.Count;
    int? nullable2 = nullable1;
    int num1 = count - 1;
    if (nullable2.GetValueOrDefault() == num1 & nullable2.HasValue)
      return EXPLORE_HISTORY_TYPE.CURRENT;
    int? nullable3 = nullable1;
    int num2 = count - 2;
    if (nullable3.GetValueOrDefault() == num2 & nullable3.HasValue)
      return EXPLORE_HISTORY_TYPE.LAST;
    nullable3 = nullable1;
    int num3 = count - 3;
    return nullable3.GetValueOrDefault() == num3 & nullable3.HasValue ? EXPLORE_HISTORY_TYPE.SECOND_LAST : EXPLORE_HISTORY_TYPE.NONE;
  }

  public bool IsBossAppearMap(int mapId) => this.GetCurrentBossMapId() == mapId;

  public void SyncBoss(Coop_Model_RoomSyncExploreBoss boss)
  {
    if (this.bossMapIdHistory.Count == 0)
      this.bossMapIdHistory.Add(boss.mId);
    else if (this.bossMapIdHistory[this.bossMapIdHistory.Count - 1] != boss.mId)
      this.bossMapIdHistory.Add(boss.mId);
    if (boss.hp < 0)
      return;
    if (this.bossStatus == null)
      this.bossStatus = new ExploreBossStatus();
    this.bossStatus.UpdateStatus(boss);
  }

  public void SyncBossMap(Coop_Model_RoomSyncExploreBossMap boss)
  {
    if (this.bossMapIdHistory.Count == 0)
    {
      this.bossMapIdHistory.Add(boss.mId);
    }
    else
    {
      if (this.bossMapIdHistory[this.bossMapIdHistory.Count - 1] == boss.mId)
        return;
      this.bossMapIdHistory.Add(boss.mId);
    }
  }

  public void SetBossDead(Coop_Model_RoomExploreBossDead model)
  {
    if (this.bossStatus == null)
      this.bossStatus = new ExploreBossStatus();
    this.bossStatus.UpdateStatus(model);
  }

  public void SyncPortalPoint(Coop_Model_RoomSyncAllPortalPoint model)
  {
    for (int index = 0; index < model.ps.Count; ++index)
    {
      Coop_Model_RoomSyncAllPortalPoint.PortalData p = model.ps[index];
      ExplorePortalPoint portalData1 = this.GetPortalData(p.id);
      if (portalData1 != null)
      {
        portalData1.UpdatePoint(p.pt);
        portalData1.UpdateUsedFlag(p.u);
        ExplorePortalPoint portalData2 = this.GetPortalData(portalData1.linkPortalId);
        if (portalData2 != null)
        {
          portalData2.UpdatePoint(p.pt);
          portalData2.UpdateUsedFlag(p.u);
        }
      }
    }
  }

  public void UpdateBossTraceMapIdHistory(
    int mapId,
    int lastCount,
    string playerName,
    bool reserve)
  {
    EXPLORE_HISTORY_TYPE type = EXPLORE_HISTORY_TYPE.NONE;
    switch (lastCount)
    {
      case 0:
        type = EXPLORE_HISTORY_TYPE.LAST;
        break;
      case 1:
        type = EXPLORE_HISTORY_TYPE.SECOND_LAST;
        break;
    }
    ExploreStatus.TraceInfo traceInfo = new ExploreStatus.TraceInfo(mapId, type, playerName);
    this.bossTraceMapIdHistory.Add(traceInfo);
    if (!reserve)
      return;
    this.reservedTraceInfo = traceInfo;
  }

  public ExploreStatus.TraceInfo[] GetTraceInfoHistory() => this.bossTraceMapIdHistory.ToArray();

  public void CompleteShowedTrace() => this.reservedTraceInfo = (ExploreStatus.TraceInfo) null;

  public void UpdatePortalPoint(int portalId, int point, bool force = false)
  {
    ExplorePortalPoint portalData = this.GetPortalData(portalId);
    if (portalData == null)
      return;
    portalData.UpdatePoint(point, force);
    this.GetPortalData(portalData.linkPortalId)?.UpdatePoint(point, force);
  }

  public void UpdatePortalUsedFlag(int portalId)
  {
    ExplorePortalPoint portalData = this.GetPortalData(portalId);
    if (portalData == null)
      return;
    portalData.UpdateUsedFlag(ExplorePortalPoint.USEDFLAG_PASSED);
    this.GetPortalData(portalData.linkPortalId)?.UpdateUsedFlag(ExplorePortalPoint.USEDFLAG_PASSED);
  }

  public ExplorePortalPoint GetPortalData(int portalId)
  {
    return this.portals.Find((Predicate<ExplorePortalPoint>) (x => x.portaiId == portalId));
  }

  public List<ExplorePortalPoint> GetAllPortalData() => this.portals;

  public List<ExplorePortalPoint> GetPortalDataFromMapId(int mapId)
  {
    return this.portals.FindAll((Predicate<ExplorePortalPoint>) (o => (int) o.portalData.dstMapID == mapId));
  }

  public List<ExplorePortalPoint> GetPortalDataFromSrcMapId(int mapId)
  {
    return this.portals.FindAll((Predicate<ExplorePortalPoint>) (o => (int) o.portalData.srcMapID == mapId));
  }

  public uint GetLastPortalId() => this.lastUsePortal != null ? this.lastUsePortal.portalID : 0U;

  public void UpdateLastPortal(FieldMapTable.PortalTableData portal)
  {
    this.lastUsePortal = portal;
    this.UpdatePortalUsedFlag((int) portal.portalID);
  }

  public ExplorePlayerStatus GetMyPlayerStatus() => this.selfPlayerStatus;

  private ExplorePlayerStatus GetPlayerStatus(CoopClient coopClient)
  {
    return Object.op_Implicit((Object) coopClient) ? this.GetPlayerStatus(coopClient.userId) : (ExplorePlayerStatus) null;
  }

  public ExplorePlayerStatus GetPlayerStatus(int userId)
  {
    for (int index = 0; index < 8; ++index)
    {
      ExplorePlayerStatus playerStatuse = this.playerStatuses[index];
      if (playerStatuse != null && playerStatuse.userId == userId)
        return playerStatuse;
    }
    return (ExplorePlayerStatus) null;
  }

  public void RemovePlayerStatus(CoopClient coopClient)
  {
    for (int index = 0; index < 8; ++index)
    {
      ExplorePlayerStatus playerStatuse = this.playerStatuses[index];
      if (playerStatuse != null && playerStatuse.userId == coopClient.userId)
      {
        this.playerStatuses[index] = (ExplorePlayerStatus) null;
        if (this.onChangeExploreMemberList == null)
          break;
        this.onChangeExploreMemberList();
        break;
      }
    }
  }

  public void ActivatePlayerStatus(CoopClient coopClient)
  {
    ExplorePlayerStatus explorePlayerStatus = this.GetPlayerStatus(coopClient);
    if (explorePlayerStatus == null)
    {
      bool isSelf = coopClient is CoopMyClient;
      explorePlayerStatus = new ExplorePlayerStatus(coopClient.userInfo, isSelf);
      this.playerStatuses[coopClient.slotIndex] = explorePlayerStatus;
      if (isSelf)
        this.selfPlayerStatus = explorePlayerStatus;
    }
    explorePlayerStatus.Activate(coopClient);
    if (this.onChangeExploreMemberList == null)
      return;
    this.onChangeExploreMemberList();
  }

  public void UpdatePlayerStatus(CoopClient coopClient, Coop_Model_RoomSyncPlayerStatus status)
  {
    this.GetPlayerStatus(coopClient)?.Sync(status);
  }

  public void UpdatePlayerStatus(CoopClient coopClient)
  {
    ExplorePlayerStatus playerStatus = this.GetPlayerStatus(coopClient);
    Player player = coopClient.GetPlayer();
    if (playerStatus == null || !Object.op_Implicit((Object) player))
      return;
    playerStatus.SyncFromPlayer(player);
  }

  public void UpdateTotalDamageToBoss(CoopClient coopClient, int total)
  {
    this.UpdateTotalDamageToBoss(coopClient.userId, total);
  }

  public void UpdateTotalDamageToBoss(int userId, int total)
  {
    this.GetPlayerStatus(userId)?.SyncTotalDamageToBoss(total);
  }

  public void UpdateBossMoveRemainTime(float time) => this.bossMoveRemainTime = time;

  public void UpdateHostDCRemainTime(float time) => this.hostDCRemainTime = time;

  public List<ExplorePlayerStatus> GetEnabledPlayerStatusList()
  {
    List<ExplorePlayerStatus> playerStatusList = new List<ExplorePlayerStatus>();
    foreach (ExplorePlayerStatus playerStatuse in this.playerStatuses)
    {
      if (playerStatuse != null)
        playerStatusList.Add(playerStatuse);
    }
    return playerStatusList;
  }

  public void SetEncountered(int mapId)
  {
    this.isEncountered = true;
    if (this.GetCurrentBossMapId() == mapId)
      return;
    this.bossMapIdHistory.Add(mapId);
  }

  public void ResetMemberEncountered() => this.isEncountered = false;

  public void UpdateBossStatus(Enemy boss)
  {
    if (this.bossStatus == null)
      this.bossStatus = new ExploreBossStatus();
    this.bossStatus.UpdateStatus(boss);
  }

  public void SetMissions(List<MissionCheckBase> missionCheck)
  {
    if (this.missionCheck != null)
      return;
    this.missionCheck = missionCheck;
  }

  public List<MissionCheckBase> GetMissions() => this.missionCheck;

  private List<ExplorePortalPoint> InitPortalPoint(List<int> mapIds)
  {
    List<ExplorePortalPoint> explorePortalPointList = new List<ExplorePortalPoint>();
    int index1 = 0;
    for (int index2 = mapIds.Count - 1; index1 < index2; ++index1)
    {
      List<FieldMapTable.PortalTableData> portalListByMapId = Singleton<FieldMapTable>.I.GetPortalListByMapID((uint) mapIds[index1]);
      int index3 = 0;
      for (int count = portalListByMapId.Count; index3 < count; ++index3)
      {
        ExplorePortalPoint explorePortalPoint = new ExplorePortalPoint(portalListByMapId[index3]);
        explorePortalPointList.Add(explorePortalPoint);
      }
    }
    return explorePortalPointList;
  }

  public void UpdatePassedPortal()
  {
    this.portals.ForEach((Action<ExplorePortalPoint>) (o =>
    {
      if (!o.passed)
        return;
      o.UpdateUsedFlag(ExplorePortalPoint.USEDFLAG_OPENED);
    }));
  }

  public class TraceInfo
  {
    public int mapId;
    public EXPLORE_HISTORY_TYPE historyType;
    public string playerName;

    public TraceInfo(int mapId, EXPLORE_HISTORY_TYPE type, string playerName)
    {
      this.mapId = mapId;
      this.historyType = type;
      this.playerName = playerName;
    }
  }
}
