public class ConfigAnnounce : GameSection
{
	private enum UI
	{
		TGL_EVENT_ON,
		TGL_EVENT_OFF,
		TGL_FRIEND_ON,
		TGL_FRIEND_OFF,
		TGL_GUILD_REQUEST_ON,
		TGL_GUILD_REQUEST_OFF,
		BTN_EVENT_ON,
		BTN_EVENT_OFF,
		BTN_FRIEND_ON,
		BTN_FRIEND_OFF,
		BTN_GUILD_REQUEST_ON,
		BTN_GUILD_REQUEST_OFF
	}

	private int pushEnable;

	private bool autoClose;

	public override string overrideBackKeyEvent => "CLOSE";

	private void Update()
	{
		if (autoClose && !MonoBehaviourSingleton<GameSceneManager>.I.isWaiting)
		{
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("ConfigAnnounce", base.gameObject, "CLOSE", null, null, true);
			autoClose = false;
		}
	}

	public override void UpdateUI()
	{
		updateNoticeFlag();
		UpdateUI_Event();
		UpdateUI_Friend();
		UpdateUI_GuildRequest();
		base.UpdateUI();
	}

	private void UpdateUI_Event()
	{
		bool flag = (pushEnable & 1) != 0;
		SetToggle(UI.TGL_EVENT_ON, flag);
		SetToggle(UI.TGL_EVENT_OFF, !flag);
		SetButtonEnabled(UI.BTN_EVENT_ON, !flag);
		SetButtonEnabled(UI.BTN_EVENT_OFF, flag);
	}

	private void UpdateUI_Friend()
	{
		bool flag = (pushEnable & 2) != 0;
		SetToggle(UI.TGL_FRIEND_ON, flag);
		SetToggle(UI.TGL_FRIEND_OFF, !flag);
		SetButtonEnabled(UI.BTN_FRIEND_ON, !flag);
		SetButtonEnabled(UI.BTN_FRIEND_OFF, flag);
	}

	private void UpdateUI_GuildRequest()
	{
		bool localPushFlag = MonoBehaviourSingleton<GuildRequestManager>.I.GetLocalPushFlag();
		SetToggle(UI.TGL_GUILD_REQUEST_ON, localPushFlag);
		SetToggle(UI.TGL_GUILD_REQUEST_OFF, !localPushFlag);
		SetButtonEnabled(UI.BTN_GUILD_REQUEST_ON, !localPushFlag);
		SetButtonEnabled(UI.BTN_GUILD_REQUEST_OFF, localPushFlag);
	}

	private void updateNoticeFlag()
	{
		pushEnable = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.pushEnable;
	}

	private void OnQuery_EVENT()
	{
		pushEnable ^= 1;
		UpdateUI_Event();
	}

	private void OnQuery_FRIEND()
	{
		pushEnable ^= 2;
		UpdateUI_Friend();
	}

	private void OnQuery_GUILD_REQUEST()
	{
		MonoBehaviourSingleton<GuildRequestManager>.I.ToggleLocalPushFlag();
		UpdateUI_GuildRequest();
	}

	private void OnQuery_CLOSE()
	{
		if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.pushEnable != pushEnable)
		{
			GameSection.StayEvent();
			MonoBehaviourSingleton<UserInfoManager>.I.SendPushNotificationDeviceEnable(pushEnable, delegate(bool is_success)
			{
				GameSection.ResumeEvent(is_success, null);
				pushEnable = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.pushEnable;
				autoClose = true;
			});
		}
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_USER_INFO;
	}
}
