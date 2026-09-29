// Decompiled with JetBrains decompiler
// Type: Chat_Model_LeaveClanRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Chat_Model_LeaveClanRoom : Chat_Model_Base
{
  public string NickName { get; protected set; }

  public string RoomId { get; protected set; }

  public int Owner { get; protected set; }

  public string Result { get; protected set; }

  public string UserId { get; protected set; }

  public Chat_Model_LeaveClanRoom() => this.m_packetType = CHAT_PACKET_TYPE.CLAN_LEAVE_ROOM;

  public override string Serialize()
  {
    return $"{$"{0:D32}".Substring(this.RoomId.Length) + this.RoomId}{this.NickName}";
  }

  public override string ToString() => this.Serialize();

  public static Chat_Model_Base Parse(string str)
  {
    string s = str.Substring(72, 8);
    Chat_Model_LeaveClanRoom modelLeaveClanRoom1;
    if (int.Parse(s) == 1)
    {
      Chat_Model_LeaveClanRoom modelLeaveClanRoom2 = new Chat_Model_LeaveClanRoom();
      modelLeaveClanRoom2.m_packetType = CHAT_PACKET_TYPE.CLAN_LEAVE_ROOM;
      modelLeaveClanRoom2.payload = str.Substring(Chat_Model_Base.PAYLOAD_ORIGIN_INDEX);
      modelLeaveClanRoom2.RoomId = str.Substring(40, 32 /*0x20*/);
      modelLeaveClanRoom2.Owner = int.Parse(s);
      modelLeaveClanRoom1 = modelLeaveClanRoom2;
      string errorCode = str.Substring(80 /*0x50*/);
      modelLeaveClanRoom1.SetErrorType(errorCode);
    }
    else
    {
      Chat_Model_LeaveClanRoom modelLeaveClanRoom3 = new Chat_Model_LeaveClanRoom();
      modelLeaveClanRoom3.m_packetType = CHAT_PACKET_TYPE.CLAN_LEAVE_ROOM;
      modelLeaveClanRoom3.payload = str.Substring(Chat_Model_Base.PAYLOAD_ORIGIN_INDEX);
      modelLeaveClanRoom3.RoomId = str.Substring(40, 32 /*0x20*/);
      modelLeaveClanRoom3.UserId = str.Substring(80 /*0x50*/, 32 /*0x20*/);
      modelLeaveClanRoom3.Owner = int.Parse(s);
      modelLeaveClanRoom1 = modelLeaveClanRoom3;
      modelLeaveClanRoom1.SetErrorType("00000000");
    }
    return (Chat_Model_Base) modelLeaveClanRoom1;
  }

  public static Chat_Model_LeaveClanRoom Create(string roomId, string nickName)
  {
    Chat_Model_LeaveClanRoom modelLeaveClanRoom = new Chat_Model_LeaveClanRoom();
    modelLeaveClanRoom.RoomId = roomId;
    modelLeaveClanRoom.NickName = nickName;
    modelLeaveClanRoom.payload = modelLeaveClanRoom.Serialize();
    return modelLeaveClanRoom;
  }
}
