// Decompiled with JetBrains decompiler
// Type: EnemyCollectionTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
[Obsolete]
public class EnemyCollectionTable : Singleton<EnemyCollectionTable>, IDataTable
{
  private UIntKeyTable<EnemyCollectionTable.EnemyCollectionData> enemyCollectionTable;

  public void CreateTable(string csv_text)
  {
    this.enemyCollectionTable = TableUtility.CreateUIntKeyTable<EnemyCollectionTable.EnemyCollectionData>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<EnemyCollectionTable.EnemyCollectionData>(EnemyCollectionTable.EnemyCollectionData.cb), "id,enemySpeciesId,name,regionId,collectionType,flavorText");
    this.enemyCollectionTable.TrimExcess();
  }

  public void AddTable(string csv_text)
  {
    TableUtility.AddUIntKeyTable<EnemyCollectionTable.EnemyCollectionData>(this.enemyCollectionTable, csv_text, new TableUtility.CallBackUIntKeyReadCSV<EnemyCollectionTable.EnemyCollectionData>(EnemyCollectionTable.EnemyCollectionData.cb), "id,enemySpeciesId,name,regionId,collectionType,flavorText");
  }

  public EnemyCollectionTable.EnemyCollectionData GetEnemyCollectionData(uint id)
  {
    if (id == 0U || this.enemyCollectionTable == null)
      return (EnemyCollectionTable.EnemyCollectionData) null;
    EnemyCollectionTable.EnemyCollectionData enemyCollectionData = this.enemyCollectionTable.Get(id);
    if (enemyCollectionData == null)
    {
      Log.TableError((object) this, id);
      enemyCollectionData = new EnemyCollectionTable.EnemyCollectionData();
      enemyCollectionData.name = Log.NON_DATA_NAME;
    }
    return enemyCollectionData;
  }

  public List<EnemyCollectionTable.EnemyCollectionData> GetEnemyCollectionDataByRegion(uint regionId)
  {
    List<EnemyCollectionTable.EnemyCollectionData> searchData = new List<EnemyCollectionTable.EnemyCollectionData>();
    this.enemyCollectionTable.ForEach((Action<EnemyCollectionTable.EnemyCollectionData>) (data =>
    {
      if ((int) data.regionId != (int) regionId)
        return;
      searchData.Add(data);
    }));
    return searchData;
  }

  public class EnemyCollectionData
  {
    public uint id;
    public uint enemySpeciesId;
    public string name;
    public uint regionId;
    public COLLECTION_TYPE collectionType;
    public string flavorText;
    public const string NT = "id,enemySpeciesId,name,regionId,collectionType,flavorText";

    public static bool cb(
      CSVReader csv_reader,
      EnemyCollectionTable.EnemyCollectionData data,
      ref uint key)
    {
      data.id = key;
      csv_reader.Pop(ref data.enemySpeciesId);
      csv_reader.Pop(ref data.name);
      csv_reader.Pop(ref data.regionId);
      csv_reader.Pop<COLLECTION_TYPE>(ref data.collectionType);
      csv_reader.Pop(ref data.flavorText);
      return true;
    }
  }
}
