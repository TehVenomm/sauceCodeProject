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
		SetProgressInt(UI.SLD_TOUCH_FLICK, Mathf.RoundToInt(GameSaveData.instance.touchInGameFlick * 100f), 0, 100, OnChangeTouchFlick);
		SetProgressInt(UI.SLD_TOUCH_LONG, Mathf.RoundToInt(GameSaveData.instance.touchInGameLong * 100f), 0, 100, OnChangeTouchLong);
	}

	private void OnChangeTouchFlick()
	{
		GameSaveData.instance.touchInGameFlick = (float)GetProgressInt(UI.SLD_TOUCH_FLICK) * 0.01f;
		if (MonoBehaviourSingleton<InputManager>.IsValid())
		{
			MonoBehaviourSingleton<InputManager>.I.UpdateConfigInput();
		}
	}

	private void OnChangeTouchLong()
	{
		GameSaveData.instance.touchInGameLong = (float)GetProgressInt(UI.SLD_TOUCH_LONG) * 0.01f;
	}
}
