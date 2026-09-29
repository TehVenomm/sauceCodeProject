// Decompiled with JetBrains decompiler
// Type: Coop_Model_WaveMatchDropCreate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class Coop_Model_WaveMatchDropCreate : Coop_Model_Base
{
  public int managedId;
  public uint dataId;
  public Vector3 basePos;
  public Vector3 offset;
  public float sec;

  public Coop_Model_WaveMatchDropCreate() => this.packetType = PACKET_TYPE.WAVEMATCH_DROP_CREATE;

  public override string ToString()
  {
    string str = $"{$"{$"{$"{$",mid={this.managedId.ToString()}"},did={this.dataId.ToString()}"},pos({(object) this.basePos.x}, {(object) this.basePos.z})"},offset({(object) this.offset.x}, {(object) this.offset.z})"},sec={(object) this.sec}";
    return base.ToString() + str;
  }
}
