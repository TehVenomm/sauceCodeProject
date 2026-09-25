// Decompiled with JetBrains decompiler
// Type: LoadingProcess
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class LoadingProcess : MonoBehaviourSingleton<LoadingProcess>
{
  private IEnumerator Start()
  {
    PredownloadManager.Stop(PredownloadManager.STOP_FLAG.LOADING_PROCESS, true);
    ResourceManager.internalMode = false;
    bool is_tutorial = FieldManager.IsValidInTutorial();
    LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lo_loading_ui = load_queue.Load(RESOURCE_CATEGORY.UI, "LoadingUI", true);
    yield return (object) load_queue.Wait();
    MonoBehaviourSingleton<UIManager>.I.SetLoadingUI(lo_loading_ui.loadedObject);
    MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.INITIALIZE, true);
    yield return (object) null;
    if (!is_tutorial)
      yield return (object) MonoBehaviourSingleton<AppMain>.I.ClearMemory(true, true);
    ResourceManager.enableCache = false;
    LoadObject lo_common_prefabs = load_queue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemCommon", new string[4]
    {
      "MainCamera",
      "GlobalSettingsManager",
      "InputManager",
      "GoGameSettingsManager"
    });
    yield return (object) load_queue.Wait();
    LoadObject lo_outgame_prefabs = load_queue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemOutGame", new string[1]
    {
      "OutGameSettingsManager"
    });
    yield return (object) load_queue.Wait();
    ResourceManager.enableCache = true;
    if (MonoBehaviourSingleton<SoundManager>.IsValid() && !MonoBehaviourSingleton<SoundManager>.I.IsLoadedAudioClip())
      MonoBehaviourSingleton<SoundManager>.I.LoadParmanentAudioClip();
    while (load_queue.IsLoading())
      yield return (object) null;
    if (MonoBehaviourSingleton<InputManager>.IsValid())
      Object.DestroyImmediate((Object) ((Component) MonoBehaviourSingleton<InputManager>.I).gameObject);
    if (MonoBehaviourSingleton<FieldManager>.IsValid())
      Object.DestroyImmediate((Object) MonoBehaviourSingleton<FieldManager>.I);
    if (MonoBehaviourSingleton<WorldMapManager>.IsValid())
      Object.DestroyImmediate((Object) MonoBehaviourSingleton<WorldMapManager>.I);
    if (MonoBehaviourSingleton<FilterManager>.IsValid())
      Object.DestroyImmediate((Object) MonoBehaviourSingleton<FilterManager>.I);
    if (MonoBehaviourSingleton<OnceManager>.IsValid())
      Object.DestroyImmediate((Object) MonoBehaviourSingleton<OnceManager>.I);
    GameSceneGlobalSettings.SetOrientation(false);
    if (Object.op_Inequality((Object) Camera.main, (Object) null))
      Object.DestroyImmediate((Object) ((Component) Camera.main).gameObject);
    foreach (ResourceObject loadedObject in lo_common_prefabs.loadedObjects)
      ResourceUtility.Realizes(loadedObject.obj, MonoBehaviourSingleton<AppMain>.I._transform);
    yield return (object) null;
    foreach (ResourceObject loadedObject in lo_outgame_prefabs.loadedObjects)
      ResourceUtility.Realizes(loadedObject.obj, MonoBehaviourSingleton<AppMain>.I._transform);
    bool isLinkResourceLoaded = false;
    if (MonoBehaviourSingleton<GlobalSettingsManager>.IsValid())
      MonoBehaviourSingleton<GlobalSettingsManager>.I.LoadLinkResources((System.Action) (() => isLinkResourceLoaded = true));
    yield return (object) null;
    MonoBehaviourSingleton<AppMain>.I.SetMainCamera(Camera.main);
    MonoBehaviourSingleton<AudioListenerManager>.I.SetFlag(AudioListenerManager.STATUS_FLAGS.CAMERA_MAIN_ACTIVE, true);
    yield return (object) null;
    ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<QuestManager>();
    bool loadmanifest = true;
    bool dataTableLoading = true;
    MonoBehaviourSingleton<DataTableManager>.I.Initialize();
    MonoBehaviourSingleton<DataTableManager>.I.UpdateManifest((System.Action) (() =>
    {
      MonoBehaviourSingleton<UIManager>.I.loading.SetProgress((IProgress) new DataTableLoadProgress(MonoBehaviourSingleton<DataTableManager>.I.LoadInitialTable((System.Action) (() => dataTableLoading = false))));
      loadmanifest = false;
    }));
    yield return (object) null;
    ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<FilterManager>();
    ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<InGameManager>();
    ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<InventoryManager>();
    ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<PresentManager>();
    yield return (object) null;
    ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<GachaManager>();
    ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<ShopManager>();
    ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<PartyManager>();
    ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<FriendManager>();
    ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<LoungeMatchingManager>();
    ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<ClanMatchingManager>();
    yield return (object) null;
    ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<ItemStorageManager>();
    ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<GatherManager>();
    ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<BlackListManager>();
    ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<WorldMapManager>();
    yield return (object) null;
    ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<FieldManager>();
    ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<DeliveryManager>();
    ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<AchievementManager>();
    ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<StatusManager>();
    ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<FortuneWheelManager>();
    ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<TradingPostManager>();
    Utility.CreateGameObjectAndComponent("StageManager", MonoBehaviourSingleton<AppMain>.I._transform);
    Utility.CreateGameObjectAndComponent("GuildManager", MonoBehaviourSingleton<AppMain>.I._transform);
    yield return (object) null;
    ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<SmithManager>();
    ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<ItemExchangeManager>();
    ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<OnceManager>();
    ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<GuildRequestManager>();
    while (dataTableLoading | loadmanifest)
      yield return (object) null;
    MonoBehaviourSingleton<DataTableManager>.I.LoadAllTable((System.Action) (() => { }));
    while (!MonoBehaviourSingleton<GameSceneManager>.I.isInitialized)
      yield return (object) null;
    MonoBehaviourSingleton<GlobalSettingsManager>.I.InitAvatarData();
    if (is_tutorial)
    {
      MonoBehaviourSingleton<FieldManager>.I.SetCurrentFieldMapPortalID(10000101U, 0.0f, 0.0f, 180f);
      MonoBehaviourSingleton<UIManager>.I.loading.downloadGaugeVisible = false;
    }
    while (MonoBehaviourSingleton<SoundManager>.IsValid() && MonoBehaviourSingleton<SoundManager>.I.IsLoadingAudioClip() || !isLinkResourceLoaded)
      yield return (object) null;
    if (MonoBehaviourSingleton<AccountManager>.I.account.IsRegist() && TitleTop.isFirstBoot && 2 <= MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep)
    {
      bool wait = true;
      MonoBehaviourSingleton<LoungeMatchingManager>.I.SendInfo((Action<bool>) (isSuccess => wait = false));
      while (wait)
        yield return (object) null;
      if (MonoBehaviourSingleton<LoungeMatchingManager>.IsValid() && MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData != null)
      {
        MonoBehaviourSingleton<AppMain>.I.startScene = "Lounge";
      }
      else
      {
        wait = true;
        MonoBehaviourSingleton<ClanMatchingManager>.I.RequestUserDetail(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, (Action<UserClanData>) (userClanData =>
        {
          MonoBehaviourSingleton<UserInfoManager>.I.SetUserClan(userClanData);
          wait = false;
        }));
        while (wait)
          yield return (object) null;
        MonoBehaviourSingleton<AppMain>.I.startScene = !ClanMatchingManager.IsValidInClan() ? "Home" : "Clan";
      }
      TitleTop.isFirstBoot = false;
      TitleTop.isFirstServerSelection = false;
    }
    if (!string.IsNullOrEmpty(MonoBehaviourSingleton<AppMain>.I.startScene))
    {
      string startScene = MonoBehaviourSingleton<AppMain>.I.startScene;
      string section_name = (string) null;
      if (startScene.Contains("@"))
      {
        string[] strArray = startScene.Split('@');
        if (strArray.Length == 2)
        {
          section_name = strArray[0];
          startScene = strArray[1];
        }
      }
      MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene(startScene, section_name);
    }
    MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.INITIALIZE, false);
    if (MonoBehaviourSingleton<NativeGameService>.IsValid())
      MonoBehaviourSingleton<NativeGameService>.I.SignIn();
    MonoBehaviourSingleton<AppMain>.I.OnLoadFinished();
    PredownloadManager.Stop(PredownloadManager.STOP_FLAG.LOADING_PROCESS, false);
    Object.Destroy((Object) this);
  }
}
