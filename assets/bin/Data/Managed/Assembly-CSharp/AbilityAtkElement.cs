using System;

public class AbilityAtkElement : AbilityAtkBase
{
	public override void init(Player _player, string target, int val)
	{
		switch ((int)Enum.Parse(typeof(ELEMENT_TYPE), target))
		{
		case 0:
			attr.fire = (float)val * 0.01f;
			break;
		case 1:
			attr.water = (float)val * 0.01f;
			break;
		case 2:
			attr.thunder = (float)val * 0.01f;
			break;
		case 3:
			attr.soil = (float)val * 0.01f;
			break;
		case 4:
			attr.light = (float)val * 0.01f;
			break;
		case 5:
			attr.dark = (float)val * 0.01f;
			break;
		}
	}
}
