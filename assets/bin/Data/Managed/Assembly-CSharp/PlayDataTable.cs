// Decompiled with JetBrains decompiler
// Type: PlayDataTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;

#nullable disable
public class PlayDataTable : Singleton<PlayDataTable>, IDataTable
{
  private UIntKeyTable<PlayDataTable.PlayData> playDataTable;

  public void CreateTable(string csv_text)
  {
    this.playDataTable = TableUtility.CreateUIntKeyTable<PlayDataTable.PlayData>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<PlayDataTable.PlayData>(PlayDataTable.PlayData.cb), "id,type,subType,name,format,orderNo,startAt");
    this.playDataTable.TrimExcess();
  }

  public PlayDataTable.PlayData[] GetSortedPlayData(AchievementCounter[] dataList)
  {
    DateTime now = TimeManager.GetNow();
    List<PlayDataTable.PlayData> sortData = new List<PlayDataTable.PlayData>();
    this.playDataTable.ForEach((Action<PlayDataTable.PlayData>) (o =>
    {
      if (!(o.startAt <= now))
        return;
      AchievementCounter achievementCounter = dataList.Find<AchievementCounter>((Predicate<AchievementCounter>) (x => x.type == o.type && x.subType == o.subType));
      string s = achievementCounter != null ? achievementCounter.count : "0";
      o.count = int.Parse(s);
      sortData.Add(o);
    }));
    sortData.Sort((Comparison<PlayDataTable.PlayData>) ((a, b) => a.orderNo - b.orderNo));
    return sortData.ToArray();
  }

  public class PlayData
  {
    public int id;
    public int type;
    public int subType;
    public string name;
    public string format;
    public int orderNo;
    public DateTime startAt;
    public int count;
    public const string NT = "id,type,subType,name,format,orderNo,startAt";

    public static bool cb(CSVReader csv_reader, PlayDataTable.PlayData data, ref uint key)
    {
      data.id = (int) key;
      csv_reader.Pop(ref data.type);
      csv_reader.Pop(ref data.subType);
      csv_reader.Pop(ref data.name);
      csv_reader.Pop(ref data.format);
      csv_reader.Pop(ref data.orderNo);
      string empty = string.Empty;
      csv_reader.Pop(ref empty);
      if (!string.IsNullOrEmpty(empty))
        DateTime.TryParse(empty, out data.startAt);
      return true;
    }
  }
}
