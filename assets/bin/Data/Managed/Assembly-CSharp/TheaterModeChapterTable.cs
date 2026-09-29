// Decompiled with JetBrains decompiler
// Type: TheaterModeChapterTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class TheaterModeChapterTable : MonoBehaviourSingleton<TheaterModeChapterTable>, IDataTable
{
  private UIntKeyTable<TheaterModeChapterTable.TheaterModeChapterData> dataTable;

  public bool isLoading { get; private set; }

  protected override void Awake()
  {
    base.Awake();
    this.LoadTable();
  }

  private void LoadTable()
  {
    this.isLoading = true;
    MonoBehaviourSingleton<DataTableManager>.I.RequestLoadTable(nameof (TheaterModeChapterTable), (IDataTable) this, (System.Action) (() => this.isLoading = false));
  }

  public void CreateTable(string csv)
  {
    this.dataTable = TableUtility.CreateUIntKeyTable<TheaterModeChapterTable.TheaterModeChapterData>(csv, new TableUtility.CallBackUIntKeyReadCSV<TheaterModeChapterTable.TheaterModeChapterData>(TheaterModeChapterTable.TheaterModeChapterData.CB), "chapter_id,chapter_name,order,is_main,banner_id");
    this.dataTable.TrimExcess();
  }

  public void AllChapterData(
    Action<TheaterModeChapterTable.TheaterModeChapterData> call_back)
  {
    if (this.dataTable == null || call_back == null)
      return;
    this.dataTable.ForEach((Action<TheaterModeChapterTable.TheaterModeChapterData>) (data => call_back(data)));
  }

  public TheaterModeChapterTable.TheaterModeChapterData GetData(uint chapter_id)
  {
    return this.dataTable.Get(chapter_id);
  }

  public List<TheaterModeChapterTable.TheaterModeChapterData> GetPickedData(List<uint> chapter_ids)
  {
    List<TheaterModeChapterTable.TheaterModeChapterData> list = new List<TheaterModeChapterTable.TheaterModeChapterData>(this.dataTable.GetCount());
    this.dataTable.ForEach((Action<TheaterModeChapterTable.TheaterModeChapterData>) (data =>
    {
      if (!chapter_ids.Contains(data.chapter_id))
        return;
      list.Add(data);
    }));
    return list;
  }

  public int GetCount() => this.dataTable.GetCount();

  [Serializable]
  public class TheaterModeChapterData
  {
    public uint chapter_id;
    public string chapter_name;
    public int order;
    public int is_main;
    public int banner_id;
    public const string NT = "chapter_id,chapter_name,order,is_main,banner_id";

    public static bool CB(
      CSVReader csv,
      TheaterModeChapterTable.TheaterModeChapterData data,
      ref uint key1)
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
      return $"{string.Empty}{(object) this.chapter_id},{this.chapter_name},{(object) this.order},{(object) this.is_main},{(object) this.banner_id}";
    }
  }
}
