using UnityEngine;

public class TradingPostInventoryDialog : ItemStorageTop
{
	private enum UI
	{
		SCR_INVENTORY,
		GRD_INVENTORY,
		GRD_INVENTORY_SMALL,
		SPR_SCR_BAR,
		SCR_INVENTORY_EQUIP,
		GRD_INVENTORY_EQUIP,
		GRD_INVENTORY_EQUIP_SMALL,
		SPR_EQUIP_SCR_BAR,
		TGL_CHANGE_INVENTORY,
		TGL_ICON_ASC,
		LBL_SORT,
		BTN_SORT,
		SPR_INVALID_SORT,
		LBL_INVALID_SORT,
		BTN_CHANGE,
		SPR_INVALID_CHANGE,
		TGL_TAB0,
		TGL_TAB1,
		TGL_TAB2,
		TGL_TAB3,
		TGL_TAB4,
		TGL_TAB5,
		OBJ_BTN_SELL_MODE,
		OBJ_SELL_MODE_ROOT,
		LBL_MAX_SELECT_NUM,
		LBL_SELECT_NUM,
		LBL_TOTAL,
		LBL_MAX_HAVE_NUM,
		LBL_NOW_HAVE_NUM,
		OBJ_CAPTION_3,
		LBL_CAPTION
	}

	protected override void InitSellObjectMode()
	{
	}

	protected override void InitListItemEvent(ItemIcon icon, int i, SortCompareData item)
	{
		SetEvent(icon.transform, "DETAIL", item);
	}

	protected override void InitializeCaption()
	{
		Transform ctrl = GetCtrl(UI.OBJ_CAPTION_3);
		UITweenCtrl component = ctrl.get_gameObject().GetComponent<UITweenCtrl>();
		if (component != null)
		{
			component.Reset();
			int i = 0;
			for (int num = component.tweens.Length; i < num; i++)
			{
				component.tweens[i].ResetToBeginning();
			}
			component.Play();
		}
	}

	protected override void ToDetail()
	{
		SortCompareData sortCompareData = GameSection.GetEventData() as SortCompareData;
		if (!TradingPostManager.IsItemValid(sortCompareData.GetTableID()))
		{
			GameSection.ChangeEvent("CANT_SELL");
		}
		else
		{
			MonoBehaviourSingleton<TradingPostManager>.I.SetTradingPostSellItemData(sortCompareData.GetTableID(), sortCompareData.GetUniqID(), sortCompareData.GetNum());
		}
	}
}
