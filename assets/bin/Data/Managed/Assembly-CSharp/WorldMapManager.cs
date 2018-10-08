using Network;
using System;
using System.Collections.Generic;

public class WorldMapManager : MonoBehaviourSingleton<WorldMapManager>
{
	public class TransferInfo
	{
		public int nextRegionId;

		public bool nextInGame;

		public TransferInfo(int regionId, bool directInGame)
		{
			nextRegionId = regionId;
			nextInGame = directInGame;
		}
	}

	private List<int> traveledMapList = new List<int>();

	private List<FieldPortal> fieldPortalList = new List<FieldPortal>();

	private bool displayQuestTargetMode;

	private int questTargetMapID;

	private int[] questTargetPortalIDs;

	private uint jumpPortalID;

	private bool firstSetTravelList = true;

	public TransferInfo transferInfo
	{
		get;
		set;
	}

	public int openNewFieldId
	{
		get;
		set;
	}

	public bool ignoreTutorial
	{
		get;
		set;
	}

	public int eventMapRegionID
	{
		get;
		set;
	}

	public int releaseCrystalNum
	{
		get;
		private set;
	}

	public List<int> releasedRegionIds
	{
		get;
		private set;
	}

	public void PushDisplayQuestTarget(int mapID, int[] portalIDs)
	{
		displayQuestTargetMode = true;
		questTargetMapID = mapID;
		questTargetPortalIDs = portalIDs;
	}

	public void PopDisplayQuestTarget(out int mapID, out int[] portalIDs)
	{
		mapID = questTargetMapID;
		portalIDs = questTargetPortalIDs;
		questTargetMapID = -1;
		questTargetPortalIDs = null;
		displayQuestTargetMode = false;
	}

	public bool isDisplayQuestTargetMode()
	{
		return displayQuestTargetMode;
	}

	public void SetReleasedRegion()
	{
		releasedRegionIds = MonoBehaviourSingleton<OnceManager>.I.result.region;
	}

	public void AddReleasedRegion(int regionId)
	{
		releasedRegionIds.Add(regionId);
	}

	public bool IsTraveledMap(int mapId)
	{
		if (QuestManager.IsValidInGameExplore())
		{
			return MonoBehaviourSingleton<QuestManager>.I.MapIsTraveldInExplore(mapId);
		}
		int num = traveledMapList.IndexOf(mapId);
		return num >= 0;
	}

	public uint[] GetOpenRegionIdList()
	{
		List<uint> list = new List<uint>();
		RegionTable.Data[] data = Singleton<RegionTable>.I.GetData();
		foreach (RegionTable.Data data2 in data)
		{
			if (!data2.HasStartAt() || !(data2.startAt > TimeManager.GetNow()))
			{
				FieldMapTable.FieldMapTableData[] fieldMapDataInRegion = Singleton<FieldMapTable>.I.GetFieldMapDataInRegion(data2.regionId);
				foreach (FieldMapTable.FieldMapTableData fieldMapTableData in fieldMapDataInRegion)
				{
					if (MonoBehaviourSingleton<FieldManager>.I.CanJumpToMap(fieldMapTableData))
					{
						list.Add(fieldMapTableData.regionId);
						break;
					}
				}
			}
		}
		return list.ToArray();
	}

	public uint[] GetOpenRegionIdListInWorldMap()
	{
		List<uint> list = new List<uint>();
		RegionTable.Data[] data = Singleton<RegionTable>.I.GetData();
		foreach (RegionTable.Data data2 in data)
		{
			if (100 > data2.regionId && (!data2.HasStartAt() || !(data2.startAt > TimeManager.GetNow())))
			{
				FieldMapTable.FieldMapTableData[] fieldMapDataInRegion = Singleton<FieldMapTable>.I.GetFieldMapDataInRegion(data2.regionId);
				foreach (FieldMapTable.FieldMapTableData fieldMapTableData in fieldMapDataInRegion)
				{
					if (MonoBehaviourSingleton<FieldManager>.I.CanJumpToMap(fieldMapTableData))
					{
						list.Add(fieldMapTableData.regionId);
						break;
					}
				}
			}
		}
		return list.ToArray();
	}

	public uint[] GetOpenRegionIdListInWorldMap(REGION_DIFFICULTY_TYPE type)
	{
		List<uint> list = new List<uint>();
		RegionTable.Data[] data = Singleton<RegionTable>.I.GetData();
		foreach (RegionTable.Data data2 in data)
		{
			if (100 > data2.regionId && type == data2.difficulty && (!data2.HasStartAt() || !(data2.startAt > TimeManager.GetNow())))
			{
				FieldMapTable.FieldMapTableData[] fieldMapDataInRegion = Singleton<FieldMapTable>.I.GetFieldMapDataInRegion(data2.regionId);
				foreach (FieldMapTable.FieldMapTableData fieldMapTableData in fieldMapDataInRegion)
				{
					if (MonoBehaviourSingleton<FieldManager>.I.CanJumpToMap(fieldMapTableData))
					{
						list.Add(fieldMapTableData.regionId);
						break;
					}
				}
			}
		}
		return list.ToArray();
	}

	public uint[] GetValidRegionIdListInWorldMap()
	{
		List<uint> list = new List<uint>();
		RegionTable.Data[] data = Singleton<RegionTable>.I.GetData();
		foreach (RegionTable.Data data2 in data)
		{
			if (100 > data2.regionId && (!data2.HasStartAt() || !(data2.startAt > TimeManager.GetNow())))
			{
				list.Add(data2.regionId);
			}
		}
		return list.ToArray();
	}

	public bool IsAllOpenedMap(int regionId)
	{
		RegionTable.Data data = Singleton<RegionTable>.I.GetData((uint)regionId);
		if (data == null)
		{
			return false;
		}
		FieldMapTable.FieldMapTableData[] fieldMapDataInRegion = Singleton<FieldMapTable>.I.GetFieldMapDataInRegion(data.regionId);
		foreach (FieldMapTable.FieldMapTableData map in fieldMapDataInRegion)
		{
			if (!MonoBehaviourSingleton<FieldManager>.I.CanJumpToMap(map))
			{
				return false;
			}
		}
		return true;
	}

	public bool IsOpenRegion(uint regionId)
	{
		FieldMapTable.FieldMapTableData[] fieldMapDataInRegion = Singleton<FieldMapTable>.I.GetFieldMapDataInRegion(regionId);
		foreach (FieldMapTable.FieldMapTableData map in fieldMapDataInRegion)
		{
			if (MonoBehaviourSingleton<FieldManager>.I.CanJumpToMap(map))
			{
				return true;
			}
		}
		return false;
	}

	public FieldPortal GetFieldPortal(int portalId)
	{
		return fieldPortalList.Find((FieldPortal p) => p.pId == portalId);
	}

	public bool IsTraveledPortal(uint portalId)
	{
		FieldMapTable.PortalTableData portalData = Singleton<FieldMapTable>.I.GetPortalData(portalId);
		if (portalData == null)
		{
			return false;
		}
		return IsTraveledPortal(portalData);
	}

	public bool IsTraveledPortal(FieldMapTable.PortalTableData portal)
	{
		if (QuestManager.IsValidInGameExplore())
		{
			return MonoBehaviourSingleton<QuestManager>.I.PortalIsUsedInExplore((int)portal.portalID);
		}
		if (portal.srcMapID != 0 && !IsTraveledMap((int)portal.srcMapID))
		{
			return false;
		}
		return GetFieldPortal((int)portal.portalID)?.used ?? false;
	}

	public int GetPortalPoint(uint portalId)
	{
		return GetFieldPortal((int)portalId)?.point ?? 0;
	}

	public void SetWorldMapTraveledList()
	{
		if (firstSetTravelList)
		{
			firstSetTravelList = false;
			OnceTraveledListModel.Param traveledlist = MonoBehaviourSingleton<OnceManager>.I.result.traveledlist;
			traveledMapList = traveledlist.travel;
			fieldPortalList = traveledlist.portal;
		}
	}

	public void SendDebugSetTraveled(int mapId, int cnt, Action<bool> call_back)
	{
		DebugSetTraveledModel.RequestSendForm requestSendForm = new DebugSetTraveledModel.RequestSendForm();
		requestSendForm.mapId = mapId;
		requestSendForm.cnt = cnt;
		Protocol.Send(DebugSetTraveledModel.URL, requestSendForm, delegate(DebugSetTraveledModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
			}
			call_back(obj);
		}, string.Empty);
	}

	public void SendDebugUsedPortal(int portalId, int used, Action<bool> call_back)
	{
		DebugUsedPortalModel.RequestSendForm requestSendForm = new DebugUsedPortalModel.RequestSendForm();
		requestSendForm.pId = portalId;
		requestSendForm.used = used;
		Protocol.Send(DebugUsedPortalModel.URL, requestSendForm, delegate(DebugUsedPortalModel ret)
		{
			bool obj = ErrorCodeChecker.IsSuccess(ret.Error);
			call_back(obj);
		}, string.Empty);
	}

	public void SendDebugSetPortalPoint(int portalId, int point, Action<bool> call_back)
	{
		DebugSetPortalPointModel.RequestSendForm requestSendForm = new DebugSetPortalPointModel.RequestSendForm();
		requestSendForm.pId = portalId;
		requestSendForm.point = point;
		Protocol.Send(DebugSetPortalPointModel.URL, requestSendForm, delegate(DebugSetPortalPointModel ret)
		{
			bool obj = ErrorCodeChecker.IsSuccess(ret.Error);
			call_back(obj);
		}, string.Empty);
	}

	public void Dirty()
	{
	}

	public void OnDiff(BaseModelDiff.DiffTraveled diff)
	{
		bool flag = false;
		if (Utility.IsExist(diff.add))
		{
			uint[] openRegionIdListInWorldMap = GetOpenRegionIdListInWorldMap();
			traveledMapList.AddRange(diff.add);
			flag = true;
			if (diff.add.Contains(10010500))
			{
				GameSaveData.instance.happyTimeForRating = true;
			}
			List<uint> list = new List<uint>(GetOpenRegionIdListInWorldMap());
			for (int i = 0; i < openRegionIdListInWorldMap.Length; i++)
			{
				list.Remove(openRegionIdListInWorldMap[i]);
			}
			if (list.Contains(1u) || list.Contains(3u) || list.Contains(5u) || list.Contains(7u))
			{
				GameSaveData.instance.happyTimeForRating = true;
			}
		}
		if (flag)
		{
			Dirty();
		}
	}

	public void OnDiff(BaseModelDiff.DiffFieldPortal diff)
	{
		bool flag = false;
		if (Utility.IsExist(diff.add))
		{
			fieldPortalList.AddRange(diff.add);
			flag = true;
		}
		if (Utility.IsExist(diff.update))
		{
			diff.update.ForEach(delegate(FieldPortal portal)
			{
				WorldMapManager worldMapManager = this;
				FieldPortal fieldPortal = fieldPortalList.Find((FieldPortal f) => f.pId == portal.pId);
				if (fieldPortal != null)
				{
					fieldPortal.used = portal.used;
					fieldPortal.point = portal.point;
				}
			});
			flag = true;
		}
		if (flag)
		{
			Dirty();
		}
	}

	public void SetJumpPortalID(uint portalID)
	{
		jumpPortalID = portalID;
	}

	public uint GetJumpPortalID()
	{
		return jumpPortalID;
	}

	public static bool IsValidPortalIDs(int[] ids)
	{
		if (ids == null)
		{
			return false;
		}
		if (ids.Length == 0)
		{
			return false;
		}
		if (0 >= ids[0])
		{
			return false;
		}
		return true;
	}

	public void SendRegionCrystalNum(int regionId, Action<bool, string> call_back)
	{
		RegionCrystalNumModel.RequestSendForm requestSendForm = new RegionCrystalNumModel.RequestSendForm();
		requestSendForm.regionId = regionId;
		Protocol.Send(RegionCrystalNumModel.URL, requestSendForm, delegate(RegionCrystalNumModel ret)
		{
			bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
			string arg = string.Empty;
			if (flag)
			{
				releaseCrystalNum = ret.result.crystalNum;
				arg = ret.result.text;
			}
			call_back(flag, arg);
		}, string.Empty);
	}

	public void SendRegionOpen(int regionId, Action<bool> call_back)
	{
		RegionOpenModel.RequestSendForm requestSendForm = new RegionOpenModel.RequestSendForm();
		requestSendForm.crystalCL = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
		requestSendForm.regionId = regionId;
		requestSendForm.useCrystal = releaseCrystalNum;
		Protocol.Send(RegionOpenModel.URL, requestSendForm, delegate(RegionOpenModel ret)
		{
			bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
			if (flag)
			{
				releasedRegionIds.Add(regionId);
			}
			call_back(flag);
		}, string.Empty);
	}

	public bool IsShowedOpenRegion(int regionId)
	{
		RegionTable.Data data = Singleton<RegionTable>.I.GetData((uint)regionId);
		if (data.difficulty != 0)
		{
			return true;
		}
		if (data.regionId == 0)
		{
			return true;
		}
		List<int> showedOpenRegionIds = GameSaveData.instance.showedOpenRegionIds;
		if (showedOpenRegionIds.Contains(regionId))
		{
			return true;
		}
		FieldMapTable.FieldMapTableData[] fieldMapDataInRegion = Singleton<FieldMapTable>.I.GetFieldMapDataInRegion((uint)regionId);
		foreach (FieldMapTable.FieldMapTableData fieldMapTableData in fieldMapDataInRegion)
		{
			if (traveledMapList.Contains((int)fieldMapTableData.mapID))
			{
				GameSaveData.instance.AddShowedOpenRegionId((int)fieldMapTableData.regionId);
				if (fieldMapTableData.mapID != (uint)openNewFieldId)
				{
					return true;
				}
			}
		}
		return false;
	}

	public bool ExistRegionDirection()
	{
		uint[] openRegionIdListInWorldMap = GetOpenRegionIdListInWorldMap();
		uint[] array = openRegionIdListInWorldMap;
		foreach (uint num in array)
		{
			if (num != 0 && !IsShowedOpenRegion((int)num))
			{
				return true;
			}
		}
		return false;
	}

	public bool IsExistedWorld2()
	{
		uint[] validRegionIdListInWorldMap = GetValidRegionIdListInWorldMap();
		uint[] array = validRegionIdListInWorldMap;
		foreach (uint id in array)
		{
			RegionTable.Data data = Singleton<RegionTable>.I.GetData(id);
			if (data.regionId < 100 && data.worldId == 2)
			{
				return true;
			}
		}
		return false;
	}
}
