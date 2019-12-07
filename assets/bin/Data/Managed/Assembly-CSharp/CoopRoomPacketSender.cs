using System.Collections.Generic;
using UnityEngine;

public class CoopRoomPacketSender : MonoBehaviour
{
	private CoopRoom coopRoom
	{
		get;
		set;
	}

	protected virtual void Awake()
	{
		coopRoom = base.gameObject.GetComponent<CoopRoom>();
	}

	protected virtual void Start()
	{
	}

	public void SendSyncAllPortalPoint(List<ExplorePortalPoint> portals, int toClientId)
	{
		Coop_Model_RoomSyncAllPortalPoint coop_Model_RoomSyncAllPortalPoint = new Coop_Model_RoomSyncAllPortalPoint();
		coop_Model_RoomSyncAllPortalPoint.id = 1001;
		coop_Model_RoomSyncAllPortalPoint.SetFromExplorePortalList(portals);
		MonoBehaviourSingleton<CoopNetworkManager>.I.SendTo(toClientId, coop_Model_RoomSyncAllPortalPoint);
	}

	public void SendUpdatePortalPoint(int portalId, int point, int x, int z)
	{
		Coop_Model_RoomUpdatePortalPoint coop_Model_RoomUpdatePortalPoint = new Coop_Model_RoomUpdatePortalPoint();
		coop_Model_RoomUpdatePortalPoint.id = 1001;
		coop_Model_RoomUpdatePortalPoint.pid = portalId;
		coop_Model_RoomUpdatePortalPoint.pt = point;
		coop_Model_RoomUpdatePortalPoint.x = x;
		coop_Model_RoomUpdatePortalPoint.z = z;
		MonoBehaviourSingleton<CoopNetworkManager>.I.SendBroadcast(coop_Model_RoomUpdatePortalPoint);
	}

	public void SendSyncExploreBoss(ExploreStatus explore, int toClientId = -1)
	{
		Coop_Model_RoomSyncExploreBoss coop_Model_RoomSyncExploreBoss = new Coop_Model_RoomSyncExploreBoss();
		coop_Model_RoomSyncExploreBoss.id = 1001;
		coop_Model_RoomSyncExploreBoss.mId = explore.GetCurrentBossMapId();
		if (explore.bossStatus != null)
		{
			coop_Model_RoomSyncExploreBoss.ceId = explore.bossStatus.coopEnemyId;
			coop_Model_RoomSyncExploreBoss.hp = explore.bossStatus.hp;
			coop_Model_RoomSyncExploreBoss.hpm = explore.bossStatus.hpMax;
			coop_Model_RoomSyncExploreBoss.bhp = explore.bossStatus.barrierHp;
			coop_Model_RoomSyncExploreBoss.shp = explore.bossStatus.shieldHp;
			coop_Model_RoomSyncExploreBoss.SetRegions(explore.bossStatus.regionWorks);
			coop_Model_RoomSyncExploreBoss.concussionTotal = explore.bossStatus.concussionTotal;
			coop_Model_RoomSyncExploreBoss.concussionMax = explore.bossStatus.concussionMax;
			coop_Model_RoomSyncExploreBoss.concussionExtend = explore.bossStatus.concussionExtend;
			coop_Model_RoomSyncExploreBoss.angid = explore.bossStatus.nowAngryId;
			coop_Model_RoomSyncExploreBoss.eangids = explore.bossStatus.execAngryIds;
			coop_Model_RoomSyncExploreBoss.isMM = explore.bossStatus.isMadMode;
			coop_Model_RoomSyncExploreBoss.deadReviveCount = explore.bossStatus.deadReviveCount;
			coop_Model_RoomSyncExploreBoss.recoveredHP = explore.bossStatus.deadReviveCount;
		}
		else
		{
			coop_Model_RoomSyncExploreBoss.hp = -1;
		}
		if (toClientId > 0)
		{
			MonoBehaviourSingleton<CoopNetworkManager>.I.SendTo(toClientId, coop_Model_RoomSyncExploreBoss);
		}
		else
		{
			MonoBehaviourSingleton<CoopNetworkManager>.I.SendBroadcast(coop_Model_RoomSyncExploreBoss);
		}
	}

	public void SendSyncExploreBossMap(int mapId, int toClientId = -1)
	{
		Coop_Model_RoomSyncExploreBossMap coop_Model_RoomSyncExploreBossMap = new Coop_Model_RoomSyncExploreBossMap();
		coop_Model_RoomSyncExploreBossMap.id = 1001;
		coop_Model_RoomSyncExploreBossMap.mId = mapId;
		if (toClientId > 0)
		{
			MonoBehaviourSingleton<CoopNetworkManager>.I.SendTo(toClientId, coop_Model_RoomSyncExploreBossMap);
		}
		else
		{
			MonoBehaviourSingleton<CoopNetworkManager>.I.SendBroadcast(coop_Model_RoomSyncExploreBossMap);
		}
	}

	public void SendExploreBossDamage(int totalDamage)
	{
		Coop_Model_RoomExploreBossDamage coop_Model_RoomExploreBossDamage = new Coop_Model_RoomExploreBossDamage();
		coop_Model_RoomExploreBossDamage.id = 1001;
		coop_Model_RoomExploreBossDamage.dmg = totalDamage;
		MonoBehaviourSingleton<CoopNetworkManager>.I.SendBroadcast(coop_Model_RoomExploreBossDamage, promise: false);
	}

	public void SendExploreBossDead(Enemy boss, List<ExplorePlayerStatus> statuses)
	{
		Coop_Model_RoomExploreBossDead coop_Model_RoomExploreBossDead = new Coop_Model_RoomExploreBossDead();
		coop_Model_RoomExploreBossDead.id = 1001;
		coop_Model_RoomExploreBossDead.downCount = boss.downCount;
		coop_Model_RoomExploreBossDead.concussionTotal = boss.concussionTotal;
		coop_Model_RoomExploreBossDead.concussionMax = boss.concussionMax;
		coop_Model_RoomExploreBossDead.concussionExtend = boss.concussionExtend;
		coop_Model_RoomExploreBossDead.breakIds = boss.GetBreakRegionIDList();
		if (statuses != null)
		{
			int i = 0;
			for (int count = statuses.Count; i < count; i++)
			{
				coop_Model_RoomExploreBossDead.AddTotalDamageFromExplorePlayerStatus(statuses[i]);
			}
		}
		MonoBehaviourSingleton<CoopNetworkManager>.I.SendBroadcast(coop_Model_RoomExploreBossDead);
	}

	public void SendExploreAlive()
	{
		Coop_Model_RoomExploreAlive coop_Model_RoomExploreAlive = new Coop_Model_RoomExploreAlive();
		coop_Model_RoomExploreAlive.id = 1001;
		MonoBehaviourSingleton<CoopNetworkManager>.I.SendBroadcast(coop_Model_RoomExploreAlive, promise: false);
	}

	public void SendExploreAliveRequest()
	{
		Coop_Model_RoomExploreAliveRequest coop_Model_RoomExploreAliveRequest = new Coop_Model_RoomExploreAliveRequest();
		coop_Model_RoomExploreAliveRequest.id = 1001;
		MonoBehaviourSingleton<CoopNetworkManager>.I.SendBroadcast(coop_Model_RoomExploreAliveRequest, promise: false);
	}

	public void SendNotifyEncounterBoss(int mapId, int portalId)
	{
		Coop_Model_RoomNotifyEncounterBoss coop_Model_RoomNotifyEncounterBoss = new Coop_Model_RoomNotifyEncounterBoss();
		coop_Model_RoomNotifyEncounterBoss.id = 1001;
		coop_Model_RoomNotifyEncounterBoss.mid = mapId;
		coop_Model_RoomNotifyEncounterBoss.pid = portalId;
		MonoBehaviourSingleton<CoopNetworkManager>.I.SendBroadcast(coop_Model_RoomNotifyEncounterBoss);
	}

	public void SendNotifyTraceBoss(int mapId, int lastCount)
	{
		Coop_Model_RoomNotifyTraceBoss coop_Model_RoomNotifyTraceBoss = new Coop_Model_RoomNotifyTraceBoss();
		coop_Model_RoomNotifyTraceBoss.id = 1001;
		coop_Model_RoomNotifyTraceBoss.mid = mapId;
		coop_Model_RoomNotifyTraceBoss.lc = lastCount;
		MonoBehaviourSingleton<CoopNetworkManager>.I.SendBroadcast(coop_Model_RoomNotifyTraceBoss);
	}

	public void SendSyncPlayerStatus(Self self, int toClientId = -1)
	{
		if (coopRoom.clients.Find((CoopClient x) => x.stageId != MonoBehaviourSingleton<CoopManager>.I.coopMyClient.stageId) == null)
		{
			return;
		}
		Coop_Model_RoomSyncPlayerStatus coop_Model_RoomSyncPlayerStatus = new Coop_Model_RoomSyncPlayerStatus();
		coop_Model_RoomSyncPlayerStatus.id = 1001;
		coop_Model_RoomSyncPlayerStatus.hp = self.hp;
		coop_Model_RoomSyncPlayerStatus.buff = self.buffParam.CreateSyncParamIfNeeded();
		coop_Model_RoomSyncPlayerStatus.wid = self.weaponData.eId;
		if (QuestManager.IsValidInGameExplore())
		{
			ExplorePlayerStatus myExplorePlayerStatus = MonoBehaviourSingleton<QuestManager>.I.GetMyExplorePlayerStatus();
			if (myExplorePlayerStatus != null)
			{
				coop_Model_RoomSyncPlayerStatus.SetExtraStatus(self, myExplorePlayerStatus.extraStatus);
			}
			else
			{
				coop_Model_RoomSyncPlayerStatus.SetExtraStatus(self, null);
			}
		}
		if (toClientId > 0)
		{
			MonoBehaviourSingleton<CoopNetworkManager>.I.SendTo(toClientId, coop_Model_RoomSyncPlayerStatus, promise: false);
		}
		else
		{
			MonoBehaviourSingleton<CoopNetworkManager>.I.SendBroadcast(coop_Model_RoomSyncPlayerStatus, promise: false);
		}
	}

	public void SendChatStamp(int stamp_id)
	{
		Coop_Model_RoomChatStamp coop_Model_RoomChatStamp = new Coop_Model_RoomChatStamp();
		coop_Model_RoomChatStamp.id = 1001;
		coop_Model_RoomChatStamp.userId = 0;
		coop_Model_RoomChatStamp.stampId = stamp_id;
		if (MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.userInfo != null)
		{
			coop_Model_RoomChatStamp.userId = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
		}
		MonoBehaviourSingleton<CoopNetworkManager>.I.SendBroadcast(coop_Model_RoomChatStamp, promise: false);
	}

	public void SendMoveField(int portalId)
	{
		Coop_Model_RoomMoveField coop_Model_RoomMoveField = new Coop_Model_RoomMoveField();
		coop_Model_RoomMoveField.id = 1001;
		coop_Model_RoomMoveField.pid = portalId;
		MonoBehaviourSingleton<CoopNetworkManager>.I.SendBroadcast(coop_Model_RoomMoveField);
	}

	public void SendRushRequest()
	{
		Coop_Model_RushRequest coop_Model_RushRequest = new Coop_Model_RushRequest();
		coop_Model_RushRequest.id = 1001;
		coop_Model_RushRequest.requestRushIndex = MonoBehaviourSingleton<InGameManager>.I.GetRushIndex();
		MonoBehaviourSingleton<CoopNetworkManager>.I.SendBroadcast(coop_Model_RushRequest);
	}

	public void SendRushRequested(int toClientId, int requestRushIndex)
	{
		Coop_Model_RushRequested coop_Model_RushRequested = new Coop_Model_RushRequested();
		coop_Model_RushRequested.id = 1001;
		coop_Model_RushRequested.currentWaveIndex = MonoBehaviourSingleton<InGameManager>.I.GetRushIndex();
		coop_Model_RushRequested.syncData = MonoBehaviourSingleton<InGameManager>.I.GetRushSyncData(requestRushIndex);
		MonoBehaviourSingleton<CoopNetworkManager>.I.SendTo(toClientId, coop_Model_RushRequested);
	}

	public void SendSyncDefenseBattle(float endurance)
	{
		Coop_Model_RoomSyncDefenseBattle coop_Model_RoomSyncDefenseBattle = new Coop_Model_RoomSyncDefenseBattle();
		coop_Model_RoomSyncDefenseBattle.id = 1001;
		coop_Model_RoomSyncDefenseBattle.endurance = endurance;
		MonoBehaviourSingleton<CoopNetworkManager>.I.SendBroadcast(coop_Model_RoomSyncDefenseBattle);
	}
}
