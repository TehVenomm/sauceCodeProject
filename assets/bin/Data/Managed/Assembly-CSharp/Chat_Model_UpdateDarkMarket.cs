// Decompiled with JetBrains decompiler
// Type: Chat_Model_UpdateDarkMarket
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Chat_Model_UpdateDarkMarket : Chat_Model_Base
{
  public string itemMarketId { get; protected set; }

  public string soldNum { get; protected set; }

  public Chat_Model_UpdateDarkMarket() => this.m_packetType = CHAT_PACKET_TYPE.DARK_MARKET_UPDATE;

  public override string Serialize() => $"{this.itemMarketId}--{this.soldNum}";

  public override string ToString() => this.Serialize();

  public static Chat_Model_Base Parse(string str)
  {
    Chat_Model_UpdateDarkMarket updateDarkMarket = new Chat_Model_UpdateDarkMarket();
    updateDarkMarket.m_packetType = CHAT_PACKET_TYPE.DARK_MARKET_UPDATE;
    updateDarkMarket.payload = str.Substring(Chat_Model_Base.PAYLOAD_ORIGIN_INDEX);
    updateDarkMarket.itemMarketId = str.Substring(40, 10);
    updateDarkMarket.soldNum = str.Substring(50, 10);
    updateDarkMarket.SetErrorType("0");
    return (Chat_Model_Base) updateDarkMarket;
  }
}
