using Network;
using System;
using UnityEngine;

public class FriendFollowList : FollowListBase
{
	public override void Initialize()
	{
		int followListSortType = GameSaveData.instance.FollowListSortType;
		if (0 <= followListSortType && followListSortType < 5)
		{
			m_currentSortType = (USER_SORT_TYPE)followListSortType;
		}
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

	protected unsafe override void UpdateDynamicList()
	{
		FriendCharaInfo[] currentUserArray = GetCurrentUserArray();
		int pageItemLength = GetPageItemLength(nowPage);
		int num = nowPage * 10;
		if (pageItemLength >= 1 && currentUserArray.Length >= 1 && currentUserArray.Length >= num + pageItemLength)
		{
			FriendCharaInfo[] info = new FriendCharaInfo[pageItemLength];
			for (int i = 0; i < pageItemLength; i++)
			{
				info[i] = currentUserArray[num + i];
			}
			if (GameDefine.ACTIVE_DEGREE)
			{
				base.ScrollGrid.cellHeight = (float)GameDefine.DEGREE_FRIEND_LIST_HEIGHT;
			}
			CleanItemList();
			_003CUpdateDynamicList_003Ec__AnonStorey321 _003CUpdateDynamicList_003Ec__AnonStorey;
			SetDynamicList((Enum)UI.GRD_LIST, GetListItemName, pageItemLength, false, null, null, new Action<int, Transform, bool>((object)_003CUpdateDynamicList_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}

	private int GetPageItemLength(int currentPage)
	{
		return (currentPage + 1 < pageNumMax || recvList.Count % 10 <= 0) ? 10 : (recvList.Count % 10);
	}

	protected unsafe override void SendGetList(int page, Action<bool> callback)
	{
		_003CSendGetList_003Ec__AnonStorey322 _003CSendGetList_003Ec__AnonStorey;
		MonoBehaviourSingleton<FriendManager>.I.SendGetFollowList(page, new Action<bool, FriendFollowListModel.Param>((object)_003CSendGetList_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
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

	public override void OnQuery_FOLLOW_INFO()
	{
		int num = (int)GameSection.GetEventData();
		FriendCharaInfo[] currentUserArray = GetCurrentUserArray();
		int num2 = num + nowPage * 10;
		if (num2 >= 0 && !currentUserArray.IsNullOrEmpty() && currentUserArray.Length > num2)
		{
			FriendCharaInfo eventData = currentUserArray[num2];
			MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex = num + 4;
			GameSection.SetEventData(eventData);
		}
	}

	protected override void OnQuery_PAGE_PREV()
	{
		int num = nowPage = (nowPage - 1 + pageNumMax) % pageNumMax;
		MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GetUpdateUINotifyFlags());
	}

	protected override void OnQuery_PAGE_NEXT()
	{
		int num = nowPage = (nowPage + 1) % pageNumMax;
		MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GetUpdateUINotifyFlags());
	}

	protected override void OnQuery_SORT()
	{
		if (recvList.Count > 0)
		{
			base.OnQuery_SORT();
			GameSaveData.instance.SetFollowListSortType((int)m_currentSortType);
		}
		else
		{
			MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, "You have no Following Hunters", StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 100u), null, null, null), delegate
			{
			}, false, 0);
		}
	}
}
