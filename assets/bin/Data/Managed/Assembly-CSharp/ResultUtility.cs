// Decompiled with JetBrains decompiler
// Type: ResultUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public static class ResultUtility
{
  public static void DevideRewardDropAndEvent(
    QuestCompleteReward reward,
    ref QuestCompleteReward dropReward,
    ref QuestCompleteReward eventReward,
    ref List<string> eventRewardTitles)
  {
    List<string> stringList = new List<string>();
    dropReward.exp += reward.exp;
    int num1 = 0;
    int num2 = 0;
    for (int index = 0; index < reward.eventPrice.Count; ++index)
    {
      num1 += reward.eventPrice[index].gold;
      num2 += reward.eventPrice[index].crystal;
      eventReward.eventPrice.Add(reward.eventPrice[index]);
      stringList.Add(reward.eventPrice[index].rewardTitle);
    }
    dropReward.money += Mathf.Max(0, reward.money - num1);
    dropReward.crystal += Mathf.Max(0, reward.crystal - num2);
    for (int index = 0; index < reward.item.Count; ++index)
    {
      if (string.IsNullOrEmpty(reward.item[index].rewardTitle))
      {
        dropReward.item.Add(reward.item[index]);
      }
      else
      {
        eventReward.item.Add(reward.item[index]);
        stringList.Add(reward.item[index].rewardTitle);
      }
    }
    for (int index = 0; index < reward.skillItem.Count; ++index)
    {
      if (string.IsNullOrEmpty(reward.skillItem[index].rewardTitle))
      {
        dropReward.skillItem.Add(reward.skillItem[index]);
      }
      else
      {
        eventReward.skillItem.Add(reward.skillItem[index]);
        stringList.Add(reward.skillItem[index].rewardTitle);
      }
    }
    for (int index = 0; index < reward.equipItem.Count; ++index)
    {
      if (string.IsNullOrEmpty(reward.equipItem[index].rewardTitle))
      {
        dropReward.equipItem.Add(reward.equipItem[index]);
      }
      else
      {
        eventReward.equipItem.Add(reward.equipItem[index]);
        stringList.Add(reward.equipItem[index].rewardTitle);
      }
    }
    for (int index = 0; index < reward.questItem.Count; ++index)
    {
      if (string.IsNullOrEmpty(reward.questItem[index].rewardTitle))
      {
        dropReward.questItem.Add(reward.questItem[index]);
      }
      else
      {
        eventReward.questItem.Add(reward.questItem[index]);
        stringList.Add(reward.questItem[index].rewardTitle);
      }
    }
    for (int index = 0; index < reward.accessoryItem.Count; ++index)
    {
      if (string.IsNullOrEmpty(reward.accessoryItem[index].rewardTitle))
      {
        dropReward.accessoryItem.Add(reward.accessoryItem[index]);
      }
      else
      {
        eventReward.accessoryItem.Add(reward.accessoryItem[index]);
        stringList.Add(reward.accessoryItem[index].rewardTitle);
      }
    }
    if (eventRewardTitles == null)
      eventRewardTitles = new List<string>();
    for (int index = 0; index < stringList.Count; ++index)
    {
      if (!eventRewardTitles.Contains(stringList[index]))
        eventRewardTitles.Add(stringList[index]);
    }
  }

  public static bool IsRare(SortCompareData icon_base)
  {
    return icon_base != null && GameDefine.IsRare(icon_base.GetRarity());
  }

  public static bool IsBreakReward(SortCompareData icon_base)
  {
    return icon_base != null && icon_base.GetCategory() == REWARD_CATEGORY.BREAK;
  }

  public static int SetDropData(
    List<SortCompareData> drop_ary,
    int start_ary_index,
    List<QuestCompleteReward.Item> drop_data,
    REWARD_CATEGORY category = REWARD_CATEGORY.DROP)
  {
    int num = start_ary_index;
    QuestCompleteReward.Item[] ary = drop_data.ToArray();
    int i = 0;
    for (int length = ary.Length; i < length; i++)
    {
      SortCompareData sortCompareData = (SortCompareData) null;
      if (num > 0 && category != REWARD_CATEGORY.BREAK)
        sortCompareData = drop_ary.Find((Predicate<SortCompareData>) (_data => _data != null && (int) _data.GetTableID() == ary[i].itemId && _data is ItemSortData));
      if (sortCompareData == null)
      {
        ItemInfo item = new ItemInfo();
        item.tableID = (uint) ary[i].itemId;
        item.tableData = Singleton<ItemTable>.I.GetItemData(item.tableID);
        item.num = ary[i].num;
        if (MonoBehaviourSingleton<InventoryManager>.I.IsHaveingItem(item.tableID))
          MonoBehaviourSingleton<InventoryManager>.I.ForAllItemInventory((Action<ItemInfo>) (inventory_item =>
          {
            if ((int) inventory_item.tableID != (int) item.tableID || inventory_item.num != item.num)
              return;
            item.uniqueID = inventory_item.uniqueID;
          }));
        ItemSortData itemSortData = new ItemSortData();
        itemSortData.SetItem((object) item);
        itemSortData.SetCategory(category);
        drop_ary.Add((SortCompareData) itemSortData);
        ++num;
      }
      else
        ((sortCompareData as ItemSortData).GetItemData() as ItemInfo).num += ary[i].num;
    }
    return num;
  }

  public static int SetDropData(
    List<SortCompareData> drop_ary,
    int start_ary_index,
    List<QuestCompleteReward.EquipItem> drop_data,
    REWARD_CATEGORY category = REWARD_CATEGORY.DROP)
  {
    int num = start_ary_index;
    QuestCompleteReward.EquipItem[] ary = drop_data.ToArray();
    int i = 0;
    for (int length = ary.Length; i < length; i++)
    {
      SortCompareData sortCompareData = (SortCompareData) null;
      if (num > 0 && category != REWARD_CATEGORY.BREAK)
        sortCompareData = drop_ary.Find((Predicate<SortCompareData>) (_data => _data != null && (int) _data.GetTableID() == ary[i].equipItemId && _data is ResultUtility.RewardEquipItemSortData));
      if (sortCompareData == null)
      {
        EquipItemInfo equipItemInfo = new EquipItemInfo(new EquipItem()
        {
          uniqId = "0",
          equipItemId = ary[i].equipItemId,
          level = (XorInt) ary[i].lv,
          exceed = 0,
          is_locked = 0,
          price = 0
        });
        ResultUtility.RewardEquipItemSortData equipItemSortData = new ResultUtility.RewardEquipItemSortData();
        equipItemSortData.SetItem((object) equipItemInfo);
        equipItemSortData.SetCategory(category);
        equipItemSortData.rewardNum = ary[i].num;
        drop_ary.Add((SortCompareData) equipItemSortData);
        ++num;
      }
      else
        (sortCompareData as ResultUtility.RewardEquipItemSortData).rewardNum += ary[i].num;
    }
    return num;
  }

  public static int SetDropData(
    List<SortCompareData> drop_ary,
    int start_ary_index,
    List<QuestCompleteReward.SkillItem> drop_data,
    REWARD_CATEGORY category = REWARD_CATEGORY.DROP)
  {
    int num = start_ary_index;
    QuestCompleteReward.SkillItem[] ary = drop_data.ToArray();
    int i = 0;
    for (int length = ary.Length; i < length; i++)
    {
      SortCompareData sortCompareData = (SortCompareData) null;
      if (num > 0 && category != REWARD_CATEGORY.BREAK)
        sortCompareData = drop_ary.Find((Predicate<SortCompareData>) (_data => _data != null && (int) _data.GetTableID() == ary[i].skillItemId && _data is ResultUtility.RewardSkillItemSortData));
      if (sortCompareData == null)
      {
        SkillItemInfo skillItemInfo = new SkillItemInfo();
        skillItemInfo.tableID = (uint) ary[i].skillItemId;
        skillItemInfo.tableData = Singleton<SkillItemTable>.I.GetSkillItemData((uint) ary[i].skillItemId);
        ResultUtility.RewardSkillItemSortData skillItemSortData = new ResultUtility.RewardSkillItemSortData();
        skillItemSortData.SetItem((object) skillItemInfo);
        skillItemSortData.SetCategory(category);
        skillItemSortData.rewardNum = ary[i].num;
        drop_ary.Add((SortCompareData) skillItemSortData);
        ++num;
      }
      else
        (sortCompareData as ResultUtility.RewardSkillItemSortData).rewardNum += ary[i].num;
    }
    return num;
  }

  public static int SetDropData(
    List<SortCompareData> drop_ary,
    int start_ary_index,
    List<QuestCompleteReward.QuestItem> drop_data,
    REWARD_CATEGORY category = REWARD_CATEGORY.DROP)
  {
    int num = start_ary_index;
    QuestCompleteReward.QuestItem[] ary = drop_data.ToArray();
    int i = 0;
    for (int length = ary.Length; i < length; i++)
    {
      SortCompareData sortCompareData = (SortCompareData) null;
      if (num > 0 && category != REWARD_CATEGORY.BREAK)
        sortCompareData = drop_ary.Find((Predicate<SortCompareData>) (_data => _data != null && (int) _data.GetTableID() == ary[i].questId && _data is QuestSortData));
      if (sortCompareData == null)
      {
        QuestItemInfo item = new QuestItemInfo();
        item.tableID = (uint) ary[i].questId;
        QuestData quest_list = new QuestData();
        item.infoData = new QuestInfoData(Singleton<QuestTable>.I.GetQuestData(item.tableID), quest_list, (int[]) null);
        item.infoData.questData.num = ary[i].num;
        if (MonoBehaviourSingleton<InventoryManager>.I.IsHaveingItem(item.tableID))
          MonoBehaviourSingleton<InventoryManager>.I.ForAllItemInventory((Action<ItemInfo>) (inventory_item =>
          {
            if ((int) inventory_item.tableID != (int) item.tableID || inventory_item.num != item.infoData.questData.num)
              return;
            item.uniqueID = inventory_item.uniqueID;
          }));
        QuestSortData questSortData = new QuestSortData();
        questSortData.SetItem((object) item);
        questSortData.SetCategory(category);
        drop_ary.Add((SortCompareData) questSortData);
        ++num;
      }
      else
        (sortCompareData as QuestSortData).itemData.infoData.questData.num += ary[i].num;
    }
    return num;
  }

  public static int SetDropData(
    List<SortCompareData> drop_ary,
    int start_ary_index,
    List<QuestCompleteReward.AccessoryItem> drop_data,
    REWARD_CATEGORY category = REWARD_CATEGORY.DROP)
  {
    int num = start_ary_index;
    QuestCompleteReward.AccessoryItem[] ary = drop_data.ToArray();
    int i = 0;
    for (int length = ary.Length; i < length; i++)
    {
      SortCompareData sortCompareData = (SortCompareData) null;
      if (num > 0 && category != REWARD_CATEGORY.BREAK)
        sortCompareData = drop_ary.Find((Predicate<SortCompareData>) (_data => _data != null && (int) _data.GetTableID() == ary[i].accessoryId && _data is ResultUtility.RewardAccessoryItemSortData));
      if (sortCompareData == null)
      {
        AccessoryInfo accessoryInfo = new AccessoryInfo();
        accessoryInfo.tableID = (uint) ary[i].accessoryId;
        accessoryInfo.tableData = Singleton<AccessoryTable>.I.GetData(accessoryInfo.tableID);
        accessoryInfo.tableInfos = Singleton<AccessoryTable>.I.GetInfoList(accessoryInfo.tableID);
        ResultUtility.RewardAccessoryItemSortData accessoryItemSortData = new ResultUtility.RewardAccessoryItemSortData();
        accessoryItemSortData.SetItem((object) accessoryInfo);
        accessoryItemSortData.SetCategory(category);
        accessoryItemSortData.rewardNum = ary[i].num;
        drop_ary.Add((SortCompareData) accessoryItemSortData);
        ++num;
      }
      else
        (sortCompareData as ResultUtility.RewardAccessoryItemSortData).rewardNum += ary[i].num;
    }
    return num;
  }

  private class RewardEquipItemSortData : EquipItemSortData
  {
    public int rewardNum = -1;

    public override int GetNum() => this.rewardNum;
  }

  private class RewardSkillItemSortData : SkillItemSortData
  {
    public int rewardNum = -1;

    public override int GetNum() => this.rewardNum;
  }

  private class RewardAccessoryItemSortData : AccessorySortData
  {
    public int rewardNum = -1;

    public override int GetNum() => this.rewardNum;
  }
}
