public class AbilityAtkBurstSpearSpinMaxDamageUp : AbilityAtkWeapon
{
	public override void init(Player _player, string target, int val)
	{
		base.init(_player, "SPEAR", val);
	}

	public override AtkAttribute GetDamageRate(Character chara, AttackedHitStatusLocal status)
	{
		if (!player.spearCtrl.IsBurstSpin())
		{
			return null;
		}
		float rate = 0f;
		if (!player.spearCtrl.GetSpinRate(ref rate))
		{
			return null;
		}
		if (rate < 1f)
		{
			return null;
		}
		return base.GetDamageRate(chara, status);
	}
}
