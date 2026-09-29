// Decompiled with JetBrains decompiler
// Type: InstantiateManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using rhyme;
using System;
using UnityEngine;

#nullable disable
public class InstantiateManager : MonoBehaviourSingleton<InstantiateManager>
{
  private BetterList<InstantiateManager.InstantiateData> requests = new BetterList<InstantiateManager.InstantiateData>();
  private BetterList<InstantiateManager.StockData> stocks = new BetterList<InstantiateManager.StockData>();
  private Transform inactiveRoot;
  private const int LIMIT_STOCK_COUNT = 1;

  public static void ClearPoolObjects()
  {
    rymTPool<InstantiateManager.InstantiateData>.Clear();
    rymTPool<InstantiateManager.StockData>.Clear();
  }

  public static bool isBusy
  {
    get
    {
      return MonoBehaviourSingleton<InstantiateManager>.IsValid() && MonoBehaviourSingleton<InstantiateManager>.I.requests.size != 0;
    }
  }

  protected override void Awake()
  {
    base.Awake();
    this.inactiveRoot = Utility.CreateGameObject("InactiveRoot", this._transform);
    ((Component) this.inactiveRoot).gameObject.SetActive(false);
    ((Behaviour) this).enabled = false;
  }

  protected override void OnDestroySingleton()
  {
    base.OnDestroySingleton();
    this.ClearStocks();
  }

  private void Update()
  {
    InstantiateManager.InstantiateData data = this.requests.buffer[0];
    this.requests.RemoveAt(0);
    InstantiateManager.DoInstantiate(ref data);
    if (this.requests.size != 0)
      return;
    ((Behaviour) this).enabled = false;
  }

  private void Stock(InstantiateManager.InstantiateData data)
  {
    if (data.stockData == null)
      return;
    data.stockData.originalObject = data.originalObject;
    Object instantiatedObject = data.instantiatedObject;
    data.stockData.instantiatedObject = instantiatedObject;
    this.stocks.Add(data.stockData);
    data.stockData = (InstantiateManager.StockData) null;
    while (this.stocks.size >= 1)
      this.RemoveStockAt(0, true);
  }

  private int FindStockIndex(RESOURCE_CATEGORY category, string name)
  {
    return this.FindStockIndex(category, name, name.GetHashCode());
  }

  private int FindStockIndex(RESOURCE_CATEGORY category, string name, int hash_code)
  {
    for (int idx = this.stocks.size - 1; idx >= 0; --idx)
    {
      InstantiateManager.StockData stockData = this.stocks.buffer[idx];
      if (stockData.hashCode == hash_code && stockData.category == category && stockData.name == name)
      {
        if (Object.op_Inequality(stockData.instantiatedObject, (Object) null))
          return idx;
        this.RemoveStockAt(idx, false);
      }
    }
    return -1;
  }

  private int FindStockRequestIndex(RESOURCE_CATEGORY category, string name)
  {
    return this.FindStockRequestIndex(category, name, name.GetHashCode());
  }

  private int FindStockRequestIndex(RESOURCE_CATEGORY category, string name, int hash_code)
  {
    int stockRequestIndex = 0;
    for (int size = this.requests.size; stockRequestIndex < size; ++stockRequestIndex)
    {
      InstantiateManager.StockData stockData = this.requests.buffer[stockRequestIndex].stockData;
      if (stockData != null && stockData.hashCode == hash_code && stockData.category == category && stockData.name == name)
        return stockRequestIndex;
    }
    return -1;
  }

  private void RemoveStockAt(int idx, bool with_destroy)
  {
    InstantiateManager.StockData stockData = this.stocks.buffer[idx];
    if (with_destroy && Object.op_Inequality(stockData.instantiatedObject, (Object) null))
      Object.DestroyImmediate(stockData.instantiatedObject);
    this.stocks.RemoveAt(idx);
    stockData.Clear();
    rymTPool<InstantiateManager.StockData>.Release(ref stockData);
  }

  public void ClearStocks()
  {
    int i = 0;
    for (int size = this.stocks.size; i < size; ++i)
    {
      if (Object.op_Inequality(this.stocks[i].instantiatedObject, (Object) null))
        Object.DestroyImmediate(this.stocks[i].instantiatedObject);
    }
    this.stocks.Release();
  }

  private static void DoInstantiate(ref InstantiateManager.InstantiateData data)
  {
    if (Object.op_Inequality(data.master, (Object) null) && data.callback != null)
    {
      if (data.isInactivateInstantiatedObject)
      {
        GameObject originalObject = data.originalObject as GameObject;
        data.instantiatedObject = ResourceUtility.Instantiate<Object>(data.originalObject);
        if (Object.op_Inequality((Object) originalObject, (Object) null))
          ((GameObject) data.instantiatedObject).transform.parent = MonoBehaviourSingleton<InstantiateManager>.I.inactiveRoot;
      }
      else
        data.instantiatedObject = ResourceUtility.Instantiate<Object>(data.originalObject);
      data.callback(data);
    }
    data.Clear();
    rymTPool<InstantiateManager.InstantiateData>.Release(ref data);
  }

  private static void Request(InstantiateManager.InstantiateData data)
  {
    if (MonoBehaviourSingleton<InstantiateManager>.IsValid())
    {
      if (data.stockData != null || MonoBehaviourSingleton<InstantiateManager>.I.requests.size == 0 || MonoBehaviourSingleton<InstantiateManager>.I.requests.buffer[MonoBehaviourSingleton<InstantiateManager>.I.requests.size - 1].stockData == null)
      {
        MonoBehaviourSingleton<InstantiateManager>.I.requests.Add(data);
      }
      else
      {
        int index = 0;
        for (int size = MonoBehaviourSingleton<InstantiateManager>.I.requests.size; index < size; ++index)
        {
          if (MonoBehaviourSingleton<InstantiateManager>.I.requests.buffer[index].stockData != null)
          {
            MonoBehaviourSingleton<InstantiateManager>.I.requests.Insert(index, data);
            break;
          }
        }
      }
      ((Behaviour) MonoBehaviourSingleton<InstantiateManager>.I).enabled = true;
    }
    else
      InstantiateManager.DoInstantiate(ref data);
  }

  public static void Request(
    Object master,
    Object original_object,
    Action<InstantiateManager.InstantiateData> callback,
    bool is_inactivate_instantiated_object = false)
  {
    if (Object.op_Equality(original_object, (Object) null))
      return;
    InstantiateManager.InstantiateData data = rymTPool<InstantiateManager.InstantiateData>.Get();
    data.master = master;
    data.callback = callback;
    data.originalObject = original_object;
    data.isInactivateInstantiatedObject = is_inactivate_instantiated_object;
    InstantiateManager.Request(data);
  }

  public static void RequestStock(
    RESOURCE_CATEGORY category,
    Object original_object,
    string name,
    bool is_one)
  {
    if (!MonoBehaviourSingleton<InstantiateManager>.IsValid() || Object.op_Equality(original_object, (Object) null))
      return;
    int hashCode = name.GetHashCode();
    if (is_one && (MonoBehaviourSingleton<InstantiateManager>.I.FindStockRequestIndex(category, name, hashCode) != -1 || MonoBehaviourSingleton<InstantiateManager>.I.FindStockIndex(category, name, hashCode) != -1))
      return;
    InstantiateManager.InstantiateData data = rymTPool<InstantiateManager.InstantiateData>.Get();
    data.master = (Object) MonoBehaviourSingleton<InstantiateManager>.I;
    data.callback = new Action<InstantiateManager.InstantiateData>(MonoBehaviourSingleton<InstantiateManager>.I.Stock);
    data.originalObject = original_object;
    data.isInactivateInstantiatedObject = true;
    data.stockData = rymTPool<InstantiateManager.StockData>.Get();
    data.stockData.hashCode = name.GetHashCode();
    data.stockData.category = category;
    data.stockData.name = name;
    InstantiateManager.Request(data);
  }

  public static Object FindStock(RESOURCE_CATEGORY category, string name)
  {
    if (!MonoBehaviourSingleton<InstantiateManager>.IsValid())
      return (Object) null;
    int stockIndex = MonoBehaviourSingleton<InstantiateManager>.I.FindStockIndex(category, name);
    if (stockIndex == -1)
      return (Object) null;
    InstantiateManager.StockData stockData = MonoBehaviourSingleton<InstantiateManager>.I.stocks.buffer[stockIndex];
    Object instantiatedObject = stockData.instantiatedObject;
    Object originalObject = stockData.originalObject;
    MonoBehaviourSingleton<InstantiateManager>.I.RemoveStockAt(stockIndex, false);
    if ((category == RESOURCE_CATEGORY.EFFECT_ACTION || category == RESOURCE_CATEGORY.EFFECT_UI) && Object.op_Inequality(originalObject, (Object) null))
      InstantiateManager.RequestStock(category, originalObject, name, false);
    return instantiatedObject;
  }

  public static Transform Realizes(ref GameObject inactive_inctance, Transform parent, int layer)
  {
    if (Object.op_Equality((Object) inactive_inctance, (Object) null))
      return (Transform) null;
    string str = ResourceName.Normalize(((Object) inactive_inctance).name).Replace("(Clone)", string.Empty);
    ((Object) inactive_inctance).name = str;
    Transform transform = inactive_inctance.transform;
    if (Object.op_Inequality((Object) parent, (Object) null))
      Utility.Attach(parent, transform);
    if (layer != -1)
      Utility.SetLayerWithChildren(transform, layer);
    ((Object) inactive_inctance).hideFlags = (HideFlags) 0;
    inactive_inctance = (GameObject) null;
    return transform;
  }

  private class Pool_InstantiateData : rymTPool<InstantiateManager.InstantiateData>
  {
  }

  private class Pool_StockData : rymTPool<InstantiateManager.StockData>
  {
  }

  public class InstantiateData
  {
    public Object master;
    public Action<InstantiateManager.InstantiateData> callback;
    public Object originalObject;
    public Object instantiatedObject;
    public bool isInactivateInstantiatedObject;
    public InstantiateManager.StockData stockData;

    public void Clear()
    {
      this.master = (Object) null;
      this.callback = (Action<InstantiateManager.InstantiateData>) null;
      this.originalObject = (Object) null;
      this.instantiatedObject = (Object) null;
      this.isInactivateInstantiatedObject = false;
      if (this.stockData == null)
        return;
      rymTPool<InstantiateManager.StockData>.Release(ref this.stockData);
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
      this.name = (string) null;
      this.originalObject = (Object) null;
      this.instantiatedObject = (Object) null;
    }
  }
}
