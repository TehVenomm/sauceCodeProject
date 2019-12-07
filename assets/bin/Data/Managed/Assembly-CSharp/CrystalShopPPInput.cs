public class CrystalShopPPInput : PPInputBase
{
	public override void UpdateUI()
	{
		SetActive(UI.STR_REMOVE_PASS, is_visible: false);
		base.UpdateUI();
	}

	private void OnQuery_OK()
	{
		RequestEvent("PP_TO_BUY", GetInputValue(UI.IPT_PW));
	}
}
