// Decompiled with JetBrains decompiler
// Type: StatusEquipSecond
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class StatusEquipSecond : StatusEquip
{
  private StatusEquipSecond.UI[] tgl = new StatusEquipSecond.UI[5]
  {
    StatusEquipSecond.UI.BTN_WEAPON_1,
    StatusEquipSecond.UI.BTN_WEAPON_2,
    StatusEquipSecond.UI.BTN_WEAPON_3,
    StatusEquipSecond.UI.BTN_WEAPON_4,
    StatusEquipSecond.UI.BTN_WEAPON_5
  };
  protected object[] backEventData;

  public override void Initialize()
  {
    object eventData = GameSection.GetEventData();
    if (eventData is ItemDetailEquip.DetailEquipEventData)
    {
      this.backEventData = (eventData as ItemDetailEquip.DetailEquipEventData).currentEventData;
      GameSection.SetEventData((object) (eventData as ItemDetailEquip.DetailEquipEventData).localEquipSetData);
    }
    this.SetupTargetInventory();
    base.Initialize();
  }

  protected override SortCompareData[] CreateSortAry()
  {
    return (SortCompareData[]) this.sortSettings.CreateSortAry<EquipItemInfo, EquipItemSortWithPayCheckData>(MonoBehaviourSingleton<SmithManager>.I.localInventoryEquipData as EquipItemInfo[]);
  }

  private void SetupTargetInventory()
  {
    string text;
    if (MonoBehaviourSingleton<InventoryManager>.I.IsWeaponInventoryType(MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType))
    {
      this.switchInventoryAry = this.weaponInventoryAry;
      this.SetActive((Enum) StatusEquipSecond.UI.OBJ_WEAPON_WINDOW, true);
      this.SetActive((Enum) StatusEquipSecond.UI.OBJ_DEFENSE_WINDOW, false);
      this.SetToggle((Enum) this.tgl[(int) (MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType - 1)], true);
      text = this.sectionData.GetText("CAPTION_WEAPON");
    }
    else
    {
      this.switchInventoryAry = this.defenseInventoryAry;
      this.SetActive((Enum) StatusEquipSecond.UI.OBJ_WEAPON_WINDOW, false);
      this.SetActive((Enum) StatusEquipSecond.UI.OBJ_DEFENSE_WINDOW, true);
      text = this.sectionData.GetText("CAPTION_DEFENCE");
    }
    this.InitializeCaption(text);
  }

  private void OnQuery_TAB_1()
  {
    int index = 0;
    this.SetToggle((Enum) this.tgl[index], true);
    this.LimitedInventory(index);
  }

  private void OnQuery_TAB_2()
  {
    int index = 1;
    this.SetToggle((Enum) this.tgl[index], true);
    this.LimitedInventory(index);
  }

  private void OnQuery_TAB_3()
  {
    int index = 2;
    this.SetToggle((Enum) this.tgl[index], true);
    this.LimitedInventory(index);
  }

  private void OnQuery_TAB_4()
  {
    int index = 3;
    this.SetToggle((Enum) this.tgl[index], true);
    this.LimitedInventory(index);
  }

  private void OnQuery_TAB_5()
  {
    int index = 4;
    this.SetToggle((Enum) this.tgl[index], true);
    this.LimitedInventory(index);
  }

  private void LimitedInventory(int index)
  {
    MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType = (InventoryManager.INVENTORY_TYPE) (index + 1);
    this.InitLocalInventory();
    this.SetDirty((Enum) this.InventoryUI);
    this.RefreshUI();
  }

  protected override void InitSort() => base.InitSort();

  public override void UpdateUI() => base.UpdateUI();

  protected override SortBase.DIALOG_TYPE GetDialogType(bool isWeapon)
  {
    return !isWeapon ? SortBase.DIALOG_TYPE.TYPE_FILTERABLE_ARMOR : SortBase.DIALOG_TYPE.TYPE_FILTERABLE_WEAPON;
  }

  protected override void _OnOpenStatusStage()
  {
  }

  protected override void _OnCloseStatusStage()
  {
  }

  protected override void EquipParam()
  {
  }

  protected override void OnQuery_TRY_ON()
  {
    if (GameSaveData.instance.canPushTrackEquipTutorial)
      GameSaveData.instance.SetPushTrackEquipTutorial(false);
    base.OnQuery_TRY_ON();
    GameSection.ChangeEvent("SELECT_ITEM");
    this.OnQuery_SELECT_ITEM();
  }

  private void OnQuery_SECTION_BACK()
  {
    if (this.backEventData == null)
      return;
    GameSection.SetEventData((object) this.backEventData);
    this.Close(UITransition.TYPE.CLOSE);
  }

  private void InitializeCaption(string caption)
  {
    Transform ctrl = this.GetCtrl((Enum) StatusEquipSecond.UI.OBJ_CAPTION_3);
    this.SetLabelText(ctrl, (Enum) StatusEquipSecond.UI.LBL_CAPTION, caption);
    UITweenCtrl component = ((Component) ctrl).gameObject.GetComponent<UITweenCtrl>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.Reset();
    int index = 0;
    for (int length = component.tweens.Length; index < length; ++index)
      component.tweens[index].ResetToBeginning();
    component.Play();
  }

  public new enum UI
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
  }
}
