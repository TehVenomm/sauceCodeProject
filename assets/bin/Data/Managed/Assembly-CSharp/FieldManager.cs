using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviourSingleton<FieldManager>
{
	public class FieldTransitionInfo
	{
		public uint portalID;

		public uint mapID;

		public float mapX;

		public float mapZ;

		public float mapDir;
	}

	private class CurrentFieldData
	{
		public FieldTransitionInfo fieldTransitionInfo = new FieldTransitionInfo();

		public FieldMapTable.FieldMapTableData mapData;

		public bool isValidBoss;

		public List<FieldMapPortalInfo> fieldPortalInfoList = new List<FieldMapPortalInfo>();

		public int portalPointToIndex = -1;
	}

	public const uint NUMBERTOP_MAPID_EVENT = 2u;

	public const uint NUMBERTOP_MAPID_QUEST = 3u;

	public const uint TUTORIAL_FIELD_ID_0 = 10000100u;

	public const uint TUTORIAL_FIELD_ID_1 = 10000101u;

	private int lastMapId;

	private CurrentFieldData current = new CurrentFieldData();

	private Vector3 cameraOffsetPortraitPos = Vector3.get_zero();

	private Quaternion cameraOffsetPortraitRot = Quaternion.get_identity();

	private Vector3 cameraOffsetLandscapePos = Vector3.get_zero();

	private Quaternion cameraOffsetLandscapeRot = Quaternion.get_identity();

	public List<int> fieldGatherPointIdList = new List<int>();

	public List<GatherGrowthInfo> fieldGatherGrowthList = new List<GatherGrowthInfo>();

	public FieldModel.Param fieldData
	{
		get;
		private set;
	}

	public Error matchingErrorCode
	{
		get;
		private set;
	}

	public string noticeText
	{
		get;
		private set;
	}

	public uint currentMapID => current.fieldTransitionInfo.mapID;

	public uint currentPortalID => current.fieldTransitionInfo.portalID;

	public FieldMapTable.FieldMapTableData currentMapData => current.mapData;

	public uint currentFieldBuffId => (!object.ReferenceEquals(current.mapData, null)) ? current.mapData.fieldBuffId : 0;

	public float currentStartMapX => current.fieldTransitionInfo.mapX;

	public float currentStartMapZ => current.fieldTransitionInfo.mapZ;

	public float currentStartMapDir => current.fieldTransitionInfo.mapDir;

	public bool currentIsValidBoss => current.isValidBoss;

	public List<FieldMapPortalInfo> currentFieldPortalInfoList => current.fieldPortalInfoList;

	public List<int> currentFieldPointIdList => fieldGatherPointIdList;

	public List<GatherGrowthInfo> currentFieldGatherGrowthList => fieldGatherGrowthList;

	public Vector3 cameraOffsetPos_Vec
	{
		get
		{
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid() && !MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait)
			{
				return cameraOffsetLandscapePos;
			}
			return cameraOffsetPortraitPos;
		}
	}

	public Quaternion cameraOffsetRot_Quat
	{
		get
		{
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid() && !MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait)
			{
				return cameraOffsetLandscapeRot;
			}
			return cameraOffsetPortraitRot;
		}
	}

	public bool useFastTravel
	{
		get;
		set;
	}

	public bool isTutorialField => currentPortalID == 10000100 || currentPortalID == 10000101;

	public FieldManager()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		fieldData = new FieldModel.Param();
		noticeText = string.Empty;
	}

	public static bool IsValidInField()
	{
		return MonoBehaviourSingleton<FieldManager>.IsValid() && MonoBehaviourSingleton<FieldManager>.I.fieldData.field != null;
	}

	public static bool IsValidInGame()
	{
		return MonoBehaviourSingleton<FieldManager>.IsValid() && MonoBehaviourSingleton<FieldManager>.I.currentMapID != 0;
	}

	public static bool IsValidInGameNoQuest()
	{
		return IsValidInGame() && !QuestManager.IsValidInGame();
	}

	public static bool IsValidInGameNoBoss()
	{
		if (IsValidInGame())
		{
			if (MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsExplore())
			{
				return MonoBehaviourSingleton<FieldManager>.I.currentMapID != MonoBehaviourSingleton<QuestManager>.I.GetCurrentMapId();
			}
			return !QuestManager.IsValidInGame() || MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestEnemyID() <= 0;
		}
		return false;
	}

	public static bool IsValidInTutorial()
	{
		return MonoBehaviourSingleton<FieldManager>.IsValid() && MonoBehaviourSingleton<FieldManager>.I.isTutorialField;
	}

	public bool IsEnabledStandby()
	{
		if (fieldData == null || fieldData.field == null)
		{
			return false;
		}
		if (fieldData.field.enableStandby == 0)
		{
			return false;
		}
		return true;
	}

	private string GetHappenStageName()
	{
		FieldTransitionInfo backTransitionInfo = MonoBehaviourSingleton<InGameManager>.I.backTransitionInfo;
		if (backTransitionInfo == null)
		{
			return string.Empty;
		}
		FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(backTransitionInfo.mapID);
		if (fieldMapData == null)
		{
			return string.Empty;
		}
		return fieldMapData.happenStageName;
	}

	public int GetHappenMapBGMID()
	{
		FieldTransitionInfo backTransitionInfo = MonoBehaviourSingleton<InGameManager>.I.backTransitionInfo;
		if (backTransitionInfo == null)
		{
			return 0;
		}
		return Singleton<FieldMapTable>.I.GetFieldMapData(backTransitionInfo.mapID)?.happenBgmID ?? 0;
	}

	public string GetCurrentMapStageName()
	{
		if (current.mapData == null)
		{
			return string.Empty;
		}
		if (MonoBehaviourSingleton<InGameManager>.I.isQuestHappen)
		{
			string happenStageName = GetHappenStageName();
			if (!string.IsNullOrEmpty(happenStageName))
			{
				return happenStageName;
			}
		}
		if (QuestManager.IsValidInGameExplore() && MonoBehaviourSingleton<QuestManager>.I.IsExploreBossMap())
		{
			string currentBossMapStageName = MonoBehaviourSingleton<QuestManager>.I.GetCurrentBossMapStageName();
			if (!string.IsNullOrEmpty(currentBossMapStageName))
			{
				return currentBossMapStageName;
			}
		}
		return current.mapData.stageName;
	}

	public int GetCurrentMapBGMID()
	{
		if (current.mapData == null)
		{
			return 0;
		}
		return current.mapData.bgmID;
	}

	public void ClearCurrentFieldData()
	{
		current = new CurrentFieldData();
	}

	public void SetCurrentFieldMapPortalID(uint portal_id)
	{
		FieldMapTable.PortalTableData portalData = Singleton<FieldMapTable>.I.GetPortalData(portal_id);
		if (portalData == null)
		{
			Log.Error(LOG.INGAME, "FieldManager.SetCurrentPortalID() portal data is none. portal_id : {0}", portal_id);
		}
		else
		{
			SetCurrentFieldMapID(portalData.dstMapID, portalData.dstX, portalData.dstZ, portalData.dstDir);
			current.fieldTransitionInfo.portalID = portal_id;
		}
	}

	public void SetCurrentFieldMapPortalID(uint portal_id, float map_x, float map_z, float map_dir)
	{
		SetCurrentFieldMapPortalID(portal_id);
		current.fieldTransitionInfo.mapX = map_x;
		current.fieldTransitionInfo.mapZ = map_z;
		current.fieldTransitionInfo.mapDir = map_dir;
	}

	public void SetCurrentFieldMapID(uint map_id, float map_x, float map_z, float map_dir)
	{
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		current = new CurrentFieldData();
		current.fieldTransitionInfo.portalID = 0u;
		current.fieldTransitionInfo.mapID = map_id;
		current.fieldTransitionInfo.mapX = map_x;
		current.fieldTransitionInfo.mapZ = map_z;
		current.fieldTransitionInfo.mapDir = map_dir;
		if (current.fieldTransitionInfo.mapID != 0)
		{
			current.mapData = Singleton<FieldMapTable>.I.GetFieldMapData(current.fieldTransitionInfo.mapID);
			cameraOffsetPortraitPos = current.mapData.camOffsetPortraitPos;
			cameraOffsetPortraitRot = Quaternion.Euler(current.mapData.camOffsetPortraitRot);
			cameraOffsetLandscapePos = current.mapData.camOffsetLandscapePos;
			cameraOffsetLandscapeRot = Quaternion.Euler(current.mapData.camOffsetLandscapeRot);
		}
		else
		{
			current.mapData = null;
			cameraOffsetPortraitPos = (cameraOffsetLandscapePos = Vector3.get_zero());
			cameraOffsetPortraitRot = (cameraOffsetLandscapeRot = Quaternion.get_identity());
		}
		current.isValidBoss = false;
		List<FieldMapTable.EnemyPopTableData> enemyPopList = Singleton<FieldMapTable>.I.GetEnemyPopList(currentMapID);
		if (enemyPopList != null)
		{
			int i = 0;
			for (int count = enemyPopList.Count; i < count; i++)
			{
				FieldMapTable.EnemyPopTableData enemyPopTableData = enemyPopList[i];
				if (enemyPopTableData != null && enemyPopTableData.bossFlag)
				{
					current.isValidBoss = true;
					break;
				}
			}
		}
		_SetFieldPortalInfoList(current.fieldTransitionInfo.mapID);
	}

	private void _SetFieldPortalInfoList(uint mapId)
	{
		int num = current.fieldPortalInfoList.Count;
		current.portalPointToIndex = -1;
		int num2 = 0;
		List<FieldMapTable.PortalTableData> portalListByMapID = Singleton<FieldMapTable>.I.GetPortalListByMapID(mapId, true);
		if (portalListByMapID != null)
		{
			num2 = portalListByMapID.Count;
			for (int i = 0; i < num2; i++)
			{
				FieldPortal fieldPortal = MonoBehaviourSingleton<WorldMapManager>.I.GetFieldPortal((int)portalListByMapID[i].portalID);
				if (QuestManager.IsValidExplore() && fieldPortal != null)
				{
					FieldPortal fieldPortal2 = new FieldPortal();
					fieldPortal2.pId = fieldPortal.pId;
					fieldPortal2.used = fieldPortal.used;
					fieldPortal2.point = fieldPortal.point;
					fieldPortal = fieldPortal2;
				}
				if (num <= i)
				{
					current.fieldPortalInfoList.Add(new FieldMapPortalInfo(portalListByMapID[i], fieldPortal));
					num++;
				}
				else
				{
					current.fieldPortalInfoList[i].SetPortalData(portalListByMapID[i], fieldPortal);
				}
				if (current.portalPointToIndex < 0 && current.fieldPortalInfoList[i].IsAddPortalPoint())
				{
					current.portalPointToIndex = i;
				}
			}
		}
		for (int j = num2; j < num; j++)
		{
			current.fieldPortalInfoList[j].Clear();
		}
	}

	public FieldMapPortalInfo GetPortalPointToPortalInfo()
	{
		if (current.portalPointToIndex < 0)
		{
			return null;
		}
		return current.fieldPortalInfoList[current.portalPointToIndex];
	}

	public bool AddPortalPointToPortalInfo(int addPoint)
	{
		FieldMapPortalInfo portalPointToPortalInfo = GetPortalPointToPortalInfo();
		if (portalPointToPortalInfo == null)
		{
			return false;
		}
		return AddPortalPointToPortalInfo(portalPointToPortalInfo, addPoint);
	}

	public bool AddPortalPointToPortalInfo(FieldMapPortalInfo portalInfo, int addPoint)
	{
		FieldPortal fieldPortal = portalInfo.fieldPortal;
		if (fieldPortal == null)
		{
			fieldPortal = MonoBehaviourSingleton<WorldMapManager>.I.GetFieldPortal((int)portalInfo.portalData.portalID);
			if (fieldPortal == null)
			{
				fieldPortal = new FieldPortal();
				fieldPortal.pId = (int)portalInfo.portalData.portalID;
				fieldPortal.used = false;
				fieldPortal.point = 0;
			}
			else if (QuestManager.IsValidExplore())
			{
				FieldPortal fieldPortal2 = new FieldPortal();
				fieldPortal2.pId = fieldPortal.pId;
				fieldPortal2.used = fieldPortal.used;
				fieldPortal2.point = fieldPortal.point;
				fieldPortal = fieldPortal2;
			}
			portalInfo.fieldPortal = fieldPortal;
		}
		fieldPortal.point += addPoint;
		if (!portalInfo.IsFull())
		{
			return false;
		}
		_ResetPortalPointToIndex(current.portalPointToIndex);
		if (QuestManager.IsValidExplore())
		{
			_ResetPortalPointToIndex(0);
		}
		return true;
	}

	public void ResetPortalPointToIndex()
	{
		_ResetPortalPointToIndex(0);
	}

	private int _ResetPortalPointToIndex(int startIndex = 0)
	{
		if (startIndex < 0)
		{
			startIndex = 0;
		}
		current.portalPointToIndex = -1;
		int i = startIndex;
		for (int count = current.fieldPortalInfoList.Count; i < count; i++)
		{
			if (current.fieldPortalInfoList[i].IsValid() && current.fieldPortalInfoList[i].IsAddPortalPoint())
			{
				current.portalPointToIndex = i;
				break;
			}
		}
		return current.portalPointToIndex;
	}

	public FieldMapPortalInfo GetPortalInfo(uint portalId)
	{
		int i = 0;
		for (int count = current.fieldPortalInfoList.Count; i < count; i++)
		{
			if (current.fieldPortalInfoList[i].IsValid() && current.fieldPortalInfoList[i].portalData.portalID == portalId)
			{
				return current.fieldPortalInfoList[i];
			}
		}
		return null;
	}

	public void InitPortalPointForExplore(ExploreStatus exploreStatus)
	{
		if (current != null && current.fieldPortalInfoList != null)
		{
			foreach (FieldMapPortalInfo fieldPortalInfo in current.fieldPortalInfoList)
			{
				if (fieldPortalInfo.IsValid())
				{
					FieldMapTable.PortalTableData portalData = Singleton<FieldMapTable>.I.GetPortalData(fieldPortalInfo.portalData.portalID);
					FieldPortal fieldPortal = new FieldPortal();
					fieldPortal.pId = (int)fieldPortalInfo.portalData.portalID;
					fieldPortal.used = false;
					fieldPortal.point = 0;
					ExplorePortalPoint portalData2 = exploreStatus.GetPortalData(fieldPortal.pId);
					if (portalData2 != null)
					{
						fieldPortal.point = portalData2.point;
						fieldPortal.used = (ExplorePortalPoint.USEDFLAG_CLOSED != portalData2.used);
					}
					fieldPortalInfo.fieldPortal = fieldPortal;
				}
			}
			_ResetPortalPointToIndex(0);
		}
	}

	public void Dirty()
	{
	}

	private void UpdateFieldData(FieldModel.Param field_data, bool is_user_collection_init = true)
	{
		fieldData = field_data;
		if (field_data.field != null && MonoBehaviourSingleton<QuestManager>.IsValid() && is_user_collection_init)
		{
			MonoBehaviourSingleton<QuestManager>.I.resultUserCollection.Init(field_data.field);
			MonoBehaviourSingleton<LoungeMatchingManager>.I.SendStartField();
		}
	}

	public string GetFieldId()
	{
		return (fieldData.field == null) ? "0" : fieldData.field.id;
	}

	public int GetMapId()
	{
		return (fieldData.field != null) ? fieldData.field.mapId : 0;
	}

	public CoopNetworkManager.ConnectData GetWebSockConnectData()
	{
		if (fieldData.field == null)
		{
			return null;
		}
		int num = -1;
		List<FieldModel.SlotInfo> slotInfos = fieldData.field.slotInfos;
		int i = 0;
		for (int count = slotInfos.Count; i < count; i++)
		{
			if (slotInfos[i].userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
			{
				num = i;
				break;
			}
		}
		if (num < 0)
		{
			return null;
		}
		CoopNetworkManager.ConnectData connectData = new CoopNetworkManager.ConnectData();
		connectData.path = fieldData.field.wsHost;
		connectData.ports = fieldData.field.wsPorts;
		connectData.fromId = fieldData.field.slotInfos[num].userId;
		connectData.ackPrefix = num;
		connectData.roomId = fieldData.field.id;
		connectData.token = fieldData.field.slotInfos[num].token;
		return connectData;
	}

	public static string GenerateToken()
	{
		return Guid.NewGuid().ToString().Replace("-", string.Empty);
	}

	public void SendMatching(int portalId, uint deliveryId, int _toUserId, Action<bool> call_back)
	{
		fieldData.field = null;
		FieldModel.RequestMatching requestMatching = new FieldModel.RequestMatching();
		requestMatching.portalId = portalId;
		requestMatching.dId = (int)deliveryId;
		requestMatching.token = GenerateToken();
		requestMatching.prevId = lastMapId;
		requestMatching.toUserId = _toUserId;
		fieldGatherPointIdList.Clear();
		fieldGatherGrowthList.Clear();
		Protocol.Send(FieldModel.RequestMatching.path, requestMatching, delegate(FieldModel ret)
		{
			bool obj = false;
			matchingErrorCode = ret.Error;
			if (ret.Error == Error.None)
			{
				obj = true;
				lastMapId = ret.result.field.mapId;
				if (ret.result.gather != null)
				{
					fieldGatherPointIdList = ret.result.gather;
				}
				if (ret.result.growth != null)
				{
					fieldGatherGrowthList = ret.result.growth;
				}
				if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.knockDownRaidBoss != null)
				{
					MonoBehaviourSingleton<UIManager>.I.knockDownRaidBoss.SetRaidBossHp(ret.result.raidBossHp);
				}
				noticeText = ret.result.noticeText;
				UpdateFieldData(ret.result, true);
				Dirty();
			}
			call_back(obj);
		}, string.Empty);
	}

	public void SendQuest(int questId, Action<bool> call_back)
	{
		fieldData.field = null;
		FieldModel.RequestQuest requestQuest = new FieldModel.RequestQuest();
		requestQuest.token = GenerateToken();
		requestQuest.qid = questId;
		Protocol.Send(FieldModel.RequestQuest.path, requestQuest, delegate(FieldModel ret)
		{
			bool obj = false;
			matchingErrorCode = ret.Error;
			if (ret.Error == Error.None)
			{
				obj = true;
				SetCurrentFieldMapID((uint)ret.result.field.mapId, 0f, 0f, 0f);
				UpdateFieldData(ret.result, true);
				Dirty();
			}
			call_back(obj);
		}, string.Empty);
	}

	public void SendParty(string party_id, bool is_owner, Action<bool> call_back)
	{
		if (is_owner)
		{
			SendCreate(party_id, call_back);
		}
		else
		{
			SendEnter(party_id, call_back);
		}
	}

	public void SendCreate(string party_id, Action<bool> call_back)
	{
		fieldData.field = null;
		FieldModel.RequestCreate requestCreate = new FieldModel.RequestCreate();
		requestCreate.partyId = party_id;
		requestCreate.token = GenerateToken();
		Protocol.Send(FieldModel.RequestCreate.path, requestCreate, delegate(FieldModel ret)
		{
			bool obj = false;
			matchingErrorCode = ret.Error;
			if (ret.Error == Error.None)
			{
				obj = true;
				SetCurrentFieldMapID((uint)ret.result.field.mapId, 0f, 0f, 0f);
				UpdateFieldData(ret.result, true);
				Dirty();
			}
			call_back(obj);
		}, string.Empty);
	}

	public void SendEnter(string party_id, Action<bool> call_back)
	{
		fieldData.field = null;
		FieldModel.RequestEnter requestEnter = new FieldModel.RequestEnter();
		requestEnter.partyId = party_id;
		requestEnter.token = GenerateToken();
		Protocol.Send(FieldModel.RequestEnter.path, requestEnter, delegate(FieldModel ret)
		{
			bool obj = false;
			matchingErrorCode = ret.Error;
			if (ret.Error == Error.None)
			{
				obj = true;
				SetCurrentFieldMapID((uint)ret.result.field.mapId, 0f, 0f, 0f);
				UpdateFieldData(ret.result, true);
				Dirty();
			}
			call_back(obj);
		}, string.Empty);
	}

	public void SendInfo(Action<bool> call_back)
	{
		if (fieldData.field == null)
		{
			call_back(false);
		}
		else
		{
			MonoBehaviourSingleton<NetworkManager>.I.Request(FieldModel.RequestInfo.path, delegate(FieldModel ret)
			{
				bool obj = false;
				if (ret.Error == Error.None)
				{
					obj = true;
					UpdateFieldData(ret.result, false);
					Dirty();
				}
				call_back(obj);
			}, string.Empty, string.Empty);
		}
	}

	public void SendLeave(bool toHome, bool retire, Action<bool> call_back)
	{
		if (fieldData.field == null)
		{
			call_back(false);
		}
		else
		{
			FieldLeaveModel.RequestSendForm requestSendForm = new FieldLeaveModel.RequestSendForm();
			requestSendForm.toHome = 0;
			requestSendForm.retire = 0;
			if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && MonoBehaviourSingleton<StageObjectManager>.I.self != null)
			{
				requestSendForm.actioncount = MonoBehaviourSingleton<StageObjectManager>.I.self.taskChecker.GetTaskCount();
				MonoBehaviourSingleton<StageObjectManager>.I.self.taskChecker.Clear();
			}
			if (toHome)
			{
				requestSendForm.toHome = 1;
			}
			if (retire)
			{
				requestSendForm.retire = 1;
			}
			Protocol.Send(FieldLeaveModel.URL, requestSendForm, delegate(FieldLeaveModel ret)
			{
				bool obj = false;
				if (ret.Error == Error.None)
				{
					obj = true;
					if (toHome)
					{
						lastMapId = 0;
					}
					fieldData.field = null;
					Dirty();
				}
				call_back(obj);
			}, string.Empty);
		}
	}

	public void SendFieldDrop(FieldDropModel.RequestSendForm send, Action<bool> call_back)
	{
		Protocol.Send(FieldDropModel.URL, send, delegate(FieldDropModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
			}
			call_back(obj);
		}, string.Empty);
	}

	public void SendFieldContinue(Action<bool, Error> call_back)
	{
		FieldContinueModel.RequestSendForm requestSendForm = new FieldContinueModel.RequestSendForm();
		requestSendForm.crystalCL = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.Crystal;
		Protocol.Send(FieldContinueModel.URL, requestSendForm, delegate(FieldContinueModel ret)
		{
			bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
			call_back.Invoke(flag, ret.Error);
		}, string.Empty);
	}

	public void SendFieldCharaList(Action<bool, List<FriendCharaInfo>> call_back)
	{
		Protocol.Send(FieldCharaListModel.URL, delegate(FieldCharaListModel ret)
		{
			bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
			call_back.Invoke(flag, ret.result);
		}, string.Empty);
	}

	public void SendFieldGather(int pointId, Action<bool, FieldGatherRewardList> call_back)
	{
		FieldGatherModel.RequestSendForm requestSendForm = new FieldGatherModel.RequestSendForm();
		requestSendForm.pId = pointId;
		Protocol.Send(FieldGatherModel.URL, requestSendForm, delegate(FieldGatherModel ret)
		{
			bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
			call_back.Invoke(flag, ret.result.reward);
		}, string.Empty);
	}

	public void SendFieldGatherGimmick(int lotId, int time, int isPop, Action<bool, FieldGatherRewardList> call_back)
	{
		FieldFishModel.RequestSendForm requestSendForm = new FieldFishModel.RequestSendForm();
		requestSendForm.lotId = lotId;
		requestSendForm.time = time;
		requestSendForm.isPop = isPop;
		Protocol.Send(FieldFishModel.URL, requestSendForm, delegate(FieldFishModel ret)
		{
			bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
			call_back.Invoke(flag, ret.result.reward);
		}, string.Empty);
	}

	public void SendFieldQuestOpenPortal(int portalId, Action<bool, Error> call_back)
	{
		FieldQuestOpenPortalModel.RequestSendForm requestSendForm = new FieldQuestOpenPortalModel.RequestSendForm();
		requestSendForm.portalId = portalId;
		Protocol.Send(FieldQuestOpenPortalModel.URL, requestSendForm, delegate(FieldQuestOpenPortalModel ret)
		{
			bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
			call_back.Invoke(flag, ret.Error);
		}, string.Empty);
	}

	public void SendFieldQuestMapChange(int mapId, Action<bool, Error> call_back)
	{
		FieldQuestMapChangeModel.RequestSendForm requestSendForm = new FieldQuestMapChangeModel.RequestSendForm();
		requestSendForm.mapId = mapId;
		fieldGatherPointIdList.Clear();
		fieldGatherGrowthList.Clear();
		Protocol.Send(FieldQuestMapChangeModel.URL, requestSendForm, delegate(FieldQuestMapChangeModel ret)
		{
			bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
			if (flag && ret.result.gather != null)
			{
				fieldGatherPointIdList = ret.result.gather;
				if (ret.result.growth != null)
				{
					fieldGatherGrowthList = ret.result.growth;
				}
				if (MonoBehaviourSingleton<InGameProgress>.IsValid())
				{
					MonoBehaviourSingleton<InGameProgress>.I.CheckGatherPointList();
				}
			}
			call_back.Invoke(flag, ret.Error);
		}, string.Empty);
	}

	public void SendDebugSetBoost(int type, int value, string end, Action<bool> call_back)
	{
		DebugSetBoostModel.RequestSendForm requestSendForm = new DebugSetBoostModel.RequestSendForm();
		requestSendForm.type = type;
		requestSendForm.value = value;
		requestSendForm.end = end;
		Protocol.Send(DebugSetBoostModel.URL, requestSendForm, delegate(DebugSetBoostModel ret)
		{
			bool obj = ErrorCodeChecker.IsSuccess(ret.Error);
			MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_USER_STATUS);
			call_back(obj);
		}, string.Empty);
	}

	public void SendDebugAppearFieldGather(int pointId, string appear, Action<bool> call_back)
	{
		DebugAppearFieldGatherModel.RequestSendForm requestSendForm = new DebugAppearFieldGatherModel.RequestSendForm();
		requestSendForm.pId = pointId;
		requestSendForm.appear = appear;
		Protocol.Send(DebugAppearFieldGatherModel.URL, requestSendForm, delegate(DebugAppearFieldGatherModel ret)
		{
			bool obj = ErrorCodeChecker.IsSuccess(ret.Error);
			call_back(obj);
		}, string.Empty);
	}

	public static bool IsShowPortal(uint portalId)
	{
		FieldMapTable.PortalTableData portalData = Singleton<FieldMapTable>.I.GetPortalData(portalId);
		if (portalData == null)
		{
			return false;
		}
		return IsShowPortal(portalData);
	}

	public static bool IsShowPortal(FieldMapTable.PortalTableData portal)
	{
		if (portal.hideQuestId != 0 && MonoBehaviourSingleton<QuestManager>.I.IsClearQuest(portal.hideQuestId))
		{
			return false;
		}
		if (portal.showDeliveryId != 0 && !MonoBehaviourSingleton<DeliveryManager>.I.IsClearDelivery(portal.showDeliveryId))
		{
			return false;
		}
		return true;
	}

	public static bool IsOpenPortal(uint portalId)
	{
		FieldMapTable.PortalTableData portalData = Singleton<FieldMapTable>.I.GetPortalData(portalId);
		if (portalData == null)
		{
			return false;
		}
		return IsOpenPortal(portalData);
	}

	public static bool IsOpenPortal(FieldMapTable.PortalTableData portal)
	{
		if (portal.srcMapID != 0 && !MonoBehaviourSingleton<WorldMapManager>.I.IsTraveledMap((int)portal.srcMapID))
		{
			return false;
		}
		if (portal.dstMapID == 0 && portal.dstQuestID == 0)
		{
			return true;
		}
		if (MonoBehaviourSingleton<WorldMapManager>.I.IsTraveledPortal(portal))
		{
			return true;
		}
		if (!IsOpenPortalClearOrder(portal))
		{
			return false;
		}
		if (portal.portalPoint != 0)
		{
			int portalPoint = MonoBehaviourSingleton<WorldMapManager>.I.GetPortalPoint(portal.portalID);
			if (portalPoint < portal.portalPoint)
			{
				return false;
			}
		}
		return true;
	}

	public static bool IsOpenPortalClearOrder(FieldMapTable.PortalTableData portal)
	{
		if (portal.appearQuestId != 0 && !MonoBehaviourSingleton<QuestManager>.I.IsClearQuest(portal.appearQuestId))
		{
			return false;
		}
		if (portal.appearDeliveryId != 0 && !MonoBehaviourSingleton<DeliveryManager>.I.IsClearDelivery(portal.appearDeliveryId))
		{
			return false;
		}
		if (portal.appearRegionId != 0)
		{
			FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(portal.dstMapID);
			if (fieldMapData == null)
			{
				return false;
			}
			if (!MonoBehaviourSingleton<WorldMapManager>.I.releasedRegionIds.Contains((int)fieldMapData.regionId))
			{
				return false;
			}
		}
		else if (portal.travelMapId != 0 && !MonoBehaviourSingleton<WorldMapManager>.I.IsTraveledMap((int)portal.travelMapId))
		{
			return false;
		}
		return true;
	}

	public bool CanJumpToMap(uint mapId)
	{
		FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(mapId);
		if (fieldMapData == null)
		{
			return false;
		}
		return CanJumpToMap(fieldMapData);
	}

	public bool CanJumpToMap(FieldMapTable.FieldMapTableData map)
	{
		return IsOpenPortal(map.jumpPortalID);
	}

	public static bool IsToHardPortal(uint portalId)
	{
		FieldMapTable.PortalTableData portalData = Singleton<FieldMapTable>.I.GetPortalData(portalId);
		if (portalData == null)
		{
			return false;
		}
		return IsToHardPortal(portalData);
	}

	public static bool IsToHardPortal(FieldMapTable.PortalTableData portal)
	{
		if (portal.dstMapID != 0)
		{
			FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(portal.dstMapID);
			if (fieldMapData != null)
			{
				return fieldMapData.fieldMode == DIFFICULTY_MODE.HARD;
			}
		}
		return false;
	}

	public static bool IsEventMap(uint mapId)
	{
		if (Singleton<FieldMapTable>.IsValid())
		{
			return IsEventMap(Singleton<FieldMapTable>.I.GetFieldMapData(mapId));
		}
		return false;
	}

	public static bool IsEventMap(FieldMapTable.FieldMapTableData map)
	{
		if (map != null)
		{
			return map.mapID / 10000000u == 2;
		}
		return false;
	}

	public static bool IsQuestMap(uint mapId)
	{
		if (Singleton<FieldMapTable>.IsValid())
		{
			return IsQuestMap(Singleton<FieldMapTable>.I.GetFieldMapData(mapId));
		}
		return false;
	}

	public static bool IsQuestMap(FieldMapTable.FieldMapTableData map)
	{
		if (map != null)
		{
			return map.mapID / 10000000u == 3;
		}
		return false;
	}

	public static bool IsInWorldMap(uint mapId)
	{
		if (mapId == 0)
		{
			return true;
		}
		if (Singleton<FieldMapTable>.IsValid())
		{
			return IsInWorldMap(Singleton<FieldMapTable>.I.GetFieldMapData(mapId));
		}
		return false;
	}

	public static bool IsInWorldMap(FieldMapTable.FieldMapTableData map)
	{
		if (map != null)
		{
			return !IsEventMap(map) && !IsQuestMap(map);
		}
		return false;
	}

	public static bool HasWorldMap(uint mapId)
	{
		if (mapId == 0)
		{
			return true;
		}
		if (!Singleton<FieldMapTable>.IsValid())
		{
			return false;
		}
		return HasWorldMap(Singleton<FieldMapTable>.I.GetFieldMapData(mapId));
	}

	public static bool HasWorldMap(FieldMapTable.FieldMapTableData map)
	{
		if (map != null && Singleton<RegionTable>.IsValid())
		{
			RegionTable.Data[] data = Singleton<RegionTable>.I.GetData();
			RegionTable.Data data2 = Array.Find(data, (RegionTable.Data o) => o.regionId == map.regionId);
			return null != data2;
		}
		return false;
	}

	public static bool IsEventRegion(uint regionID)
	{
		if (!Singleton<RegionTable>.IsValid())
		{
			return false;
		}
		RegionTable.Data data = Singleton<RegionTable>.I.GetData(regionID);
		if (data == null)
		{
			return false;
		}
		return 0 < data.eventId;
	}

	public void OnDiff(BaseModelDiff.DiffFieldPortal diff)
	{
		if (!QuestManager.IsValidExplore())
		{
			bool flag = false;
			if (Utility.IsExist(diff.add))
			{
				int i = 0;
				for (int count = diff.add.Count; i < count; i++)
				{
					FieldMapPortalInfo fieldMapPortalInfo = current.fieldPortalInfoList.Find((FieldMapPortalInfo p) => p.IsValid() && p.portalData.portalID == diff.add[i].pId);
					if (fieldMapPortalInfo != null)
					{
						fieldMapPortalInfo.fieldPortal = diff.add[i];
						flag |= fieldMapPortalInfo.IsFull();
					}
				}
			}
			if (Utility.IsExist(diff.update))
			{
				int j = 0;
				for (int count2 = diff.update.Count; j < count2; j++)
				{
					FieldMapPortalInfo fieldMapPortalInfo2 = current.fieldPortalInfoList.Find((FieldMapPortalInfo p) => p.IsValid() && p.portalData.portalID == diff.update[j].pId);
					if (fieldMapPortalInfo2 != null)
					{
						if (fieldMapPortalInfo2.fieldPortal != null)
						{
							fieldMapPortalInfo2.fieldPortal.used = diff.update[j].used;
							fieldMapPortalInfo2.fieldPortal.point = diff.update[j].point;
						}
						else
						{
							fieldMapPortalInfo2.fieldPortal = MonoBehaviourSingleton<WorldMapManager>.I.GetFieldPortal(diff.update[j].pId);
						}
						flag |= fieldMapPortalInfo2.IsFull();
					}
				}
			}
			if (flag)
			{
				_ResetPortalPointToIndex(0);
			}
		}
	}

	public void OnDiff(BaseModelDiff.DiffFieldGather diff)
	{
		bool flag = false;
		if (Utility.IsExist(diff.add))
		{
			int i = 0;
			for (int count = diff.add.Count; i < count; i++)
			{
				if (!fieldGatherPointIdList.Contains(diff.add[i]))
				{
					fieldGatherPointIdList.Add(diff.add[i]);
					flag = true;
				}
			}
		}
		if (Utility.IsExist(diff.del))
		{
			int j = 0;
			for (int count2 = diff.del.Count; j < count2; j++)
			{
				fieldGatherPointIdList.Remove(diff.del[j]);
				flag = true;
			}
		}
		if (flag && MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			MonoBehaviourSingleton<InGameProgress>.I.CheckGatherPointList();
		}
	}

	public void OnDiff(BaseModelDiff.DiffFieldGatherGrowth diff)
	{
		bool flag = false;
		if (Utility.IsExist(diff.add))
		{
			int i = 0;
			for (int count = diff.add.Count; i < count; i++)
			{
				if (!fieldGatherGrowthList.Contains(diff.add[i]))
				{
					fieldGatherGrowthList.Add(diff.add[i]);
					flag = true;
				}
			}
		}
		if (Utility.IsExist(diff.del))
		{
			int j = 0;
			for (int count2 = diff.del.Count; j < count2; j++)
			{
				fieldGatherGrowthList.Remove(diff.del[j]);
				flag = true;
			}
		}
		if (flag && MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			MonoBehaviourSingleton<InGameProgress>.I.CheckGatherPointList();
		}
	}

	public void MatchingNotice()
	{
		if (!string.IsNullOrEmpty(noticeText))
		{
			if (MonoBehaviourSingleton<UIEnemyAnnounce>.IsValid())
			{
				MonoBehaviourSingleton<UIEnemyAnnounce>.I.RequestTextAnnounce(noticeText);
			}
			noticeText = string.Empty;
		}
	}
}
