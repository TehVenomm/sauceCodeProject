// Decompiled with JetBrains decompiler
// Type: QuestResultEventRewardDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class QuestResultEventRewardDialog : ItemSellConfirm
{
  private int totalGold;
  private int crystalNum;

  public override void Initialize()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    this.sellData = eventData[0] as List<SortCompareData>;
    this.totalGold = (int) eventData[1];
    this.crystalNum = (int) eventData[2];
    this.SetLabelText((Enum) QuestResultEventRewardDialog.UI.LBL_REWARD_TITLE, (string) eventData[3]);
    this.isRareConfirm = true;
    this.isHideMainText = true;
    this.isButtonSingle = true;
    base.Initialize();
  }

  private void OnQuery_OK() => GameSection.BackSection();

  protected override void DrawIcon()
  {
    SortCompareData[] itemData = this.sellData.ToArray();
    int reward_num = itemData.Length;
    if (this.crystalNum > 0)
      ++reward_num;
    if (this.totalGold > 0)
      ++reward_num;
    bool shouldAddGold = this.totalGold > 0;
    this.SetGrid((Enum) QuestResultEventRewardDialog.UI.GRD_EVENT_REWARD, (string) null, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.SELL_SELECT_MAX, false, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      if (i < reward_num)
      {
        if (i < itemData.Length)
        {
          int enemy_icon_id = 0;
          int enemy_icon_id2 = 0;
          object itemData1 = itemData[i].GetItemData();
          if (itemData1 is ItemSortData)
          {
            ItemSortData itemSortData = itemData1 as ItemSortData;
            enemy_icon_id = itemSortData.itemData.tableData.enemyIconID;
            enemy_icon_id2 = itemSortData.itemData.tableData.enemyIconID2;
          }
          ItemIcon itemIcon;
          if (itemData[i].GetIconType() == ITEM_ICON_TYPE.QUEST_ITEM)
            itemIcon = ItemIcon.Create(new ItemIcon.ItemIconCreateParam()
            {
              icon_type = itemData[i].GetIconType(),
              icon_id = itemData[i].GetIconID(),
              rarity = new RARITY_TYPE?(itemData[i].GetRarity()),
              parent = t,
              element = itemData[i].GetIconElement(),
              magi_enable_equip_type = itemData[i].GetIconMagiEnableType(),
              num = itemData[i].GetNum(),
              enemy_icon_id = enemy_icon_id,
              enemy_icon_id2 = enemy_icon_id2,
              questIconSizeType = ItemIcon.QUEST_ICON_SIZE_TYPE.REWARD_DELIVERY_LIST
            });
          else
            itemIcon = ItemIcon.Create(itemData[i].GetIconType(), itemData[i].GetIconID(), new RARITY_TYPE?(itemData[i].GetRarity()), t, itemData[i].GetIconElement(), itemData[i].GetIconMagiEnableType(), itemData[i].GetNum(), enemy_icon_id: enemy_icon_id, enemy_icon_id2: enemy_icon_id2, getType: itemData[i].GetGetType());
          itemIcon.SetRewardBG(true);
          this.SetMaterialInfo(itemIcon.transform, itemData[i].GetMaterialType(), itemData[i].GetTableID(), this.GetCtrl((Enum) QuestResultEventRewardDialog.UI.OBJ_SCROLL_VIEW));
        }
        else if (shouldAddGold)
        {
          ItemIcon rewardItemIcon = ItemIcon.CreateRewardItemIcon(REWARD_TYPE.MONEY, 1U, t, this.totalGold);
          rewardItemIcon.SetRewardBG(true);
          this.SetMaterialInfo(rewardItemIcon.transform, REWARD_TYPE.MONEY, 0U, this.GetCtrl((Enum) QuestResultEventRewardDialog.UI.OBJ_SCROLL_VIEW));
          shouldAddGold = false;
        }
        else
        {
          ItemIcon rewardItemIcon = ItemIcon.CreateRewardItemIcon(REWARD_TYPE.CRYSTAL, 1U, t, this.crystalNum);
          rewardItemIcon.SetRewardBG(true);
          this.SetMaterialInfo(rewardItemIcon.transform, REWARD_TYPE.CRYSTAL, 0U, this.GetCtrl((Enum) QuestResultEventRewardDialog.UI.OBJ_SCROLL_VIEW));
        }
      }
      else
        this.SetActive(t, false);
    }));
  }

  protected override int GetSellGold() => this.totalGold;

  public new enum UI
  {
    STR_INCLUDE_RARE,
    STR_MAIN_TEXT,
    STR_TITLE_R,
    GRD_ICON,
    LBL_TOTAL,
    OBJ_GOLD,
    BTN_0,
    BTN_1,
    BTN_CENTER,
    SCR_ICON,
    GRD_REWARD_ICON,
    LBL_REWARD_TITLE,
    GRD_EVENT_REWARD,
    OBJ_SCROLL_VIEW,
  }
}
