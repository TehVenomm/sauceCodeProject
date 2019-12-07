using System;

public class ConfigTop : GameSection
{
	private enum UI
	{
		BTN_STOPPER_ON,
		BTN_STOPPER_OFF,
		BTN_PP_ON,
		BTN_PP_OFF,
		SPR_STOPPER_ON,
		SPR_STOPPER_OFF,
		SPR_PP_ON,
		SPR_PP_OFF,
		TGL_STOPPER_ON,
		TGL_STOPPER_OFF,
		TGL_PP_ON,
		TGL_PP_OFF
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_USER_INFO;
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		bool flag = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.isStopperSet != 0;
		bool flag2 = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.isParentPassSet != 0;
		SetToggle(UI.TGL_STOPPER_ON, flag);
		SetToggle(UI.TGL_STOPPER_OFF, !flag);
		SetToggle(UI.TGL_PP_ON, flag2);
		SetToggle(UI.TGL_PP_OFF, !flag2);
		SetButtonEnabled(UI.BTN_STOPPER_ON, !flag);
		SetButtonEnabled(UI.BTN_STOPPER_OFF, flag);
		SetButtonEnabled(UI.BTN_PP_ON, !flag2);
		SetButtonEnabled(UI.BTN_PP_OFF, flag2);
	}

	private void OnQuery_STOPPER_ON()
	{
		SendStopper(enable: true);
	}

	private void OnQuery_STOPPER_OFF()
	{
		SendStopper(enable: false);
	}

	private void SendStopper(bool enable)
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<UserInfoManager>.I.SendStopper(enable, delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success);
		});
	}

	private void OnQuery_NAME()
	{
		DateTime now = TimeManager.GetNow();
		if (DateTime.TryParse(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.editNameAt.date, out DateTime result))
		{
			if (result.CompareTo(now) > 0)
			{
				GameSection.ChangeEvent("NOT_CHANGE_NAME", new string[1]
				{
					MonoBehaviourSingleton<UserInfoManager>.I.userInfo.editNameAt.date
				});
			}
		}
		else
		{
			GameSection.StopEvent();
		}
	}

	private void OnQuery_ConfigClearCacheConfirm_YES()
	{
		MenuReset.needClearCache = true;
		MenuReset.needPredownload = true;
	}
}
