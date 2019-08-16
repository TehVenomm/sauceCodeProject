using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class ProductDataTable : Singleton<ProductDataTable>, IDataTable
{
	public class ProductData
	{
		public string bundleId;

		public uint bundleImageId;

		public float openAnimEndTime;

		public string eventName;

		public string chestName;

		public string popupAdsBanner;

		public const string NT = "bundleId,bundleImageId,openAnimEndTime,eventName,chestName,popupAdsBanner";

		public static bool cb(CSVReader csv, ProductData data, ref string key)
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

	public List<PackInfo> packs;

	[CompilerGenerated]
	private static TableUtility.CallBackStringKeyReadCSV<ProductData> _003C_003Ef__mg_0024cache0;

	public StringKeyTable<ProductData> dataTable
	{
		get;
		private set;
	}

	public void CreateTable(string csv_text)
	{
		dataTable = TableUtility.CreateStringKeyTable<ProductData>(csv_text, ProductData.cb, "bundleId,bundleImageId,openAnimEndTime,eventName,chestName,popupAdsBanner");
		dataTable.TrimExcess();
		OnCreatePacksData();
	}

	public ProductData GetData(string name)
	{
		if (dataTable == null)
		{
			return null;
		}
		return dataTable.Get(name);
	}

	private void OnCreatePacksData()
	{
		if (dataTable != null)
		{
			packs = new List<PackInfo>();
			PackInfo packInfo;
			dataTable.ForEach(delegate(ProductData data)
			{
				packInfo = new PackInfo();
				packInfo.bundleId = data.bundleId;
				packInfo.bundleImageId = data.bundleImageId;
				packInfo.openAnimEndTime = data.openAnimEndTime;
				packInfo.eventName = data.eventName;
				packInfo.chestName = data.chestName;
				packInfo.popupAdsBanner = data.popupAdsBanner;
				packs.Add(packInfo);
			});
		}
	}

	public PackInfo GetPack(string id)
	{
		return packs.Find((PackInfo o) => o.bundleId == id);
	}

	public bool HasPack(string id)
	{
		return packs.Find((PackInfo o) => o.bundleId == id) != null;
	}

	public int TotalPack()
	{
		return packs.Count;
	}
}
