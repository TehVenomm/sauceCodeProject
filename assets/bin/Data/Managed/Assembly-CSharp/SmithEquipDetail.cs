using System;

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
		SetActive((Enum)UI.BTN_CHANGE, is_visible: false);
		SetActive((Enum)UI.BTN_CREATE, is_visible: false);
		SetActive((Enum)UI.BTN_GROW, is_visible: false);
	}
}
