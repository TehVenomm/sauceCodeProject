using Network;
using System;
using UnityEngine;

public class PointShopBuy : GameSection
{
	private enum UI
	{
		TEX_POINT_ICON,
		LBL_NEED_POINT,
		LBL_ITEM_NAME,
		OBJ_ITEM_ICON_ROOT,
		LBL_CURRENT_CHANGE_NUM,
		BTN_R,
		BTN_L
	}

	private PointShopItem currentItem;

	private PointShop pointShop;

	private Action<PointShopItem, int> onBuy;

	private int currentNum = 1;

	private int changableNum;

	public override string overrideBackKeyEvent => "NO";

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		if (array.Length == 3)
		{
			currentItem = (array[0] as PointShopItem);
			pointShop = (array[1] as PointShop);
			onBuy = (array[2] as Action<PointShopItem, int>);
			int a = currentItem.hasLimit ? (currentItem.limit - currentItem.buyCount) : int.MaxValue;
			changableNum = Mathf.Min(Mathf.Min(a, pointShop.userPoint / currentItem.needPoint), GameDefine.POINT_SHOP_MAX_BUY_LIMIT);
		}
		if (changableNum == 1)
		{
			SetActive(UI.BTN_L, is_visible: false);
			SetActive(UI.BTN_R, is_visible: false);
		}
		else
		{
			SetRepeatButton(UI.BTN_L, "L");
			SetRepeatButton(UI.BTN_R, "R");
		}
		base.Initialize();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		if (currentItem != null)
		{
			SetLabelText(UI.LBL_ITEM_NAME, currentItem.name);
			ItemIcon.CreateRewardItemIcon((REWARD_TYPE)currentItem.type, (uint)currentItem.itemId, GetCtrl(UI.OBJ_ITEM_ICON_ROOT)).SetEnableCollider(is_enable: false);
			ResourceLoad.LoadPointIconImageTexture(GetCtrl(UI.TEX_POINT_ICON).GetComponent<UITexture>(), (uint)pointShop.pointShopId);
		}
		SetLabelText(UI.LBL_CURRENT_CHANGE_NUM, string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2u), currentNum));
		SetLabelText(UI.LBL_NEED_POINT, string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2u), currentItem.needPoint * currentNum));
	}

	private void OnQuery_R()
	{
		if (currentNum < changableNum)
		{
			currentNum++;
		}
		else
		{
			currentNum = 1;
		}
		RefreshUI();
	}

	private void OnQuery_L()
	{
		if (currentNum > 1)
		{
			currentNum--;
		}
		else
		{
			currentNum = changableNum;
		}
		RefreshUI();
	}

	private void OnQuery_NO()
	{
		GameSection.BackSection();
	}

	private void OnQuery_YES()
	{
		if (onBuy != null && currentNum > 0)
		{
			onBuy(currentItem, currentNum);
		}
	}
}
