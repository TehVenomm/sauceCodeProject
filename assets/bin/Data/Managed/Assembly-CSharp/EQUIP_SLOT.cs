public static class EQUIP_SLOT
{
	public const int WEAPON_0 = 0;

	public const int WEAPON_1 = 1;

	public const int WEAPON_2 = 2;

	public const int ARMOR = 3;

	public const int HELM = 4;

	public const int ARM = 5;

	public const int LEG = 6;

	public const int MAX = 7;

	public const int WEAPON_NUM = 3;

	public static EQUIPMENT_TYPE ToType(int equip_slot)
	{
		switch (equip_slot)
		{
		case 3:
			return EQUIPMENT_TYPE.ARMOR;
		case 4:
			return EQUIPMENT_TYPE.HELM;
		case 5:
			return EQUIPMENT_TYPE.ARM;
		case 6:
			return EQUIPMENT_TYPE.LEG;
		default:
			return EQUIPMENT_TYPE.ONE_HAND_SWORD;
		}
	}

	public static int AvatatToEquip(int avatar_equip_slot)
	{
		switch (avatar_equip_slot)
		{
		case 0:
			return 3;
		case 1:
			return 4;
		case 2:
			return 5;
		case 3:
			return 6;
		default:
			return 0;
		}
	}
}
