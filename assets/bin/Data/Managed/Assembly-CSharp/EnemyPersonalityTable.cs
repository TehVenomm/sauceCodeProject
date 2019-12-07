using UnityEngine;

public class EnemyPersonalityTable : Singleton<EnemyPersonalityTable>, IDataTable
{
	public class Data
	{
		public uint id;

		public HateParam param = new HateParam();

		public const string NT = "id,distanceHateRate,shortShortDistance,shortDistance,middleDistance,longDistance,lifeLowerImportance,lifeLownerVolatize,lifeLowerAttackedVolatize,shortShortDistanceDamage,shortDistanceDamage,middleDistanceDamage,longDistanceDamage,damageImportance,damageVolatize,damageAttackedVolatize,healImportance,healVolatize,healAttackedVolatize,skillImportance,skillVolatize,skillDamagedVolatize,skillHateParam,specialDamageImportance,specialDamageVolatize,specialDamageAttackedVolatize,weakPointHate";

		public static bool cb(CSVReader csv_reader, Data data, ref uint key)
		{
			float value = 0f;
			for (int i = 0; i < data.param.categoryParam.Length; i++)
			{
				data.param.categoryParam[i] = new HateParam.CategoryParam();
			}
			data.id = key;
			csv_reader.Pop(ref data.param.categoryParam[0].importance);
			for (int j = 0; j < 4; j++)
			{
				csv_reader.Pop(ref data.param.distanceHateParams[j]);
			}
			csv_reader.Pop(ref data.param.categoryParam[1].importance);
			csv_reader.Pop(ref value);
			data.param.categoryParam[1].volatilizeRate = 1f - value;
			csv_reader.Pop(ref value);
			data.param.categoryParam[1].atackedVolatizeRate = 1f - value;
			for (int k = 0; k < 4; k++)
			{
				csv_reader.Pop(ref data.param.distanceAttackRatio[k]);
			}
			for (int l = 2; l < 7; l++)
			{
				csv_reader.Pop(ref data.param.categoryParam[l].importance);
				csv_reader.Pop(ref value);
				data.param.categoryParam[l].volatilizeRate = 1f - value;
				csv_reader.Pop(ref value);
				data.param.categoryParam[l].atackedVolatizeRate = 1f - value;
				if (l == 4)
				{
					csv_reader.Pop(ref data.param.skillHate);
				}
				if (l == 5)
				{
					csv_reader.Pop(ref data.param.attackedWeakPointHate);
				}
			}
			return true;
		}
	}

	private UIntKeyTable<Data> dataTable;

	public void CreateTable(TextAsset textasset)
	{
		CreateTable(textasset.text);
	}

	public void CreateTable(string csv)
	{
		dataTable = TableUtility.CreateUIntKeyTable<Data>(csv, Data.cb, "id,distanceHateRate,shortShortDistance,shortDistance,middleDistance,longDistance,lifeLowerImportance,lifeLownerVolatize,lifeLowerAttackedVolatize,shortShortDistanceDamage,shortDistanceDamage,middleDistanceDamage,longDistanceDamage,damageImportance,damageVolatize,damageAttackedVolatize,healImportance,healVolatize,healAttackedVolatize,skillImportance,skillVolatize,skillDamagedVolatize,skillHateParam,specialDamageImportance,specialDamageVolatize,specialDamageAttackedVolatize,weakPointHate");
		dataTable.TrimExcess();
	}

	public void AddTable(TextAsset textasset)
	{
		TableUtility.AddUIntKeyTable(dataTable, textasset.text, Data.cb, "id,distanceHateRate,shortShortDistance,shortDistance,middleDistance,longDistance,lifeLowerImportance,lifeLownerVolatize,lifeLowerAttackedVolatize,shortShortDistanceDamage,shortDistanceDamage,middleDistanceDamage,longDistanceDamage,damageImportance,damageVolatize,damageAttackedVolatize,healImportance,healVolatize,healAttackedVolatize,skillImportance,skillVolatize,skillDamagedVolatize,skillHateParam,specialDamageImportance,specialDamageVolatize,specialDamageAttackedVolatize,weakPointHate");
	}

	public Data GetData(uint id)
	{
		if (dataTable == null)
		{
			return null;
		}
		return dataTable.Get(id);
	}
}
