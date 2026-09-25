// Decompiled with JetBrains decompiler
// Type: SmithAbilityChangeSelect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class SmithAbilityChangeSelect : SmithEquipSelectBase
{
  protected override string prefabSuffix => "Ability";

  public override void Initialize()
  {
    this.smithType = SmithEquipBase.SmithType.ABILITY_CHANGE;
    this.switchInventoryAry = new EquipSelectBase.UI[2]
    {
      EquipSelectBase.UI.GRD_INVENTORY,
      EquipSelectBase.UI.GRD_INVENTORY
    };
    GameSection.SetEventData((object) EQUIPMENT_TYPE.ONE_HAND_SWORD);
    base.Initialize();
    this.inventoryUIIndex = 1;
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
    EquipItemInfo[] inventoryEquipData = MonoBehaviourSingleton<SmithManager>.I.localInventoryEquipData as EquipItemInfo[];
    List<EquipItemInfo> equipItemInfoList = new List<EquipItemInfo>(inventoryEquipData.Length);
    int index = 0;
    for (int length = inventoryEquipData.Length; index < length; ++index)
    {
      EquipItemInfo equipItemInfo = inventoryEquipData[index];
      if (equipItemInfo.GetValidLotAbility() > 0 || equipItemInfo.tableData.IsShadow())
        equipItemInfoList.Add(equipItemInfo);
    }
    this.localInventoryEquipData = (SortCompareData[]) this.sortSettings.CreateSortAry<EquipItemInfo, EquipItemSortData>(equipItemInfoList.ToArray());
  }

  protected override void LocalInventory()
  {
    this.SetupEnableInventoryUI();
    if (this.localInventoryEquipData != null)
    {
      this.SetLabelText((Enum) SmithEquipSelectBase.UI.LBL_SORT, this.sortSettings.GetSortLabel());
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
          EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(tableId);
          EquipItemSortData equipItemSortData = this.localInventoryEquipData[i] as EquipItemSortData;
          EquipItemInfo itemData = equipItemSortData.GetItemData() as EquipItemInfo;
          ITEM_ICON_TYPE iconType = equipItemSortData.GetIconType();
          bool is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(iconType, equipItemSortData.GetUniqID());
          SkillSlotUIData[] skillSlotData = this.GetSkillSlotData(itemData);
          ItemIcon abilityIconDetail = this.CreateEquipAbilityIconDetail(iconType, equipItemData.GetIconID(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex), new RARITY_TYPE?(equipItemData.rarity), equipItemSortData, skillSlotData, this.IsShowMainStatus, t, "TRY_ON", i, equipItemSortData.GetIconStatus(), is_new, getType: equipItemSortData.GetGetType());
          abilityIconDetail.SetItemID(equipItemSortData.GetTableID());
          abilityIconDetail.SetButtonColor(this.localInventoryEquipData[i].IsPriority(this.sortSettings.orderTypeAsc), true);
          this.SetLongTouch(abilityIconDetail.transform, "DETAIL", (object) i);
          if (Object.op_Inequality((Object) abilityIconDetail, (Object) null) && equipItemSortData != null)
            abilityIconDetail.SetInitData((SortCompareData) equipItemSortData);
          if (!Object.op_Inequality((Object) abilityIconDetail, (Object) null) || this.m_generatedIconList.Contains(abilityIconDetail))
            return;
          this.m_generatedIconList.Add(abilityIconDetail);
        }
      }));
      this.SetActive(this.GetCtrl((Enum) SmithEquipSelectBase.UI.OBJ_ROOT), (Enum) SmithEquipSelectBase.UI.LBL_NO_ITEM, !initItem);
      this.SetLabelText(this.GetCtrl((Enum) SmithEquipSelectBase.UI.OBJ_ROOT), (Enum) SmithEquipSelectBase.UI.LBL_NO_ITEM, StringTable.Get(STRING_CATEGORY.COMMON, 19799U));
    }
    else
    {
      this.SetActive(this.GetCtrl((Enum) SmithEquipSelectBase.UI.OBJ_ROOT), (Enum) SmithEquipSelectBase.UI.LBL_NO_ITEM, true);
      this.SetLabelText(this.GetCtrl((Enum) SmithEquipSelectBase.UI.OBJ_ROOT), (Enum) SmithEquipSelectBase.UI.LBL_NO_ITEM, StringTable.Get(STRING_CATEGORY.COMMON, 19799U));
    }
  }

  protected ItemIcon CreateEquipAbilityIconDetail(
    ITEM_ICON_TYPE icon_type,
    int icon_id,
    RARITY_TYPE? rarity,
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
    int equip_index = -1,
    GET_TYPE getType = GET_TYPE.PAY)
  {
    return ItemIconDetail.CreateEquipAbilityIcon(icon_type, icon_id, rarity, item_data, skill_slot_data, is_show_main_status, parent, event_name, event_data, icon_status, is_new, toggle_group, is_select, equip_index, getType);
  }

  protected override bool sorting()
  {
    this.InitLocalInventory();
    return true;
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

  protected override void OnQuery_TRY_ON()
  {
    if (this.localInventoryEquipData == null || this.localInventoryEquipData.Length == 0)
      return;
    this.selectInventoryIndex = (int) GameSection.GetEventData();
    SortCompareData sortCompareData = this.localInventoryEquipData[this.selectInventoryIndex];
    if (sortCompareData == null)
      return;
    ulong uniqId = sortCompareData.GetUniqID();
    if (uniqId == 0UL)
      return;
    MonoBehaviourSingleton<SmithManager>.I.CreateSmithData<SmithManager.SmithGrowData>().selectEquipData = MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(uniqId);
    GameSection.ChangeEvent("SELECT_ITEM");
    this.OnQuery_SELECT_ITEM();
  }

  protected override void OnQuery_SELECT_ITEM()
  {
    if (this.localInventoryEquipData == null || this.localInventoryEquipData.Length == 0)
    {
      GameSection.StopEvent();
    }
    else
    {
      if (MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>() != null)
        return;
      GameSection.StopEvent();
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
}
