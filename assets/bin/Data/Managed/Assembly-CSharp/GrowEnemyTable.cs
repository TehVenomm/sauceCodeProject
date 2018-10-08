using UnityEngine;

public class GrowEnemyTable : Singleton<GrowEnemyTable>, IDataTable
{
	public class GrowEnemyData
	{
		public const string NT = "growId,lv,hp,atk";

		public uint growId;

		public XorInt level = 1;

		public XorInt hp = 100;

		public XorInt atk = 100;

		public float hpRate => (float)(int)hp * 0.01f;

		public float atkRate => (float)(int)atk * 0.01f;

		public static bool cb(CSVReader csv_reader, GrowEnemyData data, ref uint key1, ref uint key2)
		{
			data.growId = key1;
			data.level = (int)key2;
			float value = 0f;
			csv_reader.Pop(ref value);
			data.hp = Mathf.RoundToInt(value);
			float value2 = 0f;
			csv_reader.Pop(ref value2);
			data.atk = Mathf.RoundToInt(value2);
			return true;
		}
	}

	private DoubleUIntKeyTable<GrowEnemyData> growTableData;

	private GrowEnemyData _defaultData = new GrowEnemyData();

	public void CreateTable(string csv_text)
	{
		growTableData = TableUtility.CreateDoubleUIntKeyTable<GrowEnemyData>(csv_text, GrowEnemyData.cb, "growId,lv,hp,atk", null, null, null, null);
		growTableData.TrimExcess();
	}

	public void AddTable(string csv_text)
	{
		TableUtility.AddDoubleUIntKeyTable(growTableData, csv_text, GrowEnemyData.cb, "growId,lv,hp,atk", null, null, null);
	}

	public GrowEnemyData GetGrowEnemyData(uint growId, int level)
	{
		if (growTableData == null)
		{
			Log.Warning("GetGrowEnemyData: growTableDate is null!");
			return _defaultData;
		}
		UIntKeyTable<GrowEnemyData> uIntKeyTable = growTableData.Get(growId);
		if (uIntKeyTable == null)
		{
			Log.Warning("GetGrowEnemyData: growId {0} is not found!", growId);
			return _defaultData;
		}
		GrowEnemyData growEnemyData = uIntKeyTable.Get((uint)level);
		if (growEnemyData != null)
		{
			return growEnemyData;
		}
		GrowEnemyData prev = null;
		GrowEnemyData next = null;
		uIntKeyTable.ForEach(delegate(GrowEnemyData grow)
		{
			if ((int)grow.level < level && (prev == null || (int)grow.level > (int)prev.level))
			{
				prev = grow;
			}
			if ((int)grow.level > level && (next == null || (int)grow.level < (int)next.level))
			{
				next = grow;
			}
		});
		if (next == null || prev == null)
		{
			if (next != null)
			{
				return next;
			}
			if (prev != null)
			{
				return prev;
			}
			Log.Warning("GetGrowEnemyData: growId {0}, Lv {1} Lerp error", growId, level);
			return _defaultData;
		}
		float num = (float)(level - (int)prev.level) / (float)((int)next.level - (int)prev.level);
		growEnemyData = new GrowEnemyData();
		growEnemyData.growId = growId;
		growEnemyData.level = level;
		growEnemyData.hp = Mathf.FloorToInt(Mathf.Lerp((float)(int)prev.hp, (float)(int)next.hp, num));
		growEnemyData.atk = Mathf.FloorToInt(Mathf.Lerp((float)(int)prev.atk, (float)(int)next.atk, num));
		return growEnemyData;
	}
}
