// Decompiled with JetBrains decompiler
// Type: EquipSetInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System.Collections.Generic;

#nullable disable
public class EquipSetInfo
{
  public EquipItemInfo[] item = new EquipItemInfo[7];
  public string name = "装備セット";
  public int showHelm;
  public int order;
  public AccessoryPlaceInfo acc = new AccessoryPlaceInfo();
  public const int INVISIBLE_HELM = 0;
  public const int VISIBLE_HELM = 1;
  public const int COMPLY_USER_STATUS_SHOW_HELM = 2;
  public string tier;
  public uint skillId;

  public EquipSetInfo(EquipSet recv_data)
  {
    this.item[0] = !string.IsNullOrEmpty(recv_data.weapon_0.uniqId) ? new EquipItemInfo(recv_data.weapon_0) : (EquipItemInfo) null;
    this.item[1] = !string.IsNullOrEmpty(recv_data.weapon_1.uniqId) ? new EquipItemInfo(recv_data.weapon_1) : (EquipItemInfo) null;
    this.item[2] = !string.IsNullOrEmpty(recv_data.weapon_2.uniqId) ? new EquipItemInfo(recv_data.weapon_2) : (EquipItemInfo) null;
    this.item[3] = !string.IsNullOrEmpty(recv_data.armor.uniqId) ? new EquipItemInfo(recv_data.armor) : (EquipItemInfo) null;
    this.item[4] = !string.IsNullOrEmpty(recv_data.helm.uniqId) ? new EquipItemInfo(recv_data.helm) : (EquipItemInfo) null;
    this.item[5] = !string.IsNullOrEmpty(recv_data.arm.uniqId) ? new EquipItemInfo(recv_data.arm) : (EquipItemInfo) null;
    this.item[6] = !string.IsNullOrEmpty(recv_data.leg.uniqId) ? new EquipItemInfo(recv_data.leg) : (EquipItemInfo) null;
    this.name = recv_data.setName;
    this.showHelm = recv_data.showHelm;
    this.order = 0;
    this.acc.Copy(recv_data.acc);
  }

  public EquipSetInfo(EquipSetSimple recv_data)
  {
    this.item[0] = !string.IsNullOrEmpty(recv_data.weapon_0) ? MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(ulong.Parse(recv_data.weapon_0)) : (EquipItemInfo) null;
    this.item[1] = !string.IsNullOrEmpty(recv_data.weapon_1) ? MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(ulong.Parse(recv_data.weapon_1)) : (EquipItemInfo) null;
    this.item[2] = !string.IsNullOrEmpty(recv_data.weapon_2) ? MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(ulong.Parse(recv_data.weapon_2)) : (EquipItemInfo) null;
    this.item[3] = !string.IsNullOrEmpty(recv_data.armor) ? MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(ulong.Parse(recv_data.armor)) : (EquipItemInfo) null;
    this.item[4] = !string.IsNullOrEmpty(recv_data.helm) ? MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(ulong.Parse(recv_data.helm)) : (EquipItemInfo) null;
    this.item[5] = !string.IsNullOrEmpty(recv_data.arm) ? MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(ulong.Parse(recv_data.arm)) : (EquipItemInfo) null;
    this.item[6] = !string.IsNullOrEmpty(recv_data.leg) ? MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(ulong.Parse(recv_data.leg)) : (EquipItemInfo) null;
    this.name = recv_data.setName;
    this.showHelm = recv_data.showHelm;
    this.order = recv_data.order;
    this.acc.Copy(recv_data.acc);
  }

  public EquipSetInfo(TutorialGearSetTable.ItemData data)
  {
    this.item[0] = new EquipItemInfo(data.weaponId);
    this.item[3] = new EquipItemInfo(data.armorId);
    this.item[4] = new EquipItemInfo(data.helmId);
    this.item[5] = new EquipItemInfo(data.armId);
    this.item[6] = new EquipItemInfo(data.legId);
    this.name = data.name;
    this.tier = data.difficulty;
    this.skillId = data.skillItemId;
  }

  public EquipSetInfo(
    EquipItemInfo[] equip_item_info_ary,
    string equipName,
    int showHelm,
    AccessoryPlaceInfo _acc)
  {
    if (equip_item_info_ary == null || equip_item_info_ary.Length < 7)
    {
      Log.Warning("EquipSetInfo data is short or null");
    }
    else
    {
      int index1 = 0;
      for (int index2 = 7; index1 < index2; ++index1)
        this.item[index1] = equip_item_info_ary[index1];
      this.name = equipName;
      this.tier = equipName;
      this.showHelm = showHelm;
      this.acc.Copy(_acc);
    }
  }

  public EquipSetInfo(
    EquipItemInfo[] equip_item_info_ary,
    string equipName,
    int showHelm,
    int order,
    AccessoryPlaceInfo _acc)
  {
    if (equip_item_info_ary == null || equip_item_info_ary.Length < 7)
    {
      Log.Warning("EquipSetInfo data is short or null");
    }
    else
    {
      int index1 = 0;
      for (int index2 = 7; index1 < index2; ++index1)
        this.item[index1] = equip_item_info_ary[index1];
      this.name = equipName;
      this.showHelm = showHelm;
      this.order = order;
      this.acc.Copy(_acc);
    }
  }

  public EquipSetInfo(
    EquipItemInfo[] equip_item_info_ary,
    string equipName,
    string difficulty,
    uint skillId)
  {
    if (equip_item_info_ary == null || equip_item_info_ary.Length < 7)
    {
      Log.Warning("EquipSetInfo data is short or null");
    }
    else
    {
      int index1 = 0;
      for (int index2 = 7; index1 < index2; ++index1)
        this.item[index1] = equip_item_info_ary[index1];
      this.name = equipName;
      this.tier = difficulty;
      this.showHelm = 1;
      this.skillId = skillId;
    }
  }

  public EquipSetInfo SwapArmorAndHelm()
  {
    EquipSetInfo equipSetInfo = new EquipSetInfo(this.item, this.name, this.showHelm, this.acc);
    equipSetInfo.item[3] = this.item[4];
    equipSetInfo.item[4] = this.item[3];
    return equipSetInfo;
  }

  public ItemStatus GetTotalEquipTypeBuff(EQUIPMENT_TYPE type)
  {
    ItemStatus totalEquipTypeBuff = new ItemStatus();
    int equipmentTypeIndex = MonoBehaviourSingleton<StatusManager>.I.GetEquipmentTypeIndex(type);
    foreach (EquipItemInfo equipItemInfo in this.item)
    {
      if (equipItemInfo != null)
      {
        ItemStatus itemStatus = equipItemInfo.GetEquipTypeSkillParam()[equipmentTypeIndex + 1];
        totalEquipTypeBuff.Add(itemStatus);
      }
    }
    return totalEquipTypeBuff;
  }

  public ItemStatus GetTotalEquipTypeBuffFromSlot(int slot)
  {
    return this.GetTotalEquipTypeBuff(this.item[slot].tableData.type);
  }

  public CharaInfo.EquipItem ConvertSelfEquipSetItem(int index, int setNo)
  {
    if (index >= this.item.Length || this.item[index] == null)
      return (CharaInfo.EquipItem) null;
    CharaInfo.EquipItem equipItem = new CharaInfo.EquipItem();
    equipItem.eId = (int) this.item[index].tableID;
    equipItem.lv = this.item[index].level;
    equipItem.exceed = this.item[index].exceed;
    int index1 = 0;
    for (int maxSlot = this.item[index].GetMaxSlot(); index1 < maxSlot; ++index1)
    {
      SkillItemInfo skillItemInfo = setNo != -1 ? this.item[index].GetSkillItem(index1, setNo) : this.item[index].GetSkillItem(index1);
      if (skillItemInfo != null)
      {
        equipItem.sIds.Add((int) skillItemInfo.tableID);
        equipItem.sLvs.Add(skillItemInfo.level);
        equipItem.sExs.Add(skillItemInfo.exceedCnt);
      }
    }
    return equipItem;
  }

  public CharaInfo.EquipItem ConvertSelfUniqueEquipSetItem(int index, int setNo)
  {
    if (index >= this.item.Length || this.item[index] == null)
      return (CharaInfo.EquipItem) null;
    CharaInfo.EquipItem equipItem = new CharaInfo.EquipItem();
    equipItem.eId = (int) this.item[index].tableID;
    equipItem.lv = this.item[index].level;
    equipItem.exceed = this.item[index].exceed;
    int index1 = 0;
    for (int maxSlot = this.item[index].GetMaxSlot(); index1 < maxSlot; ++index1)
    {
      SkillItemInfo uniqueSkillItem = this.item[index].GetUniqueSkillItem(index1);
      if (uniqueSkillItem != null)
      {
        equipItem.sIds.Add((int) uniqueSkillItem.tableID);
        equipItem.sLvs.Add(uniqueSkillItem.level);
        equipItem.sExs.Add(uniqueSkillItem.exceedCnt);
      }
    }
    return equipItem;
  }

  public List<CharaInfo.EquipItem> ConvertSelfEquipSetItemList(int setNo = -1, bool isAddNull = false)
  {
    List<CharaInfo.EquipItem> equipItemList = new List<CharaInfo.EquipItem>();
    int index = 0;
    for (int length = this.item.Length; index < length; ++index)
    {
      CharaInfo.EquipItem equipItem = this.ConvertSelfEquipSetItem(index, setNo);
      if (equipItem != null | isAddNull)
        equipItemList.Add(equipItem);
    }
    return equipItemList;
  }

  public List<CharaInfo.EquipItem> ConvertSelfUniqueEquipSetItemList(int setNo = -1, bool isAddNull = false)
  {
    List<CharaInfo.EquipItem> equipItemList = new List<CharaInfo.EquipItem>();
    int index = 0;
    for (int length = this.item.Length; index < length; ++index)
    {
      CharaInfo.EquipItem equipItem = this.ConvertSelfUniqueEquipSetItem(index, setNo);
      if (equipItem != null | isAddNull)
        equipItemList.Add(equipItem);
    }
    return equipItemList;
  }

  public void ChangeName(string setName) => this.name = setName;

  public EQUIPMENT_TYPE[] GetWeaponTypes()
  {
    EQUIPMENT_TYPE[] weaponTypes = new EQUIPMENT_TYPE[3];
    int index1 = 0;
    for (int index2 = 3; index1 < index2; ++index1)
      weaponTypes[index1] = this.item[index1] != null ? this.item[index1].tableData.type : EQUIPMENT_TYPE.NONE;
    return weaponTypes;
  }

  public ELEMENT_TYPE[] GetWeaponElementTypes()
  {
    ELEMENT_TYPE[] weaponElementTypes = new ELEMENT_TYPE[3];
    int index1 = 0;
    for (int index2 = 3; index1 < index2; ++index1)
      weaponElementTypes[index1] = this.item[index1] != null ? (ELEMENT_TYPE) this.item[index1].tableData.GetElemAtkTypePriorityToTable() : ELEMENT_TYPE.MAX;
    return weaponElementTypes;
  }
}
