// Decompiled with JetBrains decompiler
// Type: SETable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class SETable : Singleton<SETable>, IDataTable
{
  public UIntKeyTable<SETable.Data> seTable;

  public void CreateTableFromInternal(string encrypted_csv_text)
  {
    this.CreateTable(DataTableManager.Decrypt(encrypted_csv_text));
  }

  public void CreateTable(string csv_text)
  {
    this.seTable = TableUtility.CreateUIntKeyTable<SETable.Data>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<SETable.Data>(SETable.Data.cb), "id,priority,intervalLimit,volumeOffset,randomPitch,dopplerLevel,minDistance,maxDistance,limitNum,cullingType");
    this.seTable.TrimExcess();
  }

  public SETable.Data GetSeData(uint id)
  {
    return this.seTable == null ? (SETable.Data) null : this.seTable.Get(id);
  }

  [Serializable]
  public class Data
  {
    public uint id;
    public uint priority;
    public float intervalLimit;
    public float volumeOffset;
    public float volumeScale;
    public float dopplerLevel;
    public float minDistance;
    public float maxDistance;
    public float randomPitch;
    public int limitNum;
    public uint cullingType;
    public AudioControlGroup.CullingTypes CullingType;
    public const string NT = "id,priority,intervalLimit,volumeOffset,randomPitch,dopplerLevel,minDistance,maxDistance,limitNum,cullingType";

    public static bool cb(CSVReader csvReader, SETable.Data data, ref uint key)
    {
      data.id = key;
      csvReader.Pop(ref data.priority);
      csvReader.Pop(ref data.intervalLimit);
      data.intervalLimit /= 1000f;
      csvReader.Pop(ref data.volumeOffset);
      data.volumeOffset = Mathf.Clamp(data.volumeOffset, -80f, 0.0f);
      data.volumeScale = Utility.DecibelToVolume(data.volumeOffset);
      csvReader.Pop(ref data.randomPitch);
      csvReader.Pop(ref data.dopplerLevel);
      data.dopplerLevel = Mathf.Clamp(data.dopplerLevel, 0.0f, 5f);
      csvReader.Pop(ref data.minDistance);
      csvReader.Pop(ref data.maxDistance);
      csvReader.Pop(ref data.limitNum);
      csvReader.Pop(ref data.cullingType);
      data.CullingType = 4U <= data.cullingType ? AudioControlGroup.CullingTypes.NONE : (AudioControlGroup.CullingTypes) data.cullingType;
      return true;
    }
  }
}
