// Decompiled with JetBrains decompiler
// Type: CoopManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
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
  public const int ID_ENEMY_SERVANT_START = 490000;
  public const int ID_ENEMY_SERVANT_END = 499999;
  public const int ID_ENEMY_START = 500000;
  public const int ID_ENEMY_END = 999999;
  public const int ID_BULLET_START = 1000000;
  public const int ID_BULLET_END = 1999999;
  public const int ID_ENEMY_STAGE_COUNT = 100000;
  protected int nonplayerIDCount;
  protected int enemyIDCount;

  public static bool IsValidInOnline()
  {
    return MonoBehaviourSingleton<CoopManager>.IsValid() && CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected();
  }

  public static bool IsValidInCoop()
  {
    return MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.I.isCoop;
  }

  public CoopRoom coopRoom { get; private set; }

  public CoopStage coopStage { get; private set; }

  public CoopMyClient coopMyClient { get; private set; }

  protected override void Awake()
  {
    base.Awake();
    this.coopRoom = ((Component) this).gameObject.AddComponent<CoopRoom>();
    this.coopStage = ((Component) this).gameObject.AddComponent<CoopStage>();
    this.coopMyClient = (CoopMyClient) Utility.CreateGameObjectAndComponent("CoopMyClient", ((Component) this).transform);
  }

  private void Start()
  {
  }

  private void Update()
  {
  }

  private void Logd(string str, params object[] objs)
  {
    int num = Log.enabled ? 1 : 0;
  }

  public void Clear()
  {
    this.Logd("Clear.");
    this.coopRoom.Deactivate();
    this.coopStage.Deactivate();
    MonoBehaviourSingleton<CoopNetworkManager>.I.Clear();
    MonoBehaviourSingleton<CoopNetworkManager>.I.EraseAllPackets();
    MonoBehaviourSingleton<KtbWebSocket>.I.Close();
    MonoBehaviourSingleton<CoopOfflineManager>.I.Clear();
  }

  public void OnStageChangeInterval()
  {
    if (Object.op_Inequality((Object) this.coopRoom, (Object) null))
      this.coopRoom.OnStageChangeInterval();
    if (Object.op_Inequality((Object) this.coopStage, (Object) null))
      this.coopStage.OnStageChangeInterval();
    if (!MonoBehaviourSingleton<CoopOfflineManager>.IsValid())
      return;
    MonoBehaviourSingleton<CoopOfflineManager>.I.OnStageChangeInterval();
  }

  public void OnQuestSeriesInterval()
  {
    if (Object.op_Inequality((Object) this.coopRoom, (Object) null))
      this.coopRoom.OnQuestSeriesInterval();
    if (Object.op_Inequality((Object) this.coopStage, (Object) null))
      this.coopStage.OnQuestSeriesInterval();
    if (!MonoBehaviourSingleton<CoopOfflineManager>.IsValid())
      return;
    MonoBehaviourSingleton<CoopOfflineManager>.I.OnQuestSeriesInterval();
  }

  public bool isCoop => this.coopRoom.IsActivate();

  public bool isStageHost
  {
    get => !MonoBehaviourSingleton<KtbWebSocket>.I.IsConnected() || this.coopMyClient.isStageHost;
  }

  public bool IsSendRoomPacket()
  {
    return MonoBehaviourSingleton<KtbWebSocket>.I.IsConnected() && this.coopRoom.IsActivate() && this.coopMyClient.IsActivate();
  }

  public int GetSelfID()
  {
    return MonoBehaviourSingleton<CoopNetworkManager>.I.registerAck != null ? MonoBehaviourSingleton<CoopNetworkManager>.I.registerAck.sid : this.GetPlayerID((CoopClient) this.coopMyClient);
  }

  public int GetPlayerID(CoopClient client) => 100000 + client.slotIndex;

  public int GetPartyOwnerPlayerID()
  {
    if (Object.op_Equality((Object) this.coopRoom, (Object) null) || this.coopRoom.clients == null || InGameManager.IsValidRush())
      return -1;
    CoopClient partyOwner = this.coopRoom.clients.FindPartyOwner();
    return Object.op_Equality((Object) partyOwner, (Object) null) ? -1 : partyOwner.playerId;
  }

  public int CreateUniqueNonPlayerID()
  {
    int id = this.nonplayerIDCount + 150000;
    if (id > 199999)
    {
      id = 150000;
      this.nonplayerIDCount = 0;
    }
    ++this.nonplayerIDCount;
    return Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.FindNonPlayer(id), (Object) null) ? this.CreateUniqueNonPlayerID() : id;
  }

  public PacketReceiver GetPacketReceiver(CoopPacket packet)
  {
    if (packet.destObjectId == 1000)
      return (PacketReceiver) MonoBehaviourSingleton<CoopNetworkManager>.I.packetReceiver;
    if (packet.destObjectId == 1001)
      return (PacketReceiver) this.coopRoom.packetReceiver;
    if (packet.destObjectId == 1002)
      return (PacketReceiver) this.coopStage.packetReceiver;
    if (packet.destObjectId == 1003)
    {
      CoopClient byClientId = this.coopRoom.clients.FindByClientId(packet.fromClientId);
      if (Object.op_Inequality((Object) byClientId, (Object) null))
        return (PacketReceiver) byClientId.packetReceiver;
    }
    if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
    {
      StageObject cache = MonoBehaviourSingleton<StageObjectManager>.I.FindObject(packet.destObjectId);
      if (Object.op_Equality((Object) cache, (Object) null))
        cache = MonoBehaviourSingleton<StageObjectManager>.I.FindCache(packet.destObjectId);
      if (Object.op_Inequality((Object) cache, (Object) null))
        return (PacketReceiver) cache.packetReceiver;
    }
    return (PacketReceiver) null;
  }

  public bool PacketRelay(CoopPacket packet)
  {
    PacketReceiver packetReceiver = this.GetPacketReceiver(packet);
    if (!Object.op_Inequality((Object) packetReceiver, (Object) null))
      return false;
    packetReceiver.Set(packet);
    return true;
  }

  public void ForcePacketProcess(CoopPacket packet)
  {
    PacketReceiver packetReceiver = this.GetPacketReceiver(packet);
    if (!Object.op_Inequality((Object) packetReceiver, (Object) null))
      return;
    packetReceiver.ForcePacketProcess(packet);
  }

  public ChatCoopConnection CreateChatConnection()
  {
    ChatCoopConnection chat_connection = new ChatCoopConnection();
    this.coopStage.SetChatConnection(chat_connection);
    this.coopRoom.SetChatConnection(chat_connection);
    return chat_connection;
  }
}
