public static class ItemIconTypeExtension
{
	public static bool IsEquip(this ITEM_ICON_TYPE type)
	{
		if ((uint)(type - 1) <= 8u)
		{
			return true;
		}
		return false;
	}
}
