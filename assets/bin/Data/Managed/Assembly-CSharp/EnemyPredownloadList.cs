// Decompiled with JetBrains decompiler
// Type: EnemyPredownloadList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class EnemyPredownloadList : AssetPreDownloadList
{
  private const string SAVE_KEY = "ENEMY_ASSET_VERSION";
  private const int THRESHOLE_SLEEP = 5;
  private EnemyPredownloadTable enemyInfo;
  private int Version;

  public override void Init()
  {
    this.enemyInfo = (EnemyPredownloadTable) null;
    this.Version = 0;
    this.isAvailable = false;
  }

  public override IEnumerator Setup()
  {
    while (!Singleton<EnemyTable>.IsValid() || !Singleton<EnemyTable>.I.IsAvailable())
      yield return (object) null;
    this.isAvailable = true;
  }

  public override void Check(MonoBehaviour mono)
  {
    this.SetStopFlag(false);
    mono.StartCoroutine(this.CheckDownloadFiles());
  }

  public override void FinishDownload() => PlayerPrefs.SetInt("ENEMY_ASSET_VERSION", this.Version);

  private IEnumerator CheckDownloadFiles()
  {
    if (Object.op_Equality((Object) MonoBehaviourSingleton<ResourceManager>.I.sizeInfoManifest, (Object) null) && !ResourceSizeInfo.IsValid())
      yield return (object) ResourceSizeInfo.ReInit();
    yield return (object) this.LoadEnemyPredownloadData((Action<EnemyPredownloadTable>) (o => this.enemyInfo = o));
    EnemyPredownloadTable table = this.enemyInfo;
    if (Object.op_Equality((Object) table, (Object) null))
    {
      AssetPreDownloadList.OnStartCheckDownload startCheckDownload = this.m_OnStartCheckDownload;
      if (startCheckDownload != null)
        startCheckDownload((AssetPreDownloadList) this, false);
    }
    else
    {
      this.loading_packages = (List<AssetPreDownloadList.LoadPackage>) null;
      this.totalFileSize = 0.0f;
      this.totalCount = 0;
      if (Object.op_Equality((Object) table, (Object) null))
      {
        AssetPreDownloadList.OnStartCheckDownload startCheckDownload = this.m_OnStartCheckDownload;
        if (startCheckDownload != null)
          startCheckDownload((AssetPreDownloadList) this, false);
        AssetPreDownloadList.OnFinishCheckDownload finishCheckDownLoad = this.m_OnFinishCheckDownLoad;
        if (finishCheckDownLoad != null)
          finishCheckDownLoad((AssetPreDownloadList) this, false);
      }
      else
      {
        int num = PlayerPrefs.GetInt("ENEMY_ASSET_VERSION", 0);
        this.Version = table.Version;
        int version = this.Version;
        if (num == version)
        {
          AssetPreDownloadList.OnStartCheckDownload startCheckDownload = this.m_OnStartCheckDownload;
          if (startCheckDownload != null)
            startCheckDownload((AssetPreDownloadList) this, false);
          AssetPreDownloadList.OnFinishCheckDownload finishCheckDownLoad = this.m_OnFinishCheckDownLoad;
          if (finishCheckDownLoad != null)
            finishCheckDownLoad((AssetPreDownloadList) this, false);
        }
        else
        {
          this.totalCheckCount = table.EnemyDatas.Count;
          System.Type category_type = typeof (RESOURCE_CATEGORY);
          this.loading_packages = new List<AssetPreDownloadList.LoadPackage>();
          this.checkedCount = 0;
          this.SetWaitFlag(true);
          AssetPreDownloadList.OnStartCheckDownload startCheckDownload = this.m_OnStartCheckDownload;
          if (startCheckDownload != null)
            startCheckDownload((AssetPreDownloadList) this, this.totalCheckCount > 0);
          while (this.waitFlag)
            yield return (object) null;
          while (this.checkedCount < this.totalCheckCount)
          {
            EnemyPredownloadTable.Data enemyData = table.EnemyDatas[this.checkedCount];
            RESOURCE_CATEGORY category = (RESOURCE_CATEGORY) Enum.Parse(category_type, enemyData.categoryName);
            if (!MonoBehaviourSingleton<ResourceManager>.I.IsCached(category, enemyData.packageName) && this.IsValidAssetURL(category, enemyData.packageName))
            {
              AssetPreDownloadList.LoadPackage loadPackage = new AssetPreDownloadList.LoadPackage();
              loadPackage.category = category;
              loadPackage.packageName = enemyData.packageName;
              loadPackage.size = ResourceSizeInfo.ConvertBToMB(enemyData.Size);
              this.totalFileSize += loadPackage.size;
              this.loading_packages.Add(loadPackage);
            }
            ++this.checkedCount;
            AssetPreDownloadList.OnUpdateCheckDownload updateCheckDownload = this.m_OnUpdateCheckDownload;
            if (updateCheckDownload != null)
              updateCheckDownload((AssetPreDownloadList) this);
            if (this.checkedCount % 5 == 0)
              yield return (object) null;
            if (this.stopFlag)
              yield break;
          }
          if (this.loading_packages != null)
            this.totalCount = this.loading_packages.Count;
          else
            this.totalCount = 0;
          AssetPreDownloadList.OnFinishCheckDownload finishCheckDownLoad = this.m_OnFinishCheckDownLoad;
          if (finishCheckDownLoad != null)
            finishCheckDownLoad((AssetPreDownloadList) this, this.totalCount > 0);
        }
      }
    }
  }

  private IEnumerator LoadEnemyPredownloadData(Action<EnemyPredownloadTable> callback)
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
      Action<EnemyPredownloadTable> action = callback;
      if (action != null)
        action(lo.loadedObject as EnemyPredownloadTable);
      load_queue = (LoadingQueue) null;
      lo = (LoadObject) null;
    }
  }
}
