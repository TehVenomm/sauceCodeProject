// Decompiled with JetBrains decompiler
// Type: SoulEnergyController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public class SoulEnergyController
{
  private List<SoulEnergy> collection = new List<SoulEnergy>();
  private Player cacheOwner;

  public void Initialize(Player owner) => this.cacheOwner = owner;

  public SoulEnergy Get(float baseValue)
  {
    if ((double) baseValue <= 0.0)
      return (SoulEnergy) null;
    SoulEnergy soulEnergy1 = (SoulEnergy) null;
    int index = 0;
    for (int count = this.collection.Count; index < count; ++index)
    {
      SoulEnergy soulEnergy2 = this.collection[index];
      if (soulEnergy2.canWork())
      {
        soulEnergy1 = soulEnergy2;
        break;
      }
    }
    if (soulEnergy1 == null)
    {
      soulEnergy1 = new SoulEnergy();
      soulEnergy1.Init();
      this.collection.Add(soulEnergy1);
    }
    soulEnergy1.Exec(this.cacheOwner, baseValue);
    return soulEnergy1;
  }

  public void Sleep()
  {
    int index = 0;
    for (int count = this.collection.Count; index < count; ++index)
      this.collection[index].Sleep();
  }

  public void Tap()
  {
    int index = 0;
    for (int count = this.collection.Count; index < count; ++index)
      this.collection[index].Tap();
  }

  public void Absorbed()
  {
    int index = 0;
    for (int count = this.collection.Count; index < count; ++index)
      this.collection[index].Absorbed();
  }

  public void Clear()
  {
    int index = 0;
    for (int count = this.collection.Count; index < count; ++index)
      this.collection[index] = (SoulEnergy) null;
    this.collection.Clear();
  }
}
