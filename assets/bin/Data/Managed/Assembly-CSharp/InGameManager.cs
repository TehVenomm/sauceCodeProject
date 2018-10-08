using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviourSingleton<InGameManager>
{
	public enum VoiceOption
	{
		ENGLISH,
		JAPANESE,
		MUTE
	}

	public enum LanguageOption
	{
		ENGLISH,
		FRENCH,
		GERMAN,
		ITALIAN,
		PORTUGUESE,
		THAI,
		VIETNAM,
		SPANISH
	}

	public class IntervalTransferInfo
	{
		public class PlayerInfo
		{
			public int id;

			public StageObjectManager.CreatePlayerInfo createInfo;

			public StageObjectManager.PlayerTransferInfo transferInfo;

			public bool isSelf;

			public StageObject.COOP_MODE_TYPE coopMode;

			public int coopClientId;

			public bool isNpcController;

			public bool isCoopPlayer;

			public TaskChecker taskChecker = new TaskChecker();
		}

		public float remaindTime = -1f;

		public float elapsedTime = -1f;

		public List<PlayerInfo> playerInfoList = new List<PlayerInfo>();
	}

	public class DropItemInfo
	{
		public REWARD_TYPE type;

		public uint id;

		public int num;

		public DropItemInfo(REWARD_TYPE _type, uint _id, int _num)
		{
			type = _type;
			id = _id;
			num = _num;
		}

		public UIDropAnnounce.DropAnnounceInfo CreateAnnounceInfo(out bool is_rare)
		{
			is_rare = false;
			UIDropAnnounce.DropAnnounceInfo dropAnnounceInfo = null;
			switch (type)
			{
			case REWARD_TYPE.SKILL_ITEM:
				return UIDropAnnounce.DropAnnounceInfo.CreateSkillItemInfo(id, num, out is_rare);
			case REWARD_TYPE.EQUIP_ITEM:
				return UIDropAnnounce.DropAnnounceInfo.CreateEquipItemInfo(id, num, out is_rare);
			case REWARD_TYPE.ACCESSORY:
				return UIDropAnnounce.DropAnnounceInfo.CreateAccessoryItemInfo(id, num, out is_rare);
			default:
				return UIDropAnnounce.DropAnnounceInfo.CreateItemInfo(id, num, out is_rare);
			}
		}
	}

	public class DropDeliveryInfo
	{
		public int id;

		public int index;

		public string name;

		public string itemName;

		public int num;

		public List<DELIVERY_CONDITION_TYPE> conditionTypes;

		private bool? isCountUpAtKillFieldEnemy;

		public DropDeliveryInfo(int delivery_id, int delivery_index, string _name, string item_name, int _num, List<DELIVERY_CONDITION_TYPE> condition_type)
		{
			id = delivery_id;
			index = delivery_index;
			name = _name;
			itemName = item_name;
			num = _num;
			conditionTypes = condition_type;
		}

		public bool IsCountUpAtDefeatFieldEnemy()
		{
			bool? nullable = isCountUpAtKillFieldEnemy;
			if (nullable.HasValue)
			{
				bool? nullable2 = isCountUpAtKillFieldEnemy;
				return nullable2.Value;
			}
			int i = 0;
			for (int count = conditionTypes.Count; i < count; i++)
			{
				if (MonoBehaviourSingleton<DeliveryManager>.I.IsDefeatFieldConditionType(conditionTypes[i]))
				{
					isCountUpAtKillFieldEnemy = true;
					return true;
				}
			}
			isCountUpAtKillFieldEnemy = false;
			return false;
		}
	}

	public class QuestTransferInfo
	{
		public IntervalTransferInfo intervalTransferInfo;

		public bool isQuestHappen;

		public bool isQuestGate;

		public bool isQuestPortal;

		public bool isGateQuestClear;

		public bool isTransitionFieldToQuest;

		public bool isTransitionQuestToField;

		public bool isTransitionFieldReentry;

		public bool isStoryPortal;

		public int readStoryID;

		public uint beforePortalID;

		public FieldManager.FieldTransitionInfo backTransitionInfo;
	}

	public class RushWaveSyncData
	{
		public float elapsedTime;

		public List<int> bossBreakIds;
	}

	public const string defaultVariant = "en-sd";

	public const int GRAPHIC_OPTION_LOW = 0;

	public const int GRAPHIC_OPTION_STANDARD = 1;

	public const int GRAPHIC_OPTION_HIGH = 2;

	public const int GRAPHIC_OPTION_HIGHEST = 3;

	public const string GRAPHIC_OPTION_KEY_LOW = "low";

	public const string GRAPHIC_OPTION_KEY_STANDARD = "standard";

	public const string GRAPHIC_OPTION_KEY_HIGH = "high";

	public const string GRAPHIC_OPTION_KEY_HIGHEST = "highest";

	public const int ARROW_CAMERA_OPTION_A = 0;

	public const int ARROW_CAMERA_OPTION_B = 1;

	public const string ARROW_CAMERA_OPTION_KEY_A = "typea";

	public const string ARROW_CAMERA_OPTION_KEY_B = "typeb";

	public static string[] voiceVariants = new string[2]
	{
		"en-sd",
		"jp-sd"
	};

	public static string[] languageVariants = new string[8]
	{
		"en-sd",
		"fr-sd",
		"ge-sd",
		"it-sd",
		"po-sd",
		"th-sd",
		"vn-sd",
		"es-sd"
	};

	[NonSerialized]
	public int graphicOptionType;

	[NonSerialized]
	public int arrowCameraType = 1;

	public List<UIInGamePopupDialog.Desc> dialogOpenInfoList = new List<UIInGamePopupDialog.Desc>();

	protected List<FieldDropObject> dropList = new List<FieldDropObject>();

	protected List<GameObject> dropNCaches = new List<GameObject>();

	protected List<GameObject> dropHNCaches = new List<GameObject>();

	protected List<GameObject> dropRCaches = new List<GameObject>();

	protected List<GameObject> dropLoungeCaches = new List<GameObject>();

	protected List<GameObject> bossDropNCaches = new List<GameObject>();

	protected List<GameObject> bossDropRCaches = new List<GameObject>();

	protected List<GameObject> bossDropRegionBreakCaches = new List<GameObject>();

	protected List<GameObject> dropSPNCaches = new List<GameObject>();

	protected List<GameObject> dropSPHNCaches = new List<GameObject>();

	protected List<GameObject> dropSPRCaches = new List<GameObject>();

	protected List<GameObject> dropHalloweenCaches = new List<GameObject>();

	protected List<GameObject> dropESPNCaches = new List<GameObject>();

	protected List<GameObject> dropESPHNCaches = new List<GameObject>();

	protected List<GameObject> dropESPRCaches = new List<GameObject>();

	protected List<GameObject> dropSeasonalCaches = new List<GameObject>();

	public IntervalTransferInfo intervalTransferInfo;

	[NonSerialized]
	public DeliveryBattleChecker deliveryBattleChecker = new DeliveryBattleChecker();

	[NonSerialized]
	public bool isAlreadyBattleStarted;

	[NonSerialized]
	public List<uint> disableHappenQuestIdList = new List<uint>();

	public QuestTransferInfo questTransferInfo;

	private int rushIndex;

	private PartyModel.RushInfo rushInfo;

	private Coop_Model_EnemyInitialize rushBossBackup;

	private int arenaIndex;

	private PartyModel.ArenaInfo arenaInfo;

	private Coop_Model_EnemyInitialize seriesBossBackup;

	[NonSerialized]
	public CoopClient.CLIENT_JOIN_TYPE currentJoinType;

	private IEnumerator updateIntervalTransferInfoRemaindTime;

	private static string[] disableEffectsGraphicLow = new string[23]
	{
		"ef_btl_pl_runsmoke_",
		"ef_ui_skillgauge_",
		"ef_btl_pl_avoidsmoke_",
		"ef_btl_pl_landingsmoke_",
		"ef_btl_bg_takingspot_",
		"ef_btl_damage_add_blood",
		"ef_btl_wyvern_downsmoke_",
		"ef_btl_pl_downsmoke_",
		"ef_btl_bg_birds_",
		"ef_btl_bg_spray_01",
		"ef_btl_bg_rain_01",
		"ef_btl_pl_attacksmoke_",
		"ef_btl_pl01_attack02smoke_",
		"ef_btl_pl02_attack03smoke_",
		"ef_btl_bg_magma_01",
		"ef_btl_bg_sparks_",
		"ef_ui_downgauge_",
		"ef_btl_dragon_downsmoke_",
		"ef_btl_dragon_walksmoke",
		"ef_btl_enemy_landingsmoke_m_",
		"ef_btl_pl_jump_01",
		"ef_btl_wyvern_walksmoke1_",
		"ef_btl_pl_impactsmoke_"
	};

	public List<FieldDropObject> dropItemList => dropList;

	public GameObject selfCacheObject
	{
		get;
		private set;
	}

	public bool isValidGimmickObject
	{
		get;
		protected set;
	}

	public bool isQuestHappen
	{
		get;
		set;
	}

	public bool isQuestGate
	{
		get;
		set;
	}

	public bool isQuestPortal
	{
		get;
		set;
	}

	public bool isStoryPortal
	{
		get;
		set;
	}

	public bool isGateQuestClear
	{
		get;
		set;
	}

	public bool isTransitionFieldToQuest
	{
		get;
		set;
	}

	public bool isTransitionQuestToField
	{
		get;
		set;
	}

	public bool isTransitionQuestToFieldExplore
	{
		get;
		set;
	}

	public bool isTransitionFieldReentry
	{
		get;
		set;
	}

	public bool isRetry
	{
		get;
		set;
	}

	public bool isTransitionQuestToQuest
	{
		get;
		set;
	}

	public bool isQuestResultFieldLeave
	{
		get;
		set;
	}

	public int readStoryID
	{
		get;
		set;
	}

	public uint beforePortalID
	{
		get;
		set;
	}

	public FieldManager.FieldTransitionInfo backTransitionInfo
	{
		get;
		set;
	}

	public int rushId
	{
		get;
		private set;
	}

	public bool isResultedRush
	{
		get;
		private set;
	}

	public List<QuestCompleteRewardList> rushRewards
	{
		get;
		private set;
	}

	public List<PointShopResultData> rushPointShops
	{
		get;
		private set;
	}

	public List<PointEventCurrentData> rushPointEvents
	{
		get;
		private set;
	}

	public bool isRushReentry
	{
		get;
		private set;
	}

	public List<RushWaveSyncData> rushWaveSyncDataList
	{
		get;
		private set;
	}

	public List<QuestCompleteRewardList> arenaRewards
	{
		get;
		private set;
	}

	public List<PointShopResultData> arenaPointShops
	{
		get;
		private set;
	}

	public bool isSeriesReentry
	{
		get;
		private set;
	}

	public bool IsQuestInField()
	{
		return isQuestHappen || isQuestGate || isQuestPortal;
	}

	public bool IsQuestInPortal()
	{
		return isQuestGate || isQuestPortal;
	}

	public static bool IsReentry()
	{
		return IsValidRush() || FieldManager.IsValidInGameNoQuest() || QuestManager.IsValidInGameExplore() || QuestManager.IsValidInGameSeries() || QuestManager.IsValidInGameWaveMatch(false);
	}

	public static bool IsReentryNotLeaveParty()
	{
		return IsValidRush() || QuestManager.IsValidInGameExplore() || QuestManager.IsValidInGameSeries() || QuestManager.IsValidInGameWaveMatch(false);
	}

	public static bool IsReentryMapId()
	{
		return IsValidRush() || QuestManager.IsValidInGameExplore() || QuestManager.IsValidInGameSeries() || QuestManager.IsValidInGameWaveMatch(false);
	}

	public bool IsRush()
	{
		return rushInfo != null;
	}

	public static bool IsValidRush()
	{
		return MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.IsRush();
	}

	public void ClearRush()
	{
		rushInfo = null;
		rushId = 0;
		rushIndex = 0;
		rushRewards = null;
		rushPointEvents = null;
		rushPointShops = null;
		rushWaveSyncDataList = null;
		isResultedRush = false;
	}

	public void SetRushInfo(int rushId, PartyModel.RushInfo rushInfo)
	{
		this.rushId = rushId;
		this.rushInfo = rushInfo;
		rushIndex = 0;
		rushRewards = new List<QuestCompleteRewardList>();
		rushPointShops = new List<PointShopResultData>();
		rushPointEvents = new List<PointEventCurrentData>();
		rushWaveSyncDataList = new List<RushWaveSyncData>();
	}

	public void SetResultedRush()
	{
		isResultedRush = true;
	}

	public void ProgressRush()
	{
		rushIndex++;
	}

	public bool IsLastRash()
	{
		return rushInfo.waves.Count - 1 <= rushIndex;
	}

	public int GetRushIndex()
	{
		return rushIndex;
	}

	public uint GetCurrentRushQuestId()
	{
		return (uint)rushInfo.waves[rushIndex].questId;
	}

	public int GetCurrentWaveNum()
	{
		return rushInfo.waves[rushIndex].wave;
	}

	public int GetWaveNum(int index)
	{
		return rushInfo.waves[index].wave;
	}

	public int GetCurrentRushRescureResetNum()
	{
		return rushInfo.waves[rushIndex].rescueResetNum;
	}

	public bool CanRushPayContinue()
	{
		return rushInfo.continueFlag > 0;
	}

	public int GetRushQuestId(int wave)
	{
		return rushInfo.waves.Find((PartyModel.RushInfo.WaveInfo w) => w.wave == wave).questId;
	}

	public void AddWaveResult(QuestCompleteRewardList reward, List<PointEventCurrentData> pointEvent, List<PointShopResultData> pointShop)
	{
		rushRewards.Add(reward);
		rushPointEvents.AddRange(pointEvent);
		AddRushPointShop(pointShop);
	}

	private void AddRushPointShop(List<PointShopResultData> pointShop)
	{
		using (List<PointShopResultData>.Enumerator enumerator = pointShop.GetEnumerator())
		{
			PointShopResultData add_data;
			while (enumerator.MoveNext())
			{
				add_data = enumerator.Current;
				PointShopResultData pointShopResultData = rushPointShops.Find((PointShopResultData list_data) => list_data.pointShopId == add_data.pointShopId);
				if (pointShopResultData == null)
				{
					rushPointShops.Add(add_data);
				}
				else
				{
					pointShopResultData.getPoint += add_data.getPoint;
					pointShopResultData.totalPoint = add_data.totalPoint;
				}
			}
		}
	}

	public void BackupRushStageInReentry()
	{
		rushBossBackup = null;
		Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
		if (boss != null)
		{
			rushBossBackup = boss.CreateBackup();
		}
		isRushReentry = true;
	}

	public void RestoreRushInReentry()
	{
		Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
		if (boss != null && rushBossBackup != null)
		{
			CoopPacket coopPacket = new CoopPacket();
			coopPacket.model = rushBossBackup;
			boss.enemyReceiver.Set(coopPacket);
		}
	}

	public void ResetRushInReentry()
	{
		isRushReentry = false;
		rushBossBackup = null;
	}

	public int GetCurrentRushStandupHpPer()
	{
		return rushInfo.waves[rushIndex].standupHpPer;
	}

	public void RecordRushWaveSyncData()
	{
		CoopStage coopStage = MonoBehaviourSingleton<CoopManager>.I.coopStage;
		RushWaveSyncData rushWaveSyncData = new RushWaveSyncData();
		rushWaveSyncData.bossBreakIds = coopStage.bossBreakIDLists[0];
		rushWaveSyncData.elapsedTime = MonoBehaviourSingleton<InGameProgress>.I.GetElapsedTime();
		rushWaveSyncDataList.Add(rushWaveSyncData);
	}

	public RushWaveSyncData GetRushSyncData(int index)
	{
		if (rushWaveSyncDataList.Count > index)
		{
			return rushWaveSyncDataList[index];
		}
		return null;
	}

	public void SetArenaInfo(int arenaId)
	{
		ArenaTable.ArenaData arenaData = Singleton<ArenaTable>.I.GetArenaData(arenaId);
		arenaInfo = new PartyModel.ArenaInfo();
		arenaInfo.arenaData = arenaData;
		for (int i = 0; i < 5; i++)
		{
			if (arenaData.questIds[i] > 0)
			{
				PartyModel.ArenaInfo.WaveInfo waveInfo = new PartyModel.ArenaInfo.WaveInfo();
				waveInfo.wave = i + 1;
				waveInfo.questId = arenaData.questIds[i];
				arenaInfo.waves.Add(waveInfo);
			}
		}
		arenaIndex = 0;
		arenaRewards = new List<QuestCompleteRewardList>();
		arenaPointShops = new List<PointShopResultData>();
	}

	public void ClearArenaInfo()
	{
		arenaInfo = null;
		arenaIndex = 0;
		arenaRewards = null;
		arenaPointShops = null;
	}

	public bool HasArenaInfo()
	{
		return arenaInfo != null;
	}

	public bool IsArenaTimeAttack()
	{
		return HasArenaInfo() && arenaInfo.arenaData.rank == ARENA_RANK.S;
	}

	public ARENA_CONDITION[] GetArenaConditions()
	{
		if (arenaInfo == null)
		{
			return null;
		}
		if (arenaInfo.arenaData == null)
		{
			return null;
		}
		return arenaInfo.arenaData.conditions;
	}

	public void ProgressArena()
	{
		arenaIndex++;
	}

	public bool IsArenaFirstWave()
	{
		return arenaIndex <= 0;
	}

	public bool IsArenaFinalWave()
	{
		return arenaInfo.waves.Count - 1 <= arenaIndex;
	}

	public int GetArenaWaveMax()
	{
		return arenaInfo.waves.Count;
	}

	public int GetCurrentArenaWaveNum()
	{
		return arenaInfo.waves[arenaIndex].wave;
	}

	public uint GetCurrentArenaQuestId()
	{
		return (uint)arenaInfo.waves[arenaIndex].questId;
	}

	public uint GetFirstArenaQuestId()
	{
		return (uint)arenaInfo.waves[0].questId;
	}

	public bool ContainsArenaCondition(ARENA_CONDITION condition)
	{
		if (!HasArenaInfo())
		{
			return false;
		}
		for (int i = 0; i < arenaInfo.arenaData.conditions.Length; i++)
		{
			if (arenaInfo.arenaData.conditions[i] == condition)
			{
				return true;
			}
		}
		return false;
	}

	public ARENA_GROUP GetCurrentArenaGroup()
	{
		return arenaInfo.arenaData.group;
	}

	public ARENA_RANK GetCurrentArenaRank()
	{
		return arenaInfo.arenaData.rank;
	}

	public void AddArenaWaveResult(QuestCompleteRewardList reward, List<PointShopResultData> pointShop)
	{
		arenaRewards.Add(reward);
		AddArenaPointShop(pointShop);
	}

	private void AddArenaPointShop(List<PointShopResultData> pointShop)
	{
		using (List<PointShopResultData>.Enumerator enumerator = pointShop.GetEnumerator())
		{
			PointShopResultData addData;
			while (enumerator.MoveNext())
			{
				addData = enumerator.Current;
				PointShopResultData pointShopResultData = arenaPointShops.Find((PointShopResultData listData) => listData.pointShopId == addData.pointShopId);
				if (pointShopResultData == null)
				{
					arenaPointShops.Add(addData);
				}
				else
				{
					pointShopResultData.getPoint += addData.getPoint;
					pointShopResultData.totalPoint = addData.totalPoint;
				}
			}
		}
	}

	public void BackupSeriesStageInReentry()
	{
		seriesBossBackup = null;
		Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
		if (boss != null && !boss.isDead)
		{
			seriesBossBackup = boss.CreateBackup();
		}
		isSeriesReentry = true;
	}

	public void RestoreSeriesInReentry()
	{
		Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
		if (boss != null && seriesBossBackup != null)
		{
			CoopPacket coopPacket = new CoopPacket();
			coopPacket.model = seriesBossBackup;
			boss.enemyReceiver.Set(coopPacket);
		}
	}

	public void ResetSeriesInReentry()
	{
		isSeriesReentry = false;
		seriesBossBackup = null;
	}

	public static bool IsValidInGameTest()
	{
		bool flag = false;
		return MonoBehaviourSingleton<InGameManager>.IsValid() && flag;
	}

	public static bool IsValidInGameTestField()
	{
		bool flag = false;
		return MonoBehaviourSingleton<InGameManager>.IsValid() && flag;
	}

	protected override void Awake()
	{
		base.Awake();
		UpdateConfig();
	}

	public static int GetGraphicOptionType(string key)
	{
		int result = 1;
		switch (key)
		{
		case "low":
			result = 0;
			break;
		case "standard":
			result = 1;
			break;
		case "high":
			result = 2;
			break;
		case "highest":
			result = 3;
			break;
		}
		return result;
	}

	public void UpdateConfig()
	{
		graphicOptionType = 0;
		arrowCameraType = 1;
		if (GameSaveData.instance != null)
		{
			graphicOptionType = GetGraphicOptionType(GameSaveData.instance.graphicOptionKey);
			arrowCameraType = GetArrowCameraType(GameSaveData.instance.arrowCameraKey);
		}
	}

	public static int GetArrowCameraType(string key)
	{
		int result = 1;
		switch (key)
		{
		case "typea":
			result = 0;
			break;
		case "typeb":
			result = 1;
			break;
		}
		return result;
	}

	public void OnEndInGameScene()
	{
		dialogOpenInfoList = new List<UIInGamePopupDialog.Desc>();
		intervalTransferInfo = null;
		isQuestHappen = false;
		isQuestGate = false;
		isQuestPortal = false;
		isGateQuestClear = false;
		isTransitionFieldToQuest = false;
		isTransitionQuestToField = false;
		isTransitionQuestToFieldExplore = false;
		isTransitionFieldReentry = false;
		isStoryPortal = false;
		isAlreadyBattleStarted = false;
		beforePortalID = 0u;
		backTransitionInfo = null;
		currentJoinType = CoopClient.CLIENT_JOIN_TYPE.NONE;
		MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_GRAB, false);
		MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_COMMAND, false);
		ClearAllDrop();
		StopIntervalTransferInfoRemaindTimeUpdate();
	}

	public string GetCurrentStageName()
	{
		string result = "ST011D_01";
		if (MonoBehaviourSingleton<FieldManager>.IsValid())
		{
			result = MonoBehaviourSingleton<FieldManager>.I.GetCurrentMapStageName();
		}
		return result;
	}

	public float GetCurrentLimitTime()
	{
		float result = 0f;
		if (QuestManager.IsValidInGameExplore())
		{
			if (MonoBehaviourSingleton<QuestManager>.I.currentQuestID != 0)
			{
				result = MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestLimitTime();
			}
		}
		else if (IsRush())
		{
			int questId = rushInfo.waves[0].questId;
			QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint)questId);
			result = questData.limitTime;
			if (intervalTransferInfo != null && intervalTransferInfo.remaindTime >= 0f)
			{
				result = intervalTransferInfo.remaindTime;
			}
		}
		else if (HasArenaInfo())
		{
			result = (float)arenaInfo.arenaData.timeLimit * 0.001f;
			if (intervalTransferInfo != null && intervalTransferInfo.remaindTime >= 0f)
			{
				result = intervalTransferInfo.remaindTime;
			}
		}
		else if (QuestManager.IsValidInGame())
		{
			if (intervalTransferInfo != null && intervalTransferInfo.remaindTime >= 0f)
			{
				result = intervalTransferInfo.remaindTime;
			}
			else if (MonoBehaviourSingleton<QuestManager>.I.currentQuestID != 0)
			{
				result = MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestLimitTime();
			}
		}
		else
		{
			result = 0f;
		}
		return result;
	}

	public bool IsNeedInitBoss()
	{
		bool result = false;
		if (QuestManager.IsValidInGame())
		{
			if (MonoBehaviourSingleton<QuestManager>.I.IsExplore())
			{
				QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID);
				uint mapId = questData.mapId;
				result = (MonoBehaviourSingleton<FieldManager>.I.currentMapID == mapId);
				if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsExploreBossDead())
				{
					result = false;
				}
			}
			else
			{
				QuestTable.QuestTableData questData2 = Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID);
				result = (questData2.enemyID[MonoBehaviourSingleton<QuestManager>.I.currentQuestSeriesIndex] > 0);
			}
		}
		else if (FieldManager.IsValidInGame())
		{
			result = false;
		}
		return result;
	}

	public void CheckStageInitialState()
	{
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		isValidGimmickObject = (MonoBehaviourSingleton<StageObjectManager>.I.gimmickList.Count > 0);
		if (FieldManager.IsValidInGameNoBoss())
		{
			MonoBehaviourSingleton<InGameCameraManager>.I.hitObjectType = InGameCameraManager.CAM_HIT_OBJ_TYPE.NONE;
		}
		else
		{
			bool flag = false;
			Transform[] componentsInChildren = MonoBehaviourSingleton<StageManager>.I._transform.GetComponentsInChildren<Transform>();
			int i = 0;
			for (int num = componentsInChildren.Length; i < num; i++)
			{
				if (componentsInChildren[i].get_gameObject().get_layer() == 18 || componentsInChildren[i].get_gameObject().get_layer() == 9 || componentsInChildren[i].get_gameObject().get_layer() == 21)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				MonoBehaviourSingleton<InGameCameraManager>.I.hitObjectType = InGameCameraManager.CAM_HIT_OBJ_TYPE.ZOOM;
			}
			else
			{
				MonoBehaviourSingleton<InGameCameraManager>.I.hitObjectType = InGameCameraManager.CAM_HIT_OBJ_TYPE.NONE;
			}
		}
	}

	public IEnumerator InitializeEnemyPop()
	{
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && MonoBehaviourSingleton<FieldManager>.IsValid())
		{
			if (MonoBehaviourSingleton<QuestManager>.I.IsCurrentQuestTypeSeries())
			{
				this.StartCoroutine(InitializeEnemyPopForSeries());
			}
			else
			{
				List<FieldMapTable.EnemyPopTableData> enemy_pop_list = Singleton<FieldMapTable>.I.GetEnemyPopList(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
				if (enemy_pop_list != null && enemy_pop_list.Count > 0)
				{
					uint quest_enemy_id = 0u;
					int quest_enemy_lv = 0;
					if (QuestManager.IsValidInGame())
					{
						quest_enemy_id = (uint)MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestEnemyID();
						quest_enemy_lv = MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestEnemyLv();
					}
					if (HasArenaInfo())
					{
						quest_enemy_lv = arenaInfo.arenaData.level;
					}
					List<uint> enemy_list = new List<uint>();
					int load_count = 0;
					int i = 0;
					for (int len = enemy_pop_list.Count; i < len; i++)
					{
						FieldMapTable.EnemyPopTableData enemy_pop = enemy_pop_list[i];
						if (enemy_pop != null)
						{
							uint enemy_id;
							int enemy_level;
							if (enemy_pop.enemyID != 0)
							{
								enemy_id = enemy_pop.enemyID;
								enemy_level = (int)enemy_pop.enemyLv;
							}
							else
							{
								if (quest_enemy_id == 0)
								{
									continue;
								}
								enemy_id = quest_enemy_id;
								enemy_level = quest_enemy_lv;
							}
							if (!enemy_list.Contains(enemy_id))
							{
								enemy_list.Add(enemy_pop.enemyID);
								MonoBehaviourSingleton<StageObjectManager>.I.CreateEnemy(0, Vector3.get_zero(), 0f, (int)enemy_id, enemy_level, enemy_pop.bossFlag, enemy_pop.bigMonsterFlag, true, true, delegate(Enemy o)
								{
									//IL_0001: Unknown result type (might be due to invalid IL or missing references)
									o.get_gameObject().SetActive(false);
									MonoBehaviourSingleton<StageObjectManager>.I.enemyStokeList.Add(o);
									((_003CInitializeEnemyPop_003Ec__Iterator230)/*Error near IL_0232: stateMachine*/)._003Cload_count_003E__4++;
								});
							}
						}
					}
					while (load_count < enemy_list.Count)
					{
						yield return (object)null;
					}
				}
			}
		}
	}

	private IEnumerator InitializeEnemyPopForSeries()
	{
		QuestManager questMgr = MonoBehaviourSingleton<QuestManager>.I;
		int count = questMgr.GetCurrentQuestSeriesNum();
		int loadCount = 0;
		for (int i = 0; i < count; i++)
		{
			uint enemyId = (uint)questMgr.GetCurrentQuestEnemyID(i);
			int enemyLv = questMgr.GetCurrentQuestEnemyLv(i);
			if (enemyId != 0 && enemyLv > 0)
			{
				MonoBehaviourSingleton<StageObjectManager>.I.CreateEnemy(0, Vector3.get_zero(), 0f, (int)enemyId, enemyLv, true, true, true, true, delegate(Enemy o)
				{
					//IL_0001: Unknown result type (might be due to invalid IL or missing references)
					o.get_gameObject().SetActive(false);
					MonoBehaviourSingleton<StageObjectManager>.I.enemyStokeList.Add(o);
					((_003CInitializeEnemyPopForSeries_003Ec__Iterator231)/*Error near IL_00c4: stateMachine*/)._003CloadCount_003E__2++;
				});
			}
		}
		while (loadCount < count)
		{
			yield return (object)null;
		}
		LoadingQueue loadingQueue = new LoadingQueue(this);
		loadingQueue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enemy_entry_01");
		while (loadingQueue.IsLoading())
		{
			yield return (object)loadingQueue.Wait();
		}
	}

	public GameObject CreateBossDropObject(int rarity)
	{
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Expected O, but got Unknown
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Expected O, but got Unknown
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Expected O, but got Unknown
		GameObject val = null;
		switch (rarity)
		{
		case 2:
		{
			int k = 0;
			for (int count3 = bossDropRegionBreakCaches.Count; k < count3; k++)
			{
				if (!bossDropRegionBreakCaches[k].get_activeSelf())
				{
					val = bossDropRegionBreakCaches[k];
					val.SetActive(true);
					break;
				}
			}
			if (val == null)
			{
				Transform val4 = ResourceUtility.Realizes(MonoBehaviourSingleton<InGameLinkResourcesQuest>.I.bossDropRegionBreak, MonoBehaviourSingleton<StageObjectManager>.I._transform, -1);
				if (val4 != null)
				{
					val = val4.get_gameObject();
					bossDropRegionBreakCaches.Add(val);
				}
			}
			break;
		}
		case 1:
		{
			int j = 0;
			for (int count2 = bossDropRCaches.Count; j < count2; j++)
			{
				if (!bossDropRCaches[j].get_activeSelf())
				{
					val = bossDropRCaches[j];
					val.SetActive(true);
					break;
				}
			}
			if (val == null)
			{
				Transform val3 = ResourceUtility.Realizes(MonoBehaviourSingleton<InGameLinkResourcesQuest>.I.bossDropR, MonoBehaviourSingleton<StageObjectManager>.I._transform, -1);
				if (val3 != null)
				{
					val = val3.get_gameObject();
					bossDropRCaches.Add(val);
				}
			}
			break;
		}
		default:
		{
			int i = 0;
			for (int count = bossDropNCaches.Count; i < count; i++)
			{
				if (!bossDropNCaches[i].get_activeSelf())
				{
					val = bossDropNCaches[i];
					val.SetActive(true);
					break;
				}
			}
			if (val == null)
			{
				Transform val2 = ResourceUtility.Realizes(MonoBehaviourSingleton<InGameLinkResourcesQuest>.I.bossDropN, MonoBehaviourSingleton<StageObjectManager>.I._transform, -1);
				if (val2 != null)
				{
					val = val2.get_gameObject();
					bossDropNCaches.Add(val);
				}
			}
			break;
		}
		}
		return val;
	}

	public void CreateDropObject(Coop_Model_EnemyDefeat model, List<DropDeliveryInfo> deliveryList, List<DropItemInfo> itemList)
	{
		FieldDropObject fieldDropObject = FieldDropObject.Create(model, deliveryList, itemList);
		if (fieldDropObject != null)
		{
			dropList.Add(fieldDropObject);
			if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameProgress>.I.isHappenQuestDirection)
			{
				OpenAllDropObject();
			}
		}
	}

	public void CreateDropInfoList(Coop_Model_EnemyDefeat model, out List<DropDeliveryInfo> deliveryList, out List<DropItemInfo> itemList)
	{
		itemList = new List<DropItemInfo>();
		deliveryList = new List<DropDeliveryInfo>();
		int i = 0;
		for (int count = model.dropIds.Count; i < count; i++)
		{
			itemList.Add(new DropItemInfo((REWARD_TYPE)model.dropTypes[i], (uint)model.dropItemIds[i], model.dropNums[i]));
		}
		int mapId = MonoBehaviourSingleton<FieldManager>.I.GetMapId();
		Delivery[] deliveryList2 = MonoBehaviourSingleton<DeliveryManager>.I.GetDeliveryList(false);
		int j = 0;
		for (int num = deliveryList2.Length; j < num; j++)
		{
			DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)deliveryList2[j].dId);
			if (deliveryTableData != null)
			{
				int k = 0;
				for (int num2 = deliveryTableData.needs.Length; k < num2; k++)
				{
					uint num3 = (uint)k;
					if (deliveryTableData.IsNeedTarget(num3, (uint)model.eid, (uint)mapId) && (model.deliver & (1 << (int)deliveryTableData.GetRateType(num3))) > 0)
					{
						int have = 0;
						int need = 0;
						MonoBehaviourSingleton<DeliveryManager>.I.GetProgressDelivery(deliveryList2[j].dId, out have, out need, num3);
						if (have < need)
						{
							int num4 = 1;
							if ((model.boostBit & (1 << (int)deliveryTableData.GetRateType(num3))) > 0)
							{
								num4 += model.boostNum;
							}
							deliveryList.Add(new DropDeliveryInfo(deliveryList2[j].dId, (int)num3, deliveryTableData.name, deliveryTableData.GetNeedItemName(num3), num4, new List<DELIVERY_CONDITION_TYPE>
							{
								deliveryTableData.GetConditionType(0u),
								deliveryTableData.GetConditionType(1u),
								deliveryTableData.GetConditionType(2u),
								deliveryTableData.GetConditionType(3u),
								deliveryTableData.GetConditionType(4u)
							}));
						}
					}
				}
			}
		}
	}

	public void OpenAllDropObject()
	{
		for (int i = 0; i < dropList.Count; i++)
		{
			dropList[i].OpenDropObject();
		}
	}

	public void DeleteDropObject(FieldDropObject obj)
	{
		dropList.Remove(obj);
	}

	public void DeleteDropObject(int reward_id, bool is_get)
	{
		FieldDropObject fieldDropObject = dropList.Find((FieldDropObject o) => o.rewardId == reward_id);
		if (fieldDropObject != null)
		{
			fieldDropObject.Delete(is_get);
		}
		dropList.Remove(fieldDropObject);
	}

	public void ClearAllDrop()
	{
		dropList.Clear();
		ClearDrop(dropNCaches);
		ClearDrop(dropHNCaches);
		ClearDrop(dropRCaches);
		ClearDrop(dropLoungeCaches);
		ClearDrop(bossDropNCaches);
		ClearDrop(bossDropRCaches);
		ClearDrop(bossDropRegionBreakCaches);
		ClearDrop(dropSPNCaches);
		ClearDrop(dropSPHNCaches);
		ClearDrop(dropSPRCaches);
		ClearDrop(dropHalloweenCaches);
		ClearDrop(dropESPNCaches);
		ClearDrop(dropESPHNCaches);
		ClearDrop(dropESPRCaches);
		ClearDrop(dropSeasonalCaches);
	}

	public void ClearDrop(List<GameObject> dropCaches)
	{
		int i = 0;
		for (int count = dropCaches.Count; i < count; i++)
		{
			Object.Destroy(dropCaches[i]);
		}
		dropCaches.Clear();
	}

	public GameObject CreateTreasureBox(UIDropAnnounce.COLOR color)
	{
		switch (color)
		{
		case UIDropAnnounce.COLOR.NORMAL:
			return RealizeTreasureBox(dropNCaches, MonoBehaviourSingleton<InGameLinkResourcesField>.I.dropItemBoxN);
		case UIDropAnnounce.COLOR.DELIVERY:
			return RealizeTreasureBox(dropHNCaches, MonoBehaviourSingleton<InGameLinkResourcesField>.I.dropItemBoxHN);
		case UIDropAnnounce.COLOR.RARE:
			return RealizeTreasureBox(dropRCaches, MonoBehaviourSingleton<InGameLinkResourcesField>.I.dropItemBoxR);
		case UIDropAnnounce.COLOR.LOUNGE:
			return RealizeTreasureBox(dropLoungeCaches, MonoBehaviourSingleton<InGameLinkResourcesField>.I.dropItemLoungeShare);
		case UIDropAnnounce.COLOR.SP_N:
			return RealizeTreasureBox(dropSPNCaches, MonoBehaviourSingleton<InGameLinkResourcesField>.I.dropItemBoxSPN);
		case UIDropAnnounce.COLOR.SP_HN:
			return RealizeTreasureBox(dropSPHNCaches, MonoBehaviourSingleton<InGameLinkResourcesField>.I.dropItemBoxSPHN);
		case UIDropAnnounce.COLOR.SP_R:
			return RealizeTreasureBox(dropSPRCaches, MonoBehaviourSingleton<InGameLinkResourcesField>.I.dropItemBoxSPR);
		case UIDropAnnounce.COLOR.HALLOWEEN:
			return RealizeTreasureBox(dropHalloweenCaches, MonoBehaviourSingleton<InGameLinkResourcesField>.I.dropItemHalloween);
		case UIDropAnnounce.COLOR.ESP_N:
			return RealizeTreasureBox(dropESPNCaches, MonoBehaviourSingleton<InGameLinkResourcesField>.I.dropItemBoxESPN);
		case UIDropAnnounce.COLOR.ESP_HN:
			return RealizeTreasureBox(dropESPHNCaches, MonoBehaviourSingleton<InGameLinkResourcesField>.I.dropItemBoxESPHN);
		case UIDropAnnounce.COLOR.ESP_R:
			return RealizeTreasureBox(dropESPRCaches, MonoBehaviourSingleton<InGameLinkResourcesField>.I.dropItemBoxESPR);
		case UIDropAnnounce.COLOR.SEASONAL:
			return RealizeTreasureBox(dropSeasonalCaches, MonoBehaviourSingleton<InGameLinkResourcesField>.I.dropItemSeasonal);
		default:
			return null;
		}
	}

	private GameObject RealizeTreasureBox(List<GameObject> caches, GameObject prefab)
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Expected O, but got Unknown
		int i = 0;
		for (int count = caches.Count; i < count; i++)
		{
			if (caches[i] != null && !caches[i].get_activeSelf())
			{
				GameObject val = caches[i];
				val.SetActive(true);
				return val;
			}
		}
		Transform val2 = ResourceUtility.Realizes(prefab, MonoBehaviourSingleton<StageObjectManager>.I._transform, -1);
		if (val2 != null)
		{
			GameObject val3 = val2.get_gameObject();
			caches.Add(val3);
			return val3;
		}
		return null;
	}

	public List<UIDropAnnounce.DropAnnounceInfo> CreateDropAnnounceInfoList(List<DropDeliveryInfo> deliveryInfo, List<DropItemInfo> itemInfo, bool isTreasureBox)
	{
		List<UIDropAnnounce.DropAnnounceInfo> list = new List<UIDropAnnounce.DropAnnounceInfo>();
		int i = 0;
		for (int count = deliveryInfo.Count; i < count; i++)
		{
			bool flag = MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery(deliveryInfo[i].id);
			MonoBehaviourSingleton<DeliveryManager>.I.ProgressDelivery(deliveryInfo[i].id, deliveryInfo[i].index, deliveryInfo[i].num);
			int have = 0;
			int need = 0;
			MonoBehaviourSingleton<DeliveryManager>.I.GetProgressDelivery(deliveryInfo[i].id, out have, out need, (uint)deliveryInfo[i].index);
			UIDropAnnounce.DropAnnounceInfo dropAnnounceInfo = new UIDropAnnounce.DropAnnounceInfo();
			dropAnnounceInfo.text = StringTable.Format(STRING_CATEGORY.IN_GAME, 2001u, deliveryInfo[i].itemName, deliveryInfo[i].num, have, need);
			dropAnnounceInfo.color = UIDropAnnounce.COLOR.DELIVERY;
			list.Add(dropAnnounceInfo);
			if (have >= need)
			{
				GameSection currentSection = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection();
				if (currentSection != null)
				{
					InGameMain component = currentSection.GetComponent<InGameMain>();
					if (component != null)
					{
						component.OnNoticeCompletedDelivery();
					}
				}
				if (flag != MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery(deliveryInfo[i].id) && MonoBehaviourSingleton<UIAnnounceBand>.IsValid())
				{
					string empty = string.Empty;
					empty = ((!DeliveryManager.IsDeliveryBingo((uint)deliveryInfo[i].id)) ? StringTable.Get(STRING_CATEGORY.DELIVERY_COMPLETE, 0u) : StringTable.Get(STRING_CATEGORY.DELIVERY_COMPLETE, 2u));
					MonoBehaviourSingleton<UIAnnounceBand>.I.SetAnnounce(deliveryInfo[i].name, empty);
					SoundManager.PlayOneshotJingle(40000030, null, null);
					MonoBehaviourSingleton<CoopManager>.I.coopStage.fieldRewardPool.SendFieldDrop(null);
				}
				if (MonoBehaviourSingleton<DropTargetMarkerManeger>.IsValid())
				{
					MonoBehaviourSingleton<DropTargetMarkerManeger>.I.UpdateList();
				}
			}
		}
		if (MonoBehaviourSingleton<InventoryManager>.IsValid() && isTreasureBox)
		{
			int j = 0;
			for (int count2 = itemInfo.Count; j < count2; j++)
			{
				MonoBehaviourSingleton<InventoryManager>.I.AddInGameTempItem(itemInfo[j].id, itemInfo[j].num);
				bool is_rare = false;
				list.Add(itemInfo[j].CreateAnnounceInfo(out is_rare));
				if (is_rare)
				{
					MonoBehaviourSingleton<StageObjectManager>.I.self.OnGetRareDrop(itemInfo[j].type, (int)itemInfo[j].id);
				}
			}
		}
		return list;
	}

	public void SetIntervalTransferInfo(bool enable_limit_time, float remaind_time, float elapsed_time, bool transfer_other, bool keep_dead, bool isReentry, bool isQuestToField)
	{
		intervalTransferInfo = new IntervalTransferInfo();
		if (enable_limit_time)
		{
			intervalTransferInfo.remaindTime = remaind_time;
		}
		else
		{
			intervalTransferInfo.remaindTime = -1f;
		}
		intervalTransferInfo.elapsedTime = elapsed_time;
		int i = 0;
		for (int count = MonoBehaviourSingleton<StageObjectManager>.I.playerList.Count; i < count; i++)
		{
			Player player = MonoBehaviourSingleton<StageObjectManager>.I.playerList[i] as Player;
			if (!(player == null) && (transfer_other || player is Self))
			{
				if (!keep_dead && player.hp <= 0)
				{
					player.hp = 1;
				}
				if (isQuestToField)
				{
					player.hp = player.hpMax;
				}
				if (MonoBehaviourSingleton<InGameManager>.I.IsRush() && !keep_dead)
				{
					int currentRushStandupHpPer = MonoBehaviourSingleton<InGameManager>.I.GetCurrentRushStandupHpPer();
					int num = (int)((float)player.hpMax * ((float)currentRushStandupHpPer / 100f));
					player.hp = Mathf.Max(player.hp, num);
				}
				IntervalTransferInfo.PlayerInfo playerInfo = new IntervalTransferInfo.PlayerInfo();
				playerInfo.id = player.id;
				playerInfo.createInfo = player.createInfo;
				playerInfo.transferInfo = player.CreateTransferInfo();
				if (MonoBehaviourSingleton<InGameManager>.I.IsRush() && !isReentry)
				{
					int currentRushRescureResetNum = MonoBehaviourSingleton<InGameManager>.I.GetCurrentRushRescureResetNum();
					if (currentRushRescureResetNum > 0)
					{
						playerInfo.transferInfo.rescueCount = Mathf.Max(0, playerInfo.transferInfo.rescueCount - currentRushRescureResetNum);
					}
				}
				playerInfo.isSelf = (player is Self);
				playerInfo.coopMode = player.coopMode;
				playerInfo.coopClientId = player.coopClientId;
				playerInfo.isNpcController = (player.controller is NpcController);
				playerInfo.isCoopPlayer = false;
				if (player is Self)
				{
					Self self = player as Self;
					if (self != null)
					{
						playerInfo.taskChecker = self.taskChecker;
					}
				}
				if (player.coopClientId != 0 && MonoBehaviourSingleton<CoopManager>.IsValid())
				{
					CoopClient coopClient = MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.FindByPlayerId(player.id);
					if (coopClient != null)
					{
						playerInfo.coopClientId = coopClient.clientId;
						playerInfo.isCoopPlayer = true;
					}
				}
				intervalTransferInfo.playerInfoList.Add(playerInfo);
			}
		}
	}

	public void SetIntervalTransferSelf()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Expected O, but got Unknown
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		if (!(selfCacheObject != null))
		{
			selfCacheObject = new GameObject();
			selfCacheObject.set_name("SelfCacheObject");
			selfCacheObject.get_transform().set_parent(MonoBehaviourSingleton<AppMain>.I._transform);
			Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
			self.OnCached();
			GameObject val = self.get_gameObject();
			val.get_transform().set_parent(selfCacheObject.get_transform());
			val.get_gameObject().set_name("SelfCache");
			val.get_gameObject().SetActive(false);
		}
	}

	public void DestroySelfCache()
	{
		if (!(selfCacheObject == null))
		{
			Object.Destroy(selfCacheObject);
			selfCacheObject = null;
		}
	}

	public void SetEnableIntervalTransferInfoRemaindTimeUpdate()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		if (updateIntervalTransferInfoRemaindTime == null)
		{
			updateIntervalTransferInfoRemaindTime = UpdateIntervalTransferInfoRemaindTime();
			this.StartCoroutine(updateIntervalTransferInfoRemaindTime);
		}
	}

	public void StopIntervalTransferInfoRemaindTimeUpdate()
	{
		if (updateIntervalTransferInfoRemaindTime != null)
		{
			this.StopCoroutine(updateIntervalTransferInfoRemaindTime);
			updateIntervalTransferInfoRemaindTime = null;
		}
	}

	private IEnumerator UpdateIntervalTransferInfoRemaindTime()
	{
		float startTime = Time.get_realtimeSinceStartup();
		float startRemaindTime = intervalTransferInfo.remaindTime;
		while (intervalTransferInfo != null)
		{
			intervalTransferInfo.remaindTime = startRemaindTime - (Time.get_realtimeSinceStartup() - startTime);
			yield return (object)null;
		}
	}

	public void SaveQuestTransferInfo()
	{
		if (questTransferInfo == null)
		{
			questTransferInfo = new QuestTransferInfo();
		}
		questTransferInfo.intervalTransferInfo = intervalTransferInfo;
		questTransferInfo.isQuestHappen = isQuestHappen;
		questTransferInfo.isQuestGate = isQuestGate;
		questTransferInfo.isQuestPortal = isQuestPortal;
		questTransferInfo.isGateQuestClear = isGateQuestClear;
		questTransferInfo.isTransitionFieldToQuest = isTransitionFieldToQuest;
		questTransferInfo.isTransitionQuestToField = isTransitionQuestToField;
		questTransferInfo.isTransitionFieldReentry = isTransitionFieldReentry;
		questTransferInfo.isStoryPortal = isStoryPortal;
		questTransferInfo.readStoryID = readStoryID;
		questTransferInfo.beforePortalID = beforePortalID;
		questTransferInfo.backTransitionInfo = backTransitionInfo;
	}

	public void ResumeQuestTransferInfo()
	{
		if (questTransferInfo != null)
		{
			intervalTransferInfo = questTransferInfo.intervalTransferInfo;
			isQuestHappen = questTransferInfo.isQuestHappen;
			isQuestGate = questTransferInfo.isQuestGate;
			isQuestPortal = questTransferInfo.isQuestPortal;
			isGateQuestClear = questTransferInfo.isGateQuestClear;
			isTransitionFieldToQuest = questTransferInfo.isTransitionFieldToQuest;
			isTransitionQuestToField = questTransferInfo.isTransitionQuestToField;
			isTransitionFieldReentry = questTransferInfo.isTransitionFieldReentry;
			isStoryPortal = questTransferInfo.isStoryPortal;
			readStoryID = questTransferInfo.readStoryID;
			beforePortalID = questTransferInfo.beforePortalID;
			backTransitionInfo = questTransferInfo.backTransitionInfo;
			questTransferInfo = null;
		}
	}

	public bool IsDisableEffectGraphicLow(string effectName)
	{
		if (effectName.Length == 0)
		{
			return false;
		}
		if (graphicOptionType <= 0)
		{
			for (int i = 0; i < disableEffectsGraphicLow.Length; i++)
			{
				if (effectName.StartsWith(disableEffectsGraphicLow[i]))
				{
					return true;
				}
			}
		}
		return false;
	}
}
