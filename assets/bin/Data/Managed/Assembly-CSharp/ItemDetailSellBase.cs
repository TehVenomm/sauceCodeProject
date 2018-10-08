using System;

public class ItemDetailSellBase : GameSection
{
	protected enum UI
	{
		LBL_ITEM_NUM,
		LBL_SALE_NUM,
		LBL_SALE_PRICE,
		LBL_MONEY,
		LBL_CANT_SALE,
		BTN_SALE_NUM_MINUS,
		BTN_SALE_NUM_PLUS,
		SLD_SALE_NUM,
		SPR_SALE_FRAME,
		OBJ_MONEY_ROOT,
		STR_TITLE_U,
		STR_TITLE_D,
		STR_SALE_NUM,
		LBL_CAPTION
	}

	protected SortCompareData data;

	public override void Initialize()
	{
		data = (GameSection.GetEventData() as SortCompareData);
		base.Initialize();
	}

	public override void UpdateUI()
	{
		string key = "TEXT_SELL";
		SetLabelText((Enum)UI.LBL_CAPTION, base.sectionData.GetText(key));
		SetLabelText((Enum)UI.STR_TITLE_U, base.sectionData.GetText(key));
		SetLabelText((Enum)UI.STR_TITLE_D, base.sectionData.GetText(key));
		string key2 = "TEXT_SELL_NUM";
		SetLabelText((Enum)UI.STR_SALE_NUM, base.sectionData.GetText(key2));
		SetProgressInt((Enum)UI.SLD_SALE_NUM, 1, 1, data.GetNum(), (EventDelegate.Callback)OnChagenSlider);
		SetLabelText((Enum)UI.LBL_ITEM_NUM, data.GetNum().ToString());
		SetLabelText((Enum)UI.LBL_MONEY, string.Format("{0, 8:#,0}", MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money));
	}

	private void OnChagenSlider()
	{
		int progressInt = GetProgressInt((Enum)UI.SLD_SALE_NUM);
		int num = data.GetSalePrice() * progressInt;
		SetLabelText((Enum)UI.LBL_SALE_NUM, string.Format("{0,8:#,0}", progressInt));
		SetLabelText((Enum)UI.LBL_SALE_PRICE, string.Format("{0,8:#,0}", num));
	}

	private void OnQuery_SALE_NUM_MINUS()
	{
		SetProgressInt((Enum)UI.SLD_SALE_NUM, GetProgressInt((Enum)UI.SLD_SALE_NUM) - 1, -1, -1, (EventDelegate.Callback)null);
	}

	private void OnQuery_SALE_NUM_PLUS()
	{
		SetProgressInt((Enum)UI.SLD_SALE_NUM, GetProgressInt((Enum)UI.SLD_SALE_NUM) + 1, -1, -1, (EventDelegate.Callback)null);
	}

	protected int GetSliderNum()
	{
		return GetProgressInt((Enum)UI.SLD_SALE_NUM);
	}
}
