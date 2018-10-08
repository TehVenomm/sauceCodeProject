using System.Collections;
using UnityEngine;

public class BootProcess
{
	public const string STORAGE_PERMISSION = "android.permission.WRITE_EXTERNAL_STORAGE";

	private const float ANALYTIC_TIMEOUT = 5f;

	private bool isWaitGrantedPermission = true;

	private bool isGrantedPermission;

	private static bool isAnalytics = true;

	private static string analyticsString;

	public BootProcess()
		: this()
	{
	}

	private bool CheckPermissions()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Invalid comparison between Unknown and I4
		if ((int)Application.get_platform() != 11)
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
				Application.set_targetFrameRate(120);
				Time.set_fixedDeltaTime(0.008333335f);
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
				Application.set_targetFrameRate(120);
				Time.set_fixedDeltaTime(0.008333335f);
			}
			else
			{
				Application.set_targetFrameRate(30);
			}
		}
		NetworkNative.setHost(NetworkManager.APP_HOST);
		isAnalytics = true;
		NetworkNative.getAnalytics();
		MonoBehaviourSingleton<AppMain>.I.UpdateResolution(true);
		MonoBehaviourSingleton<SoundManager>.I.UpdateConfigVolume();
		if (MonoBehaviourSingleton<InGameManager>.IsValid())
		{
			MonoBehaviourSingleton<InGameManager>.I.UpdateConfig();
		}
		if (MonoBehaviourSingleton<InputManager>.IsValid())
		{
			MonoBehaviourSingleton<InputManager>.I.UpdateConfigInput();
		}
		Transform ui_root = ResourceUtility.Realizes(Resources.Load("UI/UI_Root"), MonoBehaviourSingleton<AppMain>.I._transform, -1);
		ui_root.get_gameObject().AddComponent<UIManager>();
		MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.INITIALIZE, true);
		yield return (object)null;
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<GameSceneManager>();
		yield return (object)null;
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<UserInfoManager>();
		yield return (object)null;
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<TransitionManager>();
		yield return (object)null;
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<FBManager>();
		yield return (object)null;
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<NativeGameService>();
		yield return (object)null;
		Singleton<StringTable>.Create();
		Singleton<StringTable>.I.CreateTable(null);
		MonoBehaviourSingleton<GameSceneManager>.I.Initialize();
		LoadingQueue load_queue = new LoadingQueue(this);
		LoadObject lo_sound_se_table = load_queue.Load(RESOURCE_CATEGORY.TABLE, "SETable", false);
		LoadObject lo_audio_setting_table = load_queue.Load(RESOURCE_CATEGORY.TABLE, "AudioSettingTable", false);
		while (load_queue.IsLoading() || !MonoBehaviourSingleton<GameSceneManager>.I.isInitialized)
		{
			yield return (object)null;
		}
		Singleton<SETable>.Create();
		Singleton<SETable>.I.CreateTableFromInternal((lo_sound_se_table.loadedObject as TextAsset).get_text());
		Singleton<AudioSettingTable>.Create();
		Singleton<AudioSettingTable>.I.CreateTableFromInternal((lo_audio_setting_table.loadedObject as TextAsset).get_text());
		if (MonoBehaviourSingleton<SoundManager>.IsValid())
		{
			MonoBehaviourSingleton<SoundManager>.I.LoadParmanentAudioClip();
		}
		int reset = PlayerPrefs.GetInt("AppMain.Reset", 0);
		if (reset != 0)
		{
			yield return (object)null;
			MonoBehaviourSingleton<AppMain>.I.Reset((reset & 1) != 0, (reset & 2) != 0);
		}
		else
		{
			float analyticTimeCount = 5f;
			while (isAnalytics)
			{
				analyticTimeCount -= Time.get_deltaTime();
				if (analyticTimeCount < 0f)
				{
					int err_code = 200000;
					MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, StringTable.Format(STRING_CATEGORY.COMMON_DIALOG, 1001u, err_code), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 100u), null, null, null), delegate
					{
						MonoBehaviourSingleton<AppMain>.I.Reset();
					}, true, err_code);
					yield break;
				}
				yield return (object)null;
			}
			bool wait = true;
			MonoBehaviourSingleton<AccountManager>.I.SendCheckRegister(analyticsString, delegate
			{
				((_003CStart_003Ec__Iterator19)/*Error near IL_04ab: stateMachine*/)._003Cwait_003E__7 = false;
			});
			while (wait)
			{
				yield return (object)null;
			}
			MonoBehaviourSingleton<ResourceManager>.I.cache.ClearObjectCaches(true);
			MonoBehaviourSingleton<ResourceManager>.I.cache.ClearPackageCaches();
			bool assetbundle_mode = isAssetBundleMode();
			bool to_opening = IsOpening();
			if (assetbundle_mode)
			{
				if (!CheckPermissions())
				{
					bool isFirstLoad = PlayerPrefs.GetInt("first_time_load_game_msg", 0) == 0;
					AndroidPermissionsManager.ShouldShowRequestPermission("android.permission.WRITE_EXTERNAL_STORAGE");
					PlayerPrefs.SetInt("first_time_load_game_msg", 1);
					if (!isFirstLoad)
					{
						MonoBehaviourSingleton<UIManager>.I.loading.ShowChangePermissionMsg(true);
						while (!CheckPermissions())
						{
							yield return (object)null;
						}
					}
					else
					{
						MonoBehaviourSingleton<UIManager>.I.loading.ShowWellcomeMsg(true);
						while (isWaitGrantedPermission)
						{
							yield return (object)null;
						}
						while (!isGrantedPermission)
						{
							isWaitGrantedPermission = true;
							MonoBehaviourSingleton<UIManager>.I.loading.ShowDellyMsg(true);
							MonoBehaviourSingleton<UIManager>.I.loading.ShowEmptyFirstLoad(false);
							while (isWaitGrantedPermission)
							{
								yield return (object)null;
							}
							if (!isGrantedPermission && !AndroidPermissionsManager.ShouldShowRequestPermission("android.permission.WRITE_EXTERNAL_STORAGE"))
							{
								MonoBehaviourSingleton<UIManager>.I.loading.HideAllPermissionMsg();
								MonoBehaviourSingleton<UIManager>.I.loading.ShowChangePermissionMsg(true);
								while (!CheckPermissions())
								{
									yield return (object)null;
								}
								isGrantedPermission = true;
							}
						}
					}
					MonoBehaviourSingleton<UIManager>.I.loading.ShowEmptyFirstLoad(true);
					MonoBehaviourSingleton<UIManager>.I.loading.HideAllTextMsg();
					yield return (object)null;
				}
				else if (PlayerPrefs.GetInt("first_time_load_game_msg", 0) == 0)
				{
					PlayerPrefs.SetInt("first_time_load_game_msg", 1);
					MonoBehaviourSingleton<UIManager>.I.loading.ShowEmptyFirstLoad(true);
				}
				if (to_opening)
				{
					MonoBehaviourSingleton<UIManager>.I.loading.downloadGaugeVisible = false;
				}
				MonoBehaviourSingleton<ResourceManager>.I.SetURL(NetworkManager.IMG_HOST);
				MonoBehaviourSingleton<GoGameResourceManager>.I.LoadVariantManifest();
				while (MonoBehaviourSingleton<GoGameResourceManager>.I.isLoadingVariantManifest)
				{
					Debug.Log((object)"Waiting LoadingVariantManifest ");
					yield return (object)null;
				}
				MonoBehaviourSingleton<ResourceManager>.I.LoadManifest();
				while (MonoBehaviourSingleton<ResourceManager>.I.isLoadingManifest)
				{
					yield return (object)null;
				}
				ResourceManager.internalMode = false;
				load_queue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemCommon", new string[1]
				{
					"GoGameSettingsManager"
				}, false);
				load_queue.Load(RESOURCE_CATEGORY.SHADER, null, null, true);
				load_queue.Load(RESOURCE_CATEGORY.UI_FONT, null, null, true);
				ResourceManager.internalMode = true;
				yield return (object)load_queue.Wait();
				MonoBehaviourSingleton<ResourceManager>.I.cache.MarkSystemPackage(RESOURCE_CATEGORY.SHADER.ToAssetBundleName(null));
				MonoBehaviourSingleton<ResourceManager>.I.cache.MarkSystemPackage(RESOURCE_CATEGORY.UI_FONT.ToAssetBundleName(null));
				MonoBehaviourSingleton<ResourceManager>.I.cache.CacheShadersFromPackage(RESOURCE_CATEGORY.SHADER.ToAssetBundleName(null));
				MonoBehaviourSingleton<UIManager>.I.loading.downloadGaugeVisible = true;
			}
			NetworkNative.getNativeAsset();
			if (MonoBehaviourSingleton<AccountManager>.I.sendAsset)
			{
				NetworkNative.getNativeiOSAsset();
			}
			if (to_opening)
			{
				Native.CheckReferrerSendToAppBrowser();
				ResourceManager.internalMode = true;
				ResourceManager.internalMode = false;
				if (MonoBehaviourSingleton<ResourceManager>.I.manifest != null)
				{
					ResourceManager.internalMode = true;
				}
				MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<OpeningStartProcess>();
			}
			else
			{
				ResourceManager.internalMode = false;
				MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<LoadingProcess>();
			}
			Object.Destroy(this);
		}
	}

	public static void notifyFinishedAnalytics(string data)
	{
		analyticsString = data;
		AppMain.appStr = data;
		isAnalytics = false;
	}

	private bool isAssetBundleMode()
	{
		bool flag = false;
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
