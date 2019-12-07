using System.Collections.Generic;

public class CreatePickupItemTable : Singleton<CreatePickupItemTable>, IDataTable
{
	public class CreatePickupItemData
	{
		public uint id;

		public uint createTableID;

		public uint eventLocationID;

		public int preShowTime;

		public int postShowTime;

		public bool openOnly;

		public const string NT = "pickupId,createId,locationId,preShowTime,postShowTime,openOnly";

		public static bool cb(CSVReader csv_reader, CreatePickupItemData data, ref uint key)
		{
			data.id = key;
			csv_reader.Pop(ref data.createTableID);
			csv_reader.Pop(ref data.eventLocationID);
			csv_reader.Pop(ref data.preShowTime);
			csv_reader.Pop(ref data.postShowTime);
			csv_reader.Pop(ref data.openOnly);
			return true;
		}
	}

	private class SortData
	{
		public int index;

		public SmithCreateItemInfo data;

		public SortData(SmithCreateItemInfo _data, int _index)
		{
			data = _data;
			index = _index;
		}
	}

	private UIntKeyTable<CreatePickupItemData> pickupTable;

	public void CreateTable(string csv_text)
	{
		pickupTable = TableUtility.CreateUIntKeyTable<CreatePickupItemData>(csv_text, CreatePickupItemData.cb, "pickupId,createId,locationId,preShowTime,postShowTime,openOnly");
		pickupTable.TrimExcess();
	}

	public CreatePickupItemData GetLevelTable(uint id)
	{
		if (pickupTable == null)
		{
			return null;
		}
		CreatePickupItemData createPickupItemData = pickupTable.Get(id);
		if (createPickupItemData == null)
		{
			Log.Error("CreatePickupItemData is NULL :: id(Lv) = " + id);
			return null;
		}
		return createPickupItemData;
	}

	private int GetSortIndex(List<SortData> sort_data, SmithCreateItemInfo search_data)
	{
		return sort_data.Find((SortData _data) => _data.data == search_data).index;
	}

	public CreatePickupItemData GetPickupCreateItem(uint create_item_id)
	{
		CreatePickupItemData find_data = null;
		pickupTable.ForEach(delegate(CreatePickupItemData data)
		{
			if (find_data == null && data.createTableID == create_item_id)
			{
				find_data = data;
			}
		});
		return find_data;
	}

	public SmithCreateItemInfo[] GetPickupItemAry(SortBase.TYPE item_type = SortBase.TYPE.EQUIP_ALL)
	{
		if (!Singleton<EquipItemTable>.IsValid() || !Singleton<QuestTable>.IsValid())
		{
			return null;
		}
		List<SmithCreateItemInfo> list = new List<SmithCreateItemInfo>();
		List<SortData> sort_data = new List<SortData>();
		pickupTable.ForEach(delegate(CreatePickupItemData pickup_data)
		{
			if ((pickup_data.eventLocationID == 0) ? true : false)
			{
				CreateEquipItemTable.CreateEquipItemData createEquipItemTableData = Singleton<CreateEquipItemTable>.I.GetCreateEquipItemTableData(pickup_data.createTableID);
				if (createEquipItemTableData != null)
				{
					EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(createEquipItemTableData.equipItemID);
					if (equipItemData != null)
					{
						SmithCreateItemInfo smithCreateItemInfo = new SmithCreateItemInfo(equipItemData, createEquipItemTableData);
						SortBase.TYPE tYPE = SortBase.TYPE.NONE;
						switch (smithCreateItemInfo.equipTableData.type)
						{
						case EQUIPMENT_TYPE.ARMOR:
						case EQUIPMENT_TYPE.VISUAL_ARMOR:
							tYPE = SortBase.TYPE.ARMOR;
							break;
						case EQUIPMENT_TYPE.HELM:
						case EQUIPMENT_TYPE.VISUAL_HELM:
							tYPE = SortBase.TYPE.HELM;
							break;
						case EQUIPMENT_TYPE.ARM:
						case EQUIPMENT_TYPE.VISUAL_ARM:
							tYPE = SortBase.TYPE.ARMOR;
							break;
						case EQUIPMENT_TYPE.LEG:
						case EQUIPMENT_TYPE.VISUAL_LEG:
							tYPE = SortBase.TYPE.LEG;
							break;
						case EQUIPMENT_TYPE.ONE_HAND_SWORD:
							tYPE = SortBase.TYPE.ONE_HAND_SWORD;
							break;
						case EQUIPMENT_TYPE.TWO_HAND_SWORD:
							tYPE = SortBase.TYPE.TWO_HAND_SWORD;
							break;
						case EQUIPMENT_TYPE.SPEAR:
							tYPE = SortBase.TYPE.SPEAR;
							break;
						case EQUIPMENT_TYPE.PAIR_SWORDS:
							tYPE = SortBase.TYPE.PAIR_SWORDS;
							break;
						case EQUIPMENT_TYPE.ARROW:
							tYPE = SortBase.TYPE.ARROW;
							break;
						default:
							tYPE = SortBase.TYPE.NONE;
							break;
						}
						if ((tYPE & item_type) != 0)
						{
							bool flag = true;
							if (!MonoBehaviourSingleton<InventoryManager>.I.IsHaveingKeyMaterial(createEquipItemTableData.needKeyOrder, createEquipItemTableData.needMaterial))
							{
								flag = false;
							}
							if ((int)createEquipItemTableData.researchLv > MonoBehaviourSingleton<UserInfoManager>.I.userStatus.researchLv)
							{
								flag = false;
							}
							if (flag)
							{
								list.Add(smithCreateItemInfo);
								sort_data.Add(new SortData(smithCreateItemInfo, (int)pickup_data.id));
							}
						}
					}
				}
			}
		});
		list.Sort((SmithCreateItemInfo l, SmithCreateItemInfo r) => GetSortIndex(sort_data, l) - GetSortIndex(sort_data, r));
		return list.ToArray();
	}
}
