using rhyme;
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
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		if (isFirstBoot && CheckTitleSkip())
		{
			bool wait = true;
			MonoBehaviourSingleton<LoungeMatchingManager>.I.SendInfo(delegate
			{
				((_003CDoInitialize_003Ec__Iterator171)/*Error near IL_004b: stateMachine*/)._003Cwait_003E__0 = false;
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
			if ((Object)director_t != (Object)null)
			{
				director = director_t.GetComponent<TutorialBossDirector>();
				director.StartLogoAnimation(false, null, delegate
				{
					((_003CDoInitialize_003Ec__Iterator171)/*Error near IL_0163: stateMachine*/)._003C_003Ef__this.SetActiveUI(true);
				});
				MonoBehaviourSingleton<AppMain>.I.mainCamera.GetComponent<RenderTargetCacher>().enabled = false;
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
		GetCtrl(UI.Container).gameObject.SetActive(enable);
	}

	public override void UpdateUI()
	{
		SetApplicationVersionText(UI.LBL_APP_VERSION);
		SetVisibleWidgetEffect(UI.TEX_BG, "ef_ui_title_01");
		if (MonoBehaviourSingleton<GlobalSettingsManager>.I.submissionVersion)
		{
			SetActive(UI.BTN_ADVANCED_LOGIN, false);
		}
		else
		{
			SetActive(UI.BTN_ADVANCED_LOGIN, !MonoBehaviourSingleton<AccountManager>.I.account.IsRegist());
		}
	}

	public override void Exit()
	{
		base.Exit();
		if ((Object)director != (Object)null)
		{
			Object.Destroy(director.gameObject);
			MonoBehaviourSingleton<AppMain>.I.mainCamera.GetComponent<RenderTargetCacher>().enabled = true;
		}
	}

	private void OnQuery_PUSH_START()
	{
		if (!((Object)null != (Object)tapEffect))
		{
			GameSection.StayEvent();
			MonoBehaviourSingleton<LoungeMatchingManager>.I.SendInfo(delegate(bool is_success)
			{
				GameSection.ResumeEvent(is_success, null);
			}, false);
			tapEffect = ResourceUtility.Realizes(tapPrefab, -1);
			if ((Object)null != (Object)tapEffect)
			{
				rymFX component = tapEffect.GetComponent<rymFX>();
				if ((Object)null != (Object)component && (Object)null != (Object)director)
				{
					component.Cameras = new Camera[1]
					{
						director.logoCamera
					};
				}
				tapEffect.localPosition = new Vector3(0f, 1000f, 0.1f);
				tapEffect.localScale = new Vector3(11f, 11f, 1f);
				tapEffect.gameObject.SetActive(true);
			}
			SetActive(UI.BTN_CLEARCACHE, false);
			StartCoroutine(DelayStart());
		}
	}

	private IEnumerator DelayStart()
	{
		yield return (object)new WaitForSeconds(0.5f);
		SetActive(UI.BTN_START, false);
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

	private bool CheckTitleSkip()
	{
		if (MonoBehaviourSingleton<AccountManager>.I.account.IsRegist() && 1 <= MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep)
		{
			return true;
		}
		return false;
	}
}
