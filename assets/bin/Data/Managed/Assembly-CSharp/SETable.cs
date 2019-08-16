using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SETable : Singleton<SETable>, IDataTable
{
	[Serializable]
	public class Data
	{
		public uint id;

		public uint priority;

		public float intervalLimit;

		public float volumeOffset;

		public float volumeScale;

		public float dopplerLevel;

		public float minDistance;

		public float maxDistance;

		public float randomPitch;

		public int limitNum;

		public uint cullingType;

		public AudioControlGroup.CullingTypes CullingType;

		public const string NT = "id,priority,intervalLimit,volumeOffset,randomPitch,dopplerLevel,minDistance,maxDistance,limitNum,cullingType";

		public static bool cb(CSVReader csvReader, Data data, ref uint key)
		{
			data.id = key;
			csvReader.Pop(ref data.priority);
			csvReader.Pop(ref data.intervalLimit);
			data.intervalLimit /= 1000f;
			csvReader.Pop(ref data.volumeOffset);
			data.volumeOffset = Mathf.Clamp(data.volumeOffset, -80f, 0f);
			data.volumeScale = Utility.DecibelToVolume(data.volumeOffset);
			csvReader.Pop(ref data.randomPitch);
			csvReader.Pop(ref data.dopplerLevel);
			data.dopplerLevel = Mathf.Clamp(data.dopplerLevel, 0f, 5f);
			csvReader.Pop(ref data.minDistance);
			csvReader.Pop(ref data.maxDistance);
			csvReader.Pop(ref data.limitNum);
			csvReader.Pop(ref data.cullingType);
			if (4 > data.cullingType)
			{
				data.CullingType = (AudioControlGroup.CullingTypes)data.cullingType;
			}
			else
			{
				data.CullingType = AudioControlGroup.CullingTypes.NONE;
			}
			return true;
		}
	}

	public UIntKeyTable<Data> seTable;

	[CompilerGenerated]
	private static TableUtility.CallBackUIntKeyReadCSV<Data> _003C_003Ef__mg_0024cache0;

	public void CreateTableFromInternal(string encrypted_csv_text)
	{
		string csv_text = DataTableManager.Decrypt(encrypted_csv_text);
		CreateTable(csv_text);
	}

	public void CreateTable(string csv_text)
	{
		seTable = TableUtility.CreateUIntKeyTable<Data>(csv_text, Data.cb, "id,priority,intervalLimit,volumeOffset,randomPitch,dopplerLevel,minDistance,maxDistance,limitNum,cullingType");
		seTable.TrimExcess();
	}

	public Data GetSeData(uint id)
	{
		if (seTable == null)
		{
			return null;
		}
		return seTable.Get(id);
	}
}
