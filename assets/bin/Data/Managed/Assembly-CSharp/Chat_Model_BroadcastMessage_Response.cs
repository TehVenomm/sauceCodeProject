// Decompiled with JetBrains decompiler
// Type: Chat_Model_BroadcastMessage_Response
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Chat_Model_BroadcastMessage_Response : Chat_Model_Base
{
  public string NickName { get; protected set; }

  public int RoomNumber { get; protected set; }

  public string RoomId { get; protected set; }

  public string TimeStampClient { get; protected set; }

  public string TimeStampServer { get; protected set; }

  public string SenderId { get; protected set; }

  public string SenderName { get; protected set; }

  public string Message { get; protected set; }

  public Chat_Model_BroadcastMessage_Response()
  {
    this.m_packetType = CHAT_PACKET_TYPE.BROADCAST_ROOM;
  }

  public static Chat_Model_Base Parse(string str)
  {
    string str1 = str.Substring(40, 32 /*0x20*/);
    string str2 = str.Substring(72, 8);
    string str3 = str.Substring(80 /*0x50*/, 16 /*0x10*/);
    string str4 = str.Substring(96 /*0x60*/, 16 /*0x10*/);
    string str5 = str.Substring(112 /*0x70*/, 32 /*0x20*/);
    string[] strArray = str.Substring(144 /*0x90*/).Split(':');
    string str6 = strArray.Length == 2 ? strArray[0] : string.Empty;
    string str7 = strArray.Length == 2 ? strArray[1] : string.Empty;
    if (int.Parse(str2) != 0)
      return (Chat_Model_Base) null;
    Chat_Model_BroadcastMessage_Response broadcastMessageResponse = new Chat_Model_BroadcastMessage_Response();
    broadcastMessageResponse.NickName = str6;
    broadcastMessageResponse.RoomId = str1;
    broadcastMessageResponse.TimeStampClient = str3;
    broadcastMessageResponse.TimeStampServer = str4;
    broadcastMessageResponse.SenderId = str5;
    broadcastMessageResponse.SenderName = str6;
    broadcastMessageResponse.Message = str7;
    broadcastMessageResponse.SetErrorType(str2);
    return (Chat_Model_Base) broadcastMessageResponse;
  }
}
