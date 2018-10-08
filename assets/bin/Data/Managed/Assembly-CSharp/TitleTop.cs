using rhyme;
using System;
using System.Collections;
using UnityEngine;

public class TitleTop : GameSection
{
	private enum UI
	{
		LBL_APP_VERSION,
		TEX_BG,
		Container,
		BTN_START,
		BTN_ADVANCED_LOGIN,
		BTN_CLEARCACHE
	}

	private const string EFFECT01_NAME = "ef_ui_title_01";

	private const string EFFECT04_NAME = "ef_ui_title_04";

	private TutorialBossDirector director;

	private GameObject tapPrefab;

	private Transform tapEffect;

	public static bool isFirstBoot = true;

	public override bool useOnPressBackKey => true;

	public override void OnPressBackKey()
	{
		Native.applicationQuit();
	}

	public override void Initialize()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoInitialize());
	}

	private unsafe IEnumerator DoInitialize()
	{
		if (isFirstBoot && CheckTitleSkip())
		{
			bool wait = true;
			MonoBehaviourSingleton<LoungeMatchingManager>.I.SendInfo(delegate
			{
				((_003CDoInitialize_003Ec__Iterator179)/*Error near IL_0045: stateMachine*/)._003Cwait_003E__0 = false;
			}, false);
			while (wait)
			{
				yield return (object)null;
			}
			SetActiveUI(false);
			base.Initialize();
		}
		else
		{
			LoadingQueue load_queue = new LoadingQueue(this);
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_title_01");
			LoadObject lo_director = load_queue.Load(RESOURCE_CATEGORY.CUTSCENE, "InGameTutorialDirector", false);
			LoadObject lo_tap = load_queue.Load(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_title_04", false);
			while (load_queue.IsLoading())
			{
				yield return (object)null;
			}
			Transform director_t = ResourceUtility.Realizes(lo_director.loadedObject, -1);
			if (director_t != null)
			{
				director = director_t.GetComponent<TutorialBossDirector>();
				director.StartLogoAnimation(false, null, new Action((object)/*Error near IL_015d: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
				MonoBehaviourSingleton<AppMain>.I.mainCamera.GetComponent<RenderTargetCacher>().set_enabled(false);
			}
			else
			{
				SetActiveUI(true);
			}
			tapPrefab = (lo_tap.loadedObject as GameObject);
			base.Initialize();
		}
	}

	private void SetActiveUI(bool enable)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		GetCtrl(UI.Container).get_gameObject().SetActive(enable);
	}

	public override void UpdateUI()
	{
		SetApplicationVersionText(UI.LBL_APP_VERSION);
		SetVisibleWidgetEffect(UI.TEX_BG, "ef_ui_title_01");
		if (MonoBehaviourSingleton<GlobalSettingsManager>.I.submissionVersion)
		{
			SetActive((Enum)UI.BTN_ADVANCED_LOGIN, false);
		}
		else
		{
			SetActive((Enum)UI.BTN_ADVANCED_LOGIN, !MonoBehaviourSingleton<AccountManager>.I.account.IsRegist());
		}
	}

	public override void Exit()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		base.Exit();
		if (director != null)
		{
			Object.Destroy(director.get_gameObject());
			MonoBehaviourSingleton<AppMain>.I.mainCamera.GetComponent<RenderTargetCacher>().set_enabled(true);
		}
	}

	private void OnQuery_PUSH_START()
	{
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		if (!(null != tapEffect))
		{
			GameSection.StayEvent();
			MonoBehaviourSingleton<LoungeMatchingManager>.I.SendInfo(delegate(bool is_success)
			{
				GameSection.ResumeEvent(is_success, null);
			}, false);
			tapEffect = ResourceUtility.Realizes(tapPrefab, -1);
			if (null != tapEffect)
			{
				rymFX component = tapEffect.GetComponent<rymFX>();
				if (null != component && null != director)
				{
					component.Cameras = (Camera[])new Camera[1]
					{
						director.logoCamera
					};
				}
				tapEffect.set_localPosition(new Vector3(0f, 1000f, 0.1f));
				tapEffect.set_localScale(new Vector3(11f, 11f, 1f));
				tapEffect.get_gameObject().SetActive(true);
			}
			SetActive((Enum)UI.BTN_CLEARCACHE, false);
			this.StartCoroutine(DelayStart());
		}
	}

	private IEnumerator DelayStart()
	{
		yield return (object)new WaitForSeconds(0.5f);
		SetActive((Enum)UI.BTN_START, false);
		yield return (object)new WaitForSeconds(1.5f);
		DispatchEvent("START", null);
	}

	private void OnQuery_START()
	{
		if (!MonoBehaviourSingleton<AccountManager>.I.account.IsRegist())
		{
			GameSection.StayEvent();
			MonoBehaviourSingleton<AccountManager>.I.SendRegistCreate(delegate(bool is_success)
			{
				if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name == "/colopl_rob")
				{
					GameSection.ChangeStayEvent("OPENING", null);
				}
				GameSection.ResumeEvent(is_success, null);
			});
		}
		else if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep > 0 && MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep <= 2)
		{
			if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep == 1)
			{
				MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Title", "CharaMake", UITransition.TYPE.CLOSE, UITransition.TYPE.OPEN, false);
			}
			else if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep == 2)
			{
				DispatchEvent("MAIN_MENU_HOME", null);
			}
			else
			{
				DispatchEvent("TUTORIAL_" + MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep.ToString(), null);
			}
		}
	}

	private void OnQuery_HOST_SELECT()
	{
	}

	private void OnQuery_TitleClearCacheConfirm_YES()
	{
		MenuReset.needClearCache = true;
		MenuReset.needPredownload = true;
	}

	public override void StartSection()
	{
		if (isFirstBoot && CheckTitleSkip())
		{
			DispatchEvent("START", null);
		}
		else if (MonoBehaviourSingleton<SoundManager>.IsValid())
		{
			SoundManager.RequestBGM(1, true);
		}
		isFirstBoot = false;
	}

	public static bool CheckTitleSkip()
	{
		if (!MonoBehaviourSingleton<AccountManager>.I.account.IsRegist())
		{
			return false;
		}
		return 1 <= MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep;
	}
}
