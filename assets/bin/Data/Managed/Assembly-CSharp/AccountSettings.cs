public class AccountSettings : GameSection
{
	private enum UI
	{
		BTN_MAIL,
		BTN_GOOGLE,
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
			SetActive(UI.BTN_MAIL, false);
			SetActive(UI.BTN_GOOGLE, false);
			SetActive(UI.LBL_REGISTED_TEXT, true);
			SetLabelText(UI.LBL_REGISTED, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.advancedUserMail);
			SetLabelText(UI.LBL_REGISTED_TEXT, base.sectionData.GetText((!MonoBehaviourSingleton<UserInfoManager>.I.userInfo.isAdvancedUser) ? "REGISTED_GOOGLE" : "REGISTED_MAIL"));
			SetActive(UI.BTN_MAIL_CHANGE_PASSWORD, true);
		}
		else
		{
			bool is_visible = false;
			SetActive(UI.BTN_MAIL, true);
			SetActive(UI.BTN_GOOGLE, is_visible);
			SetActive(UI.LBL_REGISTED_TEXT, false);
			GetComponent<UIGrid>(UI.GRD_BTN).Reposition();
		}
	}
}
