public class ClanRegisteredDialog : GameSection
{
	private enum UI
	{
		LBL_CLAN_NAME,
		BTN_SETTING
	}

	public override void UpdateUI()
	{
		SetLabelText(UI.LBL_CLAN_NAME, MonoBehaviourSingleton<UserInfoManager>.I.userClan.name);
		bool is_visible = MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsLeader();
		SetActive(UI.BTN_SETTING, is_visible);
	}

	private void OnQuery_CLAN_DETAIL()
	{
		GameSection.SetEventData("0");
	}

	private void OnQuery_TO_CLAN()
	{
	}
}
