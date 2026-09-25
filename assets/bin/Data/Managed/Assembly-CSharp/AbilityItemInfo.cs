// Decompiled with JetBrains decompiler
// Type: AbilityItemInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;

#nullable disable
public class AbilityItemInfo : ItemInfoBase<AbilityItem>
{
  public ulong equipUniqueId;
  public List<AbilityItemInfo.AbilityInfoWithFormat> info = new List<AbilityItemInfo.AbilityInfoWithFormat>();
  public AbilityItem originalData;

  public override void SetValue(AbilityItem recv)
  {
    this.uniqueID = ulong.Parse(recv.uniqId);
    this.tableID = (uint) recv.abilityItemId;
    this.equipUniqueId = ulong.Parse(recv.equipItemUniqId);
    this.originalData = recv;
    this.info = AbilityItemInfo.ConvertAbilityItemToInfo(recv);
  }

  public EquipItemInfo GetEquipItem()
  {
    if (this.equipUniqueId == 0UL)
      return (EquipItemInfo) null;
    return !MonoBehaviourSingleton<InventoryManager>.IsValid() || MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory == null ? (EquipItemInfo) null : MonoBehaviourSingleton<InventoryManager>.I.GetEquipItem(this.equipUniqueId);
  }

  public ItemTable.ItemData GetItemTableData() => Singleton<ItemTable>.I.GetItemData(this.tableID);

  public string GetName()
  {
    ItemTable.ItemData itemTableData = this.GetItemTableData();
    return itemTableData == null ? "" : itemTableData.name;
  }

  public string GetDescription()
  {
    string description = "";
    for (int index = 0; index < this.info.Count; ++index)
    {
      description += this.info[index].format;
      if (index + 1 < this.info.Count)
        description += "\n";
    }
    return description;
  }

  public static List<AbilityItemInfo.AbilityInfoWithFormat> ConvertAbilityItemToInfo(
    AbilityItem recv)
  {
    List<AbilityItemInfo.AbilityInfoWithFormat> info = new List<AbilityItemInfo.AbilityInfoWithFormat>();
    if (recv == null)
      return info;
    foreach (AbilityItem.Data data in recv.data)
    {
      AbilityItemLotTable.AbilityItemLot abilityItemLot = (AbilityItemLotTable.AbilityItemLot) null;
      ABILITY_TYPE abilityType = ABILITY_TYPE.NONE;
      string _enableText1 = "";
      string _enableText2 = "";
      string _spAttackTypeText = "";
      int num1 = 0;
      string str = "";
      int num2 = 0;
      if (data.abilityItemLotId > 0)
        abilityItemLot = Singleton<AbilityItemLotTable>.I.GetAbilityItemLot((uint) data.abilityItemLotId);
      if (abilityItemLot == null)
      {
        if (Enum.IsDefined(typeof (ABILITY_TYPE), (object) data.abilityType))
        {
          abilityType = (ABILITY_TYPE) Enum.Parse(typeof (ABILITY_TYPE), data.abilityType);
          _enableText1 = data.target;
          _enableText2 = data.spTarget;
          _spAttackTypeText = data.spAttackType;
          num1 = data.value;
          str = data.format;
          num2 = 0;
        }
      }
      else
      {
        abilityType = abilityItemLot.abilityType;
        _enableText1 = abilityItemLot.target;
        _enableText2 = abilityItemLot.spTarget;
        _spAttackTypeText = abilityItemLot.spAttackType;
        num1 = data.value;
        str = abilityItemLot.format.Replace("XX", data.value.ToString());
        num2 = abilityItemLot.unlockEventId;
      }
      AbilityItemInfo.AbilityInfoWithFormat abilityInfoWithFormat = new AbilityItemInfo.AbilityInfoWithFormat();
      if (abilityType != ABILITY_TYPE.NONE)
      {
        abilityInfoWithFormat.type = abilityType;
        abilityInfoWithFormat.target = _enableText1;
        abilityInfoWithFormat.value = (XorInt) num1;
        abilityInfoWithFormat.format = str;
        abilityInfoWithFormat.unlockEventId = num2;
        AbilityDataTable.AbilityData.AbilityInfo.Enable enable1 = AbilityItemInfo.MakeAbilityEnableData(_enableText1);
        if (enable1 != null)
          abilityInfoWithFormat.enables.Add(enable1);
        AbilityDataTable.AbilityData.AbilityInfo.Enable enable2 = AbilityItemInfo.MakeAbilityEnableData(_enableText2);
        if (enable2 != null)
          abilityInfoWithFormat.enables.Add(enable2);
        AbilityDataTable.AbilityData.AbilityInfo.Enable enable3 = AbilityItemInfo.MakeAbilityEnableDataAsSpAtkTypeBit(_spAttackTypeText);
        if (enable3 != null)
          abilityInfoWithFormat.enables.Add(enable3);
      }
      else
      {
        abilityInfoWithFormat.type = ABILITY_TYPE.NEED_UPDATE;
        abilityInfoWithFormat.target = "";
        abilityInfoWithFormat.value = (XorInt) 0;
      }
      info.Add(abilityInfoWithFormat);
    }
    return info;
  }

  private static AbilityDataTable.AbilityData.AbilityInfo.Enable MakeAbilityEnableData(
    string _enableText)
  {
    if (string.IsNullOrEmpty(_enableText))
      return (AbilityDataTable.AbilityData.AbilityInfo.Enable) null;
    if (!Enum.IsDefined(typeof (ABILITY_ENABLE_TYPE), (object) _enableText))
      return (AbilityDataTable.AbilityData.AbilityInfo.Enable) null;
    AbilityDataTable.AbilityData.AbilityInfo.Enable enable = new AbilityDataTable.AbilityData.AbilityInfo.Enable()
    {
      type = (ABILITY_ENABLE_TYPE) Enum.Parse(typeof (ABILITY_ENABLE_TYPE), _enableText)
    };
    enable.SpAtkEnableTypeBit = AbilityItemInfo.ConvertAbilityEnableType2SpAtkEnableTypeBit(enable.type);
    return enable;
  }

  private static AbilityDataTable.AbilityData.AbilityInfo.Enable MakeAbilityEnableDataAsSpAtkTypeBit(
    string _spAttackTypeText)
  {
    if (string.IsNullOrEmpty(_spAttackTypeText))
      return (AbilityDataTable.AbilityData.AbilityInfo.Enable) null;
    int atkEnableTypeBit = AbilityItemInfo.ParseSpAtkEnableTypeBit(_spAttackTypeText);
    if (atkEnableTypeBit == 0)
      return (AbilityDataTable.AbilityData.AbilityInfo.Enable) null;
    return new AbilityDataTable.AbilityData.AbilityInfo.Enable()
    {
      type = ABILITY_ENABLE_TYPE.WEAPON_SP_TYPE,
      SpAtkEnableTypeBit = atkEnableTypeBit
    };
  }

  public static int ConvertAbilityEnableType2SpAtkEnableTypeBit(ABILITY_ENABLE_TYPE _type)
  {
    if (_type < ABILITY_ENABLE_TYPE.NORMAL || ABILITY_ENABLE_TYPE.HEAT_SOUL < _type)
      return 0;
    int num = 0;
    if (_type == ABILITY_ENABLE_TYPE.NORMAL || _type == ABILITY_ENABLE_TYPE.NORMAL_HEAT || _type == ABILITY_ENABLE_TYPE.NORMAL_SOUL)
      num |= 1;
    if (_type == ABILITY_ENABLE_TYPE.HEAT || _type == ABILITY_ENABLE_TYPE.NORMAL_HEAT || _type == ABILITY_ENABLE_TYPE.HEAT_SOUL)
      num |= 2;
    if (_type == ABILITY_ENABLE_TYPE.SOUL || _type == ABILITY_ENABLE_TYPE.NORMAL_SOUL || _type == ABILITY_ENABLE_TYPE.HEAT_SOUL)
      num |= 4;
    return num;
  }

  public static int ParseSpAtkEnableTypeBit(string _input_text)
  {
    int atkEnableTypeBit = 0;
    if (string.IsNullOrEmpty(_input_text))
      return atkEnableTypeBit;
    foreach (object obj in Enum.GetValues(typeof (SP_ATK_ENABLE_TYPE_BIT)))
    {
      if (_input_text.Contains(obj.ToString()))
        atkEnableTypeBit |= (int) obj;
    }
    return atkEnableTypeBit;
  }

  public static InventoryList<AbilityItemInfo, AbilityItem> CreateList(List<AbilityItem> recv_list)
  {
    InventoryList<AbilityItemInfo, AbilityItem> list = new InventoryList<AbilityItemInfo, AbilityItem>();
    recv_list.ForEach((Action<AbilityItem>) (o => list.Add(o)));
    return list;
  }

  public class AbilityInfoWithFormat : AbilityDataTable.AbilityData.AbilityInfo
  {
    public string format;
  }
}
