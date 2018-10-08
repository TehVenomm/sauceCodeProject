using System;

public class CrystalShopSpecialMonthlyMagi : CrystalShopSpecialStarter
{
	private enum UI
	{
		OBJ_MODEL
	}

	public override void UpdateUI()
	{
		SetModel((Enum)UI.OBJ_MODEL, "MonthlyMagiChest_Open");
	}
}
