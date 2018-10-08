using System;

public class FriendTop : GameSection
{
	private enum UI
	{
		LBL_FOLLOW_NUM,
		LBL_FOLLOWER_NUM,
		LBL_BLACK_LIST_NUM,
		BTN_MUTUAL_FOLLOW_LIST,
		BTN_MESSAGE
	}

	public override void Initialize()
	{
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetLabelText((Enum)UI.LBL_FOLLOW_NUM, MonoBehaviourSingleton<FriendManager>.I.followNum.ToString() + "/" + MonoBehaviourSingleton<UserInfoManager>.I.userStatus.maxFollow.ToString());
		ServerConstDefine constDefine = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine;
		SetLabelText((Enum)UI.LBL_FOLLOWER_NUM, MonoBehaviourSingleton<FriendManager>.I.followerNum.ToString() + "/" + constDefine.FRIEND_MAX_FOLLOWER.ToString());
		SetLabelText((Enum)UI.LBL_BLACK_LIST_NUM, MonoBehaviourSingleton<BlackListManager>.I.GetBlackListUserNum().ToString() + "/" + constDefine.BLACKLIST_MAX.ToString());
		SetBadge((Enum)UI.BTN_MUTUAL_FOLLOW_LIST, MonoBehaviourSingleton<FriendManager>.I.noReadMessageNum, 3, -4, -4, true);
	}

	private void OnQuery_MUTUAL_FOLLOW()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<FriendManager>.I.SendGetFollowLink(delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success, null, false);
		});
	}
}
