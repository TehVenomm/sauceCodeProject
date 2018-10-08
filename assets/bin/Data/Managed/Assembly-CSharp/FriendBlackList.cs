using System;

public class FriendBlackList : FollowListBase
{
	public override void Initialize()
	{
		SetActive((Enum)UI.BTN_SORT, false);
		titleType = TITLE_TYPE.BLACKLIST;
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetActive((Enum)UI.OBJ_FOLLOW_NUMBER_ROOT, true);
		SetLabelText((Enum)UI.LBL_FOLLOW_NUMBER_NOW, MonoBehaviourSingleton<BlackListManager>.I.GetBlackListUserNum().ToString());
		SetLabelText((Enum)UI.LBL_FOLLOW_NUMBER_MAX, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.BLACKLIST_MAX.ToString());
		ListUI();
	}

	protected unsafe override void SendGetList(int page, Action<bool> callback)
	{
		_003CSendGetList_003Ec__AnonStorey320 _003CSendGetList_003Ec__AnonStorey;
		MonoBehaviourSingleton<BlackListManager>.I.SendList(page, new Action<bool, BlackListListModel.Param>((object)_003CSendGetList_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
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
