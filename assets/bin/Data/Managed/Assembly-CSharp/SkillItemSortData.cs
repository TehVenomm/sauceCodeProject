public class SkillItemSortData : SortCompareData
{
	public SkillItemInfo skillData;

	public override object GetItemData()
	{
		return skillData;
	}

	public override void SetItem(object item)
	{
		skillData = (SkillItemInfo)item;
	}

	public override void SetupSortingData(SortBase.SORT_REQUIREMENT requirement, EquipItemStatus status = null)
	{
		switch (requirement)
		{
		default:
			sortingData = (long)skillData.uniqueID;
			break;
		case SortBase.SORT_REQUIREMENT.RARITY:
			sortingData = (long)skillData.tableData.rarity;
			break;
		case SortBase.SORT_REQUIREMENT.LV:
			sortingData = skillData.level;
			break;
		case SortBase.SORT_REQUIREMENT.ATK:
			sortingData = skillData.atk;
			break;
		case SortBase.SORT_REQUIREMENT.DEF:
			sortingData = skillData.def;
			break;
		case SortBase.SORT_REQUIREMENT.HP:
			sortingData = skillData.hp;
			break;
		case SortBase.SORT_REQUIREMENT.SALE:
			sortingData = skillData.sellPrice;
			break;
		case SortBase.SORT_REQUIREMENT.PRICE:
			sortingData = 0L;
			break;
		case SortBase.SORT_REQUIREMENT.NUM:
			sortingData = 1L;
			break;
		case SortBase.SORT_REQUIREMENT.ELEMENT:
			sortingData = 6L - (long)GetIconElement();
			break;
		case SortBase.SORT_REQUIREMENT.SKILL_TYPE:
			sortingData = 63L - (long)skillData.tableData.type;
			break;
		}
	}

	public override bool IsFavorite()
	{
		return skillData.isFavorite;
	}

	public override int GetItemType()
	{
		return 1 << (int)(skillData.tableData.type - 1);
	}

	public override ulong GetUniqID()
	{
		return skillData.uniqueID;
	}

	public override uint GetTableID()
	{
		return skillData.tableID;
	}

	public override string GetName()
	{
		return skillData.tableData.name;
	}

	public override int GetIconID()
	{
		return skillData.tableData.iconID;
	}

	public override ITEM_ICON_TYPE GetIconType()
	{
		return ItemIcon.GetItemIconType(skillData.tableData.type);
	}

	public override EQUIPMENT_TYPE? GetIconMagiEnableType()
	{
		return skillData.tableData.GetEnableEquipType();
	}

	public override string GetDetail()
	{
		return skillData.GetExplanationText(false);
	}

	public override int GetNum()
	{
		return skillData.num;
	}

	public override RARITY_TYPE GetRarity()
	{
		return skillData.tableData.rarity;
	}

	public override ELEMENT_TYPE GetIconElement()
	{
		return skillData.tableData.skillAtkType;
	}

	public override int GetSalePrice()
	{
		return skillData.sellPrice;
	}

	public override bool CanSale()
	{
		return !skillData.isFavorite;
	}

	public override bool IsEquipping()
	{
		return skillData.isAttached;
	}

	public override int GetLevel()
	{
		return skillData.level;
	}

	public override REWARD_TYPE GetMaterialType()
	{
		return REWARD_TYPE.SKILL_ITEM;
	}

	public override bool IsEquipSomewhere()
	{
		return IsEquipping();
	}

	public override bool IsExceeded()
	{
		return skillData.IsExceeded();
	}

	public override uint GetMainorSortWeight()
	{
		uint num = 0u;
		uint num2 = (uint)(63 - skillData.tableData.type);
		num += num2 << 22;
		uint num3 = ElementTypeToMinorSortValue(GetIconElement());
		num += num3 << 19;
		uint rarity = (uint)GetRarity();
		num += rarity << 13;
		uint level = (uint)skillData.level;
		return num + (level << 6);
	}
}
