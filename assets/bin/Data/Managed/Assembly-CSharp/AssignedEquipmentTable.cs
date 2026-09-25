// Decompiled with JetBrains decompiler
// Type: AssignedEquipmentTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;

#nullable disable
public class AssignedEquipmentTable : Singleton<AssignedEquipmentTable>, IDataTable
{
  private UIntKeyTable<AssignedEquipmentTable.AssignedEquipmentData> dataTable;

  public void CreateTable(string csv_text)
  {
    this.dataTable = TableUtility.CreateUIntKeyTable<AssignedEquipmentTable.AssignedEquipmentData>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<AssignedEquipmentTable.AssignedEquipmentData>(AssignedEquipmentTable.AssignedEquipmentData.cb), "id,setName,deliveryId,questids,helpUrl,weapon1Id,w1A1Id,w1A1Pt,w1A2Id,w1A2Pt,w1A3Id,w1A3Pt,w1M1Id,w1M2Id,w1M3Id,armorId,arA1Id,arA1Pt,arA2Id,arA2Pt,arA3Id,arA3Pt,arM1Id,arM2Id,arM3Id,helmId,heA1Id,heA1Pt,heA2Id,heA2Pt,heA3Id,heA3Pt,heM1Id,heM2Id,heM3Id,armId,armA1Id,armA1Pt,armA2Id,armA2Pt,armA3Id,armA3Pt,armM1Id,armM2Id,armM3Id,legId,legA1Id,legA1Pt,legA2Id,legA2Pt,legA3Id,legA3Pt,legM1Id,legM2Id,legM3Id");
    this.dataTable.TrimExcess();
  }

  public AssignedEquipmentTable.AssignedEquipmentData GetAssignedEquipmentData(int id)
  {
    if (this.dataTable == null)
      return (AssignedEquipmentTable.AssignedEquipmentData) null;
    AssignedEquipmentTable.AssignedEquipmentData assignedEquipmentData = this.dataTable.Get((uint) id);
    if (assignedEquipmentData != null)
      return assignedEquipmentData;
    Log.TableError((object) this, (uint) id);
    return assignedEquipmentData;
  }

  public AssignedEquipmentTable.AssignedEquipmentData GetAssignedEquipmentDataFromDeliveryId(
    uint deliveryId)
  {
    if (this.dataTable == null)
      return (AssignedEquipmentTable.AssignedEquipmentData) null;
    AssignedEquipmentTable.AssignedEquipmentData data = (AssignedEquipmentTable.AssignedEquipmentData) null;
    this.dataTable.ForEach((Action<AssignedEquipmentTable.AssignedEquipmentData>) (o =>
    {
      if (data != null || (int) o.deliveryId != (int) deliveryId)
        return;
      data = o;
    }));
    return data;
  }

  public bool HasAssignedEquip(uint questid)
  {
    return this.GetAssignedEquipmentDataFromQuestId(questid) != null;
  }

  public AssignedEquipmentTable.AssignedEquipmentData GetAssignedEquipmentDataFromQuestId(
    uint questid)
  {
    if (this.dataTable == null)
      return (AssignedEquipmentTable.AssignedEquipmentData) null;
    AssignedEquipmentTable.AssignedEquipmentData data = (AssignedEquipmentTable.AssignedEquipmentData) null;
    this.dataTable.ForEach((Action<AssignedEquipmentTable.AssignedEquipmentData>) (o =>
    {
      if (data != null || o.questids == null || o.questids.Length == 0)
        return;
      for (int index = 0; index < o.questids.Length; ++index)
      {
        if (o.questids[index] == (int) questid)
        {
          data = o;
          break;
        }
      }
    }));
    return data;
  }

  public static AssignedEquipmentTable.AssignedSet CreateAssignedSet(
    AssignedEquipmentTable.AssignedEquipmentData assignedEquipment)
  {
    AssignedEquipmentTable.AssignedSet assignedSet = new AssignedEquipmentTable.AssignedSet();
    for (int index1 = 0; index1 < assignedEquipment.equipmentData.Length; ++index1)
    {
      AssignedEquipmentTable.EquipmentData equipmentData = assignedEquipment.equipmentData[index1];
      if (equipmentData.id != 0U)
      {
        EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(equipmentData.id);
        CharaInfo.EquipItem equipItem = new CharaInfo.EquipItem();
        equipItem.eId = (int) equipItemData.id;
        equipItem.lv = equipItemData.maxLv;
        equipItem.exceed = equipItemData.exceedID != 0U ? 4 : 0;
        if (equipmentData.skillIds != null)
        {
          foreach (uint skillId in equipmentData.skillIds)
          {
            if (skillId > 0U)
            {
              SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData(skillId);
              equipItem.sIds.Add((int) skillId);
              if (skillItemData == null)
                equipItem.sLvs.Add(1);
              else
                equipItem.sLvs.Add(skillItemData.GetMaxLv(0));
              equipItem.sExs.Add(0);
            }
          }
        }
        if (equipmentData.abilityIds != null)
        {
          for (int index2 = 0; index2 < equipmentData.abilityIds.Length; ++index2)
          {
            uint abilityId = equipmentData.abilityIds[index2];
            if (abilityId > 0U)
            {
              int num = 1;
              if (equipmentData.abilityPts != null && equipmentData.abilityPts.Length > index2)
                num = equipmentData.abilityPts[index2];
              equipItem.aIds.Add((int) abilityId);
              equipItem.aPts.Add(num);
            }
          }
        }
        switch (equipItemData.type)
        {
          case EQUIPMENT_TYPE.ARMOR:
            assignedSet.armor = equipItem;
            continue;
          case EQUIPMENT_TYPE.HELM:
            assignedSet.helm = equipItem;
            continue;
          case EQUIPMENT_TYPE.ARM:
            assignedSet.arm = equipItem;
            continue;
          case EQUIPMENT_TYPE.LEG:
            assignedSet.leg = equipItem;
            continue;
          default:
            if (equipItemData.IsWeapon())
            {
              assignedSet.weapon_0 = equipItem;
              continue;
            }
            continue;
        }
      }
    }
    return assignedSet;
  }

  public static void MergeAssignedEquip(
    ref CharaInfo org,
    AssignedEquipmentTable.AssignedEquipmentData assignedEquipment)
  {
    AssignedEquipmentTable.AssignedSet assignedSet = AssignedEquipmentTable.CreateAssignedSet(assignedEquipment);
    List<CharaInfo.EquipItem> org_weapons = new List<CharaInfo.EquipItem>();
    org.equipSet.ForEach((Action<CharaInfo.EquipItem>) (data =>
    {
      if (data == null)
        return;
      EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint) data.eId);
      if (equipItemData == null || !equipItemData.IsWeapon())
        return;
      org_weapons.Add(data);
    }));
    List<CharaInfo.EquipItem> equipItemList = new List<CharaInfo.EquipItem>();
    if (assignedSet.weapon_0 != null)
      equipItemList.Add(assignedSet.weapon_0);
    else if (org_weapons.Count > 0)
      equipItemList.Add(org_weapons[0]);
    if (assignedSet.helm != null)
      equipItemList.Add(assignedSet.helm);
    if (assignedSet.armor != null)
      equipItemList.Add(assignedSet.armor);
    if (assignedSet.arm != null)
      equipItemList.Add(assignedSet.arm);
    if (assignedSet.leg != null)
      equipItemList.Add(assignedSet.leg);
    org.equipSet = equipItemList;
  }

  public static void MergeAssignedEquip(
    ref StageObjectManager.CreatePlayerInfo createinfo,
    AssignedEquipmentTable.AssignedEquipmentData assignedEquipment)
  {
    if (createinfo == null || createinfo.charaInfo == null)
      return;
    AssignedEquipmentTable.MergeAssignedEquip(ref createinfo.charaInfo, assignedEquipment);
    if (createinfo.extentionInfo == null || createinfo.extentionInfo.weaponIndexList == null)
      return;
    createinfo.extentionInfo.weaponIndexList.Clear();
    createinfo.extentionInfo.weaponIndexList.Add(0);
    createinfo.extentionInfo.weaponIndexList.Add(-1);
    createinfo.extentionInfo.weaponIndexList.Add(-1);
  }

  public class AssignedEquipmentData
  {
    public int id;
    public string setName;
    public uint deliveryId;
    public int[] questids;
    public string helpUrl;
    public AssignedEquipmentTable.EquipmentData[] equipmentData;
    public const string NT = "id,setName,deliveryId,questids,helpUrl,weapon1Id,w1A1Id,w1A1Pt,w1A2Id,w1A2Pt,w1A3Id,w1A3Pt,w1M1Id,w1M2Id,w1M3Id,armorId,arA1Id,arA1Pt,arA2Id,arA2Pt,arA3Id,arA3Pt,arM1Id,arM2Id,arM3Id,helmId,heA1Id,heA1Pt,heA2Id,heA2Pt,heA3Id,heA3Pt,heM1Id,heM2Id,heM3Id,armId,armA1Id,armA1Pt,armA2Id,armA2Pt,armA3Id,armA3Pt,armM1Id,armM2Id,armM3Id,legId,legA1Id,legA1Pt,legA2Id,legA2Pt,legA3Id,legA3Pt,legM1Id,legM2Id,legM3Id";

    public static bool cb(
      CSVReader csv_reader,
      AssignedEquipmentTable.AssignedEquipmentData data,
      ref uint key)
    {
      data.id = (int) key;
      csv_reader.Pop(ref data.setName);
      csv_reader.Pop(ref data.deliveryId);
      string buff = "";
      csv_reader.Pop(ref buff);
      data.questids = TableUtility.ParseStringToIntArray(buff);
      csv_reader.Pop(ref data.helpUrl);
      data.equipmentData = new AssignedEquipmentTable.EquipmentData[5];
      for (int index1 = 0; index1 < 5; ++index1)
      {
        uint id = 0;
        csv_reader.Pop(ref id);
        uint[] abilityIds = new uint[3];
        int[] abilityPts = new int[3];
        for (int index2 = 0; index2 < 3; ++index2)
        {
          csv_reader.Pop(ref abilityIds[index2]);
          csv_reader.Pop(ref abilityPts[index2]);
        }
        uint[] skillIds = new uint[3];
        for (int index3 = 0; index3 < 3; ++index3)
          csv_reader.Pop(ref skillIds[index3]);
        data.equipmentData[index1] = new AssignedEquipmentTable.EquipmentData(id, abilityIds, abilityPts, skillIds);
      }
      return true;
    }
  }

  public class EquipmentData
  {
    public uint id;
    public uint[] abilityIds;
    public int[] abilityPts;
    public uint[] skillIds;

    public EquipmentData()
    {
    }

    public EquipmentData(uint id, uint[] abilityIds, int[] abilityPts, uint[] skillIds)
    {
      this.id = id;
      this.abilityIds = abilityIds;
      this.abilityPts = abilityPts;
      this.skillIds = skillIds;
    }
  }

  public class AssignedSet
  {
    public CharaInfo.EquipItem weapon_0;
    public CharaInfo.EquipItem weapon_1;
    public CharaInfo.EquipItem weapon_2;
    public CharaInfo.EquipItem armor;
    public CharaInfo.EquipItem arm;
    public CharaInfo.EquipItem leg;
    public CharaInfo.EquipItem helm;
  }
}
