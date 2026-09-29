// Decompiled with JetBrains decompiler
// Type: SmithRevertLithographSelect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class SmithRevertLithographSelect : SmithEquipSelectBase
{
  protected override string prefabSuffix => "Lithograph";

  public override void Initialize()
  {
    this.smithType = SmithEquipBase.SmithType.REVERT_LITHOGRAPH;
    this.switchInventoryAry = new EquipSelectBase.UI[2]
    {
      EquipSelectBase.UI.GRD_INVENTORY,
      EquipSelectBase.UI.GRD_INVENTORY
    };
    GameSection.SetEventData((object) EQUIPMENT_TYPE.ONE_HAND_SWORD);
    base.Initialize();
    GameSection.SetEventData((object) null);
    this.InitializeCaption(this.sectionData.GetText("CAPTION"));
  }

  public override void InitializeReopen()
  {
    this.InitLocalInventory();
    base.InitializeReopen();
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_USER_STATUS | GameSection.NOTIFY_FLAG.UPDATE_EQUIP_GROW | GameSection.NOTIFY_FLAG.UPDATE_EQUIP_EVOLVE | GameSection.NOTIFY_FLAG.UPDATE_EQUIP_FAVORITE | GameSection.NOTIFY_FLAG.UPDATE_SKILL_CHANGE | GameSection.NOTIFY_FLAG.UPDATE_ITEM_INVENTORY | GameSection.NOTIFY_FLAG.UPDATE_EQUIP_INVENTORY | GameSection.NOTIFY_FLAG.UPDATE_SKILL_INVENTORY | GameSection.NOTIFY_FLAG.UPDATE_EQUIP_ABILITY;
  }

  public override void UpdateUI()
  {
    this.SetActive(this.GetCtrl((Enum) this.uiTypeTab[this.weaponPickupIndex]).parent, false);
    this.SetActive(this.GetCtrl((Enum) this.uiTypeTab[this.armorPickupIndex]).parent, false);
    base.UpdateUI();
  }

  protected override void InitSort()
  {
    this.sortSettings = SortSettings.CreateMemSortSettings(MonoBehaviourSingleton<InventoryManager>.I.IsWeaponInventoryType(this.selectInventoryType) ? SortBase.DIALOG_TYPE.WEAPON : SortBase.DIALOG_TYPE.ARMOR, SortSettings.SETTINGS_TYPE.EQUIP_ITEM);
  }

  protected override void InitLocalInventory()
  {
    MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType = this.selectInventoryType;
    MonoBehaviourSingleton<SmithManager>.I.CreateLocalInventory();
    this.selectInventoryIndex = -1;
    this.localInventoryEquipData = (SortCompareData[]) this.sortSettings.CreateSortAry<EquipItemInfo, EquipItemSortData>(MonoBehaviourSingleton<SmithManager>.I.localInventoryEquipData as EquipItemInfo[]);
  }

  protected override void LocalInventory()
  {
    this.SetupEnableInventoryUI();
    if (this.localInventoryEquipData == null)
      return;
    this.SetLabelText((Enum) SmithEquipSelectBase.UI.LBL_SORT, this.sortSettings.GetSortLabel());
    this.m_generatedIconList.Clear();
    this.UpdateNewIconInfo();
    this.SetDynamicList((Enum) this.InventoryUI, (string) null, this.localInventoryEquipData.Length, false, (Func<int, bool>) (i =>
    {
      SortCompareData sortCompareData = this.localInventoryEquipData[i];
      if (sortCompareData == null || !sortCompareData.IsPriority(this.sortSettings.orderTypeAsc))
        return false;
      uint tableId = this.localInventoryEquipData[i].GetTableID();
      return Singleton<EquipItemTable>.I.GetEquipItemData(tableId).IsRevertable();
    }), (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      if (this.localInventoryEquipData[i].GetTableID() == 0U)
      {
        this.SetActive(t, false);
      }
      else
      {
        this.SetActive(t, true);
        EquipItemSortData equipItemSortData = this.localInventoryEquipData[i] as EquipItemSortData;
        EquipItemInfo itemData = equipItemSortData.GetItemData() as EquipItemInfo;
        bool is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(equipItemSortData.GetIconType(), equipItemSortData.GetUniqID());
        SkillSlotUIData[] skillSlotData = this.GetSkillSlotData(itemData);
        ItemIcon itemIconDetail = this.CreateItemIconDetail(equipItemSortData, skillSlotData, this.IsShowMainStatus, t, "SELECT_ITEM", i, is_new: is_new);
        itemIconDetail.SetItemID(equipItemSortData.GetTableID());
        itemIconDetail.SetButtonColor(this.localInventoryEquipData[i].IsPriority(this.sortSettings.orderTypeAsc), true);
        itemIconDetail.SetGrayout(this.IsRequiredIconGrayOut((SortCompareData) equipItemSortData));
        this.SetLongTouch(itemIconDetail.transform, "DETAIL", (object) i);
        if (Object.op_Inequality((Object) itemIconDetail, (Object) null) && equipItemSortData != null)
          itemIconDetail.SetInitData((SortCompareData) equipItemSortData);
        if (!Object.op_Inequality((Object) itemIconDetail, (Object) null) || this.m_generatedIconList.Contains(itemIconDetail))
          return;
        this.m_generatedIconList.Add(itemIconDetail);
      }
    }));
  }

  protected override bool sorting()
  {
    this.InitLocalInventory();
    return true;
  }

  protected virtual bool IsRequiredIconGrayOut(SortCompareData _data)
  {
    return _data.IsFavorite() || _data.IsEquipping();
  }

  protected override void SelectingInventoryFirst()
  {
    if (this.localInventoryEquipData == null || this.localInventoryEquipData.Length == 0 || this.localInventoryEquipData[0] == null)
      return;
    SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
    if (smithData == null)
      return;
    smithData.selectEquipData = this.localInventoryEquipData[0].GetItemData() as EquipItemInfo;
    this.selectInventoryIndex = 0;
  }

  protected override int GetSelectItemIndex()
  {
    if (this.localInventoryEquipData == null || this.localInventoryEquipData.Length == 0)
      return -1;
    EquipItemInfo equipData = this.GetEquipData();
    if (equipData == null)
      return -2;
    int selectItemIndex = 0;
    for (int length = this.localInventoryEquipData.Length; selectItemIndex < length; ++selectItemIndex)
    {
      if ((long) equipData.uniqueID == (long) this.localInventoryEquipData[selectItemIndex].GetUniqID())
        return selectItemIndex;
    }
    return -1;
  }

  protected void OnCloseDialog_SmithEquipChangeSort() => this.OnCloseSortDialog();

  protected override void OnQuery_SELECT_ITEM()
  {
    if (this.localInventoryEquipData == null || this.localInventoryEquipData.Length == 0)
    {
      GameSection.StopEvent();
    }
    else
    {
      EquipItemSortData event_data = this.localInventoryEquipData[(int) GameSection.GetEventData()] as EquipItemSortData;
      if (!event_data.CanSale())
      {
        if (event_data.IsFavorite())
          GameSection.ChangeEvent("NOT_REVERT_FAVORITE", (object) StringTable.Get(STRING_CATEGORY.SMITH, 14U));
        else if (event_data.IsHomeEquipping())
          GameSection.ChangeEvent("NOT_REVERT_EQUIPPING");
        else
          GameSection.ChangeEvent("NOT_REVERT_UNIQUE_EQUIPPING");
      }
      else
      {
        uint tableId = event_data.GetTableID();
        if (!Singleton<EquipItemTable>.I.GetEquipItemData(tableId).IsRevertable())
          GameSection.StopEvent();
        else
          GameSection.SetEventData((object) event_data);
      }
    }
  }

  protected override void OnQueryDetail()
  {
    SortCompareData sortCompareData = this.localInventoryEquipData[(int) GameSection.GetEventData()];
    if (sortCompareData == null)
    {
      GameSection.StopEvent();
    }
    else
    {
      ulong uniqId = sortCompareData.GetUniqID();
      if (uniqId != 0UL)
        MonoBehaviourSingleton<SmithManager>.I.CreateSmithData<SmithManager.SmithGrowData>().selectEquipData = MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(uniqId);
      base.OnQueryDetail();
    }
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_ITEM_INVENTORY) != (GameSection.NOTIFY_FLAG) 0 || (flags & GameSection.NOTIFY_FLAG.UPDATE_EQUIP_INVENTORY) != (GameSection.NOTIFY_FLAG) 0)
    {
      this.SetDirty((Enum) this.InventoryUI);
      this.InitLocalInventory();
    }
    base.OnNotify(flags);
  }
}
