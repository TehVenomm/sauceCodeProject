using Network;

public class GuildMessageMemberList : GuildMemberList
{
	public override string ListItemEvent => "MESSAGE";

	private void OnQuery_MESSAGE()
	{
		FriendCharaInfo talkUser = GameSection.GetEventData() as FriendCharaInfo;
		MonoBehaviourSingleton<GuildManager>.I.SetTalkUser(talkUser);
	}
}
