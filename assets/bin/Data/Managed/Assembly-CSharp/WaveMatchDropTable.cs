// Decompiled with JetBrains decompiler
// Type: WaveMatchDropTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class WaveMatchDropTable : Singleton<WaveMatchDropTable>, IDataTable
{
  private UIntKeyTable<WaveMatchDropTable.WaveMatchDropData> dataTable;

  public void CreateTable(string text)
  {
    this.dataTable = TableUtility.CreateUIntKeyTable<WaveMatchDropTable.WaveMatchDropData>(text, new TableUtility.CallBackUIntKeyReadCSV<WaveMatchDropTable.WaveMatchDropData>(WaveMatchDropTable.WaveMatchDropData.cb), "id,model,type,calcType,value,buffTableIds,getEffect,getSE");
    this.dataTable.TrimExcess();
  }

  public WaveMatchDropTable.WaveMatchDropData GetData(uint id)
  {
    return this.dataTable == null ? (WaveMatchDropTable.WaveMatchDropData) null : this.dataTable.Get(id);
  }

  public List<WaveMatchDropTable.WaveMatchDropData> GetAllData()
  {
    if (this.dataTable == null)
      return (List<WaveMatchDropTable.WaveMatchDropData>) null;
    List<WaveMatchDropTable.WaveMatchDropData> list = new List<WaveMatchDropTable.WaveMatchDropData>();
    this.dataTable.ForEach((Action<WaveMatchDropTable.WaveMatchDropData>) (data => list.Add(data)));
    return list;
  }

  public class WaveMatchDropData
  {
    public uint id;
    public string model;
    public WAVEMATCH_ITEM_TYPE type;
    public CALCULATE_TYPE calcType;
    public int value;
    public List<uint> buffTableIds = new List<uint>();
    public string getEffect = "";
    public int getSE;
    public const string NT = "id,model,type,calcType,value,buffTableIds,getEffect,getSE";

    public static bool cb(
      CSVReader csv_reader,
      WaveMatchDropTable.WaveMatchDropData data,
      ref uint key)
    {
      data.id = key;
      csv_reader.Pop(ref data.model);
      csv_reader.Pop<WAVEMATCH_ITEM_TYPE>(ref data.type);
      csv_reader.Pop<CALCULATE_TYPE>(ref data.calcType);
      csv_reader.Pop(ref data.value);
      string str = "";
      csv_reader.Pop(ref str);
      string[] strArray = str.Split(':');
      data.buffTableIds.Clear();
      int index = 0;
      for (int length = strArray.Length; index < length; ++index)
      {
        uint result = 0;
        if (uint.TryParse(strArray[index], out result))
          data.buffTableIds.Add(result);
      }
      csv_reader.Pop(ref data.getEffect);
      csv_reader.Pop(ref data.getSE);
      return true;
    }
  }
}
