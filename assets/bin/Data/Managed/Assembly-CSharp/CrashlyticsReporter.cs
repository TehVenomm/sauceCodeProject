// Decompiled with JetBrains decompiler
// Type: CrashlyticsReporter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class CrashlyticsReporter
{
  private static bool isEnable;

  public static void EnableReport()
  {
    if (CrashlyticsReporter.isEnable)
      return;
    Application.logMessageReceived += new Application.LogCallback(CrashlyticsReporter.HandleLog);
    CrashlyticsReporter.isEnable = true;
  }

  private static void HandleLog(string log, string stack, LogType type)
  {
    if (type != 4)
      return;
    CrashlyticsReporter.ReportException(log, stack);
  }

  public static void SetUserInfo(Network.UserInfo userInfo)
  {
    if (!CrashlyticsReporter.isEnable)
      return;
    CrashlyticsWrapper.SetUserId(userInfo.id);
    CrashlyticsWrapper.SetUserName(userInfo.name);
  }

  public static void SetAssetIndex(int index)
  {
    if (!CrashlyticsReporter.isEnable)
      return;
    CrashlyticsWrapper.SetInt("AssetIndex", index);
  }

  public static void SetManifestVersion(int ver)
  {
    if (!CrashlyticsReporter.isEnable)
      return;
    CrashlyticsWrapper.SetInt("ManifestVersion", ver);
  }

  public static void SetAPIRequest(string url)
  {
    if (!CrashlyticsReporter.isEnable)
      return;
    CrashlyticsWrapper.SetString("APIRequestURL", url);
  }

  public static void SetAPIRequestStatus(bool requesting)
  {
    if (!CrashlyticsReporter.isEnable)
      return;
    CrashlyticsWrapper.SetBool("APIRequesting", requesting);
  }

  public static void SetSceneInfo(string sceneName, string sectionName)
  {
    if (!CrashlyticsReporter.isEnable)
      return;
    CrashlyticsWrapper.SetString("SceneName", sceneName);
    CrashlyticsWrapper.SetString("SectionName", sectionName);
  }

  public static void SetSceneStatus(bool isChanging)
  {
    if (!CrashlyticsReporter.isEnable)
      return;
    CrashlyticsWrapper.SetBool("SceneChaning", isChanging);
  }

  public static void SetLoadingBundle(string bundleName)
  {
    if (!CrashlyticsReporter.isEnable)
      return;
    CrashlyticsWrapper.SetString("LoadingBundle", bundleName);
  }

  private static void ReportException(string log, string stack)
  {
    if (!CrashlyticsReporter.isEnable)
      return;
    log = log ?? "";
    stack = stack ?? "";
    CrashlyticsWrapper.ReportException($"{log}\n{stack}");
  }
}
