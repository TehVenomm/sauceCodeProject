public class InGameDetailUseItemDialog : ItemDetailUseItemDialog
{
	private bool isInActiveRotate;

	public override void Initialize()
	{
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
		base.UpdateUI();
	}

	private void Reposition(bool isPortrait)
	{
		GetCtrl(UI.OBJ_BACK).GetComponent<UIScreenRotationHandler>().InvokeRotate();
		GetCtrl(UI.BTN_USE).GetComponent<UIScreenRotationHandler>().InvokeRotate();
		UpdateAnchors();
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

	protected void OnQuery_InGameDetailUseConfirm_YES()
	{
		SendUseItem();
	}

	protected void OnQuery_InGameDetailUseOverWriteConfirm_YES()
	{
		SendUseItem();
	}
}
