using System;

public class CrystalShopPPInput : PPInputBase
{
	public override void UpdateUI()
	{
		SetActive((Enum)UI.STR_REMOVE_PASS, false);
		base.UpdateUI();
	}

	private void OnQuery_OK()
	{
		RequestEvent("PP_TO_BUY", GetInputValue((Enum)UI.IPT_PW));
	}
}
