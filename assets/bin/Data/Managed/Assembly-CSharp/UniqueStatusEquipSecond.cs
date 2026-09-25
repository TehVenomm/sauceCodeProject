// Decompiled with JetBrains decompiler
// Type: UniqueStatusEquipSecond
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class UniqueStatusEquipSecond : StatusEquip
{
  private UniqueStatusEquipSecond.UI[] tgl = new UniqueStatusEquipSecond.UI[5]
  {
    UniqueStatusEquipSecond.UI.BTN_WEAPON_1,
    UniqueStatusEquipSecond.UI.BTN_WEAPON_2,
    UniqueStatusEquipSecond.UI.BTN_WEAPON_3,
    UniqueStatusEquipSecond.UI.BTN_WEAPON_4,
    UniqueStatusEquipSecond.UI.BTN_WEAPON_5
  };
  protected object[] backEventData;
  private int swapSetNo;
  private int swapSlotNo;

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
      this.SetActive((Enum) UniqueStatusEquipSecond.UI.OBJ_WEAPON_WINDOW, true);
      this.SetActive((Enum) UniqueStatusEquipSecond.UI.OBJ_DEFENSE_WINDOW, false);
      this.SetToggle((Enum) this.tgl[(int) (MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType - 1)], true);
      text = this.sectionData.GetText("CAPTION_WEAPON");
    }
    else
    {
      this.switchInventoryAry = this.defenseInventoryAry;
      this.SetActive((Enum) UniqueStatusEquipSecond.UI.OBJ_WEAPON_WINDOW, false);
      this.SetActive((Enum) UniqueStatusEquipSecond.UI.OBJ_DEFENSE_WINDOW, true);
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
    base.OnQuery_TRY_ON();
    this.OnQuery_SELECT_ITEM();
  }

  protected void OnQuery_UniqueStatusRemoveEquipConfirm_YES()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<StatusManager>.I.RemoveOrderNo(this.selectEquipSetData.setNo, (Action<bool>) (is_success =>
    {
      GameSection.ResumeEvent(is_success);
      GameSection.SetEventData((object) new StatusEquip.ChangeEquipData(this.selectEquipSetData.setNo, this.selectEquipSetData.index, (EquipItemInfo) null));
    }));
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
    Transform ctrl = this.GetCtrl((Enum) UniqueStatusEquipSecond.UI.OBJ_CAPTION_3);
    this.SetLabelText(ctrl, (Enum) UniqueStatusEquipSecond.UI.LBL_CAPTION, caption);
    UITweenCtrl component = ((Component) ctrl).gameObject.GetComponent<UITweenCtrl>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.Reset();
    int index = 0;
    for (int length = component.tweens.Length; index < length; ++index)
      component.tweens[index].ResetToBeginning();
    component.Play();
  }

  protected override int GetEquipIndex(EquipItemInfo select_item)
  {
    EquipSetInfo[] localEquipSet = MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSet();
    if (select_item == null)
      return -1;
    for (int index = 0; index < localEquipSet.Length; ++index)
    {
      EquipSetInfo equipSetInfo = localEquipSet[index];
      for (int equipIndex = 0; equipIndex < equipSetInfo.item.Length; ++equipIndex)
      {
        if (equipSetInfo.item[equipIndex] != null && equipSetInfo.item[equipIndex].uniqueID != 0UL && (long) select_item.uniqueID == (long) equipSetInfo.item[equipIndex].uniqueID)
        {
          this.swapSetNo = index;
          this.swapSlotNo = equipIndex;
          return equipIndex;
        }
      }
    }
    return -1;
  }

  protected override bool IsAlreadyEquipItem(EquipItemInfo item)
  {
    if (this.GetEquipIndex(item) < 0)
      return false;
    return this.selectEquipSetData.index != this.swapSlotNo || this.selectEquipSetData.setNo != this.swapSetNo;
  }

  protected void OnQuery_UniqueStatusSwapEquipConfirm_YES()
  {
    int swapIndex = this.selectEquipSetData.EquippingIndexOf(this.EquipItem);
    int index = this.selectEquipSetData.index;
    EquipItemInfo equipItemInfo = this.selectEquipSetData.equipSetInfo.item[index];
    this.selectEquipSetData.equipSetInfo.item[index] = this.selectEquipSetData.equipSetInfo.item[swapIndex];
    this.selectEquipSetData.equipSetInfo.item[swapIndex] = equipItemInfo;
    MonoBehaviourSingleton<StatusManager>.I.SwapUniqueWeapon(swapIndex, index);
  }

  protected void OnQuery_UniqueStatusClosetSwapEquipConfirm_YES()
  {
    int index = this.selectEquipSetData.index;
    EquipSetInfo[] localEquipSet = MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSet();
    this.selectEquipSetData.equipSetInfo.item[index] = localEquipSet[this.swapSetNo].item[this.swapSlotNo];
    localEquipSet[this.swapSetNo].item[this.swapSlotNo] = (EquipItemInfo) null;
    MonoBehaviourSingleton<StatusManager>.I.ReplaceUniqueEquipSet(this.selectEquipSetData.equipSetInfo, this.selectEquipSetData.setNo);
    MonoBehaviourSingleton<StatusManager>.I.ReplaceUniqueEquipSet(localEquipSet[this.swapSetNo], this.swapSetNo);
  }

  protected void OnQuery_UniqueStatusOrderSwapEquipConfirm_YES()
  {
    int index = this.selectEquipSetData.index;
    EquipSetInfo[] localEquipSet = MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSet();
    this.selectEquipSetData.equipSetInfo.item[index] = localEquipSet[this.swapSetNo].item[this.swapSlotNo];
    localEquipSet[this.swapSetNo].item[this.swapSlotNo] = (EquipItemInfo) null;
    MonoBehaviourSingleton<StatusManager>.I.ReplaceUniqueEquipSet(this.selectEquipSetData.equipSetInfo, this.selectEquipSetData.setNo);
    MonoBehaviourSingleton<StatusManager>.I.ReplaceUniqueEquipSet(localEquipSet[this.swapSetNo], this.swapSetNo);
    GameSection.StayEvent();
    MonoBehaviourSingleton<StatusManager>.I.RemoveOrderNo(this.swapSetNo, (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
  }

  protected override void ChangeSwapEquipConfirm(int slotNo, string equipName)
  {
    EquipSetInfo[] localEquipSet = MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSet();
    if (this.swapSetNo != this.selectEquipSetData.setNo)
    {
      if (localEquipSet[this.swapSetNo].order > 0)
        GameSection.ChangeEvent("SWAP_CONFIRM_EQUIP_ORDER", (object) new object[1]
        {
          (object) equipName
        });
      else
        GameSection.ChangeEvent("SWAP_CONFIRM_OTHER_CLOSET", (object) new object[1]
        {
          (object) equipName
        });
    }
    else
      base.ChangeSwapEquipConfirm(slotNo, equipName);
  }

  protected override bool IsRemoveEquipSloat(int equip_slot_index) => true;

  protected override ItemIcon CreateRemoveIcon(
    Transform parent,
    string event_name,
    int event_data = 0,
    int toggle_group = -1,
    bool is_select = false,
    string name = null)
  {
    if (this.selectEquipSetData.equipSetInfo.order > 0)
      event_name = "EQUIP_ORDER_REMOVE";
    return base.CreateRemoveIcon(parent, event_name, event_data, toggle_group, is_select, name);
  }

  protected override bool IsCreateRemoveButton() => true;

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
