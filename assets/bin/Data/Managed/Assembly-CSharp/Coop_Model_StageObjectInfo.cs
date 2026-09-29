// Decompiled with JetBrains decompiler
// Type: Coop_Model_StageObjectInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Text;

#nullable disable
public class Coop_Model_StageObjectInfo : Coop_Model_Base
{
  public int StageObjectID;
  public StageObject.COOP_MODE_TYPE CoopModeType;

  public Coop_Model_StageObjectInfo()
  {
    this.packetType = PACKET_TYPE.STAGE_OBJECT_INFO;
    this.StageObjectID = 0;
    this.CoopModeType = StageObject.COOP_MODE_TYPE.NONE;
  }

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendFormat(",StageObjectID={0}", (object) this.StageObjectID);
    stringBuilder.AppendFormat(",CoopModeType={0}", (object) this.CoopModeType);
    return base.ToString() + stringBuilder.ToString();
  }
}
