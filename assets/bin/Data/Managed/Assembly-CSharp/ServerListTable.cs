using System;
using System.Collections.Generic;

public class ServerListTable : Singleton<ServerListTable>, IDataTable
{
	[Serializable]
	public class ServerData
	{
		public uint id;

		public string name = "";

		public string url = "";

		public uint status;

		public string environment;

		public string note;

		public const string NT = "id,name,url,status,environment,note";

		public static bool cb(CSVReader csv, ServerData data, ref uint key)
		{
			data.id = key;
			csv.Pop(ref data.name);
			csv.Pop(ref data.url);
			csv.Pop(ref data.status);
			csv.Pop(ref data.environment);
			csv.Pop(ref data.note);
			return true;
		}
	}

	public const string PRD_KEY = "prd";

	public const string BETA_KEY = "beta";

	public const string STG_KEY = "stg";

	private List<ServerData> listActiveServer;

	public UIntKeyTable<ServerData> dataTable
	{
		get;
		private set;
	}

	public void CreateTable(string csv_text)
	{
		dataTable = TableUtility.CreateUIntKeyTable<ServerData>(csv_text, ServerData.cb, "id,name,url,status,environment,note");
		dataTable.TrimExcess();
	}

	public ServerData GetData(uint id)
	{
		if (dataTable == null)
		{
			return null;
		}
		return dataTable.Get(id);
	}

	public ServerData GetActiveServerData(string serverUrl)
	{
		foreach (ServerData activeServer in GetActiveServerList())
		{
			if (activeServer.url.Equals(serverUrl))
			{
				return activeServer;
			}
		}
		return null;
	}

	public List<ServerData> GetActiveServerList()
	{
		string environment = "prd";
		if (listActiveServer == null)
		{
			listActiveServer = new List<ServerData>();
			dataTable.ForEach(delegate(ServerData serverData)
			{
				if (serverData.status == 1 && serverData.environment.Equals(environment))
				{
					listActiveServer.Add(serverData);
				}
			});
			listActiveServer.Reverse();
		}
		return listActiveServer;
	}

	public ServerData GetNewestServer()
	{
		return GetActiveServerList()[0];
	}
}
