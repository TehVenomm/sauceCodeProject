using System.Collections.Generic;

public class WaveMatchDropObjectHealHp : WaveMatchDropObject
{
	public override void OnPicked(Self self)
	{
		base.OnPicked(self);
		Character.HealData healData = new Character.HealData(tableData.value, HEAL_TYPE.NONE, HEAL_EFFECT_TYPE.BASIS, new List<int>
		{
			10
		});
		if (tableData.calcType == CALCULATE_TYPE.CONSTANT)
		{
			self.ExecHealHp(healData, false);
		}
		else
		{
			healData.healHp = (int)((float)self.hpMax * ((float)tableData.value * 0.01f));
			self.ExecHealHp(healData, false);
		}
	}
}
