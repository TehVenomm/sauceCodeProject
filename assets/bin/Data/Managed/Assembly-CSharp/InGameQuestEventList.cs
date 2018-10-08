using System;
using UnityEngine;

public class InGameQuestEventList : QuestEventList
{
	private bool isInActiveRotate;

	public override void Initialize()
	{
		isInGame = true;
		base.Initialize();
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += OnScreenRotate;
			isInActiveRotate = true;
		}
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
		UpdateEventList();
		UpdateCheck();
	}

	protected override bool IsCarnival()
	{
		return false;
	}

	private void Reposition(bool isPortrait)
	{
		UIScreenRotationHandler[] components = GetCtrl(UI.OBJ_FRAME).GetComponents<UIScreenRotationHandler>();
		for (int i = 0; i < components.Length; i++)
		{
			components[i].InvokeRotate();
		}
		GetCtrl(UI.BTN_EVENT).GetComponent<UIScreenRotationHandler>().InvokeRotate();
		GetCtrl(UI.SPR_BG_FRAME).GetComponent<UIScreenRotationHandler>().InvokeRotate();
		UpdateAnchors();
		UIScrollView component = GetCtrl(UI.SCR_EVENT_QUEST).GetComponent<UIScrollView>();
		component.ResetPosition();
		AppMain i2 = MonoBehaviourSingleton<AppMain>.I;
		i2.onDelayCall = (Action)Delegate.Combine(i2.onDelayCall, (Action)delegate
		{
			RefreshUI();
			UIPanel component2 = GetCtrl(UI.SCR_EVENT_QUEST).GetComponent<UIPanel>();
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
}
