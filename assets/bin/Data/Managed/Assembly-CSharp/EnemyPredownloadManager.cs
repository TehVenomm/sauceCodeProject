// Decompiled with JetBrains decompiler
// Type: EnemyPredownloadManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class EnemyPredownloadManager : MonoBehaviourSingleton<EnemyPredownloadManager>
{
  private List<EnemyPredownloadManager.LoadInfo> LoadInfoList;
  private List<EnemyPredownloadManager.LoadPackage> loading_packages;
  private int Version;
  private bool isDownload;
  private bool isFN;
  private float totalSize;
  private float loadedSize;
  private bool stopFlags;
  private EnemyPredownloadManager.OnDownLoadPackage m_OnDownloadPackage = (EnemyPredownloadManager.OnDownLoadPackage) (() => { });
  private EnemyPredownloadManager.OnStopDownloadPackage m_OnStopDownloadPackage = (EnemyPredownloadManager.OnStopDownloadPackage) (_param1 => { });
  private EnemyPredownloadManager.OnCheckDownload m_OnCheckDownload = (EnemyPredownloadManager.OnCheckDownload) (() => { });
  private EnemyPredownloadManager.OnCheckDownloadFinish m_OnCheckDownLoadFinish = (EnemyPredownloadManager.OnCheckDownloadFinish) (_param1 => { });
  private EnemyPredownloadTable enemyInfo;

  public static void Stop()
  {
    if (!MonoBehaviourSingleton<EnemyPredownloadManager>.IsValid())
      return;
    MonoBehaviourSingleton<EnemyPredownloadManager>.I.SetStopFlag(true);
  }

  public int totalCount { get; private set; }

  public int loadedCount { get; private set; }

  public int checkedCount { get; private set; }

  public int totalPackage { get; private set; }

  public EnemyPredownloadManager.DownloadState CurrentState { get; private set; }

  public bool isLoading => this.totalCount == 0 || this.loadedCount < this.totalCount;

  public bool IsAvai { get; private set; }

  public bool IsAvaiDownload()
  {
    if (this.IsAvai)
      return true;
    if (this.CurrentState != EnemyPredownloadManager.DownloadState.Downloading && this.CurrentState != EnemyPredownloadManager.DownloadState.ReadyDownload)
      return false;
    if (this.totalCount != 0)
      return true;
    this.CurrentState = EnemyPredownloadManager.DownloadState.FinishChecking;
    return false;
  }

  public float FileSize
  {
    get => Mathf.Clamp(this.totalSize - this.loadedSize, 0.0f, float.PositiveInfinity);
  }

  public string BackScene { get; private set; }

  protected override void Awake()
  {
    this.CurrentState = EnemyPredownloadManager.DownloadState.Init;
    this.checkedCount = 0;
    this.totalSize = 0.0f;
    this.loadedSize = 0.0f;
    this.totalCount = 0;
    this.loadedCount = 0;
    this.IsAvai = false;
    this.BackScene = "";
    base.Awake();
  }

  private IEnumerator Start()
  {
    this.CurrentState = EnemyPredownloadManager.DownloadState.Ready;
    while (!MonoBehaviourSingleton<GameSceneManager>.IsValid())
      yield return (object) null;
    while (string.IsNullOrEmpty(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName()))
      yield return (object) null;
    while (!MonoBehaviourSingleton<UserInfoManager>.IsValid() || MonoBehaviourSingleton<UserInfoManager>.I.userStatus == null || !MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady)
      yield return (object) null;
    bool flag = TutorialStep.HasAllTutorialCompleted() && (MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_MAINSTATUS) || MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.UPGRADE_ITEM));
    if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "HomeScene" && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "ClanScene" && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "Lounge" || !flag)
    {
      ((Behaviour) MonoBehaviourSingleton<EnemyPredownloadManager>.I).enabled = false;
    }
    else
    {
      this.BackScene = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName();
      while (!Singleton<EnemyTable>.IsValid() || !Singleton<EnemyTable>.I.IsAvailable())
        yield return (object) null;
      this.CheckDownload((Action<bool>) (isSucceed => this.IsAvai = isSucceed));
    }
  }

  private void OnEnable()
  {
    if (this.CurrentState == EnemyPredownloadManager.DownloadState.Ready)
    {
      this.StartCoroutine(this.Start());
    }
    else
    {
      if (this.CurrentState != EnemyPredownloadManager.DownloadState.CheckingFile)
        return;
      this.StartCoroutine(this.ResumeCheckDownloadFiles());
    }
  }

  private void SetStopFlag(bool is_stop) => this.stopFlags = is_stop;

  private IEnumerator LoadEnemyPredownloadData()
  {
    while (!MonoBehaviourSingleton<ResourceManager>.IsValid())
      yield return (object) null;
    while (Object.op_Equality((Object) MonoBehaviourSingleton<ResourceManager>.I.sizeInfoManifest, (Object) null))
      yield return (object) null;
    Hash128 hash128 = new Hash128();
    if (Object.op_Inequality((Object) MonoBehaviourSingleton<ResourceManager>.I.sizeInfoManifest, (Object) null))
      hash128 = MonoBehaviourSingleton<ResourceManager>.I.sizeInfoManifest.GetAssetBundleHash(RESOURCE_CATEGORY.ASSETBUNDLEINFO.ToAssetBundleName());
    if (((Hash128) ref hash128).isValid)
    {
      LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) MonoBehaviourSingleton<AppMain>.I);
      LoadObject lo = load_queue.Load(RESOURCE_CATEGORY.ASSETBUNDLEINFO, "EnemyPredownloadTable");
      while (load_queue.IsLoading())
        yield return (object) null;
      this.enemyInfo = lo.loadedObject as EnemyPredownloadTable;
      load_queue = (LoadingQueue) null;
      lo = (LoadObject) null;
    }
  }

  private IEnumerator CheckDownloadFiles(Action<bool> onFinish = null)
  {
    this.CurrentState = EnemyPredownloadManager.DownloadState.CheckingFile;
    if (Object.op_Equality((Object) MonoBehaviourSingleton<ResourceManager>.I.sizeInfoManifest, (Object) null) && !ResourceSizeInfo.IsValid())
      yield return (object) ResourceSizeInfo.ReInit();
    yield return (object) this.LoadEnemyPredownloadData();
    EnemyPredownloadTable table = this.enemyInfo;
    if (!Object.op_Equality((Object) table, (Object) null))
    {
      this.loading_packages = (List<EnemyPredownloadManager.LoadPackage>) null;
      this.IsAvai = false;
      this.totalSize = 0.0f;
      this.loadedSize = 0.0f;
      this.totalCount = 0;
      this.loadedCount = 0;
      this.isDownload = false;
      if (Object.op_Equality((Object) table, (Object) null))
      {
        this.CurrentState = EnemyPredownloadManager.DownloadState.FinishChecking;
        this.isFN = true;
        if (onFinish != null)
          onFinish(false);
        if (this.m_OnCheckDownLoadFinish != null)
          this.m_OnCheckDownLoadFinish(false);
      }
      else
      {
        int id = PlayerPrefs.GetInt("ENEMY_ASSET_VERSION", 0);
        if (id == table.Version)
        {
          this.CurrentState = EnemyPredownloadManager.DownloadState.FinishChecking;
          this.isFN = true;
          if (onFinish != null)
            onFinish(false);
          if (this.m_OnCheckDownLoadFinish != null)
            this.m_OnCheckDownLoadFinish(false);
        }
        else
        {
          this.IsAvai = true;
          if (this.m_OnCheckDownLoadFinish != null)
            this.m_OnCheckDownLoadFinish(true);
          this.Version = table.Version;
          this.totalPackage = table.EnemyDatas.Count;
          System.Type category_type = typeof (RESOURCE_CATEGORY);
          this.loading_packages = new List<EnemyPredownloadManager.LoadPackage>();
          this.checkedCount = 0;
          while (this.checkedCount < this.totalPackage)
          {
            EnemyPredownloadTable.Data enemyData = table.EnemyDatas[this.checkedCount];
            RESOURCE_CATEGORY category = (RESOURCE_CATEGORY) Enum.Parse(category_type, enemyData.categoryName);
            if (!MonoBehaviourSingleton<ResourceManager>.I.IsCached(category, enemyData.packageName) && this.IsValidAssetURL(category, enemyData.packageName))
            {
              EnemyPredownloadManager.LoadPackage loadPackage = new EnemyPredownloadManager.LoadPackage();
              loadPackage.category = category;
              loadPackage.packageName = enemyData.packageName;
              loadPackage.size = ResourceSizeInfo.ConvertBToMB(enemyData.Size);
              this.totalSize += loadPackage.size;
              this.loading_packages.Add(loadPackage);
            }
            this.checkedCount++;
            if (this.m_OnCheckDownload != null)
              this.m_OnCheckDownload();
            if (this.checkedCount % 5 == 0)
              yield return (object) null;
            if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "HomeScene" && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "ClanScene" && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "Lounge" && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "EnemyDownloadScene")
            {
              ((Behaviour) MonoBehaviourSingleton<EnemyPredownloadManager>.I).enabled = false;
              yield break;
            }
          }
          if (this.loading_packages != null && this.loading_packages.Count > 0)
            this.IsAvai = true;
          Debug.Log((object) $"FINISH CHECK DONWLOAD FILES {(object) this.loading_packages.Count} WITH SIZE {(object) this.totalSize}");
          this.totalCount = this.loading_packages.Count;
          if (this.totalCount == 0)
          {
            PlayerPrefs.SetInt("ENEMY_ASSET_VERSION", id);
            this.isFN = true;
            this.IsAvai = false;
          }
          this.CurrentState = EnemyPredownloadManager.DownloadState.FinishChecking;
          if (this.totalCount > 0)
            this.CurrentState = EnemyPredownloadManager.DownloadState.ReadyDownload;
          if (onFinish != null)
            onFinish(true);
          if (this.m_OnCheckDownLoadFinish != null)
            this.m_OnCheckDownLoadFinish(this.totalCount > 0);
        }
      }
    }
  }

  private IEnumerator ResumeCheckDownloadFiles()
  {
    this.CurrentState = EnemyPredownloadManager.DownloadState.CheckingFile;
    if (!ResourceSizeInfo.IsValid())
      yield return (object) ResourceSizeInfo.Init();
    if (Object.op_Equality((Object) this.enemyInfo, (Object) null))
      yield return (object) this.LoadEnemyPredownloadData();
    EnemyPredownloadTable table = this.enemyInfo;
    if (!Object.op_Equality((Object) table, (Object) null))
    {
      this.IsAvai = false;
      this.totalSize = 0.0f;
      this.loadedSize = 0.0f;
      this.totalCount = 0;
      this.loadedCount = 0;
      this.isDownload = false;
      if (Object.op_Equality((Object) table, (Object) null))
      {
        this.CurrentState = EnemyPredownloadManager.DownloadState.FinishChecking;
        this.isFN = true;
        if (this.m_OnCheckDownLoadFinish != null)
          this.m_OnCheckDownLoadFinish(false);
      }
      else
      {
        int id = PlayerPrefs.GetInt("ENEMY_ASSET_VERSION", 0);
        if (id == table.Version)
        {
          this.CurrentState = EnemyPredownloadManager.DownloadState.FinishChecking;
          this.isFN = true;
          if (this.m_OnCheckDownLoadFinish != null)
            this.m_OnCheckDownLoadFinish(false);
        }
        else
        {
          this.IsAvai = true;
          this.Version = table.Version;
          this.totalPackage = table.EnemyDatas.Count;
          System.Type category_type = typeof (RESOURCE_CATEGORY);
          while (this.checkedCount < this.totalPackage)
          {
            EnemyPredownloadTable.Data enemyData = table.EnemyDatas[this.checkedCount];
            RESOURCE_CATEGORY category = (RESOURCE_CATEGORY) Enum.Parse(category_type, enemyData.categoryName);
            if (!MonoBehaviourSingleton<ResourceManager>.I.IsCached(category, enemyData.packageName) && this.IsValidAssetURL(category, enemyData.packageName))
            {
              EnemyPredownloadManager.LoadPackage loadPackage = new EnemyPredownloadManager.LoadPackage();
              loadPackage.category = category;
              loadPackage.packageName = enemyData.packageName;
              loadPackage.size = ResourceSizeInfo.ConvertBToMB(enemyData.Size);
              this.totalSize += loadPackage.size;
              this.loading_packages.Add(loadPackage);
            }
            this.checkedCount++;
            if (this.m_OnCheckDownload != null)
              this.m_OnCheckDownload();
            if (this.checkedCount % 5 == 0)
              yield return (object) null;
            if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "HomeScene" && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "ClanScene" && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "Lounge" && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "EnemyDownloadScene")
            {
              ((Behaviour) MonoBehaviourSingleton<EnemyPredownloadManager>.I).enabled = false;
              yield break;
            }
          }
          if (this.loading_packages != null && this.loading_packages.Count > 0)
            this.IsAvai = true;
          Debug.Log((object) $"FINISH CHECK DONWLOAD FILES {(object) this.loading_packages.Count} WITH SIZE {(object) this.totalSize}");
          this.totalCount = this.loading_packages.Count;
          if (this.totalCount == 0)
          {
            PlayerPrefs.SetInt("ENEMY_ASSET_VERSION", id);
            this.isFN = true;
            this.IsAvai = false;
          }
          this.CurrentState = EnemyPredownloadManager.DownloadState.FinishChecking;
          if (this.totalCount > 0)
            this.CurrentState = EnemyPredownloadManager.DownloadState.ReadyDownload;
          if (this.m_OnCheckDownLoadFinish != null)
            this.m_OnCheckDownLoadFinish(this.totalCount > 0);
        }
      }
    }
  }

  private IEnumerator StartDownload()
  {
    if (this.loading_packages == null)
    {
      if (this.BackScene == "ClanScene")
        MonoBehaviourSingleton<GameSceneManager>.I.AddHighForceChangeScene("Clan", "ClanTop");
      else if (this.BackScene == "Lounge")
        MonoBehaviourSingleton<GameSceneManager>.I.AddHighForceChangeScene("Lounge", "LoungeTop");
      if (this.m_OnStopDownloadPackage != null)
        this.m_OnStopDownloadPackage(false);
    }
    else
    {
      this.isDownload = true;
      this.CurrentState = EnemyPredownloadManager.DownloadState.Downloading;
      Debug.Log((object) "START LOAD ENEMY DATA ");
      LoadingQueue loading_queue = new LoadingQueue((MonoBehaviour) this);
      yield return (object) new WaitForSeconds(0.5f);
      if (this.totalCount > 0)
      {
        List<EnemyPredownloadManager.LoadPackage> load_packages = this.loading_packages;
        List<LoadObject> loading_list = new List<LoadObject>();
        int count = 0;
        if (load_packages != null)
        {
          bool waitOne = false;
          while (this.loadedCount < this.totalCount)
          {
            yield return (object) null;
            if (this.stopFlags)
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
              if (this.loadedCount == this.totalCount)
              {
                PlayerPrefs.SetInt("ENEMY_ASSET_VERSION", this.Version);
                this.isFN = true;
                this.IsAvai = false;
                if (this.m_OnStopDownloadPackage != null)
                  this.m_OnStopDownloadPackage(true);
              }
              else if (this.m_OnStopDownloadPackage != null)
                this.m_OnStopDownloadPackage(false);
              yield return (object) new WaitForSeconds(0.5f);
              yield break;
            }
            if (count % 100 == 0 & waitOne)
            {
              if (loading_list.Count == 0)
              {
                yield return (object) MonoBehaviourSingleton<AppMain>.I.ClearMemory(false, false);
                waitOne = false;
              }
            }
            else
            {
              while (loading_list.Count < 10 && count < this.totalCount)
              {
                EnemyPredownloadManager.LoadPackage loadPackage = load_packages[count];
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
                if (count % 100 == 0)
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
                if (this.m_OnDownloadPackage != null)
                  this.m_OnDownloadPackage();
              }
            }
          }
        }
        Debug.Log((object) ("FINISH LOAD ENEMY DATA " + (object) this.loadedCount));
        load_packages = (List<EnemyPredownloadManager.LoadPackage>) null;
        loading_list = (List<LoadObject>) null;
      }
      PlayerPrefs.SetInt("ENEMY_ASSET_VERSION", this.Version);
      yield return (object) MonoBehaviourSingleton<AppMain>.I.ClearMemory(false, true);
      yield return (object) MonoBehaviourSingleton<ResourceManager>.I.cache.CheckShaderCacheOnMemory((MonoBehaviour) this);
      Debug.Log((object) "END LOAD");
      this.isDownload = false;
      this.isFN = true;
      this.IsAvai = false;
      this.CurrentState = EnemyPredownloadManager.DownloadState.FinishDownload;
      if (this.BackScene == "ClanScene")
        MonoBehaviourSingleton<GameSceneManager>.I.AddHighForceChangeScene("Clan", "ClanTop");
      else if (this.BackScene == "Lounge")
        MonoBehaviourSingleton<GameSceneManager>.I.AddHighForceChangeScene("Lounge", "LoungeTop");
      if (this.m_OnStopDownloadPackage != null)
        this.m_OnStopDownloadPackage(true);
      yield return (object) new WaitForSeconds(0.5f);
      MonoBehaviourSingleton<AppMain>.I.Reset();
    }
  }

  private float GetPackageTotalSizeMB(string[] loadPackageName)
  {
    return !ResourceSizeInfo.IsValid() ? 0.0f : ResourceSizeInfo.GetAssetsSizeMB(loadPackageName);
  }

  public void CheckDownload(Action<bool> callback)
  {
    this.StartCoroutine(this.CheckDownloadFiles(callback));
  }

  public void ProceedDownload()
  {
    this.SetStopFlag(false);
    if (this.isDownload)
      return;
    this.StartCoroutine(this.StartDownload());
  }

  private bool IsValidAssetURL(RESOURCE_CATEGORY category, string packageName)
  {
    if (Object.op_Equality((Object) MonoBehaviourSingleton<ResourceManager>.I, (Object) null) || Object.op_Equality((Object) MonoBehaviourSingleton<ResourceManager>.I.manifest, (Object) null))
      return false;
    Hash128 assetBundleHash = MonoBehaviourSingleton<ResourceManager>.I.manifest.GetAssetBundleHash(MonoBehaviourSingleton<GoGameResourceManager>.I.GetFullBundleName(category.ToAssetBundleName(packageName)));
    return ((Hash128) ref assetBundleHash).isValid;
  }

  public void AddListenerDownload(
    EnemyPredownloadManager.OnDownLoadPackage onDownload = null,
    EnemyPredownloadManager.OnStopDownloadPackage onStop = null)
  {
    this.m_OnDownloadPackage += onDownload;
    this.m_OnStopDownloadPackage += onStop;
  }

  public void RemoveListenerDownload(
    EnemyPredownloadManager.OnDownLoadPackage onDownload = null,
    EnemyPredownloadManager.OnStopDownloadPackage onStop = null)
  {
    this.m_OnDownloadPackage -= onDownload;
    this.m_OnStopDownloadPackage -= onStop;
  }

  public void AddListenerCheck(
    EnemyPredownloadManager.OnCheckDownload onCheckDownload,
    EnemyPredownloadManager.OnCheckDownloadFinish onCheckDownloadFinish)
  {
    this.m_OnCheckDownload += onCheckDownload;
    this.m_OnCheckDownLoadFinish += onCheckDownloadFinish;
  }

  public void RemoveListenerCheck(
    EnemyPredownloadManager.OnCheckDownload onCheckDownload,
    EnemyPredownloadManager.OnCheckDownloadFinish onCheckDownloadFinish)
  {
    this.m_OnCheckDownload -= onCheckDownload;
    this.m_OnCheckDownLoadFinish -= onCheckDownloadFinish;
  }

  private class LoadInfo
  {
    public int iconId;
    public string modelname;
    public string mateName;
    public string animName;
  }

  public enum DownloadState
  {
    Init,
    Ready,
    CheckingFile,
    ReadyDownload,
    FinishChecking,
    Downloading,
    FinishDownload,
  }

  private class LoadPackage
  {
    public RESOURCE_CATEGORY category;
    public string packageName;
    public float size;
  }

  public delegate void OnCheckDownloadFinish(bool avaiDownload);

  public delegate void OnCheckDownload();

  public delegate void OnDownLoadPackage();

  public delegate void OnStopDownloadPackage(bool succeed);
}
