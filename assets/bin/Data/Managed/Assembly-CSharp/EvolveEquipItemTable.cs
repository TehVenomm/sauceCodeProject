using System.Collections.Generic;

public class EvolveEquipItemTable : Singleton<EvolveEquipItemTable>, IDataTable
{
	public class EvolveEquipItemData
	{
		public uint id;

		public uint equipBaseItemID;

		public uint equipEvolveItemID;

		public NeedMaterial[] needMaterial;

		public XorInt needMoney = 0;

		public NeedEquip[] needEquip;

		public const string NT = "evolveId,baseEquipItemId,nextEquipItemId,itemID_0,itemNum_0,itemID_1,itemNum_1,itemID_2,itemNum_2,itemID_3,itemNum_3,itemID_4,itemNum_4,itemID_5,itemNum_5,itemID_6,itemNum_6,itemID_7,itemNum_7,itemID_8,itemNum_8,itemID_9,itemNum_9,money,equipItemID_0,equipItemNum_0,needLv_0,equipItemID_1,equipItemNum_1,needLv_1,equipItemID_2,equipItemNum_2,needLv_2,equipItemID_3,equipItemNum_3,needLv_3,equipItemID_4,equipItemNum_4,needLv_4,equipItemID_5,equipItemNum_5,needLv_5,equipItemID_6,equipItemNum_6,needLv_6,equipItemID_7,equipItemNum_7,needLv_7,equipItemID_8,equipItemNum_8,needLv_8,equipItemID_9,equipItemNum_9,needLv_9";

		public static bool cb(CSVReader csv_reader, EvolveEquipItemData data, ref uint key)
		{
			data.id = key;
			csv_reader.Pop(ref data.equipBaseItemID);
			csv_reader.Pop(ref data.equipEvolveItemID);
			List<NeedMaterial> list = new List<NeedMaterial>();
			for (int i = 0; i < 10; i++)
			{
				uint value = 0u;
				int value2 = 0;
				csv_reader.Pop(ref value);
				csv_reader.Pop(ref value2);
				if (value != 0 && value2 != 0)
				{
					list.Add(new NeedMaterial(value, value2));
				}
			}
			data.needMaterial = list.ToArray();
			csv_reader.Pop(ref data.needMoney);
			List<NeedEquip> list2 = new List<NeedEquip>();
			for (int j = 0; j < 10; j++)
			{
				uint value3 = 0u;
				int value4 = 0;
				int value5 = 0;
				csv_reader.Pop(ref value3);
				csv_reader.Pop(ref value4);
				csv_reader.Pop(ref value5);
				if (value3 != 0 && value4 != 0 && value5 != 0)
				{
					list2.Add(new NeedEquip(value3, value4, value5));
				}
			}
			data.needEquip = list2.ToArray();
			return true;
		}
	}

	private UIntKeyTable<EvolveEquipItemData> tableData;

	public void CreateTable(string csv_text)
	{
		tableData = TableUtility.CreateUIntKeyTable<EvolveEquipItemData>(csv_text, EvolveEquipItemData.cb, "evolveId,baseEquipItemId,nextEquipItemId,itemID_0,itemNum_0,itemID_1,itemNum_1,itemID_2,itemNum_2,itemID_3,itemNum_3,itemID_4,itemNum_4,itemID_5,itemNum_5,itemID_6,itemNum_6,itemID_7,itemNum_7,itemID_8,itemNum_8,itemID_9,itemNum_9,money,equipItemID_0,equipItemNum_0,needLv_0,equipItemID_1,equipItemNum_1,needLv_1,equipItemID_2,equipItemNum_2,needLv_2,equipItemID_3,equipItemNum_3,needLv_3,equipItemID_4,equipItemNum_4,needLv_4,equipItemID_5,equipItemNum_5,needLv_5,equipItemID_6,equipItemNum_6,needLv_6,equipItemID_7,equipItemNum_7,needLv_7,equipItemID_8,equipItemNum_8,needLv_8,equipItemID_9,equipItemNum_9,needLv_9");
		tableData.TrimExcess();
	}

	public void AddTable(string csv_text)
	{
		TableUtility.AddUIntKeyTable(tableData, csv_text, EvolveEquipItemData.cb, "evolveId,baseEquipItemId,nextEquipItemId,itemID_0,itemNum_0,itemID_1,itemNum_1,itemID_2,itemNum_2,itemID_3,itemNum_3,itemID_4,itemNum_4,itemID_5,itemNum_5,itemID_6,itemNum_6,itemID_7,itemNum_7,itemID_8,itemNum_8,itemID_9,itemNum_9,money,equipItemID_0,equipItemNum_0,needLv_0,equipItemID_1,equipItemNum_1,needLv_1,equipItemID_2,equipItemNum_2,needLv_2,equipItemID_3,equipItemNum_3,needLv_3,equipItemID_4,equipItemNum_4,needLv_4,equipItemID_5,equipItemNum_5,needLv_5,equipItemID_6,equipItemNum_6,needLv_6,equipItemID_7,equipItemNum_7,needLv_7,equipItemID_8,equipItemNum_8,needLv_8,equipItemID_9,equipItemNum_9,needLv_9");
	}

	public EvolveEquipItemData[] GetEvolveEquipItemData(uint base_equip_id)
	{
		if (tableData == null)
		{
			return null;
		}
		List<EvolveEquipItemData> list = new List<EvolveEquipItemData>();
		tableData.ForEach(delegate(EvolveEquipItemData table)
		{
			if (table.equipBaseItemID == base_equip_id)
			{
				list.Add(table);
			}
		});
		if (list.Count == 0)
		{
			return null;
		}
		list.Sort((EvolveEquipItemData l, EvolveEquipItemData r) => (int)(l.equipEvolveItemID - r.equipEvolveItemID));
		return list.ToArray();
	}

	public EvolveEquipItemData GetEvolveEquipItemDataFromEvolveEquipId(uint evolve_equip_id)
	{
		if (tableData == null)
		{
			return null;
		}
		return tableData.Find((EvolveEquipItemData table) => table.equipEvolveItemID == evolve_equip_id);
	}
}
