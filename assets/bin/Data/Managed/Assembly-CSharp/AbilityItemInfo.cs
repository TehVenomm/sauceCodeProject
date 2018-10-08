using Network;
using System;
using System.Collections.Generic;

public class AbilityItemInfo : ItemInfoBase<AbilityItem>
{
	public class AbilityInfoWithFormat : AbilityDataTable.AbilityData.AbilityInfo
	{
		public string format;
	}

	public ulong equipUniqueId;

	public List<AbilityInfoWithFormat> info = new List<AbilityInfoWithFormat>();

	public AbilityItem originalData;

	public override void SetValue(AbilityItem recv)
	{
		base.uniqueID = ulong.Parse(recv.uniqId);
		base.tableID = (uint)recv.abilityItemId;
		equipUniqueId = ulong.Parse(recv.equipItemUniqId);
		originalData = recv;
		info = ConvertAbilityItemToInfo(recv);
	}

	public EquipItemInfo GetEquipItem()
	{
		if (equipUniqueId == 0L)
		{
			return null;
		}
		if (!MonoBehaviourSingleton<InventoryManager>.IsValid() || MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory == null)
		{
			return null;
		}
		return MonoBehaviourSingleton<InventoryManager>.I.GetEquipItem(equipUniqueId);
	}

	public ItemTable.ItemData GetItemTableData()
	{
		return Singleton<ItemTable>.I.GetItemData(base.tableID);
	}

	public string GetName()
	{
		ItemTable.ItemData itemTableData = GetItemTableData();
		if (itemTableData == null)
		{
			return string.Empty;
		}
		return itemTableData.name;
	}

	public string GetDescription()
	{
		string text = string.Empty;
		for (int i = 0; i < info.Count; i++)
		{
			text += info[i].format;
			if (i + 1 < info.Count)
			{
				text += "\n";
			}
		}
		return text;
	}

	public static List<AbilityInfoWithFormat> ConvertAbilityItemToInfo(AbilityItem recv)
	{
		List<AbilityInfoWithFormat> list = new List<AbilityInfoWithFormat>();
		if (recv == null)
		{
			return list;
		}
		foreach (AbilityItem.Data datum in recv.data)
		{
			AbilityInfoWithFormat abilityInfoWithFormat = new AbilityInfoWithFormat();
			if (Enum.IsDefined(typeof(ABILITY_TYPE), datum.abilityType))
			{
				abilityInfoWithFormat.type = (ABILITY_TYPE)(int)Enum.Parse(typeof(ABILITY_TYPE), datum.abilityType);
				abilityInfoWithFormat.target = datum.target;
				abilityInfoWithFormat.value = datum.value;
				abilityInfoWithFormat.format = datum.format;
				if (Enum.IsDefined(typeof(ABILITY_ENABLE_TYPE), datum.spTarget))
				{
					AbilityDataTable.AbilityData.AbilityInfo.Enable enable = new AbilityDataTable.AbilityData.AbilityInfo.Enable();
					enable.type = (ABILITY_ENABLE_TYPE)(int)Enum.Parse(typeof(ABILITY_ENABLE_TYPE), datum.spTarget);
					abilityInfoWithFormat.enables.Add(enable);
				}
			}
			else
			{
				abilityInfoWithFormat.type = ABILITY_TYPE.NEED_UPDATE;
				abilityInfoWithFormat.target = string.Empty;
				abilityInfoWithFormat.value = 0;
			}
			list.Add(abilityInfoWithFormat);
		}
		return list;
	}

	public static InventoryList<AbilityItemInfo, AbilityItem> CreateList(List<AbilityItem> recv_list)
	{
		InventoryList<AbilityItemInfo, AbilityItem> list = new InventoryList<AbilityItemInfo, AbilityItem>();
		recv_list.ForEach(delegate(AbilityItem o)
		{
			list.Add(o);
		});
		return list;
	}
}
