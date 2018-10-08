using System;

public class SortCompareData
{
	public const long SHIFT_BASE = 1L;

	public const int PRIORITY_SHIFT_VALUE = 61;

	public const int SUPER_PRIORITY_SHIFT_VALUE = 62;

	public const int REQUIREMENT_SHIFT_VALUE = 31;

	public long sortingData;

	protected REWARD_CATEGORY m_category;

	public virtual object GetItemData()
	{
		return null;
	}

	public virtual void SetItem(object item)
	{
	}

	public long GetValue()
	{
		return (sortingData - GetMainorSortWeight()) % 2305843009213693952L >> 31;
	}

	public bool IsPriority(bool isAsc)
	{
		bool flag = sortingData >= 2305843009213693952L;
		return flag != isAsc;
	}

	protected int EquipmentTypeToSortBaseType(EQUIPMENT_TYPE type)
	{
		switch (type)
		{
		default:
			return 1;
		case EQUIPMENT_TYPE.TWO_HAND_SWORD:
			return 2;
		case EQUIPMENT_TYPE.SPEAR:
			return 4;
		case EQUIPMENT_TYPE.PAIR_SWORDS:
			return 8;
		case EQUIPMENT_TYPE.ARROW:
			return 16;
		case EQUIPMENT_TYPE.ARMOR:
		case EQUIPMENT_TYPE.VISUAL_ARMOR:
			return 32;
		case EQUIPMENT_TYPE.HELM:
		case EQUIPMENT_TYPE.VISUAL_HELM:
			return 64;
		case EQUIPMENT_TYPE.ARM:
		case EQUIPMENT_TYPE.VISUAL_ARM:
			return 128;
		case EQUIPMENT_TYPE.LEG:
		case EQUIPMENT_TYPE.VISUAL_LEG:
			return 256;
		}
	}

	protected uint EquipmentTypeToMinorSortValue(EQUIPMENT_TYPE type)
	{
		switch (type)
		{
		default:
			return 0u;
		case EQUIPMENT_TYPE.ONE_HAND_SWORD:
			return 9u;
		case EQUIPMENT_TYPE.TWO_HAND_SWORD:
			return 8u;
		case EQUIPMENT_TYPE.SPEAR:
			return 7u;
		case EQUIPMENT_TYPE.PAIR_SWORDS:
			return 6u;
		case EQUIPMENT_TYPE.ARROW:
			return 5u;
		case EQUIPMENT_TYPE.ARMOR:
		case EQUIPMENT_TYPE.VISUAL_ARMOR:
			return 3u;
		case EQUIPMENT_TYPE.HELM:
		case EQUIPMENT_TYPE.VISUAL_HELM:
			return 4u;
		case EQUIPMENT_TYPE.ARM:
		case EQUIPMENT_TYPE.VISUAL_ARM:
			return 2u;
		case EQUIPMENT_TYPE.LEG:
		case EQUIPMENT_TYPE.VISUAL_LEG:
			return 1u;
		}
	}

	protected uint ElementTypeToMinorSortValue(ELEMENT_TYPE type)
	{
		return (uint)(6 - type);
	}

	protected uint GetTypeToMinorSortValue(GET_TYPE type)
	{
		switch (type)
		{
		case GET_TYPE.PAY:
			return 1u;
		default:
			return 0u;
		}
	}

	public virtual void SetupSortingData(SortBase.SORT_REQUIREMENT requirement, EquipItemStatus status = null)
	{
	}

	public void Filtering(SortSettings settings)
	{
		bool flag = false;
		if (!flag)
		{
			int num = 1 << (int)GetRarity();
			if ((num & settings.rarity) == 0)
			{
				flag = true;
			}
		}
		if (!flag && settings.dialogType != SortBase.DIALOG_TYPE.USE_ITEM)
		{
			if (settings.dialogType == SortBase.DIALOG_TYPE.MATERIAL)
			{
				int num2;
				switch (GetItemType())
				{
				case 7:
					num2 = 4;
					break;
				case 14:
					num2 = 1;
					break;
				case 15:
					num2 = 8;
					break;
				case 6:
					num2 = 16;
					break;
				default:
					num2 = 2;
					break;
				}
				if ((settings.type & num2) == 0)
				{
					flag = true;
				}
				ELEMENT_TYPE iconElement = GetIconElement();
				int num3 = 1 << (int)GetIconElement();
				if ((num3 & settings.element) == 0)
				{
					flag = true;
				}
			}
			else if (settings.dialogType == SortBase.DIALOG_TYPE.SMITH_CREATE_WEAPON || settings.dialogType == SortBase.DIALOG_TYPE.SMITH_CREATE_ARMOR)
			{
				if ((settings.type & GetItemType()) == 0)
				{
					flag = true;
				}
				if (getEquipFilterPayAndCreatable(settings.equipFilter))
				{
					flag = true;
				}
				ELEMENT_TYPE iconElement2 = GetIconElement();
				int num4 = 1 << (int)GetIconElement();
				if ((num4 & settings.element) == 0)
				{
					flag = true;
				}
			}
			else if (settings.dialogType == SortBase.DIALOG_TYPE.ABILITY_ITEM)
			{
				if ((settings.type & GetItemType()) == 0)
				{
					flag = true;
				}
				int num5 = 1 << (int)GetIconElement();
				if ((num5 & settings.element) == 0)
				{
					flag = true;
				}
			}
			else
			{
				if ((settings.type & GetItemType()) == 0)
				{
					flag = true;
				}
				int num6 = 1 << (int)GetIconElement();
				if ((num6 & settings.element) == 0)
				{
					flag = true;
				}
			}
		}
		sortingData <<= 31;
		if (settings.orderTypeAsc == flag)
		{
			sortingData |= 2305843009213693952L;
		}
		sortingData += GetMainorSortWeight();
		if (IsAbsFirst())
		{
			if (settings.orderTypeAsc)
			{
				sortingData = 0L;
			}
			else
			{
				sortingData |= 4611686018427387904L;
			}
		}
	}

	public static SORT_DATA[] CreateSortDataAry<ITEM_DATA, SORT_DATA>(ITEM_DATA[] data, SortSettings sort_settings, SortSettings.SortEquipSetInfo sort_equip_set_info = null) where SORT_DATA : SortCompareData, new()
	{
		EquipItemStatus equipItemStatus = null;
		if (sort_equip_set_info != null)
		{
			equipItemStatus = MonoBehaviourSingleton<StatusManager>.I.GetEquipSetAllSkillParam(sort_equip_set_info.equipSetNo, sort_equip_set_info.isLocal, sort_equip_set_info.exclusionSlotIndex);
		}
		SORT_DATA[] equipItemSortAry = (SORT_DATA[])new SORT_DATA[data.Length];
		int i;
		for (i = 0; i < data.Length; i++)
		{
			equipItemSortAry[i] = (SORT_DATA)new SORT_DATA();
			equipItemSortAry[i].SetItem(data[i]);
			EquipItemStatus equipItemStatus2 = new EquipItemStatus(equipItemStatus);
			if (equipItemStatus != null)
			{
				bool exclusion = false;
				sort_equip_set_info?.exclusionUniqID.ForEach((Action<ulong>)delegate(ulong uniq_id)
				{
					if (!exclusion && equipItemSortAry[i].GetUniqID() == uniq_id)
					{
						exclusion = true;
					}
				});
				if (!exclusion)
				{
					equipItemStatus2.Add(equipItemSortAry[i].GetItemStatus());
				}
			}
			equipItemSortAry[i].SetupSortingData(sort_settings.requirement, equipItemStatus2);
			equipItemSortAry[i].Filtering(sort_settings);
		}
		return (SORT_DATA[])equipItemSortAry;
	}

	public static void InitSortDataAry<SORT_DATA>(SORT_DATA[] sort_data, SortSettings sort_settings, SortSettings.SortEquipSetInfo sort_equip_set_info = null) where SORT_DATA : SortCompareData, new()
	{
		EquipItemStatus equipItemStatus = null;
		if (sort_equip_set_info != null)
		{
			equipItemStatus = MonoBehaviourSingleton<StatusManager>.I.GetEquipSetAllSkillParam(sort_equip_set_info.equipSetNo, sort_equip_set_info.isLocal, sort_equip_set_info.exclusionSlotIndex);
		}
		for (int i = 0; i < sort_data.Length; i++)
		{
			EquipItemStatus equipItemStatus2 = new EquipItemStatus(equipItemStatus);
			SORT_DATA sortData = (SORT_DATA)sort_data[i];
			if (equipItemStatus != null)
			{
				bool exclusion = false;
				sort_equip_set_info?.exclusionUniqID.ForEach((Action<ulong>)delegate(ulong uniq_id)
				{
					if (!exclusion && sortData.GetUniqID() == uniq_id)
					{
						exclusion = true;
					}
				});
				if (!exclusion)
				{
					equipItemStatus2.Add(sort_data[i].GetItemStatus());
				}
			}
			sortData.SetupSortingData(sort_settings.requirement, equipItemStatus2);
			sortData.Filtering(sort_settings);
		}
	}

	public virtual bool IsAbsFirst()
	{
		return false;
	}

	public virtual bool IsFavorite()
	{
		return false;
	}

	public virtual int GetItemType()
	{
		return 0;
	}

	public virtual RARITY_TYPE GetRarity()
	{
		return RARITY_TYPE.D;
	}

	public virtual ulong GetUniqID()
	{
		return 0uL;
	}

	public virtual uint GetTableID()
	{
		return 0u;
	}

	public virtual string GetName()
	{
		return string.Empty;
	}

	public virtual int GetIconID()
	{
		return 0;
	}

	public virtual ITEM_ICON_TYPE GetIconType()
	{
		return ITEM_ICON_TYPE.NONE;
	}

	public virtual ELEMENT_TYPE GetIconElement()
	{
		return ELEMENT_TYPE.MAX;
	}

	public virtual EQUIPMENT_TYPE? GetIconMagiEnableType()
	{
		return null;
	}

	public virtual string GetDetail()
	{
		return string.Empty;
	}

	public virtual int GetNum()
	{
		return 0;
	}

	public virtual int GetSalePrice()
	{
		return 0;
	}

	public virtual bool CanSale()
	{
		return false;
	}

	public virtual bool IsEquipping()
	{
		return false;
	}

	public virtual int GetLevel()
	{
		return 1;
	}

	public virtual ItemStatus GetItemStatus()
	{
		return null;
	}

	public virtual REWARD_TYPE GetMaterialType()
	{
		return REWARD_TYPE.NONE;
	}

	public virtual bool IsEquipSomewhere()
	{
		return false;
	}

	public virtual bool IsLithograph()
	{
		return false;
	}

	public virtual int getEquipFilterPay()
	{
		return 0;
	}

	public virtual int getEquipFilterCreatable()
	{
		return 0;
	}

	public virtual int getEquipFilterObtained()
	{
		return 0;
	}

	public virtual bool getEquipFilterPayAndCreatable(int filter)
	{
		return false;
	}

	public virtual bool IsExceeded()
	{
		return false;
	}

	public virtual GET_TYPE GetGetType()
	{
		return GET_TYPE.PAY;
	}

	public virtual uint GetMainorSortWeight()
	{
		return 0u;
	}

	public REWARD_CATEGORY GetCategory()
	{
		return m_category;
	}

	public void SetCategory(REWARD_CATEGORY category)
	{
		m_category = category;
	}

	public int GetSortValueQuestResult()
	{
		int num = 0;
		num += (IsLithograph() ? 100000 : 0);
		num += ((GetIconType() == ITEM_ICON_TYPE.ABILITY_ITEM) ? 10000 : 0);
		num += (int)(4 - m_category) * 100;
		return (int)(num + GetRarity());
	}
}
