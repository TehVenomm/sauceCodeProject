// Decompiled with JetBrains decompiler
// Type: StampTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class StampTable : Singleton<StampTable>, IDataTable
{
  public UIntKeyTable<StampTable.Data> table;

  public void CreateTable(string csv_text)
  {
    this.table = TableUtility.CreateUIntKeyTable<StampTable.Data>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<StampTable.Data>(StampTable.Data.InsertRow), "id,desc,seId,type");
    this.table.TrimExcess();
  }

  public StampTable.Data GetData(uint id)
  {
    return this.table == null ? (StampTable.Data) null : this.table.Get(id);
  }

  public List<StampTable.Data> GetUnlockStamps(UserInfoManager userInfo)
  {
    List<StampTable.Data> data = new List<StampTable.Data>();
    this.table.ForEach((Action<StampTable.Data>) (stampData =>
    {
      if (stampData.type != STAMP_TYPE.COMMON && !userInfo.unlockStampIds.Contains((int) stampData.id))
        return;
      data.Add(stampData);
    }));
    return data;
  }

  [Serializable]
  public class Data
  {
    public const string INDEX_NAMES = "id,desc,seId,type";
    public uint id;
    public string desc;
    public STAMP_TYPE type;
    public int seId;
    public bool hasSE;

    public static bool InsertRow(CSVReader CSVReader, StampTable.Data Data, ref uint key)
    {
      Data.id = key;
      CSVReader.Pop(ref Data.desc);
      CSVReader.Pop(ref Data.seId);
      CSVReader.PopEnum<STAMP_TYPE>(ref Data.type, STAMP_TYPE.NONE);
      Data.hasSE = Data.seId > 0;
      return true;
    }
  }
}
