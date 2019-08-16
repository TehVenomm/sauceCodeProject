using Network;
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
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		if (isFirstBoot && CheckTitleSkip())
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
			wait = true;
			MonoBehaviourSingleton<ClanMatchingManager>.I.SendInfo(delegate
			{
				wait = false;
			});
			while (wait)
			{
				yield return null;
			}
			SetActiveUI(enable: false);
			base.Initialize();
			yield break;
		}
		LoadingQueue load_queue = new LoadingQueue(this);
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_title_01");
		LoadObject lo_director = load_queue.Load(RESOURCE_CATEGORY.CUTSCENE, "InGameTutorialDirector");
		LoadObject lo_tap = load_queue.Load(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_title_04");
		while (load_queue.IsLoading())
		{
			yield return null;
		}
		Transform director_t = ResourceUtility.Realizes(lo_director.loadedObject);
		if (director_t != null)
		{
			director = director_t.GetComponent<TutorialBossDirector>();
			if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.NeedModifyTitleTop)
			{
				DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
				director.logo.camera.set_orthographicSize(specialDeviceInfo.TitleTopCameraSize);
				director.logo.bg.get_transform().set_localScale(specialDeviceInfo.TitleTopBGScale);
			}
			director.StartLogoAnimation(tutorial_flag: false, null, delegate
			{
				SetActiveUI(enable: true);
			});
			MonoBehaviourSingleton<AppMain>.I.mainCamera.GetComponent<RenderTargetCacher>().set_enabled(false);
		}
		else
		{
			SetActiveUI(enable: true);
		}
		tapPrefab = (lo_tap.loadedObject as GameObject);
		base.Initialize();
	}

	private void SetActiveUI(bool enable)
	{
		GetCtrl(UI.Container).get_gameObject().SetActive(enable);
	}

	public override void UpdateUI()
	{
		SetApplicationVersionText(UI.LBL_APP_VERSION);
		SetVisibleWidgetEffect(UI.TEX_BG, "ef_ui_title_01");
		if (MonoBehaviourSingleton<GlobalSettingsManager>.I.submissionVersion)
		{
			SetActive((Enum)UI.BTN_ADVANCED_LOGIN, is_visible: false);
		}
		else
		{
			SetActive((Enum)UI.BTN_ADVANCED_LOGIN, !MonoBehaviourSingleton<AccountManager>.I.account.IsRegist());
		}
	}

	public override void Exit()
	{
		base.Exit();
		if (director != null)
		{
			Object.Destroy(director.get_gameObject());
			MonoBehaviourSingleton<AppMain>.I.mainCamera.GetComponent<RenderTargetCacher>().set_enabled(true);
		}
	}

	private bool IsAgreement()
	{
		if (!MonoBehaviourSingleton<AccountManager>.I.account.IsRegist())
		{
			return false;
		}
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep < 9)
		{
			return false;
		}
		return MonoBehaviourSingleton<AccountManager>.I.termsCheck;
	}

	private void OnQuery_PUSH_START()
	{
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		if (null != tapEffect)
		{
			return;
		}
		GameSection.StayEvent();
		MonoBehaviourSingleton<LoungeMatchingManager>.I.SendInfo(delegate(bool is_success)
		{
			MonoBehaviourSingleton<ClanMatchingManager>.I.RequestUserDetail(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, delegate(UserClanData userClanData)
			{
				MonoBehaviourSingleton<UserInfoManager>.I.SetUserClan(userClanData);
				MonoBehaviourSingleton<ClanMatchingManager>.I.SendInfo(delegate(bool is_success_clan)
				{
					GameSection.ResumeEvent(is_success && is_success_clan);
				});
			});
			GameSection.ResumeEvent(is_success);
		});
		tapEffect = ResourceUtility.Realizes(tapPrefab);
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
		SetActive((Enum)UI.BTN_CLEARCACHE, is_visible: false);
		this.StartCoroutine(DelayStart());
	}

	private IEnumerator DelayStart()
	{
		yield return (object)new WaitForSeconds(0.5f);
		SetActive((Enum)UI.BTN_START, is_visible: false);
		yield return (object)new WaitForSeconds(1.5f);
		DispatchEvent("START");
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
					GameSection.ChangeStayEvent("OPENING");
				}
				GameSection.ResumeEvent(is_success);
			});
		}
		else if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep > 0 && MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep <= 2)
		{
			if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep == 1 || MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep == 2)
			{
			}
			DispatchEvent("MAIN_MENU_HOME");
		}
		if (!TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.END))
		{
			Protocol.Force(delegate
			{
				MonoBehaviourSingleton<UserInfoManager>.I.SendTutorialStep(delegate
				{
				});
			});
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
			DispatchEvent("START");
		}
		else if (MonoBehaviourSingleton<SoundManager>.IsValid())
		{
			SoundManager.RequestBGM(1);
		}
		isFirstBoot = false;
	}

	public static bool CheckTitleSkip()
	{
		if (MonoBehaviourSingleton<AccountManager>.I.account.IsRegist() && 1 <= MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep)
		{
			return true;
		}
		return false;
	}
}
