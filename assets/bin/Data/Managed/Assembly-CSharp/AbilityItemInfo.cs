using Network;
using System;
using System.Collections;
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
		if (equipUniqueId == 0)
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
			AbilityItemLotTable.AbilityItemLot abilityItemLot = null;
			ABILITY_TYPE aBILITY_TYPE = ABILITY_TYPE.NONE;
			string text = string.Empty;
			string enableText = string.Empty;
			string spAttackTypeText = string.Empty;
			int val = 0;
			string format = string.Empty;
			int unlockEventId = 0;
			if (datum.abilityItemLotId > 0)
			{
				abilityItemLot = Singleton<AbilityItemLotTable>.I.GetAbilityItemLot((uint)datum.abilityItemLotId);
			}
			if (abilityItemLot == null)
			{
				if (Enum.IsDefined(typeof(ABILITY_TYPE), datum.abilityType))
				{
					aBILITY_TYPE = (ABILITY_TYPE)Enum.Parse(typeof(ABILITY_TYPE), datum.abilityType);
					text = datum.target;
					enableText = datum.spTarget;
					spAttackTypeText = datum.spAttackType;
					val = datum.value;
					format = datum.format;
					unlockEventId = 0;
				}
			}
			else
			{
				aBILITY_TYPE = abilityItemLot.abilityType;
				text = abilityItemLot.target;
				enableText = abilityItemLot.spTarget;
				spAttackTypeText = abilityItemLot.spAttackType;
				val = datum.value;
				format = abilityItemLot.format.Replace("XX", datum.value.ToString());
				unlockEventId = abilityItemLot.unlockEventId;
			}
			AbilityInfoWithFormat abilityInfoWithFormat = new AbilityInfoWithFormat();
			if (aBILITY_TYPE != 0)
			{
				abilityInfoWithFormat.type = aBILITY_TYPE;
				abilityInfoWithFormat.target = text;
				abilityInfoWithFormat.value = val;
				abilityInfoWithFormat.format = format;
				abilityInfoWithFormat.unlockEventId = unlockEventId;
				AbilityDataTable.AbilityData.AbilityInfo.Enable enable = MakeAbilityEnableData(text);
				if (enable != null)
				{
					abilityInfoWithFormat.enables.Add(enable);
				}
				enable = MakeAbilityEnableData(enableText);
				if (enable != null)
				{
					abilityInfoWithFormat.enables.Add(enable);
				}
				enable = MakeAbilityEnableDataAsSpAtkTypeBit(spAttackTypeText);
				if (enable != null)
				{
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

	private static AbilityDataTable.AbilityData.AbilityInfo.Enable MakeAbilityEnableData(string _enableText)
	{
		if (string.IsNullOrEmpty(_enableText))
		{
			return null;
		}
		if (!Enum.IsDefined(typeof(ABILITY_ENABLE_TYPE), _enableText))
		{
			return null;
		}
		AbilityDataTable.AbilityData.AbilityInfo.Enable enable = new AbilityDataTable.AbilityData.AbilityInfo.Enable();
		enable.type = (ABILITY_ENABLE_TYPE)Enum.Parse(typeof(ABILITY_ENABLE_TYPE), _enableText);
		enable.SpAtkEnableTypeBit = ConvertAbilityEnableType2SpAtkEnableTypeBit(enable.type);
		return enable;
	}

	private static AbilityDataTable.AbilityData.AbilityInfo.Enable MakeAbilityEnableDataAsSpAtkTypeBit(string _spAttackTypeText)
	{
		if (string.IsNullOrEmpty(_spAttackTypeText))
		{
			return null;
		}
		int num = ParseSpAtkEnableTypeBit(_spAttackTypeText);
		if (num == 0)
		{
			return null;
		}
		AbilityDataTable.AbilityData.AbilityInfo.Enable enable = new AbilityDataTable.AbilityData.AbilityInfo.Enable();
		enable.type = ABILITY_ENABLE_TYPE.WEAPON_SP_TYPE;
		enable.SpAtkEnableTypeBit = num;
		return enable;
	}

	public static int ConvertAbilityEnableType2SpAtkEnableTypeBit(ABILITY_ENABLE_TYPE _type)
	{
		if (_type < ABILITY_ENABLE_TYPE.NORMAL || ABILITY_ENABLE_TYPE.HEAT_SOUL < _type)
		{
			return 0;
		}
		int num = 0;
		if (_type == ABILITY_ENABLE_TYPE.NORMAL || _type == ABILITY_ENABLE_TYPE.NORMAL_HEAT || _type == ABILITY_ENABLE_TYPE.NORMAL_SOUL)
		{
			num |= 1;
		}
		if (_type == ABILITY_ENABLE_TYPE.HEAT || _type == ABILITY_ENABLE_TYPE.NORMAL_HEAT || _type == ABILITY_ENABLE_TYPE.HEAT_SOUL)
		{
			num |= 2;
		}
		if (_type == ABILITY_ENABLE_TYPE.SOUL || _type == ABILITY_ENABLE_TYPE.NORMAL_SOUL || _type == ABILITY_ENABLE_TYPE.HEAT_SOUL)
		{
			num |= 4;
		}
		return num;
	}

	public static int ParseSpAtkEnableTypeBit(string _input_text)
	{
		int num = 0;
		if (string.IsNullOrEmpty(_input_text))
		{
			return num;
		}
		IEnumerator enumerator = Enum.GetValues(typeof(SP_ATK_ENABLE_TYPE_BIT)).GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object current = enumerator.Current;
				if (_input_text.Contains(current.ToString()))
				{
					num |= (int)current;
				}
			}
			return num;
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
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
