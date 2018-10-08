using Network;

public class GuildMemberDetail : HomeFriendDetail
{
	private FriendCharaInfo charaInfo;

	public override void Initialize()
	{
		charaInfo = (GameSection.GetEventData() as FriendCharaInfo);
		base.Initialize();
	}

	public override void SetupFollowButton()
	{
		bool flag = MonoBehaviourSingleton<FriendManager>.I.followNum == MonoBehaviourSingleton<UserInfoManager>.I.userStatus.maxFollow;
		bool flag2 = !charaInfo.following;
		bool follower = charaInfo.follower;
		SetEvent(transRoot, UI.BTN_FOLLOW, "FOLLOW", 0);
		if (flag && flag2)
		{
			SetActive(transRoot, UI.BTN_FOLLOW, true);
			SetActive(transRoot, UI.BTN_UNFOLLOW, false);
			SetEvent(transRoot, UI.BTN_FOLLOW, "INVALID_FOLLOW", 0);
		}
		else
		{
			SetActive(transRoot, UI.BTN_FOLLOW, flag2);
			SetActive(transRoot, UI.BTN_UNFOLLOW, !flag2);
		}
		SetActive(transRoot, UI.OBJ_BLACKLIST_ROOT, true);
		bool flag3 = MonoBehaviourSingleton<BlackListManager>.I.CheckBlackList(charaInfo.userId);
		SetActive(transRoot, UI.BTN_BLACKLIST_IN, !flag3);
		SetActive(transRoot, UI.BTN_BLACKLIST_OUT, flag3);
		SetActive(transRoot, UI.SPR_FOLLOW_ARROW, !flag3 && !flag2);
		SetActive(transRoot, UI.SPR_FOLLOWER_ARROW, !flag3 && follower);
		SetActive(transRoot, UI.SPR_BLACKLIST_ICON, flag3);
	}
}
