// Decompiled with JetBrains decompiler
// Type: Chat_Model_LeaveRoom_Request
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Chat_Model_LeaveRoom_Request : Chat_Model_Base
{
  public string RoomId { get; protected set; }

  public Chat_Model_LeaveRoom_Request() => this.m_packetType = CHAT_PACKET_TYPE.LEAVE_ROOM;

  public override string Serialize() => $"{$"{0:D32}".Substring(this.RoomId.Length) + this.RoomId}";

  public override string ToString() => this.Serialize();

  public static Chat_Model_LeaveRoom_Request Create(string roomId)
  {
    Chat_Model_LeaveRoom_Request leaveRoomRequest = new Chat_Model_LeaveRoom_Request();
    leaveRoomRequest.RoomId = roomId;
    leaveRoomRequest.payload = leaveRoomRequest.Serialize();
    return leaveRoomRequest;
  }
}
