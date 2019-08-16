public class CommunityInClanDialog : CommunityDialogBase
{
	private enum UI
	{
		SPR_FRAME,
		BTN_CLAN_ON,
		BTN_CLAN_OFF,
		BTN_LOUNGE,
		BTN_LOUNGE_OFF,
		BTN_EXIT,
		SPR_NEW_ICON
	}

	private void OnQuery_CLAN_DETAIL()
	{
		GameSection.SetEventData("0");
	}
}
