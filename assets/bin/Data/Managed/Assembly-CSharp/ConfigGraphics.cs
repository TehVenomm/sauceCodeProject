using UnityEngine;

public class ConfigGraphics : GameSection
{
	protected enum UI
	{
		SPR_BG_FRAME,
		SCR_ROOT,
		DSV_ROOT,
		TGL_DRAW_LOW,
		TGL_DRAW_STANDARD,
		TGL_DRAW_HIGH,
		TGL_DRAW_HIGHEST,
		TGL_HEAD_NAME_ON,
		TGL_HEAD_NAME_OFF,
		TGL_AUTO_ROTATION_ON,
		TGL_AUTO_ROTATION_OFF,
		TGL_MINIMAP_ENEMY_ON,
		TGL_MINIMAP_ENEMY_OFF,
		TGL_ARROW_CAMERA_A,
		TGL_ARROW_CAMERA_B,
		BTN_DRAW_LOW,
		BTN_DRAW_STANDARD,
		BTN_DRAW_HIGH,
		BTN_DRAW_HIGHEST,
		BTN_HEAD_NAME_ON,
		BTN_HEAD_NAME_OFF,
		BTN_AUTO_ROTATION_ON,
		BTN_AUTO_ROTATION_OFF,
		BTN_MINIMAP_ENEMY_ON,
		BTN_MINIMAP_ENEMY_OFF,
		BTN_ARROW_CAMERA_A,
		BTN_ARROW_CAMERA_B
	}

	public override void Initialize()
	{
		base.Initialize();
		GetCtrl(UI.DSV_ROOT).GetComponent<Collider>().enabled = false;
	}

	public override void UpdateUI()
	{
		int graphicOptionType = InGameManager.GetGraphicOptionType(GameSaveData.instance.graphicOptionKey);
		int arrowCameraType = InGameManager.GetArrowCameraType(GameSaveData.instance.arrowCameraKey);
		SetToggle(UI.TGL_DRAW_LOW, graphicOptionType == 0);
		SetToggle(UI.TGL_DRAW_STANDARD, graphicOptionType == 1);
		SetToggle(UI.TGL_DRAW_HIGH, graphicOptionType == 2);
		SetToggle(UI.TGL_DRAW_HIGHEST, graphicOptionType == 3);
		SetToggle(UI.TGL_HEAD_NAME_ON, GameSaveData.instance.headName);
		SetToggle(UI.TGL_HEAD_NAME_OFF, !GameSaveData.instance.headName);
		SetToggle(UI.TGL_AUTO_ROTATION_ON, GameSaveData.instance.enableLandscape);
		SetToggle(UI.TGL_AUTO_ROTATION_OFF, !GameSaveData.instance.enableLandscape);
		SetToggle(UI.TGL_MINIMAP_ENEMY_ON, GameSaveData.instance.enableMinimapEnemy);
		SetToggle(UI.TGL_MINIMAP_ENEMY_OFF, !GameSaveData.instance.enableMinimapEnemy);
		SetToggle(UI.TGL_ARROW_CAMERA_A, arrowCameraType == 0);
		SetToggle(UI.TGL_ARROW_CAMERA_B, arrowCameraType == 1);
		SetButtonEnabled(UI.BTN_DRAW_LOW, graphicOptionType != 0);
		SetButtonEnabled(UI.BTN_DRAW_STANDARD, graphicOptionType != 1);
		SetButtonEnabled(UI.BTN_DRAW_HIGH, graphicOptionType != 2);
		SetButtonEnabled(UI.BTN_DRAW_HIGHEST, graphicOptionType != 3);
		SetButtonEnabled(UI.BTN_HEAD_NAME_ON, !GameSaveData.instance.headName);
		SetButtonEnabled(UI.BTN_HEAD_NAME_OFF, GameSaveData.instance.headName);
		SetButtonEnabled(UI.BTN_AUTO_ROTATION_ON, !GameSaveData.instance.enableLandscape);
		SetButtonEnabled(UI.BTN_AUTO_ROTATION_OFF, GameSaveData.instance.enableLandscape);
		SetButtonEnabled(UI.BTN_MINIMAP_ENEMY_ON, !GameSaveData.instance.enableMinimapEnemy);
		SetButtonEnabled(UI.BTN_MINIMAP_ENEMY_OFF, GameSaveData.instance.enableMinimapEnemy);
		SetButtonEnabled(UI.BTN_ARROW_CAMERA_A, arrowCameraType != 0);
		SetButtonEnabled(UI.BTN_ARROW_CAMERA_B, arrowCameraType != 1);
		base.UpdateUI();
	}

	private void OnQuery_DRAW_LOW()
	{
		GameSaveData.instance.graphicOptionKey = "low";
		RefreshUI();
		if (MonoBehaviourSingleton<InGameManager>.IsValid())
		{
			MonoBehaviourSingleton<InGameManager>.I.UpdateConfig();
		}
		MonoBehaviourSingleton<AppMain>.I.UpdateResolution(Screen.width < Screen.height);
		Application.targetFrameRate = 30;
	}

	private void OnQuery_DRAW_STANDARD()
	{
		GameSaveData.instance.graphicOptionKey = "standard";
		RefreshUI();
		if (MonoBehaviourSingleton<InGameManager>.IsValid())
		{
			MonoBehaviourSingleton<InGameManager>.I.UpdateConfig();
		}
		MonoBehaviourSingleton<AppMain>.I.UpdateResolution(Screen.width < Screen.height);
		Application.targetFrameRate = 30;
	}

	private void OnQuery_DRAW_HIGH()
	{
		GameSaveData.instance.graphicOptionKey = "high";
		RefreshUI();
		if (MonoBehaviourSingleton<InGameManager>.IsValid())
		{
			MonoBehaviourSingleton<InGameManager>.I.UpdateConfig();
		}
		MonoBehaviourSingleton<AppMain>.I.UpdateResolution(Screen.width < Screen.height);
		Application.targetFrameRate = 30;
	}

	private void OnQuery_DRAW_HIGHEST()
	{
		GameSaveData.instance.graphicOptionKey = "highest";
		RefreshUI();
		if (MonoBehaviourSingleton<InGameManager>.IsValid())
		{
			MonoBehaviourSingleton<InGameManager>.I.UpdateConfig();
		}
		MonoBehaviourSingleton<AppMain>.I.UpdateResolution(Screen.width < Screen.height);
		Application.targetFrameRate = 120;
		Time.fixedDeltaTime = 0.008333335f;
	}

	private void OnQuery_DRAW_HEAD_NAME_ON()
	{
		GameSaveData.instance.headName = true;
		RefreshUI();
	}

	protected virtual void OnQuery_DRAW_HEAD_NAME_OFF()
	{
		GameSaveData.instance.headName = false;
		RefreshUI();
	}

	protected virtual void OnQuery_AUTO_ROTATION_ON()
	{
		GameSaveData.instance.enableLandscape = true;
		RefreshUI();
	}

	protected virtual void OnQuery_AUTO_ROTATION_OFF()
	{
		GameSaveData.instance.enableLandscape = false;
		RefreshUI();
	}

	protected virtual void OnQuery_MINIMAP_ENEMY_ON()
	{
		GameSaveData.instance.enableMinimapEnemy = true;
		RefreshUI();
	}

	protected virtual void OnQuery_MINIMAP_ENEMY_OFF()
	{
		GameSaveData.instance.enableMinimapEnemy = false;
		RefreshUI();
	}

	private void OnQuery_ARROW_CAMERA_A()
	{
		GameSaveData.instance.arrowCameraKey = "typea";
		RefreshUI();
		if (MonoBehaviourSingleton<InGameManager>.IsValid())
		{
			MonoBehaviourSingleton<InGameManager>.I.UpdateConfig();
		}
		if (MonoBehaviourSingleton<InGameCameraManager>.IsValid())
		{
			MonoBehaviourSingleton<InGameCameraManager>.I.SetArrowCameraMode(0);
		}
	}

	private void OnQuery_ARROW_CAMERA_B()
	{
		GameSaveData.instance.arrowCameraKey = "typeb";
		RefreshUI();
		if (MonoBehaviourSingleton<InGameManager>.IsValid())
		{
			MonoBehaviourSingleton<InGameManager>.I.UpdateConfig();
		}
		if (MonoBehaviourSingleton<InGameCameraManager>.IsValid())
		{
			MonoBehaviourSingleton<InGameCameraManager>.I.SetArrowCameraMode(1);
		}
	}
}
