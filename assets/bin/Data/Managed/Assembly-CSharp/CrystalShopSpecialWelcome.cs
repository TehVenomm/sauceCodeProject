using System;

public class CrystalShopSpecialWelcome : CrystalShopSpecialStarter
{
	private enum UI
	{
		OBJ_MODEL
	}

	public override void UpdateUI()
	{
		SetModel((Enum)UI.OBJ_MODEL, "EyesChest_Open");
	}
}
