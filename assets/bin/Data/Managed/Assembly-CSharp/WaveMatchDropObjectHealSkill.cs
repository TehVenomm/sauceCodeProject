public class WaveMatchDropObjectHealSkill : WaveMatchDropObject
{
	public override void OnPicked(Self self)
	{
		base.OnPicked(self);
		if (tableData.calcType == CALCULATE_TYPE.CONSTANT)
		{
			self.OnGetChargeSkillGauge(BuffParam.BUFFTYPE.SKILL_CHARGE, tableData.value, -1, false, false);
		}
		else
		{
			self.OnGetChargeSkillGauge(BuffParam.BUFFTYPE.SKILL_CHARGE_RATE, tableData.value, -1, false, false);
		}
	}
}
