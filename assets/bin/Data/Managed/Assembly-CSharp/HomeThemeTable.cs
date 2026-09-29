// Decompiled with JetBrains decompiler
// Type: HomeThemeTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class HomeThemeTable : Singleton<HomeThemeTable>, IDataTable
{
  private UIntKeyTable<HomeThemeTable.HomeThemeData> homeThemeDataTable;
  private string currentHomeTheme;

  public string CurrentHomeTheme => this.currentHomeTheme;

  public void CreateTable(string csv_text)
  {
    this.homeThemeDataTable = TableUtility.CreateUIntKeyTable<HomeThemeTable.HomeThemeData>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<HomeThemeTable.HomeThemeData>(HomeThemeTable.HomeThemeData.cb), "id,name,sceneName,npc0MdlID,npc1MdlID,npc2MdlID,npc6MdlID,bgmID,stumblePercent,startAt,endAt");
    this.homeThemeDataTable.TrimExcess();
  }

  public HomeThemeTable.HomeThemeData GetHomeThemeData(DateTime dateTime)
  {
    HomeThemeTable.HomeThemeData data = (HomeThemeTable.HomeThemeData) null;
    this.homeThemeDataTable.ForEach((Action<HomeThemeTable.HomeThemeData>) (o =>
    {
      if (data != null || !(o.startAt <= dateTime) || !(o.endAt > dateTime))
        return;
      data = o;
    }));
    if (data == null)
      this.homeThemeDataTable.ForEach((Action<HomeThemeTable.HomeThemeData>) (o =>
      {
        if (!(o.name == "NORMAL"))
          return;
        data = o;
      }));
    return data;
  }

  public HomeThemeTable.HomeThemeData GetHomeThemeData(string themeName)
  {
    HomeThemeTable.HomeThemeData data = (HomeThemeTable.HomeThemeData) null;
    this.homeThemeDataTable.ForEach((Action<HomeThemeTable.HomeThemeData>) (o =>
    {
      if (data != null || !(o.name == themeName))
        return;
      data = o;
    }));
    return data;
  }

  public int GetNpcModelID(HomeThemeTable.HomeThemeData data, int npcId)
  {
    switch (npcId)
    {
      case 0:
        return data.npc0MdlID;
      case 1:
        return data.npc1MdlID;
      case 2:
        return data.npc2MdlID;
      case 6:
        return data.npc6MdlID;
      default:
        return -1;
    }
  }

  public void SetCurrentHomeThemeName(string name) => this.currentHomeTheme = name;

  public class HomeThemeData
  {
    public int id;
    public string name;
    public string sceneName;
    public int npc0MdlID = -1;
    public int npc1MdlID = -1;
    public int npc2MdlID = -1;
    public int npc6MdlID = -1;
    public int bgmId = -1;
    public int stumblePercent;
    public DateTime startAt;
    public DateTime endAt;
    public const string NT = "id,name,sceneName,npc0MdlID,npc1MdlID,npc2MdlID,npc6MdlID,bgmID,stumblePercent,startAt,endAt";

    public static bool cb(CSVReader csv_reader, HomeThemeTable.HomeThemeData data, ref uint key)
    {
      data.id = (int) key;
      csv_reader.Pop(ref data.name);
      csv_reader.Pop(ref data.sceneName);
      csv_reader.Pop(ref data.npc0MdlID);
      csv_reader.Pop(ref data.npc1MdlID);
      csv_reader.Pop(ref data.npc2MdlID);
      csv_reader.Pop(ref data.npc6MdlID);
      csv_reader.Pop(ref data.bgmId);
      csv_reader.Pop(ref data.stumblePercent);
      string empty1 = string.Empty;
      csv_reader.Pop(ref empty1);
      if (!string.IsNullOrEmpty(empty1))
        DateTime.TryParse(empty1, out data.startAt);
      string empty2 = string.Empty;
      csv_reader.Pop(ref empty2);
      if (!string.IsNullOrEmpty(empty2))
        DateTime.TryParse(empty2, out data.endAt);
      return true;
    }
  }
}
