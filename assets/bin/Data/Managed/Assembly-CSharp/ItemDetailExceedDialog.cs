public class ItemDetailExceedDialog : SmithExceedDialog
{
	protected override bool IsValidExceedSection()
	{
		return false;
	}

	protected override void SetupExceedData()
	{
		object[] array = GameSection.GetEventData() as object[];
		itemTable = (array[0] as EquipItemTable.EquipItemData);
		exceedCount = (int)array[1];
	}
}
