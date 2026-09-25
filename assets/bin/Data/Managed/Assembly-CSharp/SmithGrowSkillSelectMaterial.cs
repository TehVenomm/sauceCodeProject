// Decompiled with JetBrains decompiler
// Type: SmithGrowSkillSelectMaterial
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class SmithGrowSkillSelectMaterial : GameSection
{
  public int MATERIAL_SELECT_MAX = 10;
  protected List<ItemIcon> m_generatedIconList = new List<ItemIcon>();
  protected List<SortCompareData> m_newIconUpdateTargetList = new List<SortCompareData>();
  private SkillItemInfo skillItem;
  protected ItemStorageTop.SkillItemInventory inventory;
  private List<SkillItemInfo> materialSkillItem;
  protected SmithGrowSkillSelectMaterial.UI inventoryUI;
  protected SmithGrowSkillSelectMaterial.UI[] switchInventoryAry = new SmithGrowSkillSelectMaterial.UI[2]
  {
    SmithGrowSkillSelectMaterial.UI.GRD_INVENTORY,
    SmithGrowSkillSelectMaterial.UI.GRD_INVENTORY_SMALL
  };
  protected int inventoryUIIndex;
  private Color goldColor = Color.white;
  private bool isSelectMax;
  private bool isExceed;
  private bool isSortTypeReset;
  private Comparison<SortCompareData> m_defaultComparison;

  protected bool IsShowMainStatus => this.inventoryUIIndex == 0;

  public override void Initialize()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    this.skillItem = eventData[0] as SkillItemInfo;
    SkillItemInfo[] skillItemInfoArray = eventData[1] as SkillItemInfo[];
    this.isExceed = (bool) eventData[2];
    this.isSortTypeReset = (bool) eventData[3];
    GameSection.SetEventData((object) new object[2]
    {
      (object) ItemDetailEquip.CURRENT_SECTION.UI_PARTS,
      (object) this.skillItem
    });
    this.SetActive((Enum) SmithGrowSkillSelectMaterial.UI.BTN_CHANGE_INVENTORY, false);
    this.materialSkillItem = new List<SkillItemInfo>();
    if (skillItemInfoArray != null)
    {
      int index = 0;
      for (int length = skillItemInfoArray.Length; index < length; ++index)
        this.materialSkillItem.Add(skillItemInfoArray[index]);
    }
    if (this.materialSkillItem.Count == this.MATERIAL_SELECT_MAX)
      this.isSelectMax = true;
    UILabel component = this.GetComponent<UILabel>((Enum) SmithGrowSkillSelectMaterial.UI.LBL_GOLD);
    if (Object.op_Inequality((Object) component, (Object) null))
      this.goldColor = component.color;
    this.MATERIAL_SELECT_MAX = this.isExceed ? 10 : 10;
    base.Initialize();
    UIScrollView componentInChildren = ((Component) this.GetCtrl((Enum) SmithGrowSkillSelectMaterial.UI.SCR_INVENTORY)).GetComponentInChildren<UIScrollView>();
    componentInChildren.onDragFinished = new UIScrollView.OnDragNotification(this.OnReposition);
    componentInChildren.onStoppedMoving = new UIScrollView.OnDragNotification(this.OnReposition);
  }

  protected override void OnClose()
  {
    this.UpdateNewIconInfo();
    base.OnClose();
  }

  protected virtual void Update() => this.ObserveItemList();

  public void OnReposition()
  {
    ItemIcon[] icons = ((Component) this.GetCtrl((Enum) this.inventoryUI)).GetComponentsInChildren<ItemIcon>();
    this.materialSkillItem.ForEach((Action<SkillItemInfo>) (material =>
    {
      ItemIcon icon = Array.Find<ItemIcon>(icons, (Predicate<ItemIcon>) (_icon => (long) _icon.GetUniqID == (long) material.uniqueID));
      if (!Object.op_Inequality((Object) icon, (Object) null))
        return;
      this.IconSelect(icon, true);
    }));
  }

  public override void UpdateUI()
  {
    this.SetFontStyle((Enum) SmithGrowSkillSelectMaterial.UI.STR_TITLE_MATERIAL, (FontStyle) 2);
    this.SetFontStyle((Enum) SmithGrowSkillSelectMaterial.UI.STR_TITLE_MONEY, (FontStyle) 2);
    this.SetActive((Enum) SmithGrowSkillSelectMaterial.UI.STR_EXCEED_CAUTION, this.isExceed);
    this.SetActive((Enum) SmithGrowSkillSelectMaterial.UI.OBJ_GOLD, !this.isExceed);
    if (!this.isExceed)
      this.UpdateNeedGold();
    this.UpdateLvExp();
    if (this.inventory == null)
      this.InitInventory();
    this.SetActive((Enum) SmithGrowSkillSelectMaterial.UI.STR_NON_MATERIAL, this.inventory == null || this.inventory.datas == null || this.inventory.datas.Length <= 1);
    this.SetupEnableInventoryUI();
    this.UpdateInventory();
    this.UpdateSelectMaterialIcon();
    this.SetLabelText((Enum) SmithGrowSkillSelectMaterial.UI.LBL_SORT, this.inventory.sortSettings.GetSortLabel());
    this.SetToggle((Enum) SmithGrowSkillSelectMaterial.UI.TGL_ICON_ASC, this.inventory.sortSettings.orderTypeAsc);
  }

  private void UpdateInventory()
  {
    this.m_generatedIconList.Clear();
    this.UpdateNewIconInfo();
    int base_item_index = Array.FindIndex<SortCompareData>(this.inventory.datas, (Predicate<SortCompareData>) (data => (long) data.GetUniqID() == (long) this.skillItem.uniqueID));
    this.SetDynamicList((Enum) this.inventoryUI, (string) null, this.inventory.datas.Length, false, (Func<int, bool>) (i => i != base_item_index && this.inventory.datas[i] is SkillItemSortData data1 && data1.IsPriority(this.inventory.sortSettings.orderTypeAsc) && (!this.isExceed || data1.skillData.tableData.type == this.skillItem.tableData.type || data1.skillData.tableData.type == SKILL_SLOT_TYPE.PASSIVE)), (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      SkillItemSortData item = this.inventory.datas[i] as SkillItemSortData;
      int index = this.materialSkillItem.FindIndex((Predicate<SkillItemInfo>) (material => (long) material.uniqueID == (long) item.GetUniqID()));
      if (index > -1)
        ++index;
      ITEM_ICON_TYPE iconType = item.GetIconType();
      bool is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(iconType, item.GetUniqID());
      ItemIcon itemIconDetail = this.CreateItemIconDetail(iconType, item.skillData.tableData.iconID, new RARITY_TYPE?(item.skillData.tableData.rarity), item, this.IsShowMainStatus, t, "MATERIAL", i, is_new, select_number: index, is_equipping: item.IsEquipping(), is_select_max: this.isSelectMax);
      itemIconDetail.SetUniqID(item.GetUniqID());
      this.SetLongTouch(itemIconDetail.transform, "DETAIL", (object) i);
      if (Object.op_Inequality((Object) itemIconDetail, (Object) null) && item != null)
        itemIconDetail.SetInitData((SortCompareData) item);
      if (this.m_generatedIconList.Contains(itemIconDetail))
        return;
      this.m_generatedIconList.Add(itemIconDetail);
    }));
  }

  private void InitInventory()
  {
    this.inventory = new ItemStorageTop.SkillItemInventory(this.isExceed ? SortSettings.SETTINGS_TYPE.EXCEED_SKILL_ITEM : SortSettings.SETTINGS_TYPE.GROW_SKILL_ITEM, isAddMaterial: true);
    if (this.isSortTypeReset)
      this.inventory.sortSettings.ResetType();
    this.sorting();
  }

  private SkillItemInfo ParamCopy(SkillItemInfo _ref, bool isLevelUp = false, bool isExceedUp = false)
  {
    return SmithGrowSkillSecond.ParamCopy(_ref, isLevelUp, isExceedUp);
  }

  protected bool sorting()
  {
    this.inventory.sortSettings.indivComparison = new Comparison<SortCompareData>(this.CustomCompare);
    this.m_defaultComparison = new SortComparison(this.inventory.sortSettings.orderTypeAsc).comparison;
    return this.inventory.sortSettings.Sort<SkillItemSortData>(this.inventory.datas as SkillItemSortData[]);
  }

  private void OnQuery_SORT()
  {
    GameSection.SetEventData((object) new object[3]
    {
      (object) this.skillItem,
      (object) this.inventory.sortSettings.Clone(),
      (object) this.isExceed
    });
  }

  private void OnCloseDialog_SmithSkillGrowSort()
  {
    SortSettings eventData = (SortSettings) GameSection.GetEventData();
    if (eventData == null)
      return;
    this.inventory.sortSettings.indivComparison = new Comparison<SortCompareData>(this.CustomCompare);
    this.m_defaultComparison = new SortComparison(this.inventory.sortSettings.orderTypeAsc).comparison;
    if (!this.inventory.Sort(eventData))
      return;
    this.SetDirty((Enum) SmithGrowSkillSelectMaterial.UI.GRD_INVENTORY);
    this.SetDirty((Enum) SmithGrowSkillSelectMaterial.UI.GRD_INVENTORY_SMALL);
    this.RefreshUI();
  }

  private bool IsEnableSelect(SortCompareData item)
  {
    return item != null && !item.IsFavorite() && (long) item.GetUniqID() != (long) this.skillItem.uniqueID;
  }

  private void OnQuery_MATERIAL()
  {
    int eventData = (int) GameSection.GetEventData();
    SkillItemSortData item = this.inventory.datas[eventData] as SkillItemSortData;
    bool flag = this.materialSkillItem.Find((Predicate<SkillItemInfo>) (material => (long) material.uniqueID == (long) item.GetUniqID())) != null;
    SkillItemInfo itemData = item.GetItemData() as SkillItemInfo;
    if (!this.IsEnableSelect(this.inventory.datas[eventData]))
    {
      if (item.IsFavorite())
        GameSection.ChangeEvent("NOT_MATERIAL_FAVORITE");
    }
    else if (flag)
      this.materialSkillItem.Remove(itemData);
    else if (this.materialSkillItem.Count < this.MATERIAL_SELECT_MAX)
      this.materialSkillItem.Add(itemData);
    int num1 = this.isSelectMax ? 1 : 0;
    this.isSelectMax = this.materialSkillItem.Count == this.MATERIAL_SELECT_MAX;
    int num2 = this.isSelectMax ? 1 : 0;
    if (num1 != num2)
      this.UpdateInventory();
    this.UpdateSelectMaterialIcon();
  }

  private void OnQuery_MATERIAL_NUM()
  {
    SkillItemSortData data = this.inventory.datas[(int) GameSection.GetEventData()] as SkillItemSortData;
    SkillItemInfo itemData = data.GetItemData() as SkillItemInfo;
    int num = 0;
    int index = 0;
    for (int count = this.materialSkillItem.Count; index < count; ++index)
    {
      if ((long) this.materialSkillItem[index].uniqueID == (long) itemData.uniqueID)
        ++num;
    }
    GameSection.SetEventData((object) new object[3]
    {
      (object) data,
      (object) (this.MATERIAL_SELECT_MAX - this.materialSkillItem.Count),
      (object) num
    });
  }

  private void OnCloseDialog_SmithGrowSkillSelectMaterialItemNum()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    SkillItemInfo itemData = (eventData[0] as SkillItemSortData).GetItemData() as SkillItemInfo;
    int num1 = (int) eventData[1];
    int num2 = 0;
    int index1 = 0;
    for (int count = this.materialSkillItem.Count; index1 < count; ++index1)
    {
      if ((long) this.materialSkillItem[index1].uniqueID == (long) itemData.uniqueID)
        ++num2;
    }
    int num3 = num1 - num2;
    if (num3 < 0)
    {
      for (int index2 = this.materialSkillItem.Count - 1; index2 >= 0; --index2)
      {
        if ((long) this.materialSkillItem[index2].uniqueID == (long) itemData.uniqueID)
        {
          this.materialSkillItem.RemoveAt(index2);
          ++num3;
          if (num3 >= 0)
            break;
        }
      }
    }
    else if (num3 > 0)
    {
      for (int index3 = 0; index3 < num3; ++index3)
        this.materialSkillItem.Add(itemData);
    }
    int num4 = this.isSelectMax ? 1 : 0;
    this.isSelectMax = this.materialSkillItem.Count == this.MATERIAL_SELECT_MAX;
    int num5 = this.isSelectMax ? 1 : 0;
    if (num4 != num5)
      this.UpdateInventory();
    this.UpdateSelectMaterialIcon();
  }

  private void ResetSelectMaterialIcon() => this._UpdateSelectMaterialIcon(true);

  private void UpdateSelectMaterialIcon() => this._UpdateSelectMaterialIcon(false);

  private void _UpdateSelectMaterialIcon(bool reset)
  {
    ItemIcon[] icons = ((Component) this.GetCtrl((Enum) this.inventoryUI)).GetComponentsInChildren<ItemIcon>();
    int index1 = 0;
    for (int length = icons.Length; index1 < length; ++index1)
    {
      if (this.inventoryUI == SmithGrowSkillSelectMaterial.UI.GRD_INVENTORY)
        (icons[index1] as ItemIconDetail).setupperSkill.SetupSelectNumberSprite(-1);
      else
        (icons[index1] as ItemIconDetailSmall).SetupSelectNumberSprite();
      this.IconSelect(icons[index1], false);
    }
    int index = reset ? -1 : 1;
    this.materialSkillItem.ForEach((Action<SkillItemInfo>) (material =>
    {
      ItemIcon itemIcon = Array.Find<ItemIcon>(icons, (Predicate<ItemIcon>) (_icon => (long) _icon.GetUniqID == (long) material.uniqueID));
      if (Object.op_Inequality((Object) itemIcon, (Object) null))
      {
        if (this.inventoryUI == SmithGrowSkillSelectMaterial.UI.GRD_INVENTORY)
        {
          ItemIconDetail icon = itemIcon as ItemIconDetail;
          if (Object.op_Inequality((Object) icon, (Object) null))
          {
            if (icon.iconType != ITEM_ICON_TYPE.SKILL_GROW)
            {
              icon.setupperSkill.SetupSelectNumberSprite(index);
              this.IconSelect((ItemIcon) icon, true);
            }
            else
            {
              icon.setupperSkill.SetupSelectNumberSprite(-1);
              this.IconSelect((ItemIcon) icon, true);
            }
          }
        }
        else
        {
          ItemIconDetailSmall icon = itemIcon as ItemIconDetailSmall;
          if (Object.op_Inequality((Object) icon, (Object) null))
          {
            if (icon.iconType != ITEM_ICON_TYPE.SKILL_GROW)
            {
              icon.SetupSelectNumberSprite(index);
              this.IconSelect((ItemIcon) icon, true);
            }
            else
            {
              icon.SetupSelectNumberSprite();
              this.IconSelect((ItemIcon) icon, true);
            }
          }
        }
      }
      if (reset)
        return;
      ++index;
    }));
    if (!this.isExceed)
      this.UpdateNeedGold();
    this.UpdateLvExp();
  }

  private void OnQuery_DECISION()
  {
    GameSection.SetEventData((object) new object[2]
    {
      (object) this.skillItem,
      (object) this.materialSkillItem.ToArray()
    });
  }

  private void OnQuery_CLEAR()
  {
    this.materialSkillItem.Clear();
    this.isSelectMax = false;
    this.SetDirty((Enum) SmithGrowSkillSelectMaterial.UI.GRD_INVENTORY);
    this.SetDirty((Enum) SmithGrowSkillSelectMaterial.UI.GRD_INVENTORY_SMALL);
    this.RefreshUI();
  }

  private void OnQuery_DETAIL()
  {
    SkillItemSortData data = this.inventory.datas[(int) GameSection.GetEventData()] as SkillItemSortData;
    if (data.skillData.tableData.type == SKILL_SLOT_TYPE.GROW)
      GameSection.StopEvent();
    else
      GameSection.SetEventData((object) new object[2]
      {
        (object) ItemDetailEquip.CURRENT_SECTION.SMITH_SKILL_GROW,
        (object) data
      });
  }

  protected void OnQuery_CHANGE_INVENTORY()
  {
    this.inventoryUIIndex = this.inventoryUIIndex + 1 < this.switchInventoryAry.Length ? this.inventoryUIIndex + 1 : 0;
    this.SetDirty((Enum) SmithGrowSkillSelectMaterial.UI.GRD_INVENTORY);
    this.SetDirty((Enum) SmithGrowSkillSelectMaterial.UI.GRD_INVENTORY_SMALL);
    this.RefreshUI();
  }

  protected void SetupEnableInventoryUI()
  {
    int index = 0;
    for (int length = this.switchInventoryAry.Length; index < length; ++index)
      this.SetActive((Enum) this.switchInventoryAry[index], false);
    this.SetActive((Enum) this.switchInventoryAry[this.inventoryUIIndex], true);
    this.inventoryUI = this.switchInventoryAry[this.inventoryUIIndex];
    this.SetToggle((Enum) SmithGrowSkillSelectMaterial.UI.TGL_CHANGE_INVENTORY, this.inventoryUI == SmithGrowSkillSelectMaterial.UI.GRD_INVENTORY);
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
    int select_number = -1,
    bool is_equipping = false,
    bool is_select_max = false)
  {
    ItemIconDetail.ICON_STATUS iconStatus = ItemIconDetail.ICON_STATUS.NONE;
    if (is_select_max && select_number == -1)
      iconStatus = ItemIconDetail.ICON_STATUS.GRAYOUT;
    if (this.inventoryUI == SmithGrowSkillSelectMaterial.UI.GRD_INVENTORY)
    {
      if (icon_type == ITEM_ICON_TYPE.SKILL_GROW)
      {
        ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(item_data.skillData.itemId);
        bool is_select = select_number != -1;
        ItemIcon materialIcon;
        ((ItemIconDetail) (materialIcon = ItemIconDetail.CreateMaterialIcon(icon_type, icon_id, rarity, itemData, is_show_main_status, parent, item_data.skillData.num, itemData.name, "MATERIAL_NUM", event_data, toggle_group, is_select))).setupperSkill.GrayOut(iconStatus);
        return materialIcon;
      }
      bool isSameSkillExceed = this.isExceed && (int) this.skillItem.tableData.id == (int) item_data.skillData.tableData.id;
      return ItemIconDetail.CreateSkillDetailSelectNumberIcon(icon_type, icon_id, rarity, item_data, is_show_main_status, parent, event_name, event_data, is_new, toggle_group, select_number, is_equipping, iconStatus, isSameSkillExceed);
    }
    if (icon_type != ITEM_ICON_TYPE.SKILL_GROW)
      return ItemIconDetailSmall.CreateSmallSkillSelectDetailIcon(icon_type, icon_id, rarity, item_data, parent, event_name, event_data, is_new, toggle_group, select_number, is_equipping, iconStatus);
    ItemTable.ItemData itemData1 = Singleton<ItemTable>.I.GetItemData(item_data.skillData.itemId);
    bool is_select1 = select_number != -1;
    return ItemIconDetailSmall.CreateSmallMaterialIcon(icon_type, icon_id, rarity, parent, item_data.skillData.num, itemData1.name, "MATERIAL_NUM", event_data, toggle_group, is_select1, is_new, icon_status: iconStatus);
  }

  private void UpdateLvExp()
  {
    SkillItemInfo[] array = this.materialSkillItem.ToArray();
    this.SetLabelText((Enum) SmithGrowSkillSelectMaterial.UI.LBL_SELECT_NUM, (this.MATERIAL_SELECT_MAX - (array != null ? array.Length : 0)).ToString());
    SkillItemInfo skillItemInfo = this.ParamCopy(this.skillItem);
    SkillItemInfo _ref = this.ParamCopy(this.skillItem);
    if (array != null)
    {
      int index = 0;
      for (int length = array.Length; index < length; ++index)
      {
        if (this.isExceed)
        {
          if (!_ref.IsMaxExceed())
          {
            if ((int) this.skillItem.tableData.id == (int) array[index].tableData.id)
              _ref.exceedExp += array[index].giveSameSkillExceedExp;
            else
              _ref.exceedExp += array[index].giveExceedExp;
            while (_ref.exceedExpNext <= _ref.exceedExp)
            {
              _ref = this.ParamCopy(_ref, isExceedUp: true);
              if (_ref.IsMaxExceed())
              {
                _ref.exceedExp = _ref.expPrev;
                break;
              }
            }
          }
        }
        else if (!_ref.IsLevelMax() && array[index].level <= array[index].GetMaxLevel())
        {
          _ref.exp += array[index].giveExp;
          while (_ref.expNext <= _ref.exp)
          {
            _ref = this.ParamCopy(_ref, true);
            if (_ref.IsLevelMax())
            {
              _ref.exp = _ref.expPrev;
              break;
            }
          }
        }
      }
    }
    this.SetLabelText((Enum) SmithGrowSkillSelectMaterial.UI.LBL_LV_NOW, _ref.level.ToString());
    this.SetLabelText((Enum) SmithGrowSkillSelectMaterial.UI.LBL_LV_MAX, _ref.GetMaxLevel().ToString());
    this.SetActive((Enum) SmithGrowSkillSelectMaterial.UI.OBJ_LV_EX, _ref.IsExceeded());
    this.SetLabelText((Enum) SmithGrowSkillSelectMaterial.UI.LBL_LV_EX, _ref.exceedCnt.ToString());
    SkillGrowProgress component = ((Component) this.FindCtrl(((Component) this).transform, (Enum) SmithGrowSkillSelectMaterial.UI.PRG_EXP_BAR)).GetComponent<SkillGrowProgress>();
    if (this.isExceed)
    {
      float fill_amount = (float) (this.skillItem.exceedExp - this.skillItem.exceedExpPrev) / (float) (this.skillItem.exceedExpNext - this.skillItem.exceedExpPrev);
      this.SetProgressInt(((Component) this).transform, (Enum) SmithGrowSkillSelectMaterial.UI.PRG_EXP_BAR, _ref.exceedExp, _ref.exceedExpPrev, _ref.exceedExpNext);
      component.SetExceedMode();
      component.SetBaseGauge(_ref.exceedCnt == skillItemInfo.exceedCnt, fill_amount);
    }
    else
    {
      float fill_amount = (float) (this.skillItem.exp - this.skillItem.expPrev) / (float) (this.skillItem.expNext - this.skillItem.expPrev);
      this.SetProgressInt(((Component) this).transform, (Enum) SmithGrowSkillSelectMaterial.UI.PRG_EXP_BAR, _ref.exp, _ref.expPrev, _ref.expNext);
      component.SetGrowMode();
      component.SetBaseGauge(_ref.level == skillItemInfo.level, fill_amount);
    }
  }

  private void UpdateNeedGold()
  {
    int num = 0;
    if (this.materialSkillItem != null && this.skillItem != null)
      num = (int) ((double) this.skillItem.growCost * (double) this.materialSkillItem.Count);
    this.SetLabelText((Enum) SmithGrowSkillSelectMaterial.UI.LBL_GOLD, num.ToString("N0"));
    if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money < num)
      this.SetColor((Enum) SmithGrowSkillSelectMaterial.UI.LBL_GOLD, Color.red);
    else
      this.SetColor((Enum) SmithGrowSkillSelectMaterial.UI.LBL_GOLD, this.goldColor);
  }

  private void IconSelect(ItemIcon icon, bool is_select)
  {
    ((Component) icon.selectFrame).gameObject.SetActive(is_select);
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((flags & (GameSection.NOTIFY_FLAG.UPDATE_SKILL_FAVORITE | GameSection.NOTIFY_FLAG.UPDATE_ITEM_INVENTORY | GameSection.NOTIFY_FLAG.UPDATE_SKILL_INVENTORY)) != (GameSection.NOTIFY_FLAG) 0)
    {
      this.inventory = new ItemStorageTop.SkillItemInventory(this.isExceed ? SortSettings.SETTINGS_TYPE.EXCEED_SKILL_ITEM : SortSettings.SETTINGS_TYPE.GROW_SKILL_ITEM, isAddMaterial: true);
      this.sorting();
      this.skillItem = MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.Find(this.skillItem.uniqueID);
      List<SkillItemInfo> del_list = new List<SkillItemInfo>();
      this.materialSkillItem.ForEach((Action<SkillItemInfo>) (skill =>
      {
        SkillItemInfo skillItemInfo1 = MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.Find(skill.uniqueID);
        SkillItemInfo skillItemInfo2 = MonoBehaviourSingleton<InventoryManager>.I.skillMaterialInventory.Find(skill.uniqueID);
        if ((skillItemInfo1 != null || skillItemInfo2 != null) && (skillItemInfo1 == null || !skillItemInfo1.isFavorite))
          return;
        del_list.Add(skill);
      }));
      del_list.ForEach((Action<SkillItemInfo>) (delitem => this.materialSkillItem.Remove(delitem)));
      this.inventory = (ItemStorageTop.SkillItemInventory) null;
      this.RefreshUI();
    }
    base.OnNotify(flags);
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_USER_STATUS | GameSection.NOTIFY_FLAG.UPDATE_SKILL_FAVORITE | GameSection.NOTIFY_FLAG.UPDATE_ITEM_INVENTORY | GameSection.NOTIFY_FLAG.UPDATE_SKILL_INVENTORY;
  }

  private int CustomCompare(SortCompareData lp, SortCompareData rp)
  {
    SkillItemSortData skillItemSortData1 = lp as SkillItemSortData;
    SkillItemSortData skillItemSortData2 = rp as SkillItemSortData;
    if (skillItemSortData1 == null || skillItemSortData2 == null || skillItemSortData1.skillData == null || skillItemSortData1.skillData.tableData == null || skillItemSortData2.skillData == null || skillItemSortData2.skillData.tableData == null)
      return 0;
    if (skillItemSortData1.skillData.tableData.type == SKILL_SLOT_TYPE.GROW && skillItemSortData2.skillData.tableData.type == SKILL_SLOT_TYPE.GROW)
      return this.m_defaultComparison(lp, rp);
    if (skillItemSortData1.skillData.tableData.type == SKILL_SLOT_TYPE.GROW)
      return -1;
    return skillItemSortData2.skillData.tableData.type == SKILL_SLOT_TYPE.GROW ? 1 : this.m_defaultComparison(lp, rp);
  }

  protected void ObserveItemList()
  {
    if (this.m_generatedIconList == null || this.m_generatedIconList.Count < 1)
      return;
    int index = 0;
    for (int count = this.m_generatedIconList.Count; index < count; ++index)
      this.ObserveItemListNewIcon(this.m_generatedIconList[index]);
  }

  protected void ObserveItemListNewIcon(ItemIcon _icon)
  {
    if (Object.op_Equality((Object) _icon, (Object) null) || _icon.InitData == null || !_icon.IsVisbleNewIcon() || this.m_newIconUpdateTargetList.Contains(_icon.InitData))
      return;
    this.m_newIconUpdateTargetList.Add(_icon.InitData);
  }

  protected void UpdateNewIconInfo()
  {
    if (this.m_newIconUpdateTargetList.Count < 1)
      return;
    GameSaveData instance = GameSaveData.instance;
    if (instance == null)
      return;
    int index = 0;
    for (int count = this.m_newIconUpdateTargetList.Count; index < count; ++index)
    {
      SortCompareData iconUpdateTarget = this.m_newIconUpdateTargetList[index];
      instance.RemoveNewIconAndSave(iconUpdateTarget.GetIconType(), iconUpdateTarget.GetUniqID());
    }
    this.m_newIconUpdateTargetList.Clear();
  }

  protected enum UI
  {
    STR_NON_MATERIAL,
    LBL_EQUIP_ITEM_NAME,
    SCR_INVENTORY,
    GRD_INVENTORY,
    GRD_INVENTORY_SMALL,
    TGL_CHANGE_INVENTORY,
    BTN_CHANGE_INVENTORY,
    LBL_SORT,
    TGL_ICON_ASC,
    LBL_SELECT_NUM,
    STR_TITLE_MATERIAL,
    STR_TITLE_MONEY,
    OBJ_GOLD,
    LBL_GOLD,
    LBL_LV_NOW,
    LBL_LV_MAX,
    OBJ_LV_EX,
    LBL_LV_EX,
    OBJ_NEXT_EXP_ROOT,
    PRG_EXP_BAR,
    STR_EXCEED_CAUTION,
  }
}
