using System;

public class FriendBlackList : FollowListBase
{
	public override void Initialize()
	{
		SetActive((Enum)UI.BTN_SORT, is_visible: false);
		titleType = TITLE_TYPE.BLACKLIST;
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetActive((Enum)UI.OBJ_FOLLOW_NUMBER_ROOT, is_visible: true);
		SetLabelText((Enum)UI.LBL_FOLLOW_NUMBER_NOW, MonoBehaviourSingleton<BlackListManager>.I.GetBlackListUserNum().ToString());
		SetLabelText((Enum)UI.LBL_FOLLOW_NUMBER_MAX, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.BLACKLIST_MAX.ToString());
		ListUI();
	}

	protected override void SendGetList(int page, Action<bool> callback)
	{
		MonoBehaviourSingleton<BlackListManager>.I.SendList(page, delegate(bool is_success, BlackListListModel.Param recv_data)
		{
			if (is_success)
			{
				recvList = recv_data.black;
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
