using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[Obsolete]
public class EnemyCollectionTable : Singleton<EnemyCollectionTable>, IDataTable
{
	public class EnemyCollectionData
	{
		public uint id;

		public uint enemySpeciesId;

		public string name;

		public uint regionId;

		public COLLECTION_TYPE collectionType;

		public string flavorText;

		public const string NT = "id,enemySpeciesId,name,regionId,collectionType,flavorText";

		public static bool cb(CSVReader csv_reader, EnemyCollectionData data, ref uint key)
		{
			data.id = key;
			csv_reader.Pop(ref data.enemySpeciesId);
			csv_reader.Pop(ref data.name);
			csv_reader.Pop(ref data.regionId);
			csv_reader.Pop(ref data.collectionType);
			csv_reader.Pop(ref data.flavorText);
			return true;
		}
	}

	private UIntKeyTable<EnemyCollectionData> enemyCollectionTable;

	[CompilerGenerated]
	private static TableUtility.CallBackUIntKeyReadCSV<EnemyCollectionData> _003C_003Ef__mg_0024cache0;

	[CompilerGenerated]
	private static TableUtility.CallBackUIntKeyReadCSV<EnemyCollectionData> _003C_003Ef__mg_0024cache1;

	public void CreateTable(string csv_text)
	{
		enemyCollectionTable = TableUtility.CreateUIntKeyTable<EnemyCollectionData>(csv_text, EnemyCollectionData.cb, "id,enemySpeciesId,name,regionId,collectionType,flavorText");
		enemyCollectionTable.TrimExcess();
	}

	public void AddTable(string csv_text)
	{
		TableUtility.AddUIntKeyTable(enemyCollectionTable, csv_text, EnemyCollectionData.cb, "id,enemySpeciesId,name,regionId,collectionType,flavorText");
	}

	public EnemyCollectionData GetEnemyCollectionData(uint id)
	{
		if (id == 0 || enemyCollectionTable == null)
		{
			return null;
		}
		EnemyCollectionData enemyCollectionData = enemyCollectionTable.Get(id);
		if (enemyCollectionData == null)
		{
			Log.TableError(this, id);
			enemyCollectionData = new EnemyCollectionData();
			enemyCollectionData.name = Log.NON_DATA_NAME;
		}
		return enemyCollectionData;
	}

	public List<EnemyCollectionData> GetEnemyCollectionDataByRegion(uint regionId)
	{
		List<EnemyCollectionData> searchData = new List<EnemyCollectionData>();
		enemyCollectionTable.ForEach(delegate(EnemyCollectionData data)
		{
			if (data.regionId == regionId)
			{
				searchData.Add(data);
			}
		});
		return searchData;
	}
}
