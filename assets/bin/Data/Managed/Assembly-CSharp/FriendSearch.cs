using Network;
using System;

public class FriendSearch : FollowListBase
{
	protected new enum UI
	{
		SPR_TITLE_FOLLOW_LIST,
		SPR_TITLE_FOLLOWER_LIST,
		SPR_TITLE_MESSAGE,
		SPR_TITLE_BLACKLIST,
		OBJ_FOLLOW_NUMBER_ROOT,
		LBL_FOLLOW_NUMBER_NOW,
		LBL_FOLLOW_NUMBER_MAX,
		OBJ_DISABLE_USER_MASK,
		LBL_NAME,
		GRD_LIST,
		TEX_MODEL,
		STR_NON_LIST,
		SPR_FOLLOW,
		SPR_FOLLOWER,
		SPR_BLACKLIST_ICON,
		OBJ_COMMENT,
		LBL_COMMENT,
		LBL_LAST_LOGIN,
		LBL_LAST_LOGIN_TIME,
		LBL_ATK,
		LBL_DEF,
		LBL_HP,
		LBL_LEVEL,
		LBL_NOW,
		LBL_MAX,
		OBJ_ACTIVE_ROOT,
		OBJ_INACTIVE_ROOT,
		BTN_PAGE_PREV,
		BTN_PAGE_NEXT,
		STR_TITLE,
		STR_TITLE_REFLECT
	}

	private enum SEARCH_TYPE
	{
		AUTO,
		NAME,
		ID
	}

	private SEARCH_TYPE searchType;

	private SEARCH_TYPE tmpSearchType;

	private string searchName = string.Empty;

	private string searchID = string.Empty;

	public override void Initialize()
	{
		isInitializeSend = false;
		tmpSearchType = searchType;
		base.Initialize();
	}

	public override void UpdateUI()
	{
		ListUI();
	}

	private void OnQuery_SEARCH_NAME()
	{
		tmpSearchType = SEARCH_TYPE.NAME;
		GameSection.SetEventData(0);
	}

	private void OnQuery_ID_SEARCH()
	{
		tmpSearchType = SEARCH_TYPE.ID;
	}

	private void OnQuery_AUTO_SEARCH()
	{
		GameSection.StayEvent();
		searchType = SEARCH_TYPE.AUTO;
		SendGetList(0, delegate(bool b)
		{
			SetDirty(UI.GRD_LIST);
			RefreshUI();
			GameSection.ResumeEvent(b, null);
		});
	}

	protected unsafe override void SendGetList(int page, Action<bool> callback)
	{
		_003CSendGetList_003Ec__AnonStorey319 _003CSendGetList_003Ec__AnonStorey;
		switch (searchType)
		{
		case SEARCH_TYPE.NAME:
			if (string.IsNullOrEmpty(searchName))
			{
				callback(false);
			}
			else
			{
				MonoBehaviourSingleton<FriendManager>.I.SendSearchName(searchName, page, new Action<bool, FriendSearchResult>((object)_003CSendGetList_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
			break;
		case SEARCH_TYPE.ID:
			if (string.IsNullOrEmpty(searchID))
			{
				callback(false);
			}
			else
			{
				MonoBehaviourSingleton<FriendManager>.I.SendSearchID(searchID, new Action<bool, FriendSearchResult>((object)_003CSendGetList_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
			break;
		case SEARCH_TYPE.AUTO:
			MonoBehaviourSingleton<FriendManager>.I.SendSearchLevel(page, new Action<bool, FriendSearchResult>((object)_003CSendGetList_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			break;
		}
	}

	private void OnCloseDialog_FriendSearchName()
	{
		OnCloseSearchDialog();
	}

	private void OnCloseDialog_FriendSearchID()
	{
		OnCloseSearchDialog();
	}

	private void OnCloseSearchDialog()
	{
		object[] array = GameSection.GetEventData() as object[];
		if (array != null)
		{
			searchType = tmpSearchType;
			if ((bool)array[0])
			{
				int nowPage = (int)array[1];
				FriendSearchResult friendSearchResult = array[2] as FriendSearchResult;
				recvList = ChangeData(friendSearchResult.search);
				pageNumMax = friendSearchResult.pageNumMax;
				base.nowPage = nowPage;
				if (searchType == SEARCH_TYPE.ID)
				{
					searchID = (array[3] as string);
				}
				else if (searchType == SEARCH_TYPE.NAME)
				{
					searchName = (array[3] as string);
				}
				RefreshUI();
			}
		}
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		if ((flags & NOTIFY_FLAG.UPDATE_FRIEND_LIST) != (NOTIFY_FLAG)0L)
		{
			SetDirty(UI.GRD_LIST);
		}
		base.OnNotify(flags);
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_FRIEND_LIST;
	}
}
