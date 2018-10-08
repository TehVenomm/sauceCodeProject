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
		SetActive((Enum)UI.BTN_CHANGE, false);
		SetActive((Enum)UI.BTN_CREATE, false);
		SetActive((Enum)UI.BTN_GROW, false);
	}
}
