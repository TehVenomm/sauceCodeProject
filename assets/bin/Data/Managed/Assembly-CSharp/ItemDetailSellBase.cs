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
		SetLabelText(UI.LBL_CAPTION, base.sectionData.GetText(key));
		SetLabelText(UI.STR_TITLE_U, base.sectionData.GetText(key));
		SetLabelText(UI.STR_TITLE_D, base.sectionData.GetText(key));
		string key2 = "TEXT_SELL_NUM";
		SetLabelText(UI.STR_SALE_NUM, base.sectionData.GetText(key2));
		SetProgressInt(UI.SLD_SALE_NUM, 1, 1, data.GetNum(), OnChagenSlider);
		SetLabelText(UI.LBL_ITEM_NUM, data.GetNum().ToString());
		SetLabelText(UI.LBL_MONEY, string.Format("{0, 8:#,0}", MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money));
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
		SetProgressInt(UI.SLD_SALE_NUM, GetProgressInt(UI.SLD_SALE_NUM) - 1, -1, -1, null);
	}

	private void OnQuery_SALE_NUM_PLUS()
	{
		SetProgressInt(UI.SLD_SALE_NUM, GetProgressInt(UI.SLD_SALE_NUM) + 1, -1, -1, null);
	}

	protected int GetSliderNum()
	{
		return GetProgressInt(UI.SLD_SALE_NUM);
	}
}
