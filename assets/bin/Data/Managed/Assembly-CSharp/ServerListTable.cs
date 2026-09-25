// Decompiled with JetBrains decompiler
// Type: ServerListTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class ServerListTable : Singleton<ServerListTable>, IDataTable
{
  public const string PRD_KEY = "prd";
  public const string BETA_KEY = "beta";
  public const string STG_KEY = "stg";
  private List<ServerListTable.ServerData> listActiveServer;

  public UIntKeyTable<ServerListTable.ServerData> dataTable { get; private set; }

  public void CreateTable(string csv_text)
  {
    this.dataTable = TableUtility.CreateUIntKeyTable<ServerListTable.ServerData>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<ServerListTable.ServerData>(ServerListTable.ServerData.cb), "id,name,url,status,environment,note");
    this.dataTable.TrimExcess();
  }

  public ServerListTable.ServerData GetData(uint id)
  {
    return this.dataTable == null ? (ServerListTable.ServerData) null : this.dataTable.Get(id);
  }

  public ServerListTable.ServerData GetActiveServerDataByUrl(string serverUrl)
  {
    foreach (ServerListTable.ServerData activeServer in this.GetActiveServerList())
    {
      if (activeServer.url.Equals(serverUrl))
        return activeServer;
    }
    return (ServerListTable.ServerData) null;
  }

  public ServerListTable.ServerData GetActiveServerDataById(uint serverId)
  {
    foreach (ServerListTable.ServerData activeServer in this.GetActiveServerList())
    {
      if ((int) activeServer.id == (int) serverId)
        return activeServer;
    }
    return (ServerListTable.ServerData) null;
  }

  public List<ServerListTable.ServerData> GetActiveServerList()
  {
    string environment = "prd";
    if (this.listActiveServer == null)
    {
      this.listActiveServer = new List<ServerListTable.ServerData>();
      this.dataTable.ForEach((Action<ServerListTable.ServerData>) (serverData =>
      {
        if (serverData.status != 1U || !serverData.environment.Equals(environment))
          return;
        this.listActiveServer.Add(serverData);
      }));
      this.listActiveServer.Reverse();
    }
    return this.listActiveServer;
  }

  public ServerListTable.ServerData GetNewestServer() => this.GetActiveServerList()[0];

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

    public static bool cb(CSVReader csv, ServerListTable.ServerData data, ref uint key)
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
}
