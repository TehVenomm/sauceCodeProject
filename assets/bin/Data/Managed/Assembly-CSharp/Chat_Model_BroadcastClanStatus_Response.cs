// Decompiled with JetBrains decompiler
// Type: Chat_Model_BroadcastClanStatus_Response
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Chat_Model_BroadcastClanStatus_Response : Chat_Model_Base
{
  public string Id { get; protected set; }

  public string RoomId { get; protected set; }

  public string Result { get; protected set; }

  public string Type { get; protected set; }

  public string Status { get; protected set; }

  public Chat_Model_BroadcastClanStatus_Response()
  {
    this.m_packetType = CHAT_PACKET_TYPE.CLAN_BROADCAST_STATUS;
  }

  public static Chat_Model_Base Parse(string str)
  {
    string str1 = str.Substring(40, 32 /*0x20*/);
    string str2 = str.Substring(72, 8);
    string str3 = str.Substring(80 /*0x50*/, 8);
    string str4 = str.Substring(88, 8);
    string str5 = str.Substring(96 /*0x60*/, 32 /*0x20*/);
    Chat_Model_BroadcastClanStatus_Response clanStatusResponse = new Chat_Model_BroadcastClanStatus_Response();
    clanStatusResponse.Id = str5;
    clanStatusResponse.RoomId = str1;
    clanStatusResponse.Result = str2;
    clanStatusResponse.Type = str3;
    clanStatusResponse.Status = str4;
    clanStatusResponse.SetErrorType("0");
    return (Chat_Model_Base) clanStatusResponse;
  }
}
