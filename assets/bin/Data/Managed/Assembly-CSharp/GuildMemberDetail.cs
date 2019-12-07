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
		bool num = MonoBehaviourSingleton<FriendManager>.I.followNum == MonoBehaviourSingleton<UserInfoManager>.I.userStatus.maxFollow;
		bool flag = !charaInfo.following;
		bool follower = charaInfo.follower;
		SetEvent(transRoot, UI.BTN_FOLLOW, "FOLLOW", 0);
		if (num & flag)
		{
			SetActive(transRoot, UI.BTN_FOLLOW, is_visible: true);
			SetActive(transRoot, UI.BTN_UNFOLLOW, is_visible: false);
			SetEvent(transRoot, UI.BTN_FOLLOW, "INVALID_FOLLOW", 0);
		}
		else
		{
			SetActive(transRoot, UI.BTN_FOLLOW, flag);
			SetActive(transRoot, UI.BTN_UNFOLLOW, !flag);
		}
		SetActive(transRoot, UI.OBJ_BLACKLIST_ROOT, is_visible: true);
		bool flag2 = MonoBehaviourSingleton<BlackListManager>.I.CheckBlackList(charaInfo.userId);
		SetActive(transRoot, UI.BTN_BLACKLIST_IN, !flag2);
		SetActive(transRoot, UI.BTN_BLACKLIST_OUT, flag2);
		SetActive(transRoot, UI.SPR_FOLLOW_ARROW, !flag2 && !flag);
		SetActive(transRoot, UI.SPR_FOLLOWER_ARROW, !flag2 && follower);
		SetActive(transRoot, UI.SPR_BLACKLIST_ICON, flag2);
	}
}
