public class LoungeInviteDialog : QuestRoomInviteDialog
{
	protected new enum UI
	{
		LBL_SNS,
		OBJ_LINE_ROOT,
		OBJ_TWITTER_ROOT,
		LBL_SERVICE,
		LBL_INVITE
	}

	public override void Initialize()
	{
		string inviteMessage = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetInviteMessage();
		helpLinkURL = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetInviteHelpURL();
		base.inviteMessage = inviteMessage.Replace("<BR>", "\n").Replace("<br>", "\n");
		InitializeBase();
	}
}
