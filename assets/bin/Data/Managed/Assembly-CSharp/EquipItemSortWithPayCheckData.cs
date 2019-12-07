public class EquipItemSortWithPayCheckData : EquipItemSortData
{
	public override int getEquipFilterPay()
	{
		return MonoBehaviourSingleton<InventoryManager>.I.IsPay(equipData) & 3;
	}
}
