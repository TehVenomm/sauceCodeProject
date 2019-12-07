public class AccountSettings : GameSection
{
	private enum UI
	{
		BTN_MAIL,
		BTN_LINK_ACCOUNT,
		LBL_REGISTED_TEXT,
		STR_REGISTED,
		LBL_REGISTED,
		GRD_BTN,
		BTN_MAIL_CHANGE_PASSWORD
	}

	public override void Initialize()
	{
		base.Initialize();
	}

	public override void UpdateUI()
	{
		if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.isAdvancedUser | MonoBehaviourSingleton<UserInfoManager>.I.userInfo.isAdvancedUserGoogle)
		{
			SetActive(UI.BTN_MAIL, is_visible: false);
			SetActive(UI.BTN_LINK_ACCOUNT, is_visible: false);
			SetActive(UI.LBL_REGISTED_TEXT, is_visible: true);
			SetLabelText(UI.LBL_REGISTED, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.advancedUserMail);
			SetLabelText(UI.LBL_REGISTED_TEXT, base.sectionData.GetText(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.isAdvancedUser ? "REGISTED_MAIL" : "REGISTED_GOOGLE"));
			SetActive(UI.BTN_MAIL_CHANGE_PASSWORD, is_visible: true);
		}
		else
		{
			SetActive(UI.BTN_MAIL, is_visible: true);
			SetActive(UI.BTN_LINK_ACCOUNT, is_visible: true);
			SetActive(UI.LBL_REGISTED_TEXT, is_visible: false);
			GetComponent<UIGrid>(UI.GRD_BTN).Reposition();
		}
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		base.OnNotify(flags);
		if ((NOTIFY_FLAG.UPDATE_QUEST_ITEM_INVENTORY & flags) != (NOTIFY_FLAG)0L)
		{
			RefreshUI();
		}
	}
}
