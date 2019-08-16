using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class DegreeTable : Singleton<DegreeTable>, IDataTable
{
	public class DegreeData
	{
		public uint id;

		public string name;

		public DEGREE_TYPE type;

		public DEGREE_REQUIREMENT_TYPE requirementType;

		public string requirementText;

		public uint lockNameId;

		public uint lockTextId;

		public const string NT = "id,name,type,requirementType,requirementText,lockNameId,lockTextId";

		public static bool cb(CSVReader csv_reader, DegreeData data, ref uint key)
		{
			data.id = key;
			csv_reader.Pop(ref data.name);
			csv_reader.PopEnum(ref data.type, DEGREE_TYPE.NONE);
			csv_reader.PopEnum(ref data.requirementType, DEGREE_REQUIREMENT_TYPE.COMMON);
			csv_reader.Pop(ref data.requirementText);
			csv_reader.Pop(ref data.lockNameId);
			csv_reader.Pop(ref data.lockTextId);
			return true;
		}

		public bool IsUnlcok(List<int> userUnlockList)
		{
			if (requirementType == DEGREE_REQUIREMENT_TYPE.COMMON)
			{
				return true;
			}
			return userUnlockList?.Contains((int)id) ?? false;
		}

		public bool IsSecretText(List<int> userUnlockList)
		{
			if (lockTextId == 0)
			{
				return false;
			}
			if (IsUnlcok(userUnlockList))
			{
				return false;
			}
			if (userUnlockList == null)
			{
				return true;
			}
			return !userUnlockList.Contains((int)lockTextId);
		}

		public bool IsSecretName(List<int> userUnlockList)
		{
			if (lockNameId == 0)
			{
				return false;
			}
			if (IsUnlcok(userUnlockList))
			{
				return false;
			}
			if (userUnlockList == null)
			{
				return true;
			}
			return !userUnlockList.Contains((int)lockNameId);
		}
	}

	public const int INFO_MAX = 3;

	private UIntKeyTable<DegreeData> dataTable;

	[CompilerGenerated]
	private static TableUtility.CallBackUIntKeyReadCSV<DegreeData> _003C_003Ef__mg_0024cache0;

	[CompilerGenerated]
	private static TableUtility.CallBackUIntKeyReadCSV<DegreeData> _003C_003Ef__mg_0024cache1;

	public void CreateTable(string csv_text)
	{
		dataTable = TableUtility.CreateUIntKeyTable<DegreeData>(csv_text, DegreeData.cb, "id,name,type,requirementType,requirementText,lockNameId,lockTextId");
		dataTable.TrimExcess();
	}

	public void AddTable(string csv_text)
	{
		TableUtility.AddUIntKeyTable(dataTable, csv_text, DegreeData.cb, "id,name,type,requirementType,requirementText,lockNameId,lockTextId");
	}

	public DegreeData GetData(uint id)
	{
		if (dataTable == null)
		{
			return null;
		}
		DegreeData degreeData = dataTable.Get(id);
		if (degreeData == null)
		{
			Log.TableError(this, id);
			degreeData = new DegreeData();
			degreeData.name = Log.NON_DATA_NAME;
		}
		return degreeData;
	}

	public List<DegreeData> GetAll()
	{
		List<DegreeData> allData = new List<DegreeData>();
		dataTable.ForEach(delegate(DegreeData x)
		{
			if (x != null)
			{
				allData.Add(x);
			}
		});
		return allData;
	}
}
