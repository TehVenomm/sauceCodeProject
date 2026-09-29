// Decompiled with JetBrains decompiler
// Type: GoGameResourceManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using MsgPack;
using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Networking;

#nullable disable
public class GoGameResourceManager : MonoBehaviourSingleton<GoGameResourceManager>
{
  public bool isLoadingVariantManifest;
  public Dictionary<int, string> variantManifest;
  public bool isLoadingServerList;

  private void Start() => this.isLoadingVariantManifest = false;

  public static string GetDefaultAssetBundleExtension() => ".dat";

  public void LoadVariantManifest() => this.StartCoroutine(this.IELoadVariantManifest());

  private IEnumerator IELoadVariantManifest()
  {
    this.isLoadingVariantManifest = true;
    this.variantManifest = (Dictionary<int, string>) null;
    string url = $"{MonoBehaviourSingleton<ResourceManager>.I.downloadURL}{"variants_manifest.bytes"}";
    Error error_code = Error.None;
    do
    {
      error_code = Error.None;
      UnityWebRequest _www = new UnityWebRequest(url);
      _www.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
      yield return (object) _www.SendWebRequest();
      string error = _www.error;
      if (string.IsNullOrEmpty(error))
      {
        if (_www.downloadHandler.data != null)
        {
          this.variantManifest = new ObjectPacker().Unpack<Dictionary<int, string>>(_www.downloadHandler.data);
        }
        else
        {
          error_code = Error.AssetLoadFailed;
          Log.Error(LOG.RESOURCE, _www.downloadHandler.text);
        }
      }
      else
        error_code = !error.Contains("404") ? Error.AssetLoadFailed : Error.AssetNotFound;
      _www.Dispose();
      _www = (UnityWebRequest) null;
      if (error_code != Error.None)
      {
        MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, StringTable.Format(STRING_CATEGORY.COMMON_DIALOG, 1001U, (object) error_code), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 100U)), (Action<string>) (btn => MonoBehaviourSingleton<AppMain>.I.Reset()), true, (int) error_code);
        break;
      }
      _www = (UnityWebRequest) null;
    }
    while (error_code != Error.None);
    this.isLoadingVariantManifest = error_code != Error.None;
  }

  public string GetVariantName(RESOURCE_CATEGORY? category)
  {
    if (!category.HasValue)
      return "";
    RESOURCE_CATEGORY? nullable = category;
    RESOURCE_CATEGORY resourceCategory = RESOURCE_CATEGORY.SOUND_VOICE;
    if (nullable.GetValueOrDefault() == resourceCategory & nullable.HasValue)
      return GameSaveData.instance.voiceOption == 2 ? ".en-sd" : "." + InGameManager.voiceVariants[GameSaveData.instance.voiceOption];
    if (this.variantManifest == null || !this.variantManifest.ContainsKey((int) category.Value))
      return "";
    return this.variantManifest[(int) category.Value].Contains(InGameManager.languageVariants[GameSaveData.instance.languageOption]) ? "." + InGameManager.languageVariants[GameSaveData.instance.languageOption] : ".en-sd";
  }

  public string GetFullBundleName(string bundleName)
  {
    string[] split = bundleName.Split('.');
    if (!((IEnumerable<string>) InGameManager.languageVariants).Any<string>((Func<string, bool>) (s => split[split.Length - 1].Equals(s))))
      bundleName += this.GetVariantName(this.GetCategory(bundleName));
    return bundleName;
  }

  public RESOURCE_CATEGORY? GetCategory(string fullBundleName)
  {
    fullBundleName = fullBundleName.ToUpper();
    string str = ((IEnumerable<string>) Enum.GetNames(typeof (RESOURCE_CATEGORY))).FirstOrDefault<string>((Func<string, bool>) (s => fullBundleName.Contains(s + "/")));
    return string.IsNullOrEmpty(str) ? new RESOURCE_CATEGORY?() : new RESOURCE_CATEGORY?((RESOURCE_CATEGORY) Enum.Parse(typeof (RESOURCE_CATEGORY), str));
  }

  public string GetBundleNameWithoutVariant(string fullBundleName)
  {
    string[] splits = fullBundleName.Split('.');
    return ((IEnumerable<string>) InGameManager.languageVariants).Any<string>((Func<string, bool>) (s => splits[splits.Length - 1].Equals(s))) ? fullBundleName.Remove(fullBundleName.LastIndexOf(".")) : fullBundleName;
  }

  public void LoadServerList() => this.StartCoroutine(this.IELoadServerList());

  private IEnumerator IELoadServerList()
  {
    string www_url = NetworkManager.TABLE_HOST;
    www_url += "serverlisttable.dat";
    this.isLoadingServerList = true;
    Error error_code = Error.None;
    do
    {
      error_code = Error.None;
      UnityWebRequest _www = new UnityWebRequest(www_url);
      _www.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
      yield return (object) _www.SendWebRequest();
      string error = _www.error;
      if (string.IsNullOrEmpty(error))
      {
        if (_www.downloadHandler.data != null)
        {
          Singleton<ServerListTable>.Create();
          Singleton<ServerListTable>.I.CreateTable(DataTableManager.DecompressToString(_www.downloadHandler.data));
        }
        else
        {
          error_code = Error.AssetLoadFailed;
          Log.Error(LOG.RESOURCE, _www.downloadHandler.text);
        }
      }
      else
        error_code = !error.Contains("404") ? Error.AssetLoadFailed : Error.AssetNotFound;
      _www.Dispose();
      _www = (UnityWebRequest) null;
      if (error_code != Error.None)
      {
        MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, StringTable.Format(STRING_CATEGORY.COMMON_DIALOG, 1001U, (object) error_code), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 100U)), (Action<string>) (btn => MonoBehaviourSingleton<AppMain>.I.Reset()), true, (int) error_code);
        break;
      }
      _www = (UnityWebRequest) null;
    }
    while (error_code != Error.None);
    if (error_code == Error.None)
      this.isLoadingServerList = false;
    else
      this.isLoadingVariantManifest = true;
  }
}
