using System;
using System.Collections.Generic;

public class EnemyTable : Singleton<EnemyTable>, IDataTable
{
	public class EnemyData
	{
		public uint id;

		public uint enemyCollectionId;

		public string appVer;

		public string name;

		public ENEMY_TYPE type;

		public int enemySpecies;

		public XorInt level;

		public int actionId;

		public int iconId;

		public int modelId;

		public int animId;

		public float modelScale;

		public string baseEffectName;

		public string baseEffectNode;

		public bool active;

		public ELEMENT_TYPE element;

		public ELEMENT_TYPE weakElement;

		public XorFloat hpRate = 0f;

		public XorFloat atkRate = 0f;

		public string convertRegionKey;

		public float aimMarkerRate;

		public string effectEnemyKey;

		public uint personality;

		public uint growId;

		public string weatherChangeEffect;

		public const string NT = "id,enemyCollectionId,appVer,name,type,enemySpecies,lv,actionId,iconID,modelID,animID,modelScale,baseEffectName,baseEffectNode,active,element,weakElement,hp,atk,convertRegionKey,aimMarkerRate,effectEnemyKey,personality,growId,weatherChangeEffect,exActionId,exActionCondition,exActionConditionValue";

		public bool IsEnableNowApplicationVersion()
		{
			return AppMain.CheckApplicationVersion(appVer);
		}

		public static bool cb(CSVReader csv_reader, EnemyData data, ref uint key)
		{
			data.id = key;
			csv_reader.Pop(ref data.enemyCollectionId);
			csv_reader.Pop(ref data.appVer);
			csv_reader.Pop(ref data.name);
			csv_reader.PopEnum(ref data.type, ENEMY_TYPE.NONE);
			csv_reader.Pop(ref data.enemySpecies);
			csv_reader.Pop(ref data.level);
			csv_reader.Pop(ref data.actionId);
			csv_reader.Pop(ref data.iconId);
			csv_reader.Pop(ref data.modelId);
			csv_reader.Pop(ref data.animId);
			csv_reader.Pop(ref data.modelScale);
			csv_reader.Pop(ref data.baseEffectName);
			csv_reader.Pop(ref data.baseEffectNode);
			csv_reader.Pop(ref data.active);
			string value = string.Empty;
			csv_reader.Pop(ref value);
			if (value.Length > 1)
			{
				data.element = (ELEMENT_TYPE)Enum.Parse(typeof(ELEMENT_TYPE), value);
			}
			else
			{
				data.element = ELEMENT_TYPE.MAX;
			}
			csv_reader.Pop(ref value);
			if (value.Length > 1)
			{
				data.weakElement = (ELEMENT_TYPE)Enum.Parse(typeof(ELEMENT_TYPE), value);
			}
			else
			{
				data.weakElement = ELEMENT_TYPE.MAX;
			}
			csv_reader.Pop(ref data.hpRate);
			data.hpRate = (float)data.hpRate * 0.01f;
			csv_reader.Pop(ref data.atkRate);
			data.atkRate = (float)data.atkRate * 0.01f;
			csv_reader.Pop(ref data.convertRegionKey);
			data.aimMarkerRate = 1f;
			csv_reader.Pop(ref data.aimMarkerRate);
			csv_reader.Pop(ref data.effectEnemyKey);
			csv_reader.Pop(ref data.personality);
			csv_reader.Pop(ref data.growId);
			csv_reader.Pop(ref data.weatherChangeEffect);
			return true;
		}
	}

	private UIntKeyTable<EnemyData> enemyTable;

	public void CreateTable(string csv_text)
	{
		enemyTable = TableUtility.CreateUIntKeyTable<EnemyData>(csv_text, EnemyData.cb, "id,enemyCollectionId,appVer,name,type,enemySpecies,lv,actionId,iconID,modelID,animID,modelScale,baseEffectName,baseEffectNode,active,element,weakElement,hp,atk,convertRegionKey,aimMarkerRate,effectEnemyKey,personality,growId,weatherChangeEffect,exActionId,exActionCondition,exActionConditionValue");
	}

	public void CreateTable(string csv_text, TableUtility.Progress progress)
	{
		enemyTable = TableUtility.CreateUIntKeyTable<EnemyData>(csv_text, EnemyData.cb, "id,enemyCollectionId,appVer,name,type,enemySpecies,lv,actionId,iconID,modelID,animID,modelScale,baseEffectName,baseEffectNode,active,element,weakElement,hp,atk,convertRegionKey,aimMarkerRate,effectEnemyKey,personality,growId,weatherChangeEffect,exActionId,exActionCondition,exActionConditionValue", progress);
		enemyTable.TrimExcess();
	}

	public void AddTable(string csv_text)
	{
		TableUtility.AddUIntKeyTable(enemyTable, csv_text, EnemyData.cb, "id,enemyCollectionId,appVer,name,type,enemySpecies,lv,actionId,iconID,modelID,animID,modelScale,baseEffectName,baseEffectNode,active,element,weakElement,hp,atk,convertRegionKey,aimMarkerRate,effectEnemyKey,personality,growId,weatherChangeEffect,exActionId,exActionCondition,exActionConditionValue");
	}

	public EnemyData GetEnemyData(uint id)
	{
		if (id == 0 || enemyTable == null)
		{
			return null;
		}
		EnemyData enemyData = enemyTable.Get(id);
		if (enemyData == null)
		{
			Log.TableError(this, id);
			enemyData = new EnemyData();
			enemyData.name = Log.NON_DATA_NAME;
		}
		return enemyData;
	}

	public List<EnemyData> GetEnemyDataByEnemyCollectionId(uint collectionId)
	{
		List<EnemyData> searchData = new List<EnemyData>();
		enemyTable.ForEach(delegate(EnemyData data)
		{
			if (data.enemyCollectionId == collectionId)
			{
				searchData.Add(data);
			}
		});
		if (searchData.Count == 0)
		{
			Log.Error("Not found enemyData by collection id {0}", collectionId);
		}
		return searchData;
	}

	public string GetEnemyName(uint id)
	{
		EnemyData enemyData = GetEnemyData(id);
		if (enemyData == null)
		{
			return string.Empty;
		}
		return enemyData.name;
	}

	public bool IsAvailable()
	{
		return enemyTable != null;
	}

	public List<EnemyData> GetAllEnemyDatas()
	{
		List<EnemyData> datas = new List<EnemyData>();
		enemyTable.ForEach(delegate(EnemyData data)
		{
			datas.Add(data);
		});
		return datas;
	}
}
