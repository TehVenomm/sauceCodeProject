public class CrystalShopSpecialMonthlyMagi : CrystalShopSpecialStarter
{
	private enum UI
	{
		OBJ_MODEL
	}

	public override void UpdateUI()
	{
		SetModel(UI.OBJ_MODEL, "MonthlyMagiChest_Open");
	}
}
