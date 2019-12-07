using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSizeInfo
{
	public const uint MSG_CONFIRM_INIT_DATA = 3001u;

	public const uint MSG_CONFIRM_ADD_QUEST = 3002u;

	private static ResourceSizeInfo _instance;

	public AssetBundleInfoCollection assetInfo;

	private Dictionary<string, AssetBundleInfoCollection.Info> assetDict = new Dictionary<string, AssetBundleInfoCollection.Info>();

	private int currentManifestVersion;

	private PredownloadTable predownloadTable;

	private static readonly List<string> BUNDLENAMES_OPENING_PROCESS = new List<string>
	{
		RESOURCE_CATEGORY.SYSTEM.ToAssetBundleName("SystemCommon"),
		RESOURCE_CATEGORY.TABLE.ToAssetBundleName("FieldMapTable"),
		RESOURCE_CATEGORY.TABLE.ToAssetBundleName("FieldMapPortalTable"),
		RESOURCE_CATEGORY.TABLE.ToAssetBundleName("FieldMapEnemyPopTable")
	};

	private static ResourceSizeInfo GetInstance()
	{
		if (_instance == null)
		{
			_instance = new ResourceSizeInfo();
		}
		return _instance;
	}

	public static IEnumerator Init(int manifestVersion = -1)
	{
		if (manifestVersion == -1)
		{
			manifestVersion = MonoBehaviourSingleton<ResourceManager>.I.manifestVersion;
		}
		ResourceSizeInfo instance = GetInstance();
		if (manifestVersion > instance.currentManifestVersion)
		{
			yield return instance.LoadSizeInfo();
			instance.assetDict.Clear();
			foreach (AssetBundleInfoCollection.Info assetBundle in instance.assetInfo.assetBundles)
			{
				instance.assetDict[assetBundle.assetBundleName] = assetBundle;
			}
			instance.currentManifestVersion = manifestVersion;
			if (instance.predownloadTable == null)
			{
				yield return instance.LoadPredownloadTable();
			}
		}
	}

	public static IEnumerator ReInit(int manifestVersion = -1)
	{
		_instance = null;
		if (manifestVersion == -1)
		{
			manifestVersion = MonoBehaviourSingleton<ResourceManager>.I.manifestVersion;
		}
		ResourceSizeInfo instance = GetInstance();
		if (manifestVersion > instance.currentManifestVersion)
		{
			yield return instance.LoadSizeInfo();
			instance.assetDict.Clear();
			foreach (AssetBundleInfoCollection.Info assetBundle in instance.assetInfo.assetBundles)
			{
				instance.assetDict[assetBundle.assetBundleName] = assetBundle;
			}
			instance.currentManifestVersion = manifestVersion;
			if (instance.predownloadTable == null)
			{
				yield return instance.LoadPredownloadTable();
			}
		}
	}

	private IEnumerator LoadSizeInfo()
	{
		MonoBehaviourSingleton<ResourceManager>.I.LoadSizeInfoManifest();
		while (MonoBehaviourSingleton<ResourceManager>.I.sizeInfoManifest == null)
		{
			yield return null;
		}
		Hash128 hash = default(Hash128);
		if (MonoBehaviourSingleton<ResourceManager>.I.sizeInfoManifest != null)
		{
			hash = MonoBehaviourSingleton<ResourceManager>.I.sizeInfoManifest.GetAssetBundleHash(RESOURCE_CATEGORY.ASSETBUNDLEINFO.ToAssetBundleName());
		}
		if (hash.isValid)
		{
			LoadingQueue loadingQueue = new LoadingQueue(MonoBehaviourSingleton<AppMain>.I);
			LoadObject lo = loadingQueue.Load(RESOURCE_CATEGORY.ASSETBUNDLEINFO, "assetbundleinfo");
			yield return loadingQueue.Wait();
			assetInfo = (lo.loadedObject as AssetBundleInfoCollection);
		}
	}

	public IEnumerator LoadPredownloadTable()
	{
		LoadingQueue loadingQueue = new LoadingQueue(MonoBehaviourSingleton<AppMain>.I);
		LoadObject lo_table = loadingQueue.Load(RESOURCE_CATEGORY.TABLE, "PredownloadTable");
		yield return loadingQueue.Wait();
		predownloadTable = (lo_table.loadedObject as PredownloadTable);
	}

	private long GetAssetSize(string assetName, bool failSafe = true)
	{
		if (failSafe && !assetDict.ContainsKey(assetName))
		{
			return 0L;
		}
		return assetDict[assetName].size;
	}

	public static long GetAssetsSize(string[] assetNames)
	{
		long num = 0L;
		for (int i = 0; i < assetNames.Length; i++)
		{
			num += GetInstance().GetAssetSize(assetNames[i]);
		}
		return num;
	}

	public static float GetAssetsSizeMB(string[] assetNames)
	{
		return ConvertBToMB(GetAssetsSize(assetNames));
	}

	public static float ConvertBToMB(long size)
	{
		return (float)size / 1048576f;
	}

	public static IEnumerator OpenConfirmDialog(float sizeMB, uint stringId = 3001u, CommonDialog.TYPE dialogType = CommonDialog.TYPE.OK, Action<string> callback = null)
	{
		while (MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
		{
			yield return null;
		}
		if (!(sizeMB <= 0f))
		{
			bool close = false;
			MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(dialogType, string.Format(StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, stringId), sizeMB.ToString("#0.0"))), delegate(string str)
			{
				close = true;
				if (callback != null)
				{
					callback(str);
				}
			});
			while (!close)
			{
				yield return null;
			}
		}
	}

	public List<string> GetPredownloadBundleNameList(bool tutorial)
	{
		List<string> list = new List<string>();
		PredownloadTable predownloadTable = this.predownloadTable;
		if (tutorial)
		{
			list.AddRange(GetPredownloadAssetNames(predownloadTable.tutorialDatas));
		}
		list.AddRange(GetPredownloadAssetNames(predownloadTable.preloadDatas));
		list.AddRange(GetPredownloadAssetNames(predownloadTable.autoDatas));
		list.AddRange(GetPredownloadAssetNames(predownloadTable.inGameDatas));
		list.AddRange(GetPredownloadAssetNames(predownloadTable.manualDatas));
		Dictionary<string, AssetBundleInfoCollection.Info> dictionary = assetDict;
		List<string> list2 = new List<string>();
		list2.AddRange(list);
		foreach (string item in list)
		{
			if (dictionary.TryGetValue(item, out AssetBundleInfoCollection.Info value))
			{
				if (IsCached(value) && tutorial)
				{
					list2.Remove(item);
				}
			}
			else
			{
				list2.Remove(item);
			}
		}
		return list2;
	}

	private static List<string> GetPredownloadAssetNames(List<PredownloadTable.Data> dataList)
	{
		List<string> list = new List<string>();
		Type typeFromHandle = typeof(RESOURCE_CATEGORY);
		foreach (PredownloadTable.Data data in dataList)
		{
			RESOURCE_CATEGORY rESOURCE_CATEGORY = (RESOURCE_CATEGORY)Enum.Parse(typeFromHandle, data.categoryName);
			if (rESOURCE_CATEGORY != RESOURCE_CATEGORY.UI)
			{
				foreach (PredownloadTable.Package package in data.packages)
				{
					list.Add(rESOURCE_CATEGORY.ToAssetBundleName(package.packageName));
				}
			}
		}
		return list;
	}

	public static float GetOpeningAssetSizeMB(bool isTutorial)
	{
		List<string> list = new List<string>();
		list.AddRange(BUNDLENAMES_OPENING_PROCESS);
		list.AddRange(GetInstance().GetPredownloadBundleNameList(isTutorial));
		return GetAssetsSizeMB(list.ToArray());
	}

	public static bool IsCached(AssetBundleInfoCollection.Info data)
	{
		return MonoBehaviourSingleton<ResourceManager>.I.IsCached(data.assetBundleName);
	}

	public static bool IsValid()
	{
		if (_instance == null)
		{
			return false;
		}
		if (!MonoBehaviourSingleton<ResourceManager>.IsValid())
		{
			return false;
		}
		if (MonoBehaviourSingleton<ResourceManager>.I.sizeInfoManifest == null)
		{
			return false;
		}
		if (_instance.assetInfo == null)
		{
			return false;
		}
		return true;
	}
}
