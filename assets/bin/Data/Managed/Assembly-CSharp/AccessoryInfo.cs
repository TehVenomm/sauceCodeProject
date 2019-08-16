using Network;
using System.Collections.Generic;

public class AccessoryInfo : ItemInfoBase<Accessory>
{
	public bool isFavorite;

	public AccessoryTable.AccessoryData tableData;

	public List<AccessoryTable.AccessoryInfoData> tableInfos;

	public static InventoryList<AccessoryInfo, Accessory> CreateList(List<Accessory> recv_list)
	{
		InventoryList<AccessoryInfo, Accessory> list = new InventoryList<AccessoryInfo, Accessory>();
		if (!recv_list.IsNullOrEmpty())
		{
			recv_list.ForEach(delegate(Accessory o)
			{
				list.Add(o);
			});
		}
		return list;
	}

	public override void SetValue(Accessory recieve)
	{
		if (Singleton<AccessoryTable>.IsValid())
		{
			tableData = Singleton<AccessoryTable>.I.GetData((uint)recieve.accessoryId);
			if (tableData != null)
			{
				tableInfos = Singleton<AccessoryTable>.I.GetInfoList((uint)recieve.accessoryId);
				base.uniqueID = ulong.Parse(recieve.uniqId);
				base.tableID = (uint)recieve.accessoryId;
				isFavorite = (recieve.is_locked != 0);
			}
		}
	}

	public void SetValue(uint aid)
	{
		if (Singleton<AccessoryTable>.IsValid())
		{
			tableData = Singleton<AccessoryTable>.I.GetData(aid);
			if (tableData != null)
			{
				tableInfos = Singleton<AccessoryTable>.I.GetInfoList(aid);
				base.tableID = aid;
				isFavorite = false;
			}
		}
	}

	public AccessoryTable.AccessoryInfoData GetInfo(ACCESSORY_PART part)
	{
		if (tableInfos.IsNullOrEmpty())
		{
			return null;
		}
		return tableInfos.Find((AccessoryTable.AccessoryInfoData i) => i.attachPlace == part);
	}
}
