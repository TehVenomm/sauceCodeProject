using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoopApp : MonoBehaviourSingleton<CoopApp>
{
	protected override void Awake()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
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
			return;
		}
	}

	public static void EnterQuestOnly(Action<bool> call_back = null)
	{
		QuestStart(call_back);
	}

	public unsafe static void EnterQuestOffline(Action<bool, bool, bool, bool> call_back = null)
	{
		_003CEnterQuestOffline_003Ec__AnonStorey4C4 _003CEnterQuestOffline_003Ec__AnonStorey4C;
		StartCoopOffline(new Action<bool, bool, bool>((object)_003CEnterQuestOffline_003Ec__AnonStorey4C, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	public unsafe static void EnterQuestOfflineAssignedEquipment(AssignedEquipmentTable.AssignedEquipmentData assignedEquipmentData, CharaInfo charaInfo, Action<bool, bool, bool, bool> callBack = null)
	{
		_003CEnterQuestOfflineAssignedEquipment_003Ec__AnonStorey4C6 _003CEnterQuestOfflineAssignedEquipment_003Ec__AnonStorey4C;
		StartCoopOffline(new Action<bool, bool, bool>((object)_003CEnterQuestOfflineAssignedEquipment_003Ec__AnonStorey4C, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	public unsafe static void EnterArenaQuestOffline(Action<bool, bool, bool, bool> callBack = null)
	{
		QuestManager questMgr = MonoBehaviourSingleton<QuestManager>.I;
		MonoBehaviourSingleton<InGameManager>.I.SetArenaInfo(questMgr.currentArenaId);
		_003CEnterArenaQuestOffline_003Ec__AnonStorey4C8 _003CEnterArenaQuestOffline_003Ec__AnonStorey4C;
		StartCoopOffline(new Action<bool, bool, bool>((object)_003CEnterArenaQuestOffline_003Ec__AnonStorey4C, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	public static void EnterQuest(Action<bool, bool, bool, bool> call_back = null)
	{
		if (MonoBehaviourSingleton<CoopManager>.I.coopRoom.IsActivate())
		{
			Logd("already in quest.");
			if (call_back != null)
			{
				call_back.Invoke(true, true, true, true);
			}
		}
		else
		{
			MatchingQuestField(delegate(bool is_m)
			{
				StartCoopAndQuestStart(is_m, true, call_back);
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
				call_back.Invoke(true, true, true, QuestManager.IsValidInGame());
			}
		}
		else
		{
			MatchingPartyField(delegate(bool is_m)
			{
				StartCoopAndQuestStart(is_m, false, call_back);
			}, false);
		}
	}

	public unsafe static void EnterPartyField(Action<bool, bool, bool> call_back = null, bool is_reentry = false)
	{
		if (MonoBehaviourSingleton<CoopManager>.I.coopRoom.IsActivate())
		{
			Logd("already in party field.");
			if (call_back != null)
			{
				call_back.Invoke(true, true, true);
			}
		}
		else
		{
			MatchingPartyField(delegate(bool is_m)
			{
				if (is_m)
				{
					_003CEnterPartyField_003Ec__AnonStorey4CC._003CEnterPartyField_003Ec__AnonStorey4CD _003CEnterPartyField_003Ec__AnonStorey4CD;
					StartCoop(new Action<bool, bool>((object)_003CEnterPartyField_003Ec__AnonStorey4CD, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), is_reentry);
				}
				else if (call_back != null)
				{
					call_back.Invoke(is_m, false, false);
				}
			}, true);
		}
	}

	public static void EnterField(uint portal_id, uint deliveryId, Action<bool, bool, bool> call_back = null)
	{
		EnterField(portal_id, deliveryId, 0, call_back);
	}

	public unsafe static void EnterField(uint portal_id, uint deliveryId, int _toUserId, Action<bool, bool, bool> call_back = null)
	{
		if (MonoBehaviourSingleton<CoopManager>.I.coopRoom.IsActivate())
		{
			Logd("already in field.");
			if (call_back != null)
			{
				call_back.Invoke(true, true, true);
			}
		}
		else if (!MonoBehaviourSingleton<GameSceneManager>.IsValid() || MonoBehaviourSingleton<GameSceneManager>.I.CheckPortalAndOpenUpdateAppDialog(portal_id, false, true))
		{
			if (portal_id != MonoBehaviourSingleton<FieldManager>.I.currentPortalID)
			{
				MonoBehaviourSingleton<FieldManager>.I.SetCurrentFieldMapPortalID(portal_id);
			}
			MatchingField(deliveryId, _toUserId, delegate(bool is_m)
			{
				if (is_m)
				{
					_003CEnterField_003Ec__AnonStorey4CE._003CEnterField_003Ec__AnonStorey4CF _003CEnterField_003Ec__AnonStorey4CF;
					StartCoop(new Action<bool, bool>((object)_003CEnterField_003Ec__AnonStorey4CF, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), false);
				}
				else if (call_back != null)
				{
					call_back.Invoke(is_m, false, false);
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
				call_back.Invoke(true, true, true, true);
			}
		}
		else
		{
			RandomMatchingQuestField(delegate(bool isSuccess)
			{
				StartCoopAndQuestStart(isSuccess, true, call_back);
			});
		}
	}

	private static void StartCoopOffline(Action<bool, bool, bool> call_back = null)
	{
		if (!CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected())
		{
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
						call_back.Invoke(true, true, is_regist);
					}
				});
			}
		}
	}

	private unsafe static void StartCoop(Action<bool, bool> call_back = null, bool is_reentry = false)
	{
		MonoBehaviourSingleton<CoopManager>.I.Clear();
		CoopNetworkManager.ConnectData webSockConnectData = MonoBehaviourSingleton<FieldManager>.I.GetWebSockConnectData();
		_003CStartCoop_003Ec__AnonStorey4D2 _003CStartCoop_003Ec__AnonStorey4D;
		MonoBehaviourSingleton<CoopNetworkManager>.I.ConnectAndRegist(webSockConnectData, new Action<bool, bool>((object)_003CStartCoop_003Ec__AnonStorey4D, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	private unsafe static void StartCoopAndQuestStart(bool is_m, bool isFromField, Action<bool, bool, bool, bool> call_back = null)
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
					MonoBehaviourSingleton<QuestManager>.I.SetExploreStatus(new ExploreStatus(explore, true));
				}
				if (partyData.quest.rush != null)
				{
					MonoBehaviourSingleton<InGameManager>.I.SetRushInfo(partyData.quest.questId, partyData.quest.rush);
					MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID((uint)partyData.quest.rush.waves[0].questId, true);
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
			_003CStartCoopAndQuestStart_003Ec__AnonStorey4D4 _003CStartCoopAndQuestStart_003Ec__AnonStorey4D;
			StartCoop(new Action<bool, bool>((object)_003CStartCoopAndQuestStart_003Ec__AnonStorey4D, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), false);
		}
		else if (call_back != null)
		{
			call_back.Invoke(is_m, false, false, false);
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

	private unsafe static void RandomMatchingQuestField(Action<bool> call_back = null)
	{
		int quest_id = (int)MonoBehaviourSingleton<QuestManager>.I.currentQuestID;
		_003CRandomMatchingQuestField_003Ec__AnonStorey4D8 _003CRandomMatchingQuestField_003Ec__AnonStorey4D;
		MonoBehaviourSingleton<PartyManager>.I.SendRandomMatching(quest_id, 0, false, new Action<bool, int, bool, float>((object)_003CRandomMatchingQuestField_003Ec__AnonStorey4D, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	private static void MatchingPartyField(Action<bool> call_back = null, bool is_force_enter = false)
	{
		if (!PartyManager.IsValidInParty())
		{
			if (call_back != null)
			{
				call_back(false);
			}
		}
		else
		{
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

	public unsafe static void QuestComplete(Action<bool, Error> call_back = null)
	{
		if (!MonoBehaviourSingleton<QuestManager>.IsValid())
		{
			if (call_back != null)
			{
				call_back.Invoke(false, Error.Unknown);
			}
		}
		else
		{
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
			if (QuestManager.IsValidInGameWaveMatch(false))
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
			_003CQuestComplete_003Ec__AnonStorey4DD _003CQuestComplete_003Ec__AnonStorey4DD;
			if (QuestManager.IsValidTrial())
			{
				MonoBehaviourSingleton<QuestManager>.I.SendQuestCompleteTrial(missionClearStatuses, new Action<bool, Error>((object)_003CQuestComplete_003Ec__AnonStorey4DD, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
			else
			{
				MonoBehaviourSingleton<QuestManager>.I.SendQuestComplete(list, missionClearStatuses, memIds, hpRate, logs, new Action<bool, Error>((object)_003CQuestComplete_003Ec__AnonStorey4DD, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
		}
	}

	public static void ArenaComplete(Action<bool, Error> callBack = null)
	{
		if (!MonoBehaviourSingleton<QuestManager>.IsValid() && callBack != null)
		{
			callBack.Invoke(false, Error.Unknown);
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
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		Logd("Leave. start");
		this.StartCoroutine(LeaveCoroutine(call_back, toHome, fieldRetire, false));
	}

	public void LeaveWithParty(Action<bool> call_back = null, bool toHome = false, bool fieldRetire = false)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		Logd("LeaveWithParty. start");
		this.StartCoroutine(LeaveCoroutine(call_back, toHome, fieldRetire, true));
	}

	private unsafe IEnumerator LeaveCoroutine(Action<bool> call_back = null, bool toHome = false, bool fieldRetire = false, bool isParty = false)
	{
		bool is_success = true;
		if (FieldManager.IsValidInField())
		{
			bool is_leaved2 = false;
			bool wait3 = true;
			MonoBehaviourSingleton<FieldManager>.I.SendLeave(toHome, fieldRetire, delegate(bool is_leave)
			{
				((_003CLeaveCoroutine_003Ec__Iterator1AF)/*Error near IL_0059: stateMachine*/)._003Cwait_003E__2 = false;
				((_003CLeaveCoroutine_003Ec__Iterator1AF)/*Error near IL_0059: stateMachine*/)._003Cis_leaved_003E__1 = is_leave;
			});
			while (wait3)
			{
				yield return (object)null;
			}
			Logd("LeaveCoroutine. Field leaved:{0}", is_leaved2);
			is_success = is_leaved2;
		}
		if (isParty && PartyManager.IsValidInParty())
		{
			bool is_leaved = false;
			bool wait2 = true;
			MonoBehaviourSingleton<PartyManager>.I.SendLeave(delegate(bool is_leave)
			{
				((_003CLeaveCoroutine_003Ec__Iterator1AF)/*Error near IL_00df: stateMachine*/)._003Cwait_003E__4 = false;
				((_003CLeaveCoroutine_003Ec__Iterator1AF)/*Error near IL_00df: stateMachine*/)._003Cis_leaved_003E__3 = is_leave;
			});
			while (wait2)
			{
				yield return (object)null;
			}
			Logd("LeaveCoroutine. Party leaved:{0}", is_leaved);
		}
		if (MonoBehaviourSingleton<KtbWebSocket>.I.IsConnected())
		{
			bool wait = true;
			MonoBehaviourSingleton<CoopNetworkManager>.I.Close(1000, "Bye!", new Action((object)/*Error near IL_0156: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			while (wait)
			{
				yield return (object)null;
			}
		}
		MonoBehaviourSingleton<CoopManager>.I.Clear();
		call_back?.Invoke(is_success);
	}

	private unsafe IEnumerator OnApplicationPause(bool paused)
	{
		if (CoopWebSocketSingleton<KtbWebSocket>.IsValidOpen())
		{
			Logd("OnApplicationPause. pause={0}, is_connect={1}", paused, CoopWebSocketSingleton<KtbWebSocket>.IsValidOpen());
			if (paused)
			{
				if (MonoBehaviourSingleton<CoopNetworkManager>.IsValid())
				{
					if (MonoBehaviourSingleton<FieldManager>.IsValid() && MonoBehaviourSingleton<FieldManager>.I.IsEnabledStandby())
					{
						MonoBehaviourSingleton<CoopNetworkManager>.I.Standby();
					}
					else
					{
						MonoBehaviourSingleton<CoopNetworkManager>.I.LoopBackRoomLeave(true);
						if (PartyManager.IsValidInParty() && !InGameManager.IsReentryNotLeaveParty())
						{
							if (_003COnApplicationPause_003Ec__Iterator1B0._003C_003Ef__am_0024cache7 == null)
							{
								_003COnApplicationPause_003Ec__Iterator1B0._003C_003Ef__am_0024cache7 = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
							}
							Protocol.Force(_003COnApplicationPause_003Ec__Iterator1B0._003C_003Ef__am_0024cache7);
						}
						MonoBehaviourSingleton<KtbWebSocket>.I.Close(1000, "Bye!");
					}
				}
			}
			else
			{
				if (MonoBehaviourSingleton<CoopNetworkManager>.IsValid() && MonoBehaviourSingleton<FieldManager>.IsValid() && MonoBehaviourSingleton<FieldManager>.I.IsEnabledStandby())
				{
					MonoBehaviourSingleton<CoopNetworkManager>.I.Resume();
					if (MonoBehaviourSingleton<InGameProgress>.IsValid())
					{
						MonoBehaviourSingleton<CoopManager>.I.coopStage.packetSender.SendStageSyncTimeRequest();
					}
					if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && MonoBehaviourSingleton<StageObjectManager>.I.playerList != null)
					{
						List<StageObject> players = MonoBehaviourSingleton<StageObjectManager>.I.playerList;
						for (int i = 0; i < players.Count; i++)
						{
							Player player = players[i] as Player;
							if (player != null && player.rescueTime > 0f && !player.IsPrayed())
							{
								player.playerSender.SendDeadCountRequest(player.id);
							}
						}
					}
				}
				MonoBehaviourSingleton<KtbWebSocket>.I.ClearLastPacketReceivedTime();
			}
		}
		yield break;
	}
}
