using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opening : GameSection
{
	private enum UI
	{
		LBL_APP_VERSION,
		BTN_START,
		BTN_FB_LOGIN,
		BTN_ADVANCED_LOGIN,
		BTN_CLEARCACHE
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
		LoadingQueue loadQueue = new LoadingQueue(this);
		LoadObject loadedCutSceneObj = loadQueue.Load(RESOURCE_CATEGORY.CUTSCENE, "Opening", false);
		LoadObject loadedTitleObj = loadQueue.Load(RESOURCE_CATEGORY.CUTSCENE, "Title", false);
		int[] se_ids = (int[])Enum.GetValues(typeof(AUDIO));
		int[] array = se_ids;
		foreach (int id in array)
		{
			loadQueue.CacheSE(id, null);
		}
		int[] voices = (int[])Enum.GetValues(typeof(VOICE));
		int[] array2 = voices;
		foreach (int id2 in array2)
		{
			loadQueue.CacheVoice(id2, null);
		}
		if (loadQueue.IsLoading())
		{
			yield return (object)loadQueue.Wait();
		}
		if ((UnityEngine.Object)MonoBehaviourSingleton<AppMain>.I.mainCamera != (UnityEngine.Object)null)
		{
			MonoBehaviourSingleton<AppMain>.I.mainCamera.enabled = false;
		}
		cutSceneObjectRoot = (ResourceUtility.Instantiate(loadedCutSceneObj.loadedObject) as GameObject);
		cutSceneObjectRoot.transform.parent = MonoBehaviourSingleton<AppMain>.I.transform;
		titleObjectRoot = (ResourceUtility.Instantiate(loadedTitleObj.loadedObject) as GameObject);
		titleObjectRoot.transform.parent = MonoBehaviourSingleton<AppMain>.I.transform;
		Transform eventObj = cutSceneObjectRoot.transform.FindChild("CUT_op");
		if ((UnityEngine.Object)eventObj != (UnityEngine.Object)null)
		{
			cutOP = eventObj.gameObject;
			cutSceneAnimation = cutOP.GetComponent<Animation>();
			cutOP.SetActive(false);
		}
		Transform fade = cutSceneObjectRoot.transform.FindChild("Main Camera/Plane");
		if ((UnityEngine.Object)fade != (UnityEngine.Object)null)
		{
			MeshRenderer renderer = fade.GetComponent<MeshRenderer>();
			whiteFadeMaterial = renderer.material;
		}
		titleAnimation = titleObjectRoot.GetComponent<Animation>();
		cutSceneAnimation.Stop();
		MonoBehaviourSingleton<FieldManager>.I.SetCurrentFieldMapPortalID(10000100u);
		MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_start_screen, "Tutorial");
		MonoBehaviourSingleton<UIManager>.I.loading.HideAllPermissionMsg();
		base.Initialize();
		PredownloadManager.openingMode = true;
		MonoBehaviourSingleton<AppMain>.I.gameObject.AddComponent<PredownloadManager>();
		MonoBehaviourSingleton<UIManager>.I.loading.downloadGaugeVisible = false;
		PredownloadManager.Stop(PredownloadManager.STOP_FLAG.INGAME_TUTORIAL, true);
		DataTableManager dataTableManager = MonoBehaviourSingleton<DataTableManager>.I;
		Protocol.Send<CheckRegisterModel>(CheckRegisterModel.URL, delegate
		{
			((_003CDoInitialzie_003Ec__Iterator103)/*Error near IL_0399: stateMachine*/)._003CupdatedTableIndex_003E__15 = true;
		}, string.Empty);
		yield return (object)new WaitUntil(() => ((_003CDoInitialzie_003Ec__Iterator103)/*Error near IL_03b0: stateMachine*/)._003CupdatedTableIndex_003E__15);
		isDownloading = true;
		dataTableManager.InitializeForDownload();
		dataTableManager.UpdateManifest(delegate
		{
			((_003CDoInitialzie_003Ec__Iterator103)/*Error near IL_03ef: stateMachine*/)._003CdataTableManager_003E__14.LoadInitialTable(delegate
			{
				List<DataLoadRequest> loadings = ((_003CDoInitialzie_003Ec__Iterator103)/*Error near IL_03ef: stateMachine*/)._003CdataTableManager_003E__14.LoadAllTable(delegate
				{
					PredownloadManager.Stop(PredownloadManager.STOP_FLAG.INGAME_TUTORIAL, false);
					((_003CDoInitialzie_003Ec__Iterator103)/*Error near IL_03ef: stateMachine*/)._003C_003Ef__this.isDownloading = false;
				}, true);
				MonoBehaviourSingleton<UIManager>.I.loading.SetProgress(new FirstOpeningProgress(loadings));
			}, true);
		});
		TitleTop.isFirstBoot = false;
	}

	public override void StartSection()
	{
		base.StartSection();
		if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.isAdvancedUserFacebook)
		{
			DispatchEvent("START", null);
		}
	}

	protected override void OnClose()
	{
		SoundManager.StopVoice(0u, 2);
		SoundManager.StopSEAll(0);
		if ((UnityEngine.Object)cutSceneObjectRoot != (UnityEngine.Object)null)
		{
			UnityEngine.Object.Destroy(cutSceneObjectRoot);
			cutSceneObjectRoot = null;
		}
		if ((UnityEngine.Object)titleObjectRoot != (UnityEngine.Object)null)
		{
			UnityEngine.Object.Destroy(titleObjectRoot);
			titleObjectRoot = null;
		}
		if ((UnityEngine.Object)MonoBehaviourSingleton<AppMain>.I.mainCamera != (UnityEngine.Object)null)
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
		if (!endCutScene)
		{
			if (MonoBehaviourSingleton<UIManager>.I.loading.downloadGaugeVisible)
			{
				downloadGaugeDisplayTimer -= Time.deltaTime;
				if (downloadGaugeDisplayTimer <= 0f)
				{
					downloadGaugeDisplayTimer = 0f;
					MonoBehaviourSingleton<UIManager>.I.loading.downloadGaugeVisible = false;
				}
			}
			if (isAnimationStarted)
			{
				GameSceneManager i = MonoBehaviourSingleton<GameSceneManager>.I;
				if (!i.isChangeing && isRegisted)
				{
					bool flag = i.GetCurrentSectionName() == "Opening";
					if ((UnityEngine.Object)cutSceneAnimation == (UnityEngine.Object)null)
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
					else if (MonoBehaviourSingleton<InputManager>.I.IsTouch() && (UnityEngine.Object)MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection() == (UnityEngine.Object)this)
					{
						if (MonoBehaviourSingleton<PredownloadManager>.IsValid() && !MonoBehaviourSingleton<PredownloadManager>.I.isLoadingInOpening)
						{
							MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Title", "OpeningSkipConfirm", UITransition.TYPE.CLOSE, UITransition.TYPE.OPEN, false);
						}
						else if (!MonoBehaviourSingleton<UIManager>.I.loading.downloadGaugeVisible)
						{
							MonoBehaviourSingleton<UIManager>.I.loading.downloadGaugeVisible = true;
							downloadGaugeDisplayTimer = 1f;
						}
					}
				}
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
				DispatchEvent("ENTER_TUTORIAL", null);
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
			yield return (object)null;
		}
		ResourceManager.internalMode = false;
		DispatchEvent("ENTER_TUTORIAL", null);
		MonoBehaviourSingleton<UIManager>.I.ShowGGTutorialMessage();
	}

	private void OnQuery_START()
	{
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
				GameSection.ResumeEvent(is_success, null);
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
					GameSection.ResumeEvent(success, null);
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
			GameSection.ResumeEvent(success, null);
		});
	}

	private void StartOpening()
	{
		SetActive(UI.LBL_APP_VERSION, false);
		SetActive(UI.BTN_START, false);
		SetActive(UI.BTN_ADVANCED_LOGIN, false);
		SetActive(UI.BTN_CLEARCACHE, false);
		SetActive(UI.BTN_FB_LOGIN, false);
		MonoBehaviourSingleton<SoundManager>.I.TransitionTo("Opening", 1f);
		titleAnimation.Play("Tap");
		Fade(Color.white, 0f, 1f, 1f, delegate
		{
			titleObjectRoot.SetActive(false);
			cutOP.SetActive(true);
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
			yield return (object)new WaitForSeconds(seq.delay);
			if (!hasSkipped)
			{
				SoundManager.PlayVoice(seq.id, 1f, 0u, null, null);
			}
		}
	}

	private IEnumerator DoSEPlay()
	{
		AudioSequeceData[] array = audio_sequence;
		foreach (AudioSequeceData seq in array)
		{
			yield return (object)new WaitForSeconds(seq.delay);
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
			float alpha = Mathf.Lerp(_from, _to, timer / duration);
			whiteFadeMaterial.SetColor("_Color", new Color(baseColor.r, baseColor.g, baseColor.b, alpha));
			yield return (object)null;
		}
		onComplete?.Invoke();
	}

	public override void UpdateUI()
	{
		SetApplicationVersionText(UI.LBL_APP_VERSION);
		if (MonoBehaviourSingleton<GlobalSettingsManager>.IsValid() && MonoBehaviourSingleton<GlobalSettingsManager>.I.submissionVersion)
		{
			SetActive(UI.BTN_ADVANCED_LOGIN, false);
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
		if (!isCacheClear)
		{
			if (!MonoBehaviourSingleton<LoadingProcess>.IsValid())
			{
				MonoBehaviourSingleton<AppMain>.I.gameObject.AddComponent<LoadingProcess>();
			}
			MonoBehaviourSingleton<AppMain>.I.gameObject.AddComponent<InGameTutorialManager>();
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
