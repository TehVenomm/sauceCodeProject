// Decompiled with JetBrains decompiler
// Type: Chat_Model_BroadcastClanMessage_Response
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Chat_Model_BroadcastClanMessage_Response : Chat_Model_Base
{
  public string Uuid { get; protected set; }

  public string Id { get; protected set; }

  public string NickName { get; protected set; }

  public string RoomId { get; protected set; }

  public string TimeStampClient { get; protected set; }

  public string TimeStampServer { get; protected set; }

  public string SenderId { get; protected set; }

  public string SenderName { get; protected set; }

  public string Message { get; protected set; }

  public Chat_Model_BroadcastClanMessage_Response()
  {
    this.m_packetType = CHAT_PACKET_TYPE.CLAN_BROADCAST_ROOM;
  }

  public static Chat_Model_Base Parse(string str)
  {
    string str1 = str.Substring(40, 36);
    string str2 = str.Substring(76, 32 /*0x20*/);
    string str3 = str.Substring(108, 32 /*0x20*/);
    string str4 = str.Substring(140, 8);
    string str5 = str.Substring(148, 16 /*0x10*/);
    string str6 = str.Substring(164, 16 /*0x10*/);
    string str7 = str.Substring(180, 32 /*0x20*/);
    string[] strArray = str.Substring(212).Split(':');
    string str8 = strArray.Length == 2 ? strArray[0] : string.Empty;
    string str9 = strArray.Length == 2 ? strArray[1] : string.Empty;
    if (int.Parse(str4) != 0)
      return (Chat_Model_Base) null;
    Chat_Model_BroadcastClanMessage_Response clanMessageResponse = new Chat_Model_BroadcastClanMessage_Response();
    clanMessageResponse.Uuid = str1;
    clanMessageResponse.Id = str2;
    clanMessageResponse.NickName = str8;
    clanMessageResponse.RoomId = str3;
    clanMessageResponse.TimeStampClient = str5;
    clanMessageResponse.TimeStampServer = str6;
    clanMessageResponse.SenderId = str7;
    clanMessageResponse.SenderName = str8;
    clanMessageResponse.Message = str9;
    clanMessageResponse.SetErrorType(str4);
    return (Chat_Model_Base) clanMessageResponse;
  }
}
