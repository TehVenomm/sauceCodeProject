using System;

public abstract class SortBase : GameSection
{
	public enum DIALOG_TYPE
	{
		WEAPON,
		ARMOR,
		SKILL,
		STORAGE_EQUIP,
		STORAGE_SKILL,
		USE_ITEM,
		MATERIAL,
		SMITH_CREATE_WEAPON,
		SMITH_CREATE_ARMOR,
		SMITH_CREATE_PICKUP_WEAPON,
		SMITH_CREATE_PICKUP_ARMOR,
		QUEST,
		ABILITY_ITEM
	}

	[Flags]
	public enum RARITY
	{
		D = 0x1,
		C = 0x2,
		B = 0x4,
		A = 0x8,
		S = 0x10,
		SS = 0x20,
		SSS = 0x40,
		ALL = 0x7F
	}

	[Flags]
	public enum ELEMENT
	{
		FIRE = 0x1,
		WATER = 0x2,
		THUNDER = 0x4,
		SOIL = 0x8,
		LIGHT = 0x10,
		DARK = 0x20,
		NONE = 0x40,
		ALL = 0x7F
	}

	[Flags]
	public enum TYPE
	{
		NONE = 0x0,
		ONE_HAND_SWORD = 0x1,
		TWO_HAND_SWORD = 0x2,
		SPEAR = 0x4,
		PAIR_SWORDS = 0x8,
		ARROW = 0x10,
		ARMOR = 0x20,
		HELM = 0x40,
		ARM = 0x80,
		LEG = 0x100,
		WEAPON_ALL = 0x1F,
		ARMOR_ALL = 0x1E0,
		EQUIP_ALL = 0x1FF,
		FIRE = 0x1,
		WARTER = 0x2,
		THUNDER = 0x4,
		SOIL = 0x8,
		LIGHT = 0x10,
		DARK = 0x20,
		ELEMENT_ALL = 0x3F,
		SKILL_ATTACK = 0x1,
		SKILL_SUPPORT = 0x2,
		SKILL_HEAL = 0x4,
		SKILL_RESTRAINT = 0x8,
		SKILL_SPECIAL = 0x10,
		SKILL_BUFF = 0x20,
		SKILL_SKILL_BUFF = 0x40,
		SKILL_PASSIVE = 0x80,
		SKILL_MOTION = 0x100,
		SKILL_LIMITED = 0x200,
		SKILL_GROW = 0x400,
		SKILL_ALL = 0x487,
		MATERIAL_COMMON = 0x1,
		MATERIAL_UNIQUE = 0x2,
		MATERIAL_LITHOGRAPH = 0x4,
		MATERIAL_EQUIP = 0x8,
		MATERIAL_METAL = 0x10,
		MATERIAL_ALL = 0x1F,
		ENEMY_GORILLA = 0x1,
		ENEMY_RABBIT = 0x2,
		ENEMY_CHIMERA = 0x4,
		ENEMY_WOLF = 0x8,
		ENEMY_DRAGON = 0x10,
		ENEMY_DRAKE = 0x20,
		ENEMY_WYVERN = 0x40,
		ENEMY_UNDEAD_KNIGHT = 0x80,
		ENEMY_WRAITH = 0x100,
		ENEMY_GIANT = 0x200,
		ENEMY_GOLEM = 0x400,
		ENEMY_ELEMENTAL = 0x800,
		ENEMY_CHICKEN = 0x1000,
		ENEMY_MUSHROOM = 0x2000,
		ENEMY_COW = 0x4000,
		ENEMY_FROG = 0x8000,
		ENEMY_BAT = 0x10000,
		ENEMY_SLIME = 0x20000,
		ENEMY_SAHUAGIN = 0x40000,
		ENEMY_ALL = 0x7FFFF
	}

	[Flags]
	public enum SORT_REQUIREMENT
	{
		ID = 0x1,
		NUM = 0x2,
		GET = 0x4,
		RARITY = 0x8,
		LV = 0x10,
		ATK = 0x20,
		DEF = 0x40,
		SALE = 0x80,
		SOCKET = 0x100,
		PRICE = 0x200,
		DIFFICULTY = 0x400,
		ENEMY = 0x800,
		HP = 0x1000,
		ELEMENT = 0x2000,
		ELEM_ATK = 0x4000,
		ELEM_DEF = 0x8000,
		SKILL_TYPE = 0x10000,
		REQUIREMENT_ALL_EQUIP_BIT = 0x219C,
		REQUIREMENT_WEAPON_BIT = 0x61BC,
		REQUIREMENT_ARMORS_BIT = 0xA1DC,
		REQUIREMENT_ITEM_BIT = 0x8A,
		REQUIREMENT_CREATE_WEAPON_BIT = 0x2128,
		REQUIREMENT_CREATE_PICKUP_WEAPON_BIT = 0x2129,
		REQUIREMENT_CREATE_ARMORS_BIT = 0x2148,
		REQUIREMENT_CREATE_PICKUP_ARMORS_BIT = 0x2149,
		REQUIREMENT_SKILL_BIT = 0x110FC,
		REQUIREMENT_QUEST_BIT = 0x40B
	}

	[Flags]
	public enum EQUIP_FILTER
	{
		PAY = 0x1,
		NO_PAY = 0x2,
		PAY_ALL = 0x3,
		CREATABLE = 0x4,
		NO_CREATABLE = 0x8,
		CREATABLE_ALL = 0xC,
		OBTAINED = 0x10,
		NO_OBTAINED = 0x20,
		OBTAINED_ALL = 0x30,
		GET_EQUIP_FILTER_ALL = 0x3F
	}

	protected SortSettings baseSortOrder;

	protected SortSettings sortOrder;

	public override void Initialize()
	{
		sortOrder = (SortSettings)GameSection.GetEventData();
		baseSortOrder = sortOrder.Clone();
		base.Initialize();
	}

	protected void OnQueryEvent_Rarity(out int _index, out bool _is_enable)
	{
		_index = (int)GameSection.GetEventData();
		int num = 1 << _index;
		if ((sortOrder.rarity & num) == 0)
		{
			_is_enable = true;
			sortOrder.rarity += num;
		}
		else
		{
			_is_enable = false;
			sortOrder.rarity -= num;
		}
	}

	protected void OnQueryEvent_Type(out int _index, out bool _is_enable)
	{
		_index = (int)GameSection.GetEventData();
		int num = 1 << _index;
		if ((sortOrder.type & num) == 0)
		{
			_is_enable = true;
			sortOrder.type += num;
		}
		else
		{
			_is_enable = false;
			sortOrder.type -= num;
		}
	}

	protected void OnQueryEvent_EquipFilter(out int _index, out bool _is_enable)
	{
		_index = (int)GameSection.GetEventData();
		int num = 1 << _index;
		if ((sortOrder.equipFilter & num) == 0)
		{
			_is_enable = true;
			sortOrder.equipFilter += num;
		}
		else
		{
			_is_enable = false;
			sortOrder.equipFilter -= num;
		}
	}

	protected void OnQueryEvent_Element(out int _index, out bool _is_enable)
	{
		_index = (int)GameSection.GetEventData();
		int num = 1 << _index;
		if ((sortOrder.element & num) == 0)
		{
			_is_enable = true;
			sortOrder.element += num;
		}
		else
		{
			_is_enable = false;
			sortOrder.element -= num;
		}
	}

	protected void OnQuery_REQUIREMENT()
	{
		SORT_REQUIREMENT requirement = (SORT_REQUIREMENT)(int)GameSection.GetEventData();
		sortOrder.requirement = requirement;
	}

	protected void OnQuery_ORDER_TYPE()
	{
		bool orderTypeAsc = (int)GameSection.GetEventData() == 0;
		sortOrder.orderTypeAsc = orderTypeAsc;
	}

	protected void OnQuery_BACK()
	{
		GameSection.SetEventData(null);
		GameSection.BackSection();
	}

	protected void OnQuery_SORTING()
	{
		GameSaveData.instance.SetSortBit(sortOrder);
		GameSaveData.Save();
		GameSection.SetEventData(sortOrder);
		GameSection.BackSection();
	}
}
