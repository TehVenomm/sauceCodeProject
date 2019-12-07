using System;
using System.Collections;

public class InGameQuestAreaDeliveryList : QuestAreaDeliveryList
{
	private bool isInActiveRotate;

	protected override bool showStory => false;

	protected override bool showMap => false;

	public override void Initialize()
	{
		int id = (int)GameSection.GetEventData();
		regionData = Singleton<RegionTable>.I.GetData((uint)id);
		base.Initialize();
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += OnScreenRotate;
			isInActiveRotate = true;
		}
	}

	protected override IEnumerator DoInitialize()
	{
		SetActive(UI.OBJ_IMAGE, is_visible: false);
		GetDeliveryList();
		EndInitialize();
		yield break;
	}

	public override void Exit()
	{
		base.Exit();
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= OnScreenRotate;
		}
	}

	public override void UpdateUI()
	{
		if (isInActiveRotate && MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			Reposition(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
		}
		isInActiveRotate = false;
		base.UpdateUI();
	}

	private void Reposition(bool isPortrait)
	{
		UIScreenRotationHandler[] components = GetCtrl(UI.OBJ_FRAME).GetComponents<UIScreenRotationHandler>();
		for (int i = 0; i < components.Length; i++)
		{
			components[i].InvokeRotate();
		}
		GetCtrl(UI.SPR_BG_FRAME).GetComponent<UIScreenRotationHandler>().InvokeRotate();
		GetCtrl(UI.SCR_DELIVERY_QUEST).GetComponent<UIScreenRotationHandler>().InvokeRotate();
		UpdateAnchors();
		GetCtrl(UI.SCR_DELIVERY_QUEST).GetComponent<UIScrollView>().ResetPosition();
		AppMain i2 = MonoBehaviourSingleton<AppMain>.I;
		i2.onDelayCall = (Action)Delegate.Combine(i2.onDelayCall, (Action)delegate
		{
			RefreshUI();
			GetCtrl(UI.SCR_DELIVERY_QUEST).GetComponent<UIPanel>().Refresh();
		});
	}

	private void OnScreenRotate(bool isPortrait)
	{
		if (base.transferUI != null)
		{
			isInActiveRotate = !base.transferUI.gameObject.activeInHierarchy;
		}
		else
		{
			isInActiveRotate = !base.collectUI.gameObject.activeInHierarchy;
		}
		if (!isInActiveRotate)
		{
			Reposition(isPortrait);
		}
	}
}
