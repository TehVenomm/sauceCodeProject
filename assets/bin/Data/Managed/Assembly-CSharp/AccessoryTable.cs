using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

public class AccessoryTable : Singleton<AccessoryTable>, IDataTable
{
	public class AccessoryData
	{
		public uint accessoryId;

		public RARITY_TYPE rarity;

		public string name;

		public int orderValue;

		public uint attachPlaceBit;

		public GET_TYPE getType;

		public int price;

		public bool cantSell;

		public float detailScale;

		public string descript;

		public string descriptPart;

		public const string NT = "accessoryId,rarity,name,orderValue,attachPlaceBit,getType,price,cantSell,detailScale";

		public static bool cb(CSVReader csv_reader, AccessoryData data, ref uint key)
		{
			data.accessoryId = key;
			csv_reader.PopEnum(ref data.rarity, RARITY_TYPE.D);
			csv_reader.Pop(ref data.name);
			csv_reader.Pop(ref data.orderValue);
			csv_reader.Pop(ref data.attachPlaceBit);
			csv_reader.PopEnum(ref data.getType, GET_TYPE.FREE);
			csv_reader.Pop(ref data.price);
			int value = 0;
			csv_reader.Pop(ref value);
			data.cantSell = (value != 0);
			csv_reader.Pop(ref data.detailScale);
			bool flag = true;
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i <= 9; i++)
			{
				if ((data.attachPlaceBit & (1 << i)) != 0)
				{
					if (flag)
					{
						flag = false;
					}
					else
					{
						stringBuilder.Append("/");
					}
					switch (i)
					{
					case 0:
						stringBuilder.Append("Head");
						break;
					case 1:
						stringBuilder.Append("Face");
						break;
					case 2:
						stringBuilder.Append("Right Shoulder");
						break;
					case 3:
						stringBuilder.Append("Left Shoulder");
						break;
					case 4:
						stringBuilder.Append("Right Arm");
						break;
					case 5:
						stringBuilder.Append("Left Arm");
						break;
					case 6:
						stringBuilder.Append("Chest");
						break;
					case 7:
						stringBuilder.Append("Hip");
						break;
					case 8:
						stringBuilder.Append("Right Leg");
						break;
					case 9:
						stringBuilder.Append("Left Leg");
						break;
					}
				}
			}
			data.descriptPart = stringBuilder.ToString();
			stringBuilder.Remove(0, stringBuilder.Length);
			stringBuilder.Append("Equippable Areas:");
			stringBuilder.AppendLine();
			stringBuilder.Append(data.descriptPart);
			data.descript = stringBuilder.ToString();
			return true;
		}
	}

	public class AccessoryInfoData
	{
		public uint id;

		public uint accessoryId;

		public ACCESSORY_PART attachPlace;

		public string node;

		public Vector3 offset;

		public Quaternion rotation;

		public Vector3 scale;

		public const string NT = "id,accessoryId,attachPlace,node,px,py,pz,rx,ry,rz,sx,sy,sz";

		public static bool cb(CSVReader csv_reader, AccessoryInfoData data, ref uint key)
		{
			//IL_0061: Unknown result type (might be due to invalid IL or missing references)
			//IL_0066: Unknown result type (might be due to invalid IL or missing references)
			//IL_008a: Unknown result type (might be due to invalid IL or missing references)
			//IL_008f: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
			data.id = key;
			csv_reader.Pop(ref data.accessoryId);
			csv_reader.PopEnum(ref data.attachPlace, ACCESSORY_PART.NONE);
			csv_reader.Pop(ref data.node);
			float value = 0f;
			float value2 = 0f;
			float value3 = 0f;
			csv_reader.Pop(ref value);
			csv_reader.Pop(ref value2);
			csv_reader.Pop(ref value3);
			data.offset = new Vector3(value, value2, value3);
			csv_reader.Pop(ref value);
			csv_reader.Pop(ref value2);
			csv_reader.Pop(ref value3);
			data.rotation = Quaternion.Euler(value, value2, value3);
			csv_reader.Pop(ref value);
			csv_reader.Pop(ref value2);
			csv_reader.Pop(ref value3);
			data.scale = new Vector3(value, value2, value3);
			return true;
		}
	}

	private UIntKeyTable<AccessoryData> dataTable;

	private UIntKeyTable<AccessoryInfoData> infoTable;

	[CompilerGenerated]
	private static TableUtility.CallBackUIntKeyReadCSV<AccessoryData> _003C_003Ef__mg_0024cache0;

	[CompilerGenerated]
	private static TableUtility.CallBackUIntKeyReadCSV<AccessoryInfoData> _003C_003Ef__mg_0024cache1;

	public void CreateTable(string text)
	{
		dataTable = TableUtility.CreateUIntKeyTable<AccessoryData>(text, AccessoryData.cb, "accessoryId,rarity,name,orderValue,attachPlaceBit,getType,price,cantSell,detailScale");
		dataTable.TrimExcess();
	}

	public void CreateInfoTable(string text)
	{
		infoTable = TableUtility.CreateUIntKeyTable<AccessoryInfoData>(text, AccessoryInfoData.cb, "id,accessoryId,attachPlace,node,px,py,pz,rx,ry,rz,sx,sy,sz");
		infoTable.TrimExcess();
	}

	public AccessoryData GetData(uint aid)
	{
		if (dataTable == null)
		{
			return null;
		}
		AccessoryData accessoryData = dataTable.Get(aid);
		if (accessoryData == null)
		{
			return null;
		}
		return accessoryData;
	}

	public void ForEachData(Action<AccessoryData> cb)
	{
		dataTable.ForEach(cb);
	}

	public AccessoryInfoData GetInfoData(uint uid)
	{
		if (infoTable == null)
		{
			return null;
		}
		AccessoryInfoData accessoryInfoData = infoTable.Get(uid);
		if (accessoryInfoData == null)
		{
			return null;
		}
		return accessoryInfoData;
	}

	public List<AccessoryInfoData> GetInfoList(uint aid)
	{
		if (infoTable == null)
		{
			return null;
		}
		List<AccessoryInfoData> list = new List<AccessoryInfoData>();
		infoTable.ForEach(delegate(AccessoryInfoData i)
		{
			if (i.accessoryId == aid)
			{
				list.Add(i);
			}
		});
		return list;
	}
}
