// Decompiled with JetBrains decompiler
// Type: BootProcess
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class BootProcess : MonoBehaviour
{
  public const string STORAGE_PERMISSION = "android.permission.WRITE_EXTERNAL_STORAGE";
  private bool isWaitGrantedPermission = true;
  private bool isGrantedPermission;
  private const float ANALYTIC_TIMEOUT = 5f;
  private static bool isAnalytics = true;
  private static string analyticsString;

  private bool CheckPermissions()
  {
    return Application.platform != 11 || AndroidPermissionsManager.IsPermissionGranted("android.permission.WRITE_EXTERNAL_STORAGE");
  }

  public void OnGrantButtonPress()
  {
    AndroidPermissionsManager.RequestPermission(new string[1]
    {
      "android.permission.WRITE_EXTERNAL_STORAGE"
    }, new AndroidPermissionCallback((Action<string>) (grantedPermission =>
    {
      this.isWaitGrantedPermission = false;
      this.isGrantedPermission = true;
    }), (Action<string>) (deniedPermission =>
    {
      this.isWaitGrantedPermission = false;
      this.isGrantedPermission = false;
    })));
  }

  private IEnumerator Start()
  {
    ResourceManager.internalMode = true;
    GameSaveData.Load();
    if (PlayerPrefs.GetInt("INIT_SE_VOLUME", 0) == 0)
    {
      if ((double) GameSaveData.instance.volumeSE == 1.0)
        GameSaveData.instance.volumeSE = 0.5f;
      PlayerPrefs.SetInt("INIT_SE_VOLUME", 1);
    }
    if (string.IsNullOrEmpty(GameSaveData.instance.graphicOptionKey))
    {
      if (NetworkNative.isRunOnRazerPhone())
      {
        GameSaveData.instance.graphicOptionKey = "highest";
        Application.targetFrameRate = 120;
        Time.fixedDeltaTime = 0.008333335f;
      }
      else
        GameSaveData.instance.graphicOptionKey = "low";
    }
    if (GameSaveData.instance != null)
    {
      if (GameSaveData.instance.graphicOptionKey == "highest")
      {
        Application.targetFrameRate = 120;
        Time.fixedDeltaTime = 0.008333335f;
      }
      else
        Application.targetFrameRate = 30;
    }
    MonoBehaviourSingleton<AppMain>.I.UpdateResolution(true);
    MonoBehaviourSingleton<SoundManager>.I.UpdateConfigVolume();
    if (MonoBehaviourSingleton<InGameManager>.IsValid())
      MonoBehaviourSingleton<InGameManager>.I.UpdateConfig();
    if (MonoBehaviourSingleton<InputManager>.IsValid())
      MonoBehaviourSingleton<InputManager>.I.UpdateConfigInput();
    ((Component) ResourceUtility.Realizes(Resources.Load("UI/UI_Root"), MonoBehaviourSingleton<AppMain>.I._transform)).gameObject.AddComponent<UIManager>();
    MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.INITIALIZE, true);
    yield return (object) null;
    ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<GameSceneManager>();
    yield return (object) null;
    ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<UserInfoManager>();
    yield return (object) null;
    ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<TransitionManager>();
    yield return (object) null;
    ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<FBManager>();
    yield return (object) null;
    ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<NativeGameService>();
    yield return (object) null;
    Singleton<StringTable>.Create();
    Singleton<StringTable>.I.CreateTable();
    MonoBehaviourSingleton<GameSceneManager>.I.Initialize();
    MonoBehaviourSingleton<GoGameResourceManager>.I.LoadServerList();
    while (MonoBehaviourSingleton<GoGameResourceManager>.I.isLoadingServerList)
      yield return (object) null;
    if (string.IsNullOrEmpty(GameSaveData.instance.currentServer.url))
    {
      ServerListTable.ServerData activeServerDataByUrl = Singleton<ServerListTable>.I.GetActiveServerDataByUrl(NetworkManager.OLD_SERVER_URL);
      if (!string.IsNullOrEmpty(MonoBehaviourSingleton<AccountManager>.I.account.token) && activeServerDataByUrl != null)
        GameSaveData.instance.SetCurrentServer(activeServerDataByUrl);
      else
        GameSaveData.instance.SetCurrentServer(Singleton<ServerListTable>.I.GetNewestServer());
    }
    else
    {
      ServerListTable.ServerData activeServerDataById = Singleton<ServerListTable>.I.GetActiveServerDataById(GameSaveData.instance.currentServer.id);
      GameSaveData.instance.SetCurrentServer(activeServerDataById == null ? Singleton<ServerListTable>.I.GetNewestServer() : activeServerDataById);
    }
    MonoBehaviourSingleton<AccountManager>.I.GetLastLoginAccountOnServer();
    NetworkNative.setHost(NetworkManager.APP_HOST);
    BootProcess.isAnalytics = true;
    NetworkNative.getAnalytics();
    LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lo_sound_se_table = load_queue.Load(RESOURCE_CATEGORY.TABLE, "SETable");
    LoadObject lo_audio_setting_table = load_queue.Load(RESOURCE_CATEGORY.TABLE, "AudioSettingTable");
    while (load_queue.IsLoading() || !MonoBehaviourSingleton<GameSceneManager>.I.isInitialized)
      yield return (object) null;
    Singleton<SETable>.Create();
    Singleton<SETable>.I.CreateTableFromInternal((lo_sound_se_table.loadedObject as TextAsset).text);
    Singleton<AudioSettingTable>.Create();
    Singleton<AudioSettingTable>.I.CreateTableFromInternal((lo_audio_setting_table.loadedObject as TextAsset).text);
    if (MonoBehaviourSingleton<SoundManager>.IsValid())
      MonoBehaviourSingleton<SoundManager>.I.LoadParmanentAudioClip();
    int reset = PlayerPrefs.GetInt("AppMain.Reset", 0);
    if (reset != 0)
    {
      yield return (object) null;
      MonoBehaviourSingleton<AppMain>.I.Reset((reset & 1) != 0, (reset & 2) != 0);
    }
    else
    {
      float analyticTimeCount = 5f;
      while (BootProcess.isAnalytics)
      {
        analyticTimeCount -= Time.deltaTime;
        if ((double) analyticTimeCount < 0.0)
        {
          int errorCode = 200000;
          MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, StringTable.Format(STRING_CATEGORY.COMMON_DIALOG, 1001U, (object) errorCode), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 100U)), (Action<string>) (btn => MonoBehaviourSingleton<AppMain>.I.Reset()), true, errorCode);
          yield break;
        }
        yield return (object) null;
      }
      bool wait = true;
      MonoBehaviourSingleton<AccountManager>.I.SendCheckRegister(BootProcess.analyticsString, (Action<bool>) (b => wait = false));
      while (wait)
        yield return (object) null;
      MonoBehaviourSingleton<ResourceManager>.I.cache.ClearObjectCaches(true);
      MonoBehaviourSingleton<ResourceManager>.I.cache.ClearPackageCaches();
      int num1 = this.isAssetBundleMode() ? 1 : 0;
      bool to_opening = this.IsOpening();
      if (num1 != 0)
      {
        if (!this.CheckPermissions())
        {
          int num2 = PlayerPrefs.GetInt("first_time_load_game_msg", 0) == 0 ? 1 : 0;
          AndroidPermissionsManager.ShouldShowRequestPermission("android.permission.WRITE_EXTERNAL_STORAGE");
          PlayerPrefs.SetInt("first_time_load_game_msg", 1);
          if (num2 == 0)
          {
            MonoBehaviourSingleton<UIManager>.I.loading.ShowChangePermissionMsg(true);
            while (!this.CheckPermissions())
              yield return (object) null;
          }
          else
          {
            MonoBehaviourSingleton<UIManager>.I.loading.ShowWellcomeMsg(true);
            while (this.isWaitGrantedPermission)
              yield return (object) null;
            while (!this.isGrantedPermission)
            {
              this.isWaitGrantedPermission = true;
              MonoBehaviourSingleton<UIManager>.I.loading.ShowDellyMsg(true);
              MonoBehaviourSingleton<UIManager>.I.loading.ShowEmptyFirstLoad(false);
              while (this.isWaitGrantedPermission)
                yield return (object) null;
              if (!this.isGrantedPermission && !AndroidPermissionsManager.ShouldShowRequestPermission("android.permission.WRITE_EXTERNAL_STORAGE"))
              {
                MonoBehaviourSingleton<UIManager>.I.loading.HideAllPermissionMsg();
                MonoBehaviourSingleton<UIManager>.I.loading.ShowChangePermissionMsg(true);
                while (!this.CheckPermissions())
                  yield return (object) null;
                this.isGrantedPermission = true;
              }
            }
          }
          MonoBehaviourSingleton<UIManager>.I.loading.ShowEmptyFirstLoad(true);
          MonoBehaviourSingleton<UIManager>.I.loading.HideAllTextMsg();
          yield return (object) null;
        }
        else if (PlayerPrefs.GetInt("first_time_load_game_msg", 0) == 0)
        {
          PlayerPrefs.SetInt("first_time_load_game_msg", 1);
          MonoBehaviourSingleton<UIManager>.I.loading.ShowEmptyFirstLoad(true);
        }
        if (to_opening)
          MonoBehaviourSingleton<UIManager>.I.loading.downloadGaugeVisible = false;
        MonoBehaviourSingleton<ResourceManager>.I.SetURL(NetworkManager.IMG_HOST);
        MonoBehaviourSingleton<GoGameResourceManager>.I.LoadVariantManifest();
        while (MonoBehaviourSingleton<GoGameResourceManager>.I.isLoadingVariantManifest)
          yield return (object) null;
        MonoBehaviourSingleton<ResourceManager>.I.LoadManifest();
        while (MonoBehaviourSingleton<ResourceManager>.I.isLoadingManifest)
          yield return (object) null;
        ResourceManager.internalMode = false;
        load_queue.Load(RESOURCE_CATEGORY.SHADER, (string) null, (string[]) null, true);
        load_queue.Load(RESOURCE_CATEGORY.UI_FONT, (string) null, (string[]) null, true);
        ResourceManager.internalMode = true;
        yield return (object) load_queue.Wait();
        MonoBehaviourSingleton<ResourceManager>.I.cache.MarkSystemPackage(RESOURCE_CATEGORY.SHADER.ToAssetBundleName());
        MonoBehaviourSingleton<ResourceManager>.I.cache.MarkSystemPackage(RESOURCE_CATEGORY.UI_FONT.ToAssetBundleName());
        MonoBehaviourSingleton<ResourceManager>.I.cache.CacheShadersFromPackage(RESOURCE_CATEGORY.SHADER.ToAssetBundleName());
        MonoBehaviourSingleton<UIManager>.I.loading.downloadGaugeVisible = true;
      }
      NetworkNative.getNativeAsset();
      if (MonoBehaviourSingleton<AccountManager>.I.sendAsset)
        NetworkNative.getNativeiOSAsset();
      if (MonoBehaviourSingleton<AccountManager>.I.appClose)
        ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<AppCloseProcess>();
      else if (to_opening)
      {
        Native.CheckReferrerSendToAppBrowser();
        ResourceManager.internalMode = true;
        ResourceManager.internalMode = false;
        if (Object.op_Inequality((Object) MonoBehaviourSingleton<ResourceManager>.I.manifest, (Object) null))
          ResourceManager.internalMode = true;
        ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<OpeningStartProcess>();
      }
      else
      {
        ResourceManager.internalMode = false;
        ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<LoadingProcess>();
      }
      Object.Destroy((Object) this);
    }
  }

  public static void notifyFinishedAnalytics(string data)
  {
    BootProcess.analyticsString = data;
    AppMain.appStr = data;
    BootProcess.isAnalytics = false;
  }

  private bool isAssetBundleMode() => true;

  private bool IsOpening()
  {
    bool flag1 = false;
    bool flag2 = false;
    if (!string.IsNullOrEmpty(MonoBehaviourSingleton<AppMain>.I.startScene) && (!MonoBehaviourSingleton<AccountManager>.I.account.IsRegist() || MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name == "/colopl_rob"))
    {
      flag1 = true;
      flag2 = true;
    }
    return (!flag2 || !flag1 || MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep <= 0) && flag1;
  }
}
