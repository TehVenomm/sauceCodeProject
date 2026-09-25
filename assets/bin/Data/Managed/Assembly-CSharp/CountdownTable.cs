// Decompiled with JetBrains decompiler
// Type: CountdownTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class CountdownTable : Singleton<CountdownTable>, IDataTable
{
  private UIntKeyTable<CountdownTable.CountdownData> countdownDataTable;

  public void CreateTable(string csv_text)
  {
    this.countdownDataTable = TableUtility.CreateUIntKeyTable<CountdownTable.CountdownData>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<CountdownTable.CountdownData>(CountdownTable.CountdownData.cb), "id,imageID,startAt,endAt");
    this.countdownDataTable.TrimExcess();
  }

  public CountdownTable.CountdownData GetCountdownData(DateTime dateTime)
  {
    CountdownTable.CountdownData data = (CountdownTable.CountdownData) null;
    this.countdownDataTable.ForEach((Action<CountdownTable.CountdownData>) (o =>
    {
      if (data != null || !(o.startAt <= dateTime) || !(o.endAt > dateTime))
        return;
      data = o;
    }));
    return data;
  }

  public class CountdownData
  {
    public int id;
    public int imageID;
    public DateTime startAt;
    public DateTime endAt;
    public const string NT = "id,imageID,startAt,endAt";

    public static bool cb(CSVReader csv_reader, CountdownTable.CountdownData data, ref uint key)
    {
      data.id = (int) key;
      csv_reader.Pop(ref data.imageID);
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
