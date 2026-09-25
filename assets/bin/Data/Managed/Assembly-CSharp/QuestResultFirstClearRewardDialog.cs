// Decompiled with JetBrains decompiler
// Type: QuestResultFirstClearRewardDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class QuestResultFirstClearRewardDialog : ItemSellConfirm
{
  private int totalSell;
  private int crystalNum;

  public override void Initialize()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    this.sellData = eventData[0] as List<SortCompareData>;
    this.totalSell = (int) eventData[1];
    this.isRareConfirm = true;
    this.isHideMainText = true;
    this.isButtonSingle = true;
    base.Initialize();
  }

  private void OnQuery_OK() => GameSection.BackSection();

  protected override void DrawIcon()
  {
    SortCompareData[] sell_data_ary = this.sellData.ToArray();
    int reward_num = sell_data_ary.Length;
    if (this.crystalNum > 0)
      ++reward_num;
    this.SetGrid((Enum) ItemSellConfirm.UI.GRD_ICON, (string) null, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.SELL_SELECT_MAX, false, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      if (i < reward_num)
      {
        if (i < sell_data_ary.Length)
        {
          int enemy_icon_id = 0;
          int enemy_icon_id2 = 0;
          object itemData = sell_data_ary[i].GetItemData();
          if (itemData is ItemSortData)
          {
            ItemSortData itemSortData = itemData as ItemSortData;
            enemy_icon_id = itemSortData.itemData.tableData.enemyIconID;
            enemy_icon_id2 = itemSortData.itemData.tableData.enemyIconID2;
          }
          ItemIcon itemIcon = ItemIcon.Create(sell_data_ary[i].GetIconType(), sell_data_ary[i].GetIconID(), new RARITY_TYPE?(sell_data_ary[i].GetRarity()), t, sell_data_ary[i].GetIconElement(), sell_data_ary[i].GetIconMagiEnableType(), sell_data_ary[i].GetNum(), enemy_icon_id: enemy_icon_id, enemy_icon_id2: enemy_icon_id2, getType: sell_data_ary[i].GetGetType());
          itemIcon.SetRewardBG(true);
          this.SetMaterialInfo(itemIcon.transform, sell_data_ary[i].GetMaterialType(), sell_data_ary[i].GetTableID());
        }
        else
        {
          ItemIcon rewardItemIcon = ItemIcon.CreateRewardItemIcon(REWARD_TYPE.CRYSTAL, 1U, t, this.crystalNum);
          rewardItemIcon.SetRewardBG(true);
          this.SetMaterialInfo(rewardItemIcon.transform, REWARD_TYPE.CRYSTAL, 0U);
        }
      }
      else
        this.SetActive(t, false);
    }));
  }

  protected override int GetSellGold() => this.totalSell;
}
