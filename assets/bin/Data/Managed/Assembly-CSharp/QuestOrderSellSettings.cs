public class QuestOrderSellSettings : GameSection
{
	private enum UI
	{
		LBL_ITEM_NUM,
		LBL_SALE_NUM,
		BTN_SALE_NUM_MINUS,
		BTN_SALE_NUM_PLUS,
		SLD_SALE_NUM,
		SPR_SALE_FRAME
	}

	private QuestInfoData quest;

	private int haveNum;

	private int sellNum;

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		quest = (array[0] as QuestInfoData);
		haveNum = (int)array[1];
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetLabelText(UI.LBL_ITEM_NUM, string.Format("{0, 8:#,0}", haveNum));
		SetProgressInt(UI.SLD_SALE_NUM, 1, 1, haveNum, OnChagenSlider);
	}

	private void OnChagenSlider()
	{
		int progressInt = GetProgressInt(UI.SLD_SALE_NUM);
		SetLabelText(UI.LBL_SALE_NUM, string.Format("{0,8:#,0}", progressInt));
	}

	private void OnQuery_SALE_NUM_MINUS()
	{
		SetProgressInt(UI.SLD_SALE_NUM, GetProgressInt(UI.SLD_SALE_NUM) - 1, -1, -1, null);
	}

	private void OnQuery_SALE_NUM_PLUS()
	{
		SetProgressInt(UI.SLD_SALE_NUM, GetProgressInt(UI.SLD_SALE_NUM) + 1, -1, -1, null);
	}

	private void OnQuery_SELL()
	{
		sellNum = GetProgressInt(UI.SLD_SALE_NUM);
		GameSection.SetEventData(new object[2]
		{
			quest.questData.tableData.questText,
			sellNum.ToString()
		});
	}

	public void OnQuery_QuestSellOrderConfirm_YES()
	{
		GameSection.SetEventData(new object[2]
		{
			quest.questData.tableData.questID,
			sellNum
		});
	}
}
