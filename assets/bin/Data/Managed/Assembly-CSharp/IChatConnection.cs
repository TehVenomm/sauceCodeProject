// Decompiled with JetBrains decompiler
// Type: IChatConnection
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public interface IChatConnection
{
  bool isEstablished { get; }

  event ChatRoom.OnJoin onJoin;

  event ChatRoom.OnReceiveText onReceiveText;

  event ChatRoom.OnReceiveStamp onReceiveStamp;

  event ChatRoom.OnReceiveNotification onReceiveNotification;

  event ChatRoom.OnAfterSendUserMessage onAfterSendUserMessage;

  event ChatRoom.OnDisconnect onDisconnect;

  void Connect();

  void Disconnect(System.Action onFinished = null);

  void Join(int roomNo, string userName);

  void SendText(string message);

  void SendStamp(int stampId);
}
