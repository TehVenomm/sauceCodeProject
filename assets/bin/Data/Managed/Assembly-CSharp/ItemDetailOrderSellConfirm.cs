// Decompiled with JetBrains decompiler
// Type: ItemDetailOrderSellConfirm
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ItemDetailOrderSellConfirm : GameSection
{
  private QuestSortData itemData;
  private int sellNum;

  public override IEnumerable<string> requireDataTable
  {
    get
    {
      yield return "EquipItemExceedTable";
    }
  }

  public override void Initialize()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    this.itemData = eventData[0] as QuestSortData;
    this.sellNum = (int) eventData[1];
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetLabelText((Enum) ItemDetailOrderSellConfirm.UI.LBL_TITLE_U, this.sectionData.GetText("TITLE"));
    this.SetLabelText((Enum) ItemDetailOrderSellConfirm.UI.LBL_TITLE_D, this.sectionData.GetText("TITLE"));
    int num1 = 0;
    int num2 = 0;
    QuestItemInfo item_info = this.itemData.GetItemData() as QuestItemInfo;
    this.SetGrid((Enum) ItemDetailOrderSellConfirm.UI.GRD_ICON, "", 1, false, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      uint num3 = 0;
      EquipItemExceedTable.EquipItemExceedData equipItemExceedData = Singleton<EquipItemExceedTable>.I.GetEquipItemExceedData(item_info.infoData.questData.tableData.rarity, item_info.infoData.questData.tableData.getType, item_info.infoData.questData.tableData.eventId);
      if (equipItemExceedData != null)
        num3 = equipItemExceedData.exchangeItemId;
      REWARD_TYPE rewardType = REWARD_TYPE.ITEM;
      ItemIcon rewardItemIcon = ItemIcon.CreateRewardItemIcon(rewardType, num3, t, this.sellNum);
      this.SetMaterialInfo(rewardItemIcon.transform, rewardType, num3);
      rewardItemIcon.SetRewardBG(true);
    }));
    this.SetLabelText((Enum) ItemDetailOrderSellConfirm.UI.LBL_GOLD, num1.ToString());
    this.SetLabelText((Enum) ItemDetailOrderSellConfirm.UI.LBL_EXP, num2.ToString());
    this.SetLabelText((Enum) ItemDetailOrderSellConfirm.UI.LBL_SELL, string.Format(this.sectionData.GetText("STR_SELL"), (object) this.itemData.GetName(), (object) this.sellNum));
  }

  private void OnQuery_OK()
  {
    List<string> uids = new List<string>();
    List<int> nums = new List<int>();
    uids.Add(this.itemData.GetUniqID().ToString());
    nums.Add(this.sellNum);
    GameSection.StayEvent();
    MonoBehaviourSingleton<ItemExchangeManager>.I.SendSellQuest(uids, nums, (Action<bool, SellQuestItemReward, List<uint>>) ((is_success, reward, empty_quest_item_list) =>
    {
      empty_quest_item_list.ForEach((Action<uint>) (empty_data =>
      {
        if ((int) this.itemData.GetTableID() != (int) empty_data)
          return;
        GameSection.ChangeStayEvent("CLOSE_DETAIL");
      }));
      GameSection.ResumeEvent(is_success);
    }));
  }

  private enum UI
  {
    GRD_ICON,
    LBL_SELL,
    LBL_GOLD,
    LBL_EXP,
    LBL_TITLE_U,
    LBL_TITLE_D,
  }
}
