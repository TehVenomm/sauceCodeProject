public abstract class PPInputBase : GameSection
{
	protected enum UI
	{
		IPT_PW,
		BTN_OK,
		STR_REMOVE_PASS
	}

	public override void UpdateUI()
	{
		SetInput(UI.IPT_PW, string.Empty, 4, OnInputChange);
	}

	private void OnInputChange()
	{
		SetButtonEnabled(UI.BTN_OK, GetInputValue(UI.IPT_PW).Length == 4);
	}
}
