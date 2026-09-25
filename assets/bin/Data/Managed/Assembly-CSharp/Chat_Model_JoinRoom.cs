// Decompiled with JetBrains decompiler
// Type: Chat_Model_JoinRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Chat_Model_JoinRoom : Chat_Model_Base
{
  public string NickName { get; protected set; }

  public int RoomNumber { get; protected set; }

  public string RoomId { get; protected set; }

  public Chat_Model_JoinRoom() => this.m_packetType = CHAT_PACKET_TYPE.JOIN_ROOM;

  public override string Serialize()
  {
    return $"{$"{0:D32}".Substring(this.RoomId.Length) + this.RoomId}{this.NickName}";
  }

  public override string ToString() => this.Serialize();

  public static Chat_Model_Base Parse(string str)
  {
    Chat_Model_JoinRoom chatModelJoinRoom = new Chat_Model_JoinRoom();
    chatModelJoinRoom.m_packetType = CHAT_PACKET_TYPE.JOIN_ROOM;
    chatModelJoinRoom.payload = str.Substring(Chat_Model_Base.PAYLOAD_ORIGIN_INDEX);
    chatModelJoinRoom.RoomId = str.Substring(40, 32 /*0x20*/);
    chatModelJoinRoom.SetErrorType(str.Substring(72));
    return (Chat_Model_Base) chatModelJoinRoom;
  }

  public static Chat_Model_JoinRoom Create(string roomId, string nickName)
  {
    Chat_Model_JoinRoom chatModelJoinRoom = new Chat_Model_JoinRoom();
    chatModelJoinRoom.RoomId = roomId;
    chatModelJoinRoom.NickName = nickName;
    chatModelJoinRoom.payload = chatModelJoinRoom.Serialize();
    return chatModelJoinRoom;
  }
}
