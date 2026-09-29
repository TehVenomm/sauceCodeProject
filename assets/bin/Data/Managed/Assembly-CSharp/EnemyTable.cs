// Decompiled with JetBrains decompiler
// Type: EnemyTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class EnemyTable : Singleton<EnemyTable>, IDataTable
{
  private UIntKeyTable<EnemyTable.EnemyData> enemyTable;

  public void CreateTable(string csv_text)
  {
    this.enemyTable = TableUtility.CreateUIntKeyTable<EnemyTable.EnemyData>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<EnemyTable.EnemyData>(EnemyTable.EnemyData.cb), "id,enemyCollectionId,appVer,name,type,enemySpecies,lv,actionId,iconID,modelID,animID,modelScale,baseEffectName,baseEffectNode,active,element,weakElement,hp,atk,convertRegionKey,aimMarkerRate,effectEnemyKey,personality,growId,weatherChangeEffect,exActionId,exActionCondition,exActionConditionValue");
  }

  public void CreateTable(string csv_text, TableUtility.Progress progress)
  {
    this.enemyTable = TableUtility.CreateUIntKeyTable<EnemyTable.EnemyData>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<EnemyTable.EnemyData>(EnemyTable.EnemyData.cb), "id,enemyCollectionId,appVer,name,type,enemySpecies,lv,actionId,iconID,modelID,animID,modelScale,baseEffectName,baseEffectNode,active,element,weakElement,hp,atk,convertRegionKey,aimMarkerRate,effectEnemyKey,personality,growId,weatherChangeEffect,exActionId,exActionCondition,exActionConditionValue", progress);
    this.enemyTable.TrimExcess();
  }

  public void AddTable(string csv_text)
  {
    TableUtility.AddUIntKeyTable<EnemyTable.EnemyData>(this.enemyTable, csv_text, new TableUtility.CallBackUIntKeyReadCSV<EnemyTable.EnemyData>(EnemyTable.EnemyData.cb), "id,enemyCollectionId,appVer,name,type,enemySpecies,lv,actionId,iconID,modelID,animID,modelScale,baseEffectName,baseEffectNode,active,element,weakElement,hp,atk,convertRegionKey,aimMarkerRate,effectEnemyKey,personality,growId,weatherChangeEffect,exActionId,exActionCondition,exActionConditionValue");
  }

  public EnemyTable.EnemyData GetEnemyData(uint id)
  {
    if (id == 0U || this.enemyTable == null)
      return (EnemyTable.EnemyData) null;
    EnemyTable.EnemyData enemyData = this.enemyTable.Get(id);
    if (enemyData == null)
    {
      Log.TableError((object) this, id);
      enemyData = new EnemyTable.EnemyData();
      enemyData.name = Log.NON_DATA_NAME;
    }
    return enemyData;
  }

  public List<EnemyTable.EnemyData> GetEnemyDataByEnemyCollectionId(uint collectionId)
  {
    List<EnemyTable.EnemyData> searchData = new List<EnemyTable.EnemyData>();
    this.enemyTable.ForEach((Action<EnemyTable.EnemyData>) (data =>
    {
      if ((int) data.enemyCollectionId != (int) collectionId)
        return;
      searchData.Add(data);
    }));
    if (searchData.Count == 0)
      Log.Error("Not found enemyData by collection id {0}", (object) collectionId);
    return searchData;
  }

  public string GetEnemyName(uint id)
  {
    EnemyTable.EnemyData enemyData = this.GetEnemyData(id);
    return enemyData == null ? string.Empty : enemyData.name;
  }

  public bool IsAvailable() => this.enemyTable != null;

  public List<EnemyTable.EnemyData> GetAllEnemyDatas()
  {
    List<EnemyTable.EnemyData> datas = new List<EnemyTable.EnemyData>();
    this.enemyTable.ForEach((Action<EnemyTable.EnemyData>) (data => datas.Add(data)));
    return datas;
  }

  public class EnemyData
  {
    public uint id;
    public uint enemyCollectionId;
    public string appVer;
    public string name;
    public ENEMY_TYPE type;
    public int enemySpecies;
    public XorInt level;
    public int actionId;
    public int iconId;
    public int modelId;
    public int animId;
    public float modelScale;
    public string baseEffectName;
    public string baseEffectNode;
    public bool active;
    public ELEMENT_TYPE element;
    public ELEMENT_TYPE weakElement;
    public XorFloat hpRate = (XorFloat) 0.0f;
    public XorFloat atkRate = (XorFloat) 0.0f;
    public string convertRegionKey;
    public float aimMarkerRate;
    public string effectEnemyKey;
    public uint personality;
    public uint growId;
    public string weatherChangeEffect;
    public const string NT = "id,enemyCollectionId,appVer,name,type,enemySpecies,lv,actionId,iconID,modelID,animID,modelScale,baseEffectName,baseEffectNode,active,element,weakElement,hp,atk,convertRegionKey,aimMarkerRate,effectEnemyKey,personality,growId,weatherChangeEffect,exActionId,exActionCondition,exActionConditionValue";

    public bool IsEnableNowApplicationVersion() => AppMain.CheckApplicationVersion(this.appVer);

    public static bool cb(CSVReader csv_reader, EnemyTable.EnemyData data, ref uint key)
    {
      data.id = key;
      csv_reader.Pop(ref data.enemyCollectionId);
      csv_reader.Pop(ref data.appVer);
      csv_reader.Pop(ref data.name);
      csv_reader.PopEnum<ENEMY_TYPE>(ref data.type, ENEMY_TYPE.NONE);
      csv_reader.Pop(ref data.enemySpecies);
      csv_reader.Pop(ref data.level);
      csv_reader.Pop(ref data.actionId);
      csv_reader.Pop(ref data.iconId);
      csv_reader.Pop(ref data.modelId);
      csv_reader.Pop(ref data.animId);
      csv_reader.Pop(ref data.modelScale);
      csv_reader.Pop(ref data.baseEffectName);
      csv_reader.Pop(ref data.baseEffectNode);
      csv_reader.Pop(ref data.active);
      string empty = string.Empty;
      csv_reader.Pop(ref empty);
      data.element = empty.Length <= 1 ? ELEMENT_TYPE.MAX : (ELEMENT_TYPE) Enum.Parse(typeof (ELEMENT_TYPE), empty);
      csv_reader.Pop(ref empty);
      data.weakElement = empty.Length <= 1 ? ELEMENT_TYPE.MAX : (ELEMENT_TYPE) Enum.Parse(typeof (ELEMENT_TYPE), empty);
      csv_reader.Pop(ref data.hpRate);
      EnemyTable.EnemyData enemyData1 = data;
      enemyData1.hpRate = (XorFloat) ((float) enemyData1.hpRate * 0.01f);
      csv_reader.Pop(ref data.atkRate);
      EnemyTable.EnemyData enemyData2 = data;
      enemyData2.atkRate = (XorFloat) ((float) enemyData2.atkRate * 0.01f);
      csv_reader.Pop(ref data.convertRegionKey);
      data.aimMarkerRate = 1f;
      csv_reader.Pop(ref data.aimMarkerRate);
      csv_reader.Pop(ref data.effectEnemyKey);
      csv_reader.Pop(ref data.personality);
      csv_reader.Pop(ref data.growId);
      csv_reader.Pop(ref data.weatherChangeEffect);
      return true;
    }
  }
}
