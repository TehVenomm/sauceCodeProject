using Network;
using UnityEngine;

public class PointShopItemList
{
	public UILabel tradeNum;

	public UILabel remainingTime;

	public UILabel pointNum;

	public UILabel itemName;

	public UITexture pointIcon;

	public Transform itemIconRoot;

	public PointShopItemList()
		: this()
	{
	}

	public void SetUp(PointShopItem item, uint pointId, bool isChangable)
	{
		SetUpText(item, isChangable);
		SetUpPointIcon(pointIcon, pointId);
		SetUpItemIcon(item);
	}

	protected void SetUpText(PointShopItem item, bool isChangable)
	{
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		itemName.text = item.name;
		if (item.hasLimit)
		{
			switch (item.limitPeriodType)
			{
			case POINT_SHOP_ITEM_LIMIT_TYPE.DAILY:
				tradeNum.text = string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 3u), item.limit - item.buyCount);
				break;
			case POINT_SHOP_ITEM_LIMIT_TYPE.WEEKLY:
				tradeNum.text = string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 4u), item.limit - item.buyCount);
				break;
			case POINT_SHOP_ITEM_LIMIT_TYPE.MONTHLY:
				tradeNum.text = string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 5u), item.limit - item.buyCount);
				break;
			default:
				tradeNum.text = string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 1u), item.limit - item.buyCount);
				break;
			}
		}
		else
		{
			tradeNum.text = StringTable.Get(STRING_CATEGORY.POINT_SHOP, 0u);
		}
		pointNum.text = string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2u), item.needPoint);
		pointNum.color = ((!isChangable) ? Color.get_red() : Color.get_white());
		remainingTime.text = item.expire;
	}

	protected void SetUpPointIcon(UITexture targetUITexture, uint pointId)
	{
		ResourceLoad.LoadPointIconImageTexture(targetUITexture, pointId);
	}

	protected void SetUpItemIcon(PointShopItem item)
	{
		ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon((REWARD_TYPE)item.type, (uint)item.itemId, itemIconRoot, -1, null, 0, false, -1, false, null, false, false, ItemIcon.QUEST_ICON_SIZE_TYPE.DEFAULT);
		itemIcon.SetEnableCollider(false);
	}
}
