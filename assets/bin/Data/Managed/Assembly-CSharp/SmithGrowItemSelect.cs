// Decompiled with JetBrains decompiler
// Type: SmithGrowItemSelect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class SmithGrowItemSelect : SmithEquipSelectBase
{
  protected override string prefabSuffix => "Grow";

  public override IEnumerable<string> requireDataTable
  {
    get
    {
      yield return "GrowEquipItemNeedItemTable";
      foreach (string str in base.requireDataTable)
        yield return str;
    }
  }

  protected override SortBase.DIALOG_TYPE GetDialogType(bool isWeapon)
  {
    return !isWeapon ? SortBase.DIALOG_TYPE.TYPE_FILTERABLE_ARMOR : SortBase.DIALOG_TYPE.TYPE_FILTERABLE_WEAPON;
  }

  public override void Initialize()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    this.smithType = (SmithEquipBase.SmithType) eventData[0];
    GameSection.SetEventData(eventData[1]);
    base.Initialize();
    GameSection.SetEventData((object) null);
    this.InitializeCaption(!MonoBehaviourSingleton<InventoryManager>.I.IsWeaponInventoryType(this.selectInventoryType) ? this.sectionData.GetText("CAPTION_DEFENCE") : this.sectionData.GetText("CAPTION_WEAPON"));
  }

  public override void UpdateUI()
  {
    this.SetActive(this.GetCtrl((Enum) this.uiTypeTab[this.weaponPickupIndex]).parent, false);
    this.SetActive(this.GetCtrl((Enum) this.uiTypeTab[this.armorPickupIndex]).parent, false);
    if (MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>() == null)
      MonoBehaviourSingleton<SmithManager>.I.CreateSmithData<SmithManager.SmithGrowData>();
    base.UpdateUI();
  }

  protected override string GetSelectTypeText()
  {
    return this.sectionData.GetText(this.smithType == SmithEquipBase.SmithType.GROW ? "TYPE_GROW" : "TYPE_EVOLVE");
  }

  protected override void OnOpen()
  {
    if (GameSection.GetEventData() is SmithEquipBase.SmithType eventData && this.smithType != eventData)
    {
      this.smithType = eventData;
      this.InitLocalInventory();
    }
    SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
    if (smithData == null)
      return;
    ulong uniqueId = smithData.selectEquipData.uniqueID;
    int index = 0;
    for (int length = this.localInventoryEquipData.Length; index < length; ++index)
    {
      if ((long) uniqueId == (long) this.localInventoryEquipData[index].GetUniqID())
        this.localInventoryEquipData[index].SetItem((object) smithData.selectEquipData);
    }
  }

  protected override void InitSort()
  {
    this.sortSettings = SortSettings.CreateMemSortSettings(this.GetDialogType(MonoBehaviourSingleton<InventoryManager>.I.IsWeaponInventoryType(this.selectInventoryType)), SortSettings.SETTINGS_TYPE.EQUIP_ITEM);
  }

  protected override void InitLocalInventory()
  {
    MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType = this.selectInventoryType;
    MonoBehaviourSingleton<SmithManager>.I.CreateLocalInventory();
    this.selectInventoryIndex = -1;
    this.localInventoryEquipData = (SortCompareData[]) this.sortSettings.CreateSortAry<EquipItemInfo, EquipItemSortWithPayCheckData>(MonoBehaviourSingleton<SmithManager>.I.localInventoryEquipData as EquipItemInfo[]);
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
    bool flag = GameSceneEvent.current.eventName == "TRY_ON";
    if (this.localInventoryEquipData == null || this.localInventoryEquipData.Length == 0)
      return;
    this.selectInventoryIndex = (int) GameSection.GetEventData();
    SortCompareData sortCompareData = this.localInventoryEquipData[this.selectInventoryIndex];
    if (sortCompareData == null)
      return;
    ulong uniqId = sortCompareData.GetUniqID();
    if (uniqId != 0UL)
      MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>().selectEquipData = MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(uniqId);
    base.OnQuery_TRY_ON();
    if (!flag)
      return;
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
      SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
      if (smithData == null)
      {
        GameSection.StopEvent();
      }
      else
      {
        EquipItemInfo selectEquipData = smithData.selectEquipData;
        if (!selectEquipData.IsLevelMax())
          return;
        if (selectEquipData.tableData.IsEvolve())
        {
          GameSection.ChangeEvent("EVOLVE");
        }
        else
        {
          if (!selectEquipData.IsExceedMax() || selectEquipData.tableData.IsShadow())
            return;
          GameSection.ChangeEvent("ALREADY_LV_MAX");
        }
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
        MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>().selectEquipData = MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(uniqId);
      base.OnQueryDetail();
    }
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG notify_flags)
  {
    if ((notify_flags & (GameSection.NOTIFY_FLAG.UPDATE_EQUIP_GROW | GameSection.NOTIFY_FLAG.UPDATE_EQUIP_EVOLVE | GameSection.NOTIFY_FLAG.UPDATE_EQUIP_FAVORITE)) != (GameSection.NOTIFY_FLAG) 0)
    {
      SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
      if (smithData != null && smithData.selectEquipData != null)
        smithData.selectEquipData = MonoBehaviourSingleton<InventoryManager>.I.GetEquipItem(smithData.selectEquipData.uniqueID);
    }
    if ((notify_flags & GameSection.NOTIFY_FLAG.UPDATE_EQUIP_INVENTORY) != (GameSection.NOTIFY_FLAG) 0 || (notify_flags & GameSection.NOTIFY_FLAG.UPDATE_SKILL_INVENTORY) != (GameSection.NOTIFY_FLAG) 0)
    {
      this.InitLocalInventory();
      if (this.GetSelectItemIndex() < 0)
        this.SelectingInventoryFirst();
    }
    base.OnNotify(notify_flags);
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_USER_STATUS | GameSection.NOTIFY_FLAG.UPDATE_EQUIP_GROW | GameSection.NOTIFY_FLAG.UPDATE_EQUIP_EVOLVE | GameSection.NOTIFY_FLAG.UPDATE_EQUIP_FAVORITE | GameSection.NOTIFY_FLAG.UPDATE_SKILL_CHANGE | GameSection.NOTIFY_FLAG.UPDATE_ITEM_INVENTORY | GameSection.NOTIFY_FLAG.UPDATE_EQUIP_INVENTORY | GameSection.NOTIFY_FLAG.UPDATE_SKILL_INVENTORY;
  }

  public override EventData CheckAutoEvent(string event_name, object event_data)
  {
    if (event_name == "TRY_ON")
    {
      ulong num = (ulong) event_data;
      int _data = 0;
      for (int length = this.localInventoryEquipData.Length; _data < length; ++_data)
      {
        if ((long) this.localInventoryEquipData[_data].GetUniqID() == (long) num)
          return new EventData(event_name, (object) _data);
      }
    }
    return base.CheckAutoEvent(event_name, event_data);
  }
}
