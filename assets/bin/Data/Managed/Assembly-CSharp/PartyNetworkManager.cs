// Decompiled with JetBrains decompiler
// Type: PartyNetworkManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using rhyme;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class PartyNetworkManager : MonoBehaviourSingleton<PartyNetworkManager>
{
  private const float CONNECT_TIMEOUT = 15f;
  private const float ALIVE_SENDTIME = 20f;
  private int sendId;
  public Party_Model_RegisterACK registerAck;

  public static void ClearPoolObjects() => rymTPool<List<CoopPacket>>.Clear();

  private PartyPacketReceiver packetReceiver { get; set; }

  private ChatPartyConnection chatConnection { get; set; }

  public ChatPartyConnection CreateChatConnection()
  {
    if (this.chatConnection == null)
      this.chatConnection = new ChatPartyConnection();
    return this.chatConnection;
  }

  protected override void Awake()
  {
    base.Awake();
    this.packetReceiver = ((Component) this).gameObject.AddComponent<PartyPacketReceiver>();
  }

  private void Update()
  {
    this.packetReceiver.OnUpdate();
    if (!CoopWebSocketSingleton<PartyWebSocket>.IsValidConnected() || (double) Time.time - (double) MonoBehaviourSingleton<PartyWebSocket>.I.packetSendTime < 20.0)
      return;
    this.Alive();
  }

  public void EraseAllPackets() => this.packetReceiver.EraseAllPackets();

  public void Clear()
  {
    this.sendId = 0;
    this.registerAck = (Party_Model_RegisterACK) null;
    this.EraseAllPackets();
  }

  private void Logd(string str, params object[] objs)
  {
    int num = Log.enabled ? 1 : 0;
  }

  private string GetRelayServerPath(string path, int port)
  {
    return new UriBuilder(path) { Port = port }.Uri.ToString();
  }

  public void Connect(PartyNetworkManager.ConnectData conn_data, Action<bool> call_back)
  {
    this.StartCoroutine(this.RequestCoroutineConnect(conn_data, call_back));
  }

  private IEnumerator RequestCoroutineConnect(
    PartyNetworkManager.ConnectData conn_data,
    Action<bool> call_back)
  {
    yield return (object) this.StartCoroutine(this.RequestCoroutineClose());
    if (string.IsNullOrEmpty(conn_data.path))
    {
      this.Logd("Connect fail. nothing connection path...");
      if (call_back != null)
        call_back(false);
    }
    else
    {
      if (conn_data.ports.Count == 0)
        conn_data.ports.Add(new Uri(conn_data.path).Port);
      bool is_success = false;
      foreach (int port in conn_data.ports)
      {
        float timeoutTimer = 15f;
        string relayServerPath = this.GetRelayServerPath(conn_data.path, port);
        this.Logd("Connect. path={0}", (object) relayServerPath);
        MonoBehaviourSingleton<PartyWebSocket>.I.Connect(relayServerPath, conn_data.fromId, conn_data.ackPrefix);
        while (!MonoBehaviourSingleton<PartyWebSocket>.I.IsConnected() && 0.0 < (double) timeoutTimer && MonoBehaviourSingleton<PartyWebSocket>.I.CurrentConnectionStatus != CoopWebSocketSingleton<PartyWebSocket>.CONNECTION_STATUS.ERROR)
        {
          timeoutTimer -= Time.deltaTime;
          yield return (object) new WaitForEndOfFrame();
        }
        if (MonoBehaviourSingleton<PartyWebSocket>.I.IsConnected())
        {
          is_success = true;
          this.RegisterPacketReceiveAction();
          break;
        }
      }
      if (call_back != null)
        call_back(is_success);
    }
  }

  public void Close(ushort code = 1000, string msg = "Bye!", System.Action call_back = null)
  {
    this.Logd("Close.");
    this.StartCoroutine(this.RequestCoroutineClose(code, msg, call_back));
  }

  private IEnumerator RequestCoroutineClose(ushort code = 1000, string msg = "Bye!", System.Action call_back = null)
  {
    if (MonoBehaviourSingleton<PartyWebSocket>.I.IsConnected())
    {
      MonoBehaviourSingleton<PartyWebSocket>.I.Close(code, msg);
      while (MonoBehaviourSingleton<PartyWebSocket>.I.IsConnected())
        yield return (object) new WaitForEndOfFrame();
    }
    this.Clear();
    if (call_back != null)
      call_back();
  }

  public void Regist(PartyNetworkManager.ConnectData conn_data, Action<bool> call_back)
  {
    Party_Model_Register model = new Party_Model_Register();
    model.roomId = conn_data.roomId;
    model.owner = conn_data.owner;
    model.ownerToken = conn_data.ownerToken;
    model.uid = conn_data.uid;
    model.signature = conn_data.signature;
    this.Logd("Regist. roomId={0}", (object) conn_data.roomId);
    this.registerAck = (Party_Model_RegisterACK) null;
    this.SendServer<Party_Model_Register>(model, onReceiveAck: (Func<Coop_Model_ACK, bool>) (ack =>
    {
      bool flag = true;
      this.registerAck = ack as Party_Model_RegisterACK;
      if (ack == null || !ack.positive)
      {
        flag = false;
        MonoBehaviourSingleton<PartyWebSocket>.I.Close();
      }
      if (call_back != null)
        call_back(flag);
      return true;
    }));
  }

  public void ConnectAndRegist(
    PartyNetworkManager.ConnectData conn_data,
    Action<bool, bool> call_back)
  {
    this.Connect(conn_data, (Action<bool>) (is_connect =>
    {
      this.Logd("Connected. valid={0}", (object) is_connect);
      if (!is_connect)
      {
        if (call_back == null)
          return;
        call_back(is_connect, false);
      }
      else
        this.Regist(conn_data, (Action<bool>) (is_regist =>
        {
          this.Logd("Registed. valid={0}", (object) is_regist);
          if (call_back == null)
            return;
          call_back(is_connect, is_regist);
        }));
    }));
  }

  public void Disconnect(ushort code)
  {
    this.SendServer<Coop_Model_Disconnect>(new Coop_Model_Disconnect()
    {
      code = (int) code
    }, false);
  }

  public void Alive() => this.SendServer<Coop_Model_Alive>(new Coop_Model_Alive(), false);

  public void ChatMessage(string message)
  {
    if (!UserInfoManager.IsValidUser())
      return;
    Coop_Model_StageChatMessage model = new Coop_Model_StageChatMessage();
    model.id = 1004;
    model.text = message;
    model.chara_id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    model.user_id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    this.SendBroadcast<Coop_Model_StageChatMessage>(model, false);
  }

  public void ChatStamp(int stamp_id)
  {
    if (!UserInfoManager.IsValidUser())
      return;
    Coop_Model_StageChatStamp model = new Coop_Model_StageChatStamp();
    model.id = 1004;
    model.stamp_id = stamp_id;
    model.chara_id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    model.user_id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    this.SendBroadcast<Coop_Model_StageChatStamp>(model, false);
  }

  public void SyncSend()
  {
    if (this.sendId <= 0)
      return;
    this.StartCoroutine(this.CoroutineSyncSend(this.sendId));
  }

  public IEnumerator CoroutineSyncSend(int id)
  {
    while (!MonoBehaviourSingleton<PartyWebSocket>.I.IsCompleteSend(id) && MonoBehaviourSingleton<PartyWebSocket>.I.IsConnected())
    {
      this.Logd("Sync send. id={0}", (object) id);
      yield return (object) new WaitForEndOfFrame();
    }
  }

  private int Send(
    int to_client_id,
    Coop_Model_Base model,
    System.Type type,
    bool promise = true,
    Func<Coop_Model_ACK, bool> onReceiveAck = null,
    Func<Coop_Model_Base, bool> onPreResend = null)
  {
    if (!MonoBehaviourSingleton<PartyWebSocket>.I.IsConnected())
      return -1;
    this.sendId = 0;
    this.sendId = MonoBehaviourSingleton<PartyWebSocket>.I.Send(model, type, to_client_id, promise, onReceiveAck, onPreResend);
    return this.sendId;
  }

  public int SendServer<T>(
    T model,
    bool promise = true,
    Func<Coop_Model_ACK, bool> onReceiveAck = null,
    Func<Coop_Model_Base, bool> onPreResend = null)
    where T : Coop_Model_Base
  {
    return this.Send(-1000, (Coop_Model_Base) model, typeof (T), promise, onReceiveAck, onPreResend);
  }

  public int SendTo<T>(
    int to_client_id,
    T model,
    bool promise = true,
    Func<Coop_Model_ACK, bool> onReceiveAck = null,
    Func<Coop_Model_Base, bool> onPreResend = null)
    where T : Coop_Model_Base
  {
    return this.Send(to_client_id, (Coop_Model_Base) model, typeof (T), promise, onReceiveAck, onPreResend);
  }

  public int SendBroadcast<T>(
    T model,
    bool promise = true,
    Func<Coop_Model_ACK, bool> onReceiveAck = null,
    Func<Coop_Model_Base, bool> onPreResend = null)
    where T : Coop_Model_Base
  {
    if (model.id != -1)
      return this.Send(-2000, (Coop_Model_Base) model, typeof (T), promise, onReceiveAck, onPreResend);
    Log.Warning(LOG.COOP, $"PartyNetwork({MonoBehaviourSingleton<PartyWebSocket>.I.IsConnected().ToString()}): model.id not set...");
    return -1;
  }

  private void RegisterPacketReceiveAction()
  {
    PartyWebSocket i1 = MonoBehaviourSingleton<PartyWebSocket>.I;
    i1.ReceivePacketAction = i1.ReceivePacketAction + (Action<CoopPacket>) (packet =>
    {
      if (packet.destObjectId == -1 || packet.packetType == PACKET_TYPE.HEARTBEAT)
        return;
      this.packetReceiver.Set(packet);
    });
    PartyWebSocket i2 = MonoBehaviourSingleton<PartyWebSocket>.I;
    i2.PrepareCloseOccurred = i2.PrepareCloseOccurred + (Action<ushort, string>) ((code, msg) =>
    {
      this.Logd("PrepareCloseOccurred. code={0}, msg={1}", (object) code, (object) msg);
      this.Disconnect(code);
    });
    PartyWebSocket i3 = MonoBehaviourSingleton<PartyWebSocket>.I;
    i3.CloseOccurred = i3.CloseOccurred + (Action<ushort, string>) ((code, msg) =>
    {
      this.Logd("CloseOccurred. code={0}, msg={1}", (object) code, (object) msg);
      this.LoopBackRoomLeave();
    });
    PartyWebSocket i4 = MonoBehaviourSingleton<PartyWebSocket>.I;
    i4.ErrorOccurred = i4.ErrorOccurred + (Action<Exception>) (ex =>
    {
      this.Logd("ErrorOccurred. ex={0}", (object) ex);
      this.LoopBackRoomLeave();
    });
    PartyWebSocket i5 = MonoBehaviourSingleton<PartyWebSocket>.I;
    i5.HeartbeatDisconnected = i5.HeartbeatDisconnected + (System.Action) (() =>
    {
      this.Logd("HeartbeatDisconnected.");
      this.LoopBackRoomLeave();
    });
  }

  public static CoopPacket CreateLoopBackRoomLeavedPacket()
  {
    Party_Model_RoomLeaved model = new Party_Model_RoomLeaved();
    model.id = 1000;
    model.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    model.token = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id.ToString();
    return CoopPacket.Create((Coop_Model_Base) model, -1000, -2000, false, -8989);
  }

  public void LoopBackRoomLeave()
  {
    CoopPacket roomLeavedPacket = PartyNetworkManager.CreateLoopBackRoomLeavedPacket();
    this.Logd("LoopBackRoomLeave. is_connect={0}", (object) CoopWebSocketSingleton<PartyWebSocket>.IsValidConnected());
    if (CoopWebSocketSingleton<PartyWebSocket>.IsValidConnected())
      MonoBehaviourSingleton<PartyWebSocket>.I.ReceivePacketAction(roomLeavedPacket);
    else
      this.packetReceiver.ForcePacketProcess(roomLeavedPacket);
  }

  public bool OnRecvRoomJoined(Party_Model_RoomJoined model)
  {
    this.Logd("OnRecvRoomJoined. cid={0}", (object) model.cid);
    return true;
  }

  public bool OnRecvRoomLeaved(Party_Model_RoomLeaved model)
  {
    this.Logd("OnRecvRoomLeaved. cid={0}", (object) model.cid);
    if (model.cid != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
    {
      string str = string.Empty;
      PartyModel.SlotInfo slotInfoByUserId = MonoBehaviourSingleton<PartyManager>.I.GetSlotInfoByUserId(model.cid);
      if (slotInfoByUserId != null)
        str = slotInfoByUserId.userInfo.name;
      else if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
      {
        InGameRecorder.PlayerRecord playerByUserId = MonoBehaviourSingleton<InGameRecorder>.I.GetPlayerByUserId(model.cid);
        if (playerByUserId != null)
          str = playerByUserId.charaInfo.name;
      }
      if (this.chatConnection != null && !string.IsNullOrEmpty(str))
        this.chatConnection.OnReceiveNotification(StringTable.Format(STRING_CATEGORY.CHAT, 6U, (object) str));
    }
    return true;
  }

  public bool OnRecvChatMessage(Coop_Model_StageChatMessage model)
  {
    this.Logd("OnRecvChatMessage. user_id={0},text={1}", (object) model.user_id, (object) model.text);
    if (!PartyManager.IsValidInParty())
      return true;
    PartyModel.SlotInfo slotInfoByUserId = MonoBehaviourSingleton<PartyManager>.I.GetSlotInfoByUserId(model.user_id);
    if (slotInfoByUserId == null || slotInfoByUserId.userInfo == null || this.chatConnection == null)
      return true;
    this.chatConnection.OnReceiveMessage(model.user_id, slotInfoByUserId.userInfo.name, model.text);
    return true;
  }

  public bool OnRecvChatStamp(Coop_Model_StageChatStamp model)
  {
    this.Logd("OnRecvChatStamp. user_id={0},stamp_id={1}", (object) model.user_id, (object) model.stamp_id);
    if (!PartyManager.IsValidInParty())
      return true;
    PartyModel.SlotInfo slotInfoByUserId = MonoBehaviourSingleton<PartyManager>.I.GetSlotInfoByUserId(model.user_id);
    if (slotInfoByUserId == null || slotInfoByUserId.userInfo == null || this.chatConnection == null)
      return true;
    this.chatConnection.OnReceiveStamp(model.user_id, slotInfoByUserId.userInfo.name, model.stamp_id);
    return true;
  }

  private IEnumerator OnApplicationPause(bool paused)
  {
    if (PartyManager.IsValidInParty() && MonoBehaviourSingleton<PartyWebSocket>.IsValid())
    {
      this.Logd("OnApplicationPause. pause={0}, is_connect={1}", (object) paused, (object) CoopWebSocketSingleton<PartyWebSocket>.IsValidConnected());
      if (paused)
      {
        MonoBehaviourSingleton<PartyWebSocket>.I.Close();
        yield break;
      }
    }
  }

  public class Pool_List_CoopPacket : rymTPool<List<CoopPacket>>
  {
  }

  public class ConnectData
  {
    public string path = string.Empty;
    public List<int> ports = new List<int>();
    public int fromId;
    public int ackPrefix;
    public string roomId = string.Empty;
    public int owner;
    public string ownerToken = string.Empty;
    public int uid;
    public string signature = string.Empty;
  }
}
