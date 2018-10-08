public class LoungeInviteDialog : QuestRoomInviteDialog
{
	public override void Initialize()
	{
		string inviteMessage = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetInviteMessage();
		helpLinkURL = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetInviteHelpURL();
		base.inviteMessage = inviteMessage.Replace("<BR>", "\n").Replace("<br>", "\n");
		InitializeBase();
	}
}
