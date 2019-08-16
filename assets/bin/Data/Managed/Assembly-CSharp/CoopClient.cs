using Network;
using System.Collections.Generic;
using UnityEngine;

public class CoopClient : MonoBehaviour
{
	public enum CLIENT_STATUS
	{
		NONE,
		STAGE_START,
		LOADING_START,
		LOADING_FINISH,
		STAGE_REQUEST,
		BATTLE_START,
		BATTLE_END
	}

	public enum CLIENT_JOIN_TYPE
	{
		NONE,
		FROM_QUEST_LIST,
		FROM_FIELD
	}

	public CLIENT_STATUS status
	{
		get;
		protected set;
	}

	public bool isLeave
	{
		get;
		protected set;
	}

	public CLIENT_JOIN_TYPE joinType
	{
		get;
		protected set;
	}

	public int clientId
	{
		get;
		protected set;
	}

	public int userId
	{
		get;
		protected set;
	}

	public string userToken
	{
		get;
		protected set;
	}

	public int slotIndex
	{
		get;
		protected set;
	}

	public CharaInfo userInfo
	{
		get;
		protected set;
	}

	public bool isPartyOwner
	{
		get;
		protected set;
	}

	public int loadingPer
	{
		get;
		protected set;
	}

	public int continueCount
	{
		get;
		protected set;
	}

	public int stageId
	{
		get;
		protected set;
	}

	public int seriesIndex
	{
		get;
		protected set;
	}

	public int exploreMapIndex => seriesIndex;

	public int rushIndex => seriesIndex;

	public bool isStageHost
	{
		get;
		protected set;
	}

	public int playerId
	{
		get;
		protected set;
	}

	public bool isSendPopOriginal
	{
		get;
		set;
	}

	public bool isSendStageInfo
	{
		get;
		set;
	}

	public bool isSeriesProgressEnd
	{
		get;
		protected set;
	}

	public bool isBattleRetire
	{
		get;
		protected set;
	}

	public CoopClientPacketReceiver packetReceiver
	{
		get;
		private set;
	}

	public int cachePlayerID
	{
		get;
		private set;
	}

	public List<int> cacheSubPlayerIDs
	{
		get;
		private set;
	}

	public bool isStageResponseEnd
	{
		get;
		set;
	}

	public CoopClient()
		: this()
	{
		status = CLIENT_STATUS.NONE;
		slotIndex = -1;
		cacheSubPlayerIDs = new List<int>();
	}

	protected virtual void Awake()
	{
		packetReceiver = this.get_gameObject().AddComponent<CoopClientPacketReceiver>();
	}

	protected virtual void Update()
	{
		packetReceiver.OnUpdate();
	}

	protected virtual void Logd(string str, params object[] objs)
	{
		if (!Log.enabled)
		{
		}
	}

	public override string ToString()
	{
		return "CoopClient[" + slotIndex + "](" + status + "/" + isPartyOwner + "/" + isStageHost + ").userId=" + userId;
	}

	public void Clear()
	{
		status = CLIENT_STATUS.NONE;
		isLeave = false;
		clientId = 0;
		userId = 0;
		userToken = string.Empty;
		slotIndex = -1;
		userInfo = null;
		isPartyOwner = false;
		loadingPer = 0;
		continueCount = 0;
		stageId = 0;
		seriesIndex = 0;
		isStageHost = false;
		playerId = 0;
		isSendPopOriginal = false;
		isSendStageInfo = false;
		isSeriesProgressEnd = false;
		isBattleRetire = false;
		cachePlayerID = 0;
		cacheSubPlayerIDs.Clear();
		isStageResponseEnd = false;
		joinType = CLIENT_JOIN_TYPE.NONE;
	}

	public void OnStageChangeInterval()
	{
		playerId = 0;
		isSendPopOriginal = false;
		isSendStageInfo = false;
		isSeriesProgressEnd = false;
		isBattleRetire = false;
		cachePlayerID = 0;
		cacheSubPlayerIDs.Clear();
		isStageResponseEnd = false;
		loadingPer = 0;
		SetStatus(CLIENT_STATUS.STAGE_START);
	}

	public void OnQuestSeriesInterval()
	{
		playerId = 0;
		isSendPopOriginal = false;
		isSendStageInfo = false;
		isSeriesProgressEnd = false;
		cachePlayerID = 0;
		cacheSubPlayerIDs.Clear();
		isStageResponseEnd = false;
		loadingPer = 0;
		SetStatus(CLIENT_STATUS.STAGE_START);
	}

	public virtual void Init(int client_id)
	{
		Clear();
		clientId = client_id;
	}

	public void Activate(int user_id, string token, CharaInfo user_info, int slot_index)
	{
		userId = user_id;
		userToken = token;
		slotIndex = slot_index;
		userInfo = user_info;
		isPartyOwner = (PartyManager.IsValidInParty() && MonoBehaviourSingleton<PartyManager>.I.GetOwnerUserId() == userId);
		if (QuestManager.IsValidExplore())
		{
			MonoBehaviourSingleton<QuestManager>.I.ActivateExplorePlayerStatus(this);
		}
		Logd("Activate.");
	}

	public void Deactivate()
	{
		userId = 0;
		userToken = string.Empty;
		slotIndex = -1;
		userInfo = null;
		Logd("Deactivate.");
	}

	protected virtual void SetStatus(CLIENT_STATUS st)
	{
		Logd("status {0} => {1}", status, st);
		status = st;
		if (status == CLIENT_STATUS.LOADING_FINISH)
		{
			loadingPer = 100;
		}
	}

	public virtual void SetUserInfo(CharaInfo user_info)
	{
		userInfo = user_info;
	}

	public virtual void SetLoadingPer(int per)
	{
		loadingPer = per;
	}

	public virtual string GetPlayerName()
	{
		Player player = GetPlayer();
		if (player != null)
		{
			return player.charaName;
		}
		if (userInfo != null)
		{
			return userInfo.name;
		}
		return "player" + playerId;
	}

	public void SetStage(int stage_id, int series_index, bool is_host)
	{
		stageId = stage_id;
		seriesIndex = series_index;
		isStageHost = is_host;
	}

	public void SetStageHost(bool is_host)
	{
		isStageHost = is_host;
	}

	public bool IsActivate()
	{
		return userId > 0;
	}

	public bool IsPlayingStage()
	{
		return status == CLIENT_STATUS.STAGE_REQUEST || status == CLIENT_STATUS.BATTLE_START;
	}

	public bool IsStageStart()
	{
		return status >= CLIENT_STATUS.STAGE_START;
	}

	public bool IsLoadingStart()
	{
		return status >= CLIENT_STATUS.LOADING_START;
	}

	public bool IsStageRequest()
	{
		return status >= CLIENT_STATUS.STAGE_REQUEST;
	}

	public bool IsBattleStart()
	{
		return status >= CLIENT_STATUS.BATTLE_START;
	}

	public bool IsBattleEnd()
	{
		return status >= CLIENT_STATUS.BATTLE_END;
	}

	public void SetPlayerID(int id)
	{
		Logd("playerId {0} => {1}", playerId, id);
		playerId = id;
	}

	public bool IsPlayerPop()
	{
		return playerId > 0;
	}

	public Player GetPlayer()
	{
		if (playerId <= 0)
		{
			return null;
		}
		Player result = null;
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			result = (MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(playerId) as Player);
		}
		return result;
	}

	public void SetCachePlayer(int player_id, bool sub)
	{
		if (sub)
		{
			cachePlayerID = player_id;
		}
		else
		{
			cacheSubPlayerIDs.Add(player_id);
		}
	}

	public void OnPlayerPop(int player_id)
	{
		if (cachePlayerID != 0 && cachePlayerID == player_id)
		{
			SetPlayerID(player_id);
			cachePlayerID = 0;
		}
		int num = 0;
		while (num < cacheSubPlayerIDs.Count)
		{
			if (cacheSubPlayerIDs[num] != 0 && cacheSubPlayerIDs[num] == player_id)
			{
				cacheSubPlayerIDs.RemoveAt(num);
			}
			else
			{
				num++;
			}
		}
	}

	public void PopCachePlayer(StageObject.COOP_MODE_TYPE coop_mode = StageObject.COOP_MODE_TYPE.NONE)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return;
		}
		int num = 0;
		Vector3 appearPosGuest = Vector3.get_zero();
		if (MonoBehaviourSingleton<StageObjectManager>.I.boss != null)
		{
			appearPosGuest = MonoBehaviourSingleton<StageObjectManager>.I.boss._transform.get_position();
		}
		if (cachePlayerID != 0 && !IsPlayerPop())
		{
			num = cachePlayerID;
			cachePlayerID = 0;
			Player player = MonoBehaviourSingleton<StageObjectManager>.I.FindCache(num) as Player;
			if (player != null)
			{
				MonoBehaviourSingleton<StageObjectManager>.I.RemoveCacheObject(player);
				player.get_gameObject().SetActive(true);
				SetPlayerID(num);
				player.SetAppearPosGuest(appearPosGuest);
				if (coop_mode != 0)
				{
					player.SetCoopMode(coop_mode, clientId);
				}
			}
		}
		int i = 0;
		for (int count = cacheSubPlayerIDs.Count; i < count; i++)
		{
			num = cacheSubPlayerIDs[i];
			Player player2 = MonoBehaviourSingleton<StageObjectManager>.I.FindCache(num) as Player;
			if (player2 != null)
			{
				MonoBehaviourSingleton<StageObjectManager>.I.RemoveCacheObject(player2);
				player2.get_gameObject().SetActive(true);
				player2.SetAppearPosGuest(appearPosGuest);
				if (coop_mode != 0)
				{
					player2.SetCoopMode(coop_mode, clientId);
				}
			}
		}
		cacheSubPlayerIDs.Clear();
	}

	public virtual void OnRoomLeaved()
	{
		Logd("OnRoomLeaved.");
		bool flag = IsBattleEnd();
		isLeave = true;
		if (MonoBehaviourSingleton<CoopManager>.I.coopStage.stageId != stageId || isBattleRetire || !IsStageStart() || flag || userInfo == null || !QuestManager.IsValidInGame())
		{
			return;
		}
		uint id = 101u;
		if (isPartyOwner && QuestManager.IsValidInGameExplore())
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
		string text = StringTable.Format(STRING_CATEGORY.IN_GAME, id, GetPlayerName());
		UIInGamePopupDialog.PushOpen(text, is_important: false);
	}

	public virtual bool OnRecvClientStatus(Coop_Model_ClientStatus model, CoopPacket packet)
	{
		if (isLeave)
		{
			MonoBehaviourSingleton<CoopManager>.I.coopMyClient.WelcomeClient(clientId);
		}
		SetStatus((CLIENT_STATUS)model.status);
		joinType = (CLIENT_JOIN_TYPE)model.joinType;
		if (model.status == 5)
		{
			if (isStageHost && MonoBehaviourSingleton<CoopManager>.I.coopStage.isRecvStageInfo && MonoBehaviourSingleton<InGameProgress>.IsValid())
			{
				MonoBehaviourSingleton<InGameProgress>.I.StartTimer();
			}
			if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
			{
				List<StageObject> list = new List<StageObject>(MonoBehaviourSingleton<StageObjectManager>.I.cacheList);
				int i = 0;
				for (int count = list.Count; i < count; i++)
				{
					Player player = list[i] as Player;
					if (!(player == null) && player.coopClientId == clientId && player.isWaitBattleStart)
					{
						MonoBehaviourSingleton<StageObjectManager>.I.RemoveCacheObject(player);
						player.get_gameObject().SetActive(true);
						player.ActBattleStart();
					}
				}
			}
		}
		if (isPartyOwner && model.status == 6)
		{
			MonoBehaviourSingleton<CoopManager>.I.coopRoom.SetOwnerCleared();
		}
		if (MonoBehaviourSingleton<CoopManager>.I.coopRoom.NeedsForceLeave())
		{
			MonoBehaviourSingleton<CoopNetworkManager>.I.Close(1000);
		}
		return true;
	}

	public virtual bool OnRecvClientLoadingProgress(Coop_Model_ClientLoadingProgress model)
	{
		SetLoadingPer(model.per);
		return true;
	}

	public virtual bool OnRecvClientChangeEquip(Coop_Model_ClientChangeEquip model)
	{
		if (userInfo != null)
		{
			userInfo = model.userInfo;
			if (MonoBehaviourSingleton<GameSceneManager>.IsValid())
			{
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.RECEIVE_COOP_ROOM_UPDATE);
			}
		}
		return true;
	}

	public virtual bool OnRecvClientBattleRetire(Coop_Model_ClientBattleRetire model)
	{
		isBattleRetire = true;
		if (isPartyOwner)
		{
			MonoBehaviourSingleton<CoopManager>.I.coopRoom.ownerRetire = true;
		}
		if (QuestManager.IsValidInGame() && MonoBehaviourSingleton<UIDeadAnnounce>.IsValid())
		{
			MonoBehaviourSingleton<UIDeadAnnounce>.I.Announce(UIDeadAnnounce.ANNOUNCE_TYPE.RETIRE, GetPlayer());
		}
		if (!isStageHost)
		{
			CoopStageObjectUtility.TransfarOwnerForClientObjects(clientId, MonoBehaviourSingleton<CoopManager>.I.coopStage.hostClientId);
		}
		if (QuestManager.IsValidInGameExplore())
		{
			MonoBehaviourSingleton<QuestManager>.I.RemoveExplorePlayerStatus(this);
		}
		if (MonoBehaviourSingleton<CoopManager>.I.coopRoom.NeedsForceLeave())
		{
			MonoBehaviourSingleton<CoopNetworkManager>.I.Close(1000);
		}
		return true;
	}

	public virtual bool OnRecvClientSeriesProgress(Coop_Model_ClientSeriesProgress model)
	{
		isSeriesProgressEnd = true;
		if (model.ep == MonoBehaviourSingleton<QuestManager>.I.currentQuestSeriesIndex && MonoBehaviourSingleton<InGameProgress>.IsValid() && !MonoBehaviourSingleton<InGameProgress>.I.isGameProgressStop)
		{
			MonoBehaviourSingleton<InGameProgress>.I.PortalNext(0u);
		}
		return true;
	}
}
