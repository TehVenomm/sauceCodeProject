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

		public bool isBuyable => !hasLimit || limit > buyCount;

		public POINT_SHOP_ITEM_LIMIT_TYPE limitPeriodType => (POINT_SHOP_ITEM_LIMIT_TYPE)(Enum.IsDefined(typeof(POINT_SHOP_ITEM_LIMIT_TYPE), limitType) ? limitType : 0);
	}
}
