// Decompiled with JetBrains decompiler
// Type: ResourceSizeInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ResourceSizeInfo
{
  public const uint MSG_CONFIRM_INIT_DATA = 3001;
  public const uint MSG_CONFIRM_ADD_QUEST = 3002;
  private static ResourceSizeInfo _instance;
  public AssetBundleInfoCollection assetInfo;
  private Dictionary<string, AssetBundleInfoCollection.Info> assetDict = new Dictionary<string, AssetBundleInfoCollection.Info>();
  private int currentManifestVersion;
  private PredownloadTable predownloadTable;
  private static readonly List<string> BUNDLENAMES_OPENING_PROCESS = new List<string>()
  {
    RESOURCE_CATEGORY.SYSTEM.ToAssetBundleName("SystemCommon"),
    RESOURCE_CATEGORY.TABLE.ToAssetBundleName("FieldMapTable"),
    RESOURCE_CATEGORY.TABLE.ToAssetBundleName("FieldMapPortalTable"),
    RESOURCE_CATEGORY.TABLE.ToAssetBundleName("FieldMapEnemyPopTable")
  };

  private static ResourceSizeInfo GetInstance()
  {
    if (ResourceSizeInfo._instance == null)
      ResourceSizeInfo._instance = new ResourceSizeInfo();
    return ResourceSizeInfo._instance;
  }

  public static IEnumerator Init(int manifestVersion = -1)
  {
    if (manifestVersion == -1)
      manifestVersion = MonoBehaviourSingleton<ResourceManager>.I.manifestVersion;
    ResourceSizeInfo instance = ResourceSizeInfo.GetInstance();
    if (manifestVersion > instance.currentManifestVersion)
    {
      yield return (object) instance.LoadSizeInfo();
      instance.assetDict.Clear();
      foreach (AssetBundleInfoCollection.Info assetBundle in instance.assetInfo.assetBundles)
        instance.assetDict[assetBundle.assetBundleName] = assetBundle;
      instance.currentManifestVersion = manifestVersion;
      if (Object.op_Equality((Object) instance.predownloadTable, (Object) null))
        yield return (object) instance.LoadPredownloadTable();
    }
  }

  public static IEnumerator ReInit(int manifestVersion = -1)
  {
    ResourceSizeInfo._instance = (ResourceSizeInfo) null;
    if (manifestVersion == -1)
      manifestVersion = MonoBehaviourSingleton<ResourceManager>.I.manifestVersion;
    ResourceSizeInfo instance = ResourceSizeInfo.GetInstance();
    if (manifestVersion > instance.currentManifestVersion)
    {
      yield return (object) instance.LoadSizeInfo();
      instance.assetDict.Clear();
      foreach (AssetBundleInfoCollection.Info assetBundle in instance.assetInfo.assetBundles)
        instance.assetDict[assetBundle.assetBundleName] = assetBundle;
      instance.currentManifestVersion = manifestVersion;
      if (Object.op_Equality((Object) instance.predownloadTable, (Object) null))
        yield return (object) instance.LoadPredownloadTable();
    }
  }

  private IEnumerator LoadSizeInfo()
  {
    MonoBehaviourSingleton<ResourceManager>.I.LoadSizeInfoManifest();
    while (Object.op_Equality((Object) MonoBehaviourSingleton<ResourceManager>.I.sizeInfoManifest, (Object) null))
      yield return (object) null;
    Hash128 hash128 = new Hash128();
    if (Object.op_Inequality((Object) MonoBehaviourSingleton<ResourceManager>.I.sizeInfoManifest, (Object) null))
      hash128 = MonoBehaviourSingleton<ResourceManager>.I.sizeInfoManifest.GetAssetBundleHash(RESOURCE_CATEGORY.ASSETBUNDLEINFO.ToAssetBundleName());
    if (((Hash128) ref hash128).isValid)
    {
      LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) MonoBehaviourSingleton<AppMain>.I);
      LoadObject lo = loadingQueue.Load(RESOURCE_CATEGORY.ASSETBUNDLEINFO, "assetbundleinfo");
      yield return (object) loadingQueue.Wait();
      this.assetInfo = lo.loadedObject as AssetBundleInfoCollection;
      lo = (LoadObject) null;
    }
  }

  public IEnumerator LoadPredownloadTable()
  {
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) MonoBehaviourSingleton<AppMain>.I);
    LoadObject lo_table = loadingQueue.Load(RESOURCE_CATEGORY.TABLE, "PredownloadTable");
    yield return (object) loadingQueue.Wait();
    this.predownloadTable = lo_table.loadedObject as PredownloadTable;
  }

  private long GetAssetSize(string assetName, bool failSafe = true)
  {
    return failSafe && !this.assetDict.ContainsKey(assetName) ? 0L : this.assetDict[assetName].size;
  }

  public static long GetAssetsSize(string[] assetNames)
  {
    long assetsSize = 0;
    for (int index = 0; index < assetNames.Length; ++index)
      assetsSize += ResourceSizeInfo.GetInstance().GetAssetSize(assetNames[index]);
    return assetsSize;
  }

  public static float GetAssetsSizeMB(string[] assetNames)
  {
    return ResourceSizeInfo.ConvertBToMB(ResourceSizeInfo.GetAssetsSize(assetNames));
  }

  public static float ConvertBToMB(long size) => (float) size / 1048576f;

  public static IEnumerator OpenConfirmDialog(
    float sizeMB,
    uint stringId = 3001,
    CommonDialog.TYPE dialogType = CommonDialog.TYPE.OK,
    Action<string> callback = null)
  {
    while (MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
      yield return (object) null;
    if ((double) sizeMB > 0.0)
    {
      bool close = false;
      MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(dialogType, string.Format(StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, stringId), (object) sizeMB.ToString("#0.0"))), (Action<string>) (str =>
      {
        close = true;
        if (callback == null)
          return;
        callback(str);
      }));
      while (!close)
        yield return (object) null;
    }
  }

  public List<string> GetPredownloadBundleNameList(bool tutorial)
  {
    List<string> collection = new List<string>();
    PredownloadTable predownloadTable = this.predownloadTable;
    if (tutorial)
      collection.AddRange((IEnumerable<string>) ResourceSizeInfo.GetPredownloadAssetNames(predownloadTable.tutorialDatas));
    collection.AddRange((IEnumerable<string>) ResourceSizeInfo.GetPredownloadAssetNames(predownloadTable.preloadDatas));
    collection.AddRange((IEnumerable<string>) ResourceSizeInfo.GetPredownloadAssetNames(predownloadTable.autoDatas));
    collection.AddRange((IEnumerable<string>) ResourceSizeInfo.GetPredownloadAssetNames(predownloadTable.inGameDatas));
    collection.AddRange((IEnumerable<string>) ResourceSizeInfo.GetPredownloadAssetNames(predownloadTable.manualDatas));
    Dictionary<string, AssetBundleInfoCollection.Info> assetDict = this.assetDict;
    List<string> predownloadBundleNameList = new List<string>();
    predownloadBundleNameList.AddRange((IEnumerable<string>) collection);
    foreach (string key in collection)
    {
      AssetBundleInfoCollection.Info data;
      if (assetDict.TryGetValue(key, out data))
      {
        if (ResourceSizeInfo.IsCached(data) & tutorial)
          predownloadBundleNameList.Remove(key);
      }
      else
        predownloadBundleNameList.Remove(key);
    }
    return predownloadBundleNameList;
  }

  private static List<string> GetPredownloadAssetNames(List<PredownloadTable.Data> dataList)
  {
    List<string> predownloadAssetNames = new List<string>();
    System.Type enumType = typeof (RESOURCE_CATEGORY);
    foreach (PredownloadTable.Data data in dataList)
    {
      RESOURCE_CATEGORY category = (RESOURCE_CATEGORY) Enum.Parse(enumType, data.categoryName);
      if (category != RESOURCE_CATEGORY.UI)
      {
        foreach (PredownloadTable.Package package in data.packages)
          predownloadAssetNames.Add(category.ToAssetBundleName(package.packageName));
      }
    }
    return predownloadAssetNames;
  }

  public static float GetOpeningAssetSizeMB(bool isTutorial)
  {
    List<string> stringList = new List<string>();
    stringList.AddRange((IEnumerable<string>) ResourceSizeInfo.BUNDLENAMES_OPENING_PROCESS);
    stringList.AddRange((IEnumerable<string>) ResourceSizeInfo.GetInstance().GetPredownloadBundleNameList(isTutorial));
    return ResourceSizeInfo.GetAssetsSizeMB(stringList.ToArray());
  }

  public static bool IsCached(AssetBundleInfoCollection.Info data)
  {
    return MonoBehaviourSingleton<ResourceManager>.I.IsCached(data.assetBundleName);
  }

  public static bool IsValid()
  {
    return ResourceSizeInfo._instance != null && MonoBehaviourSingleton<ResourceManager>.IsValid() && !Object.op_Equality((Object) MonoBehaviourSingleton<ResourceManager>.I.sizeInfoManifest, (Object) null) && !Object.op_Equality((Object) ResourceSizeInfo._instance.assetInfo, (Object) null);
  }
}
