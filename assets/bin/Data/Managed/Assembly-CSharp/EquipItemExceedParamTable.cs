// Decompiled with JetBrains decompiler
// Type: EquipItemExceedParamTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
public class EquipItemExceedParamTable : Singleton<EquipItemExceedParamTable>, IDataTable
{
  private DoubleUIntKeyTable<EquipItemExceedParamTable.EquipItemExceedParam> tableData;

  public void CreateTable(string csv_text)
  {
    this.tableData = TableUtility.CreateDoubleUIntKeyTable<EquipItemExceedParamTable.EquipItemExceedParam>(csv_text, new TableUtility.CallBackDoubleUIntKeyReadCSV<EquipItemExceedParamTable.EquipItemExceedParam>(EquipItemExceedParamTable.EquipItemExceedParam.cb), "exceedId,cnt,atk,def,hp,fireAtk,waterAtk,thunderAtk,earthAtk,lightAtk,darkAtk,fireDef,waterDef,thunderDef,earthDef,lightDef,darkDef,skillType,abilityId,abilityPoint", (TableUtility.CallBackDoubleUIntSecondKey) null);
    this.tableData.TrimExcess();
  }

  public void AddTable(string csv_text)
  {
    TableUtility.AddDoubleUIntKeyTable<EquipItemExceedParamTable.EquipItemExceedParam>(this.tableData, csv_text, new TableUtility.CallBackDoubleUIntKeyReadCSV<EquipItemExceedParamTable.EquipItemExceedParam>(EquipItemExceedParamTable.EquipItemExceedParam.cb), "exceedId,cnt,atk,def,hp,fireAtk,waterAtk,thunderAtk,earthAtk,lightAtk,darkAtk,fireDef,waterDef,thunderDef,earthDef,lightDef,darkDef,skillType,abilityId,abilityPoint", (TableUtility.CallBackDoubleUIntSecondKey) null);
  }

  public EquipItemExceedParamTable.EquipItemExceedParam GetEquipItemExceedParam(
    uint exceedId,
    uint exceedCnt)
  {
    if (this.tableData == null)
      return (EquipItemExceedParamTable.EquipItemExceedParam) null;
    UIntKeyTable<EquipItemExceedParamTable.EquipItemExceedParam> uintKeyTable = this.tableData.Get(exceedId);
    if (uintKeyTable == null)
      return (EquipItemExceedParamTable.EquipItemExceedParam) null;
    EquipItemExceedParamTable.EquipItemExceedParam equipItemExceedParam = uintKeyTable.Get(exceedCnt);
    if (equipItemExceedParam == null)
      Log.Warning("EquipItemExceedParamTable is NULL :: exceedID = {0}, exceedCount = {1}", (object) exceedId, (object) exceedCnt);
    return equipItemExceedParam;
  }

  public EquipItemExceedParamTable.EquipItemExceedParamAll GetEquipItemExceedParamAll(
    uint exceedId,
    uint exceedCnt)
  {
    if (this.tableData == null)
      return (EquipItemExceedParamTable.EquipItemExceedParamAll) null;
    UIntKeyTable<EquipItemExceedParamTable.EquipItemExceedParam> uintKeyTable = this.tableData.Get(exceedId);
    if (uintKeyTable == null)
      return (EquipItemExceedParamTable.EquipItemExceedParamAll) null;
    EquipItemExceedParamTable.EquipItemExceedParamAll result = new EquipItemExceedParamTable.EquipItemExceedParamAll();
    List<SkillItemTable.SkillSlotData> slotList = new List<SkillItemTable.SkillSlotData>();
    List<EquipItem.Ability> abilityList = new List<EquipItem.Ability>();
    uintKeyTable.ForEach((Action<EquipItemExceedParamTable.EquipItemExceedParam>) (param =>
    {
      if ((long) param.cnt > (long) (int) exceedCnt)
        return;
      EquipItemExceedParamTable.EquipItemExceedParamAll itemExceedParamAll1 = result;
      itemExceedParamAll1.atk = (XorInt) ((int) itemExceedParamAll1.atk + (int) param.atk);
      EquipItemExceedParamTable.EquipItemExceedParamAll itemExceedParamAll2 = result;
      itemExceedParamAll2.def = (XorInt) ((int) itemExceedParamAll2.def + (int) param.def);
      EquipItemExceedParamTable.EquipItemExceedParamAll itemExceedParamAll3 = result;
      itemExceedParamAll3.hp = (XorInt) ((int) itemExceedParamAll3.hp + (int) param.hp);
      for (int index = 0; index < 6; ++index)
      {
        result.atkElement[index] += param.atkElement[index];
        result.defElement[index] += param.defElement[index];
      }
      if (param.skillSlot.slotType != SKILL_SLOT_TYPE.NONE)
        slotList.Add(param.skillSlot);
      if (param.ability.id <= 0)
        return;
      abilityList.Add(param.ability);
    }));
    result.skillSlot = slotList.ToArray();
    result.ability = abilityList.ToArray();
    return result;
  }

  public class EquipItemExceedParamBase : EquipItemTable.EquipItemDataUtil
  {
    public XorInt atk = (XorInt) 0;
    public XorInt def = (XorInt) 0;
    public XorInt hp = (XorInt) 0;
    public int[] atkElement;
    public int[] defElement;

    public int GetElemAtk(int[] base_elem)
    {
      int elemAtkType = this.GetElemAtkType(base_elem);
      switch (elemAtkType)
      {
        case -1:
          return this.atkElement[0];
        case 6:
          return 0;
        default:
          return this.atkElement[elemAtkType] + base_elem[elemAtkType];
      }
    }

    public int GetElemDef(int[] base_elem)
    {
      int elemDefType = this.GetElemDefType(base_elem);
      switch (elemDefType)
      {
        case -1:
          return this.defElement[0];
        case 6:
          return 0;
        default:
          return this.defElement[elemDefType] + base_elem[elemDefType];
      }
    }

    public int GetElemAtkType(int[] base_elem)
    {
      if (base_elem == null)
        return 6;
      int[] elem = new int[base_elem.Length];
      int index = 0;
      for (int length = elem.Length; index < length; ++index)
        elem[index] = base_elem[index] + this.atkElement[index];
      return this.GetElemType(elem);
    }

    public int GetElemDefType(int[] base_elem)
    {
      if (base_elem == null)
        return 6;
      int[] elem = new int[base_elem.Length];
      int index = 0;
      for (int length = elem.Length; index < length; ++index)
        elem[index] = base_elem[index] + this.defElement[index];
      return this.GetElemType(elem);
    }
  }

  public class EquipItemExceedParam : EquipItemExceedParamTable.EquipItemExceedParamBase
  {
    public uint exceedId;
    public uint cnt;
    public SkillItemTable.SkillSlotData skillSlot;
    public EquipItem.Ability ability;
    private string paramName;
    public const string NT = "exceedId,cnt,atk,def,hp,fireAtk,waterAtk,thunderAtk,earthAtk,lightAtk,darkAtk,fireDef,waterDef,thunderDef,earthDef,lightDef,darkDef,skillType,abilityId,abilityPoint";

    public EquipItemExceedParam()
    {
      this.atk = (XorInt) 0;
      this.def = (XorInt) 0;
      this.hp = (XorInt) 0;
      this.atkElement = new int[6];
      this.defElement = new int[6];
      for (int index = 0; index < 6; ++index)
      {
        this.atkElement[index] = 0;
        this.defElement[index] = 0;
      }
      this.skillSlot = new SkillItemTable.SkillSlotData();
      this.skillSlot.slotType = SKILL_SLOT_TYPE.NONE;
      this.skillSlot.skill_id = 0U;
      this.ability = new EquipItem.Ability();
      this.ability.id = 0;
      this.ability.pt = 0;
    }

    public string GetExceedParamName()
    {
      if (this.paramName != null)
        return this.paramName;
      StringBuilder stringBuilder = new StringBuilder(string.Empty);
      if ((int) this.atk > 0)
      {
        stringBuilder.Append(StringTable.Format(STRING_CATEGORY.SMITH, 1U, (object) this.atk));
        stringBuilder.Append(" ");
      }
      if ((int) this.def > 0)
      {
        stringBuilder.Append(StringTable.Format(STRING_CATEGORY.SMITH, 2U, (object) this.def));
        stringBuilder.Append(" ");
      }
      if ((int) this.hp > 0)
      {
        stringBuilder.Append(StringTable.Format(STRING_CATEGORY.SMITH, 3U, (object) this.hp));
        stringBuilder.Append(" ");
      }
      int id1 = 0;
      for (int length = this.atkElement.Length; id1 < length; ++id1)
      {
        if (this.atkElement[id1] > 0)
        {
          stringBuilder.Append(StringTable.Format(STRING_CATEGORY.SMITH, 4U, (object) StringTable.Get(STRING_CATEGORY.ELEMENT, (uint) id1), (object) this.atkElement[id1]));
          stringBuilder.Append(" ");
        }
      }
      int id2 = 0;
      for (int length = this.defElement.Length; id2 < length; ++id2)
      {
        if (this.defElement[id2] > 0)
        {
          stringBuilder.Append(StringTable.Format(STRING_CATEGORY.SMITH, 5U, (object) StringTable.Get(STRING_CATEGORY.ELEMENT, (uint) id2), (object) this.defElement[id2]));
          stringBuilder.Append(" ");
        }
      }
      if (this.skillSlot.slotType != SKILL_SLOT_TYPE.NONE)
      {
        stringBuilder.Append(StringTable.Format(STRING_CATEGORY.SMITH, 6U, (object) StringTable.Get(STRING_CATEGORY.SKILL, (uint) this.skillSlot.slotType)));
        stringBuilder.Append(" ");
      }
      if (this.ability.id != 0)
      {
        AbilityTable.Ability ability = Singleton<AbilityTable>.I.GetAbility((uint) this.ability.id);
        stringBuilder.Append(StringTable.Format(STRING_CATEGORY.SMITH, 7U, (object) ability.name, (object) this.ability.pt));
      }
      this.paramName = stringBuilder.ToString();
      return this.paramName;
    }

    public static bool cb(
      CSVReader csv_reader,
      EquipItemExceedParamTable.EquipItemExceedParam data,
      ref uint key1,
      ref uint key2)
    {
      data.exceedId = key1;
      data.cnt = key2;
      csv_reader.Pop(ref data.atk);
      csv_reader.Pop(ref data.def);
      csv_reader.Pop(ref data.hp);
      data.atkElement = new int[6];
      data.defElement = new int[6];
      for (int index = 0; index < 6; ++index)
        csv_reader.Pop(ref data.atkElement[index]);
      for (int index = 0; index < 6; ++index)
        csv_reader.Pop(ref data.defElement[index]);
      data.skillSlot = new SkillItemTable.SkillSlotData();
      csv_reader.Pop<SKILL_SLOT_TYPE>(ref data.skillSlot.slotType);
      csv_reader.Pop(ref data.ability.id);
      csv_reader.Pop(ref data.ability.pt);
      return true;
    }
  }

  public class EquipItemExceedParamAll : EquipItemExceedParamTable.EquipItemExceedParamBase
  {
    public SkillItemTable.SkillSlotData[] skillSlot;
    public EquipItem.Ability[] ability;

    public EquipItemExceedParamAll()
    {
      this.atk = (XorInt) 0;
      this.def = (XorInt) 0;
      this.hp = (XorInt) 0;
      this.atkElement = new int[6];
      this.defElement = new int[6];
      for (int index = 0; index < 6; ++index)
      {
        this.atkElement[index] = 0;
        this.defElement[index] = 0;
      }
      this.skillSlot = new SkillItemTable.SkillSlotData[0];
      this.ability = new EquipItem.Ability[0];
    }
  }
}
