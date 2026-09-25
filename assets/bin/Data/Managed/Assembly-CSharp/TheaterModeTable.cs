// Decompiled with JetBrains decompiler
// Type: TheaterModeTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
public class TheaterModeTable : MonoBehaviourSingleton<TheaterModeTable>, IDataTable
{
  private UIntKeyTable<TheaterModeTable.TheaterModeData> dataTable;

  public bool isLoading { get; private set; }

  protected override void Awake()
  {
    base.Awake();
    this.LoadTable();
  }

  private void LoadTable()
  {
    this.isLoading = true;
    MonoBehaviourSingleton<DataTableManager>.I.RequestLoadTable(nameof (TheaterModeTable), (IDataTable) this, (System.Action) (() => this.isLoading = false));
  }

  public void CreateTable(string csv)
  {
    this.dataTable = TableUtility.CreateUIntKeyTable<TheaterModeTable.TheaterModeData>(csv, new TableUtility.CallBackUIntKeyReadCSV<TheaterModeTable.TheaterModeData>(TheaterModeTable.TheaterModeData.CB), "story_id,title,chapter_id,order,script_id,state_id");
    this.dataTable.TrimExcess();
  }

  public void AllTheaterData(Action<TheaterModeTable.TheaterModeData> call_back)
  {
    if (this.dataTable == null || call_back == null)
      return;
    this.dataTable.ForEach((Action<TheaterModeTable.TheaterModeData>) (data => call_back(data)));
  }

  public void AllTheaterDataAsc(Action<TheaterModeTable.TheaterModeData> call_back)
  {
    if (this.dataTable == null || call_back == null)
      return;
    this.dataTable.ForEach((Action<TheaterModeTable.TheaterModeData>) (data => call_back(data)));
  }

  public void AllTheaterDataDesc(Action<TheaterModeTable.TheaterModeData> call_back)
  {
    if (this.dataTable == null || call_back == null)
      return;
    this.dataTable.ForEachDesc((Action<TheaterModeTable.TheaterModeData>) (data => call_back(data)));
  }

  public List<TheaterModeTable.TheaterModeData> GetTableFromOKDic(Dictionary<int, int> ok_dic)
  {
    List<TheaterModeTable.TheaterModeData> list = new List<TheaterModeTable.TheaterModeData>();
    this.dataTable.ForEach((Action<TheaterModeTable.TheaterModeData>) (data =>
    {
      if (ok_dic[data.script_id] < 1)
        return;
      list.Add(data);
    }));
    return list;
  }

  public List<TheaterModeTable.TheaterModeData> GetTableFromChapter(int chapter_id)
  {
    List<TheaterModeTable.TheaterModeData> list = new List<TheaterModeTable.TheaterModeData>();
    this.dataTable.ForEach((Action<TheaterModeTable.TheaterModeData>) (data =>
    {
      if (data.chapter_id != chapter_id)
        return;
      list.Add(data);
    }));
    return list;
  }

  [Serializable]
  public class TheaterModeData
  {
    public uint story_id;
    public string title;
    public int chapter_id;
    public int order;
    public int script_id;
    public int state_id;
    public const string NT = "story_id,title,chapter_id,order,script_id,state_id";

    public static bool CB(CSVReader csv, TheaterModeTable.TheaterModeData data, ref uint key1)
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
      stringBuilder.AppendFormat("{0}", (object) this.story_id);
      stringBuilder.AppendFormat(",{0}", (object) this.title);
      stringBuilder.AppendFormat(",{0}", (object) this.chapter_id);
      stringBuilder.AppendFormat(",{0}", (object) this.order);
      stringBuilder.AppendFormat(",{0}", (object) this.script_id);
      stringBuilder.AppendFormat(",{0}", (object) this.state_id);
      return stringBuilder.ToString();
    }

    public enum STATE_ID
    {
      BLACK_LIST,
      MAIN_STORY,
      EVENT_STORY,
    }
  }
}
