using System;

public class InGameShopItemSelect : ShopItemSelect
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
		GetCtrl(UI.OBJ_FRAME).GetComponent<UIScreenRotationHandler>().InvokeRotate();
		GetCtrl(UI.OBJ_FRAME).GetComponent<UIRect>().UpdateAnchors();
		UpdateAnchors();
		UIScrollView component = GetCtrl(UI.SCR_LIST).GetComponent<UIScrollView>();
		component.ResetPosition();
		AppMain i = MonoBehaviourSingleton<AppMain>.I;
		i.onDelayCall = (Action)Delegate.Combine(i.onDelayCall, (Action)delegate
		{
			RefreshUI();
			UIPanel component2 = GetCtrl(UI.SCR_LIST).GetComponent<UIPanel>();
			component2.Refresh();
		});
	}

	private void OnScreenRotate(bool isPortrait)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		if (base.transferUI != null)
		{
			isInActiveRotate = !base.transferUI.get_gameObject().get_activeInHierarchy();
		}
		else
		{
			isInActiveRotate = !base.collectUI.get_gameObject().get_activeInHierarchy();
		}
		if (!isInActiveRotate)
		{
			Reposition(isPortrait);
		}
	}

	private void OnQuery_InGameShopItemConfirm_YES()
	{
		OnQuery_ShopItemConfirm_YES();
	}
}
