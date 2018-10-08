public class GuildInviteDialog : QuestRoomInviteDialog
{
	public override void Initialize()
	{
		string inviteMessage = MonoBehaviourSingleton<GuildManager>.I.GetInviteMessage();
		helpLinkURL = MonoBehaviourSingleton<GuildManager>.I.GetInviteHelpURL();
		base.inviteMessage = inviteMessage.Replace("<BR>", "\n").Replace("<br>", "\n");
		InitializeBase();
	}
}
