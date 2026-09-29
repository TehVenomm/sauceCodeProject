// Decompiled with JetBrains decompiler
// Type: SmithUseAbilityItemList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class SmithUseAbilityItemList : GameSection
{
  private EquipItemInfo equipItemInfo;
  private bool isSellMode;
  private List<AbilityItemSortData> sellItemData = new List<AbilityItemSortData>();
  private SmithUseAbilityItemList.SHOW_INVENTORY_MODE currentShowInventoryMode;
  private ItemStorageTop.AbilityItemInventory inventory;

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return base.GetUpdateUINotifyFlags() | GameSection.NOTIFY_FLAG.UPDATE_ABILITY_ITEM_INVENTORY;
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_ABILITY_ITEM_INVENTORY) != (GameSection.NOTIFY_FLAG) 0)
    {
      this.inventory = (ItemStorageTop.AbilityItemInventory) null;
      this.SetDirty((Enum) this.GetCurrentInventoryRoot());
    }
    base.OnNotify(flags);
  }

  public override void Initialize()
  {
    this.equipItemInfo = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>().selectEquipData;
    this.currentShowInventoryMode = SmithUseAbilityItemList.SHOW_INVENTORY_MODE.MAIN_STATUS;
    this.InitializeCaption();
    base.Initialize();
  }

  public override void UpdateUI()
  {
    if (this.inventory == null)
      this.inventory = new ItemStorageTop.AbilityItemInventory();
    this.SetActive((Enum) SmithUseAbilityItemList.UI.OBJ_BTN_SELL_MODE, !this.isSellMode);
    this.SetActive((Enum) SmithUseAbilityItemList.UI.OBJ_SELL_MODE_ROOT, this.isSellMode);
    this.SetLabelText((Enum) SmithUseAbilityItemList.UI.LBL_MAX_HAVE_NUM, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.maxAbilityItem.ToString());
    this.SetLabelText((Enum) SmithUseAbilityItemList.UI.LBL_NOW_HAVE_NUM, this.inventory.datas.Length.ToString());
    this.SetActive((Enum) SmithUseAbilityItemList.UI.GRD_INVENTORY, false);
    this.SetActive((Enum) SmithUseAbilityItemList.UI.GRD_INVENTORY_SMALL, false);
    this.SetLabelText((Enum) SmithUseAbilityItemList.UI.LBL_SORT, this.inventory.sortSettings.GetSortLabel());
    this.SetToggle((Enum) SmithUseAbilityItemList.UI.TGL_ICON_ASC, this.inventory.sortSettings.orderTypeAsc);
    if (this.isSellMode)
    {
      this.SetLabelText((Enum) SmithUseAbilityItemList.UI.LBL_MAX_SELECT_NUM, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.SELL_SELECT_MAX.ToString());
      this.SetLabelText((Enum) SmithUseAbilityItemList.UI.LBL_SELECT_NUM, this.sellItemData.Count.ToString());
      int num = 0;
      foreach (AbilityItemSortData abilityItemSortData in this.sellItemData)
        num += abilityItemSortData.itemData.GetItemTableData().price;
      this.SetLabelText((Enum) SmithUseAbilityItemList.UI.LBL_TOTAL, num.ToString());
    }
    SmithUseAbilityItemList.UI currentInventoryRoot = this.GetCurrentInventoryRoot();
    this.SetActive((Enum) currentInventoryRoot, true);
    this.SetDynamicList((Enum) currentInventoryRoot, (string) null, this.inventory.datas.Length, false, (Func<int, bool>) (i =>
    {
      SortCompareData data = this.inventory.datas[i];
      return data != null && data.IsPriority(this.inventory.sortSettings.orderTypeAsc);
    }), (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycre) =>
    {
      AbilityItemSortData abilityItem = this.inventory.datas[i] as AbilityItemSortData;
      int index = this.sellItemData.FindIndex((Predicate<AbilityItemSortData>) (x => (long) x.GetUniqID() == (long) abilityItem.GetUniqID()));
      ItemIcon icon = this.CreateIcon(abilityItem, t, i);
      if (!Object.op_Inequality((Object) icon, (Object) null))
        return;
      icon.SetUniqID(abilityItem.GetUniqID());
      bool flag = abilityItem.itemData.GetItemTableData().rarity <= this.equipItemInfo.tableData.rarity;
      icon.SetGrayout(!flag);
      if (icon is ItemIconDetail)
      {
        (icon as ItemIconDetail).setupperMaterial.SetDescription(abilityItem.itemData.GetDescription());
        (icon as ItemIconDetail).setupperMaterial.SetActiveInfo(1);
      }
      ((Component) icon.textLabel).gameObject.SetActive(true);
      if (this.isSellMode)
      {
        ((Component) icon.selectFrame).gameObject.SetActive(index >= 0);
        if (index >= 0)
        {
          if (icon is ItemIconDetail)
            (icon as ItemIconDetail).setupperEquip.SetupSelectNumberSprite(index + 1);
          else
            (icon as ItemIconDetailSmall).SetupSelectNumberSprite(index + 1);
        }
      }
      this.SetEvent(icon.transform, flag ? "SELECT_ITEM" : "LESS_RARITY", (object) abilityItem);
    }));
    this.SetActive((Enum) SmithUseAbilityItemList.UI.BTN_CHANGE, false);
    this.UpdateAnchors();
    base.UpdateUI();
  }

  public override void Exit()
  {
    MonoBehaviourSingleton<InventoryManager>.I.DoRemoveNewFragsAbilityItem();
    base.Exit();
  }

  private ItemIcon CreateIcon(AbilityItemSortData item_data, Transform parent, int index)
  {
    bool is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(ITEM_ICON_TYPE.ABILITY_ITEM, item_data.GetUniqID());
    MonoBehaviourSingleton<InventoryManager>.I.AddShowFragsAbilityItem(item_data.GetUniqID());
    ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(item_data.GetTableID());
    return ItemIconDetail.CreateMaterialIcon(item_data.GetIconType(), item_data.GetIconID(), new RARITY_TYPE?(item_data.GetRarity()), itemData, true, parent, item_data.GetNum(), item_data.GetName(), "SELECT_ITEM", index, is_new: is_new);
  }

  private void OnQuery_CHANGE_INVENTORY()
  {
    this.currentShowInventoryMode = SmithUseAbilityItemList.SHOW_INVENTORY_MODE.MAIN_STATUS;
    this.SetToggle((Enum) SmithUseAbilityItemList.UI.TGL_CHANGE_INVENTORY, true);
    this.RefreshUI();
  }

  private void OnQuery_SELECT_ITEM()
  {
    if (!(GameSection.GetEventData() is AbilityItemSortData eventData))
      GameSection.StopEvent();
    else if (this.isSellMode)
      this.ChangeSellModeSelectItem(eventData);
    else
      GameSection.SetEventData((object) new object[2]
      {
        (object) this.equipItemInfo,
        (object) eventData
      });
  }

  private void OnQuery_SELL_MODE()
  {
    this.isSellMode = !this.isSellMode;
    this.RefreshUI();
  }

  private void OnQuery_SELL_MODE_END()
  {
    this.sellItemData.Clear();
    this.isSellMode = !this.isSellMode;
    this.RefreshUI();
  }

  protected void OnQuery_SELL()
  {
    if (this.sellItemData.Count == 0)
      GameSection.ChangeEvent("SELL_NOT_SELECT");
    else
      GameSection.SetEventData((object) this.sellItemData);
  }

  private void OnQuery_LESS_RARITY()
  {
    if (!(GameSection.GetEventData() is AbilityItemSortData eventData))
    {
      GameSection.StopEvent();
    }
    else
    {
      if (!this.isSellMode)
        return;
      this.ChangeSellModeSelectItem(eventData);
    }
  }

  private void OnQuery_SECTION_BACK() => GameSection.SetEventData((object) this.equipItemInfo);

  private void OnQuery_SORT()
  {
    GameSection.SetEventData((object) this.inventory.sortSettings.Clone());
  }

  private void InitializeCaption()
  {
    Transform ctrl = this.GetCtrl((Enum) SmithUseAbilityItemList.UI.OBJ_CAPTION_3);
    this.SetLabelText(ctrl, (Enum) SmithUseAbilityItemList.UI.LBL_CAPTION, StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 26U));
    UITweenCtrl component = ((Component) ctrl).gameObject.GetComponent<UITweenCtrl>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.Reset();
    int index = 0;
    for (int length = component.tweens.Length; index < length; ++index)
      component.tweens[index].ResetToBeginning();
    component.Play();
  }

  private SmithUseAbilityItemList.UI GetCurrentInventoryRoot()
  {
    return this.currentShowInventoryMode != SmithUseAbilityItemList.SHOW_INVENTORY_MODE.MAIN_STATUS ? SmithUseAbilityItemList.UI.GRD_INVENTORY_SMALL : SmithUseAbilityItemList.UI.GRD_INVENTORY;
  }

  private void ChangeSellModeSelectItem(AbilityItemSortData abilityItem)
  {
    if (this.sellItemData.Contains(abilityItem))
    {
      this.sellItemData.Remove(abilityItem);
      this.RefreshSelectSell();
      GameSection.StopEvent();
    }
    else if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.SELL_SELECT_MAX > this.sellItemData.Count)
    {
      this.sellItemData.Add(abilityItem);
      this.RefreshSelectSell();
      GameSection.StopEvent();
    }
    else
      GameSection.ChangeEvent("OVER_SELL_ITEM");
  }

  private void RefreshSelectSell()
  {
    foreach (ItemIcon componentsInChild in ((Component) this.GetCtrl((Enum) this.GetCurrentInventoryRoot())).GetComponentsInChildren<ItemIcon>())
    {
      ulong uniqueId = componentsInChild.GetUniqID;
      int index = this.sellItemData.FindIndex((Predicate<AbilityItemSortData>) (x => (long) x.GetUniqID() == (long) uniqueId));
      ((Component) componentsInChild.selectFrame).gameObject.SetActive(index >= 0);
      if (componentsInChild is ItemIconDetail)
        (componentsInChild as ItemIconDetail).setupperEquip.SetupSelectNumberSprite(index + 1);
      else
        (componentsInChild as ItemIconDetailSmall).SetupSelectNumberSprite(index + 1);
    }
    this.SetSellInfoView();
  }

  private void SetSellInfoView()
  {
    this.SetLabelText((Enum) SmithUseAbilityItemList.UI.LBL_MAX_SELECT_NUM, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.SELL_SELECT_MAX.ToString());
    this.SetLabelText((Enum) SmithUseAbilityItemList.UI.LBL_SELECT_NUM, this.sellItemData.Count.ToString());
    int num = 0;
    foreach (AbilityItemSortData abilityItemSortData in this.sellItemData)
      num += abilityItemSortData.itemData.GetItemTableData().price;
    this.SetLabelText((Enum) SmithUseAbilityItemList.UI.LBL_TOTAL, num.ToString());
  }

  private void OnCloseDialog_AbilityItemSellIncludeRareConfirm()
  {
    this.OnCloseDialog_AbilityItemSellConfirm();
  }

  private void OnCloseDialog_AbilityItemSellConfirm()
  {
    this.inventory = (ItemStorageTop.AbilityItemInventory) null;
    this.isSellMode = false;
    this.sellItemData.Clear();
    this.SetDirty((Enum) this.GetCurrentInventoryRoot());
    this.RefreshUI();
  }

  private void OnCloseDialog_ItemStorageAbilityItemSort() => this.CloseSort();

  protected void CloseSort()
  {
    if (!(GameSection.GetEventData() is SortSettings) || !this.inventory.Sort(GameSection.GetEventData() as SortSettings))
      return;
    this.SetDirty((Enum) SmithUseAbilityItemList.UI.GRD_INVENTORY);
    this.SetDirty((Enum) SmithUseAbilityItemList.UI.GRD_INVENTORY_SMALL);
    this.RefreshUI();
  }

  public enum UI
  {
    OBJ_CAPTION_3,
    LBL_CAPTION,
    SCR_INVENTORY,
    GRD_INVENTORY,
    GRD_INVENTORY_SMALL,
    TGL_CHANGE_INVENTORY,
    BTN_SELL_MODE_END,
    BTN_SELL,
    STR_HAVE_NUM,
    LBL_NOW_HAVE_NUM,
    LBL_MAX_HAVE_NUM,
    OBJ_BTN_SELL_MODE,
    OBJ_SELL_MODE_ROOT,
    LBL_TOTAL,
    LBL_MAX_SELECT_NUM,
    LBL_SELECT_NUM,
    LBL_SORT,
    TGL_ICON_ASC,
    BTN_CHANGE,
  }

  public enum SHOW_INVENTORY_MODE
  {
    MAIN_STATUS,
  }
}
