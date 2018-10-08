public class AbilityAtkBase
{
	protected Player player;

	protected AtkAttribute attr = new AtkAttribute();

	public virtual void init(Player _player, string target, int val)
	{
		player = _player;
		attr.normal = (float)val * 0.01f;
		attr.fire = (float)val * 0.01f;
		attr.water = (float)val * 0.01f;
		attr.thunder = (float)val * 0.01f;
		attr.soil = (float)val * 0.01f;
		attr.light = (float)val * 0.01f;
		attr.dark = (float)val * 0.01f;
	}

	public virtual AtkAttribute GetDamageRate(Character chara, AttackedHitStatusLocal status)
	{
		return attr;
	}
}
