// Decompiled with JetBrains decompiler
// Type: QuestResultMissionClearRewardDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class QuestResultMissionClearRewardDialog : ItemSellConfirm
{
  private int totalGold;
  private int crystalNum;
  private bool isComplete;
  private PointShopResultData missionPointData;

  public override void Initialize()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    this.sellData = eventData[0] as List<SortCompareData>;
    this.totalGold = (int) eventData[1];
    this.crystalNum = (int) eventData[2];
    this.isComplete = (bool) eventData[3];
    this.missionPointData = (PointShopResultData) eventData[4];
    this.isRareConfirm = true;
    this.isHideMainText = true;
    this.isButtonSingle = true;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    base.UpdateUI();
    this.SetActive((Enum) QuestResultMissionClearRewardDialog.UI.OBJ_NORMAL_ROOT, !this.isComplete);
    this.SetActive((Enum) QuestResultMissionClearRewardDialog.UI.OBJ_COMPLETE_ROOT, this.isComplete);
  }

  private void OnQuery_OK() => GameSection.BackSection();

  protected override void DrawIcon()
  {
    SortCompareData[] sell_data_ary = this.sellData.ToArray();
    int reward_num = sell_data_ary.Length;
    if (this.crystalNum > 0)
      ++reward_num;
    if (this.totalGold > 0)
      ++reward_num;
    if (this.missionPointData != null && this.missionPointData.missionPoint > 0)
      ++reward_num;
    bool shouldAddGold = this.totalGold > 0;
    bool shouldAddMissionPoint = this.missionPointData != null && this.missionPointData.missionPoint > 0;
    this.SetGrid((Enum) QuestResultMissionClearRewardDialog.UI.GRD_ICON, (string) null, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.SELL_SELECT_MAX, false, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
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
        else if (shouldAddGold)
        {
          ItemIcon rewardItemIcon = ItemIcon.CreateRewardItemIcon(REWARD_TYPE.MONEY, 1U, t, this.totalGold);
          rewardItemIcon.SetRewardBG(true);
          this.SetMaterialInfo(rewardItemIcon.transform, REWARD_TYPE.MONEY, 0U);
          shouldAddGold = false;
        }
        else if (shouldAddMissionPoint)
        {
          int icon_id;
          ITEM_ICON_TYPE icon_type;
          RARITY_TYPE? rarity;
          ELEMENT_TYPE element;
          ItemIcon.GetIconShowData(REWARD_TYPE.POINT_SHOP_POINT, (uint) this.missionPointData.pointShopId, out icon_id, out icon_type, out rarity, out element, out ELEMENT_TYPE _, out EQUIPMENT_TYPE? _, out int _, out int _, out GET_TYPE _);
          ItemIcon itemIcon = ItemIcon.Create(icon_type, icon_id, rarity, t, element, num: this.missionPointData.missionPoint);
          itemIcon.SetRewardBG(true);
          int id = this.missionPointData.isEvent ? 0 : 1;
          this.SetMaterialInfo(itemIcon.transform, REWARD_TYPE.POINT_SHOP_POINT, (uint) id);
          shouldAddMissionPoint = false;
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
    OBJ_COMPLETE_ROOT,
    OBJ_NORMAL_ROOT,
  }
}
