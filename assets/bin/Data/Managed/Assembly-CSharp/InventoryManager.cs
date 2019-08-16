using Network;
using System;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviourSingleton<InventoryManager>
{
	public enum INVENTORY_TYPE
	{
		NONE,
		ONE_HAND_SWORD,
		TWO_HAND_SWORD,
		SPEAR,
		PAIR_SWORDS,
		ARROW,
		ARMOR,
		HELM,
		ARM,
		LEG,
		ALL_WEAPON,
		ALL_ARMOR,
		ALL_EQUIP,
		ALL_EVOLVE_EQUIP,
		VISUAL_ARMOR,
		VISUAL_HELM,
		VISUAL_ARM,
		VISUAL_LEG
	}

	private class InGameTempItem
	{
		public uint itemId;

		public int num;
	}

	private class EquipItemFilter
	{
		public uint itemId;

		public bool isCreateble;

		public bool isPay;

		public bool isObtained;
	}

	private delegate bool CompareInventoryType(EQUIPMENT_TYPE equipment_type);

	private List<InGameTempItem> inGameTempItemInventory;

	private Dictionary<uint, EquipItemFilter> equipItemFilterList;

	private bool firstSetList = true;

	private List<ulong> removeNewFragAbilityItemIds = new List<ulong>();

	public InventoryList<EquipItemInfo, EquipItem> equipItemInventory
	{
		get;
		private set;
	}

	public InventoryList<SkillItemInfo, SkillItem> skillItemInventory
	{
		get;
		private set;
	}

	public InventoryList<SkillItemInfo, SkillItem> skillMaterialInventory
	{
		get;
		private set;
	}

	public InventoryList<ItemInfo, Item> itemInventory
	{
		get;
		private set;
	}

	public InventoryList<AbilityItemInfo, AbilityItem> abilityItemInventory
	{
		get;
		private set;
	}

	public InventoryList<QuestItemInfo, QuestItem> questItemInventory
	{
		get;
		private set;
	}

	public InventoryList<AccessoryInfo, Accessory> accessoryInventory
	{
		get;
		private set;
	}

	public INVENTORY_TYPE changeInventoryType
	{
		get;
		set;
	}

	private InventoryManager()
	{
		equipItemInventory = new InventoryList<EquipItemInfo, EquipItem>();
		skillItemInventory = new InventoryList<SkillItemInfo, SkillItem>();
		skillMaterialInventory = new InventoryList<SkillItemInfo, SkillItem>();
		itemInventory = new InventoryList<ItemInfo, Item>();
		abilityItemInventory = new InventoryList<AbilityItemInfo, AbilityItem>();
		inGameTempItemInventory = new List<InGameTempItem>();
		questItemInventory = new InventoryList<QuestItemInfo, QuestItem>();
		accessoryInventory = new InventoryList<AccessoryInfo, Accessory>();
		equipItemFilterList = new Dictionary<uint, EquipItemFilter>();
	}

	public bool IsWeaponInventoryType(INVENTORY_TYPE type)
	{
		bool result = false;
		switch (type)
		{
		case INVENTORY_TYPE.ONE_HAND_SWORD:
		case INVENTORY_TYPE.TWO_HAND_SWORD:
		case INVENTORY_TYPE.SPEAR:
		case INVENTORY_TYPE.PAIR_SWORDS:
		case INVENTORY_TYPE.ARROW:
		case INVENTORY_TYPE.ALL_WEAPON:
		case INVENTORY_TYPE.ALL_EQUIP:
		case INVENTORY_TYPE.ALL_EVOLVE_EQUIP:
			result = true;
			break;
		}
		return result;
	}

	public List<EquipItemInfo> GetWeaponInventory()
	{
		return GetItemInventory((EQUIPMENT_TYPE type) => type < EQUIPMENT_TYPE.ARMOR);
	}

	public List<EquipItemInfo> GetOneHandSwordInventory()
	{
		return GetItemInventory((EQUIPMENT_TYPE type) => type == EQUIPMENT_TYPE.ONE_HAND_SWORD);
	}

	public List<EquipItemInfo> GetTwoHandSwordInventory()
	{
		return GetItemInventory((EQUIPMENT_TYPE type) => type == EQUIPMENT_TYPE.TWO_HAND_SWORD);
	}

	public List<EquipItemInfo> GetPairSwordInventory()
	{
		return GetItemInventory((EQUIPMENT_TYPE type) => type == EQUIPMENT_TYPE.PAIR_SWORDS);
	}

	public List<EquipItemInfo> GetSpearInventory()
	{
		return GetItemInventory((EQUIPMENT_TYPE type) => type == EQUIPMENT_TYPE.SPEAR);
	}

	public List<EquipItemInfo> GetArrowInventory()
	{
		return GetItemInventory((EQUIPMENT_TYPE type) => type == EQUIPMENT_TYPE.ARROW);
	}

	public List<EquipItemInfo> GetArmorInventory()
	{
		return GetItemInventory((EQUIPMENT_TYPE type) => type == EQUIPMENT_TYPE.ARMOR);
	}

	public List<EquipItemInfo> GetHelmInventory()
	{
		return GetItemInventory((EQUIPMENT_TYPE type) => type == EQUIPMENT_TYPE.HELM);
	}

	public List<EquipItemInfo> GetArmInventory()
	{
		return GetItemInventory((EQUIPMENT_TYPE type) => type == EQUIPMENT_TYPE.ARM);
	}

	public List<EquipItemInfo> GetLegInventory()
	{
		return GetItemInventory((EQUIPMENT_TYPE type) => type == EQUIPMENT_TYPE.LEG);
	}

	public List<EquipItemInfo> GetEvolveItemInventory()
	{
		List<EquipItemInfo> list = new List<EquipItemInfo>();
		ForAllEquipItemInventory(delegate(EquipItemInfo data)
		{
			if (data != null && data.tableData.IsEvolve())
			{
				list.Add(data);
			}
		});
		return list;
	}

	public int GetEquipItemNum(uint equip_id)
	{
		int num = 0;
		for (LinkedListNode<EquipItemInfo> linkedListNode = equipItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			if (linkedListNode.Value.tableID == equip_id)
			{
				num++;
			}
		}
		return num;
	}

	public int GetEquipItemNumWithShadow(uint equip_id)
	{
		int num = 0;
		for (LinkedListNode<EquipItemInfo> linkedListNode = equipItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			if (linkedListNode.Value.tableID == equip_id || linkedListNode.Value.tableData.shadowEvolveEquipItemId == equip_id)
			{
				num++;
			}
		}
		return num;
	}

	public List<EquipItemInfo> GetVisualArmorInventory()
	{
		return GetItemInventory((EQUIPMENT_TYPE type) => type == EQUIPMENT_TYPE.ARMOR || type == EQUIPMENT_TYPE.VISUAL_ARMOR);
	}

	public List<EquipItemInfo> GetVisualHelmInventory()
	{
		return GetItemInventory((EQUIPMENT_TYPE type) => type == EQUIPMENT_TYPE.HELM || type == EQUIPMENT_TYPE.VISUAL_HELM);
	}

	public List<EquipItemInfo> GetVisualArmInventory()
	{
		return GetItemInventory((EQUIPMENT_TYPE type) => type == EQUIPMENT_TYPE.ARM || type == EQUIPMENT_TYPE.VISUAL_ARM);
	}

	public List<EquipItemInfo> GetVisualLegInventory()
	{
		return GetItemInventory((EQUIPMENT_TYPE type) => type == EQUIPMENT_TYPE.LEG || type == EQUIPMENT_TYPE.VISUAL_LEG);
	}

	private List<EquipItemInfo> GetItemInventory(CompareInventoryType compare)
	{
		List<EquipItemInfo> list = new List<EquipItemInfo>();
		ForAllEquipItemInventory(delegate(EquipItemInfo data)
		{
			if (data != null && compare(data.tableData.type))
			{
				list.Add(data);
			}
		});
		return list;
	}

	public void ForAllEquipItemInventory(Action<EquipItemInfo> callback)
	{
		for (LinkedListNode<EquipItemInfo> linkedListNode = equipItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			callback(linkedListNode.Value);
		}
	}

	public void ForAllSkillItemInventory(Action<SkillItemInfo> callback)
	{
		for (LinkedListNode<SkillItemInfo> linkedListNode = skillItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			callback(linkedListNode.Value);
		}
	}

	public EquipItemInfo GetEquipItem(ulong uniq_id)
	{
		for (LinkedListNode<EquipItemInfo> linkedListNode = equipItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			if (linkedListNode.Value.uniqueID == uniq_id)
			{
				return linkedListNode.Value;
			}
		}
		return null;
	}

	public SkillItemInfo GetSkillItem(ulong uniq_id)
	{
		for (LinkedListNode<SkillItemInfo> linkedListNode = skillItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			if (linkedListNode.Value.uniqueID == uniq_id)
			{
				return linkedListNode.Value;
			}
		}
		return null;
	}

	public AbilityItemInfo GetAbilityItem(ulong uniq_id)
	{
		for (LinkedListNode<AbilityItemInfo> linkedListNode = abilityItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			if (linkedListNode.Value.uniqueID == uniq_id)
			{
				return linkedListNode.Value;
			}
		}
		return null;
	}

	public AccessoryInfo GetAccessory(ulong uniq_id)
	{
		for (LinkedListNode<AccessoryInfo> linkedListNode = accessoryInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			if (linkedListNode.Value.uniqueID == uniq_id)
			{
				return linkedListNode.Value;
			}
		}
		return null;
	}

	public EquipItemInfo[] GetEquipInventoryClone()
	{
		if (changeInventoryType == INVENTORY_TYPE.NONE)
		{
			return null;
		}
		List<EquipItemInfo> list = null;
		switch (changeInventoryType)
		{
		case INVENTORY_TYPE.ALL_EQUIP:
			list = GetWeaponInventory();
			list.AddRange(GetArmorInventory());
			list.AddRange(GetHelmInventory());
			list.AddRange(GetArmInventory());
			list.AddRange(GetLegInventory());
			break;
		case INVENTORY_TYPE.ALL_WEAPON:
			list = GetWeaponInventory();
			break;
		case INVENTORY_TYPE.ARMOR:
			list = GetArmorInventory();
			break;
		case INVENTORY_TYPE.HELM:
			list = GetHelmInventory();
			break;
		case INVENTORY_TYPE.ARM:
			list = GetArmInventory();
			break;
		case INVENTORY_TYPE.LEG:
			list = GetLegInventory();
			break;
		case INVENTORY_TYPE.ONE_HAND_SWORD:
			list = GetOneHandSwordInventory();
			break;
		case INVENTORY_TYPE.TWO_HAND_SWORD:
			list = GetTwoHandSwordInventory();
			break;
		case INVENTORY_TYPE.PAIR_SWORDS:
			list = GetPairSwordInventory();
			break;
		case INVENTORY_TYPE.SPEAR:
			list = GetSpearInventory();
			break;
		case INVENTORY_TYPE.ARROW:
			list = GetArrowInventory();
			break;
		case INVENTORY_TYPE.ALL_EVOLVE_EQUIP:
			list = GetEvolveItemInventory();
			break;
		}
		return list.ToArray();
	}

	public SkillItemInfo[] GetSkillInventoryClone()
	{
		SkillItemInfo[] array = new SkillItemInfo[skillItemInventory.GetCount()];
		int num = 0;
		for (LinkedListNode<SkillItemInfo> linkedListNode = skillItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			array[num++] = linkedListNode.Value;
		}
		return array;
	}

	public void ForAllQuestInvetory(Action<QuestItemInfo> callback)
	{
		if (callback != null)
		{
			for (LinkedListNode<QuestItemInfo> linkedListNode = questItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				callback(linkedListNode.Value);
			}
		}
	}

	public bool IsHaveingMaterial(NeedMaterial[] needMaterial)
	{
		if (needMaterial == null)
		{
			return false;
		}
		List<NeedMaterial> list = new List<NeedMaterial>(needMaterial);
		LinkedListNode<ItemInfo> node;
		for (node = itemInventory.GetFirstNode(); node != null; node = node.Next)
		{
			bool not_enough = false;
			NeedMaterial find_material = null;
			list.ForEach(delegate(NeedMaterial material)
			{
				if (!not_enough && material.itemID == node.Value.tableID)
				{
					if (material.num > node.Value.num)
					{
						not_enough = true;
					}
					find_material = material;
				}
			});
			if (not_enough)
			{
				return false;
			}
			if (find_material != null)
			{
				list.Remove(find_material);
			}
		}
		if (list.Count != 0)
		{
			return false;
		}
		return true;
	}

	public bool IsHaveingEquip(NeedEquip[] needEquip)
	{
		if (needEquip == null)
		{
			return true;
		}
		List<NeedEquip> list = new List<NeedEquip>(NeedEquip.DivideNeedEquip(needEquip));
		for (LinkedListNode<EquipItemInfo> linkedListNode = equipItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			NeedEquip needEquip2 = null;
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].equipItemID == linkedListNode.Value.tableID && list[i].num == 1 && list[i].needLv <= linkedListNode.Value.level)
				{
					needEquip2 = list[i];
					break;
				}
			}
			if (needEquip2 != null)
			{
				list.Remove(needEquip2);
			}
			if (list.Count == 0)
			{
				return true;
			}
		}
		if (list.Count != 0)
		{
			return false;
		}
		return true;
	}

	public bool IsSetEquipMaterial(ulong[] uniqIdList)
	{
		if (uniqIdList == null)
		{
			return false;
		}
		for (int i = 0; i < uniqIdList.Length; i++)
		{
			LinkedListNode<EquipItemInfo> linkedListNode = equipItemInventory.GetFirstNode();
			bool flag = false;
			while (linkedListNode != null)
			{
				if (uniqIdList[i] == linkedListNode.Value.uniqueID)
				{
					flag = true;
					break;
				}
				linkedListNode = linkedListNode.Next;
			}
			if (!flag)
			{
				return false;
			}
		}
		return true;
	}

	public bool IsHaveingKeyMaterial(LOGICAL_ORDER_TYPE needKeyOrder, NeedMaterial[] needMaterial)
	{
		List<NeedMaterial> list = new List<NeedMaterial>();
		int i = 0;
		for (int num = needMaterial.Length; i < num; i++)
		{
			if (needMaterial[i].isKey)
			{
				list.Add(needMaterial[i]);
			}
		}
		int count = list.Count;
		if (count <= 0)
		{
			return true;
		}
		LinkedListNode<ItemInfo> node;
		for (node = itemInventory.GetFirstNode(); node != null; node = node.Next)
		{
			NeedMaterial find_material = null;
			list.ForEach(delegate(NeedMaterial material)
			{
				if (material.itemID == node.Value.tableID)
				{
					find_material = material;
				}
			});
			if (find_material != null)
			{
				list.Remove(find_material);
			}
		}
		switch (needKeyOrder)
		{
		case LOGICAL_ORDER_TYPE.OR:
			if (list.Count == count)
			{
				return false;
			}
			break;
		case LOGICAL_ORDER_TYPE.AND:
			if (list.Count != 0)
			{
				return false;
			}
			break;
		}
		return true;
	}

	public int IsPay(EquipItemInfo equipData)
	{
		bool flag = equipData.tableData.getType == GET_TYPE.PAY;
		int num = 0;
		if (flag)
		{
			return num | 1;
		}
		return num | 2;
	}

	public int IsHaveingMaterialAndPayAndObtained(SmithCreateItemInfo createData)
	{
		bool flag = false;
		bool flag2 = true;
		bool flag3 = false;
		int num = 0;
		bool flag4 = equipItemFilterList.ContainsKey(createData.smithCreateTableData.equipItemID);
		if (flag4)
		{
			flag2 = equipItemFilterList[createData.smithCreateTableData.equipItemID].isCreateble;
			flag = equipItemFilterList[createData.smithCreateTableData.equipItemID].isPay;
			flag3 = equipItemFilterList[createData.smithCreateTableData.equipItemID].isObtained;
		}
		else
		{
			NeedMaterial[] needMaterial = createData.smithCreateTableData.needMaterial;
			if (createData.equipTableData.getType == GET_TYPE.PAY)
			{
				flag = true;
			}
			flag3 = MonoBehaviourSingleton<AchievementManager>.I.CheckEquipItemCollection(createData.equipTableData);
			List<NeedMaterial> list = new List<NeedMaterial>(needMaterial);
			int count = list.Count;
			if (count > 0)
			{
				LinkedListNode<ItemInfo> node;
				for (node = itemInventory.GetFirstNode(); node != null; node = node.Next)
				{
					NeedMaterial find_enough_material = null;
					list.ForEach(delegate(NeedMaterial material)
					{
						if (material.itemID == node.Value.tableID && material.num <= node.Value.num)
						{
							find_enough_material = material;
						}
					});
					if (find_enough_material != null)
					{
						list.Remove(find_enough_material);
					}
				}
				if (list.Count != 0)
				{
					flag2 = false;
				}
			}
		}
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money < (int)createData.smithCreateTableData.needMoney)
		{
			flag2 = false;
		}
		if (!flag4)
		{
			EquipItemFilter equipItemFilter = new EquipItemFilter();
			equipItemFilter.itemId = createData.smithCreateTableData.equipItemID;
			equipItemFilter.isCreateble = flag2;
			equipItemFilter.isPay = flag;
			equipItemFilter.isObtained = flag3;
			equipItemFilterList[equipItemFilter.itemId] = equipItemFilter;
		}
		num = ((!flag2) ? (num | 8) : (num | 4));
		num = ((!flag) ? (num | 2) : (num | 1));
		if (flag3)
		{
			return num | 0x10;
		}
		return num | 0x20;
	}

	public bool IsHaveingItem(uint item_id)
	{
		for (LinkedListNode<ItemInfo> linkedListNode = itemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			if (linkedListNode.Value.tableID == item_id && linkedListNode.Value.num > 0)
			{
				return true;
			}
		}
		return false;
	}

	public int GetHaveingItemNum(uint item_id)
	{
		int inGameTempItemNum = GetInGameTempItemNum(item_id);
		for (LinkedListNode<ItemInfo> linkedListNode = itemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			if (linkedListNode.Value.tableID == item_id && linkedListNode.Value.GetNum() > 0)
			{
				if (item_id == 1200000)
				{
					return MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((ItemInfo x) => x.tableData.type == ITEM_TYPE.TICKET);
				}
				return linkedListNode.Value.GetNum() + inGameTempItemNum;
			}
		}
		return inGameTempItemNum;
	}

	protected int GetInGameTempItemNum(uint item_id)
	{
		int i = 0;
		for (int count = inGameTempItemInventory.Count; i < count; i++)
		{
			if (inGameTempItemInventory[i].itemId == item_id)
			{
				return inGameTempItemInventory[i].num;
			}
		}
		return 0;
	}

	public void AddInGameTempItem(uint item_id, int num)
	{
		int i = 0;
		for (int count = inGameTempItemInventory.Count; i < count; i++)
		{
			if (inGameTempItemInventory[i].itemId == item_id)
			{
				inGameTempItemInventory[i].num += num;
				return;
			}
		}
		InGameTempItem inGameTempItem = new InGameTempItem();
		inGameTempItem.itemId = item_id;
		inGameTempItem.num = num;
		inGameTempItemInventory.Add(inGameTempItem);
	}

	public void ForAllItemInventory(Action<ItemInfo> callback)
	{
		for (LinkedListNode<ItemInfo> linkedListNode = itemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			if (linkedListNode != null && linkedListNode.Value != null)
			{
				if (linkedListNode.Value.tableData != null)
				{
					callback(linkedListNode.Value);
				}
				else
				{
					Log.Error("SVに存在するが、CLには存在しないアイテム tableID = " + linkedListNode.Value.tableID);
				}
			}
		}
	}

	public List<ItemInfo> GetItemList(Predicate<ItemInfo> match, int listNum = 0)
	{
		List<ItemInfo> list = new List<ItemInfo>();
		int num = 0;
		for (LinkedListNode<ItemInfo> linkedListNode = itemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			ItemInfo value = linkedListNode.Value;
			if (match(value))
			{
				list.Add(value);
				if (listNum > 0)
				{
					num++;
					if (listNum <= num)
					{
						return list;
					}
				}
			}
		}
		return list;
	}

	public int GetItemNum(Predicate<ItemInfo> match, int listNum = 0, bool isItemNum = false)
	{
		int num = 0;
		List<ItemInfo> itemList = GetItemList(match, listNum);
		foreach (ItemInfo item in itemList)
		{
			num = ((!isItemNum) ? (num + item.GetNum()) : (num + item.num));
		}
		return num;
	}

	public QuestItemInfo GetQuestItem(uint quest_id)
	{
		if (quest_id == 0)
		{
			return null;
		}
		QuestItemInfo result = null;
		for (LinkedListNode<QuestItemInfo> linkedListNode = questItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			if (linkedListNode.Value.infoData.questData.tableData.questID == quest_id && linkedListNode.Value.infoData.questData.num > 0)
			{
				result = linkedListNode.Value;
				break;
			}
		}
		return result;
	}

	public bool IsNewItem(ITEM_ICON_TYPE type, ulong uniq_id)
	{
		switch (type)
		{
		case ITEM_ICON_TYPE.NONE:
			return false;
		case ITEM_ICON_TYPE.ITEM:
			for (LinkedListNode<ItemInfo> linkedListNode2 = itemInventory.GetFirstNode(); linkedListNode2 != null; linkedListNode2 = linkedListNode2.Next)
			{
				if (linkedListNode2.Value.uniqueID == uniq_id)
				{
					if (linkedListNode2.Value.num > 0)
					{
						return GameSaveData.instance.IsNewItem(type, uniq_id);
					}
					break;
				}
			}
			break;
		case ITEM_ICON_TYPE.SKILL_ATTACK:
		case ITEM_ICON_TYPE.SKILL_SUPPORT:
		case ITEM_ICON_TYPE.SKILL_HEAL:
		case ITEM_ICON_TYPE.SKILL_PASSIVE:
		case ITEM_ICON_TYPE.SKILL_GROW:
			for (LinkedListNode<SkillItemInfo> linkedListNode4 = skillItemInventory.GetFirstNode(); linkedListNode4 != null; linkedListNode4 = linkedListNode4.Next)
			{
				if (linkedListNode4.Value.uniqueID == uniq_id)
				{
					return GameSaveData.instance.IsNewItem(type, uniq_id);
				}
			}
			break;
		default:
			for (LinkedListNode<EquipItemInfo> linkedListNode6 = equipItemInventory.GetFirstNode(); linkedListNode6 != null; linkedListNode6 = linkedListNode6.Next)
			{
				if (linkedListNode6.Value.uniqueID == uniq_id)
				{
					return GameSaveData.instance.IsNewItem(type, uniq_id);
				}
			}
			break;
		case ITEM_ICON_TYPE.QUEST_ITEM:
			for (LinkedListNode<QuestItemInfo> linkedListNode5 = questItemInventory.GetFirstNode(); linkedListNode5 != null; linkedListNode5 = linkedListNode5.Next)
			{
				if (linkedListNode5.Value.uniqueID == uniq_id)
				{
					if (linkedListNode5.Value.infoData.questData.num > 0)
					{
						return GameSaveData.instance.IsNewItem(type, uniq_id);
					}
					break;
				}
			}
			break;
		case ITEM_ICON_TYPE.ABILITY_ITEM:
			for (LinkedListNode<AbilityItemInfo> linkedListNode3 = abilityItemInventory.GetFirstNode(); linkedListNode3 != null; linkedListNode3 = linkedListNode3.Next)
			{
				if (linkedListNode3.Value.uniqueID == uniq_id)
				{
					if (linkedListNode3.Value.uniqueID == uniq_id)
					{
						return GameSaveData.instance.IsNewItem(type, uniq_id);
					}
					break;
				}
			}
			break;
		case ITEM_ICON_TYPE.ACCESSORY:
			for (LinkedListNode<AccessoryInfo> linkedListNode = accessoryInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				if (linkedListNode.Value.uniqueID == uniq_id)
				{
					if (linkedListNode.Value.uniqueID == uniq_id)
					{
						return GameSaveData.instance.IsNewItem(type, uniq_id);
					}
					break;
				}
			}
			break;
		}
		return false;
	}

	public bool RemoveNewIcon(ITEM_ICON_TYPE type, string str_uniq_id, int update_num)
	{
		if (type == ITEM_ICON_TYPE.NONE)
		{
			return false;
		}
		ulong num = ulong.Parse(str_uniq_id);
		switch (type)
		{
		case ITEM_ICON_TYPE.ITEM:
			for (LinkedListNode<ItemInfo> linkedListNode4 = itemInventory.GetFirstNode(); linkedListNode4 != null; linkedListNode4 = linkedListNode4.Next)
			{
				if (linkedListNode4.Value.uniqueID == num)
				{
					if (linkedListNode4.Value.num > update_num)
					{
						return GameSaveData.instance.RemoveNewIcon(type, num);
					}
					break;
				}
			}
			break;
		case ITEM_ICON_TYPE.SKILL_ATTACK:
		case ITEM_ICON_TYPE.SKILL_SUPPORT:
		case ITEM_ICON_TYPE.SKILL_HEAL:
		case ITEM_ICON_TYPE.SKILL_PASSIVE:
		case ITEM_ICON_TYPE.SKILL_GROW:
			for (LinkedListNode<SkillItemInfo> linkedListNode6 = skillItemInventory.GetFirstNode(); linkedListNode6 != null; linkedListNode6 = linkedListNode6.Next)
			{
				if (linkedListNode6.Value.uniqueID == num)
				{
					return GameSaveData.instance.RemoveNewIcon(type, num);
				}
			}
			break;
		case ITEM_ICON_TYPE.QUEST_ITEM:
			for (LinkedListNode<QuestItemInfo> linkedListNode2 = questItemInventory.GetFirstNode(); linkedListNode2 != null; linkedListNode2 = linkedListNode2.Next)
			{
				if (linkedListNode2.Value.uniqueID == num)
				{
					if (linkedListNode2.Value.infoData.questData.num > update_num)
					{
						return GameSaveData.instance.RemoveNewIcon(type, num);
					}
					break;
				}
			}
			break;
		case ITEM_ICON_TYPE.ABILITY_ITEM:
			for (LinkedListNode<AbilityItemInfo> linkedListNode5 = abilityItemInventory.GetFirstNode(); linkedListNode5 != null; linkedListNode5 = linkedListNode5.Next)
			{
				if (linkedListNode5.Value.uniqueID == num)
				{
					return GameSaveData.instance.RemoveNewIcon(type, num);
				}
			}
			break;
		case ITEM_ICON_TYPE.ACCESSORY:
			for (LinkedListNode<AccessoryInfo> linkedListNode3 = accessoryInventory.GetFirstNode(); linkedListNode3 != null; linkedListNode3 = linkedListNode3.Next)
			{
				if (linkedListNode3.Value.uniqueID == num)
				{
					return GameSaveData.instance.RemoveNewIcon(type, num);
				}
			}
			break;
		default:
			for (LinkedListNode<EquipItemInfo> linkedListNode = equipItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				if (linkedListNode.Value.uniqueID == num)
				{
					return GameSaveData.instance.RemoveNewIcon(type, num);
				}
			}
			break;
		}
		return false;
	}

	public void SetList()
	{
		if (firstSetList)
		{
			firstSetList = false;
			OnceInventoryModel.Param inventory = MonoBehaviourSingleton<OnceManager>.I.result.inventory;
			equipItemInventory = EquipItemInfo.CreateList(inventory.equipItem);
			MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_INVENTORY);
			skillItemInventory = SkillItemInfo.CreateList(inventory.skillItem);
			MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SKILL_INVENTORY);
			skillMaterialInventory = SkillItemInfo.CreateListFromItem(inventory.item);
			abilityItemInventory = AbilityItemInfo.CreateList(inventory.abilityItem);
			accessoryInventory = AccessoryInfo.CreateList(inventory.accessory);
			inGameTempItemInventory.Clear();
			equipItemFilterList.Clear();
			itemInventory = ItemInfo.CreateList(inventory.item);
			SetExpiredAtList(inventory.expiredItem);
			MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_ITEM_INVENTORY);
			questItemInventory = QuestItemInfo.CreateList(inventory.questItem);
			MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_QUEST_ITEM_INVENTORY);
			MonoBehaviourSingleton<QuestManager>.I.needRequestOrderQuestList = true;
		}
	}

	public void SetExpiredAtList(List<ExpiredItem> list)
	{
		foreach (ExpiredItem expiredItem in list)
		{
			List<ItemInfo> itemList = GetItemList((ItemInfo x) => x.tableID == expiredItem.itemId, 1);
			foreach (ItemInfo item in itemList)
			{
				if (item.expiredAtItem == null)
				{
					item.expiredAtItem = new List<ExpiredItem>();
				}
				item.expiredAtItem.Add(expiredItem);
			}
		}
	}

	public void SendInventoryUseItem(string uid, Action<bool> call_back)
	{
		InventoryUseItemModel.RequestSendForm requestSendForm = new InventoryUseItemModel.RequestSendForm();
		requestSendForm.uid = uid;
		Protocol.Send(InventoryUseItemModel.URL, requestSendForm, delegate(InventoryUseItemModel ret)
		{
			bool obj = ErrorCodeChecker.IsSuccess(ret.Error);
			call_back(obj);
		}, string.Empty);
	}

	public void SendInventoryEquipSetExt(string uid, Action<bool> call_back)
	{
		InventoryUseItemModel.RequestSendForm requestSendForm = new InventoryUseItemModel.RequestSendForm();
		requestSendForm.uid = uid;
		Protocol.Send(InventoryEquipSetExtModel.URL, requestSendForm, delegate(InventoryEquipSetExtModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
			}
			call_back(obj);
		}, string.Empty);
	}

	public void SendInventoryAutoItem(string uid, Action<bool> call_back)
	{
		InventoryAutoItemModel.RequestSendForm requestSendForm = new InventoryAutoItemModel.RequestSendForm();
		requestSendForm.uid = uid;
		Protocol.Send(InventoryAutoItemModel.URL, requestSendForm, delegate(InventoryAutoItemModel ret)
		{
			bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
			if (flag && FieldManager.IsValidInGame() && MonoBehaviourSingleton<CoopNetworkManager>.IsValid() && MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
			{
				MonoBehaviourSingleton<UIPlayerStatus>.I.autoBattleButton.OnUseItem(ret.result.timeLeft);
			}
			call_back(flag);
		}, string.Empty);
	}

	public void SendInventoryEquipSetCopy(StatusEquipSetCopyModel.RequestSendForm send_form, Action<bool> call_back)
	{
		Protocol.Send(StatusEquipSetCopyModel.URL, send_form, delegate(StatusEquipSetCopyModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
			}
			call_back(obj);
		}, string.Empty);
	}

	public void SendInventoryExtend(Action<bool> call_back)
	{
		InventoryExtendModel.RequestSendForm requestSendForm = new InventoryExtendModel.RequestSendForm();
		requestSendForm.crystalCL = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
		Protocol.Send(InventoryExtendModel.URL, requestSendForm, delegate(InventoryExtendModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_INVENTORY_CAPACITY);
			}
			call_back(obj);
		}, string.Empty);
	}

	public void OnDiff(BaseModelDiff.DiffItem diff)
	{
		bool flag = false;
		bool flag2 = true;
		if (Utility.IsExist(diff.add))
		{
			int i = 0;
			for (int count = diff.add.Count; i < count; i++)
			{
				Item item = diff.add[i];
				List<ExpiredItem> list = null;
				ItemInfo itemInfo = itemInventory.Find(ulong.Parse(item.uniqId));
				if (itemInfo != null)
				{
					list = itemInfo.expiredAtItem;
				}
				ItemInfo itemInfo2 = itemInventory.Set(item.uniqId, item);
				if (list != null)
				{
					itemInfo2.expiredAtItem = list;
				}
				if (GameSaveData.instance.AddNewItem(ITEM_ICON_TYPE.ITEM, item.uniqId))
				{
					flag2 = true;
				}
			}
			flag = true;
		}
		if (Utility.IsExist(diff.update))
		{
			int j = 0;
			for (int count2 = diff.update.Count; j < count2; j++)
			{
				Item item2 = diff.update[j];
				if (RemoveNewIcon(ITEM_ICON_TYPE.ITEM, item2.uniqId, item2.num))
				{
					flag2 = true;
				}
				List<ExpiredItem> list2 = null;
				ItemInfo itemInfo3 = itemInventory.Find(ulong.Parse(item2.uniqId));
				if (itemInfo3 != null)
				{
					list2 = itemInfo3.expiredAtItem;
				}
				ItemInfo itemInfo4 = itemInventory.Set(item2.uniqId, item2);
				if (list2 != null)
				{
					itemInfo4.expiredAtItem = list2;
				}
			}
			flag = true;
		}
		skillMaterialInventory = SkillItemInfo.CreateListFromItemInventory(itemInventory);
		inGameTempItemInventory.Clear();
		equipItemFilterList.Clear();
		if (flag2)
		{
			GameSaveData.Save();
		}
		if (flag)
		{
			if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "InGameScene" && MonoBehaviourSingleton<SmithManager>.IsValid())
			{
				MonoBehaviourSingleton<SmithManager>.I.CreateBadgeData(is_force: true);
			}
			MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_ITEM_INVENTORY);
		}
	}

	public void OnDiff(BaseModelDiff.DiffExpiredItem diff)
	{
		bool flag = false;
		if (Utility.IsExist(diff.add))
		{
			List<ItemInfo> list = null;
			int num = -1;
			int i = 0;
			for (int count = diff.add.Count; i < count; i++)
			{
				ExpiredItem o = diff.add[i];
				List<ItemInfo> list2 = list;
				if (num != o.itemId || list == null)
				{
					list2 = GetItemList((ItemInfo x) => x.tableID == o.itemId, 1);
					num = o.itemId;
					list = list2;
				}
				foreach (ItemInfo item in list2)
				{
					if (item.expiredAtItem == null)
					{
						item.expiredAtItem = new List<ExpiredItem>();
					}
					item.expiredAtItem.Add(o);
				}
			}
			flag = true;
		}
		if (Utility.IsExist(diff.update))
		{
			List<ItemInfo> list3 = null;
			int num2 = -1;
			int j = 0;
			for (int count2 = diff.update.Count; j < count2; j++)
			{
				ExpiredItem o2 = diff.update[j];
				List<ItemInfo> list4 = list3;
				if (num2 != o2.itemId || list3 == null)
				{
					list4 = GetItemList((ItemInfo x) => x.tableID == o2.itemId, 1);
					num2 = o2.itemId;
					list3 = list4;
				}
				foreach (ItemInfo item2 in list4)
				{
					ExpiredItem expiredItem = item2.expiredAtItem.Find((ExpiredItem y) => y.uniqId.Equals(o2.uniqId));
					expiredItem.expiredAt = o2.expiredAt;
					expiredItem.used = o2.used;
				}
			}
			flag = true;
		}
		if (flag)
		{
			MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_ITEM_INVENTORY);
		}
	}

	public void OnDiff(BaseModelDiff.DiffEquipItem diff)
	{
		bool flag = false;
		bool flag2 = false;
		if (Utility.IsExist(diff.add))
		{
			int i = 0;
			for (int count = diff.add.Count; i < count; i++)
			{
				EquipItem equipItem = diff.add[i];
				equipItemInventory.Set(equipItem.uniqId, equipItem);
				if (GameSaveData.instance.AddNewItem(ITEM_ICON_TYPE.ONE_HAND_SWORD, equipItem.uniqId))
				{
					flag2 = true;
				}
			}
			flag = true;
		}
		if (Utility.IsExist(diff.update))
		{
			int j = 0;
			for (int count2 = diff.update.Count; j < count2; j++)
			{
				EquipItem equipItem2 = diff.update[j];
				equipItemInventory.Set(equipItem2.uniqId, equipItem2);
				if (GameSaveData.instance.RemoveNewIcon(ITEM_ICON_TYPE.ONE_HAND_SWORD, equipItem2.uniqId))
				{
					flag2 = true;
				}
			}
			flag = true;
		}
		if (Utility.IsExist(diff.del))
		{
			int k = 0;
			for (int count3 = diff.del.Count; k < count3; k++)
			{
				string text = diff.del[k];
				equipItemInventory.Delete(ulong.Parse(text));
				if (GameSaveData.instance.RemoveNewIcon(ITEM_ICON_TYPE.ONE_HAND_SWORD, text))
				{
					flag2 = true;
				}
			}
			flag = true;
		}
		if (flag2)
		{
			GameSaveData.Save();
		}
		if (flag)
		{
			MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_INVENTORY);
		}
	}

	public void OnDiff(BaseModelDiff.DiffAbilityItem diff)
	{
		bool flag = false;
		bool flag2 = false;
		if (Utility.IsExist(diff.add))
		{
			int i = 0;
			for (int count = diff.add.Count; i < count; i++)
			{
				AbilityItem abilityItem = diff.add[i];
				abilityItemInventory.Set(abilityItem.uniqId, abilityItem);
				if (GameSaveData.instance.AddNewItem(ITEM_ICON_TYPE.ABILITY_ITEM, abilityItem.uniqId))
				{
					flag2 = true;
				}
			}
			flag = true;
		}
		if (Utility.IsExist(diff.update))
		{
			int j = 0;
			for (int count2 = diff.update.Count; j < count2; j++)
			{
				AbilityItem abilityItem2 = diff.update[j];
				abilityItemInventory.Set(abilityItem2.uniqId, abilityItem2);
				if (GameSaveData.instance.RemoveNewIcon(ITEM_ICON_TYPE.ABILITY_ITEM, abilityItem2.uniqId))
				{
					flag2 = true;
				}
			}
			flag = true;
		}
		if (Utility.IsExist(diff.del))
		{
			int k = 0;
			for (int count3 = diff.del.Count; k < count3; k++)
			{
				string text = diff.del[k];
				abilityItemInventory.Delete(ulong.Parse(text));
				if (GameSaveData.instance.RemoveNewIcon(ITEM_ICON_TYPE.ABILITY_ITEM, text))
				{
					flag2 = true;
				}
			}
			flag = true;
		}
		if (flag2)
		{
			GameSaveData.Save();
		}
		if (flag)
		{
			MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_ABILITY_ITEM_INVENTORY);
		}
	}

	public void OnDiff(BaseModelDiff.DiffAccessory diff)
	{
		bool flag = false;
		bool flag2 = false;
		if (Utility.IsExist(diff.add))
		{
			int i = 0;
			for (int count = diff.add.Count; i < count; i++)
			{
				Accessory accessory = diff.add[i];
				accessoryInventory.Set(accessory.uniqId, accessory);
				if (GameSaveData.instance.AddNewItem(ITEM_ICON_TYPE.ACCESSORY, accessory.uniqId))
				{
					flag2 = true;
				}
			}
			flag = true;
		}
		if (Utility.IsExist(diff.update))
		{
			int j = 0;
			for (int count2 = diff.update.Count; j < count2; j++)
			{
				Accessory accessory2 = diff.update[j];
				accessoryInventory.Set(accessory2.uniqId.ToString(), accessory2);
				if (GameSaveData.instance.RemoveNewIcon(ITEM_ICON_TYPE.ACCESSORY, accessory2.uniqId))
				{
					flag2 = true;
				}
			}
			flag = true;
		}
		if (Utility.IsExist(diff.del))
		{
			int k = 0;
			for (int count3 = diff.del.Count; k < count3; k++)
			{
				string text = diff.del[k];
				accessoryInventory.Delete(ulong.Parse(text));
				if (GameSaveData.instance.RemoveNewIcon(ITEM_ICON_TYPE.ACCESSORY, text))
				{
					flag2 = true;
				}
			}
			flag = true;
		}
		if (flag2)
		{
			GameSaveData.Save();
		}
		if (flag)
		{
			MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_GROW);
		}
	}

	public void OnDiff(BaseModelDiff.DiffSkillItem diff)
	{
		bool flag = false;
		bool flag2 = false;
		if (Utility.IsExist(diff.add))
		{
			int i = 0;
			for (int count = diff.add.Count; i < count; i++)
			{
				SkillItem skillItem = diff.add[i];
				skillItemInventory.Set(skillItem.uniqId, skillItem);
				if (GameSaveData.instance.AddNewItem(ITEM_ICON_TYPE.SKILL_ATTACK, skillItem.uniqId))
				{
					flag2 = true;
				}
			}
			flag = true;
		}
		if (Utility.IsExist(diff.update))
		{
			int j = 0;
			for (int count2 = diff.update.Count; j < count2; j++)
			{
				SkillItem skillItem2 = diff.update[j];
				SkillItemInfo skillItemInfo = skillItemInventory.Find(ulong.Parse(skillItem2.uniqId));
				if (skillItemInfo == null)
				{
					Log.Error("Not Found SkillItem:{0}, Update Action!", skillItem2.uniqId);
					skillItemInventory.Set(skillItem2.uniqId, skillItem2);
				}
				else
				{
					List<EquipSetSkillData> equipSetSkill = skillItemInfo.equipSetSkill;
					SkillItemInfo skillItemInfo2 = skillItemInventory.Set(skillItem2.uniqId, skillItem2);
					skillItemInfo2.UpdateEquipSetSkill(equipSetSkill);
					skillItemInfo2.UpdateUniqueEquipSetSkill(skillItemInfo.uniqueEquipSetSkill);
				}
				if (GameSaveData.instance.RemoveNewIcon(ITEM_ICON_TYPE.SKILL_ATTACK, skillItem2.uniqId))
				{
					flag2 = true;
				}
			}
			flag = true;
		}
		if (Utility.IsExist(diff.del))
		{
			int k = 0;
			for (int count3 = diff.del.Count; k < count3; k++)
			{
				string text = diff.del[k];
				skillItemInventory.Delete(ulong.Parse(text));
				if (GameSaveData.instance.RemoveNewIcon(ITEM_ICON_TYPE.SKILL_ATTACK, text))
				{
					flag2 = true;
				}
			}
			flag = true;
		}
		if (flag2)
		{
			GameSaveData.Save();
		}
		if (flag)
		{
			MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SKILL_INVENTORY);
		}
	}

	public void OnDiff(BaseModelDiff.DiffEquipSetSlot diff)
	{
		bool flag = false;
		bool flag2 = false;
		if (Utility.IsExist(diff.add))
		{
			int i = 0;
			for (int count = diff.add.Count; i < count; i++)
			{
				SkillItem.DiffEquipSetSlot diffEquipSetSlot = diff.add[i];
				skillItemInventory.Find(ulong.Parse(diffEquipSetSlot.uniqId))?.equipSetSkill.Add(new EquipSetSkillData(diffEquipSetSlot));
			}
			flag = true;
		}
		if (Utility.IsExist(diff.update))
		{
			int j = 0;
			for (int count2 = diff.update.Count; j < count2; j++)
			{
				SkillItem.DiffEquipSetSlot o = diff.update[j];
				if (GameSaveData.instance.RemoveNewIcon(ITEM_ICON_TYPE.SKILL_ATTACK, o.uniqId))
				{
					flag2 = true;
				}
				SkillItemInfo skillItemInfo = skillItemInventory.Find(ulong.Parse(o.uniqId));
				if (skillItemInfo == null)
				{
					continue;
				}
				EquipSetSkillData equipSetSkillData = skillItemInfo.equipSetSkill.Find((EquipSetSkillData x) => x.equipSetNo == o.setNo);
				ulong num = ulong.Parse(o.euid);
				if (num == 0)
				{
					if (equipSetSkillData != null)
					{
						skillItemInfo.equipSetSkill.Remove(equipSetSkillData);
					}
				}
				else if (equipSetSkillData == null)
				{
					skillItemInfo.equipSetSkill.Add(new EquipSetSkillData(o));
				}
				else
				{
					equipSetSkillData.equipItemUniqId = num;
					equipSetSkillData.equipSlotNo = o.slotNo;
				}
			}
			flag = true;
		}
		if (flag2)
		{
			GameSaveData.Save();
		}
		if (flag)
		{
			MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SKILL_INVENTORY);
		}
	}

	public void OnDiff(BaseModelDiff.DiffUniqueEquipSetSlot diff)
	{
		bool flag = false;
		bool flag2 = false;
		if (Utility.IsExist(diff.add))
		{
			int i = 0;
			for (int count = diff.add.Count; i < count; i++)
			{
				SkillItem.DiffEquipSetSlot diffEquipSetSlot = diff.add[i];
				SkillItemInfo skillItemInfo = skillItemInventory.Find(ulong.Parse(diffEquipSetSlot.uniqId));
				if (skillItemInfo != null)
				{
					skillItemInfo.uniqueEquipSetSkill = new EquipSetSkillData(diffEquipSetSlot);
				}
			}
			flag = true;
		}
		if (Utility.IsExist(diff.update))
		{
			int j = 0;
			for (int count2 = diff.update.Count; j < count2; j++)
			{
				SkillItem.DiffEquipSetSlot diffEquipSetSlot2 = diff.update[j];
				if (GameSaveData.instance.RemoveNewIcon(ITEM_ICON_TYPE.SKILL_ATTACK, diffEquipSetSlot2.uniqId))
				{
					flag2 = true;
				}
				SkillItemInfo skillItemInfo = skillItemInventory.Find(ulong.Parse(diffEquipSetSlot2.uniqId));
				if (skillItemInfo == null)
				{
					continue;
				}
				EquipSetSkillData uniqueEquipSetSkill = skillItemInfo.uniqueEquipSetSkill;
				ulong num = ulong.Parse(diffEquipSetSlot2.euid);
				if (num == 0)
				{
					if (uniqueEquipSetSkill != null)
					{
						skillItemInfo.uniqueEquipSetSkill.equipItemUniqId = 0uL;
						skillItemInfo.uniqueEquipSetSkill.equipSlotNo = 0;
					}
				}
				else if (uniqueEquipSetSkill == null)
				{
					skillItemInfo.uniqueEquipSetSkill = new EquipSetSkillData(diffEquipSetSlot2);
				}
				else
				{
					uniqueEquipSetSkill.equipItemUniqId = num;
					uniqueEquipSetSkill.equipSlotNo = diffEquipSetSlot2.slotNo;
				}
			}
			flag = true;
		}
		if (flag2)
		{
			GameSaveData.Save();
		}
		if (flag)
		{
			MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SKILL_INVENTORY);
		}
	}

	public void OnDiff(BaseModelDiff.DiffQuestItem diff)
	{
		bool flag = false;
		bool flag2 = false;
		if (Utility.IsExist(diff.add))
		{
			int i = 0;
			for (int count = diff.add.Count; i < count; i++)
			{
				QuestItem questItem = diff.add[i];
				questItemInventory.Set(questItem.uniqId, questItem);
				if (GameSaveData.instance.AddNewItem(ITEM_ICON_TYPE.QUEST_ITEM, questItem.uniqId))
				{
					flag2 = true;
				}
			}
			flag = true;
		}
		if (Utility.IsExist(diff.update))
		{
			int j = 0;
			for (int count2 = diff.update.Count; j < count2; j++)
			{
				QuestItem questItem2 = diff.update[j];
				if (RemoveNewIcon(ITEM_ICON_TYPE.QUEST_ITEM, questItem2.uniqId, questItem2.num))
				{
					flag2 = true;
				}
				if (questItem2.num == 0)
				{
					questItemInventory.Delete(ulong.Parse(questItem2.uniqId));
				}
				else if (questItem2.num > 0)
				{
					questItemInventory.Set(questItem2.uniqId, questItem2);
				}
			}
			flag = true;
		}
		if (flag2)
		{
			GameSaveData.Save();
		}
		if (flag)
		{
			MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_QUEST_ITEM_INVENTORY);
			MonoBehaviourSingleton<QuestManager>.I.needRequestOrderQuestList = true;
		}
	}

	public void AddShowFragsAbilityItem(ulong removeId)
	{
		if (IsNewItem(ITEM_ICON_TYPE.ABILITY_ITEM, removeId) && !removeNewFragAbilityItemIds.Contains(removeId))
		{
			removeNewFragAbilityItemIds.Add(removeId);
		}
	}

	public void DoRemoveNewFragsAbilityItem()
	{
		foreach (ulong removeNewFragAbilityItemId in removeNewFragAbilityItemIds)
		{
			RemoveNewIcon(ITEM_ICON_TYPE.ABILITY_ITEM, removeNewFragAbilityItemId.ToString(), 0);
		}
		removeNewFragAbilityItemIds.Clear();
	}
}
