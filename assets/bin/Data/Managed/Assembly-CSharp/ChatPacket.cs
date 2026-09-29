// Decompiled with JetBrains decompiler
// Type: ChatPacket
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class ChatPacket
{
  public ChatPacketHeader header { get; set; }

  public Chat_Model_Base model { get; set; }

  public CHAT_PACKET_TYPE packetType => (CHAT_PACKET_TYPE) this.header.cmd;

  public string fromClientId => this.header.fromId;

  public T GetModel<T>() where T : Chat_Model_Base => this.model as T;

  public override string ToString() => $"{this.header}{this.model}";

  public virtual PacketStream Serialize() => new PacketStream((object) this.ToString());

  public static ChatPacket Deserialize(PacketStream stream)
  {
    return stream.IsString() ? ChatPacket.Parse(stream.ToString()) : (ChatPacket) null;
  }

  public static ChatPacket Parse(string str)
  {
    ChatPacket chatPacket = new ChatPacket()
    {
      header = ChatPacketHeader.Parse(str)
    };
    chatPacket.model = ChatPacket.ParseModel(str, chatPacket.packetType);
    return chatPacket;
  }

  public static Chat_Model_Base ParseModel(string str, CHAT_PACKET_TYPE type)
  {
    switch (type)
    {
      case CHAT_PACKET_TYPE.JOIN_ROOM:
        return Chat_Model_JoinRoom.Parse(str);
      case CHAT_PACKET_TYPE.BROADCAST_ROOM:
        return Chat_Model_BroadcastMessage_Response.Parse(str);
      case CHAT_PACKET_TYPE.PARTY_INVITE:
        return Chat_Model_PartyInvite.Parse(str);
      case CHAT_PACKET_TYPE.CLAN_JOIN_ROOM:
        return Chat_Model_JoinClanRoom.Parse(str);
      case CHAT_PACKET_TYPE.RALLY_INVITE:
        return Chat_Model_RallyInvite.Parse(str);
      case CHAT_PACKET_TYPE.CLAN_LEAVE_ROOM:
        return Chat_Model_LeaveClanRoom.Parse(str);
      case CHAT_PACKET_TYPE.DARK_MARKET_RESET:
        return Chat_Model_ResetDarkMarket.Parse(str);
      case CHAT_PACKET_TYPE.DARK_MARKET_UPDATE:
        return Chat_Model_UpdateDarkMarket.Parse(str);
      case CHAT_PACKET_TYPE.JACKPOT_WIN_UPDATE:
        return Chat_Model_JackpotWin.Parse(str);
      case CHAT_PACKET_TYPE.TRADING_POST_SOLD:
        return TradingPostSoldModel.Parse(str);
      case CHAT_PACKET_TYPE.CLAN_BROADCAST_ROOM:
        return Chat_Model_BroadcastClanMessage_Response.Parse(str);
      case CHAT_PACKET_TYPE.CLAN_BROADCAST_STATUS:
        return Chat_Model_BroadcastClanStatus_Response.Parse(str);
      case CHAT_PACKET_TYPE.CLAN_SENDTO:
        return Chat_Model_SendToClanMessage_Response.Parse(str);
      default:
        return new Chat_Model_Base();
    }
  }
}
