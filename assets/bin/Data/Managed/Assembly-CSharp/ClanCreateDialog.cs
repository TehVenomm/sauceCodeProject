using Network;

public class ClanCreateDialog : ClanSettings
{
	private void OnQuery_CREATE()
	{
		string text = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.CLAN_CREATE_COST.ToString();
		GameSection.SetEventData(new string[1]
		{
			text
		});
	}

	private void OnQuery_ClanCreateConfirmDialog_YES()
	{
		int crystal = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
		if (crystal < MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.CLAN_CREATE_COST)
		{
			GameSection.ChangeEvent("COST_ERROR");
			return;
		}
		if (createRequest.label == CLAN_LABEL.NONE)
		{
			createRequest.SetLabel(CLAN_LABEL.ANYONE);
		}
		MonoBehaviourSingleton<ClanMatchingManager>.I.SetClanCreateRequest(createRequest);
		GameSection.StayEvent();
		MonoBehaviourSingleton<ClanMatchingManager>.I.SendCreate(delegate(bool is_success, Error err)
		{
			GameSection.ResumeEvent(is_success);
		});
	}
}
