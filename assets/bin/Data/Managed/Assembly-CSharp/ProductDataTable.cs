// Decompiled with JetBrains decompiler
// Type: ProductDataTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class ProductDataTable : Singleton<ProductDataTable>, IDataTable
{
  public List<ProductDataTable.PackInfo> packs;

  public StringKeyTable<ProductDataTable.ProductData> dataTable { get; private set; }

  public void CreateTable(string csv_text)
  {
    this.dataTable = TableUtility.CreateStringKeyTable<ProductDataTable.ProductData>(csv_text, new TableUtility.CallBackStringKeyReadCSV<ProductDataTable.ProductData>(ProductDataTable.ProductData.cb), "bundleId,bundleImageId,openAnimEndTime,eventName,chestName,popupAdsBanner");
    this.dataTable.TrimExcess();
    this.OnCreatePacksData();
  }

  public ProductDataTable.ProductData GetData(string name)
  {
    return this.dataTable == null ? (ProductDataTable.ProductData) null : this.dataTable.Get(name);
  }

  private void OnCreatePacksData()
  {
    if (this.dataTable == null)
      return;
    this.packs = new List<ProductDataTable.PackInfo>();
    this.dataTable.ForEach((Action<ProductDataTable.ProductData>) (data => this.packs.Add(new ProductDataTable.PackInfo()
    {
      bundleId = data.bundleId,
      bundleImageId = data.bundleImageId,
      openAnimEndTime = data.openAnimEndTime,
      eventName = data.eventName,
      chestName = data.chestName,
      popupAdsBanner = data.popupAdsBanner
    })));
  }

  public ProductDataTable.PackInfo GetPack(string id)
  {
    return this.packs.Find((Predicate<ProductDataTable.PackInfo>) (o => o.bundleId == id));
  }

  public bool HasPack(string id)
  {
    return this.packs.Find((Predicate<ProductDataTable.PackInfo>) (o => o.bundleId == id)) != null;
  }

  public int TotalPack() => this.packs.Count;

  public class ProductData
  {
    public string bundleId;
    public uint bundleImageId;
    public float openAnimEndTime;
    public string eventName;
    public string chestName;
    public string popupAdsBanner;
    public const string NT = "bundleId,bundleImageId,openAnimEndTime,eventName,chestName,popupAdsBanner";

    public static bool cb(CSVReader csv, ProductDataTable.ProductData data, ref string key)
    {
      data.bundleId = key;
      csv.Pop(ref data.bundleImageId);
      csv.Pop(ref data.openAnimEndTime);
      csv.Pop(ref data.eventName);
      csv.Pop(ref data.chestName);
      csv.Pop(ref data.popupAdsBanner);
      return true;
    }
  }

  public class PackInfo
  {
    public string bundleId;
    public string bundleName;
    public uint bundleImageId;
    public uint offerId;
    public float openAnimEndTime;
    public string eventName;
    public string chestName;
    public string popupAdsBanner;
  }
}
