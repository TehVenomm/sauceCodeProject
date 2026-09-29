// Decompiled with JetBrains decompiler
// Type: ClanChatWebSocketConnection
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class ClanChatWebSocketConnection : MonoBehaviour, IClanChatConnection
{
  private static readonly string STAMP_SYMBOL_BEGIN = "[STMP]";
  private bool established;
  private bool joined;
  private string uri;
  private string roomId;
  private string userName;
  private Coroutine m_ConnectProcess;
  private bool autoReconnect;
  private float RECONNECT_WAIT_SEC = 1f;
  private int RECONNECT_RETRY_LIMIT = 1;
  private bool reconnecting;
  private bool isConnectProcessing;
  private float CONNECTION_TRY_TIMEOUT = 15f;

  public event ClanChatRoom.OnJoin onJoin;

  public event ClanChatRoom.OnLeave onLeave;

  public event ClanChatRoom.OnReceiveText onReceiveText;

  public event ClanChatRoom.OnReceiveStamp onReceiveStamp;

  public event ClanChatRoom.OnReceiveText onReceivePrivateText;

  public event ClanChatRoom.OnReceiveStamp onReceivePrivateStamp;

  public event ClanChatRoom.OnReceiveNotification onReceiveNotification;

  public event ClanChatRoom.OnDisconnect onDisconnect;

  public event ClanChatRoom.OnReceiveUpdateStatus onReceiveUpdateStatus;

  public bool isEstablished => this.established;

  public bool isReadyToChat => this.joined;

  public bool isConnecting => this.isConnectProcessing;

  public ClanChatWebSocket chatWebSocket { get; private set; }

  public void Setup(string host, int port, string path, bool autoReconnect = true)
  {
    this.uri = new UriBuilder("ws", host, port, path).Uri.ToString();
    this.autoReconnect = autoReconnect;
    this.chatWebSocket = Utility.CreateGameObjectAndComponent("ClanChatWebSocket", ((Component) this).transform) as ClanChatWebSocket;
  }

  private void OnWebSocketClosed()
  {
    this.chatWebSocket.OnClosed -= new System.Action(this.OnWebSocketClosed);
    this.established = false;
    this.joined = false;
    if (this.autoReconnect && !this.reconnecting)
      this.Reconnect(this.RECONNECT_RETRY_LIMIT);
    if (this.onDisconnect == null)
      return;
    this.onDisconnect();
  }

  private void Reconnect(int count)
  {
    this.reconnecting = true;
    this.StartCoroutine(this.TryReconnect(count));
  }

  private IEnumerator TryReconnect(int count)
  {
    for (float time = this.RECONNECT_WAIT_SEC * (float) (this.RECONNECT_RETRY_LIMIT - count + 1); (double) time > 0.0; time -= Time.deltaTime)
      yield return (object) null;
    this.TryConnect((Action<bool>) (success =>
    {
      this.reconnecting = false;
      if (!success)
      {
        if (count <= 0)
          return;
        this.Reconnect(count - 1);
      }
      else
        this.Join(this.roomId, this.userName);
    }));
  }

  public void Connect() => this.TryConnect((Action<bool>) (x => { }));

  private void TryConnect(Action<bool> onFinished)
  {
    if (this.isEstablished)
    {
      if (onFinished == null)
        return;
      onFinished(true);
    }
    else if (this.isConnectProcessing)
    {
      this.StartCoroutine(this.WaitConnectProcess(onFinished));
    }
    else
    {
      this.chatWebSocket.ReceivePacketAction = new Action<ChatPacket>(this.OnReceivePacket);
      this.m_ConnectProcess = this.StartCoroutine(this.ConnectProcess(onFinished));
    }
  }

  public void Disconnect(System.Action onFinished = null)
  {
    if (this.isEstablished)
    {
      this.chatWebSocket.OnClosed -= new System.Action(this.OnWebSocketClosed);
      this.chatWebSocket.Send<Chat_Model_LeaveRoom_Request>(Chat_Model_LeaveRoom_Request.Create(this.roomId), 0);
      this.chatWebSocket.Close();
      this.established = false;
      this.joined = false;
      if (onFinished == null && this.onDisconnect == null || AppMain.isApplicationQuit)
        return;
      this.StartCoroutine(this.WaitClose(onFinished));
    }
    else
    {
      this.StopConnectProcess();
      if (onFinished != null)
        onFinished();
      if (this.onDisconnect == null)
        return;
      this.onDisconnect();
    }
  }

  private IEnumerator WaitClose(System.Action onFinished = null)
  {
    while (this.chatWebSocket.IsOpen())
      yield return (object) null;
    if (onFinished != null)
      onFinished();
    if (this.onDisconnect != null)
      this.onDisconnect();
  }

  public void Join(int roomNo, string userName)
  {
    this.roomId = roomNo.ToString();
    this.Join(this.roomId, userName);
  }

  private void Join(string roomId, string userName)
  {
    this.roomId = roomId;
    this.userName = userName.Replace(":", "：");
    this.TryConnect((Action<bool>) (result =>
    {
      if (result)
        this.chatWebSocket.Send<Chat_Model_JoinClanRoom>(Chat_Model_JoinClanRoom.Create(this.roomId, this.userName), 0);
      else
        Log.Error("I failed to enter chat");
    }));
  }

  public void Leave(int roomNo, string userName)
  {
    this.roomId = roomNo.ToString();
    this.Leave(this.roomId, userName);
  }

  private void Leave(string roomId, string userName)
  {
    this.roomId = roomId;
    this.userName = userName.Replace(":", "：");
    this.TryConnect((Action<bool>) (result =>
    {
      if (result)
        this.chatWebSocket.Send<Chat_Model_LeaveClanRoom>(Chat_Model_LeaveClanRoom.Create(this.roomId, this.userName), 0);
      else
        Log.Error("I failed to leave chat");
    }));
  }

  public void SendText(string message)
  {
    if (!this.isEstablished)
      return;
    string str = message.Replace(":", "：");
    int id = ClanChatWebSocketConnection.PickStampId(str);
    if (id > 0 && !MonoBehaviourSingleton<UIManager>.I.mainChat.CanIPostTheStamp(id))
      return;
    this.Send(str);
  }

  public void SendStamp(int stampId)
  {
    if (!this.isEstablished)
      return;
    this.Send($"{ClanChatWebSocketConnection.STAMP_SYMBOL_BEGIN}{stampId:D8}");
  }

  private void Send(string message)
  {
    this.chatWebSocket.Send<Chat_Model_BroadcastClanMessage_Request>(Chat_Model_BroadcastClanMessage_Request.Create(this.roomId, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name, message), 0);
  }

  public void SendPrivateText(string target_id, string message)
  {
    if (!this.isEstablished)
      return;
    string str = message.Replace(":", "：");
    int id = ClanChatWebSocketConnection.PickStampId(str);
    if (id > 0 && !MonoBehaviourSingleton<UIManager>.I.mainChat.CanIPostTheStamp(id))
      return;
    this.SendPrivate(target_id, str);
  }

  public void SendPrivateStamp(string target_id, int stampId)
  {
    if (!this.isEstablished)
      return;
    string message = $"{ClanChatWebSocketConnection.STAMP_SYMBOL_BEGIN}{stampId:D8}";
    this.SendPrivate(target_id, message);
  }

  private void SendPrivate(string target_id, string message)
  {
    this.chatWebSocket.Send<Chat_Model_SendToClanMessage_Request>(Chat_Model_SendToClanMessage_Request.Create(target_id, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name, this.roomId, message), 0);
  }

  private void OnReceivePacket(ChatPacket packet)
  {
    if (packet == null)
      return;
    switch (packet.model.packetType)
    {
      case CHAT_PACKET_TYPE.CLAN_JOIN_ROOM:
        this.OnJoin(packet);
        break;
      case CHAT_PACKET_TYPE.CLAN_LEAVE_ROOM:
        this.OnLeave(packet);
        break;
      case CHAT_PACKET_TYPE.CLAN_BROADCAST_ROOM:
        this.OnReceiveMessage(packet);
        break;
      case CHAT_PACKET_TYPE.CLAN_BROADCAST_STATUS:
        this.OnReceiveUpdateStatus(packet);
        break;
      case CHAT_PACKET_TYPE.CLAN_SENDTO:
        this.OnReceivePrivateMessage(packet);
        break;
    }
  }

  private void OnJoin(ChatPacket packet)
  {
    if (!(packet.model is Chat_Model_JoinClanRoom model) || model.errorType != CHAT_ERROR_TYPE.NO_ERROR)
      return;
    long result1 = 0;
    long.TryParse(packet.header.fromId, out result1);
    long result2 = 0;
    long.TryParse(this.chatWebSocket.fromId, out result2);
    if (result1 != result2)
      return;
    if (string.IsNullOrEmpty(model.UserId))
      this.joined = true;
    if (this.onJoin == null)
      return;
    this.onJoin(model.errorType, model.UserId);
  }

  private void OnLeave(ChatPacket packet)
  {
    if (!(packet.model is Chat_Model_LeaveClanRoom model) || model.errorType != CHAT_ERROR_TYPE.NO_ERROR)
      return;
    long result1 = 0;
    long.TryParse(packet.header.fromId, out result1);
    long result2 = 0;
    long.TryParse(this.chatWebSocket.fromId, out result2);
    if (result1 != result2)
      return;
    if (model.Owner == 1)
      this.joined = false;
    if (this.onLeave == null)
      return;
    this.onLeave(model.errorType, model.UserId);
  }

  private void OnReceiveUpdateStatus(ChatPacket packet)
  {
    Chat_Model_BroadcastClanStatus_Response model = packet.model as Chat_Model_BroadcastClanStatus_Response;
    int result1 = 0;
    int result2 = 0;
    int result3 = 0;
    int result4 = 0;
    int result5 = 0;
    int.TryParse(model.Type, out result1);
    int.TryParse(model.RoomId, out result2);
    int.TryParse(model.Result, out result3);
    int.TryParse(model.Status, out result4);
    int.TryParse(model.Id, out result5);
    ClanUpdateStatusData statusData = new ClanUpdateStatusData();
    statusData.id = result5;
    statusData.type = result1;
    statusData.roomId = result2;
    statusData.result = result3;
    statusData.status = result4;
    switch (result1)
    {
      case 2:
        if (this.onReceiveUpdateStatus == null)
          break;
        this.onReceiveUpdateStatus(statusData);
        break;
      case 3:
        if (this.onReceiveUpdateStatus == null)
          break;
        this.onReceiveUpdateStatus(statusData);
        break;
    }
  }

  private void OnReceiveMessage(ChatPacket packet)
  {
    Chat_Model_BroadcastClanMessage_Response model = packet.model as Chat_Model_BroadcastClanMessage_Response;
    int result1 = 0;
    int result2 = 0;
    int.TryParse(model.SenderId, out result1);
    int.TryParse(model.Id, out result2);
    if (result1 == 0)
    {
      if (this.onReceiveNotification == null)
        return;
      this.onReceiveNotification(model.Message);
    }
    else if (model.Message.Contains(ClanChatWebSocketConnection.STAMP_SYMBOL_BEGIN))
    {
      string s = model.Message.Substring(ClanChatWebSocketConnection.STAMP_SYMBOL_BEGIN.Length, 8);
      int num = -1;
      ref int local = ref num;
      int.TryParse(s, out local);
      if (this.onReceiveStamp == null)
        return;
      this.onReceiveStamp(new ClanChatLogMessageData()
      {
        uuid = model.Uuid,
        id = result2,
        fromUserId = result1,
        senderName = model.SenderName,
        stampId = num
      });
    }
    else
    {
      if (this.onReceiveText == null)
        return;
      this.onReceiveText(new ClanChatLogMessageData()
      {
        uuid = model.Uuid,
        id = result2,
        fromUserId = result1,
        senderName = model.SenderName,
        message = model.Message
      });
    }
  }

  private void OnReceivePrivateMessage(ChatPacket packet)
  {
    Chat_Model_SendToClanMessage_Response model = packet.model as Chat_Model_SendToClanMessage_Response;
    int result1 = 0;
    int result2 = 0;
    int result3 = 0;
    int.TryParse(model.SenderId, out result1);
    int.TryParse(model.ReceiveId, out result2);
    int.TryParse(model.Id, out result3);
    if (model.Message.Contains(ClanChatWebSocketConnection.STAMP_SYMBOL_BEGIN))
    {
      string s = model.Message.Substring(ClanChatWebSocketConnection.STAMP_SYMBOL_BEGIN.Length, 8);
      int num = -1;
      ref int local = ref num;
      int.TryParse(s, out local);
      if (this.onReceivePrivateStamp == null)
        return;
      this.onReceivePrivateStamp(new ClanChatLogMessageData()
      {
        uuid = model.Uuid,
        id = result3,
        fromUserId = result1,
        toUserId = result2,
        senderName = model.SenderName,
        stampId = num
      });
    }
    else
    {
      if (this.onReceivePrivateText == null)
        return;
      this.onReceivePrivateText(new ClanChatLogMessageData()
      {
        uuid = model.Uuid,
        id = result3,
        fromUserId = result1,
        toUserId = result2,
        senderName = model.SenderName,
        message = model.Message
      });
    }
  }

  private IEnumerator ConnectProcess(Action<bool> onFinished)
  {
    if (!MonoBehaviourSingleton<UserInfoManager>.IsValid())
    {
      onFinished(false);
    }
    else
    {
      this.isConnectProcessing = true;
      this.chatWebSocket.Connect(this.uri, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id.ToString(), 0);
      float waitTimeRest = this.CONNECTION_TRY_TIMEOUT;
      while (!this.chatWebSocket.IsConnected() && this.chatWebSocket.CurrentConnectionStatus != ClanChatWebSocket.CONNECTION_STATUS.ERROR && (double) waitTimeRest > 0.0)
      {
        waitTimeRest -= Time.deltaTime;
        yield return (object) null;
      }
      this.m_ConnectProcess = (Coroutine) null;
      this.established = this.chatWebSocket.IsConnected();
      if (this.established)
        this.chatWebSocket.OnClosed += new System.Action(this.OnWebSocketClosed);
      if (onFinished != null)
        onFinished(this.isEstablished);
      this.isConnectProcessing = false;
    }
  }

  private IEnumerator WaitConnectProcess(Action<bool> onFinished)
  {
    while (this.isConnectProcessing)
      yield return (object) null;
    if (onFinished != null)
      onFinished(this.isEstablished);
  }

  private void StopConnectProcess()
  {
    if (this.m_ConnectProcess != null)
      this.StopCoroutine(this.m_ConnectProcess);
    this.m_ConnectProcess = (Coroutine) null;
    this.isConnectProcessing = false;
  }

  private static int PickStampId(string msg)
  {
    int result = -1;
    if (msg.Contains(ClanChatWebSocketConnection.STAMP_SYMBOL_BEGIN))
      int.TryParse(msg.Substring(ClanChatWebSocketConnection.STAMP_SYMBOL_BEGIN.Length, 8), out result);
    return result;
  }

  private void OnDestroy()
  {
    this.Disconnect((System.Action) null);
    if (!Object.op_Implicit((Object) this.chatWebSocket))
      return;
    Object.Destroy((Object) ((Component) this.chatWebSocket).gameObject);
  }

  private void OnApplicationPause(bool pause)
  {
    if (pause)
    {
      if (!Object.op_Inequality((Object) this.chatWebSocket, (Object) null))
        return;
      this.Disconnect((System.Action) null);
    }
    else
      this.Reconnect(this.RECONNECT_RETRY_LIMIT);
  }
}
