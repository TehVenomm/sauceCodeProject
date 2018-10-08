using System.Collections.Generic;
using UnityEngine;

public class CoopRoom : MonoBehaviour
{
	public enum ROOM_STATUS
	{
		NONE,
		STAGE,
		BATTLE
	}

	public const int MAX_TEAM_CLIENT = 4;

	private SpanTimer timeCheckSpan = new SpanTimer(30f);

	private SpanTimer exploreAliveSpan = new SpanTimer(14f);

	public ChatCoopConnection chatConnection;

	public ROOM_STATUS status
	{
		get;
		private set;
	}

	public List<FieldModel.SlotInfo> slotInfos
	{
		get;
		private set;
	}

	public CoopClientCollector clients
	{
		get;
		private set;
	}

	public CoopRoomPacketSender packetSender
	{
		get;
		private set;
	}

	public CoopRoomPacketReceiver packetReceiver
	{
		get;
		private set;
	}

	public bool isOwnerFirstClear
	{
		get;
		private set;
	}

	public bool isOwnerCleared
	{
		get;
		private set;
	}

	public bool forceRetire
	{
		get;
		private set;
	}

	public bool ownerRetire
	{
		get;
		set;
	}

	public bool isOfflinePlay
	{
		get;
		private set;
	}

	public int roomLeaveCnt
	{
		get;
		private set;
	}

	private void Awake()
	{
		clients = new CoopClientCollector();
		packetSender = base.gameObject.AddComponent<CoopRoomPacketSender>();
		packetReceiver = base.gameObject.AddComponent<CoopRoomPacketReceiver>();
	}

	private void Update()
	{
		packetReceiver.OnUpdate();
		if (QuestManager.IsValidInGameExplore() && MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameProgress>.I.IsStartTimer() && timeCheckSpan.IsReady())
		{
			MonoBehaviourSingleton<CoopNetworkManager>.I.RoomTimeCheck(0f);
		}
		if (QuestManager.IsValidInGameExplore() && exploreAliveSpan.IsReady() && MonoBehaviourSingleton<CoopManager>.I.coopMyClient.isPartyOwner)
		{
			packetSender.SendExploreAlive();
		}
	}

	private void Logd(string str, params object[] objs)
	{
		if (!Log.enabled)
		{
			return;
		}
	}

	public void Clear()
	{
		status = ROOM_STATUS.NONE;
		isOfflinePlay = false;
		isOwnerFirstClear = false;
		isOwnerCleared = false;
		forceRetire = false;
		ownerRetire = false;
		roomLeaveCnt = 0;
		chatConnection = null;
		DestroyAllClient();
	}

	public void OnStageChangeInterval()
	{
		SetStatus(ROOM_STATUS.STAGE);
		roomLeaveCnt = 0;
		clients.ForEach(delegate(CoopClient c)
		{
			c.OnStageChangeInterval();
		});
	}

	public void OnQuestSeriesInterval()
	{
		SetStatus(ROOM_STATUS.STAGE);
		clients.ForEach(delegate(CoopClient c)
		{
			c.OnQuestSeriesInterval();
		});
	}

	private void SetStatus(ROOM_STATUS st)
	{
		Logd("status {0} => {1}", status, st);
		status = st;
	}

	public bool IsActivate()
	{
		return status != ROOM_STATUS.NONE;
	}

	public bool IsStage()
	{
		return status >= ROOM_STATUS.STAGE;
	}

	public bool IsBattle()
	{
		return status >= ROOM_STATUS.BATTLE;
	}

	public void SetOwnerFirstClear(bool is_owner_first_clear)
	{
		isOwnerFirstClear = is_owner_first_clear;
		Logd("isOwnerFirstClear:{0}", isOwnerFirstClear);
	}

	public void SetOwnerCleared()
	{
		isOwnerCleared = true;
		Logd("owner is cleared!");
	}

	public void Activate(List<FieldModel.SlotInfo> slot_infos)
	{
		Deactivate();
		SetSlotInfos(slot_infos);
		Coop_Model_RegisterACK registerAck = MonoBehaviourSingleton<CoopNetworkManager>.I.registerAck;
		if (registerAck != null)
		{
			InitStage(registerAck.ids, registerAck.stgids, registerAck.stgidxs, registerAck.stghosts);
			InitClients(registerAck.ids, registerAck.stgids, registerAck.stgidxs, registerAck.stghosts);
			SetOwnerFirstClear(registerAck.of);
		}
		SetStatus(ROOM_STATUS.STAGE);
		MonoBehaviourSingleton<CoopManager>.I.coopMyClient.StageStart();
		if (isOwnerFirstClear)
		{
			CoopClient x = clients.FindPartyOwner();
			if ((Object)x == (Object)null)
			{
				Logd("Activate... party owner not found.");
				MonoBehaviourSingleton<CoopNetworkManager>.I.Close(1000, "Bye!", null);
			}
		}
	}

	public void Deactivate()
	{
		Clear();
	}

	public void SetSlotInfos(List<FieldModel.SlotInfo> slot_infos)
	{
		slotInfos = slot_infos;
		if (slotInfos != null)
		{
			int i = 0;
			for (int count = slotInfos.Count; i < count; i++)
			{
				FieldModel.SlotInfo slotInfo = slotInfos[i];
				CoopClient coopClient = clients.FindByClientId(slotInfo.userId);
				if ((Object)coopClient != (Object)null)
				{
					coopClient.Activate(slotInfo.userId, slotInfo.token, slotInfo.userInfo, i);
				}
			}
		}
	}

	private void InitClients(List<int> ids, List<int> stgids, List<int> stgidxs, List<bool> stghosts)
	{
		DestroyAllClient();
		int num = 0;
		int i = 0;
		for (int count = ids.Count; i < count; i++)
		{
			CoopClient coopClient = OnJoinClient(ids[i], stgids[i], stgidxs[i], stghosts[i], num);
			if (!coopClient.IsActivate())
			{
				num++;
			}
		}
	}

	public void DestroyClient(CoopClient client)
	{
		if (!((Object)client == (Object)null))
		{
			Logd("destory client:{0}", client.clientId);
			if (CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected())
			{
				MonoBehaviourSingleton<KtbWebSocket>.I.RemoveResendPackets(client.clientId);
			}
			clients.Remove(client);
			client.Clear();
			if (!(client is CoopMyClient))
			{
				Logd("object destory client");
				Object.Destroy(client.gameObject);
				client = null;
			}
		}
	}

	public void DestroyAllClient()
	{
		clients.ForEach(delegate(CoopClient c)
		{
			DestroyClient(c);
		});
	}

	public void DestroyAllGuestClient()
	{
		clients.ForEach(delegate(CoopClient c)
		{
			if (!(c is CoopMyClient))
			{
				DestroyClient(c);
			}
		});
	}

	private void InitStage(List<int> ids, List<int> stgids, List<int> stgidxs, List<bool> stghosts)
	{
		int id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
		int num = 0;
		int stage_idx = 0;
		int i = 0;
		for (int count = ids.Count; i < count; i++)
		{
			if (id == ids[i])
			{
				num = stgids[i];
				stage_idx = stgidxs[i];
				break;
			}
		}
		int host_client_id = 0;
		int j = 0;
		for (int count2 = ids.Count; j < count2; j++)
		{
			if (num == stgids[j] && stghosts[j])
			{
				host_client_id = ids[j];
				break;
			}
		}
		MonoBehaviourSingleton<CoopManager>.I.coopStage.OnInitStage(num, stage_idx, host_client_id);
	}

	private CoopClient OnJoinClient(int client_id, int stgid, int stgidx, bool stghost, int counter = 0)
	{
		CoopClient coopClient = null;
		coopClient = clients.FindByClientId(client_id);
		if ((Object)coopClient != (Object)null)
		{
			Logd("OnJoinClient: already join. client={0}", coopClient);
			return coopClient;
		}
		if (client_id == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
		{
			coopClient = MonoBehaviourSingleton<CoopManager>.I.coopMyClient;
			coopClient.Init(client_id);
		}
		else
		{
			coopClient = (CoopClient)Utility.CreateGameObjectAndComponent("CoopClient", base.transform, -1);
			coopClient.Init(client_id);
			MonoBehaviourSingleton<CoopManager>.I.coopMyClient.WelcomeClient(client_id);
			if (QuestManager.IsValidInGameExplore())
			{
				if (MonoBehaviourSingleton<CoopManager>.I.isStageHost)
				{
					MonoBehaviourSingleton<CoopManager>.I.coopRoom.packetSender.SendSyncAllPortalPoint(MonoBehaviourSingleton<QuestManager>.I.GetExploreStatus().GetAllPortalData(), client_id);
					MonoBehaviourSingleton<CoopManager>.I.coopRoom.packetSender.SendSyncExploreBoss(MonoBehaviourSingleton<QuestManager>.I.GetExploreStatus(), client_id);
				}
				if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && (Object)MonoBehaviourSingleton<StageObjectManager>.I.self != (Object)null)
				{
					MonoBehaviourSingleton<CoopManager>.I.coopRoom.packetSender.SendSyncPlayerStatus(MonoBehaviourSingleton<StageObjectManager>.I.self, client_id);
				}
			}
			else if (MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsDefenseBattle())
			{
				if (MonoBehaviourSingleton<CoopManager>.I.isStageHost && MonoBehaviourSingleton<InGameProgress>.IsValid())
				{
					MonoBehaviourSingleton<CoopManager>.I.coopRoom.packetSender.SendSyncDefenseBattle(MonoBehaviourSingleton<InGameProgress>.I.defenseBattleEndurance);
				}
			}
			else if (QuestManager.IsValidInGameWaveMatch(false) && MonoBehaviourSingleton<CoopManager>.I.isStageHost && !MonoBehaviourSingleton<InGameProgress>.IsValid())
			{
				goto IL_01d9;
			}
		}
		goto IL_01d9;
		IL_01d9:
		coopClient.SetStage(stgid, stgidx, stghost);
		int num = slotInfos.FindIndex((FieldModel.SlotInfo s) => s.userId == client_id);
		if (num >= 0)
		{
			FieldModel.SlotInfo slotInfo = slotInfos[num];
			coopClient.Activate(slotInfo.userId, slotInfo.token, slotInfo.userInfo, num);
		}
		else if (counter == 0)
		{
			CoopApp.UpdateField(null);
		}
		clients.Add(coopClient);
		if (MonoBehaviourSingleton<CoopManager>.I.coopStage.stageId == coopClient.stageId)
		{
			MonoBehaviourSingleton<CoopManager>.I.coopStage.OnJoinClient(coopClient);
		}
		return coopClient;
	}

	private void OnLeaveClient(CoopClient client, int stgid, int stghostid)
	{
		Logd("OnLeaveClient: leave client. status={0} clientId={1} userid={2} token={3}", client.status, client.clientId, client.userId, client.userToken);
		if (!client.isLeave)
		{
			if (MonoBehaviourSingleton<CoopManager>.I.coopMyClient.IsBattleEnd())
			{
				CoopClient.CLIENT_STATUS status = MonoBehaviourSingleton<CoopManager>.I.coopMyClient.status;
				Logd("OnLeaveClient: already my client is clear or leave({0}).", status);
			}
			else
			{
				client.OnRoomLeaved();
				roomLeaveCnt++;
				if (isOwnerFirstClear && client.isPartyOwner)
				{
					SwitchOfflinePlay();
				}
				else if (NeedsForceLeave())
				{
					SwitchOfflinePlay();
				}
				else
				{
					if (stgid == MonoBehaviourSingleton<CoopManager>.I.coopStage.stageId)
					{
						MonoBehaviourSingleton<CoopManager>.I.coopStage.OnLeaveClient(client, stghostid);
					}
					if (client is CoopMyClient)
					{
						SwitchOfflinePlay();
					}
					else
					{
						DestroyClient(client);
					}
				}
			}
		}
	}

	public void StartBattle()
	{
		SetStatus(ROOM_STATUS.BATTLE);
	}

	public bool IsValidBattleComplete()
	{
		if (!MonoBehaviourSingleton<KtbWebSocket>.I.IsConnected())
		{
			return true;
		}
		if (isOwnerFirstClear && !MonoBehaviourSingleton<CoopManager>.I.coopMyClient.isPartyOwner && !isOwnerCleared)
		{
			return false;
		}
		return true;
	}

	public void SwitchOfflinePlay()
	{
		if (isOfflinePlay)
		{
			Logd("already offline.");
		}
		else
		{
			isOfflinePlay = true;
			Logd("SwitchOfflinePlay.");
			if ((!MonoBehaviourSingleton<InGameProgress>.IsValid() || !MonoBehaviourSingleton<InGameProgress>.I.isEnding) && (!QuestManager.IsValidInGameExplore() || (MonoBehaviourSingleton<InGameProgress>.IsValid() && !MonoBehaviourSingleton<InGameProgress>.I.isEnding)))
			{
				if (CoopWebSocketSingleton<KtbWebSocket>.IsValidOpen())
				{
					MonoBehaviourSingleton<CoopNetworkManager>.I.Close(1000, "Bye!", delegate
					{
					});
				}
				if (isOwnerFirstClear && !MonoBehaviourSingleton<CoopManager>.I.coopMyClient.isPartyOwner && !MonoBehaviourSingleton<CoopManager>.I.coopStage.isQuestClose)
				{
					forceRetire = true;
				}
				else if (NeedsForceLeave() && !MonoBehaviourSingleton<CoopManager>.I.coopStage.isQuestClose)
				{
					forceRetire = true;
					if (PartyManager.IsValidInParty())
					{
						Protocol.Force(delegate
						{
							MonoBehaviourSingleton<PartyManager>.I.SendLeave(delegate(bool is_leave)
							{
								Logd("PartyLeave. {0}", is_leave);
							});
						});
					}
				}
				else
				{
					MonoBehaviourSingleton<CoopManager>.I.coopStage.OnSwitchOfflinePlay();
					if (PartyManager.IsValidInParty() && !InGameManager.IsReentryNotLeaveParty())
					{
						Protocol.Force(delegate
						{
							MonoBehaviourSingleton<PartyManager>.I.SendLeave(delegate(bool is_leave)
							{
								Logd("PartyLeave. {0}", is_leave);
							});
						});
					}
					DestroyAllGuestClient();
				}
			}
		}
	}

	public bool OnRecvRoomJoined(Coop_Model_RoomJoined model)
	{
		if (!IsActivate())
		{
			return true;
		}
		OnJoinClient(model.cid, model.stgid, model.stgidx, model.stghostid == model.cid, 0);
		return true;
	}

	public bool OnRecvRoomLeaved(Coop_Model_RoomLeaved model)
	{
		CoopClient coopClient = clients.FindByClientId(model.cid);
		if ((Object)coopClient == (Object)null || coopClient.userToken != model.token)
		{
			Logd("OnRecvRoomLeaved: client not found. clientId={0}, client={1}", model.cid, coopClient);
			return true;
		}
		OnLeaveClient(coopClient, model.stgid, model.stghostid);
		return true;
	}

	public bool OnRecvRoomStageChanged(Coop_Model_RoomStageChanged model)
	{
		if (!IsActivate())
		{
			return true;
		}
		CoopClient coopClient = clients.FindByClientId(model.cid);
		if ((Object)coopClient == (Object)null)
		{
			Logd("OnRecvRoomStageChanged: client not found. clientId={0}", model.cid);
			return true;
		}
		Logd("OnRecvRoomStageChanged: sid={0}, client={1}", model.sid, coopClient);
		coopClient.SetStage(model.stgid, model.stgidx, coopClient.clientId == model.stghostid);
		if (coopClient is CoopMyClient)
		{
			MonoBehaviourSingleton<CoopManager>.I.coopStage.OnInitStage(model.stgid, model.stgidx, model.stghostid);
			MonoBehaviourSingleton<CoopNetworkManager>.I.SetRegisterSID(model.sid);
		}
		else if (model.pstgid == MonoBehaviourSingleton<CoopManager>.I.coopStage.stageId)
		{
			MonoBehaviourSingleton<CoopManager>.I.coopStage.OnLeaveClient(coopClient, model.pstghostid);
		}
		else if (model.stgid == MonoBehaviourSingleton<CoopManager>.I.coopStage.stageId)
		{
			MonoBehaviourSingleton<CoopManager>.I.coopStage.OnJoinClient(coopClient);
		}
		clients.ForEach(delegate(CoopClient c)
		{
			if (c.stageId == model.pstgid)
			{
				c.SetStageHost(c.clientId == model.pstghostid);
			}
			if (c.stageId == model.stgid)
			{
				c.SetStageHost(c.clientId == model.stghostid);
			}
		});
		return true;
	}

	public bool OnRecvRoomStageRequested(Coop_Model_RoomStageRequested model)
	{
		CoopClient coopClient = clients.FindByClientId(model.cid);
		if ((Object)coopClient == (Object)null)
		{
			return true;
		}
		if (coopClient is CoopMyClient)
		{
			MonoBehaviourSingleton<CoopManager>.I.coopStage.OnRequested();
		}
		else
		{
			MonoBehaviourSingleton<CoopManager>.I.coopMyClient.WelcomeClient(model.cid);
			if (coopClient.stageId == MonoBehaviourSingleton<CoopManager>.I.coopStage.stageId)
			{
				MonoBehaviourSingleton<CoopManager>.I.coopStage.OnRequestedClient(coopClient);
			}
		}
		return true;
	}

	public bool OnRecvRoomStageHostChanged(Coop_Model_RoomStageHostChanged model)
	{
		int hostClientId = MonoBehaviourSingleton<CoopManager>.I.coopStage.hostClientId;
		if (model.stgid == MonoBehaviourSingleton<CoopManager>.I.coopStage.stageId)
		{
			if (model.stghostid != 0)
			{
				CoopStageObjectUtility.TransfarOwnerForClientObjects(hostClientId, model.stghostid);
			}
			MonoBehaviourSingleton<CoopManager>.I.coopStage.SetHostClient(model.stghostid);
		}
		clients.ForEach(delegate(CoopClient c)
		{
			if (c.stageId == model.stgid)
			{
				c.SetStageHost(c.clientId == model.stghostid);
			}
		});
		return true;
	}

	public bool OnRecvRoomTimeUpdate(Coop_Model_RoomTimeUpdate model)
	{
		Logd("OnRecvRoomTimeUpdate:{0}", model.elapsedSec);
		if (QuestManager.IsValidInGameExplore() && MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.isAlreadyBattleStarted && MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameProgress>.I.enableLimitTime)
		{
			MonoBehaviourSingleton<InGameProgress>.I.SetElapsedTime((float)model.elapsedSec);
		}
		return true;
	}

	public void OnRecvSyncAllPortalPoint(Coop_Model_RoomSyncAllPortalPoint model)
	{
		if (QuestManager.IsValidExplore())
		{
			MonoBehaviourSingleton<QuestManager>.I.SyncExplorePortalPoint(model);
			if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameProgress>.I.portalObjectList != null)
			{
				foreach (PortalObject portalObject in MonoBehaviourSingleton<InGameProgress>.I.portalObjectList)
				{
					portalObject.Initialize(portalObject.portalInfo);
				}
				MonoBehaviourSingleton<FieldManager>.I.InitPortalPointForExplore(MonoBehaviourSingleton<QuestManager>.I.GetExploreStatus());
			}
		}
	}

	public void OnRecvRoomUpdatePortalPoint(Coop_Model_RoomUpdatePortalPoint model)
	{
		MonoBehaviourSingleton<QuestManager>.I.UpdateExplorePortalPoint(model.pid, model.pt, model.x, model.z);
	}

	public void OnRecvSyncExploreBoss(Coop_Model_RoomSyncExploreBoss model)
	{
		if (MonoBehaviourSingleton<InGameManager>.IsValid() && (!MonoBehaviourSingleton<QuestManager>.I.IsExploreBossMap() || !MonoBehaviourSingleton<QuestManager>.I.IsBossAppearMap(model.mId)))
		{
			int bossMapId = (int)MonoBehaviourSingleton<QuestManager>.I.GetBossMapId();
			if (bossMapId != 0 && bossMapId != model.mId)
			{
				MonoBehaviourSingleton<QuestManager>.I.UpdatePassedPortal();
			}
		}
		MonoBehaviourSingleton<QuestManager>.I.SyncExploreBossStatus(model);
	}

	public void OnRecvSyncExploreBossMap(Coop_Model_RoomSyncExploreBossMap model)
	{
		MonoBehaviourSingleton<QuestManager>.I.SyncExploreBossMap(model);
	}

	public void OnRecvExploreBossDamage(int fromClientId, Coop_Model_RoomExploreBossDamage model)
	{
		CoopClient coopClient = clients.FindByClientId(fromClientId);
		if ((Object)coopClient != (Object)null)
		{
			MonoBehaviourSingleton<QuestManager>.I.UpdateExploreTotalDamageToBoss(coopClient, model.dmg);
		}
	}

	public void OnRecvExploreAlive()
	{
		if (MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			MonoBehaviourSingleton<InGameProgress>.I.ResetExploreHostDCTimer();
		}
		else if (QuestManager.IsValidInGameExplore())
		{
			MonoBehaviourSingleton<QuestManager>.I.UpdateExploreHostDCTime(30f);
		}
	}

	public void OnRecvExploreAliveRequest()
	{
		if (!((Object)packetSender == (Object)null) && MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.I.coopMyClient.isPartyOwner)
		{
			packetSender.SendExploreAlive();
		}
	}

	public bool OnRecvExploreBossDead(Coop_Model_RoomExploreBossDead model)
	{
		MonoBehaviourSingleton<QuestManager>.I.SetExploreBossDead(model);
		if (model.dmgs != null)
		{
			foreach (Coop_Model_RoomExploreBossDead.TotalDamage dmg in model.dmgs)
			{
				MonoBehaviourSingleton<QuestManager>.I.UpdateExploreTotalDamageToBoss(dmg.uid, dmg.dmg);
			}
		}
		if (MonoBehaviourSingleton<QuestManager>.I.IsExploreBossMap() && MonoBehaviourSingleton<CoopManager>.I.coopMyClient.IsBattleStart())
		{
			return true;
		}
		CoopStage coopStage = MonoBehaviourSingleton<CoopManager>.I.coopStage;
		if (coopStage.bossBreakIDLists == null)
		{
			coopStage.InitBossBreakIdList();
		}
		int index = 0;
		if (QuestManager.IsValidInGame())
		{
			index = (int)MonoBehaviourSingleton<QuestManager>.I.currentQuestSeriesIndex;
		}
		MonoBehaviourSingleton<CoopManager>.I.coopStage.bossBreakIDLists[index] = model.breakIds;
		if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			return false;
		}
		foreach (MissionCheckBase item in MonoBehaviourSingleton<InGameProgress>.I.missionCheck)
		{
			(item as MissionCheckDownCount)?.SetCount(model.downCount);
		}
		if (!MonoBehaviourSingleton<InGameProgress>.I.BattleComplete(false))
		{
			MonoBehaviourSingleton<CoopManager>.I.coopStage.SetQuestClose(true);
		}
		return true;
	}

	public void OnRecvNotifyEncounterBoss(int fromClientId, Coop_Model_RoomNotifyEncounterBoss model)
	{
		if (!QuestManager.IsValidInGameExplore() || !MonoBehaviourSingleton<QuestManager>.I.IsExploreBossMap())
		{
			if (!MonoBehaviourSingleton<QuestManager>.I.IsEncountered() && MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameProgress>.I.progressEndType == InGameProgress.PROGRESS_END_TYPE.NONE)
			{
				CoopClient coopClient = clients.FindByClientId(fromClientId);
				MonoBehaviourSingleton<QuestManager>.I.SetMemberEncounteredMap(model.mid);
				uint currentQuestID = MonoBehaviourSingleton<QuestManager>.I.currentQuestID;
				QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(currentQuestID);
				int mainEnemyID = questData.GetMainEnemyID();
				string enemyName = Singleton<EnemyTable>.I.GetEnemyName((uint)mainEnemyID);
				string text = StringTable.Format(STRING_CATEGORY.IN_GAME, 8000u, coopClient.GetPlayerName(), enemyName);
				UIInGamePopupDialog.PushOpen(text, false, 1.22f);
			}
			if (QuestManager.IsValidExplore())
			{
				MonoBehaviourSingleton<QuestManager>.I.UpdatePortalUsedFlag(model.pid);
			}
		}
	}

	public void OnRecvNotifyTraceBoss(int fromClientId, Coop_Model_RoomNotifyTraceBoss model)
	{
		if (MonoBehaviourSingleton<QuestManager>.IsValid())
		{
			ExploreStatus.TraceInfo[] bossTraceHistory = MonoBehaviourSingleton<QuestManager>.I.GetBossTraceHistory();
			if (bossTraceHistory == null || bossTraceHistory.Length <= 0 || bossTraceHistory[bossTraceHistory.Length - 1].mapId != model.mid)
			{
				bool reserve = false;
				CoopClient coopClient = clients.FindByClientId(fromClientId);
				string playerName = coopClient.GetPlayerName();
				if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameProgress>.I.progressEndType == InGameProgress.PROGRESS_END_TYPE.NONE)
				{
					int num = 0;
					num = ((model.lc != 0) ? 8004 : 8003);
					string text = StringTable.Format(STRING_CATEGORY.IN_GAME, (uint)num, playerName);
					UIInGamePopupDialog.PushOpen(text, false, 1.22f);
				}
				else if (QuestManager.IsValidExplore())
				{
					reserve = true;
				}
				if (QuestManager.IsValidExplore())
				{
					MonoBehaviourSingleton<QuestManager>.I.UpdateBossTraceHistory(model.mid, model.lc, playerName, reserve);
				}
			}
		}
	}

	public void OnRecvSyncPlayerStatus(int fromClientId, Coop_Model_RoomSyncPlayerStatus model)
	{
		if (QuestManager.IsValidExplore())
		{
			CoopClient coopClient = clients.FindByClientId(fromClientId);
			if (model.hp <= 0 && (bool)coopClient)
			{
				ExplorePlayerStatus explorePlayerStatus = MonoBehaviourSingleton<QuestManager>.I.GetExplorePlayerStatus(coopClient.userId);
				if (explorePlayerStatus != null && explorePlayerStatus.hp > 0 && MonoBehaviourSingleton<UIDeadAnnounce>.IsValid())
				{
					MonoBehaviourSingleton<UIDeadAnnounce>.I.Announce(UIDeadAnnounce.ANNOUNCE_TYPE.DEAD, coopClient.GetPlayerName());
				}
			}
			MonoBehaviourSingleton<QuestManager>.I.UpdateExplorePlayerStatus(coopClient, model);
		}
	}

	public void OnRecvChatStamp(int fromClientId, Coop_Model_RoomChatStamp model)
	{
		CoopClient coopClient = clients.FindByClientId(fromClientId);
		if (!((Object)coopClient == (Object)null))
		{
			Player player = coopClient.GetPlayer();
			if ((bool)player)
			{
				player.ChatSayStamp(model.stampId);
			}
			else if (QuestManager.IsValidInGameExplore())
			{
				ExplorePlayerStatus explorePlayerStatus = MonoBehaviourSingleton<QuestManager>.I.GetExplorePlayerStatus(model.userId);
				if (MonoBehaviourSingleton<UIInGameMessageBar>.IsValid() && MonoBehaviourSingleton<UIInGameMessageBar>.I.isActiveAndEnabled && explorePlayerStatus != null)
				{
					MonoBehaviourSingleton<UIInGameMessageBar>.I.Announce(explorePlayerStatus.userName, model.stampId);
				}
			}
			if (chatConnection != null)
			{
				if ((bool)player)
				{
					chatConnection.OnReceiveStamp(coopClient.userId, player.charaName, model.stampId);
				}
				else if ((Object)coopClient != (Object)null)
				{
					chatConnection.OnReceiveStamp(coopClient.userId, coopClient.GetPlayerName(), model.stampId);
				}
			}
		}
	}

	public void SendChatStamp(int stamp_id)
	{
		packetSender.SendChatStamp(stamp_id);
	}

	public void SetChatConnection(ChatCoopConnection chat_connection)
	{
		chatConnection = chat_connection;
	}

	public void OnRecvMoveField(Coop_Model_RoomMoveField model)
	{
		if (QuestManager.IsValidInGameExplore())
		{
			MonoBehaviourSingleton<QuestManager>.I.UpdatePortalUsedFlag(model.pid);
		}
	}

	public void SnedMoveField(int usePortalId)
	{
		packetSender.SendMoveField(usePortalId);
	}

	public void SendRushRequest()
	{
		packetSender.SendRushRequest();
	}

	public void OnRecvRushRequest(int fromClientId, Coop_Model_RushRequest model)
	{
		if (MonoBehaviourSingleton<CoopManager>.I.coopMyClient.isStageHost)
		{
			packetSender.SendRushRequested(fromClientId, model.requestRushIndex);
		}
	}

	public void OnRecvRushRequested(Coop_Model_RushRequested model)
	{
		MonoBehaviourSingleton<CoopManager>.I.coopStage.OnRecvRushRequested(model);
	}

	public bool NeedsForceLeave()
	{
		if (MonoBehaviourSingleton<QuestManager>.IsValid() && (MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestType() == QUEST_TYPE.GATE || MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestType() == QUEST_TYPE.DEFENSE) && MonoBehaviourSingleton<InGameManager>.I.currentJoinType == CoopClient.CLIENT_JOIN_TYPE.FROM_QUEST_LIST)
		{
			bool foundFromField = false;
			bool foundJoinTypeNone = false;
			clients.ForEach(delegate(CoopClient c)
			{
				if (!c.isLeave)
				{
					if (c.joinType == CoopClient.CLIENT_JOIN_TYPE.NONE)
					{
						foundJoinTypeNone = true;
					}
					else if (c.joinType == CoopClient.CLIENT_JOIN_TYPE.FROM_FIELD && !c.isBattleRetire)
					{
						foundFromField = true;
					}
				}
			});
			if (foundJoinTypeNone)
			{
				return false;
			}
			if (!foundFromField)
			{
				return true;
			}
		}
		return false;
	}

	public void OnRecvSyncDefenseBattle(Coop_Model_RoomSyncDefenseBattle model)
	{
		if (MonoBehaviourSingleton<QuestManager>.IsValid())
		{
			MonoBehaviourSingleton<QuestManager>.I.UpdateTotalDamageToEndurance(model.endurance);
		}
	}
}
