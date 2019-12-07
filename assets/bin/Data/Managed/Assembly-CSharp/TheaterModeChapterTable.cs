using System;
using System.Collections.Generic;

public class TheaterModeChapterTable : MonoBehaviourSingleton<TheaterModeChapterTable>, IDataTable
{
	[Serializable]
	public class TheaterModeChapterData
	{
		public uint chapter_id;

		public string chapter_name;

		public int order;

		public int is_main;

		public int banner_id;

		public const string NT = "chapter_id,chapter_name,order,is_main,banner_id";

		public static bool CB(CSVReader csv, TheaterModeChapterData data, ref uint key1)
		{
			data.chapter_id = key1;
			csv.Pop(ref data.chapter_name);
			csv.Pop(ref data.order);
			csv.Pop(ref data.is_main);
			csv.Pop(ref data.banner_id);
			return true;
		}

		public override string ToString()
		{
			string empty = string.Empty;
			return empty + chapter_id + "," + chapter_name + "," + order + "," + is_main + "," + banner_id;
		}
	}

	private UIntKeyTable<TheaterModeChapterData> dataTable;

	public bool isLoading
	{
		get;
		private set;
	}

	protected override void Awake()
	{
		base.Awake();
		LoadTable();
	}

	private void LoadTable()
	{
		isLoading = true;
		MonoBehaviourSingleton<DataTableManager>.I.RequestLoadTable("TheaterModeChapterTable", this, delegate
		{
			isLoading = false;
		});
	}

	public void CreateTable(string csv)
	{
		dataTable = TableUtility.CreateUIntKeyTable<TheaterModeChapterData>(csv, TheaterModeChapterData.CB, "chapter_id,chapter_name,order,is_main,banner_id");
		dataTable.TrimExcess();
	}

	public void AllChapterData(Action<TheaterModeChapterData> call_back)
	{
		if (dataTable != null && call_back != null)
		{
			dataTable.ForEach(delegate(TheaterModeChapterData data)
			{
				call_back(data);
			});
		}
	}

	public TheaterModeChapterData GetData(uint chapter_id)
	{
		return dataTable.Get(chapter_id);
	}

	public List<TheaterModeChapterData> GetPickedData(List<uint> chapter_ids)
	{
		List<TheaterModeChapterData> list = new List<TheaterModeChapterData>(dataTable.GetCount());
		dataTable.ForEach(delegate(TheaterModeChapterData data)
		{
			if (chapter_ids.Contains(data.chapter_id))
			{
				list.Add(data);
			}
		});
		return list;
	}

	public int GetCount()
	{
		return dataTable.GetCount();
	}
}
