using System.Collections;
using UnityEngine;

public class TradingPostSellItemDialog : GameSection
{
	protected enum UI
	{
		OBJ_FRAME,
		SPR_FRAME,
		TITLE,
		STR_TITLE_U,
		STR_TITLE_D,
		OBJ_NUM_GEM,
		SPR_NUM_GEM_BG,
		LBL_NUM_GEM,
		STR_NUM_GEM,
		OBJ_SALE_NUM,
		STR_SALE_NUM,
		LBL_SALE_NUM,
		SLD_SALE_NUM,
		BTN_SALE_NUM_MINUS,
		BTN_SALE_NUM_PLUS,
		OBJ_SALE_PRICE,
		STR_SALE_PRICE_TEXT,
		STR_SALE_PRICE,
		SLD_SALE_PRICE,
		BTN_SALE_PRICE_MINUS,
		BTN_SALE_PRICE_PLUS,
		OBJ_BASE_PRICE,
		STR_BASE_PRICE,
		STR_SOLD_COUNT,
		LBL_BASE_PRICE,
		SPR_GEM_ICON,
		BTN_SELL,
		TEXT_BTN_SELL,
		LBL_CAPTION
	}

	protected TradingPostItemStartingAtPriceModel startingPrice;

	public override void Initialize()
	{
		StartCoroutine(DoInitialize());
		base.Initialize();
	}

	private IEnumerator DoInitialize()
	{
		int itemId = (int)MonoBehaviourSingleton<TradingPostManager>.I.tradingPostSellItemData.itemId;
		bool isRequestDone = false;
		MonoBehaviourSingleton<TradingPostManager>.I.SendRequestItemStartingAtPrice(itemId, delegate(bool isSuccess, TradingPostItemStartingAtPriceModel ret)
		{
			if (isSuccess)
			{
				isRequestDone = true;
				startingPrice = ret;
			}
		});
		while (isRequestDone)
		{
			yield return null;
		}
	}

	public override void UpdateUI()
	{
		string key = "TEXT_SELL";
		SetLabelText(UI.STR_TITLE_U, base.sectionData.GetText(key));
		SetLabelText(UI.STR_TITLE_D, base.sectionData.GetText(key));
		SetLabelText(UI.LBL_NUM_GEM, MonoBehaviourSingleton<TradingPostManager>.I.tradingPostSellItemData.itemQuantity);
		TradingPostTable.ItemData itemData = Singleton<TradingPostTable>.I.GetItemData(MonoBehaviourSingleton<TradingPostManager>.I.tradingPostSellItemData.itemId);
		SetProgressInt(UI.SLD_SALE_NUM, 1, 1, Mathf.Min(MonoBehaviourSingleton<TradingPostManager>.I.tradingPostSellItemData.itemQuantity, (int)itemData.maxQuantity), OnChangeSliderNum);
		SetLabelText(UI.LBL_SALE_NUM, string.Format("{0,8:#,0}", GetProgressInt(UI.SLD_SALE_NUM)));
		SetProgressInt(UI.SLD_SALE_PRICE, 1, MonoBehaviourSingleton<TradingPostManager>.I.tradingSellMinGem, MonoBehaviourSingleton<TradingPostManager>.I.tradingSellMaxGem, OnChangeSliderPrice);
		SetLabelText(UI.STR_SALE_PRICE, GetPriceRate());
		SetLabelText(UI.LBL_BASE_PRICE, startingPrice.result.unitPrice);
		SetLabelText(UI.STR_SOLD_COUNT, string.Format(base.sectionData.GetText("STR_NUM_PEOPLE_COUNT"), startingPrice.result.saleNumber));
		SetSupportEncoding(UI.STR_SOLD_COUNT, isEnable: true);
	}

	private void OnQuery_SALE_NUM_MINUS()
	{
		SetProgressInt(UI.SLD_SALE_NUM, GetProgressInt(UI.SLD_SALE_NUM) - 1);
	}

	private void OnQuery_SALE_NUM_PLUS()
	{
		SetProgressInt(UI.SLD_SALE_NUM, GetProgressInt(UI.SLD_SALE_NUM) + 1);
	}

	private void OnQuery_SALE_PRICE_MINUS()
	{
		SetProgressInt(UI.SLD_SALE_PRICE, GetProgressInt(UI.SLD_SALE_PRICE) - 1);
	}

	private void OnQuery_SALE_PRICE_PLUS()
	{
		SetProgressInt(UI.SLD_SALE_PRICE, GetProgressInt(UI.SLD_SALE_PRICE) + 1);
	}

	private void OnQuery_SALE()
	{
		GameSection.StayEvent();
		int progressInt = GetProgressInt(UI.SLD_SALE_NUM);
		_ = (float)GetProgressInt(UI.SLD_SALE_PRICE) / (float)progressInt;
		MonoBehaviourSingleton<TradingPostManager>.I.SendRequestSellItem((int)MonoBehaviourSingleton<TradingPostManager>.I.tradingPostSellItemData.uniqID, GetProgressInt(UI.SLD_SALE_NUM), GetProgressInt(UI.SLD_SALE_PRICE), delegate(bool isSuccess)
		{
			MonoBehaviourSingleton<TradingPostManager>.I.isRefreshTradingPost = isSuccess;
			GameSection.ResumeEvent(isSuccess);
		});
	}

	protected int GetSliderNum()
	{
		return GetProgressInt(UI.SLD_SALE_NUM);
	}

	protected int GetSliderPrice()
	{
		return GetProgressInt(UI.SLD_SALE_PRICE);
	}

	private void OnChangeSliderNum()
	{
		SetLabelText(UI.LBL_SALE_NUM, string.Format("{0,8:#,0}", GetProgressInt(UI.SLD_SALE_NUM)));
		SetLabelText(UI.STR_SALE_PRICE, GetPriceRate());
	}

	private void OnChangeSliderPrice()
	{
		SetLabelText(UI.STR_SALE_PRICE, GetPriceRate());
	}

	protected string GetPriceRate()
	{
		int progressInt = GetProgressInt(UI.SLD_SALE_NUM);
		int progressInt2 = GetProgressInt(UI.SLD_SALE_PRICE);
		float num = (float)progressInt2 / (float)progressInt;
		return string.Format(base.sectionData.GetText("STR_SALE_PRICE"), progressInt2, (num >= 10f) ? $"{num:#,0.}" : $"{num:#,0.00}");
	}
}
