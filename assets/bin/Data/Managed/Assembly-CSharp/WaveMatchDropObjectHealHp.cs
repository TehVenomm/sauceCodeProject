// Decompiled with JetBrains decompiler
// Type: WaveMatchDropObjectHealHp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public class WaveMatchDropObjectHealHp : WaveMatchDropObject
{
  public override void OnPicked(Self self)
  {
    base.OnPicked(self);
    Character.HealData healData = new Character.HealData(this.tableData.value, HEAL_TYPE.NONE, HEAL_EFFECT_TYPE.BASIS, new List<int>()
    {
      10
    });
    if (this.tableData.calcType == CALCULATE_TYPE.CONSTANT)
    {
      self.ExecHealHp(healData, false);
    }
    else
    {
      healData.healHp = (int) ((double) self.hpMax * ((double) this.tableData.value * 0.0099999997764825821));
      self.ExecHealHp(healData, false);
    }
  }
}
