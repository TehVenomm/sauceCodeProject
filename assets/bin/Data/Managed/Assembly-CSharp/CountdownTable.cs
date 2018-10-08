using System;

public class CountdownTable : Singleton<CountdownTable>, IDataTable
{
	public class CountdownData
	{
		public const string NT = "id,imageID,startAt,endAt";

		public int id;

		public int imageID;

		public DateTime startAt;

		public DateTime endAt;

		public static bool cb(CSVReader csv_reader, CountdownData data, ref uint key)
		{
			data.id = (int)key;
			csv_reader.Pop(ref data.imageID);
			string value = string.Empty;
			csv_reader.Pop(ref value);
			if (!string.IsNullOrEmpty(value))
			{
				DateTime.TryParse(value, out data.startAt);
			}
			string value2 = string.Empty;
			csv_reader.Pop(ref value2);
			if (!string.IsNullOrEmpty(value2))
			{
				DateTime.TryParse(value2, out data.endAt);
			}
			return true;
		}
	}

	private UIntKeyTable<CountdownData> countdownDataTable;

	public void CreateTable(string csv_text)
	{
		countdownDataTable = TableUtility.CreateUIntKeyTable<CountdownData>(csv_text, CountdownData.cb, "id,imageID,startAt,endAt", null);
		countdownDataTable.TrimExcess();
	}

	public CountdownData GetCountdownData(DateTime dateTime)
	{
		CountdownData data = null;
		countdownDataTable.ForEach(delegate(CountdownData o)
		{
			if (data == null && o.startAt <= dateTime && o.endAt > dateTime)
			{
				data = o;
			}
		});
		return data;
	}
}
