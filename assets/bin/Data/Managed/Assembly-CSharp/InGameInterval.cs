// Decompiled with JetBrains decompiler
// Type: InGameInterval
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class InGameInterval : GameSection
{
  private bool isTransitionFieldMap;
  private bool isNewField;
  private bool isEncounterBoss;
  private bool fromBossExplore;
  private uint portalID;
  private bool coopServerInvalidFlag;

  public override void Initialize() => this.StartCoroutine(this.DoInitialize());

  private IEnumerator DoInitialize()
  {
    yield return (object) null;
    if (MonoBehaviourSingleton<InputManager>.IsValid())
    {
      MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_GRAB, false);
      MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_COMMAND, false);
    }
    MonoBehaviourSingleton<InGameManager>.I.ResumeQuestTransferInfo();
    bool keep_record = false;
    bool flag1 = false;
    bool flag2 = false;
    bool is_send_read_story = true;
    if (MonoBehaviourSingleton<InGameManager>.I.isTransitionQuestToField)
    {
      MonoBehaviourSingleton<InGameManager>.I.isTransitionQuestToField = false;
      if (MonoBehaviourSingleton<QuestManager>.IsValid())
        MonoBehaviourSingleton<QuestManager>.I.ClearPlayData();
      if (MonoBehaviourSingleton<InGameManager>.I.isQuestGate && MonoBehaviourSingleton<InGameRecorder>.I.isVictory)
      {
        MonoBehaviourSingleton<FieldManager>.I.SetCurrentFieldMapPortalID(MonoBehaviourSingleton<InGameManager>.I.beforePortalID);
        MonoBehaviourSingleton<InGameManager>.I.isGateQuestClear = true;
      }
      else if (MonoBehaviourSingleton<InGameManager>.I.backTransitionInfo != null)
      {
        FieldManager.FieldTransitionInfo backTransitionInfo = MonoBehaviourSingleton<InGameManager>.I.backTransitionInfo;
        MonoBehaviourSingleton<FieldManager>.I.SetCurrentFieldMapPortalID(backTransitionInfo.portalID, backTransitionInfo.mapX, backTransitionInfo.mapZ, backTransitionInfo.mapDir);
        flag1 = true;
        if (MonoBehaviourSingleton<InGameManager>.I.isQuestHappen)
          flag2 = true;
        if (MonoBehaviourSingleton<InGameManager>.I.readStoryID != 0)
        {
          is_send_read_story = false;
          MonoBehaviourSingleton<DeliveryManager>.I.SendReadStoryRead(MonoBehaviourSingleton<InGameManager>.I.readStoryID, (Action<bool, Error>) ((is_success, recv_reward) => is_send_read_story = true));
        }
      }
      MonoBehaviourSingleton<InGameManager>.I.backTransitionInfo = (FieldManager.FieldTransitionInfo) null;
      MonoBehaviourSingleton<InGameManager>.I.isQuestHappen = false;
      MonoBehaviourSingleton<InGameManager>.I.isQuestPortal = false;
      MonoBehaviourSingleton<InGameManager>.I.isQuestGate = false;
      MonoBehaviourSingleton<InGameManager>.I.isQuestFromGimmick = false;
      MonoBehaviourSingleton<InGameManager>.I.isStoryPortal = false;
      MonoBehaviourSingleton<InGameManager>.I.readStoryID = 0;
    }
    bool matching_flag = false;
    Action<bool> matching_end_action = (Action<bool>) (is_connect =>
    {
      if (!is_connect)
        this.coopServerInvalidFlag = true;
      else
        matching_flag = true;
    });
    int now_stage_id;
    if (MonoBehaviourSingleton<InGameManager>.I.isTransitionFieldToQuest)
    {
      MonoBehaviourSingleton<InGameManager>.I.isTransitionFieldToQuest = false;
      if (MonoBehaviourSingleton<InGameManager>.I.isQuestGate)
      {
        if (MonoBehaviourSingleton<FieldManager>.IsValid())
          MonoBehaviourSingleton<FieldManager>.I.ClearCurrentFieldData();
        if (MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.GetVorgonQuestType() != QuestManager.VorgonQuetType.NONE)
          CoopApp.EnterQuestOffline((Action<bool, bool, bool, bool>) ((is_m, is_c, is_r, is_s) => matching_end_action(is_c)));
        else if (MonoBehaviourSingleton<QuestManager>.I.IsForceDefeatQuest())
          CoopApp.EnterQuest((Action<bool, bool, bool, bool>) ((is_m, is_c, is_r, is_s) => matching_end_action(is_c)));
        else
          CoopApp.EnterQuestRandomMatching((Action<bool, bool, bool, bool>) ((is_m, is_c, is_r, is_s) => matching_end_action(is_s)));
      }
      else if (MonoBehaviourSingleton<InGameManager>.I.isQuestFromGimmick)
        CoopApp.EnterQuestRandomMatching((Action<bool, bool, bool, bool>) ((is_m, is_c, is_r, is_s) => matching_end_action(is_s)));
      else if (MonoBehaviourSingleton<InGameManager>.I.isQuestHappen)
      {
        now_stage_id = MonoBehaviourSingleton<CoopManager>.I.coopStage.stageId;
        MonoBehaviourSingleton<CoopNetworkManager>.I.RoomStageChange((int) MonoBehaviourSingleton<QuestManager>.I.currentQuestID, 0);
        while (CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected() && now_stage_id == MonoBehaviourSingleton<CoopManager>.I.coopStage.stageId)
          yield return (object) null;
        CoopApp.EnterQuestOnly((Action<bool>) (is_s => matching_end_action(CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected())));
        if (MonoBehaviourSingleton<CoopManager>.IsValid())
          MonoBehaviourSingleton<CoopManager>.I.OnStageChangeInterval();
      }
      else
      {
        if (MonoBehaviourSingleton<FieldManager>.IsValid())
          MonoBehaviourSingleton<FieldManager>.I.ClearCurrentFieldData();
        CoopApp.EnterQuest((Action<bool, bool, bool, bool>) ((is_m, is_c, is_r, is_s) => matching_end_action(is_c)));
      }
      if (MonoBehaviourSingleton<InGameManager>.I.isQuestGate)
        this.isEncounterBoss = true;
    }
    else if (MonoBehaviourSingleton<InGameManager>.I.isTransitionQuestToQuest)
    {
      MonoBehaviourSingleton<InGameManager>.I.isTransitionQuestToQuest = false;
      if (MonoBehaviourSingleton<FieldManager>.IsValid())
        MonoBehaviourSingleton<FieldManager>.I.ClearCurrentFieldData();
      CoopApp.EnterPartyQuest((Action<bool, bool, bool, bool>) ((is_m, is_c, is_r, is_s) => matching_end_action(is_c)));
    }
    else if (QuestManager.IsValidInGame())
    {
      if (MonoBehaviourSingleton<QuestManager>.I.IsExplore() || MonoBehaviourSingleton<InGameManager>.I.IsRush() || MonoBehaviourSingleton<QuestManager>.I.IsCurrentQuestTypeSeries() || MonoBehaviourSingleton<QuestManager>.I.IsWaveMatch() || MonoBehaviourSingleton<QuestManager>.I.IsCurrentQuestTypeSeriesArena())
      {
        if (MonoBehaviourSingleton<QuestManager>.I.IsExplore())
          this.isTransitionFieldMap = true;
        bool is_stage_change = true;
        if (MonoBehaviourSingleton<InGameManager>.I.isTransitionFieldReentry)
        {
          MonoBehaviourSingleton<InGameManager>.I.isTransitionFieldReentry = false;
          this.isTransitionFieldMap = false;
          is_stage_change = false;
          bool wait = true;
          uint before_map_id = MonoBehaviourSingleton<FieldManager>.I.currentMapID;
          float before_map_x = MonoBehaviourSingleton<FieldManager>.I.currentStartMapX;
          float before_map_z = MonoBehaviourSingleton<FieldManager>.I.currentStartMapZ;
          CoopApp.EnterPartyField((Action<bool, bool, bool>) ((is_m, is_c, is_r) =>
          {
            wait = false;
            if (is_r)
              is_stage_change = true;
            if ((int) before_map_id == (int) MonoBehaviourSingleton<FieldManager>.I.currentMapID)
              return;
            MonoBehaviourSingleton<FieldManager>.I.SetCurrentFieldMapID(before_map_id, before_map_x, before_map_z, 0.0f);
          }), true);
          while (wait)
            yield return (object) null;
        }
        else
          MonoBehaviourSingleton<CoopManager>.I.coopRoom.SnedMoveField((int) MonoBehaviourSingleton<QuestManager>.I.GetLastPortalId());
        int idx = 0;
        int questId = 0;
        uint dstMapId = MonoBehaviourSingleton<FieldManager>.I.currentMapID;
        if (MonoBehaviourSingleton<QuestManager>.I.IsExplore())
        {
          questId = (int) MonoBehaviourSingleton<QuestManager>.I.currentQuestID;
          idx = MonoBehaviourSingleton<QuestManager>.I.ExploreMapIdToIndex(dstMapId);
        }
        else if (MonoBehaviourSingleton<InGameManager>.I.IsRush())
        {
          questId = (int) MonoBehaviourSingleton<PartyManager>.I.GetQuestId();
          idx = MonoBehaviourSingleton<InGameManager>.I.GetRushIndex();
        }
        else if (MonoBehaviourSingleton<QuestManager>.I.IsCurrentQuestTypeSeries() || MonoBehaviourSingleton<QuestManager>.I.IsWaveMatch() || MonoBehaviourSingleton<QuestManager>.I.IsCurrentQuestTypeSeriesArena())
          questId = (int) MonoBehaviourSingleton<PartyManager>.I.GetQuestId();
        if (is_stage_change && MonoBehaviourSingleton<CoopManager>.I.coopStage.stageIndex != idx)
        {
          now_stage_id = MonoBehaviourSingleton<CoopManager>.I.coopStage.stageId;
          MonoBehaviourSingleton<CoopNetworkManager>.I.RoomStageChange(questId, idx);
          while (CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected() && now_stage_id == MonoBehaviourSingleton<CoopManager>.I.coopStage.stageId)
            yield return (object) null;
        }
        matching_end_action(true);
        keep_record = true;
        if (MonoBehaviourSingleton<InGameManager>.I.IsRush() || MonoBehaviourSingleton<QuestManager>.I.IsCurrentQuestTypeSeries() || MonoBehaviourSingleton<QuestManager>.I.IsWaveMatch() || MonoBehaviourSingleton<QuestManager>.I.IsCurrentQuestTypeSeriesArena())
        {
          uint currentMapId = MonoBehaviourSingleton<QuestManager>.I.GetCurrentMapId();
          if ((int) MonoBehaviourSingleton<FieldManager>.I.currentMapID != (int) currentMapId)
            MonoBehaviourSingleton<FieldManager>.I.SetCurrentFieldMapID(currentMapId, 0.0f, 0.0f, 0.0f);
        }
        if (MonoBehaviourSingleton<CoopManager>.IsValid())
          MonoBehaviourSingleton<CoopManager>.I.OnStageChangeInterval();
        if ((long) dstMapId == (long) MonoBehaviourSingleton<QuestManager>.I.GetExploreBossBatlleMapId() && this.isTransitionFieldMap)
          this.isEncounterBoss = true;
        if (MonoBehaviourSingleton<InGameManager>.I.isTransitionQuestToFieldExplore)
        {
          this.fromBossExplore = true;
          MonoBehaviourSingleton<InGameManager>.I.isTransitionQuestToFieldExplore = false;
        }
      }
      else
      {
        now_stage_id = MonoBehaviourSingleton<CoopManager>.I.coopStage.stageId;
        MonoBehaviourSingleton<CoopNetworkManager>.I.RoomStageChange((int) MonoBehaviourSingleton<QuestManager>.I.currentQuestID, (int) MonoBehaviourSingleton<QuestManager>.I.currentQuestSeriesIndex);
        while (CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected() && now_stage_id == MonoBehaviourSingleton<CoopManager>.I.coopStage.stageId)
          yield return (object) null;
        matching_end_action(true);
        uint currentMapId = MonoBehaviourSingleton<QuestManager>.I.GetCurrentMapId();
        if ((int) MonoBehaviourSingleton<FieldManager>.I.currentMapID != (int) currentMapId)
          MonoBehaviourSingleton<FieldManager>.I.SetCurrentFieldMapID(currentMapId, 0.0f, 0.0f, 0.0f);
        keep_record = true;
        if (MonoBehaviourSingleton<CoopManager>.IsValid())
          MonoBehaviourSingleton<CoopManager>.I.OnQuestSeriesInterval();
      }
    }
    else if (FieldManager.IsValidInGame())
    {
      this.isNewField = !MonoBehaviourSingleton<FieldManager>.I.CanJumpToMap(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
      this.portalID = MonoBehaviourSingleton<FieldManager>.I.currentPortalID;
      if (!flag1 && !MonoBehaviourSingleton<InGameManager>.I.isTransitionFieldReentry)
        this.isTransitionFieldMap = true;
      MonoBehaviourSingleton<InGameManager>.I.isTransitionFieldReentry = false;
      if (flag2 && CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected())
      {
        now_stage_id = MonoBehaviourSingleton<CoopManager>.I.coopStage.stageId;
        MonoBehaviourSingleton<CoopNetworkManager>.I.RoomStageChange(0, 0);
        while (CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected() && now_stage_id == MonoBehaviourSingleton<CoopManager>.I.coopStage.stageId)
          yield return (object) null;
        matching_end_action(CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected());
        if (MonoBehaviourSingleton<CoopManager>.IsValid())
          MonoBehaviourSingleton<CoopManager>.I.OnStageChangeInterval();
      }
      else
        CoopApp.EnterField(MonoBehaviourSingleton<FieldManager>.I.currentPortalID, 0U, (Action<bool, bool, bool>) ((is_m, is_c, is_r) => matching_end_action(is_c)));
    }
    while (!is_send_read_story)
      yield return (object) null;
    while (!matching_flag && !this.coopServerInvalidFlag)
      yield return (object) null;
    if (this.coopServerInvalidFlag)
    {
      base.Initialize();
    }
    else
    {
      if (MonoBehaviourSingleton<UIManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.mainChat, (Object) null))
        MonoBehaviourSingleton<UIManager>.I.mainChat.Open();
      if (!keep_record && MonoBehaviourSingleton<InGameRecorder>.IsValid())
        Object.DestroyImmediate((Object) MonoBehaviourSingleton<InGameRecorder>.I);
      MonoBehaviourSingleton<StageManager>.I.UnloadStage();
      yield return (object) MonoBehaviourSingleton<AppMain>.I.UnloadUnusedAssets(true);
      if (MonoBehaviourSingleton<InGameManager>.IsValid())
        MonoBehaviourSingleton<InGameManager>.I.ClearAllDrop();
      ((Component) MonoBehaviourSingleton<AppMain>.I.mainCamera).gameObject.SetActive(true);
      if (MonoBehaviourSingleton<InGameManager>.I.requestEventData != null)
      {
        MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(MonoBehaviourSingleton<InGameManager>.I.requestEventData);
        MonoBehaviourSingleton<InGameManager>.I.requestEventData = (EventData[]) null;
      }
      base.Initialize();
    }
  }

  public override void StartSection()
  {
    if (this.coopServerInvalidFlag)
      MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameProgress", ((Component) this).gameObject, "COOP_SERVER_INVALID");
    else if ((this.isTransitionFieldMap || this.isEncounterBoss) && !MonoBehaviourSingleton<FieldManager>.I.useFastTravel)
    {
      bool flag1 = InGameInterval.IsSrcOrDstChildRegion(this.portalID);
      bool flag2 = InGameInterval.IsDifferentRegion(this.portalID);
      bool flag3 = InGameInterval.IsSameField(this.portalID);
      bool flag4 = MonoBehaviourSingleton<QuestManager>.I.IsExplore();
      if (((!MonoBehaviourSingleton<InGameManager>.IsValid() ? 0 : (MonoBehaviourSingleton<InGameManager>.I.isStoryPortal ? 1 : 0)) | (flag3 ? 1 : 0)) != 0)
        MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameProgress", ((Component) this).gameObject, "INGAME_MAIN");
      else if (flag4)
        this.ToExplore();
      else if (flag2 && !flag1)
      {
        WorldMapOpenNewRegion.EVENT_TYPE _eventType = WorldMapOpenNewRegion.EVENT_TYPE.ONLY_CAMERA_MOVE;
        if (this.isNewField)
          _eventType = WorldMapOpenNewRegion.EVENT_TYPE.NONE;
        MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameProgress", ((Component) this).gameObject, "NEW_REGION", (object) new WorldMapOpenNewRegion.SectionEventData(_eventType));
      }
      else if (InGameInterval.IsJumpPortal(this.portalID))
      {
        MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameProgress", ((Component) this).gameObject, "NEW_FIELD", (object) new WorldMapOpenNewField.SectionEventData(WorldMapOpenNewField.EVENT_TYPE.QUEST_TO_FIELD, ENEMY_TYPE.BAT));
      }
      else
      {
        WorldMapOpenNewField.EVENT_TYPE _eventType = WorldMapOpenNewField.EVENT_TYPE.ONLY_CAMERA_MOVE;
        if (this.isNewField)
          _eventType = !(flag2 & flag1) ? WorldMapOpenNewField.EVENT_TYPE.NONE : WorldMapOpenNewField.EVENT_TYPE.OPEN_NEW_DUNGEON;
        else if (this.isEncounterBoss)
          _eventType = WorldMapOpenNewField.EVENT_TYPE.ENCOUNTER_BOSS;
        else if (flag1)
          _eventType = WorldMapOpenNewField.EVENT_TYPE.EXIST_IN_DUNGEON;
        MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameProgress", ((Component) this).gameObject, "NEW_FIELD", (object) new WorldMapOpenNewField.SectionEventData(_eventType, ENEMY_TYPE.BAT));
      }
    }
    else
      MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameProgress", ((Component) this).gameObject, "INGAME_MAIN");
    if (!MonoBehaviourSingleton<FieldManager>.IsValid())
      return;
    MonoBehaviourSingleton<FieldManager>.I.useFastTravel = false;
  }

  private void ToExplore()
  {
    FieldMapTable.PortalTableData portalData = Singleton<FieldMapTable>.I.GetPortalData(MonoBehaviourSingleton<InGameManager>.I.beforePortalID);
    if (portalData == null)
    {
      MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameProgress", ((Component) this).gameObject, "INGAME_MAIN");
    }
    else
    {
      FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(portalData.dstMapID);
      if (fieldMapData == null)
        MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameProgress", ((Component) this).gameObject, "INGAME_MAIN");
      else
        MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameProgress", ((Component) this).gameObject, "NEW_EXPLORE", (object) new ExploreMapOpenNewField.EventData()
        {
          regionId = fieldMapData.regionId,
          portalId = MonoBehaviourSingleton<InGameManager>.I.beforePortalID,
          fromBoss = this.fromBossExplore,
          toBoss = this.isEncounterBoss
        });
    }
  }

  private static bool IsDifferentRegion(uint portalID)
  {
    if (!Singleton<FieldMapTable>.IsValid())
      return false;
    FieldMapTable.PortalTableData portalData = Singleton<FieldMapTable>.I.GetPortalData(portalID);
    if (portalData == null)
      return false;
    FieldMapTable.FieldMapTableData fieldMapData1 = Singleton<FieldMapTable>.I.GetFieldMapData(portalData.srcMapID);
    FieldMapTable.FieldMapTableData fieldMapData2 = Singleton<FieldMapTable>.I.GetFieldMapData(portalData.dstMapID);
    return fieldMapData1 != null && fieldMapData2 != null && (int) fieldMapData1.regionId != (int) fieldMapData2.regionId;
  }

  private static bool IsSameField(uint portalID)
  {
    if (!Singleton<FieldMapTable>.IsValid())
      return false;
    FieldMapTable.PortalTableData portalData = Singleton<FieldMapTable>.I.GetPortalData(portalID);
    return portalData != null && (int) portalData.srcMapID == (int) portalData.dstMapID;
  }

  private static bool IsSrcOrDstChildRegion(uint portalID)
  {
    if (!Singleton<FieldMapTable>.IsValid())
      return false;
    FieldMapTable.PortalTableData portalData = Singleton<FieldMapTable>.I.GetPortalData(portalID);
    if (portalData == null)
      return false;
    FieldMapTable.FieldMapTableData fieldMapData1 = Singleton<FieldMapTable>.I.GetFieldMapData(portalData.srcMapID);
    FieldMapTable.FieldMapTableData fieldMapData2 = Singleton<FieldMapTable>.I.GetFieldMapData(portalData.dstMapID);
    if (fieldMapData1 == null || fieldMapData2 == null)
      return false;
    if ((int) fieldMapData1.childRegionId == (int) fieldMapData2.regionId)
      return true;
    if (!Singleton<RegionTable>.IsValid())
      return false;
    RegionTable.Data data1 = Singleton<RegionTable>.I.GetData(fieldMapData1.regionId);
    RegionTable.Data data2 = Singleton<RegionTable>.I.GetData(fieldMapData2.regionId);
    if (data1 == null || data2 == null)
      return false;
    return (int) data2.parentRegionId == (int) fieldMapData1.regionId || (int) data1.parentRegionId == (int) fieldMapData2.regionId;
  }

  private static bool IsJumpPortal(uint portalID)
  {
    if (!Singleton<FieldMapTable>.IsValid())
      return false;
    FieldMapTable.PortalTableData portalData = Singleton<FieldMapTable>.I.GetPortalData(portalID);
    return portalData != null && 0U >= portalData.srcMapID && 10000000U != portalID;
  }
}
