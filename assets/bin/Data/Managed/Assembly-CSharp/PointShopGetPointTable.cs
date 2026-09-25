// Decompiled with JetBrains decompiler
// Type: PointShopGetPointTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class PointShopGetPointTable : Singleton<PointShopGetPointTable>, IDataTable
{
  public UIntKeyTable<PointShopGetPointTable.Data> table;

  public void CreateTable(string csv_text)
  {
    this.table = TableUtility.CreateUIntKeyTable<PointShopGetPointTable.Data>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<PointShopGetPointTable.Data>(PointShopGetPointTable.Data.InsertRow), "id,pointShopId,type,typeId,basePoint,rate");
    this.table.TrimExcess();
  }

  public PointShopGetPointTable.Data GetData(uint id)
  {
    return this.table == null ? (PointShopGetPointTable.Data) null : this.table.Get(id);
  }

  public List<PointShopGetPointTable.Data> GetFromDeiliveryId(uint id)
  {
    List<PointShopGetPointTable.Data> findData = new List<PointShopGetPointTable.Data>();
    this.table.ForEach((Action<PointShopGetPointTable.Data>) (x =>
    {
      if (x.type != SHOP_POINT_GET_TYPE.DELIVERY || (int) x.typeId != (int) id)
        return;
      findData.Add(x);
    }));
    return findData;
  }

  [Serializable]
  public class Data
  {
    public const string INDEX_NAMES = "id,pointShopId,type,typeId,basePoint,rate";
    public uint id;
    public uint pointShopId;
    public SHOP_POINT_GET_TYPE type;
    public uint typeId;
    public int basePoint;
    public int rate;

    public static bool InsertRow(
      CSVReader CSVReader,
      PointShopGetPointTable.Data Data,
      ref uint key)
    {
      Data.id = key;
      CSVReader.Pop(ref Data.pointShopId);
      CSVReader.PopEnum<SHOP_POINT_GET_TYPE>(ref Data.type, SHOP_POINT_GET_TYPE.DELIVERY);
      CSVReader.Pop(ref Data.typeId);
      CSVReader.Pop(ref Data.basePoint);
      CSVReader.Pop(ref Data.rate);
      return true;
    }
  }
}
