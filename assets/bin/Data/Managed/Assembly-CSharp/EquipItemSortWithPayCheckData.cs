public class EquipItemSortWithPayCheckData : EquipItemSortData
{
	public override int getEquipFilterPay()
	{
		int num = 0;
		num = MonoBehaviourSingleton<InventoryManager>.I.IsPay(equipData);
		return num & 3;
	}
}
