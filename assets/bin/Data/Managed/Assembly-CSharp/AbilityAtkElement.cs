using System;

public class AbilityAtkElement : AbilityAtkBase
{
	public override void init(Player _player, string target, int val)
	{
		switch ((ELEMENT_TYPE)Enum.Parse(typeof(ELEMENT_TYPE), target))
		{
		case ELEMENT_TYPE.FIRE:
			attr.fire = (float)val * 0.01f;
			break;
		case ELEMENT_TYPE.WATER:
			attr.water = (float)val * 0.01f;
			break;
		case ELEMENT_TYPE.THUNDER:
			attr.thunder = (float)val * 0.01f;
			break;
		case ELEMENT_TYPE.SOIL:
			attr.soil = (float)val * 0.01f;
			break;
		case ELEMENT_TYPE.LIGHT:
			attr.light = (float)val * 0.01f;
			break;
		case ELEMENT_TYPE.DARK:
			attr.dark = (float)val * 0.01f;
			break;
		}
	}
}
