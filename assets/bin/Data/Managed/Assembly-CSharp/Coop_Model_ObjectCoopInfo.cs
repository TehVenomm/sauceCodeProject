// Decompiled with JetBrains decompiler
// Type: Coop_Model_ObjectCoopInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Text;

#nullable disable
public class Coop_Model_ObjectCoopInfo : Coop_Model_Base
{
  public StageObject.COOP_MODE_TYPE CoopModeType;

  public Coop_Model_ObjectCoopInfo()
  {
    this.packetType = PACKET_TYPE.OBJECT_COOP_INFO;
    this.CoopModeType = StageObject.COOP_MODE_TYPE.NONE;
  }

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendFormat(",CoopModeType={0}", (object) this.CoopModeType);
    return base.ToString() + stringBuilder.ToString();
  }
}
