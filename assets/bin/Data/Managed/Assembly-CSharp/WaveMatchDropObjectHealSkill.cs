// Decompiled with JetBrains decompiler
// Type: WaveMatchDropObjectHealSkill
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class WaveMatchDropObjectHealSkill : WaveMatchDropObject
{
  public override void OnPicked(Self self)
  {
    base.OnPicked(self);
    if (this.tableData.calcType == CALCULATE_TYPE.CONSTANT)
      self.OnGetChargeSkillGauge(BuffParam.BUFFTYPE.SKILL_CHARGE, this.tableData.value, -1, isCorrectWaveMatch: false);
    else
      self.OnGetChargeSkillGauge(BuffParam.BUFFTYPE.SKILL_CHARGE_RATE, this.tableData.value, -1, isCorrectWaveMatch: false);
  }
}
