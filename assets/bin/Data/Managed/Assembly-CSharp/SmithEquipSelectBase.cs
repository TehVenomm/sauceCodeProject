// Decompiled with JetBrains decompiler
// Type: SmithEquipSelectBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public abstract class SmithEquipSelectBase : EquipSelectBase
{
  protected SmithEquipSelectBase.UI[] uiTypeTab = new SmithEquipSelectBase.UI[11]
  {
    SmithEquipSelectBase.UI.BTN_WEAPON_1,
    SmithEquipSelectBase.UI.BTN_WEAPON_2,
    SmithEquipSelectBase.UI.BTN_WEAPON_3,
    SmithEquipSelectBase.UI.BTN_WEAPON_4,
    SmithEquipSelectBase.UI.BTN_WEAPON_5,
    SmithEquipSelectBase.UI.BTN_ARMOR,
    SmithEquipSelectBase.UI.BTN_HELM,
    SmithEquipSelectBase.UI.BTN_ARM,
    SmithEquipSelectBase.UI.BTN_LEG,
    SmithEquipSelectBase.UI.BTN_WEAPON_PICKUP,
    SmithEquipSelectBase.UI.BTN_ARMOR_PICKUP
  };
  private InventoryManager.INVENTORY_TYPE[] inventoryType = new InventoryManager.INVENTORY_TYPE[11]
  {
    InventoryManager.INVENTORY_TYPE.ONE_HAND_SWORD,
    InventoryManager.INVENTORY_TYPE.TWO_HAND_SWORD,
    InventoryManager.INVENTORY_TYPE.SPEAR,
    InventoryManager.INVENTORY_TYPE.PAIR_SWORDS,
    InventoryManager.INVENTORY_TYPE.ARROW,
    InventoryManager.INVENTORY_TYPE.ARMOR,
    InventoryManager.INVENTORY_TYPE.HELM,
    InventoryManager.INVENTORY_TYPE.ARM,
    InventoryManager.INVENTORY_TYPE.LEG,
    InventoryManager.INVENTORY_TYPE.ALL_WEAPON,
    InventoryManager.INVENTORY_TYPE.ALL_ARMOR
  };
  protected int selectTypeIndex;
  protected int weaponPickupIndex;
  protected int armorPickupIndex;
  protected SmithEquipSelectBase.UI[] tabAnimTarget = new SmithEquipSelectBase.UI[2]
  {
    SmithEquipSelectBase.UI.SPR_SELECT_WEAPON,
    SmithEquipSelectBase.UI.SPR_SELECT_DEF
  };

  protected InventoryManager.INVENTORY_TYPE selectInventoryType
  {
    get => this.inventoryType[this.selectTypeIndex];
  }

  protected abstract string prefabSuffix { get; }

  public override void Initialize()
  {
    this.selectTypeIndex = UIBehaviour.GetEquipmentTypeIndex((EQUIPMENT_TYPE) GameSection.GetEventData());
    this.weaponPickupIndex = Array.FindIndex<SmithEquipSelectBase.UI>(this.uiTypeTab, (Predicate<SmithEquipSelectBase.UI>) (ui => ui == SmithEquipSelectBase.UI.BTN_WEAPON_PICKUP));
    this.armorPickupIndex = Array.FindIndex<SmithEquipSelectBase.UI>(this.uiTypeTab, (Predicate<SmithEquipSelectBase.UI>) (ui => ui == SmithEquipSelectBase.UI.BTN_ARMOR_PICKUP));
    this.SetPrefab((Enum) SmithEquipSelectBase.UI.OBJ_ROOT, "SmithEquipSelectBase_" + this.prefabSuffix);
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.UpdateTabButton();
    this.SetupInventoryTypeToggole();
    base.UpdateUI();
  }

  protected virtual void SetupInventoryTypeToggole()
  {
    bool flag = this.selectTypeIndex < UIBehaviour.GetEquipmentTypeIndex(EQUIPMENT_TYPE.ARMOR) || this.selectTypeIndex == this.weaponPickupIndex;
    this.SetActive((Enum) SmithEquipSelectBase.UI.OBJ_ATK_ROOT, flag);
    this.SetActive((Enum) SmithEquipSelectBase.UI.OBJ_DEF_ROOT, !flag);
    this.SetToggleButton((Enum) SmithEquipSelectBase.UI.TGL_BUTTON_ROOT, flag, (Action<bool>) (is_active =>
    {
      EQUIPMENT_TYPE type = is_active ? EQUIPMENT_TYPE.ONE_HAND_SWORD : EQUIPMENT_TYPE.HELM;
      int index = is_active ? 0 : 1;
      this.ResetTween((Enum) this.tabAnimTarget[index]);
      this.PlayTween((Enum) this.tabAnimTarget[index], is_input_block: false);
      this.SetActive((Enum) SmithEquipSelectBase.UI.OBJ_ATK_ROOT, is_active);
      this.SetActive((Enum) SmithEquipSelectBase.UI.OBJ_DEF_ROOT, !is_active);
      this.selectTypeIndex = UIBehaviour.GetEquipmentTypeIndex(type);
      this.sortSettings.dialogType = this.GetDialogType(is_active);
      if ((this.sortSettings.requirement & (this.sortSettings.dialogType != SortBase.DIALOG_TYPE.ARMOR ? SortBase.SORT_REQUIREMENT.REQUIREMENT_WEAPON_BIT : SortBase.SORT_REQUIREMENT.REQUIREMENT_ARMORS_BIT)) == (SortBase.SORT_REQUIREMENT) 0)
      {
        if (this.sortSettings.requirement == SortBase.SORT_REQUIREMENT.ATK)
          this.sortSettings.requirement = SortBase.SORT_REQUIREMENT.DEF;
        else if (this.sortSettings.requirement == SortBase.SORT_REQUIREMENT.DEF)
          this.sortSettings.requirement = SortBase.SORT_REQUIREMENT.ATK;
        else if (this.sortSettings.requirement == SortBase.SORT_REQUIREMENT.ELEM_ATK)
          this.sortSettings.requirement = SortBase.SORT_REQUIREMENT.ELEM_DEF;
        else if (this.sortSettings.requirement == SortBase.SORT_REQUIREMENT.ELEM_DEF)
          this.sortSettings.requirement = SortBase.SORT_REQUIREMENT.ELEM_ATK;
        else
          this.sortSettings.requirement = SortBase.SORT_REQUIREMENT.ELEMENT;
      }
      this.SetDirty((Enum) this.InventoryUI);
      this.InitLocalInventory();
      this.LocalInventory();
      this.UpdateTabButton();
    }));
  }

  protected virtual SortBase.DIALOG_TYPE GetDialogType(bool isWeapon)
  {
    return !isWeapon ? SortBase.DIALOG_TYPE.ARMOR : SortBase.DIALOG_TYPE.WEAPON;
  }

  protected void ShowNoItemText()
  {
  }

  protected override void EquipParam()
  {
  }

  protected virtual void OnQuery_TYPE_TAB()
  {
    this.selectTypeIndex = (int) GameSection.GetEventData();
    this.InitLocalInventory();
    this.SetDirty((Enum) this.InventoryUI);
    this.RefreshUI();
  }

  protected void UpdateTabButton()
  {
    int event_data = 0;
    for (int length = this.uiTypeTab.Length; event_data < length; ++event_data)
      this.SetEvent((Enum) this.uiTypeTab[event_data], "TYPE_TAB", event_data);
    this.SetToggle((Enum) this.uiTypeTab[this.selectTypeIndex], true);
  }

  protected virtual void OnQuery_SECTION_BACK()
  {
    MonoBehaviourSingleton<SmithManager>.I.BackSection();
    MonoBehaviourSingleton<SmithManager>.I.DisableSmithBlur(false);
  }

  protected void InitializeCaption(string caption)
  {
    Transform ctrl = this.GetCtrl((Enum) SmithEquipSelectBase.UI.OBJ_CAPTION_3);
    this.SetLabelText(ctrl, (Enum) SmithEquipSelectBase.UI.LBL_CAPTION, caption);
    UITweenCtrl component = ((Component) ctrl).gameObject.GetComponent<UITweenCtrl>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.Reset();
    int index = 0;
    for (int length = component.tweens.Length; index < length; ++index)
      component.tweens[index].ResetToBeginning();
    component.Play();
  }

  protected new enum UI
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
    LBL_ABILITY,
    LBL_ABILITY_NUM,
    BTN_HELM,
    BTN_ARMOR,
    BTN_ARM,
    BTN_LEG,
    BTN_ARMOR_PICKUP,
    BTN_WEAPON_1,
    BTN_WEAPON_2,
    BTN_WEAPON_3,
    BTN_WEAPON_4,
    BTN_WEAPON_5,
    BTN_WEAPON_PICKUP,
    TGL_BUTTON_ROOT,
    GRD_WEAPON,
    GRD_ARMOR,
    OBJ_CAPTION_3,
    LBL_CAPTION,
    OBJ_ROOT,
    LBL_NO_ITEM,
  }
}
