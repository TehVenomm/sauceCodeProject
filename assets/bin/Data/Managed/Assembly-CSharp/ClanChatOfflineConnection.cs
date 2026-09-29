// Decompiled with JetBrains decompiler
// Type: ClanChatOfflineConnection
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class ClanChatOfflineConnection : IClanChatConnection
{
  public bool isEstablished => false;

  public bool isConnecting => false;

  public event ClanChatRoom.OnJoin onJoin;

  public event ClanChatRoom.OnLeave onLeave;

  public event ClanChatRoom.OnReceiveText onReceiveText;

  public event ClanChatRoom.OnReceiveStamp onReceiveStamp;

  public event ClanChatRoom.OnReceiveText onReceivePrivateText;

  public event ClanChatRoom.OnReceiveStamp onReceivePrivateStamp;

  public event ClanChatRoom.OnReceiveNotification onReceiveNotification;

  public event ClanChatRoom.OnDisconnect onDisconnect;

  public event ClanChatRoom.OnReceiveUpdateStatus onReceiveUpdateStatus;

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
    this.onJoin(CHAT_ERROR_TYPE.NO_ERROR, (string) null);
  }

  public void Leave(int roomNo, string userName)
  {
    if (this.onLeave == null)
      return;
    this.onLeave(CHAT_ERROR_TYPE.NO_ERROR, (string) null);
  }

  public void SendText(string message)
  {
  }

  public void SendStamp(int stampId)
  {
  }

  public void SendPrivateText(string targetId, string message)
  {
  }

  public void SendPrivateStamp(string targetId, int stampId)
  {
  }
}
