// Decompiled with JetBrains decompiler
// Type: TradingPostSoldModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class TradingPostSoldModel : Chat_Model_Base
{
  public string soldNum { get; protected set; }

  public TradingPostSoldModel() => this.m_packetType = CHAT_PACKET_TYPE.TRADING_POST_SOLD;

  public override string Serialize() => $"{this.soldNum}";

  public override string ToString() => this.Serialize();

  public static Chat_Model_Base Parse(string str)
  {
    TradingPostSoldModel tradingPostSoldModel = new TradingPostSoldModel();
    tradingPostSoldModel.m_packetType = CHAT_PACKET_TYPE.TRADING_POST_SOLD;
    tradingPostSoldModel.soldNum = str.Substring(41);
    tradingPostSoldModel.SetErrorType("0");
    return (Chat_Model_Base) tradingPostSoldModel;
  }
}
