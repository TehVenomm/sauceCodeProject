using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class QuestManager : MonoBehaviourSingleton<QuestManager>
{
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
		MAX_NUM
	}

	private const string DIFFICULTY_SPRITE_NAME_EASY = "Quest_classicon_beginner";

	private const string DIFFICULTY_SPRITE_NAME_NORMAL = "Quest_classicon_middle";

	private const string DIFFICULTY_SPRITE_NAME_HARD = "Quest_classicon_high";

	public bool needRequestOrderQuestList = true;

	private QuestStartData startData;

	private uint howToGetTargetQuestID;

	private PartyModel.ExploreInfo exploreInfo;

	private ExploreStatus explore;

	private float remainEndurance;

	private SelectQuestData current = new SelectQuestData();

	public uint currentDeliveryId;

	private int m_currentArenaId;

	private bool firstSetGetClearStatus = true;

	public QuestCollection questCollection = new QuestCollection();

	public static string SPR_NAME_MISSION_CLEAR = "Quest_crownicon_clear";

	public List<ClearStatusQuest> clearStatusQuest
	{
		get;
		private set;
	}

	public List<ClearStatusQuestEnemySpecies> clearStatusQuestEnemySpecies
	{
		get;
		private set;
	}

	public QuestResultUserCollection resultUserCollection
	{
		get;
		private set;
	}

	public List<QuestData> questList
	{
		get;
		private set;
	}

	public List<QuestData> orderQuestList
	{
		get;
		private set;
	}

	public List<QuestData> challengeList
	{
		get;
		private set;
	}

	public List<Network.EventData> eventList
	{
		get;
		private set;
	}

	public int carnivalEventId
	{
		get;
		private set;
	}

	public List<int> futureEventIdList
	{
		get;
		private set;
	}

	public List<Network.EventData> bingoEventList
	{
		get;
		private set;
	}

	public QuestCompleteData compData
	{
		get;
		private set;
	}

	public QuestRetireModel.Param retireData
	{
		get;
		private set;
	}

	public QuestArenaCompleteData arenaCompData
	{
		get;
		private set;
	}

	public float startTime
	{
		get;
		private set;
	}

	public List<int> missionNewClearFlag
	{
		get;
		private set;
	}

	public bool isTrial
	{
		get;
		private set;
	}

	public bool IsHowToGetAutoEvent => howToGetTargetQuestID != 0;

	public int currentArenaId => m_currentArenaId;

	public uint currentQuestID => current.questId;

	public uint currentQuestSeriesIndex => current.seriesIndex;

	public bool currentQuestIsFreeJoin => current.isFreeJoin;

	public QuestTable.QuestTableData currentQuestData => current.questData;

	public bool initialized
	{
		get;
		private set;
	}

	public bool isBackGachaQuest
	{
		get;
		set;
	}

	private QuestManager()
	{
		clearStatusQuest = new List<ClearStatusQuest>();
		clearStatusQuestEnemySpecies = new List<ClearStatusQuestEnemySpecies>();
		resultUserCollection = new QuestResultUserCollection();
		isBackGachaQuest = false;
	}

	public static bool IsValidInGame()
	{
		return MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.startData != null;
	}

	public static bool IsValidInGameExplore()
	{
		return IsValidInGame() && MonoBehaviourSingleton<QuestManager>.I.IsExplore();
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
		return IsValidInGame() && MonoBehaviourSingleton<QuestManager>.I.IsDefenseBattle();
	}

	public static bool IsValidInGameWaveMatch(bool isOnlyEvent = false)
	{
		return IsValidInGame() && MonoBehaviourSingleton<QuestManager>.I.IsWaveMatch(isOnlyEvent);
	}

	public static bool IsValidInGameSeries()
	{
		return IsValidInGame() && MonoBehaviourSingleton<QuestManager>.I.IsCurrentQuestTypeSeries();
	}

	public static bool IsValidInGameTrial()
	{
		return IsValidInGame() && MonoBehaviourSingleton<QuestManager>.I.isTrial;
	}

	public static bool IsValidTrial()
	{
		return MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.isTrial;
	}

	public void SetEventList(List<Network.EventData> _eventList)
	{
		eventList = _eventList;
		int i = 0;
		for (int count = eventList.Count; i < count; i++)
		{
			eventList[i].OnRecv();
			eventList[i].SetupEnum();
		}
	}

	public void SetCarnivalEventId(int carnivalEventId)
	{
		this.carnivalEventId = carnivalEventId;
	}

	public Network.EventData _GetEventData(int eventId)
	{
		int i = 0;
		for (int count = eventList.Count; i < count; i++)
		{
			if (eventList[i].eventId == eventId)
			{
				return eventList[i];
			}
		}
		return null;
	}

	public bool IsPlayableVersionEvent(int eventId)
	{
		Version nativeVersionFromName = NetworkNative.getNativeVersionFromName();
		return IsEventPlayableWith(eventId, nativeVersionFromName);
	}

	public bool IsEventPlayableWith(int eventId, Version version)
	{
		return _GetEventData(eventId)?.IsPlayableWith(version) ?? false;
	}

	public bool IsEventOpen(int eventId)
	{
		Network.EventData eventData = _GetEventData(eventId);
		if (eventData == null)
		{
			return false;
		}
		return eventData.GetRest() > 0;
	}

	public void SetFutureEventList(List<int> _idList)
	{
		futureEventIdList = _idList;
	}

	public bool IsFutureEvent(int eventId)
	{
		return futureEventIdList.Contains(eventId);
	}

	public void SetBingoEventList(List<Network.EventData> _bingoEventList)
	{
		bingoEventList = _bingoEventList;
		int i = 0;
		for (int count = bingoEventList.Count; i < count; i++)
		{
			bingoEventList[i].OnRecv();
		}
		if (MonoBehaviourSingleton<SceneSettingsManager>.IsValid())
		{
			MonoBehaviourSingleton<SceneSettingsManager>.I.SwitchBingoObjectsActivation(IsBingoPlayableEventExist());
		}
	}

	private Network.EventData _GetBingoEventData(int eventId)
	{
		int i = 0;
		for (int count = bingoEventList.Count; i < count; i++)
		{
			if (bingoEventList[i].eventId == eventId)
			{
				return bingoEventList[i];
			}
		}
		return null;
	}

	public bool IsBingoEventPlayableWith(int eventId, Version version)
	{
		return _GetBingoEventData(eventId)?.IsPlayableWith(version) ?? false;
	}

	public bool IsBingoEventOpen(int eventId)
	{
		Network.EventData eventData = _GetBingoEventData(eventId);
		if (eventData == null)
		{
			return false;
		}
		return eventData.GetRest() > 0;
	}

	public void StartTrial()
	{
		isTrial = true;
	}

	public void ClearTrial()
	{
		isTrial = false;
	}

	public void StartHowToGetAutoEvent(uint id)
	{
		howToGetTargetQuestID = id;
	}

	public void EndHowToGetAutoEvent()
	{
		howToGetTargetQuestID = 0u;
	}

	public bool IsDefenseBattle()
	{
		if (current.questData == null)
		{
			return false;
		}
		return current.questData.questType == QUEST_TYPE.DEFENSE || current.questData.questStyle == QUEST_STYLE.DEFENSE;
	}

	public bool IsWaveMatch(bool isOnlyEvent = false)
	{
		if (current.questData == null)
		{
			return false;
		}
		if (isOnlyEvent)
		{
			return current.questData.questType == QUEST_TYPE.EVENT_WAVE;
		}
		return current.questData.questType == QUEST_TYPE.WAVE || current.questData.questType == QUEST_TYPE.EVENT_WAVE;
	}

	public bool IsExplore()
	{
		return explore != null;
	}

	public int ExploreMapIdToIndex(uint mapId)
	{
		if (exploreInfo != null)
		{
			return exploreInfo.mapIds.IndexOf((int)mapId);
		}
		return 0;
	}

	public int ExploreMapIndexToId(int index)
	{
		if (exploreInfo != null && index < exploreInfo.mapIds.Count)
		{
			return exploreInfo.mapIds[index];
		}
		return 0;
	}

	public int GetExploreStartMapId()
	{
		if (exploreInfo != null && exploreInfo.mapIds != null && exploreInfo.mapIds.Count > 0)
		{
			return exploreInfo.mapIds[0];
		}
		return 0;
	}

	public int GetExploreBossBatlleMapId()
	{
		if (exploreInfo != null)
		{
			return exploreInfo.mapIds[exploreInfo.mapIds.Count - 1];
		}
		return 0;
	}

	public int GetExploreBossAppearMapId()
	{
		if (explore == null)
		{
			return 0;
		}
		return explore.GetCurrentBossMapId();
	}

	public EXPLORE_HISTORY_TYPE GetExploreHistoryType(int mapId)
	{
		if (explore == null)
		{
			return EXPLORE_HISTORY_TYPE.NONE;
		}
		return explore.GetHistoryTypeOfMap(mapId);
	}

	public float GetExploreBossMoveRemainTime()
	{
		if (explore == null)
		{
			return 0f;
		}
		return explore.bossMoveRemainTime;
	}

	public void UpdateExploreBossMoveRemainTime(float time)
	{
		if (explore != null)
		{
			explore.UpdateBossMoveRemainTime(time);
		}
	}

	public void UpdateBossTraceHistory(int mapId, int lastCount, string playerName, bool reserve)
	{
		if (explore != null)
		{
			explore.UpdateBossTraceMapIdHistory(mapId, lastCount, playerName, reserve);
		}
	}

	public ExploreStatus.TraceInfo[] GetBossTraceHistory()
	{
		if (explore == null)
		{
			return null;
		}
		return explore.GetTraceInfoHistory();
	}

	public ExploreStatus.TraceInfo GetReservedTraceInfo()
	{
		if (explore == null)
		{
			return null;
		}
		return explore.reservedTraceInfo;
	}

	public void CompleteBossTracePopup()
	{
		if (explore != null)
		{
			explore.CompleteShowedTrace();
		}
	}

	public float GetExploreHostDCRemainTime()
	{
		if (explore == null)
		{
			return 0f;
		}
		return explore.hostDCRemainTime;
	}

	public void UpdateExploreHostDCTime(float remainTime)
	{
		if (explore != null)
		{
			explore.UpdateHostDCRemainTime(remainTime);
		}
	}

	public string GetCurrentBossMapStageName()
	{
		if (explore == null)
		{
			return null;
		}
		int currentBossMapId = explore.GetCurrentBossMapId();
		FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData((uint)currentBossMapId);
		if (fieldMapData != null)
		{
			if (!string.IsNullOrEmpty(fieldMapData.happenStageName))
			{
				return fieldMapData.happenStageName;
			}
			fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData((uint)GetExploreBossBatlleMapId());
			if (fieldMapData != null)
			{
				return fieldMapData.stageName;
			}
		}
		return null;
	}

	public ExploreBossStatus GetExploreBossStatus()
	{
		if (explore != null)
		{
			return explore.bossStatus;
		}
		return null;
	}

	public void UpdateBossAppearMap()
	{
		if (explore != null)
		{
			explore.UpdateBossMap();
			if (MonoBehaviourSingleton<InGameProgress>.IsValid() && explore.GetCurrentBossMapId() == MonoBehaviourSingleton<FieldManager>.I.currentMapID)
			{
				MonoBehaviourSingleton<InGameProgress>.I.ExploreHappenQuestDirection();
			}
		}
	}

	public bool IsBossAppearMap(int mapId)
	{
		if (explore == null)
		{
			return false;
		}
		return explore.IsBossAppearMap(mapId);
	}

	public bool IsExploreBossMap()
	{
		if (exploreInfo != null)
		{
			return exploreInfo.mapIds[exploreInfo.mapIds.Count - 1] == MonoBehaviourSingleton<FieldManager>.I.currentMapID;
		}
		return false;
	}

	public void SyncExplorePortalPoint(Coop_Model_RoomSyncAllPortalPoint model)
	{
		if (explore != null)
		{
			explore.SyncPortalPoint(model);
		}
	}

	public void UpdatePortalUsedFlag(int portalId)
	{
		if (explore != null)
		{
			explore.UpdatePortalUsedFlag(portalId);
		}
	}

	public void UpdateExplorePortalPoint(int portalId, int point, int x, int y)
	{
		if (explore != null)
		{
			UpdatePortalPointForExplore(portalId, point, x, y);
		}
	}

	private void SetExplorePortalPoint(int portalId, int point)
	{
		if (explore != null)
		{
			explore.UpdatePortalPoint(portalId, point, false);
		}
	}

	private void RollbackExplorePortalPoint(int portalId, int point)
	{
		if (explore != null)
		{
			explore.UpdatePortalPoint(portalId, point, true);
		}
	}

	private unsafe void UpdatePortalPointForExplore(int portalId, int point, int x, int z)
	{
		ExplorePortalPoint portal = explore.GetPortalData(portalId);
		if (portal != null)
		{
			int prevPoint = portal.point;
			if (point > prevPoint)
			{
				uint portalPoint = portal.portalData.portalPoint;
				if (point > portalPoint)
				{
					point = (int)portalPoint;
				}
				FieldMapPortalInfo portalInfo = MonoBehaviourSingleton<FieldManager>.I.GetPortalInfo((uint)portalId);
				if (portalInfo == null)
				{
					FieldMapTable.PortalTableData portalData = Singleton<FieldMapTable>.I.GetPortalData((uint)portalId);
					portalInfo = MonoBehaviourSingleton<FieldManager>.I.GetPortalInfo(portalData.linkPortalId);
					if (portalInfo != null)
					{
						x = Mathf.RoundToInt(portalInfo.portalData.srcX);
						z = Mathf.RoundToInt(portalInfo.portalData.srcZ);
					}
				}
				if (portalInfo != null)
				{
					prevPoint = portalInfo.GetNowPortalPoint();
				}
				if (point >= portalPoint)
				{
					FieldPortal fieldPortal = MonoBehaviourSingleton<WorldMapManager>.I.GetFieldPortal(portalId);
					if (fieldPortal == null || fieldPortal.point < portalPoint)
					{
						SetExplorePortalPoint(portalId, point);
						_003CUpdatePortalPointForExplore_003Ec__AnonStorey679 _003CUpdatePortalPointForExplore_003Ec__AnonStorey;
						MonoBehaviourSingleton<FieldManager>.I.SendFieldQuestOpenPortal(portalId, new Action<bool, Error>((object)_003CUpdatePortalPointForExplore_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
					}
					else
					{
						SetExplorePortalPoint(portalId, point);
						if (!PlayPortalPointEffect(portalInfo, point - prevPoint, x, z) && ShouldShowPortalOpenNotification())
						{
							string text = string.Empty;
							FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(portal.portalData.dstMapID);
							if (fieldMapData != null)
							{
								text = fieldMapData.mapName;
							}
							string text2 = StringTable.Format(STRING_CATEGORY.IN_GAME, 6002u, text);
							UIInGamePopupDialog.PushOpen(text2, false, 1.4f);
						}
					}
				}
				else
				{
					SetExplorePortalPoint(portalId, point);
					PlayPortalPointEffect(portalInfo, point - prevPoint, x, z);
				}
			}
		}
	}

	private bool ShouldShowPortalOpenNotification()
	{
		return !MonoBehaviourSingleton<QuestManager>.I.IsExploreBossMap() && MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameProgress>.I.progressEndType == InGameProgress.PROGRESS_END_TYPE.NONE;
	}

	private bool PlayPortalPointEffect(FieldMapPortalInfo portalInfo, int getPoint, int portalX, int portalZ)
	{
		if (portalInfo == null)
		{
			return false;
		}
		if (MonoBehaviourSingleton<FieldManager>.I.AddPortalPointToPortalInfo(portalInfo, getPoint))
		{
			MonoBehaviourSingleton<CoopManager>.I.coopStage.fieldRewardPool.SendFieldDrop(null);
		}
		if (getPoint <= 0)
		{
			return false;
		}
		if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameProgress>.I.portalObjectList != null)
		{
			Coop_Model_EnemyDefeat coop_Model_EnemyDefeat = new Coop_Model_EnemyDefeat();
			coop_Model_EnemyDefeat.ppt = getPoint;
			coop_Model_EnemyDefeat.x = portalX;
			coop_Model_EnemyDefeat.z = portalZ;
			MonoBehaviourSingleton<InGameProgress>.I.CreatePortalPoint(portalInfo, coop_Model_EnemyDefeat);
			return true;
		}
		return false;
	}

	public bool PortalIsUsedInExplore(int portalId)
	{
		if (explore != null)
		{
			ExplorePortalPoint portalData = explore.GetPortalData(portalId);
			if (portalData != null)
			{
				return ExplorePortalPoint.USEDFLAG_CLOSED != portalData.used;
			}
		}
		return false;
	}

	public bool PortalIsPassedInExplore(int portalId)
	{
		if (explore != null)
		{
			ExplorePortalPoint portalData = explore.GetPortalData(portalId);
			if (portalData != null)
			{
				return ExplorePortalPoint.USEDFLAG_PASSED == portalData.used;
			}
		}
		return false;
	}

	public bool MapIsTraveldInExplore(int mapId)
	{
		if (explore != null)
		{
			if (GetExploreStartMapId() == mapId)
			{
				return true;
			}
			List<ExplorePortalPoint> portalDataFromMapId = explore.GetPortalDataFromMapId(mapId);
			bool traveld = false;
			portalDataFromMapId.ForEach(delegate(ExplorePortalPoint o)
			{
				traveld = (traveld || ExplorePortalPoint.USEDFLAG_CLOSED != o.used);
			});
			return traveld;
		}
		return false;
	}

	public bool TraveldAllPortalExplore(int mapId)
	{
		if (explore != null)
		{
			if (GetExploreStartMapId() == mapId)
			{
				return true;
			}
			List<ExplorePortalPoint> portalDataFromSrcMapId = explore.GetPortalDataFromSrcMapId(mapId);
			bool traveld = true;
			portalDataFromSrcMapId.ForEach(delegate(ExplorePortalPoint o)
			{
				if (ExplorePortalPoint.USEDFLAG_CLOSED == o.used)
				{
					traveld = false;
				}
			});
			return traveld;
		}
		return false;
	}

	public void UpdateLastPortalData(FieldMapTable.PortalTableData portalData)
	{
		if (explore != null)
		{
			explore.UpdateLastPortal(portalData);
		}
	}

	public uint GetLastPortalId()
	{
		if (explore != null)
		{
			return explore.GetLastPortalId();
		}
		return 0u;
	}

	public void UpdateExploreBossStatus(Enemy enemy)
	{
		if (explore != null)
		{
			explore.UpdateBossStatus(enemy);
		}
	}

	public void SyncExploreBossStatus(Coop_Model_RoomSyncExploreBoss model)
	{
		if (explore != null)
		{
			explore.SyncBoss(model);
			explore.ResetMemberEncountered();
			if (MonoBehaviourSingleton<InGameProgress>.IsValid() && explore.GetCurrentBossMapId() == MonoBehaviourSingleton<FieldManager>.I.currentMapID)
			{
				MonoBehaviourSingleton<InGameProgress>.I.ExploreHappenQuestDirection();
			}
		}
	}

	public void SyncExploreBossMap(Coop_Model_RoomSyncExploreBossMap model)
	{
		if (explore != null)
		{
			explore.SyncBossMap(model);
			if (MonoBehaviourSingleton<InGameProgress>.IsValid() && explore.GetCurrentBossMapId() == MonoBehaviourSingleton<FieldManager>.I.currentMapID)
			{
				MonoBehaviourSingleton<InGameProgress>.I.ExploreHappenQuestDirection();
			}
		}
	}

	public void SetExploreBossDead(Coop_Model_RoomExploreBossDead model)
	{
		if (explore != null)
		{
			explore.SetBossDead(model);
		}
	}

	public bool IsExploreBossDead()
	{
		if (explore != null)
		{
			return explore.isBossDead;
		}
		return false;
	}

	public List<int> GetExploreBossBreakIdList()
	{
		if (explore != null)
		{
			return explore.bossStatus.GetBreakIds();
		}
		return new List<int>();
	}

	public ExplorePlayerStatus GetMyExplorePlayerStatus()
	{
		if (explore != null)
		{
			return explore.GetMyPlayerStatus();
		}
		return null;
	}

	public void ActivateExplorePlayerStatus(CoopClient coopClient)
	{
		if (explore != null)
		{
			explore.ActivatePlayerStatus(coopClient);
		}
	}

	public void RemoveExplorePlayerStatus(CoopClient coopClient)
	{
		if (explore != null)
		{
			explore.RemovePlayerStatus(coopClient);
		}
	}

	public void UpdateExplorePlayerStatus(CoopClient coopClient, Coop_Model_RoomSyncPlayerStatus status)
	{
		if (explore != null)
		{
			explore.UpdatePlayerStatus(coopClient, status);
		}
	}

	public void UpdateExplorePlayerStatus(CoopClient coopClient)
	{
		if (explore != null)
		{
			explore.UpdatePlayerStatus(coopClient);
		}
	}

	public void UpdateExploreTotalDamageToBoss(CoopClient coopClient, int total)
	{
		if (explore != null)
		{
			explore.UpdateTotalDamageToBoss(coopClient, total);
		}
	}

	public void UpdateExploreTotalDamageToBoss(int userId, int total)
	{
		if (explore != null)
		{
			explore.UpdateTotalDamageToBoss(userId, total);
		}
	}

	public List<ExplorePlayerStatus> GetExplorePlayerStatusList()
	{
		if (explore != null)
		{
			return explore.GetEnabledPlayerStatusList();
		}
		return null;
	}

	public ExplorePlayerStatus GetExplorePlayerStatus(int userId)
	{
		if (explore != null)
		{
			return explore.GetPlayerStatus(userId);
		}
		return null;
	}

	public bool IsEncountered()
	{
		if (explore == null)
		{
			return false;
		}
		return explore.isEncountered;
	}

	public void SetMemberEncounteredMap(int mapId)
	{
		if (explore != null)
		{
			explore.SetEncountered(mapId);
		}
	}

	public void ResetMemberEncountered()
	{
		if (explore == null)
		{
			explore.ResetMemberEncountered();
		}
	}

	public ExploreStatus GetExploreStatus()
	{
		return explore;
	}

	public void SetExploreInfo(PartyModel.ExploreInfo exploreInfo)
	{
		this.exploreInfo = exploreInfo;
	}

	public void SetExploreStatus(ExploreStatus explore)
	{
		this.explore = explore;
	}

	public int[] GetExploreDisplayIndices()
	{
		int[] array = new int[4]
		{
			0,
			1,
			2,
			3
		};
		if (explore == null)
		{
			return array;
		}
		int id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
		bool flag = true;
		List<ExplorePlayerStatus> enabledPlayerStatusList = explore.GetEnabledPlayerStatusList();
		for (int i = 0; i < enabledPlayerStatusList.Count; i++)
		{
			ExplorePlayerStatus explorePlayerStatus = enabledPlayerStatusList[i];
			CoopClient coopClient = explorePlayerStatus.coopClient;
			if (!(null == coopClient))
			{
				int num = coopClient.slotIndex;
				if (flag)
				{
					num++;
				}
				if (id == explorePlayerStatus.userId)
				{
					num = 0;
					flag = false;
				}
				if (0 <= num && array.Length > num)
				{
					array[i] = num;
				}
			}
		}
		return array;
	}

	public int GetExploreMapId(int statusIndex)
	{
		if (explore == null)
		{
			return -1;
		}
		List<ExplorePlayerStatus> enabledPlayerStatusList = explore.GetEnabledPlayerStatusList();
		if (enabledPlayerStatusList.Count <= statusIndex)
		{
			return -1;
		}
		CoopClient coopClient = enabledPlayerStatusList[statusIndex].coopClient;
		if (null == coopClient)
		{
			return -1;
		}
		return ExploreMapIndexToId(coopClient.exploreMapIndex);
	}

	public void UpdatePassedPortal()
	{
		if (explore != null)
		{
			explore.UpdatePassedPortal();
		}
	}

	public uint GetBossMapId()
	{
		if (explore == null)
		{
			return 0u;
		}
		return (uint)explore.GetCurrentBossMapId();
	}

	public float GetRemainEndurance()
	{
		return remainEndurance;
	}

	public void UpdateTotalDamageToEndurance(float endurance)
	{
		remainEndurance = endurance;
		if (MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			MonoBehaviourSingleton<InGameProgress>.I.SetDefenseBattleEndurance(endurance);
		}
	}

	public void ClearPlayData()
	{
		startData = null;
		compData = null;
		retireData = null;
		startTime = 0f;
		missionNewClearFlag = null;
		resultUserCollection.Clear();
		explore = null;
		exploreInfo = null;
		arenaCompData = null;
		remainEndurance = 0f;
		isTrial = false;
	}

	public void SaveLastNewClearQuest(int status)
	{
		if (status < 3 && (GameSaveData.instance.lastNewClearQusetID != (int)currentQuestID || GameSaveData.instance.lastQusetID != (int)currentQuestID))
		{
			GameSaveData.instance.lastNewClearQusetID = (int)currentQuestID;
			GameSaveData.instance.lastQusetID = (int)currentQuestID;
			GameSaveData.Save();
		}
	}

	public void SaveLastQuestId(int quest_id)
	{
		if (GameSaveData.instance.lastQusetID != quest_id)
		{
			GameSaveData.instance.lastQusetID = quest_id;
			GameSaveData.Save();
		}
	}

	public bool IsCurrentQuestTypeSeries()
	{
		if (current.questData == null)
		{
			return false;
		}
		return current.questData.questType == QUEST_TYPE.SERIES;
	}

	public void SetCurrentQuestID(uint quest_id, bool is_free_join = true)
	{
		SaveLastQuestId((int)quest_id);
		current.isFreeJoin = is_free_join;
		current.questId = quest_id;
		if (quest_id != 0)
		{
			current.questData = Singleton<QuestTable>.I.GetQuestData(quest_id);
		}
		else
		{
			current.questData = null;
		}
		current.limitTime = ((current.questData == null) ? 0f : current.questData.limitTime);
		current.seriesIndex = 0u;
	}

	public void SetCurrentQuestSeriesIndex(uint index)
	{
		current.seriesIndex = index;
	}

	public void SetCurrentQuestLimitTime(float time)
	{
		current.limitTime = time;
	}

	public void SetCurrentArenaId(int arenaId)
	{
		m_currentArenaId = arenaId;
	}

	public int GetCurrentQuestId()
	{
		if (current.questData == null)
		{
			return 0;
		}
		return (int)current.questId;
	}

	public string GetCurrentQuestName()
	{
		if (current.questData == null)
		{
			return string.Empty;
		}
		return current.questData.questText;
	}

	public QUEST_TYPE GetCurrentQuestType()
	{
		if (current.questData == null)
		{
			return QUEST_TYPE.NORMAL;
		}
		return current.questData.questType;
	}

	public QUEST_STYLE GetCurrentQuestStyle()
	{
		if (current.questData == null)
		{
			return QUEST_STYLE.NORMAL;
		}
		return current.questData.questStyle;
	}

	public uint GetCurrentMapId()
	{
		if (current.questData == null)
		{
			return 0u;
		}
		return current.questData.mapId;
	}

	public string GetCurrentQuestStageName()
	{
		if (current.questData == null)
		{
			return string.Empty;
		}
		return current.questData.stageName[current.seriesIndex];
	}

	public int GetCurrentQuestEnemyID()
	{
		if (current.questData == null)
		{
			return 0;
		}
		return current.questData.enemyID[current.seriesIndex];
	}

	public int GetCurrentQuestEnemyID(int index)
	{
		if (current.questData == null)
		{
			return 0;
		}
		if (index >= current.questData.enemyID.Length)
		{
			return 0;
		}
		return current.questData.enemyID[index];
	}

	public int GetCurrentQuestEnemyLv()
	{
		if (current.questData == null)
		{
			return 0;
		}
		int num = current.questData.enemyLv[current.seriesIndex];
		if (num == 0 && Singleton<EnemyTable>.IsValid())
		{
			EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)GetCurrentQuestEnemyID());
			if (enemyData != null)
			{
				num = enemyData.level;
			}
		}
		return num;
	}

	public int GetCurrentQuestEnemyLv(int index)
	{
		if (current.questData == null)
		{
			return 0;
		}
		if (index >= current.questData.enemyLv.Length)
		{
			return 0;
		}
		return current.questData.enemyLv[index];
	}

	public int GetCurrentQuestBGMID()
	{
		if (current.questData == null)
		{
			return 0;
		}
		return current.questData.bgmID[current.seriesIndex];
	}

	public float GetCurrentQuestLimitTime()
	{
		if (current.questData == null)
		{
			return 0f;
		}
		return current.limitTime;
	}

	public int GetCurrentQuestSeriesNum()
	{
		if (current.questData == null)
		{
			return 0;
		}
		return current.questData.seriesNum;
	}

	public bool IsLastEnemyCurrentQuestSeries()
	{
		if (current.questData == null)
		{
			return false;
		}
		if (!IsCurrentQuestTypeSeries())
		{
			return false;
		}
		return current.seriesIndex >= current.questData.seriesNum - 1;
	}

	public bool IsOverCurrentQuestSeries()
	{
		if (current.questData == null)
		{
			return false;
		}
		if (GetCurrentQuestSeriesNum() <= 1)
		{
			return false;
		}
		return current.seriesIndex >= current.questData.seriesNum;
	}

	public int GetCurrentQuestMaxTeamMemberNum()
	{
		if (current.questData == null)
		{
			return 4;
		}
		return current.questData.userNumLimit;
	}

	public QuestStartData.EnemyReward GetCurrentQuestEnemyReward()
	{
		if (startData == null)
		{
			return null;
		}
		return startData.enemy[(int)current.seriesIndex];
	}

	public CLEAR_STATUS GetClearStatusQuest(uint questId)
	{
		CLEAR_STATUS result = CLEAR_STATUS.NEW;
		ClearStatusQuest clearStatusQuest = this.clearStatusQuest.Find((ClearStatusQuest data) => data.questId == questId);
		if (clearStatusQuest != null)
		{
			result = (CLEAR_STATUS)clearStatusQuest.questStatus;
		}
		return result;
	}

	public bool IsClearQuest(uint questId)
	{
		CLEAR_STATUS clearStatusQuest = GetClearStatusQuest(questId);
		CLEAR_STATUS cLEAR_STATUS = clearStatusQuest;
		if (cLEAR_STATUS == CLEAR_STATUS.CLEAR || cLEAR_STATUS == CLEAR_STATUS.ALL_CLEAR)
		{
			return true;
		}
		return false;
	}

	public bool IsOpenedQuest(uint questId)
	{
		QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(questId);
		if (questData == null)
		{
			return false;
		}
		if (questData.appearQuestId != 0 && !IsClearQuest(questData.appearQuestId))
		{
			return false;
		}
		if (questData.appearDeliveryId != 0 && !MonoBehaviourSingleton<DeliveryManager>.I.IsClearDelivery(questData.appearDeliveryId))
		{
			return false;
		}
		return true;
	}

	public bool IsTutorialCurrentQuest()
	{
		if (!IsValidInGame() || !MonoBehaviourSingleton<UserInfoManager>.IsValid() || !MonoBehaviourSingleton<CoopManager>.IsValid() || MonoBehaviourSingleton<CoopManager>.I.coopMyClient == null)
		{
			return false;
		}
		if (current.questData != null && MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialQuestId == (int)current.questData.questID && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA_QUEST_WIN))
		{
			return MonoBehaviourSingleton<CoopManager>.I.coopMyClient.isPartyOwner;
		}
		return false;
	}

	public bool IsTutorialOrderQuest(uint questId)
	{
		if (MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA1) && MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialQuestId == (int)questId)
		{
			return !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA_QUEST_END);
		}
		return false;
	}

	public ClearStatusQuestEnemySpecies GetClearStatusQuestEnemySpecies(uint questId)
	{
		QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(questId);
		if (questData == null)
		{
			return null;
		}
		EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)questData.GetMainEnemyID());
		if (enemyData == null)
		{
			return null;
		}
		return clearStatusQuestEnemySpecies.Find((ClearStatusQuestEnemySpecies data) => data.enemySpecies == enemyData.enemySpecies);
	}

	public QuestInfoData[] GetQuestInfoData()
	{
		if (questList == null || questList.Count == 0)
		{
			return null;
		}
		List<QuestInfoData> open_quest_ary = new List<QuestInfoData>();
		questList.ForEach(delegate(QuestData quest)
		{
			QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint)quest.questId);
			if (questData != null && questData.questType != QUEST_TYPE.STORY)
			{
				ClearStatusQuest clearStatusQuest = this.clearStatusQuest.Find((ClearStatusQuest data) => data.questId == quest.questId);
				if (clearStatusQuest == null)
				{
					open_quest_ary.Add(new QuestInfoData(questData, quest, null));
				}
				else if (clearStatusQuest.questStatus > 0)
				{
					open_quest_ary.Add(new QuestInfoData(questData, quest, clearStatusQuest.missionStatus.ToArray()));
				}
			}
		});
		if (open_quest_ary.Count == 0)
		{
			return null;
		}
		return open_quest_ary.ToArray();
	}

	public QuestInfoData[] GetOrderQuestInfoData()
	{
		if (orderQuestList == null)
		{
			return null;
		}
		List<QuestInfoData> open_quest_ary = new List<QuestInfoData>();
		orderQuestList.ForEach(delegate(QuestData order)
		{
			QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint)order.questId);
			if (questData != null)
			{
				ClearStatusQuest clearStatusQuest = this.clearStatusQuest.Find((ClearStatusQuest data) => data.questId == order.questId);
				if (clearStatusQuest == null)
				{
					open_quest_ary.Add(new QuestInfoData(questData, order, null));
				}
				else if (clearStatusQuest.questStatus > 0)
				{
					open_quest_ary.Add(new QuestInfoData(questData, order, clearStatusQuest.missionStatus.ToArray()));
				}
			}
		});
		if (open_quest_ary.Count == 0)
		{
			return null;
		}
		return open_quest_ary.ToArray();
	}

	public QuestInfoData GetQuestInfoData(uint quest_id)
	{
		QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(quest_id);
		if (questData == null)
		{
			return null;
		}
		List<QuestData> list = null;
		list = ((questData.questType != QUEST_TYPE.ORDER) ? questList : orderQuestList);
		if (list == null)
		{
			return null;
		}
		QuestData questData2 = list.Find((QuestData q) => q.questId == quest_id);
		if (questData2 == null)
		{
			return null;
		}
		ClearStatusQuest clearStatusQuest = this.clearStatusQuest.Find((ClearStatusQuest data) => data.questId == quest_id);
		if (clearStatusQuest != null && clearStatusQuest.questStatus > 0)
		{
			return new QuestInfoData(questData, questData2, clearStatusQuest.missionStatus.ToArray());
		}
		return new QuestInfoData(questData, questData2, null);
	}

	public QuestInfoData GetQuestChallengeInfoData(uint quest_id)
	{
		List<QuestData> challengeList = this.challengeList;
		if (challengeList == null)
		{
			return null;
		}
		QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(quest_id);
		if (questData == null)
		{
			return null;
		}
		QuestData questData2 = challengeList.Find((QuestData q) => q.questId == quest_id);
		if (questData2 == null)
		{
			return null;
		}
		return CreateQuestChallengeInfoData(questData2, questData);
	}

	public QuestInfoData CreateQuestChallengeInfoData(QuestData questData, QuestTable.QuestTableData tableData)
	{
		int questId = (int)tableData.questID;
		if (questId != questData.questId)
		{
			Debug.LogWarning((object)("Network.QuestDataとQuestTableDataのクエストIDが一致していません questData = " + questData.questId + " tableData = " + questId));
		}
		ClearStatusQuest clearStatusQuest = this.clearStatusQuest.Find((ClearStatusQuest data) => data.questId == questId);
		if (clearStatusQuest != null && clearStatusQuest.questStatus > 0)
		{
			return new QuestInfoData(tableData, questData, clearStatusQuest.missionStatus.ToArray());
		}
		return new QuestInfoData(tableData, questData, null);
	}

	public QuestInfoData GetExploreQuestInfo(uint quest_id)
	{
		QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(quest_id);
		if (questData == null)
		{
			Log.Error("QuestTableDataがありません");
			return null;
		}
		QuestData questData2 = new QuestData();
		questData2.questId = (int)quest_id;
		questData2.crystalNum = 0;
		questData2.order = null;
		QuestData questData3 = questData2;
		if (questData3 == null)
		{
			Log.Error("対象のEventQuestDataがありません");
			return null;
		}
		return new QuestInfoData(questData, questData3, null);
	}

	public ClearStatusQuest GetClearStatusQuestData(uint quest_id)
	{
		QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(quest_id);
		if (questData == null)
		{
			return null;
		}
		return clearStatusQuest.Find((ClearStatusQuest data) => data.questId == quest_id);
	}

	public bool ExistsExploreEvent()
	{
		for (int i = 0; i < eventList.Count; i++)
		{
			if (eventList[i].eventType == 4)
			{
				return true;
			}
		}
		return false;
	}

	public int[] GetExploreEventIds()
	{
		List<int> list = new List<int>(eventList.Count);
		for (int i = 0; i < eventList.Count; i++)
		{
			if (eventList[i].eventType == 4)
			{
				list.Add(eventList[i].eventId);
			}
		}
		return list.ToArray();
	}

	public bool IsValidStartingStory(uint quest_id)
	{
		return false;
	}

	private void SendGetQuestList(QuestListModel.RequestSendForm send, Action<bool> call_back)
	{
		Protocol.Send(QuestListModel.URL, send, delegate(QuestListModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
				if (send.req_q > 0)
				{
					questList = ret.result.quests;
				}
				if (send.req_gq > 0)
				{
					orderQuestList = ret.result.order;
				}
				if (send.req_eq <= 0)
				{
					goto IL_0075;
				}
				goto IL_0075;
			}
			goto IL_0134;
			IL_0134:
			call_back(obj);
			return;
			IL_0075:
			if (send.req_e > 0)
			{
				SetEventList(ret.result.events);
				SetCarnivalEventId(ret.result.carnivalEventId);
				SetFutureEventList(ret.result.futureEventIds);
			}
			if (send.req_d > 0 && MonoBehaviourSingleton<DeliveryManager>.IsValid())
			{
				MonoBehaviourSingleton<DeliveryManager>.I.UpdateDeliveryReaminTime(ret.result.dailyRemainTime, ret.result.weeklyRemainTime);
			}
			if (send.req_bingo > 0)
			{
				SetBingoEventList(ret.result.bingoEvents);
			}
			goto IL_0134;
		}, string.Empty);
	}

	public void SendGetQuestList(Action<bool> call_back)
	{
		QuestListModel.RequestSendForm requestSendForm = new QuestListModel.RequestSendForm();
		requestSendForm.req_gq = 1;
		SendGetQuestList(requestSendForm, call_back);
	}

	public void SendGetDeliveryList(Action<bool> call_back)
	{
		QuestListModel.RequestSendForm requestSendForm = new QuestListModel.RequestSendForm();
		requestSendForm.req_d = 1;
		SendGetQuestList(requestSendForm, call_back);
	}

	public void SendGetEventList(Action<bool> call_back)
	{
		QuestListModel.RequestSendForm requestSendForm = new QuestListModel.RequestSendForm();
		requestSendForm.req_d = 1;
		requestSendForm.req_e = 1;
		SendGetQuestList(requestSendForm, call_back);
	}

	public void SendGetBingoEventList(Action<bool> call_back)
	{
		QuestListModel.RequestSendForm requestSendForm = new QuestListModel.RequestSendForm();
		requestSendForm.req_d = 1;
		requestSendForm.req_bingo = 1;
		SendGetQuestList(requestSendForm, call_back);
	}

	public Network.EventData FindArenaDataFromList()
	{
		int i = 0;
		for (int count = MonoBehaviourSingleton<QuestManager>.I.eventList.Count; i < count; i++)
		{
			Network.EventData eventData = MonoBehaviourSingleton<QuestManager>.I.eventList[i];
			if (eventData.eventType == 15)
			{
				return eventData;
			}
		}
		return null;
	}

	public List<Network.EventData> GetBingoDataList()
	{
		return bingoEventList;
	}

	public unsafe List<Network.EventData> GetValidBingoDataListInSection()
	{
		if (!MonoBehaviourSingleton<GameSceneManager>.IsValid())
		{
			return new List<Network.EventData>();
		}
		EVENT_DISPLAY_LOCATION_TYPE excludeLocationType;
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene")
		{
			excludeLocationType = EVENT_DISPLAY_LOCATION_TYPE.HOME;
		}
		else
		{
			excludeLocationType = EVENT_DISPLAY_LOCATION_TYPE.FIELD;
		}
		_003CGetValidBingoDataListInSection_003Ec__AnonStorey687 _003CGetValidBingoDataListInSection_003Ec__AnonStorey;
		return bingoEventList.Where(new Func<Network.EventData, bool>((object)_003CGetValidBingoDataListInSection_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)).ToList();
	}

	public bool IsBingoPlayableEventExist()
	{
		List<Network.EventData> validBingoDataListInSection = GetValidBingoDataListInSection();
		if (validBingoDataListInSection == null)
		{
			return false;
		}
		if (validBingoDataListInSection.Count <= 0)
		{
			return false;
		}
		int i = 0;
		for (int count = validBingoDataListInSection.Count; i < count; i++)
		{
			if (validBingoDataListInSection[i].GetRest() >= 1)
			{
				return true;
			}
		}
		return false;
	}

	public void SendGetExploreList(Action<bool> call_back)
	{
		QuestListModel.RequestSendForm requestSendForm = new QuestListModel.RequestSendForm();
		requestSendForm.req_d = 1;
		requestSendForm.req_e = 1;
		requestSendForm.req_eq = 1;
		SendGetQuestList(requestSendForm, call_back);
	}

	public static string GenerateQuestToken()
	{
		return GenerateQuestToken(DateTime.Now.GetHashCode());
	}

	public unsafe static string GenerateQuestToken(int key)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(key.ToString());
		byte[] array = MD5.Create().ComputeHash(bytes);
		byte[] source = array;
		if (_003C_003Ef__am_0024cache1E == null)
		{
			_003C_003Ef__am_0024cache1E = new Func<byte, string>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		return string.Concat(source.Select<byte, string>(_003C_003Ef__am_0024cache1E).ToArray());
	}

	public void SendQuestStart(int questId, int equip_set_no, bool free_join, Action<bool> call_back)
	{
		ClearPlayData();
		QuestStartModel.RequestSendForm requestSendForm = new QuestStartModel.RequestSendForm();
		requestSendForm.qid = questId;
		requestSendForm.qt = GenerateQuestToken();
		requestSendForm.setNo = equip_set_no;
		requestSendForm.crystalCL = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.Crystal;
		requestSendForm.free = (free_join ? 1 : 0);
		requestSendForm.dId = (int)currentDeliveryId;
		currentDeliveryId = 0u;
		requestSendForm.d = NetworkNative.getUniqueDeviceId();
		if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.intervalTransferInfo != null)
		{
			InGameManager.IntervalTransferInfo intervalTransferInfo = MonoBehaviourSingleton<InGameManager>.I.intervalTransferInfo;
			for (int i = 0; i < intervalTransferInfo.playerInfoList.Count; i++)
			{
				InGameManager.IntervalTransferInfo.PlayerInfo playerInfo = intervalTransferInfo.playerInfoList[i];
				if (playerInfo.isSelf)
				{
					requestSendForm.actioncount = playerInfo.taskChecker.GetTaskCount();
					playerInfo.taskChecker.Clear();
				}
			}
		}
		if (MonoBehaviourSingleton<InGameManager>.IsValid())
		{
			MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.ClearInfo();
		}
		Protocol.Send(QuestStartModel.URL, requestSendForm, delegate(QuestStartModel ret)
		{
			bool obj = false;
			switch (ret.Error)
			{
			case Error.None:
				obj = true;
				startData = ret.result;
				startTime = Time.get_time();
				MonoBehaviourSingleton<GoWrapManager>.I.trackQuestStart(currentQuestID);
				break;
			}
			call_back(obj);
		}, string.Empty);
	}

	public bool IsUnLockedTimeForCompleteSend()
	{
		float num = 0f;
		if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
		{
			num = (float)MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.QUEST_LOCK_SEC;
		}
		float num2 = Time.get_time() - startTime;
		if (num2 < num)
		{
			return false;
		}
		return true;
	}

	public void SetReciveCompleteData(QuestCompleteData recv_comp, List<int> now_clear_mission_list, List<int> old_mission_clear_state)
	{
		if (recv_comp != null)
		{
			compData = recv_comp;
		}
		missionNewClearFlag = now_clear_mission_list;
		if (old_mission_clear_state != null)
		{
			int i = 0;
			for (int count = old_mission_clear_state.Count; i < count; i++)
			{
				if (old_mission_clear_state[i] >= 3)
				{
					missionNewClearFlag[i] = 0;
				}
			}
		}
	}

	public void SetCompleteDataFromGuildRequest(uint beforeQuestId, GuildRequestCompleteModel.Param guildRequestCompleteData)
	{
		compData = new QuestCompleteData();
		compData.reward = guildRequestCompleteData.reward;
		compData.pointEvent = guildRequestCompleteData.pointEvent;
		SetCurrentQuestID(beforeQuestId, false);
	}

	public void SetCompleteDataFromGuildRequestMultiComplete(GuildRequestCompleteModel.Param guildRequestCompleteData)
	{
		compData = new QuestCompleteData();
		compData.reward = guildRequestCompleteData.reward;
		compData.pointEvent = guildRequestCompleteData.pointEvent;
	}

	private void SetGivenDamageList(List<int> damage_list)
	{
		List<InGameRecorder.PlayerRecord> players = MonoBehaviourSingleton<InGameRecorder>.I.players;
		for (int i = 0; i < players.Count; i++)
		{
			if (players[i].isSelf && damage_list.Count == 0)
			{
				damage_list.Add(players[i].givenTotalDamage);
				i = 0;
			}
			else if (damage_list.Count != 0)
			{
				damage_list.Add(players[i].givenTotalDamage);
			}
		}
	}

	private List<int> GetMissionClearStatus()
	{
		List<int> mission_clear_status = null;
		QuestInfoData questInfoData = GetQuestInfoData(currentQuestID);
		if (questInfoData != null && !questInfoData.IsMissionEmpty())
		{
			mission_clear_status = new List<int>();
			Array.ForEach(questInfoData.missionData, delegate(QuestInfoData.Mission data)
			{
				mission_clear_status.Add((int)(data?.state ?? CLEAR_STATUS.LOCK));
			});
		}
		return mission_clear_status;
	}

	public void SendQuestCompleteTrial(List<int> mClear, Action<bool, Error> call_back)
	{
		if (startData == null)
		{
			call_back.Invoke(false, Error.Unknown);
		}
		else
		{
			QuestCompleteTrialModel.RequestSendForm requestSendForm = new QuestCompleteTrialModel.RequestSendForm();
			requestSendForm.qt = startData.qt;
			requestSendForm.enemyHp = MonoBehaviourSingleton<InGameRecorder>.I.GetTotalEnemyHP();
			if (mClear != null)
			{
				requestSendForm.mClear = mClear;
			}
			Protocol.Send(QuestCompleteTrialModel.URL, requestSendForm, delegate(QuestCompleteTrialModel ret)
			{
				bool flag = false;
				if (ret.Error == Error.None)
				{
					flag = true;
				}
				call_back.Invoke(flag, ret.Error);
			}, string.Empty);
		}
	}

	public void SendQuestComplete(List<List<int>> breakIds, List<int> mClear, List<int> memIds, float hpRate, List<QuestCompleteModel.BattleUserLog> logs, Action<bool, Error> call_back)
	{
		if (startData == null)
		{
			call_back.Invoke(false, Error.Unknown);
		}
		else
		{
			QuestCompleteModel.RequestSendForm requestSendForm = new QuestCompleteModel.RequestSendForm();
			requestSendForm.qt = startData.qt;
			if (breakIds != null)
			{
				if (breakIds.Count > 0 && breakIds[0] != null)
				{
					requestSendForm.breakIds0 = breakIds[0];
				}
				if (breakIds.Count > 1 && breakIds[1] != null)
				{
					requestSendForm.breakIds1 = breakIds[1];
				}
				if (breakIds.Count > 2 && breakIds[2] != null)
				{
					requestSendForm.breakIds2 = breakIds[2];
				}
				if (breakIds.Count > 3 && breakIds[3] != null)
				{
					requestSendForm.breakIds3 = breakIds[3];
				}
				if (breakIds.Count > 4 && breakIds[4] != null)
				{
					requestSendForm.breakIds4 = breakIds[4];
				}
			}
			if (memIds != null)
			{
				requestSendForm.memids = memIds;
			}
			if (mClear != null)
			{
				requestSendForm.mClear = mClear;
			}
			requestSendForm.hpRate = hpRate;
			if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
			{
				SetGivenDamageList(requestSendForm.givenDamageList);
			}
			if (logs != null)
			{
				requestSendForm.fieldId = MonoBehaviourSingleton<FieldManager>.I.GetFieldId();
				requestSendForm.logs = logs;
				requestSendForm.enemyHp = MonoBehaviourSingleton<InGameRecorder>.I.GetTotalEnemyHP();
			}
			if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && MonoBehaviourSingleton<StageObjectManager>.I.self != null)
			{
				requestSendForm.actioncount = MonoBehaviourSingleton<StageObjectManager>.I.self.taskChecker.GetTaskCount();
				MonoBehaviourSingleton<StageObjectManager>.I.self.taskChecker.Clear();
			}
			if (MonoBehaviourSingleton<InGameManager>.IsValid())
			{
				requestSendForm.deliveryBattleInfo = MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.GetInfo();
				MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.ClearInfo();
			}
			List<int> mission_clear_status = GetMissionClearStatus();
			if (MonoBehaviourSingleton<StatusManager>.I.GetBoostStatus(USE_ITEM_EFFECT_TYPE.EVENT_POINT_UP) == null)
			{
				if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
				{
					MonoBehaviourSingleton<UIPlayerStatus>.I.SetHGPBoostUpdatePermitFlag(false);
				}
				if (MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
				{
					MonoBehaviourSingleton<UIEnduranceStatus>.I.SetHGPBoostUpdatePermitFlag(false);
				}
			}
			if (MonoBehaviourSingleton<InGameManager>.IsValid())
			{
				requestSendForm.remainSec = MonoBehaviourSingleton<InGameProgress>.I.remaindTime;
				requestSendForm.elapseSec = MonoBehaviourSingleton<InGameProgress>.I.GetElapsedTime();
			}
			if (IsWaveMatch(true))
			{
				requestSendForm.dc = MonoBehaviourSingleton<InGameProgress>.I.defeatCount;
				requestSendForm.dbc = MonoBehaviourSingleton<InGameProgress>.I.defeatBossCount;
				requestSendForm.pdbc = MonoBehaviourSingleton<InGameProgress>.I.partyDefeatBossCount;
				requestSendForm.rHp = MonoBehaviourSingleton<StageObjectManager>.I.GetWaveMatchTargetHpRate();
				requestSendForm.rSec = MonoBehaviourSingleton<InGameProgress>.I.remaindTime;
			}
			Protocol.Send(QuestCompleteModel.URL, requestSendForm, delegate(QuestCompleteModel ret)
			{
				bool flag = false;
				if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
				{
					MonoBehaviourSingleton<UIPlayerStatus>.I.SetHGPBoostUpdatePermitFlag(true);
				}
				if (MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
				{
					MonoBehaviourSingleton<UIEnduranceStatus>.I.SetHGPBoostUpdatePermitFlag(true);
				}
				switch (ret.Error)
				{
				case Error.None:
					flag = true;
					SetReciveCompleteData(ret.result, mClear, mission_clear_status);
					resultUserCollection.SetPartyFollowInfo(ret.result.friend);
					if (MonoBehaviourSingleton<PartyManager>.IsValid())
					{
						MonoBehaviourSingleton<PartyManager>.I.SetFollowPartyMember(ret.result.friend);
					}
					if (MonoBehaviourSingleton<FriendManager>.IsValid())
					{
						MonoBehaviourSingleton<FriendManager>.I.SetFollowNum(ret.result.followNum);
					}
					if (ret.result.repeatParty != null)
					{
						MonoBehaviourSingleton<PartyManager>.I.repeatPartyStatus = ret.result.repeatParty.repeatPartyStatus;
						if (ret.result.repeatParty.repeatPartyStatus > 0)
						{
							MonoBehaviourSingleton<PartyManager>.I.UpdatePartyRepeat(ret.result.repeatParty.party, null, ret.result.repeatParty.partyServer, ret.result.repeatParty.inviteFriendInfo, null);
						}
						else if (ret.result.repeatParty.repeatPartyStatus < 0)
						{
							MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest = false;
						}
					}
					MonoBehaviourSingleton<GoWrapManager>.I.trackQuestEnd(currentQuestID, true);
					break;
				}
				call_back.Invoke(flag, ret.Error);
			}, string.Empty);
		}
	}

	public void SendQuestRetire(bool is_timeout, List<int> memIDs, string roomId, List<QuestCompleteModel.BattleUserLog> logs, Action<bool> call_back)
	{
		QuestRetireModel.RequestSendForm requestSendForm = new QuestRetireModel.RequestSendForm();
		requestSendForm.qt = startData.qt;
		requestSendForm.timeout = (is_timeout ? 1 : 0);
		if (memIDs != null)
		{
			requestSendForm.memids = memIDs;
		}
		if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.IsRush())
		{
			requestSendForm.wave = MonoBehaviourSingleton<InGameManager>.I.GetCurrentWaveNum();
		}
		if (logs != null)
		{
			requestSendForm.fieldId = MonoBehaviourSingleton<FieldManager>.I.GetFieldId();
			requestSendForm.logs = logs;
			requestSendForm.enemyHp = MonoBehaviourSingleton<InGameRecorder>.I.GetTotalEnemyHP();
		}
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && MonoBehaviourSingleton<StageObjectManager>.I.self != null)
		{
			if (!IsValidTrial())
			{
				requestSendForm.actioncount = MonoBehaviourSingleton<StageObjectManager>.I.self.taskChecker.GetTaskCount();
			}
			MonoBehaviourSingleton<StageObjectManager>.I.self.taskChecker.Clear();
		}
		if (IsWaveMatch(true))
		{
			requestSendForm.dc = MonoBehaviourSingleton<InGameProgress>.I.defeatCount;
			requestSendForm.dbc = MonoBehaviourSingleton<InGameProgress>.I.defeatBossCount;
			requestSendForm.pdbc = MonoBehaviourSingleton<InGameProgress>.I.partyDefeatBossCount;
			requestSendForm.rSec = MonoBehaviourSingleton<InGameProgress>.I.remaindTime;
		}
		Protocol.Send(QuestRetireModel.URL, requestSendForm, delegate(QuestRetireModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
				retireData = ret.result;
				resultUserCollection.SetPartyFollowInfo(ret.result.friend);
				if (MonoBehaviourSingleton<PartyManager>.IsValid())
				{
					MonoBehaviourSingleton<PartyManager>.I.SetFollowPartyMember(ret.result.friend);
				}
				if (MonoBehaviourSingleton<FriendManager>.IsValid())
				{
					MonoBehaviourSingleton<FriendManager>.I.SetFollowNum(ret.result.followNum);
				}
				MonoBehaviourSingleton<GoWrapManager>.I.trackQuestEnd(currentQuestID, false);
			}
			call_back(obj);
		}, string.Empty);
	}

	public void SendQuestContinue(Action<bool, Error> call_back)
	{
		QuestContinueModel.RequestSendForm requestSendForm = new QuestContinueModel.RequestSendForm();
		requestSendForm.crystalCL = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.Crystal;
		Protocol.Send(QuestContinueModel.URL, requestSendForm, delegate(QuestContinueModel ret)
		{
			bool flag = false;
			if (ret.Error == Error.None)
			{
				flag = true;
			}
			call_back.Invoke(flag, ret.Error);
		}, string.Empty);
	}

	public void SendQuestReadEventStory(int eventId, Action<bool, Error> call_back)
	{
		QuestReadEventStoryModel.RequestSendForm requestSendForm = new QuestReadEventStoryModel.RequestSendForm();
		requestSendForm.eventId = eventId;
		Protocol.Send(QuestReadEventStoryModel.URL, requestSendForm, delegate(QuestReadEventStoryModel ret)
		{
			bool flag = false;
			if (ret.Error == Error.None)
			{
				flag = true;
			}
			call_back.Invoke(flag, ret.Error);
		}, string.Empty);
	}

	public void SendQuestRushProgress(int wave, int remainSec, List<int> breakIds, List<int> mClear, List<int> memIds, float hpRate, List<QuestCompleteModel.BattleUserLog> logs, Action<bool, Error> call_back)
	{
		if (startData == null)
		{
			call_back.Invoke(false, Error.Unknown);
		}
		else
		{
			QuestRushProgressModel.RequestSendForm requestSendForm = new QuestRushProgressModel.RequestSendForm();
			requestSendForm.wave = wave;
			requestSendForm.remainSec = remainSec;
			requestSendForm.qt = startData.qt;
			if (breakIds != null)
			{
				requestSendForm.breakIds = breakIds;
			}
			if (memIds != null)
			{
				requestSendForm.memids = memIds;
			}
			if (mClear != null)
			{
				requestSendForm.mClear = mClear;
			}
			requestSendForm.hpRate = hpRate;
			if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
			{
				SetGivenDamageList(requestSendForm.givenDamageList);
			}
			if (logs != null)
			{
				requestSendForm.fieldId = MonoBehaviourSingleton<FieldManager>.I.GetFieldId();
				requestSendForm.logs = logs;
				requestSendForm.enemyHp = MonoBehaviourSingleton<InGameRecorder>.I.GetTotalEnemyHP();
			}
			if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && MonoBehaviourSingleton<StageObjectManager>.I.self != null)
			{
				requestSendForm.actioncount = MonoBehaviourSingleton<StageObjectManager>.I.self.taskChecker.GetTaskCount();
				MonoBehaviourSingleton<StageObjectManager>.I.self.taskChecker.Clear();
			}
			if (MonoBehaviourSingleton<InGameManager>.IsValid())
			{
				requestSendForm.deliveryBattleInfo = MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.GetInfo();
				MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.ClearInfo();
			}
			List<int> mission_clear_status = GetMissionClearStatus();
			Protocol.Send(QuestRushProgressModel.URL, requestSendForm, delegate(QuestRushProgressModel ret)
			{
				bool flag = false;
				switch (ret.Error)
				{
				case Error.None:
					flag = true;
					MonoBehaviourSingleton<InGameManager>.I.AddWaveResult(ret.result.reward, ret.result.pointEvent, ret.result.pointShop);
					MonoBehaviourSingleton<InGameProgress>.I.SetRushRemainTime(ret.result.remainSec);
					MonoBehaviourSingleton<InGameProgress>.I.SetRushTimeBonus(ret.result.plusSec);
					SetReciveCompleteData(null, mClear, mission_clear_status);
					break;
				}
				call_back.Invoke(flag, ret.Error);
			}, string.Empty);
		}
	}

	public void SendArenaQuestStart(ArenaStartModel.RequestSendForm requestData, Action<bool> callBack)
	{
		ClearPlayData();
		requestData.qt = GenerateQuestToken();
		Protocol.Send(ArenaStartModel.URL, requestData, delegate(ArenaStartModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
				startData = ret.result;
				startTime = Time.get_time();
			}
			callBack(obj);
		}, string.Empty);
	}

	public void SendQuestArenaProgress(ArenaProgressModel.RequestSendForm requestData, Action<bool, Error> callback)
	{
		if (startData == null)
		{
			callback.Invoke(false, Error.Unknown);
		}
		else
		{
			requestData.qt = startData.qt;
			Protocol.Send(ArenaProgressModel.URL, requestData, delegate(ArenaProgressModel ret)
			{
				bool flag = false;
				if (ret.Error == Error.None)
				{
					flag = true;
					MonoBehaviourSingleton<InGameManager>.I.AddArenaWaveResult(ret.result.reward, ret.result.pointShop);
					MonoBehaviourSingleton<InGameProgress>.I.SetArenaRemainTime(ret.result.remainMilliSec);
					MonoBehaviourSingleton<InGameProgress>.I.SetArenaTimeBonus(ret.result.plusSec);
				}
				callback.Invoke(flag, ret.Error);
			}, string.Empty);
		}
	}

	public void SendArenaComplete(Action<bool, Error> callBack)
	{
		if (startData == null)
		{
			callBack.Invoke(false, Error.Unknown);
		}
		else if (!MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			callBack.Invoke(false, Error.Unknown);
		}
		else
		{
			CoopManager i = MonoBehaviourSingleton<CoopManager>.I;
			ArenaCompleteModel.RequestSendForm requestSendForm = new ArenaCompleteModel.RequestSendForm();
			if (MonoBehaviourSingleton<InGameProgress>.IsValid())
			{
				requestSendForm.remainMilliSec = MonoBehaviourSingleton<InGameProgress>.I.GetArenaRemainMilliSec();
				requestSendForm.totalElapseMilliSec = MonoBehaviourSingleton<InGameProgress>.I.GetArenaElapsedMilliSec();
			}
			Protocol.Send(ArenaCompleteModel.URL, requestSendForm, delegate(ArenaCompleteModel ret)
			{
				bool flag = false;
				if (ret.Error == Error.None)
				{
					flag = true;
					arenaCompData = ret.result;
				}
				callBack.Invoke(flag, ret.Error);
			}, string.Empty);
		}
	}

	public void SendArenaRetire(ArenaRetireModel.RequestSendForm requestData, Action<bool> callBack)
	{
		if (startData == null)
		{
			callBack(false);
		}
		else
		{
			requestData.qt = startData.qt;
			Protocol.Send(ArenaRetireModel.URL, requestData, delegate(ArenaRetireModel ret)
			{
				bool obj = false;
				if (ret.Error == Error.None)
				{
					obj = true;
				}
				callBack(obj);
			}, string.Empty);
		}
	}

	public void SendGetArenaUserRecord(int userId, int eventId, Action<bool, ArenaUserRecordModel.Param> callBack)
	{
		ArenaUserRecordModel.RequestSendForm requestSendForm = new ArenaUserRecordModel.RequestSendForm();
		requestSendForm.userId = userId;
		requestSendForm.eventId = eventId;
		Protocol.Send(ArenaUserRecordModel.URL, requestSendForm, delegate(ArenaUserRecordModel ret)
		{
			bool flag = false;
			if (ret.Error == Error.None)
			{
				flag = true;
			}
			callBack.Invoke(flag, ret.result);
		}, string.Empty);
	}

	public void SetClearStatus()
	{
		if (firstSetGetClearStatus)
		{
			firstSetGetClearStatus = false;
			clearStatusQuest = MonoBehaviourSingleton<OnceManager>.I.result.clearstatus.clearStatusQuest;
			clearStatusQuestEnemySpecies = MonoBehaviourSingleton<OnceManager>.I.result.clearstatus.clearStatusQuestEnemySpecies;
		}
	}

	public void OnDiff(BaseModelDiff.DiffClearStatusQuest diff)
	{
		bool flag = false;
		if (Utility.IsExist(diff.add))
		{
			diff.add.ForEach(delegate(ClearStatusQuest data)
			{
				clearStatusQuest.Add(data);
			});
			flag = true;
		}
		if (Utility.IsExist(diff.update))
		{
			diff.update.ForEach(delegate(ClearStatusQuest data)
			{
				QuestManager questManager = this;
				ClearStatusQuest clearStatusQuest = this.clearStatusQuest.Find((ClearStatusQuest list_data) => list_data.questId == data.questId);
				clearStatusQuest.questId = data.questId;
				clearStatusQuest.questStatus = data.questStatus;
				clearStatusQuest.missionStatus = data.missionStatus;
				clearStatusQuest.story = data.story;
			});
			flag = true;
		}
		if (flag)
		{
			DirtyQuest();
		}
	}

	public void DirtyQuest()
	{
		MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_QUEST_CLEAR_STATUS);
	}

	public void OnDiff(BaseModelDiff.DiffClearStatusQuestEnemySpecies diff)
	{
		bool flag = false;
		if (Utility.IsExist(diff.add))
		{
			diff.add.ForEach(delegate(ClearStatusQuestEnemySpecies data)
			{
				clearStatusQuestEnemySpecies.Add(data);
			});
			flag = true;
		}
		if (Utility.IsExist(diff.update))
		{
			diff.update.ForEach(delegate(ClearStatusQuestEnemySpecies data)
			{
				QuestManager questManager = this;
				ClearStatusQuestEnemySpecies clearStatusQuestEnemySpecies = this.clearStatusQuestEnemySpecies.Find((ClearStatusQuestEnemySpecies list_data) => list_data.enemySpecies == data.enemySpecies);
				clearStatusQuestEnemySpecies.enemySpecies = data.enemySpecies;
				clearStatusQuestEnemySpecies.questStatus = data.questStatus;
			});
			flag = true;
		}
		if (flag)
		{
			DirtyQuest();
		}
	}

	private void UpdateChallengeList(List<QuestData> shadow)
	{
		challengeList = shadow;
	}

	public void SendGetChallengeList(QuestChallengeListModel.RequestSendForm send, Action<bool, Error> call_back, bool isSave)
	{
		if (isSave)
		{
			SaveChallengeSearchSettings(send);
		}
		Protocol.Send(QuestChallengeListModel.URL, send, delegate(QuestChallengeListModel ret)
		{
			bool flag = false;
			if (ret.Error == Error.None)
			{
				flag = true;
				UpdateChallengeList(ret.result.shadow);
			}
			call_back.Invoke(flag, ret.Error);
		}, string.Empty);
	}

	public void SendGetChallengeList(int enemyLevel, Action<bool, Error> call_back, bool isSave)
	{
		QuestChallengeListModel.RequestSendForm requestSendForm = new QuestChallengeListModel.RequestSendForm();
		requestSendForm.enemyLevel = enemyLevel;
		SendGetChallengeList(requestSendForm, call_back, isSave);
	}

	public void SendGetChallengeList(QuestAcceptChallengeRoomCondition.ChallengeSearchRequestParam requestParam, Action<bool, Error> call_back, bool isSave)
	{
		QuestChallengeListModel.RequestSendForm requestSendForm = new QuestChallengeListModel.RequestSendForm();
		requestSendForm.elementBit = requestParam.elementBit;
		requestSendForm.rarityBit = requestParam.rarityBit;
		requestSendForm.enemySpeciesId = requestParam.GetEnemySpeciesId(requestParam.targetEnemySpeciesName);
		requestSendForm.enemyLevel = requestParam.enemyLevel;
		requestSendForm.enemySpeciesName = requestParam.targetEnemySpeciesName;
		SendGetChallengeList(requestSendForm, call_back, isSave);
	}

	private void SaveChallengeSearchSettings(QuestChallengeListModel.RequestSendForm sendForm)
	{
		PlayerPrefs.SetInt("CHALLENGE_SEARCH_RAIRTY_KEY", sendForm.rarityBit);
		PlayerPrefs.SetInt("CHALLENGE_SEARCH_ELEMENT_KEY", sendForm.elementBit);
		PlayerPrefs.SetInt("CHALLENGE_SEARCH_ENEMY_LEVEL_KEY", sendForm.enemyLevel);
		if (!string.IsNullOrEmpty(sendForm.enemySpeciesName))
		{
			PlayerPrefs.SetString("CHALLENGE_SEARCH_SPECIES_KEY", sendForm.enemySpeciesName);
		}
		PlayerPrefs.Save();
	}

	public void SendGetChallengeEnmey(int enemyId, Action<bool, QuestChallengeEnemyModel.Param> call_back)
	{
		QuestChallengeEnemyModel.RequestSendForm requestSendForm = new QuestChallengeEnemyModel.RequestSendForm();
		requestSendForm.enemyId = enemyId;
		Protocol.Send(QuestChallengeEnemyModel.URL, requestSendForm, delegate(QuestChallengeEnemyModel ret)
		{
			bool flag = false;
			if (ret.Error == Error.None)
			{
				flag = true;
			}
			call_back.Invoke(flag, ret.result);
		}, string.Empty);
	}

	public void SetChallengeSearchRequestFromPrefs(int userLevel, QuestAcceptChallengeRoomCondition.ChallengeSearchRequestParam sendForm)
	{
		if (sendForm != null)
		{
			int @int = PlayerPrefs.GetInt("CHALLENGE_SEARCH_USER_LEVEL_KEY", 0);
			if (userLevel != @int)
			{
				PlayerPrefs.SetInt("CHALLENGE_SEARCH_USER_LEVEL_KEY", userLevel);
				PlayerPrefs.SetInt("CHALLENGE_SEARCH_ENEMY_LEVEL_KEY", userLevel);
				PlayerPrefs.Save();
			}
			sendForm.rarityBit = PlayerPrefs.GetInt("CHALLENGE_SEARCH_RAIRTY_KEY", 8388607);
			sendForm.elementBit = PlayerPrefs.GetInt("CHALLENGE_SEARCH_ELEMENT_KEY", 8388607);
			sendForm.enemyLevel = PlayerPrefs.GetInt("CHALLENGE_SEARCH_ENEMY_LEVEL_KEY", userLevel);
			sendForm.targetEnemySpeciesName = PlayerPrefs.GetString("CHALLENGE_SEARCH_SPECIES_KEY", (string)null);
		}
	}

	public List<uint> GetTargetClearedNewOpenQuest(uint target_quest_id)
	{
		List<uint> list = new List<uint>();
		Singleton<QuestTable>.I.AllQuestData(delegate(QuestTable.QuestTableData table)
		{
			if (table.appearQuestId == target_quest_id)
			{
				list.Add(table.questID);
			}
		});
		list.Sort();
		return list;
	}

	public string GetQuestDifficultySpriteName(DIFFICULTY_TYPE dufficulty)
	{
		switch (dufficulty)
		{
		default:
			return "Quest_classicon_beginner";
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
		}
	}

	public bool IsForceDefeatQuest()
	{
		if (currentQuestID != 0)
		{
			return Singleton<QuestTable>.I.GetQuestData(currentQuestID)?.forceDefeat ?? false;
		}
		return false;
	}

	public VorgonQuetType GetVorgonQuestType()
	{
		switch (currentQuestID)
		{
		case 303510901u:
			return VorgonQuetType.BATTLE_WITH_WYBURN;
		case 314410901u:
			return VorgonQuetType.BATTLE_WITH_WYBURN;
		case 303512301u:
			return VorgonQuetType.BATTLE_WITH_VORGON;
		default:
			return VorgonQuetType.NONE;
		}
	}
}
