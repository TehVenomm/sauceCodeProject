public class SmithEquipDetail : ItemDetailEquip
{
	protected override bool IsShowFrameBG()
	{
		return true;
	}

	public override void Initialize()
	{
		base.Initialize();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		SetActive(UI.BTN_CHANGE, false);
		SetActive(UI.BTN_CREATE, false);
		SetActive(UI.BTN_GROW, false);
	}
}
