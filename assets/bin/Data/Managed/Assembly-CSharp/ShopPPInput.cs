public class ShopPPInput : PPInputBase
{
	public override void UpdateUI()
	{
		SetActive(UI.STR_REMOVE_PASS, false);
		base.UpdateUI();
	}

	private void OnQuery_OK()
	{
		RequestEvent("PP_TO_BUY", GetInputValue(UI.IPT_PW));
	}
}
