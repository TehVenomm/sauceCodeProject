// Decompiled with JetBrains decompiler
// Type: Coop_Model_WaveMatchInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Coop_Model_WaveMatchInfo : Coop_Model_Base
{
  public int no;
  public int popGuardSec;
  public int intervalSec;
  public int isFinal;
  public int finalNo;

  public Coop_Model_WaveMatchInfo() => this.packetType = PACKET_TYPE.WAVEMATCH_INFO;

  public override string ToString()
  {
    string str = $"{$"{$"{$"{$",no={(object) this.no}"},popGuardSec={(object) this.popGuardSec}"},intervalSec={(object) this.intervalSec}"},isFinal={(object) this.isFinal}"},isFinal={(object) this.finalNo}";
    return base.ToString() + str;
  }
}
