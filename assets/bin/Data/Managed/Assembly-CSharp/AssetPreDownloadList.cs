// Decompiled with JetBrains decompiler
// Type: AssetPreDownloadList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public abstract class AssetPreDownloadList
{
  public AssetPreDownloadList.OnUpdateCheckDownload m_OnUpdateCheckDownload = (AssetPreDownloadList.OnUpdateCheckDownload) (_param1 => { });
  public AssetPreDownloadList.OnFinishCheckDownload m_OnFinishCheckDownLoad = (AssetPreDownloadList.OnFinishCheckDownload) ((_param1, _param2) => { });
  public AssetPreDownloadList.OnStartCheckDownload m_OnStartCheckDownload = (AssetPreDownloadList.OnStartCheckDownload) ((_param1, _param2) => { });
  protected bool stopFlag;
  protected bool waitFlag;
  protected List<AssetPreDownloadList.LoadPackage> loading_packages;

  public float totalFileSize { get; protected set; }

  public int totalCount { get; protected set; }

  public int checkedCount { get; protected set; }

  public int totalCheckCount { get; protected set; }

  public bool isAvailable { get; protected set; }

  public abstract void Check(MonoBehaviour mono);

  public abstract void Init();

  public abstract IEnumerator Setup();

  public abstract void FinishDownload();

  public void Stop() => this.SetStopFlag(true);

  public void ResumeCheck() => this.SetWaitFlag(false);

  public bool IsAvaiDownload() => this.totalCount > 0;

  protected bool IsValidAssetURL(RESOURCE_CATEGORY category, string packageName)
  {
    if (Object.op_Equality((Object) MonoBehaviourSingleton<ResourceManager>.I, (Object) null) || Object.op_Equality((Object) MonoBehaviourSingleton<ResourceManager>.I.manifest, (Object) null))
      return false;
    Hash128 assetBundleHash = MonoBehaviourSingleton<ResourceManager>.I.manifest.GetAssetBundleHash(MonoBehaviourSingleton<GoGameResourceManager>.I.GetFullBundleName(category.ToAssetBundleName(packageName)));
    return ((Hash128) ref assetBundleHash).isValid;
  }

  protected float GetTotalFileSize(List<string> asset_names)
  {
    return asset_names == null ? 0.0f : ResourceSizeInfo.GetAssetsSizeMB(asset_names.ToArray());
  }

  protected void SetStopFlag(bool flag) => this.stopFlag = flag;

  protected void SetWaitFlag(bool flag) => this.waitFlag = flag;

  public List<AssetPreDownloadList.LoadPackage> GetDownloadList() => this.loading_packages;

  public class LoadPackage
  {
    public RESOURCE_CATEGORY category;
    public string packageName;
    public float size;

    public override string ToString()
    {
      return $"{this.category.ToString()} - {this.packageName} - {this.size.ToString()}";
    }
  }

  public delegate void OnStartCheckDownload(AssetPreDownloadList sender, bool succeed);

  public delegate void OnFinishCheckDownload(AssetPreDownloadList sender, bool avaiDownload);

  public delegate void OnUpdateCheckDownload(AssetPreDownloadList sender);
}
