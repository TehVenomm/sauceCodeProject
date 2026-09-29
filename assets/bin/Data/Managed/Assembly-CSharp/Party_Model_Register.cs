// Decompiled with JetBrains decompiler
// Type: Party_Model_Register
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Party_Model_Register : Coop_Model_Base
{
  public string roomId;
  public int owner;
  public string ownerToken;
  public int uid;
  public string signature;
  public int pid;
  public int qid;

  public Party_Model_Register() => this.packetType = PACKET_TYPE.PARTY_REGISTER;

  public override string ToString()
  {
    return $"{base.ToString()},roomId={this.roomId},owner={(object) this.owner},ownerToken={this.ownerToken},uid={(object) this.uid},signature={this.signature},pid={(object) this.pid},qid={(object) this.qid}";
  }
}
