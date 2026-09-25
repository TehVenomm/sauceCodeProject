// Decompiled with JetBrains decompiler
// Type: ItemDetailLithographSellConfirm
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ItemDetailLithographSellConfirm : ItemStorageSellConfirm
{
  private SortCompareData item;
  private int num;
  private bool m_isRareConfirm;

  public override string overrideBackKeyEvent => "NO";

  protected override bool isShowIconBG() => true;

  public override void Initialize()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    this.item = eventData[0] as SortCompareData;
    this.num = (int) eventData[1];
    List<SortCompareData> sortCompareDataList = new List<SortCompareData>();
    sortCompareDataList.Add(this.item);
    if (MonoBehaviourSingleton<GameSceneManager>.I.GetHierarchyList().Exists((Predicate<GameSectionHierarchy.HierarchyData>) (x => ((Object) x.section).name == "ItemStorageSell")))
      GameSection.SetEventData((object) new object[3]
      {
        (object) ItemStorageTop.TAB_MODE.MATERIAL,
        (object) sortCompareDataList,
        (object) ItemStorageSellConfirm.GO_BACK.SELL
      });
    else
      GameSection.SetEventData((object) new object[3]
      {
        (object) ItemStorageTop.TAB_MODE.MATERIAL,
        (object) sortCompareDataList,
        (object) ItemStorageSellConfirm.GO_BACK.TOP
      });
    base.Initialize();
    this.m_isRareConfirm = false;
    sortCompareDataList.ForEach((Action<SortCompareData>) (sort_data =>
    {
      if (this.m_isRareConfirm || !GameDefine.IsRareLithograph(sort_data.GetRarity()))
        return;
      this.m_isRareConfirm = true;
    }));
  }

  public override void UpdateUI() => base.UpdateUI();

  protected override NeedMaterial[] CreateNeedMaterialAry()
  {
    List<NeedMaterial> needMaterialList = new List<NeedMaterial>();
    if (this.item.GetItemData() is ItemInfo itemData && itemData.tableData.type == ITEM_TYPE.LITHOGRAPH)
    {
      uint num = 0;
      EquipItemExceedTable.EquipItemExceedData equipItemExceedData = Singleton<EquipItemExceedTable>.I.GetEquipItemExceedData(itemData.tableData.rarity, itemData.tableData.getType, itemData.tableData.eventId);
      if (equipItemExceedData != null)
        num = equipItemExceedData.exchangeItemId;
      if (num != 0U && Singleton<ItemTable>.I.GetItemData(num) != null)
        needMaterialList.Add(new NeedMaterial(num, this.num));
    }
    return needMaterialList.ToArray();
  }

  protected override int GetTargetIconNum(SortCompareData[] sell_data_ary, int i) => this.num;

  protected override int GetSellGold() => this.item.GetSalePrice() * this.num;

  public void OnQuery_YES()
  {
    if (this.m_isRareConfirm)
    {
      GameSection.SetEventData((object) new object[1]
      {
        (object) this.item.GetRarity().ToString()
      });
      GameSection.ChangeEvent("INCLUDE_RARE_CONFIRM");
    }
    else
      this.OnQuery_ItemDetailLithographSellIncludeRareConfirm_YES();
  }

  public void OnQuery_ItemDetailLithographSellIncludeRareConfirm_YES()
  {
    List<string> uids = new List<string>();
    List<int> nums = new List<int>();
    uids.Add(this.item.GetUniqID().ToString());
    nums.Add(this.num);
    if (this.num >= this.item.GetNum())
      GameSection.ChangeEvent("CLOSE_DETAIL");
    GameSection.StayEvent();
    MonoBehaviourSingleton<ItemExchangeManager>.I.SendInventorySellItem(uids, nums, (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
  }

  public void OnQuery_ItemDetailLithographSellIncludeRareConfirm_NO()
  {
  }

  protected override void ChangeEventForGoBack()
  {
  }

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
    STR_NON_REWARD,
  }
}
