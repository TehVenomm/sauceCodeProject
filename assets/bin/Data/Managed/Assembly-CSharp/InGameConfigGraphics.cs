using System;
using System.Collections.Generic;
using UnityEngine;

public class InGameConfigGraphics : ConfigGraphics
{
	private bool isInActiveRotate;

	public override void Initialize()
	{
		base.Initialize();
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += OnScreenRotate;
			isInActiveRotate = true;
			GetCtrl(UI.DSV_ROOT).GetComponent<Collider>().enabled = !MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait;
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
		base.UpdateUI();
	}

	private void Reposition(bool isPortrait)
	{
		GetCtrl(UI.SPR_BG_FRAME).GetComponent<UIScreenRotationHandler>().InvokeRotate();
		GetCtrl(UI.SCR_ROOT).GetComponent<UIScreenRotationHandler>().InvokeRotate();
		UpdateAnchors();
		UIScrollView component = GetCtrl(UI.SCR_ROOT).GetComponent<UIScrollView>();
		component.ResetPosition();
		AppMain i = MonoBehaviourSingleton<AppMain>.I;
		i.onDelayCall = (Action)Delegate.Combine(i.onDelayCall, (Action)delegate
		{
			RefreshUI();
			UIPanel component2 = GetCtrl(UI.SCR_ROOT).GetComponent<UIPanel>();
			component2.Refresh();
		});
		GetCtrl(UI.DSV_ROOT).GetComponent<Collider>().enabled = !isPortrait;
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

	protected override void OnQuery_DRAW_HEAD_NAME_OFF()
	{
		base.OnQuery_DRAW_HEAD_NAME_OFF();
		List<StageObject> enemyList = MonoBehaviourSingleton<StageObjectManager>.I.enemyList;
		int i = 0;
		for (int count = enemyList.Count; i < count; i++)
		{
			Enemy enemy = enemyList[i] as Enemy;
			enemy.DeleteStatusGizmo();
		}
	}

	protected override void OnQuery_AUTO_ROTATION_ON()
	{
		base.OnQuery_AUTO_ROTATION_ON();
		GameSceneGlobalSettings.SetOrientation(true);
	}

	protected override void OnQuery_AUTO_ROTATION_OFF()
	{
		base.OnQuery_AUTO_ROTATION_OFF();
		GameSceneGlobalSettings.SetOrientation(true);
	}

	protected override void OnQuery_MINIMAP_ENEMY_ON()
	{
		base.OnQuery_MINIMAP_ENEMY_ON();
		RefreshEnemyMiniMap(true);
	}

	protected override void OnQuery_MINIMAP_ENEMY_OFF()
	{
		base.OnQuery_MINIMAP_ENEMY_OFF();
		RefreshEnemyMiniMap(false);
	}

	private void RefreshEnemyMiniMap(bool is_enable)
	{
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && MonoBehaviourSingleton<MiniMap>.IsValid())
		{
			MonoBehaviourSingleton<StageObjectManager>.I.enemyList.ForEach(delegate(StageObject o)
			{
				Enemy root_object = o as Enemy;
				if (is_enable)
				{
					MonoBehaviourSingleton<MiniMap>.I.Attach(root_object);
				}
				else
				{
					MonoBehaviourSingleton<MiniMap>.I.Detach(root_object);
				}
			});
		}
	}
}
