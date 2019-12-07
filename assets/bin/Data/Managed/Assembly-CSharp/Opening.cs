using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opening : GameSection
{
	private enum UI
	{
		LBL_APP_VERSION,
		LBL_CURRENT_SERVER,
		BTN_START,
		BTN_FB_LOGIN,
		BTN_ADVANCED_LOGIN,
		BTN_CLEARCACHE,
		BTN_CHANGE_SERVER
	}

	private enum AUDIO
	{
		SE_CUT_01 = 40000095,
		SE_CUT_02,
		SE_CUT_03,
		SE_CUT_04,
		SE_CUT_05
	}

	private enum VOICE
	{
		V00 = 401,
		V01,
		V02,
		V03,
		V04,
		V05,
		V06,
		V07,
		V08,
		V09
	}

	private class VoiceSequenceData
	{
		public int id;

		public float delay;

		public VoiceSequenceData(float _delay, VOICE _voice)
		{
			delay = _delay;
			id = (int)_voice;
		}
	}

	private class AudioSequeceData
	{
		public float delay;

		public AUDIO SEType;

		public int id => (int)SEType;

		public AudioSequeceData(float _delay, AUDIO _audio)
		{
			delay = _delay;
			SEType = _audio;
		}
	}

	private GameObject cutSceneObjectRoot;

	private GameObject cutOP;

	private Animation cutSceneAnimation;

	private GameObject titleObjectRoot;

	private Animation titleAnimation;

	private Material whiteFadeMaterial;

	private float downloadGaugeDisplayTimer;

	private bool isAnimationStarted;

	private bool isRegisted;

	private bool isCacheClear;

	private bool isDownloading = true;

	private bool hasSkipped;

	private bool endCutScene;

	private VoiceSequenceData[] voice_sequence = new VoiceSequenceData[10]
	{
		new VoiceSequenceData(4f, VOICE.V00),
		new VoiceSequenceData(5f, VOICE.V01),
		new VoiceSequenceData(6.6f, VOICE.V02),
		new VoiceSequenceData(6.8f, VOICE.V03),
		new VoiceSequenceData(4f, VOICE.V04),
		new VoiceSequenceData(7f, VOICE.V05),
		new VoiceSequenceData(7f, VOICE.V06),
		new VoiceSequenceData(4.8f, VOICE.V07),
		new VoiceSequenceData(7.4f, VOICE.V08),
		new VoiceSequenceData(5.2f, VOICE.V09)
	};

	private AudioSequeceData[] audio_sequence = new AudioSequeceData[5]
	{
		new AudioSequeceData(4f, AUDIO.SE_CUT_01),
		new AudioSequeceData(10f, AUDIO.SE_CUT_02),
		new AudioSequeceData(10f, AUDIO.SE_CUT_03),
		new AudioSequeceData(14f, AUDIO.SE_CUT_04),
		new AudioSequeceData(12f, AUDIO.SE_CUT_05)
	};

	public override bool useOnPressBackKey => true;

	public override void OnPressBackKey()
	{
		if (!isAnimationStarted)
		{
			Native.applicationQuit();
		}
	}

	public override void Initialize()
	{
		StartCoroutine(DoInitialzie());
	}

	private IEnumerator DoInitialzie()
	{
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject loadedCutSceneObj = loadingQueue.Load(RESOURCE_CATEGORY.CUTSCENE, "Opening");
		LoadObject loadedTitleObj = loadingQueue.Load(RESOURCE_CATEGORY.CUTSCENE, "Title");
		int[] array = (int[])Enum.GetValues(typeof(AUDIO));
		foreach (int se_id in array)
		{
			loadingQueue.CacheSE(se_id);
		}
		array = (int[])Enum.GetValues(typeof(VOICE));
		foreach (int voice_id in array)
		{
			loadingQueue.CacheVoice(voice_id);
		}
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		if (MonoBehaviourSingleton<AppMain>.I.mainCamera != null)
		{
			MonoBehaviourSingleton<AppMain>.I.mainCamera.enabled = false;
		}
		cutSceneObjectRoot = (ResourceUtility.Instantiate(loadedCutSceneObj.loadedObject) as GameObject);
		cutSceneObjectRoot.transform.parent = MonoBehaviourSingleton<AppMain>.I.transform;
		titleObjectRoot = (ResourceUtility.Instantiate(loadedTitleObj.loadedObject) as GameObject);
		titleObjectRoot.transform.parent = MonoBehaviourSingleton<AppMain>.I.transform;
		Transform transform = cutSceneObjectRoot.transform.Find("CUT_op");
		if (transform != null)
		{
			cutOP = transform.gameObject;
			cutSceneAnimation = cutOP.GetComponent<Animation>();
			cutOP.SetActive(value: false);
			if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.NeedModifyOpening)
			{
				cutOP.transform.localScale = SpecialDeviceManager.SpecialDeviceInfo.OpeningCutScale;
			}
		}
		Transform transform2 = cutSceneObjectRoot.transform.Find("Main Camera/Plane");
		if (transform2 != null)
		{
			MeshRenderer component = transform2.GetComponent<MeshRenderer>();
			whiteFadeMaterial = component.material;
		}
		titleAnimation = titleObjectRoot.GetComponent<Animation>();
		cutSceneAnimation.Stop();
		MonoBehaviourSingleton<FieldManager>.I.SetCurrentFieldMapPortalID(10000101u);
		MonoBehaviourSingleton<UIManager>.I.loading.HideAllPermissionMsg();
		base.Initialize();
		PredownloadManager.openingMode = true;
		MonoBehaviourSingleton<AppMain>.I.gameObject.AddComponent<PredownloadManager>();
		MonoBehaviourSingleton<UIManager>.I.loading.downloadGaugeVisible = false;
		PredownloadManager.Stop(PredownloadManager.STOP_FLAG.INGAME_TUTORIAL, is_stop: true);
		DataTableManager dataTableManager = MonoBehaviourSingleton<DataTableManager>.I;
		bool updatedTableIndex = false;
		Protocol.Send<CheckRegisterModel>(CheckRegisterModel.URL, delegate
		{
			updatedTableIndex = true;
		});
		yield return new WaitUntil(() => updatedTableIndex);
		isDownloading = true;
		dataTableManager.InitializeForDownload();
		dataTableManager.UpdateManifest(delegate
		{
			dataTableManager.LoadInitialTable(delegate
			{
				List<DataLoadRequest> loadings = dataTableManager.LoadAllTable(delegate
				{
					PredownloadManager.Stop(PredownloadManager.STOP_FLAG.INGAME_TUTORIAL, is_stop: false);
					isDownloading = false;
				}, downloadOnly: true);
				MonoBehaviourSingleton<UIManager>.I.loading.SetProgress(new FirstOpeningProgress(loadings));
			}, downloadOnly: true);
		});
		TitleTop.isFirstBoot = false;
	}

	public override void StartSection()
	{
		base.StartSection();
		if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.isAdvancedUserFacebook)
		{
			DispatchEvent("START");
		}
		RefreshUI();
	}

	protected override void OnClose()
	{
		SoundManager.StopVoice();
		SoundManager.StopSEAll();
		if (cutSceneObjectRoot != null)
		{
			UnityEngine.Object.Destroy(cutSceneObjectRoot);
			cutSceneObjectRoot = null;
		}
		if (titleObjectRoot != null)
		{
			UnityEngine.Object.Destroy(titleObjectRoot);
			titleObjectRoot = null;
		}
		if (MonoBehaviourSingleton<AppMain>.I.mainCamera != null)
		{
			MonoBehaviourSingleton<AppMain>.I.mainCamera.enabled = true;
		}
		if (MonoBehaviourSingleton<PredownloadManager>.IsValid() && (MonoBehaviourSingleton<PredownloadManager>.I.tutorialCount == 0 || MonoBehaviourSingleton<PredownloadManager>.I.loadedCount < MonoBehaviourSingleton<PredownloadManager>.I.tutorialCount))
		{
			UnityEngine.Object.Destroy(MonoBehaviourSingleton<PredownloadManager>.I);
		}
		MonoBehaviourSingleton<UIManager>.I.loading.downloadGaugeVisible = true;
		base.OnClose();
	}

	private void Update()
	{
		if (endCutScene)
		{
			return;
		}
		if (MonoBehaviourSingleton<UIManager>.I.loading.downloadGaugeVisible)
		{
			downloadGaugeDisplayTimer -= Time.deltaTime;
			if (downloadGaugeDisplayTimer <= 0f)
			{
				downloadGaugeDisplayTimer = 0f;
				MonoBehaviourSingleton<UIManager>.I.loading.downloadGaugeVisible = false;
			}
		}
		if (!isAnimationStarted)
		{
			return;
		}
		GameSceneManager i = MonoBehaviourSingleton<GameSceneManager>.I;
		if (i.isChangeing || !isRegisted)
		{
			return;
		}
		bool flag = i.GetCurrentSectionName() == "Opening";
		if (cutSceneAnimation == null)
		{
			if (i.IsEventExecutionPossible() && flag)
			{
				GoEventTutorial();
			}
		}
		else if (!cutSceneAnimation.isPlaying)
		{
			if (i.IsEventExecutionPossible() && flag)
			{
				GoEventTutorial();
			}
		}
		else if (MonoBehaviourSingleton<InputManager>.I.IsTouch() && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection() == this)
		{
			if (MonoBehaviourSingleton<PredownloadManager>.IsValid() && !MonoBehaviourSingleton<PredownloadManager>.I.isLoadingInOpening)
			{
				MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Title", "OpeningSkipConfirm");
			}
			else if (!MonoBehaviourSingleton<UIManager>.I.loading.downloadGaugeVisible)
			{
				MonoBehaviourSingleton<UIManager>.I.loading.downloadGaugeVisible = true;
				downloadGaugeDisplayTimer = 1f;
			}
		}
	}

	private void GoEventTutorial()
	{
		MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_loading_start, "Tutorial");
		endCutScene = true;
		Fade(Color.black, 0f, 1f, 1f, delegate
		{
			if (MonoBehaviourSingleton<PredownloadManager>.I.isLoadingInOpening || isDownloading)
			{
				StartCoroutine("WaitForDownload");
			}
			else
			{
				DispatchEvent("ENTER_TUTORIAL");
				ResourceManager.internalMode = false;
				MonoBehaviourSingleton<UIManager>.I.ShowGGTutorialMessage();
			}
		});
	}

	private IEnumerator WaitForDownload()
	{
		MonoBehaviourSingleton<UIManager>.I.loading.downloadGaugeVisible = true;
		while (MonoBehaviourSingleton<PredownloadManager>.I.isLoadingInOpening || isDownloading)
		{
			yield return null;
		}
		ResourceManager.internalMode = false;
		DispatchEvent("ENTER_TUTORIAL");
		MonoBehaviourSingleton<UIManager>.I.ShowGGTutorialMessage();
	}

	private void OnQuery_START()
	{
		if (MonoBehaviourSingleton<AccountManager>.I.usageLimitMode)
		{
			GameSection.ChangeEvent("SERVICE_LIMIT");
			return;
		}
		isRegisted = MonoBehaviourSingleton<AccountManager>.I.account.IsRegist();
		if (!isRegisted)
		{
			GameSection.StayEvent();
			MonoBehaviourSingleton<AccountManager>.I.SendRegistCreate(delegate(bool is_success)
			{
				if (is_success)
				{
					StartOpening();
					Dictionary<string, object> values = new Dictionary<string, object>
					{
						{
							"login_type",
							1
						}
					};
					MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_login, "Tutorial", values);
				}
				isRegisted = is_success;
				GameSection.ResumeEvent(is_success);
			});
		}
		else
		{
			StartOpening();
		}
		MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_start_game, "Tutorial");
	}

	private void OnQuery_FB_LOGIN()
	{
		GameSection.StayEvent();
		if (MonoBehaviourSingleton<FBManager>.I.isLoggedIn)
		{
			_SendRegistAuthFacebook();
		}
		else
		{
			MonoBehaviourSingleton<FBManager>.I.LoginWithReadPermission(delegate(bool success, string b)
			{
				if (success)
				{
					_SendRegistAuthFacebook();
				}
				else
				{
					GameSection.ResumeEvent(success);
				}
			});
		}
	}

	private void _SendRegistAuthFacebook()
	{
		MonoBehaviourSingleton<AccountManager>.I.SendRegistAuthFacebook(MonoBehaviourSingleton<FBManager>.I.accessToken, delegate(bool success)
		{
			if (success)
			{
				MenuReset.needClearCache = true;
				MenuReset.needPredownload = true;
				if (!MonoBehaviourSingleton<UserInfoManager>.I.userInfo.IsModiedName)
				{
					MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_start_game, "Tutorial");
					Dictionary<string, object> values = new Dictionary<string, object>
					{
						{
							"login_type",
							0
						}
					};
					MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_login, "Tutorial", values);
				}
				else
				{
					MonoBehaviourSingleton<NativeGameService>.I.SetOldUserLogin();
				}
			}
			GameSection.ResumeEvent(success);
		});
	}

	private void StartOpening()
	{
		SetActive(UI.LBL_CURRENT_SERVER, is_visible: false);
		SetActive(UI.BTN_CHANGE_SERVER, is_visible: false);
		SetActive(UI.LBL_APP_VERSION, is_visible: false);
		SetActive(UI.BTN_START, is_visible: false);
		SetActive(UI.BTN_ADVANCED_LOGIN, is_visible: false);
		SetActive(UI.BTN_CLEARCACHE, is_visible: false);
		SetActive(UI.BTN_FB_LOGIN, is_visible: false);
		MonoBehaviourSingleton<SoundManager>.I.TransitionTo("Opening");
		titleAnimation.Play("Tap");
		Fade(Color.white, 0f, 1f, 1f, delegate
		{
			titleObjectRoot.SetActive(value: false);
			cutOP.SetActive(value: true);
			cutSceneAnimation.Play();
			StartCoroutine(DoSEPlay());
			StartCoroutine(DoVOICEPlay());
			isAnimationStarted = true;
			Fade(Color.white, 1f, 0f, 1f, delegate
			{
			});
		});
		MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_story, "Tutorial");
	}

	private IEnumerator DoVOICEPlay()
	{
		VoiceSequenceData[] array = voice_sequence;
		foreach (VoiceSequenceData seq in array)
		{
			yield return new WaitForSeconds(seq.delay);
			if (!hasSkipped)
			{
				SoundManager.PlayVoice(seq.id);
			}
		}
	}

	private IEnumerator DoSEPlay()
	{
		AudioSequeceData[] array = audio_sequence;
		foreach (AudioSequeceData seq in array)
		{
			yield return new WaitForSeconds(seq.delay);
			if (!hasSkipped)
			{
				SoundManager.PlayOneShotUISE(seq.id);
			}
		}
	}

	private void Fade(Color baseColor, float _from, float _to, float duration, Action onComplete)
	{
		StartCoroutine(DoFade(baseColor, _from, _to, duration, onComplete));
	}

	private IEnumerator DoFade(Color baseColor, float _from, float _to, float duration, Action onComplete)
	{
		float timer = 0f;
		if (baseColor != Color.white)
		{
			whiteFadeMaterial.shader = Shader.Find("mobile/Custom/Effect/effect_alpha");
		}
		else
		{
			whiteFadeMaterial.shader = Shader.Find("mobile/Custom/Effect/effect_add");
		}
		while (timer < duration)
		{
			timer += Time.deltaTime;
			float a = Mathf.Lerp(_from, _to, timer / duration);
			whiteFadeMaterial.SetColor("_Color", new Color(baseColor.r, baseColor.g, baseColor.b, a));
			yield return null;
		}
		onComplete?.Invoke();
	}

	public override void UpdateUI()
	{
		SetApplicationVersionText(UI.LBL_APP_VERSION);
		SetLabelText(UI.LBL_CURRENT_SERVER, "Server: " + GameSaveData.instance.currentServer.name);
		if (MonoBehaviourSingleton<GlobalSettingsManager>.IsValid() && MonoBehaviourSingleton<GlobalSettingsManager>.I.submissionVersion)
		{
			SetActive(UI.BTN_ADVANCED_LOGIN, is_visible: false);
		}
		else
		{
			SetActive(UI.BTN_ADVANCED_LOGIN, !MonoBehaviourSingleton<AccountManager>.I.account.IsRegist());
		}
		SetActive(UI.BTN_FB_LOGIN, !MonoBehaviourSingleton<UserInfoManager>.I.userInfo.isAdvancedUserFacebook);
		SetActive(UI.BTN_START, !MonoBehaviourSingleton<UserInfoManager>.I.userInfo.isAdvancedUserFacebook);
	}

	public override void Exit()
	{
		base.Exit();
		if (!isCacheClear && !MonoBehaviourSingleton<LoadingProcess>.IsValid())
		{
			MonoBehaviourSingleton<AppMain>.I.gameObject.AddComponent<LoadingProcess>();
		}
	}

	private void OnQuery_OpeningSkipConfirm_YES()
	{
		MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_loading_start, "Tutorial");
		ResourceManager.internalMode = false;
		hasSkipped = true;
		MonoBehaviourSingleton<UIManager>.I.ShowGGTutorialMessage();
	}

	private void OnQuery_TitleClearCacheConfirm_YES()
	{
		isCacheClear = true;
		MenuReset.needClearCache = true;
		MenuReset.needPredownload = false;
	}
}
