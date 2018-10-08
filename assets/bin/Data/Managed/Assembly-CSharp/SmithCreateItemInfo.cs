public class SmithCreateItemInfo
{
	public EquipItemTable.EquipItemData equipTableData;

	public CreateEquipItemTable.CreateEquipItemData smithCreateTableData;

	public SmithCreateItemInfo(EquipItemTable.EquipItemData equip, CreateEquipItemTable.CreateEquipItemData create_data)
	{
		equipTableData = equip;
		smithCreateTableData = create_data;
	}
}
