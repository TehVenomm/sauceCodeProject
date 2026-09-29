// Decompiled with JetBrains decompiler
// Type: QuestManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

#nullable disable
public class QuestManager : MonoBehaviourSingleton<QuestManager>
{
  public bool needRequestOrderQuestList = true;
  private QuestStartData startData;
  private uint howToGetTargetQuestID;
  private PartyModel.ExploreInfo exploreInfo;
  private ExploreStatus explore;
  private float remainEndurance;
  private QuestManager.SelectQuestData current = new QuestManager.SelectQuestData();
  public uint currentDeliveryId;
  private int m_currentArenaId;
  private int m_currentSeriesArenaId;
  private bool firstSetGetClearStatus = true;
  public QuestCollection questCollection = new QuestCollection();
  public static string SPR_NAME_MISSION_CLEAR = "Quest_crownicon_clear";
  private const string DIFFICULTY_SPRITE_NAME_EASY = "Quest_classicon_beginner";
  private const string DIFFICULTY_SPRITE_NAME_NORMAL = "Quest_classicon_middle";
  private const string DIFFICULTY_SPRITE_NAME_HARD = "Quest_classicon_high";

  public static bool IsValidInGame()
  {
    return MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.startData != null;
  }

  public static bool IsValidInGameExplore()
  {
    return QuestManager.IsValidInGame() && MonoBehaviourSingleton<QuestManager>.I.IsExplore();
  }

  public static bool IsValidExplore()
  {
    return MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsExplore();
  }

  public static bool IsValidInGameArena()
  {
    return MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.startData != null && MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestType() == QUEST_TYPE.ARENA;
  }

  public static bool IsValidInGameDefenseBattle()
  {
    return QuestManager.IsValidInGame() && MonoBehaviourSingleton<QuestManager>.I.IsDefenseBattle();
  }

  public static bool IsValidInGameWaveMatch(bool isOnlyEvent = false)
  {
    return QuestManager.IsValidInGame() && MonoBehaviourSingleton<QuestManager>.I.IsWaveMatch(isOnlyEvent);
  }

  public static bool IsValidInGameSeries()
  {
    return QuestManager.IsValidInGame() && MonoBehaviourSingleton<QuestManager>.I.IsCurrentQuestTypeSeries();
  }

  public static bool IsValidInGameSeriesArena()
  {
    return QuestManager.IsValidInGame() && MonoBehaviourSingleton<QuestManager>.I.IsCurrentQuestTypeSeriesArena();
  }

  public static bool IsValidInGameTrial()
  {
    return QuestManager.IsValidInGame() && MonoBehaviourSingleton<QuestManager>.I.isTrial;
  }

  public static bool IsValidTrial()
  {
    return MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.isTrial;
  }

  public static bool IsValidInGameWaveStrategy()
  {
    if (!QuestManager.IsValidInGame())
      return false;
    return MonoBehaviourSingleton<QuestManager>.I.IsMatchQuestType(QUEST_TYPE.WAVE_STRATEGY) || MonoBehaviourSingleton<QuestManager>.I.IsMatchQuestType(QUEST_TYPE.EVENT_WAVE_STRATEGY);
  }

  public List<ClearStatusQuest> clearStatusQuest { get; private set; }

  public List<ClearStatusQuestEnemySpecies> clearStatusQuestEnemySpecies { get; private set; }

  public QuestResultUserCollection resultUserCollection { get; private set; }

  public List<QuestData> questList { get; private set; }

  public List<QuestData> orderQuestList { get; private set; }

  public List<QuestData> challengeList { get; private set; }

  public List<Network.EventData> eventList { get; private set; }

  public int carnivalEventId { get; private set; }

  public void SetEventList(List<Network.EventData> _eventList)
  {
    this.eventList = _eventList;
    int index = 0;
    for (int count = this.eventList.Count; index < count; ++index)
    {
      this.eventList[index].OnRecv();
      this.eventList[index].SetupEnum();
    }
  }

  public void SetCarnivalEventId(int carnivalEventId) => this.carnivalEventId = carnivalEventId;

  public Network.EventData _GetEventData(int eventId)
  {
    int index = 0;
    for (int count = this.eventList.Count; index < count; ++index)
    {
      if (this.eventList[index].eventId == eventId)
        return this.eventList[index];
    }
    return (Network.EventData) null;
  }

  public bool IsPlayableVersionEvent(int eventId)
  {
    Version nativeVersionFromName = NetworkNative.getNativeVersionFromName();
    return this.IsEventPlayableWith(eventId, nativeVersionFromName);
  }

  public bool IsEventPlayableWith(int eventId, Version version)
  {
    Network.EventData eventData = this._GetEventData(eventId);
    return eventData != null && eventData.IsPlayableWith(version);
  }

  public bool IsEventOpen(int eventId)
  {
    Network.EventData eventData = this._GetEventData(eventId);
    return eventData != null && eventData.GetRest() > 0;
  }

  public List<int> futureEventIdList { get; private set; }

  public void SetFutureEventList(List<int> _idList) => this.futureEventIdList = _idList;

  public bool IsFutureEvent(int eventId) => this.futureEventIdList.Contains(eventId);

  public List<Network.EventData> bingoEventList { get; private set; }

  public void SetBingoEventList(List<Network.EventData> _bingoEventList)
  {
    this.bingoEventList = _bingoEventList;
    int index = 0;
    for (int count = this.bingoEventList.Count; index < count; ++index)
      this.bingoEventList[index].OnRecv();
    if (!MonoBehaviourSingleton<SceneSettingsManager>.IsValid())
      return;
    MonoBehaviourSingleton<SceneSettingsManager>.I.SwitchBingoObjectsActivation(this.IsBingoPlayableEventExist());
  }

  private Network.EventData _GetBingoEventData(int eventId)
  {
    int index = 0;
    for (int count = this.bingoEventList.Count; index < count; ++index)
    {
      if (this.bingoEventList[index].eventId == eventId)
        return this.bingoEventList[index];
    }
    return (Network.EventData) null;
  }

  public bool IsBingoEventPlayableWith(int eventId, Version version)
  {
    Network.EventData bingoEventData = this._GetBingoEventData(eventId);
    return bingoEventData != null && bingoEventData.IsPlayableWith(version);
  }

  public bool IsBingoEventOpen(int eventId)
  {
    Network.EventData bingoEventData = this._GetBingoEventData(eventId);
    return bingoEventData != null && bingoEventData.GetRest() > 0;
  }

  public QuestCompleteData compData { get; private set; }

  public QuestRetireModel.Param retireData { get; private set; }

  public QuestArenaCompleteData arenaCompData { get; private set; }

  public float startTime { get; private set; }

  public List<int> missionNewClearFlag { get; private set; }

  public bool isTrial { get; private set; }

  public void StartTrial() => this.isTrial = true;

  public void ClearTrial() => this.isTrial = false;

  public bool IsHowToGetAutoEvent => this.howToGetTargetQuestID > 0U;

  public void StartHowToGetAutoEvent(uint id) => this.howToGetTargetQuestID = id;

  public void EndHowToGetAutoEvent() => this.howToGetTargetQuestID = 0U;

  public bool IsMatchQuestType(QUEST_TYPE questType)
  {
    return this.current.questData != null && this.current.questData.questType == questType;
  }

  public bool IsDefenseBattle()
  {
    if (this.current.questData == null)
      return false;
    return this.current.questData.questType == QUEST_TYPE.DEFENSE || this.current.questData.questStyle == QUEST_STYLE.DEFENSE;
  }

  public bool IsWaveMatch(bool isOnlyEvent = false)
  {
    if (this.current.questData == null)
      return false;
    return isOnlyEvent ? this.current.questData.questType == QUEST_TYPE.EVENT_WAVE || this.current.questData.questType == QUEST_TYPE.EVENT_WAVE_STRATEGY : this.current.questData.questType == QUEST_TYPE.WAVE || this.current.questData.questType == QUEST_TYPE.EVENT_WAVE || this.current.questData.questType == QUEST_TYPE.WAVE_STRATEGY || this.current.questData.questType == QUEST_TYPE.EVENT_WAVE_STRATEGY;
  }

  public bool IsWaveStrategyMatch(bool isOnlyEvent = false)
  {
    if (this.current.questData == null)
      return false;
    return !isOnlyEvent && this.current.questData.questType == QUEST_TYPE.WAVE_STRATEGY || this.current.questData.questType == QUEST_TYPE.EVENT_WAVE_STRATEGY;
  }

  public bool IsExplore() => this.explore != null;

  public int ExploreMapIdToIndex(uint mapId)
  {
    return this.exploreInfo != null ? this.exploreInfo.mapIds.IndexOf((int) mapId) : 0;
  }

  public int ExploreMapIndexToId(int index)
  {
    return this.exploreInfo != null && index < this.exploreInfo.mapIds.Count ? this.exploreInfo.mapIds[index] : 0;
  }

  public int GetExploreStartMapId()
  {
    return this.exploreInfo != null && this.exploreInfo.mapIds != null && this.exploreInfo.mapIds.Count > 0 ? this.exploreInfo.mapIds[0] : 0;
  }

  public int GetExploreBossBatlleMapId()
  {
    return this.exploreInfo != null ? this.exploreInfo.mapIds[this.exploreInfo.mapIds.Count - 1] : 0;
  }

  public int GetExploreBossAppearMapId()
  {
    return this.explore == null ? 0 : this.explore.GetCurrentBossMapId();
  }

  public EXPLORE_HISTORY_TYPE GetExploreHistoryType(int mapId)
  {
    return this.explore == null ? EXPLORE_HISTORY_TYPE.NONE : this.explore.GetHistoryTypeOfMap(mapId);
  }

  public float GetExploreBossMoveRemainTime()
  {
    return this.explore == null ? 0.0f : this.explore.bossMoveRemainTime;
  }

  public void UpdateExploreBossMoveRemainTime(float time)
  {
    if (this.explore == null)
      return;
    this.explore.UpdateBossMoveRemainTime(time);
  }

  public void UpdateBossTraceHistory(int mapId, int lastCount, string playerName, bool reserve)
  {
    if (this.explore == null)
      return;
    this.explore.UpdateBossTraceMapIdHistory(mapId, lastCount, playerName, reserve);
  }

  public ExploreStatus.TraceInfo[] GetBossTraceHistory()
  {
    return this.explore == null ? (ExploreStatus.TraceInfo[]) null : this.explore.GetTraceInfoHistory();
  }

  public ExploreStatus.TraceInfo GetReservedTraceInfo()
  {
    return this.explore == null ? (ExploreStatus.TraceInfo) null : this.explore.reservedTraceInfo;
  }

  public void CompleteBossTracePopup()
  {
    if (this.explore == null)
      return;
    this.explore.CompleteShowedTrace();
  }

  public float GetExploreHostDCRemainTime()
  {
    return this.explore == null ? 0.0f : this.explore.hostDCRemainTime;
  }

  public void UpdateExploreHostDCTime(float remainTime)
  {
    if (this.explore == null)
      return;
    this.explore.UpdateHostDCRemainTime(remainTime);
  }

  public string GetCurrentBossMapStageName()
  {
    if (this.explore == null)
      return (string) null;
    int currentBossMapId = this.explore.GetCurrentBossMapId();
    FieldMapTable.FieldMapTableData fieldMapData1 = Singleton<FieldMapTable>.I.GetFieldMapData((uint) currentBossMapId);
    if (fieldMapData1 != null)
    {
      if (!string.IsNullOrEmpty(fieldMapData1.happenStageName))
        return fieldMapData1.happenStageName;
      FieldMapTable.FieldMapTableData fieldMapData2 = Singleton<FieldMapTable>.I.GetFieldMapData((uint) this.GetExploreBossBatlleMapId());
      if (fieldMapData2 != null)
        return fieldMapData2.stageName;
    }
    return (string) null;
  }

  public ExploreBossStatus GetExploreBossStatus()
  {
    return this.explore != null ? this.explore.bossStatus : (ExploreBossStatus) null;
  }

  public void UpdateBossAppearMap()
  {
    if (this.explore == null)
      return;
    this.explore.UpdateBossMap();
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid() || (long) this.explore.GetCurrentBossMapId() != (long) MonoBehaviourSingleton<FieldManager>.I.currentMapID)
      return;
    MonoBehaviourSingleton<InGameProgress>.I.ExploreHappenQuestDirection();
  }

  public bool IsBossAppearMap(int mapId)
  {
    return this.explore != null && this.explore.IsBossAppearMap(mapId);
  }

  public bool IsExploreBossMap()
  {
    return this.exploreInfo != null && (long) this.exploreInfo.mapIds[this.exploreInfo.mapIds.Count - 1] == (long) MonoBehaviourSingleton<FieldManager>.I.currentMapID;
  }

  public void SyncExplorePortalPoint(Coop_Model_RoomSyncAllPortalPoint model)
  {
    if (this.explore == null)
      return;
    this.explore.SyncPortalPoint(model);
  }

  public void UpdatePortalUsedFlag(int portalId)
  {
    if (this.explore == null)
      return;
    this.explore.UpdatePortalUsedFlag(portalId);
  }

  public void UpdateExplorePortalPoint(int portalId, int point, int x, int y)
  {
    if (this.explore == null)
      return;
    this.UpdatePortalPointForExplore(portalId, point, x, y);
  }

  private void SetExplorePortalPoint(int portalId, int point)
  {
    if (this.explore == null)
      return;
    this.explore.UpdatePortalPoint(portalId, point);
  }

  private void RollbackExplorePortalPoint(int portalId, int point)
  {
    if (this.explore == null)
      return;
    this.explore.UpdatePortalPoint(portalId, point, true);
  }

  private void UpdatePortalPointForExplore(int portalId, int point, int x, int z)
  {
    ExplorePortalPoint portal = this.explore.GetPortalData(portalId);
    if (portal == null)
      return;
    int prevPoint = portal.point;
    if (point <= prevPoint)
      return;
    uint portalPoint = portal.portalData.portalPoint;
    if ((long) point > (long) portalPoint)
      point = (int) portalPoint;
    FieldMapPortalInfo portalInfo = MonoBehaviourSingleton<FieldManager>.I.GetPortalInfo((uint) portalId);
    if (portalInfo == null)
    {
      portalInfo = MonoBehaviourSingleton<FieldManager>.I.GetPortalInfo(Singleton<FieldMapTable>.I.GetPortalData((uint) portalId).linkPortalId);
      if (portalInfo != null)
      {
        x = Mathf.RoundToInt(portalInfo.portalData.srcX);
        z = Mathf.RoundToInt(portalInfo.portalData.srcZ);
      }
    }
    if (portalInfo != null)
      prevPoint = portalInfo.GetNowPortalPoint();
    if ((long) point >= (long) portalPoint)
    {
      FieldPortal fieldPortal = MonoBehaviourSingleton<WorldMapManager>.I.GetFieldPortal(portalId);
      if (fieldPortal == null || (long) fieldPortal.point < (long) portalPoint)
      {
        this.SetExplorePortalPoint(portalId, point);
        MonoBehaviourSingleton<FieldManager>.I.SendFieldQuestOpenPortal(portalId, (Action<bool, Error>) ((succeeded, error) =>
        {
          if (succeeded)
          {
            if (this.PlayPortalPointEffect(portalInfo, point - prevPoint, x, z) || !this.ShouldShowPortalOpenNotification())
              return;
            string str = "";
            FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(portal.portalData.dstMapID);
            if (fieldMapData != null)
              str = fieldMapData.mapName;
            UIInGamePopupDialog.PushOpen(StringTable.Format(STRING_CATEGORY.IN_GAME, 6002U, (object) str), false);
          }
          else
            this.RollbackExplorePortalPoint(portalId, prevPoint);
        }));
      }
      else
      {
        this.SetExplorePortalPoint(portalId, point);
        if (this.PlayPortalPointEffect(portalInfo, point - prevPoint, x, z) || !this.ShouldShowPortalOpenNotification())
          return;
        string str = "";
        FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(portal.portalData.dstMapID);
        if (fieldMapData != null)
          str = fieldMapData.mapName;
        UIInGamePopupDialog.PushOpen(StringTable.Format(STRING_CATEGORY.IN_GAME, 6002U, (object) str), false, 1.4f);
      }
    }
    else
    {
      this.SetExplorePortalPoint(portalId, point);
      this.PlayPortalPointEffect(portalInfo, point - prevPoint, x, z);
    }
  }

  private bool ShouldShowPortalOpenNotification()
  {
    return !MonoBehaviourSingleton<QuestManager>.I.IsExploreBossMap() && MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameProgress>.I.progressEndType == InGameProgress.PROGRESS_END_TYPE.NONE;
  }

  private bool PlayPortalPointEffect(
    FieldMapPortalInfo portalInfo,
    int getPoint,
    int portalX,
    int portalZ)
  {
    if (portalInfo == null)
      return false;
    if (MonoBehaviourSingleton<FieldManager>.I.AddPortalPointToPortalInfo(portalInfo, getPoint))
      MonoBehaviourSingleton<CoopManager>.I.coopStage.fieldRewardPool.SendFieldDrop();
    if (getPoint <= 0 || !MonoBehaviourSingleton<InGameProgress>.IsValid() || MonoBehaviourSingleton<InGameProgress>.I.portalObjectList == null)
      return false;
    MonoBehaviourSingleton<InGameProgress>.I.CreatePortalPoint(portalInfo, new Coop_Model_EnemyDefeat()
    {
      ppt = getPoint,
      x = portalX,
      z = portalZ
    });
    return true;
  }

  public bool PortalIsUsedInExplore(int portalId)
  {
    if (this.explore != null)
    {
      ExplorePortalPoint portalData = this.explore.GetPortalData(portalId);
      if (portalData != null)
        return ExplorePortalPoint.USEDFLAG_CLOSED != portalData.used;
    }
    return false;
  }

  public bool PortalIsPassedInExplore(int portalId)
  {
    if (this.explore != null)
    {
      ExplorePortalPoint portalData = this.explore.GetPortalData(portalId);
      if (portalData != null)
        return ExplorePortalPoint.USEDFLAG_PASSED == portalData.used;
    }
    return false;
  }

  public bool MapIsTraveldInExplore(int mapId)
  {
    if (this.explore == null)
      return false;
    if (this.GetExploreStartMapId() == mapId)
      return true;
    List<ExplorePortalPoint> portalDataFromMapId = this.explore.GetPortalDataFromMapId(mapId);
    bool traveld = false;
    Action<ExplorePortalPoint> action = (Action<ExplorePortalPoint>) (o => traveld = traveld || ExplorePortalPoint.USEDFLAG_CLOSED != o.used);
    portalDataFromMapId.ForEach(action);
    return traveld;
  }

  public bool TraveldAllPortalExplore(int mapId)
  {
    if (this.explore == null)
      return false;
    if (this.GetExploreStartMapId() == mapId)
      return true;
    List<ExplorePortalPoint> dataFromSrcMapId = this.explore.GetPortalDataFromSrcMapId(mapId);
    bool traveld = true;
    Action<ExplorePortalPoint> action = (Action<ExplorePortalPoint>) (o =>
    {
      if (ExplorePortalPoint.USEDFLAG_CLOSED != o.used)
        return;
      traveld = false;
    });
    dataFromSrcMapId.ForEach(action);
    return traveld;
  }

  public void UpdateLastPortalData(FieldMapTable.PortalTableData portalData)
  {
    if (this.explore == null)
      return;
    this.explore.UpdateLastPortal(portalData);
  }

  public uint GetLastPortalId() => this.explore != null ? this.explore.GetLastPortalId() : 0U;

  public void UpdateExploreBossStatus(Enemy enemy)
  {
    if (this.explore == null)
      return;
    this.explore.UpdateBossStatus(enemy);
  }

  public void SyncExploreBossStatus(Coop_Model_RoomSyncExploreBoss model)
  {
    if (this.explore == null)
      return;
    this.explore.SyncBoss(model);
    this.explore.ResetMemberEncountered();
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid() || (long) this.explore.GetCurrentBossMapId() != (long) MonoBehaviourSingleton<FieldManager>.I.currentMapID)
      return;
    MonoBehaviourSingleton<InGameProgress>.I.ExploreHappenQuestDirection();
  }

  public void SyncExploreBossMap(Coop_Model_RoomSyncExploreBossMap model)
  {
    if (this.explore == null)
      return;
    this.explore.SyncBossMap(model);
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid() || (long) this.explore.GetCurrentBossMapId() != (long) MonoBehaviourSingleton<FieldManager>.I.currentMapID)
      return;
    MonoBehaviourSingleton<InGameProgress>.I.ExploreHappenQuestDirection();
  }

  public void SetExploreBossDead(Coop_Model_RoomExploreBossDead model)
  {
    if (this.explore == null)
      return;
    this.explore.SetBossDead(model);
  }

  public bool IsExploreBossDead() => this.explore != null && this.explore.isBossDead;

  public List<int> GetExploreBossBreakIdList()
  {
    return this.explore != null ? this.explore.bossStatus.GetBreakIds() : new List<int>();
  }

  public ExplorePlayerStatus GetMyExplorePlayerStatus()
  {
    return this.explore != null ? this.explore.GetMyPlayerStatus() : (ExplorePlayerStatus) null;
  }

  public void ActivateExplorePlayerStatus(CoopClient coopClient)
  {
    if (this.explore == null)
      return;
    this.explore.ActivatePlayerStatus(coopClient);
  }

  public void RemoveExplorePlayerStatus(CoopClient coopClient)
  {
    if (this.explore == null)
      return;
    this.explore.RemovePlayerStatus(coopClient);
  }

  public void UpdateExplorePlayerStatus(
    CoopClient coopClient,
    Coop_Model_RoomSyncPlayerStatus status)
  {
    if (this.explore == null)
      return;
    this.explore.UpdatePlayerStatus(coopClient, status);
  }

  public void UpdateExplorePlayerStatus(CoopClient coopClient)
  {
    if (this.explore == null)
      return;
    this.explore.UpdatePlayerStatus(coopClient);
  }

  public void UpdateExploreTotalDamageToBoss(CoopClient coopClient, int total)
  {
    if (this.explore == null)
      return;
    this.explore.UpdateTotalDamageToBoss(coopClient, total);
  }

  public void UpdateExploreTotalDamageToBoss(int userId, int total)
  {
    if (this.explore == null)
      return;
    this.explore.UpdateTotalDamageToBoss(userId, total);
  }

  public List<ExplorePlayerStatus> GetExplorePlayerStatusList()
  {
    return this.explore != null ? this.explore.GetEnabledPlayerStatusList() : (List<ExplorePlayerStatus>) null;
  }

  public ExplorePlayerStatus GetExplorePlayerStatus(int userId)
  {
    return this.explore != null ? this.explore.GetPlayerStatus(userId) : (ExplorePlayerStatus) null;
  }

  public bool IsEncountered() => this.explore != null && this.explore.isEncountered;

  public void SetMemberEncounteredMap(int mapId)
  {
    if (this.explore == null)
      return;
    this.explore.SetEncountered(mapId);
  }

  public void ResetMemberEncountered()
  {
    if (this.explore != null)
      return;
    this.explore.ResetMemberEncountered();
  }

  public ExploreStatus GetExploreStatus() => this.explore;

  public void SetExploreInfo(PartyModel.ExploreInfo exploreInfo) => this.exploreInfo = exploreInfo;

  public void SetExploreStatus(ExploreStatus explore) => this.explore = explore;

  public int[] GetExploreDisplayIndices()
  {
    int[] exploreDisplayIndices = new int[4]{ 0, 1, 2, 3 };
    if (this.explore == null)
      return exploreDisplayIndices;
    int id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    bool flag = true;
    List<ExplorePlayerStatus> playerStatusList = this.explore.GetEnabledPlayerStatusList();
    for (int index = 0; index < playerStatusList.Count; ++index)
    {
      ExplorePlayerStatus explorePlayerStatus = playerStatusList[index];
      CoopClient coopClient = explorePlayerStatus.coopClient;
      if (!Object.op_Equality((Object) null, (Object) coopClient))
      {
        int num = coopClient.slotIndex;
        if (flag)
          ++num;
        if (id == explorePlayerStatus.userId)
        {
          num = 0;
          flag = false;
        }
        if (0 <= num && exploreDisplayIndices.Length > num)
          exploreDisplayIndices[index] = num;
      }
    }
    return exploreDisplayIndices;
  }

  public int GetExploreMapId(int statusIndex)
  {
    if (this.explore == null)
      return -1;
    List<ExplorePlayerStatus> playerStatusList = this.explore.GetEnabledPlayerStatusList();
    if (playerStatusList.Count <= statusIndex)
      return -1;
    CoopClient coopClient = playerStatusList[statusIndex].coopClient;
    return Object.op_Equality((Object) null, (Object) coopClient) ? -1 : this.ExploreMapIndexToId(coopClient.exploreMapIndex);
  }

  public void UpdatePassedPortal()
  {
    if (this.explore == null)
      return;
    this.explore.UpdatePassedPortal();
  }

  public uint GetBossMapId()
  {
    return this.explore == null ? 0U : (uint) this.explore.GetCurrentBossMapId();
  }

  public float GetRemainEndurance() => this.remainEndurance;

  public void UpdateTotalDamageToEndurance(float endurance)
  {
    this.remainEndurance = endurance;
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
      return;
    MonoBehaviourSingleton<InGameProgress>.I.SetDefenseBattleEndurance(endurance);
  }

  private QuestManager()
  {
    this.clearStatusQuest = new List<ClearStatusQuest>();
    this.clearStatusQuestEnemySpecies = new List<ClearStatusQuestEnemySpecies>();
    this.resultUserCollection = new QuestResultUserCollection();
    this.isBackGachaQuest = false;
  }

  public void ClearPlayData()
  {
    this.startData = (QuestStartData) null;
    this.compData = (QuestCompleteData) null;
    this.retireData = (QuestRetireModel.Param) null;
    this.startTime = 0.0f;
    this.missionNewClearFlag = (List<int>) null;
    this.resultUserCollection.Clear();
    this.explore = (ExploreStatus) null;
    this.exploreInfo = (PartyModel.ExploreInfo) null;
    this.arenaCompData = (QuestArenaCompleteData) null;
    this.remainEndurance = 0.0f;
    this.isTrial = false;
  }

  public void SaveLastNewClearQuest(int status)
  {
    if (status >= 3 || GameSaveData.instance.lastNewClearQusetID == (int) this.currentQuestID && GameSaveData.instance.lastQusetID == (int) this.currentQuestID)
      return;
    GameSaveData.instance.lastNewClearQusetID = (int) this.currentQuestID;
    GameSaveData.instance.lastQusetID = (int) this.currentQuestID;
    GameSaveData.Save();
  }

  public void SaveLastQuestId(int quest_id)
  {
    if (GameSaveData.instance.lastQusetID == quest_id)
      return;
    GameSaveData.instance.lastQusetID = quest_id;
    GameSaveData.Save();
  }

  public int currentArenaId => this.m_currentArenaId;

  public int currentSeriesArenaId => this.m_currentSeriesArenaId;

  public uint currentQuestID => this.current.questId;

  public uint currentQuestSeriesIndex => this.current.seriesIndex;

  public bool currentQuestIsFreeJoin => this.current.isFreeJoin;

  public QuestTable.QuestTableData currentQuestData => this.current.questData;

  public bool IsCurrentQuestTypeSeries()
  {
    return this.current.questData != null && this.current.questData.questType == QUEST_TYPE.SERIES;
  }

  public bool IsCurrentQuestTypeSeriesArena()
  {
    return this.current.questData != null && this.current.questData.questType == QUEST_TYPE.SERIES_ARENA;
  }

  public void SetCurrentQuestID(uint quest_id, bool is_free_join = true)
  {
    this.SaveLastQuestId((int) quest_id);
    this.current.isFreeJoin = is_free_join;
    this.current.questId = quest_id;
    this.current.questData = quest_id <= 0U ? (QuestTable.QuestTableData) null : Singleton<QuestTable>.I.GetQuestData(quest_id);
    this.current.limitTime = this.current.questData != null ? this.current.questData.limitTime : 0.0f;
    this.current.seriesIndex = 0U;
  }

  public void SetCurrentQuestSeriesIndex(uint index) => this.current.seriesIndex = index;

  public void SetCurrentQuestLimitTime(float time) => this.current.limitTime = time;

  public void SetCurrentArenaId(int arenaId) => this.m_currentArenaId = arenaId;

  public void SetCurrentSeriesArenaId(int seriesArenaId)
  {
    this.m_currentSeriesArenaId = seriesArenaId;
  }

  public int GetCurrentQuestId() => this.current.questData == null ? 0 : (int) this.current.questId;

  public string GetCurrentQuestName()
  {
    return this.current.questData == null ? string.Empty : this.current.questData.questText;
  }

  public QUEST_TYPE GetCurrentQuestType()
  {
    return this.current.questData == null ? QUEST_TYPE.NORMAL : this.current.questData.questType;
  }

  public QUEST_STYLE GetCurrentQuestStyle()
  {
    return this.current.questData == null ? QUEST_STYLE.NORMAL : this.current.questData.questStyle;
  }

  public uint GetCurrentMapId()
  {
    return this.current.questData == null ? 0U : this.current.questData.mapId;
  }

  public string GetCurrentQuestStageName()
  {
    return this.current.questData == null ? string.Empty : this.current.questData.stageName[(int) this.current.seriesIndex];
  }

  public int GetCurrentQuestEnemyID()
  {
    return this.current.questData == null ? 0 : this.current.questData.enemyID[(int) this.current.seriesIndex];
  }

  public int GetCurrentQuestEnemyID(int index)
  {
    return this.current.questData == null || index >= this.current.questData.enemyID.Length ? 0 : this.current.questData.enemyID[index];
  }

  public int GetCurrentQuestEnemyLv()
  {
    if (this.current.questData == null)
      return 0;
    int level = this.current.questData.enemyLv[(int) this.current.seriesIndex];
    if (level == 0 && Singleton<EnemyTable>.IsValid())
    {
      EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) this.GetCurrentQuestEnemyID());
      if (enemyData != null)
        level = (int) enemyData.level;
    }
    return level;
  }

  public int GetCurrentQuestEnemyLv(int index)
  {
    return this.current.questData == null || index >= this.current.questData.enemyLv.Length ? 0 : this.current.questData.enemyLv[index];
  }

  public int GetCurrentQuestBGMID()
  {
    return this.current.questData == null ? 0 : this.current.questData.bgmID[(int) this.current.seriesIndex];
  }

  public float GetCurrentQuestLimitTime()
  {
    return this.current.questData == null ? 0.0f : this.current.limitTime;
  }

  public int GetCurrentQuestSeriesNum()
  {
    return this.current.questData == null ? 0 : this.current.questData.seriesNum;
  }

  public bool IsLastEnemyCurrentQuestSeries()
  {
    return this.current.questData != null && (this.IsCurrentQuestTypeSeries() || this.IsCurrentQuestTypeSeriesArena()) && (long) this.current.seriesIndex >= (long) (this.current.questData.seriesNum - 1);
  }

  public bool IsOverCurrentQuestSeries()
  {
    return this.current.questData != null && this.GetCurrentQuestSeriesNum() > 1 && (long) this.current.seriesIndex >= (long) this.current.questData.seriesNum;
  }

  public int GetCurrentQuestMaxTeamMemberNum()
  {
    return this.current.questData == null ? 4 : this.current.questData.userNumLimit;
  }

  public QuestStartData.EnemyReward GetCurrentQuestEnemyReward()
  {
    return this.startData == null ? (QuestStartData.EnemyReward) null : this.startData.enemy[(int) this.current.seriesIndex];
  }

  public CLEAR_STATUS GetClearStatusQuest(uint questId)
  {
    CLEAR_STATUS clearStatusQuest1 = CLEAR_STATUS.NEW;
    ClearStatusQuest clearStatusQuest2 = this.clearStatusQuest.Find((Predicate<ClearStatusQuest>) (data => (long) data.questId == (long) questId));
    if (clearStatusQuest2 != null)
      clearStatusQuest1 = (CLEAR_STATUS) clearStatusQuest2.questStatus;
    return clearStatusQuest1;
  }

  public bool IsClearQuest(uint questId)
  {
    switch (this.GetClearStatusQuest(questId))
    {
      case CLEAR_STATUS.CLEAR:
      case CLEAR_STATUS.ALL_CLEAR:
        return true;
      default:
        return false;
    }
  }

  public bool IsOpenedQuest(uint questId)
  {
    QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(questId);
    return questData != null && (questData.appearQuestId <= 0U || this.IsClearQuest(questData.appearQuestId)) && (questData.appearDeliveryId <= 0U || MonoBehaviourSingleton<DeliveryManager>.I.IsClearDelivery(questData.appearDeliveryId));
  }

  public bool IsTutorialCurrentQuest()
  {
    return QuestManager.IsValidInGame() && MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.IsValid() && !Object.op_Equality((Object) MonoBehaviourSingleton<CoopManager>.I.coopMyClient, (Object) null) && this.current.questData != null && MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialQuestId == (int) this.current.questData.questID && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA_QUEST_WIN) && MonoBehaviourSingleton<CoopManager>.I.coopMyClient.isPartyOwner;
  }

  public bool IsTutorialOrderQuest(uint questId)
  {
    return MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA1) && MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialQuestId == (int) questId && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA_QUEST_END);
  }

  public bool IsTutorialOrderShadowQuest()
  {
    return MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.FORGE_ITEM) && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.SHADOW_QUEST_END);
  }

  public ClearStatusQuestEnemySpecies GetClearStatusQuestEnemySpecies(uint questId)
  {
    QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(questId);
    if (questData == null)
      return (ClearStatusQuestEnemySpecies) null;
    EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) questData.GetMainEnemyID());
    return enemyData == null ? (ClearStatusQuestEnemySpecies) null : this.clearStatusQuestEnemySpecies.Find((Predicate<ClearStatusQuestEnemySpecies>) (data => data.enemySpecies == enemyData.enemySpecies));
  }

  public QuestInfoData[] GetQuestInfoData()
  {
    if (this.questList == null || this.questList.Count == 0)
      return (QuestInfoData[]) null;
    List<QuestInfoData> open_quest_ary = new List<QuestInfoData>();
    this.questList.ForEach((Action<QuestData>) (quest =>
    {
      QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint) quest.questId);
      if (questData == null || questData.questType == QUEST_TYPE.STORY)
        return;
      ClearStatusQuest clearStatusQuest = this.clearStatusQuest.Find((Predicate<ClearStatusQuest>) (data => data.questId == quest.questId));
      if (clearStatusQuest == null)
      {
        open_quest_ary.Add(new QuestInfoData(questData, quest, (int[]) null));
      }
      else
      {
        if (clearStatusQuest.questStatus <= 0)
          return;
        open_quest_ary.Add(new QuestInfoData(questData, quest, clearStatusQuest.missionStatus.ToArray()));
      }
    }));
    return open_quest_ary.Count == 0 ? (QuestInfoData[]) null : open_quest_ary.ToArray();
  }

  public QuestInfoData[] GetOrderQuestInfoData()
  {
    if (this.orderQuestList == null)
      return (QuestInfoData[]) null;
    List<QuestInfoData> open_quest_ary = new List<QuestInfoData>();
    this.orderQuestList.ForEach((Action<QuestData>) (order =>
    {
      QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint) order.questId);
      if (questData == null)
        return;
      ClearStatusQuest clearStatusQuest = this.clearStatusQuest.Find((Predicate<ClearStatusQuest>) (data => data.questId == order.questId));
      if (clearStatusQuest == null)
      {
        open_quest_ary.Add(new QuestInfoData(questData, order, (int[]) null));
      }
      else
      {
        if (clearStatusQuest.questStatus <= 0)
          return;
        open_quest_ary.Add(new QuestInfoData(questData, order, clearStatusQuest.missionStatus.ToArray()));
      }
    }));
    return open_quest_ary.Count == 0 ? (QuestInfoData[]) null : open_quest_ary.ToArray();
  }

  public QuestInfoData GetQuestInfoData(uint quest_id)
  {
    QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(quest_id);
    if (questData == null)
      return (QuestInfoData) null;
    List<QuestData> questDataList = questData.questType != QUEST_TYPE.ORDER ? this.questList : this.orderQuestList;
    if (questDataList == null)
      return (QuestInfoData) null;
    QuestData quest_list = questDataList.Find((Predicate<QuestData>) (q => (long) q.questId == (long) quest_id));
    if (quest_list == null)
      return (QuestInfoData) null;
    ClearStatusQuest clearStatusQuest = this.clearStatusQuest.Find((Predicate<ClearStatusQuest>) (data => (long) data.questId == (long) quest_id));
    return clearStatusQuest != null && clearStatusQuest.questStatus > 0 ? new QuestInfoData(questData, quest_list, clearStatusQuest.missionStatus.ToArray()) : new QuestInfoData(questData, quest_list, (int[]) null);
  }

  public QuestInfoData GetQuestChallengeInfoData(uint quest_id)
  {
    List<QuestData> challengeList = this.challengeList;
    if (challengeList == null)
      return (QuestInfoData) null;
    QuestTable.QuestTableData questData1 = Singleton<QuestTable>.I.GetQuestData(quest_id);
    if (questData1 == null)
      return (QuestInfoData) null;
    QuestData questData2 = challengeList.Find((Predicate<QuestData>) (q => (long) q.questId == (long) quest_id));
    return questData2 == null ? (QuestInfoData) null : this.CreateQuestChallengeInfoData(questData2, questData1);
  }

  public QuestInfoData CreateQuestChallengeInfoData(
    QuestData questData,
    QuestTable.QuestTableData tableData)
  {
    int questId = (int) tableData.questID;
    if (questId != questData.questId)
      Debug.LogWarning((object) $"Network.QuestDataとQuestTableDataのクエストIDが一致していません questData = {(object) questData.questId} tableData = {(object) questId}");
    ClearStatusQuest clearStatusQuest = this.clearStatusQuest.Find((Predicate<ClearStatusQuest>) (data => data.questId == questId));
    return clearStatusQuest != null && clearStatusQuest.questStatus > 0 ? new QuestInfoData(tableData, questData, clearStatusQuest.missionStatus.ToArray()) : new QuestInfoData(tableData, questData, (int[]) null);
  }

  public QuestInfoData GetExploreQuestInfo(uint quest_id)
  {
    QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(quest_id);
    if (questData == null)
    {
      Log.Error("QuestTableDataがありません");
      return (QuestInfoData) null;
    }
    QuestData quest_list = new QuestData()
    {
      questId = (int) quest_id,
      crystalNum = 0,
      order = (QuestData.OrderQuestInfo) null
    };
    if (quest_list != null)
      return new QuestInfoData(questData, quest_list, (int[]) null);
    Log.Error("対象のEventQuestDataがありません");
    return (QuestInfoData) null;
  }

  public ClearStatusQuest GetClearStatusQuestData(uint quest_id)
  {
    return Singleton<QuestTable>.I.GetQuestData(quest_id) == null ? (ClearStatusQuest) null : this.clearStatusQuest.Find((Predicate<ClearStatusQuest>) (data => (long) data.questId == (long) quest_id));
  }

  public bool ExistsExploreEvent()
  {
    for (int index = 0; index < this.eventList.Count; ++index)
    {
      if (this.eventList[index].eventType == 4)
        return true;
    }
    return false;
  }

  public int[] GetExploreEventIds()
  {
    List<int> intList = new List<int>(this.eventList.Count);
    for (int index = 0; index < this.eventList.Count; ++index)
    {
      if (this.eventList[index].eventType == 4)
        intList.Add(this.eventList[index].eventId);
    }
    return intList.ToArray();
  }

  public bool IsValidStartingStory(uint quest_id) => false;

  private void SendGetQuestList(QuestListModel.RequestSendForm send, Action<bool> call_back)
  {
    Protocol.Send<QuestListModel.RequestSendForm, QuestListModel>(QuestListModel.URL, send, (Action<QuestListModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        if (send.req_q > 0)
          this.questList = ret.result.quests;
        if (send.req_gq > 0)
          this.orderQuestList = ret.result.order;
        int reqEq = send.req_eq;
        if (send.req_e > 0)
        {
          this.SetEventList(ret.result.events);
          this.SetCarnivalEventId(ret.result.carnivalEventId);
          this.SetFutureEventList(ret.result.futureEventIds);
        }
        if (send.req_d > 0 && MonoBehaviourSingleton<DeliveryManager>.IsValid())
          MonoBehaviourSingleton<DeliveryManager>.I.UpdateDeliveryReaminTime(ret.result.dailyRemainTime, ret.result.weeklyRemainTime);
        if (send.req_bingo > 0)
          this.SetBingoEventList(ret.result.bingoEvents);
      }
      call_back(flag);
    }));
  }

  public void SendGetQuestList(Action<bool> call_back)
  {
    this.SendGetQuestList(new QuestListModel.RequestSendForm()
    {
      req_gq = 1
    }, call_back);
  }

  public void SendGetDeliveryList(Action<bool> call_back)
  {
    this.SendGetQuestList(new QuestListModel.RequestSendForm()
    {
      req_d = 1
    }, call_back);
  }

  public void SendGetEventList(Action<bool> call_back)
  {
    this.SendGetQuestList(new QuestListModel.RequestSendForm()
    {
      req_d = 1,
      req_e = 1
    }, call_back);
  }

  public void SendGetBingoEventList(Action<bool> call_back)
  {
    this.SendGetQuestList(new QuestListModel.RequestSendForm()
    {
      req_d = 1,
      req_bingo = 1
    }, call_back);
  }

  public Network.EventData FindArenaDataFromList()
  {
    int index = 0;
    for (int count = MonoBehaviourSingleton<QuestManager>.I.eventList.Count; index < count; ++index)
    {
      Network.EventData arenaDataFromList = MonoBehaviourSingleton<QuestManager>.I.eventList[index];
      if (arenaDataFromList.eventType == 15)
        return arenaDataFromList;
    }
    return (Network.EventData) null;
  }

  public List<Network.EventData> GetBingoDataList() => this.bingoEventList;

  public List<Network.EventData> GetValidBingoDataListInSection()
  {
    if (!MonoBehaviourSingleton<GameSceneManager>.IsValid())
      return new List<Network.EventData>();
    EVENT_DISPLAY_LOCATION_TYPE excludeLocationType = !(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene") ? EVENT_DISPLAY_LOCATION_TYPE.FIELD : EVENT_DISPLAY_LOCATION_TYPE.HOME;
    return this.bingoEventList.Where<Network.EventData>((Func<Network.EventData, bool>) (e => (EVENT_DISPLAY_LOCATION_TYPE) e.displayLocationType != excludeLocationType)).ToList<Network.EventData>();
  }

  public bool IsBingoPlayableEventExist()
  {
    List<Network.EventData> dataListInSection = this.GetValidBingoDataListInSection();
    if (dataListInSection == null || dataListInSection.Count <= 0)
      return false;
    int index = 0;
    for (int count = dataListInSection.Count; index < count; ++index)
    {
      if (dataListInSection[index].GetRest() >= 1)
        return true;
    }
    return false;
  }

  public void SendGetExploreList(Action<bool> call_back)
  {
    this.SendGetQuestList(new QuestListModel.RequestSendForm()
    {
      req_d = 1,
      req_e = 1,
      req_eq = 1
    }, call_back);
  }

  public static string GenerateQuestToken()
  {
    return QuestManager.GenerateQuestToken(DateTime.Now.GetHashCode());
  }

  public static string GenerateQuestToken(int key)
  {
    byte[] bytes = Encoding.UTF8.GetBytes(key.ToString());
    return string.Concat(((IEnumerable<byte>) MD5.Create().ComputeHash(bytes)).Select<byte, string>((Func<byte, string>) (i => i.ToString("x2"))).ToArray<string>());
  }

  public void SendQuestStart(
    int questId,
    int equip_set_no,
    bool free_join,
    Action<bool> call_back)
  {
    this.ClearPlayData();
    QuestStartModel.RequestSendForm postData = new QuestStartModel.RequestSendForm();
    postData.qid = questId;
    postData.qt = QuestManager.GenerateQuestToken();
    postData.setNo = equip_set_no;
    postData.crystalCL = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.Crystal;
    postData.free = free_join ? 1 : 0;
    postData.dId = (int) this.currentDeliveryId;
    this.currentDeliveryId = 0U;
    postData.d = NetworkNative.getUniqueDeviceId();
    if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.intervalTransferInfo != null)
    {
      InGameManager.IntervalTransferInfo intervalTransferInfo = MonoBehaviourSingleton<InGameManager>.I.intervalTransferInfo;
      for (int index = 0; index < intervalTransferInfo.playerInfoList.Count; ++index)
      {
        InGameManager.IntervalTransferInfo.PlayerInfo playerInfo = intervalTransferInfo.playerInfoList[index];
        if (playerInfo.isSelf)
        {
          postData.actioncount = playerInfo.taskChecker.GetTaskCount();
          playerInfo.taskChecker.Clear();
        }
      }
    }
    if (MonoBehaviourSingleton<InGameManager>.IsValid())
      MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.ClearInfo();
    Protocol.Send<QuestStartModel.RequestSendForm, QuestStartModel>(QuestStartModel.URL, postData, (Action<QuestStartModel>) (ret =>
    {
      bool flag = false;
      switch (ret.Error)
      {
        case Error.None:
          flag = true;
          this.startData = ret.result;
          this.startTime = Time.time;
          MonoBehaviourSingleton<GoWrapManager>.I.trackQuestStart(this.currentQuestID);
          break;
      }
      call_back(flag);
    }));
  }

  public bool IsUnLockedTimeForCompleteSend()
  {
    float num = 0.0f;
    if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
      num = (float) MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.QUEST_LOCK_SEC;
    return (double) Time.time - (double) this.startTime >= (double) num;
  }

  public void SetReciveCompleteData(
    QuestCompleteData recv_comp,
    List<int> now_clear_mission_list,
    List<int> old_mission_clear_state)
  {
    if (recv_comp != null)
      this.compData = recv_comp;
    this.missionNewClearFlag = now_clear_mission_list;
    if (old_mission_clear_state == null)
      return;
    int index = 0;
    for (int count = old_mission_clear_state.Count; index < count; ++index)
    {
      if (old_mission_clear_state[index] >= 3)
        this.missionNewClearFlag[index] = 0;
    }
  }

  public void SetCompleteDataFromGuildRequest(
    uint beforeQuestId,
    GuildRequestCompleteModel.Param guildRequestCompleteData)
  {
    this.compData = new QuestCompleteData();
    this.compData.reward = guildRequestCompleteData.reward;
    this.compData.pointEvent = guildRequestCompleteData.pointEvent;
    this.SetCurrentQuestID(beforeQuestId, false);
  }

  public void SetCompleteDataFromGuildRequestMultiComplete(
    GuildRequestCompleteModel.Param guildRequestCompleteData)
  {
    this.compData = new QuestCompleteData();
    this.compData.reward = guildRequestCompleteData.reward;
    this.compData.pointEvent = guildRequestCompleteData.pointEvent;
  }

  private void SetGivenDamageList(List<int> damage_list)
  {
    List<InGameRecorder.PlayerRecord> players = MonoBehaviourSingleton<InGameRecorder>.I.players;
    for (int index = 0; index < players.Count; ++index)
    {
      if (players[index].isSelf && damage_list.Count == 0)
      {
        damage_list.Add(players[index].givenTotalDamage);
        index = 0;
      }
      else if (damage_list.Count != 0)
        damage_list.Add(players[index].givenTotalDamage);
    }
  }

  private List<int> GetMissionClearStatus()
  {
    List<int> mission_clear_status = (List<int>) null;
    QuestInfoData questInfoData = this.GetQuestInfoData(this.currentQuestID);
    if (questInfoData != null && !questInfoData.IsMissionEmpty())
    {
      mission_clear_status = new List<int>();
      Array.ForEach<QuestInfoData.Mission>(questInfoData.missionData, (Action<QuestInfoData.Mission>) (data => mission_clear_status.Add(data != null ? (int) data.state : 0)));
    }
    return mission_clear_status;
  }

  public bool CheckMissionAllClear(uint questID)
  {
    ClearStatusQuest clearStatusQuestData = MonoBehaviourSingleton<QuestManager>.I.GetClearStatusQuestData(questID);
    return clearStatusQuestData != null && !clearStatusQuestData.missionStatus.Any<int>((Func<int, bool>) (missionStatus => missionStatus < 3));
  }

  public bool CheckEventMissionAllClear(int eventID)
  {
    List<QuestTable.QuestTableData> eventQuestDataList = Singleton<QuestTable>.I.GetEventQuestDataList(eventID);
    for (int index = 0; index < eventQuestDataList.Count; ++index)
    {
      if (!this.CheckMissionAllClear(eventQuestDataList[index].questID))
        return false;
    }
    return true;
  }

  public void SendQuestCompleteTrial(List<int> mClear, Action<bool, Error> call_back)
  {
    if (this.startData == null)
    {
      call_back(false, Error.Unknown);
    }
    else
    {
      QuestCompleteTrialModel.RequestSendForm postData = new QuestCompleteTrialModel.RequestSendForm();
      postData.qt = this.startData.qt;
      postData.enemyHp = MonoBehaviourSingleton<InGameRecorder>.I.GetTotalEnemyHP();
      if (mClear != null)
        postData.mClear = mClear;
      Protocol.Send<QuestCompleteTrialModel.RequestSendForm, QuestCompleteTrialModel>(QuestCompleteTrialModel.URL, postData, (Action<QuestCompleteTrialModel>) (ret =>
      {
        bool flag = false;
        if (ret.Error == Error.None)
          flag = true;
        call_back(flag, ret.Error);
      }));
    }
  }

  public void SendQuestComplete(
    List<List<int>> breakIds,
    List<int> mClear,
    List<int> memIds,
    float hpRate,
    List<QuestCompleteModel.BattleUserLog> logs,
    Action<bool, Error> call_back)
  {
    if (this.startData == null)
    {
      call_back(false, Error.Unknown);
    }
    else
    {
      QuestCompleteModel.RequestSendForm postData = new QuestCompleteModel.RequestSendForm();
      postData.qt = this.startData.qt;
      if (breakIds != null)
      {
        if (breakIds.Count > 0 && breakIds[0] != null)
          postData.breakIds0 = breakIds[0];
        if (breakIds.Count > 1 && breakIds[1] != null)
          postData.breakIds1 = breakIds[1];
        if (breakIds.Count > 2 && breakIds[2] != null)
          postData.breakIds2 = breakIds[2];
        if (breakIds.Count > 3 && breakIds[3] != null)
          postData.breakIds3 = breakIds[3];
        if (breakIds.Count > 4 && breakIds[4] != null)
          postData.breakIds4 = breakIds[4];
      }
      if (memIds != null)
        postData.memids = memIds;
      if (mClear != null)
        postData.mClear = mClear;
      postData.hpRate = hpRate;
      if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
        this.SetGivenDamageList(postData.givenDamageList);
      if (logs != null)
      {
        postData.fieldId = MonoBehaviourSingleton<FieldManager>.I.GetFieldId();
        postData.logs = logs;
        postData.enemyHp = MonoBehaviourSingleton<InGameRecorder>.I.GetTotalEnemyHP();
      }
      if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.self, (Object) null))
      {
        postData.actioncount = MonoBehaviourSingleton<StageObjectManager>.I.self.taskChecker.GetTaskCount();
        MonoBehaviourSingleton<StageObjectManager>.I.self.taskChecker.Clear();
      }
      if (MonoBehaviourSingleton<InGameManager>.IsValid())
      {
        postData.deliveryBattleInfo = MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.GetInfo();
        MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.ClearInfo();
      }
      List<int> mission_clear_status = this.GetMissionClearStatus();
      if (MonoBehaviourSingleton<StatusManager>.I.GetBoostStatus(USE_ITEM_EFFECT_TYPE.EVENT_POINT_UP) == null)
      {
        if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
          MonoBehaviourSingleton<UIPlayerStatus>.I.SetHGPBoostUpdatePermitFlag(false);
        if (MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
          MonoBehaviourSingleton<UIEnduranceStatus>.I.SetHGPBoostUpdatePermitFlag(false);
      }
      if (MonoBehaviourSingleton<InGameManager>.IsValid())
      {
        postData.remainSec = MonoBehaviourSingleton<InGameProgress>.I.remaindTime;
        postData.elapseSec = MonoBehaviourSingleton<InGameProgress>.I.GetElapsedTime();
      }
      if (this.IsWaveMatch(true))
      {
        postData.dc = MonoBehaviourSingleton<InGameProgress>.I.defeatCount;
        postData.dbc = MonoBehaviourSingleton<InGameProgress>.I.defeatBossCount;
        postData.pdbc = MonoBehaviourSingleton<InGameProgress>.I.partyDefeatBossCount;
        postData.rHp = MonoBehaviourSingleton<StageObjectManager>.I.GetWaveMatchTargetHpRate();
        postData.rSec = MonoBehaviourSingleton<InGameProgress>.I.remaindTime;
        postData.wmwave = MonoBehaviourSingleton<InGameProgress>.I.waveMatchWave;
      }
      Protocol.Send<QuestCompleteModel.RequestSendForm, QuestCompleteModel>(QuestCompleteModel.URL, postData, (Action<QuestCompleteModel>) (ret =>
      {
        bool flag = false;
        if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
          MonoBehaviourSingleton<UIPlayerStatus>.I.SetHGPBoostUpdatePermitFlag(true);
        if (MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
          MonoBehaviourSingleton<UIEnduranceStatus>.I.SetHGPBoostUpdatePermitFlag(true);
        switch (ret.Error)
        {
          case Error.None:
            flag = true;
            this.SetReciveCompleteData(ret.result, mClear, mission_clear_status);
            this.resultUserCollection.SetPartyFollowInfo(ret.result.friend);
            if (MonoBehaviourSingleton<PartyManager>.IsValid())
              MonoBehaviourSingleton<PartyManager>.I.SetFollowPartyMember(ret.result.friend);
            if (MonoBehaviourSingleton<FriendManager>.IsValid())
              MonoBehaviourSingleton<FriendManager>.I.SetFollowNum(ret.result.followNum);
            if (ret.result.repeatParty != null)
            {
              MonoBehaviourSingleton<PartyManager>.I.repeatPartyStatus = ret.result.repeatParty.repeatPartyStatus;
              if (ret.result.repeatParty.repeatPartyStatus > 0)
                MonoBehaviourSingleton<PartyManager>.I.UpdatePartyRepeat(ret.result.repeatParty.party, (List<FollowPartyMember>) null, ret.result.repeatParty.partyServer, ret.result.repeatParty.inviteFriendInfo);
              else if (ret.result.repeatParty.repeatPartyStatus < 0)
                MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest = false;
            }
            MonoBehaviourSingleton<GoWrapManager>.I.trackQuestEnd(this.currentQuestID, true);
            break;
        }
        call_back(flag, ret.Error);
      }));
    }
  }

  public void SendQuestRetire(
    bool is_timeout,
    List<int> memIDs,
    string roomId,
    List<QuestCompleteModel.BattleUserLog> logs,
    Action<bool> call_back)
  {
    QuestRetireModel.RequestSendForm postData = new QuestRetireModel.RequestSendForm();
    postData.qt = this.startData.qt;
    postData.timeout = is_timeout ? 1 : 0;
    if (memIDs != null)
      postData.memids = memIDs;
    if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.IsRush())
      postData.wave = MonoBehaviourSingleton<InGameManager>.I.GetCurrentWaveNum();
    if (logs != null)
    {
      postData.fieldId = MonoBehaviourSingleton<FieldManager>.I.GetFieldId();
      postData.logs = logs;
      postData.enemyHp = MonoBehaviourSingleton<InGameRecorder>.I.GetTotalEnemyHP();
    }
    if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.self, (Object) null))
    {
      if (!QuestManager.IsValidTrial())
        postData.actioncount = MonoBehaviourSingleton<StageObjectManager>.I.self.taskChecker.GetTaskCount();
      MonoBehaviourSingleton<StageObjectManager>.I.self.taskChecker.Clear();
    }
    if (this.IsWaveMatch(true))
    {
      postData.dc = MonoBehaviourSingleton<InGameProgress>.I.defeatCount;
      postData.dbc = MonoBehaviourSingleton<InGameProgress>.I.defeatBossCount;
      postData.pdbc = MonoBehaviourSingleton<InGameProgress>.I.partyDefeatBossCount;
      postData.rSec = MonoBehaviourSingleton<InGameProgress>.I.remaindTime;
      postData.wmwave = MonoBehaviourSingleton<InGameProgress>.I.waveMatchWave;
    }
    Protocol.Send<QuestRetireModel.RequestSendForm, QuestRetireModel>(QuestRetireModel.URL, postData, (Action<QuestRetireModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        this.retireData = ret.result;
        this.resultUserCollection.SetPartyFollowInfo(ret.result.friend);
        if (MonoBehaviourSingleton<PartyManager>.IsValid())
          MonoBehaviourSingleton<PartyManager>.I.SetFollowPartyMember(ret.result.friend);
        if (MonoBehaviourSingleton<FriendManager>.IsValid())
          MonoBehaviourSingleton<FriendManager>.I.SetFollowNum(ret.result.followNum);
        MonoBehaviourSingleton<GoWrapManager>.I.trackQuestEnd(this.currentQuestID, false);
      }
      call_back(flag);
    }));
  }

  public void SendQuestContinue(Action<bool, Error> call_back)
  {
    Protocol.Send<QuestContinueModel.RequestSendForm, QuestContinueModel>(QuestContinueModel.URL, new QuestContinueModel.RequestSendForm()
    {
      crystalCL = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.Crystal
    }, (Action<QuestContinueModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      call_back(flag, ret.Error);
    }));
  }

  public void SendQuestReadEventStory(int eventId, Action<bool, Error> call_back)
  {
    Protocol.Send<QuestReadEventStoryModel.RequestSendForm, QuestReadEventStoryModel>(QuestReadEventStoryModel.URL, new QuestReadEventStoryModel.RequestSendForm()
    {
      eventId = eventId
    }, (Action<QuestReadEventStoryModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      call_back(flag, ret.Error);
    }));
  }

  public void SendQuestRushProgress(
    int wave,
    int remainSec,
    List<int> breakIds,
    List<int> mClear,
    List<int> memIds,
    float hpRate,
    List<QuestCompleteModel.BattleUserLog> logs,
    Action<bool, Error> call_back)
  {
    if (this.startData == null)
    {
      call_back(false, Error.Unknown);
    }
    else
    {
      QuestRushProgressModel.RequestSendForm postData = new QuestRushProgressModel.RequestSendForm();
      postData.wave = wave;
      postData.remainSec = remainSec;
      postData.qt = this.startData.qt;
      if (breakIds != null)
        postData.breakIds = breakIds;
      if (memIds != null)
        postData.memids = memIds;
      if (mClear != null)
        postData.mClear = mClear;
      postData.hpRate = hpRate;
      if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
        this.SetGivenDamageList(postData.givenDamageList);
      if (logs != null)
      {
        postData.fieldId = MonoBehaviourSingleton<FieldManager>.I.GetFieldId();
        postData.logs = logs;
        postData.enemyHp = MonoBehaviourSingleton<InGameRecorder>.I.GetTotalEnemyHP();
      }
      if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.self, (Object) null))
      {
        postData.actioncount = MonoBehaviourSingleton<StageObjectManager>.I.self.taskChecker.GetTaskCount();
        MonoBehaviourSingleton<StageObjectManager>.I.self.taskChecker.Clear();
      }
      if (MonoBehaviourSingleton<InGameManager>.IsValid())
      {
        postData.deliveryBattleInfo = MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.GetInfo();
        MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.ClearInfo();
      }
      List<int> mission_clear_status = this.GetMissionClearStatus();
      Protocol.Send<QuestRushProgressModel.RequestSendForm, QuestRushProgressModel>(QuestRushProgressModel.URL, postData, (Action<QuestRushProgressModel>) (ret =>
      {
        bool flag = false;
        switch (ret.Error)
        {
          case Error.None:
            flag = true;
            MonoBehaviourSingleton<InGameManager>.I.AddWaveResult(ret.result.reward, ret.result.pointEvent, ret.result.pointShop);
            MonoBehaviourSingleton<InGameProgress>.I.SetRushRemainTime(ret.result.remainSec);
            MonoBehaviourSingleton<InGameProgress>.I.SetRushTimeBonus(ret.result.plusSec);
            this.SetReciveCompleteData((QuestCompleteData) null, mClear, mission_clear_status);
            break;
        }
        call_back(flag, ret.Error);
      }));
    }
  }

  public void SendArenaQuestStart(
    ArenaStartModel.RequestSendForm requestData,
    Action<bool> callBack)
  {
    this.ClearPlayData();
    requestData.qt = QuestManager.GenerateQuestToken();
    Protocol.Send<ArenaStartModel.RequestSendForm, ArenaStartModel>(ArenaStartModel.URL, requestData, (Action<ArenaStartModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        this.startData = ret.result;
        this.startTime = Time.time;
      }
      callBack(flag);
    }));
  }

  public void SendQuestArenaProgress(
    ArenaProgressModel.RequestSendForm requestData,
    Action<bool, Error> callback)
  {
    if (this.startData == null)
    {
      callback(false, Error.Unknown);
    }
    else
    {
      requestData.qt = this.startData.qt;
      Protocol.Send<ArenaProgressModel.RequestSendForm, ArenaProgressModel>(ArenaProgressModel.URL, requestData, (Action<ArenaProgressModel>) (ret =>
      {
        bool flag = false;
        if (ret.Error == Error.None)
        {
          flag = true;
          MonoBehaviourSingleton<InGameManager>.I.AddArenaWaveResult(ret.result.reward, ret.result.pointShop);
          MonoBehaviourSingleton<InGameProgress>.I.SetArenaRemainTime(ret.result.remainMilliSec);
          MonoBehaviourSingleton<InGameProgress>.I.SetArenaTimeBonus(ret.result.plusSec);
        }
        callback(flag, ret.Error);
      }));
    }
  }

  public void SendArenaComplete(Action<bool, Error> callBack)
  {
    if (this.startData == null)
      callBack(false, Error.Unknown);
    else if (!MonoBehaviourSingleton<CoopManager>.IsValid())
    {
      callBack(false, Error.Unknown);
    }
    else
    {
      CoopManager i = MonoBehaviourSingleton<CoopManager>.I;
      ArenaCompleteModel.RequestSendForm postData = new ArenaCompleteModel.RequestSendForm();
      if (MonoBehaviourSingleton<InGameProgress>.IsValid())
      {
        postData.remainMilliSec = MonoBehaviourSingleton<InGameProgress>.I.GetArenaRemainMilliSec();
        postData.totalElapseMilliSec = MonoBehaviourSingleton<InGameProgress>.I.GetArenaElapsedMilliSec();
      }
      Protocol.Send<ArenaCompleteModel.RequestSendForm, ArenaCompleteModel>(ArenaCompleteModel.URL, postData, (Action<ArenaCompleteModel>) (ret =>
      {
        bool flag = false;
        if (ret.Error == Error.None)
        {
          flag = true;
          this.arenaCompData = ret.result;
        }
        callBack(flag, ret.Error);
      }));
    }
  }

  public void SendArenaRetire(ArenaRetireModel.RequestSendForm requestData, Action<bool> callBack)
  {
    if (this.startData == null)
    {
      callBack(false);
    }
    else
    {
      requestData.qt = this.startData.qt;
      Protocol.Send<ArenaRetireModel.RequestSendForm, ArenaRetireModel>(ArenaRetireModel.URL, requestData, (Action<ArenaRetireModel>) (ret =>
      {
        bool flag = false;
        if (ret.Error == Error.None)
          flag = true;
        callBack(flag);
      }));
    }
  }

  public void SendGetArenaUserRecord(
    int userId,
    int eventId,
    Action<bool, ArenaUserRecordModel.Param> callBack)
  {
    Protocol.Send<ArenaUserRecordModel.RequestSendForm, ArenaUserRecordModel>(ArenaUserRecordModel.URL, new ArenaUserRecordModel.RequestSendForm()
    {
      userId = userId,
      eventId = eventId
    }, (Action<ArenaUserRecordModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      callBack(flag, ret.result);
    }));
  }

  public void SetClearStatus()
  {
    if (!this.firstSetGetClearStatus)
      return;
    this.firstSetGetClearStatus = false;
    this.clearStatusQuest = MonoBehaviourSingleton<OnceManager>.I.result.clearstatus.clearStatusQuest;
    this.clearStatusQuestEnemySpecies = MonoBehaviourSingleton<OnceManager>.I.result.clearstatus.clearStatusQuestEnemySpecies;
  }

  public void OnDiff(BaseModelDiff.DiffClearStatusQuest diff)
  {
    bool flag = false;
    if (Utility.IsExist((ICollection) diff.add))
    {
      diff.add.ForEach((Action<ClearStatusQuest>) (data => this.clearStatusQuest.Add(data)));
      flag = true;
    }
    if (Utility.IsExist((ICollection) diff.del))
    {
      diff.del.ForEach((Action<int>) (qusetId => this.clearStatusQuest.RemoveAll((Predicate<ClearStatusQuest>) (list_data => list_data.questId == qusetId))));
      flag = true;
    }
    if (Utility.IsExist((ICollection) diff.update))
    {
      diff.update.ForEach((Action<ClearStatusQuest>) (data =>
      {
        ClearStatusQuest clearStatusQuest = this.clearStatusQuest.Find((Predicate<ClearStatusQuest>) (list_data => list_data.questId == data.questId));
        clearStatusQuest.questId = data.questId;
        clearStatusQuest.questStatus = data.questStatus;
        clearStatusQuest.missionStatus = data.missionStatus;
        clearStatusQuest.story = data.story;
        clearStatusQuest.clearTime = data.clearTime;
      }));
      flag = true;
    }
    if (!flag)
      return;
    this.DirtyQuest();
  }

  public void DirtyQuest()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_QUEST_CLEAR_STATUS);
  }

  public void OnDiff(
    BaseModelDiff.DiffClearStatusQuestEnemySpecies diff)
  {
    bool flag = false;
    if (Utility.IsExist((ICollection) diff.add))
    {
      diff.add.ForEach((Action<ClearStatusQuestEnemySpecies>) (data => this.clearStatusQuestEnemySpecies.Add(data)));
      flag = true;
    }
    if (Utility.IsExist((ICollection) diff.update))
    {
      diff.update.ForEach((Action<ClearStatusQuestEnemySpecies>) (data =>
      {
        ClearStatusQuestEnemySpecies questEnemySpecies = this.clearStatusQuestEnemySpecies.Find((Predicate<ClearStatusQuestEnemySpecies>) (list_data => list_data.enemySpecies == data.enemySpecies));
        questEnemySpecies.enemySpecies = data.enemySpecies;
        questEnemySpecies.questStatus = data.questStatus;
      }));
      flag = true;
    }
    if (!flag)
      return;
    this.DirtyQuest();
  }

  private void UpdateChallengeList(List<QuestData> shadow) => this.challengeList = shadow;

  public void SendGetChallengeList(
    QuestChallengeListModel.RequestSendForm send,
    Action<bool, Error> call_back,
    bool isSave)
  {
    if (isSave)
      this.SaveChallengeSearchSettings(send);
    Protocol.Send<QuestChallengeListModel.RequestSendForm, QuestChallengeListModel>(QuestChallengeListModel.URL, send, (Action<QuestChallengeListModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        this.UpdateChallengeList(ret.result.shadow);
      }
      call_back(flag, ret.Error);
    }));
  }

  public void SendGetChallengeList(int enemyLevel, Action<bool, Error> call_back, bool isSave)
  {
    this.SendGetChallengeList(new QuestChallengeListModel.RequestSendForm()
    {
      enemyLevel = enemyLevel
    }, call_back, isSave);
  }

  public void SendGetChallengeList(
    QuestAcceptChallengeRoomCondition.ChallengeSearchRequestParam requestParam,
    Action<bool, Error> call_back,
    bool isSave)
  {
    this.SendGetChallengeList(new QuestChallengeListModel.RequestSendForm()
    {
      elementBit = requestParam.elementBit,
      rarityBit = requestParam.rarityBit,
      enemySpeciesId = requestParam.GetEnemySpeciesId(requestParam.targetEnemySpeciesName),
      enemyLevel = requestParam.enemyLevel,
      enemySpeciesName = requestParam.targetEnemySpeciesName
    }, call_back, isSave);
  }

  private void SaveChallengeSearchSettings(QuestChallengeListModel.RequestSendForm sendForm)
  {
    PlayerPrefs.SetInt("CHALLENGE_SEARCH_RAIRTY_KEY", sendForm.rarityBit);
    PlayerPrefs.SetInt("CHALLENGE_SEARCH_ELEMENT_KEY", sendForm.elementBit);
    PlayerPrefs.SetInt("CHALLENGE_SEARCH_ENEMY_LEVEL_KEY", sendForm.enemyLevel);
    if (!string.IsNullOrEmpty(sendForm.enemySpeciesName))
      PlayerPrefs.SetString("CHALLENGE_SEARCH_SPECIES_KEY", sendForm.enemySpeciesName);
    PlayerPrefs.Save();
  }

  public void SendGetChallengeEnmey(
    int enemyId,
    Action<bool, QuestChallengeEnemyModel.Param> call_back)
  {
    Protocol.Send<QuestChallengeEnemyModel.RequestSendForm, QuestChallengeEnemyModel>(QuestChallengeEnemyModel.URL, new QuestChallengeEnemyModel.RequestSendForm()
    {
      enemyId = enemyId
    }, (Action<QuestChallengeEnemyModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      call_back(flag, ret.result);
    }));
  }

  public void SetChallengeSearchRequestFromPrefs(
    int userLevel,
    QuestAcceptChallengeRoomCondition.ChallengeSearchRequestParam sendForm)
  {
    if (sendForm == null)
      return;
    int num = PlayerPrefs.GetInt("CHALLENGE_SEARCH_USER_LEVEL_KEY", 0);
    if (userLevel != num)
    {
      PlayerPrefs.SetInt("CHALLENGE_SEARCH_USER_LEVEL_KEY", userLevel);
      PlayerPrefs.SetInt("CHALLENGE_SEARCH_ENEMY_LEVEL_KEY", userLevel);
      PlayerPrefs.Save();
    }
    sendForm.rarityBit = PlayerPrefs.GetInt("CHALLENGE_SEARCH_RAIRTY_KEY", 8388607 /*0x7FFFFF*/);
    sendForm.elementBit = PlayerPrefs.GetInt("CHALLENGE_SEARCH_ELEMENT_KEY", 8388607 /*0x7FFFFF*/);
    sendForm.enemyLevel = PlayerPrefs.GetInt("CHALLENGE_SEARCH_ENEMY_LEVEL_KEY", userLevel);
    sendForm.targetEnemySpeciesName = PlayerPrefs.GetString("CHALLENGE_SEARCH_SPECIES_KEY", (string) null);
  }

  public bool initialized { get; private set; }

  public List<uint> GetTargetClearedNewOpenQuest(uint target_quest_id)
  {
    List<uint> list = new List<uint>();
    Singleton<QuestTable>.I.AllQuestData((Action<QuestTable.QuestTableData>) (table =>
    {
      if ((int) table.appearQuestId != (int) target_quest_id)
        return;
      list.Add(table.questID);
    }));
    list.Sort();
    return list;
  }

  public string GetQuestDifficultySpriteName(DIFFICULTY_TYPE dufficulty)
  {
    switch (dufficulty)
    {
      case DIFFICULTY_TYPE.LV2:
        return "Quest_classicon_middle";
      case DIFFICULTY_TYPE.LV3:
        return "Quest_classicon_high";
      case DIFFICULTY_TYPE.LV4:
      case DIFFICULTY_TYPE.LV5:
      case DIFFICULTY_TYPE.LV6:
      case DIFFICULTY_TYPE.LV7:
      case DIFFICULTY_TYPE.LV8:
      case DIFFICULTY_TYPE.LV9:
      case DIFFICULTY_TYPE.LV10:
        return "Quest_classicon_high";
      default:
        return "Quest_classicon_beginner";
    }
  }

  public bool IsForceDefeatQuest()
  {
    if (this.currentQuestID == 0U)
      return false;
    QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(this.currentQuestID);
    return questData != null && questData.forceDefeat;
  }

  public QuestManager.VorgonQuetType GetVorgonQuestType()
  {
    switch (this.currentQuestID)
    {
      case 303510901:
        return QuestManager.VorgonQuetType.BATTLE_WITH_WYBURN;
      case 303512301:
        return QuestManager.VorgonQuetType.BATTLE_WITH_VORGON;
      case 314410901:
        return QuestManager.VorgonQuetType.BATTLE_WITH_WYBURN;
      default:
        return QuestManager.VorgonQuetType.NONE;
    }
  }

  public bool isBackGachaQuest { get; set; }

  public class SelectQuestData
  {
    public uint questId;
    public bool isFreeJoin;
    public uint seriesIndex;
    public float limitTime;
    public QuestTable.QuestTableData questData;
  }

  public enum VorgonQuetType
  {
    NONE,
    BATTLE_WITH_WYBURN,
    BATTLE_WITH_VORGON,
    MAX_NUM,
  }
}
