// Decompiled with JetBrains decompiler
// Type: ItemStorageTop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
public class ItemStorageTop : SkillInfoBase
{
  private const float DEFAULT_LAST_SCROLL_POS_Y = -9999.9f;
  private readonly ItemStorageTop.UI[] uiTab = new ItemStorageTop.UI[6]
  {
    ItemStorageTop.UI.TGL_TAB0,
    ItemStorageTop.UI.TGL_TAB1,
    ItemStorageTop.UI.TGL_TAB2,
    ItemStorageTop.UI.TGL_TAB3,
    ItemStorageTop.UI.TGL_TAB4,
    ItemStorageTop.UI.TGL_TAB5
  };
  protected ItemStorageTop.InventoryBase[] inventories = new ItemStorageTop.InventoryBase[6];
  protected ItemStorageTop.TAB_MODE tab;
  protected ItemStorageTop.TAB_MODE m_prevOpendTab;
  public List<SortCompareData> sellItemData = new List<SortCompareData>();
  protected bool isSellMode;
  private ItemStorageTop.UI inventoryUI;
  protected bool includeLithograph = true;
  protected bool includeMaterialItem = true;
  protected GET_TYPE? ItemGetType;
  protected ItemStorageSellConfirm.GO_BACK confirmTo;
  private Vector3 m_currentScrollTopPos = Vector3.zero;
  protected bool m_isEnableUpdateScrollTopPos = true;
  protected Vector3 m_lastScrollPosition = Vector3.op_Multiply(Vector3.up, -9999.9f);
  protected Vector3 m_scrollTopPosition = Vector3.zero;
  protected float m_maxScrollableValue;
  public ItemStorageTop.SHOW_INVENTORY_MODE showInventoryMode;
  private ItemStorageTop.TAB_MODE tmpTab = ItemStorageTop.TAB_MODE.MAX;
  private int toggleIndex = -1;

  public override void Exit()
  {
    MonoBehaviourSingleton<InventoryManager>.I.DoRemoveNewFragsAbilityItem();
    base.Exit();
  }

  protected void SetNextTab(ItemStorageTop.TAB_MODE _tab)
  {
    this.m_prevOpendTab = this.tab;
    this.tab = _tab;
  }

  protected Vector3 CurrentScrollTopPos => this.m_currentScrollTopPos;

  protected void UpdateCurrentScrollTopPos(Vector3 _newPos)
  {
    if (!this.m_isEnableUpdateScrollTopPos)
      return;
    this.m_isEnableUpdateScrollTopPos = false;
    this.m_currentScrollTopPos = _newPos;
  }

  public override void Initialize()
  {
    int index = 0;
    for (int length = this.uiTab.Length; index < length; ++index)
    {
      UIToggle component = this.GetComponent<UIToggle>((Enum) this.uiTab[index]);
      if (Object.op_Inequality((Object) component, (Object) null))
        component.onChange.Clear();
    }
    this.InitializeCaption();
    base.Initialize();
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return base.GetUpdateUINotifyFlags() | GameSection.NOTIFY_FLAG.UPDATE_ITEM_INVENTORY | GameSection.NOTIFY_FLAG.UPDATE_EQUIP_INVENTORY | GameSection.NOTIFY_FLAG.UPDATE_SKILL_INVENTORY | GameSection.NOTIFY_FLAG.UPDATE_QUEST_ITEM_INVENTORY | GameSection.NOTIFY_FLAG.UPDATE_SKILL_CHANGE | GameSection.NOTIFY_FLAG.UPDATE_EQUIP_FAVORITE | GameSection.NOTIFY_FLAG.UPDATE_SKILL_FAVORITE | GameSection.NOTIFY_FLAG.REMOVE_NEW_ICON | GameSection.NOTIFY_FLAG.UPDATE_INVENTORY_CAPACITY | GameSection.NOTIFY_FLAG.UPDATE_ABILITY_ITEM_INVENTORY | GameSection.NOTIFY_FLAG.UPDATE_EQUIP_GROW;
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_EQUIP_FAVORITE) != (GameSection.NOTIFY_FLAG) 0 || (flags & GameSection.NOTIFY_FLAG.UPDATE_SKILL_FAVORITE) != (GameSection.NOTIFY_FLAG) 0)
    {
      this.inventories[(int) this.tab] = (ItemStorageTop.InventoryBase) null;
      this.SetDirty((Enum) this.SelectListTarget(this.tab, this.showInventoryMode));
      this.SaveCurrentScrollPosition();
    }
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_ITEM_INVENTORY) != (GameSection.NOTIFY_FLAG) 0)
    {
      this.inventories[0] = (ItemStorageTop.InventoryBase) null;
      this.inventories[1] = (ItemStorageTop.InventoryBase) null;
      this.inventories[4] = (ItemStorageTop.InventoryBase) null;
      this.SetDirty((Enum) this.SelectListTarget(this.tab, this.showInventoryMode));
    }
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_EQUIP_INVENTORY) != (GameSection.NOTIFY_FLAG) 0 || (flags & GameSection.NOTIFY_FLAG.UPDATE_SKILL_CHANGE) != (GameSection.NOTIFY_FLAG) 0)
    {
      this.inventories[2] = (ItemStorageTop.InventoryBase) null;
      this.inventories[3] = (ItemStorageTop.InventoryBase) null;
      this.SetDirty((Enum) this.SelectListTarget(this.tab, this.showInventoryMode));
    }
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_SKILL_INVENTORY) != (GameSection.NOTIFY_FLAG) 0)
    {
      this.inventories[3] = (ItemStorageTop.InventoryBase) null;
      this.SetDirty((Enum) this.SelectListTarget(this.tab, this.showInventoryMode));
    }
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_ABILITY_ITEM_INVENTORY) != (GameSection.NOTIFY_FLAG) 0)
    {
      this.inventories[0] = (ItemStorageTop.InventoryBase) null;
      this.SetDirty((Enum) this.SelectListTarget(this.tab, this.showInventoryMode));
    }
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_EQUIP_GROW) != (GameSection.NOTIFY_FLAG) 0)
    {
      this.inventories[5] = (ItemStorageTop.InventoryBase) null;
      this.SetDirty((Enum) this.SelectListTarget(this.tab, this.showInventoryMode));
    }
    base.OnNotify(flags);
  }

  public override void UpdateUI()
  {
    base.UpdateUI();
    int index1 = 0;
    for (int length = this.uiTab.Length; index1 < length; ++index1)
    {
      UIToggle component1 = this.GetComponent<UIToggle>((Enum) this.uiTab[index1]);
      if (Object.op_Inequality((Object) component1, (Object) null))
        component1.value = (ItemStorageTop.TAB_MODE) index1 == this.tab;
      UIToggledComponents component2 = this.GetComponent<UIToggledComponents>((Enum) this.uiTab[index1]);
      if (Object.op_Inequality((Object) component2, (Object) null))
      {
        UIToggle.current = component1;
        component2.Toggle();
      }
    }
    UIToggle.current = (UIToggle) null;
    this.SetToggle((Enum) ItemStorageTop.UI.TGL_CHANGE_INVENTORY, true);
    ItemStorageTop.InventoryBase inventory = this.inventories[(int) this.tab];
    if (inventory == null)
    {
      switch (this.tab)
      {
        case ItemStorageTop.TAB_MODE.MATERIAL:
          this.inventories[(int) this.tab] = (ItemStorageTop.InventoryBase) new ItemStorageTop.MaterialInventory(this.includeMaterialItem, this.includeLithograph, false, this.ItemGetType);
          break;
        case ItemStorageTop.TAB_MODE.USE_ITEM:
          this.inventories[(int) this.tab] = (ItemStorageTop.InventoryBase) new ItemStorageTop.UseItemInventory();
          break;
        case ItemStorageTop.TAB_MODE.EQUIP:
          this.inventories[(int) this.tab] = (ItemStorageTop.InventoryBase) new ItemStorageTop.EquipItemInventory(this.ItemGetType);
          break;
        case ItemStorageTop.TAB_MODE.SKILL:
          this.inventories[(int) this.tab] = (ItemStorageTop.InventoryBase) new ItemStorageTop.SkillItemInventory();
          break;
        case ItemStorageTop.TAB_MODE.LAPIS:
          this.inventories[(int) this.tab] = (ItemStorageTop.InventoryBase) new ItemStorageTop.MaterialInventory(false, false, true);
          break;
      }
      inventory = this.inventories[(int) this.tab];
      this.inventoryUI = this.SelectListTarget(this.tab, this.showInventoryMode);
      if (this.isSellMode && (this.tab == ItemStorageTop.TAB_MODE.EQUIP || this.tab == ItemStorageTop.TAB_MODE.SKILL) && this.sellItemData.Count != 0 && this.inventories[(int) this.tab] != null && this.inventories[(int) this.tab].datas.Length != 0)
      {
        List<SortCompareData> find_data = new List<SortCompareData>();
        this.sellItemData.ForEach((Action<SortCompareData>) (sort_data =>
        {
          int index2 = 0;
          for (int length = this.inventories[(int) this.tab].datas.Length; index2 < length; ++index2)
          {
            if ((long) sort_data.GetUniqID() == (long) this.inventories[(int) this.tab].datas[index2].GetUniqID())
              return;
          }
          find_data.Add(sort_data);
        }));
        int index3 = 0;
        for (int length = this.inventories[(int) this.tab].datas.Length; index3 < length; ++index3)
        {
          SortCompareData data = this.inventories[(int) this.tab].datas[index3];
          if (data.IsFavorite() && find_data.IndexOf(data) == -1)
            find_data.Add(data);
        }
        if (find_data.Count > 0)
        {
          find_data.ForEach((Action<SortCompareData>) (item => this.sellItemData.RemoveAll((Predicate<SortCompareData>) (_item => (long) item.GetUniqID() == (long) _item.GetUniqID()))));
          this.SetDirty((Enum) this.inventoryUI);
        }
      }
    }
    this.SetActive((Enum) ItemStorageTop.UI.BTN_SORT, true);
    this.SetLabelText((Enum) ItemStorageTop.UI.LBL_SORT, inventory.sortSettings.GetSortLabel());
    this.SetToggle((Enum) ItemStorageTop.UI.TGL_ICON_ASC, inventory.sortSettings.orderTypeAsc);
    bool flag = this.tab != ItemStorageTop.TAB_MODE.USE_ITEM && this.tab != ItemStorageTop.TAB_MODE.LAPIS;
    this.SetActive((Enum) ItemStorageTop.UI.BTN_SORT, this.tab != ItemStorageTop.TAB_MODE.ACCESSORY && flag);
    this.SetActive((Enum) ItemStorageTop.UI.SPR_INVALID_SORT, this.tab != ItemStorageTop.TAB_MODE.ACCESSORY && !flag);
    this.SetLabelText((Enum) ItemStorageTop.UI.LBL_INVALID_SORT, inventory.sortSettings.GetSortLabel());
    this.m_generatedIconList.Clear();
    this.UpdateNewIconInfo();
    bool is_visible = this.tab != ItemStorageTop.TAB_MODE.SKILL && this.tab != ItemStorageTop.TAB_MODE.ACCESSORY;
    this.SetActive((Enum) ItemStorageTop.UI.BTN_CHANGE, is_visible);
    this.SetActive((Enum) ItemStorageTop.UI.SPR_INVALID_CHANGE, !is_visible);
    int sortedItemCount = 0;
    this.SetDynamicList((Enum) this.inventoryUI, (string) null, inventory.datas.Length, false, (Func<int, bool>) (i =>
    {
      SortCompareData data = inventory.datas[i];
      if (data == null || !data.IsPriority(inventory.sortSettings.orderTypeAsc))
        return false;
      ++sortedItemCount;
      return true;
    }), (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycre) =>
    {
      SortCompareData data = inventory.datas[i];
      ItemIcon icon = inventory.CreateIcon(new object[4]
      {
        (object) data,
        (object) t,
        (object) i,
        (object) this
      });
      if (!Object.op_Inequality((Object) icon, (Object) null))
        return;
      icon.toggleSelectFrame.onChange.Clear();
      icon.toggleSelectFrame.onChange.Add(new EventDelegate((MonoBehaviour) this, "IconToggleChange"));
      this.InitListItemEvent(icon, i, data);
      icon.SetGrayout(this.IsRequiredIconGrayOut(data));
      icon.SetInitData(data);
      if (this.m_generatedIconList.Contains(icon))
        return;
      this.m_generatedIconList.Add(icon);
    }));
    this.InitSellObjectMode();
    this.m_maxScrollableValue = this.CalcMaxScrollableValue(sortedItemCount);
    this.UpdateScrollSettings();
    this.UpdateAnchors();
  }

  protected virtual void InitListItemEvent(ItemIcon icon, int i, SortCompareData item)
  {
    this.SetLongTouch(icon.transform, "DETAIL", (object) i);
  }

  protected virtual void InitSellObjectMode()
  {
    if (this.tab == ItemStorageTop.TAB_MODE.EQUIP || this.tab == ItemStorageTop.TAB_MODE.SKILL)
    {
      if (this.isSellMode)
      {
        this.UpdateSellGoldAndExp();
        this.SetActive((Enum) ItemStorageTop.UI.OBJ_SELL_MODE_ROOT, true);
        this.SetActive((Enum) ItemStorageTop.UI.OBJ_BTN_SELL_MODE, false);
      }
      else
      {
        this.SetActive((Enum) ItemStorageTop.UI.OBJ_BTN_SELL_MODE, MonoBehaviourSingleton<ItemExchangeManager>.I.IsExchangeScene());
        this.SetActive((Enum) ItemStorageTop.UI.OBJ_SELL_MODE_ROOT, false);
      }
    }
    else
    {
      this.SetActive((Enum) ItemStorageTop.UI.OBJ_BTN_SELL_MODE, false);
      this.SetActive((Enum) ItemStorageTop.UI.OBJ_SELL_MODE_ROOT, false);
    }
    int num = 0;
    int now_num = 0;
    switch (this.tab)
    {
      case ItemStorageTop.TAB_MODE.EQUIP:
        num = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.maxEquipItem;
        MonoBehaviourSingleton<InventoryManager>.I.ForAllEquipItemInventory((Action<EquipItemInfo>) (_equip =>
        {
          if (_equip == null || _equip.uniqueID == 0UL || _equip.tableID == 0U)
            return;
          ++now_num;
        }));
        break;
      case ItemStorageTop.TAB_MODE.SKILL:
        num = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.maxSkillItem;
        MonoBehaviourSingleton<InventoryManager>.I.ForAllSkillItemInventory((Action<SkillItemInfo>) (_skill =>
        {
          if (_skill == null || _skill.uniqueID == 0UL || _skill.tableID == 0U)
            return;
          ++now_num;
        }));
        break;
    }
    this.SetLabelText((Enum) ItemStorageTop.UI.LBL_MAX_HAVE_NUM, num.ToString());
    this.SetLabelText((Enum) ItemStorageTop.UI.LBL_NOW_HAVE_NUM, now_num.ToString());
  }

  private void Update() => this.ObserveItemList();

  protected virtual bool IsRequiredIconGrayOut(SortCompareData _data)
  {
    return _data.GetNum() == 0 || this.isSellMode && _data.IsFavorite() || this.isSellMode && _data.IsEquipping() && this.tab == ItemStorageTop.TAB_MODE.EQUIP;
  }

  private void UpdateSellGoldAndExp()
  {
    int total_gold = 0;
    int num = 0;
    if (this.sellItemData.Count > 0)
    {
      num = this.sellItemData.Count;
      this.sellItemData.ForEach((Action<SortCompareData>) (sort_data => total_gold += sort_data.GetSalePrice()));
    }
    this.SetLabelText((Enum) ItemStorageTop.UI.LBL_SELECT_NUM, num.ToString());
    this.SetLabelText((Enum) ItemStorageTop.UI.LBL_MAX_SELECT_NUM, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.SELL_SELECT_MAX.ToString());
    this.SetLabelText((Enum) ItemStorageTop.UI.LBL_TOTAL, total_gold.ToString());
  }

  public override EventData CheckAutoEvent(string event_name, object event_data)
  {
    switch (event_name)
    {
      case "TAB_2":
        if (this.tab == ItemStorageTop.TAB_MODE.EQUIP)
          return new EventData((string) null, (object) null);
        break;
      case "SELECT":
        ItemStorageTop.InventoryBase inventory = this.inventories[(int) this.tab];
        ulong uniq_id = (ulong) event_data;
        int index = Array.FindIndex<SortCompareData>(inventory.datas, (Predicate<SortCompareData>) (o => (long) o.GetUniqID() == (long) uniq_id));
        return new EventData(event_name, (object) index);
    }
    return base.CheckAutoEvent(event_name, event_data);
  }

  protected void ChangeTab(ItemStorageTop.TAB_MODE t)
  {
    if (this.isSellMode && this.sellItemData.Count > 0 && this.tab != t)
    {
      this.tmpTab = t;
      GameSection.ChangeEvent("SELL_CHANGE_TAB");
    }
    else
    {
      this.tmpTab = ItemStorageTop.TAB_MODE.MAX;
      if (this.tab == t)
        return;
      this.sellItemData.Clear();
      this.isSellMode = false;
      this.SetNextTab(t);
      this.m_lastScrollPosition = Vector3.op_Multiply(Vector3.up, -9999.9f);
      this.m_isEnableUpdateScrollTopPos = true;
      this.inventoryUI = this.SelectListTarget(this.tab, this.showInventoryMode);
      this.SetDirty((Enum) this.inventoryUI);
      this.RefreshUI();
    }
  }

  protected void OnQuery_ItemStorageChangeTabMultiSell_YES()
  {
    this.isSellMode = false;
    this.ChangeTab(this.tmpTab);
  }

  protected void OnQuery_ItemExchangeChangeTabMultiSell_YES()
  {
    this.isSellMode = false;
    this.ChangeTab(this.tmpTab);
  }

  private void OnQuery_TAB_0() => this.ChangeTab(ItemStorageTop.TAB_MODE.MATERIAL);

  private void OnQuery_TAB_1() => this.ChangeTab(ItemStorageTop.TAB_MODE.USE_ITEM);

  private void OnQuery_TAB_2() => this.ChangeTab(ItemStorageTop.TAB_MODE.EQUIP);

  private void OnQuery_TAB_3() => this.ChangeTab(ItemStorageTop.TAB_MODE.SKILL);

  private void OnQuery_TAB_4() => this.ChangeTab(ItemStorageTop.TAB_MODE.LAPIS);

  private void OnQuery_SORT()
  {
    if (this.tab == ItemStorageTop.TAB_MODE.EQUIP || this.tab == ItemStorageTop.TAB_MODE.SKILL)
      GameSection.ChangeEvent("EQUIP_SORT");
    else if (this.tab == ItemStorageTop.TAB_MODE.ACCESSORY)
      GameSection.ChangeEvent("ACCESSORY_SORT");
    GameSection.SetEventData((object) this.inventories[(int) this.tab].sortSettings.Clone());
  }

  private void OnCloseDialog_ItemStorageSort() => this.CloseSort();

  private void OnCloseDialog_ItemStorageEquipSort() => this.CloseSort();

  protected void CloseSort()
  {
    if (!(GameSection.GetEventData() is SortSettings) || !this.inventories[(int) this.tab].Sort(GameSection.GetEventData() as SortSettings))
      return;
    this.SetCurrentScrollPositionOnTop();
    this.SetDirty((Enum) ItemStorageTop.UI.GRD_INVENTORY);
    this.SetDirty((Enum) ItemStorageTop.UI.GRD_INVENTORY_SMALL);
    this.SetDirty((Enum) ItemStorageTop.UI.GRD_INVENTORY_EQUIP);
    this.SetDirty((Enum) ItemStorageTop.UI.GRD_INVENTORY_EQUIP_SMALL);
    this.RefreshUI();
  }

  private void OnQuery_DETAIL() => this.ToDetail();

  protected virtual void ToDetail()
  {
    this.SaveCurrentScrollPosition();
    int eventData = (int) GameSection.GetEventData();
    if (this.tab == ItemStorageTop.TAB_MODE.EQUIP)
      GameSection.ChangeEvent("EQUIP_SELECT", (object) new object[2]
      {
        (object) ItemDetailEquip.CURRENT_SECTION.ITEM_STORAGE,
        (object) this.inventories[(int) this.tab].datas[eventData]
      });
    else if (this.tab == ItemStorageTop.TAB_MODE.SKILL)
      GameSection.ChangeEvent("SKILL_SELECT", (object) new object[2]
      {
        (object) ItemDetailEquip.CURRENT_SECTION.ITEM_STORAGE,
        (object) this.inventories[(int) this.tab].datas[eventData]
      });
    else if (this.tab == ItemStorageTop.TAB_MODE.USE_ITEM)
    {
      SortCompareData data = this.inventories[(int) this.tab].datas[eventData];
      if (this.IsItemTypeEquipSetExt(data))
        GameSection.ChangeEvent("EQUIP_SET_EXT_SELECT", (object) data);
      else
        GameSection.ChangeEvent("USE_ITEM_SELECT", (object) data);
    }
    else if (this.tab == ItemStorageTop.TAB_MODE.LAPIS)
      GameSection.ChangeEvent("SELECT", (object) this.inventories[(int) this.tab].datas[eventData]);
    else if (this.tab == ItemStorageTop.TAB_MODE.ACCESSORY)
      GameSection.ChangeEvent("ACCESSORY_SELECT", (object) new object[2]
      {
        (object) ItemDetailEquip.CURRENT_SECTION.ITEM_STORAGE,
        (object) this.inventories[(int) this.tab].datas[eventData]
      });
    else
      GameSection.ChangeEvent("SELECT", (object) this.inventories[(int) this.tab].datas[eventData]);
  }

  private bool IsItemTypeEquipSetExt(SortCompareData data)
  {
    bool flag = false;
    if (data.GetItemData() is ItemInfo itemData && itemData.tableData != null && itemData.tableData.type == ITEM_TYPE.EQUIP_SET_EXT)
      flag = true;
    return flag;
  }

  private void OnQuery_SELECT()
  {
    int eventData = (int) GameSection.GetEventData();
    if (this.isSellMode)
    {
      SortCompareData select_data = this.inventories[(int) this.tab].datas[eventData];
      SortCompareData sortCompareData = this.sellItemData.Find((Predicate<SortCompareData>) (data => (long) data.GetUniqID() == (long) select_data.GetUniqID()));
      if (this.sellItemData.Count >= MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.SELL_SELECT_MAX && sortCompareData == null)
      {
        this.toggleIndex = eventData;
        GameSection.ChangeEvent("MULTI_SELL_MAX");
      }
      else if (!select_data.CanSale())
      {
        if (select_data.IsFavorite())
          GameSection.ChangeEvent("NOT_SELL_FAVORITE");
        else if (select_data.IsHomeEquipping())
          GameSection.ChangeEvent("NOT_SELL_EQUIPPING");
        else
          GameSection.ChangeEvent("NOT_SELL_UNIQUE_EQUIPPING");
        this.toggleIndex = eventData;
      }
      else if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.SKILL_EQUIP) && select_data.GetTableID() == 10000000U)
      {
        GameSection.ChangeEvent("NOT_SELL_DEFAULT_WEAPON");
      }
      else
      {
        this.ResetSelectSellIcon();
        if (sortCompareData != null)
          this.sellItemData.Remove(sortCompareData);
        else
          this.sellItemData.Add(select_data);
        GameSection.StopEvent();
        this.UpdateSellGoldAndExp();
        this.UpdateSelectSellIcon();
      }
    }
    else
      this.ToDetail();
  }

  private void ResetSelectSellIcon() => this._UpdateSelectSellIcon(true);

  private void UpdateSelectSellIcon() => this._UpdateSelectSellIcon(false);

  private void _UpdateSelectSellIcon(bool reset)
  {
    if (this.sellItemData == null || this.sellItemData.Count == 0)
      return;
    Transform ui = this.GetCtrl((Enum) this.inventoryUI);
    int select_index = reset ? -1 : 1;
    this.sellItemData.ForEach((Action<SortCompareData>) (material =>
    {
      int num = -1;
      if (this.inventories[(int) this.tab] != null)
      {
        int index = 0;
        for (int length = this.inventories[(int) this.tab].datas.Length; index < length; ++index)
        {
          if ((long) this.inventories[(int) this.tab].datas[index].GetUniqID() == (long) material.GetUniqID())
          {
            num = index;
            break;
          }
        }
      }
      if (num == -1)
        return;
      ItemIconDetail componentInChildren = ((Component) ui.GetChild(num)).GetComponentInChildren<ItemIconDetail>();
      if (Object.op_Inequality((Object) componentInChildren, (Object) null))
      {
        if (this.tab == ItemStorageTop.TAB_MODE.EQUIP)
          componentInChildren.setupperEquip.SetupSelectNumberSprite(select_index);
        else if (this.tab == ItemStorageTop.TAB_MODE.SKILL)
          componentInChildren.setupperSkill.SetupSelectNumberSprite(select_index);
      }
      if (reset)
        return;
      ++select_index;
    }));
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
    {
      GameSection.ChangeEvent("SELL_NOT_SELECT");
    }
    else
    {
      this.SaveCurrentScrollPosition();
      GameSection.SetEventData((object) new object[3]
      {
        (object) this.tab,
        (object) this.sellItemData,
        (object) this.confirmTo
      });
    }
  }

  private void OnCloseDialog_ItemStorageSellConfirm() => this.OnCloseItemStorageSellConfirm();

  private void OnCloseDialog_ItemStorageSellIncludeRareConfirm()
  {
    this.OnCloseItemStorageSellConfirm();
  }

  private void OnCloseItemStorageSellConfirm()
  {
    if (GameSection.GetEventData() is List<SortCompareData>)
      return;
    this.sellItemData.Clear();
    this.RefreshUI();
  }

  public void IconToggleChange()
  {
    if (this.toggleIndex == -1)
      return;
    ((Component) this.GetChild((Enum) this.inventoryUI, this.toggleIndex)).GetComponentInChildren<UIToggle>().value = false;
    this.toggleIndex = -1;
  }

  private ItemStorageTop.SHOW_INVENTORY_MODE GetNextContentMode()
  {
    return this.tab != ItemStorageTop.TAB_MODE.SKILL ? (this.showInventoryMode + 1 != ItemStorageTop.SHOW_INVENTORY_MODE.MAX ? this.showInventoryMode + 1 : ItemStorageTop.SHOW_INVENTORY_MODE.MAIN_STATUS) : ItemStorageTop.SHOW_INVENTORY_MODE.MAIN_STATUS;
  }

  private void OnQuery_CHANGE_INVENTORY()
  {
    this.showInventoryMode = this.GetNextContentMode();
    switch (this.tab)
    {
      case ItemStorageTop.TAB_MODE.EQUIP:
      case ItemStorageTop.TAB_MODE.SKILL:
        this.SetDirty((Enum) ItemStorageTop.UI.GRD_INVENTORY_EQUIP);
        this.SetDirty((Enum) ItemStorageTop.UI.GRD_INVENTORY_EQUIP_SMALL);
        this.inventoryUI = ItemStorageTop.UI.GRD_INVENTORY_EQUIP;
        break;
      default:
        this.SetDirty((Enum) ItemStorageTop.UI.GRD_INVENTORY);
        this.SetDirty((Enum) ItemStorageTop.UI.GRD_INVENTORY_SMALL);
        this.inventoryUI = ItemStorageTop.UI.GRD_INVENTORY;
        break;
    }
    this.inventories[(int) this.tab] = (ItemStorageTop.InventoryBase) null;
    this.RefreshUI();
  }

  private ItemStorageTop.UI SelectListTarget(
    ItemStorageTop.TAB_MODE tab,
    ItemStorageTop.SHOW_INVENTORY_MODE show_detail_icon)
  {
    this.SetActive((Enum) ItemStorageTop.UI.SCR_INVENTORY, false);
    this.SetActive((Enum) ItemStorageTop.UI.SCR_INVENTORY_EQUIP, false);
    this.SetActive((Enum) ItemStorageTop.UI.GRD_INVENTORY, false);
    this.SetActive((Enum) ItemStorageTop.UI.GRD_INVENTORY_SMALL, false);
    this.SetActive((Enum) ItemStorageTop.UI.GRD_INVENTORY_EQUIP, false);
    this.SetActive((Enum) ItemStorageTop.UI.GRD_INVENTORY_EQUIP_SMALL, false);
    this.SetActive((Enum) ItemStorageTop.UI.SPR_SCR_BAR, false);
    this.SetActive((Enum) ItemStorageTop.UI.SPR_EQUIP_SCR_BAR, false);
    bool flag = false;
    if (MonoBehaviourSingleton<ItemExchangeManager>.I.IsExchangeScene() && (tab == ItemStorageTop.TAB_MODE.EQUIP || tab == ItemStorageTop.TAB_MODE.SKILL))
      flag = true;
    if (flag)
    {
      this.SetActive((Enum) ItemStorageTop.UI.SCR_INVENTORY_EQUIP, true);
      this.SetActive((Enum) ItemStorageTop.UI.SPR_EQUIP_SCR_BAR, true);
      this.SetActive((Enum) ItemStorageTop.UI.GRD_INVENTORY_EQUIP, true);
      return ItemStorageTop.UI.GRD_INVENTORY_EQUIP;
    }
    this.SetActive((Enum) ItemStorageTop.UI.SCR_INVENTORY, true);
    this.SetActive((Enum) ItemStorageTop.UI.SPR_SCR_BAR, true);
    this.SetActive((Enum) ItemStorageTop.UI.GRD_INVENTORY, true);
    return ItemStorageTop.UI.GRD_INVENTORY;
  }

  protected virtual void InitializeCaption()
  {
    Transform ctrl = this.GetCtrl((Enum) ItemStorageTop.UI.OBJ_CAPTION_3);
    string text = this.sectionData.GetText("CAPTION");
    this.SetLabelText(ctrl, (Enum) ItemStorageTop.UI.LBL_CAPTION, text);
    UITweenCtrl component = ((Component) ctrl).gameObject.GetComponent<UITweenCtrl>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.Reset();
    int index = 0;
    for (int length = component.tweens.Length; index < length; ++index)
      component.tweens[index].ResetToBeginning();
    component.Play();
  }

  protected float CalcMaxScrollableValue(int allItemCount)
  {
    if (allItemCount < 1)
      return 0.0f;
    Transform scrollViewController = this.GetScrollViewController(this.tab);
    if (Object.op_Equality((Object) scrollViewController, (Object) null))
      return 0.0f;
    UIPanel component = ((Component) scrollViewController).GetComponent<UIPanel>();
    if (Object.op_Equality((Object) component, (Object) null))
      return 0.0f;
    float num1 = 0.0f;
    int num2 = 0;
    UIGrid[] componentsInChildren = ((Component) scrollViewController).GetComponentsInChildren<UIGrid>(true);
    for (int index = 0; index < componentsInChildren.Length; ++index)
    {
      if (((Component) componentsInChildren[index]).gameObject.activeInHierarchy)
      {
        num1 = componentsInChildren[index].cellHeight;
        num2 = componentsInChildren[index].maxPerLine;
        break;
      }
    }
    if ((double) num1 <= 0.0 || num2 <= 0)
      return 0.0f;
    int num3 = Mathf.CeilToInt((float) allItemCount / (float) num2);
    int num4 = Mathf.FloorToInt(component.baseClipRegion.w / num1);
    return num3 <= num4 ? 0.0f : num1 * (float) (num3 - num4);
  }

  protected void UpdateScrollSettings()
  {
    Transform scrollViewController = this.GetScrollViewController(this.tab);
    if (Object.op_Equality((Object) scrollViewController, (Object) null))
      return;
    this.UpdateCurrentScrollTopPos(scrollViewController.localPosition);
    if ((double) this.m_lastScrollPosition.y <= (double) this.CurrentScrollTopPos.y)
      return;
    float num1 = this.m_maxScrollableValue + this.CurrentScrollTopPos.y;
    float y = scrollViewController.localPosition.y;
    ItemStorageTop.UI scrollViewUi = this.GetScrollViewUI(this.tab);
    float num2 = this.m_lastScrollPosition.y - y;
    if ((double) num2 > (double) this.m_maxScrollableValue)
      num2 = this.m_maxScrollableValue;
    if ((double) y >= (double) num1 || (double) y + (double) num2 >= (double) num1)
      num2 = num1 - y;
    this.MoveRelativeScrollView((Enum) scrollViewUi, Vector3.op_Multiply(Vector3.up, num2));
  }

  protected Transform GetScrollViewController(ItemStorageTop.TAB_MODE _tab)
  {
    Transform scrollViewController = (Transform) null;
    switch (_tab)
    {
      case ItemStorageTop.TAB_MODE.MATERIAL:
      case ItemStorageTop.TAB_MODE.LAPIS:
      case ItemStorageTop.TAB_MODE.ACCESSORY:
        scrollViewController = this.GetCtrl((Enum) ItemStorageTop.UI.SCR_INVENTORY);
        break;
      case ItemStorageTop.TAB_MODE.EQUIP:
      case ItemStorageTop.TAB_MODE.SKILL:
        scrollViewController = this.GetCtrl((Enum) ItemStorageTop.UI.SCR_INVENTORY_EQUIP);
        break;
    }
    return scrollViewController;
  }

  private ItemStorageTop.UI GetScrollViewUI(ItemStorageTop.TAB_MODE _tab)
  {
    ItemStorageTop.UI scrollViewUi = ItemStorageTop.UI.SCR_INVENTORY;
    switch (_tab)
    {
      case ItemStorageTop.TAB_MODE.MATERIAL:
      case ItemStorageTop.TAB_MODE.LAPIS:
      case ItemStorageTop.TAB_MODE.ACCESSORY:
        scrollViewUi = ItemStorageTop.UI.SCR_INVENTORY;
        break;
      case ItemStorageTop.TAB_MODE.EQUIP:
      case ItemStorageTop.TAB_MODE.SKILL:
        scrollViewUi = ItemStorageTop.UI.SCR_INVENTORY_EQUIP;
        break;
    }
    return scrollViewUi;
  }

  protected void SaveCurrentScrollPosition()
  {
    Transform scrollViewController = this.GetScrollViewController(this.tab);
    if (Object.op_Equality((Object) scrollViewController, (Object) null))
      return;
    this.m_lastScrollPosition = scrollViewController.localPosition;
  }

  protected void SetCurrentScrollPositionOnTop()
  {
    if (Vector3.op_Equality(this.CurrentScrollTopPos, Vector3.zero))
      return;
    this.m_lastScrollPosition = this.CurrentScrollTopPos;
  }

  private enum UI
  {
    SCR_INVENTORY,
    GRD_INVENTORY,
    GRD_INVENTORY_SMALL,
    SPR_SCR_BAR,
    SCR_INVENTORY_EQUIP,
    GRD_INVENTORY_EQUIP,
    GRD_INVENTORY_EQUIP_SMALL,
    SPR_EQUIP_SCR_BAR,
    TGL_CHANGE_INVENTORY,
    TGL_ICON_ASC,
    LBL_SORT,
    BTN_SORT,
    SPR_INVALID_SORT,
    LBL_INVALID_SORT,
    BTN_CHANGE,
    SPR_INVALID_CHANGE,
    TGL_TAB0,
    TGL_TAB1,
    TGL_TAB2,
    TGL_TAB3,
    TGL_TAB4,
    TGL_TAB5,
    OBJ_BTN_SELL_MODE,
    OBJ_SELL_MODE_ROOT,
    LBL_MAX_SELECT_NUM,
    LBL_SELECT_NUM,
    LBL_TOTAL,
    LBL_MAX_HAVE_NUM,
    LBL_NOW_HAVE_NUM,
    OBJ_CAPTION_3,
    LBL_CAPTION,
  }

  private enum POW_TYPE
  {
    NONE,
    ATK,
    DEF,
    EXP_UP,
    MONEY_UP,
  }

  public enum TAB_MODE
  {
    MATERIAL,
    USE_ITEM,
    EQUIP,
    SKILL,
    LAPIS,
    ACCESSORY,
    MAX,
  }

  public class InventoryBase
  {
    public SortSettings sortSettings;
    public SortCompareData[] datas;

    public bool Sort(SortSettings sort_settings)
    {
      this.sortSettings = sort_settings;
      return this.DoSort();
    }

    protected virtual bool DoSort() => false;

    public virtual ItemIcon CreateIcon(object[] data) => (ItemIcon) null;
  }

  public class MaterialInventory : ItemStorageTop.InventoryBase
  {
    public MaterialInventory(
      bool include_material,
      bool include_lithograph,
      bool include_lapis,
      GET_TYPE? get_type = null)
    {
      List<ItemInfo> item_inventory = new List<ItemInfo>();
      if (!include_lapis)
      {
        this.sortSettings = SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.MATERIAL, SortSettings.SETTINGS_TYPE.MATERIAL);
      }
      else
      {
        this.sortSettings = new SortSettings();
        this.sortSettings.requirement = SortBase.SORT_REQUIREMENT.NUM;
      }
      MonoBehaviourSingleton<InventoryManager>.I.ForAllItemInventory((Action<ItemInfo>) (item_data =>
      {
        if (item_data.num <= 0 || item_data.tableData.type == ITEM_TYPE.USE_ITEM || item_data.tableData.type == ITEM_TYPE.DELIVERY || item_data.tableData.type == ITEM_TYPE.EQUIP_SET_EXT || !include_material && item_data.tableData.type != ITEM_TYPE.LITHOGRAPH && item_data.tableData.type != ITEM_TYPE.LAPIS || !include_lithograph && item_data.tableData.type == ITEM_TYPE.LITHOGRAPH || !include_lapis && item_data.tableData.type == ITEM_TYPE.LAPIS)
          return;
        if (get_type.HasValue)
        {
          GET_TYPE getType = get_type.Value;
          if (getType == GET_TYPE.PAY && item_data.tableData.getType != GET_TYPE.PAY || getType != GET_TYPE.PAY && item_data.tableData.getType == GET_TYPE.PAY)
            return;
        }
        if (item_data.tableData.type == ITEM_TYPE.TICKET || item_data.tableData.type == ITEM_TYPE.ABILITY_ITEM || item_data.tableData.type == ITEM_TYPE.EVENT_POINT || item_data.tableData.type == ITEM_TYPE.FORTUNE_TICKET)
          return;
        item_inventory.Add(item_data);
      }));
      if (include_lapis)
      {
        foreach (ItemTable.ItemData itemData in Singleton<ItemTable>.I.GetItemTypeItemData(ITEM_TYPE.LAPIS))
        {
          ItemTable.ItemData lapisData = itemData;
          if (item_inventory.Find((Predicate<ItemInfo>) (x => (int) x.tableData.id == (int) lapisData.id)) == null)
          {
            DateTime dateTime = new DateTime();
            if (lapisData.endDate == dateTime || lapisData.endDate > TimeManager.GetNow())
              item_inventory.Add(new ItemInfo(new Network.Item()
              {
                num = 0,
                itemId = (int) lapisData.id,
                uniqId = "0"
              }));
          }
        }
      }
      this.datas = (SortCompareData[]) this.sortSettings.CreateSortAry<ItemInfo, ItemSortData>(item_inventory.ToArray());
      if (include_lapis)
        return;
      AbilityItemSortData[] sortAry = this.sortSettings.CreateSortAry<AbilityItemInfo, AbilityItemSortData>(MonoBehaviourSingleton<InventoryManager>.I.abilityItemInventory.GetAll().Where<AbilityItemInfo>((Func<AbilityItemInfo, bool>) (x => x.equipUniqueId == 0UL)).ToArray<AbilityItemInfo>());
      if (sortAry.Length == 0)
        return;
      int length = this.datas.Length;
      Array.Resize<SortCompareData>(ref this.datas, this.datas.Length + sortAry.Length);
      Array.Copy((Array) sortAry, 0, (Array) this.datas, length, sortAry.Length);
      this.sortSettings.Sort<SortCompareData>(this.datas);
    }

    protected override bool DoSort() => this.sortSettings.Sort<SortCompareData>(this.datas);

    public override ItemIcon CreateIcon(object[] data)
    {
      SortCompareData sortCompareData = data[0] as SortCompareData;
      Transform parent = data[1] as Transform;
      int event_data = (int) data[2];
      ItemStorageTop itemStorageTop = data[3] as ItemStorageTop;
      bool is_new = false;
      switch (sortCompareData)
      {
        case ItemSortData _:
          is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(ITEM_ICON_TYPE.ITEM, sortCompareData.GetUniqID());
          break;
        case AbilityItemSortData _:
          is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(ITEM_ICON_TYPE.ABILITY_ITEM, sortCompareData.GetUniqID());
          MonoBehaviourSingleton<InventoryManager>.I.AddShowFragsAbilityItem(sortCompareData.GetUniqID());
          break;
      }
      ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(sortCompareData.GetTableID());
      ItemIcon materialIcon = ItemIconDetail.CreateMaterialIcon(sortCompareData.GetIconType(), sortCompareData.GetIconID(), new RARITY_TYPE?(sortCompareData.GetRarity()), itemData, itemStorageTop.showInventoryMode == ItemStorageTop.SHOW_INVENTORY_MODE.MAIN_STATUS, parent, sortCompareData.GetNum(), sortCompareData.GetName(), "SELECT", event_data, is_new: is_new);
      if (sortCompareData is AbilityItemSortData)
        (materialIcon as ItemIconDetail).setupperMaterial.SetDescription(sortCompareData.GetDetail());
      return materialIcon;
    }
  }

  public class UseItemInventory : ItemStorageTop.InventoryBase
  {
    protected virtual bool IsUseItemType(ITEM_TYPE item_type)
    {
      return item_type == ITEM_TYPE.USE_ITEM || item_type == ITEM_TYPE.EQUIP_SET_EXT;
    }

    public UseItemInventory()
    {
      this.sortSettings = SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.USE_ITEM, SortSettings.SETTINGS_TYPE.USE_ITEM);
      List<ItemInfo> itemInfoList = new List<ItemInfo>();
      for (LinkedListNode<ItemInfo> linkedListNode = MonoBehaviourSingleton<InventoryManager>.I.itemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
      {
        bool flag = this.IsUseItemType(linkedListNode.Value.tableData.type);
        if (linkedListNode.Value.num > 0 & flag)
          itemInfoList.Add(linkedListNode.Value);
      }
      this.datas = (SortCompareData[]) this.sortSettings.CreateSortAry<ItemInfo, ItemSortData>(itemInfoList.ToArray());
    }

    protected override bool DoSort()
    {
      return this.sortSettings.Sort<ItemSortData>(this.datas as ItemSortData[]);
    }

    public override ItemIcon CreateIcon(object[] data)
    {
      SortCompareData sortCompareData = data[0] as SortCompareData;
      Transform parent = data[1] as Transform;
      int event_data = (int) data[2];
      ItemStorageTop itemStorageTop = data[3] as ItemStorageTop;
      bool is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(ITEM_ICON_TYPE.ITEM, sortCompareData.GetUniqID());
      ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(sortCompareData.GetTableID());
      return ItemIconDetail.CreateMaterialIcon(sortCompareData.GetIconType(), sortCompareData.GetIconID(), new RARITY_TYPE?(sortCompareData.GetRarity()), itemData, itemStorageTop.showInventoryMode == ItemStorageTop.SHOW_INVENTORY_MODE.MAIN_STATUS, parent, sortCompareData.GetNum(), sortCompareData.GetName(), "SELECT", event_data, is_new: is_new);
    }
  }

  public class EquipItemInventory : ItemStorageTop.InventoryBase
  {
    public EquipItemInventory(GET_TYPE? get_type = null)
    {
      this.sortSettings = SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.STORAGE_EQUIP, SortSettings.SETTINGS_TYPE.STORAGE_EQUIP);
      List<EquipItemInfo> equipItemInfoList = new List<EquipItemInfo>();
      for (LinkedListNode<EquipItemInfo> linkedListNode = MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
      {
        bool flag = true;
        if (get_type.HasValue)
        {
          GET_TYPE getType = get_type.Value;
          if (getType == GET_TYPE.PAY && linkedListNode.Value.tableData.getType != GET_TYPE.PAY)
            flag = false;
          else if (getType != GET_TYPE.PAY && linkedListNode.Value.tableData.getType == GET_TYPE.PAY)
            flag = false;
        }
        if (flag)
          equipItemInfoList.Add(linkedListNode.Value);
      }
      this.datas = (SortCompareData[]) this.sortSettings.CreateSortAry<EquipItemInfo, EquipItemSortData>(equipItemInfoList.ToArray());
    }

    protected override bool DoSort()
    {
      return this.sortSettings.Sort<EquipItemSortData>(this.datas as EquipItemSortData[]);
    }

    public override ItemIcon CreateIcon(object[] data)
    {
      EquipItemSortData item_data = data[0] as EquipItemSortData;
      Transform parent = data[1] as Transform;
      int event_data = (int) data[2];
      ItemStorageTop itemStorageTop = data[3] as ItemStorageTop;
      int select_number = -1;
      int toggle_group = -1;
      if (itemStorageTop.isSellMode)
      {
        toggle_group = 0;
        select_number = itemStorageTop.sellItemData.FindIndex((Predicate<SortCompareData>) (sell_data => (long) sell_data.GetUniqID() == (long) item_data.GetUniqID()));
      }
      bool is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(item_data.GetIconType(), item_data.GetUniqID());
      EquipItemInfo itemData = item_data.GetItemData() as EquipItemInfo;
      SkillSlotUIData[] skillSlotData = itemStorageTop.GetSkillSlotData(itemData);
      if (select_number > -1)
        ++select_number;
      return ItemIconDetail.CreateEquipDetailSelectNumberIcon(item_data, skillSlotData, itemStorageTop.showInventoryMode == ItemStorageTop.SHOW_INVENTORY_MODE.MAIN_STATUS, parent, "SELECT", event_data, is_new: is_new, toggle_group: toggle_group, select_number: select_number, equipping_sp_index: item_data.IsEquipping() ? 0 : -1);
    }
  }

  public class SkillItemInventory : ItemStorageTop.InventoryBase
  {
    public SkillItemInventory(
      SortSettings.SETTINGS_TYPE sort_mem_type = SortSettings.SETTINGS_TYPE.STORAGE_SKILL,
      SKILL_SLOT_TYPE slot_type = SKILL_SLOT_TYPE.NONE,
      bool isAddMaterial = false)
    {
      this.Init(sort_mem_type, slot_type, isAddMaterial: isAddMaterial);
    }

    private void Init(
      SortSettings.SETTINGS_TYPE sort_mem_type = SortSettings.SETTINGS_TYPE.STORAGE_SKILL,
      SKILL_SLOT_TYPE slot_type = SKILL_SLOT_TYPE.NONE,
      int base_item_id = -1,
      bool isAddMaterial = false)
    {
      this.sortSettings = SortSettings.CreateMemSortSettings((slot_type == SKILL_SLOT_TYPE.NONE ? 1159 : 1 << (int) (slot_type - 1 & (SKILL_SLOT_TYPE) 31 /*0x1F*/)) == 1159 ? SortBase.DIALOG_TYPE.STORAGE_SKILL : SortBase.DIALOG_TYPE.SKILL, sort_mem_type);
      List<SkillItemInfo> skillItemInfoList = new List<SkillItemInfo>();
      for (LinkedListNode<SkillItemInfo> linkedListNode = MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
      {
        if (slot_type == SKILL_SLOT_TYPE.NONE || linkedListNode.Value.tableData.type == slot_type)
          skillItemInfoList.Add(linkedListNode.Value);
      }
      if (isAddMaterial)
      {
        for (LinkedListNode<SkillItemInfo> linkedListNode = MonoBehaviourSingleton<InventoryManager>.I.skillMaterialInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
        {
          if (slot_type == SKILL_SLOT_TYPE.NONE || linkedListNode.Value.tableData.type == slot_type)
            skillItemInfoList.Add(linkedListNode.Value);
        }
      }
      this.datas = (SortCompareData[]) this.sortSettings.CreateSortAry<SkillItemInfo, SkillItemSortData>(skillItemInfoList.ToArray());
    }

    protected override bool DoSort()
    {
      return this.sortSettings.Sort<SkillItemSortData>(this.datas as SkillItemSortData[]);
    }

    public override ItemIcon CreateIcon(object[] data)
    {
      SkillItemSortData item_data = data[0] as SkillItemSortData;
      Transform parent = data[1] as Transform;
      int event_data = (int) data[2];
      ItemStorageTop itemStorageTop = data[3] as ItemStorageTop;
      int select_number = -1;
      int toggle_group = -1;
      if (itemStorageTop.isSellMode)
      {
        toggle_group = 0;
        select_number = itemStorageTop.sellItemData.FindIndex((Predicate<SortCompareData>) (sell_data => (long) sell_data.GetUniqID() == (long) item_data.GetUniqID()));
      }
      bool is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(item_data.GetIconType(), item_data.GetUniqID());
      if (select_number > -1)
        ++select_number;
      ItemIconDetail.ICON_STATUS icon_status = item_data.IsExceeded() ? ItemIconDetail.ICON_STATUS.VALID_EXCEED : ItemIconDetail.ICON_STATUS.NONE;
      return ItemIconDetail.CreateSkillDetailSelectNumberIcon(item_data.GetIconType(), item_data.GetIconID(), new RARITY_TYPE?(item_data.GetRarity()), item_data, itemStorageTop.showInventoryMode == ItemStorageTop.SHOW_INVENTORY_MODE.MAIN_STATUS, parent, "SELECT", event_data, is_new, toggle_group, select_number, item_data.IsEquipping(), icon_status);
    }
  }

  public class QuestItemInventory : ItemStorageTop.InventoryBase
  {
    public QuestItemInventory()
    {
      this.sortSettings = SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.QUEST, SortSettings.SETTINGS_TYPE.ORDER_QUEST);
      List<QuestItemInfo> list = new List<QuestItemInfo>();
      MonoBehaviourSingleton<InventoryManager>.I.ForAllQuestInvetory((Action<QuestItemInfo>) (data =>
      {
        if (data.infoData.questData.num <= 0)
          return;
        list.Add(data);
      }));
      this.datas = (SortCompareData[]) this.sortSettings.CreateSortAry<QuestItemInfo, QuestSortData>(list.ToArray());
    }

    protected override bool DoSort()
    {
      return this.sortSettings.Sort<QuestSortData>(this.datas as QuestSortData[]);
    }

    public override ItemIcon CreateIcon(object[] data)
    {
      QuestSortData quest_item = data[0] as QuestSortData;
      Transform parent = data[1] as Transform;
      int event_data = (int) data[2];
      ItemStorageTop itemStorageTop = data[3] as ItemStorageTop;
      bool is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(quest_item.GetIconType(), quest_item.GetUniqID());
      ItemIcon questItemIcon = ItemIconDetail.CreateQuestItemIcon(quest_item.GetIconType(), quest_item.GetIconID(), new RARITY_TYPE?(quest_item.GetRarity()), quest_item, itemStorageTop.showInventoryMode == ItemStorageTop.SHOW_INVENTORY_MODE.MAIN_STATUS, parent, quest_item.GetNum(), quest_item.GetName(), "SELECT", event_data, is_new: is_new);
      questItemIcon.SetEnemyIconScale(quest_item.GetIconType(), 0.9f);
      return questItemIcon;
    }
  }

  public class AbilityItemInventory : ItemStorageTop.InventoryBase
  {
    public AbilityItemInventory()
    {
      this.sortSettings = SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.ABILITY_ITEM, SortSettings.SETTINGS_TYPE.STORAGE_ABILITY_ITEM);
      this.datas = (SortCompareData[]) this.sortSettings.CreateSortAry<AbilityItemInfo, AbilityItemSortData>(MonoBehaviourSingleton<InventoryManager>.I.abilityItemInventory.GetAll().Where<AbilityItemInfo>((Func<AbilityItemInfo, bool>) (x => x.equipUniqueId == 0UL)).ToArray<AbilityItemInfo>());
    }

    protected override bool DoSort()
    {
      return this.sortSettings.Sort<AbilityItemSortData>(this.datas as AbilityItemSortData[]);
    }

    public override ItemIcon CreateIcon(object[] objects)
    {
      AbilityItemSortData abilityItemSortData = objects[0] as AbilityItemSortData;
      Transform parent = objects[1] as Transform;
      int event_data = (int) objects[2];
      int num = (int) objects[3];
      bool is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(ITEM_ICON_TYPE.ABILITY_ITEM, abilityItemSortData.GetUniqID());
      ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(abilityItemSortData.GetTableID());
      return ItemIconDetail.CreateMaterialIcon(abilityItemSortData.GetIconType(), abilityItemSortData.GetIconID(), new RARITY_TYPE?(abilityItemSortData.GetRarity()), itemData, true, parent, abilityItemSortData.GetNum(), abilityItemSortData.GetName(), "SELECT_ITEM", event_data, is_new: is_new);
    }
  }

  public class AccessoryInventory : ItemStorageTop.InventoryBase
  {
    public AccessoryInventory()
    {
      this.sortSettings = SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.ACCESSORY, SortSettings.SETTINGS_TYPE.STORAGE_ACCESSORY);
      this.datas = (SortCompareData[]) this.sortSettings.CreateSortAry<AccessoryInfo, AccessorySortData>(MonoBehaviourSingleton<InventoryManager>.I.accessoryInventory.GetAll().ToArray());
    }

    protected override bool DoSort()
    {
      return this.sortSettings.Sort<AccessorySortData>(this.datas as AccessorySortData[]);
    }

    public override ItemIcon CreateIcon(object[] objects)
    {
      AccessorySortData accessorySortData = objects[0] as AccessorySortData;
      Transform _parent = objects[1] as Transform;
      int _eventData = (int) objects[2];
      bool _isNew = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(ITEM_ICON_TYPE.ACCESSORY, accessorySortData.GetUniqID());
      return ItemIconDetail.CreateAccessoryIcon(accessorySortData.itemData.tableData, _parent, "SELECT", _eventData, _isNew, false);
    }
  }

  public enum SHOW_INVENTORY_MODE
  {
    MAIN_STATUS,
    SUB_STATUS,
    MAX,
  }
}
