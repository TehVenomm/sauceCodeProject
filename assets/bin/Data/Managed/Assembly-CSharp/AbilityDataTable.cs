// Decompiled with JetBrains decompiler
// Type: AbilityDataTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

#nullable disable
public class AbilityDataTable : Singleton<AbilityDataTable>, IDataTable
{
  public const int INFO_MAX = 3;
  private DoubleUIntKeyTable<AbilityDataTable.AbilityData> abilityDataTable;

  public static DoubleUIntKeyTable<AbilityDataTable.AbilityData> CreateTableCSV(string csv_text)
  {
    return TableUtility.CreateDoubleUIntKeyTable<AbilityDataTable.AbilityData>(csv_text, new TableUtility.CallBackDoubleUIntKeyReadCSV<AbilityDataTable.AbilityData>(AbilityDataTable.AbilityData.cb), "abilityId,needAP,enableEquipType,name,description,abilityType1,abilityTarget1,enableSpAttackType1,abilityValue1,abilityEnableType1,abilityEnableValue11,abilityEnableValue12,abilityType2,abilityTarget2,enableSpAttackType2,abilityValue2,abilityEnableType2,abilityEnableValue21,abilityEnableValue22,abilityType3,abilityTarget3,enableSpAttackType3,abilityValue3,abilityEnableType3,abilityEnableValue31,abilityEnableValue32,descriptionPreGrant,minNeedAP", new TableUtility.CallBackDoubleUIntSecondKey(AbilityDataTable.AbilityData.CBSecondKey));
  }

  public void CreateTable(string csv_text)
  {
    this.abilityDataTable = AbilityDataTable.CreateTableCSV(csv_text);
    this.abilityDataTable.TrimExcess();
  }

  public void AddTable(string csv_text)
  {
    TableUtility.AddDoubleUIntKeyTable<AbilityDataTable.AbilityData>(this.abilityDataTable, csv_text, new TableUtility.CallBackDoubleUIntKeyReadCSV<AbilityDataTable.AbilityData>(AbilityDataTable.AbilityData.cb), "abilityId,needAP,enableEquipType,name,description,abilityType1,abilityTarget1,enableSpAttackType1,abilityValue1,abilityEnableType1,abilityEnableValue11,abilityEnableValue12,abilityType2,abilityTarget2,enableSpAttackType2,abilityValue2,abilityEnableType2,abilityEnableValue21,abilityEnableValue22,abilityType3,abilityTarget3,enableSpAttackType3,abilityValue3,abilityEnableType3,abilityEnableValue31,abilityEnableValue32,descriptionPreGrant,minNeedAP", new TableUtility.CallBackDoubleUIntSecondKey(AbilityDataTable.AbilityData.CBSecondKey));
  }

  public static DoubleUIntKeyTable<AbilityDataTable.AbilityData> CreateTableBinary(byte[] bytes)
  {
    DoubleUIntKeyTable<AbilityDataTable.AbilityData> tableBinary = new DoubleUIntKeyTable<AbilityDataTable.AbilityData>();
    BinaryTableReader reader = new BinaryTableReader(bytes);
    while (reader.MoveNext())
    {
      uint key1 = reader.ReadUInt32();
      uint key2 = 0;
      UIntKeyTable<AbilityDataTable.AbilityData> uintKeyTable = tableBinary.Get(key1);
      if (uintKeyTable != null)
        key2 = (uint) uintKeyTable.GetCount();
      AbilityDataTable.AbilityData abilityData = new AbilityDataTable.AbilityData();
      abilityData.LoadFromBinary(reader, ref key1, ref key2);
      tableBinary.Add(key1, key2, abilityData);
    }
    return tableBinary;
  }

  public void CreateTable(byte[] bytes)
  {
    this.abilityDataTable = AbilityDataTable.CreateTableBinary(bytes);
  }

  public AbilityDataTable.AbilityData GetAbilityData(uint ability_id, int AP)
  {
    if (this.abilityDataTable == null)
      return (AbilityDataTable.AbilityData) null;
    UIntKeyTable<AbilityDataTable.AbilityData> uintKeyTable = this.abilityDataTable.Get(ability_id);
    if (uintKeyTable == null)
    {
      Log.Error($"AbilityDataTable is NULL :: ability id = {(object) ability_id} AP = {(object) AP}");
      return (AbilityDataTable.AbilityData) null;
    }
    AbilityDataTable.AbilityData under = (AbilityDataTable.AbilityData) null;
    AbilityDataTable.AbilityData linearBaseData = (AbilityDataTable.AbilityData) null;
    uintKeyTable.ForEach((Action<AbilityDataTable.AbilityData>) (data =>
    {
      if (AP >= 0)
      {
        if ((int) data.needAP <= AP && (int) data.needAP >= 0 && (under == null || (int) data.needAP > (int) under.needAP))
          under = data;
        if (data.minNeedAP <= 0 || data.minNeedAP > AP || (int) data.needAP < AP)
          return;
        linearBaseData = data;
      }
      else
      {
        if ((int) data.needAP < AP || (int) data.needAP >= 0 || under != null && (int) data.needAP >= (int) under.needAP)
          return;
        under = data;
      }
    }));
    return linearBaseData != null ? this.CreateLinearInterpolationData(linearBaseData, AP) : under;
  }

  public AbilityDataTable.AbilityData[] GetAbilityDataArray(uint ability_id)
  {
    if (this.abilityDataTable == null)
      return (AbilityDataTable.AbilityData[]) null;
    UIntKeyTable<AbilityDataTable.AbilityData> uintKeyTable = this.abilityDataTable.Get(ability_id);
    if (uintKeyTable == null)
    {
      Log.Error("GetAbilityDataList is NULL :: ability id = " + (object) ability_id);
      return (AbilityDataTable.AbilityData[]) null;
    }
    List<AbilityDataTable.AbilityData> list = new List<AbilityDataTable.AbilityData>();
    uintKeyTable.ForEach((Action<AbilityDataTable.AbilityData>) (data => list.Add(data)));
    return list.ToArray();
  }

  public AbilityDataTable.AbilityData GetMinimumAbilityData(uint ability_id)
  {
    if (this.abilityDataTable == null)
      return (AbilityDataTable.AbilityData) null;
    UIntKeyTable<AbilityDataTable.AbilityData> uintKeyTable = this.abilityDataTable.Get(ability_id);
    if (uintKeyTable == null)
    {
      Log.Error("GetAbilityDataList is NULL :: ability id = " + (object) ability_id);
      return (AbilityDataTable.AbilityData) null;
    }
    AbilityDataTable.AbilityData res = (AbilityDataTable.AbilityData) null;
    AbilityDataTable.AbilityData linearBaseData = (AbilityDataTable.AbilityData) null;
    uintKeyTable.ForEach((Action<AbilityDataTable.AbilityData>) (data =>
    {
      if ((int) data.needAP >= 0 && (res == null || (int) res.needAP > (int) data.needAP))
        res = data;
      if (data.minNeedAP <= 0)
        return;
      if (linearBaseData != null)
      {
        if (linearBaseData.minNeedAP <= data.minNeedAP)
          return;
        linearBaseData = data;
      }
      else
        linearBaseData = data;
    }));
    return linearBaseData != null ? this.CreateLinearInterpolationData(linearBaseData, linearBaseData.minNeedAP) : res;
  }

  private AbilityDataTable.AbilityData CreateLinearInterpolationData(
    AbilityDataTable.AbilityData baseData,
    int targetAP)
  {
    AbilityDataTable.AbilityData interpolationData = new AbilityDataTable.AbilityData();
    interpolationData.Clone(baseData);
    interpolationData.Interpolate(targetAP);
    return interpolationData;
  }

  public string GenerateAbilityDescriptionPreGrant(uint ability_id, int minAp, int maxAp)
  {
    return this.ReplaceDescription(this.ReplaceDescription(this.GetMinimumAbilityData(ability_id).descriptionPreGrant, this.GetAbilityData(ability_id, minAp).info, "@s_"), this.GetAbilityData(ability_id, maxAp).info, "@e_");
  }

  private string ReplaceDescription(
    string message,
    AbilityDataTable.AbilityData.AbilityInfo[] infos,
    string wordPreFix)
  {
    if (!message.Contains(wordPreFix))
      return message;
    string input = message;
    MatchCollection matchCollection = new Regex(wordPreFix + "(\\w+)_@").Matches(message);
    int i = 0;
    for (int count = matchCollection.Count; i < count; ++i)
    {
      string str = matchCollection[i].Value;
      string s = Regex.Split(str, "_")[1];
      string replacement = "1";
      int index;
      ref int local = ref index;
      if (int.TryParse(s, out local))
      {
        AbilityDataTable.AbilityData.AbilityInfo info = infos[index];
        if (info != null)
          replacement = info.value.ToString();
      }
      input = Regex.Replace(input, str, replacement);
    }
    return input;
  }

  public class AbilityData
  {
    public XorUInt id = (XorUInt) 0U;
    public XorInt needAP = (XorInt) 0;
    public int minNeedAP;
    public string name;
    public string description;
    public string descriptionPreGrant;
    public ENABLE_EQUIP_TYPE enableEquipType;
    private uint key2;
    private AbilityDataTable.AbilityData.AbilityInfo[] m_info;
    private AbilityDataTable.AbilityData.AbilityInfo[] m_cashedInfo;
    private AbilityTable.Ability m_cashedAbility;
    public const string NT = "abilityId,needAP,enableEquipType,name,description,abilityType1,abilityTarget1,enableSpAttackType1,abilityValue1,abilityEnableType1,abilityEnableValue11,abilityEnableValue12,abilityType2,abilityTarget2,enableSpAttackType2,abilityValue2,abilityEnableType2,abilityEnableValue21,abilityEnableValue22,abilityType3,abilityTarget3,enableSpAttackType3,abilityValue3,abilityEnableType3,abilityEnableValue31,abilityEnableValue32,descriptionPreGrant,minNeedAP";

    public AbilityDataTable.AbilityData.AbilityInfo[] info
    {
      get
      {
        if (this.m_cashedInfo == null && this.m_info != null)
        {
          this.m_cashedInfo = new AbilityDataTable.AbilityData.AbilityInfo[this.m_info.Length];
          for (int index = 0; index < this.m_cashedInfo.Length; ++index)
          {
            this.m_cashedInfo[index] = new AbilityDataTable.AbilityData.AbilityInfo();
            this.m_cashedInfo[index].target = this.m_info[index].target;
            this.m_cashedInfo[index].type = this.m_info[index].type;
            this.m_cashedInfo[index].value = this.m_info[index].value;
            this.m_cashedInfo[index].enables = this.m_info[index].enables;
          }
        }
        if (this.m_cashedAbility == null)
          this.m_cashedAbility = Singleton<AbilityTable>.I.GetAbility(this.id.value);
        if (this.m_cashedInfo == null || this.m_cashedAbility == null)
          return new AbilityDataTable.AbilityData.AbilityInfo[0];
        bool flag = this.m_cashedAbility.IsActive();
        for (int index = 0; index < this.m_cashedInfo.Length; ++index)
          this.m_cashedInfo[index].type = flag ? this.m_info[index].type : ABILITY_TYPE.TIME_LIMIT;
        return this.m_cashedInfo;
      }
    }

    public static bool cb(
      CSVReader csv_reader,
      AbilityDataTable.AbilityData data,
      ref uint key1,
      ref uint key2)
    {
      data.id = (XorUInt) key1;
      data.key2 = key2;
      csv_reader.Pop(ref data.needAP);
      csv_reader.PopEnum<ENABLE_EQUIP_TYPE>(ref data.enableEquipType, ENABLE_EQUIP_TYPE.ALL);
      csv_reader.Pop(ref data.name);
      csv_reader.Pop(ref data.description);
      data.m_info = new AbilityDataTable.AbilityData.AbilityInfo[3];
      for (int index = 0; index < 3; ++index)
      {
        data.m_info[index] = new AbilityDataTable.AbilityData.AbilityInfo();
        CSVReader.PopResult _result1 = csv_reader.PopEnum<ABILITY_TYPE>(ref data.m_info[index].type, ABILITY_TYPE.NONE);
        csv_reader.Pop(ref data.m_info[index].target);
        string _input_text = "";
        CSVReader.PopResult _result2 = csv_reader.Pop(ref _input_text);
        int atkEnableTypeBit = AbilityDataTable.AbilityData.ParseSpAtkEnableTypeBit(_input_text);
        csv_reader.Pop(ref data.m_info[index].value);
        if (!CSVReader.PopResult.IsParseSucceeded(_result1))
        {
          data.m_info[index].type = ABILITY_TYPE.NEED_UPDATE;
          data.m_info[index].target = "";
        }
        if (CSVReader.PopResult.IsParseSucceeded(_result2) && atkEnableTypeBit != 0)
          data.m_info[index].enables.Add(new AbilityDataTable.AbilityData.AbilityInfo.Enable()
          {
            type = ABILITY_ENABLE_TYPE.WEAPON_SP_TYPE,
            SpAtkEnableTypeBit = atkEnableTypeBit
          });
        ABILITY_ENABLE_TYPE abilityEnableType = ABILITY_ENABLE_TYPE.NONE;
        int num1 = 0;
        int num2 = 0;
        CSVReader.PopResult _result3 = csv_reader.PopEnum<ABILITY_ENABLE_TYPE>(ref abilityEnableType, ABILITY_ENABLE_TYPE.NONE);
        csv_reader.Pop(ref num1);
        csv_reader.Pop(ref num2);
        if (CSVReader.PopResult.IsParseSucceeded(_result3) && abilityEnableType != ABILITY_ENABLE_TYPE.NONE)
        {
          AbilityDataTable.AbilityData.AbilityInfo.Enable enable = new AbilityDataTable.AbilityData.AbilityInfo.Enable();
          enable.type = abilityEnableType;
          enable.values[0] = num1;
          enable.values[1] = num2;
          data.m_info[index].enables.Add(enable);
        }
      }
      csv_reader.Pop(ref data.descriptionPreGrant);
      csv_reader.Pop(ref data.minNeedAP);
      if (data.enableEquipType != ENABLE_EQUIP_TYPE.ALL)
      {
        for (int index = 0; index < 3; ++index)
        {
          if (data.m_info[index].type != ABILITY_TYPE.NONE && data.m_info[index].type != ABILITY_TYPE.TIME_LIMIT)
          {
            AbilityDataTable.AbilityData.AbilityInfo.Enable enable = new AbilityDataTable.AbilityData.AbilityInfo.Enable();
            enable.type = Utility.GetAbilityEnableType(data.enableEquipType);
            enable.values[0] = 0;
            enable.values[1] = 0;
            data.m_info[index].enables.Add(enable);
          }
        }
      }
      for (int index1 = 0; index1 < 3; ++index1)
      {
        if (Utility.IsConditionsAbilityType(data.m_info[index1].type))
        {
          ABILITY_TYPE type = data.m_info[index1].type;
          for (int index2 = 0; index2 < 3; ++index2)
          {
            if (data.m_info[index2].type != ABILITY_TYPE.NONE && data.m_info[index2].type != ABILITY_TYPE.TIME_LIMIT)
            {
              AbilityDataTable.AbilityData.AbilityInfo.Enable enable = new AbilityDataTable.AbilityData.AbilityInfo.Enable();
              enable.type = Utility.GetAbilityEnableType(type);
              enable.values[0] = (int) data.m_info[index1].value;
              enable.values[1] = 0;
              data.m_info[index2].enables.Add(enable);
            }
          }
        }
      }
      return true;
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

    public static string CBSecondKey(CSVReader csv, int table_data_num)
    {
      return table_data_num.ToString();
    }

    public uint GetSecondKey() => this.key2;

    public bool HasNeedUpdateAbility()
    {
      for (int index = 0; index < this.info.Length; ++index)
      {
        if (this.info[index].IsNeedUpdate())
          return true;
      }
      return false;
    }

    public void Clone(AbilityDataTable.AbilityData baseData)
    {
      this.id = baseData.id;
      this.needAP = baseData.needAP;
      this.minNeedAP = baseData.minNeedAP;
      this.name = baseData.name;
      this.description = baseData.description;
      this.descriptionPreGrant = baseData.descriptionPreGrant;
      this.enableEquipType = baseData.enableEquipType;
      this.key2 = baseData.key2;
      this.m_info = new AbilityDataTable.AbilityData.AbilityInfo[baseData.m_info.Length];
      for (int index = 0; index < baseData.m_info.Length; ++index)
        this.m_info[index] = baseData.m_info[index].Clone();
      this.m_cashedInfo = (AbilityDataTable.AbilityData.AbilityInfo[]) null;
      this.m_cashedAbility = (AbilityTable.Ability) null;
    }

    public void Interpolate(int targetAP)
    {
      this.name = this.name.Replace("@ap", targetAP.ToString());
      for (int index = 0; index < this.m_info.Length; ++index)
      {
        if ((int) this.m_info[index].value != 0)
        {
          int num = (int) this.m_info[index].value / (int) this.needAP;
          this.m_info[index].value = (XorInt) (num * targetAP);
          this.description = this.description.Replace("@v" + (index + 1).ToString(), this.m_info[index].value.ToString());
        }
      }
    }

    public void LoadFromBinary(BinaryTableReader reader, ref uint key1, ref uint key2)
    {
      this.id = (XorUInt) key1;
      this.key2 = key2;
      this.needAP = (XorInt) reader.ReadInt32();
      this.name = reader.ReadString();
      this.description = reader.ReadString();
      this.m_info = new AbilityDataTable.AbilityData.AbilityInfo[3];
      for (int index = 0; index < 3; ++index)
        this.m_info[index] = new AbilityDataTable.AbilityData.AbilityInfo()
        {
          type = (ABILITY_TYPE) reader.ReadInt32(),
          target = reader.ReadString(),
          value = (XorInt) reader.ReadInt32()
        };
    }

    public void DumpBinary(BinaryWriter writer)
    {
      writer.Write((int) this.needAP);
      writer.Write(this.name);
      writer.Write(this.description);
      for (int index = 0; index < 3; ++index)
      {
        AbilityDataTable.AbilityData.AbilityInfo abilityInfo = this.info[index];
        writer.Write((int) abilityInfo.type);
        writer.Write(abilityInfo.target);
        writer.Write((int) abilityInfo.value);
      }
    }

    public override bool Equals(object obj)
    {
      if (obj == null || !(obj is AbilityDataTable.AbilityData abilityData))
        return false;
      bool flag = (int) this.id.value == (int) abilityData.id.value && this.needAP.value == abilityData.needAP.value && this.name == abilityData.name && this.description == abilityData.description && (int) this.key2 == (int) abilityData.key2 && this.info == null && abilityData.info == null || this.info != null && abilityData.info != null;
      if (this.info != null)
      {
        for (int index = 0; index < this.info.Length; ++index)
          flag = flag && object.Equals((object) this.info[index], (object) abilityData.info[index]);
      }
      return flag;
    }

    public override int GetHashCode() => base.GetHashCode();

    public override string ToString()
    {
      return $"id:{(object) this.id}, needAP:{(object) this.needAP}, name:{this.name}, description:{this.description}, key2:{(object) this.key2}";
    }

    public class AbilityInfo
    {
      public ABILITY_TYPE type;
      public string target;
      public XorInt value = (XorInt) 0;
      public int unlockEventId;
      public List<AbilityDataTable.AbilityData.AbilityInfo.Enable> enables = new List<AbilityDataTable.AbilityData.AbilityInfo.Enable>();
      private int enableCnt = -1;

      public int getEnablesCount()
      {
        if (this.enableCnt == -1)
          this.enableCnt = this.enables.Count;
        return this.enableCnt;
      }

      public bool IsNeedUpdate() => this.type == ABILITY_TYPE.NEED_UPDATE;

      public override bool Equals(object obj)
      {
        return obj != null && obj is AbilityDataTable.AbilityData.AbilityInfo abilityInfo && this.type == abilityInfo.type && this.target == abilityInfo.target && this.value.value == abilityInfo.value.value;
      }

      public override int GetHashCode() => base.GetHashCode();

      public override string ToString()
      {
        return $"type:{(object) this.type}, target:{this.target}, value:{(object) this.value}";
      }

      public AbilityDataTable.AbilityData.AbilityInfo Clone()
      {
        return (AbilityDataTable.AbilityData.AbilityInfo) this.MemberwiseClone();
      }

      public class Enable
      {
        public ABILITY_ENABLE_TYPE type;
        public int SpAtkEnableTypeBit;
        public int[] values = new int[2];
      }
    }
  }
}
