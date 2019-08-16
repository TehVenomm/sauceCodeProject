using System;
using System.Collections;
using UnityEngine;

public class InGameInterval : GameSection
{
	private bool isTransitionFieldMap;

	private bool isNewField;

	private bool isEncounterBoss;

	private bool fromBossExplore;

	private uint portalID;

	private bool coopServerInvalidFlag;

	public override void Initialize()
	{
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		yield return null;
		if (MonoBehaviourSingleton<InputManager>.IsValid())
		{
			MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_GRAB, disable: false);
			MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_COMMAND, disable: false);
		}
		MonoBehaviourSingleton<InGameManager>.I.ResumeQuestTransferInfo();
		bool keep_record = false;
		bool is_back_transition = false;
		bool is_back_online_field = false;
		bool is_send_read_story = true;
		if (MonoBehaviourSingleton<InGameManager>.I.isTransitionQuestToField)
		{
			MonoBehaviourSingleton<InGameManager>.I.isTransitionQuestToField = false;
			if (MonoBehaviourSingleton<QuestManager>.IsValid())
			{
				MonoBehaviourSingleton<QuestManager>.I.ClearPlayData();
			}
			if (MonoBehaviourSingleton<InGameManager>.I.isQuestGate && MonoBehaviourSingleton<InGameRecorder>.I.isVictory)
			{
				MonoBehaviourSingleton<FieldManager>.I.SetCurrentFieldMapPortalID(MonoBehaviourSingleton<InGameManager>.I.beforePortalID);
				MonoBehaviourSingleton<InGameManager>.I.isGateQuestClear = true;
			}
			else if (MonoBehaviourSingleton<InGameManager>.I.backTransitionInfo != null)
			{
				FieldManager.FieldTransitionInfo backTransitionInfo = MonoBehaviourSingleton<InGameManager>.I.backTransitionInfo;
				MonoBehaviourSingleton<FieldManager>.I.SetCurrentFieldMapPortalID(backTransitionInfo.portalID, backTransitionInfo.mapX, backTransitionInfo.mapZ, backTransitionInfo.mapDir);
				is_back_transition = true;
				if (MonoBehaviourSingleton<InGameManager>.I.isQuestHappen)
				{
					is_back_online_field = true;
				}
				if (MonoBehaviourSingleton<InGameManager>.I.readStoryID != 0)
				{
					is_send_read_story = false;
					MonoBehaviourSingleton<DeliveryManager>.I.SendReadStoryRead(MonoBehaviourSingleton<InGameManager>.I.readStoryID, delegate
					{
						is_send_read_story = true;
					});
				}
			}
			MonoBehaviourSingleton<InGameManager>.I.backTransitionInfo = null;
			MonoBehaviourSingleton<InGameManager>.I.isQuestHappen = false;
			MonoBehaviourSingleton<InGameManager>.I.isQuestPortal = false;
			MonoBehaviourSingleton<InGameManager>.I.isQuestGate = false;
			MonoBehaviourSingleton<InGameManager>.I.isQuestFromGimmick = false;
			MonoBehaviourSingleton<InGameManager>.I.isStoryPortal = false;
			MonoBehaviourSingleton<InGameManager>.I.readStoryID = 0;
		}
		bool matching_flag = false;
		Action<bool> matching_end_action = delegate(bool is_connect)
		{
			if (!is_connect)
			{
				coopServerInvalidFlag = true;
			}
			else
			{
				matching_flag = true;
			}
		};
		if (MonoBehaviourSingleton<InGameManager>.I.isTransitionFieldToQuest)
		{
			MonoBehaviourSingleton<InGameManager>.I.isTransitionFieldToQuest = false;
			if (MonoBehaviourSingleton<InGameManager>.I.isQuestGate)
			{
				if (MonoBehaviourSingleton<FieldManager>.IsValid())
				{
					MonoBehaviourSingleton<FieldManager>.I.ClearCurrentFieldData();
				}
				if (MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.GetVorgonQuestType() != 0)
				{
					CoopApp.EnterQuestOffline(delegate(bool is_m, bool is_c, bool is_r, bool is_s)
					{
						matching_end_action(is_c);
					});
				}
				else if (MonoBehaviourSingleton<QuestManager>.I.IsForceDefeatQuest())
				{
					CoopApp.EnterQuest(delegate(bool is_m, bool is_c, bool is_r, bool is_s)
					{
						matching_end_action(is_c);
					});
				}
				else
				{
					CoopApp.EnterQuestRandomMatching(delegate(bool is_m, bool is_c, bool is_r, bool is_s)
					{
						matching_end_action(is_s);
					});
				}
			}
			else if (MonoBehaviourSingleton<InGameManager>.I.isQuestFromGimmick)
			{
				CoopApp.EnterQuestRandomMatching(delegate(bool is_m, bool is_c, bool is_r, bool is_s)
				{
					matching_end_action(is_s);
				});
			}
			else if (MonoBehaviourSingleton<InGameManager>.I.isQuestHappen)
			{
				int now_stage_id4 = MonoBehaviourSingleton<CoopManager>.I.coopStage.stageId;
				MonoBehaviourSingleton<CoopNetworkManager>.I.RoomStageChange((int)MonoBehaviourSingleton<QuestManager>.I.currentQuestID, 0);
				while (CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected() && now_stage_id4 == MonoBehaviourSingleton<CoopManager>.I.coopStage.stageId)
				{
					yield return null;
				}
				CoopApp.EnterQuestOnly(delegate
				{
					matching_end_action(CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected());
				});
				if (MonoBehaviourSingleton<CoopManager>.IsValid())
				{
					MonoBehaviourSingleton<CoopManager>.I.OnStageChangeInterval();
				}
			}
			else
			{
				if (MonoBehaviourSingleton<FieldManager>.IsValid())
				{
					MonoBehaviourSingleton<FieldManager>.I.ClearCurrentFieldData();
				}
				CoopApp.EnterQuest(delegate(bool is_m, bool is_c, bool is_r, bool is_s)
				{
					matching_end_action(is_c);
				});
			}
			if (MonoBehaviourSingleton<InGameManager>.I.isQuestGate)
			{
				isEncounterBoss = true;
			}
		}
		else if (MonoBehaviourSingleton<InGameManager>.I.isTransitionQuestToQuest)
		{
			MonoBehaviourSingleton<InGameManager>.I.isTransitionQuestToQuest = false;
			if (MonoBehaviourSingleton<FieldManager>.IsValid())
			{
				MonoBehaviourSingleton<FieldManager>.I.ClearCurrentFieldData();
			}
			CoopApp.EnterPartyQuest(delegate(bool is_m, bool is_c, bool is_r, bool is_s)
			{
				matching_end_action(is_c);
			});
		}
		else if (QuestManager.IsValidInGame())
		{
			if (MonoBehaviourSingleton<QuestManager>.I.IsExplore() || MonoBehaviourSingleton<InGameManager>.I.IsRush() || MonoBehaviourSingleton<QuestManager>.I.IsCurrentQuestTypeSeries() || MonoBehaviourSingleton<QuestManager>.I.IsWaveMatch() || MonoBehaviourSingleton<QuestManager>.I.IsCurrentQuestTypeSeriesArena())
			{
				if (MonoBehaviourSingleton<QuestManager>.I.IsExplore())
				{
					isTransitionFieldMap = true;
				}
				bool is_stage_change = true;
				if (MonoBehaviourSingleton<InGameManager>.I.isTransitionFieldReentry)
				{
					MonoBehaviourSingleton<InGameManager>.I.isTransitionFieldReentry = false;
					isTransitionFieldMap = false;
					is_stage_change = false;
					bool wait = true;
					uint before_map_id = MonoBehaviourSingleton<FieldManager>.I.currentMapID;
					float before_map_x = MonoBehaviourSingleton<FieldManager>.I.currentStartMapX;
					float before_map_z = MonoBehaviourSingleton<FieldManager>.I.currentStartMapZ;
					CoopApp.EnterPartyField(delegate(bool is_m, bool is_c, bool is_r)
					{
						wait = false;
						if (is_r)
						{
							is_stage_change = true;
						}
						if (before_map_id != MonoBehaviourSingleton<FieldManager>.I.currentMapID)
						{
							MonoBehaviourSingleton<FieldManager>.I.SetCurrentFieldMapID(before_map_id, before_map_x, before_map_z, 0f);
						}
					}, is_reentry: true);
					while (wait)
					{
						yield return null;
					}
				}
				else
				{
					MonoBehaviourSingleton<CoopManager>.I.coopRoom.SnedMoveField((int)MonoBehaviourSingleton<QuestManager>.I.GetLastPortalId());
				}
				int mapIndex = 0;
				int questId = 0;
				uint dstMapId = MonoBehaviourSingleton<FieldManager>.I.currentMapID;
				if (MonoBehaviourSingleton<QuestManager>.I.IsExplore())
				{
					questId = (int)MonoBehaviourSingleton<QuestManager>.I.currentQuestID;
					mapIndex = MonoBehaviourSingleton<QuestManager>.I.ExploreMapIdToIndex(dstMapId);
				}
				else if (MonoBehaviourSingleton<InGameManager>.I.IsRush())
				{
					questId = (int)MonoBehaviourSingleton<PartyManager>.I.GetQuestId();
					mapIndex = MonoBehaviourSingleton<InGameManager>.I.GetRushIndex();
				}
				else if (MonoBehaviourSingleton<QuestManager>.I.IsCurrentQuestTypeSeries() || MonoBehaviourSingleton<QuestManager>.I.IsWaveMatch() || MonoBehaviourSingleton<QuestManager>.I.IsCurrentQuestTypeSeriesArena())
				{
					questId = (int)MonoBehaviourSingleton<PartyManager>.I.GetQuestId();
				}
				if (is_stage_change && MonoBehaviourSingleton<CoopManager>.I.coopStage.stageIndex != mapIndex)
				{
					int now_stage_id2 = MonoBehaviourSingleton<CoopManager>.I.coopStage.stageId;
					MonoBehaviourSingleton<CoopNetworkManager>.I.RoomStageChange(questId, mapIndex);
					while (CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected() && now_stage_id2 == MonoBehaviourSingleton<CoopManager>.I.coopStage.stageId)
					{
						yield return null;
					}
				}
				matching_end_action(obj: true);
				keep_record = true;
				if (MonoBehaviourSingleton<InGameManager>.I.IsRush() || MonoBehaviourSingleton<QuestManager>.I.IsCurrentQuestTypeSeries() || MonoBehaviourSingleton<QuestManager>.I.IsWaveMatch() || MonoBehaviourSingleton<QuestManager>.I.IsCurrentQuestTypeSeriesArena())
				{
					uint currentMapId = MonoBehaviourSingleton<QuestManager>.I.GetCurrentMapId();
					if (MonoBehaviourSingleton<FieldManager>.I.currentMapID != currentMapId)
					{
						MonoBehaviourSingleton<FieldManager>.I.SetCurrentFieldMapID(currentMapId, 0f, 0f, 0f);
					}
				}
				if (MonoBehaviourSingleton<CoopManager>.IsValid())
				{
					MonoBehaviourSingleton<CoopManager>.I.OnStageChangeInterval();
				}
				if (dstMapId == MonoBehaviourSingleton<QuestManager>.I.GetExploreBossBatlleMapId() && isTransitionFieldMap)
				{
					isEncounterBoss = true;
				}
				if (MonoBehaviourSingleton<InGameManager>.I.isTransitionQuestToFieldExplore)
				{
					fromBossExplore = true;
					MonoBehaviourSingleton<InGameManager>.I.isTransitionQuestToFieldExplore = false;
				}
			}
			else
			{
				int now_stage_id3 = MonoBehaviourSingleton<CoopManager>.I.coopStage.stageId;
				MonoBehaviourSingleton<CoopNetworkManager>.I.RoomStageChange((int)MonoBehaviourSingleton<QuestManager>.I.currentQuestID, (int)MonoBehaviourSingleton<QuestManager>.I.currentQuestSeriesIndex);
				while (CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected() && now_stage_id3 == MonoBehaviourSingleton<CoopManager>.I.coopStage.stageId)
				{
					yield return null;
				}
				matching_end_action(obj: true);
				uint map_id = MonoBehaviourSingleton<QuestManager>.I.GetCurrentMapId();
				if (MonoBehaviourSingleton<FieldManager>.I.currentMapID != map_id)
				{
					MonoBehaviourSingleton<FieldManager>.I.SetCurrentFieldMapID(map_id, 0f, 0f, 0f);
				}
				keep_record = true;
				if (MonoBehaviourSingleton<CoopManager>.IsValid())
				{
					MonoBehaviourSingleton<CoopManager>.I.OnQuestSeriesInterval();
				}
			}
		}
		else if (FieldManager.IsValidInGame())
		{
			isNewField = !MonoBehaviourSingleton<FieldManager>.I.CanJumpToMap(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
			portalID = MonoBehaviourSingleton<FieldManager>.I.currentPortalID;
			if (!is_back_transition && !MonoBehaviourSingleton<InGameManager>.I.isTransitionFieldReentry)
			{
				isTransitionFieldMap = true;
			}
			MonoBehaviourSingleton<InGameManager>.I.isTransitionFieldReentry = false;
			if (is_back_online_field && CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected())
			{
				int now_stage_id = MonoBehaviourSingleton<CoopManager>.I.coopStage.stageId;
				MonoBehaviourSingleton<CoopNetworkManager>.I.RoomStageChange(0, 0);
				while (CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected() && now_stage_id == MonoBehaviourSingleton<CoopManager>.I.coopStage.stageId)
				{
					yield return null;
				}
				matching_end_action(CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected());
				if (MonoBehaviourSingleton<CoopManager>.IsValid())
				{
					MonoBehaviourSingleton<CoopManager>.I.OnStageChangeInterval();
				}
			}
			else
			{
				uint currentPortalID = MonoBehaviourSingleton<FieldManager>.I.currentPortalID;
				CoopApp.EnterField(currentPortalID, 0u, delegate(bool is_m, bool is_c, bool is_r)
				{
					matching_end_action(is_c);
				});
			}
		}
		while (!is_send_read_story)
		{
			yield return null;
		}
		while (!matching_flag && !coopServerInvalidFlag)
		{
			yield return null;
		}
		if (coopServerInvalidFlag)
		{
			base.Initialize();
			yield break;
		}
		if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.mainChat != null)
		{
			MonoBehaviourSingleton<UIManager>.I.mainChat.Open();
		}
		if (!keep_record && MonoBehaviourSingleton<InGameRecorder>.IsValid())
		{
			Object.DestroyImmediate(MonoBehaviourSingleton<InGameRecorder>.I);
		}
		MonoBehaviourSingleton<StageManager>.I.UnloadStage();
		yield return MonoBehaviourSingleton<AppMain>.I.UnloadUnusedAssets(need_gc_collect: true);
		if (MonoBehaviourSingleton<InGameManager>.IsValid())
		{
			MonoBehaviourSingleton<InGameManager>.I.ClearAllDrop();
		}
		MonoBehaviourSingleton<AppMain>.I.mainCamera.get_gameObject().SetActive(true);
		if (MonoBehaviourSingleton<InGameManager>.I.requestEventData != null)
		{
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(MonoBehaviourSingleton<InGameManager>.I.requestEventData);
			MonoBehaviourSingleton<InGameManager>.I.requestEventData = null;
		}
		base.Initialize();
	}

	public override void StartSection()
	{
		if (coopServerInvalidFlag)
		{
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameProgress", this.get_gameObject(), "COOP_SERVER_INVALID");
		}
		else if ((isTransitionFieldMap || isEncounterBoss) && !MonoBehaviourSingleton<FieldManager>.I.useFastTravel)
		{
			bool flag = IsSrcOrDstChildRegion(portalID);
			bool flag2 = IsDifferentRegion(portalID);
			bool flag3 = IsSameField(portalID);
			bool flag4 = MonoBehaviourSingleton<QuestManager>.I.IsExplore();
			if ((MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.isStoryPortal) || flag3)
			{
				MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameProgress", this.get_gameObject(), "INGAME_MAIN");
			}
			else if (flag4)
			{
				ToExplore();
			}
			else if (flag2 && !flag)
			{
				WorldMapOpenNewRegion.EVENT_TYPE eventType = WorldMapOpenNewRegion.EVENT_TYPE.ONLY_CAMERA_MOVE;
				if (isNewField)
				{
					eventType = WorldMapOpenNewRegion.EVENT_TYPE.NONE;
				}
				WorldMapOpenNewRegion.SectionEventData user_data = new WorldMapOpenNewRegion.SectionEventData(eventType);
				MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameProgress", this.get_gameObject(), "NEW_REGION", user_data);
			}
			else if (IsJumpPortal(portalID))
			{
				WorldMapOpenNewField.EVENT_TYPE eventType2 = WorldMapOpenNewField.EVENT_TYPE.QUEST_TO_FIELD;
				WorldMapOpenNewField.SectionEventData user_data2 = new WorldMapOpenNewField.SectionEventData(eventType2, ENEMY_TYPE.BAT);
				MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameProgress", this.get_gameObject(), "NEW_FIELD", user_data2);
			}
			else
			{
				WorldMapOpenNewField.EVENT_TYPE eventType3 = WorldMapOpenNewField.EVENT_TYPE.ONLY_CAMERA_MOVE;
				if (isNewField)
				{
					eventType3 = ((flag2 && flag) ? WorldMapOpenNewField.EVENT_TYPE.OPEN_NEW_DUNGEON : WorldMapOpenNewField.EVENT_TYPE.NONE);
				}
				else if (isEncounterBoss)
				{
					eventType3 = WorldMapOpenNewField.EVENT_TYPE.ENCOUNTER_BOSS;
				}
				else if (flag)
				{
					eventType3 = WorldMapOpenNewField.EVENT_TYPE.EXIST_IN_DUNGEON;
				}
				WorldMapOpenNewField.SectionEventData user_data3 = new WorldMapOpenNewField.SectionEventData(eventType3, ENEMY_TYPE.BAT);
				MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameProgress", this.get_gameObject(), "NEW_FIELD", user_data3);
			}
		}
		else
		{
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameProgress", this.get_gameObject(), "INGAME_MAIN");
		}
		if (MonoBehaviourSingleton<FieldManager>.IsValid())
		{
			MonoBehaviourSingleton<FieldManager>.I.useFastTravel = false;
		}
	}

	private void ToExplore()
	{
		FieldMapTable.PortalTableData portalData = Singleton<FieldMapTable>.I.GetPortalData(MonoBehaviourSingleton<InGameManager>.I.beforePortalID);
		if (portalData == null)
		{
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameProgress", this.get_gameObject(), "INGAME_MAIN");
			return;
		}
		FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(portalData.dstMapID);
		if (fieldMapData == null)
		{
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameProgress", this.get_gameObject(), "INGAME_MAIN");
			return;
		}
		ExploreMapOpenNewField.EventData eventData = new ExploreMapOpenNewField.EventData();
		eventData.regionId = fieldMapData.regionId;
		eventData.portalId = MonoBehaviourSingleton<InGameManager>.I.beforePortalID;
		eventData.fromBoss = fromBossExplore;
		eventData.toBoss = isEncounterBoss;
		MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameProgress", this.get_gameObject(), "NEW_EXPLORE", eventData);
	}

	private static bool IsDifferentRegion(uint portalID)
	{
		if (!Singleton<FieldMapTable>.IsValid())
		{
			return false;
		}
		FieldMapTable.PortalTableData portalData = Singleton<FieldMapTable>.I.GetPortalData(portalID);
		if (portalData == null)
		{
			return false;
		}
		FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(portalData.srcMapID);
		FieldMapTable.FieldMapTableData fieldMapData2 = Singleton<FieldMapTable>.I.GetFieldMapData(portalData.dstMapID);
		if (fieldMapData == null || fieldMapData2 == null)
		{
			return false;
		}
		if (fieldMapData.regionId == fieldMapData2.regionId)
		{
			return false;
		}
		return true;
	}

	private static bool IsSameField(uint portalID)
	{
		if (!Singleton<FieldMapTable>.IsValid())
		{
			return false;
		}
		FieldMapTable.PortalTableData portalData = Singleton<FieldMapTable>.I.GetPortalData(portalID);
		if (portalData == null)
		{
			return false;
		}
		return portalData.srcMapID == portalData.dstMapID;
	}

	private static bool IsSrcOrDstChildRegion(uint portalID)
	{
		if (!Singleton<FieldMapTable>.IsValid())
		{
			return false;
		}
		FieldMapTable.PortalTableData portalData = Singleton<FieldMapTable>.I.GetPortalData(portalID);
		if (portalData == null)
		{
			return false;
		}
		FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(portalData.srcMapID);
		FieldMapTable.FieldMapTableData fieldMapData2 = Singleton<FieldMapTable>.I.GetFieldMapData(portalData.dstMapID);
		if (fieldMapData == null || fieldMapData2 == null)
		{
			return false;
		}
		if (fieldMapData.childRegionId == fieldMapData2.regionId)
		{
			return true;
		}
		if (!Singleton<RegionTable>.IsValid())
		{
			return false;
		}
		RegionTable.Data data = Singleton<RegionTable>.I.GetData(fieldMapData.regionId);
		RegionTable.Data data2 = Singleton<RegionTable>.I.GetData(fieldMapData2.regionId);
		if (data == null || data2 == null)
		{
			return false;
		}
		return data2.parentRegionId == fieldMapData.regionId || data.parentRegionId == fieldMapData2.regionId;
	}

	private static bool IsJumpPortal(uint portalID)
	{
		if (!Singleton<FieldMapTable>.IsValid())
		{
			return false;
		}
		FieldMapTable.PortalTableData portalData = Singleton<FieldMapTable>.I.GetPortalData(portalID);
		if (portalData == null)
		{
			return false;
		}
		if (0 < portalData.srcMapID)
		{
			return false;
		}
		if (portalID == 10000000)
		{
			return false;
		}
		return true;
	}
}
