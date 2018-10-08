using Network;
using UnityEngine;

public class PointShopItemList : MonoBehaviour
{
	public UILabel tradeNum;

	public UILabel remainingTime;

	public UILabel pointNum;

	public UILabel itemName;

	public UITexture pointIcon;

	public Transform itemIconRoot;

	public void SetUp(PointShopItem item, uint pointId, bool isChangable)
	{
		SetUpText(item, isChangable);
		SetUpPointIcon(pointIcon, pointId);
		SetUpItemIcon(item);
	}

	protected void SetUpText(PointShopItem item, bool isChangable)
	{
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
		pointNum.color = ((!isChangable) ? Color.red : Color.white);
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
