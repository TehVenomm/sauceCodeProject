using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoopApp : MonoBehaviourSingleton<CoopApp>
{
	protected override void Awake()
	{
		base.Awake();
		this.get_gameObject().AddComponent<CoopManager>();
		this.get_gameObject().AddComponent<KtbWebSocket>();
		this.get_gameObject().AddComponent<CoopNetworkManager>();
		this.get_gameObject().AddComponent<CoopOfflineManager>();
	}

	private static void Logd(string str, params object[] objs)
	{
		if (!Log.enabled)
		{
		}
	}

	public static void EnterQuestOnly(Action<bool> call_back = null)
	{
		QuestStart(call_back);
	}

	public static void EnterQuestOffline(Action<bool, bool, bool, bool> call_back = null)
	{
		StartCoopOffline(delegate(bool is_m, bool is_c, bool is_r)
		{
			QuestStart(delegate(bool is_s)
			{
				if (call_back != null)
				{
					call_back(is_m, is_c, is_r, is_s);
				}
			});
		});
	}

	public static void EnterQuestOfflineAssignedEquipment(AssignedEquipmentTable.AssignedEquipmentData assignedEquipmentData, CharaInfo charaInfo, Action<bool, bool, bool, bool> callBack = null)
	{
		StartCoopOffline(delegate(bool isMatching, bool isConnect, bool isRegist)
		{
			QuestStart(delegate(bool isStart)
			{
				if (isStart)
				{
					if (assignedEquipmentData != null && charaInfo != null)
					{
						MonoBehaviourSingleton<StatusManager>.I.SetAssignedEquipmentData(assignedEquipmentData, charaInfo);
					}
					MonoBehaviourSingleton<QuestManager>.I.StartTrial();
				}
				if (callBack != null)
				{
					callBack(isMatching, isConnect, isRegist, isStart);
				}
			});
		});
	}

	public static void EnterSeriesArenaQuestOffline(Action<bool, bool, bool, bool> callBack = null)
	{
		EnterQuestOffline(callBack);
	}

	public static void EnterArenaQuestOffline(Action<bool, bool, bool, bool> callBack = null)
	{
		QuestManager questMgr = MonoBehaviourSingleton<QuestManager>.I;
		MonoBehaviourSingleton<InGameManager>.I.SetArenaInfo(questMgr.currentArenaId);
		StartCoopOffline(delegate(bool isMatching, bool isConnect, bool isRegist)
		{
			ArenaStartModel.RequestSendForm requestData = new ArenaStartModel.RequestSendForm
			{
				aid = questMgr.currentArenaId,
				qid = (int)MonoBehaviourSingleton<InGameManager>.I.GetFirstArenaQuestId(),
				setNo = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo
			};
			questMgr.SetCurrentQuestID(MonoBehaviourSingleton<InGameManager>.I.GetFirstArenaQuestId());
			questMgr.SendArenaQuestStart(requestData, delegate(bool isStart)
			{
				Logd("QuestStarted: {0}: ", isStart);
				if (isStart)
				{
					uint currentMapId = MonoBehaviourSingleton<QuestManager>.I.GetCurrentMapId();
					if (MonoBehaviourSingleton<FieldManager>.I.currentMapID != currentMapId)
					{
						MonoBehaviourSingleton<FieldManager>.I.SetCurrentFieldMapID(currentMapId, 0f, 0f, 0f);
					}
					questMgr.resultUserCollection.AddSelf();
					MonoBehaviourSingleton<LoungeMatchingManager>.I.SendStartArena(questMgr.currentArenaId);
				}
				if (callBack != null)
				{
					callBack(isMatching, isConnect, isRegist, isStart);
				}
			});
		});
	}

	public static void EnterQuest(Action<bool, bool, bool, bool> call_back = null)
	{
		if (MonoBehaviourSingleton<CoopManager>.I.coopRoom.IsActivate())
		{
			Logd("already in quest.");
			if (call_back != null)
			{
				call_back(arg1: true, arg2: true, arg3: true, arg4: true);
			}
		}
		else
		{
			MatchingQuestField(delegate(bool is_m)
			{
				StartCoopAndQuestStart(is_m, isFromField: true, call_back);
			});
		}
	}

	public static void EnterPartyQuest(Action<bool, bool, bool, bool> call_back = null)
	{
		if (MonoBehaviourSingleton<CoopManager>.I.coopRoom.IsActivate())
		{
			Logd("already in party quest.");
			if (call_back != null)
			{
				call_back(arg1: true, arg2: true, arg3: true, QuestManager.IsValidInGame());
			}
		}
		else
		{
			MatchingPartyField(delegate(bool is_m)
			{
				StartCoopAndQuestStart(is_m, isFromField: false, call_back);
			});
		}
	}

	public static void EnterPartyField(Action<bool, bool, bool> call_back = null, bool is_reentry = false)
	{
		if (MonoBehaviourSingleton<CoopManager>.I.coopRoom.IsActivate())
		{
			Logd("already in party field.");
			if (call_back != null)
			{
				call_back(arg1: true, arg2: true, arg3: true);
			}
		}
		else
		{
			MatchingPartyField(delegate(bool is_m)
			{
				if (is_m)
				{
					StartCoop(delegate(bool is_c, bool is_r)
					{
						if (call_back != null)
						{
							call_back(is_m, is_c, is_r);
						}
					}, is_reentry);
				}
				else if (call_back != null)
				{
					call_back(is_m, arg2: false, arg3: false);
				}
			}, is_force_enter: true);
		}
	}

	public static void EnterField(uint portal_id, uint deliveryId, Action<bool, bool, bool> call_back = null)
	{
		EnterField(portal_id, deliveryId, 0, call_back);
	}

	public static void EnterField(uint portal_id, uint deliveryId, int _toUserId, Action<bool, bool, bool> call_back = null)
	{
		if (MonoBehaviourSingleton<CoopManager>.I.coopRoom.IsActivate())
		{
			Logd("already in field.");
			if (call_back != null)
			{
				call_back(arg1: true, arg2: true, arg3: true);
			}
		}
		else if (!MonoBehaviourSingleton<GameSceneManager>.IsValid() || MonoBehaviourSingleton<GameSceneManager>.I.CheckPortalAndOpenUpdateAppDialog(portal_id, check_dst_quest: false))
		{
			if (portal_id != MonoBehaviourSingleton<FieldManager>.I.currentPortalID)
			{
				MonoBehaviourSingleton<FieldManager>.I.SetCurrentFieldMapPortalID(portal_id);
			}
			MatchingField(deliveryId, _toUserId, delegate(bool is_m)
			{
				if (is_m)
				{
					StartCoop(delegate(bool is_c, bool is_r)
					{
						if (call_back != null)
						{
							call_back(is_m, is_c, is_r);
						}
					});
				}
				else if (call_back != null)
				{
					call_back(is_m, arg2: false, arg3: false);
				}
			});
		}
	}

	public static void EnterQuestRandomMatching(Action<bool, bool, bool, bool> call_back = null)
	{
		if (MonoBehaviourSingleton<CoopManager>.I.coopRoom.IsActivate())
		{
			Logd("already in quest.");
			if (call_back != null)
			{
				call_back(arg1: true, arg2: true, arg3: true, arg4: true);
			}
		}
		else
		{
			RandomMatchingQuestField(delegate(bool isSuccess)
			{
				StartCoopAndQuestStart(isSuccess, isFromField: true, call_back);
			});
		}
	}

	private static void StartCoopOffline(Action<bool, bool, bool> call_back = null)
	{
		if (CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected())
		{
			return;
		}
		StageObjectManager.CreatePlayerInfo createPlayerInfo = MonoBehaviourSingleton<StatusManager>.I.GetCreatePlayerInfo();
		if (createPlayerInfo != null)
		{
			CharaInfo chara_info = createPlayerInfo.charaInfo;
			if (MonoBehaviourSingleton<CoopOfflineManager>.IsValid())
			{
				MonoBehaviourSingleton<CoopOfflineManager>.I.Activate();
			}
			CoopNetworkManager.ConnectData conn_data = new CoopNetworkManager.ConnectData();
			MonoBehaviourSingleton<CoopNetworkManager>.I.Regist(conn_data, delegate(bool is_regist)
			{
				if (is_regist)
				{
					List<FieldModel.SlotInfo> slot_infos = new List<FieldModel.SlotInfo>
					{
						new FieldModel.SlotInfo
						{
							userId = chara_info.userId,
							userInfo = (chara_info as FriendCharaInfo)
						}
					};
					MonoBehaviourSingleton<CoopManager>.I.coopRoom.Activate(slot_infos);
				}
				if (call_back != null)
				{
					call_back(arg1: true, arg2: true, is_regist);
				}
			});
		}
	}

	private static void StartCoop(Action<bool, bool> call_back = null, bool is_reentry = false)
	{
		MonoBehaviourSingleton<CoopManager>.I.Clear();
		CoopNetworkManager.ConnectData webSockConnectData = MonoBehaviourSingleton<FieldManager>.I.GetWebSockConnectData();
		MonoBehaviourSingleton<CoopNetworkManager>.I.ConnectAndRegist(webSockConnectData, delegate(bool is_connect, bool is_regist)
		{
			if (is_regist)
			{
				FieldModel.Param fieldData = MonoBehaviourSingleton<FieldManager>.I.fieldData;
				MonoBehaviourSingleton<CoopManager>.I.coopRoom.Activate(fieldData.field.slotInfos);
				if (call_back != null)
				{
					call_back(is_connect, is_regist);
				}
			}
			else if (is_reentry)
			{
				if (call_back != null)
				{
					call_back(is_connect, is_regist);
				}
			}
			else
			{
				MonoBehaviourSingleton<CoopApp>.I.LeaveWithParty(delegate
				{
					if (call_back != null)
					{
						call_back(is_connect, is_regist);
					}
				});
			}
		});
	}

	private static void StartCoopAndQuestStart(bool is_m, bool isFromField, Action<bool, bool, bool, bool> call_back = null)
	{
		if (is_m)
		{
			if (PartyManager.IsValidInParty())
			{
				PartyModel.Party partyData = MonoBehaviourSingleton<PartyManager>.I.partyData;
				PartyModel.ExploreInfo explore = partyData.quest.explore;
				if (explore != null)
				{
					MonoBehaviourSingleton<QuestManager>.I.SetExploreInfo(explore);
					MonoBehaviourSingleton<QuestManager>.I.SetExploreStatus(new ExploreStatus(explore, host: true));
				}
				if (partyData.quest.rush != null)
				{
					MonoBehaviourSingleton<InGameManager>.I.SetRushInfo(partyData.quest.questId, partyData.quest.rush);
					MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID((uint)partyData.quest.rush.waves[0].questId);
				}
			}
			if (isFromField)
			{
				MonoBehaviourSingleton<InGameManager>.I.currentJoinType = CoopClient.CLIENT_JOIN_TYPE.FROM_FIELD;
			}
			else
			{
				MonoBehaviourSingleton<InGameManager>.I.currentJoinType = CoopClient.CLIENT_JOIN_TYPE.FROM_QUEST_LIST;
			}
			StartCoop(delegate(bool is_c, bool is_r)
			{
				if (is_r)
				{
					QuestStart(delegate(bool is_s)
					{
						if (call_back != null)
						{
							call_back(is_m, is_c, is_r, is_s);
						}
					});
				}
				else if (call_back != null)
				{
					call_back(is_m, is_c, is_r, arg4: false);
				}
			});
		}
		else if (call_back != null)
		{
			call_back(is_m, arg2: false, arg3: false, arg4: false);
		}
	}

	private static void MatchingField(uint _deliveryId, int _toUserId, Action<bool> call_back = null)
	{
		int currentPortalID = (int)MonoBehaviourSingleton<FieldManager>.I.currentPortalID;
		MonoBehaviourSingleton<FieldManager>.I.SendMatching(currentPortalID, _deliveryId, _toUserId, delegate(bool is_matching)
		{
			Logd("Matched:{0}", is_matching);
			if (call_back != null)
			{
				call_back(is_matching);
			}
		});
	}

	private static void MatchingQuestField(Action<bool> call_back = null)
	{
		int currentQuestID = (int)MonoBehaviourSingleton<QuestManager>.I.currentQuestID;
		MonoBehaviourSingleton<FieldManager>.I.SendQuest(currentQuestID, delegate(bool is_matching)
		{
			Logd("Matched:{0}", is_matching);
			if (call_back != null)
			{
				call_back(is_matching);
			}
		});
	}

	private static void RandomMatchingQuestField(Action<bool> call_back = null)
	{
		int quest_id = (int)MonoBehaviourSingleton<QuestManager>.I.currentQuestID;
		MonoBehaviourSingleton<PartyManager>.I.SendRandomMatching(quest_id, 0, isExplore: false, delegate(bool is_success, int maxRetryCount, bool isJoined, float waitTime)
		{
			Logd("Matched:{0}", is_success);
			if (is_success)
			{
				PartyManager.PartySetting setting = new PartyManager.PartySetting(is_lock: false, 0, 0);
				if (!isJoined)
				{
					MonoBehaviourSingleton<PartyManager>.I.SendCreate(quest_id, setting, delegate(bool is_success2)
					{
						if (is_success2)
						{
							MonoBehaviourSingleton<PartyManager>.I.SetPartySetting(setting);
						}
						MatchingPartyField(call_back);
					});
				}
				else
				{
					MonoBehaviourSingleton<PartyManager>.I.SetPartySetting(setting);
					MatchingPartyField(call_back);
				}
			}
			else
			{
				call_back.SafeInvoke(t: false);
			}
		});
	}

	private static void MatchingPartyField(Action<bool> call_back = null, bool is_force_enter = false)
	{
		if (!PartyManager.IsValidInParty())
		{
			if (call_back != null)
			{
				call_back(obj: false);
			}
			return;
		}
		string partyId = MonoBehaviourSingleton<PartyManager>.I.GetPartyId();
		bool is_owner = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id == MonoBehaviourSingleton<PartyManager>.I.GetOwnerUserId();
		if (is_force_enter)
		{
			is_owner = false;
		}
		MonoBehaviourSingleton<FieldManager>.I.SendParty(partyId, is_owner, delegate(bool is_matching)
		{
			Logd("Matched:{0}", is_matching);
			if (call_back != null)
			{
				call_back(is_matching);
			}
		});
	}

	public static void UpdateField(Action<bool> call_back = null)
	{
		MonoBehaviourSingleton<FieldManager>.I.SendInfo(delegate(bool is_get)
		{
			if (is_get)
			{
				FieldModel.Param fieldData = MonoBehaviourSingleton<FieldManager>.I.fieldData;
				MonoBehaviourSingleton<CoopManager>.I.coopRoom.SetSlotInfos(fieldData.field.slotInfos);
			}
			if (call_back != null)
			{
				call_back(is_get);
			}
		});
	}

	public static void QuestStart(Action<bool> call_back = null)
	{
		int eSetNo = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo;
		int questId = (int)MonoBehaviourSingleton<QuestManager>.I.currentQuestID;
		bool currentQuestIsFreeJoin = MonoBehaviourSingleton<QuestManager>.I.currentQuestIsFreeJoin;
		ExploreStatus exploreStatus = MonoBehaviourSingleton<QuestManager>.I.GetExploreStatus();
		if (MonoBehaviourSingleton<InGameManager>.I.IsRush())
		{
			PartyModel.Party partyData = MonoBehaviourSingleton<PartyManager>.I.partyData;
			questId = partyData.quest.questId;
		}
		MonoBehaviourSingleton<QuestManager>.I.SendQuestStart(questId, eSetNo, currentQuestIsFreeJoin, delegate(bool is_start)
		{
			Logd("QuestStarted:{0}", is_start);
			if (is_start)
			{
				uint num = MonoBehaviourSingleton<QuestManager>.I.GetCurrentMapId();
				if (MonoBehaviourSingleton<QuestManager>.I.IsExplore())
				{
					num = (uint)MonoBehaviourSingleton<QuestManager>.I.GetExploreStartMapId();
				}
				if (MonoBehaviourSingleton<FieldManager>.I.currentMapID != num)
				{
					MonoBehaviourSingleton<FieldManager>.I.SetCurrentFieldMapID(num, 0f, 0f, 0f);
				}
				MonoBehaviourSingleton<QuestManager>.I.resultUserCollection.AddSelf();
				if (MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestType() == QUEST_TYPE.ORDER && MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialQuestId != MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestId())
				{
					MonoBehaviourSingleton<QuestManager>.I.isBackGachaQuest = true;
				}
			}
			if (call_back != null)
			{
				call_back(is_start);
			}
		});
		if (exploreStatus != null)
		{
			MonoBehaviourSingleton<QuestManager>.I.SetExploreStatus(exploreStatus);
		}
		if (PartyManager.IsValidInParty())
		{
			PartyModel.Party partyData2 = MonoBehaviourSingleton<PartyManager>.I.partyData;
			PartyModel.ExploreInfo explore = partyData2.quest.explore;
			if (explore != null)
			{
				MonoBehaviourSingleton<QuestManager>.I.SetExploreInfo(explore);
			}
		}
	}

	public static void QuestComplete(Action<bool, Error> call_back = null)
	{
		if (!MonoBehaviourSingleton<QuestManager>.IsValid())
		{
			if (call_back != null)
			{
				call_back(arg1: false, Error.Unknown);
			}
			return;
		}
		List<List<int>> list = new List<List<int>>();
		float hpRate = 100f;
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			list = MonoBehaviourSingleton<CoopManager>.I.coopStage.bossBreakIDLists;
			hpRate = MonoBehaviourSingleton<CoopManager>.I.coopStage.bossStartHpDamageRate;
		}
		if (QuestManager.IsValidInGameExplore())
		{
			List<int> exploreBossBreakIdList = MonoBehaviourSingleton<QuestManager>.I.GetExploreBossBreakIdList();
			if (list == null)
			{
				MonoBehaviourSingleton<CoopManager>.I.coopStage.InitBossBreakIdList();
				list = MonoBehaviourSingleton<CoopManager>.I.coopStage.bossBreakIDLists;
			}
			if (list.Count == 0)
			{
				list.Add(exploreBossBreakIdList);
			}
			else
			{
				list[0] = exploreBossBreakIdList;
			}
			hpRate = 100f;
		}
		if (QuestManager.IsValidInGameWaveMatch())
		{
			List<int> list2 = new List<int>();
			list2.Add(0);
			if (list.Count > 0)
			{
				list[0] = list2;
			}
			else
			{
				list.Add(list2);
			}
		}
		List<int> missionClearStatuses = MonoBehaviourSingleton<InGameProgress>.I.GetMissionClearStatuses();
		List<int> memIds = null;
		if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
		{
			memIds = MonoBehaviourSingleton<QuestManager>.I.resultUserCollection.GetUserIdList(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
		}
		List<QuestCompleteModel.BattleUserLog> logs = null;
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			logs = MonoBehaviourSingleton<CoopManager>.I.coopStage.battleUserLog.list;
		}
		if (QuestManager.IsValidTrial())
		{
			MonoBehaviourSingleton<QuestManager>.I.SendQuestCompleteTrial(missionClearStatuses, delegate(bool is_comp, Error result)
			{
				Logd("Trial Completed:{0}", is_comp);
				if (call_back != null)
				{
					call_back(is_comp, result);
				}
			});
		}
		else
		{
			MonoBehaviourSingleton<QuestManager>.I.SendQuestComplete(list, missionClearStatuses, memIds, hpRate, logs, delegate(bool is_comp, Error result)
			{
				Logd("Quest Completed:{0}", is_comp);
				if (call_back != null)
				{
					call_back(is_comp, result);
				}
			});
		}
	}

	public static void ArenaComplete(Action<bool, Error> callBack = null)
	{
		if (!MonoBehaviourSingleton<QuestManager>.IsValid() && callBack != null)
		{
			callBack(arg1: false, Error.Unknown);
		}
		else
		{
			MonoBehaviourSingleton<QuestManager>.I.SendArenaComplete(callBack);
		}
	}

	public static void QuestRetire(bool is_timeout, Action<bool> call_back = null)
	{
		string empty = string.Empty;
		List<int> memIDs = null;
		if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
		{
			memIDs = MonoBehaviourSingleton<QuestManager>.I.resultUserCollection.GetUserIdList(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
		}
		List<QuestCompleteModel.BattleUserLog> logs = null;
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			logs = MonoBehaviourSingleton<CoopManager>.I.coopStage.battleUserLog.list;
		}
		MonoBehaviourSingleton<QuestManager>.I.SendQuestRetire(is_timeout, memIDs, empty, logs, call_back);
	}

	public static void ArenaRetire(bool isTimeout, Action<bool> callBack = null)
	{
		ArenaRetireModel.RequestSendForm requestSendForm = new ArenaRetireModel.RequestSendForm();
		requestSendForm.timeout = (isTimeout ? 1 : 0);
		if (MonoBehaviourSingleton<InGameManager>.IsValid())
		{
			requestSendForm.wave = MonoBehaviourSingleton<InGameManager>.I.GetCurrentArenaWaveNum();
		}
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			requestSendForm.logs = MonoBehaviourSingleton<CoopManager>.I.coopStage.battleUserLog.list;
		}
		if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
		{
			requestSendForm.enemyHp = MonoBehaviourSingleton<InGameRecorder>.I.GetTotalEnemyHP();
		}
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && Object.op_Implicit(MonoBehaviourSingleton<StageObjectManager>.I.self))
		{
			requestSendForm.actioncount = MonoBehaviourSingleton<StageObjectManager>.I.self.taskChecker.GetTaskCount();
			MonoBehaviourSingleton<StageObjectManager>.I.self.taskChecker.Clear();
		}
		MonoBehaviourSingleton<QuestManager>.I.SendArenaRetire(requestSendForm, callBack);
	}

	public void Leave(Action<bool> call_back = null, bool toHome = false, bool fieldRetire = false)
	{
		Logd("Leave. start");
		this.StartCoroutine(LeaveCoroutine(call_back, toHome, fieldRetire));
	}

	public void LeaveWithParty(Action<bool> call_back = null, bool toHome = false, bool fieldRetire = false)
	{
		Logd("LeaveWithParty. start");
		this.StartCoroutine(LeaveCoroutine(call_back, toHome, fieldRetire, isParty: true));
	}

	private IEnumerator LeaveCoroutine(Action<bool> call_back = null, bool toHome = false, bool fieldRetire = false, bool isParty = false)
	{
		bool is_success = true;
		if (FieldManager.IsValidInField())
		{
			bool is_leaved = false;
			bool wait = true;
			MonoBehaviourSingleton<FieldManager>.I.SendLeave(toHome, fieldRetire, delegate(bool is_leave)
			{
				wait = false;
				is_leaved = is_leave;
			});
			while (wait)
			{
				yield return null;
			}
			Logd("LeaveCoroutine. Field leaved:{0}", is_leaved);
			is_success = is_leaved;
		}
		if (isParty && PartyManager.IsValidInParty())
		{
			bool is_leaved2 = false;
			bool wait2 = true;
			MonoBehaviourSingleton<PartyManager>.I.SendLeave(delegate(bool is_leave)
			{
				wait2 = false;
				is_leaved2 = is_leave;
			});
			while (wait2)
			{
				yield return null;
			}
			Logd("LeaveCoroutine. Party leaved:{0}", is_leaved2);
		}
		if (MonoBehaviourSingleton<KtbWebSocket>.I.IsConnected())
		{
			bool wait3 = true;
			MonoBehaviourSingleton<CoopNetworkManager>.I.Close(1000, "Bye!", delegate
			{
				wait3 = false;
			});
			while (wait3)
			{
				yield return null;
			}
		}
		MonoBehaviourSingleton<CoopManager>.I.Clear();
		call_back?.Invoke(is_success);
	}

	private IEnumerator OnApplicationPause(bool paused)
	{
		if (!CoopWebSocketSingleton<KtbWebSocket>.IsValidOpen())
		{
			yield break;
		}
		Logd("OnApplicationPause. pause={0}, is_connect={1}", paused, CoopWebSocketSingleton<KtbWebSocket>.IsValidOpen());
		if (paused)
		{
			if (!MonoBehaviourSingleton<CoopNetworkManager>.IsValid())
			{
				yield break;
			}
			if (MonoBehaviourSingleton<FieldManager>.IsValid() && MonoBehaviourSingleton<FieldManager>.I.IsEnabledStandby())
			{
				MonoBehaviourSingleton<CoopNetworkManager>.I.Standby();
				yield break;
			}
			MonoBehaviourSingleton<CoopNetworkManager>.I.LoopBackRoomLeave(is_force: true);
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
			MonoBehaviourSingleton<KtbWebSocket>.I.Close(1000);
			yield break;
		}
		if (MonoBehaviourSingleton<CoopNetworkManager>.IsValid() && MonoBehaviourSingleton<FieldManager>.IsValid() && MonoBehaviourSingleton<FieldManager>.I.IsEnabledStandby())
		{
			MonoBehaviourSingleton<CoopNetworkManager>.I.Resume();
			if (MonoBehaviourSingleton<InGameProgress>.IsValid())
			{
				MonoBehaviourSingleton<CoopManager>.I.coopStage.packetSender.SendStageSyncTimeRequest();
			}
			if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && MonoBehaviourSingleton<StageObjectManager>.I.playerList != null)
			{
				List<StageObject> playerList = MonoBehaviourSingleton<StageObjectManager>.I.playerList;
				for (int i = 0; i < playerList.Count; i++)
				{
					Player player = playerList[i] as Player;
					if (player != null && player.rescueTime > 0f && !player.IsPrayed() && !player.isWaitingResurrectionHoming)
					{
						player.playerSender.SendDeadCountRequest(player.id);
					}
				}
			}
		}
		MonoBehaviourSingleton<KtbWebSocket>.I.ClearLastPacketReceivedTime();
	}
}
