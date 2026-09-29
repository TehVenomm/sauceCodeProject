// Decompiled with JetBrains decompiler
// Type: ClanChatRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;

#nullable disable
public class ClanChatRoom
{
  private ChatSendLimitter sendLimitter;

  public string FromId { get; set; }

  public string MyName { get; private set; }

  public string RoomId { get; private set; }

  public int RoomNo { get; private set; }

  public bool HasConnect => this.connection != null && this.connection.isEstablished;

  public bool IsConnecting => this.connection != null && this.connection.isConnecting;

  public IClanChatConnection connection { get; private set; }

  public event ClanChatRoom.OnJoin onJoin;

  public event ClanChatRoom.OnLeave onLeave;

  public event ClanChatRoom.OnReceiveText onReceiveText;

  public event ClanChatRoom.OnReceiveStamp onReceiveStamp;

  public event ClanChatRoom.OnReceiveText onReceivePrivateText;

  public event ClanChatRoom.OnReceiveStamp onReceivePrivateStamp;

  public event ClanChatRoom.OnReceiveNotification onReceiveNotification;

  public event ClanChatRoom.OnReceiveUpdateStatus onReceiveUpdateStatus;

  public event ClanChatRoom.OnDisconnect onDisconnect;

  public ClanChatRoom()
  {
    GlobalSettingsManager.ChatParam chatParam = MonoBehaviourSingleton<GlobalSettingsManager>.I.chatParam;
    this.sendLimitter = new ChatSendLimitter(chatParam.limitCount, chatParam.limitDuration);
  }

  public bool CanSendMessage() => !this.sendLimitter.IsLimit();

  public void SetConnection(IClanChatConnection connection)
  {
    if (this.connection != null)
    {
      if (this.connection.isEstablished)
        this.connection.Disconnect();
      connection.onJoin -= new ClanChatRoom.OnJoin(this._OnJoin);
      connection.onLeave -= new ClanChatRoom.OnLeave(this._OnLeave);
      connection.onReceiveText -= new ClanChatRoom.OnReceiveText(this._OnReceiveText);
      connection.onReceiveStamp -= new ClanChatRoom.OnReceiveStamp(this._OnReceiveStamp);
      connection.onReceivePrivateText -= new ClanChatRoom.OnReceiveText(this._OnReceivePrivateText);
      connection.onReceivePrivateStamp -= new ClanChatRoom.OnReceiveStamp(this._OnReceivePrivateStamp);
      connection.onReceiveNotification -= new ClanChatRoom.OnReceiveNotification(this._OnReceiveNotification);
      connection.onReceiveUpdateStatus -= new ClanChatRoom.OnReceiveUpdateStatus(this._OnReceiveUpdateStatus);
    }
    this.connection = connection;
    connection.onJoin += new ClanChatRoom.OnJoin(this._OnJoin);
    connection.onLeave += new ClanChatRoom.OnLeave(this._OnLeave);
    connection.onReceiveText += new ClanChatRoom.OnReceiveText(this._OnReceiveText);
    connection.onReceiveStamp += new ClanChatRoom.OnReceiveStamp(this._OnReceiveStamp);
    connection.onReceivePrivateText += new ClanChatRoom.OnReceiveText(this._OnReceivePrivateText);
    connection.onReceivePrivateStamp += new ClanChatRoom.OnReceiveStamp(this._OnReceivePrivateStamp);
    connection.onReceiveNotification += new ClanChatRoom.OnReceiveNotification(this._OnReceiveNotification);
    connection.onDisconnect += new ClanChatRoom.OnDisconnect(this._OnDisconnect);
    connection.onReceiveUpdateStatus += new ClanChatRoom.OnReceiveUpdateStatus(this._OnReceiveUpdateStatus);
  }

  public void JoinRoom(int roomNo)
  {
    if (!MonoBehaviourSingleton<UserInfoManager>.IsValid())
      return;
    this.connection.Join(roomNo, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name);
  }

  public void LeaveRoom(int roomNo)
  {
    if (!MonoBehaviourSingleton<UserInfoManager>.IsValid())
      return;
    this.connection.Leave(roomNo, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name);
  }

  public bool SendMessage(string message)
  {
    if (this.sendLimitter.IsLimit())
      return false;
    this.sendLimitter.Touch();
    this.connection.SendText(message);
    return !this.sendLimitter.IsLimit();
  }

  public bool SendStamp(int stampId)
  {
    if (this.sendLimitter.IsLimit())
      return false;
    this.sendLimitter.Touch();
    this.connection.SendStamp(stampId);
    return !this.sendLimitter.IsLimit();
  }

  public bool SendPrivateMessage(string target_id, string message)
  {
    if (this.sendLimitter.IsLimit())
      return false;
    this.sendLimitter.Touch();
    this.connection.SendPrivateText(target_id, message);
    return !this.sendLimitter.IsLimit();
  }

  public bool SendPrivateStamp(string target_id, int stampId)
  {
    if (this.sendLimitter.IsLimit())
      return false;
    this.sendLimitter.Touch();
    this.connection.SendPrivateStamp(target_id, stampId);
    return !this.sendLimitter.IsLimit();
  }

  public void Disconnect(System.Action onFinished = null) => this.connection.Disconnect(onFinished);

  private void _OnJoin(CHAT_ERROR_TYPE errorType, string userId)
  {
    if (this.onJoin == null)
      return;
    this.onJoin(errorType, userId);
  }

  private void _OnLeave(CHAT_ERROR_TYPE errorType, string userId)
  {
    if (this.onLeave == null)
      return;
    this.onLeave(errorType, userId);
  }

  private void _OnReceiveText(ClanChatLogMessageData clanChatMsgData)
  {
    if (this.onReceiveText == null)
      return;
    this.onReceiveText(clanChatMsgData);
  }

  private void _OnReceiveStamp(ClanChatLogMessageData clanChatMsgData)
  {
    if (this.onReceiveStamp == null)
      return;
    this.onReceiveStamp(clanChatMsgData);
  }

  private void _OnReceivePrivateText(ClanChatLogMessageData clanChatMsgData)
  {
    if (this.onReceivePrivateText == null)
      return;
    this.onReceivePrivateText(clanChatMsgData);
  }

  private void _OnReceivePrivateStamp(ClanChatLogMessageData clanChatMsgData)
  {
    if (this.onReceivePrivateStamp == null)
      return;
    this.onReceivePrivateStamp(clanChatMsgData);
  }

  private void _OnReceiveNotification(string message)
  {
    if (this.onReceiveNotification == null)
      return;
    this.onReceiveNotification(message);
  }

  private void _OnReceiveUpdateStatus(ClanUpdateStatusData clanUpdateStatusData)
  {
    if (this.onReceiveUpdateStatus == null)
      return;
    this.onReceiveUpdateStatus(clanUpdateStatusData);
  }

  private void _OnDisconnect()
  {
    if (this.onDisconnect == null)
      return;
    this.onDisconnect();
  }

  public override string ToString() => this.connection.ToString();

  public delegate void OnJoin(CHAT_ERROR_TYPE errorType, string userId);

  public delegate void OnLeave(CHAT_ERROR_TYPE errorType, string userId);

  public delegate void OnReceiveText(ClanChatLogMessageData clanChatMsgData);

  public delegate void OnReceiveStamp(ClanChatLogMessageData clanChatMsgData);

  public delegate void OnReceiveNotification(string message);

  public delegate void OnDisconnect();

  public delegate void OnReceiveUpdateStatus(ClanUpdateStatusData statusData);
}
