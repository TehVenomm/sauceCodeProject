public class StatusExchangeEntrance : GameSection
{
	public enum UI
	{
		LBL_TITLE_U,
		LBL_TITLE_D,
		LBL_HAVE_RARE,
		LBL_HAVE_NORMAL,
		LBL_HAVE_GOLD
	}

	public override void Initialize()
	{
		base.Initialize();
	}

	protected override void OnOpen()
	{
		MonoBehaviourSingleton<ItemExchangeManager>.I.SetExchangeType(EXCHANGE_TYPE.NONE);
		base.OnOpen();
	}

	public override void UpdateUI()
	{
		SetLabelText(UI.LBL_TITLE_U, base.sectionData.GetText("WINDOW_TITLE"));
		SetLabelText(UI.LBL_TITLE_D, base.sectionData.GetText("WINDOW_TITLE"));
		SetLabelText(UI.LBL_HAVE_RARE, 0.ToString("N0"));
		SetLabelText(UI.LBL_HAVE_NORMAL, 0.ToString("N0"));
		SetLabelText(UI.LBL_HAVE_GOLD, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money.ToString("N0"));
		base.UpdateUI();
	}

	private void OnQuery_RARE()
	{
		MonoBehaviourSingleton<ItemExchangeManager>.I.SetExchangeType(EXCHANGE_TYPE.RARE);
		GameSection.ChangeEvent("EXCHANGE", null);
	}

	private void OnQuery_NORMAL()
	{
		MonoBehaviourSingleton<ItemExchangeManager>.I.SetExchangeType(EXCHANGE_TYPE.NORMAL);
		GameSection.ChangeEvent("EXCHANGE", null);
	}

	private void OnQuery_SELL()
	{
		MonoBehaviourSingleton<ItemExchangeManager>.I.SetExchangeType(EXCHANGE_TYPE.SELL);
		GameSection.ChangeEvent("EXCHANGE", null);
	}
}
