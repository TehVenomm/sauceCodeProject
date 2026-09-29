// Decompiled with JetBrains decompiler
// Type: Coop_Model_GatherGimmickInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Text;

#nullable disable
public class Coop_Model_GatherGimmickInfo : Coop_Model_Base
{
  public int managedId;
  public int ownerId;
  public bool isUsed;

  public Coop_Model_GatherGimmickInfo() => this.packetType = PACKET_TYPE.GATHER_GIMMICK_INFO;

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendFormat(",managedId={0}", (object) this.managedId);
    stringBuilder.AppendFormat(",ownerId={0}", (object) this.ownerId);
    stringBuilder.AppendFormat(",isUsed={0}", (object) this.isUsed);
    return base.ToString() + stringBuilder.ToString();
  }
}
