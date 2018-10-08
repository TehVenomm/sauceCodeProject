using System.Collections.Generic;
using UnityEngine;

public class EnemyHitMaterialTable : Singleton<EnemyHitMaterialTable>, IDataTable
{
	public class MaterialData
	{
		public const string NT = "name,se_id_,add_effect_name";

		public int[] typeSEIDs;

		public string addEffectName;

		public static bool cb(CSVReader csv, MaterialData data, ref string key)
		{
			int num = 0;
			if (Singleton<EnemyHitMaterialTable>.IsValid() && Singleton<EnemyHitMaterialTable>.I.typeKeyTable != null)
			{
				num = Singleton<EnemyHitMaterialTable>.I.typeKeyTable.Count;
			}
			data.typeSEIDs = new int[num];
			for (int i = 0; i < num; i++)
			{
				data.typeSEIDs[i] = 0;
				csv.Pop(ref data.typeSEIDs[i]);
			}
			csv.Pop(ref data.addEffectName);
			return true;
		}

		public int GetTypeSEID(string hit_type)
		{
			if (!Singleton<EnemyHitMaterialTable>.IsValid())
			{
				return 0;
			}
			if (Singleton<EnemyHitMaterialTable>.I.typeKeyTable == null)
			{
				return 0;
			}
			if (typeSEIDs == null)
			{
				return 0;
			}
			int num = Singleton<EnemyHitMaterialTable>.I.typeKeyTable.IndexOf(hit_type);
			if (num < 0 || num >= typeSEIDs.Length)
			{
				return 0;
			}
			return typeSEIDs[num];
		}
	}

	protected List<string> typeKeyTable = new List<string>();

	public StringKeyTable<MaterialData> dataTable
	{
		get;
		private set;
	}

	public void CreateTable(TextAsset stage_table_text_asset)
	{
		CreateTable(stage_table_text_asset.get_text());
	}

	public void CreateTable(string csv)
	{
		if (!Singleton<EnemyHitTypeTable>.IsValid() || Singleton<EnemyHitTypeTable>.I.dataTable == null)
		{
			Log.Error(LOG.INGAME, "EnemyHitMaterialTable::CreateTable() Err ( EnemyHitTypeTable is invalid. )");
		}
		else
		{
			typeKeyTable = new List<string>();
			Singleton<EnemyHitTypeTable>.I.dataTable.ForEachKeys(delegate(string key)
			{
				if (key.IndexOf("@") < 0)
				{
					typeKeyTable.Add(key);
				}
			});
			string text = string.Empty;
			int i = 0;
			for (int count = typeKeyTable.Count; i < count; i++)
			{
				text = text + "se_id_" + typeKeyTable[i];
				if (i != count - 1)
				{
					text += ",";
				}
			}
			string name_table = "name,se_id_,add_effect_name".Replace("se_id_", text);
			dataTable = TableUtility.CreateStringKeyTable<MaterialData>(csv, MaterialData.cb, name_table);
			dataTable.TrimExcess();
		}
	}

	public MaterialData GetData(string name)
	{
		if (dataTable == null)
		{
			return null;
		}
		return dataTable.Get(name);
	}
}
