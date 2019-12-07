using System;

namespace Network
{
	[Serializable]
	public class PointShopItem : Present
	{
		public int needPoint;

		public int limit = -1;

		public int limitType;

		public int buyCount;

		public string pointShopItemId;

		public bool hasLimit => limit != -1;

		public bool isBuyable
		{
			get
			{
				if (hasLimit)
				{
					return limit > buyCount;
				}
				return true;
			}
		}

		public POINT_SHOP_ITEM_LIMIT_TYPE limitPeriodType
		{
			get
			{
				if (!Enum.IsDefined(typeof(POINT_SHOP_ITEM_LIMIT_TYPE), limitType))
				{
					return POINT_SHOP_ITEM_LIMIT_TYPE.NONE;
				}
				return (POINT_SHOP_ITEM_LIMIT_TYPE)limitType;
			}
		}
	}
}
