using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

public class LimitedEquipItemExceedTable : Singleton<LimitedEquipItemExceedTable>, IDataTable
{
	public class LimitedEquipItemExceedData
	{
		public class ExceedNeedItem
		{
			public uint itemId;

			public uint[] num;

			public uint getNeedNum(int exceedCnt)
			{
				if (exceedCnt <= 0 || exceedCnt > num.Length)
				{
					return 0u;
				}
				return num[exceedCnt - 1];
			}
		}

		public uint id;

		public RARITY_TYPE rarity;

		public EQUIPMENT_TYPE equipmentType;

		public GET_TYPE getType;

		public int eventId;

		public int equipmentId;

		public ExceedNeedItem exceed;

		public const string NT = "id,rarity,equipmentType,getType,eventId,equipmentId,exceedItemId,exceedNum1,exceedNum2,exceedNum3,exceedNum4";

		public static bool cb(CSVReader csv_reader, LimitedEquipItemExceedData data, ref uint key)
		{
			data.id = key;
			csv_reader.PopEnum(ref data.rarity, RARITY_TYPE.D);
			csv_reader.PopEnum(ref data.equipmentType, EQUIPMENT_TYPE.NONE);
			csv_reader.PopEnum(ref data.getType, GET_TYPE.NONE);
			csv_reader.Pop(ref data.eventId);
			csv_reader.Pop(ref data.equipmentId);
			ExceedNeedItem exceedNeedItem = new ExceedNeedItem();
			uint value = 0u;
			csv_reader.Pop(ref value);
			exceedNeedItem.itemId = value;
			exceedNeedItem.num = new uint[4];
			for (int i = 0; i < exceedNeedItem.num.Length; i++)
			{
				csv_reader.Pop(ref exceedNeedItem.num[i]);
			}
			data.exceed = exceedNeedItem;
			return true;
		}
	}

	private UIntKeyTable<LimitedEquipItemExceedData> limitedEquipItemExceedTable;

	[CompilerGenerated]
	private static TableUtility.CallBackUIntKeyReadCSV<LimitedEquipItemExceedData> _003C_003Ef__mg_0024cache0;

	public void CreateTable(string csv_text)
	{
		limitedEquipItemExceedTable = TableUtility.CreateUIntKeyTable<LimitedEquipItemExceedData>(csv_text, LimitedEquipItemExceedData.cb, "id,rarity,equipmentType,getType,eventId,equipmentId,exceedItemId,exceedNum1,exceedNum2,exceedNum3,exceedNum4");
		limitedEquipItemExceedTable.TrimExcess();
	}

	public LimitedEquipItemExceedData[] GetLimitedEquipItemExceedData(EquipItemTable.EquipItemData itemData)
	{
		if (limitedEquipItemExceedTable == null)
		{
			return null;
		}
		LimitedEquipItemExceedData[] validItemData = GetValidItemData();
		List<LimitedEquipItemExceedData> list = new List<LimitedEquipItemExceedData>();
		for (int i = 0; i < validItemData.Length; i++)
		{
			if (itemData.id == validItemData[i].equipmentId)
			{
				list.Add(validItemData[i]);
				continue;
			}
			if (itemData.rarity == validItemData[i].rarity && itemData.type == validItemData[i].equipmentType && itemData.getType == validItemData[i].getType)
			{
				if (validItemData[i].getType != GET_TYPE.EVENT)
				{
					list.Add(validItemData[i]);
					continue;
				}
				if (itemData.eventId == validItemData[i].eventId)
				{
					list.Add(validItemData[i]);
					continue;
				}
			}
			if (validItemData[i].equipmentType == EQUIPMENT_TYPE.NONE && validItemData[i].getType == GET_TYPE.NONE)
			{
				if (itemData.rarity == validItemData[i].rarity)
				{
					list.Add(validItemData[i]);
				}
			}
			else if (validItemData[i].equipmentType == EQUIPMENT_TYPE.NONE)
			{
				if (itemData.rarity == validItemData[i].rarity && itemData.getType == validItemData[i].getType)
				{
					list.Add(validItemData[i]);
				}
			}
			else if (validItemData[i].getType == GET_TYPE.NONE && itemData.rarity == validItemData[i].rarity && itemData.type == validItemData[i].equipmentType)
			{
				list.Add(validItemData[i]);
			}
		}
		list.Sort((LimitedEquipItemExceedData a, LimitedEquipItemExceedData b) => (int)(a.id - b.id));
		return list.Distinct().ToArray();
	}

	private LimitedEquipItemExceedData[] GetValidItemData()
	{
		DateTime now = TimeManager.GetNow();
		DateTime dateDefault = default(DateTime);
		List<LimitedEquipItemExceedData> validData = new List<LimitedEquipItemExceedData>();
		limitedEquipItemExceedTable.ForEach(delegate(LimitedEquipItemExceedData o)
		{
			ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(o.exceed.itemId);
			if (itemData != null && itemData.startDate <= now)
			{
				if (itemData.endDate.CompareTo(dateDefault) == 0 || itemData.endDate > now)
				{
					validData.Add(o);
				}
				else if (MonoBehaviourSingleton<InventoryManager>.I.GetHaveingItemNum(itemData.id) > 0)
				{
					validData.Add(o);
				}
			}
		});
		return validData.ToArray();
	}

	public bool IsLimitedLapis(uint itemId)
	{
		bool isLimitedLapis = false;
		limitedEquipItemExceedTable.ForEach(delegate(LimitedEquipItemExceedData o)
		{
			if (o.exceed.itemId == itemId)
			{
				isLimitedLapis = true;
			}
		});
		return isLimitedLapis;
	}
}
