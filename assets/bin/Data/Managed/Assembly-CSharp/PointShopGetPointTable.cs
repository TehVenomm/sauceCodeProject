using System;
using System.Collections.Generic;

public class PointShopGetPointTable : Singleton<PointShopGetPointTable>, IDataTable
{
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

		public static bool InsertRow(CSVReader CSVReader, Data Data, ref uint key)
		{
			Data.id = key;
			CSVReader.Pop(ref Data.pointShopId);
			CSVReader.PopEnum(ref Data.type, SHOP_POINT_GET_TYPE.DELIVERY);
			CSVReader.Pop(ref Data.typeId);
			CSVReader.Pop(ref Data.basePoint);
			CSVReader.Pop(ref Data.rate);
			return true;
		}
	}

	public UIntKeyTable<Data> table;

	public void CreateTable(string csv_text)
	{
		table = TableUtility.CreateUIntKeyTable<Data>(csv_text, Data.InsertRow, "id,pointShopId,type,typeId,basePoint,rate");
		table.TrimExcess();
	}

	public Data GetData(uint id)
	{
		if (table == null)
		{
			return null;
		}
		return table.Get(id);
	}

	public List<Data> GetFromDeiliveryId(uint id)
	{
		List<Data> findData = new List<Data>();
		table.ForEach(delegate(Data x)
		{
			if (x.type == SHOP_POINT_GET_TYPE.DELIVERY && x.typeId == id)
			{
				findData.Add(x);
			}
		});
		return findData;
	}
}
