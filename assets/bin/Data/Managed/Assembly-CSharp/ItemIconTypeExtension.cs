public static class ItemIconTypeExtension
{
	public static bool IsEquip(this ITEM_ICON_TYPE type)
	{
		switch (type)
		{
		case ITEM_ICON_TYPE.ONE_HAND_SWORD:
		case ITEM_ICON_TYPE.TWO_HAND_SWORD:
		case ITEM_ICON_TYPE.SPEAR:
		case ITEM_ICON_TYPE.PAIR_SWORDS:
		case ITEM_ICON_TYPE.ARROW:
		case ITEM_ICON_TYPE.ARMOR:
		case ITEM_ICON_TYPE.HELM:
		case ITEM_ICON_TYPE.ARM:
		case ITEM_ICON_TYPE.LEG:
			return true;
		default:
			return false;
		}
	}
}
