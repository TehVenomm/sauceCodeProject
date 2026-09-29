// Decompiled with JetBrains decompiler
// Type: ItemSellConfirm
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ItemSellConfirm : GameSection
{
  protected List<SortCompareData> sellData;

  protected bool isRareConfirm { set; get; }

  protected bool isEquipConfirm { set; get; }

  protected bool isExceedConfirm { set; get; }

  protected bool isExceedEquipmentConfirm { set; get; }

  protected bool isHideMainText { set; get; }

  protected bool isButtonSingle { set; get; }

  protected virtual bool isShowIconBG() => true;

  public override void Initialize() => base.Initialize();

  public override void UpdateUI()
  {
    if (this.sellData == null)
      return;
    this.SetLabelText((Enum) ItemSellConfirm.UI.STR_TITLE_R, this.sectionData.GetText("STR_TITLE"));
    this.SetActive((Enum) ItemSellConfirm.UI.STR_INCLUDE_RARE, this.isRareConfirm);
    this.SetActive((Enum) ItemSellConfirm.UI.STR_MAIN_TEXT, !this.isHideMainText);
    this.DrawIcon();
    SortCompareData[] array = this.sellData.ToArray();
    int sellGold = this.GetSellGold();
    if (sellGold == 0)
    {
      int index = 0;
      for (int length = array.Length; index < length; ++index)
        sellGold += array[index].GetSalePrice();
    }
    this.SetActive((Enum) ItemSellConfirm.UI.OBJ_GOLD, sellGold != 0);
    this.SetLabelText((Enum) ItemSellConfirm.UI.LBL_TOTAL, sellGold.ToString());
    if (this.isButtonSingle)
    {
      this.SetActive((Enum) ItemSellConfirm.UI.BTN_CENTER, true);
      this.SetActive((Enum) ItemSellConfirm.UI.BTN_0, false);
      this.SetActive((Enum) ItemSellConfirm.UI.BTN_1, false);
    }
    else
    {
      this.SetActive((Enum) ItemSellConfirm.UI.BTN_CENTER, false);
      this.SetActive((Enum) ItemSellConfirm.UI.BTN_0, true);
      this.SetActive((Enum) ItemSellConfirm.UI.BTN_1, true);
    }
  }

  protected virtual int GetSellGold() => 0;

  protected virtual void DrawIcon()
  {
    SortCompareData[] sell_data_ary = this.sellData.ToArray();
    this.SetGrid((Enum) ItemSellConfirm.UI.GRD_ICON, (string) null, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.SELL_SELECT_MAX, false, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      if (i < sell_data_ary.Length)
      {
        int enemy_icon_id = 0;
        int enemy_icon_id2 = 0;
        bool is_equipping = false;
        SortCompareData sortCompareData = sell_data_ary[i];
        switch (sortCompareData)
        {
          case ItemSortData _:
            ItemSortData itemSortData = sortCompareData as ItemSortData;
            enemy_icon_id = itemSortData.itemData.tableData.enemyIconID;
            enemy_icon_id2 = itemSortData.itemData.tableData.enemyIconID2;
            break;
          case SkillItemSortData _:
            is_equipping = (sortCompareData as SkillItemSortData).IsEquipping();
            break;
          case AbilityItemSortData _:
            enemy_icon_id = (sortCompareData as AbilityItemSortData).itemData.GetItemTableData().enemyIconID;
            enemy_icon_id2 = (sortCompareData as AbilityItemSortData).itemData.GetItemTableData().enemyIconID2;
            break;
        }
        ItemIcon itemIcon = ItemIcon.Create(sell_data_ary[i].GetIconType(), sell_data_ary[i].GetIconID(), new RARITY_TYPE?(sell_data_ary[i].GetRarity()), t, sell_data_ary[i].GetIconElement(), sell_data_ary[i].GetIconMagiEnableType(), this.GetTargetIconNum(sell_data_ary, i), is_equipping: is_equipping, enemy_icon_id: enemy_icon_id, enemy_icon_id2: enemy_icon_id2, getType: sell_data_ary[i].GetGetType(), element2: sell_data_ary[i].GetIconElementSub());
        itemIcon.SetRewardBG(this.isShowIconBG());
        Transform ctrl = this.GetCtrl((Enum) ItemSellConfirm.UI.SCR_ICON);
        this.SetMaterialInfo(itemIcon.transform, sell_data_ary[i].GetMaterialType(), sell_data_ary[i].GetTableID(), ctrl);
      }
      else
        this.SetActive(t, false);
    }));
  }

  protected virtual int GetTargetIconNum(SortCompareData[] sell_data_ary, int i)
  {
    return sell_data_ary[i].GetNum();
  }

  public enum UI
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
  }
}
