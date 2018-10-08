using System;
using UnityEngine;

public class ConfigTouch : GameSection
{
	private enum UI
	{
		SLD_TOUCH_FLICK,
		SLD_TOUCH_LONG
	}

	public override void Initialize()
	{
		base.Initialize();
	}

	public override void Exit()
	{
		GameSaveData.Save();
		base.Exit();
	}

	public override void UpdateUI()
	{
		SetProgressInt((Enum)UI.SLD_TOUCH_FLICK, Mathf.RoundToInt(GameSaveData.instance.touchInGameFlick * 100f), 0, 100, (EventDelegate.Callback)OnChangeTouchFlick);
		SetProgressInt((Enum)UI.SLD_TOUCH_LONG, Mathf.RoundToInt(GameSaveData.instance.touchInGameLong * 100f), 0, 100, (EventDelegate.Callback)OnChangeTouchLong);
	}

	private void OnChangeTouchFlick()
	{
		GameSaveData.instance.touchInGameFlick = (float)GetProgressInt((Enum)UI.SLD_TOUCH_FLICK) * 0.01f;
		if (MonoBehaviourSingleton<InputManager>.IsValid())
		{
			MonoBehaviourSingleton<InputManager>.I.UpdateConfigInput();
		}
	}

	private void OnChangeTouchLong()
	{
		GameSaveData.instance.touchInGameLong = (float)GetProgressInt((Enum)UI.SLD_TOUCH_LONG) * 0.01f;
	}
}
