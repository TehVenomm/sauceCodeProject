// Decompiled with JetBrains decompiler
// Type: SmithGrowSkill
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class SmithGrowSkill : ItemDetailSkill
{
  public const int MATERIAL_SELECT_MAX = 10;
  private SkillItemInfo skillItem;
  protected ItemStorageTop.SkillItemInventory inventory;
  private List<SkillItemInfo> materialSkillItem;
  private int needGold;
  private Color goldColor = Color.white;
  protected SmithGrowSkill.UI inventoryUI;
  protected SmithGrowSkill.UI[] switchInventoryAry = new SmithGrowSkill.UI[2]
  {
    SmithGrowSkill.UI.GRD_INVENTORY,
    SmithGrowSkill.UI.GRD_INVENTORY_SMALL
  };
  protected int inventoryUIIndex;
  private bool isNoticeSendGrow;
  private int toggleIndex = -1;

  protected bool IsShowMainStatus => this.inventoryUIIndex == 0;

  public override void Initialize()
  {
    this.skillItem = GameSection.GetEventData() as SkillItemInfo;
    GameSection.SetEventData((object) new object[2]
    {
      (object) ItemDetailEquip.CURRENT_SECTION.UI_PARTS,
      (object) this.skillItem
    });
    this.materialSkillItem = new List<SkillItemInfo>();
    UILabel component = this.GetComponent<UILabel>((Enum) SmithGrowSkill.UI.LBL_GOLD);
    if (Object.op_Inequality((Object) component, (Object) null))
      this.goldColor = component.color;
    base.Initialize();
  }

  protected override void OnOpen()
  {
    this.isNoticeSendGrow = false;
    base.OnOpen();
  }

  public override void UpdateUI()
  {
    this.SetFontStyle((Enum) SmithGrowSkill.UI.STR_TITLE_MATERIAL, (FontStyle) 2);
    this.SetFontStyle((Enum) SmithGrowSkill.UI.STR_TITLE_MONEY, (FontStyle) 2);
    if (Object.op_Inequality((Object) this.detailBase, (Object) null))
      this.SetActive(this.detailBase, (Enum) SmithGrowSkill.UI.OBJ_FAVORITE_ROOT, false);
    if (this.inventory == null)
      this.InitInventory();
    this.SetActive((Enum) SmithGrowSkill.UI.STR_NON_MATERIAL, this.inventory == null || this.inventory.datas == null || this.inventory.datas.Length <= 1);
    this.UpdateMaterial();
    this.SetupEnableInventoryUI();
    int base_item_index = Array.FindIndex<SortCompareData>(this.inventory.datas, (Predicate<SortCompareData>) (data => (long) data.GetUniqID() == (long) this.skillItem.uniqueID));
    this.SetDynamicList((Enum) this.inventoryUI, (string) null, this.inventory.datas.Length, false, (Func<int, bool>) (i => i != base_item_index), (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      SkillItemSortData item = this.inventory.datas[i] as SkillItemSortData;
      int index = this.materialSkillItem.FindIndex((Predicate<SkillItemInfo>) (material => (long) material.uniqueID == (long) item.GetUniqID()));
      if (index > -1)
        ++index;
      ITEM_ICON_TYPE iconType = item.GetIconType();
      bool is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(iconType, item.GetUniqID());
      ItemIcon itemIconDetail = this.CreateItemIconDetail(iconType, item.skillData.tableData.iconID, new RARITY_TYPE?(item.skillData.tableData.rarity), item, this.IsShowMainStatus, t, "MATERIAL", i, is_new, 0, index, item.IsEquipping());
      itemIconDetail.toggleSelectFrame.onChange.Clear();
      itemIconDetail.toggleSelectFrame.onChange.Add(new EventDelegate((MonoBehaviour) this, "IconToggleChange"));
      this.SetLongTouch(itemIconDetail.transform, "DETAIL", (object) i);
    }));
  }

  private void InitInventory()
  {
    this.inventory = new ItemStorageTop.SkillItemInventory(SortSettings.SETTINGS_TYPE.GROW_SKILL_ITEM);
    this.inventory.sortSettings = SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.STORAGE_SKILL, SortSettings.SETTINGS_TYPE.GROW_SKILL_ITEM);
    this.sorting();
  }

  private SkillItemInfo ParamCopy(SkillItemInfo _ref, bool is_level_up = false)
  {
    int lv = !is_level_up ? _ref.level : _ref.level + 1;
    SkillItemInfo skillItemInfo = new SkillItemInfo(0, (int) _ref.tableID, lv, _ref.exceedCnt);
    skillItemInfo.uniqueID = _ref.uniqueID;
    skillItemInfo.exp = _ref.exp;
    skillItemInfo.expPrev = MonoBehaviourSingleton<SmithManager>.I.GetGrowResultValue(skillItemInfo.tableData.baseNeedExp, skillItemInfo.growData.needExp);
    skillItemInfo.expNext = MonoBehaviourSingleton<SmithManager>.I.GetGrowResultValue(skillItemInfo.tableData.baseNeedExp, skillItemInfo.nextGrowData.needExp);
    skillItemInfo.growCost = _ref.growCost;
    return skillItemInfo;
  }

  private void UpdateMaterial()
  {
    this.SetLabelText((Enum) SmithGrowSkill.UI.LBL_SELECT_NUM, (10 - this.materialSkillItem.Count).ToString());
    this.needGold = (int) ((double) this.skillItem.growCost * (double) this.materialSkillItem.Count);
    this.SetLabelText((Enum) SmithGrowSkill.UI.LBL_GOLD, this.needGold.ToString("N0"));
    if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money < this.needGold)
      this.SetColor((Enum) SmithGrowSkill.UI.LBL_GOLD, Color.red);
    else
      this.SetColor((Enum) SmithGrowSkill.UI.LBL_GOLD, this.goldColor);
    int exp = 0;
    SkillItemInfo data = this.ParamCopy(this.skillItem);
    int level = data.level;
    this.materialSkillItem.ForEach((Action<SkillItemInfo>) (material =>
    {
      if (data.IsLevelMax() || material.level > material.GetMaxLevel())
        return;
      exp += material.giveExp;
      data.exp += material.giveExp;
      while (data.expNext <= data.exp)
      {
        data = this.ParamCopy(data, true);
        if (data.IsLevelMax())
        {
          data.exp = data.expPrev;
          break;
        }
      }
    }));
    this.itemData = (object) data;
    base.UpdateUI();
    this.SetLabelText(this.detailBase, (Enum) SmithGrowSkill.UI.LBL_LV_NOW, data.level.ToString());
    this.SetLabelText(this.detailBase, (Enum) SmithGrowSkill.UI.LBL_LV_MAX, data.GetMaxLevel().ToString());
    this.SetActive(this.detailBase, (Enum) SmithGrowSkill.UI.OBJ_LV_EX, data.IsExceeded());
    this.SetLabelText(this.detailBase, (Enum) SmithGrowSkill.UI.LBL_LV_EX, data.exceedCnt.ToString());
    this.SetLabelText(this.detailBase, (Enum) SmithGrowSkill.UI.LBL_ATK, data.atk.ToString());
    this.SetLabelText(this.detailBase, (Enum) SmithGrowSkill.UI.LBL_DEF, data.def.ToString());
    this.SetLabelText(this.detailBase, (Enum) SmithGrowSkill.UI.LBL_HP, data.hp.ToString());
    this.SetLabelText(this.detailBase, (Enum) SmithGrowSkill.UI.LBL_SELL, this.needGold.ToString());
    this.SetLabelText(this.detailBase, (Enum) SmithGrowSkill.UI.STR_SELL, this.sectionData.GetText("STR_SELL"));
    SkillGrowProgress component = ((Component) this.FindCtrl(this.detailBase, (Enum) SmithGrowSkill.UI.PRG_EXP_BAR)).GetComponent<SkillGrowProgress>();
    float fill_amount = (float) (this.skillItem.exp - this.skillItem.expPrev) / (float) (this.skillItem.expNext - this.skillItem.expPrev);
    component.SetGrowMode();
    component.SetBaseGauge(data.level == level, fill_amount);
    this.UpdateAnchors();
  }

  protected bool sorting()
  {
    return this.inventory.sortSettings.Sort<SkillItemSortData>(this.inventory.datas as SkillItemSortData[]);
  }

  private void OnQuery_SORT()
  {
    GameSection.SetEventData((object) new object[2]
    {
      (object) this.skillItem,
      (object) this.inventory.sortSettings.Clone()
    });
  }

  private void OnCloseDialog_SmithSkillGrowSort()
  {
    SortSettings eventData = (SortSettings) GameSection.GetEventData();
    if (eventData == null || !this.inventory.Sort(eventData))
      return;
    this.SetDirty((Enum) SmithGrowSkill.UI.GRD_INVENTORY);
    this.SetDirty((Enum) SmithGrowSkill.UI.GRD_INVENTORY_SMALL);
    this.RefreshUI();
  }

  private bool IsEnableSelect(SortCompareData item)
  {
    return item != null && !item.IsFavorite() && (long) item.GetUniqID() != (long) this.skillItem.uniqueID;
  }

  private void OnQuery_MATERIAL()
  {
    this.ResetSelectMaterialIcon();
    int eventData = (int) GameSection.GetEventData();
    SkillItemSortData item = this.inventory.datas[eventData] as SkillItemSortData;
    bool flag = this.materialSkillItem.Find((Predicate<SkillItemInfo>) (material => (long) material.uniqueID == (long) item.GetUniqID())) != null;
    SkillItemInfo itemData = item.GetItemData() as SkillItemInfo;
    if (!this.IsEnableSelect(this.inventory.datas[eventData]))
    {
      this.toggleIndex = eventData;
      this.DispatchEvent("NOT_MATERIAL_FAVORITE");
    }
    else if (flag)
      this.materialSkillItem.Remove(itemData);
    else if (this.materialSkillItem.Count < 10)
      this.materialSkillItem.Add(itemData);
    else
      this.toggleIndex = eventData;
    this.UpdateMaterial();
    this.UpdateSelectMaterialIcon();
  }

  private void ResetSelectMaterialIcon() => this._UpdateSelectMaterialIcon(true);

  private void UpdateSelectMaterialIcon() => this._UpdateSelectMaterialIcon(false);

  private void _UpdateSelectMaterialIcon(bool reset)
  {
    Transform grid = this.GetCtrl((Enum) this.inventoryUI);
    int base_item_index = Array.FindIndex<SortCompareData>(this.inventory.datas, (Predicate<SortCompareData>) (data => (long) data.GetUniqID() == (long) this.skillItem.uniqueID));
    int select_index = reset ? -1 : 1;
    this.materialSkillItem.ForEach((Action<SkillItemInfo>) (material =>
    {
      bool flag = false;
      int num = -1;
      int index = 0;
      for (int length = this.inventory.datas.Length; index < length; ++index)
      {
        if (index == base_item_index)
          flag = true;
        else if ((long) this.inventory.datas[index].GetUniqID() == (long) material.uniqueID)
        {
          num = index;
          break;
        }
      }
      if (num == -1)
        return;
      if (flag)
        --num;
      Transform child = grid.GetChild(num);
      if (this.inventoryUI == SmithGrowSkill.UI.GRD_INVENTORY)
      {
        ItemIconDetail componentInChildren = ((Component) child).GetComponentInChildren<ItemIconDetail>();
        if (Object.op_Inequality((Object) componentInChildren, (Object) null))
          componentInChildren.setupperSkill.SetupSelectNumberSprite(select_index);
      }
      else
      {
        ItemIconDetailSmall componentInChildren = ((Component) child).GetComponentInChildren<ItemIconDetailSmall>();
        if (Object.op_Inequality((Object) componentInChildren, (Object) null))
          componentInChildren.SetupSelectNumberSprite(select_index);
      }
      if (reset)
        return;
      ++select_index;
    }));
  }

  private void OnQuery_DECISION()
  {
    if (this.needGold > MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money)
      GameSection.ChangeEvent("NOT_ENOUGH_MONEY");
    else if (this.materialSkillItem == null || this.materialSkillItem.Count <= 0)
      GameSection.ChangeEvent("NOT_MATERIAL");
    else if (this.skillItem.IsLevelMax() && !this.skillItem.IsExistNextExceed())
    {
      GameSection.ChangeEvent("NOT_INCLUDE_EXCEED");
    }
    else
    {
      this.isNoticeSendGrow = true;
      GameSection.SetEventData((object) new object[2]
      {
        (object) this.skillItem,
        (object) this.materialSkillItem.ToArray()
      });
    }
  }

  private void OnCloseDialog_SmithGrowSkillConfirm() => this.isNoticeSendGrow = false;

  private void OnQuery_DETAIL()
  {
    GameSection.SetEventData((object) new object[2]
    {
      (object) ItemDetailEquip.CURRENT_SECTION.SMITH_SKILL_GROW,
      (object) (this.inventory.datas[(int) GameSection.GetEventData()] as SkillItemSortData)
    });
  }

  public void IconToggleChange()
  {
    if (this.toggleIndex == -1)
      return;
    Transform transform = this.GetCtrl((Enum) SmithGrowSkill.UI.GRD_INVENTORY).Find(this.toggleIndex.ToString());
    if (!Object.op_Inequality((Object) transform, (Object) null))
      return;
    ((Component) transform).GetComponentInChildren<UIToggle>().value = false;
    this.toggleIndex = -1;
  }

  protected void OnQuery_CHANGE_INVENTORY()
  {
    this.inventoryUIIndex = this.inventoryUIIndex + 1 < this.switchInventoryAry.Length ? this.inventoryUIIndex + 1 : 0;
    this.SetDirty((Enum) SmithGrowSkill.UI.GRD_INVENTORY);
    this.SetDirty((Enum) SmithGrowSkill.UI.GRD_INVENTORY_SMALL);
    this.RefreshUI();
  }

  protected void SetupEnableInventoryUI()
  {
    int index = 0;
    for (int length = this.switchInventoryAry.Length; index < length; ++index)
      this.SetActive((Enum) this.switchInventoryAry[index], false);
    this.SetActive((Enum) this.switchInventoryAry[this.inventoryUIIndex], true);
    this.inventoryUI = this.switchInventoryAry[this.inventoryUIIndex];
    this.SetToggle((Enum) SmithGrowSkill.UI.TGL_CHANGE_INVENTORY, this.inventoryUI == SmithGrowSkill.UI.GRD_INVENTORY);
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
    bool is_equipping = false)
  {
    return this.inventoryUI == SmithGrowSkill.UI.GRD_INVENTORY ? ItemIconDetail.CreateSkillDetailSelectNumberIcon(icon_type, icon_id, rarity, item_data, is_show_main_status, parent, event_name, event_data, is_new, toggle_group, select_number, is_equipping) : ItemIconDetailSmall.CreateSmallSkillSelectDetailIcon(icon_type, icon_id, rarity, item_data, parent, event_name, event_data, is_new, toggle_group, select_number, is_equipping);
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((flags & (GameSection.NOTIFY_FLAG.UPDATE_SKILL_FAVORITE | GameSection.NOTIFY_FLAG.UPDATE_SKILL_INVENTORY)) != (GameSection.NOTIFY_FLAG) 0)
    {
      this.skillItem = MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.Find(this.skillItem.uniqueID);
      List<SkillItemInfo> del_list = new List<SkillItemInfo>();
      this.materialSkillItem.ForEach((Action<SkillItemInfo>) (skill =>
      {
        SkillItemInfo skillItemInfo = MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.Find(skill.uniqueID);
        if (skillItemInfo != null && !skillItemInfo.isFavorite)
          return;
        del_list.Add(skill);
      }));
      del_list.ForEach((Action<SkillItemInfo>) (delitem => this.materialSkillItem.Remove(delitem)));
      this.inventory = (ItemStorageTop.SkillItemInventory) null;
      this.SetDirty((Enum) this.inventoryUI);
    }
    base.OnNotify(flags);
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return this.isNoticeSendGrow ? (GameSection.NOTIFY_FLAG) 0 : GameSection.NOTIFY_FLAG.UPDATE_SKILL_FAVORITE | GameSection.NOTIFY_FLAG.UPDATE_SKILL_INVENTORY;
  }

  protected new enum UI
  {
    OBJ_DETAIL_ROOT,
    TEX_MODEL,
    TEX_INNER_MODEL,
    LBL_NAME,
    LBL_LV_NOW,
    LBL_LV_MAX,
    OBJ_LV_EX,
    LBL_LV_EX,
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
    STR_NON_MATERIAL,
    LBL_EQUIP_ITEM_NAME,
    SCR_INVENTORY,
    GRD_INVENTORY,
    GRD_INVENTORY_SMALL,
    TGL_CHANGE_INVENTORY,
    LBL_SORT,
    BTN_BACK,
    LBL_GOLD,
    LBL_SELECT_NUM,
    STR_SELL,
    STR_TITLE_MATERIAL,
    STR_TITLE_MONEY,
  }
}
