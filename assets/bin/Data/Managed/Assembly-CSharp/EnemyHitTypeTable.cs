// Decompiled with JetBrains decompiler
// Type: EnemyHitTypeTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class EnemyHitTypeTable : Singleton<EnemyHitTypeTable>, IDataTable
{
  public StringKeyTable<EnemyHitTypeTable.TypeData> dataTable { get; private set; }

  public void CreateTable(TextAsset stage_table_text_asset)
  {
    this.CreateTable(stage_table_text_asset.text);
  }

  public void CreateTable(string csv)
  {
    this.dataTable = TableUtility.CreateStringKeyTable<EnemyHitTypeTable.TypeData>(csv, new TableUtility.CallBackStringKeyReadCSV<EnemyHitTypeTable.TypeData>(EnemyHitTypeTable.TypeData.cb), "name,base_effect,fire_effect,water_effect,thunder_effect,soil_effect,light_effect,dark_effect");
    this.dataTable.TrimExcess();
  }

  public EnemyHitTypeTable.TypeData GetData(string name, bool is_field)
  {
    if (this.dataTable == null)
      return (EnemyHitTypeTable.TypeData) null;
    if (is_field)
      name += "@FIELD";
    return this.dataTable.Get(name);
  }

  public class TypeData
  {
    public string baseEffectName;
    public string[] elementEffectNames = new string[6];
    public const string NT = "name,base_effect,fire_effect,water_effect,thunder_effect,soil_effect,light_effect,dark_effect";

    public static bool cb(CSVReader csv, EnemyHitTypeTable.TypeData data, ref string key)
    {
      csv.Pop(ref data.baseEffectName);
      for (int index = 0; index < 6; ++index)
        csv.Pop(ref data.elementEffectNames[index]);
      return true;
    }
  }
}
