// Decompiled with JetBrains decompiler
// Type: ChatClanConnection
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class ChatClanConnection : IChatConnection
{
  private bool established;

  public event ChatRoom.OnJoin onJoin;

  public event ChatRoom.OnReceiveText onReceiveText;

  public event ChatRoom.OnReceiveStamp onReceiveStamp;

  public event ChatRoom.OnReceiveNotification onReceiveNotification;

  public event ChatRoom.OnAfterSendUserMessage onAfterSendUserMessage;

  public event ChatRoom.OnDisconnect onDisconnect;

  public bool isEstablished => this.established;

  public void Connect() => this.established = true;

  public void Disconnect(System.Action onFinished = null)
  {
    this.established = false;
    if (onFinished == null)
      return;
    onFinished();
  }

  public void Join(int roomNo, string userName)
  {
    this.established = true;
    if (this.onJoin == null)
      return;
    this.onJoin(CHAT_ERROR_TYPE.NO_ERROR);
  }

  public void SendText(string message)
  {
    if (!MonoBehaviourSingleton<ClanMatchingManager>.IsValid())
      return;
    MonoBehaviourSingleton<ClanMatchingManager>.I.ChatMessage(message);
  }

  public void SendStamp(int stampId)
  {
    if (!MonoBehaviourSingleton<ClanMatchingManager>.IsValid())
      return;
    MonoBehaviourSingleton<ClanMatchingManager>.I.ChatStamp(stampId);
  }

  public void OnReceiveMessage(int userId, string userName, string message, string chatItemId = "")
  {
    if (this.onReceiveText == null)
      return;
    this.onReceiveText(userId, userName, message, chatItemId);
  }

  public void OnReceiveStamp(int userId, string userName, int stampId, string chatItemId = "")
  {
    if (this.onReceiveStamp == null)
      return;
    this.onReceiveStamp(userId, userName, stampId, chatItemId);
  }

  public void OnReceiveNotification(string message, string chatItemId = "")
  {
    if (this.onReceiveNotification == null)
      return;
    this.onReceiveNotification(message, chatItemId);
  }

  public void OnReceiveMessageOld(int userId, string userName, string message, string chatItemId = "")
  {
    if (this.onReceiveText == null)
      return;
    this.onReceiveText(userId, userName, message, chatItemId, true);
  }

  public void OnReceiveStampOld(int userId, string userName, int stampId, string chatItemId = "")
  {
    if (this.onReceiveStamp == null)
      return;
    this.onReceiveStamp(userId, userName, stampId, chatItemId, true);
  }

  public void OnReceiveNotificationOld(string message, string chatItemId = "")
  {
    if (this.onReceiveNotification == null)
      return;
    this.onReceiveNotification(message, chatItemId, true);
  }

  public void OnAfterSendUserMessage()
  {
    if (this.onAfterSendUserMessage == null)
      return;
    this.onAfterSendUserMessage();
  }
}
