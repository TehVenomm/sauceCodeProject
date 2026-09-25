// Decompiled with JetBrains decompiler
// Type: Coop_Model_WaveMatchDropPicked
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Coop_Model_WaveMatchDropPicked : Coop_Model_Base
{
  public int managedId;
  public uint tableId;

  public Coop_Model_WaveMatchDropPicked() => this.packetType = PACKET_TYPE.WAVEMATCH_DROP_PICKED;

  public override string ToString()
  {
    return $"{base.ToString()}/{this.managedId.ToString()}/{this.tableId.ToString()}";
  }
}
