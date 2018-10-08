using System;
using System.Collections.Generic;
using System.Linq;

public class CreateEquipItemTable : Singleton<CreateEquipItemTable>, IDataTable
{
	public class CreateEquipItemData
	{
		public const string NT = "createId,equipItemId,researchLv,pickupPriority,keyOrder,isKey_0,itemID_0,itemNum_0,isKey_1,itemID_1,itemNum_1,isKey_2,itemID_2,itemNum_2,isKey_3,itemID_3,itemNum_3,isKey_4,itemID_4,itemNum_4,isKey_5,itemID_5,itemNum_5,isKey_6,itemID_6,itemNum_6,isKey_7,itemID_7,itemNum_7,isKey_8,itemID_8,itemNum_8,isKey_9,itemID_9,itemNum_9,money";

		public uint id;

		public uint equipItemID;

		public XorInt researchLv = 0;

		public int pickupPriority;

		public LOGICAL_ORDER_TYPE needKeyOrder;

		public NeedMaterial[] needMaterial;

		public XorInt needMoney = 0;

		public static bool cb(CSVReader csv_reader, CreateEquipItemData data, ref uint key)
		{
			data.id = key;
			csv_reader.Pop(ref data.equipItemID);
			csv_reader.Pop(ref data.researchLv);
			csv_reader.Pop(ref data.pickupPriority);
			csv_reader.Pop(ref data.needKeyOrder);
			List<NeedMaterial> list = new List<NeedMaterial>();
			for (int i = 0; i < 10; i++)
			{
				bool value = false;
				uint value2 = 0u;
				int value3 = 0;
				csv_reader.Pop(ref value);
				csv_reader.Pop(ref value2);
				csv_reader.Pop(ref value3);
				if (value2 != 0 && value3 != 0)
				{
					list.Add(new NeedMaterial(value, value2, value3));
				}
			}
			data.needMaterial = list.ToArray();
			csv_reader.Pop(ref data.needMoney);
			return true;
		}
	}

	private UIntKeyTable<CreateEquipItemData> tableData;

	public void CreateTable(string csv_text)
	{
		tableData = TableUtility.CreateUIntKeyTable<CreateEquipItemData>(csv_text, CreateEquipItemData.cb, "createId,equipItemId,researchLv,pickupPriority,keyOrder,isKey_0,itemID_0,itemNum_0,isKey_1,itemID_1,itemNum_1,isKey_2,itemID_2,itemNum_2,isKey_3,itemID_3,itemNum_3,isKey_4,itemID_4,itemNum_4,isKey_5,itemID_5,itemNum_5,isKey_6,itemID_6,itemNum_6,isKey_7,itemID_7,itemNum_7,isKey_8,itemID_8,itemNum_8,isKey_9,itemID_9,itemNum_9,money", null);
		tableData.TrimExcess();
	}

	public void AddTable(string csv_text)
	{
		TableUtility.AddUIntKeyTable(tableData, csv_text, CreateEquipItemData.cb, "createId,equipItemId,researchLv,pickupPriority,keyOrder,isKey_0,itemID_0,itemNum_0,isKey_1,itemID_1,itemNum_1,isKey_2,itemID_2,itemNum_2,isKey_3,itemID_3,itemNum_3,isKey_4,itemID_4,itemNum_4,isKey_5,itemID_5,itemNum_5,isKey_6,itemID_6,itemNum_6,isKey_7,itemID_7,itemNum_7,isKey_8,itemID_8,itemNum_8,isKey_9,itemID_9,itemNum_9,money", null);
	}

	public CreateEquipItemData GetCreateEquipItemTableData(uint id)
	{
		if (tableData == null)
		{
			return null;
		}
		return tableData.Get(id);
	}

	public SmithCreateItemInfo[] GetCreateEquipItemDataAry(EQUIPMENT_TYPE type)
	{
		if (!Singleton<EquipItemTable>.IsValid())
		{
			return null;
		}
		List<SmithCreateItemInfo> list = new List<SmithCreateItemInfo>();
		tableData.ForEach(delegate(CreateEquipItemData create_equip_item_table)
		{
			EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(create_equip_item_table.equipItemID);
			if (equipItemData != null && equipItemData.type == type)
			{
				list.Add(new SmithCreateItemInfo(equipItemData, create_equip_item_table));
			}
		});
		return list.ToArray();
	}

	public CreateEquipItemData[] GetCreatableEquipItem(uint material_id)
	{
		if (!Singleton<EquipItemTable>.IsValid())
		{
			return null;
		}
		List<CreateEquipItemData> list = new List<CreateEquipItemData>();
		tableData.ForEach(delegate(CreateEquipItemData create_equip_item_table)
		{
			if (create_equip_item_table.needMaterial[0].itemID == material_id)
			{
				list.Add(create_equip_item_table);
			}
		});
		list.Sort(delegate(CreateEquipItemData l, CreateEquipItemData r)
		{
			int num = (l.pickupPriority != 0) ? l.pickupPriority : 100;
			int num2 = (r.pickupPriority != 0) ? r.pickupPriority : 100;
			int num3 = num - num2;
			if (num3 == 0)
			{
				num3 = (int)(r.equipItemID - l.equipItemID);
			}
			return num3;
		});
		return list.ToArray();
	}

	public unsafe CreateEquipItemData[] GetSortedCreateEquipItemsByPart(uint materialId)
	{
		CreateEquipItemData[] creatableEquipItem = Singleton<CreateEquipItemTable>.I.GetCreatableEquipItem(materialId);
		CreateEquipItemData[] source = creatableEquipItem;
		if (_003C_003Ef__am_0024cache2 == null)
		{
			_003C_003Ef__am_0024cache2 = new Func<CreateEquipItemData, int>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		return source.OrderBy<CreateEquipItemData, int>(_003C_003Ef__am_0024cache2).ToArray();
	}

	public CreateEquipItemData GetCreateEquipItemByPart(uint materialId, EQUIPMENT_TYPE type)
	{
		CreateEquipItemData[] creatableEquipItem = Singleton<CreateEquipItemTable>.I.GetCreatableEquipItem(materialId);
		int i = 0;
		for (int num = creatableEquipItem.Length; i < num; i++)
		{
			CreateEquipItemData createEquipItemData = creatableEquipItem[i];
			EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(createEquipItemData.equipItemID);
			if (equipItemData.type == type)
			{
				return createEquipItemData;
			}
		}
		return null;
	}

	public CreateEquipItemData GetCreateItemDataByEquipItem(uint equipId)
	{
		return tableData.Find((CreateEquipItemData x) => x.equipItemID == equipId);
	}
}
