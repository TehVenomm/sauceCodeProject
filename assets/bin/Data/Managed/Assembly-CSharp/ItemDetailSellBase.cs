using UnityEngine;

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
		BTN_SALE_TP,
		STR_TITLE_U,
		STR_TITLE_D,
		STR_SALE_NUM,
		LBL_CAPTION,
		LBL_SELL_GOLD_U,
		LBL_SELL_GOLD_D,
		LBL_SELL_TRADING_POST_U,
		LBL_SELL_TRADING_POST_D
	}

	protected SortCompareData data;

	public override void Initialize()
	{
		data = (GameSection.GetEventData() as SortCompareData);
		base.Initialize();
	}

	public override void UpdateUI()
	{
		bool uiUpdateInstant = base.uiUpdateInstant;
		base.uiUpdateInstant = true;
		bool flag = TradingPostManager.IsItemValid(data.GetTableID());
		SetActive(UI.BTN_SALE_TP, flag);
		base.uiUpdateInstant = uiUpdateInstant;
		string key = "TEXT_SELL";
		SetLabelText(UI.LBL_CAPTION, base.sectionData.GetText(key));
		SetLabelText(UI.STR_TITLE_U, base.sectionData.GetText(key));
		SetLabelText(UI.STR_TITLE_D, base.sectionData.GetText(key));
		string key2 = "TEXT_SELL_NUM";
		SetLabelText(UI.STR_SALE_NUM, base.sectionData.GetText(key2));
		SetProgressInt(UI.SLD_SALE_NUM, 1, 1, data.GetNum(), OnChagenSlider);
		SetLabelText(UI.LBL_ITEM_NUM, data.GetNum().ToString());
		SetLabelText(UI.LBL_MONEY, string.Format("{0, 8:#,0}", MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money));
		string key3 = "STR_SELL_GOLD";
		SetLabelText(UI.LBL_SELL_GOLD_U, base.sectionData.GetText(key3));
		SetLabelText(UI.LBL_SELL_GOLD_D, base.sectionData.GetText(key3));
		string key4 = "STR_SELL_TP";
		SetLabelText(UI.LBL_SELL_TRADING_POST_U, base.sectionData.GetText(key4));
		SetLabelText(UI.LBL_SELL_TRADING_POST_D, base.sectionData.GetText(key4));
		if (flag)
		{
			UISprite component = GetCtrl(UI.SPR_SALE_FRAME).gameObject.GetComponent<UISprite>();
			if (component != null)
			{
				component.height += GetCtrl(UI.BTN_SALE_TP).gameObject.GetComponent<UISprite>().height;
			}
			else
			{
				Debug.LogError("Sprite frame is null");
			}
		}
	}

	private void OnChagenSlider()
	{
		int progressInt = GetProgressInt(UI.SLD_SALE_NUM);
		int num = data.GetSalePrice() * progressInt;
		SetLabelText(UI.LBL_SALE_NUM, string.Format("{0,8:#,0}", progressInt));
		SetLabelText(UI.LBL_SALE_PRICE, string.Format("{0,8:#,0}", num));
	}

	private void OnQuery_SALE_NUM_MINUS()
	{
		SetProgressInt(UI.SLD_SALE_NUM, GetProgressInt(UI.SLD_SALE_NUM) - 1);
	}

	private void OnQuery_SALE_NUM_PLUS()
	{
		SetProgressInt(UI.SLD_SALE_NUM, GetProgressInt(UI.SLD_SALE_NUM) + 1);
	}

	protected int GetSliderNum()
	{
		return GetProgressInt(UI.SLD_SALE_NUM);
	}
}
