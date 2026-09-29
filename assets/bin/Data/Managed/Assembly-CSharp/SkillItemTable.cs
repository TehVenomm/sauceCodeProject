// Decompiled with JetBrains decompiler
// Type: SkillItemTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class SkillItemTable : Singleton<SkillItemTable>, IDataTable
{
  public const int SUPPORT_MAX = 3;
  public const float SKILL_ATK_DISP_RATE = 0.02f;
  public const int SKILL_SLOT_TABLE_DATA_MAX = 9;
  private UIntKeyTable<SkillItemTable.SkillItemData> skillTable;

  public void CreateTable(string csv_text)
  {
    this.skillTable = TableUtility.CreateUIntKeyTable<SkillItemTable.SkillItemData>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<SkillItemTable.SkillItemData>(SkillItemTable.SkillItemData.cb), "skillItemId,appVer,type,name,text,rarity,R,G,B,iconId,maxLv,growId,needExp,giveExp,atk,def,hp,fireAtk,waterAtk,thunderAtk,earthAtk,lightAtk,darkAtk,fireDef,waterDef,thunderDef,earthDef,lightDef,darkDef,price,enableEquipType,castTime,useGauge,useGauge2,castStateName,actStateName,startEffectName,startSEID,actLocalEffectName,actOneshotEffectName,actSEID,enchantEffectName,bulletName,attackInfoNames0,attackInfoNames1,attackInfoNames2,attackInfoNames3,attackInfoNames4,selfOnly,skillAtk,skillAtkType,skillAtkRate,skillAtkType2,skillAtkRate2,hitEffectName,hitSEID,skillRange,healHp,healType,supportPassiveEqType1,supportType1,supportValue1,supportTime1,supportEffectName1,supportPassiveEqType2,supportType2,supportValue2,supportTime2,supportEffectName2,supportPassiveEqType3,supportType3,supportValue3,supportTime3,supportEffectName3,supportPassiveSpAttackType,buffTableIds,lockBuffTypes,isTeleportation,exceedExtraText");
    this.skillTable.TrimExcess();
  }

  public void AddTable(string csv_text)
  {
    TableUtility.AddUIntKeyTable<SkillItemTable.SkillItemData>(this.skillTable, csv_text, new TableUtility.CallBackUIntKeyReadCSV<SkillItemTable.SkillItemData>(SkillItemTable.SkillItemData.cb), "skillItemId,appVer,type,name,text,rarity,R,G,B,iconId,maxLv,growId,needExp,giveExp,atk,def,hp,fireAtk,waterAtk,thunderAtk,earthAtk,lightAtk,darkAtk,fireDef,waterDef,thunderDef,earthDef,lightDef,darkDef,price,enableEquipType,castTime,useGauge,useGauge2,castStateName,actStateName,startEffectName,startSEID,actLocalEffectName,actOneshotEffectName,actSEID,enchantEffectName,bulletName,attackInfoNames0,attackInfoNames1,attackInfoNames2,attackInfoNames3,attackInfoNames4,selfOnly,skillAtk,skillAtkType,skillAtkRate,skillAtkType2,skillAtkRate2,hitEffectName,hitSEID,skillRange,healHp,healType,supportPassiveEqType1,supportType1,supportValue1,supportTime1,supportEffectName1,supportPassiveEqType2,supportType2,supportValue2,supportTime2,supportEffectName2,supportPassiveEqType3,supportType3,supportValue3,supportTime3,supportEffectName3,supportPassiveSpAttackType,buffTableIds,lockBuffTypes,isTeleportation,exceedExtraText");
  }

  public SkillItemTable.SkillItemData GetSkillItemData(uint skill_id)
  {
    if (this.skillTable == null)
      return (SkillItemTable.SkillItemData) null;
    SkillItemTable.SkillItemData skillItemData = this.skillTable.Get(skill_id);
    if (skillItemData == null)
    {
      Log.TableError((object) this, skill_id);
      skillItemData = new SkillItemTable.SkillItemData();
      skillItemData.name = Log.NON_DATA_NAME;
    }
    return skillItemData;
  }

  public class SkillItemData
  {
    public uint id;
    public string appVer;
    public SKILL_SLOT_TYPE type;
    public string name;
    public string text;
    public RARITY_TYPE rarity;
    public int modelID;
    public Vector3 modelColor;
    public int iconID;
    private int maxLv;
    public uint growID;
    public int baseNeedExp;
    public int baseGiveExp;
    public XorInt baseAtk;
    public XorInt baseDef;
    public XorInt baseHp;
    public int[] atkElement;
    public int[] defElement;
    public int baseSell;
    public ENABLE_EQUIP_TYPE enableEquipType;
    public float castTime;
    public XorInt useGauge;
    public XorInt useGauge2;
    public string castStateName;
    public string actStateName;
    public string startEffectName;
    public int startSEID;
    public string actLocalEffectName;
    public string actOneshotEffectName;
    public int actSEID;
    public string enchantEffectName;
    public string bulletName;
    public string[] attackInfoNames;
    public bool selfOnly;
    public XorInt skillAtk;
    public ELEMENT_TYPE[] skillAtkTypes;
    public XorInt[] skillAtkRates;
    public string hitEffectName;
    public int hitSEID;
    public XorFloat skillRange;
    public XorInt healHp;
    public HEAL_TYPE healType;
    public ENABLE_EQUIP_TYPE[] supportPassiveEqType;
    public BuffParam.BUFFTYPE[] supportType;
    public int[] supportValue;
    public float[] supportTime;
    public string[] supportEffectName;
    public SP_ATTACK_TYPE supportPassiveSpAttackType;
    public int[] buffTableIds;
    public int[] lockBuffTypes;
    public bool isTeleportation;
    public string exceedExtraText;
    private UIntKeyTable<SkillItemTable.SkillItemData.SkillMaxLevel> maxLvData = new UIntKeyTable<SkillItemTable.SkillItemData.SkillMaxLevel>();
    public const string NT = "skillItemId,appVer,type,name,text,rarity,R,G,B,iconId,maxLv,growId,needExp,giveExp,atk,def,hp,fireAtk,waterAtk,thunderAtk,earthAtk,lightAtk,darkAtk,fireDef,waterDef,thunderDef,earthDef,lightDef,darkDef,price,enableEquipType,castTime,useGauge,useGauge2,castStateName,actStateName,startEffectName,startSEID,actLocalEffectName,actOneshotEffectName,actSEID,enchantEffectName,bulletName,attackInfoNames0,attackInfoNames1,attackInfoNames2,attackInfoNames3,attackInfoNames4,selfOnly,skillAtk,skillAtkType,skillAtkRate,skillAtkType2,skillAtkRate2,hitEffectName,hitSEID,skillRange,healHp,healType,supportPassiveEqType1,supportType1,supportValue1,supportTime1,supportEffectName1,supportPassiveEqType2,supportType2,supportValue2,supportTime2,supportEffectName2,supportPassiveEqType3,supportType3,supportValue3,supportTime3,supportEffectName3,supportPassiveSpAttackType,buffTableIds,lockBuffTypes,isTeleportation,exceedExtraText";

    public ELEMENT_TYPE skillAtkType
    {
      get
      {
        return this.skillAtkTypes != null && this.skillAtkTypes.Length != 0 ? this.skillAtkTypes[0] : ELEMENT_TYPE.MAX;
      }
    }

    public XorInt skillAtkRate
    {
      get
      {
        return this.skillAtkRates != null && this.skillAtkRates.Length != 0 ? this.skillAtkRates[0] : (XorInt) 0;
      }
    }

    public int baseElemAtk => this.atkElement == null ? 0 : Mathf.Max(this.atkElement);

    public int baseElemDef => this.defElement == null ? 0 : Mathf.Max(this.defElement);

    public int GetMaxLv(int exceed_cnt)
    {
      SkillItemTable.SkillItemData.SkillMaxLevel skillMaxLevel = this.maxLvData.Get((uint) exceed_cnt);
      if (skillMaxLevel != null)
        return skillMaxLevel.maxLevel;
      int _max_lv = this.GetExceedMaxLevel(exceed_cnt);
      if (_max_lv > this.maxLv)
        _max_lv = this.maxLv;
      this.maxLvData.Add((uint) exceed_cnt, new SkillItemTable.SkillItemData.SkillMaxLevel(exceed_cnt, _max_lv));
      return _max_lv;
    }

    public int GetExceededMaxLevel() => this.maxLv;

    private int GetExceedMaxLevel(int exceed_cnt)
    {
      GrowSkillItemTable.GrowSkillItemData[] skillItemDataAry = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemDataAry(this.growID);
      GrowSkillItemTable.GrowSkillItemData under = (GrowSkillItemTable.GrowSkillItemData) null;
      GrowSkillItemTable.GrowSkillItemData over = (GrowSkillItemTable.GrowSkillItemData) null;
      Action<GrowSkillItemTable.GrowSkillItemData> action = (Action<GrowSkillItemTable.GrowSkillItemData>) (data =>
      {
        if (data.exceedCnt > exceed_cnt && (over == null || data.lv < over.lv))
          over = data;
        if (data.exceedCnt > exceed_cnt || under != null && data.lv <= under.lv)
          return;
        under = data;
      });
      Array.ForEach<GrowSkillItemTable.GrowSkillItemData>(skillItemDataAry, action);
      if (over != null)
        return over.lv;
      return under != null ? under.lv : 1;
    }

    public bool IsPassive()
    {
      return this.type != SKILL_SLOT_TYPE.ATTACK && this.type != SKILL_SLOT_TYPE.SUPPORT && this.type != SKILL_SLOT_TYPE.HEAL;
    }

    public bool IsEnableEquipType(EQUIPMENT_TYPE type)
    {
      return this.IsEnableEquipType(MonoBehaviourSingleton<StatusManager>.I.GetEquipmentTypeIndex(type));
    }

    public bool IsEnableEquipType(int type_index)
    {
      if (type_index < 0)
      {
        Log.Warning(LOG.OUTGAME, "IsEnableEquipType : Check index is out of bounds : index = " + (object) type_index);
        return false;
      }
      if (this.enableEquipType == ENABLE_EQUIP_TYPE.ALL)
        return true;
      int num = type_index + 1;
      if (num > 6)
        num = 6;
      return this.enableEquipType == (ENABLE_EQUIP_TYPE) num;
    }

    public EQUIPMENT_TYPE? GetEnableEquipType()
    {
      switch (this.enableEquipType)
      {
        case ENABLE_EQUIP_TYPE.ONE_HAND_SWORD:
          return new EQUIPMENT_TYPE?(EQUIPMENT_TYPE.ONE_HAND_SWORD);
        case ENABLE_EQUIP_TYPE.TWO_HAND_SWORD:
          return new EQUIPMENT_TYPE?(EQUIPMENT_TYPE.TWO_HAND_SWORD);
        case ENABLE_EQUIP_TYPE.SPEAR:
          return new EQUIPMENT_TYPE?(EQUIPMENT_TYPE.SPEAR);
        case ENABLE_EQUIP_TYPE.PAIR_SWORDS:
          return new EQUIPMENT_TYPE?(EQUIPMENT_TYPE.PAIR_SWORDS);
        case ENABLE_EQUIP_TYPE.ARROW:
          return new EQUIPMENT_TYPE?(EQUIPMENT_TYPE.ARROW);
        case ENABLE_EQUIP_TYPE.ARMORS:
          return new EQUIPMENT_TYPE?(EQUIPMENT_TYPE.ARMOR);
        default:
          return new EQUIPMENT_TYPE?();
      }
    }

    public bool IsEnableSupportEquipType(EQUIPMENT_TYPE type, int support_index)
    {
      return this.IsEnableSupportEquipType(MonoBehaviourSingleton<StatusManager>.I.GetEquipmentTypeIndex(type), support_index);
    }

    public bool IsEnableSupportEquipType(int type_index, int support_index)
    {
      if (type_index < 0)
      {
        Log.Warning(LOG.OUTGAME, "IsEnableEquipType : Check index is out of bounds : index = " + (object) type_index);
        return false;
      }
      if (this.supportPassiveEqType[support_index] == ENABLE_EQUIP_TYPE.ALL)
        return true;
      int num = type_index + 1;
      if (num > 6)
        num = 6;
      return this.supportPassiveEqType[support_index] == (ENABLE_EQUIP_TYPE) num;
    }

    public bool IsEnableSupportEquipType(ENABLE_EQUIP_TYPE type, int support_index)
    {
      return this.supportPassiveEqType[support_index] == ENABLE_EQUIP_TYPE.ALL || this.supportPassiveEqType[support_index] == type;
    }

    public bool IsMatchSupportEquipType(EQUIPMENT_TYPE type)
    {
      if (this.supportPassiveEqType == null || this.supportPassiveEqType.Length == 0)
        return false;
      ENABLE_EQUIP_TYPE fromEquipmentType = this.GetEnableEquipTypeFromEquipmentType(type);
      for (int index = 0; index < this.supportPassiveEqType.Length; ++index)
      {
        if (fromEquipmentType == this.supportPassiveEqType[index])
          return true;
      }
      return false;
    }

    private ENABLE_EQUIP_TYPE GetEnableEquipTypeFromEquipmentType(EQUIPMENT_TYPE type)
    {
      switch (type)
      {
        case EQUIPMENT_TYPE.ONE_HAND_SWORD:
          return ENABLE_EQUIP_TYPE.ONE_HAND_SWORD;
        case EQUIPMENT_TYPE.TWO_HAND_SWORD:
          return ENABLE_EQUIP_TYPE.TWO_HAND_SWORD;
        case EQUIPMENT_TYPE.SPEAR:
          return ENABLE_EQUIP_TYPE.SPEAR;
        case EQUIPMENT_TYPE.PAIR_SWORDS:
          return ENABLE_EQUIP_TYPE.PAIR_SWORDS;
        case EQUIPMENT_TYPE.ARROW:
          return ENABLE_EQUIP_TYPE.ARROW;
        case EQUIPMENT_TYPE.ARMOR:
        case EQUIPMENT_TYPE.HELM:
        case EQUIPMENT_TYPE.ARM:
        case EQUIPMENT_TYPE.LEG:
          return ENABLE_EQUIP_TYPE.ARMORS;
        default:
          return ENABLE_EQUIP_TYPE.ALL;
      }
    }

    public string GetExplanationText(int level = 1, int exceedCnt = 0)
    {
      return SkillItemInfo.GetExplanationText(this, level, exceedCnt);
    }

    public bool IsEnableNowApplicationVersion() => AppMain.CheckApplicationVersion(this.appVer);

    public ELEMENT_TYPE GetAttackElementByIndex(int index)
    {
      return this.skillAtkTypes != null && this.skillAtkTypes.Length > index ? this.skillAtkTypes[index] : ELEMENT_TYPE.MAX;
    }

    public XorInt GetAttackElementRateByIndex(int index)
    {
      return this.skillAtkRates != null && this.skillAtkRates.Length > index ? this.skillAtkRates[index] : (XorInt) 100;
    }

    public int GetAttackElementNum() => this.skillAtkTypes == null ? 0 : this.skillAtkTypes.Length;

    public bool HasElement(ELEMENT_TYPE element)
    {
      if (this.skillAtkTypes != null && this.skillAtkTypes.Length != 0)
      {
        for (int index = 0; index < this.skillAtkTypes.Length; ++index)
        {
          if (this.skillAtkTypes[index] == element)
            return true;
        }
      }
      return false;
    }

    public static bool cb(CSVReader csv_reader, SkillItemTable.SkillItemData data, ref uint key)
    {
      data.id = key;
      csv_reader.Pop(ref data.appVer);
      csv_reader.Pop<SKILL_SLOT_TYPE>(ref data.type);
      csv_reader.Pop(ref data.name);
      csv_reader.Pop(ref data.text);
      csv_reader.Pop<RARITY_TYPE>(ref data.rarity);
      csv_reader.PopColor(ref data.modelColor);
      csv_reader.Pop(ref data.iconID);
      csv_reader.Pop(ref data.maxLv);
      csv_reader.Pop(ref data.growID);
      csv_reader.Pop(ref data.baseNeedExp);
      csv_reader.Pop(ref data.baseGiveExp);
      csv_reader.Pop(ref data.baseAtk);
      csv_reader.Pop(ref data.baseDef);
      csv_reader.Pop(ref data.baseHp);
      data.atkElement = new int[6];
      data.defElement = new int[6];
      for (int index = 0; index < 6; ++index)
        csv_reader.Pop(ref data.atkElement[index]);
      for (int index = 0; index < 6; ++index)
        csv_reader.Pop(ref data.defElement[index]);
      csv_reader.Pop(ref data.baseSell);
      csv_reader.Pop<ENABLE_EQUIP_TYPE>(ref data.enableEquipType);
      csv_reader.Pop(ref data.castTime);
      csv_reader.Pop(ref data.useGauge);
      csv_reader.Pop(ref data.useGauge2);
      csv_reader.Pop(ref data.castStateName);
      csv_reader.Pop(ref data.actStateName);
      csv_reader.Pop(ref data.startEffectName);
      csv_reader.Pop(ref data.startSEID);
      csv_reader.Pop(ref data.actLocalEffectName);
      csv_reader.Pop(ref data.actOneshotEffectName);
      csv_reader.Pop(ref data.actSEID);
      csv_reader.Pop(ref data.enchantEffectName);
      csv_reader.Pop(ref data.bulletName);
      data.attackInfoNames = new string[5];
      for (int index = 0; index < 5; ++index)
        csv_reader.Pop(ref data.attackInfoNames[index]);
      csv_reader.Pop(ref data.selfOnly);
      csv_reader.Pop(ref data.skillAtk);
      ELEMENT_TYPE elementType1 = ELEMENT_TYPE.MAX;
      ELEMENT_TYPE elementType2 = ELEMENT_TYPE.MAX;
      XorInt xorInt1 = (XorInt) 0;
      XorInt xorInt2 = (XorInt) 0;
      csv_reader.PopEnum<ELEMENT_TYPE>(ref elementType1, ELEMENT_TYPE.MAX);
      csv_reader.Pop(ref xorInt1);
      csv_reader.PopEnum<ELEMENT_TYPE>(ref elementType2, ELEMENT_TYPE.MAX);
      csv_reader.Pop(ref xorInt2);
      if (elementType2 == ELEMENT_TYPE.MAX)
      {
        data.skillAtkTypes = new ELEMENT_TYPE[1];
        data.skillAtkRates = new XorInt[1];
        data.skillAtkTypes[0] = elementType1;
        data.skillAtkRates[0] = xorInt1;
      }
      else
      {
        data.skillAtkTypes = new ELEMENT_TYPE[2];
        data.skillAtkRates = new XorInt[2];
        data.skillAtkTypes[0] = elementType1;
        data.skillAtkRates[0] = xorInt1;
        data.skillAtkTypes[1] = elementType2;
        data.skillAtkRates[1] = xorInt2;
      }
      csv_reader.Pop(ref data.hitEffectName);
      csv_reader.Pop(ref data.hitSEID);
      csv_reader.Pop(ref data.skillRange);
      csv_reader.Pop(ref data.healHp);
      csv_reader.Pop<HEAL_TYPE>(ref data.healType);
      data.supportPassiveEqType = new ENABLE_EQUIP_TYPE[3];
      data.supportType = new BuffParam.BUFFTYPE[3];
      data.supportValue = new int[3];
      data.supportTime = new float[3];
      data.supportEffectName = new string[3];
      for (int index = 0; index < 3; ++index)
      {
        csv_reader.Pop<ENABLE_EQUIP_TYPE>(ref data.supportPassiveEqType[index]);
        csv_reader.PopEnum<BuffParam.BUFFTYPE>(ref data.supportType[index], BuffParam.BUFFTYPE.NONE);
        csv_reader.Pop(ref data.supportValue[index]);
        csv_reader.Pop(ref data.supportTime[index]);
        csv_reader.Pop(ref data.supportEffectName[index]);
      }
      csv_reader.PopEnum<SP_ATTACK_TYPE>(ref data.supportPassiveSpAttackType, SP_ATTACK_TYPE.NONE);
      string empty1 = string.Empty;
      csv_reader.Pop(ref empty1);
      if (!string.IsNullOrEmpty(empty1))
      {
        string[] strArray = empty1.Split(':');
        data.buffTableIds = new int[strArray.Length];
        for (int index = 0; index < strArray.Length; ++index)
          data.buffTableIds[index] = strArray[index].ToInt32OrDefault();
      }
      string empty2 = string.Empty;
      csv_reader.Pop(ref empty2);
      if (!string.IsNullOrEmpty(empty2))
      {
        string[] strArray = empty2.Split(':');
        data.lockBuffTypes = new int[strArray.Length];
        for (int index = 0; index < strArray.Length; ++index)
          data.lockBuffTypes[index] = strArray[index].ToInt32OrDefault();
      }
      switch (data.type)
      {
        case SKILL_SLOT_TYPE.ATTACK:
          data.modelID = 1;
          break;
        case SKILL_SLOT_TYPE.SUPPORT:
          data.modelID = 3;
          break;
        case SKILL_SLOT_TYPE.HEAL:
          data.modelID = 2;
          break;
        case SKILL_SLOT_TYPE.PASSIVE:
          data.modelID = 4;
          break;
        case SKILL_SLOT_TYPE.GROW:
          data.modelID = 5;
          break;
      }
      csv_reader.Pop(ref data.isTeleportation);
      csv_reader.Pop(ref data.exceedExtraText);
      return true;
    }

    private class SkillMaxLevel
    {
      public int exceedCnt;
      public int maxLevel;

      public SkillMaxLevel(int _cnt, int _max_lv)
      {
        this.exceedCnt = _cnt;
        this.maxLevel = _max_lv;
      }
    }
  }

  public class SkillSlotData
  {
    public SKILL_SLOT_TYPE slotType;
    public uint skill_id;

    public SkillSlotData()
    {
    }

    public SkillSlotData(uint id, SKILL_SLOT_TYPE type)
    {
      this.skill_id = id;
      this.slotType = type;
    }
  }
}
