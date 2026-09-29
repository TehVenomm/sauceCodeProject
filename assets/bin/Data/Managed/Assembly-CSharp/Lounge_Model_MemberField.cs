// Decompiled with JetBrains decompiler
// Type: Lounge_Model_MemberField
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Lounge_Model_MemberField : Coop_Model_Base
{
  public int cid;
  public int fid;
  public int fmid;
  public int pid;
  public int qid;
  public bool h;

  public Lounge_Model_MemberField() => this.packetType = PACKET_TYPE.LOUNGE_MEMBER_FIELD;

  public override string ToString()
  {
    string str = $"{$"{$"{$"{$"{$",cid={(object) this.cid}"},fid={(object) this.fid}"},fmid={(object) this.fmid}"},pid={(object) this.pid}"},qid={(object) this.qid}"},host={this.h.ToString()}";
    return base.ToString() + str;
  }
}
