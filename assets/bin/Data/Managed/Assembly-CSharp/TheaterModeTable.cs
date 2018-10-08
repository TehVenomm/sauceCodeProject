using System;
using System.Collections.Generic;
using System.Text;

public class TheaterModeTable : MonoBehaviourSingleton<TheaterModeTable>, IDataTable
{
	[Serializable]
	public class TheaterModeData
	{
		public enum STATE_ID
		{
			BLACK_LIST,
			MAIN_STORY,
			EVENT_STORY
		}

		public const string NT = "story_id,title,chapter_id,order,script_id,state_id";

		public uint story_id;

		public string title;

		public int chapter_id;

		public int order;

		public int script_id;

		public int state_id;

		public static bool CB(CSVReader csv, TheaterModeData data, ref uint key1)
		{
			data.story_id = key1;
			csv.Pop(ref data.title);
			csv.Pop(ref data.chapter_id);
			csv.Pop(ref data.order);
			csv.Pop(ref data.script_id);
			csv.Pop(ref data.state_id);
			return true;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0}", story_id);
			stringBuilder.AppendFormat(",{0}", title);
			stringBuilder.AppendFormat(",{0}", chapter_id);
			stringBuilder.AppendFormat(",{0}", order);
			stringBuilder.AppendFormat(",{0}", script_id);
			stringBuilder.AppendFormat(",{0}", state_id);
			return stringBuilder.ToString();
		}
	}

	private UIntKeyTable<TheaterModeData> dataTable;

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
		MonoBehaviourSingleton<DataTableManager>.I.RequestLoadTable("TheaterModeTable", this, delegate
		{
			isLoading = false;
		}, false);
	}

	public void CreateTable(string csv)
	{
		dataTable = TableUtility.CreateUIntKeyTable<TheaterModeData>(csv, TheaterModeData.CB, "story_id,title,chapter_id,order,script_id,state_id", null);
		dataTable.TrimExcess();
	}

	public void AllTheaterData(Action<TheaterModeData> call_back)
	{
		if (dataTable != null && call_back != null)
		{
			dataTable.ForEach(delegate(TheaterModeData data)
			{
				call_back(data);
			});
		}
	}

	public void AllTheaterDataAsc(Action<TheaterModeData> call_back)
	{
		if (dataTable != null && call_back != null)
		{
			dataTable.ForEach(delegate(TheaterModeData data)
			{
				call_back(data);
			});
		}
	}

	public void AllTheaterDataDesc(Action<TheaterModeData> call_back)
	{
		if (dataTable != null && call_back != null)
		{
			dataTable.ForEachDesc(delegate(TheaterModeData data)
			{
				call_back(data);
			});
		}
	}

	public List<TheaterModeData> GetTableFromOKDic(Dictionary<int, int> ok_dic)
	{
		List<TheaterModeData> list = new List<TheaterModeData>();
		dataTable.ForEach(delegate(TheaterModeData data)
		{
			if (ok_dic[data.script_id] >= 1)
			{
				list.Add(data);
			}
		});
		return list;
	}

	public List<TheaterModeData> GetTableFromChapter(int chapter_id)
	{
		List<TheaterModeData> list = new List<TheaterModeData>();
		dataTable.ForEach(delegate(TheaterModeData data)
		{
			if (data.chapter_id == chapter_id)
			{
				list.Add(data);
			}
		});
		return list;
	}
}
