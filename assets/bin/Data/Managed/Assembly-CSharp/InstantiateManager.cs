using rhyme;
using System;
using UnityEngine;

public class InstantiateManager : MonoBehaviourSingleton<InstantiateManager>
{
	private class Pool_InstantiateData : rymTPool<InstantiateData>
	{
	}

	private class Pool_StockData : rymTPool<StockData>
	{
	}

	public class InstantiateData
	{
		public Object master;

		public Action<InstantiateData> callback;

		public Object originalObject;

		public Object instantiatedObject;

		public bool isInactivateInstantiatedObject;

		public StockData stockData;

		public void Clear()
		{
			master = null;
			callback = null;
			originalObject = null;
			instantiatedObject = null;
			isInactivateInstantiatedObject = false;
			if (stockData != null)
			{
				rymTPool<StockData>.Release(ref stockData);
			}
		}
	}

	public class StockData
	{
		public int hashCode;

		public RESOURCE_CATEGORY category;

		public string name;

		public Object originalObject;

		public Object instantiatedObject;

		public void Clear()
		{
			name = null;
			originalObject = null;
			instantiatedObject = null;
		}
	}

	private BetterList<InstantiateData> requests = new BetterList<InstantiateData>();

	private BetterList<StockData> stocks = new BetterList<StockData>();

	private Transform inactiveRoot;

	private const int LIMIT_STOCK_COUNT = 1;

	public static bool isBusy
	{
		get
		{
			if (!MonoBehaviourSingleton<InstantiateManager>.IsValid())
			{
				return false;
			}
			if (MonoBehaviourSingleton<InstantiateManager>.I.requests.size == 0)
			{
				return false;
			}
			return true;
		}
	}

	public static void ClearPoolObjects()
	{
		rymTPool<InstantiateData>.Clear();
		rymTPool<StockData>.Clear();
	}

	protected override void Awake()
	{
		base.Awake();
		inactiveRoot = Utility.CreateGameObject("InactiveRoot", base._transform);
		inactiveRoot.get_gameObject().SetActive(false);
		this.set_enabled(false);
	}

	protected override void OnDestroySingleton()
	{
		base.OnDestroySingleton();
		ClearStocks();
	}

	private void Update()
	{
		InstantiateData data = requests.buffer[0];
		requests.RemoveAt(0);
		DoInstantiate(ref data);
		if (requests.size == 0)
		{
			this.set_enabled(false);
		}
	}

	private void Stock(InstantiateData data)
	{
		if (data.stockData != null)
		{
			data.stockData.originalObject = data.originalObject;
			Object instantiatedObject = data.instantiatedObject;
			data.stockData.instantiatedObject = instantiatedObject;
			stocks.Add(data.stockData);
			data.stockData = null;
			while (stocks.size >= 1)
			{
				RemoveStockAt(0, with_destroy: true);
			}
		}
	}

	private int FindStockIndex(RESOURCE_CATEGORY category, string name)
	{
		return FindStockIndex(category, name, name.GetHashCode());
	}

	private int FindStockIndex(RESOURCE_CATEGORY category, string name, int hash_code)
	{
		for (int num = stocks.size - 1; num >= 0; num--)
		{
			StockData stockData = stocks.buffer[num];
			if (stockData.hashCode == hash_code && stockData.category == category && stockData.name == name)
			{
				if (stockData.instantiatedObject != null)
				{
					return num;
				}
				RemoveStockAt(num, with_destroy: false);
			}
		}
		return -1;
	}

	private int FindStockRequestIndex(RESOURCE_CATEGORY category, string name)
	{
		return FindStockRequestIndex(category, name, name.GetHashCode());
	}

	private int FindStockRequestIndex(RESOURCE_CATEGORY category, string name, int hash_code)
	{
		int i = 0;
		for (int size = requests.size; i < size; i++)
		{
			StockData stockData = requests.buffer[i].stockData;
			if (stockData != null && stockData.hashCode == hash_code && stockData.category == category && stockData.name == name)
			{
				return i;
			}
		}
		return -1;
	}

	private void RemoveStockAt(int idx, bool with_destroy)
	{
		StockData stockData = stocks.buffer[idx];
		if (with_destroy && stockData.instantiatedObject != null)
		{
			Object.DestroyImmediate(stockData.instantiatedObject);
		}
		stocks.RemoveAt(idx);
		stockData.Clear();
		rymTPool<StockData>.Release(ref stockData);
	}

	public void ClearStocks()
	{
		int i = 0;
		for (int size = stocks.size; i < size; i++)
		{
			if (stocks[i].instantiatedObject != null)
			{
				Object.DestroyImmediate(stocks[i].instantiatedObject);
			}
		}
		stocks.Release();
	}

	private static void DoInstantiate(ref InstantiateData data)
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		if (data.master != null && data.callback != null)
		{
			if (data.isInactivateInstantiatedObject)
			{
				GameObject val = data.originalObject as GameObject;
				data.instantiatedObject = ResourceUtility.Instantiate<Object>(data.originalObject);
				if (val != null)
				{
					GameObject val2 = data.instantiatedObject;
					val2.get_transform().set_parent(MonoBehaviourSingleton<InstantiateManager>.I.inactiveRoot);
				}
			}
			else
			{
				data.instantiatedObject = ResourceUtility.Instantiate<Object>(data.originalObject);
			}
			data.callback(data);
		}
		data.Clear();
		rymTPool<InstantiateData>.Release(ref data);
	}

	private static void Request(InstantiateData data)
	{
		if (MonoBehaviourSingleton<InstantiateManager>.IsValid())
		{
			if (data.stockData != null || MonoBehaviourSingleton<InstantiateManager>.I.requests.size == 0 || MonoBehaviourSingleton<InstantiateManager>.I.requests.buffer[MonoBehaviourSingleton<InstantiateManager>.I.requests.size - 1].stockData == null)
			{
				MonoBehaviourSingleton<InstantiateManager>.I.requests.Add(data);
			}
			else
			{
				int i = 0;
				for (int size = MonoBehaviourSingleton<InstantiateManager>.I.requests.size; i < size; i++)
				{
					if (MonoBehaviourSingleton<InstantiateManager>.I.requests.buffer[i].stockData != null)
					{
						MonoBehaviourSingleton<InstantiateManager>.I.requests.Insert(i, data);
						break;
					}
				}
			}
			MonoBehaviourSingleton<InstantiateManager>.I.set_enabled(true);
		}
		else
		{
			DoInstantiate(ref data);
		}
	}

	public static void Request(Object master, Object original_object, Action<InstantiateData> callback, bool is_inactivate_instantiated_object = false)
	{
		if (!(original_object == null))
		{
			InstantiateData instantiateData = rymTPool<InstantiateData>.Get();
			instantiateData.master = master;
			instantiateData.callback = callback;
			instantiateData.originalObject = original_object;
			instantiateData.isInactivateInstantiatedObject = is_inactivate_instantiated_object;
			Request(instantiateData);
		}
	}

	public static void RequestStock(RESOURCE_CATEGORY category, Object original_object, string name, bool is_one)
	{
		if (MonoBehaviourSingleton<InstantiateManager>.IsValid() && !(original_object == null))
		{
			int hashCode = name.GetHashCode();
			if (!is_one || (MonoBehaviourSingleton<InstantiateManager>.I.FindStockRequestIndex(category, name, hashCode) == -1 && MonoBehaviourSingleton<InstantiateManager>.I.FindStockIndex(category, name, hashCode) == -1))
			{
				InstantiateData instantiateData = rymTPool<InstantiateData>.Get();
				instantiateData.master = MonoBehaviourSingleton<InstantiateManager>.I;
				instantiateData.callback = MonoBehaviourSingleton<InstantiateManager>.I.Stock;
				instantiateData.originalObject = original_object;
				instantiateData.isInactivateInstantiatedObject = true;
				instantiateData.stockData = rymTPool<StockData>.Get();
				instantiateData.stockData.hashCode = name.GetHashCode();
				instantiateData.stockData.category = category;
				instantiateData.stockData.name = name;
				Request(instantiateData);
			}
		}
	}

	public static Object FindStock(RESOURCE_CATEGORY category, string name)
	{
		if (!MonoBehaviourSingleton<InstantiateManager>.IsValid())
		{
			return null;
		}
		int num = MonoBehaviourSingleton<InstantiateManager>.I.FindStockIndex(category, name);
		if (num == -1)
		{
			return null;
		}
		StockData stockData = MonoBehaviourSingleton<InstantiateManager>.I.stocks.buffer[num];
		Object instantiatedObject = stockData.instantiatedObject;
		Object originalObject = stockData.originalObject;
		MonoBehaviourSingleton<InstantiateManager>.I.RemoveStockAt(num, with_destroy: false);
		if ((category == RESOURCE_CATEGORY.EFFECT_ACTION || category == RESOURCE_CATEGORY.EFFECT_UI) && originalObject != null)
		{
			RequestStock(category, originalObject, name, is_one: false);
		}
		return instantiatedObject;
	}

	public static Transform Realizes(ref GameObject inactive_inctance, Transform parent, int layer)
	{
		if (inactive_inctance == null)
		{
			return null;
		}
		string name = inactive_inctance.get_name();
		name = ResourceName.Normalize(name);
		name = name.Replace("(Clone)", string.Empty);
		inactive_inctance.set_name(name);
		Transform transform = inactive_inctance.get_transform();
		if (parent != null)
		{
			Utility.Attach(parent, transform);
		}
		if (layer != -1)
		{
			Utility.SetLayerWithChildren(transform, layer);
		}
		inactive_inctance.set_hideFlags(0);
		inactive_inctance = null;
		return transform;
	}
}
