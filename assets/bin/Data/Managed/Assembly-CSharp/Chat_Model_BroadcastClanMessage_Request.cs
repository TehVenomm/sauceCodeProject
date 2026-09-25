// Decompiled with JetBrains decompiler
// Type: Chat_Model_BroadcastClanMessage_Request
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class Chat_Model_BroadcastClanMessage_Request : Chat_Model_Base
{
  public string RoomId { get; protected set; }

  public string NickName { get; protected set; }

  public int RoomNumber { get; protected set; }

  public string TimeStampClien { get; protected set; }

  public string Message { get; protected set; }

  public Chat_Model_BroadcastClanMessage_Request()
  {
    this.m_packetType = CHAT_PACKET_TYPE.CLAN_BROADCAST_ROOM;
  }

  public override string Serialize()
  {
    return $"{$"{0:D32}".Substring(this.RoomId.Length) + this.RoomId}{this.TimeStampClien}{this.Message}";
  }

  public override string ToString() => this.Serialize();

  public static Chat_Model_BroadcastClanMessage_Request Create(
    string roomId,
    string name,
    string message)
  {
    string str = DateTime.UtcNow.ToString("yyyyMMddhhmmssff");
    Chat_Model_BroadcastClanMessage_Request clanMessageRequest = new Chat_Model_BroadcastClanMessage_Request();
    clanMessageRequest.RoomId = roomId;
    clanMessageRequest.TimeStampClien = str;
    clanMessageRequest.Message = message;
    clanMessageRequest.NickName = name;
    clanMessageRequest.payload = clanMessageRequest.Serialize();
    return clanMessageRequest;
  }
}
