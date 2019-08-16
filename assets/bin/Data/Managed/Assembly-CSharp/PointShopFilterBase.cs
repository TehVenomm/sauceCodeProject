using Network;
using System;
using System.Collections.Generic;
using System.Linq;

public class PointShopFilterBase : GameSection
{
	public enum UI
	{
		BTN_ALL_SELECT,
		BTN_ALL_DESELECT,
		GRD_TYPE_ROOT,
		GRD_RARITY_ROOT,
		BTN_TICKET,
		BTN_MONEY,
		BTN_USE_ITEM,
		BTN_LITHOGRAPH,
		BTN_METAL,
		BTN_MATERIAL,
		BTN_QUEST,
		BTN_SKILL,
		BTN_EQUIP,
		BTN_STAMP,
		BTN_AVATAR,
		BTN_DEGREE,
		BTN_RARITY_D,
		BTN_RARITY_C,
		BTN_RARITY_B,
		BTN_RARITY_A,
		BTN_RARITY_S,
		BTN_RARITY_SS,
		SPR_GRAY
	}

	public class Filter
	{
		public enum CATEGORY
		{
			TYPE,
			RARITY
		}

		public class CheckFilterBase
		{
			public enum TYPE
			{
				TICKET,
				MONEY,
				USE_ITEM,
				LITHOGRAPH,
				METAL,
				MATERIAL,
				QUEST,
				SKILL,
				EQUIP,
				STAMP,
				AVATAR,
				DEGREE,
				ABILITY_ITEM
			}

			public TYPE type;

			public RARITY_TYPE rarity;

			public UI checkboxUI;

			public CheckFilterBase(UI ui)
			{
				checkboxUI = ui;
			}

			public void DoFiltering(ref List<PointShopItem> list)
			{
				list = (from x in list
				where !IsCondition(x)
				select x).ToList();
			}

			protected virtual bool IsCondition(PointShopItem item)
			{
				return false;
			}

			public bool IsInclude(List<PointShopItem> list)
			{
				return list.Any((PointShopItem x) => IsCondition(x));
			}
		}

		public class TicketCheckFilter : CheckFilterBase
		{
			public TicketCheckFilter(UI ui)
				: base(ui)
			{
				type = TYPE.TICKET;
			}

			protected override bool IsCondition(PointShopItem item)
			{
				if (item.type == 3)
				{
					ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint)item.itemId);
					return itemData.type == ITEM_TYPE.TICKET;
				}
				return false;
			}
		}

		public class MoneyCheckFilter : CheckFilterBase
		{
			public MoneyCheckFilter(UI ui)
				: base(ui)
			{
				type = TYPE.MONEY;
			}

			protected override bool IsCondition(PointShopItem item)
			{
				return item.type == 2;
			}
		}

		public class UseItemCheckFilter : CheckFilterBase
		{
			public UseItemCheckFilter(UI ui)
				: base(ui)
			{
				type = TYPE.USE_ITEM;
			}

			protected override bool IsCondition(PointShopItem item)
			{
				if (item.type == 3)
				{
					ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint)item.itemId);
					return itemData.type == ITEM_TYPE.USE_ITEM;
				}
				return false;
			}
		}

		public class LithographCheckFilter : CheckFilterBase
		{
			public LithographCheckFilter(UI ui)
				: base(ui)
			{
				type = TYPE.LITHOGRAPH;
			}

			protected override bool IsCondition(PointShopItem item)
			{
				if (item.type == 3)
				{
					ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint)item.itemId);
					return itemData.type == ITEM_TYPE.LITHOGRAPH;
				}
				return false;
			}
		}

		public class MetalCheckFilter : CheckFilterBase
		{
			public MetalCheckFilter(UI ui)
				: base(ui)
			{
				type = TYPE.METAL;
			}

			protected override bool IsCondition(PointShopItem item)
			{
				if (item.type == 3)
				{
					ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint)item.itemId);
					return itemData.type == ITEM_TYPE.MATERIAL_METAL;
				}
				return false;
			}
		}

		public class MaterialCheckFilter : CheckFilterBase
		{
			public MaterialCheckFilter(UI ui)
				: base(ui)
			{
				type = TYPE.MATERIAL;
			}

			protected override bool IsCondition(PointShopItem item)
			{
				if (item.type == 3)
				{
					ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint)item.itemId);
					switch (itemData.type)
					{
					case ITEM_TYPE.MATERIAL_METAL:
					case ITEM_TYPE.LITHOGRAPH:
					case ITEM_TYPE.USE_ITEM:
					case ITEM_TYPE.TICKET:
					case ITEM_TYPE.FORTUNE_TICKET:
						return false;
					default:
						return true;
					}
				}
				return false;
			}
		}

		public class QuestCheckFilter : CheckFilterBase
		{
			public QuestCheckFilter(UI ui)
				: base(ui)
			{
				type = TYPE.QUEST;
			}

			protected override bool IsCondition(PointShopItem item)
			{
				return item.type == 6;
			}
		}

		public class SkillCheckFilter : CheckFilterBase
		{
			public SkillCheckFilter(UI ui)
				: base(ui)
			{
				type = TYPE.SKILL;
			}

			protected override bool IsCondition(PointShopItem item)
			{
				return item.type == 5;
			}
		}

		public class EquipCheckFilter : CheckFilterBase
		{
			public EquipCheckFilter(UI ui)
				: base(ui)
			{
				type = TYPE.EQUIP;
			}

			protected override bool IsCondition(PointShopItem item)
			{
				return item.type == 4;
			}
		}

		public class StampCheckFilter : CheckFilterBase
		{
			public StampCheckFilter(UI ui)
				: base(ui)
			{
				type = TYPE.STAMP;
			}

			protected override bool IsCondition(PointShopItem item)
			{
				return item.type == 8;
			}
		}

		public class AvatarCheckFilter : CheckFilterBase
		{
			public AvatarCheckFilter(UI ui)
				: base(ui)
			{
				type = TYPE.AVATAR;
			}

			protected override bool IsCondition(PointShopItem item)
			{
				return item.type == 7;
			}
		}

		public class DegreeCheckFilter : CheckFilterBase
		{
			public DegreeCheckFilter(UI ui)
				: base(ui)
			{
				type = TYPE.DEGREE;
			}

			protected override bool IsCondition(PointShopItem item)
			{
				return item.type == 9;
			}
		}

		public class AbilityItemCheckFilter : CheckFilterBase
		{
			public AbilityItemCheckFilter(UI ui)
				: base(ui)
			{
				type = TYPE.ABILITY_ITEM;
			}

			protected override bool IsCondition(PointShopItem item)
			{
				return item.type == 10;
			}
		}

		public class RarityCheckFilter : CheckFilterBase
		{
			public RarityCheckFilter(UI ui, RARITY_TYPE r)
				: base(ui)
			{
				rarity = r;
			}

			protected override bool IsCondition(PointShopItem item)
			{
				switch (item.type)
				{
				case 3:
				case 10:
				{
					ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint)item.itemId);
					if (itemData.type == ITEM_TYPE.USE_ITEM)
					{
						return false;
					}
					return itemData.rarity == rarity;
				}
				case 4:
				{
					EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint)item.itemId);
					return equipItemData.rarity == rarity;
				}
				case 6:
				{
					QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint)item.itemId);
					return questData.rarity == rarity;
				}
				case 5:
				{
					SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData((uint)item.itemId);
					return skillItemData.rarity == rarity;
				}
				case 14:
				{
					AccessoryTable.AccessoryData data = Singleton<AccessoryTable>.I.GetData((uint)item.itemId);
					return data.rarity == rarity;
				}
				default:
					return false;
				}
			}
		}

		public int typeBit;

		public CheckFilterBase[] typeFilter;

		public int rarityBit;

		public CheckFilterBase[] rarityFilter;

		public int GetAllTrueBit(CATEGORY category)
		{
			int num = 0;
			switch (category)
			{
			case CATEGORY.TYPE:
				for (int j = 0; j < typeFilter.Length; j++)
				{
					num |= 1 << (int)typeFilter[j].type;
				}
				break;
			case CATEGORY.RARITY:
				for (int i = 0; i < rarityFilter.Length; i++)
				{
					num |= 1 << (int)rarityFilter[i].rarity;
				}
				break;
			}
			return num;
		}

		public void ResetBit(bool is_check)
		{
			if (is_check)
			{
				typeBit = GetAllTrueBit(CATEGORY.TYPE);
				rarityBit = GetAllTrueBit(CATEGORY.RARITY);
			}
			else
			{
				typeBit = 0;
				rarityBit = 0;
			}
		}

		public void FlipBit(CATEGORY category, int index)
		{
			switch (category)
			{
			case CATEGORY.TYPE:
				typeBit ^= 1 << index;
				break;
			case CATEGORY.RARITY:
				rarityBit ^= 1 << index;
				break;
			}
		}

		public bool IsCheck(CATEGORY category, int index)
		{
			if (index >= 0)
			{
				switch (category)
				{
				case CATEGORY.TYPE:
					return (typeBit & (1 << index)) != 0;
				case CATEGORY.RARITY:
					return (rarityBit & (1 << index)) != 0;
				}
			}
			return true;
		}

		public void DoFiltering(ref List<PointShopItem> list)
		{
			for (int i = 0; i < typeFilter.Length; i++)
			{
				if (!IsCheck(CATEGORY.TYPE, i))
				{
					typeFilter[i].DoFiltering(ref list);
				}
			}
			for (int j = 0; j < rarityFilter.Length; j++)
			{
				if (!IsCheck(CATEGORY.RARITY, j))
				{
					rarityFilter[j].DoFiltering(ref list);
				}
			}
		}
	}

	private Filter filter;

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		Filter filter = array[0] as Filter;
		if (filter == null)
		{
			filter = new Filter();
			filter.typeFilter = new Filter.CheckFilterBase[12]
			{
				new Filter.TicketCheckFilter(UI.BTN_TICKET),
				new Filter.MoneyCheckFilter(UI.BTN_MONEY),
				new Filter.UseItemCheckFilter(UI.BTN_USE_ITEM),
				new Filter.LithographCheckFilter(UI.BTN_LITHOGRAPH),
				new Filter.MetalCheckFilter(UI.BTN_METAL),
				new Filter.MaterialCheckFilter(UI.BTN_MATERIAL),
				new Filter.QuestCheckFilter(UI.BTN_QUEST),
				new Filter.SkillCheckFilter(UI.BTN_SKILL),
				new Filter.EquipCheckFilter(UI.BTN_EQUIP),
				new Filter.StampCheckFilter(UI.BTN_STAMP),
				new Filter.AvatarCheckFilter(UI.BTN_AVATAR),
				new Filter.DegreeCheckFilter(UI.BTN_DEGREE)
			};
			filter.typeBit = filter.GetAllTrueBit(Filter.CATEGORY.TYPE);
			filter.rarityFilter = new Filter.CheckFilterBase[6]
			{
				new Filter.RarityCheckFilter(UI.BTN_RARITY_D, RARITY_TYPE.D),
				new Filter.RarityCheckFilter(UI.BTN_RARITY_C, RARITY_TYPE.C),
				new Filter.RarityCheckFilter(UI.BTN_RARITY_B, RARITY_TYPE.B),
				new Filter.RarityCheckFilter(UI.BTN_RARITY_A, RARITY_TYPE.A),
				new Filter.RarityCheckFilter(UI.BTN_RARITY_S, RARITY_TYPE.S),
				new Filter.RarityCheckFilter(UI.BTN_RARITY_SS, RARITY_TYPE.SS)
			};
			filter.rarityBit = filter.GetAllTrueBit(Filter.CATEGORY.RARITY);
		}
		this.filter = filter;
		List<PointShopItem> list = array[1] as List<PointShopItem>;
		Filter.CheckFilterBase[] typeFilter = this.filter.typeFilter;
		foreach (Filter.CheckFilterBase checkFilterBase in typeFilter)
		{
			SetEvent((Enum)checkFilterBase.checkboxUI, "TYPE", (int)checkFilterBase.type);
			if (list != null)
			{
				bool flag = checkFilterBase.IsInclude(list);
				SetActive((Enum)checkFilterBase.checkboxUI, flag);
				SetActive(GetCtrl(checkFilterBase.checkboxUI).get_parent(), UI.SPR_GRAY, !flag);
			}
		}
		Filter.CheckFilterBase[] rarityFilter = this.filter.rarityFilter;
		foreach (Filter.CheckFilterBase checkFilterBase2 in rarityFilter)
		{
			SetEvent((Enum)checkFilterBase2.checkboxUI, "RARITY", (int)checkFilterBase2.rarity);
		}
		SetEvent((Enum)UI.BTN_ALL_DESELECT, "ALL", 0);
		SetEvent((Enum)UI.BTN_ALL_SELECT, "ALL", 1);
		RefreshUI();
		base.Initialize();
	}

	public override void UpdateUI()
	{
		if (filter != null)
		{
			Filter.CheckFilterBase[] rarityFilter = filter.rarityFilter;
			foreach (Filter.CheckFilterBase checkFilterBase in rarityFilter)
			{
				SetToggle((Enum)checkFilterBase.checkboxUI, filter.IsCheck(Filter.CATEGORY.RARITY, (int)checkFilterBase.rarity));
			}
			Filter.CheckFilterBase[] typeFilter = filter.typeFilter;
			foreach (Filter.CheckFilterBase checkFilterBase2 in typeFilter)
			{
				SetToggle((Enum)checkFilterBase2.checkboxUI, filter.IsCheck(Filter.CATEGORY.TYPE, (int)checkFilterBase2.type));
			}
		}
	}

	private void OnQuery_TYPE()
	{
		int index = (int)GameSection.GetEventData();
		filter.FlipBit(Filter.CATEGORY.TYPE, index);
	}

	private void OnQuery_RARITY()
	{
		int index = (int)GameSection.GetEventData();
		filter.FlipBit(Filter.CATEGORY.RARITY, index);
	}

	private void OnQuery_ALL()
	{
		int num = (int)GameSection.GetEventData();
		filter.ResetBit(num != 0);
		RefreshUI();
	}

	private void OnQuery_FILTERING()
	{
		GameSection.SetEventData(filter);
		GameSection.BackSection();
	}
}
