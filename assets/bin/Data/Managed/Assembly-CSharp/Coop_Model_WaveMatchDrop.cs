// Decompiled with JetBrains decompiler
// Type: Coop_Model_WaveMatchDrop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class Coop_Model_WaveMatchDrop : Coop_Model_Base
{
  public List<int> fiIds;
  public float sec;
  public int x;
  public int z;

  public Coop_Model_WaveMatchDrop() => this.packetType = PACKET_TYPE.WAVEMATCH_DROP;

  public override string ToString()
  {
    string str_fiIds = "";
    if (this.fiIds != null)
      this.fiIds.ForEach((Action<int>) (id => str_fiIds = $"{str_fiIds}{(object) id},"));
    string str = $"{$"{$",id={str_fiIds.Trim(',')}"},pos({(object) this.x}, {(object) this.z})"},sec={(object) this.sec}";
    return base.ToString() + str;
  }
}
