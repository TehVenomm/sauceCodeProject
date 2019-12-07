using Network;
using System.Collections.Generic;

public class ItemInfo : ItemInfoBase<Item>
{
	public int num;

	public ItemTable.ItemData tableData;

	public List<ExpiredItem> expiredAtItem;

	public ItemInfo()
	{
	}

	public ItemInfo(Item recv_data)
	{
		SetValue(recv_data);
	}

	public override void SetValue(Item recv_data)
	{
		base.uniqueID = ulong.Parse(recv_data.uniqId);
		base.tableID = (uint)recv_data.itemId;
		num = recv_data.num;
		tableData = Singleton<ItemTable>.I.GetItemData(base.tableID);
	}

	public static InventoryList<ItemInfo, Item> CreateList(List<Item> recv_list)
	{
		InventoryList<ItemInfo, Item> list = new InventoryList<ItemInfo, Item>();
		recv_list.ForEach(delegate(Item o)
		{
			list.Add(o);
		});
		return list;
	}

	public static ItemInfo CreateItemInfo(Item item)
	{
		ItemInfo itemInfo = new ItemInfo();
		itemInfo.SetValue(item);
		return itemInfo;
	}

	public static ItemInfo CreateItemInfo(int itemId)
	{
		return CreateItemInfo(new Item
		{
			uniqId = "0",
			itemId = itemId,
			num = 0
		});
	}

	public new ITEM_TYPE GetType()
	{
		return tableData.type;
	}

	public int GetNum()
	{
		if (expiredAtItem == null)
		{
			return num;
		}
		return expiredAtItem.FindAll((ExpiredItem x) => x.CanUse()).Count;
	}
}
