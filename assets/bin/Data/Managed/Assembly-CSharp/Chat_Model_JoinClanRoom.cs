// Decompiled with JetBrains decompiler
// Type: Chat_Model_JoinClanRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Chat_Model_JoinClanRoom : Chat_Model_Base
{
  public string NickName { get; protected set; }

  public string RoomId { get; protected set; }

  public int Owner { get; protected set; }

  public string Result { get; protected set; }

  public string UserId { get; protected set; }

  public Chat_Model_JoinClanRoom() => this.m_packetType = CHAT_PACKET_TYPE.CLAN_JOIN_ROOM;

  public override string Serialize()
  {
    return $"{$"{0:D32}".Substring(this.RoomId.Length) + this.RoomId}{this.NickName}";
  }

  public override string ToString() => this.Serialize();

  public static Chat_Model_Base Parse(string str)
  {
    string s = str.Substring(72, 8);
    Chat_Model_JoinClanRoom modelJoinClanRoom1;
    if (int.Parse(s) == 1)
    {
      Chat_Model_JoinClanRoom modelJoinClanRoom2 = new Chat_Model_JoinClanRoom();
      modelJoinClanRoom2.m_packetType = CHAT_PACKET_TYPE.CLAN_JOIN_ROOM;
      modelJoinClanRoom2.payload = str.Substring(Chat_Model_Base.PAYLOAD_ORIGIN_INDEX);
      modelJoinClanRoom2.RoomId = str.Substring(40, 32 /*0x20*/);
      modelJoinClanRoom2.Owner = int.Parse(s);
      modelJoinClanRoom1 = modelJoinClanRoom2;
      string errorCode = str.Substring(80 /*0x50*/);
      modelJoinClanRoom1.SetErrorType(errorCode);
    }
    else
    {
      Chat_Model_JoinClanRoom modelJoinClanRoom3 = new Chat_Model_JoinClanRoom();
      modelJoinClanRoom3.m_packetType = CHAT_PACKET_TYPE.CLAN_JOIN_ROOM;
      modelJoinClanRoom3.payload = str.Substring(Chat_Model_Base.PAYLOAD_ORIGIN_INDEX);
      modelJoinClanRoom3.RoomId = str.Substring(40, 32 /*0x20*/);
      modelJoinClanRoom3.UserId = str.Substring(80 /*0x50*/, 32 /*0x20*/);
      modelJoinClanRoom3.Owner = int.Parse(s);
      modelJoinClanRoom1 = modelJoinClanRoom3;
      modelJoinClanRoom1.SetErrorType("00000000");
    }
    return (Chat_Model_Base) modelJoinClanRoom1;
  }

  public static Chat_Model_JoinClanRoom Create(string roomId, string nickName)
  {
    Chat_Model_JoinClanRoom modelJoinClanRoom = new Chat_Model_JoinClanRoom();
    modelJoinClanRoom.RoomId = roomId;
    modelJoinClanRoom.NickName = nickName;
    modelJoinClanRoom.payload = modelJoinClanRoom.Serialize();
    return modelJoinClanRoom;
  }
}
