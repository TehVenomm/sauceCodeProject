public class AbilityAtkNormal : AbilityAtkBase
{
	public override void init(Player _player, string target, int val)
	{
		attr.normal = (float)val * 0.01f;
	}
}
