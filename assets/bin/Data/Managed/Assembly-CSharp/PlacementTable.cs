// Decompiled with JetBrains decompiler
// Type: PlacementTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
[Obsolete]
public class PlacementTable : Singleton<PlacementTable>
{
  private UIntKeyTable<PlacementTable.PlaceableObjectData> objectTable;
  private UIntKeyTable<PlacementTable.PlaceableMapData> mapTable;

  public void CreateTable(TextAsset placeableObjectTextAsset, TextAsset placeableMapTextAsset)
  {
    this.objectTable = TableUtility.CreateUIntKeyTable<PlacementTable.PlaceableObjectData>(placeableObjectTextAsset.text, new TableUtility.CallBackUIntKeyReadCSV<PlacementTable.PlaceableObjectData>(PlacementTable.PlaceableObjectData.cb), PlacementTable.PlaceableObjectData.NT);
    this.objectTable.TrimExcess();
    this.mapTable = TableUtility.CreateUIntKeyTable<PlacementTable.PlaceableMapData>(placeableMapTextAsset.text, new TableUtility.CallBackUIntKeyReadCSV<PlacementTable.PlaceableMapData>(PlacementTable.PlaceableMapData.cb), PlacementTable.PlaceableMapData.NT);
    this.mapTable.TrimExcess();
  }

  public PlacementTable.PlaceableObjectData GetPlaceableObjectData(uint id)
  {
    return this.objectTable == null ? (PlacementTable.PlaceableObjectData) null : this.objectTable.Get(id);
  }

  public PlacementTable.PlaceableMapData GetPlaceableMapData(uint id)
  {
    return this.mapTable == null ? (PlacementTable.PlaceableMapData) null : this.mapTable.Get(id);
  }

  public List<PlacementTable.PlaceableObjectData> GetPlaceableObjectDataArray()
  {
    if (this.objectTable == null)
      return (List<PlacementTable.PlaceableObjectData>) null;
    List<PlacementTable.PlaceableObjectData> list = new List<PlacementTable.PlaceableObjectData>();
    this.objectTable.ForEach((Action<PlacementTable.PlaceableObjectData>) (item => list.Add(item)));
    return list;
  }

  public class PlaceableObjectData
  {
    public uint id;
    public uint modelId;
    public string description;
    public ushort gridWidth;
    public ushort gridHeight;
    public PLACEABLE_OBJECT_TYPE type;
    public static readonly string NT = "id,modelId,description,type,gridWidth,gridHeight";

    public static bool cb(CSVReader csv, PlacementTable.PlaceableObjectData data, ref uint key)
    {
      data.id = key;
      csv.Pop(ref data.modelId);
      csv.Pop(ref data.description);
      csv.Pop<PLACEABLE_OBJECT_TYPE>(ref data.type);
      csv.Pop(ref data.gridWidth);
      csv.Pop(ref data.gridHeight);
      return true;
    }
  }

  public class PlaceableMapData
  {
    public uint id;
    public uint modelId;
    public string description;
    public ushort gridMaxRow;
    public ushort gridMaxCol;
    public float mapWidth;
    public float mapHeight;
    public PLACEABLE_MAP_TYPE type;
    public static readonly string NT = "id,modelId,description,type,gridMaxRow,gridMaxCol,mapWidth,mapHeight";

    public static bool cb(CSVReader csv, PlacementTable.PlaceableMapData data, ref uint key)
    {
      data.id = key;
      csv.Pop(ref data.modelId);
      csv.Pop(ref data.description);
      csv.Pop<PLACEABLE_MAP_TYPE>(ref data.type);
      csv.Pop(ref data.gridMaxRow);
      csv.Pop(ref data.gridMaxCol);
      csv.Pop(ref data.mapWidth);
      csv.Pop(ref data.mapHeight);
      return true;
    }
  }
}
