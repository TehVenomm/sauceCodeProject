// Decompiled with JetBrains decompiler
// Type: ChatOfflineConnection
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class ChatOfflineConnection : IChatConnection
{
  public bool isEstablished => false;

  public event ChatRoom.OnJoin onJoin;

  public event ChatRoom.OnReceiveText onReceiveText;

  public event ChatRoom.OnReceiveStamp onReceiveStamp;

  public event ChatRoom.OnReceiveNotification onReceiveNotification;

  public event ChatRoom.OnAfterSendUserMessage onAfterSendUserMessage;

  public event ChatRoom.OnDisconnect onDisconnect;

  public void Connect()
  {
  }

  public void Disconnect(System.Action onFinished)
  {
    if (onFinished == null)
      return;
    onFinished();
  }

  public void Join(int roomNo, string userName)
  {
    if (this.onJoin == null)
      return;
    this.onJoin(CHAT_ERROR_TYPE.NO_ERROR);
  }

  public void SendText(string message)
  {
  }

  public void SendStamp(int stampId)
  {
  }
}
