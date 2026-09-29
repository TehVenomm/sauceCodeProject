// Decompiled with JetBrains decompiler
// Type: Chat_Model_JackpotWin
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Chat_Model_JackpotWin : Chat_Model_Base
{
  public string jacpotData { get; protected set; }

  public Chat_Model_JackpotWin() => this.m_packetType = CHAT_PACKET_TYPE.JACKPOT_WIN_UPDATE;

  public override string Serialize() => $"{this.jacpotData}";

  public override string ToString() => this.Serialize();

  public static Chat_Model_Base Parse(string str)
  {
    Chat_Model_JackpotWin chatModelJackpotWin = new Chat_Model_JackpotWin();
    chatModelJackpotWin.m_packetType = CHAT_PACKET_TYPE.JACKPOT_WIN_UPDATE;
    chatModelJackpotWin.payload = str.Substring(Chat_Model_Base.PAYLOAD_ORIGIN_INDEX);
    chatModelJackpotWin.jacpotData = str.Substring(40);
    chatModelJackpotWin.SetErrorType("0");
    return (Chat_Model_Base) chatModelJackpotWin;
  }

  public static Chat_Model_JackpotWin Create(string flag)
  {
    Chat_Model_JackpotWin chatModelJackpotWin = new Chat_Model_JackpotWin();
    chatModelJackpotWin.jacpotData = flag;
    chatModelJackpotWin.payload = chatModelJackpotWin.Serialize();
    return chatModelJackpotWin;
  }
}
