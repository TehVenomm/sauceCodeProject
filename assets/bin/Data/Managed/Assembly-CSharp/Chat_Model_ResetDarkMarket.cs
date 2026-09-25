// Decompiled with JetBrains decompiler
// Type: Chat_Model_ResetDarkMarket
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Chat_Model_ResetDarkMarket : Chat_Model_Base
{
  public string endDate { get; protected set; }

  public Chat_Model_ResetDarkMarket() => this.m_packetType = CHAT_PACKET_TYPE.DARK_MARKET_RESET;

  public override string Serialize() => $"{this.endDate}";

  public override string ToString() => this.Serialize();

  public static Chat_Model_Base Parse(string str)
  {
    Chat_Model_ResetDarkMarket modelResetDarkMarket = new Chat_Model_ResetDarkMarket();
    modelResetDarkMarket.m_packetType = CHAT_PACKET_TYPE.DARK_MARKET_RESET;
    modelResetDarkMarket.payload = str.Substring(Chat_Model_Base.PAYLOAD_ORIGIN_INDEX);
    modelResetDarkMarket.endDate = str.Substring(40, 14);
    modelResetDarkMarket.SetErrorType("0");
    return (Chat_Model_Base) modelResetDarkMarket;
  }
}
