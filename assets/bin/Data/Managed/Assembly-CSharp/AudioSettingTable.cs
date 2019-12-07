using System;

public class AudioSettingTable : Singleton<AudioSettingTable>, IDataTable
{
	[Serializable]
	public class Data
	{
		public uint id;

		public string name;

		public float minDistance;

		public float maxDistance;

		public const string NT = "id,name,minDistance,maxDistance";

		public static bool cb(CSVReader csvReader, Data data, ref uint key)
		{
			data.id = key;
			csvReader.Pop(ref data.name);
			csvReader.Pop(ref data.minDistance);
			csvReader.Pop(ref data.maxDistance);
			return true;
		}
	}

	public UIntKeyTable<Data> audioSettingTable;

	public void CreateTableFromInternal(string encrypted_csv_text)
	{
		string csv_text = DataTableManager.Decrypt(encrypted_csv_text);
		CreateTable(csv_text);
	}

	public void CreateTable(string csv_text)
	{
		audioSettingTable = TableUtility.CreateUIntKeyTable<Data>(csv_text, Data.cb, "id,name,minDistance,maxDistance");
		audioSettingTable.TrimExcess();
	}

	public Data GetData(uint id)
	{
		if (audioSettingTable == null)
		{
			return null;
		}
		return audioSettingTable.Get(id);
	}

	public string GetSnapshotName(uint id)
	{
		if (audioSettingTable == null)
		{
			return string.Empty;
		}
		Data data = audioSettingTable.Get(id);
		if (data == null)
		{
			return string.Empty;
		}
		return data.name;
	}
}
