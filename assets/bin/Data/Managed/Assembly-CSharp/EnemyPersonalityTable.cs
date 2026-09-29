// Decompiled with JetBrains decompiler
// Type: EnemyPersonalityTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class EnemyPersonalityTable : Singleton<EnemyPersonalityTable>, IDataTable
{
  private UIntKeyTable<EnemyPersonalityTable.Data> dataTable;

  public void CreateTable(TextAsset textasset) => this.CreateTable(textasset.text);

  public void CreateTable(string csv)
  {
    this.dataTable = TableUtility.CreateUIntKeyTable<EnemyPersonalityTable.Data>(csv, new TableUtility.CallBackUIntKeyReadCSV<EnemyPersonalityTable.Data>(EnemyPersonalityTable.Data.cb), "id,distanceHateRate,shortShortDistance,shortDistance,middleDistance,longDistance,lifeLowerImportance,lifeLownerVolatize,lifeLowerAttackedVolatize,shortShortDistanceDamage,shortDistanceDamage,middleDistanceDamage,longDistanceDamage,damageImportance,damageVolatize,damageAttackedVolatize,healImportance,healVolatize,healAttackedVolatize,skillImportance,skillVolatize,skillDamagedVolatize,skillHateParam,specialDamageImportance,specialDamageVolatize,specialDamageAttackedVolatize,weakPointHate");
    this.dataTable.TrimExcess();
  }

  public void AddTable(TextAsset textasset)
  {
    TableUtility.AddUIntKeyTable<EnemyPersonalityTable.Data>(this.dataTable, textasset.text, new TableUtility.CallBackUIntKeyReadCSV<EnemyPersonalityTable.Data>(EnemyPersonalityTable.Data.cb), "id,distanceHateRate,shortShortDistance,shortDistance,middleDistance,longDistance,lifeLowerImportance,lifeLownerVolatize,lifeLowerAttackedVolatize,shortShortDistanceDamage,shortDistanceDamage,middleDistanceDamage,longDistanceDamage,damageImportance,damageVolatize,damageAttackedVolatize,healImportance,healVolatize,healAttackedVolatize,skillImportance,skillVolatize,skillDamagedVolatize,skillHateParam,specialDamageImportance,specialDamageVolatize,specialDamageAttackedVolatize,weakPointHate");
  }

  public EnemyPersonalityTable.Data GetData(uint id)
  {
    return this.dataTable == null ? (EnemyPersonalityTable.Data) null : this.dataTable.Get(id);
  }

  public class Data
  {
    public uint id;
    public HateParam param = new HateParam();
    public const string NT = "id,distanceHateRate,shortShortDistance,shortDistance,middleDistance,longDistance,lifeLowerImportance,lifeLownerVolatize,lifeLowerAttackedVolatize,shortShortDistanceDamage,shortDistanceDamage,middleDistanceDamage,longDistanceDamage,damageImportance,damageVolatize,damageAttackedVolatize,healImportance,healVolatize,healAttackedVolatize,skillImportance,skillVolatize,skillDamagedVolatize,skillHateParam,specialDamageImportance,specialDamageVolatize,specialDamageAttackedVolatize,weakPointHate";

    public static bool cb(CSVReader csv_reader, EnemyPersonalityTable.Data data, ref uint key)
    {
      float num = 0.0f;
      for (int index = 0; index < data.param.categoryParam.Length; ++index)
        data.param.categoryParam[index] = new HateParam.CategoryParam();
      data.id = key;
      csv_reader.Pop(ref data.param.categoryParam[0].importance);
      for (int index = 0; index < 4; ++index)
        csv_reader.Pop(ref data.param.distanceHateParams[index]);
      csv_reader.Pop(ref data.param.categoryParam[1].importance);
      csv_reader.Pop(ref num);
      data.param.categoryParam[1].volatilizeRate = 1f - num;
      csv_reader.Pop(ref num);
      data.param.categoryParam[1].atackedVolatizeRate = 1f - num;
      for (int index = 0; index < 4; ++index)
        csv_reader.Pop(ref data.param.distanceAttackRatio[index]);
      for (int index = 2; index < 7; ++index)
      {
        csv_reader.Pop(ref data.param.categoryParam[index].importance);
        csv_reader.Pop(ref num);
        data.param.categoryParam[index].volatilizeRate = 1f - num;
        csv_reader.Pop(ref num);
        data.param.categoryParam[index].atackedVolatizeRate = 1f - num;
        if (index == 4)
          csv_reader.Pop(ref data.param.skillHate);
        if (index == 5)
          csv_reader.Pop(ref data.param.attackedWeakPointHate);
      }
      return true;
    }
  }
}
