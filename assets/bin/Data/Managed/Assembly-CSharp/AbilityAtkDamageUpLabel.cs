public class AbilityAtkDamageUpLabel : AbilityAtkBase
{
	public string label;

	public override void init(Player _player, string target, int val)
	{
		base.init(_player, target, val);
		label = target;
	}

	public override AtkAttribute GetDamageRate(Character chara, AttackedHitStatusLocal status)
	{
		if (status.attackInfo.damageUpLabels != null)
		{
			int i = 0;
			for (int num = status.attackInfo.damageUpLabels.Length; i < num; i++)
			{
				if (label == status.attackInfo.damageUpLabels[i])
				{
					return base.GetDamageRate(chara, status);
				}
			}
		}
		return null;
	}
}
