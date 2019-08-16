using System;
using System.Collections.Generic;
using UnityEngine;

public class ExploreStatus
{
	public class TraceInfo
	{
		public int mapId;

		public EXPLORE_HISTORY_TYPE historyType;

		public string playerName;

		public TraceInfo(int mapId, EXPLORE_HISTORY_TYPE type, string playerName)
		{
			this.mapId = mapId;
			historyType = type;
			this.playerName = playerName;
		}
	}

	private List<int> bossMapIdHistory = new List<int>();

	private List<TraceInfo> bossTraceMapIdHistory = new List<TraceInfo>();

	private List<ExplorePortalPoint> portals;

	private List<MissionCheckBase> missionCheck;

	private ExplorePlayerStatus[] playerStatuses = new ExplorePlayerStatus[8];

	private ExplorePlayerStatus selfPlayerStatus;

	public bool isHost
	{
		get;
		private set;
	}

	public PartyModel.ExploreInfo exploreInfo
	{
		get;
		private set;
	}

	public TraceInfo reservedTraceInfo
	{
		get;
		private set;
	}

	private FieldMapTable.PortalTableData lastUsePortal
	{
		get;
		set;
	}

	public ExploreBossStatus bossStatus
	{
		get;
		private set;
	}

	public bool isBossDead => bossStatus != null && bossStatus.isDead;

	public bool isEncountered
	{
		get;
		private set;
	}

	public float bossMoveRemainTime
	{
		get;
		private set;
	}

	public float hostDCRemainTime
	{
		get;
		private set;
	}

	public event Action onChangeExploreMemberList;

	public ExploreStatus(PartyModel.ExploreInfo exploreInfo, bool host)
	{
		this.exploreInfo = exploreInfo;
		isHost = host;
		portals = InitPortalPoint(exploreInfo.mapIds);
		hostDCRemainTime = 30f;
	}

	public void UpdateBossMap()
	{
		int count = GetEnabledPlayerStatusList().Count;
		for (int i = 0; i < count; i++)
		{
			int exploreMapId = MonoBehaviourSingleton<QuestManager>.I.GetExploreMapId(i);
			if (exploreMapId == exploreInfo.mapIds[exploreInfo.mapIds.Count - 1])
			{
				return;
			}
		}
		int currentBossMapId = GetCurrentBossMapId();
		if (currentBossMapId < 0)
		{
			int num = Random.Range(0, exploreInfo.mapIds.Count - 1);
			int item = exploreInfo.mapIds[num];
			if (num == 0 && exploreInfo.mapIds.Count > 1)
			{
				UpdateBossMap();
			}
			else
			{
				bossMapIdHistory.Add(item);
			}
			return;
		}
		List<FieldMapTable.PortalTableData> portalListByMapID = Singleton<FieldMapTable>.I.GetPortalListByMapID((uint)currentBossMapId);
		if (portalListByMapID == null)
		{
			return;
		}
		List<int> list = new List<int>();
		for (int j = 0; j < portalListByMapID.Count; j++)
		{
			if (portalListByMapID[j].banEnemy != 1)
			{
				int dstMapID = (int)portalListByMapID[j].dstMapID;
				if (dstMapID != exploreInfo.mapIds[0])
				{
					list.Add(dstMapID);
				}
			}
		}
		if (list.Count > 0)
		{
			int index = Random.Range(0, list.Count);
			int num2 = list[index];
			int beforeBossMapId = GetBeforeBossMapId();
			if (beforeBossMapId == num2 && list.Count > 1)
			{
				UpdateBossMap();
			}
			else
			{
				bossMapIdHistory.Add(num2);
			}
		}
	}

	public int GetCurrentBossMapId()
	{
		if (bossMapIdHistory == null)
		{
			return -1;
		}
		if (bossMapIdHistory.Count < 1)
		{
			return -1;
		}
		return bossMapIdHistory[bossMapIdHistory.Count - 1];
	}

	private int GetBeforeBossMapId()
	{
		if (bossMapIdHistory == null)
		{
			return -1;
		}
		if (bossMapIdHistory.Count < 2)
		{
			return -1;
		}
		return bossMapIdHistory[bossMapIdHistory.Count - 2];
	}

	public EXPLORE_HISTORY_TYPE GetHistoryTypeOfMap(int mapId)
	{
		if (bossMapIdHistory == null)
		{
			return EXPLORE_HISTORY_TYPE.NONE;
		}
		int? num = null;
		for (int i = 0; i < bossMapIdHistory.Count; i++)
		{
			if (bossMapIdHistory[i] == mapId)
			{
				num = i;
			}
		}
		if (!num.HasValue)
		{
			return EXPLORE_HISTORY_TYPE.NONE;
		}
		int count = bossMapIdHistory.Count;
		if (num == count - 1)
		{
			return EXPLORE_HISTORY_TYPE.CURRENT;
		}
		if (num == count - 2)
		{
			return EXPLORE_HISTORY_TYPE.LAST;
		}
		if (num == count - 3)
		{
			return EXPLORE_HISTORY_TYPE.SECOND_LAST;
		}
		return EXPLORE_HISTORY_TYPE.NONE;
	}

	public bool IsBossAppearMap(int mapId)
	{
		if (GetCurrentBossMapId() == mapId)
		{
			return true;
		}
		return false;
	}

	public void SyncBoss(Coop_Model_RoomSyncExploreBoss boss)
	{
		if (bossMapIdHistory.Count == 0)
		{
			bossMapIdHistory.Add(boss.mId);
		}
		else if (bossMapIdHistory[bossMapIdHistory.Count - 1] != boss.mId)
		{
			bossMapIdHistory.Add(boss.mId);
		}
		if (boss.hp >= 0)
		{
			if (bossStatus == null)
			{
				bossStatus = new ExploreBossStatus();
			}
			bossStatus.UpdateStatus(boss);
		}
	}

	public void SyncBossMap(Coop_Model_RoomSyncExploreBossMap boss)
	{
		if (bossMapIdHistory.Count == 0)
		{
			bossMapIdHistory.Add(boss.mId);
		}
		else if (bossMapIdHistory[bossMapIdHistory.Count - 1] != boss.mId)
		{
			bossMapIdHistory.Add(boss.mId);
		}
	}

	public void SetBossDead(Coop_Model_RoomExploreBossDead model)
	{
		if (bossStatus == null)
		{
			bossStatus = new ExploreBossStatus();
		}
		bossStatus.UpdateStatus(model);
	}

	public void SyncPortalPoint(Coop_Model_RoomSyncAllPortalPoint model)
	{
		for (int i = 0; i < model.ps.Count; i++)
		{
			Coop_Model_RoomSyncAllPortalPoint.PortalData portalData = model.ps[i];
			ExplorePortalPoint portalData2 = GetPortalData(portalData.id);
			if (portalData2 != null)
			{
				portalData2.UpdatePoint(portalData.pt);
				portalData2.UpdateUsedFlag(portalData.u);
				portalData2 = GetPortalData(portalData2.linkPortalId);
				if (portalData2 != null)
				{
					portalData2.UpdatePoint(portalData.pt);
					portalData2.UpdateUsedFlag(portalData.u);
				}
			}
		}
	}

	public void UpdateBossTraceMapIdHistory(int mapId, int lastCount, string playerName, bool reserve)
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
		TraceInfo traceInfo = new TraceInfo(mapId, type, playerName);
		bossTraceMapIdHistory.Add(traceInfo);
		if (reserve)
		{
			reservedTraceInfo = traceInfo;
		}
	}

	public TraceInfo[] GetTraceInfoHistory()
	{
		return bossTraceMapIdHistory.ToArray();
	}

	public void CompleteShowedTrace()
	{
		reservedTraceInfo = null;
	}

	public void UpdatePortalPoint(int portalId, int point, bool force = false)
	{
		ExplorePortalPoint portalData = GetPortalData(portalId);
		if (portalData != null)
		{
			portalData.UpdatePoint(point, force);
			GetPortalData(portalData.linkPortalId)?.UpdatePoint(point, force);
		}
	}

	public void UpdatePortalUsedFlag(int portalId)
	{
		ExplorePortalPoint portalData = GetPortalData(portalId);
		if (portalData != null)
		{
			portalData.UpdateUsedFlag(ExplorePortalPoint.USEDFLAG_PASSED);
			GetPortalData(portalData.linkPortalId)?.UpdateUsedFlag(ExplorePortalPoint.USEDFLAG_PASSED);
		}
	}

	public ExplorePortalPoint GetPortalData(int portalId)
	{
		return portals.Find((ExplorePortalPoint x) => x.portaiId == portalId);
	}

	public List<ExplorePortalPoint> GetAllPortalData()
	{
		return portals;
	}

	public List<ExplorePortalPoint> GetPortalDataFromMapId(int mapId)
	{
		return portals.FindAll(delegate(ExplorePortalPoint o)
		{
			if (o.portalData.dstMapID == (uint)mapId)
			{
				return true;
			}
			return false;
		});
	}

	public List<ExplorePortalPoint> GetPortalDataFromSrcMapId(int mapId)
	{
		return portals.FindAll(delegate(ExplorePortalPoint o)
		{
			if (o.portalData.srcMapID == (uint)mapId)
			{
				return true;
			}
			return false;
		});
	}

	public uint GetLastPortalId()
	{
		if (lastUsePortal != null)
		{
			return lastUsePortal.portalID;
		}
		return 0u;
	}

	public void UpdateLastPortal(FieldMapTable.PortalTableData portal)
	{
		lastUsePortal = portal;
		UpdatePortalUsedFlag((int)portal.portalID);
	}

	public ExplorePlayerStatus GetMyPlayerStatus()
	{
		return selfPlayerStatus;
	}

	private ExplorePlayerStatus GetPlayerStatus(CoopClient coopClient)
	{
		if (Object.op_Implicit(coopClient))
		{
			return GetPlayerStatus(coopClient.userId);
		}
		return null;
	}

	public ExplorePlayerStatus GetPlayerStatus(int userId)
	{
		for (int i = 0; i < 8; i++)
		{
			ExplorePlayerStatus explorePlayerStatus = playerStatuses[i];
			if (explorePlayerStatus != null && explorePlayerStatus.userId == userId)
			{
				return explorePlayerStatus;
			}
		}
		return null;
	}

	public void RemovePlayerStatus(CoopClient coopClient)
	{
		int num = 0;
		while (true)
		{
			if (num < 8)
			{
				ExplorePlayerStatus explorePlayerStatus = playerStatuses[num];
				if (explorePlayerStatus != null && explorePlayerStatus.userId == coopClient.userId)
				{
					break;
				}
				num++;
				continue;
			}
			return;
		}
		playerStatuses[num] = null;
		if (this.onChangeExploreMemberList != null)
		{
			this.onChangeExploreMemberList();
		}
	}

	public void ActivatePlayerStatus(CoopClient coopClient)
	{
		ExplorePlayerStatus explorePlayerStatus = GetPlayerStatus(coopClient);
		if (explorePlayerStatus == null)
		{
			bool flag = coopClient is CoopMyClient;
			explorePlayerStatus = new ExplorePlayerStatus(coopClient.userInfo, flag);
			playerStatuses[coopClient.slotIndex] = explorePlayerStatus;
			if (flag)
			{
				selfPlayerStatus = explorePlayerStatus;
			}
		}
		explorePlayerStatus.Activate(coopClient);
		if (this.onChangeExploreMemberList != null)
		{
			this.onChangeExploreMemberList();
		}
	}

	public void UpdatePlayerStatus(CoopClient coopClient, Coop_Model_RoomSyncPlayerStatus status)
	{
		GetPlayerStatus(coopClient)?.Sync(status);
	}

	public void UpdatePlayerStatus(CoopClient coopClient)
	{
		ExplorePlayerStatus playerStatus = GetPlayerStatus(coopClient);
		Player player = coopClient.GetPlayer();
		if (playerStatus != null && Object.op_Implicit(player))
		{
			playerStatus.SyncFromPlayer(player);
		}
	}

	public void UpdateTotalDamageToBoss(CoopClient coopClient, int total)
	{
		UpdateTotalDamageToBoss(coopClient.userId, total);
	}

	public void UpdateTotalDamageToBoss(int userId, int total)
	{
		GetPlayerStatus(userId)?.SyncTotalDamageToBoss(total);
	}

	public void UpdateBossMoveRemainTime(float time)
	{
		bossMoveRemainTime = time;
	}

	public void UpdateHostDCRemainTime(float time)
	{
		hostDCRemainTime = time;
	}

	public List<ExplorePlayerStatus> GetEnabledPlayerStatusList()
	{
		List<ExplorePlayerStatus> list = new List<ExplorePlayerStatus>();
		ExplorePlayerStatus[] array = playerStatuses;
		foreach (ExplorePlayerStatus explorePlayerStatus in array)
		{
			if (explorePlayerStatus != null)
			{
				list.Add(explorePlayerStatus);
			}
		}
		return list;
	}

	public void SetEncountered(int mapId)
	{
		isEncountered = true;
		if (GetCurrentBossMapId() != mapId)
		{
			bossMapIdHistory.Add(mapId);
		}
	}

	public void ResetMemberEncountered()
	{
		isEncountered = false;
	}

	public void UpdateBossStatus(Enemy boss)
	{
		if (bossStatus == null)
		{
			bossStatus = new ExploreBossStatus();
		}
		bossStatus.UpdateStatus(boss);
	}

	public void SetMissions(List<MissionCheckBase> missionCheck)
	{
		if (this.missionCheck == null)
		{
			this.missionCheck = missionCheck;
		}
	}

	public List<MissionCheckBase> GetMissions()
	{
		return missionCheck;
	}

	private List<ExplorePortalPoint> InitPortalPoint(List<int> mapIds)
	{
		List<ExplorePortalPoint> list = new List<ExplorePortalPoint>();
		int i = 0;
		for (int num = mapIds.Count - 1; i < num; i++)
		{
			List<FieldMapTable.PortalTableData> portalListByMapID = Singleton<FieldMapTable>.I.GetPortalListByMapID((uint)mapIds[i]);
			int j = 0;
			for (int count = portalListByMapID.Count; j < count; j++)
			{
				ExplorePortalPoint item = new ExplorePortalPoint(portalListByMapID[j]);
				list.Add(item);
			}
		}
		return list;
	}

	public void UpdatePassedPortal()
	{
		portals.ForEach(delegate(ExplorePortalPoint o)
		{
			if (o.passed)
			{
				o.UpdateUsedFlag(ExplorePortalPoint.USEDFLAG_OPENED);
			}
		});
	}
}
