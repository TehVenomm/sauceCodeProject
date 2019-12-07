using System.Collections;
using UnityEngine;

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
		if (Application.platform != RuntimePlatform.Android)
		{
			return true;
		}
		return AndroidPermissionsManager.IsPermissionGranted("android.permission.WRITE_EXTERNAL_STORAGE");
	}

	public void OnGrantButtonPress()
	{
		AndroidPermissionsManager.RequestPermission(new string[1]
		{
			"android.permission.WRITE_EXTERNAL_STORAGE"
		}, new AndroidPermissionCallback(delegate
		{
			isWaitGrantedPermission = false;
			isGrantedPermission = true;
		}, delegate
		{
			isWaitGrantedPermission = false;
			isGrantedPermission = false;
		}));
	}

	private IEnumerator Start()
	{
		ResourceManager.internalMode = true;
		GameSaveData.Load();
		if (PlayerPrefs.GetInt("INIT_SE_VOLUME", 0) == 0)
		{
			if (GameSaveData.instance.volumeSE == 1f)
			{
				GameSaveData.instance.volumeSE = 0.5f;
			}
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
			{
				GameSaveData.instance.graphicOptionKey = "low";
			}
		}
		if (GameSaveData.instance != null)
		{
			if (GameSaveData.instance.graphicOptionKey == "highest")
			{
				Application.targetFrameRate = 120;
				Time.fixedDeltaTime = 0.008333335f;
			}
			else
			{
				Application.targetFrameRate = 30;
			}
		}
		MonoBehaviourSingleton<AppMain>.I.UpdateResolution(is_portrait: true);
		MonoBehaviourSingleton<SoundManager>.I.UpdateConfigVolume();
		if (MonoBehaviourSingleton<InGameManager>.IsValid())
		{
			MonoBehaviourSingleton<InGameManager>.I.UpdateConfig();
		}
		if (MonoBehaviourSingleton<InputManager>.IsValid())
		{
			MonoBehaviourSingleton<InputManager>.I.UpdateConfigInput();
		}
		ResourceUtility.Realizes(Resources.Load("UI/UI_Root"), MonoBehaviourSingleton<AppMain>.I._transform).gameObject.AddComponent<UIManager>();
		MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.INITIALIZE, is_disable: true);
		yield return null;
		MonoBehaviourSingleton<AppMain>.I.gameObject.AddComponent<GameSceneManager>();
		yield return null;
		MonoBehaviourSingleton<AppMain>.I.gameObject.AddComponent<UserInfoManager>();
		yield return null;
		MonoBehaviourSingleton<AppMain>.I.gameObject.AddComponent<TransitionManager>();
		yield return null;
		MonoBehaviourSingleton<AppMain>.I.gameObject.AddComponent<FBManager>();
		yield return null;
		MonoBehaviourSingleton<AppMain>.I.gameObject.AddComponent<NativeGameService>();
		yield return null;
		Singleton<StringTable>.Create();
		Singleton<StringTable>.I.CreateTable();
		MonoBehaviourSingleton<GameSceneManager>.I.Initialize();
		MonoBehaviourSingleton<GoGameResourceManager>.I.LoadServerList();
		while (MonoBehaviourSingleton<GoGameResourceManager>.I.isLoadingServerList)
		{
			yield return null;
		}
		if (string.IsNullOrEmpty(GameSaveData.instance.currentServer.url))
		{
			ServerListTable.ServerData activeServerData = Singleton<ServerListTable>.I.GetActiveServerData(NetworkManager.OLD_SERVER_URL);
			if (!string.IsNullOrEmpty(MonoBehaviourSingleton<AccountManager>.I.account.token) && activeServerData != null)
			{
				GameSaveData.instance.SetCurrentServer(activeServerData);
			}
			else
			{
				GameSaveData.instance.SetCurrentServer(Singleton<ServerListTable>.I.GetNewestServer());
			}
		}
		else
		{
			ServerListTable.ServerData activeServerData2 = Singleton<ServerListTable>.I.GetActiveServerData(GameSaveData.instance.currentServer.url);
			GameSaveData.instance.SetCurrentServer((activeServerData2 == null) ? Singleton<ServerListTable>.I.GetNewestServer() : activeServerData2);
		}
		MonoBehaviourSingleton<AccountManager>.I.GetLastLoginAccountOnServer();
		NetworkNative.setHost(NetworkManager.APP_HOST);
		isAnalytics = true;
		NetworkNative.getAnalytics();
		LoadingQueue load_queue = new LoadingQueue(this);
		LoadObject lo_sound_se_table = load_queue.Load(RESOURCE_CATEGORY.TABLE, "SETable");
		LoadObject lo_audio_setting_table = load_queue.Load(RESOURCE_CATEGORY.TABLE, "AudioSettingTable");
		while (load_queue.IsLoading() || !MonoBehaviourSingleton<GameSceneManager>.I.isInitialized)
		{
			yield return null;
		}
		Singleton<SETable>.Create();
		Singleton<SETable>.I.CreateTableFromInternal((lo_sound_se_table.loadedObject as TextAsset).text);
		Singleton<AudioSettingTable>.Create();
		Singleton<AudioSettingTable>.I.CreateTableFromInternal((lo_audio_setting_table.loadedObject as TextAsset).text);
		if (MonoBehaviourSingleton<SoundManager>.IsValid())
		{
			MonoBehaviourSingleton<SoundManager>.I.LoadParmanentAudioClip();
		}
		int reset = PlayerPrefs.GetInt("AppMain.Reset", 0);
		if (reset != 0)
		{
			yield return null;
			MonoBehaviourSingleton<AppMain>.I.Reset((reset & 1) != 0, (reset & 2) != 0);
			yield break;
		}
		float analyticTimeCount = 5f;
		while (isAnalytics)
		{
			analyticTimeCount -= Time.deltaTime;
			if (analyticTimeCount < 0f)
			{
				int num = 200000;
				MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, StringTable.Format(STRING_CATEGORY.COMMON_DIALOG, 1001u, num), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 100u)), delegate
				{
					MonoBehaviourSingleton<AppMain>.I.Reset();
				}, error: true, num);
				yield break;
			}
			yield return null;
		}
		bool wait = true;
		MonoBehaviourSingleton<AccountManager>.I.SendCheckRegister(analyticsString, delegate
		{
			wait = false;
		});
		while (wait)
		{
			yield return null;
		}
		MonoBehaviourSingleton<ResourceManager>.I.cache.ClearObjectCaches(clearPreloaded: true);
		MonoBehaviourSingleton<ResourceManager>.I.cache.ClearPackageCaches();
		bool num2 = isAssetBundleMode();
		bool to_opening = IsOpening();
		if (num2)
		{
			if (!CheckPermissions())
			{
				bool num3 = PlayerPrefs.GetInt("first_time_load_game_msg", 0) == 0;
				AndroidPermissionsManager.ShouldShowRequestPermission("android.permission.WRITE_EXTERNAL_STORAGE");
				PlayerPrefs.SetInt("first_time_load_game_msg", 1);
				if (!num3)
				{
					MonoBehaviourSingleton<UIManager>.I.loading.ShowChangePermissionMsg(isShow: true);
					while (!CheckPermissions())
					{
						yield return null;
					}
				}
				else
				{
					MonoBehaviourSingleton<UIManager>.I.loading.ShowWellcomeMsg(isShow: true);
					while (isWaitGrantedPermission)
					{
						yield return null;
					}
					while (!isGrantedPermission)
					{
						isWaitGrantedPermission = true;
						MonoBehaviourSingleton<UIManager>.I.loading.ShowDellyMsg(isShow: true);
						MonoBehaviourSingleton<UIManager>.I.loading.ShowEmptyFirstLoad(isShow: false);
						while (isWaitGrantedPermission)
						{
							yield return null;
						}
						if (!isGrantedPermission && !AndroidPermissionsManager.ShouldShowRequestPermission("android.permission.WRITE_EXTERNAL_STORAGE"))
						{
							MonoBehaviourSingleton<UIManager>.I.loading.HideAllPermissionMsg();
							MonoBehaviourSingleton<UIManager>.I.loading.ShowChangePermissionMsg(isShow: true);
							while (!CheckPermissions())
							{
								yield return null;
							}
							isGrantedPermission = true;
						}
					}
				}
				MonoBehaviourSingleton<UIManager>.I.loading.ShowEmptyFirstLoad(isShow: true);
				MonoBehaviourSingleton<UIManager>.I.loading.HideAllTextMsg();
				yield return null;
			}
			else if (PlayerPrefs.GetInt("first_time_load_game_msg", 0) == 0)
			{
				PlayerPrefs.SetInt("first_time_load_game_msg", 1);
				MonoBehaviourSingleton<UIManager>.I.loading.ShowEmptyFirstLoad(isShow: true);
			}
			if (to_opening)
			{
				MonoBehaviourSingleton<UIManager>.I.loading.downloadGaugeVisible = false;
			}
			MonoBehaviourSingleton<ResourceManager>.I.SetURL(NetworkManager.IMG_HOST);
			MonoBehaviourSingleton<GoGameResourceManager>.I.LoadVariantManifest();
			while (MonoBehaviourSingleton<GoGameResourceManager>.I.isLoadingVariantManifest)
			{
				yield return null;
			}
			MonoBehaviourSingleton<ResourceManager>.I.LoadManifest();
			while (MonoBehaviourSingleton<ResourceManager>.I.isLoadingManifest)
			{
				yield return null;
			}
			ResourceManager.internalMode = false;
			load_queue.Load(RESOURCE_CATEGORY.SHADER, null, null, cache_package: true);
			load_queue.Load(RESOURCE_CATEGORY.UI_FONT, null, null, cache_package: true);
			ResourceManager.internalMode = true;
			yield return load_queue.Wait();
			MonoBehaviourSingleton<ResourceManager>.I.cache.MarkSystemPackage(RESOURCE_CATEGORY.SHADER.ToAssetBundleName());
			MonoBehaviourSingleton<ResourceManager>.I.cache.MarkSystemPackage(RESOURCE_CATEGORY.UI_FONT.ToAssetBundleName());
			MonoBehaviourSingleton<ResourceManager>.I.cache.CacheShadersFromPackage(RESOURCE_CATEGORY.SHADER.ToAssetBundleName());
			MonoBehaviourSingleton<UIManager>.I.loading.downloadGaugeVisible = true;
		}
		NetworkNative.getNativeAsset();
		if (MonoBehaviourSingleton<AccountManager>.I.sendAsset)
		{
			NetworkNative.getNativeiOSAsset();
		}
		if (MonoBehaviourSingleton<AccountManager>.I.appClose)
		{
			MonoBehaviourSingleton<AppMain>.I.gameObject.AddComponent<AppCloseProcess>();
		}
		else if (to_opening)
		{
			Native.CheckReferrerSendToAppBrowser();
			ResourceManager.internalMode = true;
			ResourceManager.internalMode = false;
			if (MonoBehaviourSingleton<ResourceManager>.I.manifest != null)
			{
				ResourceManager.internalMode = true;
			}
			MonoBehaviourSingleton<AppMain>.I.gameObject.AddComponent<OpeningStartProcess>();
		}
		else
		{
			ResourceManager.internalMode = false;
			MonoBehaviourSingleton<AppMain>.I.gameObject.AddComponent<LoadingProcess>();
		}
		Object.Destroy(this);
	}

	public static void notifyFinishedAnalytics(string data)
	{
		analyticsString = data;
		AppMain.appStr = data;
		isAnalytics = false;
	}

	private bool isAssetBundleMode()
	{
		return true;
	}

	private bool IsOpening()
	{
		bool flag = false;
		bool flag2 = false;
		if (!string.IsNullOrEmpty(MonoBehaviourSingleton<AppMain>.I.startScene) && (!MonoBehaviourSingleton<AccountManager>.I.account.IsRegist() || MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name == "/colopl_rob"))
		{
			flag = true;
			flag2 = true;
		}
		if (flag2 && flag && MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep > 0)
		{
			return false;
		}
		return flag;
	}
}
