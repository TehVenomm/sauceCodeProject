using Network;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingProcess : MonoBehaviourSingleton<LoadingProcess>
{
	private IEnumerator Start()
	{
		PredownloadManager.Stop(PredownloadManager.STOP_FLAG.LOADING_PROCESS, is_stop: true);
		ResourceManager.internalMode = false;
		bool is_tutorial = FieldManager.IsValidInTutorial();
		LoadingQueue load_queue = new LoadingQueue(this);
		LoadObject lo_loading_ui = load_queue.Load(RESOURCE_CATEGORY.UI, "LoadingUI", cache_package: true);
		yield return load_queue.Wait();
		MonoBehaviourSingleton<UIManager>.I.SetLoadingUI(lo_loading_ui.loadedObject);
		MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.INITIALIZE, is_disable: true);
		yield return null;
		if (!is_tutorial)
		{
			yield return MonoBehaviourSingleton<AppMain>.I.ClearMemory(clearObjCaches: true, clearPreloaded: true);
		}
		ResourceManager.enableCache = false;
		LoadObject lo_common_prefabs = load_queue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemCommon", new string[4]
		{
			"MainCamera",
			"GlobalSettingsManager",
			"InputManager",
			"GoGameSettingsManager"
		});
		yield return load_queue.Wait();
		LoadObject lo_outgame_prefabs = load_queue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemOutGame", new string[1]
		{
			"OutGameSettingsManager"
		});
		yield return load_queue.Wait();
		ResourceManager.enableCache = true;
		if (MonoBehaviourSingleton<SoundManager>.IsValid() && !MonoBehaviourSingleton<SoundManager>.I.IsLoadedAudioClip())
		{
			MonoBehaviourSingleton<SoundManager>.I.LoadParmanentAudioClip();
		}
		while (load_queue.IsLoading())
		{
			yield return null;
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
		GameSceneGlobalSettings.SetOrientation(ingame: false);
		if (Camera.get_main() != null)
		{
			Object.DestroyImmediate(Camera.get_main().get_gameObject());
		}
		ResourceObject[] loadedObjects = lo_common_prefabs.loadedObjects;
		foreach (ResourceObject resourceObject in loadedObjects)
		{
			ResourceUtility.Realizes(resourceObject.obj, MonoBehaviourSingleton<AppMain>.I._transform);
		}
		yield return null;
		ResourceObject[] loadedObjects2 = lo_outgame_prefabs.loadedObjects;
		foreach (ResourceObject resourceObject2 in loadedObjects2)
		{
			ResourceUtility.Realizes(resourceObject2.obj, MonoBehaviourSingleton<AppMain>.I._transform);
		}
		bool isLinkResourceLoaded = false;
		if (MonoBehaviourSingleton<GlobalSettingsManager>.IsValid())
		{
			MonoBehaviourSingleton<GlobalSettingsManager>.I.LoadLinkResources(delegate
			{
				isLinkResourceLoaded = true;
			});
		}
		yield return null;
		MonoBehaviourSingleton<AppMain>.I.SetMainCamera(Camera.get_main());
		MonoBehaviourSingleton<AudioListenerManager>.I.SetFlag(AudioListenerManager.STATUS_FLAGS.CAMERA_MAIN_ACTIVE, isEnable: true);
		yield return null;
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<QuestManager>();
		bool loadmanifest = true;
		bool dataTableLoading = true;
		MonoBehaviourSingleton<DataTableManager>.I.Initialize();
		MonoBehaviourSingleton<DataTableManager>.I.UpdateManifest(delegate
		{
			List<DataLoadRequest> loadings = MonoBehaviourSingleton<DataTableManager>.I.LoadInitialTable(delegate
			{
				dataTableLoading = false;
			});
			MonoBehaviourSingleton<UIManager>.I.loading.SetProgress(new DataTableLoadProgress(loadings));
			loadmanifest = false;
		});
		yield return null;
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<FilterManager>();
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<InGameManager>();
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<InventoryManager>();
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<PresentManager>();
		yield return null;
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<GachaManager>();
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<ShopManager>();
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<PartyManager>();
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<FriendManager>();
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<LoungeMatchingManager>();
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<ClanMatchingManager>();
		yield return null;
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<ItemStorageManager>();
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<GatherManager>();
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<BlackListManager>();
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<WorldMapManager>();
		yield return null;
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<FieldManager>();
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<DeliveryManager>();
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<AchievementManager>();
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<StatusManager>();
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<FortuneWheelManager>();
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<TradingPostManager>();
		Utility.CreateGameObjectAndComponent("StageManager", MonoBehaviourSingleton<AppMain>.I._transform);
		Utility.CreateGameObjectAndComponent("GuildManager", MonoBehaviourSingleton<AppMain>.I._transform);
		yield return null;
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<SmithManager>();
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<ItemExchangeManager>();
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<OnceManager>();
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<GuildRequestManager>();
		while (dataTableLoading || loadmanifest)
		{
			yield return null;
		}
		MonoBehaviourSingleton<DataTableManager>.I.LoadAllTable(delegate
		{
		});
		while (!MonoBehaviourSingleton<GameSceneManager>.I.isInitialized)
		{
			yield return null;
		}
		MonoBehaviourSingleton<GlobalSettingsManager>.I.InitAvatarData();
		if (is_tutorial)
		{
			MonoBehaviourSingleton<FieldManager>.I.SetCurrentFieldMapPortalID(10000101u, 0f, 0f, 180f);
			MonoBehaviourSingleton<UIManager>.I.loading.downloadGaugeVisible = false;
		}
		while ((MonoBehaviourSingleton<SoundManager>.IsValid() && MonoBehaviourSingleton<SoundManager>.I.IsLoadingAudioClip()) || !isLinkResourceLoaded)
		{
			yield return null;
		}
		if (MonoBehaviourSingleton<AccountManager>.I.account.IsRegist() && TitleTop.isFirstBoot && 2 <= MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep)
		{
			bool wait = true;
			MonoBehaviourSingleton<LoungeMatchingManager>.I.SendInfo(delegate
			{
				wait = false;
			});
			while (wait)
			{
				yield return null;
			}
			if (MonoBehaviourSingleton<LoungeMatchingManager>.IsValid() && MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData != null)
			{
				MonoBehaviourSingleton<AppMain>.I.startScene = "Lounge";
			}
			else
			{
				wait = true;
				MonoBehaviourSingleton<ClanMatchingManager>.I.RequestUserDetail(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, delegate(UserClanData userClanData)
				{
					MonoBehaviourSingleton<UserInfoManager>.I.SetUserClan(userClanData);
					wait = false;
				});
				while (wait)
				{
					yield return null;
				}
				if (ClanMatchingManager.IsValidInClan())
				{
					MonoBehaviourSingleton<AppMain>.I.startScene = "Clan";
				}
				else
				{
					MonoBehaviourSingleton<AppMain>.I.startScene = "Home";
				}
			}
			TitleTop.isFirstBoot = false;
		}
		if (!string.IsNullOrEmpty(MonoBehaviourSingleton<AppMain>.I.startScene))
		{
			string text = MonoBehaviourSingleton<AppMain>.I.startScene;
			string section_name = null;
			if (text.Contains("@"))
			{
				string[] array = text.Split('@');
				if (array.Length == 2)
				{
					section_name = array[0];
					text = array[1];
				}
			}
			MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene(text, section_name);
		}
		MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.INITIALIZE, is_disable: false);
		MonoBehaviourSingleton<NativeGameService>.I.SignIn();
		MonoBehaviourSingleton<AppMain>.I.OnLoadFinished();
		PredownloadManager.Stop(PredownloadManager.STOP_FLAG.LOADING_PROCESS, is_stop: false);
		Object.Destroy(this);
	}
}
