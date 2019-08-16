using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

public class EquipItemExceedTable : Singleton<EquipItemExceedTable>, IDataTable
{
	public class EquipItemExceedData
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

		public RARITY_TYPE rarity;

		public GET_TYPE getType;

		public int eventId;

		public uint exchangeItemId;

		public uint exchangeMoney;

		public ExceedNeedItem[] exceed;

		public const string NT = "rarity,type,eventId,exchangeItemId,exchangeMoney,exceedItemId0,exceedNum0_1,exceedNum0_2,exceedNum0_3,exceedNum0_4,exceedItemId1,exceedNum1_1,exceedNum1_2,exceedNum1_3,exceedNum1_4,exceedItemId2,exceedNum2_1,exceedNum2_2,exceedNum2_3,exceedNum2_4,exceedItemId3,exceedNum3_1,exceedNum3_2,exceedNum3_3,exceedNum3_4,exceedItemId4,exceedNum4_1,exceedNum4_2,exceedNum4_3,exceedNum4_4";

		public EquipItemExceedData Clone()
		{
			EquipItemExceedData equipItemExceedData = new EquipItemExceedData();
			equipItemExceedData.rarity = rarity;
			equipItemExceedData.getType = getType;
			equipItemExceedData.eventId = eventId;
			equipItemExceedData.exchangeItemId = exchangeItemId;
			equipItemExceedData.exchangeMoney = exchangeMoney;
			equipItemExceedData.exceed = exceed;
			return equipItemExceedData;
		}

		public static uint cb_parse_first_key(string key_str)
		{
			return (uint)(RARITY_TYPE)Enum.Parse(typeof(RARITY_TYPE), key_str);
		}

		public static string cb_second_key(CSVReader csv, int table_data_num)
		{
			return table_data_num.ToString();
		}

		public static bool cb(CSVReader csv_reader, EquipItemExceedData data, ref uint key1, ref uint key2)
		{
			data.rarity = (RARITY_TYPE)key1;
			csv_reader.Pop(ref data.getType);
			csv_reader.Pop(ref data.eventId);
			csv_reader.Pop(ref data.exchangeItemId);
			csv_reader.Pop(ref data.exchangeMoney);
			List<ExceedNeedItem> list = new List<ExceedNeedItem>();
			for (int i = 0; i < 5; i++)
			{
				uint value = 0u;
				csv_reader.Pop(ref value);
				if (value != 0)
				{
					ExceedNeedItem item = new ExceedNeedItem();
					item.itemId = value;
					item.num = new uint[4];
					for (int j = 0; j < 4; j++)
					{
						csv_reader.Pop(ref item.num[j]);
					}
					ExceedNeedItem exceedNeedItem = list.Find((ExceedNeedItem _data) => _data.itemId == item.itemId);
					if (exceedNeedItem == null)
					{
						list.Add(item);
					}
				}
				else
				{
					for (int k = 0; k < 4; k++)
					{
						uint value2 = 0u;
						csv_reader.Pop(ref value2);
					}
				}
			}
			data.exceed = list.ToArray();
			return true;
		}
	}

	private DoubleUIntKeyTable<EquipItemExceedData> tableData;

	[CompilerGenerated]
	private static TableUtility.CallBackDoubleUIntKeyReadCSV<EquipItemExceedData> _003C_003Ef__mg_0024cache0;

	[CompilerGenerated]
	private static TableUtility.CallBackDoubleUIntSecondKey _003C_003Ef__mg_0024cache1;

	[CompilerGenerated]
	private static TableUtility.CallBackDoubleUIntParseKey _003C_003Ef__mg_0024cache2;

	[CompilerGenerated]
	private static TableUtility.CallBackDoubleUIntKeyReadCSV<EquipItemExceedData> _003C_003Ef__mg_0024cache3;

	[CompilerGenerated]
	private static TableUtility.CallBackDoubleUIntSecondKey _003C_003Ef__mg_0024cache4;

	[CompilerGenerated]
	private static TableUtility.CallBackDoubleUIntParseKey _003C_003Ef__mg_0024cache5;

	public void CreateTable(string csv_text)
	{
		tableData = TableUtility.CreateDoubleUIntKeyTable<EquipItemExceedData>(csv_text, EquipItemExceedData.cb, "rarity,type,eventId,exchangeItemId,exchangeMoney,exceedItemId0,exceedNum0_1,exceedNum0_2,exceedNum0_3,exceedNum0_4,exceedItemId1,exceedNum1_1,exceedNum1_2,exceedNum1_3,exceedNum1_4,exceedItemId2,exceedNum2_1,exceedNum2_2,exceedNum2_3,exceedNum2_4,exceedItemId3,exceedNum3_1,exceedNum3_2,exceedNum3_3,exceedNum3_4,exceedItemId4,exceedNum4_1,exceedNum4_2,exceedNum4_3,exceedNum4_4", EquipItemExceedData.cb_second_key, EquipItemExceedData.cb_parse_first_key);
		tableData.TrimExcess();
	}

	public void AddTable(string csv_text)
	{
		TableUtility.AddDoubleUIntKeyTable(tableData, csv_text, EquipItemExceedData.cb, "rarity,type,eventId,exchangeItemId,exchangeMoney,exceedItemId0,exceedNum0_1,exceedNum0_2,exceedNum0_3,exceedNum0_4,exceedItemId1,exceedNum1_1,exceedNum1_2,exceedNum1_3,exceedNum1_4,exceedItemId2,exceedNum2_1,exceedNum2_2,exceedNum2_3,exceedNum2_4,exceedItemId3,exceedNum3_1,exceedNum3_2,exceedNum3_3,exceedNum3_4,exceedItemId4,exceedNum4_1,exceedNum4_2,exceedNum4_3,exceedNum4_4", EquipItemExceedData.cb_second_key, EquipItemExceedData.cb_parse_first_key);
	}

	public EquipItemExceedData GetEquipItemExceedData(RARITY_TYPE rarity, GET_TYPE getType, int eventId = 0)
	{
		if (tableData == null)
		{
			return null;
		}
		if (getType != GET_TYPE.EVENT)
		{
			eventId = 0;
		}
		UIntKeyTable<EquipItemExceedData> uIntKeyTable = tableData.Get((uint)rarity);
		if (uIntKeyTable == null)
		{
			Log.Error("EquipItemExceedTable is NULL :: rarity = {0}( {1} )", rarity, (uint)rarity);
			return null;
		}
		EquipItemExceedData equipItemExceedData = uIntKeyTable.Find((EquipItemExceedData data) => data.getType == getType && data.eventId == eventId);
		if (equipItemExceedData == null)
		{
			if (getType == GET_TYPE.EVENT)
			{
				equipItemExceedData = uIntKeyTable.Find((EquipItemExceedData data) => data.getType == GET_TYPE.FREE && data.eventId == 0);
			}
			else
			{
				Log.Warning("EquipItemExceedTable is NULL :: getType = {0}, eventId = {1}", getType, eventId);
			}
		}
		return equipItemExceedData;
	}

	public EquipItemExceedData GetEquipItemExceedDataIncludeLimited(EquipItemTable.EquipItemData itemData)
	{
		if (itemData == null)
		{
			return null;
		}
		EquipItemExceedData equipItemExceedData = GetEquipItemExceedData(itemData.rarity, itemData.getType, itemData.eventId);
		LimitedEquipItemExceedTable.LimitedEquipItemExceedData[] limitedEquipItemExceedData = Singleton<LimitedEquipItemExceedTable>.I.GetLimitedEquipItemExceedData(itemData);
		List<EquipItemExceedData.ExceedNeedItem> list = new List<EquipItemExceedData.ExceedNeedItem>();
		for (int i = 0; i < limitedEquipItemExceedData.Length; i++)
		{
			EquipItemExceedData.ExceedNeedItem exceedNeedItem = new EquipItemExceedData.ExceedNeedItem();
			exceedNeedItem.itemId = limitedEquipItemExceedData[i].exceed.itemId;
			exceedNeedItem.num = new uint[4];
			if (exceedNeedItem.num.Length == limitedEquipItemExceedData[i].exceed.num.Length)
			{
				for (int j = 0; j < exceedNeedItem.num.Length; j++)
				{
					exceedNeedItem.num[j] = limitedEquipItemExceedData[i].exceed.num[j];
				}
				list.Add(exceedNeedItem);
			}
		}
		EquipItemExceedData equipItemExceedData2 = equipItemExceedData.Clone();
		EquipItemExceedData.ExceedNeedItem[] second = list.ToArray();
		equipItemExceedData2.exceed = equipItemExceedData2.exceed.Concat(second).ToArray();
		return equipItemExceedData2;
	}

	public bool IsFreeLapis(RARITY_TYPE rarity, uint lapis_item_id, int eventId = 0)
	{
		if (tableData == null)
		{
			return true;
		}
		UIntKeyTable<EquipItemExceedData> uIntKeyTable = tableData.Get((uint)rarity);
		if (uIntKeyTable == null)
		{
			return true;
		}
		EquipItemExceedData equipItemExceedData = uIntKeyTable.Find((EquipItemExceedData data) => data.exchangeItemId == lapis_item_id && data.eventId == eventId);
		if (equipItemExceedData == null)
		{
			return true;
		}
		return equipItemExceedData.getType != GET_TYPE.PAY;
	}
}
