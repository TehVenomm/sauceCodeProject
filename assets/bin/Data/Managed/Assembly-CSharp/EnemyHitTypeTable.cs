using UnityEngine;

public class EnemyHitTypeTable : Singleton<EnemyHitTypeTable>, IDataTable
{
	public class TypeData
	{
		public string baseEffectName;

		public string[] elementEffectNames = new string[6];

		public const string NT = "name,base_effect,fire_effect,water_effect,thunder_effect,soil_effect,light_effect,dark_effect";

		public static bool cb(CSVReader csv, TypeData data, ref string key)
		{
			csv.Pop(ref data.baseEffectName);
			for (int i = 0; i < 6; i++)
			{
				csv.Pop(ref data.elementEffectNames[i]);
			}
			return true;
		}
	}

	public StringKeyTable<TypeData> dataTable
	{
		get;
		private set;
	}

	public void CreateTable(TextAsset stage_table_text_asset)
	{
		CreateTable(stage_table_text_asset.text);
	}

	public void CreateTable(string csv)
	{
		dataTable = TableUtility.CreateStringKeyTable<TypeData>(csv, TypeData.cb, "name,base_effect,fire_effect,water_effect,thunder_effect,soil_effect,light_effect,dark_effect");
		dataTable.TrimExcess();
	}

	public TypeData GetData(string name, bool is_field)
	{
		if (dataTable == null)
		{
			return null;
		}
		if (is_field)
		{
			name += "@FIELD";
		}
		return dataTable.Get(name);
	}
}
