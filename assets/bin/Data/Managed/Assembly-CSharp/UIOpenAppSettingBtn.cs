using UnityEngine;

public class UIOpenAppSettingBtn
{
	private BootProcess currentBootProcess;

	public UIOpenAppSettingBtn()
		: this()
	{
	}

	public void SetBootProcess(BootProcess pro)
	{
		currentBootProcess = pro;
	}

	public void OpenAppSetting()
	{
		AndroidPermissionsManager.OpenAppSetting();
	}

	public void QuitApp()
	{
		Application.Quit();
	}

	public void AskPermission()
	{
		currentBootProcess.OnGrantButtonPress();
		MonoBehaviourSingleton<UIManager>.I.loading.ShowEmptyFirstLoad(true);
		MonoBehaviourSingleton<UIManager>.I.loading.HideAllTextMsg();
	}

	private void OnEnable()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		currentBootProcess = MonoBehaviourSingleton<AppMain>.I.get_gameObject().GetComponent<BootProcess>();
	}

	private void OnDisable()
	{
		currentBootProcess = null;
	}
}
