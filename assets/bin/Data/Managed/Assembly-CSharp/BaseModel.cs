// Decompiled with JetBrains decompiler
// Type: BaseModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;

#nullable disable
public class BaseModel
{
  public int error;
  public string currentTime = "";
  public int tutorial = -1;
  public List<BaseModel.AssetVersionInfo> assetVersion;
  public int assetManifestVersion;
  public int assetIndex;
  public int tableIndex;
  public int tableManifestVersion;
  public bool usageLimitMode;
  public int infoError;
  public bool appClose;
  public string closedNotice = "";
  public bool openRefundForm;
  public XorInt vm = new XorInt(1);
  public List<BaseModel.RecommendUpdate> recommendUpdate;
  public List<BaseModelDiff> diff;

  public Error Error
  {
    get => (Error) this.error;
    set => this.error = (int) value;
  }

  public void Apply()
  {
    int tutorial = this.tutorial;
    if (this.assetManifestVersion > 0 && MonoBehaviourSingleton<ResourceManager>.IsValid())
      MonoBehaviourSingleton<ResourceManager>.I.manifestVersion = this.assetManifestVersion;
    if (this.assetIndex > 0 && MonoBehaviourSingleton<ResourceManager>.IsValid())
      MonoBehaviourSingleton<ResourceManager>.I.assetIndex = this.assetIndex;
    if (this.tableIndex > 0 && MonoBehaviourSingleton<ResourceManager>.IsValid())
      MonoBehaviourSingleton<ResourceManager>.I.tableIndex = this.tableIndex;
    if (this.tableManifestVersion > 0 && MonoBehaviourSingleton<DataTableManager>.IsValid())
      MonoBehaviourSingleton<DataTableManager>.I.OnReceiveTableManifestVersion(this.tableManifestVersion);
    if ((int) this.vm > 0 && MonoBehaviourSingleton<DataTableManager>.IsValid())
      MonoBehaviourSingleton<DataTableManager>.I.OnReceiveVM(this.vm);
    if (!string.IsNullOrEmpty(this.currentTime) && MonoBehaviourSingleton<TimeManager>.IsValid())
      TimeManager.SetServerTime(this.currentTime);
    if (!MonoBehaviourSingleton<AccountManager>.IsValid())
      return;
    MonoBehaviourSingleton<AccountManager>.I.appClose = this.appClose;
    MonoBehaviourSingleton<AccountManager>.I.usageLimitMode = false;
    MonoBehaviourSingleton<AccountManager>.I.closedNotice = this.closedNotice;
    MonoBehaviourSingleton<AccountManager>.I.openRefundForm = this.openRefundForm;
  }

  [Serializable]
  public class AssetVersionInfo
  {
    public string name = "";
    public int version;
  }

  public class RecommendUpdate
  {
    public bool flag;
    public string ver = "";
  }
}
