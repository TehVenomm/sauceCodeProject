// Decompiled with JetBrains decompiler
// Type: StatusAvatarEquipSelect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class StatusAvatarEquipSelect : StatusEquip
{
  private EQUIPMENT_TYPE changeTargetType;
  private EquipItemInfo equippingItem;
  private EquipItemInfo selectItem;

  protected override EquipItemInfo EquipItem
  {
    get => this.selectItem;
    set => this.selectItem = value;
  }

  protected override EquipItemInfo GetCompareItemData() => this.equippingItem;

  public override void Initialize()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    this.changeTargetType = (EQUIPMENT_TYPE) eventData[0];
    this.equippingItem = eventData[1] as EquipItemInfo;
    this.selectEquipSetData = eventData[2] as StatusEquip.LocalEquipSetData;
    this.EquipItem = this.equippingItem;
    if (this.equippingItem == null)
      this.selectInventoryIndex = -1;
    else
      this.selectInventoryIndex = this.GetSelectItemIndex();
    GameSection.SetEventData((object) this.selectEquipSetData);
    MonoBehaviourSingleton<StatusManager>.I.SetEquippingItem(this.equippingItem);
    base.Initialize();
  }

  protected override void InitSort()
  {
    this.sortSettings = SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.ARMOR, SortSettings.SETTINGS_TYPE.EQUIP_ITEM);
  }

  protected override void InitLocalInventory()
  {
    this.localInventoryEquipData = (SortCompareData[]) this.CreateLocalnventory();
    this.sortSettings.Sort<EquipItemSortData>(this.localInventoryEquipData as EquipItemSortData[]);
  }

  protected override void SelectingInventoryFirst() => this.selectInventoryIndex = -1;

  protected override bool IsNotEquip(bool is_not_equip_any_slot, bool is_equip_now_slot)
  {
    return !is_equip_now_slot;
  }

  public override void UpdateUI()
  {
    this.SetActive((Enum) EquipSelectBase.UI.OBJ_STATUS_ROOT, false);
    base.UpdateUI();
  }

  protected override void EquipParam()
  {
  }

  protected override void EquipImg()
  {
    if (this.EquipItem != null)
      this.SetRenderEquipModel((Enum) EquipSelectBase.UI.TEX_MODEL, this.EquipItem.tableID);
    else
      this.ClearRenderModel((Enum) EquipSelectBase.UI.TEX_MODEL);
  }

  protected override void OnQuery_TRY_ON()
  {
    base.OnQuery_TRY_ON();
    if (this.EquipItem != null && !MonoBehaviourSingleton<GameSceneManager>.I.CheckEquipItemAndOpenUpdateAppDialog(this.EquipItem.tableData, new System.Action(((StatusEquip) this).OnCancelSelect)))
      GameSection.StopEvent();
    else
      this.EquipImg();
  }

  protected override bool IsCreateRemoveButton() => true;

  protected override bool IsAlreadyEquipItem(EquipItemInfo item) => false;

  private EquipItemSortData[] CreateLocalnventory()
  {
    EquipItemInfo[] array;
    switch (this.changeTargetType)
    {
      case EQUIPMENT_TYPE.HELM:
        array = MonoBehaviourSingleton<InventoryManager>.I.GetVisualHelmInventory().ToArray();
        break;
      case EQUIPMENT_TYPE.ARM:
        array = MonoBehaviourSingleton<InventoryManager>.I.GetVisualArmInventory().ToArray();
        break;
      case EQUIPMENT_TYPE.LEG:
        array = MonoBehaviourSingleton<InventoryManager>.I.GetVisualLegInventory().ToArray();
        break;
      default:
        array = MonoBehaviourSingleton<InventoryManager>.I.GetVisualArmorInventory().ToArray();
        break;
    }
    return this.sortSettings.CreateSortAry<EquipItemInfo, EquipItemSortData>(array);
  }

  protected override void OnQueryDetail()
  {
    int eventData = (int) GameSection.GetEventData();
    this.detailItem = (EquipItemInfo) null;
    if (eventData >= 0 && this.localInventoryEquipData != null)
      this.detailItem = this.localInventoryEquipData[eventData].GetItemData() as EquipItemInfo;
    if (this.detailItem == null)
      GameSection.StopEvent();
    else
      GameSection.SetEventData((object) new object[3]
      {
        (object) ItemDetailEquip.CURRENT_SECTION.STATUS_AVATAR,
        (object) this.detailItem,
        (object) this.selectEquipSetData.setNo
      });
  }

  protected override string GetSelectTypeText()
  {
    string selectTypeText = string.Empty;
    switch (this.changeTargetType)
    {
      case EQUIPMENT_TYPE.ARMOR:
        selectTypeText = this.sectionData.GetText("SELECT_ARMOR");
        break;
      case EQUIPMENT_TYPE.HELM:
        selectTypeText = this.sectionData.GetText("SELECT_HELM");
        break;
      case EQUIPMENT_TYPE.ARM:
        selectTypeText = this.sectionData.GetText("SELECT_ARM");
        break;
      case EQUIPMENT_TYPE.LEG:
        selectTypeText = this.sectionData.GetText("SELECT_LEG");
        break;
    }
    return selectTypeText;
  }
}
