public class AbilityLunaticTear : AbilityAtkBase
{
	public float validTime;

	public override void init(Player _player, string target, int val)
	{
		base.init(_player, target, 0);
		validTime = val;
	}
}
