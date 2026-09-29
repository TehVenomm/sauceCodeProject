// Decompiled with JetBrains decompiler
// Type: ChatRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class ChatRoom
{
  private ChatSendLimitter sendLimitter;

  public string FromId { get; set; }

  public string MyName { get; private set; }

  public string RoomId { get; private set; }

  public int RoomNo { get; private set; }

  public bool HasConnect => this.connection != null && this.connection.isEstablished;

  public IChatConnection connection { get; private set; }

  public event ChatRoom.OnJoin onJoin;

  public event ChatRoom.OnJoinClan onJoinClan;

  public event ChatRoom.OnReceiveText onReceiveText;

  public event ChatRoom.OnReceiveStamp onReceiveStamp;

  public event ChatRoom.OnReceiveNotification onReceiveNotification;

  public event ChatRoom.OnAfterSendUserMessage onAfterSendUserMessage;

  public event ChatRoom.OnDisconnect onDisconnect;

  public ChatRoom()
  {
    GlobalSettingsManager.ChatParam chatParam = MonoBehaviourSingleton<GlobalSettingsManager>.I.chatParam;
    this.sendLimitter = new ChatSendLimitter(chatParam.limitCount, chatParam.limitDuration);
  }

  public bool CanSendMessage() => !this.sendLimitter.IsLimit();

  public void SetConnection(IChatConnection connection)
  {
    if (this.connection != null)
    {
      if (this.connection.isEstablished)
        this.connection.Disconnect();
      connection.onReceiveText -= new ChatRoom.OnReceiveText(this._OnReceiveText);
      connection.onReceiveStamp -= new ChatRoom.OnReceiveStamp(this._OnReceiveStamp);
      connection.onReceiveNotification -= new ChatRoom.OnReceiveNotification(this._OnReceiveNotification);
    }
    this.connection = connection;
    connection.onJoin += new ChatRoom.OnJoin(this._OnJoin);
    connection.onReceiveText += new ChatRoom.OnReceiveText(this._OnReceiveText);
    connection.onReceiveStamp += new ChatRoom.OnReceiveStamp(this._OnReceiveStamp);
    connection.onReceiveNotification += new ChatRoom.OnReceiveNotification(this._OnReceiveNotification);
    connection.onDisconnect += new ChatRoom.OnDisconnect(this._OnDisconnect);
    connection.onAfterSendUserMessage += new ChatRoom.OnAfterSendUserMessage(this._OnAfterSendUserMessage);
  }

  public void JoinRoom(int roomNo)
  {
    if (!MonoBehaviourSingleton<UserInfoManager>.IsValid())
      return;
    this.connection.Join(roomNo, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name);
  }

  public void JoinClanRoom(int roomNo)
  {
    if (!MonoBehaviourSingleton<UserInfoManager>.IsValid())
      return;
    this.connection.Join(roomNo, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name);
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

  public void Disconnect(System.Action onFinished = null) => this.connection.Disconnect(onFinished);

  private void _OnJoin(CHAT_ERROR_TYPE errorType)
  {
    if (this.onJoin == null)
      return;
    this.onJoin(errorType);
  }

  private void _OnJoinClan(CHAT_ERROR_TYPE errorType, bool isOwner, string userId)
  {
    if (this.onJoinClan == null)
      return;
    this.onJoinClan(errorType, isOwner, userId);
  }

  private void _OnReceiveText(
    int userId,
    string userName,
    string message,
    string chatItemId,
    bool isOldMessage = false)
  {
    if (this.onReceiveText == null)
      return;
    this.onReceiveText(userId, userName, message, chatItemId, isOldMessage);
  }

  private void _OnReceiveStamp(
    int userId,
    string userName,
    int stampId,
    string chatItemId,
    bool isOldMessage = false)
  {
    if (this.onReceiveStamp == null)
      return;
    this.onReceiveStamp(userId, userName, stampId, chatItemId, isOldMessage);
  }

  private void _OnReceiveNotification(string message, string chatItemId, bool isOldMessage = false)
  {
    if (this.onReceiveNotification == null)
      return;
    this.onReceiveNotification(message, chatItemId, isOldMessage);
  }

  private void _OnAfterSendUserMessage()
  {
    if (this.onAfterSendUserMessage == null)
      return;
    this.onAfterSendUserMessage();
  }

  private void _OnDisconnect()
  {
    if (this.onDisconnect == null)
      return;
    this.onDisconnect();
  }

  public override string ToString() => this.connection.ToString();

  public delegate void OnJoin(CHAT_ERROR_TYPE errorType);

  public delegate void OnJoinClan(CHAT_ERROR_TYPE errorType, bool owner, string userId);

  public delegate void OnReceiveText(
    int userId,
    string userName,
    string message,
    string chatItemId,
    bool isOldMessage = false);

  public delegate void OnReceiveStamp(
    int userId,
    string userName,
    int stampId,
    string chatItemId,
    bool isOldMessage = false);

  public delegate void OnReceiveNotification(string message, string chatItemId, bool isOldMessage = false);

  public delegate void OnAfterSendUserMessage();

  public delegate void OnDisconnect();
}
