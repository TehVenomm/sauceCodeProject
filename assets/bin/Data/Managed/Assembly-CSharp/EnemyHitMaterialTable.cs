// Decompiled with JetBrains decompiler
// Type: EnemyHitMaterialTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class EnemyHitMaterialTable : Singleton<EnemyHitMaterialTable>, IDataTable
{
  protected List<string> typeKeyTable = new List<string>();

  public StringKeyTable<EnemyHitMaterialTable.MaterialData> dataTable { get; private set; }

  public void CreateTable(TextAsset stage_table_text_asset)
  {
    this.CreateTable(stage_table_text_asset.text);
  }

  public void CreateTable(string csv)
  {
    if (!Singleton<EnemyHitTypeTable>.IsValid() || Singleton<EnemyHitTypeTable>.I.dataTable == null)
    {
      Log.Error(LOG.INGAME, "EnemyHitMaterialTable::CreateTable() Err ( EnemyHitTypeTable is invalid. )");
    }
    else
    {
      this.typeKeyTable = new List<string>();
      Singleton<EnemyHitTypeTable>.I.dataTable.ForEachKeys((Action<string>) (key =>
      {
        if (key.IndexOf("@") >= 0)
          return;
        this.typeKeyTable.Add(key);
      }));
      string newValue = "";
      int index = 0;
      for (int count = this.typeKeyTable.Count; index < count; ++index)
      {
        newValue = $"{newValue}se_id_{this.typeKeyTable[index]}";
        if (index != count - 1)
          newValue += ",";
      }
      string name_table = "name,se_id_,add_effect_name".Replace("se_id_", newValue);
      this.dataTable = TableUtility.CreateStringKeyTable<EnemyHitMaterialTable.MaterialData>(csv, new TableUtility.CallBackStringKeyReadCSV<EnemyHitMaterialTable.MaterialData>(EnemyHitMaterialTable.MaterialData.cb), name_table);
      this.dataTable.TrimExcess();
    }
  }

  public EnemyHitMaterialTable.MaterialData GetData(string name)
  {
    return this.dataTable == null ? (EnemyHitMaterialTable.MaterialData) null : this.dataTable.Get(name);
  }

  public class MaterialData
  {
    public int[] typeSEIDs;
    public string addEffectName;
    public const string NT = "name,se_id_,add_effect_name";

    public static bool cb(CSVReader csv, EnemyHitMaterialTable.MaterialData data, ref string key)
    {
      int length = 0;
      if (Singleton<EnemyHitMaterialTable>.IsValid() && Singleton<EnemyHitMaterialTable>.I.typeKeyTable != null)
        length = Singleton<EnemyHitMaterialTable>.I.typeKeyTable.Count;
      data.typeSEIDs = new int[length];
      for (int index = 0; index < length; ++index)
      {
        data.typeSEIDs[index] = 0;
        csv.Pop(ref data.typeSEIDs[index]);
      }
      csv.Pop(ref data.addEffectName);
      return true;
    }

    public int GetTypeSEID(string hit_type)
    {
      if (!Singleton<EnemyHitMaterialTable>.IsValid() || Singleton<EnemyHitMaterialTable>.I.typeKeyTable == null || this.typeSEIDs == null)
        return 0;
      int index = Singleton<EnemyHitMaterialTable>.I.typeKeyTable.IndexOf(hit_type);
      return index < 0 || index >= this.typeSEIDs.Length ? 0 : this.typeSEIDs[index];
    }
  }
}
