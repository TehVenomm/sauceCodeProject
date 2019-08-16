using System;
using System.Collections;
using System.Collections.Generic;

public class PredownloadManager : MonoBehaviourSingleton<PredownloadManager>
{
	[Flags]
	public enum STOP_FLAG
	{
		LOADING_PROCESS = 0x1,
		INGAME_MAIN = 0x2,
		INGAME_TUTORIAL = 0x4
	}

	private class LoadInfo
	{
		public RESOURCE_CATEGORY category;

		public string pakageName;

		public string[] resourceNames;
	}

	public static bool openingMode;

	private STOP_FLAG stopFlags;

	public int totalCount
	{
		get;
		private set;
	}

	public int loadedCount
	{
		get;
		private set;
	}

	public int tutorialCount
	{
		get;
		private set;
	}

	public bool isLoading
	{
		get
		{
			if (totalCount == 0)
			{
				return true;
			}
			return loadedCount < totalCount;
		}
	}

	public bool isLoadingInOpening
	{
		get
		{
			if (totalCount == 0)
			{
				return true;
			}
			return loadedCount < tutorialCount;
		}
	}

	public bool isOpening
	{
		get;
		private set;
	}

	public static void Stop(STOP_FLAG flag, bool is_stop)
	{
		if (MonoBehaviourSingleton<PredownloadManager>.IsValid())
		{
			MonoBehaviourSingleton<PredownloadManager>.I.SetStopFlag(flag, is_stop);
		}
	}

	private void SetStopFlag(STOP_FLAG flag, bool is_stop)
	{
		if (is_stop)
		{
			stopFlags |= flag;
		}
		else
		{
			stopFlags &= ~flag;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		isOpening = openingMode;
		openingMode = false;
	}

	protected override void OnDestroySingleton()
	{
		if (MonoBehaviourSingleton<ResourceManager>.IsValid())
		{
			MonoBehaviourSingleton<ResourceManager>.I.loadingAssetCountLimit = 4;
		}
	}

	private IEnumerator Start()
	{
		LoadingQueue loading_queue = new LoadingQueue(this);
		ResourceManager.enableCache = false;
		ResourceManager.internalMode = true;
		LoadObject lo_table = loading_queue.Load(RESOURCE_CATEGORY.TABLE, "PredownloadTable");
		ResourceManager.internalMode = false;
		ResourceManager.enableCache = true;
		yield return loading_queue.Wait();
		PredownloadTable table = lo_table.loadedObject as PredownloadTable;
		List<LoadInfo> info_list = CreateLoadInfoList(table, isOpening);
		int info_index = 0;
		totalCount = info_list.Count;
		List<LoadObject> loading_list = new List<LoadObject>();
		while (loadedCount < totalCount)
		{
			yield return null;
			MonoBehaviourSingleton<ResourceManager>.I.loadingAssetCountLimit = 4;
			if (stopFlags != 0)
			{
				continue;
			}
			if (isLoadingInOpening)
			{
				MonoBehaviourSingleton<ResourceManager>.I.loadingAssetCountLimit = 1;
			}
			while (loading_list.Count < 4 && loadedCount + loading_list.Count < totalCount)
			{
				List<LoadInfo> list = info_list;
				int index;
				info_index = (index = info_index) + 1;
				LoadInfo loadInfo = list[index];
				if (isOpening && info_index < tutorialCount && loadInfo.category != RESOURCE_CATEGORY.STAGE_SCENE)
				{
					ResourceManager.enableCache = true;
					ResourceManager.downloadOnly = false;
				}
				else
				{
					ResourceManager.enableCache = false;
					ResourceManager.downloadOnly = true;
				}
				bool internalMode = ResourceManager.internalMode;
				ResourceManager.internalMode = false;
				LoadObject loadObject = (loadInfo.resourceNames == null || loadInfo.resourceNames.Length <= 0) ? loading_queue.Load(loadInfo.category, loadInfo.pakageName) : loading_queue.Load(loadInfo.category, loadInfo.pakageName, loadInfo.resourceNames);
				if (loadObject != null)
				{
					loading_list.Add(loadObject);
				}
				ResourceManager.internalMode = internalMode;
				ResourceManager.downloadOnly = false;
				ResourceManager.enableCache = true;
			}
			int i = 0;
			for (int num = loading_list.Count; i < num; i++)
			{
				if (!loading_list[i].isLoading)
				{
					loading_list[i] = null;
					loading_list.RemoveAt(i);
					i--;
					num--;
					loadedCount++;
				}
			}
		}
	}

	private List<LoadInfo> CreateLoadInfoList(PredownloadTable table, bool need_tutorial)
	{
		List<LoadInfo> list = new List<LoadInfo>();
		if (need_tutorial)
		{
			AddLoadInfoList(list, table.tutorialDatas);
			tutorialCount = list.Count;
		}
		AddLoadInfoList(list, table.preloadDatas);
		AddLoadInfoList(list, table.autoDatas);
		AddLoadInfoList(list, table.inGameDatas);
		AddLoadInfoList(list, table.manualDatas);
		return list;
	}

	private void AddLoadInfoList(List<LoadInfo> list, List<PredownloadTable.Data> datas)
	{
		if (datas == null)
		{
			return;
		}
		Type typeFromHandle = typeof(RESOURCE_CATEGORY);
		int i = 0;
		for (int count = datas.Count; i < count; i++)
		{
			PredownloadTable.Data data = datas[i];
			RESOURCE_CATEGORY category = (RESOURCE_CATEGORY)Enum.Parse(typeFromHandle, data.categoryName);
			int j = 0;
			for (int count2 = data.packages.Count; j < count2; j++)
			{
				PredownloadTable.Package package = data.packages[j];
				if (package == null || string.IsNullOrEmpty(package.packageName))
				{
					continue;
				}
				List<string> resourceNames = package.resourceNames;
				if (package.packageName != null && package.packageName.StartsWith("Debug"))
				{
					continue;
				}
				resourceNames = new List<string>();
				int k = 0;
				for (int count3 = package.resourceNames.Count; k < count3; k++)
				{
					if (!package.resourceNames[k].StartsWith("Debug"))
					{
						resourceNames.Add(package.resourceNames[k]);
					}
				}
				LoadInfo loadInfo = new LoadInfo();
				loadInfo.category = category;
				loadInfo.pakageName = package.packageName;
				loadInfo.resourceNames = resourceNames.ToArray();
				list.Add(loadInfo);
			}
		}
	}

	public void GetCount(out int total, out int loaded)
	{
		if (isOpening)
		{
			if (loadedCount < tutorialCount)
			{
				total = tutorialCount;
				loaded = loadedCount;
			}
			else
			{
				total = totalCount - tutorialCount;
				loaded = loadedCount - tutorialCount;
			}
		}
		else
		{
			total = totalCount;
			loaded = loadedCount;
		}
	}

	public static bool IsVisibleDownloadGauge()
	{
		if (!MonoBehaviourSingleton<PredownloadManager>.IsValid())
		{
			return false;
		}
		if (!MonoBehaviourSingleton<PredownloadManager>.I.isOpening)
		{
			return true;
		}
		if (MonoBehaviourSingleton<PredownloadManager>.I.loadedCount < MonoBehaviourSingleton<PredownloadManager>.I.tutorialCount)
		{
			return true;
		}
		return MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "CharaMake";
	}
}
