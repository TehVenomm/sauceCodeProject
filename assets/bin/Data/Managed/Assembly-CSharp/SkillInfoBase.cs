// Decompiled with JetBrains decompiler
// Type: SkillInfoBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public abstract class SkillInfoBase : GameSection
{
  protected List<ItemIcon> m_generatedIconList = new List<ItemIcon>();
  protected List<SortCompareData> m_newIconUpdateTargetList = new List<SortCompareData>();

  protected override void OnClose()
  {
    this.UpdateNewIconInfo();
    base.OnClose();
  }

  protected virtual int GetCurrentEquipSetNo()
  {
    return MonoBehaviourSingleton<StatusManager>.I.GetCurrentEquipSetNo();
  }

  protected SkillSlotUIData[] GetSkillSlotData(EquipItemInfo equip)
  {
    if (equip == null)
      return (SkillSlotUIData[]) null;
    int maxSlot = equip.GetMaxSlot();
    if (maxSlot == 0)
      return (SkillSlotUIData[]) null;
    SkillSlotUIData[] ui_slot_data = new SkillSlotUIData[maxSlot];
    int currentSetNo = this.GetCurrentEquipSetNo();
    SkillItemInfo[] all = Array.FindAll<SkillItemInfo>(MonoBehaviourSingleton<InventoryManager>.I.GetSkillInventoryClone(), (Predicate<SkillItemInfo>) (skill_item =>
    {
      EquipSetSkillData equipSetSkillData = skill_item.equipSetSkill.Find((Predicate<EquipSetSkillData>) (skill => (long) skill.equipItemUniqId == (long) equip.uniqueID && skill.equipSetNo == currentSetNo));
      return StatusManager.IsUnique() ? (long) skill_item.uniqueEquipSetSkill.equipItemUniqId == (long) equip.uniqueID : equipSetSkillData != null;
    }));
    if (all != null && all.Length > maxSlot)
      Log.Error("Attach Skill Num is Over Skill Slot Num");
    SkillItemTable.SkillSlotData[] slot_data = equip.tableData.GetSkillSlot(equip.exceed);
    Array.ForEach<SkillItemInfo>(all, (Action<SkillItemInfo>) (info =>
    {
      if (info == null)
        return;
      EquipSetSkillData equipSetSkillData = info.equipSetSkill.Find((Predicate<EquipSetSkillData>) (x => x.equipSetNo == currentSetNo));
      if (StatusManager.IsUnique())
      {
        EquipSetSkillData uniqueEquipSetSkill = info.uniqueEquipSetSkill;
        equipSetSkillData = (long) uniqueEquipSetSkill.equipItemUniqId == (long) equip.uniqueID ? uniqueEquipSetSkill : (EquipSetSkillData) null;
      }
      if (equipSetSkillData == null)
        return;
      int index = equipSetSkillData.equipSlotNo;
      if (equip.IsExceedSkillSlot(index))
        index = equip.GetExceedSkillIndex(equipSetSkillData.equipSlotNo);
      ui_slot_data[index] = new SkillSlotUIData();
      ui_slot_data[index].slotData = new SkillItemTable.SkillSlotData(info.tableData.id, slot_data[index].slotType);
      ui_slot_data[index].itemData = info;
    }));
    int index1 = 0;
    for (int length = ui_slot_data.Length; index1 < length; ++index1)
    {
      if (ui_slot_data[index1] == null)
      {
        ui_slot_data[index1] = new SkillSlotUIData();
        ui_slot_data[index1].slotData = new SkillItemTable.SkillSlotData(0U, equip.tableData.GetSkillSlot(equip.exceed)[index1].slotType);
      }
    }
    return ui_slot_data;
  }

  protected SkillSlotUIData[] GetSkillSlotData(
    EquipItemTable.EquipItemData table_data,
    int exceed_cnt)
  {
    if (table_data == null)
      return (SkillSlotUIData[]) null;
    if (table_data.GetSkillSlot(exceed_cnt) == null)
      return (SkillSlotUIData[]) null;
    SkillItemTable.SkillSlotData[] skillSlot = table_data.GetSkillSlot(exceed_cnt);
    SkillSlotUIData[] skillSlotData = new SkillSlotUIData[skillSlot.Length];
    int index = 0;
    for (int length = skillSlot.Length; index < length; ++index)
    {
      skillSlotData[index] = new SkillSlotUIData();
      skillSlotData[index].slotData = skillSlot[index] == null ? new SkillItemTable.SkillSlotData(0U, skillSlot[index].slotType) : skillSlot[index];
    }
    return skillSlotData;
  }

  protected SkillSlotUIData[] GetEvolveInheritanceSkill(
    SkillSlotUIData[] before,
    EquipItemTable.EquipItemData after_equip_table,
    int exceed_cnt)
  {
    SkillSlotUIData[] skillSlotData = this.GetSkillSlotData(after_equip_table, exceed_cnt);
    if (skillSlotData == null)
      return (SkillSlotUIData[]) null;
    int index1 = 0;
    int index2 = 0;
    for (int length = skillSlotData.Length; index2 < length; ++index2)
    {
      if (before != null && index1 < before.Length && skillSlotData[index2].slotData.slotType == before[index1].slotData.slotType)
      {
        skillSlotData[index2] = before[index1];
        ++index1;
      }
      else if (skillSlotData[index2].slotData != null && skillSlotData[index2].slotData.skill_id != 0U)
      {
        Log.Error($"Evolve Equip Expand Skill Slot Data is Not Empty :: index = {(object) index2} : ID = {(object) skillSlotData[index2].slotData.skill_id}");
        skillSlotData[index2].slotData.skill_id = 0U;
      }
    }
    return skillSlotData;
  }

  private EquipItemAndSkillData[] _GetEquipSetAttachSkillListData(EquipSetInfo equip_set)
  {
    int length = equip_set.item.Length;
    EquipItemAndSkillData[] attachSkillListData = new EquipItemAndSkillData[length];
    int index1 = 0;
    for (int index2 = length; index1 < index2; ++index1)
    {
      EquipItemAndSkillData itemAndSkillData = new EquipItemAndSkillData()
      {
        equipItemInfo = equip_set.item[index1]
      };
      itemAndSkillData.skillSlotUIData = this.GetSkillSlotData(itemAndSkillData.equipItemInfo);
      attachSkillListData[index1] = itemAndSkillData;
    }
    return attachSkillListData;
  }

  protected EquipItemAndSkillData[] GetLocalEquipSetAttachSkillListData(int equip_set_no)
  {
    return this._GetEquipSetAttachSkillListData(MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSet()[equip_set_no]);
  }

  protected EquipItemAndSkillData[] GetEquipSetAttachSkillListData(int equip_set_no)
  {
    return this._GetEquipSetAttachSkillListData(MonoBehaviourSingleton<StatusManager>.I.GetEquipSet(equip_set_no));
  }

  protected EquipItemAndSkillData[] GetEquipSetAttachSkillListData(
    List<CharaInfo.EquipItem> equip_item_data)
  {
    EquipItemAndSkillData[] ary = new EquipItemAndSkillData[7];
    int weapon_cnt = 0;
    equip_item_data.ForEach((Action<CharaInfo.EquipItem>) (data =>
    {
      EquipItemAndSkillData itemAndSkillData = new EquipItemAndSkillData();
      itemAndSkillData.equipItemInfo = new EquipItemInfo(data);
      int maxSlot = itemAndSkillData.equipItemInfo.GetMaxSlot();
      SkillSlotUIData[] skillSlotUiDataArray = new SkillSlotUIData[maxSlot];
      List<int> intList = new List<int>();
      int index1 = 0;
      for (int index2 = maxSlot; index1 < index2; ++index1)
      {
        skillSlotUiDataArray[index1] = new SkillSlotUIData();
        skillSlotUiDataArray[index1].slotData = itemAndSkillData.equipItemInfo.tableData.GetSkillSlot(data.exceed)[index1];
        skillSlotUiDataArray[index1].slotData.skill_id = 0U;
        skillSlotUiDataArray[index1].itemData = (SkillItemInfo) null;
        int index3 = 0;
        for (int count = data.sIds.Count; index3 < count; ++index3)
        {
          if (intList.IndexOf(index3) == -1 && Singleton<SkillItemTable>.I.GetSkillItemData((uint) data.sIds[index3]).type == skillSlotUiDataArray[index1].slotData.slotType)
          {
            int exceed = 0;
            if (index3 < data.sExs.Count)
              exceed = data.sExs[index3];
            skillSlotUiDataArray[index1].itemData = new SkillItemInfo(index1, data.sIds[index3], data.sLvs[index3], exceed);
            skillSlotUiDataArray[index1].slotData.skill_id = (uint) data.sIds[index3];
            intList.Add(index3);
            break;
          }
        }
      }
      itemAndSkillData.skillSlotUIData = skillSlotUiDataArray;
      int index4;
      switch (itemAndSkillData.equipItemInfo.tableData.type)
      {
        case EQUIPMENT_TYPE.ARMOR:
          index4 = 3;
          break;
        case EQUIPMENT_TYPE.HELM:
          index4 = 4;
          break;
        case EQUIPMENT_TYPE.ARM:
          index4 = 5;
          break;
        case EQUIPMENT_TYPE.LEG:
          index4 = 6;
          break;
        default:
          index4 = weapon_cnt++;
          break;
      }
      ary[index4] = itemAndSkillData;
    }));
    for (int index = 0; index < 7; ++index)
    {
      if (ary[index] == null)
        ary[index] = new EquipItemAndSkillData();
    }
    return ary;
  }

  protected StatusEquipSetCopyModel.RequestSendForm CopyEquipSetInfo(
    EquipSetInfo equipSet,
    int equipSetNo)
  {
    StatusEquipSetCopyModel.RequestSendForm requestSendForm1 = new StatusEquipSetCopyModel.RequestSendForm();
    requestSendForm1.no = equipSetNo;
    requestSendForm1.name = equipSet.name;
    StatusEquipSetCopyModel.RequestSendForm requestSendForm2 = requestSendForm1;
    ulong uniqueId;
    string str1;
    if (equipSet.item[0] == null)
    {
      str1 = "0";
    }
    else
    {
      uniqueId = equipSet.item[0].uniqueID;
      str1 = uniqueId.ToString();
    }
    requestSendForm2.wuid0 = str1;
    StatusEquipSetCopyModel.RequestSendForm requestSendForm3 = requestSendForm1;
    string str2;
    if (equipSet.item[1] == null)
    {
      str2 = "0";
    }
    else
    {
      uniqueId = equipSet.item[1].uniqueID;
      str2 = uniqueId.ToString();
    }
    requestSendForm3.wuid1 = str2;
    StatusEquipSetCopyModel.RequestSendForm requestSendForm4 = requestSendForm1;
    string str3;
    if (equipSet.item[2] == null)
    {
      str3 = "0";
    }
    else
    {
      uniqueId = equipSet.item[2].uniqueID;
      str3 = uniqueId.ToString();
    }
    requestSendForm4.wuid2 = str3;
    StatusEquipSetCopyModel.RequestSendForm requestSendForm5 = requestSendForm1;
    string str4;
    if (equipSet.item[3] == null)
    {
      str4 = "0";
    }
    else
    {
      uniqueId = equipSet.item[3].uniqueID;
      str4 = uniqueId.ToString();
    }
    requestSendForm5.auid = str4;
    StatusEquipSetCopyModel.RequestSendForm requestSendForm6 = requestSendForm1;
    string str5;
    if (equipSet.item[5] == null)
    {
      str5 = "0";
    }
    else
    {
      uniqueId = equipSet.item[5].uniqueID;
      str5 = uniqueId.ToString();
    }
    requestSendForm6.ruid = str5;
    StatusEquipSetCopyModel.RequestSendForm requestSendForm7 = requestSendForm1;
    string str6;
    if (equipSet.item[6] == null)
    {
      str6 = "0";
    }
    else
    {
      uniqueId = equipSet.item[6].uniqueID;
      str6 = uniqueId.ToString();
    }
    requestSendForm7.luid = str6;
    StatusEquipSetCopyModel.RequestSendForm requestSendForm8 = requestSendForm1;
    string str7;
    if (equipSet.item[4] == null)
    {
      str7 = "0";
    }
    else
    {
      uniqueId = equipSet.item[4].uniqueID;
      str7 = uniqueId.ToString();
    }
    requestSendForm8.huid = str7;
    requestSendForm1.show = equipSet.showHelm;
    int index1 = 0;
    for (int length1 = equipSet.item.Length; index1 < length1; ++index1)
    {
      EquipItemInfo equip = equipSet.item[index1];
      if (equip != null)
      {
        SkillSlotUIData[] skillSlotData = this.GetSkillSlotData(equip);
        if (skillSlotData != null)
        {
          int index2 = 0;
          for (int length2 = skillSlotData.Length; index2 < length2; ++index2)
          {
            SkillItemInfo itemData = skillSlotData[index2].itemData;
            List<string> euids = requestSendForm1.euids;
            uniqueId = equip.uniqueID;
            string str8 = uniqueId.ToString();
            euids.Add(str8);
            List<string> suids = requestSendForm1.suids;
            string str9;
            if (itemData == null)
            {
              str9 = "0";
            }
            else
            {
              uniqueId = itemData.uniqueID;
              str9 = uniqueId.ToString();
            }
            suids.Add(str9);
            int index3 = index2;
            if (equip.IsExceedSkillSlot(index3))
              index3 = equip.GetExceedSkillSlotNo(index3);
            requestSendForm1.slots.Add(index3);
          }
        }
      }
    }
    return requestSendForm1;
  }

  protected void ObserveItemList()
  {
    if (this.m_generatedIconList == null || this.m_generatedIconList.Count < 1)
      return;
    int index = 0;
    for (int count = this.m_generatedIconList.Count; index < count; ++index)
      this.ObserveItemListNewIcon(this.m_generatedIconList[index]);
  }

  protected void ObserveItemListNewIcon(ItemIcon _icon)
  {
    if (Object.op_Equality((Object) _icon, (Object) null) || _icon.InitData == null || !_icon.IsVisbleNewIcon() || this.m_newIconUpdateTargetList.Contains(_icon.InitData))
      return;
    this.m_newIconUpdateTargetList.Add(_icon.InitData);
  }

  protected void UpdateNewIconInfo()
  {
    if (this.m_newIconUpdateTargetList.Count < 1)
      return;
    GameSaveData instance = GameSaveData.instance;
    if (instance == null)
      return;
    int index = 0;
    for (int count = this.m_newIconUpdateTargetList.Count; index < count; ++index)
    {
      SortCompareData iconUpdateTarget = this.m_newIconUpdateTargetList[index];
      instance.RemoveNewIconAndSave(iconUpdateTarget.GetIconType(), iconUpdateTarget.GetUniqID());
    }
    this.m_newIconUpdateTargetList.Clear();
  }
}
