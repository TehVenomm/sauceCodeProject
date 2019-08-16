using System;
using System.Collections;
using UnityEngine;

public class AppClose : GameSection
{
	private enum UI
	{
		LBL_APP_VERSION,
		TEX_BG,
		Container,
		BTN_SERVICE,
		BTN_REPAYMENT,
		LBL_SERVICE_MESSAGE
	}

	private const string EFFECT01_NAME = "ef_ui_title_01";

	private TutorialBossDirector director;

	private GameObject titleObjectRoot;

	public override void Initialize()
	{
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		LoadingQueue load_queue = new LoadingQueue(this);
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_title_01");
		LoadObject lo_director = load_queue.Load(RESOURCE_CATEGORY.CUTSCENE, "InGameTutorialDirector");
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
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetApplicationVersionText(UI.LBL_APP_VERSION);
		string text = MonoBehaviourSingleton<AccountManager>.I.closedNotice.Replace("<BR>", "\n").Replace("<br>", "\n");
		SetLabelText((Enum)UI.LBL_SERVICE_MESSAGE, text);
		SetActive((Enum)UI.BTN_REPAYMENT, MonoBehaviourSingleton<AccountManager>.I.openRefundForm);
		base.UpdateUI();
	}

	private void SetActiveUI(bool enable)
	{
		GetCtrl(UI.Container).get_gameObject().SetActive(enable);
	}

	private void OnQuery_SERVICE()
	{
		GameSection.SetEventData("refund/top");
	}

	private void OnQuery_REPAYMENT()
	{
		GameSection.SetEventData("refund/form");
	}
}
