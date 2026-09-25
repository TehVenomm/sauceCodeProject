// Decompiled with JetBrains decompiler
// Type: ItemDetailEquipSkillSelect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ItemDetailEquipSkillSelect : SkillSelectBaseSecond
{
  private int slotIndex;
  private bool isPurgeBtn;
  private bool is_not_enable_skill_type;
  private bool isSelfSectionChange;

  public override void Initialize()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    this.slotIndex = (int) eventData[3];
    GameSection.SetEventData((object) new object[3]
    {
      eventData[0],
      eventData[1],
      eventData[2]
    });
    base.Initialize();
  }

  public override void UpdateUI()
  {
    base.UpdateUI();
    this.SetVisibleEmptySkillType(this.isVisibleEmptySkill, this.slotIndex);
    if (!Object.op_Inequality((Object) this.detailBase, (Object) null))
      return;
    this.SetActive(this.detailBase, (Enum) SkillSelectBaseSecond.UI.OBJ_FAVORITE_ROOT, false);
  }

  protected override void SetInventoryIsEmptyParam()
  {
    bool is_empty = true;
    if (this.inventory != null && this.inventory.datas.Length != 0 && this.equipItem != null)
    {
      SkillItemTable.SkillSlotData[] skillSlot = this.equipItem.tableData.GetSkillSlot(this.equipItem.exceed);
      if (skillSlot != null && skillSlot.Length > this.slotIndex)
      {
        SKILL_SLOT_TYPE skill_slot_type = skillSlot[this.slotIndex].slotType;
        Array.ForEach<SortCompareData>(this.inventory.datas, (Action<SortCompareData>) (_data =>
        {
          if (!is_empty || _data == null || (_data.GetItemData() as SkillItemInfo).tableData.type != skill_slot_type)
            return;
          is_empty = false;
        }));
      }
    }
    this.isVisibleEmptySkill = is_empty;
  }

  protected override ItemStorageTop.SkillItemInventory CreateInventory()
  {
    return new ItemStorageTop.SkillItemInventory(SortSettings.SETTINGS_TYPE.SKILL_ITEM, this.equipItem.tableData.GetSkillSlot(this.equipItem.exceed)[this.slotIndex].slotType);
  }

  protected override void UpdateInventoryUI()
  {
    int find_index = -1;
    if (this.equipSkillItem != null)
    {
      find_index = Array.FindIndex<SortCompareData>(this.inventory.datas, (Predicate<SortCompareData>) (data => (long) data.GetUniqID() == (long) this.equipSkillItem.uniqueID));
      if (find_index > -1 && (this.inventory.datas[find_index] == null || !this.inventory.datas[find_index].IsPriority(this.inventory.sortSettings.orderTypeAsc)))
        find_index = -1;
    }
    this.SetupEnableInventoryUI();
    this.m_generatedIconList.Clear();
    this.UpdateNewIconInfo();
    int equipStartIndex = 1;
    if (this.IsCreateAllRemove())
      equipStartIndex++;
    this.SetDynamicList((Enum) this.inventoryUI, (string) null, this.inventory.datas.Length + 3, false, (Func<int, bool>) (i =>
    {
      switch (i)
      {
        case 0:
          return this.equipSkillItem != null;
        case 1:
          if (this.IsCreateAllRemove())
            return true;
          break;
      }
      bool flag1 = false;
      bool flag2 = true;
      int index = i - equipStartIndex;
      if (find_index >= 0)
      {
        if (index == 0)
          flag1 = true;
        else
          --index;
      }
      if (!flag1 && (index >= this.inventory.datas.Length || find_index >= 0 && index == find_index))
        flag2 = false;
      if (flag2)
      {
        SortCompareData data = this.inventory.datas[index];
        if (data == null || !data.IsPriority(this.inventory.sortSettings.orderTypeAsc))
          flag2 = false;
      }
      return flag2;
    }), (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      switch (i)
      {
        case 0:
          if (this.isVisibleEmptySkill)
            return;
          this.CreateRemoveIcon(t, "SELECT", -1, 100, this.selectIndex == -1, this.sectionData.GetText("STR_DETACH"));
          return;
        case 1:
          if (this.IsCreateAllRemove())
          {
            if (this.isVisibleEmptySkill)
              return;
            this.CreateRemoveIcon(t, "SELECT", -2, 100, name: this.sectionData.GetText("STR_DETACH_ALL"));
            return;
          }
          break;
      }
      int event_data = i - equipStartIndex;
      if (find_index >= 0)
      {
        if (event_data == 0)
          event_data = find_index;
        else
          --event_data;
      }
      this.SetActive(t, true);
      SortCompareData data = this.inventory.datas[event_data];
      SkillItemInfo itemData = data.GetItemData() as SkillItemInfo;
      ITEM_ICON_TYPE iconType = data.GetIconType();
      bool is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(iconType, data.GetUniqID());
      ItemIcon itemIconDetail = this.CreateItemIconDetail(iconType, data.GetIconID(), new RARITY_TYPE?(data.GetRarity()), data as SkillItemSortData, this.IsShowMainStatus, t, "SELECT", event_data, is_new, 100, this.selectIndex == event_data, this.IsEquipSetAttached(itemData), data.IsExceeded());
      itemIconDetail.SetItemID(data.GetTableID());
      this.SetLongTouch(itemIconDetail.transform, "DETAIL", (object) event_data);
      if (Object.op_Inequality((Object) itemIconDetail, (Object) null) && data != null)
        itemIconDetail.SetInitData(data);
      if (this.m_generatedIconList.Contains(itemIconDetail))
        return;
      this.m_generatedIconList.Add(itemIconDetail);
    }));
  }

  protected override int GetInventoryFirstIndex() => -1;

  private bool IsCreateAllRemove() => !StatusManager.IsUnique();

  private bool IsEquipSetAttached(SkillItemInfo skill)
  {
    return StatusManager.IsUnique() ? skill.IsUniqueEquipSetAttached : skill.IsCurrentEquipSetAttached;
  }

  protected override void OnDecision()
  {
    List<GameSectionHistory.HistoryData> historyList = MonoBehaviourSingleton<GameSceneManager>.I.GetHistoryList();
    string sectionName = historyList[historyList.Count - 2].sectionName;
    bool flag = sectionName == "ItemDetailSkillDialog" || sectionName == "ItemDetailSkill";
    if (this.selectIndex == -1)
    {
      if (this.equipSkillItem == null)
      {
        GameSection.BackSection();
      }
      else
      {
        GameSection.ChangeEvent("DETACH");
        this.SendDetachEquipSkill();
      }
    }
    else if (this.selectIndex == -2)
    {
      GameSection.ChangeEvent("DETACH_FROM_EVERY");
    }
    else
    {
      SortCompareData data = this.inventory.datas[this.selectIndex];
      if (this.equipSkillItem != null && (long) this.equipSkillItem.uniqueID == (long) data.GetUniqID())
      {
        GameSection.BackSection();
      }
      else
      {
        EquipItemInfo equipItemInfo = (EquipItemInfo) null;
        SkillItemInfo itemData = data.GetItemData() as SkillItemInfo;
        if (this.IsEquipSetAttached(itemData))
        {
          EquipSetSkillData uniqueEquipSetSkill = itemData.equipSetSkill.Find((Predicate<EquipSetSkillData>) (x => x.equipSetNo == MonoBehaviourSingleton<StatusManager>.I.GetCurrentEquipSetNo()));
          if (StatusManager.IsUnique())
            uniqueEquipSetSkill = itemData.uniqueEquipSetSkill;
          equipItemInfo = MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(uniqueEquipSetSkill.equipItemUniqId);
        }
        if (this.equipSkillItem != null)
        {
          if (!this.IsEquipSetAttached(itemData))
          {
            GameSection.ChangeEvent(flag ? "EQUIP_DETAIL" : "EQUIP");
            this.CheckSendEquipSkill();
          }
          else
            GameSection.ChangeEvent(flag ? "STEAL_DETAIL" : "STEAL", (object) new object[5]
            {
              (object) this.equipSkillItem.tableData.name,
              (object) this.equipSkillItem.level.ToString(),
              (object) equipItemInfo.tableData.name,
              (object) data.GetName(),
              (object) data.GetLevel().ToString()
            });
        }
        else if (this.IsEquipSetAttached(itemData))
        {
          GameSection.ChangeEvent(flag ? "REPLACE_DETAIL" : "REPLACE", (object) new object[3]
          {
            (object) equipItemInfo.tableData.name,
            (object) data.GetName(),
            (object) data.GetLevel().ToString()
          });
        }
        else
        {
          GameSection.ChangeEvent(flag ? "EQUIP_DETAIL" : "EQUIP");
          this.CheckSendEquipSkill();
        }
      }
    }
  }

  private void CheckSendEquipSkill()
  {
    this.is_not_enable_skill_type = !this.CheckEnableSkillType();
    if (this.is_not_enable_skill_type)
      this.ToNotEnableSkillTypeConfirm();
    else
      this._SendEquipSkill();
  }

  private bool CheckEnableSkillType()
  {
    SortCompareData data = this.inventory.datas[this.selectIndex];
    if (data != null)
    {
      SkillItemInfo itemData = data.GetItemData() as SkillItemInfo;
      EQUIPMENT_TYPE? enableEquipType = itemData.tableData.GetEnableEquipType();
      if (itemData != null && enableEquipType.HasValue && enableEquipType.Value != EQUIPMENT_TYPE.ARMOR && !itemData.tableData.IsEnableEquipType(this.equipItem.tableData.type))
        return false;
    }
    return true;
  }

  private void ToNotEnableSkillTypeConfirm()
  {
    if (!this.is_not_enable_skill_type)
      return;
    this.is_not_enable_skill_type = false;
    GameSection.ChangeEvent("COME_BACK");
    System.Action call = (System.Action) (() => this.DispatchEvent("NOT_SKILL_ENABLE_TYPE", (object) new object[1]
    {
      (object) MonoBehaviourSingleton<StatusManager>.I.GetEquipItemGroupString((this.inventory.datas[this.selectIndex].GetItemData() as SkillItemInfo).tableData.GetEnableEquipType().Value)
    }));
    if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentScreenName().Contains(((Object) this).name))
      call();
    else
      this.StartCoroutine(this.DelayCall(call));
  }

  private IEnumerator DelayCall(System.Action call)
  {
    while (!MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName().Contains(nameof (ItemDetailEquipSkillSelect)) || MonoBehaviourSingleton<UIManager>.I.IsTransitioning() || MonoBehaviourSingleton<GameSceneManager>.I.isChangeing || !MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
      yield return (object) null;
    call();
  }

  private void OnQuery_ItemDetailSkillReplaceConfirm_YES() => this.CheckSendEquipSkill();

  private void OnCloseDialog_ItemDetailSkillReplaceConfirm() => this.ToNotEnableSkillTypeConfirm();

  private void OnQuery_ItemDetailSkillStealConfirm_YES() => this.CheckSendEquipSkill();

  private void OnCloseDialog_ItemDetailSkillStealConfirm() => this.ToNotEnableSkillTypeConfirm();

  private void OnQuery_ItemDetailSkillReplaceDetailConfirm_YES() => this.CheckSendEquipSkill();

  private void OnCloseDialog_ItemDetailSkillReplaceDetailConfirm()
  {
    this.ToNotEnableSkillTypeConfirm();
  }

  private void OnQuery_ItemDetailSkillStealDetailConfirm_YES() => this.CheckSendEquipSkill();

  private void OnCloseDialog_ItemDetailSkillStealDetailConfirm()
  {
    this.ToNotEnableSkillTypeConfirm();
  }

  private void OnQuery_ItemDetailNotSkillEnableTypeConfirm_YES() => this._SendEquipSkill();

  private void _SendEquipSkill()
  {
    SortCompareData data = this.inventory.datas[this.selectIndex];
    int num = this.slotIndex;
    if (this.equipItem.IsExceedSkillSlot(num))
      num = this.equipItem.GetExceedSkillSlotNo(num);
    this.isSelfSectionChange = true;
    GameSection.StayEvent();
    MonoBehaviourSingleton<StatusManager>.I.SendSetSkill(this.equipItem.uniqueID, data.GetUniqID(), num, MonoBehaviourSingleton<StatusManager>.I.GetCurrentEquipSetNo(), (Action<bool>) (is_success =>
    {
      if (!is_success)
        this.isSelfSectionChange = false;
      GameSection.ResumeEvent(is_success);
    }));
  }

  private void SendDetachEquipSkill()
  {
    int num = this.slotIndex;
    if (this.equipItem.IsExceedSkillSlot(num))
      num = this.equipItem.GetExceedSkillSlotNo(num);
    this.isSelfSectionChange = true;
    GameSection.StayEvent();
    MonoBehaviourSingleton<StatusManager>.I.SendDetachSkill(this.equipItem.uniqueID, num, MonoBehaviourSingleton<StatusManager>.I.GetCurrentEquipSetNo(), (Action<bool>) (is_success =>
    {
      if (is_success)
        this.equipSkillItem = (SkillItemInfo) null;
      else
        this.isSelfSectionChange = false;
      GameSection.ResumeEvent(is_success);
    }));
  }

  private void OnQuery_ItemDetailEquipSkillDetachConfirm_YES()
  {
    this.SendDetachEquipSkillAllFromEvery();
  }

  private void OnQuery_ItemDetailEquipSkillDetachResult_OK()
  {
    if (this.equipSkillItem != null)
      return;
    GameSection.ChangeEvent("NOT_EQUIP");
  }

  private void SendDetachEquipSkillAllFromEvery()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<StatusManager>.I.SendDetachAllSkillFromEvery(MonoBehaviourSingleton<StatusManager>.I.GetCurrentEquipSetNo(), (Action<bool>) (is_success =>
    {
      GameSection.ResumeEvent(is_success);
      this.RefreshUI();
    }));
  }

  private void OnCloseDialog_ItemDetailEquipSkillSort() => this.OnCloseSort();

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_SKILL_FAVORITE) != (GameSection.NOTIFY_FLAG) 0)
    {
      if (!this.isSelfSectionChange)
        this.updateInventory = true;
    }
    else if ((flags & GameSection.NOTIFY_FLAG.UPDATE_SKILL_INVENTORY) != (GameSection.NOTIFY_FLAG) 0 && !this.isSelfSectionChange)
      this.updateInventory = true;
    base.OnNotify(flags);
    this.isSelfSectionChange = false;
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return !this.isSelfSectionChange ? (GameSection.NOTIFY_FLAG) 0 : GameSection.NOTIFY_FLAG.UPDATE_SKILL_FAVORITE | GameSection.NOTIFY_FLAG.UPDATE_SKILL_INVENTORY;
  }
}
