// Decompiled with JetBrains decompiler
// Type: AssetPreDownloadManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
public class AssetPreDownloadManager : MonoBehaviourSingleton<AssetPreDownloadManager>
{
  private const int LIMIT_ASSET_DOWNLOAD = 10;
  private const int THRESHOLE_ASSET_DOWNLOAD = 20;
  private const float LIMIT_ASSET_DOWNLOAD_SIZE = 100f;
  public List<AssetPreDownloadList> myList = new List<AssetPreDownloadList>();
  public AssetPreDownloadManager.OnUpdateDownload m_OnUpdateDownload = (AssetPreDownloadManager.OnUpdateDownload) (() => { });
  public AssetPreDownloadManager.OnFinishDownload m_OnFinishDownload = (AssetPreDownloadManager.OnFinishDownload) (() => { });
  public AssetPreDownloadManager.OnStopDownload m_OnStopDownload = (AssetPreDownloadManager.OnStopDownload) (() => { });
  public AssetPreDownloadManager.OnFinishCheck m_OnFinishCheck = (AssetPreDownloadManager.OnFinishCheck) (_param1 => { });
  public AssetPreDownloadManager.OnUpdateCheck m_OnUpdateCheck = (AssetPreDownloadManager.OnUpdateCheck) (() => { });
  private List<AssetPreDownloadList.LoadPackage> loading_packages = new List<AssetPreDownloadList.LoadPackage>();
  private HashSet<AssetPreDownloadList.LoadPackage> hash_packages = new HashSet<AssetPreDownloadList.LoadPackage>();
  private Dictionary<AssetPreDownloadList, int> countPackages = new Dictionary<AssetPreDownloadList, int>();
  private int checkedListCount;
  private int resumeListCount;
  private bool stopFlag;
  private bool isNewData;
  private bool isActive;
  private EnemyPredownloadList enemyList;
  private EventPredownloadList eventList;

  public AssetPreDownloadManager.DownloadState CurrentState { get; private set; }

  public float totalFileSize { get; private set; }

  public int checkedCount { get; protected set; }

  public int totalCheckCount { get; protected set; }

  public int totalCount { get; private set; }

  public int loadedCount { get; private set; }

  public string BackScene { get; set; }

  public bool isDownload { get; private set; }

  public bool isAvailable { get; private set; }

  private IEnumerator Start()
  {
    if (!this.isActive)
      yield return (object) this.StartInit();
  }

  private void OnEnable()
  {
    if (this.isActive)
      return;
    this.StartCoroutine(this.StartInit());
  }

  private void OnDisbale() => this.isActive = false;

  public void RegisterDownloadList(AssetPreDownloadList list)
  {
    if (list == null || this.myList.Contains(list))
      return;
    list.Init();
    list.m_OnStartCheckDownload += new AssetPreDownloadList.OnStartCheckDownload(this.OnStartCheckDownload);
    list.m_OnUpdateCheckDownload += new AssetPreDownloadList.OnUpdateCheckDownload(this.OnUpdateCheckDownload);
    list.m_OnFinishCheckDownLoad += new AssetPreDownloadList.OnFinishCheckDownload(this.OnFinishCheckDownload);
    this.myList.Add(list);
    this.countPackages.Add(list, 0);
  }

  public IEnumerator Check(System.Action callback)
  {
    if (this.myList.Count == 0)
    {
      this.CurrentState = AssetPreDownloadManager.DownloadState.End;
    }
    else
    {
      this.CurrentState = AssetPreDownloadManager.DownloadState.CheckVersion;
      this.isNewData = false;
      this.checkedListCount = 0;
      this.loading_packages.Clear();
      this.hash_packages.Clear();
      this.ResetCount();
      int index = 0;
      for (int count = this.myList.Count; index < count; ++index)
      {
        if (this.myList[index].isAvailable)
          this.myList[index].Check((MonoBehaviour) this);
      }
      while (this.checkedListCount < this.myList.Count)
        yield return (object) null;
      System.Action action = callback;
      if (action != null)
        action();
    }
  }

  public IEnumerator Setup(System.Action callback)
  {
    int i = 0;
    for (int n = this.myList.Count; i < n; ++i)
      yield return (object) this.myList[i].Setup();
    System.Action action = callback;
    if (action != null)
      action();
  }

  public void Download()
  {
    this.SetStopFlag(false);
    this.StartCoroutine(this.DownloadAsset());
  }

  public void Stop() => this.SetStopFlag(true);

  public bool IsNewDownloadData()
  {
    return this.isNewData || this.CurrentState == AssetPreDownloadManager.DownloadState.Downloading || this.CurrentState == AssetPreDownloadManager.DownloadState.Ready;
  }

  public bool IsReadyDownload()
  {
    return this.CurrentState == AssetPreDownloadManager.DownloadState.Downloading || this.CurrentState == AssetPreDownloadManager.DownloadState.Ready;
  }

  public bool IsWaitCheckVersion()
  {
    return this.CurrentState == AssetPreDownloadManager.DownloadState.None || this.CurrentState == AssetPreDownloadManager.DownloadState.Avai || this.CurrentState == AssetPreDownloadManager.DownloadState.CheckVersion;
  }

  public bool IsFinishDownload() => this.CurrentState == AssetPreDownloadManager.DownloadState.End;

  private IEnumerator StartInit()
  {
    this.CurrentState = AssetPreDownloadManager.DownloadState.None;
    this.isAvailable = false;
    this.isActive = true;
    while (!MonoBehaviourSingleton<GameSceneManager>.IsValid())
      yield return (object) null;
    while (string.IsNullOrEmpty(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName()))
      yield return (object) null;
    while (!MonoBehaviourSingleton<UserInfoManager>.IsValid() || MonoBehaviourSingleton<UserInfoManager>.I.userStatus == null || !MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady)
      yield return (object) null;
    bool flag = TutorialStep.HasAllTutorialCompleted() && (MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_MAINSTATUS) || MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.UPGRADE_ITEM));
    if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "HomeScene" && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "ClanScene" && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "Lounge" || !flag)
    {
      ((Behaviour) MonoBehaviourSingleton<AssetPreDownloadManager>.I).enabled = false;
      this.isActive = false;
    }
    else
    {
      this.isAvailable = true;
      this.CurrentState = AssetPreDownloadManager.DownloadState.Avai;
      this.AddLists();
      bool fn = false;
      yield return (object) this.Setup((System.Action) (() => fn = true));
      this.BackScene = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName();
      while (!fn)
        yield return (object) null;
      this.StartCoroutine(this.Check((System.Action) (() =>
      {
        if (this.loading_packages.Count > 0)
        {
          this.CurrentState = AssetPreDownloadManager.DownloadState.Ready;
        }
        else
        {
          this.CurrentState = AssetPreDownloadManager.DownloadState.End;
          this.FinishDownload();
        }
        AssetPreDownloadManager.OnFinishCheck onFinishCheck = this.m_OnFinishCheck;
        if (onFinishCheck == null)
          return;
        onFinishCheck(this.CurrentState == AssetPreDownloadManager.DownloadState.Ready);
      })));
    }
  }

  private IEnumerator DownloadAsset()
  {
    if (this.loading_packages == null)
    {
      AssetPreDownloadManager.OnFinishDownload onFinishDownload = this.m_OnFinishDownload;
      if (onFinishDownload != null)
        onFinishDownload();
    }
    else
    {
      this.isDownload = true;
      int currentState = (int) this.CurrentState;
      this.CurrentState = AssetPreDownloadManager.DownloadState.Downloading;
      LoadingQueue loading_queue = new LoadingQueue((MonoBehaviour) this);
      yield return (object) new WaitForSeconds(0.5f);
      if (this.totalCount > 0)
      {
        List<AssetPreDownloadList.LoadPackage> load_packages = this.loading_packages;
        List<LoadObject> loading_list = new List<LoadObject>();
        int count = 0;
        float currentSize = 0.0f;
        if (load_packages != null)
        {
          bool waitOne = false;
          while (this.loadedCount < this.totalCount)
          {
            yield return (object) null;
            if (this.stopFlag)
            {
              while (loading_list.Count > 0)
              {
                int index = 0;
                for (int count1 = loading_list.Count; index < count1; ++index)
                {
                  if (!loading_list[index].isLoading)
                  {
                    loading_list[index] = (LoadObject) null;
                    loading_list.RemoveAt(index);
                    --index;
                    --count1;
                    this.loadedCount++;
                  }
                }
                yield return (object) null;
              }
              yield return (object) MonoBehaviourSingleton<AppMain>.I.ClearMemory(false, true);
              this.isDownload = false;
              if (this.loadedCount != this.totalCount)
              {
                AssetPreDownloadManager.OnStopDownload onStopDownload = this.m_OnStopDownload;
                if (onStopDownload != null)
                  onStopDownload();
                yield return (object) new WaitForSeconds(0.5f);
                yield break;
              }
              break;
            }
            if ((double) currentSize >= 100.0 & waitOne)
            {
              if (loading_list.Count == 0)
              {
                yield return (object) MonoBehaviourSingleton<AppMain>.I.ClearMemory(false, false);
                waitOne = false;
                currentSize = 0.0f;
              }
            }
            else
            {
              while (loading_list.Count < 10 && count < this.totalCount)
              {
                AssetPreDownloadList.LoadPackage loadPackage = load_packages[count];
                int num = ResourceManager.enableCache ? 1 : 0;
                bool downloadOnly = ResourceManager.downloadOnly;
                bool internalMode = ResourceManager.internalMode;
                ResourceManager.enableCache = false;
                ResourceManager.downloadOnly = true;
                ResourceManager.internalMode = false;
                if (!string.IsNullOrEmpty(loadPackage.packageName))
                  loading_list.Add(loading_queue.LoadAssetBundleToCache(loadPackage.category, loadPackage.packageName, true));
                ResourceManager.enableCache = num != 0;
                ResourceManager.downloadOnly = downloadOnly;
                ResourceManager.internalMode = internalMode;
                waitOne = true;
                ++count;
                currentSize += loadPackage.size;
                if ((double) currentSize >= 100.0)
                  break;
              }
            }
            int index1 = 0;
            for (int count2 = loading_list.Count; index1 < count2; ++index1)
            {
              if (!loading_list[index1].isLoading)
              {
                loading_list[index1] = (LoadObject) null;
                loading_list.RemoveAt(index1);
                --index1;
                --count2;
                this.loadedCount++;
                AssetPreDownloadManager.OnUpdateDownload onUpdateDownload = this.m_OnUpdateDownload;
                if (onUpdateDownload != null)
                  onUpdateDownload();
              }
            }
          }
        }
        load_packages = (List<AssetPreDownloadList.LoadPackage>) null;
        loading_list = (List<LoadObject>) null;
      }
      this.FinishDownload();
      yield return (object) MonoBehaviourSingleton<AppMain>.I.ClearMemory(false, true);
      yield return (object) MonoBehaviourSingleton<ResourceManager>.I.cache.CheckShaderCacheOnMemory((MonoBehaviour) this);
      this.isDownload = false;
      this.isNewData = false;
      this.CurrentState = AssetPreDownloadManager.DownloadState.End;
      AssetPreDownloadManager.OnFinishDownload onFinishDownload = this.m_OnFinishDownload;
      if (onFinishDownload != null)
        onFinishDownload();
      yield return (object) new WaitForSeconds(0.5f);
      MonoBehaviourSingleton<AppMain>.I.Reset();
    }
  }

  private void OnStartCheckDownload(AssetPreDownloadList sender, bool succeed)
  {
    if (succeed)
    {
      this.totalCheckCount += sender.totalCheckCount;
      this.isNewData = true;
    }
    ++this.resumeListCount;
    if (this.resumeListCount != this.myList.Count)
      return;
    this.CurrentState = AssetPreDownloadManager.DownloadState.Checking;
    this.ResumeCheck();
  }

  private void OnUpdateCheckDownload(AssetPreDownloadList sender)
  {
    this.checkedCount -= this.countPackages[sender];
    this.countPackages[sender] = sender.checkedCount;
    this.checkedCount += this.countPackages[sender];
    AssetPreDownloadManager.OnUpdateCheck onUpdateCheck = this.m_OnUpdateCheck;
    if (onUpdateCheck == null)
      return;
    onUpdateCheck();
  }

  private void OnFinishCheckDownload(AssetPreDownloadList sender, bool succeed)
  {
    if (succeed)
    {
      this.totalCount += sender.totalCount;
      List<AssetPreDownloadList.LoadPackage> downloadList = sender.GetDownloadList();
      for (int index = 0; index < downloadList.Count; ++index)
        this.hash_packages.Add(downloadList[index]);
    }
    ++this.checkedListCount;
    if (this.checkedListCount != this.myList.Count)
      return;
    this.loading_packages = this.hash_packages.ToList<AssetPreDownloadList.LoadPackage>();
    AssetPreDownloadManager.OnFinishCheck onFinishCheck = this.m_OnFinishCheck;
    if (onFinishCheck == null)
      return;
    onFinishCheck(this.loading_packages.Count > 0);
  }

  private void ResetCount()
  {
    int index = 0;
    for (int count = this.myList.Count; index < count; ++index)
      this.countPackages[this.myList[index]] = 0;
  }

  private void ResumeCheck()
  {
    int index = 0;
    for (int count = this.myList.Count; index < count; ++index)
      this.myList[index].ResumeCheck();
  }

  private void FinishDownload()
  {
    int index = 0;
    for (int count = this.myList.Count; index < count; ++index)
      this.myList[index].FinishDownload();
  }

  private void SetStopFlag(bool isStop) => this.stopFlag = isStop;

  private void AddLists()
  {
    this.enemyList = new EnemyPredownloadList();
    this.RegisterDownloadList((AssetPreDownloadList) this.enemyList);
    this.eventList = new EventPredownloadList();
    this.RegisterDownloadList((AssetPreDownloadList) this.eventList);
  }

  public void AddListenerDownload(
    AssetPreDownloadManager.OnUpdateDownload onDownload,
    AssetPreDownloadManager.OnStopDownload onStop,
    AssetPreDownloadManager.OnFinishDownload onFinish)
  {
    this.m_OnUpdateDownload += onDownload;
    this.m_OnStopDownload += onStop;
    this.m_OnFinishDownload += onFinish;
  }

  public void RemoveListenerDownload(
    AssetPreDownloadManager.OnUpdateDownload onDownload,
    AssetPreDownloadManager.OnStopDownload onStop,
    AssetPreDownloadManager.OnFinishDownload onFinish)
  {
    this.m_OnUpdateDownload -= onDownload;
    this.m_OnStopDownload -= onStop;
    this.m_OnFinishDownload -= onFinish;
  }

  public void AddListenerCheck(
    AssetPreDownloadManager.OnUpdateCheck onCheckDownload,
    AssetPreDownloadManager.OnFinishCheck onCheckDownloadFinish)
  {
    this.m_OnUpdateCheck = (AssetPreDownloadManager.OnUpdateCheck) null;
    this.m_OnFinishCheck = (AssetPreDownloadManager.OnFinishCheck) null;
    this.m_OnUpdateCheck = onCheckDownload;
    this.m_OnFinishCheck = onCheckDownloadFinish;
    Debug.Log((object) "Set Listener");
  }

  public void RemoveListenerCheck(
    AssetPreDownloadManager.OnUpdateCheck onCheckDownload,
    AssetPreDownloadManager.OnFinishCheck onCheckDownloadFinish)
  {
  }

  public delegate void OnStopDownload();

  public delegate void OnFinishDownload();

  public delegate void OnUpdateDownload();

  public delegate void OnFinishCheck(bool available);

  public delegate void OnUpdateCheck();

  public enum DownloadState
  {
    None,
    Avai,
    CheckVersion,
    Checking,
    Ready,
    Downloading,
    End,
  }
}
