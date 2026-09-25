// Decompiled with JetBrains decompiler
// Type: Chat_Model_PartyInvite
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Chat_Model_PartyInvite : Chat_Model_Base
{
  public string flag { get; protected set; }

  public Chat_Model_PartyInvite() => this.m_packetType = CHAT_PACKET_TYPE.PARTY_INVITE;

  public override string Serialize() => $"{this.flag}";

  public override string ToString() => this.Serialize();

  public static Chat_Model_Base Parse(string str)
  {
    Chat_Model_PartyInvite modelPartyInvite = new Chat_Model_PartyInvite();
    modelPartyInvite.m_packetType = CHAT_PACKET_TYPE.PARTY_INVITE;
    modelPartyInvite.payload = str.Substring(Chat_Model_Base.PAYLOAD_ORIGIN_INDEX);
    modelPartyInvite.flag = str.Substring(40, 1);
    modelPartyInvite.SetErrorType("0");
    return (Chat_Model_Base) modelPartyInvite;
  }

  public static Chat_Model_PartyInvite Create(string flag)
  {
    Chat_Model_PartyInvite modelPartyInvite = new Chat_Model_PartyInvite();
    modelPartyInvite.flag = flag;
    modelPartyInvite.payload = modelPartyInvite.Serialize();
    return modelPartyInvite;
  }
}
