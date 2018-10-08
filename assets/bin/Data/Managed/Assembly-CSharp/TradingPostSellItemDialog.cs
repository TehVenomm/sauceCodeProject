using System;
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
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoInitialize());
		base.Initialize();
	}

	private unsafe IEnumerator DoInitialize()
	{
		int itemId = (int)MonoBehaviourSingleton<TradingPostManager>.I.tradingPostSellItemData.itemId;
		bool isRequestDone = false;
		MonoBehaviourSingleton<TradingPostManager>.I.SendRequestItemStartingAtPrice(itemId, new Action<bool, TradingPostItemStartingAtPriceModel>((object)/*Error near IL_0048: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		while (isRequestDone)
		{
			yield return (object)null;
		}
	}

	public override void UpdateUI()
	{
		string key = "TEXT_SELL";
		SetLabelText((Enum)UI.STR_TITLE_U, base.sectionData.GetText(key));
		SetLabelText((Enum)UI.STR_TITLE_D, base.sectionData.GetText(key));
		SetLabelText(UI.LBL_NUM_GEM, MonoBehaviourSingleton<TradingPostManager>.I.tradingPostSellItemData.itemQuantity);
		TradingPostTable.ItemData itemData = Singleton<TradingPostTable>.I.GetItemData(MonoBehaviourSingleton<TradingPostManager>.I.tradingPostSellItemData.itemId);
		SetProgressInt((Enum)UI.SLD_SALE_NUM, 1, 1, Mathf.Min(MonoBehaviourSingleton<TradingPostManager>.I.tradingPostSellItemData.itemQuantity, (int)itemData.maxQuantity), (EventDelegate.Callback)OnChangeSliderNum);
		SetLabelText((Enum)UI.LBL_SALE_NUM, string.Format("{0,8:#,0}", GetProgressInt((Enum)UI.SLD_SALE_NUM)));
		SetProgressInt((Enum)UI.SLD_SALE_PRICE, 1, MonoBehaviourSingleton<TradingPostManager>.I.tradingSellMinGem, MonoBehaviourSingleton<TradingPostManager>.I.tradingSellMaxGem, (EventDelegate.Callback)OnChangeSliderPrice);
		SetLabelText((Enum)UI.STR_SALE_PRICE, GetPriceRate());
		SetLabelText(UI.LBL_BASE_PRICE, startingPrice.result.unitPrice);
		SetLabelText((Enum)UI.STR_SOLD_COUNT, string.Format(base.sectionData.GetText("STR_NUM_PEOPLE_COUNT"), startingPrice.result.saleNumber));
		SetSupportEncoding(UI.STR_SOLD_COUNT, true);
	}

	private void OnQuery_SALE_NUM_MINUS()
	{
		SetProgressInt((Enum)UI.SLD_SALE_NUM, GetProgressInt((Enum)UI.SLD_SALE_NUM) - 1, -1, -1, (EventDelegate.Callback)null);
	}

	private void OnQuery_SALE_NUM_PLUS()
	{
		SetProgressInt((Enum)UI.SLD_SALE_NUM, GetProgressInt((Enum)UI.SLD_SALE_NUM) + 1, -1, -1, (EventDelegate.Callback)null);
	}

	private void OnQuery_SALE_PRICE_MINUS()
	{
		SetProgressInt((Enum)UI.SLD_SALE_PRICE, GetProgressInt((Enum)UI.SLD_SALE_PRICE) - 1, -1, -1, (EventDelegate.Callback)null);
	}

	private void OnQuery_SALE_PRICE_PLUS()
	{
		SetProgressInt((Enum)UI.SLD_SALE_PRICE, GetProgressInt((Enum)UI.SLD_SALE_PRICE) + 1, -1, -1, (EventDelegate.Callback)null);
	}

	private void OnQuery_SALE()
	{
		GameSection.StayEvent();
		int progressInt = GetProgressInt((Enum)UI.SLD_SALE_NUM);
		int progressInt2 = GetProgressInt((Enum)UI.SLD_SALE_PRICE);
		float num = (float)progressInt2 / (float)progressInt;
		MonoBehaviourSingleton<TradingPostManager>.I.SendRequestSellItem((int)MonoBehaviourSingleton<TradingPostManager>.I.tradingPostSellItemData.uniqID, GetProgressInt((Enum)UI.SLD_SALE_NUM), GetProgressInt((Enum)UI.SLD_SALE_PRICE), delegate(bool isSuccess)
		{
			MonoBehaviourSingleton<TradingPostManager>.I.isRefreshTradingPost = isSuccess;
			GameSection.ResumeEvent(isSuccess, null, false);
		});
	}

	protected int GetSliderNum()
	{
		return GetProgressInt((Enum)UI.SLD_SALE_NUM);
	}

	protected int GetSliderPrice()
	{
		return GetProgressInt((Enum)UI.SLD_SALE_PRICE);
	}

	private void OnChangeSliderNum()
	{
		SetLabelText((Enum)UI.LBL_SALE_NUM, string.Format("{0,8:#,0}", GetProgressInt((Enum)UI.SLD_SALE_NUM)));
		SetLabelText((Enum)UI.STR_SALE_PRICE, GetPriceRate());
	}

	private void OnChangeSliderPrice()
	{
		SetLabelText((Enum)UI.STR_SALE_PRICE, GetPriceRate());
	}

	protected string GetPriceRate()
	{
		int progressInt = GetProgressInt((Enum)UI.SLD_SALE_NUM);
		int progressInt2 = GetProgressInt((Enum)UI.SLD_SALE_PRICE);
		float num = (float)progressInt2 / (float)progressInt;
		return string.Format(base.sectionData.GetText("STR_SALE_PRICE"), progressInt2, (!(num >= 10f)) ? $"{num:#,0.00}" : $"{num:#,0.}");
	}
}
