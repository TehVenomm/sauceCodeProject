// Decompiled with JetBrains decompiler
// Type: PredownloadManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class PredownloadManager : MonoBehaviourSingleton<PredownloadManager>
{
  public static bool openingMode;
  private PredownloadManager.STOP_FLAG stopFlags;

  public static void Stop(PredownloadManager.STOP_FLAG flag, bool is_stop)
  {
    if (!MonoBehaviourSingleton<PredownloadManager>.IsValid())
      return;
    MonoBehaviourSingleton<PredownloadManager>.I.SetStopFlag(flag, is_stop);
  }

  public int totalCount { get; private set; }

  public int loadedCount { get; private set; }

  public int tutorialCount { get; private set; }

  public bool isLoading => this.totalCount == 0 || this.loadedCount < this.totalCount;

  public bool isLoadingInOpening => this.totalCount == 0 || this.loadedCount < this.tutorialCount;

  public bool isOpening { get; private set; }

  private void SetStopFlag(PredownloadManager.STOP_FLAG flag, bool is_stop)
  {
    if (is_stop)
      this.stopFlags |= flag;
    else
      this.stopFlags &= ~flag;
  }

  protected override void Awake()
  {
    base.Awake();
    this.isOpening = PredownloadManager.openingMode;
    PredownloadManager.openingMode = false;
  }

  protected override void OnDestroySingleton()
  {
    if (!MonoBehaviourSingleton<ResourceManager>.IsValid())
      return;
    MonoBehaviourSingleton<ResourceManager>.I.loadingAssetCountLimit = 4;
  }

  private IEnumerator Start()
  {
    LoadingQueue loading_queue = new LoadingQueue((MonoBehaviour) this);
    ResourceManager.enableCache = false;
    ResourceManager.internalMode = true;
    LoadObject lo_table = loading_queue.Load(RESOURCE_CATEGORY.TABLE, "PredownloadTable");
    ResourceManager.internalMode = false;
    ResourceManager.enableCache = true;
    yield return (object) loading_queue.Wait();
    List<PredownloadManager.LoadInfo> info_list = this.CreateLoadInfoList(lo_table.loadedObject as PredownloadTable, this.isOpening);
    int info_index = 0;
    this.totalCount = info_list.Count;
    List<LoadObject> loading_list = new List<LoadObject>();
    while (this.loadedCount < this.totalCount)
    {
      yield return (object) null;
      MonoBehaviourSingleton<ResourceManager>.I.loadingAssetCountLimit = 4;
      if (this.stopFlags == (PredownloadManager.STOP_FLAG) 0)
      {
        if (this.isLoadingInOpening)
          MonoBehaviourSingleton<ResourceManager>.I.loadingAssetCountLimit = 1;
        while (loading_list.Count < 4 && this.loadedCount + loading_list.Count < this.totalCount)
        {
          PredownloadManager.LoadInfo loadInfo = info_list[info_index++];
          if (this.isOpening && info_index < this.tutorialCount && loadInfo.category != RESOURCE_CATEGORY.STAGE_SCENE)
          {
            ResourceManager.enableCache = true;
            ResourceManager.downloadOnly = false;
          }
          else
          {
            ResourceManager.enableCache = false;
            ResourceManager.downloadOnly = true;
          }
          int num = ResourceManager.internalMode ? 1 : 0;
          ResourceManager.internalMode = false;
          LoadObject loadObject = loadInfo.resourceNames == null || loadInfo.resourceNames.Length == 0 ? loading_queue.Load(loadInfo.category, loadInfo.pakageName) : loading_queue.Load(loadInfo.category, loadInfo.pakageName, loadInfo.resourceNames);
          if (loadObject != null)
            loading_list.Add(loadObject);
          ResourceManager.internalMode = num != 0;
          ResourceManager.downloadOnly = false;
          ResourceManager.enableCache = true;
        }
        int index = 0;
        for (int count = loading_list.Count; index < count; ++index)
        {
          if (!loading_list[index].isLoading)
          {
            loading_list[index] = (LoadObject) null;
            loading_list.RemoveAt(index);
            --index;
            --count;
            this.loadedCount++;
          }
        }
      }
    }
  }

  private List<PredownloadManager.LoadInfo> CreateLoadInfoList(
    PredownloadTable table,
    bool need_tutorial)
  {
    List<PredownloadManager.LoadInfo> list = new List<PredownloadManager.LoadInfo>();
    if (need_tutorial)
    {
      this.AddLoadInfoList(list, table.tutorialDatas);
      this.tutorialCount = list.Count;
    }
    this.AddLoadInfoList(list, table.preloadDatas);
    this.AddLoadInfoList(list, table.autoDatas);
    this.AddLoadInfoList(list, table.inGameDatas);
    this.AddLoadInfoList(list, table.manualDatas);
    return list;
  }

  private void AddLoadInfoList(
    List<PredownloadManager.LoadInfo> list,
    List<PredownloadTable.Data> datas)
  {
    if (datas == null)
      return;
    System.Type enumType = typeof (RESOURCE_CATEGORY);
    int index1 = 0;
    for (int count1 = datas.Count; index1 < count1; ++index1)
    {
      PredownloadTable.Data data = datas[index1];
      RESOURCE_CATEGORY resourceCategory = (RESOURCE_CATEGORY) Enum.Parse(enumType, data.categoryName);
      int index2 = 0;
      for (int count2 = data.packages.Count; index2 < count2; ++index2)
      {
        PredownloadTable.Package package = data.packages[index2];
        if (package != null && !string.IsNullOrEmpty(package.packageName))
        {
          List<string> resourceNames = package.resourceNames;
          if (package.packageName == null || !package.packageName.StartsWith("Debug"))
          {
            List<string> stringList = new List<string>();
            int index3 = 0;
            for (int count3 = package.resourceNames.Count; index3 < count3; ++index3)
            {
              if (!package.resourceNames[index3].StartsWith("Debug"))
                stringList.Add(package.resourceNames[index3]);
            }
            list.Add(new PredownloadManager.LoadInfo()
            {
              category = resourceCategory,
              pakageName = package.packageName,
              resourceNames = stringList.ToArray()
            });
          }
        }
      }
    }
  }

  public void GetCount(out int total, out int loaded)
  {
    if (this.isOpening)
    {
      if (this.loadedCount < this.tutorialCount)
      {
        total = this.tutorialCount;
        loaded = this.loadedCount;
      }
      else
      {
        total = this.totalCount - this.tutorialCount;
        loaded = this.loadedCount - this.tutorialCount;
      }
    }
    else
    {
      total = this.totalCount;
      loaded = this.loadedCount;
    }
  }

  public static bool IsVisibleDownloadGauge()
  {
    if (!MonoBehaviourSingleton<PredownloadManager>.IsValid())
      return false;
    return !MonoBehaviourSingleton<PredownloadManager>.I.isOpening || MonoBehaviourSingleton<PredownloadManager>.I.loadedCount < MonoBehaviourSingleton<PredownloadManager>.I.tutorialCount || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "CharaMake";
  }

  [Flags]
  public enum STOP_FLAG
  {
    LOADING_PROCESS = 1,
    INGAME_MAIN = 2,
    INGAME_TUTORIAL = 4,
  }

  private class LoadInfo
  {
    public RESOURCE_CATEGORY category;
    public string pakageName;
    public string[] resourceNames;
  }
}
