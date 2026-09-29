// Decompiled with JetBrains decompiler
// Type: ItemExchangeManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;

#nullable disable
public class ItemExchangeManager : MonoBehaviourSingleton<ItemExchangeManager>
{
  public EXCHANGE_TYPE exchangeType { get; private set; }

  public void SetExchangeType(EXCHANGE_TYPE value) => this.exchangeType = value;

  public bool IsExchangeScene()
  {
    return MonoBehaviourSingleton<GameSceneManager>.I.ExistHistory("ItemStorageTop") || MonoBehaviourSingleton<GameSceneManager>.I.ExistHistory("ItemStorageSell");
  }

  public void SendInventorySellItem(List<string> uids, List<int> nums, Action<bool> call_back)
  {
    Protocol.Send<InventorySellItemModel.RequestSendForm, InventorySellItemModel>(InventorySellItemModel.URL, new InventorySellItemModel.RequestSendForm()
    {
      uids = uids,
      nums = nums
    }, (Action<InventorySellItemModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      call_back(flag);
    }));
  }

  public void SendInventorySellEquipItem(List<string> uids, Action<bool> call_back)
  {
    Protocol.Send<InventorySellEquipModel.RequestSendForm, InventorySellEquipModel>(InventorySellEquipModel.URL, new InventorySellEquipModel.RequestSendForm()
    {
      uids = uids
    }, (Action<InventorySellEquipModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      call_back(flag);
    }));
  }

  public void SendInventorySellSkillItem(List<string> uids, Action<bool> call_back)
  {
    InventorySellSkillModel.RequestSendForm postData = new InventorySellSkillModel.RequestSendForm();
    postData.uids = uids;
    bool is_attach = false;
    postData.uids.ForEach((Action<string>) (str_uniq_id =>
    {
      SkillItemInfo skillItem = MonoBehaviourSingleton<InventoryManager>.I.GetSkillItem(ulong.Parse(str_uniq_id));
      if (skillItem == null || !skillItem.isAttached && !skillItem.isUniqueAttached)
        return;
      is_attach = true;
    }));
    Protocol.Send<InventorySellSkillModel.RequestSendForm, InventorySellSkillModel>(InventorySellSkillModel.URL, postData, (Action<InventorySellSkillModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        if (is_attach)
          MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SKILL_CHANGE);
      }
      call_back(flag);
    }));
  }

  public void SendSellQuest(
    List<string> uids,
    List<int> nums,
    Action<bool, SellQuestItemReward, List<uint>> call_back)
  {
    Protocol.Send<InventorySellQuestModel.RequestSendForm, InventorySellQuestModel>(InventorySellQuestModel.URL, new InventorySellQuestModel.RequestSendForm()
    {
      uids = uids,
      nums = nums
    }, (Action<InventorySellQuestModel>) (ret =>
    {
      bool flag = false;
      List<uint> is_nothing_remains_quest = new List<uint>();
      if (ret.Error == Error.None)
      {
        flag = true;
        List<QuestData> orderQuestList = MonoBehaviourSingleton<QuestManager>.I.orderQuestList;
        if (orderQuestList != null && orderQuestList.Count > 0)
        {
          List<QuestData> new_list = new List<QuestData>();
          orderQuestList.ForEach((Action<QuestData>) (o =>
          {
            for (LinkedListNode<QuestItemInfo> linkedListNode = MonoBehaviourSingleton<InventoryManager>.I.questItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
            {
              if ((long) linkedListNode.Value.infoData.questData.tableData.questID == (long) o.questId && linkedListNode.Value.infoData.questData.num > 0)
              {
                new_list.Add(o);
                o.order.num = linkedListNode.Value.infoData.questData.num;
                break;
              }
            }
          }));
          List<QuestData> questDataList = new_list;
        }
        MonoBehaviourSingleton<InventoryManager>.I.ForAllQuestInvetory((Action<QuestItemInfo>) (data =>
        {
          if (data.infoData.questData.num != 0)
            return;
          is_nothing_remains_quest.Add(data.infoData.questData.tableData.questID);
        }));
      }
      call_back(flag, ret.result.reward, is_nothing_remains_quest);
    }));
  }

  public void SendInventorySellAbilityItem(List<string> uids, Action<bool> call_back)
  {
    InventoryAbilityItemSellModel.RequestSendForm postData = new InventoryAbilityItemSellModel.RequestSendForm();
    postData.uids = uids;
    bool is_attach = false;
    postData.uids.ForEach((Action<string>) (str_uniq_id =>
    {
      AbilityItemInfo abilityItem = MonoBehaviourSingleton<InventoryManager>.I.GetAbilityItem(ulong.Parse(str_uniq_id));
      if (abilityItem == null || abilityItem.equipUniqueId == 0UL)
        return;
      is_attach = true;
    }));
    Protocol.Send<InventoryAbilityItemSellModel.RequestSendForm, InventoryAbilityItemSellModel>(InventoryAbilityItemSellModel.URL, postData, (Action<InventoryAbilityItemSellModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        if (is_attach)
          MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_ABILITY_ITEM_CHANGE);
      }
      call_back(flag);
    }));
  }
}
