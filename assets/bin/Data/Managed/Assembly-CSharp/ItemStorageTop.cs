using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemStorageTop : SkillInfoBase
{
	private enum UI
	{
		SCR_INVENTORY,
		GRD_INVENTORY,
		GRD_INVENTORY_SMALL,
		SPR_SCR_BAR,
		SCR_INVENTORY_EQUIP,
		GRD_INVENTORY_EQUIP,
		GRD_INVENTORY_EQUIP_SMALL,
		SPR_EQUIP_SCR_BAR,
		TGL_CHANGE_INVENTORY,
		TGL_ICON_ASC,
		LBL_SORT,
		BTN_SORT,
		SPR_INVALID_SORT,
		LBL_INVALID_SORT,
		BTN_CHANGE,
		SPR_INVALID_CHANGE,
		TGL_TAB0,
		TGL_TAB1,
		TGL_TAB2,
		TGL_TAB3,
		TGL_TAB4,
		TGL_TAB5,
		OBJ_BTN_SELL_MODE,
		OBJ_SELL_MODE_ROOT,
		LBL_MAX_SELECT_NUM,
		LBL_SELECT_NUM,
		LBL_TOTAL,
		LBL_MAX_HAVE_NUM,
		LBL_NOW_HAVE_NUM,
		OBJ_CAPTION_3,
		LBL_CAPTION
	}

	private enum POW_TYPE
	{
		NONE,
		ATK,
		DEF,
		EXP_UP,
		MONEY_UP
	}

	public enum TAB_MODE
	{
		MATERIAL,
		USE_ITEM,
		EQUIP,
		SKILL,
		LAPIS,
		ACCESSORY,
		MAX
	}

	public class InventoryBase
	{
		public SortSettings sortSettings;

		public SortCompareData[] datas;

		public bool Sort(SortSettings sort_settings)
		{
			sortSettings = sort_settings;
			return DoSort();
		}

		protected virtual bool DoSort()
		{
			return false;
		}

		public virtual ItemIcon CreateIcon(object[] data)
		{
			return null;
		}
	}

	public class MaterialInventory : InventoryBase
	{
		public MaterialInventory(bool include_material, bool include_lithograph, bool include_lapis, GET_TYPE? get_type = default(GET_TYPE?))
		{
			List<ItemInfo> item_inventory = new List<ItemInfo>();
			if (!include_lapis)
			{
				sortSettings = SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.MATERIAL, SortSettings.SETTINGS_TYPE.MATERIAL);
			}
			else
			{
				sortSettings = new SortSettings();
				sortSettings.requirement = SortBase.SORT_REQUIREMENT.NUM;
			}
			MonoBehaviourSingleton<InventoryManager>.I.ForAllItemInventory(delegate(ItemInfo item_data)
			{
				if (item_data.num > 0 && item_data.tableData.type != ITEM_TYPE.USE_ITEM && item_data.tableData.type != ITEM_TYPE.DELIVERY && item_data.tableData.type != ITEM_TYPE.EQUIP_SET_EXT && (include_material || item_data.tableData.type == ITEM_TYPE.LITHOGRAPH || item_data.tableData.type == ITEM_TYPE.LAPIS) && (include_lithograph || item_data.tableData.type != ITEM_TYPE.LITHOGRAPH) && (include_lapis || item_data.tableData.type != ITEM_TYPE.LAPIS))
				{
					if (get_type.HasValue)
					{
						GET_TYPE value = get_type.Value;
						if ((value == GET_TYPE.PAY && item_data.tableData.getType != GET_TYPE.PAY) || (value != GET_TYPE.PAY && item_data.tableData.getType == GET_TYPE.PAY))
						{
							return;
						}
					}
					if (item_data.tableData.type != ITEM_TYPE.TICKET && item_data.tableData.type != ITEM_TYPE.ABILITY_ITEM && item_data.tableData.type != ITEM_TYPE.EVENT_POINT)
					{
						item_inventory.Add(item_data);
					}
				}
			});
			if (include_lapis)
			{
				using (List<ItemTable.ItemData>.Enumerator enumerator = Singleton<ItemTable>.I.GetItemTypeItemData(ITEM_TYPE.LAPIS).GetEnumerator())
				{
					ItemTable.ItemData lapisData;
					while (enumerator.MoveNext())
					{
						lapisData = enumerator.Current;
						if (item_inventory.Find((ItemInfo x) => x.tableData.id == lapisData.id) == null && (lapisData.endDate == default(DateTime) || lapisData.endDate > TimeManager.GetNow()))
						{
							Item recv_data = new Item
							{
								num = 0,
								itemId = lapisData.id,
								uniqId = "0"
							};
							item_inventory.Add(new ItemInfo(recv_data));
						}
					}
				}
			}
			datas = sortSettings.CreateSortAry<ItemInfo, ItemSortData>(item_inventory.ToArray());
			if (!include_lapis)
			{
				AbilityItemSortData[] array = sortSettings.CreateSortAry<AbilityItemInfo, AbilityItemSortData>((from x in MonoBehaviourSingleton<InventoryManager>.I.abilityItemInventory.GetAll()
				where x.equipUniqueId == 0
				select x).ToArray());
				if (array.Length > 0)
				{
					int destinationIndex = datas.Length;
					Array.Resize(ref datas, datas.Length + array.Length);
					Array.Copy(array, 0, datas, destinationIndex, array.Length);
					sortSettings.Sort(datas);
				}
			}
		}

		protected override bool DoSort()
		{
			return sortSettings.Sort(datas);
		}

		public override ItemIcon CreateIcon(object[] data)
		{
			SortCompareData sortCompareData = data[0] as SortCompareData;
			Transform parent = data[1] as Transform;
			int event_data = (int)data[2];
			ItemStorageTop itemStorageTop = data[3] as ItemStorageTop;
			bool is_new = false;
			if (sortCompareData is ItemSortData)
			{
				is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(ITEM_ICON_TYPE.ITEM, sortCompareData.GetUniqID());
			}
			else if (sortCompareData is AbilityItemSortData)
			{
				is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(ITEM_ICON_TYPE.ABILITY_ITEM, sortCompareData.GetUniqID());
				MonoBehaviourSingleton<InventoryManager>.I.AddShowFragsAbilityItem(sortCompareData.GetUniqID());
			}
			ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(sortCompareData.GetTableID());
			ItemIcon itemIcon = ItemIconDetail.CreateMaterialIcon(sortCompareData.GetIconType(), sortCompareData.GetIconID(), sortCompareData.GetRarity(), itemData, itemStorageTop.showInventoryMode == SHOW_INVENTORY_MODE.MAIN_STATUS, parent, sortCompareData.GetNum(), sortCompareData.GetName(), "SELECT", event_data, -1, false, is_new);
			if (sortCompareData is AbilityItemSortData)
			{
				(itemIcon as ItemIconDetail).setupperMaterial.SetDescription(sortCompareData.GetDetail());
			}
			return itemIcon;
		}
	}

	public class UseItemInventory : InventoryBase
	{
		public UseItemInventory()
		{
			sortSettings = SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.USE_ITEM, SortSettings.SETTINGS_TYPE.USE_ITEM);
			List<ItemInfo> list = new List<ItemInfo>();
			for (LinkedListNode<ItemInfo> linkedListNode = MonoBehaviourSingleton<InventoryManager>.I.itemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				bool flag = IsUseItemType(linkedListNode.Value.tableData.type);
				if (linkedListNode.Value.num > 0 && flag)
				{
					list.Add(linkedListNode.Value);
				}
			}
			datas = sortSettings.CreateSortAry<ItemInfo, ItemSortData>(list.ToArray());
		}

		protected virtual bool IsUseItemType(ITEM_TYPE item_type)
		{
			return item_type == ITEM_TYPE.USE_ITEM || item_type == ITEM_TYPE.EQUIP_SET_EXT;
		}

		protected override bool DoSort()
		{
			return sortSettings.Sort(datas as ItemSortData[]);
		}

		public override ItemIcon CreateIcon(object[] data)
		{
			SortCompareData sortCompareData = data[0] as SortCompareData;
			Transform parent = data[1] as Transform;
			int event_data = (int)data[2];
			ItemStorageTop itemStorageTop = data[3] as ItemStorageTop;
			bool is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(ITEM_ICON_TYPE.ITEM, sortCompareData.GetUniqID());
			ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(sortCompareData.GetTableID());
			return ItemIconDetail.CreateMaterialIcon(sortCompareData.GetIconType(), sortCompareData.GetIconID(), sortCompareData.GetRarity(), itemData, itemStorageTop.showInventoryMode == SHOW_INVENTORY_MODE.MAIN_STATUS, parent, sortCompareData.GetNum(), sortCompareData.GetName(), "SELECT", event_data, -1, false, is_new);
		}
	}

	public class EquipItemInventory : InventoryBase
	{
		public EquipItemInventory(GET_TYPE? get_type = default(GET_TYPE?))
		{
			sortSettings = SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.STORAGE_EQUIP, SortSettings.SETTINGS_TYPE.STORAGE_EQUIP);
			EquipItemInfo[] array = null;
			List<EquipItemInfo> list = new List<EquipItemInfo>();
			for (LinkedListNode<EquipItemInfo> linkedListNode = MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				bool flag = true;
				if (get_type.HasValue)
				{
					GET_TYPE value = get_type.Value;
					if (value == GET_TYPE.PAY && linkedListNode.Value.tableData.getType != GET_TYPE.PAY)
					{
						flag = false;
					}
					else if (value != GET_TYPE.PAY && linkedListNode.Value.tableData.getType == GET_TYPE.PAY)
					{
						flag = false;
					}
				}
				if (flag)
				{
					list.Add(linkedListNode.Value);
				}
			}
			array = list.ToArray();
			datas = sortSettings.CreateSortAry<EquipItemInfo, EquipItemSortData>(array);
		}

		protected override bool DoSort()
		{
			return sortSettings.Sort(datas as EquipItemSortData[]);
		}

		public override ItemIcon CreateIcon(object[] data)
		{
			EquipItemSortData item_data = data[0] as EquipItemSortData;
			Transform parent = data[1] as Transform;
			int event_data = (int)data[2];
			ItemStorageTop itemStorageTop = data[3] as ItemStorageTop;
			int num = -1;
			int toggle_group = -1;
			if (itemStorageTop.isSellMode)
			{
				toggle_group = 0;
				num = itemStorageTop.sellItemData.FindIndex((SortCompareData sell_data) => sell_data.GetUniqID() == item_data.GetUniqID());
			}
			bool is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(item_data.GetIconType(), item_data.GetUniqID());
			EquipItemInfo equip = item_data.GetItemData() as EquipItemInfo;
			SkillSlotUIData[] skillSlotData = itemStorageTop.GetSkillSlotData(equip);
			if (num > -1)
			{
				num++;
			}
			return ItemIconDetail.CreateEquipDetailSelectNumberIcon(item_data, skillSlotData, itemStorageTop.showInventoryMode == SHOW_INVENTORY_MODE.MAIN_STATUS, parent, "SELECT", event_data, ItemIconDetail.ICON_STATUS.NONE, is_new, toggle_group, num, (!item_data.IsEquipping()) ? (-1) : 0, GET_TYPE.PAY);
		}
	}

	public class SkillItemInventory : InventoryBase
	{
		public SkillItemInventory(SortSettings.SETTINGS_TYPE sort_mem_type = SortSettings.SETTINGS_TYPE.STORAGE_SKILL, SKILL_SLOT_TYPE slot_type = SKILL_SLOT_TYPE.NONE, bool isAddMaterial = false)
		{
			Init(sort_mem_type, slot_type, -1, isAddMaterial);
		}

		private void Init(SortSettings.SETTINGS_TYPE sort_mem_type = SortSettings.SETTINGS_TYPE.STORAGE_SKILL, SKILL_SLOT_TYPE slot_type = SKILL_SLOT_TYPE.NONE, int base_item_id = -1, bool isAddMaterial = false)
		{
			int num = (slot_type != 0) ? (1 << (int)(slot_type - 1)) : 1159;
			SortBase.DIALOG_TYPE dialog_type = (num != 1159) ? SortBase.DIALOG_TYPE.SKILL : SortBase.DIALOG_TYPE.STORAGE_SKILL;
			sortSettings = SortSettings.CreateMemSortSettings(dialog_type, sort_mem_type);
			List<SkillItemInfo> list = new List<SkillItemInfo>();
			for (LinkedListNode<SkillItemInfo> linkedListNode = MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				if (slot_type == SKILL_SLOT_TYPE.NONE || linkedListNode.Value.tableData.type == slot_type)
				{
					list.Add(linkedListNode.Value);
				}
			}
			if (isAddMaterial)
			{
				for (LinkedListNode<SkillItemInfo> linkedListNode = MonoBehaviourSingleton<InventoryManager>.I.skillMaterialInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
				{
					if (slot_type == SKILL_SLOT_TYPE.NONE || linkedListNode.Value.tableData.type == slot_type)
					{
						list.Add(linkedListNode.Value);
					}
				}
			}
			datas = sortSettings.CreateSortAry<SkillItemInfo, SkillItemSortData>(list.ToArray());
		}

		protected override bool DoSort()
		{
			return sortSettings.Sort(datas as SkillItemSortData[]);
		}

		public override ItemIcon CreateIcon(object[] data)
		{
			SkillItemSortData item_data = data[0] as SkillItemSortData;
			Transform parent = data[1] as Transform;
			int event_data = (int)data[2];
			ItemStorageTop itemStorageTop = data[3] as ItemStorageTop;
			int num = -1;
			int toggle_group = -1;
			if (itemStorageTop.isSellMode)
			{
				toggle_group = 0;
				num = itemStorageTop.sellItemData.FindIndex((SortCompareData sell_data) => sell_data.GetUniqID() == item_data.GetUniqID());
			}
			bool is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(item_data.GetIconType(), item_data.GetUniqID());
			if (num > -1)
			{
				num++;
			}
			ItemIconDetail.ICON_STATUS icon_status = item_data.IsExceeded() ? ItemIconDetail.ICON_STATUS.VALID_EXCEED : ItemIconDetail.ICON_STATUS.NONE;
			return ItemIconDetail.CreateSkillDetailSelectNumberIcon(item_data.GetIconType(), item_data.GetIconID(), item_data.GetRarity(), item_data, itemStorageTop.showInventoryMode == SHOW_INVENTORY_MODE.MAIN_STATUS, parent, "SELECT", event_data, is_new, toggle_group, num, item_data.IsEquipping(), icon_status);
		}
	}

	public class QuestItemInventory : InventoryBase
	{
		public QuestItemInventory()
		{
			sortSettings = SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.QUEST, SortSettings.SETTINGS_TYPE.ORDER_QUEST);
			List<QuestItemInfo> list = new List<QuestItemInfo>();
			MonoBehaviourSingleton<InventoryManager>.I.ForAllQuestInvetory(delegate(QuestItemInfo data)
			{
				if (data.infoData.questData.num > 0)
				{
					list.Add(data);
				}
			});
			datas = sortSettings.CreateSortAry<QuestItemInfo, QuestSortData>(list.ToArray());
		}

		protected override bool DoSort()
		{
			return sortSettings.Sort(datas as QuestSortData[]);
		}

		public override ItemIcon CreateIcon(object[] data)
		{
			QuestSortData questSortData = data[0] as QuestSortData;
			Transform parent = data[1] as Transform;
			int event_data = (int)data[2];
			ItemStorageTop itemStorageTop = data[3] as ItemStorageTop;
			bool is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(questSortData.GetIconType(), questSortData.GetUniqID());
			ItemIcon itemIcon = ItemIconDetail.CreateQuestItemIcon(questSortData.GetIconType(), questSortData.GetIconID(), questSortData.GetRarity(), questSortData, itemStorageTop.showInventoryMode == SHOW_INVENTORY_MODE.MAIN_STATUS, parent, questSortData.GetNum(), questSortData.GetName(), "SELECT", event_data, -1, false, is_new);
			itemIcon.SetEnemyIconScale(questSortData.GetIconType(), 0.9f);
			return itemIcon;
		}
	}

	public class AbilityItemInventory : InventoryBase
	{
		public AbilityItemInventory()
		{
			sortSettings = SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.ABILITY_ITEM, SortSettings.SETTINGS_TYPE.STORAGE_ABILITY_ITEM);
			AbilityItemInfo[] target_ary = (from x in MonoBehaviourSingleton<InventoryManager>.I.abilityItemInventory.GetAll()
			where x.equipUniqueId == 0
			select x).ToArray();
			datas = sortSettings.CreateSortAry<AbilityItemInfo, AbilityItemSortData>(target_ary);
		}

		protected override bool DoSort()
		{
			return sortSettings.Sort(datas as AbilityItemSortData[]);
		}

		public override ItemIcon CreateIcon(object[] objects)
		{
			AbilityItemSortData abilityItemSortData = objects[0] as AbilityItemSortData;
			Transform parent = objects[1] as Transform;
			int event_data = (int)objects[2];
			SHOW_INVENTORY_MODE sHOW_INVENTORY_MODE = (SHOW_INVENTORY_MODE)(int)objects[3];
			bool is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(ITEM_ICON_TYPE.ABILITY_ITEM, abilityItemSortData.GetUniqID());
			ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(abilityItemSortData.GetTableID());
			return ItemIconDetail.CreateMaterialIcon(abilityItemSortData.GetIconType(), abilityItemSortData.GetIconID(), abilityItemSortData.GetRarity(), itemData, true, parent, abilityItemSortData.GetNum(), abilityItemSortData.GetName(), "SELECT_ITEM", event_data, -1, false, is_new);
		}
	}

	public class AccessoryInventory : InventoryBase
	{
		public AccessoryInventory()
		{
			sortSettings = SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.ACCESSORY, SortSettings.SETTINGS_TYPE.STORAGE_ACCESSORY);
			AccessoryInfo[] target_ary = MonoBehaviourSingleton<InventoryManager>.I.accessoryInventory.GetAll().ToArray();
			datas = sortSettings.CreateSortAry<AccessoryInfo, AccessorySortData>(target_ary);
		}

		protected override bool DoSort()
		{
			return sortSettings.Sort(datas as AccessorySortData[]);
		}

		public override ItemIcon CreateIcon(object[] objects)
		{
			AccessorySortData accessorySortData = objects[0] as AccessorySortData;
			Transform parent = objects[1] as Transform;
			int eventData = (int)objects[2];
			bool isNew = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(ITEM_ICON_TYPE.ACCESSORY, accessorySortData.GetUniqID());
			return ItemIconDetail.CreateAccessoryIcon(accessorySortData.itemData.tableData, parent, "SELECT_ITEM", eventData, isNew, false);
		}
	}

	public enum SHOW_INVENTORY_MODE
	{
		MAIN_STATUS,
		SUB_STATUS,
		MAX
	}

	private const float DEFAULT_LAST_SCROLL_POS_Y = -9999.9f;

	private readonly UI[] uiTab = new UI[6]
	{
		UI.TGL_TAB0,
		UI.TGL_TAB1,
		UI.TGL_TAB2,
		UI.TGL_TAB3,
		UI.TGL_TAB4,
		UI.TGL_TAB5
	};

	protected InventoryBase[] inventories = new InventoryBase[6];

	protected TAB_MODE tab;

	protected TAB_MODE m_prevOpendTab;

	public List<SortCompareData> sellItemData = new List<SortCompareData>();

	protected bool isSellMode;

	private UI inventoryUI;

	protected bool includeLithograph = true;

	protected bool includeMaterialItem = true;

	protected GET_TYPE? ItemGetType;

	protected ItemStorageSellConfirm.GO_BACK confirmTo;

	private Vector3 m_currentScrollTopPos = Vector3.zero;

	protected bool m_isEnableUpdateScrollTopPos = true;

	protected Vector3 m_lastScrollPosition = Vector3.up * -9999.9f;

	protected Vector3 m_scrollTopPosition = Vector3.zero;

	protected float m_maxScrollableValue;

	public SHOW_INVENTORY_MODE showInventoryMode;

	private TAB_MODE tmpTab = TAB_MODE.MAX;

	private int toggleIndex = -1;

	protected Vector3 CurrentScrollTopPos => m_currentScrollTopPos;

	public override void Exit()
	{
		MonoBehaviourSingleton<InventoryManager>.I.DoRemoveNewFragsAbilityItem();
		base.Exit();
	}

	protected void SetNextTab(TAB_MODE _tab)
	{
		m_prevOpendTab = tab;
		tab = _tab;
	}

	protected void UpdateCurrentScrollTopPos(Vector3 _newPos)
	{
		if (m_isEnableUpdateScrollTopPos)
		{
			m_isEnableUpdateScrollTopPos = false;
			m_currentScrollTopPos = _newPos;
		}
	}

	public override void Initialize()
	{
		int i = 0;
		for (int num = uiTab.Length; i < num; i++)
		{
			UIToggle component = GetComponent<UIToggle>(uiTab[i]);
			if ((UnityEngine.Object)component != (UnityEngine.Object)null)
			{
				component.onChange.Clear();
			}
		}
		InitializeCaption();
		base.Initialize();
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return base.GetUpdateUINotifyFlags() | NOTIFY_FLAG.UPDATE_ITEM_INVENTORY | NOTIFY_FLAG.UPDATE_EQUIP_INVENTORY | NOTIFY_FLAG.UPDATE_SKILL_INVENTORY | NOTIFY_FLAG.UPDATE_QUEST_ITEM_INVENTORY | NOTIFY_FLAG.UPDATE_SKILL_CHANGE | NOTIFY_FLAG.UPDATE_EQUIP_FAVORITE | NOTIFY_FLAG.UPDATE_SKILL_FAVORITE | NOTIFY_FLAG.REMOVE_NEW_ICON | NOTIFY_FLAG.UPDATE_INVENTORY_CAPACITY | NOTIFY_FLAG.UPDATE_ABILITY_ITEM_INVENTORY | NOTIFY_FLAG.UPDATE_EQUIP_GROW;
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		if ((flags & NOTIFY_FLAG.UPDATE_EQUIP_FAVORITE) != (NOTIFY_FLAG)0L || (flags & NOTIFY_FLAG.UPDATE_SKILL_FAVORITE) != (NOTIFY_FLAG)0L)
		{
			inventories[(int)tab] = null;
			SetDirty(SelectListTarget(tab, showInventoryMode));
			SaveCurrentScrollPosition();
		}
		if ((flags & NOTIFY_FLAG.UPDATE_ITEM_INVENTORY) != (NOTIFY_FLAG)0L)
		{
			inventories[0] = null;
			inventories[1] = null;
			inventories[4] = null;
			SetDirty(SelectListTarget(tab, showInventoryMode));
		}
		if ((flags & NOTIFY_FLAG.UPDATE_EQUIP_INVENTORY) != (NOTIFY_FLAG)0L || (flags & NOTIFY_FLAG.UPDATE_SKILL_CHANGE) != (NOTIFY_FLAG)0L)
		{
			inventories[2] = null;
			inventories[3] = null;
			SetDirty(SelectListTarget(tab, showInventoryMode));
		}
		if ((flags & NOTIFY_FLAG.UPDATE_SKILL_INVENTORY) != (NOTIFY_FLAG)0L)
		{
			inventories[3] = null;
			SetDirty(SelectListTarget(tab, showInventoryMode));
		}
		if ((flags & NOTIFY_FLAG.UPDATE_ABILITY_ITEM_INVENTORY) != (NOTIFY_FLAG)0L)
		{
			inventories[0] = null;
			SetDirty(SelectListTarget(tab, showInventoryMode));
		}
		if ((flags & NOTIFY_FLAG.UPDATE_EQUIP_GROW) != (NOTIFY_FLAG)0L)
		{
			inventories[5] = null;
			SetDirty(SelectListTarget(tab, showInventoryMode));
		}
		base.OnNotify(flags);
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		int j = 0;
		for (int num = uiTab.Length; j < num; j++)
		{
			UIToggle component = GetComponent<UIToggle>(uiTab[j]);
			if ((UnityEngine.Object)component != (UnityEngine.Object)null)
			{
				component.value = (j == (int)tab);
			}
			UIToggledComponents component2 = GetComponent<UIToggledComponents>(uiTab[j]);
			if ((UnityEngine.Object)component2 != (UnityEngine.Object)null)
			{
				UIToggle.current = component;
				component2.Toggle();
			}
		}
		UIToggle.current = null;
		SetToggle(UI.TGL_CHANGE_INVENTORY, true);
		InventoryBase inventory = inventories[(int)tab];
		if (inventory == null)
		{
			switch (tab)
			{
			case TAB_MODE.MATERIAL:
				inventories[(int)tab] = new MaterialInventory(includeMaterialItem, includeLithograph, false, ItemGetType);
				break;
			case TAB_MODE.USE_ITEM:
				inventories[(int)tab] = new UseItemInventory();
				break;
			case TAB_MODE.EQUIP:
				inventories[(int)tab] = new EquipItemInventory(ItemGetType);
				break;
			case TAB_MODE.SKILL:
				inventories[(int)tab] = new SkillItemInventory(SortSettings.SETTINGS_TYPE.STORAGE_SKILL, SKILL_SLOT_TYPE.NONE, false);
				break;
			case TAB_MODE.LAPIS:
				inventories[(int)tab] = new MaterialInventory(false, false, true, null);
				break;
			}
			inventory = inventories[(int)tab];
			inventoryUI = SelectListTarget(tab, showInventoryMode);
			if (isSellMode && (tab == TAB_MODE.EQUIP || tab == TAB_MODE.SKILL) && sellItemData.Count != 0 && inventories[(int)tab] != null && inventories[(int)tab].datas.Length != 0)
			{
				List<SortCompareData> find_data = new List<SortCompareData>();
				sellItemData.ForEach(delegate(SortCompareData sort_data)
				{
					int l = 0;
					for (int num4 = inventories[(int)tab].datas.Length; l < num4; l++)
					{
						if (sort_data.GetUniqID() == inventories[(int)tab].datas[l].GetUniqID())
						{
							return;
						}
					}
					find_data.Add(sort_data);
				});
				int k = 0;
				for (int num2 = inventories[(int)tab].datas.Length; k < num2; k++)
				{
					SortCompareData sortCompareData = inventories[(int)tab].datas[k];
					if (sortCompareData.IsFavorite() && find_data.IndexOf(sortCompareData) == -1)
					{
						find_data.Add(sortCompareData);
					}
				}
				if (find_data.Count > 0)
				{
					find_data.ForEach(delegate(SortCompareData item)
					{
						sellItemData.RemoveAll((SortCompareData _item) => item.GetUniqID() == _item.GetUniqID());
					});
					SetDirty(inventoryUI);
				}
			}
		}
		SetActive(UI.BTN_SORT, true);
		SetLabelText(UI.LBL_SORT, inventory.sortSettings.GetSortLabel());
		SetToggle(UI.TGL_ICON_ASC, inventory.sortSettings.orderTypeAsc);
		bool flag = tab != TAB_MODE.USE_ITEM && tab != TAB_MODE.LAPIS;
		SetActive(UI.BTN_SORT, tab != TAB_MODE.ACCESSORY && flag);
		SetActive(UI.SPR_INVALID_SORT, tab != TAB_MODE.ACCESSORY && !flag);
		SetLabelText(UI.LBL_INVALID_SORT, inventory.sortSettings.GetSortLabel());
		m_generatedIconList.Clear();
		UpdateNewIconInfo();
		bool flag2 = tab != TAB_MODE.SKILL && tab != TAB_MODE.ACCESSORY;
		SetActive(UI.BTN_CHANGE, flag2);
		SetActive(UI.SPR_INVALID_CHANGE, !flag2);
		int sortedItemCount = 0;
		SetDynamicList(inventoryUI, null, inventory.datas.Length, false, delegate(int i)
		{
			SortCompareData sortCompareData3 = inventory.datas[i];
			if (sortCompareData3 == null || !sortCompareData3.IsPriority(inventory.sortSettings.orderTypeAsc))
			{
				return false;
			}
			sortedItemCount++;
			return true;
		}, null, delegate(int i, Transform t, bool is_recycre)
		{
			SortCompareData sortCompareData2 = inventory.datas[i];
			ItemIcon itemIcon = inventory.CreateIcon(new object[4]
			{
				sortCompareData2,
				t,
				i,
				this
			});
			if ((UnityEngine.Object)itemIcon != (UnityEngine.Object)null)
			{
				itemIcon.toggleSelectFrame.onChange.Clear();
				itemIcon.toggleSelectFrame.onChange.Add(new EventDelegate(this, "IconToggleChange"));
				SetLongTouch(itemIcon.transform, "DETAIL", i);
				itemIcon.SetGrayout(IsRequiredIconGrayOut(sortCompareData2));
				itemIcon.SetInitData(sortCompareData2);
				if (!m_generatedIconList.Contains(itemIcon))
				{
					m_generatedIconList.Add(itemIcon);
				}
			}
		});
		if (tab == TAB_MODE.EQUIP || tab == TAB_MODE.SKILL)
		{
			if (isSellMode)
			{
				UpdateSellGoldAndExp();
				SetActive(UI.OBJ_SELL_MODE_ROOT, true);
				SetActive(UI.OBJ_BTN_SELL_MODE, false);
			}
			else
			{
				SetActive(UI.OBJ_BTN_SELL_MODE, MonoBehaviourSingleton<ItemExchangeManager>.I.IsExchangeScene());
				SetActive(UI.OBJ_SELL_MODE_ROOT, false);
			}
		}
		else
		{
			SetActive(UI.OBJ_BTN_SELL_MODE, false);
			SetActive(UI.OBJ_SELL_MODE_ROOT, false);
		}
		int num3 = 0;
		int now_num = 0;
		switch (tab)
		{
		case TAB_MODE.EQUIP:
			num3 = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.maxEquipItem;
			MonoBehaviourSingleton<InventoryManager>.I.ForAllEquipItemInventory(delegate(EquipItemInfo _equip)
			{
				if (_equip != null && _equip.uniqueID != 0L && _equip.tableID != 0)
				{
					now_num++;
				}
			});
			break;
		case TAB_MODE.SKILL:
			num3 = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.maxSkillItem;
			MonoBehaviourSingleton<InventoryManager>.I.ForAllSkillItemInventory(delegate(SkillItemInfo _skill)
			{
				if (_skill != null && _skill.uniqueID != 0L && _skill.tableID != 0)
				{
					now_num++;
				}
			});
			break;
		}
		SetLabelText(UI.LBL_MAX_HAVE_NUM, num3.ToString());
		SetLabelText(UI.LBL_NOW_HAVE_NUM, now_num.ToString());
		m_maxScrollableValue = CalcMaxScrollableValue(sortedItemCount);
		UpdateScrollSettings();
		UpdateAnchors();
	}

	private void Update()
	{
		ObserveItemList();
	}

	protected virtual bool IsRequiredIconGrayOut(SortCompareData _data)
	{
		if (_data.GetNum() == 0)
		{
			return true;
		}
		if (isSellMode && _data.IsFavorite())
		{
			return true;
		}
		if (isSellMode && _data.IsEquipping() && tab == TAB_MODE.EQUIP)
		{
			return true;
		}
		return false;
	}

	private void UpdateSellGoldAndExp()
	{
		int total_gold = 0;
		int num = 0;
		if (sellItemData.Count > 0)
		{
			num = sellItemData.Count;
			sellItemData.ForEach(delegate(SortCompareData sort_data)
			{
				total_gold += sort_data.GetSalePrice();
			});
		}
		SetLabelText(UI.LBL_SELECT_NUM, num.ToString());
		SetLabelText(UI.LBL_MAX_SELECT_NUM, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.SELL_SELECT_MAX.ToString());
		SetLabelText(UI.LBL_TOTAL, total_gold.ToString());
	}

	public override EventData CheckAutoEvent(string event_name, object event_data)
	{
		if (event_name == "TAB_2")
		{
			if (tab == TAB_MODE.EQUIP)
			{
				return new EventData(null, null);
			}
		}
		else if (event_name == "SELECT")
		{
			InventoryBase inventoryBase = inventories[(int)tab];
			ulong uniq_id = (ulong)event_data;
			int num = Array.FindIndex(inventoryBase.datas, (SortCompareData o) => o.GetUniqID() == uniq_id);
			return new EventData(event_name, num);
		}
		return base.CheckAutoEvent(event_name, event_data);
	}

	protected void ChangeTab(TAB_MODE t)
	{
		if (isSellMode && sellItemData.Count > 0 && tab != t)
		{
			tmpTab = t;
			GameSection.ChangeEvent("SELL_CHANGE_TAB", null);
		}
		else
		{
			tmpTab = TAB_MODE.MAX;
			if (tab != t)
			{
				sellItemData.Clear();
				isSellMode = false;
				SetNextTab(t);
				m_lastScrollPosition = Vector3.up * -9999.9f;
				m_isEnableUpdateScrollTopPos = true;
				inventoryUI = SelectListTarget(tab, showInventoryMode);
				SetDirty(inventoryUI);
				RefreshUI();
			}
		}
	}

	protected void OnQuery_ItemStorageChangeTabMultiSell_YES()
	{
		isSellMode = false;
		ChangeTab(tmpTab);
	}

	protected void OnQuery_ItemExchangeChangeTabMultiSell_YES()
	{
		isSellMode = false;
		ChangeTab(tmpTab);
	}

	private void OnQuery_TAB_0()
	{
		ChangeTab(TAB_MODE.MATERIAL);
	}

	private void OnQuery_TAB_1()
	{
		ChangeTab(TAB_MODE.USE_ITEM);
	}

	private void OnQuery_TAB_2()
	{
		ChangeTab(TAB_MODE.EQUIP);
	}

	private void OnQuery_TAB_3()
	{
		ChangeTab(TAB_MODE.SKILL);
	}

	private void OnQuery_TAB_4()
	{
		ChangeTab(TAB_MODE.LAPIS);
	}

	private void OnQuery_SORT()
	{
		if (tab == TAB_MODE.EQUIP || tab == TAB_MODE.SKILL)
		{
			GameSection.ChangeEvent("EQUIP_SORT", null);
		}
		else if (tab == TAB_MODE.ACCESSORY)
		{
			GameSection.ChangeEvent("ACCESSORY_SORT", null);
		}
		GameSection.SetEventData(inventories[(int)tab].sortSettings.Clone());
	}

	private void OnCloseDialog_ItemStorageSort()
	{
		CloseSort();
	}

	private void OnCloseDialog_ItemStorageEquipSort()
	{
		CloseSort();
	}

	protected void CloseSort()
	{
		if (GameSection.GetEventData() is SortSettings && inventories[(int)tab].Sort(GameSection.GetEventData() as SortSettings))
		{
			SetCurrentScrollPositionOnTop();
			SetDirty(UI.GRD_INVENTORY);
			SetDirty(UI.GRD_INVENTORY_SMALL);
			SetDirty(UI.GRD_INVENTORY_EQUIP);
			SetDirty(UI.GRD_INVENTORY_EQUIP_SMALL);
			RefreshUI();
		}
	}

	private void OnQuery_DETAIL()
	{
		ToDetail();
	}

	protected virtual void ToDetail()
	{
		SaveCurrentScrollPosition();
		int num = (int)GameSection.GetEventData();
		if (tab == TAB_MODE.EQUIP)
		{
			GameSection.ChangeEvent("EQUIP_SELECT", new object[2]
			{
				ItemDetailEquip.CURRENT_SECTION.ITEM_STORAGE,
				inventories[(int)tab].datas[num]
			});
		}
		else if (tab == TAB_MODE.SKILL)
		{
			GameSection.ChangeEvent("SKILL_SELECT", new object[2]
			{
				ItemDetailEquip.CURRENT_SECTION.ITEM_STORAGE,
				inventories[(int)tab].datas[num]
			});
		}
		else if (tab == TAB_MODE.USE_ITEM)
		{
			SortCompareData sortCompareData = inventories[(int)tab].datas[num];
			if (IsItemTypeEquipSetExt(sortCompareData))
			{
				GameSection.ChangeEvent("EQUIP_SET_EXT_SELECT", sortCompareData);
			}
			else
			{
				GameSection.ChangeEvent("USE_ITEM_SELECT", sortCompareData);
			}
		}
		else if (tab == TAB_MODE.LAPIS)
		{
			GameSection.ChangeEvent("SELECT", inventories[(int)tab].datas[num]);
		}
		else if (tab == TAB_MODE.ACCESSORY)
		{
			GameSection.ChangeEvent("ACCESSORY_SELECT", new object[2]
			{
				ItemDetailEquip.CURRENT_SECTION.ITEM_STORAGE,
				inventories[(int)tab].datas[num]
			});
		}
		else
		{
			GameSection.ChangeEvent("SELECT", inventories[(int)tab].datas[num]);
		}
	}

	private bool IsItemTypeEquipSetExt(SortCompareData data)
	{
		bool result = false;
		ItemInfo itemInfo = data.GetItemData() as ItemInfo;
		if (itemInfo != null && itemInfo.tableData != null && itemInfo.tableData.type == ITEM_TYPE.EQUIP_SET_EXT)
		{
			result = true;
		}
		return result;
	}

	private void OnQuery_SELECT()
	{
		int num = (int)GameSection.GetEventData();
		if (isSellMode)
		{
			SortCompareData select_data = inventories[(int)tab].datas[num];
			SortCompareData sortCompareData = sellItemData.Find((SortCompareData data) => data.GetUniqID() == select_data.GetUniqID());
			if (sellItemData.Count >= MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.SELL_SELECT_MAX && sortCompareData == null)
			{
				toggleIndex = num;
				GameSection.ChangeEvent("MULTI_SELL_MAX", null);
			}
			else if (!select_data.CanSale())
			{
				if (select_data.IsFavorite())
				{
					GameSection.ChangeEvent("NOT_SELL_FAVORITE", null);
				}
				else
				{
					GameSection.ChangeEvent("NOT_SELL_EQUIPPING", null);
				}
				toggleIndex = num;
			}
			else if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.SKILL_EQUIP) && select_data.GetTableID() == 10000000)
			{
				GameSection.ChangeEvent("NOT_SELL_DEFAULT_WEAPON", null);
			}
			else
			{
				ResetSelectSellIcon();
				if (sortCompareData != null)
				{
					sellItemData.Remove(sortCompareData);
				}
				else
				{
					sellItemData.Add(select_data);
				}
				GameSection.StopEvent();
				UpdateSellGoldAndExp();
				UpdateSelectSellIcon();
			}
		}
		else
		{
			ToDetail();
		}
	}

	private void ResetSelectSellIcon()
	{
		_UpdateSelectSellIcon(true);
	}

	private void UpdateSelectSellIcon()
	{
		_UpdateSelectSellIcon(false);
	}

	private void _UpdateSelectSellIcon(bool reset)
	{
		if (sellItemData != null && sellItemData.Count != 0)
		{
			Transform ui = GetCtrl(inventoryUI);
			int select_index = (!reset) ? 1 : (-1);
			sellItemData.ForEach(delegate(SortCompareData material)
			{
				int num = -1;
				if (inventories[(int)tab] != null)
				{
					int i = 0;
					for (int num2 = inventories[(int)tab].datas.Length; i < num2; i++)
					{
						if (inventories[(int)tab].datas[i].GetUniqID() == material.GetUniqID())
						{
							num = i;
							break;
						}
					}
				}
				if (num != -1)
				{
					Transform child = ui.GetChild(num);
					ItemIconDetail componentInChildren = child.GetComponentInChildren<ItemIconDetail>();
					if ((UnityEngine.Object)componentInChildren != (UnityEngine.Object)null)
					{
						if (tab == TAB_MODE.EQUIP)
						{
							componentInChildren.setupperEquip.SetupSelectNumberSprite(select_index);
						}
						else if (tab == TAB_MODE.SKILL)
						{
							componentInChildren.setupperSkill.SetupSelectNumberSprite(select_index);
						}
					}
					if (!reset)
					{
						select_index++;
					}
				}
			});
		}
	}

	private void OnQuery_SELL_MODE()
	{
		isSellMode = !isSellMode;
		RefreshUI();
	}

	private void OnQuery_SELL_MODE_END()
	{
		sellItemData.Clear();
		isSellMode = !isSellMode;
		RefreshUI();
	}

	protected void OnQuery_SELL()
	{
		if (sellItemData.Count == 0)
		{
			GameSection.ChangeEvent("SELL_NOT_SELECT", null);
		}
		else
		{
			SaveCurrentScrollPosition();
			GameSection.SetEventData(new object[3]
			{
				tab,
				sellItemData,
				confirmTo
			});
		}
	}

	private void OnCloseDialog_ItemStorageSellConfirm()
	{
		OnCloseItemStorageSellConfirm();
	}

	private void OnCloseDialog_ItemStorageSellIncludeRareConfirm()
	{
		OnCloseItemStorageSellConfirm();
	}

	private void OnCloseItemStorageSellConfirm()
	{
		List<SortCompareData> list = GameSection.GetEventData() as List<SortCompareData>;
		if (list == null)
		{
			sellItemData.Clear();
			RefreshUI();
		}
	}

	public void IconToggleChange()
	{
		if (toggleIndex != -1)
		{
			Transform child = GetChild(inventoryUI, toggleIndex);
			child.GetComponentInChildren<UIToggle>().value = false;
			toggleIndex = -1;
		}
	}

	private SHOW_INVENTORY_MODE GetNextContentMode()
	{
		SHOW_INVENTORY_MODE sHOW_INVENTORY_MODE = SHOW_INVENTORY_MODE.MAIN_STATUS;
		TAB_MODE tAB_MODE = tab;
		if (tAB_MODE == TAB_MODE.SKILL)
		{
			return SHOW_INVENTORY_MODE.MAIN_STATUS;
		}
		return (showInventoryMode + 1 != SHOW_INVENTORY_MODE.MAX) ? (showInventoryMode + 1) : SHOW_INVENTORY_MODE.MAIN_STATUS;
	}

	private void OnQuery_CHANGE_INVENTORY()
	{
		SHOW_INVENTORY_MODE sHOW_INVENTORY_MODE = showInventoryMode = GetNextContentMode();
		TAB_MODE tAB_MODE = tab;
		if (tAB_MODE == TAB_MODE.EQUIP || tAB_MODE == TAB_MODE.SKILL)
		{
			SetDirty(UI.GRD_INVENTORY_EQUIP);
			SetDirty(UI.GRD_INVENTORY_EQUIP_SMALL);
			inventoryUI = UI.GRD_INVENTORY_EQUIP;
		}
		else
		{
			SetDirty(UI.GRD_INVENTORY);
			SetDirty(UI.GRD_INVENTORY_SMALL);
			inventoryUI = UI.GRD_INVENTORY;
		}
		inventories[(int)tab] = null;
		RefreshUI();
	}

	private UI SelectListTarget(TAB_MODE tab, SHOW_INVENTORY_MODE show_detail_icon)
	{
		SetActive(UI.SCR_INVENTORY, false);
		SetActive(UI.SCR_INVENTORY_EQUIP, false);
		SetActive(UI.GRD_INVENTORY, false);
		SetActive(UI.GRD_INVENTORY_SMALL, false);
		SetActive(UI.GRD_INVENTORY_EQUIP, false);
		SetActive(UI.GRD_INVENTORY_EQUIP_SMALL, false);
		SetActive(UI.SPR_SCR_BAR, false);
		SetActive(UI.SPR_EQUIP_SCR_BAR, false);
		bool flag = false;
		if (MonoBehaviourSingleton<ItemExchangeManager>.I.IsExchangeScene() && (tab == TAB_MODE.EQUIP || tab == TAB_MODE.SKILL))
		{
			flag = true;
		}
		if (flag)
		{
			SetActive(UI.SCR_INVENTORY_EQUIP, true);
			SetActive(UI.SPR_EQUIP_SCR_BAR, true);
			SetActive(UI.GRD_INVENTORY_EQUIP, true);
			return UI.GRD_INVENTORY_EQUIP;
		}
		SetActive(UI.SCR_INVENTORY, true);
		SetActive(UI.SPR_SCR_BAR, true);
		SetActive(UI.GRD_INVENTORY, true);
		return UI.GRD_INVENTORY;
	}

	private void InitializeCaption()
	{
		Transform ctrl = GetCtrl(UI.OBJ_CAPTION_3);
		string text = base.sectionData.GetText("CAPTION");
		SetLabelText(ctrl, UI.LBL_CAPTION, text);
		UITweenCtrl component = ctrl.gameObject.GetComponent<UITweenCtrl>();
		if ((UnityEngine.Object)component != (UnityEngine.Object)null)
		{
			component.Reset();
			int i = 0;
			for (int num = component.tweens.Length; i < num; i++)
			{
				component.tweens[i].ResetToBeginning();
			}
			component.Play(true, null);
		}
	}

	protected float CalcMaxScrollableValue(int allItemCount)
	{
		if (allItemCount < 1)
		{
			return 0f;
		}
		Transform scrollViewController = GetScrollViewController(tab);
		if ((UnityEngine.Object)scrollViewController == (UnityEngine.Object)null)
		{
			return 0f;
		}
		UIPanel component = scrollViewController.GetComponent<UIPanel>();
		if ((UnityEngine.Object)component == (UnityEngine.Object)null)
		{
			return 0f;
		}
		float num = 0f;
		int num2 = 0;
		UIGrid[] componentsInChildren = scrollViewController.GetComponentsInChildren<UIGrid>(true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (componentsInChildren[i].gameObject.activeInHierarchy)
			{
				num = componentsInChildren[i].cellHeight;
				num2 = componentsInChildren[i].maxPerLine;
				break;
			}
		}
		if (num <= 0f || num2 <= 0)
		{
			return 0f;
		}
		int num3 = Mathf.CeilToInt((float)allItemCount / (float)num2);
		Vector4 baseClipRegion = component.baseClipRegion;
		int num4 = Mathf.FloorToInt(baseClipRegion.w / num);
		if (num3 <= num4)
		{
			return 0f;
		}
		return num * (float)(num3 - num4);
	}

	protected void UpdateScrollSettings()
	{
		Transform scrollViewController = GetScrollViewController(tab);
		if (!((UnityEngine.Object)scrollViewController == (UnityEngine.Object)null))
		{
			UpdateCurrentScrollTopPos(scrollViewController.localPosition);
			float y = m_lastScrollPosition.y;
			Vector3 currentScrollTopPos = CurrentScrollTopPos;
			if (y > currentScrollTopPos.y)
			{
				float maxScrollableValue = m_maxScrollableValue;
				Vector3 currentScrollTopPos2 = CurrentScrollTopPos;
				float num = maxScrollableValue + currentScrollTopPos2.y;
				Vector3 localPosition = scrollViewController.localPosition;
				float y2 = localPosition.y;
				UI scrollViewUI = GetScrollViewUI(tab);
				float num2 = m_lastScrollPosition.y - y2;
				if (num2 > m_maxScrollableValue)
				{
					num2 = m_maxScrollableValue;
				}
				if (y2 >= num || y2 + num2 >= num)
				{
					num2 = num - y2;
				}
				MoveRelativeScrollView(scrollViewUI, Vector3.up * num2);
			}
		}
	}

	protected Transform GetScrollViewController(TAB_MODE _tab)
	{
		Transform result = null;
		switch (_tab)
		{
		case TAB_MODE.MATERIAL:
		case TAB_MODE.LAPIS:
		case TAB_MODE.ACCESSORY:
			result = GetCtrl(UI.SCR_INVENTORY);
			break;
		case TAB_MODE.EQUIP:
		case TAB_MODE.SKILL:
			result = GetCtrl(UI.SCR_INVENTORY_EQUIP);
			break;
		}
		return result;
	}

	private UI GetScrollViewUI(TAB_MODE _tab)
	{
		UI result = UI.SCR_INVENTORY;
		switch (_tab)
		{
		case TAB_MODE.MATERIAL:
		case TAB_MODE.LAPIS:
		case TAB_MODE.ACCESSORY:
			result = UI.SCR_INVENTORY;
			break;
		case TAB_MODE.EQUIP:
		case TAB_MODE.SKILL:
			result = UI.SCR_INVENTORY_EQUIP;
			break;
		}
		return result;
	}

	protected void SaveCurrentScrollPosition()
	{
		Transform scrollViewController = GetScrollViewController(tab);
		if (!((UnityEngine.Object)scrollViewController == (UnityEngine.Object)null))
		{
			m_lastScrollPosition = scrollViewController.localPosition;
		}
	}

	protected void SetCurrentScrollPositionOnTop()
	{
		if (!(CurrentScrollTopPos == Vector3.zero))
		{
			m_lastScrollPosition = CurrentScrollTopPos;
		}
	}
}
