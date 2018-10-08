using System;

public class FriendFollowList : FollowListBase
{
	public override void Initialize()
	{
		titleType = TITLE_TYPE.FOLLOW;
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetActive((Enum)UI.OBJ_FOLLOW_NUMBER_ROOT, true);
		SetLabelText((Enum)UI.LBL_FOLLOW_NUMBER_NOW, MonoBehaviourSingleton<FriendManager>.I.followNum.ToString());
		SetLabelText((Enum)UI.LBL_FOLLOW_NUMBER_MAX, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.maxFollow.ToString());
		ListUI();
	}

	protected override void SendGetList(int page, Action<bool> callback)
	{
		MonoBehaviourSingleton<FriendManager>.I.SendGetFollowList(page, delegate(bool is_success, FriendFollowListModel.Param recv_data)
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

	public void OnQuery_MUTUAL_FOLLOW_MESSAGE()
	{
		if (!MonoBehaviourSingleton<FriendManager>.IsValid())
		{
			GameSection.StopEvent();
		}
		else if (MonoBehaviourSingleton<FriendManager>.I.mutualFollowResult == null)
		{
			GameSection.StopEvent();
		}
		else
		{
			GameSection.SetEventData(new object[1]
			{
				MonoBehaviourSingleton<FriendManager>.I.mutualFollowResult.targetUserName
			});
		}
	}
}
