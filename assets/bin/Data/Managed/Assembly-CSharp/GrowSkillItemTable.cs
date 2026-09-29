// Decompiled with JetBrains decompiler
// Type: GrowSkillItemTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class GrowSkillItemTable : Singleton<GrowSkillItemTable>, IDataTable
{
  private TripleUIntKeyTable<GrowSkillItemTable.GrowSkillItemData> growSkillItemTable;

  public void CreateTable(string csv_text)
  {
    this.growSkillItemTable = TableUtility.CreateTripleUIntKeyTable<GrowSkillItemTable.GrowSkillItemData>(csv_text, new TableUtility.CallBackTripleUIntKeyReadCSV<GrowSkillItemTable.GrowSkillItemData>(GrowSkillItemTable.GrowSkillItemData.cb), "growId,level,exceedCnt,needExpRate,needExpAdd,giveExpRate,giveExpAdd,atkRate,atkAdd,defRate,defAdd,hpRate,hpAdd,fireAtkRate,fireAtkAdd,waterAtkRate,waterAtkAdd,thunderAtkRate,thunderAtkAdd,earthAtkRate,earthAtkAdd,lightAtkRate,lightAtkAdd,darkAtkRate,darkAtkAdd,fireDefRate,fireDefAdd,waterDefRate,waterDefAdd,thunderDefRate,thunderDefAdd,earthDefRate,earthDefAdd,lightDefRate,lightDefAdd,darkDefRate,darkDefAdd,skillAtkRate,skillAtkAdd,skillAtkRateRate,skillAtkRateAdd,healRate,healAdd,supportValueRate1,supportValueAdd1,supportTimeRate1,supportTimeAdd1,supportValueRate2,supportValueAdd2,supportTimeRate2,supportTimeAdd2,supportValueRate3,supportValueAdd3,supportTimeRate3,supportTimeAdd3,castTimeRate,castTimeAdd,useGaugeRate,useGaugeAdd,useGauge2Rate,useGauge2Add");
  }

  public void CreateTable(string csv_text, TableUtility.Progress progress)
  {
    this.growSkillItemTable = TableUtility.CreateTripleUIntKeyTable<GrowSkillItemTable.GrowSkillItemData>(csv_text, new TableUtility.CallBackTripleUIntKeyReadCSV<GrowSkillItemTable.GrowSkillItemData>(GrowSkillItemTable.GrowSkillItemData.cb), "growId,level,exceedCnt,needExpRate,needExpAdd,giveExpRate,giveExpAdd,atkRate,atkAdd,defRate,defAdd,hpRate,hpAdd,fireAtkRate,fireAtkAdd,waterAtkRate,waterAtkAdd,thunderAtkRate,thunderAtkAdd,earthAtkRate,earthAtkAdd,lightAtkRate,lightAtkAdd,darkAtkRate,darkAtkAdd,fireDefRate,fireDefAdd,waterDefRate,waterDefAdd,thunderDefRate,thunderDefAdd,earthDefRate,earthDefAdd,lightDefRate,lightDefAdd,darkDefRate,darkDefAdd,skillAtkRate,skillAtkAdd,skillAtkRateRate,skillAtkRateAdd,healRate,healAdd,supportValueRate1,supportValueAdd1,supportTimeRate1,supportTimeAdd1,supportValueRate2,supportValueAdd2,supportTimeRate2,supportTimeAdd2,supportValueRate3,supportValueAdd3,supportTimeRate3,supportTimeAdd3,castTimeRate,castTimeAdd,useGaugeRate,useGaugeAdd,useGauge2Rate,useGauge2Add");
    this.growSkillItemTable.TrimExcess();
  }

  public void AddTable(string csv_text)
  {
    TableUtility.AddTripleUIntKeyTable<GrowSkillItemTable.GrowSkillItemData>(this.growSkillItemTable, csv_text, new TableUtility.CallBackTripleUIntKeyReadCSV<GrowSkillItemTable.GrowSkillItemData>(GrowSkillItemTable.GrowSkillItemData.cb), "growId,level,exceedCnt,needExpRate,needExpAdd,giveExpRate,giveExpAdd,atkRate,atkAdd,defRate,defAdd,hpRate,hpAdd,fireAtkRate,fireAtkAdd,waterAtkRate,waterAtkAdd,thunderAtkRate,thunderAtkAdd,earthAtkRate,earthAtkAdd,lightAtkRate,lightAtkAdd,darkAtkRate,darkAtkAdd,fireDefRate,fireDefAdd,waterDefRate,waterDefAdd,thunderDefRate,thunderDefAdd,earthDefRate,earthDefAdd,lightDefRate,lightDefAdd,darkDefRate,darkDefAdd,skillAtkRate,skillAtkAdd,skillAtkRateRate,skillAtkRateAdd,healRate,healAdd,supportValueRate1,supportValueAdd1,supportTimeRate1,supportTimeAdd1,supportValueRate2,supportValueAdd2,supportTimeRate2,supportTimeAdd2,supportValueRate3,supportValueAdd3,supportTimeRate3,supportTimeAdd3,castTimeRate,castTimeAdd,useGaugeRate,useGaugeAdd,useGauge2Rate,useGauge2Add");
  }

  public GrowSkillItemTable.GrowSkillItemData GetGrowSkillItemData(
    uint skill_grow_id,
    int level,
    int exceedCnt)
  {
    if (this.growSkillItemTable == null)
      return (GrowSkillItemTable.GrowSkillItemData) null;
    UIntKeyTable<UIntKeyTable<GrowSkillItemTable.GrowSkillItemData>> uintKeyTable1 = this.growSkillItemTable.Get(skill_grow_id);
    if (uintKeyTable1 == null)
    {
      Log.Error($"GrowSkillTable is NULL :: grow id = {(object) skill_grow_id} Lv = {(object) level} Ex = {(object) exceedCnt}");
      return (GrowSkillItemTable.GrowSkillItemData) null;
    }
    UIntKeyTable<GrowSkillItemTable.GrowSkillItemData> uintKeyTable2 = uintKeyTable1.Get((uint) level);
    if (uintKeyTable2 != null)
    {
      GrowSkillItemTable.GrowSkillItemData growSkillItemData = uintKeyTable2.Get((uint) exceedCnt);
      if (growSkillItemData != null)
        return growSkillItemData;
    }
    GrowSkillItemTable.GrowSkillItemData under = (GrowSkillItemTable.GrowSkillItemData) null;
    GrowSkillItemTable.GrowSkillItemData over = (GrowSkillItemTable.GrowSkillItemData) null;
    uintKeyTable1.ForEach((Action<UIntKeyTable<GrowSkillItemTable.GrowSkillItemData>>) (table => table.ForEach((Action<GrowSkillItemTable.GrowSkillItemData>) (data =>
    {
      if ((data.lv > level || data.lv >= level && data.exceedCnt > exceedCnt) && (over == null || data.lv < over.lv))
        over = data;
      if (data.lv > level || data.exceedCnt > exceedCnt || under != null && data.lv <= under.lv && data.exceedCnt <= under.exceedCnt)
        return;
      under = data;
    }))));
    if (under != null && over == null)
      return under;
    if (under == null)
      return (GrowSkillItemTable.GrowSkillItemData) null;
    float num = under.lv != over.lv || under.exceedCnt >= over.exceedCnt ? (float) (level - under.lv) / (float) (over.lv - under.lv) : (float) (exceedCnt - under.exceedCnt) / (float) (over.exceedCnt - under.exceedCnt);
    GrowSkillItemTable.GrowSkillItemData growSkillItemData1 = new GrowSkillItemTable.GrowSkillItemData()
    {
      id = skill_grow_id,
      lv = level,
      exceedCnt = exceedCnt,
      needExp = new GrowRate()
    };
    growSkillItemData1.needExp.rate = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) under.needExp.rate, (float) (int) over.needExp.rate, num));
    growSkillItemData1.needExp.add = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) under.needExp.add, (float) (int) over.needExp.add, num));
    growSkillItemData1.giveExp = new GrowRate();
    growSkillItemData1.giveExp.rate = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) under.giveExp.rate, (float) (int) over.giveExp.rate, num));
    growSkillItemData1.giveExp.add = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) under.giveExp.add, (float) (int) over.giveExp.add, num));
    growSkillItemData1.atk = new GrowRate();
    growSkillItemData1.atk.rate = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) under.atk.rate, (float) (int) over.atk.rate, num));
    growSkillItemData1.atk.add = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) under.atk.add, (float) (int) over.atk.add, num));
    growSkillItemData1.def = new GrowRate();
    growSkillItemData1.def.rate = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) under.def.rate, (float) (int) over.def.rate, num));
    growSkillItemData1.def.add = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) under.def.add, (float) (int) over.def.add, num));
    growSkillItemData1.hp = new GrowRate();
    growSkillItemData1.hp.rate = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) under.hp.rate, (float) (int) over.hp.rate, num));
    growSkillItemData1.hp.add = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) under.hp.add, (float) (int) over.hp.add, num));
    growSkillItemData1.elemAtk = new GrowRate[6];
    for (int index = 0; index < 6; ++index)
    {
      growSkillItemData1.elemAtk[index] = new GrowRate();
      growSkillItemData1.elemAtk[index].rate = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) under.elemAtk[index].rate, (float) (int) over.elemAtk[index].rate, num));
      growSkillItemData1.elemAtk[index].add = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) under.elemAtk[index].add, (float) (int) over.elemAtk[index].add, num));
    }
    growSkillItemData1.elemDef = new GrowRate[6];
    for (int index = 0; index < 6; ++index)
    {
      growSkillItemData1.elemDef[index] = new GrowRate();
      growSkillItemData1.elemDef[index].rate = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) under.elemDef[index].rate, (float) (int) over.elemDef[index].rate, num));
      growSkillItemData1.elemDef[index].add = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) under.elemDef[index].add, (float) (int) over.elemDef[index].add, num));
    }
    growSkillItemData1.skillAtk = new GrowRate();
    growSkillItemData1.skillAtk.rate = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) under.skillAtk.rate, (float) (int) over.skillAtk.rate, num));
    growSkillItemData1.skillAtk.add = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) under.skillAtk.add, (float) (int) over.skillAtk.add, num));
    growSkillItemData1.skillAtkRate = new GrowRate();
    growSkillItemData1.skillAtkRate.rate = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) under.skillAtkRate.rate, (float) (int) over.skillAtkRate.rate, num));
    growSkillItemData1.skillAtkRate.add = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) under.skillAtkRate.add, (float) (int) over.skillAtkRate.add, num));
    growSkillItemData1.heal = new GrowRate();
    growSkillItemData1.heal.rate = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) under.heal.rate, (float) (int) over.heal.rate, num));
    growSkillItemData1.heal.add = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) under.heal.add, (float) (int) over.heal.add, num));
    growSkillItemData1.supprtValue = new GrowRate[3];
    growSkillItemData1.supprtTime = new GrowRateFloat[3];
    for (int index = 0; index < 3; ++index)
    {
      growSkillItemData1.supprtValue[index] = new GrowRate();
      growSkillItemData1.supprtValue[index].rate = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) under.supprtValue[index].rate, (float) (int) over.supprtValue[index].rate, num));
      growSkillItemData1.supprtValue[index].add = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) under.supprtValue[index].add, (float) (int) over.supprtValue[index].add, num));
      growSkillItemData1.supprtTime[index] = new GrowRateFloat();
      growSkillItemData1.supprtTime[index].rate = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) under.supprtTime[index].rate, (float) (int) over.supprtTime[index].rate, num));
      growSkillItemData1.supprtTime[index].add = (XorFloat) (float) Mathf.FloorToInt(Mathf.Lerp((float) under.supprtTime[index].add, (float) over.supprtTime[index].add, num));
    }
    growSkillItemData1.castTime = new GrowRate();
    growSkillItemData1.castTime.rate = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) under.castTime.rate, (float) (int) over.castTime.rate, num));
    growSkillItemData1.castTime.add = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) under.castTime.add, (float) (int) over.castTime.add, num));
    growSkillItemData1.useGauge = new GrowRate();
    growSkillItemData1.useGauge.rate = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) under.useGauge.rate, (float) (int) over.useGauge.rate, num));
    growSkillItemData1.useGauge.add = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) under.useGauge.add, (float) (int) over.useGauge.add, num));
    growSkillItemData1.useGauge2 = new GrowRate();
    growSkillItemData1.useGauge2.rate = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) under.useGauge2.rate, (float) (int) over.useGauge2.rate, num));
    growSkillItemData1.useGauge2.add = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) under.useGauge2.add, (float) (int) over.useGauge2.add, num));
    return growSkillItemData1;
  }

  public GrowSkillItemTable.GrowSkillItemData[] GetGrowSkillItemDataAry(uint skill_grow_id)
  {
    if (this.growSkillItemTable != null)
    {
      List<GrowSkillItemTable.GrowSkillItemData> list = new List<GrowSkillItemTable.GrowSkillItemData>();
      this.growSkillItemTable.Get(skill_grow_id).ForEach((Action<UIntKeyTable<GrowSkillItemTable.GrowSkillItemData>>) (table => table.ForEach((Action<GrowSkillItemTable.GrowSkillItemData>) (o => list.Add(o)))));
      if (!list.IsNullOrEmpty<GrowSkillItemTable.GrowSkillItemData>())
        return list.ToArray();
    }
    return (GrowSkillItemTable.GrowSkillItemData[]) null;
  }

  public class GrowSkillItemData
  {
    public uint id;
    public int lv;
    public int exceedCnt;
    public GrowRate needExp;
    public GrowRate giveExp;
    public GrowRate atk;
    public GrowRate def;
    public GrowRate hp;
    public GrowRate[] elemAtk;
    public GrowRate[] elemDef;
    public GrowRate skillAtk;
    public GrowRate skillAtkRate;
    public GrowRate heal;
    public GrowRate[] supprtValue;
    public GrowRateFloat[] supprtTime;
    public GrowRate castTime;
    public GrowRate castTime2;
    public GrowRate useGauge;
    public GrowRate useGauge2;
    public const string NT = "growId,level,exceedCnt,needExpRate,needExpAdd,giveExpRate,giveExpAdd,atkRate,atkAdd,defRate,defAdd,hpRate,hpAdd,fireAtkRate,fireAtkAdd,waterAtkRate,waterAtkAdd,thunderAtkRate,thunderAtkAdd,earthAtkRate,earthAtkAdd,lightAtkRate,lightAtkAdd,darkAtkRate,darkAtkAdd,fireDefRate,fireDefAdd,waterDefRate,waterDefAdd,thunderDefRate,thunderDefAdd,earthDefRate,earthDefAdd,lightDefRate,lightDefAdd,darkDefRate,darkDefAdd,skillAtkRate,skillAtkAdd,skillAtkRateRate,skillAtkRateAdd,healRate,healAdd,supportValueRate1,supportValueAdd1,supportTimeRate1,supportTimeAdd1,supportValueRate2,supportValueAdd2,supportTimeRate2,supportTimeAdd2,supportValueRate3,supportValueAdd3,supportTimeRate3,supportTimeAdd3,castTimeRate,castTimeAdd,useGaugeRate,useGaugeAdd,useGauge2Rate,useGauge2Add";

    public static bool cb(
      CSVReader csv_reader,
      GrowSkillItemTable.GrowSkillItemData data,
      ref uint key1,
      ref uint key2,
      ref uint key3)
    {
      data.id = key1;
      data.lv = (int) key2;
      data.exceedCnt = (int) key3;
      data.needExp = new GrowRate();
      csv_reader.Pop(ref data.needExp.rate);
      csv_reader.Pop(ref data.needExp.add);
      data.giveExp = new GrowRate();
      csv_reader.Pop(ref data.giveExp.rate);
      csv_reader.Pop(ref data.giveExp.add);
      data.atk = new GrowRate();
      csv_reader.Pop(ref data.atk.rate);
      csv_reader.Pop(ref data.atk.add);
      data.def = new GrowRate();
      csv_reader.Pop(ref data.def.rate);
      csv_reader.Pop(ref data.def.add);
      data.hp = new GrowRate();
      csv_reader.Pop(ref data.hp.rate);
      csv_reader.Pop(ref data.hp.add);
      data.elemAtk = new GrowRate[6];
      int index1 = 0;
      for (int index2 = 6; index1 < index2; ++index1)
      {
        data.elemAtk[index1] = new GrowRate();
        csv_reader.Pop(ref data.elemAtk[index1].rate);
        csv_reader.Pop(ref data.elemAtk[index1].add);
      }
      data.elemDef = new GrowRate[6];
      int index3 = 0;
      for (int index4 = 6; index3 < index4; ++index3)
      {
        data.elemDef[index3] = new GrowRate();
        csv_reader.Pop(ref data.elemDef[index3].rate);
        csv_reader.Pop(ref data.elemDef[index3].add);
      }
      data.skillAtk = new GrowRate();
      csv_reader.Pop(ref data.skillAtk.rate);
      csv_reader.Pop(ref data.skillAtk.add);
      data.skillAtkRate = new GrowRate();
      csv_reader.Pop(ref data.skillAtkRate.rate);
      csv_reader.Pop(ref data.skillAtkRate.add);
      data.heal = new GrowRate();
      csv_reader.Pop(ref data.heal.rate);
      csv_reader.Pop(ref data.heal.add);
      data.supprtValue = new GrowRate[3];
      data.supprtTime = new GrowRateFloat[3];
      for (int index5 = 0; index5 < 3; ++index5)
      {
        data.supprtValue[index5] = new GrowRate();
        csv_reader.Pop(ref data.supprtValue[index5].rate);
        csv_reader.Pop(ref data.supprtValue[index5].add);
        data.supprtTime[index5] = new GrowRateFloat();
        csv_reader.Pop(ref data.supprtTime[index5].rate);
        csv_reader.Pop(ref data.supprtTime[index5].add);
      }
      data.castTime = new GrowRate();
      csv_reader.Pop(ref data.castTime.rate);
      if ((int) data.castTime.rate <= 0)
        data.castTime.rate = (XorInt) 100;
      csv_reader.Pop(ref data.castTime.add);
      data.useGauge = new GrowRate();
      csv_reader.Pop(ref data.useGauge.rate);
      if ((int) data.useGauge.rate <= 0)
        data.useGauge.rate = (XorInt) 100;
      csv_reader.Pop(ref data.useGauge.add);
      data.useGauge2 = new GrowRate();
      csv_reader.Pop(ref data.useGauge2.rate);
      if ((int) data.useGauge2.rate <= 0)
        data.useGauge2.rate = (XorInt) 100;
      csv_reader.Pop(ref data.useGauge2.add);
      return true;
    }

    public int GetGrowParamAtk(int base_atk) => this.GetGrowResultValue(base_atk, this.atk);

    public int GetGrowParamDef(int base_def) => this.GetGrowResultValue(base_def, this.def);

    public int GetGrowParamHp(int base_hp) => this.GetGrowResultValue(base_hp, this.hp);

    public int[] GetGrowParamElemAtk(int[] base_elem_atk)
    {
      int length = base_elem_atk.Length;
      int[] growParamElemAtk = new int[length];
      int index1 = 0;
      for (int index2 = length; index1 < index2; ++index1)
      {
        growParamElemAtk[index1] = 0;
        growParamElemAtk[index1] = this.GetGrowResultValue(base_elem_atk[index1], this.elemAtk[index1], true);
      }
      return growParamElemAtk;
    }

    public int[] GetGrowParamElemDef(int[] base_elem_def)
    {
      int length = base_elem_def.Length;
      int[] growParamElemDef = new int[length];
      int index1 = 0;
      for (int index2 = length; index1 < index2; ++index1)
      {
        growParamElemDef[index1] = 0;
        growParamElemDef[index1] = this.GetGrowResultValue(base_elem_def[index1], this.elemDef[index1], true);
      }
      return growParamElemDef;
    }

    public int GetGrowParamNeedExp(int base_need_exp)
    {
      return MonoBehaviourSingleton<SmithManager>.I.GetGrowResultValue(base_need_exp, this.needExp);
    }

    public int GetGrowParamGiveExp(int base_give_exp)
    {
      return MonoBehaviourSingleton<SmithManager>.I.GetGrowResultValue(base_give_exp, this.giveExp);
    }

    public int GetGrowParamSkillAtk(int base_atk)
    {
      return this.GetGrowResultValue(base_atk, this.skillAtk);
    }

    public int GetGrowParamSkillAtkRate(int base_atkrate)
    {
      return this.GetGrowResultValue(base_atkrate, this.skillAtkRate);
    }

    public int GetGrowParamHealHp(int base_heal) => this.GetGrowResultValue(base_heal, this.heal);

    public int GetGrowParamSupprtValue(int[] base_supprtvalue, int index)
    {
      return this.GetGrowResultValue(base_supprtvalue[index], this.supprtValue[index]);
    }

    public float GetGrowParamSupprtTime(float[] base_supprttime, int index)
    {
      return this.GetGrowResultValue(base_supprttime[index], this.supprtTime[index]);
    }

    public float GetGrowParamCastTimeRate()
    {
      return (float) (100 - this.GetGrowResultValue(100, this.castTime)) / 100f;
    }

    public float GetGrowParamCastTime2Rate()
    {
      return (float) (100 - this.GetGrowResultValue(100, this.castTime2)) / 100f;
    }

    public int GetGrowParamUseGauge(int base_useGauge)
    {
      return this.GetGrowResultValue(base_useGauge, this.useGauge);
    }

    public int GetGrowParamUseGauge2(int base_useGauge2)
    {
      return this.GetGrowResultValue(base_useGauge2, this.useGauge2);
    }

    public int GetGrowResultValue(int base_value, GrowRate rate_data, bool is_element = false)
    {
      return MonoBehaviourSingleton<SmithManager>.I.GetGrowResultValue(base_value, rate_data, is_element);
    }

    public float GetGrowResultValue(float base_value, GrowRateFloat rate_data, bool is_element = false)
    {
      return MonoBehaviourSingleton<SmithManager>.I.GetGrowResultValue(base_value, rate_data, is_element);
    }

    public int GetGrowResultSupportValue(int base_value, int index)
    {
      return this.GetGrowResultValue(base_value, this.supprtValue[index]);
    }
  }
}
