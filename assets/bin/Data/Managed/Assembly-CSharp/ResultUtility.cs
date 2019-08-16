using Network;
using System.Collections.Generic;
using UnityEngine;

public static class ResultUtility
{
	private class RewardEquipItemSortData : EquipItemSortData
	{
		public int rewardNum = -1;

		public override int GetNum()
		{
			return rewardNum;
		}
	}

	private class RewardSkillItemSortData : SkillItemSortData
	{
		public int rewardNum = -1;

		public override int GetNum()
		{
			return rewardNum;
		}
	}

	private class RewardAccessoryItemSortData : AccessorySortData
	{
		public int rewardNum = -1;

		public override int GetNum()
		{
			return rewardNum;
		}
	}

	public static void DevideRewardDropAndEvent(QuestCompleteReward reward, ref QuestCompleteReward dropReward, ref QuestCompleteReward eventReward, ref List<string> eventRewardTitles)
	{
		List<string> list = new List<string>();
		dropReward.exp += reward.exp;
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < reward.eventPrice.Count; i++)
		{
			num += reward.eventPrice[i].gold;
			num2 += reward.eventPrice[i].crystal;
			eventReward.eventPrice.Add(reward.eventPrice[i]);
			list.Add(reward.eventPrice[i].rewardTitle);
		}
		dropReward.money += Mathf.Max(0, reward.money - num);
		dropReward.crystal += Mathf.Max(0, reward.crystal - num2);
		for (int j = 0; j < reward.item.Count; j++)
		{
			if (string.IsNullOrEmpty(reward.item[j].rewardTitle))
			{
				dropReward.item.Add(reward.item[j]);
				continue;
			}
			eventReward.item.Add(reward.item[j]);
			list.Add(reward.item[j].rewardTitle);
		}
		for (int k = 0; k < reward.skillItem.Count; k++)
		{
			if (string.IsNullOrEmpty(reward.skillItem[k].rewardTitle))
			{
				dropReward.skillItem.Add(reward.skillItem[k]);
				continue;
			}
			eventReward.skillItem.Add(reward.skillItem[k]);
			list.Add(reward.skillItem[k].rewardTitle);
		}
		for (int l = 0; l < reward.equipItem.Count; l++)
		{
			if (string.IsNullOrEmpty(reward.equipItem[l].rewardTitle))
			{
				dropReward.equipItem.Add(reward.equipItem[l]);
				continue;
			}
			eventReward.equipItem.Add(reward.equipItem[l]);
			list.Add(reward.equipItem[l].rewardTitle);
		}
		for (int m = 0; m < reward.questItem.Count; m++)
		{
			if (string.IsNullOrEmpty(reward.questItem[m].rewardTitle))
			{
				dropReward.questItem.Add(reward.questItem[m]);
				continue;
			}
			eventReward.questItem.Add(reward.questItem[m]);
			list.Add(reward.questItem[m].rewardTitle);
		}
		for (int n = 0; n < reward.accessoryItem.Count; n++)
		{
			if (string.IsNullOrEmpty(reward.accessoryItem[n].rewardTitle))
			{
				dropReward.accessoryItem.Add(reward.accessoryItem[n]);
				continue;
			}
			eventReward.accessoryItem.Add(reward.accessoryItem[n]);
			list.Add(reward.accessoryItem[n].rewardTitle);
		}
		if (eventRewardTitles == null)
		{
			eventRewardTitles = new List<string>();
		}
		for (int num3 = 0; num3 < list.Count; num3++)
		{
			if (!eventRewardTitles.Contains(list[num3]))
			{
				eventRewardTitles.Add(list[num3]);
			}
		}
	}

	public static bool IsRare(SortCompareData icon_base)
	{
		if (icon_base != null)
		{
			return GameDefine.IsRare(icon_base.GetRarity());
		}
		return false;
	}

	public static bool IsBreakReward(SortCompareData icon_base)
	{
		if (icon_base != null)
		{
			return icon_base.GetCategory() == REWARD_CATEGORY.BREAK;
		}
		return false;
	}

	public static int SetDropData(List<SortCompareData> drop_ary, int start_ary_index, List<QuestCompleteReward.Item> drop_data, REWARD_CATEGORY category = REWARD_CATEGORY.DROP)
	{
		int num = start_ary_index;
		QuestCompleteReward.Item[] ary = drop_data.ToArray();
		int i = 0;
		for (int num2 = ary.Length; i < num2; i++)
		{
			SortCompareData sortCompareData = null;
			if (num > 0 && category != REWARD_CATEGORY.BREAK)
			{
				sortCompareData = drop_ary.Find((SortCompareData _data) => _data != null && _data.GetTableID() == (uint)ary[i].itemId && _data is ItemSortData);
			}
			if (sortCompareData == null)
			{
				ItemInfo item = new ItemInfo();
				item.tableID = (uint)ary[i].itemId;
				item.tableData = Singleton<ItemTable>.I.GetItemData(item.tableID);
				item.num = ary[i].num;
				if (MonoBehaviourSingleton<InventoryManager>.I.IsHaveingItem(item.tableID))
				{
					MonoBehaviourSingleton<InventoryManager>.I.ForAllItemInventory(delegate(ItemInfo inventory_item)
					{
						if (inventory_item.tableID == item.tableID && inventory_item.num == item.num)
						{
							item.uniqueID = inventory_item.uniqueID;
						}
					});
				}
				ItemSortData itemSortData = new ItemSortData();
				itemSortData.SetItem(item);
				itemSortData.SetCategory(category);
				drop_ary.Add(itemSortData);
				num++;
			}
			else
			{
				ItemSortData itemSortData2 = sortCompareData as ItemSortData;
				ItemInfo itemInfo = itemSortData2.GetItemData() as ItemInfo;
				itemInfo.num += ary[i].num;
			}
		}
		return num;
	}

	public static int SetDropData(List<SortCompareData> drop_ary, int start_ary_index, List<QuestCompleteReward.EquipItem> drop_data, REWARD_CATEGORY category = REWARD_CATEGORY.DROP)
	{
		int num = start_ary_index;
		QuestCompleteReward.EquipItem[] ary = drop_data.ToArray();
		int i = 0;
		for (int num2 = ary.Length; i < num2; i++)
		{
			SortCompareData sortCompareData = null;
			if (num > 0 && category != REWARD_CATEGORY.BREAK)
			{
				sortCompareData = drop_ary.Find((SortCompareData _data) => _data != null && _data.GetTableID() == (uint)ary[i].equipItemId && _data is RewardEquipItemSortData);
			}
			if (sortCompareData == null)
			{
				EquipItem equipItem = new EquipItem();
				equipItem.uniqId = "0";
				equipItem.equipItemId = ary[i].equipItemId;
				equipItem.level = ary[i].lv;
				equipItem.exceed = 0;
				equipItem.is_locked = 0;
				equipItem.price = 0;
				EquipItemInfo item = new EquipItemInfo(equipItem);
				RewardEquipItemSortData rewardEquipItemSortData = new RewardEquipItemSortData();
				rewardEquipItemSortData.SetItem(item);
				rewardEquipItemSortData.SetCategory(category);
				rewardEquipItemSortData.rewardNum = ary[i].num;
				drop_ary.Add(rewardEquipItemSortData);
				num++;
			}
			else
			{
				RewardEquipItemSortData rewardEquipItemSortData2 = sortCompareData as RewardEquipItemSortData;
				rewardEquipItemSortData2.rewardNum += ary[i].num;
			}
		}
		return num;
	}

	public static int SetDropData(List<SortCompareData> drop_ary, int start_ary_index, List<QuestCompleteReward.SkillItem> drop_data, REWARD_CATEGORY category = REWARD_CATEGORY.DROP)
	{
		int num = start_ary_index;
		QuestCompleteReward.SkillItem[] ary = drop_data.ToArray();
		int i = 0;
		for (int num2 = ary.Length; i < num2; i++)
		{
			SortCompareData sortCompareData = null;
			if (num > 0 && category != REWARD_CATEGORY.BREAK)
			{
				sortCompareData = drop_ary.Find((SortCompareData _data) => _data != null && _data.GetTableID() == (uint)ary[i].skillItemId && _data is RewardSkillItemSortData);
			}
			if (sortCompareData == null)
			{
				SkillItemInfo skillItemInfo = new SkillItemInfo();
				skillItemInfo.tableID = (uint)ary[i].skillItemId;
				skillItemInfo.tableData = Singleton<SkillItemTable>.I.GetSkillItemData((uint)ary[i].skillItemId);
				RewardSkillItemSortData rewardSkillItemSortData = new RewardSkillItemSortData();
				rewardSkillItemSortData.SetItem(skillItemInfo);
				rewardSkillItemSortData.SetCategory(category);
				rewardSkillItemSortData.rewardNum = ary[i].num;
				drop_ary.Add(rewardSkillItemSortData);
				num++;
			}
			else
			{
				RewardSkillItemSortData rewardSkillItemSortData2 = sortCompareData as RewardSkillItemSortData;
				rewardSkillItemSortData2.rewardNum += ary[i].num;
			}
		}
		return num;
	}

	public static int SetDropData(List<SortCompareData> drop_ary, int start_ary_index, List<QuestCompleteReward.QuestItem> drop_data, REWARD_CATEGORY category = REWARD_CATEGORY.DROP)
	{
		int num = start_ary_index;
		QuestCompleteReward.QuestItem[] ary = drop_data.ToArray();
		int i = 0;
		for (int num2 = ary.Length; i < num2; i++)
		{
			SortCompareData sortCompareData = null;
			if (num > 0 && category != REWARD_CATEGORY.BREAK)
			{
				sortCompareData = drop_ary.Find((SortCompareData _data) => _data != null && _data.GetTableID() == (uint)ary[i].questId && _data is QuestSortData);
			}
			if (sortCompareData == null)
			{
				QuestItemInfo item = new QuestItemInfo();
				item.tableID = (uint)ary[i].questId;
				QuestData quest_list = new QuestData();
				item.infoData = new QuestInfoData(Singleton<QuestTable>.I.GetQuestData(item.tableID), quest_list, null);
				item.infoData.questData.num = ary[i].num;
				if (MonoBehaviourSingleton<InventoryManager>.I.IsHaveingItem(item.tableID))
				{
					MonoBehaviourSingleton<InventoryManager>.I.ForAllItemInventory(delegate(ItemInfo inventory_item)
					{
						if (inventory_item.tableID == item.tableID && inventory_item.num == item.infoData.questData.num)
						{
							item.uniqueID = inventory_item.uniqueID;
						}
					});
				}
				QuestSortData questSortData = new QuestSortData();
				questSortData.SetItem(item);
				questSortData.SetCategory(category);
				drop_ary.Add(questSortData);
				num++;
			}
			else
			{
				QuestSortData questSortData2 = sortCompareData as QuestSortData;
				questSortData2.itemData.infoData.questData.num += ary[i].num;
			}
		}
		return num;
	}

	public static int SetDropData(List<SortCompareData> drop_ary, int start_ary_index, List<QuestCompleteReward.AccessoryItem> drop_data, REWARD_CATEGORY category = REWARD_CATEGORY.DROP)
	{
		int num = start_ary_index;
		QuestCompleteReward.AccessoryItem[] ary = drop_data.ToArray();
		int i = 0;
		for (int num2 = ary.Length; i < num2; i++)
		{
			SortCompareData sortCompareData = null;
			if (num > 0 && category != REWARD_CATEGORY.BREAK)
			{
				sortCompareData = drop_ary.Find((SortCompareData _data) => _data != null && _data.GetTableID() == (uint)ary[i].accessoryId && _data is RewardAccessoryItemSortData);
			}
			if (sortCompareData == null)
			{
				AccessoryInfo accessoryInfo = new AccessoryInfo();
				accessoryInfo.tableID = (uint)ary[i].accessoryId;
				accessoryInfo.tableData = Singleton<AccessoryTable>.I.GetData(accessoryInfo.tableID);
				accessoryInfo.tableInfos = Singleton<AccessoryTable>.I.GetInfoList(accessoryInfo.tableID);
				RewardAccessoryItemSortData rewardAccessoryItemSortData = new RewardAccessoryItemSortData();
				rewardAccessoryItemSortData.SetItem(accessoryInfo);
				rewardAccessoryItemSortData.SetCategory(category);
				rewardAccessoryItemSortData.rewardNum = ary[i].num;
				drop_ary.Add(rewardAccessoryItemSortData);
				num++;
			}
			else
			{
				RewardAccessoryItemSortData rewardAccessoryItemSortData2 = sortCompareData as RewardAccessoryItemSortData;
				rewardAccessoryItemSortData2.rewardNum += ary[i].num;
			}
		}
		return num;
	}
}
