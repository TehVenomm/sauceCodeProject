// Decompiled with JetBrains decompiler
// Type: ChatWebSocketConnection
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class ChatWebSocketConnection : MonoBehaviour, IChatConnection
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

  public event ChatRoom.OnJoin onJoin;

  public event ChatRoom.OnReceiveText onReceiveText;

  public event ChatRoom.OnReceiveStamp onReceiveStamp;

  public event ChatRoom.OnReceiveNotification onReceiveNotification;

  public event ChatRoom.OnAfterSendUserMessage onAfterSendUserMessage;

  public event ChatRoom.OnDisconnect onDisconnect;

  public bool isEstablished => this.established;

  public bool isReadyToChat => this.joined;

  public ChatWebSocket chatWebSocket { get; private set; }

  public void Setup(string host, int port, string path, bool autoReconnect = true)
  {
    this.uri = new UriBuilder("ws", host, port, path).Uri.ToString();
    this.autoReconnect = autoReconnect;
    this.chatWebSocket = Utility.CreateGameObjectAndComponent("ChatWebSocket", ((Component) this).transform) as ChatWebSocket;
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
    this.roomId = $"room{roomNo}";
    this.Join(this.roomId, userName);
  }

  public void JoinClan(int roomNo, string userName)
  {
    this.roomId = $"room{roomNo}";
    this.JoinClan(this.roomId, userName);
  }

  private void Join(string roomId, string userName)
  {
    this.roomId = roomId;
    this.userName = userName.Replace(":", "：");
    this.TryConnect((Action<bool>) (result =>
    {
      if (result)
        this.chatWebSocket.Send<Chat_Model_JoinRoom>(Chat_Model_JoinRoom.Create(this.roomId, this.userName), 0);
      else
        Log.Error("チャット入室に失敗しました");
    }));
  }

  private void JoinClan(string roomId, string userName)
  {
    this.roomId = roomId;
    this.userName = userName.Replace(":", "：");
    this.TryConnect((Action<bool>) (result =>
    {
      if (result)
        this.chatWebSocket.Send<Chat_Model_JoinClanRoom>(Chat_Model_JoinClanRoom.Create(this.roomId, this.userName), 0);
      else
        Log.Error("チャット入室に失敗しました");
    }));
  }

  public void SendText(string message)
  {
    if (!this.isEstablished)
      return;
    string str = message.Replace(":", "：");
    int id = ChatWebSocketConnection.PickStampId(str);
    if (id > 0 && !MonoBehaviourSingleton<UIManager>.I.mainChat.CanIPostTheStamp(id))
      return;
    this.Send(str);
  }

  public void SendStamp(int stampId)
  {
    if (!this.isEstablished)
      return;
    this.Send($"{ChatWebSocketConnection.STAMP_SYMBOL_BEGIN}{stampId:D8}");
  }

  private void Send(string message)
  {
    this.chatWebSocket.Send<Chat_Model_BroadcastMessage_Request>(Chat_Model_BroadcastMessage_Request.Create(this.roomId, message), 0);
  }

  private void OnReceivePacket(ChatPacket packet)
  {
    if (packet == null)
      return;
    switch (packet.model.packetType)
    {
      case CHAT_PACKET_TYPE.JOIN_ROOM:
        this.OnJoin(packet);
        break;
      case CHAT_PACKET_TYPE.BROADCAST_ROOM:
        if (packet.model is Chat_Model_BroadcastMessage_Response model1)
        {
          this.OnReceiveMessage(model1);
          break;
        }
        Log.Error("Failed parse: Chat_Model_BroadCastMessage_Response");
        break;
      case CHAT_PACKET_TYPE.PARTY_INVITE:
        if (packet.model is Chat_Model_PartyInvite model2)
        {
          this.OnReceivePartyInvite(model2);
          break;
        }
        Log.Error("Failed parse: Chat_Model_BroadCastMessage_Response");
        break;
      case CHAT_PACKET_TYPE.RALLY_INVITE:
        if (packet.model is Chat_Model_RallyInvite model3)
        {
          this.OnReceiveRallyInvite(model3);
          break;
        }
        Log.Error("Failed parse: Chat_Model_RallyInvite");
        break;
      case CHAT_PACKET_TYPE.DARK_MARKET_RESET:
        if (packet.model is Chat_Model_ResetDarkMarket model4)
        {
          this.OnReceiveResetDarkMarket(model4);
          break;
        }
        Log.Error("Failed parse: Chat_Model_ResetDarkMarket");
        break;
      case CHAT_PACKET_TYPE.DARK_MARKET_UPDATE:
        if (packet.model is Chat_Model_UpdateDarkMarket model5)
        {
          this.OnReceiveUpdateDarkMarket(model5);
          break;
        }
        Log.Error("Failed parse: Chat_Model_UpdateDarkMarket");
        break;
      case CHAT_PACKET_TYPE.JACKPOT_WIN_UPDATE:
        if (packet.model is Chat_Model_JackpotWin model6)
        {
          this.OnReceiveJackot(model6);
          break;
        }
        Log.Error("Failed parse: Chat_Model_RallyInvite");
        break;
      case CHAT_PACKET_TYPE.TRADING_POST_SOLD:
        Log.Error("Recv : TradingPostSoldModel");
        Debug.Log((object) "Recv : TradingPostSoldModel");
        if (packet.model is TradingPostSoldModel model7)
        {
          this.OnReceiveTradingPostSold(model7);
          break;
        }
        Log.Error("Failed parse: TradingPostSoldModel");
        break;
    }
  }

  private void OnJoin(ChatPacket packet)
  {
    if (this.joined || !(packet.model is Chat_Model_JoinRoom model) || model.errorType != CHAT_ERROR_TYPE.NO_ERROR)
      return;
    long result1 = 0;
    long.TryParse(packet.header.fromId, out result1);
    long result2 = 0;
    long.TryParse(this.chatWebSocket.fromId, out result2);
    if (result1 != result2)
      return;
    this.joined = true;
    if (this.onJoin == null)
      return;
    this.onJoin(model.errorType);
  }

  private void OnReceiveMessage(Chat_Model_BroadcastMessage_Response packet)
  {
    int result = 0;
    int.TryParse(packet.SenderId, out result);
    if (packet.Message.Contains(ChatWebSocketConnection.STAMP_SYMBOL_BEGIN))
    {
      string s = packet.Message.Substring(ChatWebSocketConnection.STAMP_SYMBOL_BEGIN.Length, 8);
      int stampId = -1;
      ref int local = ref stampId;
      int.TryParse(s, out local);
      if (this.onReceiveStamp == null)
        return;
      this.onReceiveStamp(result, packet.SenderName, stampId, "");
    }
    else
    {
      if (this.onReceiveText == null)
        return;
      this.onReceiveText(result, packet.SenderName, packet.Message, "");
    }
  }

  private void OnReceivePartyInvite(Chat_Model_PartyInvite packet)
  {
    int result = 0;
    int.TryParse(packet.flag, out result);
    if (result > 0)
    {
      if (!MonoBehaviourSingleton<UserInfoManager>.IsValid())
        return;
      MonoBehaviourSingleton<UserInfoManager>.I.SetPartyInviteChat(true);
    }
    else
    {
      if (!MonoBehaviourSingleton<UserInfoManager>.IsValid())
        return;
      MonoBehaviourSingleton<UserInfoManager>.I.SetPartyInviteChat(false);
    }
  }

  private void OnReceiveRallyInvite(Chat_Model_RallyInvite packet)
  {
    if (!MonoBehaviourSingleton<UserInfoManager>.IsValid())
      return;
    MonoBehaviourSingleton<UserInfoManager>.I.SetRallyInviteChat(true);
  }

  private void OnReceiveJackot(Chat_Model_JackpotWin packet)
  {
    string str = packet.jacpotData.Substring(0, 10);
    if (int.Parse(str) == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
      return;
    string jackpot = packet.jacpotData.Substring(10, 9);
    string userName = packet.jacpotData.Substring(19);
    MonoBehaviourSingleton<FortuneWheelManager>.I.ReceivedJackpotWin(new FortuneWheelManager.JackpotWinData(str, jackpot, userName));
  }

  private void OnReceiveResetDarkMarket(Chat_Model_ResetDarkMarket packet)
  {
    GameSaveData.instance.resetMarketTime = $"{packet.endDate.Substring(0, 4)}-{packet.endDate.Substring(4, 2)}-{packet.endDate.Substring(6, 2)} {packet.endDate.Substring(8, 2)}:{packet.endDate.Substring(10, 2)}:{packet.endDate.Substring(12, 2)}";
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.RESET_DARK_MARKET);
  }

  private void OnReceiveUpdateDarkMarket(Chat_Model_UpdateDarkMarket packet)
  {
    int result1 = 0;
    int.TryParse(packet.itemMarketId, out result1);
    int result2 = 0;
    int.TryParse(packet.soldNum, out result2);
    if (result1 != 0 && result2 != 0)
      MonoBehaviourSingleton<ShopManager>.I.UpdateDarkMarketUsedCount(result1, result2);
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_DARK_MARKET);
  }

  private void OnReceiveTradingPostSold(TradingPostSoldModel packet)
  {
    string soldNum = packet.soldNum;
    MonoBehaviourSingleton<TradingPostManager>.I.UpdateTradingPostSoldCount(1);
    MonoBehaviourSingleton<TradingPostManager>.I.SetTradingPostLastSold(soldNum);
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_TRADING_POST_SOLD);
  }

  private void OnReceiveTradingPostSold(int soldNum)
  {
    if (soldNum != 0)
      MonoBehaviourSingleton<TradingPostManager>.I.UpdateTradingPostSoldCount(soldNum);
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_TRADING_POST_SOLD);
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
      while (!this.chatWebSocket.IsConnected() && this.chatWebSocket.CurrentConnectionStatus != ChatWebSocket.CONNECTION_STATUS.ERROR && (double) waitTimeRest > 0.0)
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
    if (msg.Contains(ChatWebSocketConnection.STAMP_SYMBOL_BEGIN))
      int.TryParse(msg.Substring(ChatWebSocketConnection.STAMP_SYMBOL_BEGIN.Length, 8), out result);
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
