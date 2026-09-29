// Decompiled with JetBrains decompiler
// Type: SkillSelectBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public abstract class SkillSelectBase : ItemDetailSkill
{
  protected SkillItemInfo equipSkillItem;
  protected EquipItemInfo equipItem;
  protected ItemStorageTop.SkillItemInventory inventory;
  protected int selectIndex;
  protected SkillItemInfo selectSkillItem;
  protected bool updateInventory;
  protected int inventoryUIIndex;
  protected SkillSelectBase.UI inventoryUI;
  protected SkillSelectBase.UI[] switchInventoryAry = new SkillSelectBase.UI[1]
  {
    SkillSelectBase.UI.GRD_INVENTORY
  };

  protected bool IsShowMainStatus => this.inventoryUIIndex == 0;

  public override void Initialize()
  {
    if (GameSection.GetEventData() is object[] eventData && eventData.Length > 2)
    {
      this.equipSkillItem = eventData[1] as SkillItemInfo;
      this.equipItem = eventData[2] as EquipItemInfo;
    }
    GameSection.SetEventData((object) new object[2]
    {
      (object) (ItemDetailEquip.CURRENT_SECTION) eventData[0],
      null
    });
    base.Initialize();
  }

  protected override SkillItemInfo GetCompareItem() => this.equipSkillItem;

  public override void UpdateUI()
  {
    this.SetFontStyle((Enum) SkillSelectBase.UI.STR_TITLE_ITEM_INFO, (FontStyle) 2);
    this.SetFontStyle((Enum) SkillSelectBase.UI.STR_TITLE_DESCRIPTION, (FontStyle) 2);
    this.SetFontStyle((Enum) SkillSelectBase.UI.STR_TITLE_STATUS, (FontStyle) 2);
    this.SetFontStyle((Enum) SkillSelectBase.UI.STR_TITLE_SELL, (FontStyle) 2);
    this.SetActive((Enum) SkillSelectBase.UI.BTN_DECISION, true);
    this.SetActive((Enum) SkillSelectBase.UI.BTN_SKILL_DECISION, false);
    this.SetLabelText((Enum) SkillSelectBase.UI.STR_DECISION_R, this.sectionData.GetText("STR_DECISION"));
    this.SetActive((Enum) SkillSelectBase.UI.BTN_CHANGE_INVENTORY, false);
    if (this.inventory == null || this.updateInventory)
    {
      this.selectIndex = this.GetInventoryFirstIndex();
      this.inventory = this.CreateInventory();
      if (this.inventory.datas.Length != 0)
      {
        if (this.selectSkillItem == null)
        {
          if (this.equipSkillItem != null)
            this.selectIndex = this.GetSelectItemIndex(this.equipSkillItem);
          this.selectSkillItem = this.selectIndex < 0 ? (SkillItemInfo) null : this.inventory.datas[this.selectIndex].GetItemData() as SkillItemInfo;
        }
        else
          this.selectIndex = this.GetSelectItemIndex(this.selectSkillItem);
      }
      this.updateInventory = false;
    }
    this.SetInventoryIsEmptyParam();
    this.SetLabelText((Enum) SkillSelectBase.UI.LBL_SORT, this.inventory.sortSettings.GetSortLabel());
    this.SetToggle((Enum) SkillSelectBase.UI.TGL_ICON_ASC, this.inventory.sortSettings.orderTypeAsc);
    this.UpdateInventoryUI();
    this.UpdateParam();
  }

  protected virtual void SetInventoryIsEmptyParam()
  {
  }

  protected virtual void UpdateParam()
  {
    this.itemData = (object) this.selectSkillItem;
    base.UpdateUI();
    this.UpdateAnchors();
  }

  protected override void SetupDetailBase() => this._SetupSkillInfoRoot();

  protected void _SetupDetailBase()
  {
    this.SetActive((Enum) SkillSelectBase.UI.OBJ_SKILL_INFO_ROOT, false);
    base.SetupDetailBase();
  }

  protected void _SetupSkillInfoRoot()
  {
    this.SetActive((Enum) SkillSelectBase.UI.OBJ_SKILL_INFO_ROOT, true);
    this.detailBase = this.GetCtrl((Enum) SkillSelectBase.UI.OBJ_SKILL_INFO_ROOT);
  }

  protected virtual int GetInventoryFirstIndex() => 0;

  protected virtual ItemStorageTop.SkillItemInventory CreateInventory()
  {
    return new ItemStorageTop.SkillItemInventory(SortSettings.SETTINGS_TYPE.SKILL_ITEM);
  }

  protected virtual void UpdateInventoryUI()
  {
    this.SetupEnableInventoryUI();
    this.m_generatedIconList.Clear();
    this.UpdateNewIconInfo();
    this.SetDynamicList((Enum) this.inventoryUI, (string) null, this.inventory.datas.Length, false, (Func<int, bool>) (i =>
    {
      SortCompareData data = this.inventory.datas[i];
      return data != null && data.IsPriority(this.inventory.sortSettings.orderTypeAsc);
    }), (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      SortCompareData data = this.inventory.datas[i];
      if (data == null || !data.IsPriority(this.inventory.sortSettings.orderTypeAsc))
        return;
      ITEM_ICON_TYPE iconType = data.GetIconType();
      bool is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(iconType, data.GetUniqID());
      ItemIcon itemIconDetail = this.CreateItemIconDetail(iconType, data.GetIconID(), new RARITY_TYPE?(data.GetRarity()), data as SkillItemSortData, this.IsShowMainStatus, t, "SELECT", i, is_new, 100, this.selectIndex == i, data.IsEquipping(), data.IsExceeded());
      itemIconDetail.SetItemID(data.GetTableID());
      itemIconDetail.SetButtonColor(this.inventory.datas[i].IsPriority(this.inventory.sortSettings.orderTypeAsc), true);
      this.SetLongTouch(itemIconDetail.transform, "DETAIL", (object) i);
      if (Object.op_Inequality((Object) itemIconDetail, (Object) null) && data != null)
        itemIconDetail.SetInitData(data);
      if (this.m_generatedIconList.Contains(itemIconDetail))
        return;
      this.m_generatedIconList.Add(itemIconDetail);
    }));
  }

  protected void OnQuery_SORT()
  {
    if (this.inventory == null || this.inventory.datas.Length == 0)
      GameSection.StopEvent();
    else
      GameSection.SetEventData((object) this.inventory.sortSettings.Clone());
  }

  protected virtual void OnCloseSort()
  {
    SortSettings eventData = (SortSettings) GameSection.GetEventData();
    if (eventData == null)
      return;
    SortCompareData sortCompareData = (SortCompareData) null;
    if (this.selectIndex >= 0)
      sortCompareData = this.inventory.datas[this.selectIndex];
    if (!this.inventory.Sort(eventData))
      return;
    if (sortCompareData != null)
      this.selectIndex = this.GetSelectItemIndex(sortCompareData.GetItemData() as SkillItemInfo);
    this.SetDirty((Enum) SkillSelectBase.UI.GRD_INVENTORY);
    this.RefreshUI();
  }

  protected virtual void OnQuery_SELECT()
  {
    int eventData = (int) GameSection.GetEventData();
    if (eventData >= 0)
    {
      this.selectIndex = eventData;
      this.selectSkillItem = this.inventory.datas[this.selectIndex].GetItemData() as SkillItemInfo;
    }
    else
    {
      this.selectIndex = -1;
      this.selectSkillItem = (SkillItemInfo) null;
    }
    this.UpdateParam();
  }

  private void OnQuery_DETAIL()
  {
    GameSection.SetEventData((object) this.CreateDetailEventData((int) GameSection.GetEventData()));
  }

  protected void OnQuery_DECISION() => this.OnDecision();

  protected virtual void OnDecision()
  {
  }

  protected virtual object[] CreateDetailEventData(int index)
  {
    return new object[2]
    {
      (object) ItemDetailEquip.CURRENT_SECTION.STATUS_EQUIP,
      this.inventory.datas[index].GetItemData()
    };
  }

  protected int GetSelectItemIndex(SkillItemInfo select_item)
  {
    if (select_item != null)
    {
      int selectItemIndex = 0;
      for (int length = this.inventory.datas.Length; selectItemIndex < length; ++selectItemIndex)
      {
        if ((long) this.inventory.datas[selectItemIndex].GetUniqID() == (long) select_item.uniqueID)
          return selectItemIndex;
      }
    }
    return -1;
  }

  protected void SetupEnableInventoryUI()
  {
    int index = 0;
    for (int length = this.switchInventoryAry.Length; index < length; ++index)
      this.SetActive((Enum) this.switchInventoryAry[index], false);
    this.SetActive((Enum) this.switchInventoryAry[this.inventoryUIIndex], true);
    this.inventoryUI = this.switchInventoryAry[this.inventoryUIIndex];
    this.SetToggle((Enum) SkillSelectBase.UI.TGL_CHANGE_INVENTORY, this.inventoryUI == SkillSelectBase.UI.GRD_INVENTORY);
  }

  protected virtual void OnQuery_CHANGE_INVENTORY()
  {
    this.inventoryUIIndex = this.inventoryUIIndex + 1 < this.switchInventoryAry.Length ? this.inventoryUIIndex + 1 : 0;
    this.SetDirty((Enum) SkillSelectBase.UI.GRD_INVENTORY);
    this.SetDirty((Enum) SkillSelectBase.UI.GRD_INVENTORY_SMALL);
    this.RefreshUI();
  }

  protected ItemIcon CreateRemoveIcon(
    Transform parent = null,
    string event_name = null,
    int event_data = 0,
    int toggle_group = -1,
    bool is_select = false,
    string name = null)
  {
    return this.inventoryUI == SkillSelectBase.UI.GRD_INVENTORY ? ItemIconDetail.CreateRemoveButton(parent, event_name, event_data, toggle_group, is_select, name) : ItemIconDetailSmall.CreateSmallRemoveButton(parent, event_name, event_data, toggle_group, is_select, name);
  }

  protected ItemIcon CreateItemIconDetail(
    ITEM_ICON_TYPE icon_type,
    int icon_id,
    RARITY_TYPE? rarity,
    SkillItemSortData item_data,
    bool is_show_main_status,
    Transform parent = null,
    string event_name = null,
    int event_data = 0,
    bool is_new = false,
    int toggle_group = -1,
    bool is_select = false,
    bool is_equipping = false,
    bool isValidExceed = false,
    bool isShowEnableExceed = false)
  {
    return this.inventoryUI == SkillSelectBase.UI.GRD_INVENTORY ? ItemIconDetail.CreateSkillDetailIcon(icon_type, icon_id, rarity, item_data, is_show_main_status, parent, event_name, event_data, is_new, toggle_group, is_select, is_equipping, isValidExceed, isShowEnableExceed) : ItemIconDetailSmall.CreateSmallSkillDetailIcon(icon_type, icon_id, rarity, item_data, parent, event_name, event_data, is_new, toggle_group, is_select, is_equipping, isValidExceed, isShowEnableExceed);
  }

  protected new enum UI
  {
    OBJ_DETAIL_ROOT,
    TEX_MODEL,
    TEX_INNER_MODEL,
    LBL_NAME,
    LBL_LV_NOW,
    LBL_LV_MAX,
    LBL_ATK,
    LBL_DEF,
    LBL_HP,
    LBL_SELL,
    LBL_DESCRIPTION,
    OBJ_FAVORITE_ROOT,
    TWN_FAVORITE,
    TWN_UNFAVORITE,
    OBJ_SUB_STATUS,
    SPR_SKILL_TYPE_ICON,
    SPR_SKILL_TYPE_ICON_BG,
    SPR_SKILL_TYPE_ICON_RARITY,
    STR_TITLE_ITEM_INFO,
    STR_TITLE_DESCRIPTION,
    STR_TITLE_STATUS,
    STR_TITLE_SELL,
    PRG_EXP_BAR,
    OBJ_NEXT_EXP_ROOT,
    BTN_DECISION,
    STR_DECISION_R,
    BTN_SKILL_DECISION,
    STR_SKILL_DECISION,
    STR_SKILL_DECISION_R,
    OBJ_SKILL_INFO_ROOT,
    LBL_EQUIP_ITEM_NAME,
    SCR_INVENTORY,
    GRD_INVENTORY,
    GRD_INVENTORY_SMALL,
    LBL_SORT,
    BTN_BACK,
    TGL_CHANGE_INVENTORY,
    TGL_ICON_ASC,
    BTN_CHANGE_INVENTORY,
    OBJ_EMPTY_SKILL_ROOT,
    TEX_EMPTY_SKILL,
    SPR_EMPTY_SKILL,
    LBL_EMPTY_SKILL_TYPE,
    OBJ_CAPTION_3,
    LBL_CAPTION,
  }
}
