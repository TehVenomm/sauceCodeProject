// Decompiled with JetBrains decompiler
// Type: AudioSettingTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class AudioSettingTable : Singleton<AudioSettingTable>, IDataTable
{
  public UIntKeyTable<AudioSettingTable.Data> audioSettingTable;

  public void CreateTableFromInternal(string encrypted_csv_text)
  {
    this.CreateTable(DataTableManager.Decrypt(encrypted_csv_text));
  }

  public void CreateTable(string csv_text)
  {
    this.audioSettingTable = TableUtility.CreateUIntKeyTable<AudioSettingTable.Data>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<AudioSettingTable.Data>(AudioSettingTable.Data.cb), "id,name,minDistance,maxDistance");
    this.audioSettingTable.TrimExcess();
  }

  public AudioSettingTable.Data GetData(uint id)
  {
    return this.audioSettingTable == null ? (AudioSettingTable.Data) null : this.audioSettingTable.Get(id);
  }

  public string GetSnapshotName(uint id)
  {
    if (this.audioSettingTable == null)
      return string.Empty;
    AudioSettingTable.Data data = this.audioSettingTable.Get(id);
    return data == null ? string.Empty : data.name;
  }

  [Serializable]
  public class Data
  {
    public uint id;
    public string name;
    public float minDistance;
    public float maxDistance;
    public const string NT = "id,name,minDistance,maxDistance";

    public static bool cb(CSVReader csvReader, AudioSettingTable.Data data, ref uint key)
    {
      data.id = key;
      csvReader.Pop(ref data.name);
      csvReader.Pop(ref data.minDistance);
      csvReader.Pop(ref data.maxDistance);
      return true;
    }
  }
}
