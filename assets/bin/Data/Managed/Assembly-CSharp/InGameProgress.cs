using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameProgress : MonoBehaviourSingleton<InGameProgress>
{
	private enum AUDIO
	{
		RESULT_WIN = 40000067,
		RESULT_LOSE = 40000073,
		QUEST_START = 40000072,
		TIME_BONUS = 40000158
	}

	public enum PROGRESS_END_TYPE
	{
		NONE,
		QUEST_VICTORY,
		QUEST_RETIRE,
		QUEST_RETRY,
		QUEST_INVITEQUIT,
		QUEST_TIMEUP,
		QUEST_SERIES_INTERVAL,
		FIELD_MAP_INTERVAL,
		FIELD_TO_QUEST_INTERVAL,
		FIELD_TO_HOME,
		FIELD_RETIRE,
		FIELD_REENTRY,
		FIELD_MAP_INTERVAL_TUTORIAL,
		FORCE_DEFEAT,
		FIELD_TO_STORY,
		FIELD_TO_HOME_TIMEOUT,
		EXPLORE_MOVE_INTERVAL,
		EXPLORE_HAPPEN_INTERVAL,
		RUSH_INTERVAL,
		ARENA_INTERVAL,
		FIELD_READ_STORY,
		QUEST_TO_FIELD,
		QUEST_TO_QUEST_REPEAT
	}

	public enum eFieldGimmick
	{
		Cannon,
		Sonar,
		ReadStory,
		GatherGimmick,
		Bingo,
		Chat,
		Max
	}

	private class GimmickSearchInfo
	{
		public float dist;

		public IFieldGimmickObject obj;
	}

	public const string HAPPEN_QUEST_HIDE_OBJECT_NAME = "HideObjects";

	private const float TIME_AFK_LIMIT = 480f;

	private Transform viewFx;

	private bool isRecvQuestComplete;

	private bool isRecvRushProgress;

	private bool isRecvArenaProgress;

	public List<MissionCheckBase> missionCheck = new List<MissionCheckBase>();

	private bool isEndFieldBuffAnnounce;

	protected bool _isGameProgressStop;

	protected float startTime = -1f;

	protected float stopTime = -1f;

	private float elapsedTime;

	private float rushRemainTime;

	public List<QuestRushProgressData.RushTimeBonus> rushTimeBonus;

	private float bossMoveRemainTime;

	private float exploreHostDCRemainTime;

	private bool requestedExploreAlive;

	private XorFloat arenaRemainSec;

	private XorFloat arenaElapsedSec;

	public List<QuestArenaProgressData.ArenaTimeBonus> arenaTimeBonus;

	private float afkTime;

	private bool enableAfkTime;

	protected float startVictoryIntervalTime = -1f;

	protected float npcCheckTimeCount;

	[NonSerialized]
	public PortalObject checkPortalObject;

	private GimmickSearchInfo[] gimmickSearchInfo;

	private WaveMatchDropResource wmDropResource;

	protected uint toFieldPortalID;

	protected uint toQuestID;

	protected uint toQuestPortalID;

	protected bool toQuestGate;

	protected int fieldReadStoryId;

	protected bool isFieldReadStorySend;

	protected bool isDecidedHappenQuestDialog;

	protected bool isYesHappenQuestDialog;

	protected bool _endHappenQuestDirection;

	private bool forceComplete;

	protected float waveMatchHostRetireDelay = 1f;

	private Coroutine waitNetworkCoroutine;

	public bool isBattleStart
	{
		get;
		protected set;
	}

	public PROGRESS_END_TYPE progressEndType
	{
		get;
		protected set;
	}

	public bool isEnding => progressEndType != PROGRESS_END_TYPE.NONE;

	public bool isGameProgressStop
	{
		get
		{
			return _isGameProgressStop;
		}
		protected set
		{
			_isGameProgressStop = value;
			if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
			{
				List<StageObject> playerList = MonoBehaviourSingleton<StageObjectManager>.I.playerList;
				int i = 0;
				for (int count = playerList.Count; i < count; i++)
				{
					Player player = playerList[i] as Player;
					if (player.IsOriginal() || player.IsCoopNone())
					{
						player.StopCounter(value);
					}
				}
			}
		}
	}

	public bool disableSendProgressStop
	{
		get;
		protected set;
	}

	public bool isWaitContinueProtocol
	{
		get;
		set;
	}

	public float limitTime
	{
		get;
		protected set;
	}

	public bool enableLimitTime
	{
		get;
		protected set;
	}

	public bool isInitStartTime
	{
		get;
		protected set;
	}

	public float remaindTime
	{
		get
		{
			float num = limitTime - GetElapsedTime();
			if (num < 0f)
			{
				num = 0f;
			}
			return num;
		}
	}

	private float remainedAfkTime
	{
		get
		{
			float num = afkTime;
			if (num < 0f)
			{
				num = 0f;
			}
			return num;
		}
	}

	public int defeatCount
	{
		get;
		protected set;
	}

	public int defeatBossCount
	{
		get;
		protected set;
	}

	public int partyDefeatBossCount
	{
		get;
		protected set;
	}

	public bool enableVictoryIntervalTime
	{
		get;
		protected set;
	}

	public float victoryIntervalTime
	{
		get
		{
			float num = MonoBehaviourSingleton<InGameSettingsManager>.I.inGameProgress.victoryIntervalTime;
			if (startVictoryIntervalTime >= 0f)
			{
				num -= Time.get_time() - startVictoryIntervalTime;
			}
			if (num < 0f)
			{
				num = 0f;
			}
			return num;
		}
	}

	public List<PortalObject> portalObjectList
	{
		get;
		protected set;
	}

	public List<GatherPointObject> gatherPointList
	{
		get;
		protected set;
	}

	public List<IFieldGimmickObject>[] fieldGimmickList
	{
		get;
		protected set;
	}

	public UIntKeyTable<LoadObject> gatherPointModelTable
	{
		get;
		protected set;
	}

	public StringKeyTable<LoadObject> gatherPointToolTable
	{
		get;
		protected set;
	}

	public LoadObject fieldHealingPointModel
	{
		get;
		protected set;
	}

	public UIntKeyTable<LoadObject> fieldGimmickModelTable
	{
		get;
		protected set;
	}

	protected bool isQuestHappen => toQuestID != 0 && toQuestPortalID == 0;

	protected bool isQuestGate => toQuestID != 0 && toQuestPortalID != 0 && toQuestGate;

	protected bool isQuestPortal => toQuestID != 0 && toQuestPortalID != 0 && !toQuestGate;

	public bool isSendCompleteError
	{
		get;
		protected set;
	}

	public bool isHappenQuestDirection
	{
		get;
		protected set;
	}

	public bool endHappenQuestDirection
	{
		get
		{
			return _endHappenQuestDirection;
		}
		protected set
		{
			_endHappenQuestDirection = value;
		}
	}

	public float defenseBattleEndurance
	{
		get;
		private set;
	}

	public float defenseBattleEnduranceMax
	{
		get;
		private set;
	}

	public InGameProgress()
	{
		isBattleStart = false;
		progressEndType = PROGRESS_END_TYPE.NONE;
		isGameProgressStop = false;
		isHappenQuestDirection = false;
	}

	public void SetDefenseBattleEndurance(float endurance)
	{
		defenseBattleEndurance = endurance;
	}

	public void SetDefenseBattleEnduranceMax(float endurance)
	{
		defenseBattleEnduranceMax = endurance;
	}

	public void DamageToEndurance(int damage)
	{
		if (damage >= 0 && (MonoBehaviourSingleton<CoopManager>.I.coopRoom.isOfflinePlay || MonoBehaviourSingleton<CoopManager>.I.isStageHost))
		{
			defenseBattleEndurance -= (float)damage;
			MonoBehaviourSingleton<CoopManager>.I.coopRoom.packetSender.SendSyncDefenseBattle(defenseBattleEndurance);
		}
	}

	protected override void Awake()
	{
		base.Awake();
		if (QuestManager.IsValidInGameExplore())
		{
			List<MissionCheckBase> missions = MonoBehaviourSingleton<QuestManager>.I.GetExploreStatus().GetMissions();
			if (missions != null)
			{
				missionCheck = missions;
			}
			bossMoveRemainTime = MonoBehaviourSingleton<QuestManager>.I.GetExploreBossMoveRemainTime();
			exploreHostDCRemainTime = MonoBehaviourSingleton<QuestManager>.I.GetExploreHostDCRemainTime();
		}
		if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			waveMatchHostRetireDelay = MonoBehaviourSingleton<InGameSettingsManager>.I.GetWaveMatchParam().hostRetireDelay;
		}
		else
		{
			waveMatchHostRetireDelay = 1f;
		}
		_InitGimmickList();
	}

	private void Update()
	{
		if (isBattleStart && progressEndType == PROGRESS_END_TYPE.NONE)
		{
			if (InGameManager.IsReentry() && MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.I.coopMyClient.isLeave)
			{
				MonoBehaviourSingleton<InGameProgress>.I.FieldReentry();
			}
			Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
			if (QuestManager.IsValidInGame())
			{
				if (!IsValidEnemy())
				{
					BattleComplete(false);
					return;
				}
				if (remaindTime <= 0f && ((MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.I.isStageHost) || !MonoBehaviourSingleton<CoopManager>.IsValid()))
				{
					BattleTimeup();
					return;
				}
				if (MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsDefenseBattle() && defenseBattleEndurance <= 0f)
				{
					BattleRetire();
					return;
				}
				if (QuestManager.IsValidInGameWaveMatch(false) && (!MonoBehaviourSingleton<StageObjectManager>.IsValid() || MonoBehaviourSingleton<StageObjectManager>.I.IsWaveMatchTargetAllDead()))
				{
					if (BattleRetire())
					{
						string text = StringTable.Get(STRING_CATEGORY.IN_GAME, 112u);
						UIInGamePopupDialog.PushOpen(text, true, 1.8f);
					}
					return;
				}
				if (MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.I.coopRoom.forceRetire)
				{
					if (BattleRetire())
					{
						if (MonoBehaviourSingleton<CoopManager>.I.coopRoom.ownerRetire)
						{
							string text2 = StringTable.Get(STRING_CATEGORY.IN_GAME, 110u);
							UIInGamePopupDialog.PushOpen(text2, true, 1.8f);
						}
						else
						{
							string text3 = StringTable.Get(STRING_CATEGORY.IN_GAME, 111u);
							UIInGamePopupDialog.PushOpen(text3, true, 1.8f);
						}
					}
					return;
				}
				npcCheckTimeCount -= Time.get_deltaTime();
				if (npcCheckTimeCount <= 0f)
				{
					if (MonoBehaviourSingleton<CoopManager>.IsValid())
					{
						if (QuestManager.IsValidInGameDefenseBattle())
						{
							CoopStageObjectUtility.DestroyAllNonPlayer();
						}
						else
						{
							CoopStageObjectUtility.ShrinkOriginalNonPlayer(4);
						}
					}
					npcCheckTimeCount = MonoBehaviourSingleton<InGameSettingsManager>.I.inGameProgress.npcCheckIntervalTime;
				}
			}
			else if (FieldManager.IsValidInGameNoQuest())
			{
				if (MonoBehaviourSingleton<InputManager>.IsValid() && MonoBehaviourSingleton<InputManager>.I.IsTouchIgnoreHit())
				{
					ResetAfkTimer();
				}
				if (TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.USER_CREATE_02) && !self.isAutoMode)
				{
					ProgressAfkTimer();
				}
				if (remainedAfkTime <= 0f && MonoBehaviourSingleton<InGameProgress>.IsValid())
				{
					FieldToHomeTimeout();
				}
				if (MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.I.isStageHost && MonoBehaviourSingleton<CoopManager>.I.coopStage.HasFieldEnemyBossLimitTime() && remaindTime <= 0f)
				{
					OnHostEnmeyBossTimeUp();
					return;
				}
			}
			if (QuestManager.IsValidInGameExplore() && !MonoBehaviourSingleton<QuestManager>.I.IsExploreBossMap() && MonoBehaviourSingleton<CoopManager>.I.coopMyClient.isPartyOwner)
			{
				ProgressExploreBossMoveTimer();
				if (bossMoveRemainTime <= 0f)
				{
					uint bossMapId = MonoBehaviourSingleton<QuestManager>.I.GetBossMapId();
					MonoBehaviourSingleton<QuestManager>.I.UpdateBossAppearMap();
					if (bossMapId != MonoBehaviourSingleton<QuestManager>.I.GetBossMapId())
					{
						MonoBehaviourSingleton<CoopManager>.I.coopRoom.packetSender.SendSyncExploreBossMap(MonoBehaviourSingleton<QuestManager>.I.GetExploreBossAppearMapId(), -1);
					}
					SetExploreBossMoveTimer();
				}
			}
			if (QuestManager.IsValidInGameExplore())
			{
				if (MonoBehaviourSingleton<CoopManager>.I.coopMyClient.isPartyOwner)
				{
					ResetExploreHostDCTimer();
				}
				else
				{
					ProgressExploreHostDCTimer();
				}
				if (exploreHostDCRemainTime <= 0f)
				{
					if (requestedExploreAlive)
					{
						if (BattleRetire())
						{
							string text4 = StringTable.Get(STRING_CATEGORY.IN_GAME, 111u);
							UIInGamePopupDialog.PushOpen(text4, true, 2f);
						}
						return;
					}
					SetExploreHostDCTimer(5f, true);
					if (MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.I.coopRoom != null && MonoBehaviourSingleton<CoopManager>.I.coopRoom.packetSender != null)
					{
						MonoBehaviourSingleton<CoopManager>.I.coopRoom.packetSender.SendExploreAliveRequest();
					}
				}
				if (MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.I.coopRoom.ownerRetire && !MonoBehaviourSingleton<CoopManager>.I.coopMyClient.isPartyOwner)
				{
					if (BattleRetire())
					{
						string text5 = StringTable.Get(STRING_CATEGORY.IN_GAME, 110u);
						UIInGamePopupDialog.PushOpen(text5, true, 1.8f);
					}
					return;
				}
			}
			float distance = 3.40282347E+38f;
			GatherPointObject nearestPoint = null;
			float nearestDistance = 3.40282347E+38f;
			Enemy nearestEnemy = null;
			if (gatherPointList != null)
			{
				GetNearestGatherPoint(out distance, out nearestPoint);
			}
			if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
			{
				GetNearestCamouflagingEnemy(out nearestDistance, out nearestEnemy);
			}
			_ClearGimmickSearchInfo();
			int i = 0;
			for (int num = 6; i < num; i++)
			{
				if (i != 0 || self.cannonState == Player.CANNON_STATE.NONE)
				{
					_SearchNearestGimmickPoint(i);
				}
			}
			float num2 = 0f;
			if (distance <= nearestDistance)
			{
				self.nearGatherPoint = nearestPoint;
				nearestEnemy = null;
				num2 = distance;
			}
			else
			{
				self.nearGatherPoint = null;
				nearestPoint = null;
				num2 = nearestDistance;
			}
			self.nearFieldGimmick = null;
			int j = 0;
			int num3;
			for (num3 = this.gimmickSearchInfo.Length; j < num3; j++)
			{
				GimmickSearchInfo gimmickSearchInfo = this.gimmickSearchInfo[j];
				if (gimmickSearchInfo.obj != null && gimmickSearchInfo.dist <= num2)
				{
					num2 = gimmickSearchInfo.dist;
					self.nearFieldGimmick = gimmickSearchInfo.obj;
					self.nearGatherPoint = null;
					nearestPoint = null;
					nearestEnemy = null;
				}
			}
			if (self.nearFieldGimmick == null)
			{
				for (j = 0; j < num3; j++)
				{
					this.gimmickSearchInfo[j].obj = null;
				}
			}
			if (gatherPointList != null)
			{
				int k = 0;
				for (int count = gatherPointList.Count; k < count; k++)
				{
					gatherPointList[k].UpdateTargetMarker(gatherPointList[k] == nearestPoint);
				}
			}
			if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
			{
				List<Enemy> enemyList = MonoBehaviourSingleton<StageObjectManager>.I.EnemyList;
				for (int l = 0; l < enemyList.Count; l++)
				{
					Enemy enemy = enemyList[l];
					if (enemy.isHiding)
					{
						bool isNear = enemy == nearestEnemy;
						enemy.UpdateGatherTargetMarker(isNear);
					}
				}
			}
			_UpdateGimmickTargetMarker();
			if (!isEndFieldBuffAnnounce && MonoBehaviourSingleton<UIEnemyAnnounce>.IsValid() && MonoBehaviourSingleton<UIEnemyAnnounce>.I.get_isActiveAndEnabled())
			{
				MonoBehaviourSingleton<UIEnemyAnnounce>.I.RequestFieldBuffAnnounce();
				isEndFieldBuffAnnounce = true;
			}
		}
	}

	protected override void _OnDestroy()
	{
		if (QuestManager.IsValidInGameExplore())
		{
			MonoBehaviourSingleton<QuestManager>.I.UpdateExploreBossMoveRemainTime(bossMoveRemainTime);
			MonoBehaviourSingleton<QuestManager>.I.UpdateExploreHostDCTime(exploreHostDCRemainTime);
		}
		if (wmDropResource != null)
		{
			wmDropResource.Clear();
		}
		wmDropResource = null;
	}

	private void GetNearestGatherPoint(out float distance, out GatherPointObject nearestPoint)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
		float num = 3.40282347E+38f;
		GatherPointObject gatherPointObject = null;
		int i = 0;
		for (int count = gatherPointList.Count; i < count; i++)
		{
			GatherPointObject gatherPointObject2 = gatherPointList[i];
			Vector3 val = gatherPointObject2._transform.get_position() - self._position;
			float magnitude = val.get_magnitude();
			if (!gatherPointObject2.isGathered && magnitude < num && magnitude < gatherPointObject2.viewData.targetRadius)
			{
				gatherPointObject = gatherPointObject2;
				num = magnitude;
			}
		}
		distance = num;
		nearestPoint = gatherPointObject;
	}

	private void GetNearestCamouflagingEnemy(out float nearestDistance, out Enemy nearestEnemy)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		Vector3 position = MonoBehaviourSingleton<StageObjectManager>.I.self._transform.get_position();
		nearestDistance = 3.40282347E+38f;
		nearestEnemy = null;
		List<Enemy> enemyList = MonoBehaviourSingleton<StageObjectManager>.I.EnemyList;
		for (int i = 0; i < enemyList.Count; i++)
		{
			Enemy enemy = enemyList[i];
			if (enemy.isHiding)
			{
				Vector3 position2 = enemy._transform.get_position();
				float num = Vector3.Distance(position, position2);
				if (num < nearestDistance)
				{
					nearestDistance = num;
					nearestEnemy = enemy;
				}
			}
		}
	}

	private void AddFieldGimmickObj(eFieldGimmick type, IFieldGimmickObject obj)
	{
		fieldGimmickList[(int)type].Add(obj);
	}

	public IFieldGimmickObject GetFieldGimmickObj(eFieldGimmick type, int id)
	{
		if (fieldGimmickList[(int)type].IsNullOrEmpty())
		{
			return null;
		}
		for (int i = 0; i < fieldGimmickList[(int)type].Count; i++)
		{
			IFieldGimmickObject fieldGimmickObject = fieldGimmickList[(int)type][i];
			if (fieldGimmickObject != null && id == fieldGimmickObject.GetId())
			{
				return fieldGimmickObject;
			}
		}
		return null;
	}

	public void UpdatGatherGimmickInfo(int id, int playerId, bool isUsed)
	{
		FieldGatherGimmickObject fieldGatherGimmickObject = GetFieldGimmickObj(eFieldGimmick.GatherGimmick, id) as FieldGatherGimmickObject;
		if (!(fieldGatherGimmickObject == null))
		{
			Player player = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(playerId) as Player;
			if (!(player == null))
			{
				if (isUsed)
				{
					fieldGatherGimmickObject.OnUseStart(player, false);
				}
				else
				{
					fieldGatherGimmickObject.OnUseEnd(player, false);
				}
			}
		}
	}

	protected void _InitGimmickList()
	{
		int num = 6;
		fieldGimmickList = new List<IFieldGimmickObject>[num];
		gimmickSearchInfo = new GimmickSearchInfo[num];
		for (int i = 0; i < num; i++)
		{
			fieldGimmickList[i] = new List<IFieldGimmickObject>();
			gimmickSearchInfo[i] = new GimmickSearchInfo();
		}
	}

	protected void _ClearGimmickList()
	{
		int i = 0;
		for (int num = fieldGimmickList.Length; i < num; i++)
		{
			fieldGimmickList[i].Clear();
		}
	}

	protected void _ClearGimmickSearchInfo()
	{
		int i = 0;
		for (int num = gimmickSearchInfo.Length; i < num; i++)
		{
			gimmickSearchInfo[i].dist = 3.40282347E+38f;
			gimmickSearchInfo[i].obj = null;
		}
	}

	private void _SearchNearestGimmickPoint(eFieldGimmick type)
	{
		_SearchNearestGimmickPoint((int)type);
	}

	private void _SearchNearestGimmickPoint(int typeIndex)
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		if (!fieldGimmickList[typeIndex].IsNullOrEmpty())
		{
			Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
			float num = 3.40282347E+38f;
			int i = 0;
			for (int count = fieldGimmickList[typeIndex].Count; i < count; i++)
			{
				IFieldGimmickObject fieldGimmickObject = fieldGimmickList[typeIndex][i];
				Vector3 val = fieldGimmickObject.GetTransform().get_position() - self._position;
				float sqrMagnitude = val.get_sqrMagnitude();
				float targetSqrRadius = fieldGimmickObject.GetTargetSqrRadius();
				if (sqrMagnitude < num && sqrMagnitude < targetSqrRadius)
				{
					num = sqrMagnitude;
					gimmickSearchInfo[typeIndex].obj = fieldGimmickObject;
					gimmickSearchInfo[typeIndex].dist = sqrMagnitude;
				}
			}
		}
	}

	private void _UpdateGimmickTargetMarker()
	{
		int i = 0;
		for (int num = 6; i < num; i++)
		{
			if (!fieldGimmickList[i].IsNullOrEmpty())
			{
				int j = 0;
				for (int count = fieldGimmickList[i].Count; j < count; j++)
				{
					IFieldGimmickObject fieldGimmickObject = fieldGimmickList[i][j];
					fieldGimmickObject.UpdateTargetMarker(fieldGimmickObject == gimmickSearchInfo[i].obj);
				}
			}
		}
	}

	public void CacheUseResources(LoadingQueue load_queue, ref List<string> loadEffectNames)
	{
		gatherPointModelTable = new UIntKeyTable<LoadObject>();
		gatherPointToolTable = new StringKeyTable<LoadObject>();
		fieldGimmickModelTable = new UIntKeyTable<LoadObject>();
		List<FieldMapTable.FieldGimmickPointTableData> list = new List<FieldMapTable.FieldGimmickPointTableData>();
		if (FieldManager.IsValidInGameNoBoss())
		{
			List<FieldMapTable.GatherPointTableData> gatherPointListByMapID = Singleton<FieldMapTable>.I.GetGatherPointListByMapID(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
			if (gatherPointListByMapID != null)
			{
				int i = 0;
				for (int count = gatherPointListByMapID.Count; i < count; i++)
				{
					FieldMapTable.GatherPointViewTableData gatherPointViewData = Singleton<FieldMapTable>.I.GetGatherPointViewData(gatherPointListByMapID[i].viewID);
					if (gatherPointViewData != null)
					{
						if (gatherPointModelTable.Get(gatherPointViewData.viewID) == null)
						{
							gatherPointModelTable.Add(gatherPointViewData.viewID, load_queue.Load(RESOURCE_CATEGORY.INGAME_GATHER_POINT, ResourceName.GetGatherPointModel(gatherPointViewData.modelID), false));
						}
						if (!string.IsNullOrEmpty(gatherPointViewData.toolModelName) && gatherPointToolTable.Get(gatherPointViewData.toolModelName) == null)
						{
							gatherPointToolTable.Add(gatherPointViewData.toolModelName, load_queue.Load(RESOURCE_CATEGORY.INGAME_GATHER_POINT, gatherPointViewData.toolModelName, false));
						}
						if (!string.IsNullOrEmpty(gatherPointViewData.gatherEffectName))
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, gatherPointViewData.gatherEffectName);
							loadEffectNames.Add(gatherPointViewData.gatherEffectName);
						}
						if (!string.IsNullOrEmpty(gatherPointViewData.targetEffectName))
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, gatherPointViewData.targetEffectName);
							loadEffectNames.Add(gatherPointViewData.targetEffectName);
						}
						if (gatherPointListByMapID[i].gimmickType != 0)
						{
							list.Add(gatherPointListByMapID[i].CloneAsGimmickData());
						}
					}
				}
			}
		}
		List<FieldMapTable.FieldGimmickPointTableData> fieldGimmickPointListByMapID = Singleton<FieldMapTable>.I.GetFieldGimmickPointListByMapID(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
		fieldGimmickPointListByMapID = ((fieldGimmickPointListByMapID == null) ? new List<FieldMapTable.FieldGimmickPointTableData>() : new List<FieldMapTable.FieldGimmickPointTableData>(fieldGimmickPointListByMapID));
		fieldGimmickPointListByMapID.AddRange(list);
		if (fieldGimmickPointListByMapID.Count > 0)
		{
			int num = 0;
			for (int j = 0; j < fieldGimmickPointListByMapID.Count; j++)
			{
				FieldMapTable.FieldGimmickPointTableData fieldGimmickPointTableData = fieldGimmickPointListByMapID[j];
				int num2 = 1 << (int)(fieldGimmickPointTableData.gimmickType - 1);
				if ((num & num2) == 0)
				{
					string[] effectNameList = null;
					int[] array = null;
					int modelIndex = 0;
					switch (fieldGimmickPointTableData.gimmickType)
					{
					case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.HEALING:
						effectNameList = new string[2]
						{
							"ef_btl_heal_spot_01_01",
							"ef_btl_heal_spot_01_02"
						};
						array = new int[1]
						{
							30000038
						};
						break;
					case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON:
					case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON_HEAVY:
						effectNameList = new string[9]
						{
							ResourceName.GetFieldGimmickCannonTargetEffect(),
							"ef_btl_target_cannon_02",
							"ef_btl_target_cannon_03",
							"ef_btl_magibullet_landing_01",
							"ef_btl_magibullet_landing_02",
							"ef_btl_magibullet_landing_03",
							"ef_btl_magibullet_shot_01",
							"ef_btl_goldbird_aura_01_01",
							"ef_btl_cannon_tap"
						};
						array = new int[1]
						{
							10000079
						};
						break;
					case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON_RAPID:
						effectNameList = new string[2]
						{
							ResourceName.GetFieldGimmickCannonTargetEffect(),
							"ef_btl_cannon_tap"
						};
						array = new int[2]
						{
							10000079,
							MonoBehaviourSingleton<InGameSettingsManager>.I.cannonParam.seIdForRapid
						};
						break;
					case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON_SPECIAL:
						effectNameList = new string[1]
						{
							FieldGimmickCannonSpecial.NAME_EFFECT_CHARGE
						};
						array = new int[4]
						{
							MonoBehaviourSingleton<InGameSettingsManager>.I.cannonParam.seIdForSpecial,
							MonoBehaviourSingleton<InGameSettingsManager>.I.cannonParam.seIdForSpecialCharge,
							MonoBehaviourSingleton<InGameSettingsManager>.I.cannonParam.seIdForSpecialChargeMax,
							MonoBehaviourSingleton<InGameSettingsManager>.I.cannonParam.seIdForSpecialOnBoard
						};
						break;
					case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON_FIELD:
						effectNameList = new string[2]
						{
							ResourceName.GetFieldGimmickCannonTargetEffect(),
							"ef_btl_cannon_tap"
						};
						array = new int[2]
						{
							10000079,
							MonoBehaviourSingleton<InGameSettingsManager>.I.cannonParam.seIdForField
						};
						modelIndex = FieldGimmickCannonField.GetModelIndex(fieldGimmickPointTableData.value2);
						num2 = 0;
						break;
					case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.BOMBROCK:
						effectNameList = new string[2]
						{
							"ef_btl_enemy_explosion_01_02",
							"ef_btl_bg_bombrock_01"
						};
						array = new int[1]
						{
							30000102
						};
						break;
					case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.GEYSER:
						effectNameList = new string[1]
						{
							"ef_btl_bg_geyser_01"
						};
						array = new int[0];
						break;
					case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.SONAR:
						effectNameList = new string[3]
						{
							"ef_btl_sonar_01",
							"ef_btl_sonar_02",
							ResourceName.GetSonarTargetEffect()
						};
						array = new int[1]
						{
							40000107
						};
						break;
					case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.WAVE_TARGET:
					case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.WAVE_TARGET2:
					case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.WAVE_TARGET3:
						effectNameList = new string[0];
						array = new int[0];
						break;
					case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.READ_STORY:
					case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.BINGO:
						effectNameList = new string[1]
						{
							ResourceName.GetReadStoryTargetEffectName()
						};
						array = new int[0];
						break;
					case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.FISHING:
					{
						effectNameList = new string[4]
						{
							"ef_btl_target_fishing_01",
							"ef_btl_fishing_01",
							"ef_btl_fishing_02",
							"ef_btl_fishing_03"
						};
						InGameSettingsManager.FishingParam fishingParam = MonoBehaviourSingleton<InGameSettingsManager>.I.fishingParam;
						array = new int[2 + fishingParam.hitSeIds.Length];
						array[0] = fishingParam.omenSeId;
						array[1] = fishingParam.hookSeId;
						for (int k = 0; k < fishingParam.hitSeIds.Length; k++)
						{
							array[2 + k] = fishingParam.hitSeIds[k];
						}
						if (gatherPointToolTable.Get("Fishingrod") == null)
						{
							gatherPointToolTable.Add("Fishingrod", load_queue.Load(RESOURCE_CATEGORY.INGAME_GATHER_POINT, "Fishingrod", false));
						}
						break;
					}
					case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CHAT:
						effectNameList = new string[1]
						{
							"ef_btl_target_readstory_01"
						};
						array = new int[0];
						break;
					case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.GENERATOR:
						effectNameList = GimmickGeneratorObject.GetEffectNames(fieldGimmickPointTableData.value2);
						array = new int[0];
						break;
					case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANDYWOOD:
						effectNameList = new string[0];
						array = new int[0];
						break;
					}
					if (fieldGimmickPointTableData.gimmickType != 0)
					{
						FieldGimmickObject.CacheResources(load_queue, fieldGimmickPointTableData.gimmickType, effectNameList, array, modelIndex);
						num |= num2;
					}
				}
			}
		}
		if (QuestManager.IsValidInGameWaveMatch(false))
		{
			wmDropResource = new WaveMatchDropResource();
			wmDropResource.Cache(load_queue);
		}
		CacheAudio(load_queue);
	}

	public void BattleStart()
	{
		isBattleStart = true;
		bool flag = false;
		if (FieldManager.IsValidInGameNoBoss())
		{
			if (QuestManager.IsValidInGame() && (MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestSeriesNum() > 1 || MonoBehaviourSingleton<QuestManager>.I.IsExplore()))
			{
				MonoBehaviourSingleton<FieldManager>.I.InitPortalPointForExplore(MonoBehaviourSingleton<QuestManager>.I.GetExploreStatus());
			}
			portalObjectList = new List<PortalObject>();
			List<FieldMapPortalInfo> currentFieldPortalInfoList = MonoBehaviourSingleton<FieldManager>.I.currentFieldPortalInfoList;
			if (currentFieldPortalInfoList != null)
			{
				int i = 0;
				for (int count = currentFieldPortalInfoList.Count; i < count; i++)
				{
					FieldMapPortalInfo fieldMapPortalInfo = currentFieldPortalInfoList[i];
					if (fieldMapPortalInfo.IsValid() && FieldManager.IsShowPortal(fieldMapPortalInfo.portalData))
					{
						portalObjectList.Add(PortalObject.Create(fieldMapPortalInfo, base._transform));
					}
				}
			}
			gatherPointList = new List<GatherPointObject>();
			List<FieldMapTable.GatherPointTableData> gatherPointListByMapID = Singleton<FieldMapTable>.I.GetGatherPointListByMapID(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
			if (gatherPointListByMapID != null)
			{
				int j = 0;
				for (int count2 = gatherPointListByMapID.Count; j < count2; j++)
				{
					FieldMapTable.GatherPointTableData gatherPointTableData = gatherPointListByMapID[j];
					GatherPointObject gatherPointObject = null;
					switch (FieldMapTable.GatherPointTableData.GetGatherType(gatherPointTableData))
					{
					case FieldMapTable.GatherPointTableData.GatherType.Growth:
						gatherPointObject = GatherPointObject.Create<GrowthGatherPointObject>(gatherPointTableData, base._transform);
						break;
					default:
						gatherPointObject = GatherPointObject.Create<BasicGatherPointObject>(gatherPointTableData, base._transform);
						break;
					}
					gatherPointList.Add(gatherPointObject);
					if (gatherPointTableData.gimmickType != 0)
					{
						FieldMapTable.FieldGimmickPointTableData gimmickPointTableData = gatherPointTableData.CloneAsGimmickData();
						IFieldGimmickObject fieldGimmickObject = CreateGimmick(gimmickPointTableData);
						if (fieldGimmickObject != null)
						{
							gatherPointObject.gimmick = (fieldGimmickObject as FieldGimmickObject);
							gatherPointObject.UpdateView();
						}
					}
				}
			}
			if (FieldManager.IsValidInGameNoQuest())
			{
				DeliveryAddCheck();
			}
			GameSceneGlobalSettings.RequestSoundSettingIngameField();
		}
		List<FieldMapTable.FieldGimmickPointTableData> fieldGimmickPointListByMapID = Singleton<FieldMapTable>.I.GetFieldGimmickPointListByMapID(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
		if (fieldGimmickPointListByMapID != null)
		{
			for (int k = 0; k < fieldGimmickPointListByMapID.Count; k++)
			{
				CreateGimmick(fieldGimmickPointListByMapID[k]);
			}
		}
		if (QuestManager.IsValidInGame())
		{
			if (MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.I.coopStage.isQuestClose)
			{
				if (MonoBehaviourSingleton<CoopManager>.I.coopStage.isQuestSucceed)
				{
					BattleComplete(false);
					flag = true;
				}
				else
				{
					BattleRetire();
				}
			}
			else if (QuestManager.IsValidInGameExplore())
			{
				if (!MonoBehaviourSingleton<InGameManager>.I.isAlreadyBattleStarted)
				{
					PlayBattleStartEffect(false);
					MonoBehaviourSingleton<InGameManager>.I.isAlreadyBattleStarted = true;
				}
			}
			else
			{
				PlayBattleStartEffect(MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestSeriesNum() > 1);
			}
			if (QuestManager.IsValidInGameExplore())
			{
				if (missionCheck.Count == 0)
				{
					InitMissionCheck();
					MonoBehaviourSingleton<QuestManager>.I.GetExploreStatus().SetMissions(missionCheck);
				}
			}
			else
			{
				InitMissionCheck();
			}
		}
		int l = 0;
		for (int count3 = MonoBehaviourSingleton<StageObjectManager>.I.playerList.Count; l < count3; l++)
		{
			Player player = MonoBehaviourSingleton<StageObjectManager>.I.playerList[l] as Player;
			if (!player.isDead && !player.isLoading && !player.IsPuppet())
			{
				player.ActBattleStart(false);
			}
		}
		if (MonoBehaviourSingleton<UIInGamePopupDialog>.IsValid())
		{
			MonoBehaviourSingleton<UIInGamePopupDialog>.I.SetEnableDialog(true);
		}
		if (MonoBehaviourSingleton<InGameManager>.I.isGateQuestClear)
		{
			MonoBehaviourSingleton<InGameManager>.I.isGateQuestClear = false;
		}
		if (QuestManager.IsValidInGameExplore() && MonoBehaviourSingleton<QuestManager>.I.IsExploreBossMap())
		{
			if (MonoBehaviourSingleton<QuestManager>.I.IsExploreBossDead() && !flag && !MonoBehaviourSingleton<CoopManager>.I.coopStage.isQuestClose)
			{
				BattleComplete(false);
			}
			else if (!MonoBehaviourSingleton<QuestManager>.I.IsEncountered())
			{
				int exploreBossBatlleMapId = MonoBehaviourSingleton<QuestManager>.I.GetExploreBossBatlleMapId();
				int bossMapIndex = MonoBehaviourSingleton<QuestManager>.I.ExploreMapIdToIndex((uint)exploreBossBatlleMapId);
				bool inOtherMap = false;
				MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.ForEach(delegate(CoopClient x)
				{
					inOtherMap |= (x.exploreMapIndex != bossMapIndex);
				});
				if (inOtherMap)
				{
					int exploreBossAppearMapId = MonoBehaviourSingleton<QuestManager>.I.GetExploreBossAppearMapId();
					MonoBehaviourSingleton<CoopManager>.I.coopRoom.packetSender.SendNotifyEncounterBoss(exploreBossAppearMapId, (int)MonoBehaviourSingleton<QuestManager>.I.GetLastPortalId());
				}
			}
			MonoBehaviourSingleton<QuestManager>.I.ResetMemberEncountered();
		}
		if (MonoBehaviourSingleton<FieldManager>.IsValid() && MonoBehaviourSingleton<FieldManager>.I.currentFieldBuffId != 0)
		{
			isEndFieldBuffAnnounce = false;
		}
		else
		{
			isEndFieldBuffAnnounce = true;
		}
	}

	private IFieldGimmickObject CreateGimmick(FieldMapTable.FieldGimmickPointTableData gimmickPointTableData)
	{
		IFieldGimmickObject fieldGimmickObject = null;
		switch (gimmickPointTableData.gimmickType)
		{
		case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.HEALING:
			fieldGimmickObject = FieldGimmickObject.Create<FieldHealingPointObject>(gimmickPointTableData, 19, base._transform);
			break;
		case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON:
		{
			Transform parent4 = (!MonoBehaviourSingleton<StageObjectManager>.IsValid()) ? base._transform : MonoBehaviourSingleton<StageObjectManager>.I._transform;
			fieldGimmickObject = FieldGimmickObject.Create<FieldGimmickCannonObject>(gimmickPointTableData, 19, parent4);
			AddFieldGimmickObj(eFieldGimmick.Cannon, fieldGimmickObject);
			break;
		}
		case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON_RAPID:
		{
			Transform parent3 = (!MonoBehaviourSingleton<StageObjectManager>.IsValid()) ? base._transform : MonoBehaviourSingleton<StageObjectManager>.I._transform;
			fieldGimmickObject = FieldGimmickObject.Create<FieldGimmickCannonRapid>(gimmickPointTableData, 19, parent3);
			AddFieldGimmickObj(eFieldGimmick.Cannon, fieldGimmickObject);
			break;
		}
		case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON_HEAVY:
		{
			Transform parent2 = (!MonoBehaviourSingleton<StageObjectManager>.IsValid()) ? base._transform : MonoBehaviourSingleton<StageObjectManager>.I._transform;
			fieldGimmickObject = FieldGimmickObject.Create<FieldGimmickCannonHeavy>(gimmickPointTableData, 19, parent2);
			AddFieldGimmickObj(eFieldGimmick.Cannon, fieldGimmickObject);
			break;
		}
		case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON_SPECIAL:
		{
			Transform parent5 = (!MonoBehaviourSingleton<StageObjectManager>.IsValid()) ? base._transform : MonoBehaviourSingleton<StageObjectManager>.I._transform;
			fieldGimmickObject = FieldGimmickObject.Create<FieldGimmickCannonSpecial>(gimmickPointTableData, 19, parent5);
			AddFieldGimmickObj(eFieldGimmick.Cannon, fieldGimmickObject);
			break;
		}
		case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON_FIELD:
		{
			Transform parent6 = (!MonoBehaviourSingleton<StageObjectManager>.IsValid()) ? base._transform : MonoBehaviourSingleton<StageObjectManager>.I._transform;
			fieldGimmickObject = FieldGimmickObject.Create<FieldGimmickCannonField>(gimmickPointTableData, 19, parent6);
			AddFieldGimmickObj(eFieldGimmick.Cannon, fieldGimmickObject);
			break;
		}
		case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.BOMBROCK:
			fieldGimmickObject = FieldGimmickObject.Create<FieldGimmickBombRockObject>(gimmickPointTableData, 18, base._transform);
			break;
		case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.GEYSER:
			fieldGimmickObject = FieldGimmickObject.Create<FieldGimmickGeyserObject>(gimmickPointTableData, 19, base._transform);
			break;
		case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.SONAR:
			fieldGimmickObject = FieldGimmickObject.Create<FieldSonarObject>(gimmickPointTableData, 19, base._transform);
			AddFieldGimmickObj(eFieldGimmick.Sonar, fieldGimmickObject);
			break;
		case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.WAVE_TARGET:
		case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.WAVE_TARGET2:
		case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.WAVE_TARGET3:
			fieldGimmickObject = FieldGimmickObject.Create<FieldWaveTargetObject>(gimmickPointTableData, 18, base._transform);
			break;
		case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.READ_STORY:
			if (FieldReadStoryObject.IsValid(gimmickPointTableData.value2))
			{
				fieldGimmickObject = FieldGimmickObject.Create<FieldReadStoryObject>(gimmickPointTableData, 19, base._transform);
				AddFieldGimmickObj(eFieldGimmick.ReadStory, fieldGimmickObject);
			}
			break;
		case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.FISHING:
			fieldGimmickObject = FieldGimmickObject.Create<FieldFishingGimmickObject>(gimmickPointTableData, 19, base._transform);
			AddFieldGimmickObj(eFieldGimmick.GatherGimmick, fieldGimmickObject);
			break;
		case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.BINGO:
			fieldGimmickObject = FieldGimmickObject.Create<FieldBingoObject>(gimmickPointTableData, 19, base._transform);
			AddFieldGimmickObj(eFieldGimmick.Bingo, fieldGimmickObject);
			break;
		case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CHAT:
			if (FieldChatGimmickObject.IsValid(gimmickPointTableData.value2))
			{
				fieldGimmickObject = FieldGimmickObject.Create<FieldChatGimmickObject>(gimmickPointTableData, 19, base._transform);
				AddFieldGimmickObj(eFieldGimmick.Chat, fieldGimmickObject);
			}
			break;
		case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.GENERATOR:
		{
			Transform parent = (!MonoBehaviourSingleton<StageObjectManager>.IsValid()) ? base._transform : MonoBehaviourSingleton<StageObjectManager>.I._transform;
			fieldGimmickObject = FieldGimmickObject.Create<GimmickGeneratorObject>(gimmickPointTableData, 19, parent);
			break;
		}
		case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANDYWOOD:
			fieldGimmickObject = FieldGimmickObject.Create<FieldGimmickCandyWoodObject>(gimmickPointTableData, 19, base._transform);
			break;
		}
		fieldGimmickObject?.Initialize(gimmickPointTableData);
		return fieldGimmickObject;
	}

	public bool OnRecvWaveMatchDrop(Coop_Model_WaveMatchDrop model)
	{
		if (wmDropResource != null)
		{
			wmDropResource.Create(model);
		}
		return true;
	}

	public bool OnRecvWaveMatchDropCreate(Coop_Model_WaveMatchDropCreate model)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		if (wmDropResource != null)
		{
			wmDropResource.OnCreate(model.managedId, model.dataId, model.basePos, model.offset, model.sec, false);
		}
		return true;
	}

	public bool OnRecvWaveMatchDropPicked(Coop_Model_WaveMatchDropPicked model)
	{
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			MonoBehaviourSingleton<StageObjectManager>.I.PickedWaveMatchDropObject(model, true);
		}
		return true;
	}

	public void CheckGatherPointList()
	{
		if (gatherPointList != null)
		{
			int i = 0;
			for (int count = gatherPointList.Count; i < count; i++)
			{
				gatherPointList[i].CheckGather();
			}
		}
	}

	private void InitMissionCheck()
	{
		QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID);
		if (questData != null)
		{
			QuestTable.MissionTableData[] missionData = Singleton<QuestTable>.I.GetMissionData(questData.missionID);
			int i = 0;
			for (int num = missionData.Length; i < num; i++)
			{
				if (missionData[i] != null && missionData[i].missionID != 0)
				{
					MissionCheckBase missionCheckBase = MissionCheckBase.CreateMissionCheck(missionData[i]);
					if (missionCheckBase != null)
					{
						missionCheck.Add(missionCheckBase);
					}
				}
			}
		}
	}

	public void PlayBattleStartEffect(bool is_phase_number)
	{
		CreateUIEffect(MonoBehaviourSingleton<InGameLinkResourcesQuest>.I.questStart);
		PlayAudio(AUDIO.QUEST_START);
	}

	public unsafe bool BattleComplete(bool forceComplete = false)
	{
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Expected O, but got Unknown
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		this.forceComplete = forceComplete;
		if (!forceComplete && (!isBattleStart || progressEndType != 0))
		{
			return false;
		}
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			MonoBehaviourSingleton<CoopManager>.I.coopStage.CheckEntryClose(true);
		}
		isGameProgressStop = true;
		StopTimer();
		if (QuestManager.IsValidInGame() && MonoBehaviourSingleton<QuestManager>.I.GetVorgonQuestType() == QuestManager.VorgonQuetType.BATTLE_WITH_WYBURN)
		{
			isSendCompleteError = false;
			SendComplete();
			if (MonoBehaviourSingleton<GameSceneManager>.IsValid())
			{
				InGameMain gameMain = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection() as InGameMain;
				if (gameMain != null)
				{
					_003CBattleComplete_003Ec__AnonStorey5FD _003CBattleComplete_003Ec__AnonStorey5FD;
					gameMain.cutScenePlayer.Play(new Action((object)_003CBattleComplete_003Ec__AnonStorey5FD, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
				}
				return true;
			}
		}
		bool flag = MonoBehaviourSingleton<InGameManager>.I.IsRush() && !MonoBehaviourSingleton<InGameManager>.I.IsLastRash();
		bool flag2 = MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo() && !MonoBehaviourSingleton<InGameManager>.I.IsArenaFinalWave();
		if (flag)
		{
			SendRushProgress();
			this.StartCoroutine(OnProgressEnd(PROGRESS_END_TYPE.RUSH_INTERVAL));
		}
		else if (flag2)
		{
			SendArenaProgress();
			this.StartCoroutine(OnProgressEnd(PROGRESS_END_TYPE.ARENA_INTERVAL));
		}
		else
		{
			isSendCompleteError = false;
			if (MonoBehaviourSingleton<InGameManager>.I.IsRush())
			{
				SendRushProgress();
			}
			else if (MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
			{
				SendArenaProgress();
			}
			else
			{
				SendComplete();
			}
			if (MonoBehaviourSingleton<CoopManager>.IsValid())
			{
				MonoBehaviourSingleton<CoopManager>.I.coopStage.SetQuestClose(true);
			}
			this.StartCoroutine(OnProgressEnd(PROGRESS_END_TYPE.QUEST_VICTORY));
		}
		return true;
	}

	public bool PortalNext(uint next_portal_id)
	{
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		if (next_portal_id == 0)
		{
			return false;
		}
		FieldMapTable.PortalTableData portal_data = Singleton<FieldMapTable>.I.GetPortalData(next_portal_id);
		if (portal_data == null)
		{
			return false;
		}
		if (MonoBehaviourSingleton<QuestManager>.I.IsExplore())
		{
			MonoBehaviourSingleton<QuestManager>.I.UpdateLastPortalData(portal_data);
			toFieldPortalID = next_portal_id;
			isGameProgressStop = true;
			MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.ForEach(delegate(CoopClient x)
			{
				if (x.GetPlayer() != null)
				{
					MonoBehaviourSingleton<QuestManager>.I.UpdateExplorePlayerStatus(x);
				}
			});
			this.StartCoroutine(OnProgressEnd(PROGRESS_END_TYPE.EXPLORE_MOVE_INTERVAL));
			return true;
		}
		if (MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestSeriesNum() > 1)
		{
			isGameProgressStop = true;
			StopTimer();
			this.StartCoroutine(OnProgressEnd(PROGRESS_END_TYPE.QUEST_SERIES_INTERVAL));
			return true;
		}
		bool flag = false;
		if (portal_data.dstMapID != 0 && portal_data.dstQuestID != 0)
		{
			int num = 0;
			ClearStatusQuest clearStatusQuest = MonoBehaviourSingleton<QuestManager>.I.clearStatusQuest.Find((ClearStatusQuest data) => data.questId == portal_data.dstQuestID);
			if (clearStatusQuest != null)
			{
				num = clearStatusQuest.questStatus;
			}
			if (num != 3 && num != 4)
			{
				flag = true;
			}
		}
		if (portal_data.dstMapID != 0 && !flag)
		{
			FieldMapInterval(next_portal_id);
		}
		else if (portal_data.dstQuestID != 0)
		{
			FieldToQuestInterval(portal_data.dstQuestID, next_portal_id, flag);
		}
		else
		{
			FieldToHome();
		}
		return true;
	}

	public bool FieldMapInterval(uint to_portal_id)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		if (!isBattleStart || progressEndType != 0)
		{
			return false;
		}
		toFieldPortalID = to_portal_id;
		isGameProgressStop = true;
		if (MonoBehaviourSingleton<FieldManager>.I.isTutorialField)
		{
			this.StartCoroutine(OnProgressEnd(PROGRESS_END_TYPE.FIELD_MAP_INTERVAL_TUTORIAL));
		}
		else
		{
			this.StartCoroutine(OnProgressEnd(PROGRESS_END_TYPE.FIELD_MAP_INTERVAL));
		}
		return true;
	}

	public bool FieldToQuestInterval(uint to_quest_id, uint from_portal_id, bool is_gate)
	{
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		if (!isBattleStart || progressEndType != 0)
		{
			return false;
		}
		toQuestID = to_quest_id;
		toQuestPortalID = from_portal_id;
		toQuestGate = is_gate;
		isGameProgressStop = true;
		PROGRESS_END_TYPE type = PROGRESS_END_TYPE.FIELD_TO_QUEST_INTERVAL;
		if (Singleton<QuestTable>.IsValid())
		{
			QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(to_quest_id);
			if (questData != null && questData.storyId != 0)
			{
				type = PROGRESS_END_TYPE.FIELD_TO_STORY;
			}
		}
		this.StartCoroutine(OnProgressEnd(type));
		return true;
	}

	public bool FieldReadStory(int storyId, bool isSend)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		if (!isBattleStart || progressEndType != 0)
		{
			return false;
		}
		fieldReadStoryId = storyId;
		isFieldReadStorySend = isSend;
		this.StartCoroutine(OnProgressEnd(PROGRESS_END_TYPE.FIELD_READ_STORY));
		return true;
	}

	public bool ExploreFieldToQuestInterval()
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		if (!isBattleStart || progressEndType != 0)
		{
			return false;
		}
		isGameProgressStop = true;
		PROGRESS_END_TYPE type = PROGRESS_END_TYPE.EXPLORE_HAPPEN_INTERVAL;
		this.StartCoroutine(OnProgressEnd(type));
		return true;
	}

	public bool FieldToHome()
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		if (!isBattleStart || progressEndType != 0)
		{
			return false;
		}
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			MonoBehaviourSingleton<CoopManager>.I.coopMyClient.BattleRetire();
		}
		if (QuestManager.IsValidInGame())
		{
			return false;
		}
		this.StartCoroutine(OnProgressEnd(PROGRESS_END_TYPE.FIELD_TO_HOME));
		return true;
	}

	public bool FieldToHomeTimeout()
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		if (!isBattleStart || progressEndType != 0)
		{
			return false;
		}
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			MonoBehaviourSingleton<CoopManager>.I.coopMyClient.BattleRetire();
		}
		if (QuestManager.IsValidInGame())
		{
			return false;
		}
		this.StartCoroutine(OnProgressEnd(PROGRESS_END_TYPE.FIELD_TO_HOME_TIMEOUT));
		return true;
	}

	public bool InviteInQuest()
	{
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		if (!isBattleStart || progressEndType != 0)
		{
			return false;
		}
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			MonoBehaviourSingleton<CoopManager>.I.coopMyClient.BattleRetire();
		}
		disableSendProgressStop = true;
		isGameProgressStop = true;
		StopTimer();
		if (MonoBehaviourSingleton<InGameManager>.IsValid())
		{
			MonoBehaviourSingleton<InGameManager>.I.ClearRush();
			MonoBehaviourSingleton<InGameManager>.I.ClearArenaInfo();
		}
		if (QuestManager.IsValidInGame())
		{
			if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
			{
				SendArenaRetire();
			}
			else
			{
				SendRetire();
			}
			MonoBehaviourSingleton<CoopManager>.I.coopStage.SetRetireQuestClose();
			this.StartCoroutine(OnProgressEnd(PROGRESS_END_TYPE.QUEST_INVITEQUIT));
		}
		else
		{
			this.StartCoroutine(OnProgressEnd(PROGRESS_END_TYPE.FIELD_TO_HOME));
		}
		return true;
	}

	public bool QuestToField(uint fieldPortalID)
	{
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		if (!isBattleStart || progressEndType != 0)
		{
			return false;
		}
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			MonoBehaviourSingleton<CoopManager>.I.coopMyClient.BattleRetire();
		}
		disableSendProgressStop = true;
		isGameProgressStop = true;
		StopTimer();
		if (MonoBehaviourSingleton<InGameManager>.IsValid())
		{
			MonoBehaviourSingleton<InGameManager>.I.ClearRush();
			MonoBehaviourSingleton<InGameManager>.I.ClearArenaInfo();
		}
		toFieldPortalID = fieldPortalID;
		if (QuestManager.IsValidInGame())
		{
			if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
			{
				SendArenaRetire();
			}
			else
			{
				SendRetire();
			}
			MonoBehaviourSingleton<CoopManager>.I.coopStage.SetRetireQuestClose();
			this.StartCoroutine(OnProgressEnd(PROGRESS_END_TYPE.QUEST_TO_FIELD));
		}
		else
		{
			this.StartCoroutine(OnProgressEnd(PROGRESS_END_TYPE.FIELD_TO_HOME));
		}
		return true;
	}

	public bool QuestRepeat()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		if (progressEndType != PROGRESS_END_TYPE.QUEST_VICTORY)
		{
			return false;
		}
		isGameProgressStop = true;
		PROGRESS_END_TYPE type = PROGRESS_END_TYPE.QUEST_TO_QUEST_REPEAT;
		this.StartCoroutine(OnProgressEnd(type));
		return true;
	}

	public bool BattleRetire()
	{
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		if (!isBattleStart || progressEndType != 0)
		{
			return false;
		}
		if (MonoBehaviourSingleton<ShopManager>.IsValid())
		{
			MonoBehaviourSingleton<ShopManager>.I.trackPlayerDie = true;
		}
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			MonoBehaviourSingleton<CoopManager>.I.coopMyClient.BattleRetire();
		}
		disableSendProgressStop = true;
		isGameProgressStop = true;
		StopTimer();
		if (QuestManager.IsValidInGame())
		{
			if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
			{
				SendArenaRetire();
			}
			else
			{
				SendRetire();
			}
			MonoBehaviourSingleton<CoopManager>.I.coopStage.SetRetireQuestClose();
			this.StartCoroutine(OnProgressEnd(PROGRESS_END_TYPE.QUEST_RETIRE));
		}
		else
		{
			this.StartCoroutine(OnProgressEnd(PROGRESS_END_TYPE.FIELD_RETIRE));
		}
		return true;
	}

	public bool BattleRetry()
	{
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		if (!isBattleStart || progressEndType != 0)
		{
			return false;
		}
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			MonoBehaviourSingleton<CoopManager>.I.coopMyClient.BattleRetire();
		}
		disableSendProgressStop = true;
		isGameProgressStop = true;
		StopTimer();
		if (QuestManager.IsValidInGame())
		{
			if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
			{
				SendArenaRetire();
			}
			MonoBehaviourSingleton<CoopManager>.I.coopStage.SetRetireQuestClose();
			this.StartCoroutine(OnProgressEnd(PROGRESS_END_TYPE.QUEST_RETRY));
		}
		return true;
	}

	public unsafe bool BattleForceDefeatsEvent()
	{
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		if (!isBattleStart || progressEndType != 0)
		{
			return false;
		}
		List<List<int>> list = new List<List<int>>(5);
		for (int i = 0; i < 4; i++)
		{
			list.Add(new List<int>());
			list[i].Add(0);
		}
		MonoBehaviourSingleton<QuestManager>.I.SendQuestComplete(list, null, null, 0f, null, new Action<bool, Error>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			MonoBehaviourSingleton<CoopManager>.I.coopStage.SetQuestClose(true);
		}
		if (QuestManager.IsValidInGame())
		{
			this.StartCoroutine(OnProgressEnd(PROGRESS_END_TYPE.FORCE_DEFEAT));
		}
		return true;
	}

	public bool BattleTimeup()
	{
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		if (!isBattleStart || progressEndType != 0)
		{
			return false;
		}
		isGameProgressStop = true;
		StopTimer();
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			if (MonoBehaviourSingleton<CoopManager>.I.isStageHost)
			{
				MonoBehaviourSingleton<CoopManager>.I.coopStage.StageTimeup();
			}
			MonoBehaviourSingleton<CoopManager>.I.coopStage.SetQuestClose(false);
		}
		if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
		{
			SendArenaRetire();
		}
		else
		{
			SendRetire();
		}
		this.StartCoroutine(OnProgressEnd(PROGRESS_END_TYPE.QUEST_TIMEUP));
		return true;
	}

	public void OnHostEnmeyBossTimeUp()
	{
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			MonoBehaviourSingleton<CoopManager>.I.coopStage.EscapeHostEnmeyBoss();
		}
	}

	public bool FieldReentry()
	{
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		if (progressEndType != 0)
		{
			return false;
		}
		if (MonoBehaviourSingleton<FieldManager>.I.isTutorialField)
		{
			return false;
		}
		isGameProgressStop = true;
		if (!QuestManager.IsValidInGameExplore())
		{
			StopTimer();
			if (MonoBehaviourSingleton<InGameManager>.I.IsRush())
			{
				MonoBehaviourSingleton<InGameManager>.I.BackupRushStageInReentry();
			}
			if (MonoBehaviourSingleton<QuestManager>.I.IsCurrentQuestTypeSeries())
			{
				MonoBehaviourSingleton<InGameManager>.I.BackupSeriesStageInReentry();
			}
		}
		else
		{
			Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
			if (isBattleStart && Object.op_Implicit(boss))
			{
				MonoBehaviourSingleton<QuestManager>.I.UpdateExploreBossStatus(boss);
			}
		}
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			MonoBehaviourSingleton<CoopManager>.I.coopStage.SetIsEnterFieldEnemyBossBattle(MonoBehaviourSingleton<CoopManager>.I.coopStage.GetIsInFieldEnemyBossBattle());
		}
		this.StartCoroutine(OnProgressEnd(PROGRESS_END_TYPE.FIELD_REENTRY));
		return true;
	}

	private IEnumerator OnProgressEnd(PROGRESS_END_TYPE type)
	{
		progressEndType = type;
		if (type == PROGRESS_END_TYPE.FIELD_RETIRE)
		{
			progressEndType = PROGRESS_END_TYPE.FIELD_TO_HOME;
		}
		if (progressEndType == PROGRESS_END_TYPE.FIELD_REENTRY)
		{
			while (!isBattleStart)
			{
				yield return (object)null;
			}
		}
		if (progressEndType == PROGRESS_END_TYPE.QUEST_VICTORY && QuestManager.IsValidInGameExplore())
		{
			Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
			if (Object.op_Implicit(boss))
			{
				yield return (object)null;
				MonoBehaviourSingleton<QuestManager>.I.UpdateExploreBossStatus(boss);
				if (MonoBehaviourSingleton<CoopManager>.I.isStageHost)
				{
					MonoBehaviourSingleton<CoopManager>.I.coopRoom.packetSender.SendExploreBossDead(boss, MonoBehaviourSingleton<QuestManager>.I.GetExplorePlayerStatusList());
				}
			}
		}
		while (!MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection().isInitialized)
		{
			yield return (object)null;
		}
		if (MonoBehaviourSingleton<StageObjectManager>.I.self != null)
		{
			if (progressEndType == PROGRESS_END_TYPE.QUEST_VICTORY || progressEndType == PROGRESS_END_TYPE.QUEST_SERIES_INTERVAL || progressEndType == PROGRESS_END_TYPE.RUSH_INTERVAL || progressEndType == PROGRESS_END_TYPE.ARENA_INTERVAL)
			{
				MonoBehaviourSingleton<StageObjectManager>.I.self.hitOffFlag |= StageObject.HIT_OFF_FLAG.FORCE;
			}
			else
			{
				MonoBehaviourSingleton<StageObjectManager>.I.self.hitOffFlag |= StageObject.HIT_OFF_FLAG.FORCE;
				if (MonoBehaviourSingleton<StageObjectManager>.I.self.controller != null)
				{
					MonoBehaviourSingleton<StageObjectManager>.I.self.controller.SetEnableControll(false, ControllerBase.DISABLE_FLAG.BATTLE_END);
				}
			}
		}
		if (isHappenQuestDirection)
		{
			EndHappenQuestDirection();
			while (isHappenQuestDirection)
			{
				yield return (object)null;
			}
		}
		bool needRushIntervalEffects = progressEndType == PROGRESS_END_TYPE.RUSH_INTERVAL && !forceComplete;
		bool needArenaIntervalEffects = progressEndType == PROGRESS_END_TYPE.ARENA_INTERVAL && !forceComplete;
		if (progressEndType == PROGRESS_END_TYPE.QUEST_VICTORY || progressEndType == PROGRESS_END_TYPE.QUEST_SERIES_INTERVAL || needRushIntervalEffects || needArenaIntervalEffects)
		{
			InGameMain inGameMain = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentScreen() as InGameMain;
			if (inGameMain != null)
			{
				Transform menu_btn = inGameMain.GetCtrl(InGameMain.UI.BTN_QUEST_MENU);
				if (menu_btn != null)
				{
					menu_btn.get_gameObject().SetActive(false);
				}
			}
			if (MonoBehaviourSingleton<UIInGameMenu>.IsValid())
			{
				MonoBehaviourSingleton<UIInGameMenu>.I.get_gameObject().SetActive(false);
			}
			if (MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
			{
				MonoBehaviourSingleton<UIEnemyStatus>.I.get_gameObject().SetActive(false);
			}
			if (MonoBehaviourSingleton<UIInGameSelfAnnounceManager>.IsValid())
			{
				MonoBehaviourSingleton<UIInGameSelfAnnounceManager>.I.get_gameObject().SetActive(false);
			}
			if (MonoBehaviourSingleton<UIQuestRepeat>.IsValid())
			{
				MonoBehaviourSingleton<UIQuestRepeat>.I.OnVictory();
			}
		}
		else
		{
			ViewUI(false);
		}
		yield return (object)this.StartCoroutine(DoCloseDialog());
		if (MonoBehaviourSingleton<TargetMarkerManager>.IsValid())
		{
			MonoBehaviourSingleton<TargetMarkerManager>.I.showMarker = false;
		}
		if ((progressEndType == PROGRESS_END_TYPE.QUEST_VICTORY || progressEndType == PROGRESS_END_TYPE.QUEST_SERIES_INTERVAL || needRushIntervalEffects || needArenaIntervalEffects) && MonoBehaviourSingleton<StageObjectManager>.I.boss != null && !MonoBehaviourSingleton<StageObjectManager>.I.boss.isDead)
		{
			MonoBehaviourSingleton<StageObjectManager>.I.boss.ActDead(false, false);
		}
		if ((progressEndType == PROGRESS_END_TYPE.QUEST_RETIRE || progressEndType == PROGRESS_END_TYPE.QUEST_TIMEUP) && MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.mainChat != null && MonoBehaviourSingleton<QuestManager>.IsValid() && !MonoBehaviourSingleton<QuestManager>.I.IsTutorialOrderQuest(MonoBehaviourSingleton<QuestManager>.I.currentQuestID))
		{
			MonoBehaviourSingleton<UIManager>.I.mainChat.ShowOpenButton();
		}
		yield return (object)null;
		if (progressEndType == PROGRESS_END_TYPE.QUEST_VICTORY || progressEndType == PROGRESS_END_TYPE.QUEST_SERIES_INTERVAL || progressEndType == PROGRESS_END_TYPE.RUSH_INTERVAL || progressEndType == PROGRESS_END_TYPE.ARENA_INTERVAL)
		{
			int i = 0;
			for (int len = MonoBehaviourSingleton<StageObjectManager>.I.characterList.Count; i < len; i++)
			{
				Character character = MonoBehaviourSingleton<StageObjectManager>.I.characterList[i] as Character;
				character.hitOffFlag |= StageObject.HIT_OFF_FLAG.FORCE;
			}
		}
		else if (progressEndType == PROGRESS_END_TYPE.QUEST_RETIRE || progressEndType == PROGRESS_END_TYPE.FIELD_TO_HOME || progressEndType == PROGRESS_END_TYPE.QUEST_INVITEQUIT || progressEndType == PROGRESS_END_TYPE.FIELD_TO_HOME_TIMEOUT)
		{
			SetBattleEndCharacter(MonoBehaviourSingleton<StageObjectManager>.I.self);
		}
		else
		{
			SetBattleEndAllCharacters();
			SetBattleEndAllPlayers();
		}
		while (viewFx != null)
		{
			yield return (object)null;
		}
		switch (progressEndType)
		{
		case PROGRESS_END_TYPE.QUEST_VICTORY:
		case PROGRESS_END_TYPE.QUEST_RETIRE:
		case PROGRESS_END_TYPE.QUEST_RETRY:
		case PROGRESS_END_TYPE.RUSH_INTERVAL:
			if (progressEndType == PROGRESS_END_TYPE.QUEST_VICTORY)
			{
				yield return (object)new WaitForSeconds(1f);
				while (!isRecvQuestComplete)
				{
					yield return (object)null;
				}
			}
			if (progressEndType == PROGRESS_END_TYPE.RUSH_INTERVAL)
			{
				yield return (object)new WaitForSeconds(1f);
				while (!isRecvRushProgress)
				{
					yield return (object)null;
				}
			}
			if ((progressEndType == PROGRESS_END_TYPE.QUEST_VICTORY && !isSendCompleteError) || needRushIntervalEffects)
			{
				CreateUIEffect(MonoBehaviourSingleton<InGameLinkResourcesQuest>.I.questWin);
				PlayAudio(AUDIO.RESULT_WIN);
				SoundManager.RequestBGM(0, true);
				MonoBehaviourSingleton<SoundManager>.I.TransitionTo("PreVictory", 1f);
			}
			else if (progressEndType != PROGRESS_END_TYPE.RUSH_INTERVAL)
			{
				CreateUIEffect(MonoBehaviourSingleton<InGameLinkResourcesQuest>.I.questFaild);
				MonoBehaviourSingleton<SoundManager>.I.TransitionTo("QuestFailed", 0.1f);
				PlayAudio(AUDIO.RESULT_LOSE);
			}
			break;
		case PROGRESS_END_TYPE.ARENA_INTERVAL:
			yield return (object)new WaitForSeconds(1f);
			while (!isRecvArenaProgress)
			{
				yield return (object)null;
			}
			if (needArenaIntervalEffects)
			{
				CreateUIEffect(MonoBehaviourSingleton<InGameLinkResourcesQuest>.I.questWin);
				PlayAudio(AUDIO.RESULT_WIN);
				SoundManager.RequestBGM(0, true);
				MonoBehaviourSingleton<SoundManager>.I.TransitionTo("PreVictory", 1f);
			}
			break;
		case PROGRESS_END_TYPE.QUEST_TIMEUP:
			CreateUIEffect(MonoBehaviourSingleton<InGameLinkResourcesQuest>.I.questTimeUp);
			MonoBehaviourSingleton<SoundManager>.I.TransitionTo("QuestFailed", 0.1f);
			PlayAudio(AUDIO.RESULT_LOSE);
			while (viewFx != null)
			{
				yield return (object)null;
			}
			CreateUIEffect(MonoBehaviourSingleton<InGameLinkResourcesQuest>.I.questFaild);
			break;
		case PROGRESS_END_TYPE.FIELD_TO_HOME_TIMEOUT:
		{
			string msg2 = StringTable.Get(STRING_CATEGORY.IN_GAME, 130u);
			UIInGamePopupDialog.PushOpen(msg2, true, 1.8f);
			break;
		}
		case PROGRESS_END_TYPE.FIELD_REENTRY:
			if (MonoBehaviourSingleton<LoungeMatchingManager>.I.isKicked)
			{
				MonoBehaviourSingleton<LoungeMatchingManager>.I.CompleteKick();
			}
			else
			{
				string msg = StringTable.Get(STRING_CATEGORY.IN_GAME, 120u);
				UIInGamePopupDialog.PushOpen(msg, true, 1.8f);
			}
			break;
		case PROGRESS_END_TYPE.QUEST_TO_QUEST_REPEAT:
			if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.mainChat != null && MonoBehaviourSingleton<QuestManager>.IsValid() && !MonoBehaviourSingleton<QuestManager>.I.IsTutorialOrderQuest(MonoBehaviourSingleton<QuestManager>.I.currentQuestID))
			{
				MonoBehaviourSingleton<UIManager>.I.mainChat.HideOpenButton();
			}
			break;
		}
		while (viewFx != null)
		{
			yield return (object)null;
		}
		if (MonoBehaviourSingleton<InGameCameraManager>.IsValid())
		{
			while (!MonoBehaviourSingleton<InGameCameraManager>.I.IsEndMotionCamera())
			{
				yield return (object)null;
			}
		}
		if (MonoBehaviourSingleton<UIInGamePopupDialog>.IsValid())
		{
			while (MonoBehaviourSingleton<UIInGamePopupDialog>.I.IsShowingDialog())
			{
				yield return (object)null;
			}
			MonoBehaviourSingleton<UIInGamePopupDialog>.I.SetEnableDialog(false);
		}
		if (MonoBehaviourSingleton<UIManager>.IsValid())
		{
			MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(true);
			yield return (object)new WaitForEndOfFrame();
			GC.Collect();
			MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(false);
			yield return (object)new WaitForEndOfFrame();
		}
		if (progressEndType == PROGRESS_END_TYPE.QUEST_VICTORY && !isSendCompleteError)
		{
			if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
			{
				yield return (object)null;
				SetBattleEndAllCharacters();
				SetBattleEndAllPlayers();
				ViewUI(false);
				SoundManager.RequestBGM(14, true);
			}
			else
			{
				if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.mainChat != null && !MonoBehaviourSingleton<UIManager>.I.mainChat.IsOpeningWindow())
				{
					MonoBehaviourSingleton<UIManager>.I.mainChat.ShowInputOnly();
				}
				SoundManager.RequestBGM(14, true);
				enableVictoryIntervalTime = true;
				startVictoryIntervalTime = Time.get_time();
				while (victoryIntervalTime > 0f)
				{
					yield return (object)null;
				}
				enableVictoryIntervalTime = false;
				startVictoryIntervalTime = -1f;
				SetBattleEndAllCharacters();
				SetBattleEndAllPlayers();
				ViewUI(false);
				if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.mainChat != null && MonoBehaviourSingleton<QuestManager>.IsValid() && !MonoBehaviourSingleton<QuestManager>.I.IsTutorialOrderQuest(MonoBehaviourSingleton<QuestManager>.I.currentQuestID))
				{
					MonoBehaviourSingleton<UIManager>.I.mainChat.ShowOpenButton();
				}
			}
		}
		waitNetworkCoroutine = this.StartCoroutine(DoWaitNetwork());
		if (progressEndType == PROGRESS_END_TYPE.QUEST_VICTORY || progressEndType == PROGRESS_END_TYPE.QUEST_RETIRE || progressEndType == PROGRESS_END_TYPE.QUEST_INVITEQUIT || progressEndType == PROGRESS_END_TYPE.QUEST_TIMEUP || progressEndType == PROGRESS_END_TYPE.QUEST_TO_FIELD)
		{
			while (!isRecvQuestComplete)
			{
				yield return (object)null;
			}
		}
		if (progressEndType == PROGRESS_END_TYPE.QUEST_RETIRE || progressEndType == PROGRESS_END_TYPE.QUEST_RETRY || progressEndType == PROGRESS_END_TYPE.QUEST_INVITEQUIT || progressEndType == PROGRESS_END_TYPE.FIELD_TO_HOME || progressEndType == PROGRESS_END_TYPE.QUEST_TO_FIELD)
		{
			SetBattleEndAllCharacters();
			SetBattleEndAllPlayers();
		}
		if (progressEndType != PROGRESS_END_TYPE.FIELD_REENTRY)
		{
			float start_time = Time.get_time();
			while (MonoBehaviourSingleton<KtbWebSocket>.IsValid() && !MonoBehaviourSingleton<KtbWebSocket>.I.IsCompleteSendAll() && !(Time.get_time() - start_time > MonoBehaviourSingleton<InGameSettingsManager>.I.inGameProgress.checkCompleteSendTimeout))
			{
				yield return (object)0;
			}
			if (MonoBehaviourSingleton<KtbWebSocket>.IsValid())
			{
				MonoBehaviourSingleton<KtbWebSocket>.I.LoggingResendPackets("InGameProgressEnd: Timeout removing... ");
			}
		}
		MonoBehaviourSingleton<InGameManager>.I.isQuestResultFieldLeave = false;
		bool do_leave_coop = false;
		bool use_trasfer_info = false;
		bool online_stage_change = false;
		bool online_quest_series = false;
		bool transfer_other = false;
		bool keep_dead = false;
		bool chat_switch_to_party = false;
		switch (progressEndType)
		{
		case PROGRESS_END_TYPE.QUEST_VICTORY:
		case PROGRESS_END_TYPE.QUEST_RETIRE:
		case PROGRESS_END_TYPE.QUEST_RETRY:
		case PROGRESS_END_TYPE.QUEST_TIMEUP:
			if (MonoBehaviourSingleton<InGameManager>.I.isQuestHappen && CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected())
			{
				online_stage_change = true;
			}
			else if (PartyManager.IsValidInParty())
			{
				do_leave_coop = true;
				chat_switch_to_party = true;
			}
			else
			{
				MonoBehaviourSingleton<InGameManager>.I.isQuestResultFieldLeave = true;
			}
			if (MonoBehaviourSingleton<InGameManager>.I.IsQuestInField())
			{
				use_trasfer_info = true;
			}
			break;
		case PROGRESS_END_TYPE.QUEST_SERIES_INTERVAL:
			if (MonoBehaviourSingleton<CoopManager>.IsValid())
			{
				MonoBehaviourSingleton<CoopManager>.I.coopMyClient.SeriesProgress((int)MonoBehaviourSingleton<QuestManager>.I.currentQuestSeriesIndex);
				MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestSeriesIndex(MonoBehaviourSingleton<QuestManager>.I.currentQuestSeriesIndex + 1);
				while (MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.HasSeriesProgress())
				{
					yield return (object)null;
				}
			}
			if (CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected())
			{
				online_quest_series = true;
			}
			use_trasfer_info = true;
			transfer_other = true;
			online_stage_change = true;
			break;
		case PROGRESS_END_TYPE.EXPLORE_MOVE_INTERVAL:
		case PROGRESS_END_TYPE.EXPLORE_HAPPEN_INTERVAL:
			if (CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected())
			{
				online_quest_series = true;
			}
			use_trasfer_info = true;
			transfer_other = false;
			online_stage_change = true;
			break;
		case PROGRESS_END_TYPE.RUSH_INTERVAL:
			if (CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected())
			{
				online_quest_series = true;
			}
			use_trasfer_info = true;
			transfer_other = true;
			online_stage_change = true;
			break;
		case PROGRESS_END_TYPE.ARENA_INTERVAL:
			use_trasfer_info = true;
			break;
		case PROGRESS_END_TYPE.FIELD_MAP_INTERVAL:
			do_leave_coop = true;
			use_trasfer_info = true;
			break;
		case PROGRESS_END_TYPE.FIELD_TO_QUEST_INTERVAL:
		case PROGRESS_END_TYPE.FIELD_TO_STORY:
		case PROGRESS_END_TYPE.FIELD_READ_STORY:
			if (isQuestHappen && CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected())
			{
				online_stage_change = true;
			}
			else
			{
				do_leave_coop = true;
			}
			use_trasfer_info = true;
			break;
		case PROGRESS_END_TYPE.QUEST_INVITEQUIT:
		case PROGRESS_END_TYPE.FIELD_TO_HOME:
		case PROGRESS_END_TYPE.FIELD_TO_HOME_TIMEOUT:
			do_leave_coop = true;
			break;
		case PROGRESS_END_TYPE.FIELD_REENTRY:
			do_leave_coop = true;
			use_trasfer_info = true;
			keep_dead = true;
			break;
		case PROGRESS_END_TYPE.FORCE_DEFEAT:
			do_leave_coop = true;
			use_trasfer_info = true;
			break;
		case PROGRESS_END_TYPE.QUEST_TO_FIELD:
			do_leave_coop = true;
			use_trasfer_info = true;
			break;
		case PROGRESS_END_TYPE.QUEST_TO_QUEST_REPEAT:
			if (CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected())
			{
				online_quest_series = true;
			}
			use_trasfer_info = true;
			transfer_other = false;
			online_stage_change = true;
			isInitStartTime = false;
			break;
		}
		if (use_trasfer_info && MonoBehaviourSingleton<InGameManager>.IsValid())
		{
			float transferRemaindTime = remaindTime;
			float transferElapsedTime = GetElapsedTime();
			if (MonoBehaviourSingleton<InGameManager>.I.IsRush())
			{
				transferRemaindTime = rushRemainTime;
			}
			if (MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
			{
				transferRemaindTime = arenaRemainSec;
				transferElapsedTime = (float)arenaElapsedSec + GetElapsedTime();
			}
			if (QuestManager.IsValidInGameSeries() || QuestManager.IsValidInGameWaveMatch(false))
			{
				transferRemaindTime = limitTime;
			}
			bool isReentry = progressEndType == PROGRESS_END_TYPE.FIELD_REENTRY;
			bool toFieldMap = MonoBehaviourSingleton<InGameManager>.I.IsQuestInField() || MonoBehaviourSingleton<InGameManager>.I.IsQuestInPortal();
			if (progressEndType == PROGRESS_END_TYPE.QUEST_TO_QUEST_REPEAT)
			{
				MonoBehaviourSingleton<InGameManager>.I.SetIntervalTransferInfo(enableLimitTime, transferRemaindTime, transferElapsedTime, transfer_other, keep_dead, isReentry, true);
			}
			else
			{
				MonoBehaviourSingleton<InGameManager>.I.SetIntervalTransferInfo(enableLimitTime, transferRemaindTime, transferElapsedTime, transfer_other, keep_dead, isReentry, toFieldMap);
			}
			if (!IsStopTimer())
			{
				MonoBehaviourSingleton<InGameManager>.I.SetEnableIntervalTransferInfoRemaindTimeUpdate();
			}
		}
		CoopStageObjectUtility.SetCoopModeForAll(StageObject.COOP_MODE_TYPE.NONE, 0);
		while (MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
		{
			yield return (object)null;
		}
		if (FieldManager.IsValidInGame() && MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			bool wait2 = true;
			MonoBehaviourSingleton<CoopManager>.I.coopStage.fieldRewardPool.SendFieldDrop(delegate
			{
				((_003COnProgressEnd_003Ec__Iterator229)/*Error near IL_1386: stateMachine*/)._003Cwait_003E__22 = false;
			});
			while (wait2)
			{
				yield return (object)null;
			}
		}
		if (do_leave_coop)
		{
			bool toHome = progressEndType == PROGRESS_END_TYPE.FIELD_TO_HOME || progressEndType == PROGRESS_END_TYPE.FIELD_TO_HOME_TIMEOUT;
			bool fieldRetire = type == PROGRESS_END_TYPE.FIELD_RETIRE;
			bool wait = true;
			MonoBehaviourSingleton<CoopApp>.I.Leave(delegate
			{
				((_003COnProgressEnd_003Ec__Iterator229)/*Error near IL_140c: stateMachine*/)._003Cwait_003E__25 = false;
			}, toHome, fieldRetire);
			while (wait)
			{
				yield return (object)null;
			}
			if (chat_switch_to_party && PartyManager.IsValidInParty())
			{
				MonoBehaviourSingleton<ChatManager>.I.SwitchRoomChatConnectionToPartyConnection();
			}
		}
		else if (online_stage_change)
		{
			if (MonoBehaviourSingleton<CoopNetworkManager>.IsValid())
			{
				MonoBehaviourSingleton<CoopNetworkManager>.I.packetReceiver.EraseLostReceiverPackets();
			}
		}
		else if (!online_quest_series)
		{
			goto IL_14a5;
		}
		goto IL_14a5;
		IL_14a5:
		this.StopCoroutine(waitNetworkCoroutine);
		MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.MANUAL_NETWORK, false);
		if (progressEndType != PROGRESS_END_TYPE.FIELD_REENTRY)
		{
			MonoBehaviourSingleton<InGameManager>.I.disableHappenQuestIdList.Clear();
		}
		MonoBehaviourSingleton<InGameManager>.I.isStoryPortal = false;
		switch (progressEndType)
		{
		case PROGRESS_END_TYPE.QUEST_TO_QUEST_REPEAT:
			MonoBehaviourSingleton<InGameManager>.I.isTransitionQuestToQuest = true;
			ChangeSceneToInterval();
			break;
		case PROGRESS_END_TYPE.QUEST_TO_FIELD:
			MonoBehaviourSingleton<FieldManager>.I.SetCurrentFieldMapPortalID(toFieldPortalID);
			MonoBehaviourSingleton<InGameManager>.I.beforePortalID = toFieldPortalID;
			ChangeSceneToInterval();
			break;
		case PROGRESS_END_TYPE.QUEST_VICTORY:
		case PROGRESS_END_TYPE.QUEST_RETIRE:
		case PROGRESS_END_TYPE.QUEST_TIMEUP:
			if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
			{
				MonoBehaviourSingleton<InGameRecorder>.I.OnInGameEnd(progressEndType == PROGRESS_END_TYPE.QUEST_VICTORY && !isSendCompleteError);
			}
			OverwritePlayerRecorderForExplore(false);
			MonoBehaviourSingleton<SoundManager>.I.TransitionTo("Victory", 1f);
			ChangeSceneToResult();
			break;
		case PROGRESS_END_TYPE.QUEST_RETRY:
			ReloadScene();
			break;
		case PROGRESS_END_TYPE.QUEST_SERIES_INTERVAL:
			ChangeSceneToInterval();
			break;
		case PROGRESS_END_TYPE.EXPLORE_MOVE_INTERVAL:
		{
			FieldMapTable.PortalTableData portalData = Singleton<FieldMapTable>.I.GetPortalData(toFieldPortalID);
			int mapId2 = (int)portalData.dstMapID;
			if (MonoBehaviourSingleton<QuestManager>.I.IsBossAppearMap(mapId2))
			{
				mapId2 = MonoBehaviourSingleton<QuestManager>.I.GetExploreBossBatlleMapId();
				MonoBehaviourSingleton<FieldManager>.I.SetCurrentFieldMapID((uint)mapId2, 0f, 0f, 0f);
			}
			else
			{
				MonoBehaviourSingleton<FieldManager>.I.SetCurrentFieldMapPortalID(toFieldPortalID);
			}
			MonoBehaviourSingleton<InGameManager>.I.beforePortalID = toFieldPortalID;
			ChangeSceneToInterval();
			break;
		}
		case PROGRESS_END_TYPE.EXPLORE_HAPPEN_INTERVAL:
		{
			int mapId3 = MonoBehaviourSingleton<QuestManager>.I.GetExploreBossBatlleMapId();
			MonoBehaviourSingleton<FieldManager>.I.SetCurrentFieldMapID((uint)mapId3, 0f, 0f, 0f);
			MonoBehaviourSingleton<InGameManager>.I.beforePortalID = toFieldPortalID;
			ChangeSceneToInterval();
			break;
		}
		case PROGRESS_END_TYPE.RUSH_INTERVAL:
		{
			if (MonoBehaviourSingleton<CoopManager>.I.isStageHost)
			{
				MonoBehaviourSingleton<CoopManager>.I.coopStage.SendSyncPlayerRecord(0, false);
			}
			MonoBehaviourSingleton<InGameManager>.I.ProgressRush();
			uint nextQuestId = MonoBehaviourSingleton<InGameManager>.I.GetCurrentRushQuestId();
			MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(nextQuestId, true);
			ChangeSceneToInterval();
			break;
		}
		case PROGRESS_END_TYPE.FIELD_MAP_INTERVAL:
			if (MonoBehaviourSingleton<FieldManager>.IsValid())
			{
				MonoBehaviourSingleton<FieldManager>.I.SetCurrentFieldMapPortalID(toFieldPortalID);
				MonoBehaviourSingleton<InGameManager>.I.beforePortalID = toFieldPortalID;
				ChangeSceneToInterval();
			}
			break;
		case PROGRESS_END_TYPE.FIELD_TO_QUEST_INTERVAL:
			if (MonoBehaviourSingleton<FieldManager>.IsValid())
			{
				MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(toQuestID, true);
				MonoBehaviourSingleton<InGameManager>.I.isTransitionFieldToQuest = true;
				MonoBehaviourSingleton<InGameManager>.I.isQuestGate = isQuestGate;
				MonoBehaviourSingleton<InGameManager>.I.isQuestPortal = isQuestPortal;
				MonoBehaviourSingleton<InGameManager>.I.isQuestHappen = isQuestHappen;
				FieldManager.FieldTransitionInfo trans_info = new FieldManager.FieldTransitionInfo
				{
					portalID = MonoBehaviourSingleton<FieldManager>.I.currentPortalID,
					mapID = MonoBehaviourSingleton<FieldManager>.I.currentMapID
				};
				Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
				if (self != null)
				{
					FieldManager.FieldTransitionInfo fieldTransitionInfo7 = trans_info;
					Vector3 position9 = self._position;
					fieldTransitionInfo7.mapX = position9.x;
					FieldManager.FieldTransitionInfo fieldTransitionInfo8 = trans_info;
					Vector3 position10 = self._position;
					fieldTransitionInfo8.mapZ = position10.z;
					FieldManager.FieldTransitionInfo fieldTransitionInfo9 = trans_info;
					Quaternion rotation5 = self._rotation;
					Vector3 eulerAngles5 = rotation5.get_eulerAngles();
					fieldTransitionInfo9.mapDir = eulerAngles5.y;
				}
				MonoBehaviourSingleton<InGameManager>.I.backTransitionInfo = trans_info;
				MonoBehaviourSingleton<InGameManager>.I.beforePortalID = toQuestPortalID;
				ChangeSceneToInterval();
			}
			break;
		case PROGRESS_END_TYPE.FIELD_REENTRY:
			if (InGameManager.IsReentryMapId())
			{
				Self self3 = MonoBehaviourSingleton<StageObjectManager>.I.self;
				if (self3 != null)
				{
					FieldManager i2 = MonoBehaviourSingleton<FieldManager>.I;
					uint currentMapID = MonoBehaviourSingleton<FieldManager>.I.currentMapID;
					Vector3 position3 = self3._position;
					float x = position3.x;
					Vector3 position4 = self3._position;
					float z = position4.z;
					Quaternion rotation2 = self3._rotation;
					Vector3 eulerAngles2 = rotation2.get_eulerAngles();
					i2.SetCurrentFieldMapID(currentMapID, x, z, eulerAngles2.y);
				}
				MonoBehaviourSingleton<InGameManager>.I.isTransitionFieldReentry = true;
				ChangeSceneToInterval();
			}
			else if (MonoBehaviourSingleton<FieldManager>.IsValid())
			{
				Self self2 = MonoBehaviourSingleton<StageObjectManager>.I.self;
				if (self2 != null)
				{
					FieldManager i3 = MonoBehaviourSingleton<FieldManager>.I;
					uint currentPortalID = MonoBehaviourSingleton<FieldManager>.I.currentPortalID;
					Vector3 position5 = self2._position;
					float x2 = position5.x;
					Vector3 position6 = self2._position;
					float z2 = position6.z;
					Quaternion rotation3 = self2._rotation;
					Vector3 eulerAngles3 = rotation3.get_eulerAngles();
					i3.SetCurrentFieldMapPortalID(currentPortalID, x2, z2, eulerAngles3.y);
				}
				MonoBehaviourSingleton<InGameManager>.I.isTransitionFieldReentry = true;
				MonoBehaviourSingleton<InGameManager>.I.beforePortalID = MonoBehaviourSingleton<FieldManager>.I.currentPortalID;
				ChangeSceneToInterval();
			}
			break;
		case PROGRESS_END_TYPE.QUEST_INVITEQUIT:
		case PROGRESS_END_TYPE.FIELD_TO_HOME:
		case PROGRESS_END_TYPE.FORCE_DEFEAT:
		case PROGRESS_END_TYPE.FIELD_TO_HOME_TIMEOUT:
		{
			uint deliveryID = MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableStoryDelivery();
			if (MonoBehaviourSingleton<DeliveryManager>.I.HasClearEventID(deliveryID))
			{
				ChangeSceneToStory(deliveryID);
			}
			else
			{
				ChangeSceneToHome();
			}
			break;
		}
		case PROGRESS_END_TYPE.FIELD_TO_STORY:
			if (MonoBehaviourSingleton<FieldManager>.IsValid())
			{
				MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(toQuestID, true);
				MonoBehaviourSingleton<InGameManager>.I.isTransitionFieldToQuest = true;
				MonoBehaviourSingleton<InGameManager>.I.isQuestGate = isQuestGate;
				MonoBehaviourSingleton<InGameManager>.I.isQuestPortal = isQuestPortal;
				MonoBehaviourSingleton<InGameManager>.I.isQuestHappen = isQuestHappen;
				MonoBehaviourSingleton<InGameManager>.I.isStoryPortal = true;
				FieldManager.FieldTransitionInfo trans_info2 = new FieldManager.FieldTransitionInfo
				{
					portalID = MonoBehaviourSingleton<FieldManager>.I.currentPortalID,
					mapID = MonoBehaviourSingleton<FieldManager>.I.currentMapID
				};
				Self self4 = MonoBehaviourSingleton<StageObjectManager>.I.self;
				if (self4 != null)
				{
					FieldManager.FieldTransitionInfo fieldTransitionInfo4 = trans_info2;
					Vector3 position7 = self4._position;
					fieldTransitionInfo4.mapX = position7.x;
					FieldManager.FieldTransitionInfo fieldTransitionInfo5 = trans_info2;
					Vector3 position8 = self4._position;
					fieldTransitionInfo5.mapZ = position8.z;
					FieldManager.FieldTransitionInfo fieldTransitionInfo6 = trans_info2;
					Quaternion rotation4 = self4._rotation;
					Vector3 eulerAngles4 = rotation4.get_eulerAngles();
					fieldTransitionInfo6.mapDir = eulerAngles4.y;
				}
				MonoBehaviourSingleton<InGameManager>.I.backTransitionInfo = trans_info2;
				MonoBehaviourSingleton<InGameManager>.I.beforePortalID = toQuestPortalID;
				ChangeSceneToFieldQuestStory();
			}
			break;
		case PROGRESS_END_TYPE.FIELD_READ_STORY:
			if (MonoBehaviourSingleton<FieldManager>.IsValid())
			{
				MonoBehaviourSingleton<InGameManager>.I.isTransitionQuestToField = true;
				MonoBehaviourSingleton<InGameManager>.I.isQuestGate = false;
				MonoBehaviourSingleton<InGameManager>.I.isQuestHappen = true;
				MonoBehaviourSingleton<InGameManager>.I.readStoryID = (isFieldReadStorySend ? fieldReadStoryId : 0);
				FieldManager.FieldTransitionInfo trans_info3 = new FieldManager.FieldTransitionInfo
				{
					portalID = MonoBehaviourSingleton<FieldManager>.I.currentPortalID,
					mapID = MonoBehaviourSingleton<FieldManager>.I.currentMapID
				};
				Self self5 = MonoBehaviourSingleton<StageObjectManager>.I.self;
				if (self5 != null)
				{
					FieldManager.FieldTransitionInfo fieldTransitionInfo = trans_info3;
					Vector3 position = self5._position;
					fieldTransitionInfo.mapX = position.x;
					FieldManager.FieldTransitionInfo fieldTransitionInfo2 = trans_info3;
					Vector3 position2 = self5._position;
					fieldTransitionInfo2.mapZ = position2.z;
					FieldManager.FieldTransitionInfo fieldTransitionInfo3 = trans_info3;
					Quaternion rotation = self5._rotation;
					Vector3 eulerAngles = rotation.get_eulerAngles();
					fieldTransitionInfo3.mapDir = eulerAngles.y;
				}
				MonoBehaviourSingleton<InGameManager>.I.backTransitionInfo = trans_info3;
				ChangeSceneFieldReadStory();
			}
			break;
		case PROGRESS_END_TYPE.ARENA_INTERVAL:
		{
			MonoBehaviourSingleton<InGameManager>.I.ProgressArena();
			uint nextQuestId2 = MonoBehaviourSingleton<InGameManager>.I.GetCurrentArenaQuestId();
			MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(nextQuestId2, true);
			ChangeSceneToInterval();
			break;
		}
		}
	}

	private IEnumerator DoWaitNetwork()
	{
		yield return (object)new WaitForSeconds(MonoBehaviourSingleton<InGameSettingsManager>.I.inGameProgress.waitNetworkMarginTime);
		MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.MANUAL_NETWORK, true);
	}

	public void CreatePortalPoint(FieldMapPortalInfo portal_info, Coop_Model_EnemyDefeat model)
	{
		if (model.ppt > 0 && FieldManager.IsValidInGameNoBoss() && MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			int i = 0;
			for (int count = MonoBehaviourSingleton<InGameProgress>.I.portalObjectList.Count; i < count; i++)
			{
				PortalObject portalObject = MonoBehaviourSingleton<InGameProgress>.I.portalObjectList[i];
				if (portalObject.portalID == portal_info.portalData.portalID)
				{
					PortalPointEffect.Create(portalObject, model);
				}
				else
				{
					portalObject.UpdateView();
				}
			}
		}
	}

	public void RealizesLinkResourcesFieldEnemyBoss(Action callback)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(_RealizesLinkResourcesFieldEnemyBoss(callback));
	}

	public IEnumerator _RealizesLinkResourcesFieldEnemyBoss(Action callback)
	{
		LoadingQueue loadingLinkQueue = new LoadingQueue(this);
		LoadObject linkObj = loadingLinkQueue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemInGameLinkResources", new string[1]
		{
			"InGameLinkResourcesFieldEnemyBoss"
		}, false);
		if (loadingLinkQueue.IsLoading())
		{
			yield return (object)loadingLinkQueue.Wait();
		}
		ResourceObject[] loadedObjects = linkObj.loadedObjects;
		foreach (ResourceObject prefab in loadedObjects)
		{
			ResourceUtility.Realizes(prefab.obj, MonoBehaviourSingleton<InGameSettingsManager>.I._transform, -1);
		}
		callback.SafeInvoke();
	}

	public void PlayFieldEnemyBossVictoryEffect()
	{
		if (MonoBehaviourSingleton<InGameLinkResourcesFieldEnemyBoss>.IsValid())
		{
			CreateUIEffect(MonoBehaviourSingleton<InGameLinkResourcesFieldEnemyBoss>.I.questWin);
			PlayAudio(AUDIO.RESULT_WIN);
		}
	}

	public void PlayFieldEnemyBossTimesUpEffect()
	{
		if (MonoBehaviourSingleton<InGameLinkResourcesFieldEnemyBoss>.IsValid())
		{
			CreateUIEffect(MonoBehaviourSingleton<InGameLinkResourcesFieldEnemyBoss>.I.questTimeUp);
			PlayAudio(AUDIO.RESULT_LOSE);
		}
	}

	private void CreateUIEffect(string effect_name)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		if (viewFx != null)
		{
			Object.DestroyImmediate(viewFx.get_gameObject());
		}
		viewFx = EffectManager.GetUIEffect(effect_name);
		if (!(viewFx == null))
		{
			DisableNotifyMonoBehaviour disableNotifyMonoBehaviour = viewFx.get_gameObject().AddComponent<DisableNotifyMonoBehaviour>();
			disableNotifyMonoBehaviour.SetNotifyMaster(this);
		}
	}

	private void CreateUIEffect(GameObject obj)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		if (viewFx != null)
		{
			Object.DestroyImmediate(viewFx.get_gameObject());
		}
		viewFx = ResourceUtility.Realizes(obj, MonoBehaviourSingleton<GameSceneManager>.I.GetLastSectionExcludeCommonDialog()._transform, -1);
		if (!(viewFx == null))
		{
			DisableNotifyMonoBehaviour disableNotifyMonoBehaviour = viewFx.get_gameObject().AddComponent<DisableNotifyMonoBehaviour>();
			disableNotifyMonoBehaviour.SetNotifyMaster(this);
		}
	}

	protected override void OnDetachServant(DisableNotifyMonoBehaviour servant)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		base.OnDetachServant(servant);
		if (viewFx.get_gameObject() == servant.get_gameObject())
		{
			viewFx = null;
		}
	}

	private void ViewUI(bool enable)
	{
		Transform val = MonoBehaviourSingleton<UIManager>.I.Find("InGameMain");
		if (val != null)
		{
			val.GetComponent<UIBehaviour>().uiVisible = enable;
			if (enable)
			{
				UIRect[] componentsInChildren = val.GetComponentsInChildren<UIRect>();
				int i = 0;
				for (int num = componentsInChildren.Length; i < num; i++)
				{
					componentsInChildren[i].UpdateAnchors();
				}
			}
		}
		if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.mainChat != null)
		{
			if (enable)
			{
				MonoBehaviourSingleton<UIManager>.I.mainChat.Open(UITransition.TYPE.OPEN);
			}
			else
			{
				MonoBehaviourSingleton<UIManager>.I.mainChat.HideAll();
			}
		}
		if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.invitationInGameButton != null)
		{
			if (enable && MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite)
			{
				MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.Open(UITransition.TYPE.OPEN);
			}
			else
			{
				MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.Close(UITransition.TYPE.CLOSE);
			}
		}
	}

	private bool IsValidPlayer()
	{
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return false;
		}
		int i = 0;
		for (int count = MonoBehaviourSingleton<StageObjectManager>.I.playerList.Count; i < count; i++)
		{
			Player player = MonoBehaviourSingleton<StageObjectManager>.I.playerList[i] as Player;
			if (!player.isDead)
			{
				return true;
			}
		}
		return false;
	}

	private bool IsValidEnemy()
	{
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return false;
		}
		if (QuestManager.IsValidInGameSeries() && !MonoBehaviourSingleton<QuestManager>.I.IsOverCurrentQuestSeries())
		{
			return true;
		}
		if (FieldManager.IsValidInGame() && MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
			if (boss != null)
			{
				if (boss.isDead)
				{
					return false;
				}
			}
			else if (MonoBehaviourSingleton<CoopManager>.I.coopStage.isEnemyExtermination)
			{
				return false;
			}
		}
		else
		{
			Enemy boss2 = MonoBehaviourSingleton<StageObjectManager>.I.boss;
			if (boss2 == null)
			{
				return false;
			}
			if (boss2.isDead)
			{
				return false;
			}
		}
		return true;
	}

	private void SetBattleEndCharacter(Character character)
	{
		if (!(character == null))
		{
			character.hitOffFlag |= StageObject.HIT_OFF_FLAG.FORCE;
			if (character.controller != null)
			{
				character.controller.SetEnableControll(false, ControllerBase.DISABLE_FLAG.BATTLE_END);
			}
			if (character.packetSender != null)
			{
				character.packetSender.enableSend = false;
			}
			if (character.packetReceiver != null)
			{
				character.packetReceiver.SetStopPacketUpdate(true);
			}
		}
	}

	private void SetBattleEndAllCharacters()
	{
		int i = 0;
		for (int count = MonoBehaviourSingleton<StageObjectManager>.I.characterList.Count; i < count; i++)
		{
			Character battleEndCharacter = MonoBehaviourSingleton<StageObjectManager>.I.characterList[i] as Character;
			SetBattleEndCharacter(battleEndCharacter);
		}
	}

	private void SetBattleEndAllPlayer(Player player)
	{
		if (!(player == null))
		{
			player.ClearStoreEffect();
		}
	}

	private void SetBattleEndAllPlayers()
	{
		for (int i = 0; i < MonoBehaviourSingleton<StageObjectManager>.I.playerList.Count; i++)
		{
			Player battleEndAllPlayer = MonoBehaviourSingleton<StageObjectManager>.I.playerList[i] as Player;
			SetBattleEndAllPlayer(battleEndAllPlayer);
		}
	}

	public void DeliveryAddCheck()
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<DeliveryManager>.IsValid() && MonoBehaviourSingleton<DeliveryManager>.I.noticeNewDeliveryAtInGame.Count > 0 && !isHappenQuestDirection)
		{
			this.StartCoroutine(WaitQuestDetail());
		}
	}

	private IEnumerator WaitQuestDetail()
	{
		while (MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
		{
			yield return (object)null;
		}
		int id = MonoBehaviourSingleton<DeliveryManager>.I.noticeNewDeliveryAtInGame[0];
		MonoBehaviourSingleton<DeliveryManager>.I.noticeNewDeliveryAtInGame.RemoveAt(0);
		MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameProgress", this.get_gameObject(), "QUEST_DETAIL", id, null, true);
		while (GameSceneEvent.current.eventName == "QUEST_DETAIL")
		{
			yield return (object)null;
		}
		DeliveryAddCheck();
	}

	private void SendComplete()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoSendComplete());
	}

	private unsafe IEnumerator DoSendComplete()
	{
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			isRecvQuestComplete = false;
			yield return (object)null;
			if (QuestManager.IsValidInGameExplore())
			{
				Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
				if (Object.op_Implicit(boss))
				{
					MonoBehaviourSingleton<QuestManager>.I.UpdateExploreBossStatus(boss);
				}
			}
			while (!MonoBehaviourSingleton<QuestManager>.I.IsUnLockedTimeForCompleteSend())
			{
				yield return (object)null;
			}
			float start_time = Time.get_time();
			while (!MonoBehaviourSingleton<CoopManager>.I.coopRoom.IsValidBattleComplete() && Time.get_time() - start_time < MonoBehaviourSingleton<InGameSettingsManager>.I.inGameProgress.waitCompleteOwnerTimeout)
			{
				yield return (object)null;
			}
			while (MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
			{
				yield return (object)null;
			}
			int? nullable = MonoBehaviourSingleton<QuestManager>.I.clearStatusQuest.Find((ClearStatusQuest data) => data.questId == MonoBehaviourSingleton<QuestManager>.I.currentQuestID)?.questStatus;
			OverwritePlayerRecorderForExplore(true);
			CoopApp.QuestComplete(new Action<bool, Error>((object)/*Error near IL_0196: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}

	private void SendArenaComplete()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoSendArenaComplete());
	}

	private unsafe IEnumerator DoSendArenaComplete()
	{
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			isRecvQuestComplete = false;
			yield return (object)null;
			while (!MonoBehaviourSingleton<QuestManager>.I.IsUnLockedTimeForCompleteSend())
			{
				yield return (object)null;
			}
			float startTime = Time.get_time();
			while (!MonoBehaviourSingleton<CoopManager>.I.coopRoom.IsValidBattleComplete() && Time.get_time() - startTime < MonoBehaviourSingleton<InGameSettingsManager>.I.inGameProgress.waitCompleteOwnerTimeout)
			{
				yield return (object)null;
			}
			while (MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
			{
				yield return (object)null;
			}
			int? nullable = MonoBehaviourSingleton<QuestManager>.I.clearStatusQuest.Find((ClearStatusQuest data) => data.questId == MonoBehaviourSingleton<QuestManager>.I.currentQuestID)?.questStatus;
			CoopApp.ArenaComplete(new Action<bool, Error>((object)/*Error near IL_0155: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}

	private void SendArenaRetire()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoSendArenaRetire());
	}

	private IEnumerator DoSendArenaRetire()
	{
		isRecvQuestComplete = false;
		while (MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
		{
			yield return (object)null;
		}
		bool isTimeout = remaindTime <= 0f;
		CoopApp.ArenaRetire(isTimeout, delegate
		{
			((_003CDoSendArenaRetire_003Ec__Iterator22F)/*Error near IL_0075: stateMachine*/)._003C_003Ef__this.isRecvQuestComplete = true;
		});
	}

	private void SendRetire()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoSendRetire());
	}

	private IEnumerator DoSendRetire()
	{
		isRecvQuestComplete = false;
		while (MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
		{
			yield return (object)null;
		}
		bool is_timeout = remaindTime <= 0f;
		CoopApp.QuestRetire(is_timeout, delegate
		{
			((_003CDoSendRetire_003Ec__Iterator230)/*Error near IL_0075: stateMachine*/)._003C_003Ef__this.isRecvQuestComplete = true;
		});
	}

	private void SendRushProgress()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoSendRushProgress());
	}

	private unsafe IEnumerator DoSendRushProgress()
	{
		yield return (object)null;
		int wave = MonoBehaviourSingleton<InGameManager>.I.GetCurrentWaveNum();
		int remainSec = (int)remaindTime;
		List<int> breakIds = MonoBehaviourSingleton<CoopManager>.I.coopStage.bossBreakIDLists[0];
		List<int> clearMissions = GetMissionClearStatuses();
		float hpRate = MonoBehaviourSingleton<CoopManager>.I.coopStage.bossStartHpDamageRate;
		List<QuestCompleteModel.BattleUserLog> logs = MonoBehaviourSingleton<CoopManager>.I.coopStage.battleUserLog.list;
		List<int> memIds = null;
		if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
		{
			memIds = MonoBehaviourSingleton<QuestManager>.I.resultUserCollection.GetUserIdList(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
		}
		isRecvRushProgress = false;
		MonoBehaviourSingleton<InGameManager>.I.RecordRushWaveSyncData();
		yield return (object)null;
		MonoBehaviourSingleton<QuestManager>.I.SendQuestRushProgress(wave, remainSec, breakIds, clearMissions, memIds, hpRate, logs, new Action<bool, Error>((object)/*Error near IL_0142: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	private void SendArenaProgress()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoSendArenaProgress());
	}

	private unsafe IEnumerator DoSendArenaProgress()
	{
		yield return (object)null;
		ArenaProgressModel.RequestSendForm requestData = new ArenaProgressModel.RequestSendForm
		{
			wave = MonoBehaviourSingleton<InGameManager>.I.GetCurrentArenaWaveNum(),
			remainMilliSec = (XorInt)Mathf.FloorToInt(remaindTime * 1000f),
			elapseMilliSec = (XorInt)Mathf.FloorToInt(GetElapsedTime() * 1000f),
			breakIds = MonoBehaviourSingleton<CoopManager>.I.coopStage.bossBreakIDLists[0],
			logs = MonoBehaviourSingleton<CoopManager>.I.coopStage.battleUserLog.list,
			enemyHp = MonoBehaviourSingleton<InGameRecorder>.I.GetTotalEnemyHP()
		};
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && MonoBehaviourSingleton<StageObjectManager>.I.self != null)
		{
			requestData.actioncount = MonoBehaviourSingleton<StageObjectManager>.I.self.taskChecker.GetTaskCount();
			MonoBehaviourSingleton<StageObjectManager>.I.self.taskChecker.Clear();
		}
		if (MonoBehaviourSingleton<InGameManager>.IsValid())
		{
			requestData.deliveryBattleInfo = MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.GetInfo();
			MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.ClearInfo();
		}
		isRecvArenaProgress = false;
		yield return (object)null;
		MonoBehaviourSingleton<QuestManager>.I.SendQuestArenaProgress(requestData, new Action<bool, Error>((object)/*Error near IL_01a7: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	public void OnDamage(AttackedHitStatusFix status, Character chara)
	{
		missionCheck.ForEach(delegate(MissionCheckBase mission)
		{
			mission.OnDamage(status, chara);
		});
	}

	public void OnSkillUse(SkillInfo.SkillParam param)
	{
		missionCheck.ForEach(delegate(MissionCheckBase mission)
		{
			mission.OnSkillUse(param);
		});
	}

	private void ChangeSceneToInterval()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoChangeSceneToInterval());
	}

	private IEnumerator DoChangeSceneToInterval()
	{
		if (MonoBehaviourSingleton<UIManager>.IsValid())
		{
			MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(true);
			yield return (object)new WaitForEndOfFrame();
		}
		yield return (object)this.StartCoroutine(DoCloseDialog());
		yield return (object)MonoBehaviourSingleton<AppMain>.I.ClearEnemyAssets();
		if (MonoBehaviourSingleton<UIManager>.IsValid())
		{
			yield return (object)new WaitForEndOfFrame();
			MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(false);
			yield return (object)new WaitForEndOfFrame();
		}
		isGameProgressStop = false;
		MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameProgress", this.get_gameObject(), "INTERVAL", null, null, true);
	}

	private void ChangeSceneToFieldQuestStory()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		MonoBehaviourSingleton<InGameManager>.I.SaveQuestTransferInfo();
		this.StartCoroutine(DoChangeSceneToFieldQuestStory());
	}

	private IEnumerator DoChangeSceneToFieldQuestStory()
	{
		yield return (object)this.StartCoroutine(DoCloseDialog());
		QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID);
		MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameProgress", this.get_gameObject(), "STORY", new object[3]
		{
			questData.storyId,
			0,
			0
		}, null, true);
	}

	private void ChangeSceneToResult()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoChangeSceneToResult());
	}

	private IEnumerator DoChangeSceneToResult()
	{
		MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(true);
		yield return (object)new WaitForEndOfFrame();
		yield return (object)this.StartCoroutine(DoCloseDialog());
		string event_name = (!MonoBehaviourSingleton<InGameRecorder>.IsValid() || !MonoBehaviourSingleton<InGameRecorder>.I.isVictory) ? "FRIEND" : "RESULT";
		if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.IsRush())
		{
			event_name = ((MonoBehaviourSingleton<InGameManager>.I.GetRushIndex() == 0) ? "FRIEND" : ((!MonoBehaviourSingleton<DeliveryManager>.I.IsCarnivalEvent(MonoBehaviourSingleton<QuestManager>.I.currentQuestData.eventId)) ? "RUSH_RESULT" : "CARNIVAL_RESULT"));
		}
		if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
		{
			event_name = "ARENA_RESULT";
		}
		if (QuestManager.IsValidInGameWaveMatch(true))
		{
			event_name = ((!MonoBehaviourSingleton<DeliveryManager>.I.IsCarnivalEvent(MonoBehaviourSingleton<QuestManager>.I.currentQuestData.eventId)) ? "WAVE_RESULT" : "CARNIVAL_RESULT");
		}
		if (QuestManager.IsValidInGameTrial())
		{
			event_name = ((!MonoBehaviourSingleton<InGameRecorder>.IsValid() || !MonoBehaviourSingleton<InGameRecorder>.I.isVictory) ? "TRIAL_RETIRE" : "TRIAL_RESULT");
		}
		MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameProgress", this.get_gameObject(), event_name, null, null, true);
	}

	public void ReloadScene()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoReloadScene());
	}

	private IEnumerator DoReloadScene()
	{
		yield return (object)this.StartCoroutine(DoCloseDialog());
		isBattleStart = false;
		progressEndType = PROGRESS_END_TYPE.NONE;
		isGameProgressStop = false;
		isHappenQuestDirection = false;
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			MonoBehaviourSingleton<CoopManager>.I.Clear();
		}
		if (MonoBehaviourSingleton<InGameManager>.IsValid())
		{
			MonoBehaviourSingleton<InGameManager>.I.isRetry = true;
		}
		MonoBehaviourSingleton<GameSceneManager>.I.ReloadScene(UITransition.TYPE.CLOSE, UITransition.TYPE.OPEN, false);
	}

	private void ChangeSceneToHome()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoChangeSceneToHome());
	}

	private IEnumerator DoChangeSceneToHome()
	{
		yield return (object)this.StartCoroutine(DoCloseDialog());
		MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameProgress", this.get_gameObject(), "HOME", null, null, true);
	}

	private void ChangeSceneToStory(uint deliveryID)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoChangeSceneToStory(deliveryID));
	}

	private unsafe IEnumerator DoChangeSceneToStory(uint deliveryID)
	{
		yield return (object)this.StartCoroutine(DoCloseDialog());
		DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData(deliveryID);
		Delivery[] deliveryList = MonoBehaviourSingleton<DeliveryManager>.I.GetDeliveryList(true);
		Delivery delivery = Array.Find(deliveryList, delegate(Delivery o)
		{
			if (o.dId == (int)((_003CDoChangeSceneToStory_003Ec__Iterator238)/*Error near IL_0077: stateMachine*/).deliveryID)
			{
				return true;
			}
			return false;
		});
		if (delivery == null)
		{
			ChangeSceneToHome();
		}
		else
		{
			int clearEventID = (int)deliveryTableData.clearEventID;
			bool enable_clear_event = 0 != clearEventID;
			bool flag = !TutorialStep.HasFirstDeliveryCompleted();
			MonoBehaviourSingleton<DeliveryManager>.I.SendDeliveryComplete(delivery.uId, enable_clear_event, new Action<bool, DeliveryRewardList>((object)/*Error near IL_00ef: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}

	private void ChangeSceneFieldReadStory()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		MonoBehaviourSingleton<InGameManager>.I.SaveQuestTransferInfo();
		this.StartCoroutine(DoChangeSceneFieldReadStory(fieldReadStoryId));
	}

	private IEnumerator DoChangeSceneFieldReadStory(int storyId)
	{
		yield return (object)this.StartCoroutine(DoCloseDialog());
		MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameProgress", this.get_gameObject(), "STORY", new object[3]
		{
			storyId,
			0,
			0
		}, null, true);
	}

	public void CloseDialog()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoCloseDialog());
	}

	private IEnumerator DoCloseDialog()
	{
		if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.mainChat != null && MonoBehaviourSingleton<UIManager>.I.mainChat.IsOpeningWindow())
		{
			MonoBehaviourSingleton<UIManager>.I.mainChat.HideAll();
		}
		if (MonoBehaviourSingleton<UIInGameMenu>.IsValid())
		{
			MonoBehaviourSingleton<UIInGameMenu>.I.Close();
		}
		yield return (object)this.StartCoroutine(_DoCloseDialog());
	}

	private IEnumerator _DoCloseDialog()
	{
		while (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
		{
			yield return (object)null;
		}
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionType().IsDialog())
		{
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameProgress", this.get_gameObject(), "[BACK]", null, null, true);
			yield return (object)this.StartCoroutine(_DoCloseDialog());
		}
	}

	public void SetDisableUIOpen(bool disable)
	{
		if (UIInGameFieldMenu.IsValid())
		{
			UIInGameFieldMenu.I.SetDisableButtons(disable);
			InGameMain inGameMain = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentScreen() as InGameMain;
			if (inGameMain != null)
			{
				inGameMain.SetMapButtonState();
			}
		}
		if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
		{
			MonoBehaviourSingleton<UIPlayerStatus>.I.SetDisableButtons(disable);
		}
		if (MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
		{
			MonoBehaviourSingleton<UIEnduranceStatus>.I.SetDisableButtons(disable);
		}
		if (MonoBehaviourSingleton<UIContinueButton>.IsValid())
		{
			MonoBehaviourSingleton<UIContinueButton>.I.SetDisableButtons(disable);
		}
		if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.invitationInGameButton != null)
		{
			MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.SetDisableButton(disable);
		}
		InGameMain inGameMain2 = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentScreen() as InGameMain;
		if (inGameMain2 != null)
		{
			Transform ctrl = inGameMain2.GetCtrl(InGameMain.UI.BTN_CHAT);
			if (ctrl != null)
			{
				UIButton component = ctrl.GetComponent<UIButton>();
				if (component != null)
				{
					component.isEnabled = !disable;
				}
			}
			Transform ctrl2 = inGameMain2.GetCtrl(InGameMain.UI.BTN_QUEST_MENU);
			if (ctrl2 != null)
			{
				UIButton component2 = ctrl2.GetComponent<UIButton>();
				if (component2 != null)
				{
					component2.isEnabled = !disable;
				}
			}
		}
	}

	public void StartTimer(float elapsed_time = 0f)
	{
		if (!isInitStartTime && enableLimitTime)
		{
			isInitStartTime = true;
			elapsedTime = elapsed_time;
			startTime = Time.get_time();
			stopTime = -1f;
			MonoBehaviourSingleton<InGameManager>.I.StopIntervalTransferInfoRemaindTimeUpdate();
			bool flag = QuestManager.IsValidInGameExplore() || MonoBehaviourSingleton<InGameManager>.I.IsRush() || QuestManager.IsValidInGameSeries() || QuestManager.IsValidInGameWaveMatch(false);
			if (MonoBehaviourSingleton<CoopNetworkManager>.IsValid() && flag && MonoBehaviourSingleton<CoopManager>.I.isStageHost)
			{
				float num = limitTime;
				if (MonoBehaviourSingleton<InGameManager>.I.intervalTransferInfo != null && MonoBehaviourSingleton<InGameManager>.I.intervalTransferInfo.remaindTime >= 0f)
				{
					num = MonoBehaviourSingleton<InGameManager>.I.intervalTransferInfo.remaindTime;
				}
				float elapsed_sec = limitTime - num;
				if (MonoBehaviourSingleton<InGameManager>.I.IsRush() && MonoBehaviourSingleton<InGameManager>.I.intervalTransferInfo != null && MonoBehaviourSingleton<InGameManager>.I.isRushReentry)
				{
					elapsed_sec = MonoBehaviourSingleton<InGameManager>.I.intervalTransferInfo.elapsedTime;
				}
				if (QuestManager.IsValidInGameSeries() && MonoBehaviourSingleton<InGameManager>.I.intervalTransferInfo != null && MonoBehaviourSingleton<InGameManager>.I.isSeriesReentry)
				{
					elapsed_sec = MonoBehaviourSingleton<InGameManager>.I.intervalTransferInfo.elapsedTime;
				}
				if (QuestManager.IsValidInGameWaveMatch(false) && MonoBehaviourSingleton<InGameManager>.I.intervalTransferInfo != null)
				{
					elapsed_sec = MonoBehaviourSingleton<InGameManager>.I.intervalTransferInfo.elapsedTime;
				}
				if (QuestManager.IsValidInGameExplore())
				{
					MonoBehaviourSingleton<CoopNetworkManager>.I.RoomTimeCheck(elapsed_sec);
				}
				SetElapsedTime(elapsed_sec);
			}
			if (MonoBehaviourSingleton<InGameManager>.I.IsArenaTimeAttack() && MonoBehaviourSingleton<InGameManager>.I.intervalTransferInfo != null)
			{
				arenaElapsedSec = MonoBehaviourSingleton<InGameManager>.I.intervalTransferInfo.elapsedTime;
			}
		}
	}

	public void ResetStartTimer(float limit_time, float elapsed_time = 0f)
	{
		isInitStartTime = false;
		SetLimitTime(limit_time);
		StartTimer(elapsed_time);
	}

	public void SetElapsedTime(float elapsed_time)
	{
		if (enableLimitTime)
		{
			float num = Time.get_time() - startTime;
			elapsedTime = elapsed_time - num;
		}
	}

	public float GetElapsedTime()
	{
		if (!isInitStartTime)
		{
			return 0f;
		}
		if (!enableLimitTime)
		{
			return 0f;
		}
		float num = 0f;
		if (IsStartTimer())
		{
			num += Time.get_time() - startTime + elapsedTime;
		}
		if (IsStopTimer())
		{
			num -= Time.get_time() - stopTime;
		}
		if (num < 0f)
		{
			num = 0f;
		}
		return num;
	}

	public void SetLimitTime(float limit_time)
	{
		limitTime = limit_time;
		enableLimitTime = (limit_time != 0f);
	}

	public void AddLimitTime(float add_time)
	{
		limitTime += add_time;
	}

	public bool IsStartTimer()
	{
		return startTime >= 0f && isInitStartTime;
	}

	public bool IsStopTimer()
	{
		return stopTime >= 0f;
	}

	public void StopTimer()
	{
		stopTime = Time.get_time();
	}

	public void RestartTimer()
	{
		if (isInitStartTime && IsStopTimer())
		{
			startTime += Time.get_time() - stopTime;
			stopTime = -1f;
		}
	}

	public void SetRushRemainTime(int remainTime)
	{
		rushRemainTime = (float)remainTime;
	}

	public string GetRushRemainTimeToString()
	{
		return GetTimeToString(Mathf.CeilToInt(remaindTime));
	}

	public static string GetTimeToString(int time_int)
	{
		char[] array = new char[32];
		if (time_int < 0)
		{
			time_int = 0;
		}
		int num = 48;
		array[0] = (char)(num + time_int / 3600);
		array[1] = ':';
		int num2 = time_int / 60 % 60;
		array[2] = (char)(num + num2 / 10);
		array[3] = (char)(num + num2 % 10);
		array[4] = ':';
		int num3 = time_int % 60;
		array[5] = (char)(num + num3 / 10);
		array[6] = (char)(num + num3 % 10);
		return new string(array);
	}

	public static string GetTimeToStringMMSS(int time_int)
	{
		if (time_int < 0)
		{
			time_int = 0;
		}
		int num = time_int % 60;
		int num2 = time_int / 60;
		int num3 = num2 / 60;
		num2 -= num3 * 60;
		string str = string.Empty;
		if (num3 > 0)
		{
			str = num3.ToString() + ":";
		}
		return str + $"{num2:D2}:{num:D2}";
	}

	public static string GetTimeWithMilliSecToString(float time)
	{
		char[] array = new char[32];
		if (time < 0f)
		{
			time = 0f;
		}
		int num = 48;
		int num2 = Mathf.FloorToInt(time);
		int num3 = num2 / 60 % 60;
		array[0] = (char)(num + num3 / 10);
		array[1] = (char)(num + num3 % 10);
		array[2] = ':';
		int num4 = num2 % 60;
		array[3] = (char)(num + num4 / 10);
		array[4] = (char)(num + num4 % 10);
		array[5] = '.';
		int num5 = Mathf.FloorToInt(time % 1f * 1000f);
		array[6] = (char)(num + num5 / 100);
		array[7] = (char)(num + num5 % 100 / 10);
		array[8] = (char)(num + num5 % 100 % 10);
		return new string(array);
	}

	public string GetRemainTime()
	{
		int time_int = 0;
		bool flag = true;
		if (MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			if (MonoBehaviourSingleton<InGameProgress>.I.enableVictoryIntervalTime)
			{
				time_int = Mathf.CeilToInt(MonoBehaviourSingleton<InGameProgress>.I.victoryIntervalTime);
				flag = true;
			}
			else
			{
				time_int = Mathf.CeilToInt(MonoBehaviourSingleton<InGameProgress>.I.remaindTime);
				flag = MonoBehaviourSingleton<InGameProgress>.I.enableLimitTime;
			}
		}
		return (!flag) ? "-:--:--" : GetTimeToString(time_int);
	}

	public void SetRushTimeBonus(List<QuestRushProgressData.RushTimeBonus> rushTimeBonus)
	{
		this.rushTimeBonus = rushTimeBonus;
	}

	public void SetArenaRemainTime(XorInt remainTime)
	{
		arenaRemainSec = (float)(int)remainTime * 0.001f;
	}

	public XorInt GetArenaRemainMilliSec()
	{
		return Mathf.FloorToInt((float)arenaRemainSec * 1000f);
	}

	public string GetArenaRemainTimeToString()
	{
		return GetTimeToString(Mathf.CeilToInt(remaindTime));
	}

	public float GetArenaElapsedTime()
	{
		return (float)arenaElapsedSec + GetElapsedTime();
	}

	public XorInt GetArenaElapsedMilliSec()
	{
		return Mathf.FloorToInt(GetArenaElapsedTime() * 1000f);
	}

	public string GetArenaElapseTimeToString()
	{
		return GetTimeWithMilliSecToString(GetArenaElapsedTime());
	}

	public void SetArenaTimeBonus(List<QuestArenaProgressData.ArenaTimeBonus> arenaTimeBonus)
	{
		this.arenaTimeBonus = arenaTimeBonus;
	}

	public void SetAfkLimitTime()
	{
		enableAfkTime = ((afkTime = 480f) > 0f);
	}

	public void StartAfkTimer()
	{
		if (enableAfkTime)
		{
			afkTime = 480f;
		}
	}

	private void ResetAfkTimer()
	{
		if (MonoBehaviourSingleton<FieldManager>.IsValid())
		{
			StartAfkTimer();
		}
	}

	private void ProgressAfkTimer()
	{
		if (MonoBehaviourSingleton<FieldManager>.IsValid() && !(remainedAfkTime <= 0f))
		{
			afkTime -= Time.get_deltaTime();
		}
	}

	private void SetExploreBossMoveTimer()
	{
		int bossMoveTime = MonoBehaviourSingleton<PartyManager>.I.partyData.quest.explore.bossMoveTime;
		bossMoveTime = ((bossMoveTime > 0) ? bossMoveTime : 45);
		bossMoveRemainTime = (float)bossMoveTime;
	}

	private void ProgressExploreBossMoveTimer()
	{
		if (!(bossMoveRemainTime <= 0f))
		{
			bossMoveRemainTime -= Time.get_deltaTime();
		}
	}

	public void ResetExploreHostDCTimer()
	{
		exploreHostDCRemainTime = 30f;
		requestedExploreAlive = false;
	}

	private void SetExploreHostDCTimer(float time, bool requestAlive)
	{
		exploreHostDCRemainTime = time;
		requestedExploreAlive = requestAlive;
	}

	private void ProgressExploreHostDCTimer()
	{
		exploreHostDCRemainTime -= Time.get_deltaTime();
	}

	public void ExploreHappenQuestDirection()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		if (progressEndType == PROGRESS_END_TYPE.NONE && MonoBehaviourSingleton<QuestManager>.I.GetExploreBossAppearMapId() == (int)MonoBehaviourSingleton<FieldManager>.I.currentMapID)
		{
			this.StartCoroutine(DoExploreHappenQuestDirection());
		}
	}

	private IEnumerator DoExploreHappenQuestDirection()
	{
		isGameProgressStop = true;
		Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
		if (self != null)
		{
			self.hitOffFlag |= StageObject.HIT_OFF_FLAG.FORCE;
		}
		while (!isBattleStart)
		{
			yield return (object)null;
		}
		if (MonoBehaviourSingleton<QuestManager>.I.GetExploreBossAppearMapId() == (int)MonoBehaviourSingleton<FieldManager>.I.currentMapID)
		{
			while (MonoBehaviourSingleton<UIInGamePopupDialog>.I.IsShowingDialog())
			{
				yield return (object)null;
			}
			while (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
			{
				yield return (object)null;
			}
			SetDisableUIOpen(true);
			yield return (object)this.StartCoroutine(DoCloseDialog());
			PartyModel.ExploreInfo exploreData = MonoBehaviourSingleton<PartyManager>.I.partyData.quest.explore;
			int rareType = 0;
			if (exploreData != null)
			{
				rareType = exploreData.isRare;
			}
			if (MonoBehaviourSingleton<UIInGameFieldQuestWarning>.IsValid())
			{
				MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.Play(ENEMY_TYPE.NONE, rareType, false);
			}
			yield return (object)new WaitForSeconds(3f);
			SetDisableUIOpen(false);
			if (self != null && self.controller != null)
			{
				self.controller.SetEnableControll(false, ControllerBase.DISABLE_FLAG.BATTLE_END);
			}
			ExploreFieldToQuestInterval();
		}
	}

	public bool HappenQuestDirection(uint link_quest_id, QuestInfoData.Quest.Reward[] reward = null, int rareBossType = 0)
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		if (progressEndType != 0)
		{
			return false;
		}
		if (isHappenQuestDirection)
		{
			return false;
		}
		if (!FieldManager.IsValidInGameNoQuest())
		{
			return false;
		}
		if (MonoBehaviourSingleton<FieldManager>.I.currentMapData == null)
		{
			return false;
		}
		if (link_quest_id == 0)
		{
			return false;
		}
		if (!MonoBehaviourSingleton<QuestManager>.I.IsOpenedQuest(link_quest_id))
		{
			return false;
		}
		isHappenQuestDirection = true;
		this.StartCoroutine(DoHappenQuestDirection(link_quest_id, reward, rareBossType));
		return true;
	}

	private unsafe IEnumerator DoHappenQuestDirection(uint link_quest_id, QuestInfoData.Quest.Reward[] reward = null, int rareBossType = 0)
	{
		InGameSettingsManager.HappenQuestDirection parameter = MonoBehaviourSingleton<InGameSettingsManager>.I.happenQuestDirection;
		isGameProgressStop = true;
		Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
		if (self != null)
		{
			self.hitOffFlag |= StageObject.HIT_OFF_FLAG.FORCE;
		}
		while (!isBattleStart)
		{
			yield return (object)null;
		}
		while (MonoBehaviourSingleton<UIInGamePopupDialog>.I.IsShowingDialog())
		{
			yield return (object)null;
		}
		while (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
		{
			yield return (object)null;
		}
		SetDisableUIOpen(true);
		yield return (object)this.StartCoroutine(DoCloseDialog());
		uint enemy_id = 0u;
		QuestTable.QuestTableData quest_data = Singleton<QuestTable>.I.GetQuestData(link_quest_id);
		if (quest_data == null)
		{
			Log.Error(LOG.INGAME, "InGameProgress.DoHappenQuestDirection() LinkQuestID is invalid. quest_id : " + link_quest_id);
		}
		else
		{
			enemy_id = (uint)quest_data.GetMainEnemyID();
		}
		EnemyTable.EnemyData enemy_data = Singleton<EnemyTable>.I.GetEnemyData(enemy_id);
		if (enemy_data == null)
		{
			Log.Error(LOG.INGAME, "InGameProgress.DoHappenQuestDirection() EnemyID is invalid. enemy_id : " + enemy_id);
		}
		if (MonoBehaviourSingleton<UIInGameFieldQuestWarning>.IsValid())
		{
			MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.Play(enemy_data.type, rareBossType, false);
		}
		float before_time = Time.get_time();
		while (Time.get_time() - before_time < parameter.warningTime && !endHappenQuestDirection)
		{
			yield return (object)null;
		}
		if (!endHappenQuestDirection && !MonoBehaviourSingleton<GameSceneManager>.I.CheckQuestAndOpenUpdateAppDialog(quest_data, true, true))
		{
			endHappenQuestDirection = true;
		}
		if (endHappenQuestDirection)
		{
			if (MonoBehaviourSingleton<UIInGameFieldQuestWarning>.IsValid())
			{
				MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.get_gameObject().SetActive(false);
			}
			if (progressEndType == PROGRESS_END_TYPE.NONE)
			{
				isGameProgressStop = false;
				if (self != null)
				{
					self.hitOffFlag &= ~StageObject.HIT_OFF_FLAG.FORCE;
				}
			}
			isHappenQuestDirection = false;
			endHappenQuestDirection = false;
		}
		else
		{
			yield return (object)MonoBehaviourSingleton<TransitionManager>.I.Out(TransitionManager.TYPE.BLACK);
			SetDisableUIOpen(false);
			ViewUI(false);
			if (self != null && self.controller != null)
			{
				self.controller.SetEnableControll(false, ControllerBase.DISABLE_FLAG.BATTLE_END);
			}
			if (MonoBehaviourSingleton<TargetMarkerManager>.IsValid())
			{
				MonoBehaviourSingleton<TargetMarkerManager>.I.showMarker = false;
			}
			if (MonoBehaviourSingleton<DropTargetMarkerManeger>.IsValid())
			{
				MonoBehaviourSingleton<DropTargetMarkerManeger>.I.active = false;
			}
			Transform model = null;
			EnemyLoader enemy_loader = null;
			EnemyAnimCtrl anim_ctrl = null;
			Vector3 enemyInitPos = Vector3.get_zero();
			LoadObject lo_cam_portrait = null;
			LoadObject lo_cam_landscape = null;
			Vector3[] camera_offsets = null;
			if (enemy_data != null)
			{
				model = Utility.CreateGameObject("HappenQuestModel", this.get_transform(), -1);
				enemy_loader = model.get_gameObject().AddComponent<EnemyLoader>();
				model.set_position(Vector3.get_zero());
				int anim_id = enemy_data.animId;
				OutGameSettingsManager.EnemyDisplayInfo dispInfoAnim = MonoBehaviourSingleton<OutGameSettingsManager>.I.SearchEnemyDisplayInfoForQuestSelect(enemy_data);
				InGameSettingsManager.HappenQuestDirection.EnemyDisplayInfo disp_info;
				if (quest_data.questStyle != 0)
				{
					disp_info = parameter.GetEnemyDisplayInfoByQuestType(enemy_data, quest_data.questStyle);
					if (disp_info == null)
					{
						disp_info = parameter.GetEnemyDisplayInfo(enemy_data);
					}
				}
				else
				{
					disp_info = parameter.GetEnemyDisplayInfo(enemy_data);
				}
				if (dispInfoAnim != null && dispInfoAnim.animID > 0)
				{
					anim_id = dispInfoAnim.animID;
				}
				enemyInitPos = parameter.enemyInitPos;
				if (disp_info != null && disp_info.modelOffset != Vector3.get_zero())
				{
					enemyInitPos = disp_info.modelOffset;
				}
				LoadingQueue load_queue = new LoadingQueue(this);
				if (disp_info != null && !string.IsNullOrEmpty(disp_info.cameraNamePortrait))
				{
					lo_cam_portrait = load_queue.Load(RESOURCE_CATEGORY.ENEMY_CAMERA, disp_info.cameraNamePortrait, false);
				}
				if (disp_info != null && !string.IsNullOrEmpty(disp_info.cameraNameLandscape))
				{
					lo_cam_landscape = load_queue.Load(RESOURCE_CATEGORY.ENEMY_CAMERA, disp_info.cameraNameLandscape, false);
				}
				if (disp_info != null)
				{
					Vector3[] temp_offsets = (Vector3[])new Vector3[2]
					{
						disp_info.cameraOffsetPortrait,
						disp_info.cameraOffsetLandscape
					};
					camera_offsets = temp_offsets;
				}
				bool load_finish = false;
				enemy_loader.StartLoad(enemy_data.modelId, anim_id, enemy_data.modelScale, enemy_data.baseEffectName, enemy_data.baseEffectNode, true, true, true, ShaderGlobal.GetCharacterShaderType(), 18, null, true, false, delegate
				{
					((_003CDoHappenQuestDirection_003Ec__Iterator23D)/*Error near IL_068b: stateMachine*/)._003Cload_finish_003E__18 = true;
				});
				while (!load_finish)
				{
					yield return (object)null;
				}
				yield return (object)load_queue.Wait();
				anim_ctrl = model.get_gameObject().AddComponent<EnemyAnimCtrl>();
				anim_ctrl.Init(enemy_loader, null, true);
				Utility.SetLayerWithChildren(model, 18);
			}
			List<GameObject> change_layer_list = new List<GameObject>();
			List<GameObject> hideObjectList = new List<GameObject>();
			if (MonoBehaviourSingleton<StageManager>.IsValid())
			{
				Transform[] stage_objects = MonoBehaviourSingleton<StageManager>.I.GetComponentsInChildren<Transform>();
				int j = 0;
				for (int len2 = stage_objects.Length; j < len2; j++)
				{
					if (stage_objects[j].get_gameObject().get_layer() == 0)
					{
						stage_objects[j].get_gameObject().set_layer(18);
						change_layer_list.Add(stage_objects[j].get_gameObject());
					}
					if (stage_objects[j].get_name().Contains("HideObjects"))
					{
						stage_objects[j].get_gameObject().SetActive(false);
						hideObjectList.Add(stage_objects[j].get_gameObject());
					}
				}
			}
			if (MonoBehaviourSingleton<InGameCameraManager>.IsValid() && enemy_loader != null && enemy_loader.body != null)
			{
				Object[] cameras = (Object[])new Object[2]
				{
					lo_cam_portrait.loadedObject,
					lo_cam_landscape.loadedObject
				};
				MonoBehaviourSingleton<InGameCameraManager>.I.OnHappenQuestDirection(true, enemy_loader.body, cameras, camera_offsets);
			}
			if (model != null && enemy_loader != null && enemy_loader.body != null)
			{
				model.get_transform().set_position(Vector3.Scale(enemyInitPos, enemy_loader.body.get_lossyScale()));
				model.get_transform().set_rotation(Quaternion.AngleAxis(parameter.enemyInitDir, Vector3.get_up()));
			}
			bool anim_end = false;
			anim_ctrl.PlayQuestStartAnim(new Action((object)/*Error near IL_0933: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			if (!forceComplete)
			{
				yield return (object)MonoBehaviourSingleton<TransitionManager>.I.In();
			}
			if (MonoBehaviourSingleton<UIInGameFieldQuestWarning>.IsValid())
			{
				MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.get_gameObject().SetActive(false);
			}
			while (!anim_end && !endHappenQuestDirection)
			{
				yield return (object)null;
			}
			if (!endHappenQuestDirection)
			{
				isDecidedHappenQuestDialog = false;
				isYesHappenQuestDialog = false;
				while (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
				{
					yield return (object)null;
				}
				InGameFieldQuestConfirm.Desc desc = new InGameFieldQuestConfirm.Desc
				{
					questData = quest_data,
					reward = reward
				};
				MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameProgress", this.get_gameObject(), "HAPPEN_QUEST", desc, null, true);
				while (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
				{
					yield return (object)null;
				}
				while (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection() is InGameFieldQuestConfirm)
				{
					if (endHappenQuestDirection)
					{
						while (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
						{
							yield return (object)null;
						}
						if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection() is InGameFieldQuestConfirm)
						{
							MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameProgress", this.get_gameObject(), "[BACK]", null, null, true);
						}
						break;
					}
					yield return (object)null;
				}
				while (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
				{
					yield return (object)null;
				}
			}
			if (isYesHappenQuestDialog && !endHappenQuestDirection)
			{
				isHappenQuestDirection = false;
				FieldToQuestInterval(link_quest_id, 0u, false);
			}
			else
			{
				GameSceneGlobalSettings.RequestSoundSettingIngameField();
				if (isDecidedHappenQuestDialog && !isYesHappenQuestDialog)
				{
					MonoBehaviourSingleton<InGameManager>.I.disableHappenQuestIdList.Add(link_quest_id);
				}
				yield return (object)MonoBehaviourSingleton<TransitionManager>.I.Out(TransitionManager.TYPE.BLACK);
				if (enemy_loader != null)
				{
					enemy_loader.DeleteLoadedObjects();
					Object.DestroyImmediate(enemy_loader);
				}
				if (model != null)
				{
					Object.Destroy(model.get_gameObject());
				}
				int i = 0;
				for (int len = change_layer_list.Count; i < len; i++)
				{
					change_layer_list[i].set_layer(0);
				}
				foreach (GameObject item in hideObjectList)
				{
					if (item != null)
					{
						item.SetActive(true);
					}
				}
				if (progressEndType == PROGRESS_END_TYPE.NONE)
				{
					ViewUI(true);
					isGameProgressStop = false;
					if (MonoBehaviourSingleton<TargetMarkerManager>.IsValid())
					{
						MonoBehaviourSingleton<TargetMarkerManager>.I.showMarker = true;
					}
					if (MonoBehaviourSingleton<DropTargetMarkerManeger>.IsValid())
					{
						MonoBehaviourSingleton<DropTargetMarkerManeger>.I.active = true;
					}
					if (self != null)
					{
						self.hitOffFlag &= ~StageObject.HIT_OFF_FLAG.FORCE;
						if (self.controller != null)
						{
							self.controller.SetEnableControll(true, ControllerBase.DISABLE_FLAG.BATTLE_END);
						}
					}
				}
				if (MonoBehaviourSingleton<InGameCameraManager>.IsValid())
				{
					MonoBehaviourSingleton<InGameCameraManager>.I.OnHappenQuestDirection(false, null, null, null);
					MonoBehaviourSingleton<InGameCameraManager>.I.EndRadialBlurFilter(0f);
				}
				yield return (object)MonoBehaviourSingleton<TransitionManager>.I.In();
				isHappenQuestDirection = false;
				endHappenQuestDirection = false;
			}
		}
	}

	public void OnFieldQuestConfirm(bool is_yes)
	{
		isDecidedHappenQuestDialog = true;
		isYesHappenQuestDialog = is_yes;
	}

	public bool EndHappenQuestDirection()
	{
		if (!isHappenQuestDirection)
		{
			return false;
		}
		endHappenQuestDirection = true;
		return true;
	}

	public void CacheAudio(LoadingQueue load_queue)
	{
		int[] array = (int[])Enum.GetValues(typeof(AUDIO));
		int[] array2 = array;
		foreach (int se_id in array2)
		{
			load_queue.CacheSE(se_id, null);
		}
	}

	private void PlayAudio(AUDIO type)
	{
		SoundManager.PlayOneshotJingle((int)type, null, null);
	}

	public void PlayTimeBonusSE()
	{
		SoundManager.PlayOneShotUISE(40000158);
	}

	public List<int> GetMissionClearStatuses()
	{
		List<int> clearMissions = new List<int>();
		missionCheck.ForEach(delegate(MissionCheckBase mission)
		{
			clearMissions.Add(mission.IsMissionClear() ? 1 : 0);
		});
		return clearMissions;
	}

	private void OverwritePlayerRecorderForExplore(bool isInGame)
	{
		if (QuestManager.IsValidInGameExplore())
		{
			if (MonoBehaviourSingleton<CoopManager>.IsValid())
			{
				MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.ForEach(delegate(CoopClient x)
				{
					if (x.GetPlayer() != null)
					{
						MonoBehaviourSingleton<QuestManager>.I.UpdateExplorePlayerStatus(x);
					}
				});
			}
			List<ExplorePlayerStatus> explorePlayerStatusList = MonoBehaviourSingleton<QuestManager>.I.GetExplorePlayerStatusList();
			int bossHp = 0;
			ExploreBossStatus exploreBossStatus = MonoBehaviourSingleton<QuestManager>.I.GetExploreBossStatus();
			if (exploreBossStatus != null)
			{
				bossHp = exploreBossStatus.hpMax;
			}
			PartyModel.Party party = null;
			if (MonoBehaviourSingleton<PartyManager>.IsValid())
			{
				party = MonoBehaviourSingleton<PartyManager>.I.partyData;
			}
			MonoBehaviourSingleton<InGameRecorder>.I.SetRecordsForExplore(explorePlayerStatusList, party, bossHp, isInGame);
		}
	}

	public void AddDefeatCount(bool isBoss)
	{
		defeatCount++;
		if (isBoss)
		{
			defeatBossCount++;
		}
	}

	public void AddPartyDefeatBossCount()
	{
		partyDefeatBossCount++;
	}

	public void ClearDefeatCount()
	{
		defeatCount = 0;
		defeatBossCount = 0;
		partyDefeatBossCount = 0;
	}
}
