using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CoopStage
{
	public enum STAGE_REQUEST_ERROR
	{
		NONE,
		ENTRY_CLOSE,
		STAGE_CLOSE_FAILED,
		STAGE_CLOSE_SUCCEED,
		DEACTIVATE
	}

	private class DefeatFieldEnemyDelivery
	{
		public int rewardId;

		public List<InGameManager.DropDeliveryInfo> deliveryList;
	}

	private bool isStageRequested;

	private SpanTimer closeEntrySpan = new SpanTimer(5f);

	public List<List<int>> bossBreakIDLists;

	public float bossStartHpDamageRate;

	public int bossDropNormal;

	public int bossDropRare;

	public BattleUserLog battleUserLog = new BattleUserLog();

	public FieldRewardPool fieldRewardPool = new FieldRewardPool();

	public bool isAsyncLoop;

	public ChatCoopConnection chatConnection;

	protected float checkCharacterSyncTimer;

	private bool forceNextWave;

	private List<DefeatFieldEnemyDelivery> defeatFieldEnemyDeliveryList = new List<DefeatFieldEnemyDelivery>();

	private bool fieldEnemyBossEntering;

	private bool readyToPlay;

	private bool isEnterFieldEnemyBossBattle;

	private bool isInFieldEnemyBossBattle;

	private bool requestedEnemyBossAlive;

	private Coroutine specialEnemyAnnounceExist;

	private Coroutine specialEnemyAnnounceGone;

	private ENEMY_POP_TYPE currentAnnounceType;

	private bool isInFieldFishingEnemyBattle;

	private bool isPresentQuest;

	private Coop_Model_WaveMatchInfo firstWaveMatchInfo;

	private float firstWaveMatchPopSec;

	private bool firstWaveMatchSet = true;

	public CoopStagePacketSender packetSender
	{
		get;
		private set;
	}

	public CoopStagePacketReceiver packetReceiver
	{
		get;
		private set;
	}

	public int stageId
	{
		get;
		private set;
	}

	public int stageIndex
	{
		get;
		private set;
	}

	public int hostClientId
	{
		get;
		private set;
	}

	public bool isActivateStart
	{
		get;
		private set;
	}

	public bool isRecvStageInfo
	{
		get;
		private set;
	}

	public bool isEnemyExtermination
	{
		get;
		private set;
	}

	public bool isEntryClose
	{
		get;
		protected set;
	}

	public bool isQuestClose
	{
		get;
		protected set;
	}

	public bool isQuestSucceed
	{
		get;
		protected set;
	}

	public bool isHostStageResponseEnd
	{
		get;
		protected set;
	}

	public bool isRecvRushRequested
	{
		get;
		private set;
	}

	public CoopStage()
		: this()
	{
		isStageRequested = false;
		isRecvStageInfo = false;
	}

	public void SetReadyToPlay()
	{
		readyToPlay = true;
	}

	public void SetIsEnterFieldEnemyBossBattle(bool isEnterFieldEnemyBossBattle)
	{
		this.isEnterFieldEnemyBossBattle = isEnterFieldEnemyBossBattle;
	}

	public bool GetIsInFieldEnemyBossBattle()
	{
		return isInFieldEnemyBossBattle;
	}

	public bool HasFieldEnemyBossLimitTime()
	{
		return isInFieldEnemyBossBattle && !isInFieldFishingEnemyBattle;
	}

	public bool GetisInFieldFishingEnemyBattle()
	{
		return isInFieldFishingEnemyBattle;
	}

	private void Awake()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		packetSender = this.get_gameObject().AddComponent<CoopStagePacketSender>();
		packetReceiver = this.get_gameObject().AddComponent<CoopStagePacketReceiver>();
	}

	private void Update()
	{
		packetReceiver.OnUpdate();
		fieldRewardPool.OnUpdate();
		if (closeEntrySpan.IsReady())
		{
			CheckEntryClose(false);
		}
		if (MonoBehaviourSingleton<CoopManager>.I.coopMyClient.IsBattleStart() && MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			checkCharacterSyncTimer -= Time.get_deltaTime();
			if (checkCharacterSyncTimer <= 0f)
			{
				CheckCharacterSync();
				checkCharacterSyncTimer = MonoBehaviourSingleton<InGameSettingsManager>.I.room.checkCharacterSyncInterval;
			}
		}
		CheckFirstWaveMatchInfo();
	}

	private void CheckCharacterSync()
	{
		if (isActivateStart && MonoBehaviourSingleton<CoopManager>.I.coopMyClient.IsBattleStart() && MonoBehaviourSingleton<StageObjectManager>.IsValid() && !MonoBehaviourSingleton<CoopManager>.I.coopRoom.isOfflinePlay)
		{
			ForEachClients(delegate(CoopClient c)
			{
				if (!(c is CoopMyClient) && c.IsBattleStart() && !c.isBattleRetire && !c.IsBattleEnd() && !c.isLeave)
				{
					if (c.IsPlayerPop())
					{
						Player player2 = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(c.playerId) as Player;
						if (player2 == null)
						{
							player2 = (MonoBehaviourSingleton<StageObjectManager>.I.FindCache(c.playerId) as Player);
						}
						if (player2 != null)
						{
							return;
						}
					}
					packetSender.SendRequestPop(c.clientId, true, true, false);
				}
			});
			int i = 0;
			for (int count = MonoBehaviourSingleton<StageObjectManager>.I.cacheList.Count; i < count; i++)
			{
				Player player = MonoBehaviourSingleton<StageObjectManager>.I.cacheList[i] as Player;
				if (player != null && player.IsPuppet() && player.playerSender != null)
				{
					player.playerSender.OnLoadComplete(false);
				}
			}
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
		stageId = 0;
		stageIndex = 0;
		hostClientId = 0;
		isActivateStart = false;
		isStageRequested = false;
		isRecvStageInfo = false;
		isEnemyExtermination = false;
		isEntryClose = false;
		isQuestClose = false;
		isQuestSucceed = false;
		bossBreakIDLists = null;
		bossStartHpDamageRate = 0f;
		bossDropNormal = 0;
		bossDropRare = 0;
		battleUserLog.Clear();
		fieldRewardPool.Clear();
		chatConnection = null;
		isHostStageResponseEnd = false;
		readyToPlay = false;
		requestedEnemyBossAlive = false;
		SetFalseEnemyBossBattleFlag();
		ClearAllSpecialEnemyAnnounce();
		if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			checkCharacterSyncTimer = MonoBehaviourSingleton<InGameSettingsManager>.I.room.checkCharacterSyncInterval;
		}
	}

	public void OnStageChangeInterval()
	{
		isActivateStart = false;
		isStageRequested = false;
		isRecvStageInfo = false;
		isEnemyExtermination = false;
		isEntryClose = false;
		isQuestSucceed = false;
		isQuestClose = false;
		bossBreakIDLists = null;
		bossStartHpDamageRate = 0f;
		bossDropNormal = 0;
		bossDropRare = 0;
		battleUserLog.Clear();
		fieldRewardPool.Clear();
		isHostStageResponseEnd = false;
		if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			checkCharacterSyncTimer = MonoBehaviourSingleton<InGameSettingsManager>.I.room.checkCharacterSyncInterval;
		}
	}

	public void OnQuestSeriesInterval()
	{
		isActivateStart = false;
		isStageRequested = false;
		isRecvStageInfo = false;
		isEnemyExtermination = false;
		isHostStageResponseEnd = false;
		if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			checkCharacterSyncTimer = MonoBehaviourSingleton<InGameSettingsManager>.I.room.checkCharacterSyncInterval;
		}
	}

	public void ForEachClients(Action<CoopClient> action)
	{
		MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.ForEach(delegate(CoopClient c)
		{
			if (c.stageId == stageId)
			{
				action(c);
			}
		});
	}

	public int GetClientCount()
	{
		int count = 0;
		MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.ForEach(delegate(CoopClient c)
		{
			if (c.stageId == stageId && c.IsPlayingStage())
			{
				count++;
			}
		});
		return count;
	}

	public bool IsSolo()
	{
		return GetClientCount() <= 1;
	}

	public void OnInitStage(int stage_id, int stage_idx, int host_client_id)
	{
		stageId = stage_id;
		stageIndex = stage_idx;
		hostClientId = host_client_id;
		Logd("OnInitStage: stageId={0}, stageIndex={1}, hostClientId={2}.", stage_id, stage_idx, host_client_id);
	}

	public void OnJoinClient(CoopClient client)
	{
		Logd("OnJoinClient: client={0}.", client);
		if (client.isStageHost && client.clientId != hostClientId)
		{
			SetHostClient(client.clientId);
		}
		client.OnStageChangeInterval();
	}

	public void OnLeaveClient(CoopClient client, int host_client_id)
	{
		Logd("OnLeaveClient: client={0}.", client);
		if (chatConnection != null && QuestManager.IsValidInGame() && !(client is CoopMyClient) && client.GetPlayer() != null)
		{
			chatConnection.OnReceiveNotification(StringTable.Format(STRING_CATEGORY.CHAT, 6u, client.GetPlayerName()));
		}
		client.PopCachePlayer(StageObject.COOP_MODE_TYPE.PUPPET);
		CoopStageObjectUtility.TransfarOwnerForClientObjects(client.clientId, host_client_id);
		if (hostClientId != host_client_id)
		{
			SetHostClient(host_client_id);
		}
		if (FieldManager.IsValidInGameNoBoss() || (MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsDefenseBattle()))
		{
			Player player = client.GetPlayer();
			if (player != null && !(player is Self))
			{
				player.DestroyObject();
			}
		}
		if (!(client is CoopMyClient))
		{
			client.OnStageChangeInterval();
		}
		if (IsSolo())
		{
			OnSwitchSolo();
		}
	}

	private void OnSwitchSolo()
	{
		Logd("OnSwitchSolo.");
		CoopStageObjectUtility.SetCoopModeForAll(StageObject.COOP_MODE_TYPE.NONE, 0);
	}

	public void OnSwitchOfflinePlay()
	{
		Logd("OnSwitchOfflinePlay... my status={0}", MonoBehaviourSingleton<CoopManager>.I.coopMyClient.status);
		ForEachClients(delegate(CoopClient c)
		{
			if (!(c is CoopMyClient))
			{
				c.PopCachePlayer(StageObject.COOP_MODE_TYPE.NONE);
				if (!c.IsPlayerPop() && c.userInfo != null && c.userInfo.equipSet.Count > 0)
				{
					int playerID = MonoBehaviourSingleton<CoopManager>.I.GetPlayerID(c);
					if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
					{
						MonoBehaviourSingleton<StageObjectManager>.I.CreateGuest(playerID, c.userInfo, null);
						c.SetPlayerID(playerID);
					}
				}
			}
		});
		if (!MonoBehaviourSingleton<CoopManager>.I.coopMyClient.IsBattleStart())
		{
			CoopStageObjectUtility.FillNonPlayer(4, GetClientCount());
		}
		CoopStageObjectUtility.SetOfflineForAll();
		if (!QuestManager.IsValidInGame())
		{
			CoopStageObjectUtility.OnlySelf();
		}
		CoopStageObjectUtility.ShrinkOriginalNonPlayer(4);
		if (QuestManager.IsValidInGameDefenseBattle())
		{
			CoopStageObjectUtility.DestroyAllNonPlayer();
		}
	}

	public void OnSwitchHost()
	{
		if (isActivateStart && !FieldManager.IsValidInGameNoQuest())
		{
			ForEachClients(delegate(CoopClient c)
			{
				if (!(c is CoopMyClient))
				{
					c.PopCachePlayer(StageObject.COOP_MODE_TYPE.NONE);
					if (!c.IsPlayerPop() && !c.isStageResponseEnd && c.userInfo != null && c.userInfo.equipSet.Count > 0)
					{
						int playerID = MonoBehaviourSingleton<CoopManager>.I.GetPlayerID(c);
						if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
						{
							Player aI = MonoBehaviourSingleton<StageObjectManager>.I.CreateGuest(playerID, c.userInfo, delegate(object o)
							{
								Player player = o as Player;
								if (MonoBehaviourSingleton<CoopManager>.I.coopRoom.IsBattle())
								{
									player.ActBattleStart(false);
								}
								else if (player.controller != null)
								{
									player.controller.SetEnableControll(false, ControllerBase.DISABLE_FLAG.BATTLE_START);
								}
							});
							c.SetPlayerID(playerID);
							CoopStageObjectUtility.SetAI(aI);
						}
					}
				}
			});
			if (!MonoBehaviourSingleton<CoopManager>.I.coopStage.isHostStageResponseEnd)
			{
				CoopStageObjectUtility.FillNonPlayer(4, MonoBehaviourSingleton<CoopManager>.I.coopStage.GetClientCount());
			}
			CoopStageObjectUtility.ShrinkOriginalNonPlayer(4);
			if (QuestManager.IsValidInGameDefenseBattle())
			{
				CoopStageObjectUtility.DestroyAllNonPlayer();
			}
			MonoBehaviourSingleton<CoopManager>.I.coopStage.SendOriginalObjectPop(0);
		}
	}

	public void OnPlayerPop(int player_id)
	{
		if (player_id == 0)
		{
			return;
		}
	}

	public void SetHostClient(int host_client_id)
	{
		if (hostClientId != host_client_id)
		{
			if (host_client_id == 0)
			{
				MonoBehaviourSingleton<CoopNetworkManager>.I.LoopBackRoomLeave(false);
			}
			else
			{
				CoopClient coopClient = MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.FindByClientId(hostClientId);
				CoopClient coopClient2 = MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.FindByClientId(host_client_id);
				Logd("SetHostClient: SwitchHost: old host={0}", coopClient);
				Logd("SetHostClient: SwitchHost: new host={0}", coopClient2);
				if (coopClient2 != null && coopClient2 is CoopMyClient)
				{
					ReSendStageInfo();
					MonoBehaviourSingleton<CoopManager>.I.coopStage.OnSwitchHost();
					if (!coopClient2.IsBattleStart() && coopClient != null && coopClient.IsBattleStart() && QuestManager.IsValidInGame())
					{
						if (MonoBehaviourSingleton<InGameManager>.I.IsRush())
						{
							if (!IsOtherClientProgressed())
							{
								MonoBehaviourSingleton<CoopNetworkManager>.I.LoopBackRoomLeave(false);
							}
						}
						else if (QuestManager.IsValidInGameExplore())
						{
							if (MonoBehaviourSingleton<QuestManager>.I.IsExploreBossMap())
							{
								MonoBehaviourSingleton<CoopNetworkManager>.I.LoopBackRoomLeave(false);
							}
						}
						else
						{
							MonoBehaviourSingleton<CoopNetworkManager>.I.LoopBackRoomLeave(false);
						}
					}
				}
				if (coopClient != null && coopClient.stageId == stageId)
				{
					coopClient.SetStageHost(false);
				}
				if (coopClient2 != null && coopClient2.stageId == stageId)
				{
					coopClient2.SetStageHost(true);
				}
				Logd("SetHostClient: hostClientId {0} => {1}", hostClientId, host_client_id);
				hostClientId = host_client_id;
			}
		}
	}

	private bool IsWait(Func<bool> cb)
	{
		return isAsyncLoop || !cb.Invoke();
	}

	private bool IsAsyncWait(Func<bool> cb)
	{
		return isAsyncLoop || ((MonoBehaviourSingleton<KtbWebSocket>.I.IsOpen() || CoopOfflineManager.IsValidActivate()) && !cb.Invoke());
	}

	private bool IsGuestAsyncWait(Func<bool> cb)
	{
		return isAsyncLoop || (MonoBehaviourSingleton<KtbWebSocket>.I.IsOpen() && !MonoBehaviourSingleton<CoopManager>.I.isStageHost && !cb.Invoke());
	}

	public unsafe IEnumerator DoActivate()
	{
		isActivateStart = true;
		if (MonoBehaviourSingleton<CoopOfflineManager>.IsValid())
		{
			MonoBehaviourSingleton<CoopOfflineManager>.I.OnStageActivate();
		}
		if (QuestManager.IsValidInGame() && !FieldManager.IsValidInGameNoBoss())
		{
			InitBossBreakIdList();
		}
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && MonoBehaviourSingleton<StageObjectManager>.I.self != null)
		{
			Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
			self.id = MonoBehaviourSingleton<CoopManager>.I.GetSelfID();
			if (self.record != null)
			{
				self.record.id = self.id;
			}
			self.SetCoopMode(StageObject.COOP_MODE_TYPE.NONE, 0);
			MonoBehaviourSingleton<CoopManager>.I.coopMyClient.SetPlayerID(self.id);
		}
		forceNextWave = false;
		MonoBehaviourSingleton<CoopManager>.I.coopMyClient.StageRequest();
		MonoBehaviourSingleton<CoopNetworkManager>.I.RoomStageRequest();
		while (IsAsyncWait(new Func<bool>((object)/*Error near IL_0149: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)))
		{
			yield return (object)null;
		}
		if (IsOtherClientProgressed())
		{
			ForceProgressNextWave();
		}
		else
		{
			if (MonoBehaviourSingleton<InGameManager>.I.isValidGimmickObject)
			{
				Logd("Activate: Gimmick wait...");
				while (IsGuestAsyncWait(new Func<bool>((object)/*Error near IL_01e2: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)))
				{
					if (IsOtherClientProgressed())
					{
						ForceProgressNextWave();
						yield break;
					}
					yield return (object)null;
				}
			}
			if (QuestManager.IsValidInGameExplore())
			{
				while (IsGuestAsyncWait(new Func<bool>((object)/*Error near IL_0220: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)))
				{
					yield return (object)null;
				}
			}
			bool rushTryReentry = false;
			if (MonoBehaviourSingleton<InGameManager>.I.IsNeedInitBoss())
			{
				Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
				Time.get_time();
				while (IsWait(new Func<bool>((object)/*Error near IL_02a5: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)))
				{
					if (IsOtherClientProgressed())
					{
						ForceProgressNextWave();
						yield break;
					}
					yield return (object)null;
				}
				if (boss != null)
				{
					if (boss.IsOriginal() || boss.IsCoopNone())
					{
						MonoBehaviourSingleton<StageObjectManager>.I.self.SetAppearPosOwner(boss._position);
					}
					else
					{
						MonoBehaviourSingleton<StageObjectManager>.I.self.SetAppearPosGuest(boss._position);
					}
					if (MonoBehaviourSingleton<InGameManager>.I.IsRush() && MonoBehaviourSingleton<CoopManager>.I.isStageHost && MonoBehaviourSingleton<InGameManager>.I.isRushReentry)
					{
						MonoBehaviourSingleton<InGameManager>.I.RestoreRushInReentry();
					}
					if (MonoBehaviourSingleton<QuestManager>.I.IsCurrentQuestTypeSeries() && MonoBehaviourSingleton<CoopManager>.I.isStageHost && MonoBehaviourSingleton<InGameManager>.I.isSeriesReentry)
					{
						MonoBehaviourSingleton<InGameManager>.I.RestoreSeriesInReentry();
					}
				}
			}
			if (MonoBehaviourSingleton<StageObjectManager>.I.boss != null && bossBreakIDLists != null)
			{
				int series = (int)MonoBehaviourSingleton<QuestManager>.I.currentQuestSeriesIndex;
				bossBreakIDLists[series] = MonoBehaviourSingleton<StageObjectManager>.I.boss.GetBreakRegionIDList();
			}
			if (MonoBehaviourSingleton<StageObjectManager>.I.self != null)
			{
				Logd("Activate: Self poss wait...");
				while (IsWait(new Func<bool>((object)/*Error near IL_0456: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)))
				{
					if (IsOtherClientProgressed())
					{
						ForceProgressNextWave();
						yield break;
					}
					yield return (object)null;
				}
			}
			if (rushTryReentry)
			{
				Logd("Rush try reentry.");
				MonoBehaviourSingleton<InGameProgress>.I.FieldReentry();
			}
			isPresentQuest = false;
			int eventId = Utility.GetCurrentEventID();
			if (eventId > 0)
			{
				Network.EventData eventData = MonoBehaviourSingleton<QuestManager>.I.eventList.Where(new Func<Network.EventData, bool>((object)/*Error near IL_04c6: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)).FirstOrDefault();
				if (eventData != null && eventData.eventTypeEnum == EVENT_TYPE.PRESENT_QUEST)
				{
					isPresentQuest = true;
				}
			}
			Logd("Activate: Start battle.");
			StartBattle();
		}
	}

	public bool IsPresentQuest()
	{
		return isPresentQuest;
	}

	public void Deactivate()
	{
		Clear();
	}

	public void InitBossBreakIdList()
	{
		Logd("Activate: Boss break list stock.");
		if (bossBreakIDLists == null)
		{
			bossBreakIDLists = new List<List<int>>();
			int i = 0;
			for (int currentQuestSeriesNum = MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestSeriesNum(); i < currentQuestSeriesNum; i++)
			{
				bossBreakIDLists.Add(new List<int>());
			}
		}
	}

	private unsafe void StartBattle()
	{
		if (!MonoBehaviourSingleton<CoopManager>.I.coopMyClient.IsBattleStart())
		{
			Logd("StartBattle!");
			MonoBehaviourSingleton<CoopManager>.I.coopMyClient.StartBattle();
			MonoBehaviourSingleton<CoopManager>.I.coopRoom.StartBattle();
			MonoBehaviourSingleton<CoopNetworkManager>.I.BattleStart();
			if (MonoBehaviourSingleton<CoopManager>.I.isStageHost && MonoBehaviourSingleton<InGameProgress>.IsValid())
			{
				MonoBehaviourSingleton<InGameProgress>.I.StartTimer(0f);
			}
			if (MonoBehaviourSingleton<InGameProgress>.IsValid())
			{
				MonoBehaviourSingleton<InGameProgress>.I.BattleStart();
				if (InGameManager.IsReentry() && !CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected())
				{
					MonoBehaviourSingleton<InGameProgress>.I.FieldReentry();
				}
			}
			fieldRewardPool.SetFieldId(MonoBehaviourSingleton<FieldManager>.I.GetFieldId());
			int num = MonoBehaviourSingleton<FieldManager>.I.GetMapId();
			if (QuestManager.IsValidInGameExplore())
			{
				num = MonoBehaviourSingleton<QuestManager>.I.ExploreMapIndexToId(MonoBehaviourSingleton<CoopManager>.I.coopMyClient.exploreMapIndex);
			}
			fieldRewardPool.SetMapId(num);
			if (QuestManager.IsValidInGameExplore() && Singleton<QuestTable>.IsValid() && Singleton<EnemyTable>.IsValid())
			{
				FieldManager i = MonoBehaviourSingleton<FieldManager>.I;
				int mapId = num;
				if (_003C_003Ef__am_0024cache29 == null)
				{
					_003C_003Ef__am_0024cache29 = new Action<bool, Error>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
				}
				i.SendFieldQuestMapChange(mapId, _003C_003Ef__am_0024cache29);
				CheckBossTrace(num);
			}
			if (FieldManager.IsValidInGameNoQuest())
			{
				SetSpecialEnemyAnounceIfNeed(num);
			}
			MonoBehaviourSingleton<InGameManager>.I.ResetRushInReentry();
			MonoBehaviourSingleton<InGameManager>.I.ResetSeriesInReentry();
		}
	}

	private void SetSpecialEnemyAnounceIfNeed(int mapId)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		ClearAllSpecialEnemyAnnounce();
		if (Singleton<FieldMapEnemyPopTimeZoneTable>.I.TryGetEnableLastEndTime(mapId, out FieldMapEnemyPopTimeZoneTable.FieldMapEnemyPopTimeZoneData resultTimeZone, out ENEMY_POP_TYPE resultType))
		{
			currentAnnounceType = resultType;
			specialEnemyAnnounceGone = this.StartCoroutine(AnnounceSpecialEnemyGone(resultType, resultTimeZone));
			if (!ExistSpecialEnemyOnField())
			{
				specialEnemyAnnounceExist = this.StartCoroutine(AnnounceSpecialEnemyExist(resultType, resultTimeZone));
			}
		}
	}

	private void ClearAllSpecialEnemyAnnounce()
	{
		ClearSpecialEnemyAnnounce(ENEMY_POP_TYPE.FIELD_BOSS);
		ClearSpecialEnemyAnnounce(ENEMY_POP_TYPE.RARE_SPECIES);
		currentAnnounceType = ENEMY_POP_TYPE.NONE;
	}

	private void ClearSpecialEnemyAnnounce(ENEMY_POP_TYPE type)
	{
		if (currentAnnounceType == type)
		{
			ClearSpecialEnemyExistAnnounce(type);
			if (specialEnemyAnnounceGone != null)
			{
				this.StopCoroutine(specialEnemyAnnounceGone);
				specialEnemyAnnounceGone = null;
			}
		}
	}

	private void ClearSpecialEnemyExistAnnounce(ENEMY_POP_TYPE type)
	{
		if (currentAnnounceType == type && specialEnemyAnnounceExist != null)
		{
			this.StopCoroutine(specialEnemyAnnounceExist);
			specialEnemyAnnounceExist = null;
		}
	}

	private IEnumerator AnnounceSpecialEnemyExist(ENEMY_POP_TYPE type, FieldMapEnemyPopTimeZoneTable.FieldMapEnemyPopTimeZoneData timeZoneData)
	{
		while (!readyToPlay)
		{
			yield return (object)null;
		}
		yield return (object)new WaitForSeconds(1f);
		if (!ExistSpecialEnemyOnField())
		{
			uint msgIndex = 9000u;
			if (timeZoneData.existStrId == 0)
			{
				switch (type)
				{
				case ENEMY_POP_TYPE.FIELD_BOSS:
					msgIndex = 9000u;
					break;
				case ENEMY_POP_TYPE.RARE_SPECIES:
					msgIndex = 9002u;
					break;
				}
			}
			else
			{
				msgIndex = timeZoneData.existStrId;
			}
			string msg = StringTable.Format(STRING_CATEGORY.IN_GAME, msgIndex);
			UIInGamePopupDialog.PushOpen(msg, false, 1.8f);
		}
	}

	private IEnumerator AnnounceSpecialEnemyGone(ENEMY_POP_TYPE type, FieldMapEnemyPopTimeZoneTable.FieldMapEnemyPopTimeZoneData timeZoneData)
	{
		if (timeZoneData.TryGetEndTime(out DateTime endTime))
		{
			DateTime now = TimeManager.GetNow();
			endTime = TimeManager.CombineDateAndTime(now, endTime);
			TimeSpan stayTime = endTime - now;
			if (!(stayTime <= TimeSpan.Zero))
			{
				yield return (object)new WaitForSeconds((float)stayTime.TotalSeconds);
				while (ExistSpecialEnemyOnField())
				{
					yield return (object)new WaitForSeconds(10f);
				}
				uint msgIndex = 9001u;
				if (timeZoneData.goneStrId == 0)
				{
					switch (type)
					{
					case ENEMY_POP_TYPE.FIELD_BOSS:
						msgIndex = 9001u;
						break;
					case ENEMY_POP_TYPE.RARE_SPECIES:
						msgIndex = 9003u;
						break;
					}
				}
				else
				{
					msgIndex = timeZoneData.goneStrId;
				}
				string msg = StringTable.Format(STRING_CATEGORY.IN_GAME, msgIndex);
				UIInGamePopupDialog.PushOpen(msg, false, 1.8f);
			}
		}
	}

	private bool ExistSpecialEnemyOnField()
	{
		if (isInFieldEnemyBossBattle)
		{
			return true;
		}
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return false;
		}
		List<Enemy> enemyList = MonoBehaviourSingleton<StageObjectManager>.I.EnemyList;
		if (enemyList == null || enemyList.Count <= 0)
		{
			return false;
		}
		int i = 0;
		for (int count = enemyList.Count; i < count; i++)
		{
			if (enemyList[i].isRareSpecies)
			{
				return true;
			}
		}
		return false;
	}

	private void CheckBossTrace(int mapId)
	{
		if (CheckBossTraceSelf(mapId))
		{
			if (MonoBehaviourSingleton<QuestManager>.I.GetReservedTraceInfo() != null)
			{
				MonoBehaviourSingleton<QuestManager>.I.CompleteBossTracePopup();
			}
		}
		else
		{
			CheckBossTraceParty();
		}
	}

	private bool CheckBossTraceSelf(int mapId)
	{
		switch (MonoBehaviourSingleton<QuestManager>.I.GetExploreHistoryType(mapId))
		{
		case EXPLORE_HISTORY_TYPE.LAST:
		{
			QuestTable.QuestTableData questData2 = Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID);
			int mainEnemyID2 = questData2.GetMainEnemyID();
			EnemyTable.EnemyData enemyData2 = Singleton<EnemyTable>.I.GetEnemyData((uint)mainEnemyID2);
			string text2 = StringTable.Format(STRING_CATEGORY.IN_GAME, 8001u, enemyData2.name);
			UIInGamePopupDialog.PushOpen(text2, false, 1.4f);
			MonoBehaviourSingleton<CoopManager>.I.coopRoom.packetSender.SendNotifyTraceBoss(mapId, 0);
			MonoBehaviourSingleton<QuestManager>.I.UpdateBossTraceHistory(mapId, 0, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name, false);
			return true;
		}
		case EXPLORE_HISTORY_TYPE.SECOND_LAST:
		{
			QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID);
			int mainEnemyID = questData.GetMainEnemyID();
			EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)mainEnemyID);
			string text = StringTable.Format(STRING_CATEGORY.IN_GAME, 8002u, enemyData.name);
			UIInGamePopupDialog.PushOpen(text, false, 1.4f);
			MonoBehaviourSingleton<CoopManager>.I.coopRoom.packetSender.SendNotifyTraceBoss(mapId, 1);
			MonoBehaviourSingleton<QuestManager>.I.UpdateBossTraceHistory(mapId, 1, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name, false);
			return true;
		}
		default:
			return false;
		}
	}

	private void CheckBossTraceParty()
	{
		ExploreStatus.TraceInfo reservedTraceInfo = MonoBehaviourSingleton<QuestManager>.I.GetReservedTraceInfo();
		if (reservedTraceInfo != null)
		{
			int num = 0;
			EXPLORE_HISTORY_TYPE historyType = reservedTraceInfo.historyType;
			num = ((historyType != EXPLORE_HISTORY_TYPE.LAST) ? 8004 : 8003);
			string text = StringTable.Format(STRING_CATEGORY.IN_GAME, (uint)num, reservedTraceInfo.playerName);
			UIInGamePopupDialog.PushOpen(text, false, 1.22f);
			MonoBehaviourSingleton<QuestManager>.I.CompleteBossTracePopup();
		}
	}

	public void OnRequested()
	{
		if (!isStageRequested)
		{
			isStageRequested = true;
			if (MonoBehaviourSingleton<CoopManager>.I.isStageHost)
			{
				Logd("OnRequested: Non player create from fill.");
				CoopStageObjectUtility.FillNonPlayer(4, GetClientCount());
				ReSendStageInfo();
				isHostStageResponseEnd = true;
			}
			Logd("OnRequested: complete...");
		}
	}

	public void OnRequestedClient(CoopClient client)
	{
		if (isQuestClose)
		{
			Logd("OnRequestedClient: STAGE_CLOSE. isSucceed={0}, to={1}", isQuestSucceed, client.clientId);
			if (isQuestSucceed)
			{
				packetSender.SendStageResponseEnd(STAGE_REQUEST_ERROR.STAGE_CLOSE_SUCCEED, client.clientId);
			}
			else
			{
				packetSender.SendStageResponseEnd(STAGE_REQUEST_ERROR.STAGE_CLOSE_FAILED, client.clientId);
			}
		}
		else if (!isActivateStart)
		{
			Logd("OnRequestedClient: DEACTIVATE. to={0}", client.clientId);
			packetSender.SendStageResponseEnd(STAGE_REQUEST_ERROR.DEACTIVATE, client.clientId);
		}
		else
		{
			if (!client.isSendPopOriginal)
			{
				SendOriginalObjectPop(client.clientId);
				client.isSendPopOriginal = true;
			}
			if (MonoBehaviourSingleton<CoopManager>.I.isStageHost && !client.isSendStageInfo)
			{
				Logd("OnRequestedClient: Send StageInfo. to={0}", client.clientId);
				packetSender.SendStageInfo(client.clientId);
				client.isSendStageInfo = true;
			}
			Logd("OnRequestedClient: Send ResponseEnd. to={0}", client.clientId);
			packetSender.SendStageResponseEnd(STAGE_REQUEST_ERROR.NONE, client.clientId);
		}
	}

	public void ReSendStageInfo()
	{
		if (isActivateStart)
		{
			ForEachClients(delegate(CoopClient c)
			{
				if (!(c is CoopMyClient) && c.IsStageRequest() && !c.isSendStageInfo)
				{
					Logd("ReSendStageInfo: to={0}", c.clientId);
					packetSender.SendStageInfo(c.clientId);
					c.isSendStageInfo = true;
				}
			});
		}
	}

	public void SendOriginalObjectPop(int to_client_id)
	{
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			MonoBehaviourSingleton<StageObjectManager>.I.playerList.ForEach(delegate(StageObject o)
			{
				Player player = o as Player;
				if (player != null && (player.IsOriginal() || player.IsCoopNone()))
				{
					packetSender.SendStagePlayerPop(player, to_client_id);
					if (player is Self)
					{
						Logd("StageRequest response SelfPop({0}). to={1}", player.id, to_client_id);
					}
					else
					{
						Logd("StageRequest response PlayerPop({0}). to={1}", player.id, to_client_id);
					}
				}
				else if (player == null)
				{
					Log.Warning(LOG.COOP, "SendOriginalObjectPop, player is null. to_client_id:{0}", to_client_id);
				}
			});
			if (MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.I.isStageHost)
			{
				List<StageObject> allCoopObjectList = MonoBehaviourSingleton<StageObjectManager>.I.GetAllCoopObjectList();
				if (allCoopObjectList != null)
				{
					int i = 0;
					for (int count = allCoopObjectList.Count; i < count; i++)
					{
						if (!(allCoopObjectList[i] == null))
						{
							allCoopObjectList[i].SetCoopMode(StageObject.COOP_MODE_TYPE.ORIGINAL, 0);
							packetSender.SendObjectInfo(allCoopObjectList[i], StageObject.COOP_MODE_TYPE.MIRROR, to_client_id);
						}
					}
				}
			}
		}
	}

	public bool OnRecvStagePlayerPop(Coop_Model_StagePlayerPop model, CoopPacket packet)
	{
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		if (!isActivateStart)
		{
			return false;
		}
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return true;
		}
		CoopClient coopClient = MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.FindByClientId(packet.fromClientId);
		if (coopClient == null)
		{
			return true;
		}
		Logd("Responsed PlayerPop from {0}. sid={1},userid={2},self={3}", packet.fromClientId, model.sid, coopClient.userId, model.isSelf);
		ForEachClients(delegate(CoopClient c)
		{
			c.OnPlayerPop(model.sid);
		});
		if (model.isSelf)
		{
			if (model.charaInfo != null)
			{
				Predicate<StageObject> match = delegate(StageObject o)
				{
					Player player2 = o as Player;
					return player2 != null && !player2.isDestroyWaitFlag && player2.id != model.sid && player2.createInfo != null && player2.createInfo.charaInfo != null && model.charaInfo != null && player2.createInfo.charaInfo.userId != 0 && player2.createInfo.charaInfo.userId == model.charaInfo.userId;
				};
				StageObject stageObject = MonoBehaviourSingleton<StageObjectManager>.I.playerList.Find(match);
				if (stageObject == null)
				{
					stageObject = MonoBehaviourSingleton<StageObjectManager>.I.cacheList.Find(match);
				}
				if (stageObject != null)
				{
					Logd("OnRecvStagePlayerPop last leaved player({0}) destroy. to={1}", stageObject.id, coopClient.userId);
					stageObject.DestroyObject();
				}
			}
			coopClient.SetPlayerID(model.sid);
			if (model.charaInfo != null)
			{
				coopClient.SetUserInfo(model.charaInfo);
			}
		}
		Player player = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(model.sid) as Player;
		if (player == null)
		{
			player = (MonoBehaviourSingleton<StageObjectManager>.I.FindCache(model.sid) as Player);
		}
		if (player != null)
		{
			if (player == MonoBehaviourSingleton<StageObjectManager>.I.self)
			{
				Logd("myself player pop!");
				return true;
			}
			if (player.isLoading)
			{
				return false;
			}
		}
		if (player == null)
		{
			PlayerLoader.OnCompleteLoad callback = delegate
			{
				OnPopPlayerLoadComplete(player, packet.fromClientId);
			};
			if (model.extentionInfo != null && model.extentionInfo.npcDataID != 0)
			{
				Logd("CreateNonPlayer. sid={0},npcDataID={1},npcLv={2},npcLvIndex={3}", model.sid, model.extentionInfo.npcDataID, model.extentionInfo.npcLv, model.extentionInfo.npcLvIndex);
				player = MonoBehaviourSingleton<StageObjectManager>.I.CreateNonPlayer(model.sid, model.extentionInfo, Vector3.get_zero(), 0f, model.transferInfo, callback);
			}
			else
			{
				StageObjectManager.CreatePlayerInfo createPlayerInfo = new StageObjectManager.CreatePlayerInfo();
				createPlayerInfo.charaInfo = model.charaInfo;
				Logd("CreatePlayer. sid={0},userId={1}", model.sid, createPlayerInfo.charaInfo.userId);
				createPlayerInfo.extentionInfo = model.extentionInfo;
				player = MonoBehaviourSingleton<StageObjectManager>.I.CreatePlayer(model.sid, createPlayerInfo, false, Vector3.get_zero(), 0f, model.transferInfo, callback);
				if (QuestManager.IsValidInGame())
				{
					MonoBehaviourSingleton<QuestManager>.I.resultUserCollection.AddPlayer(createPlayerInfo.charaInfo);
				}
			}
			if (player != null)
			{
				player.SetCoopMode(StageObject.COOP_MODE_TYPE.PUPPET, packet.fromClientId);
			}
		}
		else
		{
			Logd("already player pop! sid={0}", model.sid);
			MonoBehaviourSingleton<StageObjectManager>.I.RemoveCacheObject(player);
			player.get_gameObject().SetActive(true);
			player.SafeActIdle();
			player.SetCoopMode(StageObject.COOP_MODE_TYPE.PUPPET, packet.fromClientId);
			if (!player.isLoading)
			{
				OnPopPlayerLoadComplete(player, packet.fromClientId);
			}
		}
		return true;
	}

	protected void OnPopPlayerLoadComplete(Player player, int client_id)
	{
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && MonoBehaviourSingleton<KtbWebSocket>.I.IsConnected())
		{
			if (player.IsOriginal() || player.IsCoopNone())
			{
				Logd("PlayerLoadComplete skip. became original in loading. player={0},client={1}", player.id, client_id);
				CoopStageObjectUtility.ShrinkOriginalNonPlayer(4);
				if (QuestManager.IsValidInGameDefenseBattle())
				{
					CoopStageObjectUtility.DestroyAllNonPlayer();
				}
			}
			else
			{
				player.RemoveController();
				if (player.isCoopInitialized)
				{
					Logd("PlayerLoadComplete skip. already initilized. player={0},client={1}", player.id, client_id);
				}
				else
				{
					Logd("PlayerLoadComplete. player={0},client={1}", player.id, client_id);
					player.get_gameObject().SetActive(false);
					MonoBehaviourSingleton<StageObjectManager>.I.AddCacheObject(player);
					player.packetReceiver.SetFilterMode(ObjectPacketReceiver.FILTER_MODE.WAIT_INITIALIZE);
					player.playerSender.OnLoadComplete(true);
					if (MonoBehaviourSingleton<UIInGameMessageBar>.IsValid() && !(player is NonPlayer))
					{
						MonoBehaviourSingleton<UIInGameMessageBar>.I.Announce(player.charaName, StringTable.Get(STRING_CATEGORY.IN_GAME, 0u));
					}
					CoopStageObjectUtility.ShrinkOriginalNonPlayer(4);
					if (QuestManager.IsValidInGameDefenseBattle())
					{
						CoopStageObjectUtility.DestroyAllNonPlayer();
					}
				}
			}
		}
	}

	public bool OnRecvEnemyPop(Coop_Model_EnemyPop model)
	{
		Enemy outEnmey;
		return PopEnemy(model, out outEnmey);
	}

	private bool PopEnemy(Coop_Model_EnemyPop model, out Enemy outEnmey)
	{
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		outEnmey = null;
		if (!isActivateStart)
		{
			return false;
		}
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return true;
		}
		if (!MonoBehaviourSingleton<FieldManager>.IsValid())
		{
			return true;
		}
		Logd("OnRecvEnemyPop. sid={0},ownerClientId={1},popIndex={2}", model.sid, model.ownerClientId, model.popIndex);
		Enemy enemy = MonoBehaviourSingleton<StageObjectManager>.I.FindEnemy(model.sid) as Enemy;
		if (enemy == null)
		{
			enemy = (MonoBehaviourSingleton<StageObjectManager>.I.FindCache(model.sid) as Enemy);
		}
		if (enemy != null && enemy.isLoading)
		{
			Logd("OnRecvEnemyPop. enemy is loading...");
			return false;
		}
		if (QuestManager.IsValidInGameSeries())
		{
			return PopEnemyForSeries(model, enemy);
		}
		FieldMapTable.EnemyPopTableData enemyPopData = Singleton<FieldMapTable>.I.GetEnemyPopData(MonoBehaviourSingleton<FieldManager>.I.currentMapID, model.popIndex);
		if (enemyPopData == null)
		{
			Log.Error(LOG.COOP, "CoopStage: OnRecvEnemyPop. not found field enemy pop data. mapId={0},idx={1}", MonoBehaviourSingleton<FieldManager>.I.currentMapID, model.popIndex);
			return true;
		}
		int num = (int)enemyPopData.enemyID;
		int num2 = (int)enemyPopData.enemyLv;
		QUEST_TYPE qUEST_TYPE = QUEST_TYPE.NORMAL;
		QUEST_STYLE qUEST_STYLE = QUEST_STYLE.NORMAL;
		if (num == 0 && QuestManager.IsValidInGame())
		{
			QuestManager i = MonoBehaviourSingleton<QuestManager>.I;
			num = i.GetCurrentQuestEnemyID();
			num2 = i.GetCurrentQuestEnemyLv();
			qUEST_TYPE = i.GetCurrentQuestType();
			qUEST_STYLE = i.GetCurrentQuestStyle();
		}
		if (model.ownerClientId == MonoBehaviourSingleton<CoopManager>.I.coopMyClient.clientId)
		{
			if (enemy == null)
			{
				QUEST_TYPE qUEST_TYPE2 = qUEST_TYPE;
				if (qUEST_TYPE2 == QUEST_TYPE.DEFENSE)
				{
					enemy = MonoBehaviourSingleton<StageObjectManager>.I.CreateEnemyForDefenseBattle(model.sid, num, num2);
				}
				else
				{
					QUEST_STYLE qUEST_STYLE2 = qUEST_STYLE;
					enemy = ((qUEST_STYLE2 != QUEST_STYLE.DEFENSE) ? MonoBehaviourSingleton<StageObjectManager>.I.CreateEnemy(model.sid, Vector3.get_zero(), 0f, num, num2, enemyPopData.bossFlag, enemyPopData.bigMonsterFlag, true, false, delegate(Enemy target)
					{
						if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
						{
							MonoBehaviourSingleton<InGameRecorder>.I.RecordEnemyHP(target.id, target.hpMax);
						}
					}) : MonoBehaviourSingleton<StageObjectManager>.I.CreateEnemyForDefenseBattle(model.sid, num, num2));
				}
				if (model.popIndex >= 0 && MonoBehaviourSingleton<CoopOfflineManager>.IsValid())
				{
					MonoBehaviourSingleton<CoopOfflineManager>.I.OnEnemyPop(model.popIndex, model.sid);
				}
			}
			else
			{
				MonoBehaviourSingleton<StageObjectManager>.I.RemoveCacheObject(enemy);
				enemy.get_gameObject().SetActive(true);
			}
			if (IsSolo())
			{
				enemy.SetCoopMode(StageObject.COOP_MODE_TYPE.NONE, 0);
			}
			else
			{
				enemy.SetCoopMode(StageObject.COOP_MODE_TYPE.ORIGINAL, 0);
			}
			if (enemy.controller == null)
			{
				enemy.AddController<EnemyController>();
			}
			if (enemy.isBoss && enemy.controller != null && !MonoBehaviourSingleton<CoopManager>.I.coopMyClient.IsBattleStart())
			{
				enemy.controller.SetEnableControll(false, ControllerBase.DISABLE_FLAG.BATTLE_START);
			}
			enemy.CheckFirstMadMode();
			enemy.SafeActIdle();
			enemy.enemyPopIndex = model.popIndex;
			if (!enemy.isSetAppearPos)
			{
				if (model.setPos)
				{
					enemy.SetAppearPosForce(new Vector3(model.x, 0f, model.z), enemyPopData);
				}
				else
				{
					enemy.SetAppearPosEnemy();
				}
			}
			Logd("OnRecvEnemyPop. my owner enemy({0}). is load={1}...", enemy, enemy.isLoading);
		}
		else
		{
			if (enemy == null)
			{
				enemy = MonoBehaviourSingleton<StageObjectManager>.I.CreateEnemy(model.sid, Vector3.get_zero(), 0f, num, num2, enemyPopData.bossFlag, enemyPopData.bigMonsterFlag, false, false, delegate(Enemy target)
				{
					OnPopEnemyLoadComplete(target, model.ownerClientId, true);
				});
				enemy.SetCoopMode(StageObject.COOP_MODE_TYPE.MIRROR, model.ownerClientId);
				enemy.enemyPopIndex = model.popIndex;
				if (model.popIndex >= 0 && MonoBehaviourSingleton<CoopOfflineManager>.IsValid())
				{
					MonoBehaviourSingleton<CoopOfflineManager>.I.OnEnemyPop(model.popIndex, model.sid);
				}
			}
			else
			{
				enemy.SetCoopMode(StageObject.COOP_MODE_TYPE.MIRROR, model.ownerClientId);
				enemy.isCoopInitialized = false;
				enemy.SafeActIdle();
				OnPopEnemyLoadComplete(enemy, model.ownerClientId, false);
			}
			if (enemyPopData.enablePopY)
			{
				enemy.onTheGround = false;
			}
			Logd("OnRecvEnemyPop. other owner enemy({0}). is load={1}...", enemy, enemy.isLoading);
		}
		if (enemyPopData.enemyPopType == ENEMY_POP_TYPE.RARE_SPECIES)
		{
			if (MonoBehaviourSingleton<UIInGameFieldQuestWarning>.IsValid() && !ExistSpecialEnemyOnField())
			{
				MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.PlayRareFieldEnemy();
				MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.FadeOut(4f, 1f, null);
			}
			ClearSpecialEnemyExistAnnounce(ENEMY_POP_TYPE.RARE_SPECIES);
			enemy.isRareSpecies = true;
		}
		outEnmey = enemy;
		return true;
	}

	private bool PopEnemyForSeries(Coop_Model_EnemyPop model, Enemy enemy)
	{
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestSeriesIndex((uint)model.seriesIdx);
		if (model.ownerClientId != MonoBehaviourSingleton<CoopManager>.I.coopMyClient.clientId)
		{
			if (enemy != null)
			{
				enemy.SetCoopMode(StageObject.COOP_MODE_TYPE.MIRROR, model.ownerClientId);
				enemy.isCoopInitialized = false;
				enemy.SafeActIdle();
				if (MonoBehaviourSingleton<SoundManager>.IsValid())
				{
					SoundManager.RequestBGM(MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestBGMID(), true);
				}
				OnPopEnemyLoadComplete(enemy, model.ownerClientId, false);
				return true;
			}
			enemy = MonoBehaviourSingleton<StageObjectManager>.I.CreateEnemyForSeries(model.sid, model.seriesIdx, delegate(Enemy target)
			{
				//IL_005a: Unknown result type (might be due to invalid IL or missing references)
				if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
				{
					MonoBehaviourSingleton<InGameRecorder>.I.RecordEnemyHP(target.id, target.hpMax);
				}
				OnPopEnemyLoadComplete(target, model.ownerClientId, true);
				if (model.seriesIdx > 0)
				{
					this.StartCoroutine(MonoBehaviourSingleton<StageObjectManager>.I.CreateNextEnemyForSeriesOfBattles(target));
				}
			});
			enemy.SetCoopMode(StageObject.COOP_MODE_TYPE.MIRROR, model.ownerClientId);
			return true;
		}
		if (enemy != null)
		{
			MonoBehaviourSingleton<StageObjectManager>.I.RemoveCacheObject(enemy);
			enemy.get_gameObject().SetActive(true);
			if (MonoBehaviourSingleton<SoundManager>.IsValid())
			{
				SoundManager.RequestBGM(MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestBGMID(), true);
			}
		}
		else
		{
			enemy = MonoBehaviourSingleton<StageObjectManager>.I.CreateEnemyForSeries(model.sid, model.seriesIdx, delegate(Enemy target)
			{
				//IL_0032: Unknown result type (might be due to invalid IL or missing references)
				//IL_0053: Unknown result type (might be due to invalid IL or missing references)
				if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
				{
					MonoBehaviourSingleton<InGameRecorder>.I.RecordEnemyHP(target.id, target.hpMax);
				}
				if (model.seriesIdx > 0)
				{
					target.get_gameObject().SetActive(false);
					this.StartCoroutine(MonoBehaviourSingleton<StageObjectManager>.I.CreateNextEnemyForSeriesOfBattles(enemy));
				}
			});
		}
		if (IsSolo())
		{
			enemy.SetCoopMode(StageObject.COOP_MODE_TYPE.NONE, 0);
		}
		else
		{
			enemy.SetCoopMode(StageObject.COOP_MODE_TYPE.ORIGINAL, 0);
		}
		if (enemy.controller == null)
		{
			enemy.AddController<EnemyController>();
		}
		if (enemy.isBoss && enemy.controller != null && !MonoBehaviourSingleton<CoopManager>.I.coopMyClient.IsBattleStart())
		{
			enemy.controller.SetEnableControll(false, ControllerBase.DISABLE_FLAG.BATTLE_START);
		}
		enemy.CheckFirstMadMode();
		enemy.SafeActIdle();
		if (!enemy.isSetAppearPos)
		{
			enemy.SetAppearPosEnemy();
		}
		return true;
	}

	public bool OnRecvEnemyBossPop(Coop_Model_EnemyBossPop model)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		if (!PopEnemy(model, out Enemy outEnmey))
		{
			fieldEnemyBossEntering = false;
			return false;
		}
		if (outEnmey == null)
		{
			fieldEnemyBossEntering = false;
			return false;
		}
		if (outEnmey.get_gameObject() != null)
		{
			outEnmey.get_gameObject().SetActive(false);
		}
		ClearSpecialEnemyExistAnnounce(ENEMY_POP_TYPE.FIELD_BOSS);
		this.StartCoroutine(StartEnemyBoss(model, outEnmey));
		return true;
	}

	public unsafe IEnumerator StartEnemyBoss(Coop_Model_EnemyBossPop model, Enemy enemy)
	{
		requestedEnemyBossAlive = false;
		if (!readyToPlay)
		{
			packetSender.SendEnemyBossAliveRequest();
			float requestTimer = 0f;
			while (!requestedEnemyBossAlive)
			{
				if (requestTimer > 5f)
				{
					requestedEnemyBossAlive = false;
					SetFalseEnemyBossBattleFlag();
					enemy.DestroyObject();
					yield break;
				}
				requestTimer += Time.get_deltaTime();
				yield return (object)null;
			}
			requestedEnemyBossAlive = false;
		}
		if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			MonoBehaviourSingleton<StageObjectManager>.I.SetFieldEnemyBoss(enemy);
			fieldEnemyBossEntering = true;
			while (!readyToPlay)
			{
				yield return (object)null;
			}
			if (enemy == null)
			{
				fieldEnemyBossEntering = false;
			}
			else if (isInFieldEnemyBossBattle)
			{
				fieldEnemyBossEntering = false;
				enemy.get_gameObject().SetActive(true);
			}
			else
			{
				bool isGimmickPop = false;
				FieldMapTable.EnemyPopTableData popData = Singleton<FieldMapTable>.I.GetEnemyPopData(MonoBehaviourSingleton<FieldManager>.I.currentMapID, model.popIndex);
				if (popData.enemyPopType == ENEMY_POP_TYPE.GIMMICK_POP || popData.enemyPopType == ENEMY_POP_TYPE.GIMMICK_POP_RARE)
				{
					if (!isInFieldFishingEnemyBattle)
					{
						ShowFieldFishingBossStartUI(popData.enemyPopType == ENEMY_POP_TYPE.GIMMICK_POP_RARE);
					}
					isInFieldFishingEnemyBattle = true;
					isGimmickPop = true;
				}
				else
				{
					ShowFieldEnemyBossStartUI();
					isInFieldFishingEnemyBattle = false;
				}
				isInFieldEnemyBossBattle = true;
				if (isEnterFieldEnemyBossBattle)
				{
					fieldEnemyBossEntering = false;
					isEnterFieldEnemyBossBattle = false;
					enemy.get_gameObject().SetActive(true);
				}
				else
				{
					int escapeTime = 300;
					if (popData != null)
					{
						escapeTime = popData.escapeTime;
					}
					if (MonoBehaviourSingleton<InGameProgress>.IsValid() && !isGimmickPop)
					{
						MonoBehaviourSingleton<InGameProgress>.I.ResetStartTimer((float)escapeTime, 0f);
					}
					yield return (object)this.StartCoroutine(LoadFieldEnemyEntryExitEffect());
					Transform effectTrans = EffectManager.GetEffect("ef_btl_enemy_entry_01", null);
					Vector3 effectPos = enemy._transform.get_localPosition();
					effectPos.y = StageManager.GetHeight(enemy._transform.get_position());
					effectTrans.set_localPosition(effectPos);
					enemy.get_gameObject().SetActive(true);
					enemy.hitOffFlag |= StageObject.HIT_OFF_FLAG.FORCE;
					GoUpCharacterFromUnderGround(enemy, new Action((object)/*Error near IL_0386: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
					this.StartCoroutine(LoadFieldTimesUpVictoryeffect());
				}
			}
		}
	}

	private void SetFalseEnemyBossBattleFlag()
	{
		isInFieldEnemyBossBattle = false;
		isInFieldFishingEnemyBattle = false;
		isEnterFieldEnemyBossBattle = false;
		fieldEnemyBossEntering = false;
	}

	private IEnumerator LoadFieldEnemyEntryExitEffect()
	{
		LoadingQueue loadingQueue = new LoadingQueue(this);
		loadingQueue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enemy_entry_01");
		while (loadingQueue.IsLoading())
		{
			yield return (object)loadingQueue.Wait();
		}
	}

	private unsafe IEnumerator LoadFieldTimesUpVictoryeffect()
	{
		if (!MonoBehaviourSingleton<InGameLinkResourcesFieldEnemyBoss>.IsValid() && MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			bool loadedEffectUI = false;
			MonoBehaviourSingleton<InGameProgress>.I.RealizesLinkResourcesFieldEnemyBoss(new Action((object)/*Error near IL_004b: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			int count = 100;
			while (loadedEffectUI && count > 0)
			{
				count--;
				yield return (object)null;
			}
		}
	}

	private IEnumerator LoadAndPlayVictoryEffect()
	{
		yield return (object)this.StartCoroutine(LoadFieldTimesUpVictoryeffect());
		while (!readyToPlay)
		{
			yield return (object)null;
		}
		MonoBehaviourSingleton<InGameProgress>.I.PlayFieldEnemyBossVictoryEffect();
	}

	public void ShowFieldEnemyBossStartUI()
	{
		if (MonoBehaviourSingleton<UIInGameFieldQuestWarning>.IsValid())
		{
			MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.Play(ENEMY_TYPE.NONE, 0, true);
			MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.FadeOut(4f, 1f, null);
		}
		if (MonoBehaviourSingleton<UIFieldBossInfo>.IsValid())
		{
			MonoBehaviourSingleton<UIFieldBossInfo>.I.FadeIn(0f, 1f, null);
		}
	}

	public void ShowFieldFishingBossStartUI(bool isRare)
	{
		if (MonoBehaviourSingleton<UIInGameFieldQuestWarning>.IsValid())
		{
			MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.PlayFieldFishingEnemy(isRare);
			MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.FadeOut(4f, 1f, null);
		}
	}

	public bool OnRecvEnemyBossEscape(Coop_Model_EnemyBossEscape model)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(EscapeEnemyBoss(model.sid));
		return true;
	}

	public void OnRecvEnemyBossAliveRequest(CoopPacket packet)
	{
		if (isInFieldEnemyBossBattle)
		{
			packetSender.SendEnemyBossAliveRequested(packet.fromClientId);
		}
	}

	public void OnRecvEnemyBossAliveRequested()
	{
		requestedEnemyBossAlive = true;
	}

	public void EscapeHostEnmeyBoss()
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<StageObjectManager>.I.fieldEnemyBoss == null)
		{
			isInFieldEnemyBossBattle = false;
			isInFieldFishingEnemyBattle = false;
		}
		else
		{
			int id = MonoBehaviourSingleton<StageObjectManager>.I.fieldEnemyBoss.id;
			packetSender.SendEnemyBossEscape(id, true);
			this.StartCoroutine(EscapeEnemyBoss(id));
		}
	}

	private unsafe IEnumerator EscapeEnemyBoss(int sid)
	{
		if (isInFieldEnemyBossBattle)
		{
			isInFieldEnemyBossBattle = false;
			isInFieldFishingEnemyBattle = false;
			while (!readyToPlay)
			{
				yield return (object)null;
			}
			yield return (object)this.StartCoroutine(LoadFieldEnemyEntryExitEffect());
			while (fieldEnemyBossEntering)
			{
				yield return (object)null;
			}
			Enemy enemy = null;
			if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
			{
				enemy = (MonoBehaviourSingleton<StageObjectManager>.I.FindEnemy(sid) as Enemy);
			}
			if (enemy != null)
			{
				enemy.PrepareVanishLocal();
			}
			if (enemy != null)
			{
				Transform effectTrans = EffectManager.GetEffect("ef_btl_enemy_entry_01", null);
				Vector3 effectPos = enemy._transform.get_localPosition();
				effectPos.y = StageManager.GetHeight(enemy._transform.get_position());
				effectTrans.set_localPosition(effectPos);
				GoDownCharacterToUnderGround(enemy, new Action((object)/*Error near IL_018f: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
				yield return (object)this.StartCoroutine(LoadFieldTimesUpVictoryeffect());
				MonoBehaviourSingleton<InGameProgress>.I.PlayFieldEnemyBossTimesUpEffect();
				enemy.OnEndEscape();
			}
		}
	}

	public bool OnRecvWaveMatchInfo(Coop_Model_WaveMatchInfo model)
	{
		if (model.no == 1)
		{
			firstWaveMatchInfo = model;
			firstWaveMatchPopSec = (float)model.popGuardSec;
		}
		firstWaveMatchSet = MonoBehaviourSingleton<UIWaveMatchAnnounce>.IsValid();
		if (firstWaveMatchSet)
		{
			MonoBehaviourSingleton<UIWaveMatchAnnounce>.I.Announce(model);
		}
		return true;
	}

	private void CheckFirstWaveMatchInfo()
	{
		if (firstWaveMatchInfo != null)
		{
			firstWaveMatchPopSec -= Time.get_deltaTime();
			if (firstWaveMatchPopSec <= 0f)
			{
				firstWaveMatchInfo = null;
			}
		}
		if (!firstWaveMatchSet && MonoBehaviourSingleton<UIWaveMatchAnnounce>.IsValid())
		{
			firstWaveMatchInfo.popGuardSec = Mathf.FloorToInt(firstWaveMatchPopSec);
			MonoBehaviourSingleton<UIWaveMatchAnnounce>.I.Announce(firstWaveMatchInfo);
			firstWaveMatchSet = true;
		}
	}

	private void ResetBGM()
	{
		if (MonoBehaviourSingleton<FieldManager>.IsValid() && MonoBehaviourSingleton<SoundManager>.IsValid())
		{
			SoundManager.RequestBGM(MonoBehaviourSingleton<FieldManager>.I.GetCurrentMapBGMID(), true);
		}
	}

	public unsafe void GoUpCharacterFromUnderGround(Enemy enemy, Action OnEndAction)
	{
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Expected O, but got Unknown
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		if (enemy.controller != null)
		{
			enemy.controller.SetEnableControll(false, ControllerBase.DISABLE_FLAG.DEFAULT);
		}
		float time = 3f;
		if (isInFieldFishingEnemyBattle)
		{
			time = MonoBehaviourSingleton<InGameSettingsManager>.I.fishingParam.hitEnemyMoveSec;
		}
		_003CGoUpCharacterFromUnderGround_003Ec__AnonStorey4DF _003CGoUpCharacterFromUnderGround_003Ec__AnonStorey4DF;
		this.StartCoroutine(SimpleMoveCharacterY(enemy, -10f, StageManager.GetHeight(enemy._transform.get_position()), time, new Action((object)_003CGoUpCharacterFromUnderGround_003Ec__AnonStorey4DF, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)));
	}

	public unsafe void GoDownCharacterToUnderGround(Enemy enemy, Action OnEndAction)
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Expected O, but got Unknown
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		if (enemy.controller != null)
		{
			enemy.controller.SetEnableControll(false, ControllerBase.DISABLE_FLAG.DEFAULT);
		}
		enemy.PrepareVanishLocal();
		enemy.ActIdle(false, -1f);
		Enemy enemy2 = enemy;
		Vector3 position = enemy._transform.get_position();
		_003CGoDownCharacterToUnderGround_003Ec__AnonStorey4E0 _003CGoDownCharacterToUnderGround_003Ec__AnonStorey4E;
		this.StartCoroutine(SimpleMoveCharacterY(enemy2, position.y, -10f, 3f, new Action((object)_003CGoDownCharacterToUnderGround_003Ec__AnonStorey4E, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)));
	}

	public IEnumerator SimpleMoveCharacterY(Character enemy, float from, float to, float time, Action OnEndAction)
	{
		enemy.onTheGround = false;
		Vector3 startPos = enemy._transform.get_position();
		startPos.y = from;
		enemy._transform.set_position(startPos);
		float speed = (to - from) / time;
		float elapsedTime = 0f;
		while (true)
		{
			if (enemy == null || enemy._transform == null)
			{
				yield break;
			}
			Vector3 pos = enemy._transform.get_position();
			pos.y += speed * Time.get_deltaTime();
			enemy._transform.set_position(pos);
			elapsedTime += Time.get_deltaTime();
			if (elapsedTime > time)
			{
				break;
			}
			yield return (object)null;
		}
		enemy.onTheGround = true;
		OnEndAction.SafeInvoke();
	}

	public void OnDefeatFieldEnemyBoss()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<InGameProgress>.IsValid() && isInFieldEnemyBossBattle)
		{
			this.StartCoroutine(LoadAndPlayVictoryEffect());
			isInFieldEnemyBossBattle = false;
			isInFieldFishingEnemyBattle = false;
			MonoBehaviourSingleton<InGameProgress>.I.StopTimer();
			MonoBehaviourSingleton<InGameProgress>.I.SetLimitTime(0f);
			if (MonoBehaviourSingleton<UIFieldBossInfo>.IsValid())
			{
				MonoBehaviourSingleton<UIFieldBossInfo>.I.FadeOut(0f, 1f, null);
			}
			ResetBGM();
		}
	}

	protected void OnPopEnemyLoadComplete(Enemy enemy, int client_id, bool init = true)
	{
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
		{
			MonoBehaviourSingleton<InGameRecorder>.I.RecordEnemyHP(enemy.id, enemy.hpMax);
		}
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && MonoBehaviourSingleton<KtbWebSocket>.I.IsConnected())
		{
			if (enemy.IsOriginal() || enemy.IsCoopNone())
			{
				Logd("EnemyLoadComplete skip. became original in loading. enemy={0},client={1}", enemy.id, client_id);
			}
			else
			{
				enemy.RemoveController();
				if (enemy.isCoopInitialized)
				{
					Logd("EnemyLoadComplete skip. already initilized. enemy={0},client={1}", enemy.id, client_id);
				}
				else
				{
					Logd("EnemyLoadComplete. enemy={0},client={1}", enemy.id, client_id);
					if (init)
					{
						enemy.get_gameObject().SetActive(false);
						MonoBehaviourSingleton<StageObjectManager>.I.AddCacheObject(enemy);
					}
					enemy.packetReceiver.SetFilterMode(ObjectPacketReceiver.FILTER_MODE.WAIT_INITIALIZE);
					enemy.enemySender.OnLoadComplete(true);
				}
			}
		}
	}

	public bool OnRecvStageInfo(Coop_Model_StageInfo model, CoopPacket packet)
	{
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		if (!isActivateStart)
		{
			return false;
		}
		Logd("Responsed StageInfo from {0}. elapsedTime={1},gimmicks={2}", packet.fromClientId, model.elapsedTime, model.gimmicks.Count);
		isRecvStageInfo = true;
		if (MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			CoopClient coopClient = MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.FindByClientId(packet.fromClientId);
			if (coopClient != null && coopClient.IsBattleStart())
			{
				if ((MonoBehaviourSingleton<InGameManager>.I.IsRush() || QuestManager.IsValidInGameWaveMatch(false)) && model.rushLimitTime >= 0f)
				{
					MonoBehaviourSingleton<InGameProgress>.I.SetLimitTime(model.rushLimitTime);
				}
				MonoBehaviourSingleton<InGameProgress>.I.StartTimer(model.elapsedTime);
			}
			isInFieldFishingEnemyBattle = model.isInFieldFishingEnemyBattle;
			if (model.isInFieldEnemyBossBattle)
			{
				isEnterFieldEnemyBossBattle = true;
				if (!isInFieldFishingEnemyBattle)
				{
					MonoBehaviourSingleton<InGameProgress>.I.ResetStartTimer(model.rushLimitTime, model.elapsedTime);
				}
			}
		}
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			model.gimmicks.ForEach(delegate(Coop_Model_StageInfo.GimmickInfo gimmick)
			{
				//IL_001e: Unknown result type (might be due to invalid IL or missing references)
				StageObject stageObject = MonoBehaviourSingleton<StageObjectManager>.I.FindGimmick(gimmick.id);
				if (stageObject != null)
				{
					stageObject.get_gameObject().SetActive(gimmick.enable);
					stageObject.SetCoopMode(StageObject.COOP_MODE_TYPE.MIRROR, packet.fromClientId);
				}
			});
			if (!MonoBehaviourSingleton<StageObjectManager>.I.waveTargetList.IsNullOrEmpty() && !model.waveTargets.IsNullOrEmpty())
			{
				for (int i = 0; i < MonoBehaviourSingleton<StageObjectManager>.I.waveTargetList.Count; i++)
				{
					FieldWaveTargetObject fieldWaveTargetObject = MonoBehaviourSingleton<StageObjectManager>.I.waveTargetList[i] as FieldWaveTargetObject;
					if (!(fieldWaveTargetObject == null))
					{
						bool flag = false;
						for (int j = 0; j < model.waveTargets.Count; j++)
						{
							Coop_Model_StageInfo.WaveTargetInfo waveTargetInfo = model.waveTargets[j];
							if (fieldWaveTargetObject.id == waveTargetInfo.id)
							{
								fieldWaveTargetObject.SetHp(waveTargetInfo.hp, fieldWaveTargetObject.maxHp, true);
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							fieldWaveTargetObject.SetHp(0, fieldWaveTargetObject.maxHp, true);
						}
					}
				}
			}
		}
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && MonoBehaviourSingleton<StageObjectManager>.I.self != null && !MonoBehaviourSingleton<StageObjectManager>.I.self.isSetAppearPos)
		{
			MonoBehaviourSingleton<StageObjectManager>.I.self.SetAppearPosGuest(model.enemyPos);
		}
		return true;
	}

	public virtual bool OnRecvStageResponseEnd(Coop_Model_StageResponseEnd model, CoopPacket packet)
	{
		CoopClient coopClient = MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.FindByClientId(packet.fromClientId);
		if (!(coopClient == null))
		{
			if (coopClient.isStageHost)
			{
				isHostStageResponseEnd = true;
			}
			coopClient.isStageResponseEnd = true;
			STAGE_REQUEST_ERROR error_id = (STAGE_REQUEST_ERROR)model.error_id;
			Logd("Responsed End from {0}. isStageHost={1}. error={2}.", packet.fromClientId, coopClient.isStageHost, error_id);
			switch (error_id)
			{
			case STAGE_REQUEST_ERROR.ENTRY_CLOSE:
			case STAGE_REQUEST_ERROR.STAGE_CLOSE_FAILED:
			case STAGE_REQUEST_ERROR.STAGE_CLOSE_SUCCEED:
				if (QuestManager.IsValidInGameExplore() && error_id == STAGE_REQUEST_ERROR.STAGE_CLOSE_SUCCEED)
				{
					isQuestClose = true;
					isQuestSucceed = true;
					return true;
				}
				if (MonoBehaviourSingleton<CoopManager>.I.coopRoom.isOwnerFirstClear)
				{
					isQuestClose = true;
					isQuestSucceed = (error_id == STAGE_REQUEST_ERROR.STAGE_CLOSE_SUCCEED);
				}
				MonoBehaviourSingleton<CoopNetworkManager>.I.LoopBackRoomLeave(false);
				return true;
			case STAGE_REQUEST_ERROR.DEACTIVATE:
				return true;
			default:
				if (!coopClient.isSendPopOriginal)
				{
					SendOriginalObjectPop(packet.fromClientId);
					coopClient.isSendPopOriginal = true;
				}
				return true;
			}
		}
		return true;
	}

	public bool OnRecvRequestPop(Coop_Model_StageRequestPop model, CoopPacket packet)
	{
		if (!isActivateStart)
		{
			return false;
		}
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return false;
		}
		if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			return false;
		}
		if (MonoBehaviourSingleton<InGameProgress>.I.progressEndType != 0)
		{
			return true;
		}
		if (model.isSelf && model.isPlayer)
		{
			Player self = MonoBehaviourSingleton<StageObjectManager>.I.self;
			if (self != null && (self.IsOriginal() || self.IsCoopNone()))
			{
				packetSender.SendStagePlayerPop(self, packet.fromClientId);
				Logd("StageRequest response SelfPop({0}). to={1}", self.id, packet.fromClientId);
			}
		}
		return true;
	}

	public void CheckEntryClose(bool force = false)
	{
		if (!isEntryClose && isActivateStart && QuestManager.IsValidInGame())
		{
			int num = 0;
			if (force)
			{
				num = 10;
			}
			if (MonoBehaviourSingleton<CoopManager>.I.coopMyClient.IsBattleStart() && MonoBehaviourSingleton<StageObjectManager>.IsValid() && MonoBehaviourSingleton<StageObjectManager>.I.boss != null && MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
			{
				Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
				int num2 = (int)((float)boss.hpMax * MonoBehaviourSingleton<InGameSettingsManager>.I.room.entryCloseEnemyHpRate);
				if ((boss.IsCoopNone() || boss.IsOriginal()) && boss.hp < num2)
				{
					num = 1;
				}
			}
			if (MonoBehaviourSingleton<CoopManager>.I.coopMyClient.IsBattleStart() && MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
			{
				int num3 = (int)(MonoBehaviourSingleton<InGameProgress>.I.limitTime * MonoBehaviourSingleton<InGameSettingsManager>.I.room.entryCloseTimeRate);
				if ((float)num3 > MonoBehaviourSingleton<InGameProgress>.I.remaindTime)
				{
					num = 2;
				}
			}
			if (num != 0)
			{
				isEntryClose = true;
				MonoBehaviourSingleton<CoopNetworkManager>.I.RoomEntryClose(num);
			}
		}
	}

	public void SetQuestClose(bool is_succeed)
	{
		Logd("SetQuestClose. is_succeed={0}", is_succeed);
		isQuestClose = true;
		isQuestSucceed = is_succeed;
		if (MonoBehaviourSingleton<CoopManager>.I.coopRoom.isOwnerFirstClear && MonoBehaviourSingleton<CoopManager>.I.coopMyClient.isPartyOwner)
		{
			packetSender.SendStageQuestClose(is_succeed);
		}
	}

	public void SetRetireQuestClose()
	{
		Logd("SetRetireQuestClose. ");
		for (int i = 0; i < 8; i++)
		{
			CoopClient at = MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.GetAt(i);
			if (Object.op_Implicit(at) && !(at is CoopMyClient) && !at.isBattleRetire)
			{
				return;
			}
		}
		CheckEntryClose(true);
		isQuestClose = true;
		isQuestSucceed = false;
		packetSender.SendStageQuestClose(false);
	}

	public bool OnRecvQuestClose(bool is_succeed)
	{
		if (!isActivateStart)
		{
			return false;
		}
		Logd("OnRecvQuestClose. is_succeed={0}", is_succeed);
		if (MonoBehaviourSingleton<InGameProgress>.IsValid() && !MonoBehaviourSingleton<InGameProgress>.I.isBattleStart)
		{
			isQuestClose = true;
			isQuestSucceed = is_succeed;
			MonoBehaviourSingleton<CoopManager>.I.coopRoom.SwitchOfflinePlay();
		}
		return true;
	}

	public void StageTimeup()
	{
		Logd("StageTimeup.");
		packetSender.SendStageTimeup();
	}

	public bool OnRecvStageTimeup()
	{
		Logd("OnRecvStageTimeup.");
		if (MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			return MonoBehaviourSingleton<InGameProgress>.I.BattleTimeup();
		}
		return true;
	}

	public void SetChatConnection(ChatCoopConnection chat_connection)
	{
		chatConnection = chat_connection;
	}

	public void StageChat(int chara_id, int chat_id)
	{
		packetSender.SendStageChat(chara_id, chat_id);
	}

	public bool OnRecvStageChat(Coop_Model_StageChat model)
	{
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return false;
		}
		Character character = MonoBehaviourSingleton<StageObjectManager>.I.FindCharacter(model.chara_id) as Character;
		if (character != null)
		{
			character.ChatSay(model.chat_id);
		}
		return true;
	}

	public void SendChatMessage(int chara_id, string message)
	{
		packetSender.SendChatMessage(chara_id, message);
	}

	public void SendChatStamp(int chara_id, int stamp_id)
	{
		packetSender.SendChatStamp(chara_id, stamp_id);
	}

	public bool OnRecvChatMessage(int fromClientId, Coop_Model_StageChatMessage model)
	{
		Character character = null;
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			character = (MonoBehaviourSingleton<StageObjectManager>.I.FindCharacter(model.chara_id) as Character);
			if (character != null)
			{
				character.ChatSay(model.text);
			}
			else if (QuestManager.IsValidInGameExplore())
			{
				ExplorePlayerStatus explorePlayerStatus = MonoBehaviourSingleton<QuestManager>.I.GetExplorePlayerStatus(model.user_id);
				if (MonoBehaviourSingleton<UIInGameMessageBar>.IsValid() && MonoBehaviourSingleton<UIInGameMessageBar>.I.get_isActiveAndEnabled() && explorePlayerStatus != null)
				{
					MonoBehaviourSingleton<UIInGameMessageBar>.I.Announce(explorePlayerStatus.userName, model.text);
				}
			}
		}
		if (chatConnection != null)
		{
			CoopClient coopClient = MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.FindByClientId(fromClientId);
			if (coopClient != null)
			{
				chatConnection.OnReceiveMessage(model.user_id, coopClient.GetPlayerName(), model.text);
			}
			else if (character != null)
			{
				chatConnection.OnReceiveMessage(model.user_id, character.charaName, model.text);
			}
		}
		return true;
	}

	public bool OnRecvChatStamp(Coop_Model_StageChatStamp model)
	{
		Character character = null;
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			character = (MonoBehaviourSingleton<StageObjectManager>.I.FindCharacter(model.chara_id) as Character);
			if (character != null)
			{
				character.ChatSayStamp(model.stamp_id);
			}
		}
		if (chatConnection != null)
		{
			CoopClient coopClient = MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.FindByPlayerId(model.chara_id);
			if (coopClient != null)
			{
				chatConnection.OnReceiveStamp(model.user_id, coopClient.GetPlayerName(), model.stamp_id);
			}
			else if (character != null)
			{
				chatConnection.OnReceiveStamp(model.user_id, character.charaName, model.stamp_id);
			}
		}
		return true;
	}

	public bool OnRecvEnemyDefeat(Coop_Model_EnemyDefeat model)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		if (!isActivateStart)
		{
			return false;
		}
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			Enemy enemy = MonoBehaviourSingleton<StageObjectManager>.I.FindEnemy(model.sid) as Enemy;
			if (enemy != null)
			{
				enemy.StopForceEnemyOut();
				Vector3 position = enemy._position;
				model.x = (int)position.x;
				model.z = (int)position.z;
			}
		}
		bool isDefeatFieldDelivery = false;
		if (MonoBehaviourSingleton<InGameManager>.IsValid())
		{
			List<InGameManager.DropDeliveryInfo> list = null;
			MonoBehaviourSingleton<InGameManager>.I.CreateDropInfoList(model, out List<InGameManager.DropDeliveryInfo> deliveryList, out List<InGameManager.DropItemInfo> itemList);
			List<InGameManager.DropDeliveryInfo> deliveryList2 = deliveryList.FindAll((InGameManager.DropDeliveryInfo d) => !d.IsCountUpAtDefeatFieldEnemy());
			MonoBehaviourSingleton<InGameManager>.I.CreateDropObject(model, deliveryList2, itemList);
			if (!model.dropLoungeShare)
			{
				list = deliveryList.FindAll((InGameManager.DropDeliveryInfo d) => d.IsCountUpAtDefeatFieldEnemy());
			}
			if (IsNeededCountDefeatFieldDelivery(list, model))
			{
				DefeatFieldEnemyDelivery defeatFieldEnemyDelivery = new DefeatFieldEnemyDelivery();
				defeatFieldEnemyDelivery.rewardId = model.rewardId2;
				defeatFieldEnemyDelivery.deliveryList = list;
				DefeatFieldEnemyDelivery defeatFieldEnemyDelivery2 = defeatFieldEnemyDelivery;
				defeatFieldEnemyDeliveryList.Add(defeatFieldEnemyDelivery2);
				MonoBehaviourSingleton<CoopNetworkManager>.I.RewardGet(defeatFieldEnemyDelivery2.rewardId);
			}
			isDefeatFieldDelivery = (list != null && list.Count > 0);
		}
		fieldRewardPool.AddEnemyDefeat(model, isDefeatFieldDelivery);
		if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
		{
			UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
			userStatus.exp = (int)userStatus.exp + model.exp;
			int num = Singleton<UserLevelTable>.I.GetLevelTable(Singleton<UserLevelTable>.I.GetMaxLevel()).needExp;
			if ((int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.exp > num)
			{
				MonoBehaviourSingleton<UserInfoManager>.I.userStatus.exp = num;
			}
			MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money += model.money;
			if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money > MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.MONEY_MAX)
			{
				MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.MONEY_MAX;
			}
			if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.ExpProgress01 >= 1f)
			{
				fieldRewardPool.SendFieldDrop(null);
			}
		}
		if (MonoBehaviourSingleton<FieldManager>.IsValid())
		{
			bool flag = true;
			if (!TutorialStep.HasFirstDeliveryCompleted())
			{
				flag = false;
			}
			if (flag)
			{
				FieldMapPortalInfo portalPointToPortalInfo = MonoBehaviourSingleton<FieldManager>.I.GetPortalPointToPortalInfo();
				if (QuestManager.IsValidInGameExplore())
				{
					if (portalPointToPortalInfo != null)
					{
						int point = portalPointToPortalInfo.GetNowPortalPoint() + model.ppt;
						MonoBehaviourSingleton<CoopManager>.I.coopRoom.packetSender.SendUpdatePortalPoint((int)portalPointToPortalInfo.portalData.portalID, point, model.x, model.z);
						MonoBehaviourSingleton<QuestManager>.I.UpdateExplorePortalPoint((int)portalPointToPortalInfo.portalData.portalID, point, model.x, model.z);
					}
				}
				else
				{
					if (MonoBehaviourSingleton<FieldManager>.I.AddPortalPointToPortalInfo(model.ppt))
					{
						fieldRewardPool.SendFieldDrop(null);
					}
					if (portalPointToPortalInfo != null && MonoBehaviourSingleton<InGameProgress>.IsValid())
					{
						MonoBehaviourSingleton<InGameProgress>.I.CreatePortalPoint(portalPointToPortalInfo, model);
					}
				}
			}
		}
		if (model.money > 0)
		{
			Vector3 pos = default(Vector3);
			pos._002Ector((float)model.x, 0f, (float)model.z);
			EffectManager.OneShot("ef_btl_drop_coin_01", pos, Quaternion.get_identity(), false);
			SoundManager.PlayOneShotSE(10000065, pos);
		}
		return true;
	}

	private bool IsNeededCountDefeatFieldDelivery(List<InGameManager.DropDeliveryInfo> _defeatFieldEnemyDelivery, Coop_Model_EnemyDefeat model)
	{
		if (_defeatFieldEnemyDelivery == null)
		{
			return false;
		}
		if (_defeatFieldEnemyDelivery.Count <= 0)
		{
			return false;
		}
		if (GameDefine.IsFieldBossTreasureBox(model.boxType) && model.exp <= 0)
		{
			return false;
		}
		return true;
	}

	public bool OnRecvRewardPickup(Coop_Model_RewardPickup model)
	{
		if (!isActivateStart)
		{
			return false;
		}
		if (model.rewardKeyId > 0)
		{
			fieldRewardPool.AddRewardPickup(model);
		}
		else
		{
			fieldRewardPool.ExpireRewardPickup(model);
		}
		if (!MonoBehaviourSingleton<InGameManager>.IsValid())
		{
			return true;
		}
		DefeatFieldEnemyDelivery defeatFieldEnemyDelivery = defeatFieldEnemyDeliveryList.Find((DefeatFieldEnemyDelivery d) => d.rewardId == model.rewardId);
		if (defeatFieldEnemyDelivery == null)
		{
			MonoBehaviourSingleton<InGameManager>.I.DeleteDropObject(model.rewardId, model.rewardKeyId > 0);
			return true;
		}
		if (MonoBehaviourSingleton<UIDropAnnounce>.IsValid() && defeatFieldEnemyDelivery != null)
		{
			defeatFieldEnemyDeliveryList.Remove(defeatFieldEnemyDelivery);
			List<UIDropAnnounce.DropAnnounceInfo> list = MonoBehaviourSingleton<InGameManager>.I.CreateDropAnnounceInfoList(defeatFieldEnemyDelivery.deliveryList, new List<InGameManager.DropItemInfo>(), false);
			int i = 0;
			for (int count = list.Count; i < count; i++)
			{
				MonoBehaviourSingleton<UIDropAnnounce>.I.Announce(list[i]);
			}
		}
		return true;
	}

	public bool OnRecvEnemyExtermination(Coop_Model_EnemyExtermination model)
	{
		Logd("EnemyExtermination!");
		if (QuestManager.IsValidInGameExplore())
		{
			return true;
		}
		if (MonoBehaviourSingleton<InGameManager>.I.IsRush())
		{
			isEnemyExtermination = true;
			return true;
		}
		if (!MonoBehaviourSingleton<CoopManager>.I.coopMyClient.IsBattleStart())
		{
			MonoBehaviourSingleton<CoopNetworkManager>.I.LoopBackRoomLeave(false);
		}
		else
		{
			isEnemyExtermination = true;
		}
		return true;
	}

	public bool OnRecvEventHappenQuest(Coop_Model_EventHappenQuest model)
	{
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			return true;
		}
		uint qId = (uint)model.qId;
		QuestInfoData.Quest.Reward[] reward = null;
		if (model.rewards.Count > 0)
		{
			int i = 0;
			reward = new QuestInfoData.Quest.Reward[model.rewards.Count];
			model.rewards.ForEach(delegate(List<int> r)
			{
				reward[++i] = new QuestInfoData.Quest.Reward(r[0], r[1], r[2]);
			});
		}
		if (MonoBehaviourSingleton<InGameManager>.IsValid())
		{
			PortalUnlockEvent component = MonoBehaviourSingleton<InGameManager>.I.get_gameObject().GetComponent<PortalUnlockEvent>();
			if (component != null)
			{
				Object.Destroy(component);
			}
			MonoBehaviourSingleton<InGameManager>.I.OpenAllDropObject();
		}
		MonoBehaviourSingleton<InGameProgress>.I.HappenQuestDirection(qId, reward, model.rareBossType);
		return true;
	}

	public void OnRecvSyncTimeRequest(Coop_Model_StageSyncTimeRequest model, int toClientId)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			this.StartCoroutine(DoStageSyncTime(model, toClientId));
		}
	}

	private IEnumerator DoStageSyncTime(Coop_Model_StageSyncTimeRequest model, int toClientId)
	{
		yield return (object)new WaitForSeconds(2f);
		float elapsedTime = MonoBehaviourSingleton<InGameProgress>.I.GetElapsedTime();
		packetSender.SendStageSyncTime(elapsedTime, toClientId);
	}

	public void OnRecvSyncTime(Coop_Model_StageSyncTime model)
	{
		if (MonoBehaviourSingleton<InGameProgress>.IsValid() && !(model.elapsedTime < MonoBehaviourSingleton<InGameProgress>.I.GetElapsedTime()))
		{
			MonoBehaviourSingleton<InGameProgress>.I.SetElapsedTime(model.elapsedTime);
		}
	}

	public void OnRecvSyncPlayerRecord(Coop_Model_StageSyncPlayerRecord model)
	{
		if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
		{
			MonoBehaviourSingleton<InGameRecorder>.I.ApplySyncHostData(model.rec);
		}
	}

	public unsafe void SendSyncPlayerRecord(int toClientId, bool promise = true)
	{
		if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
		{
			List<InGameRecorder.PlayerRecordSyncHost> list = MonoBehaviourSingleton<InGameRecorder>.I.CreateSyncHostData();
			int i = 0;
			for (int count = list.Count; i < count; i++)
			{
				InGameRecorder.PlayerRecordSyncHost playerRecordSyncHost = list[i];
				CoopStagePacketSender packetSender = this.packetSender;
				InGameRecorder.PlayerRecordSyncHost record = playerRecordSyncHost;
				if (_003C_003Ef__am_0024cache2D == null)
				{
					_003C_003Ef__am_0024cache2D = new Func<Coop_Model_Base, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
				}
				packetSender.SendSyncPlayerRecord(record, toClientId, promise, _003C_003Ef__am_0024cache2D);
			}
		}
	}

	public void OnRecvRushRequested(Coop_Model_RushRequested model)
	{
		if (forceNextWave && !isRecvRushRequested)
		{
			int rushIndex = MonoBehaviourSingleton<InGameManager>.I.GetRushIndex();
			if (model.currentWaveIndex > rushIndex)
			{
				isRecvRushRequested = true;
				MonoBehaviourSingleton<InGameProgress>.I.StartTimer(0f);
				MonoBehaviourSingleton<InGameProgress>.I.StopTimer();
				MonoBehaviourSingleton<InGameProgress>.I.SetElapsedTime(model.syncData.elapsedTime);
				bossBreakIDLists[0] = model.syncData.bossBreakIds;
			}
		}
	}

	private void ForceProgressNextWave()
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		Logd("ForceProgressNextWave current:{0}, other:{1}", MonoBehaviourSingleton<InGameManager>.I.GetRushIndex(), GetOtherRushIndex());
		this.StartCoroutine(_ForceProgressNextWave());
	}

	private IEnumerator _ForceProgressNextWave()
	{
		forceNextWave = true;
		isRecvRushRequested = false;
		MonoBehaviourSingleton<CoopManager>.I.coopRoom.SendRushRequest();
		float time = Time.get_time();
		while (!isRecvRushRequested)
		{
			yield return (object)null;
			if (Time.get_time() - time > 5f)
			{
				Logd("Timeout RushRequest");
				bossBreakIDLists[0].Add(0);
				break;
			}
		}
		isRecvRushRequested = true;
		MonoBehaviourSingleton<InGameProgress>.I.BattleComplete(true);
	}

	private int GetOtherRushIndex()
	{
		int otherRushIndex = -1;
		MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.ForEach(delegate(CoopClient client)
		{
			if (client.rushIndex > otherRushIndex)
			{
				otherRushIndex = client.rushIndex;
			}
		});
		return otherRushIndex;
	}

	private bool IsOtherClientProgressed()
	{
		return MonoBehaviourSingleton<InGameManager>.I.IsRush() && GetOtherRushIndex() > MonoBehaviourSingleton<InGameManager>.I.GetRushIndex();
	}

	public bool OnRecvStageObjectInfo(Coop_Model_StageObjectInfo model, CoopPacket packet)
	{
		if (!isActivateStart)
		{
			return false;
		}
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return true;
		}
		CoopClient coopClient = MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.FindByClientId(packet.fromClientId);
		if (coopClient == null)
		{
			return true;
		}
		Logd("Responsed StageObjectInfo from {0}. sid={1}, CoopMode:{2}, userid={3}", packet.fromClientId, model.StageObjectID, model.CoopModeType, coopClient.userId);
		StageObject stageObject = MonoBehaviourSingleton<StageObjectManager>.I.FindObject(model.StageObjectID);
		if (stageObject == null)
		{
			return false;
		}
		stageObject.SetCoopMode(model.CoopModeType, packet.fromClientId);
		return true;
	}
}
