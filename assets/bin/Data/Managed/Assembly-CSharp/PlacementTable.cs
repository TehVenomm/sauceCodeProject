using System;
using System.Collections.Generic;
using UnityEngine;

[Obsolete]
public class PlacementTable : Singleton<PlacementTable>
{
	public class PlaceableObjectData
	{
		public uint id;

		public uint modelId;

		public string description;

		public ushort gridWidth;

		public ushort gridHeight;

		public PLACEABLE_OBJECT_TYPE type;

		public static readonly string NT = "id,modelId,description,type,gridWidth,gridHeight";

		public static bool cb(CSVReader csv, PlaceableObjectData data, ref uint key)
		{
			data.id = key;
			csv.Pop(ref data.modelId);
			csv.Pop(ref data.description);
			csv.Pop(ref data.type);
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

		public static bool cb(CSVReader csv, PlaceableMapData data, ref uint key)
		{
			data.id = key;
			csv.Pop(ref data.modelId);
			csv.Pop(ref data.description);
			csv.Pop(ref data.type);
			csv.Pop(ref data.gridMaxRow);
			csv.Pop(ref data.gridMaxCol);
			csv.Pop(ref data.mapWidth);
			csv.Pop(ref data.mapHeight);
			return true;
		}
	}

	private UIntKeyTable<PlaceableObjectData> objectTable;

	private UIntKeyTable<PlaceableMapData> mapTable;

	public void CreateTable(TextAsset placeableObjectTextAsset, TextAsset placeableMapTextAsset)
	{
		objectTable = TableUtility.CreateUIntKeyTable<PlaceableObjectData>(placeableObjectTextAsset.get_text(), PlaceableObjectData.cb, PlaceableObjectData.NT, null);
		objectTable.TrimExcess();
		mapTable = TableUtility.CreateUIntKeyTable<PlaceableMapData>(placeableMapTextAsset.get_text(), PlaceableMapData.cb, PlaceableMapData.NT, null);
		mapTable.TrimExcess();
	}

	public PlaceableObjectData GetPlaceableObjectData(uint id)
	{
		if (objectTable == null)
		{
			return null;
		}
		return objectTable.Get(id);
	}

	public PlaceableMapData GetPlaceableMapData(uint id)
	{
		if (mapTable == null)
		{
			return null;
		}
		return mapTable.Get(id);
	}

	public List<PlaceableObjectData> GetPlaceableObjectDataArray()
	{
		if (objectTable == null)
		{
			return null;
		}
		List<PlaceableObjectData> list = new List<PlaceableObjectData>();
		objectTable.ForEach(delegate(PlaceableObjectData item)
		{
			list.Add(item);
		});
		return list;
	}
}
