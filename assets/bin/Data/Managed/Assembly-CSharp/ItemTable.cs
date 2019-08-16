using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class ItemTable : Singleton<ItemTable>, IDataTable
{
	public class ItemData
	{
		public uint id;

		public ITEM_TYPE type;

		public GET_TYPE getType;

		public int eventId;

		public string name;

		public string text;

		public int enemyIconID;

		public int enemyIconID2;

		public RARITY_TYPE rarity;

		public int iconID;

		public int price;

		public bool cantSale;

		public int element;

		public USE_ITEM_EFFECT_TYPE[] useEffectTypes;

		public int effectTime;

		public DateTime startDate;

		public DateTime endDate;

		public const string NT = "itemId,itemType,getType,eventId,name,text,enemyIconID,enemyIconID2,rarity,iconID,price,cantSell,element,effectType_0,effectType_1,effectType_2,effectTime,startDate,endDate";

		public static bool cb(CSVReader csv_reader, ItemData data, ref uint key)
		{
			data.id = key;
			csv_reader.Pop(ref data.type);
			csv_reader.Pop(ref data.getType);
			csv_reader.Pop(ref data.eventId);
			csv_reader.Pop(ref data.name);
			csv_reader.Pop(ref data.text);
			csv_reader.Pop(ref data.enemyIconID);
			csv_reader.Pop(ref data.enemyIconID2);
			csv_reader.Pop(ref data.rarity);
			csv_reader.Pop(ref data.iconID);
			csv_reader.Pop(ref data.price);
			csv_reader.Pop(ref data.cantSale);
			csv_reader.Pop(ref data.element);
			List<USE_ITEM_EFFECT_TYPE> list = new List<USE_ITEM_EFFECT_TYPE>();
			for (int i = 0; i < 3; i++)
			{
				USE_ITEM_EFFECT_TYPE value = USE_ITEM_EFFECT_TYPE.NONE;
				csv_reader.Pop(ref value);
				if (value != 0)
				{
					list.Add(value);
				}
			}
			data.useEffectTypes = list.ToArray();
			csv_reader.Pop(ref data.effectTime);
			string value2 = string.Empty;
			csv_reader.Pop(ref value2);
			if (!string.IsNullOrEmpty(value2))
			{
				DateTime.TryParse(value2, out data.startDate);
			}
			string value3 = string.Empty;
			csv_reader.Pop(ref value3);
			if (!string.IsNullOrEmpty(value3))
			{
				DateTime.TryParse(value3, out data.endDate);
			}
			return true;
		}
	}

	private UIntKeyTable<ItemData> itemTable;

	[CompilerGenerated]
	private static TableUtility.CallBackUIntKeyReadCSV<ItemData> _003C_003Ef__mg_0024cache0;

	[CompilerGenerated]
	private static TableUtility.CallBackUIntKeyReadCSV<ItemData> _003C_003Ef__mg_0024cache1;

	public void CreateTable(string csv_text)
	{
		itemTable = TableUtility.CreateUIntKeyTable<ItemData>(csv_text, ItemData.cb, "itemId,itemType,getType,eventId,name,text,enemyIconID,enemyIconID2,rarity,iconID,price,cantSell,element,effectType_0,effectType_1,effectType_2,effectTime,startDate,endDate");
		itemTable.TrimExcess();
	}

	public void AddTable(string csv_text)
	{
		TableUtility.AddUIntKeyTable(itemTable, csv_text, ItemData.cb, "itemId,itemType,getType,eventId,name,text,enemyIconID,enemyIconID2,rarity,iconID,price,cantSell,element,effectType_0,effectType_1,effectType_2,effectTime,startDate,endDate");
	}

	public ItemData GetItemData(uint id)
	{
		if (itemTable == null)
		{
			return null;
		}
		ItemData itemData = itemTable.Get(id);
		if (itemData == null)
		{
			Log.TableError(this, id);
			itemData = new ItemData();
			itemData.name = Log.NON_DATA_NAME;
		}
		return itemData;
	}

	public bool IsExistItemData(uint id)
	{
		if (itemTable == null)
		{
			return false;
		}
		ItemData itemData = itemTable.Get(id);
		if (itemData == null)
		{
			return false;
		}
		return true;
	}

	public static int ChangeItemIdToSkillItemIdIfNeed(int id)
	{
		switch (id)
		{
		case 1001000:
			return 401900001;
		case 1001001:
			return 401900002;
		case 1001002:
			return 401900003;
		case 1001003:
			return 401900004;
		default:
			return id;
		}
	}

	public List<ItemData> GetItemTypeItemData(ITEM_TYPE type)
	{
		List<ItemData> returnData = new List<ItemData>();
		itemTable.ForEach(delegate(ItemData data)
		{
			if (data.type == type)
			{
				returnData.Add(data);
			}
		});
		return returnData;
	}
}
