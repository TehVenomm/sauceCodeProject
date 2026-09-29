// Decompiled with JetBrains decompiler
// Type: DamageDistanceTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class DamageDistanceTable : Singleton<DamageDistanceTable>, IDataTable
{
  public const string NT = "id,startRate,distance0,rate0,distance1,rate1,distance2,rate2,distance3,rate3,distance4,rate4,distance5,rate5,distance6,rate6,distance7,rate7,distance8,rate8,distance9,rate9";
  private UIntKeyTable<DamageDistanceTable.DamageDistanceData> dataTable;

  public static bool cb(
    CSVReader csv_reader,
    DamageDistanceTable.DamageDistanceData data,
    ref uint key)
  {
    data.id = key;
    float num = 0.0f;
    List<DamageDistanceTable.DamagePoint> damagePointList = new List<DamageDistanceTable.DamagePoint>();
    DamageDistanceTable.DamagePoint damagePoint1 = new DamageDistanceTable.DamagePoint();
    damagePoint1.distance = (XorFloat) 0.0f;
    csv_reader.Pop(ref damagePoint1.rate);
    damagePointList.Add(damagePoint1);
    for (int index = 0; index < 10; ++index)
    {
      DamageDistanceTable.DamagePoint damagePoint2 = new DamageDistanceTable.DamagePoint();
      CSVReader.PopResult popResult1 = csv_reader.Pop(ref damagePoint2.distance);
      CSVReader.PopResult popResult2 = csv_reader.Pop(ref damagePoint2.rate);
      if ((bool) popResult1 && (bool) popResult2 && (double) (float) damagePoint2.distance > 0.0 && (double) num < (double) (float) damagePoint2.distance)
      {
        num = (float) damagePoint2.distance;
        damagePointList.Add(damagePoint2);
      }
      else
        break;
    }
    data.points = damagePointList.ToArray();
    data.CalcMaxRate();
    return true;
  }

  public void CreateTable(string csv_text)
  {
    this.dataTable = TableUtility.CreateUIntKeyTable<DamageDistanceTable.DamageDistanceData>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<DamageDistanceTable.DamageDistanceData>(DamageDistanceTable.cb), "id,startRate,distance0,rate0,distance1,rate1,distance2,rate2,distance3,rate3,distance4,rate4,distance5,rate5,distance6,rate6,distance7,rate7,distance8,rate8,distance9,rate9");
    this.dataTable.TrimExcess();
  }

  public void AddTable(string csv_text)
  {
    TableUtility.AddUIntKeyTable<DamageDistanceTable.DamageDistanceData>(this.dataTable, csv_text, new TableUtility.CallBackUIntKeyReadCSV<DamageDistanceTable.DamageDistanceData>(DamageDistanceTable.cb), "id,startRate,distance0,rate0,distance1,rate1,distance2,rate2,distance3,rate3,distance4,rate4,distance5,rate5,distance6,rate6,distance7,rate7,distance8,rate8,distance9,rate9");
  }

  public DamageDistanceTable.DamageDistanceData GetData(uint id) => this.dataTable.Get(id);

  public class DamagePoint
  {
    public XorFloat distance;
    public XorFloat rate;
  }

  public class DamageDistanceData
  {
    public uint id;
    public DamageDistanceTable.DamagePoint[] points;
    private float max = -1f;

    public float GetRate(float distance)
    {
      DamageDistanceTable.DamagePoint damagePoint1 = this.points[0];
      DamageDistanceTable.DamagePoint damagePoint2 = this.points[this.points.Length - 1];
      for (int index = 0; index < this.points.Length; ++index)
      {
        DamageDistanceTable.DamagePoint point = this.points[index];
        if ((double) (float) point.distance > (double) distance)
        {
          damagePoint2 = point;
          break;
        }
        damagePoint1 = point;
      }
      float num1 = distance - (float) damagePoint1.distance;
      float num2 = (float) damagePoint2.distance - (float) damagePoint1.distance;
      float num3 = 1f;
      if ((double) distance <= 0.0)
        num3 = 0.0f;
      else if ((double) num2 > 0.0)
        num3 = num1 / num2;
      return Mathf.Lerp((float) damagePoint1.rate, (float) damagePoint2.rate, num3);
    }

    public void CalcMaxRate()
    {
      for (int index = 0; index < this.points.Length; ++index)
        this.max = Mathf.Max(this.max, (float) this.points[index].rate);
    }

    public bool IsMaxRate(float distance) => (double) this.GetRate(distance) >= (double) this.max;
  }
}
