// Decompiled with JetBrains decompiler
// Type: IClanChatConnection
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public interface IClanChatConnection
{
  bool isEstablished { get; }

  bool isConnecting { get; }

  event ClanChatRoom.OnJoin onJoin;

  event ClanChatRoom.OnLeave onLeave;

  event ClanChatRoom.OnReceiveText onReceiveText;

  event ClanChatRoom.OnReceiveStamp onReceiveStamp;

  event ClanChatRoom.OnReceiveText onReceivePrivateText;

  event ClanChatRoom.OnReceiveUpdateStatus onReceiveUpdateStatus;

  event ClanChatRoom.OnReceiveStamp onReceivePrivateStamp;

  event ClanChatRoom.OnReceiveNotification onReceiveNotification;

  event ClanChatRoom.OnDisconnect onDisconnect;

  void Connect();

  void Disconnect(System.Action onFinished = null);

  void Join(int roomNo, string userName);

  void Leave(int roomNo, string userName);

  void SendText(string message);

  void SendStamp(int stampId);

  void SendPrivateText(string targetId, string message);

  void SendPrivateStamp(string targetId, int stampId);
}
