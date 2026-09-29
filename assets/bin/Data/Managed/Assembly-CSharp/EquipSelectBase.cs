// Decompiled with JetBrains decompiler
// Type: EquipSelectBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public abstract class EquipSelectBase : SmithEquipBase
{
  protected SortCompareData[] localInventoryEquipData;
  protected SortSettings sortSettings;
  protected int selectInventoryIndex;
  protected EquipSelectBase.UI InventoryUI;
  protected EquipSelectBase.UI[] switchInventoryAry = new EquipSelectBase.UI[2]
  {
    EquipSelectBase.UI.GRD_INVENTORY,
    EquipSelectBase.UI.GRD_INVENTORY
  };
  protected int inventoryUIIndex;
  protected EquipSelectBase.UI[] weaponInventoryAry = new EquipSelectBase.UI[2]
  {
    EquipSelectBase.UI.GRD_INVENTORY,
    EquipSelectBase.UI.GRD_INVENTORY
  };
  protected EquipSelectBase.UI[] defenseInventoryAry = new EquipSelectBase.UI[2]
  {
    EquipSelectBase.UI.GRD_INVENTORY_DEF,
    EquipSelectBase.UI.GRD_INVENTORY_DEF
  };

  protected bool IsShowMainStatus => this.inventoryUIIndex == 0;

  public override void Initialize()
  {
    this.type = SmithEquipBase.EquipDialogType.SELECT;
    this.inventoryUIIndex = 0;
    this.SetDirty((Enum) EquipSelectBase.UI.GRD_INVENTORY);
    this.SetDirty((Enum) EquipSelectBase.UI.GRD_INVENTORY_SMALL);
    this.SetDirty((Enum) EquipSelectBase.UI.GRD_INVENTORY_DEF);
    this.SetDirty((Enum) EquipSelectBase.UI.GRD_INVENTORY_SMALL_DEF);
    this.InitSort();
    this.InitLocalInventory();
    base.Initialize();
  }

  protected virtual void InitSort()
  {
  }

  protected virtual void InitLocalInventory()
  {
  }

  protected virtual void Update() => this.ObserveItemList();

  public override void UpdateUI()
  {
    if (((object) this.EquipItem ?? (object) this.GetEquipTableData()) == null)
      this.SelectingInventoryFirst();
    else
      this.selectInventoryIndex = this.GetSelectItemIndex();
    this.SetLabelText((Enum) EquipSelectBase.UI.LBL_SELECT_TYPE, this.GetSelectTypeText());
    this.SetToggle((Enum) EquipSelectBase.UI.TGL_ICON_ASC, this.sortSettings.orderTypeAsc);
    this.SetFontStyle((Enum) EquipSelectBase.UI.STR_TITLE_ITEM_INFO, (FontStyle) 2);
    this.SetFontStyle((Enum) EquipSelectBase.UI.STR_TITLE_STATUS, (FontStyle) 2);
    this.SetFontStyle((Enum) EquipSelectBase.UI.STR_TITLE_SKILL_SLOT, (FontStyle) 2);
    this.SetFontStyle((Enum) EquipSelectBase.UI.STR_TITLE_ABILITY, (FontStyle) 2);
    this.SetFontStyle((Enum) EquipSelectBase.UI.STR_TITLE_MONEY, (FontStyle) 2);
    this.SetFontStyle((Enum) EquipSelectBase.UI.STR_TITLE_MATERIAL, (FontStyle) 2);
    this.SetFontStyle((Enum) EquipSelectBase.UI.STR_TITLE_ELEMENT, (FontStyle) 2);
    base.UpdateUI();
  }

  protected virtual string GetSelectTypeText() => string.Empty;

  protected virtual EquipItemInfo EquipItem
  {
    get => this.GetEquipData();
    set
    {
    }
  }

  protected virtual EquipItemInfo GetCompareItemData() => (EquipItemInfo) null;

  private void SetLabelEquipItemParam(EquipItemInfo item, EquipItemInfo comp_item = null)
  {
    int atk = item.atk;
    int def = item.def;
    int elemAtk = item.elemAtk;
    int elemDef = item.elemDef;
    if (comp_item != null)
    {
      atk = comp_item.atk;
      def = comp_item.def;
      elemAtk = comp_item.elemAtk;
      elemDef = comp_item.elemDef;
    }
    bool is_visible = item.tableData.IsWeapon();
    this.SetActive((Enum) EquipSelectBase.UI.OBJ_ATK_ROOT, is_visible);
    this.SetActive((Enum) EquipSelectBase.UI.OBJ_DEF_ROOT, !is_visible);
    this.SetLabelText((Enum) EquipSelectBase.UI.LBL_LV_NOW, item.level.ToString());
    this.SetLabelText((Enum) EquipSelectBase.UI.LBL_LV_MAX, item.tableData.maxLv.ToString());
    if (is_visible)
    {
      this.SetActive((Enum) EquipSelectBase.UI.OBJ_ELEM_ROOT, item.elemAtk > 0);
      this.SetElementSprite((Enum) EquipSelectBase.UI.SPR_ELEM, item.GetElemAtkType());
      this.SetLabelCompareParam((Enum) EquipSelectBase.UI.LBL_ATK, item.atk, atk);
      this.SetLabelCompareParam((Enum) EquipSelectBase.UI.LBL_ELEM, item.elemAtk, elemAtk);
    }
    else
    {
      this.SetActive((Enum) EquipSelectBase.UI.OBJ_ELEM_ROOT, item.elemDef > 0);
      this.SetDefElementSprite((Enum) EquipSelectBase.UI.SPR_ELEM, item.GetElemDefType());
      this.SetLabelCompareParam((Enum) EquipSelectBase.UI.LBL_DEF, item.def, def);
      this.SetLabelCompareParam((Enum) EquipSelectBase.UI.LBL_ELEM, item.elemDef, elemDef);
    }
  }

  protected override void EquipParam()
  {
    EquipItemInfo item = this.EquipItem;
    EquipItemTable.EquipItemData tableData = item != null ? item.tableData : (EquipItemTable.EquipItemData) null;
    if (item == null || tableData == null)
      return;
    this.SetLabelText((Enum) EquipSelectBase.UI.LBL_NAME, tableData.name);
    this.SetLabelEquipItemParam(item, this.GetCompareItemData());
    this.SetActive((Enum) EquipSelectBase.UI.SPR_IS_EVOLVE, item.tableData.IsEvolve());
    this.SetSkillIconButton((Enum) EquipSelectBase.UI.OBJ_SKILL_BUTTON_ROOT, "SkillIconButton", tableData, this.GetSkillSlotData(item));
    this.SetEquipmentTypeIcon((Enum) EquipSelectBase.UI.SPR_TYPE_ICON, (Enum) EquipSelectBase.UI.SPR_TYPE_ICON_BG, (Enum) EquipSelectBase.UI.SPR_TYPE_ICON_RARITY, item.tableData);
    this.SetLabelText((Enum) EquipSelectBase.UI.LBL_SELL, item.sellPrice.ToString());
    if (item.ability != null && item.ability.Length != 0)
    {
      bool empty_ability = true;
      this.SetTable((Enum) EquipSelectBase.UI.TBL_ABILITY, "ItemDetailEquipAbilityItem", item.ability.Length, false, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
      {
        EquipItemAbility equipItemAbility = item.ability[i];
        if (equipItemAbility.id == 0U)
        {
          this.SetActive(t, false);
        }
        else
        {
          empty_ability = false;
          this.SetActive(t, true);
          if (item.IsFixedAbility(i))
          {
            this.SetActive(t, (Enum) EquipSelectBase.UI.OBJ_ABILITY, false);
            this.SetActive(t, (Enum) EquipSelectBase.UI.OBJ_FIXEDABILITY, true);
            this.SetLabelText(t, (Enum) EquipSelectBase.UI.LBL_FIXEDABILITY, equipItemAbility.GetName());
            this.SetLabelText(t, (Enum) EquipSelectBase.UI.LBL_FIXEDABILITY_NUM, equipItemAbility.GetAP());
          }
          else
          {
            this.SetLabelText(t, (Enum) EquipSelectBase.UI.LBL_ABILITY, equipItemAbility.GetName());
            this.SetLabelText(t, (Enum) EquipSelectBase.UI.LBL_ABILITY_NUM, equipItemAbility.GetAP());
          }
          this.SetEvent(t, "ABILITY", i);
        }
      }));
      if (empty_ability)
        this.SetActive((Enum) EquipSelectBase.UI.STR_NON_ABILITY, true);
      else
        this.SetActive((Enum) EquipSelectBase.UI.STR_NON_ABILITY, false);
    }
    else
      this.SetActive((Enum) EquipSelectBase.UI.STR_NON_ABILITY, true);
  }

  protected override void LocalInventory()
  {
    this.SetupEnableInventoryUI();
    if (this.localInventoryEquipData != null)
    {
      this.SetLabelText((Enum) EquipSelectBase.UI.LBL_SORT, this.sortSettings.GetSortLabel());
      this.m_generatedIconList.Clear();
      this.UpdateNewIconInfo();
      bool initItem = false;
      this.SetDynamicList((Enum) this.InventoryUI, (string) null, this.localInventoryEquipData.Length, false, (Func<int, bool>) (i =>
      {
        SortCompareData sortCompareData = this.localInventoryEquipData[i];
        return sortCompareData != null && sortCompareData.IsPriority(this.sortSettings.orderTypeAsc);
      }), (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
      {
        initItem = true;
        uint tableId = this.localInventoryEquipData[i].GetTableID();
        if (tableId == 0U)
        {
          this.SetActive(t, false);
        }
        else
        {
          this.SetActive(t, true);
          Singleton<EquipItemTable>.I.GetEquipItemData(tableId);
          EquipItemSortData equipItemSortData = this.localInventoryEquipData[i] as EquipItemSortData;
          EquipItemInfo itemData = equipItemSortData.GetItemData() as EquipItemInfo;
          bool is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(equipItemSortData.GetIconType(), equipItemSortData.GetUniqID());
          SkillSlotUIData[] skillSlotData = this.GetSkillSlotData(itemData);
          ItemIcon itemIconDetail = this.CreateItemIconDetail(equipItemSortData, skillSlotData, this.IsShowMainStatus, t, "TRY_ON", i, equipItemSortData.GetIconStatus(), is_new);
          itemIconDetail.SetItemID(equipItemSortData.GetTableID());
          itemIconDetail.SetButtonColor(this.localInventoryEquipData[i].IsPriority(this.sortSettings.orderTypeAsc), true);
          this.SetLongTouch(itemIconDetail.transform, "DETAIL", (object) i);
          itemIconDetail.SetInitData((SortCompareData) equipItemSortData);
          if (this.m_generatedIconList.Contains(itemIconDetail))
            return;
          this.m_generatedIconList.Add(itemIconDetail);
        }
      }));
      this.SetActive(this._transform, (Enum) EquipSelectBase.UI.LBL_NO_ITEM, !initItem);
      this.SetLabelText(this._transform, (Enum) EquipSelectBase.UI.LBL_NO_ITEM, StringTable.Get(STRING_CATEGORY.COMMON, 19799U));
    }
    else
    {
      this.SetActive(this._transform, (Enum) EquipSelectBase.UI.LBL_NO_ITEM, true);
      this.SetLabelText(this._transform, (Enum) EquipSelectBase.UI.LBL_NO_ITEM, StringTable.Get(STRING_CATEGORY.COMMON, 19799U));
    }
  }

  protected void OnQuery_SORT() => GameSection.SetEventData((object) this.sortSettings.Clone());

  protected void OnCloseSortDialog()
  {
    SortSettings eventData = (SortSettings) GameSection.GetEventData();
    if (eventData == null)
      return;
    this.sortSettings = eventData;
    if (this.localInventoryEquipData == null || !this.sorting())
      return;
    this.selectInventoryIndex = this.GetSelectItemIndex();
    this.SetDirty((Enum) EquipSelectBase.UI.GRD_INVENTORY);
    this.SetDirty((Enum) EquipSelectBase.UI.GRD_INVENTORY_SMALL);
    this.SetDirty((Enum) EquipSelectBase.UI.GRD_INVENTORY_DEF);
    this.SetDirty((Enum) EquipSelectBase.UI.GRD_INVENTORY_SMALL_DEF);
    this.RefreshUI();
  }

  protected virtual void OnQuery_TRY_ON() => this.EquipParam();

  protected virtual void OnQuery_SELECT_ITEM()
  {
  }

  protected void OnQuery_DETAIL() => this.OnQueryDetail();

  protected virtual void OnQuery_SKILL_ICON_BUTTON()
  {
    ItemDetailEquip.CURRENT_SECTION currentSection;
    object obj;
    if (this.smithType == SmithEquipBase.SmithType.GROW || this.smithType == SmithEquipBase.SmithType.EVOLVE || this.smithType == SmithEquipBase.SmithType.ABILITY_CHANGE || this.smithType == SmithEquipBase.SmithType.REVERT_LITHOGRAPH)
    {
      currentSection = ItemDetailEquip.CURRENT_SECTION.SMITH_GROW;
      obj = (object) this.EquipItem;
    }
    else
    {
      currentSection = ItemDetailEquip.CURRENT_SECTION.SMITH_CREATE;
      obj = (object) this.GetEquipTableData();
    }
    GameSection.SetEventData((object) new object[2]
    {
      (object) currentSection,
      obj
    });
  }

  protected virtual void OnQueryDetail()
  {
    GameSection.SetEventData((object) new object[2]
    {
      (object) ItemDetailEquip.CURRENT_SECTION.SMITH_GROW,
      (object) this.EquipItem
    });
  }

  private void OnQuery_ABILITY()
  {
    int eventData = (int) GameSection.GetEventData();
    EquipItemAbility event_data = this.smithType == SmithEquipBase.SmithType.GROW || this.smithType == SmithEquipBase.SmithType.EVOLVE || this.smithType == SmithEquipBase.SmithType.ABILITY_CHANGE || this.smithType == SmithEquipBase.SmithType.REVERT_LITHOGRAPH ? new EquipItemAbility(this.EquipItem.ability[eventData].id, 0) : new EquipItemAbility((uint) this.GetEquipTableData().fixedAbility[eventData].id, 0);
    if (event_data == null)
      GameSection.StopEvent();
    else
      GameSection.SetEventData((object) event_data);
  }

  protected virtual void SelectingInventoryFirst() => this.selectInventoryIndex = 0;

  protected virtual bool sorting() => false;

  protected virtual int GetSelectItemIndex() => -1;

  protected void SetupEnableInventoryUI()
  {
    int index = 0;
    for (int length = this.switchInventoryAry.Length; index < length; ++index)
      this.SetActive((Enum) this.switchInventoryAry[index], false);
    this.SetActive((Enum) this.switchInventoryAry[this.inventoryUIIndex], true);
    this.InventoryUI = this.switchInventoryAry[this.inventoryUIIndex];
    this.SetToggle((Enum) EquipSelectBase.UI.TGL_CHANGE_INVENTORY, this.InventoryUI == EquipSelectBase.UI.GRD_INVENTORY || this.InventoryUI == EquipSelectBase.UI.GRD_INVENTORY_DEF);
  }

  protected virtual void OnQuery_CHANGE_INVENTORY()
  {
    this.inventoryUIIndex = this.inventoryUIIndex + 1 < this.switchInventoryAry.Length ? this.inventoryUIIndex + 1 : 0;
    this.SetDirty((Enum) EquipSelectBase.UI.GRD_INVENTORY);
    this.SetDirty((Enum) EquipSelectBase.UI.GRD_INVENTORY_SMALL);
    this.SetDirty((Enum) EquipSelectBase.UI.GRD_INVENTORY_DEF);
    this.SetDirty((Enum) EquipSelectBase.UI.GRD_INVENTORY_SMALL_DEF);
    this.RefreshUI();
  }

  protected virtual ItemIcon CreateRemoveIcon(
    Transform parent = null,
    string event_name = null,
    int event_data = 0,
    int toggle_group = -1,
    bool is_select = false,
    string name = null)
  {
    return this.InventoryUI == EquipSelectBase.UI.GRD_INVENTORY || this.InventoryUI == EquipSelectBase.UI.GRD_INVENTORY_DEF ? ItemIconDetail.CreateRemoveButton(parent, event_name, event_data, toggle_group, is_select, name) : ItemIconDetailSmall.CreateSmallRemoveButton(parent, event_name, event_data, toggle_group, is_select, name);
  }

  protected ItemIcon CreateItemIconDetail(
    EquipItemSortData item_data,
    SkillSlotUIData[] skill_slot_data,
    bool is_show_main_status,
    Transform parent = null,
    string event_name = null,
    int event_data = 0,
    ItemIconDetail.ICON_STATUS icon_status = ItemIconDetail.ICON_STATUS.NONE,
    bool is_new = false,
    int toggle_group = -1,
    bool is_select = false,
    int equip_index = -1)
  {
    return this.InventoryUI == EquipSelectBase.UI.GRD_INVENTORY || this.InventoryUI == EquipSelectBase.UI.GRD_INVENTORY_DEF ? ItemIconDetail.CreateEquipDetailIcon(item_data, skill_slot_data, is_show_main_status, parent, event_name, event_data, icon_status, is_new, toggle_group, is_select, equip_index) : ItemIconDetailSmall.CreateSmallEquipDetailIcon(item_data, parent, event_name, event_data, icon_status, is_new, toggle_group, is_select, equip_index);
  }

  protected ItemIcon CreateSmithCreateItemIconDetail(
    ITEM_ICON_TYPE icon_type,
    int icon_id,
    RARITY_TYPE? rarity,
    SmithCreateSortData item_data,
    SkillSlotUIData[] skill_slot_data,
    bool is_show_main_status,
    Transform parent = null,
    string event_name = null,
    int event_data = 0,
    ItemIconDetail.ICON_STATUS icon_status = ItemIconDetail.ICON_STATUS.NONE,
    bool is_new = false,
    int toggle_group = -1,
    bool is_select = false,
    GET_TYPE getType = GET_TYPE.PAY)
  {
    bool is_visible = MonoBehaviourSingleton<AchievementManager>.I.CheckEquipItemCollection(item_data.createData.equipTableData);
    if (this.InventoryUI == EquipSelectBase.UI.GRD_INVENTORY)
    {
      ItemIcon createEquipDetailIcon = ItemIconDetail.CreateSmithCreateEquipDetailIcon(icon_type, icon_id, rarity, item_data, skill_slot_data, is_show_main_status, parent, event_name, event_data, icon_status, is_new, toggle_group, is_select, getType: getType);
      ItemIconDetail itemIconDetail = createEquipDetailIcon as ItemIconDetail;
      if (!Object.op_Inequality((Object) itemIconDetail, (Object) null))
        return createEquipDetailIcon;
      itemIconDetail.setupperEquip.SetRegistedIcon(is_visible);
      return createEquipDetailIcon;
    }
    ItemIcon createEquipDetailIcon1 = ItemIconDetailSmall.CreateSmithCreateEquipDetailIcon(icon_type, icon_id, rarity, item_data, parent, event_name, event_data, icon_status, is_new, toggle_group, is_select, getType: getType);
    ItemIconDetailSmall itemIconDetailSmall = createEquipDetailIcon1 as ItemIconDetailSmall;
    if (!Object.op_Inequality((Object) itemIconDetailSmall, (Object) null))
      return createEquipDetailIcon1;
    itemIconDetailSmall.SetRegistedIcon(is_visible);
    return createEquipDetailIcon1;
  }

  public enum UI
  {
    LBL_NAME,
    LBL_LV_NOW,
    LBL_LV_MAX,
    LBL_ATK,
    LBL_DEF,
    LBL_ELEM,
    SPR_ELEM,
    LBL_SELL,
    TEX_MODEL,
    SCR_INVENTORY,
    GRD_INVENTORY,
    GRD_INVENTORY_SMALL,
    LBL_SORT,
    BTN_SORT,
    BTN_BACK,
    TGL_CHANGE_INVENTORY,
    TGL_ICON_ASC,
    OBJ_SKILL_BUTTON_ROOT,
    OBJ_DETAIL_ROOT,
    BTN_SELL,
    BTN_GROW,
    OBJ_FAVORITE_ROOT,
    SPR_IS_EVOLVE,
    OBJ_ATK_ROOT,
    OBJ_DEF_ROOT,
    OBJ_ELEM_ROOT,
    OBJ_SELL_ROOT,
    SPR_SELECT_WEAPON,
    SPR_SELECT_DEF,
    SPR_TYPE_ICON,
    SPR_TYPE_ICON_BG,
    SPR_TYPE_ICON_RARITY,
    LBL_SELECT_TYPE,
    OBJ_STATUS_ROOT,
    LBL_STATUS_ATK,
    LBL_STATUS_DEF,
    LBL_STATUS_HP,
    LBL_STATUS_ADD_ATK,
    LBL_STATUS_ADD_DEF,
    LBL_STATUS_ADD_HP,
    STR_TITLE_ITEM_INFO,
    STR_TITLE_STATUS,
    STR_TITLE_SKILL_SLOT,
    STR_TITLE_ABILITY,
    STR_TITLE_MONEY,
    STR_TITLE_MATERIAL,
    STR_TITLE_ELEMENT,
    TBL_ABILITY,
    STR_NON_ABILITY,
    OBJ_ABILITY,
    LBL_ABILITY,
    LBL_ABILITY_NUM,
    OBJ_FIXEDABILITY,
    LBL_FIXEDABILITY,
    LBL_FIXEDABILITY_NUM,
    OBJ_ABILITY_ITEM,
    LBL_ABILITY_ITEM,
    OBJ_WEAPON_WINDOW,
    OBJ_DEFENSE_WINDOW,
    SCR_INVENTORY_DEF,
    GRD_INVENTORY_DEF,
    GRD_INVENTORY_SMALL_DEF,
    BTN_WEAPON_1,
    BTN_WEAPON_2,
    BTN_WEAPON_3,
    BTN_WEAPON_4,
    BTN_WEAPON_5,
    OBJ_CAPTION_3,
    LBL_CAPTION,
    OBJ_ROOT,
    LBL_NO_ITEM,
  }
}
