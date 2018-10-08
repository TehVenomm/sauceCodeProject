using System.Collections.Generic;
using UnityEngine;

public class GrowSkillItemTable : Singleton<GrowSkillItemTable>, IDataTable
{
	public class GrowSkillItemData
	{
		public const string NT = "growId,level,exceedCnt,needExpRate,needExpAdd,giveExpRate,giveExpAdd,atkRate,atkAdd,defRate,defAdd,hpRate,hpAdd,fireAtkRate,fireAtkAdd,waterAtkRate,waterAtkAdd,thunderAtkRate,thunderAtkAdd,earthAtkRate,earthAtkAdd,lightAtkRate,lightAtkAdd,darkAtkRate,darkAtkAdd,fireDefRate,fireDefAdd,waterDefRate,waterDefAdd,thunderDefRate,thunderDefAdd,earthDefRate,earthDefAdd,lightDefRate,lightDefAdd,darkDefRate,darkDefAdd,skillAtkRate,skillAtkAdd,skillAtkRateRate,skillAtkRateAdd,healRate,healAdd,supportValueRate1,supportValueAdd1,supportTimeRate1,supportTimeAdd1,supportValueRate2,supportValueAdd2,supportTimeRate2,supportTimeAdd2,supportValueRate3,supportValueAdd3,supportTimeRate3,supportTimeAdd3,castTimeRate,castTimeAdd,useGaugeRate,useGaugeAdd,useGauge2Rate,useGauge2Add";

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

		public static bool cb(CSVReader csv_reader, GrowSkillItemData data, ref uint key1, ref uint key2)
		{
			data.id = key1;
			data.lv = (int)key2;
			csv_reader.Pop(ref data.exceedCnt);
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
			int i = 0;
			for (int num = 6; i < num; i++)
			{
				data.elemAtk[i] = new GrowRate();
				csv_reader.Pop(ref data.elemAtk[i].rate);
				csv_reader.Pop(ref data.elemAtk[i].add);
			}
			data.elemDef = new GrowRate[6];
			int j = 0;
			for (int num2 = 6; j < num2; j++)
			{
				data.elemDef[j] = new GrowRate();
				csv_reader.Pop(ref data.elemDef[j].rate);
				csv_reader.Pop(ref data.elemDef[j].add);
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
			for (int k = 0; k < 3; k++)
			{
				data.supprtValue[k] = new GrowRate();
				csv_reader.Pop(ref data.supprtValue[k].rate);
				csv_reader.Pop(ref data.supprtValue[k].add);
				data.supprtTime[k] = new GrowRateFloat();
				csv_reader.Pop(ref data.supprtTime[k].rate);
				csv_reader.Pop(ref data.supprtTime[k].add);
			}
			data.castTime = new GrowRate();
			csv_reader.Pop(ref data.castTime.rate);
			if ((int)data.castTime.rate <= 0)
			{
				data.castTime.rate = 100;
			}
			csv_reader.Pop(ref data.castTime.add);
			data.useGauge = new GrowRate();
			csv_reader.Pop(ref data.useGauge.rate);
			if ((int)data.useGauge.rate <= 0)
			{
				data.useGauge.rate = 100;
			}
			csv_reader.Pop(ref data.useGauge.add);
			data.useGauge2 = new GrowRate();
			csv_reader.Pop(ref data.useGauge2.rate);
			if ((int)data.useGauge2.rate <= 0)
			{
				data.useGauge2.rate = 100;
			}
			csv_reader.Pop(ref data.useGauge2.add);
			return true;
		}

		public int GetGrowParamAtk(int base_atk)
		{
			return GetGrowResultValue(base_atk, atk, false);
		}

		public int GetGrowParamDef(int base_def)
		{
			return GetGrowResultValue(base_def, def, false);
		}

		public int GetGrowParamHp(int base_hp)
		{
			return GetGrowResultValue(base_hp, hp, false);
		}

		public int[] GetGrowParamElemAtk(int[] base_elem_atk)
		{
			int num = base_elem_atk.Length;
			int[] array = new int[num];
			int i = 0;
			for (int num2 = num; i < num2; i++)
			{
				array[i] = 0;
				array[i] = GetGrowResultValue(base_elem_atk[i], elemAtk[i], true);
			}
			return array;
		}

		public int[] GetGrowParamElemDef(int[] base_elem_def)
		{
			int num = base_elem_def.Length;
			int[] array = new int[num];
			int i = 0;
			for (int num2 = num; i < num2; i++)
			{
				array[i] = 0;
				array[i] = GetGrowResultValue(base_elem_def[i], elemDef[i], true);
			}
			return array;
		}

		public int GetGrowParamNeedExp(int base_need_exp)
		{
			return MonoBehaviourSingleton<SmithManager>.I.GetGrowResultValue(base_need_exp, needExp, false);
		}

		public int GetGrowParamGiveExp(int base_give_exp)
		{
			return MonoBehaviourSingleton<SmithManager>.I.GetGrowResultValue(base_give_exp, giveExp, false);
		}

		public int GetGrowParamSkillAtk(int base_atk)
		{
			return GetGrowResultValue(base_atk, skillAtk, false);
		}

		public int GetGrowParamSkillAtkRate(int base_atkrate)
		{
			return GetGrowResultValue(base_atkrate, skillAtkRate, false);
		}

		public int GetGrowParamHealHp(int base_heal)
		{
			return GetGrowResultValue(base_heal, heal, false);
		}

		public int GetGrowParamSupprtValue(int[] base_supprtvalue, int index)
		{
			return GetGrowResultValue(base_supprtvalue[index], supprtValue[index], false);
		}

		public float GetGrowParamSupprtTime(float[] base_supprttime, int index)
		{
			return GetGrowResultValue(base_supprttime[index], supprtTime[index], false);
		}

		public float GetGrowParamCastTimeRate()
		{
			return (float)(100 - GetGrowResultValue(100, castTime, false)) / 100f;
		}

		public float GetGrowParamCastTime2Rate()
		{
			return (float)(100 - GetGrowResultValue(100, castTime2, false)) / 100f;
		}

		public int GetGrowParamUseGauge(int base_useGauge)
		{
			return GetGrowResultValue(base_useGauge, useGauge, false);
		}

		public int GetGrowParamUseGauge2(int base_useGauge2)
		{
			return GetGrowResultValue(base_useGauge2, useGauge2, false);
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
			return GetGrowResultValue(base_value, supprtValue[index], false);
		}
	}

	private DoubleUIntKeyTable<GrowSkillItemData> growSkillItemTable;

	public void CreateTable(string csv_text)
	{
		growSkillItemTable = TableUtility.CreateDoubleUIntKeyTable<GrowSkillItemData>(csv_text, GrowSkillItemData.cb, "growId,level,exceedCnt,needExpRate,needExpAdd,giveExpRate,giveExpAdd,atkRate,atkAdd,defRate,defAdd,hpRate,hpAdd,fireAtkRate,fireAtkAdd,waterAtkRate,waterAtkAdd,thunderAtkRate,thunderAtkAdd,earthAtkRate,earthAtkAdd,lightAtkRate,lightAtkAdd,darkAtkRate,darkAtkAdd,fireDefRate,fireDefAdd,waterDefRate,waterDefAdd,thunderDefRate,thunderDefAdd,earthDefRate,earthDefAdd,lightDefRate,lightDefAdd,darkDefRate,darkDefAdd,skillAtkRate,skillAtkAdd,skillAtkRateRate,skillAtkRateAdd,healRate,healAdd,supportValueRate1,supportValueAdd1,supportTimeRate1,supportTimeAdd1,supportValueRate2,supportValueAdd2,supportTimeRate2,supportTimeAdd2,supportValueRate3,supportValueAdd3,supportTimeRate3,supportTimeAdd3,castTimeRate,castTimeAdd,useGaugeRate,useGaugeAdd,useGauge2Rate,useGauge2Add", null, null, null, null);
	}

	public void CreateTable(string csv_text, TableUtility.Progress progress)
	{
		growSkillItemTable = TableUtility.CreateDoubleUIntKeyTable<GrowSkillItemData>(csv_text, GrowSkillItemData.cb, "growId,level,exceedCnt,needExpRate,needExpAdd,giveExpRate,giveExpAdd,atkRate,atkAdd,defRate,defAdd,hpRate,hpAdd,fireAtkRate,fireAtkAdd,waterAtkRate,waterAtkAdd,thunderAtkRate,thunderAtkAdd,earthAtkRate,earthAtkAdd,lightAtkRate,lightAtkAdd,darkAtkRate,darkAtkAdd,fireDefRate,fireDefAdd,waterDefRate,waterDefAdd,thunderDefRate,thunderDefAdd,earthDefRate,earthDefAdd,lightDefRate,lightDefAdd,darkDefRate,darkDefAdd,skillAtkRate,skillAtkAdd,skillAtkRateRate,skillAtkRateAdd,healRate,healAdd,supportValueRate1,supportValueAdd1,supportTimeRate1,supportTimeAdd1,supportValueRate2,supportValueAdd2,supportTimeRate2,supportTimeAdd2,supportValueRate3,supportValueAdd3,supportTimeRate3,supportTimeAdd3,castTimeRate,castTimeAdd,useGaugeRate,useGaugeAdd,useGauge2Rate,useGauge2Add", null, null, null, progress);
		growSkillItemTable.TrimExcess();
	}

	public void AddTable(string csv_text)
	{
		TableUtility.AddDoubleUIntKeyTable(growSkillItemTable, csv_text, GrowSkillItemData.cb, "growId,level,exceedCnt,needExpRate,needExpAdd,giveExpRate,giveExpAdd,atkRate,atkAdd,defRate,defAdd,hpRate,hpAdd,fireAtkRate,fireAtkAdd,waterAtkRate,waterAtkAdd,thunderAtkRate,thunderAtkAdd,earthAtkRate,earthAtkAdd,lightAtkRate,lightAtkAdd,darkAtkRate,darkAtkAdd,fireDefRate,fireDefAdd,waterDefRate,waterDefAdd,thunderDefRate,thunderDefAdd,earthDefRate,earthDefAdd,lightDefRate,lightDefAdd,darkDefRate,darkDefAdd,skillAtkRate,skillAtkAdd,skillAtkRateRate,skillAtkRateAdd,healRate,healAdd,supportValueRate1,supportValueAdd1,supportTimeRate1,supportTimeAdd1,supportValueRate2,supportValueAdd2,supportTimeRate2,supportTimeAdd2,supportValueRate3,supportValueAdd3,supportTimeRate3,supportTimeAdd3,castTimeRate,castTimeAdd,useGaugeRate,useGaugeAdd,useGauge2Rate,useGauge2Add", null, null, null);
	}

	public GrowSkillItemData GetGrowSkillItemData(uint skill_grow_id, int level)
	{
		if (growSkillItemTable == null)
		{
			return null;
		}
		UIntKeyTable<GrowSkillItemData> uIntKeyTable = growSkillItemTable.Get(skill_grow_id);
		if (uIntKeyTable == null)
		{
			Log.Error("GrowSkillTable is NULL :: grow id = " + skill_grow_id + " Lv = " + level);
			return null;
		}
		GrowSkillItemData growSkillItemData = uIntKeyTable.Get((uint)level);
		if (growSkillItemData != null)
		{
			return growSkillItemData;
		}
		GrowSkillItemData under = null;
		GrowSkillItemData over = null;
		uIntKeyTable.ForEach(delegate(GrowSkillItemData data)
		{
			if (data.lv > level && (over == null || data.lv < over.lv))
			{
				over = data;
			}
			if (data.lv <= level && (under == null || data.lv > under.lv))
			{
				under = data;
			}
		});
		if (under != null && over == null)
		{
			return under;
		}
		if (under == null)
		{
			return null;
		}
		float t = (float)(level - under.lv) / (float)(over.lv - under.lv);
		GrowSkillItemData growSkillItemData2 = new GrowSkillItemData();
		growSkillItemData2.id = skill_grow_id;
		growSkillItemData2.lv = level;
		growSkillItemData2.exceedCnt = Mathf.FloorToInt(Mathf.Lerp((float)under.exceedCnt, (float)over.exceedCnt, t));
		growSkillItemData2.needExp = new GrowRate();
		growSkillItemData2.needExp.rate = Mathf.FloorToInt(Mathf.Lerp((float)(int)under.needExp.rate, (float)(int)over.needExp.rate, t));
		growSkillItemData2.needExp.add = Mathf.FloorToInt(Mathf.Lerp((float)(int)under.needExp.add, (float)(int)over.needExp.add, t));
		growSkillItemData2.giveExp = new GrowRate();
		growSkillItemData2.giveExp.rate = Mathf.FloorToInt(Mathf.Lerp((float)(int)under.giveExp.rate, (float)(int)over.giveExp.rate, t));
		growSkillItemData2.giveExp.add = Mathf.FloorToInt(Mathf.Lerp((float)(int)under.giveExp.add, (float)(int)over.giveExp.add, t));
		growSkillItemData2.atk = new GrowRate();
		growSkillItemData2.atk.rate = Mathf.FloorToInt(Mathf.Lerp((float)(int)under.atk.rate, (float)(int)over.atk.rate, t));
		growSkillItemData2.atk.add = Mathf.FloorToInt(Mathf.Lerp((float)(int)under.atk.add, (float)(int)over.atk.add, t));
		growSkillItemData2.def = new GrowRate();
		growSkillItemData2.def.rate = Mathf.FloorToInt(Mathf.Lerp((float)(int)under.def.rate, (float)(int)over.def.rate, t));
		growSkillItemData2.def.add = Mathf.FloorToInt(Mathf.Lerp((float)(int)under.def.add, (float)(int)over.def.add, t));
		growSkillItemData2.hp = new GrowRate();
		growSkillItemData2.hp.rate = Mathf.FloorToInt(Mathf.Lerp((float)(int)under.hp.rate, (float)(int)over.hp.rate, t));
		growSkillItemData2.hp.add = Mathf.FloorToInt(Mathf.Lerp((float)(int)under.hp.add, (float)(int)over.hp.add, t));
		growSkillItemData2.elemAtk = new GrowRate[6];
		for (int i = 0; i < 6; i++)
		{
			growSkillItemData2.elemAtk[i] = new GrowRate();
			growSkillItemData2.elemAtk[i].rate = Mathf.FloorToInt(Mathf.Lerp((float)(int)under.elemAtk[i].rate, (float)(int)over.elemAtk[i].rate, t));
			growSkillItemData2.elemAtk[i].add = Mathf.FloorToInt(Mathf.Lerp((float)(int)under.elemAtk[i].add, (float)(int)over.elemAtk[i].add, t));
		}
		growSkillItemData2.elemDef = new GrowRate[6];
		for (int j = 0; j < 6; j++)
		{
			growSkillItemData2.elemDef[j] = new GrowRate();
			growSkillItemData2.elemDef[j].rate = Mathf.FloorToInt(Mathf.Lerp((float)(int)under.elemDef[j].rate, (float)(int)over.elemDef[j].rate, t));
			growSkillItemData2.elemDef[j].add = Mathf.FloorToInt(Mathf.Lerp((float)(int)under.elemDef[j].add, (float)(int)over.elemDef[j].add, t));
		}
		growSkillItemData2.skillAtk = new GrowRate();
		growSkillItemData2.skillAtk.rate = Mathf.FloorToInt(Mathf.Lerp((float)(int)under.skillAtk.rate, (float)(int)over.skillAtk.rate, t));
		growSkillItemData2.skillAtk.add = Mathf.FloorToInt(Mathf.Lerp((float)(int)under.skillAtk.add, (float)(int)over.skillAtk.add, t));
		growSkillItemData2.skillAtkRate = new GrowRate();
		growSkillItemData2.skillAtkRate.rate = Mathf.FloorToInt(Mathf.Lerp((float)(int)under.skillAtkRate.rate, (float)(int)over.skillAtkRate.rate, t));
		growSkillItemData2.skillAtkRate.add = Mathf.FloorToInt(Mathf.Lerp((float)(int)under.skillAtkRate.add, (float)(int)over.skillAtkRate.add, t));
		growSkillItemData2.heal = new GrowRate();
		growSkillItemData2.heal.rate = Mathf.FloorToInt(Mathf.Lerp((float)(int)under.heal.rate, (float)(int)over.heal.rate, t));
		growSkillItemData2.heal.add = Mathf.FloorToInt(Mathf.Lerp((float)(int)under.heal.add, (float)(int)over.heal.add, t));
		growSkillItemData2.supprtValue = new GrowRate[3];
		growSkillItemData2.supprtTime = new GrowRateFloat[3];
		for (int k = 0; k < 3; k++)
		{
			growSkillItemData2.supprtValue[k] = new GrowRate();
			growSkillItemData2.supprtValue[k].rate = Mathf.FloorToInt(Mathf.Lerp((float)(int)under.supprtValue[k].rate, (float)(int)over.supprtValue[k].rate, t));
			growSkillItemData2.supprtValue[k].add = Mathf.FloorToInt(Mathf.Lerp((float)(int)under.supprtValue[k].add, (float)(int)over.supprtValue[k].add, t));
			growSkillItemData2.supprtTime[k] = new GrowRateFloat();
			growSkillItemData2.supprtTime[k].rate = Mathf.FloorToInt(Mathf.Lerp((float)(int)under.supprtTime[k].rate, (float)(int)over.supprtTime[k].rate, t));
			growSkillItemData2.supprtTime[k].add = (float)Mathf.FloorToInt(Mathf.Lerp(under.supprtTime[k].add, over.supprtTime[k].add, t));
		}
		growSkillItemData2.castTime = new GrowRate();
		growSkillItemData2.castTime.rate = Mathf.FloorToInt(Mathf.Lerp((float)(int)under.castTime.rate, (float)(int)over.castTime.rate, t));
		growSkillItemData2.castTime.add = Mathf.FloorToInt(Mathf.Lerp((float)(int)under.castTime.add, (float)(int)over.castTime.add, t));
		growSkillItemData2.useGauge = new GrowRate();
		growSkillItemData2.useGauge.rate = Mathf.FloorToInt(Mathf.Lerp((float)(int)under.useGauge.rate, (float)(int)over.useGauge.rate, t));
		growSkillItemData2.useGauge.add = Mathf.FloorToInt(Mathf.Lerp((float)(int)under.useGauge.add, (float)(int)over.useGauge.add, t));
		growSkillItemData2.useGauge2 = new GrowRate();
		growSkillItemData2.useGauge2.rate = Mathf.FloorToInt(Mathf.Lerp((float)(int)under.useGauge2.rate, (float)(int)over.useGauge2.rate, t));
		growSkillItemData2.useGauge2.add = Mathf.FloorToInt(Mathf.Lerp((float)(int)under.useGauge2.add, (float)(int)over.useGauge2.add, t));
		return growSkillItemData2;
	}

	public GrowSkillItemData[] GetGrowSkillItemDataAry(uint skill_grow_id)
	{
		if (growSkillItemTable == null)
		{
			return null;
		}
		List<GrowSkillItemData> list = new List<GrowSkillItemData>();
		growSkillItemTable.Get(skill_grow_id).ForEach(delegate(GrowSkillItemData grow_table)
		{
			list.Add(grow_table);
		});
		if (list.Count == 0)
		{
			return null;
		}
		return list.ToArray();
	}
}
