using System;

public class FriendFollowerList : FollowListBase
{
	public override void Initialize()
	{
		titleType = TITLE_TYPE.FOLLOWER;
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetActive((Enum)UI.OBJ_FOLLOW_NUMBER_ROOT, true);
		SetLabelText((Enum)UI.LBL_FOLLOW_NUMBER_NOW, MonoBehaviourSingleton<FriendManager>.I.followerNum.ToString());
		SetLabelText((Enum)UI.LBL_FOLLOW_NUMBER_MAX, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.FRIEND_MAX_FOLLOWER.ToString());
		ListUI();
	}

	protected override void SendGetList(int page, Action<bool> callback)
	{
		MonoBehaviourSingleton<FriendManager>.I.SendGetFollowerList(page, delegate(bool is_success, FriendFollowListModel.Param recv_data)
		{
			if (is_success)
			{
				recvList = recv_data.follow;
				nowPage = page;
				pageNumMax = recv_data.pageNumMax;
			}
			if (callback != null)
			{
				callback(is_success);
			}
		});
	}

	protected override void PostSendGetListByReopen(int page)
	{
		SetDirtyTable();
		base.PostSendGetListByReopen(page);
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		if ((flags & NOTIFY_FLAG.UPDATE_FRIEND_PARAM) != (NOTIFY_FLAG)0L)
		{
			isInitializeSendReopen = true;
		}
		if ((flags & NOTIFY_FLAG.UPDATE_FRIEND_LIST) != (NOTIFY_FLAG)0L)
		{
			SetDirtyTable();
		}
		base.OnNotify(flags);
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_FRIEND_LIST;
	}
}
