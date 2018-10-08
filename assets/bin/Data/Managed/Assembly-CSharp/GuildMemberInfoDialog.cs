using Network;

public class GuildMemberInfoDialog : GameSection
{
	private FriendCharaInfo data;

	public override void Initialize()
	{
		data = (GameSection.GetEventData() as FriendCharaInfo);
		base.Initialize();
	}

	private void OnQuery_PROFILE()
	{
		GameSection.SetEventData(data);
	}

	private void OnQuery_MESSAGE()
	{
		MonoBehaviourSingleton<GuildManager>.I.SetTalkUser(data);
	}

	private void OnQuery_JUMP()
	{
	}

	private void OnQuery_BAN()
	{
	}
}
