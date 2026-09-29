// Decompiled with JetBrains decompiler
// Type: Chat_Model_RallyInvite
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Chat_Model_RallyInvite : Chat_Model_Base
{
  public string flag { get; protected set; }

  public Chat_Model_RallyInvite() => this.m_packetType = CHAT_PACKET_TYPE.RALLY_INVITE;

  public override string Serialize() => $"{this.flag}";

  public override string ToString() => this.Serialize();

  public static Chat_Model_Base Parse(string str)
  {
    Chat_Model_RallyInvite modelRallyInvite = new Chat_Model_RallyInvite();
    modelRallyInvite.m_packetType = CHAT_PACKET_TYPE.RALLY_INVITE;
    modelRallyInvite.payload = str.Substring(Chat_Model_Base.PAYLOAD_ORIGIN_INDEX);
    modelRallyInvite.flag = str.Substring(40, 1);
    modelRallyInvite.SetErrorType("0");
    return (Chat_Model_Base) modelRallyInvite;
  }

  public static Chat_Model_RallyInvite Create(string flag)
  {
    Chat_Model_RallyInvite modelRallyInvite = new Chat_Model_RallyInvite();
    modelRallyInvite.flag = flag;
    modelRallyInvite.payload = modelRallyInvite.Serialize();
    return modelRallyInvite;
  }
}
