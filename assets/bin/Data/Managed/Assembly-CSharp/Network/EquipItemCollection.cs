// Decompiled with JetBrains decompiler
// Type: Network.EquipItemCollection
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace Network;

[Serializable]
public class EquipItemCollection
{
  public string category;
  public string bit;

  public long Bit => long.Parse(this.bit);

  public bool CheckBit(int flag)
  {
    return flag >= 0 && flag < 64 /*0x40*/ && (this.Bit & 1L << flag) != 0L;
  }
}
