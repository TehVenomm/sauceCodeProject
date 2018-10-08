using System;
using System.Collections;
using UnityEngine;

public class LoadingProcess : MonoBehaviourSingleton<LoadingProcess>
{
	private unsafe IEnumerator Start()
	{
		PredownloadManager.Stop(PredownloadManager.STOP_FLAG.LOADING_PROCESS, true);
		ResourceManager.internalMode = false;
		bool is_tutorial = FieldManager.IsValidInTutorial();
		LoadingQueue load_queue = new LoadingQueue(this);
		LoadObject lo_loading_ui = load_queue.Load(RESOURCE_CATEGORY.UI, "LoadingUI", true);
		yield return (object)load_queue.Wait();
		MonoBehaviourSingleton<UIManager>.I.SetLoadingUI(lo_loading_ui.loadedObject);
		MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.INITIALIZE, true);
		if (!is_tutorial)
		{
			yield return (object)MonoBehaviourSingleton<AppMain>.I.ClearMemory(true, true);
		}
		ResourceManager.enableCache = false;
		LoadObject lo_common_prefabs = load_queue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemCommon", new string[3]
		{
			"MainCamera",
			"GlobalSettingsManager",
			"InputManager"
		}, false);
		LoadObject lo_outgame_prefabs = load_queue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemOutGame", new string[1]
		{
			"OutGameSettingsManager"
		}, false);
		ResourceManager.enableCache = true;
		if (MonoBehaviourSingleton<SoundManager>.IsValid())
		{
			MonoBehaviourSingleton<SoundManager>.I.LoadParmanentAudioClip();
		}
		while (load_queue.IsLoading())
		{
			yield return (object)null;
		}
		if (MonoBehaviourSingleton<InputManager>.IsValid())
		{
			Object.DestroyImmediate(MonoBehaviourSingleton<InputManager>.I.get_gameObject());
		}
		if (MonoBehaviourSingleton<FieldManager>.IsValid())
		{
			Object.DestroyImmediate(MonoBehaviourSingleton<FieldManager>.I);
		}
		if (MonoBehaviourSingleton<WorldMapManager>.IsValid())
		{
			Object.DestroyImmediate(MonoBehaviourSingleton<WorldMapManager>.I);
		}
		if (MonoBehaviourSingleton<FilterManager>.IsValid())
		{
			Object.DestroyImmediate(MonoBehaviourSingleton<FilterManager>.I);
		}
		if (MonoBehaviourSingleton<OnceManager>.IsValid())
		{
			Object.DestroyImmediate(MonoBehaviourSingleton<OnceManager>.I);
		}
		GameSceneGlobalSettings.SetOrientation(false);
		if (Camera.get_main() != null)
		{
			Object.DestroyImmediate(Camera.get_main().get_gameObject());
		}
		ResourceObject[] loadedObjects = lo_common_prefabs.loadedObjects;
		foreach (ResourceObject prefab in loadedObjects)
		{
			ResourceUtility.Realizes(prefab.obj, MonoBehaviourSingleton<AppMain>.I._transform, -1);
		}
		ResourceObject[] loadedObjects2 = lo_outgame_prefabs.loadedObjects;
		foreach (ResourceObject prefab2 in loadedObjects2)
		{
			ResourceUtility.Realizes(prefab2.obj, MonoBehaviourSingleton<AppMain>.I._transform, -1);
		}
		bool isLinkResourceLoaded = false;
		if (MonoBehaviourSingleton<GlobalSettingsManager>.IsValid())
		{
			MonoBehaviourSingleton<GlobalSettingsManager>.I.LoadLinkResources(new Action((object)/*Error near IL_031e: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
		yield return (object)null;
		MonoBehaviourSingleton<AppMain>.I.SetMainCamera(Camera.get_main());
		MonoBehaviourSingleton<AudioListenerManager>.I.SetFlag(AudioListenerManager.STATUS_FLAGS.CAMERA_MAIN_ACTIVE, true);
		yield return (object)null;
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<QuestManager>();
		bool loadmanifest = true;
		bool dataTableLoading = true;
		MonoBehaviourSingleton<DataTableManager>.I.Initialize();
		MonoBehaviourSingleton<DataTableManager>.I.UpdateManifest(new Action((object)/*Error near IL_039d: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		yield return (object)null;
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<FilterManager>();
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<InGameManager>();
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<InventoryManager>();
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<PresentManager>();
		yield return (object)null;
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<GachaManager>();
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<ShopManager>();
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<PartyManager>();
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<FriendManager>();
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<LoungeMatchingManager>();
		yield return (object)null;
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<ItemStorageManager>();
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<GatherManager>();
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<BlackListManager>();
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<WorldMapManager>();
		yield return (object)null;
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<FieldManager>();
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<DeliveryManager>();
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<AchievementManager>();
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<StatusManager>();
		Utility.CreateGameObjectAndComponent("StageManager", MonoBehaviourSingleton<AppMain>.I._transform, -1);
		Utility.CreateGameObjectAndComponent("GuildManager", MonoBehaviourSingleton<AppMain>.I._transform, -1);
		yield return (object)null;
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<SmithManager>();
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<ItemExchangeManager>();
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<OnceManager>();
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<GuildRequestManager>();
		while (dataTableLoading || loadmanifest)
		{
			yield return (object)null;
		}
		DataTableManager i2 = MonoBehaviourSingleton<DataTableManager>.I;
		if (_003CStart_003Ec__Iterator1A._003C_003Ef__am_0024cache14 == null)
		{
			_003CStart_003Ec__Iterator1A._003C_003Ef__am_0024cache14 = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		i2.LoadAllTable(_003CStart_003Ec__Iterator1A._003C_003Ef__am_0024cache14, false);
		MonoBehaviourSingleton<GameSceneManager>.I.Initialize();
		while (!MonoBehaviourSingleton<GameSceneManager>.I.isInitialized)
		{
			yield return (object)null;
		}
		MonoBehaviourSingleton<GlobalSettingsManager>.I.InitAvatarData();
		if (is_tutorial)
		{
			MonoBehaviourSingleton<FieldManager>.I.SetCurrentFieldMapPortalID(10000100u, 0f, 0f, 180f);
			MonoBehaviourSingleton<UIManager>.I.loading.downloadGaugeVisible = false;
		}
		while ((MonoBehaviourSingleton<SoundManager>.IsValid() && MonoBehaviourSingleton<SoundManager>.I.IsLoadingAudioClip()) || !isLinkResourceLoaded)
		{
			yield return (object)null;
		}
		if (!string.IsNullOrEmpty(MonoBehaviourSingleton<AppMain>.I.startScene))
		{
			string scene = MonoBehaviourSingleton<AppMain>.I.startScene;
			string section = null;
			if (scene.Contains("@"))
			{
				string[] s = scene.Split('@');
				if (s.Length == 2)
				{
					section = s[0];
					scene = s[1];
				}
			}
			MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene(scene, section, UITransition.TYPE.CLOSE, UITransition.TYPE.OPEN, false);
		}
		MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.INITIALIZE, false);
		MonoBehaviourSingleton<NativeGameService>.I.SignIn();
		MonoBehaviourSingleton<AppMain>.I.OnLoadFinished();
		PredownloadManager.Stop(PredownloadManager.STOP_FLAG.LOADING_PROCESS, false);
		Object.Destroy(this);
	}
}
