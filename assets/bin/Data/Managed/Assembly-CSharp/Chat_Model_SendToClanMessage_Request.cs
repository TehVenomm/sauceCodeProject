// Decompiled with JetBrains decompiler
// Type: Chat_Model_SendToClanMessage_Request
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class Chat_Model_SendToClanMessage_Request : Chat_Model_Base
{
  public string UserId { get; protected set; }

  public string NickName { get; protected set; }

  public string RoomId { get; protected set; }

  public string TimeStampClien { get; protected set; }

  public string Message { get; protected set; }

  public Chat_Model_SendToClanMessage_Request() => this.m_packetType = CHAT_PACKET_TYPE.CLAN_SENDTO;

  public override string Serialize()
  {
    return $"{$"{int.Parse(this.UserId):D32}"}{$"{int.Parse(this.RoomId):D32}"}{this.TimeStampClien}{$"{this.NickName}:{this.Message}"}";
  }

  public override string ToString() => this.Serialize();

  public static Chat_Model_SendToClanMessage_Request Create(
    string user_id,
    string user_name,
    string room_id,
    string message)
  {
    string str = DateTime.UtcNow.ToString("yyyyMMddhhmmssff");
    Chat_Model_SendToClanMessage_Request clanMessageRequest = new Chat_Model_SendToClanMessage_Request();
    clanMessageRequest.UserId = user_id;
    clanMessageRequest.NickName = user_name;
    clanMessageRequest.RoomId = room_id;
    clanMessageRequest.TimeStampClien = str;
    clanMessageRequest.Message = message;
    clanMessageRequest.payload = clanMessageRequest.Serialize();
    return clanMessageRequest;
  }
}
