using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GrowEquipItemTable : Singleton<GrowEquipItemTable>
{
	public class GrowEquipItemData : IDoubleUIntKeyBinaryTableData
	{
		public const string NT = "growId,level,atkRate,atkAdd,defRate,defAdd,hpRate,hpAdd,fireAtkRate,fireAtkAdd,waterAtkRate,waterAtkAdd,thunderAtkRate,thunderAtkAdd,earthAtkRate,earthAtkAdd,lightAtkRate,lightAtkAdd,darkAtkRate,darkAtkAdd,fireDefRate,fireDefAdd,waterDefRate,waterDefAdd,thunderDefRate,thunderDefAdd,earthDefRate,earthDefAdd,lightDefRate,lightDefAdd,darkDefRate,darkDefAdd";

		public XorUInt id = 0u;

		public XorUInt lv = 0u;

		public GrowRate atk;

		public GrowRate def;

		public GrowRate hp;

		public GrowRate[] elemAtk;

		public GrowRate[] elemDef;

		public static bool cb(CSVReader csv_reader, GrowEquipItemData data, ref uint key1, ref uint key2)
		{
			data.id = key1;
			data.lv = key2;
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
			return true;
		}

		public int GetGrowParamAtk(int base_atk)
		{
			return MonoBehaviourSingleton<SmithManager>.I.GetGrowResultValue(base_atk, atk, false);
		}

		public int GetGrowParamDef(int base_def)
		{
			return MonoBehaviourSingleton<SmithManager>.I.GetGrowResultValue(base_def, def, false);
		}

		public int GetGrowParamHp(int base_hp)
		{
			return MonoBehaviourSingleton<SmithManager>.I.GetGrowResultValue(base_hp, hp, false);
		}

		public int[] GetGrowParamElemAtk(int[] base_elem_atk)
		{
			int num = base_elem_atk.Length;
			int[] array = new int[num];
			int i = 0;
			for (int num2 = num; i < num2; i++)
			{
				array[i] = MonoBehaviourSingleton<SmithManager>.I.GetGrowResultValue(base_elem_atk[i], elemAtk[i], true);
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
				array[i] = MonoBehaviourSingleton<SmithManager>.I.GetGrowResultValue(base_elem_def[i], elemDef[i], true);
			}
			return array;
		}

		public void LoadFromBinary(BinaryTableReader reader, ref uint key1, ref uint key2)
		{
			id = key1;
			lv = key2;
			atk = new GrowRate();
			atk.rate = reader.ReadInt32(0);
			atk.add = reader.ReadInt32(0);
			def = new GrowRate();
			def.rate = reader.ReadInt32(0);
			def.add = reader.ReadInt32(0);
			hp = new GrowRate();
			hp.rate = reader.ReadInt32(0);
			hp.add = reader.ReadInt32(0);
			elemAtk = new GrowRate[6];
			int i = 0;
			for (int num = 6; i < num; i++)
			{
				elemAtk[i] = new GrowRate();
				elemAtk[i].rate = reader.ReadInt32(0);
				elemAtk[i].add = reader.ReadInt32(0);
			}
			elemDef = new GrowRate[6];
			int j = 0;
			for (int num2 = 6; j < num2; j++)
			{
				elemDef[j] = new GrowRate();
				elemDef[j].rate = reader.ReadInt32(0);
				elemDef[j].add = reader.ReadInt32(0);
			}
		}

		public void DumpBinary(BinaryWriter writer)
		{
			writer.Write(atk.rate);
			writer.Write(atk.add);
			writer.Write(def.rate);
			writer.Write(def.add);
			writer.Write(hp.rate);
			writer.Write(hp.add);
			int i = 0;
			for (int num = 6; i < num; i++)
			{
				writer.Write(elemAtk[i].rate);
				writer.Write(elemAtk[i].add);
			}
			int j = 0;
			for (int num2 = 6; j < num2; j++)
			{
				writer.Write(elemDef[j].rate);
				writer.Write(elemDef[j].add);
			}
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			GrowEquipItemData growEquipItemData = obj as GrowEquipItemData;
			if (growEquipItemData == null)
			{
				return false;
			}
			bool flag = id.value == growEquipItemData.id.value && lv.value == growEquipItemData.lv.value && atk.Equals(growEquipItemData.atk) && def.Equals(growEquipItemData.def) && hp.Equals(growEquipItemData.hp);
			for (int i = 0; i < elemAtk.Length; i++)
			{
				flag = (flag && elemAtk[i].Equals(growEquipItemData.elemAtk[i]));
			}
			for (int j = 0; j < elemDef.Length; j++)
			{
				flag = (flag && elemDef[j].Equals(growEquipItemData.elemDef[j]));
			}
			return flag;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			return "id:" + id + ", lv:" + lv;
		}
	}

	public class GrowEquipItemNeedItemData : IDoubleUIntKeyBinaryTableData
	{
		public const string NT = "needId,level,itemID_0,itemNum_0,itemID_1,itemNum_1,itemID_2,itemNum_2,itemID_3,itemNum_3,itemID_4,itemNum_4,itemID_5,itemNum_5,itemID_6,itemNum_6,itemID_7,itemNum_7,itemID_8,itemNum_8,itemID_9,itemNum_9,money";

		public uint id;

		public uint lv;

		public NeedMaterial[] needMaterial;

		public int needMoney;

		private static readonly int NEED_MATERIAL_LENGTH_MAX = 10;

		private static List<NeedMaterial> need_material = new List<NeedMaterial>();

		public static bool cb(CSVReader csv_reader, GrowEquipItemNeedItemData data, ref uint key1, ref uint key2)
		{
			data.id = key1;
			data.lv = key2;
			List<NeedMaterial> list = new List<NeedMaterial>();
			for (int i = 0; i < NEED_MATERIAL_LENGTH_MAX; i++)
			{
				uint value = 0u;
				int value2 = 0;
				CSVReader.PopResult popResult = csv_reader.Pop(ref value);
				CSVReader.PopResult popResult2 = csv_reader.Pop(ref value2);
				if (value != 0 && value2 != 0)
				{
					list.Add(new NeedMaterial(value, value2));
				}
			}
			data.needMaterial = list.ToArray();
			CSVReader.PopResult popResult3 = csv_reader.Pop(ref data.needMoney);
			return true;
		}

		public void LoadFromBinary(BinaryTableReader reader, ref uint key1, ref uint key2)
		{
			id = key1;
			lv = key2;
			need_material.Clear();
			for (int i = 0; i < NEED_MATERIAL_LENGTH_MAX; i++)
			{
				uint num = reader.ReadUInt32(0u);
				int num2 = reader.ReadInt32(0);
				if (num != 0 && num2 != 0)
				{
					need_material.Add(new NeedMaterial(num, num2));
				}
			}
			needMaterial = need_material.ToArray();
			needMoney = reader.ReadInt32(0);
		}

		public void DumpBinary(BinaryWriter writer)
		{
			for (int i = 0; i < NEED_MATERIAL_LENGTH_MAX; i++)
			{
				if (i < this.needMaterial.Length)
				{
					NeedMaterial needMaterial = this.needMaterial[i];
					writer.Write(needMaterial.itemID);
					writer.Write(needMaterial.num);
				}
				else
				{
					writer.Write(0u);
					writer.Write(0);
				}
			}
			writer.Write(needMoney);
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			GrowEquipItemNeedItemData growEquipItemNeedItemData = obj as GrowEquipItemNeedItemData;
			if (growEquipItemNeedItemData == null)
			{
				return false;
			}
			bool flag = id == growEquipItemNeedItemData.id && lv == growEquipItemNeedItemData.lv && needMoney == growEquipItemNeedItemData.needMoney;
			if (needMaterial.Length == growEquipItemNeedItemData.needMaterial.Length)
			{
				for (int i = 0; i < needMaterial.Length; i++)
				{
					flag = (flag && needMaterial[i].Equals(growEquipItemNeedItemData.needMaterial[i]));
				}
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			return "id:" + id + ", lv:" + lv + ", needMoney:" + needMoney;
		}
	}

	private DoubleUIntKeyTable<GrowEquipItemData> growTableData;

	private DoubleUIntKeyTable<GrowEquipItemNeedItemData> needTableData;

	private DoubleUIntKeyTable<GrowEquipItemNeedItemData> needUniqueTableData;

	public DoubleUIntKeyTable<GrowEquipItemData> GrowTableData => growTableData;

	public static DoubleUIntKeyTable<GrowEquipItemData> CreateGrowTableCSV(string csv_text)
	{
		return TableUtility.CreateDoubleUIntKeyTable<GrowEquipItemData>(csv_text, GrowEquipItemData.cb, "growId,level,atkRate,atkAdd,defRate,defAdd,hpRate,hpAdd,fireAtkRate,fireAtkAdd,waterAtkRate,waterAtkAdd,thunderAtkRate,thunderAtkAdd,earthAtkRate,earthAtkAdd,lightAtkRate,lightAtkAdd,darkAtkRate,darkAtkAdd,fireDefRate,fireDefAdd,waterDefRate,waterDefAdd,thunderDefRate,thunderDefAdd,earthDefRate,earthDefAdd,lightDefRate,lightDefAdd,darkDefRate,darkDefAdd", null, null, null, null);
	}

	public void CreateGrowTable(string csv_text)
	{
		growTableData = CreateGrowTableCSV(csv_text);
	}

	public void AddGrowTable(string csv_text)
	{
		TableUtility.AddDoubleUIntKeyTable(growTableData, csv_text, GrowEquipItemData.cb, "growId,level,atkRate,atkAdd,defRate,defAdd,hpRate,hpAdd,fireAtkRate,fireAtkAdd,waterAtkRate,waterAtkAdd,thunderAtkRate,thunderAtkAdd,earthAtkRate,earthAtkAdd,lightAtkRate,lightAtkAdd,darkAtkRate,darkAtkAdd,fireDefRate,fireDefAdd,waterDefRate,waterDefAdd,thunderDefRate,thunderDefAdd,earthDefRate,earthDefAdd,lightDefRate,lightDefAdd,darkDefRate,darkDefAdd", null, null, null);
	}

	public static DoubleUIntKeyTable<GrowEquipItemNeedItemData> CreateNeedTableCSV(string csv_text)
	{
		return TableUtility.CreateDoubleUIntKeyTable<GrowEquipItemNeedItemData>(csv_text, GrowEquipItemNeedItemData.cb, "needId,level,itemID_0,itemNum_0,itemID_1,itemNum_1,itemID_2,itemNum_2,itemID_3,itemNum_3,itemID_4,itemNum_4,itemID_5,itemNum_5,itemID_6,itemNum_6,itemID_7,itemNum_7,itemID_8,itemNum_8,itemID_9,itemNum_9,money", null, null, null, null);
	}

	public void CreateNeedTable(string csv_text)
	{
		needTableData = CreateNeedTableCSV(csv_text);
	}

	public void AddNeedTable(string csv_text)
	{
		TableUtility.AddDoubleUIntKeyTable(needTableData, csv_text, GrowEquipItemNeedItemData.cb, "needId,level,itemID_0,itemNum_0,itemID_1,itemNum_1,itemID_2,itemNum_2,itemID_3,itemNum_3,itemID_4,itemNum_4,itemID_5,itemNum_5,itemID_6,itemNum_6,itemID_7,itemNum_7,itemID_8,itemNum_8,itemID_9,itemNum_9,money", null, null, null);
	}

	public static DoubleUIntKeyTable<GrowEquipItemData> CreateGrowTableBinary(byte[] bytes)
	{
		return TableUtility.CreateDoubleUIntKeyTableFromBinary<GrowEquipItemData>(bytes);
	}

	public void CreateGrowTable(byte[] bytes)
	{
		growTableData = CreateGrowTableBinary(bytes);
	}

	public static DoubleUIntKeyTable<GrowEquipItemNeedItemData> CreateNeedTableBinary(byte[] bytes)
	{
		DoubleUIntKeyTable<GrowEquipItemNeedItemData> doubleUIntKeyTable = new DoubleUIntKeyTable<GrowEquipItemNeedItemData>();
		BinaryTableReader binaryTableReader = new BinaryTableReader(bytes);
		while (binaryTableReader.MoveNext())
		{
			uint key = binaryTableReader.ReadUInt32(0u);
			uint key2 = binaryTableReader.ReadUInt32(0u);
			GrowEquipItemNeedItemData growEquipItemNeedItemData = new GrowEquipItemNeedItemData();
			growEquipItemNeedItemData.LoadFromBinary(binaryTableReader, ref key, ref key2);
			doubleUIntKeyTable.Add(key, key2, growEquipItemNeedItemData);
		}
		return doubleUIntKeyTable;
	}

	public void CreateNeedTable(byte[] bytes)
	{
		needTableData = CreateNeedTableBinary(bytes);
	}

	public void CreateNeedTable(MemoryStream stream)
	{
		needTableData = TableUtility.CreateDoubleUIntKeyTableFromBinary<GrowEquipItemNeedItemData>(stream);
	}

	public void CreateNeedUniqueTable(string csv_text)
	{
		needUniqueTableData = TableUtility.CreateDoubleUIntKeyTable<GrowEquipItemNeedItemData>(csv_text, GrowEquipItemNeedItemData.cb, "needId,level,itemID_0,itemNum_0,itemID_1,itemNum_1,itemID_2,itemNum_2,itemID_3,itemNum_3,itemID_4,itemNum_4,itemID_5,itemNum_5,itemID_6,itemNum_6,itemID_7,itemNum_7,itemID_8,itemNum_8,itemID_9,itemNum_9,money", null, null, null, null);
	}

	public void AddNeedUniqueTable(string csv_text)
	{
		TableUtility.AddDoubleUIntKeyTable(needUniqueTableData, csv_text, GrowEquipItemNeedItemData.cb, "needId,level,itemID_0,itemNum_0,itemID_1,itemNum_1,itemID_2,itemNum_2,itemID_3,itemNum_3,itemID_4,itemNum_4,itemID_5,itemNum_5,itemID_6,itemNum_6,itemID_7,itemNum_7,itemID_8,itemNum_8,itemID_9,itemNum_9,money", null, null, null);
	}

	public GrowEquipItemData GetGrowEquipItemData(uint id, uint lv)
	{
		if (growTableData == null)
		{
			return null;
		}
		UIntKeyTable<GrowEquipItemData> uIntKeyTable = growTableData.Get(id);
		if (uIntKeyTable == null)
		{
			Log.Error("GrowEquipItemTable is NULL :: grow id = " + id + " Lv = " + lv);
			return null;
		}
		GrowEquipItemData growEquipItemData = uIntKeyTable.Get(lv);
		if (growEquipItemData != null && lv > 1)
		{
			return growEquipItemData;
		}
		GrowEquipItemData under = null;
		GrowEquipItemData over = null;
		uIntKeyTable.ForEach(delegate(GrowEquipItemData table)
		{
			if ((uint)table.lv > lv && (over == null || (uint)table.lv < (uint)over.lv))
			{
				over = table;
			}
			if ((uint)table.lv <= lv && (under == null || (uint)table.lv > (uint)under.lv))
			{
				under = table;
			}
		});
		if (under != null && over == null)
		{
			return under;
		}
		if (under == null)
		{
			under = new GrowEquipItemData();
			under.lv = 1u;
			under.id = id;
			under.atk = new GrowRate();
			under.atk.rate = 100;
			under.atk.add = 0;
			under.def = new GrowRate();
			under.def.rate = 100;
			under.def.add = 0;
			under.hp = new GrowRate();
			under.hp.rate = 100;
			under.hp.add = 0;
			under.elemAtk = new GrowRate[6];
			for (int i = 0; i < 6; i++)
			{
				under.elemAtk[i] = new GrowRate();
				under.elemAtk[i].rate = 100;
				under.elemAtk[i].add = 0;
			}
			under.elemDef = new GrowRate[6];
			for (int j = 0; j < 6; j++)
			{
				under.elemDef[j] = new GrowRate();
				under.elemDef[j].rate = 100;
				under.elemDef[j].add = 0;
			}
		}
		GrowEquipItemData growEquipItemData2 = new GrowEquipItemData();
		float num = (float)(double)(lv - (uint)under.lv) / (float)(double)((uint)over.lv - (uint)under.lv);
		growEquipItemData2.id = id;
		growEquipItemData2.lv = lv;
		growEquipItemData2.atk = new GrowRate();
		growEquipItemData2.atk.rate = Mathf.FloorToInt(Mathf.Lerp((float)(int)under.atk.rate, (float)(int)over.atk.rate, num));
		growEquipItemData2.atk.add = Mathf.FloorToInt(Mathf.Lerp((float)(int)under.atk.add, (float)(int)over.atk.add, num));
		growEquipItemData2.def = new GrowRate();
		growEquipItemData2.def.rate = Mathf.FloorToInt(Mathf.Lerp((float)(int)under.def.rate, (float)(int)over.def.rate, num));
		growEquipItemData2.def.add = Mathf.FloorToInt(Mathf.Lerp((float)(int)under.def.add, (float)(int)over.def.add, num));
		growEquipItemData2.hp = new GrowRate();
		growEquipItemData2.hp.rate = Mathf.FloorToInt(Mathf.Lerp((float)(int)under.hp.rate, (float)(int)over.hp.rate, num));
		growEquipItemData2.hp.add = Mathf.FloorToInt(Mathf.Lerp((float)(int)under.hp.add, (float)(int)over.hp.add, num));
		growEquipItemData2.elemAtk = new GrowRate[6];
		for (int k = 0; k < 6; k++)
		{
			growEquipItemData2.elemAtk[k] = new GrowRate();
			growEquipItemData2.elemAtk[k].rate = Mathf.FloorToInt(Mathf.Lerp((float)(int)under.elemAtk[k].rate, (float)(int)over.elemAtk[k].rate, num));
			growEquipItemData2.elemAtk[k].add = Mathf.FloorToInt(Mathf.Lerp((float)(int)under.elemAtk[k].add, (float)(int)over.elemAtk[k].add, num));
		}
		growEquipItemData2.elemDef = new GrowRate[6];
		for (int l = 0; l < 6; l++)
		{
			growEquipItemData2.elemDef[l] = new GrowRate();
			growEquipItemData2.elemDef[l].rate = Mathf.FloorToInt(Mathf.Lerp((float)(int)under.elemDef[l].rate, (float)(int)over.elemDef[l].rate, num));
			growEquipItemData2.elemDef[l].add = Mathf.FloorToInt(Mathf.Lerp((float)(int)under.elemDef[l].add, (float)(int)over.elemDef[l].add, num));
		}
		return growEquipItemData2;
	}

	public GrowEquipItemNeedItemData GetGrowEquipItemNeedItemData(uint id, uint lv)
	{
		if (needTableData == null)
		{
			return null;
		}
		UIntKeyTable<GrowEquipItemNeedItemData> uIntKeyTable = needTableData.Get(id);
		if (uIntKeyTable == null)
		{
			Log.Error("GetGrowEquipItemNeedItemData is NULL :: need id = " + id + " Lv = " + lv);
			return null;
		}
		GrowEquipItemNeedItemData growEquipItemNeedItemData = uIntKeyTable.Get(lv);
		if (growEquipItemNeedItemData != null)
		{
			return growEquipItemNeedItemData;
		}
		GrowEquipItemNeedItemData under = null;
		GrowEquipItemNeedItemData over = null;
		uIntKeyTable.ForEach(delegate(GrowEquipItemNeedItemData table)
		{
			if (table.lv > lv && (over == null || table.lv < over.lv))
			{
				over = table;
			}
			if (table.lv <= lv && (under == null || table.lv > under.lv))
			{
				under = table;
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
		GrowEquipItemNeedItemData growEquipItemNeedItemData2 = new GrowEquipItemNeedItemData();
		float lerp_value = (float)(double)(lv - under.lv) / (float)(double)(over.lv - under.lv);
		growEquipItemNeedItemData2.id = id;
		growEquipItemNeedItemData2.lv = lv;
		List<NeedMaterial> material_list = new List<NeedMaterial>();
		Array.ForEach(under.needMaterial, delegate(NeedMaterial material_data)
		{
			Array.ForEach(over.needMaterial, delegate(NeedMaterial over_need_material)
			{
				if (over_need_material.itemID == material_data.itemID)
				{
					material_list.Add(new NeedMaterial(over_need_material.itemID, Mathf.FloorToInt(Mathf.Lerp((float)material_data.num, (float)over_need_material.num, lerp_value))));
				}
			});
		});
		growEquipItemNeedItemData2.needMaterial = material_list.ToArray();
		growEquipItemNeedItemData2.needMoney = Mathf.FloorToInt(Mathf.Lerp((float)under.needMoney, (float)over.needMoney, lerp_value));
		return growEquipItemNeedItemData2;
	}

	public GrowEquipItemNeedItemData GetGrowEquipItemNeedUniqueItemData(uint needUniqueId, uint lv)
	{
		if (needUniqueId == 0)
		{
			return null;
		}
		if (needUniqueTableData == null)
		{
			return null;
		}
		return needUniqueTableData.Get(needUniqueId)?.Get(lv);
	}
}
