using System;
using System.Collections;
using UnityEngine;

public class InGameQuestEventSelectList : QuestEventSelectList
{
	private bool isInActiveRotate;

	protected override bool showStory => false;

	protected override bool showMap => false;

	public override void Initialize()
	{
		base.Initialize();
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += OnScreenRotate;
			isInActiveRotate = true;
		}
	}

	protected override IEnumerator DoInitialize()
	{
		SetActive(UI.OBJ_IMAGE, false);
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
		SetActive(UI.BTN_INGAME_INFO, !string.IsNullOrEmpty(eventData.linkName));
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
		UIScrollView component = GetCtrl(UI.SCR_DELIVERY_QUEST).GetComponent<UIScrollView>();
		component.ResetPosition();
		AppMain i2 = MonoBehaviourSingleton<AppMain>.I;
		i2.onDelayCall = (Action)Delegate.Combine(i2.onDelayCall, (Action)delegate
		{
			RefreshUI();
			UIPanel component2 = GetCtrl(UI.SCR_DELIVERY_QUEST).GetComponent<UIPanel>();
			component2.Refresh();
		});
	}

	private void OnScreenRotate(bool isPortrait)
	{
		if ((UnityEngine.Object)base.transferUI != (UnityEngine.Object)null)
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

	protected override void OnQuery_INFO()
	{
		string eventData = string.Format(WebViewManager.NewsWithLinkParamFormatFromInGame, base.eventData.linkName);
		GameSection.SetEventData(eventData);
	}
}
