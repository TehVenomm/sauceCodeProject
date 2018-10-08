public static class SpAttackTypeExtension
{
	private const int ITEM_ICON_BG_ID_DEFAULT = 90000100;

	private const int ITEM_ICON_BG_ID_HEAT = 90000107;

	private const int ITEM_ICON_BG_ID_SOUL = 90000108;

	private const int ITEM_ICON_BG_ID_BURST = 90000109;

	public static string GetName(this SP_ATTACK_TYPE type)
	{
		return StringTable.Get(STRING_CATEGORY.SP_ATTACK_TYPE, (uint)type);
	}

	public static string GetBigFrameSpriteName(this SP_ATTACK_TYPE type)
	{
		switch (type)
		{
		default:
			return "SpAttackType_Frame_none";
		case SP_ATTACK_TYPE.HEAT:
			return "SpAttackType_Frame_heat";
		case SP_ATTACK_TYPE.SOUL:
			return "SpAttackType_Frame_soul";
		case SP_ATTACK_TYPE.BURST:
			return "SpAttackType_Frame_burst";
		}
	}

	public static string GetSmallFrameSpriteName(this SP_ATTACK_TYPE type)
	{
		switch (type)
		{
		default:
			return "SpAttackType_smallFrame_none";
		case SP_ATTACK_TYPE.HEAT:
			return "SpAttackType_smallFrame_heat";
		case SP_ATTACK_TYPE.SOUL:
			return "SpAttackType_smallFrame_soul";
		case SP_ATTACK_TYPE.BURST:
			return "SpAttackType_smallFrame_burst";
		}
	}

	public static string GetSpTypeTextSpriteName(this SP_ATTACK_TYPE type)
	{
		switch (type)
		{
		default:
			return "EquipRemodelingTxt_03";
		case SP_ATTACK_TYPE.HEAT:
			return "EquipRemodelingTxt_04";
		case SP_ATTACK_TYPE.SOUL:
			return "EquipRemodelingTxt_05";
		case SP_ATTACK_TYPE.BURST:
			return "EquipRemodelingTxt_06";
		}
	}

	public static int GetItemIconBGId(this SP_ATTACK_TYPE type)
	{
		switch (type)
		{
		default:
			return 90000100;
		case SP_ATTACK_TYPE.HEAT:
			return 90000107;
		case SP_ATTACK_TYPE.SOUL:
			return 90000108;
		case SP_ATTACK_TYPE.BURST:
			return 90000109;
		}
	}
}
