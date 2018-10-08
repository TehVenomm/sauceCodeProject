public class CoopManager : MonoBehaviourSingleton<CoopManager>
{
	public const int ID_ERROR = -1;

	public const int ID_COOP_SERVER = 1000;

	public const int ID_COOP_ROOM = 1001;

	public const int ID_COOP_STAGE = 1002;

	public const int ID_COOP_CLIENT = 1003;

	public const int ID_COOP_PARTY = 1004;

	public const int ID_COOP_LOUNGE = 1005;

	public const int ID_PLAYER_START = 100000;

	public const int ID_PLAYER_END = 109999;

	public const int ID_PLAYER_SV_START = 110000;

	public const int ID_NONPLAYER_START = 150000;

	public const int ID_NONPLAYER_END = 199999;

	public const int ID_GIMMICK_START = 200000;

	public const int ID_GIMMICK_END = 299999;

	public const int ID_ENEMY_START = 500000;

	public const int ID_ENEMY_END = 999999;

	public const int ID_BULLET_START = 1000000;

	public const int ID_BULLET_END = 1999999;

	public const int ID_ENEMY_STAGE_COUNT = 100000;

	protected int nonplayerIDCount;

	protected int enemyIDCount;

	public CoopRoom coopRoom
	{
		get;
		private set;
	}

	public CoopStage coopStage
	{
		get;
		private set;
	}

	public CoopMyClient coopMyClient
	{
		get;
		private set;
	}

	public bool isCoop => coopRoom.IsActivate();

	public bool isStageHost => !MonoBehaviourSingleton<KtbWebSocket>.I.IsConnected() || coopMyClient.isStageHost;

	public static bool IsValidInOnline()
	{
		return MonoBehaviourSingleton<CoopManager>.IsValid() && CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected();
	}

	public static bool IsValidInCoop()
	{
		return MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.I.isCoop;
	}

	protected override void Awake()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		base.Awake();
		coopRoom = this.get_gameObject().AddComponent<CoopRoom>();
		coopStage = this.get_gameObject().AddComponent<CoopStage>();
		coopMyClient = (CoopMyClient)Utility.CreateGameObjectAndComponent("CoopMyClient", this.get_transform(), -1);
	}

	private void Start()
	{
	}

	private void Update()
	{
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
		Logd("Clear.");
		coopRoom.Deactivate();
		coopStage.Deactivate();
		MonoBehaviourSingleton<CoopNetworkManager>.I.Clear();
		MonoBehaviourSingleton<CoopNetworkManager>.I.EraseAllPackets();
		MonoBehaviourSingleton<KtbWebSocket>.I.Close(1000, "Bye!");
		MonoBehaviourSingleton<CoopOfflineManager>.I.Clear();
	}

	public void OnStageChangeInterval()
	{
		if (coopRoom != null)
		{
			coopRoom.OnStageChangeInterval();
		}
		if (coopStage != null)
		{
			coopStage.OnStageChangeInterval();
		}
		if (MonoBehaviourSingleton<CoopOfflineManager>.IsValid())
		{
			MonoBehaviourSingleton<CoopOfflineManager>.I.OnStageChangeInterval();
		}
	}

	public void OnQuestSeriesInterval()
	{
		if (coopRoom != null)
		{
			coopRoom.OnQuestSeriesInterval();
		}
		if (coopStage != null)
		{
			coopStage.OnQuestSeriesInterval();
		}
		if (MonoBehaviourSingleton<CoopOfflineManager>.IsValid())
		{
			MonoBehaviourSingleton<CoopOfflineManager>.I.OnQuestSeriesInterval();
		}
	}

	public bool IsSendRoomPacket()
	{
		if (!MonoBehaviourSingleton<KtbWebSocket>.I.IsConnected())
		{
			return false;
		}
		if (!coopRoom.IsActivate())
		{
			return false;
		}
		if (!coopMyClient.IsActivate())
		{
			return false;
		}
		return true;
	}

	public int GetSelfID()
	{
		if (MonoBehaviourSingleton<CoopNetworkManager>.I.registerAck != null)
		{
			return MonoBehaviourSingleton<CoopNetworkManager>.I.registerAck.sid;
		}
		return GetPlayerID(coopMyClient);
	}

	public int GetPlayerID(CoopClient client)
	{
		return 100000 + client.slotIndex;
	}

	public int GetPartyOwnerPlayerID()
	{
		if (coopRoom == null || coopRoom.clients == null)
		{
			return -1;
		}
		if (InGameManager.IsValidRush())
		{
			return -1;
		}
		CoopClient coopClient = coopRoom.clients.FindPartyOwner();
		if (coopClient == null)
		{
			return -1;
		}
		return coopClient.playerId;
	}

	public int CreateUniqueNonPlayerID()
	{
		int num = nonplayerIDCount + 150000;
		if (num > 199999)
		{
			num = 150000;
			nonplayerIDCount = 0;
		}
		nonplayerIDCount++;
		if (MonoBehaviourSingleton<StageObjectManager>.I.FindNonPlayer(num) != null)
		{
			return CreateUniqueNonPlayerID();
		}
		return num;
	}

	public PacketReceiver GetPacketReceiver(CoopPacket packet)
	{
		if (packet.destObjectId == 1000)
		{
			return MonoBehaviourSingleton<CoopNetworkManager>.I.packetReceiver;
		}
		if (packet.destObjectId == 1001)
		{
			return coopRoom.packetReceiver;
		}
		if (packet.destObjectId == 1002)
		{
			return coopStage.packetReceiver;
		}
		if (packet.destObjectId == 1003)
		{
			CoopClient coopClient = coopRoom.clients.FindByClientId(packet.fromClientId);
			if (coopClient != null)
			{
				return coopClient.packetReceiver;
			}
		}
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			StageObject stageObject = MonoBehaviourSingleton<StageObjectManager>.I.FindObject(packet.destObjectId);
			if (stageObject == null)
			{
				stageObject = MonoBehaviourSingleton<StageObjectManager>.I.FindCache(packet.destObjectId);
			}
			if (stageObject != null)
			{
				return stageObject.packetReceiver;
			}
		}
		return null;
	}

	public bool PacketRelay(CoopPacket packet)
	{
		PacketReceiver packetReceiver = GetPacketReceiver(packet);
		if (packetReceiver != null)
		{
			packetReceiver.Set(packet);
			return true;
		}
		return false;
	}

	public void ForcePacketProcess(CoopPacket packet)
	{
		PacketReceiver packetReceiver = GetPacketReceiver(packet);
		if (packetReceiver != null)
		{
			packetReceiver.ForcePacketProcess(packet);
		}
	}

	public ChatCoopConnection CreateChatConnection()
	{
		ChatCoopConnection chatCoopConnection = new ChatCoopConnection();
		coopStage.SetChatConnection(chatCoopConnection);
		coopRoom.SetChatConnection(chatCoopConnection);
		return chatCoopConnection;
	}
}
