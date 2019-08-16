using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FriendMessageList : FollowListBase
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
		GRD_FOLLOW_ARROW,
		OBJ_FOLLOW,
		SPR_FOLLOW,
		SPR_FOLLOWER,
		SPR_BLACKLIST_ICON,
		SPR_SAME_CLAN_ICON,
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
		STR_TITLE_REFLECT,
		OBJ_DEGREE_FRAME_ROOT,
		LBL_SORT
	}

	private List<FriendMessageUserListModel.MessageUserInfo> recvdata;

	public override void Initialize()
	{
		isInitializeSend = true;
		m_isVisibleDefaultInfo = false;
		int mutualFollowerListSortType = GameSaveData.instance.MutualFollowerListSortType;
		if (0 <= mutualFollowerListSortType && mutualFollowerListSortType < 5)
		{
			m_currentSortType = (USER_SORT_TYPE)mutualFollowerListSortType;
		}
		titleType = TITLE_TYPE.MESSAGE;
		base.Initialize();
	}

	public override void StartSection()
	{
	}

	public override void UpdateUI()
	{
		ListUI();
	}

	public override void ListUI()
	{
		SetLabelText((Enum)UI.STR_TITLE, base.sectionData.GetText("STR_TITLE"));
		SetLabelText((Enum)UI.STR_TITLE_REFLECT, base.sectionData.GetText("STR_TITLE"));
		SetLabelText((Enum)UI.LBL_SORT, StringTable.Get(STRING_CATEGORY.USER_SORT, (uint)m_currentSortType));
		FriendMessageUserListModel.MessageUserInfo[] array = recvdata.ToArray();
		if (array == null || array.Length == 0)
		{
			SetActive((Enum)UI.STR_NON_LIST, is_visible: true);
			SetActive((Enum)UI.GRD_LIST, is_visible: false);
			SetActive((Enum)UI.OBJ_ACTIVE_ROOT, is_visible: false);
			SetActive((Enum)UI.OBJ_INACTIVE_ROOT, is_visible: true);
			SetLabelText((Enum)UI.LBL_NOW, "0");
			SetLabelText((Enum)UI.LBL_MAX, "0");
		}
		else
		{
			SetPageNumText((Enum)UI.LBL_NOW, nowPage + 1);
			SetPageNumText((Enum)UI.LBL_MAX, pageNumMax);
			SetActive((Enum)UI.STR_NON_LIST, is_visible: false);
			SetActive((Enum)UI.GRD_LIST, is_visible: true);
			SetActive((Enum)UI.OBJ_ACTIVE_ROOT, pageNumMax != 1);
			SetActive((Enum)UI.OBJ_INACTIVE_ROOT, pageNumMax == 1);
			UpdateDynamicList();
		}
	}

	protected override void UpdateDynamicList()
	{
		FriendMessageUserListModel.MessageUserInfo[] array = recvdata.ToArray();
		int pageItemLength = GetPageItemLength(nowPage);
		int num = nowPage * 10;
		if (pageItemLength >= 1 && array.Length >= 1 && array.Length >= num + pageItemLength)
		{
			FriendMessageUserListModel.MessageUserInfo[] info = new FriendMessageUserListModel.MessageUserInfo[pageItemLength];
			for (int j = 0; j < pageItemLength; j++)
			{
				info[j] = array[num + j];
			}
			if (GameDefine.ACTIVE_DEGREE)
			{
				base.ScrollGrid.cellHeight = GameDefine.DEGREE_FRIEND_LIST_HEIGHT;
			}
			CleanItemList();
			SetDynamicList((Enum)UI.GRD_LIST, "FollowListBaseItem", pageItemLength, reset: false, (Func<int, bool>)null, (Func<int, Transform, Transform>)null, (Action<int, Transform, bool>)delegate(int i, Transform t, bool is_recycle)
			{
				FriendMessageUserListModel.MessageUserInfo messageUserInfo = info[i];
				string clanId = (messageUserInfo.userClanData == null) ? "0" : messageUserInfo.userClanData.cId;
				SetFollowStatus(t, messageUserInfo.userId, following: true, follower: true, clanId);
				SetCharaInfo(messageUserInfo, i, t, is_recycle, 0 == messageUserInfo.userId);
				SetBadge(t, messageUserInfo.noReadNum, 3, -10, -6);
			});
		}
	}

	private int GetPageItemLength(int currentPage)
	{
		return (currentPage + 1 < pageNumMax || recvdata.Count % 10 <= 0) ? 10 : (recvdata.Count % 10);
	}

	protected override void SendGetList(int page, Action<bool> callback = null)
	{
		MonoBehaviourSingleton<FriendManager>.I.SendGetMessageUserList(page, delegate(bool is_success, FriendMessageUserListModel.Param recv_data)
		{
			if (is_success)
			{
				recvdata = recv_data.messageUser;
				nowPage = page;
				pageNumMax = Mathf.CeilToInt((float)recvdata.Count / 10f);
				Sort(recvdata);
			}
			if (callback != null)
			{
				callback(is_success);
			}
		});
	}

	public override void OnQuery_FOLLOW_INFO()
	{
		int num = (int)GameSection.GetEventData();
		List<FriendMessageUserListModel.MessageUserInfo> list = recvdata;
		int num2 = num + nowPage * 10;
		if (num2 >= 0 && !list.IsNullOrEmpty() && list.Count > num2)
		{
			FriendMessageUserListModel.MessageUserInfo eventData = recvdata[num2];
			MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex = num + 4;
			GameSection.SetEventData(eventData);
		}
	}

	public void OnQuery_DIRECT_VIEW_MESSAGE()
	{
		int num = (int)GameSection.GetEventData();
		FriendMessageUserListModel.MessageUserInfo[] array = recvdata.ToArray();
		int pageItemLength = GetPageItemLength(nowPage);
		int num2 = nowPage * 10;
		if (recvdata != null && num2 + num >= 0 && recvdata.Count > num2 + num)
		{
			FriendMessageUserListModel.MessageUserInfo messageUserInfo = recvdata[num2 + num];
			GameSection.StayEvent();
			MonoBehaviourSingleton<FriendManager>.I.SendGetMessageDetailList(messageUserInfo.userId, 0, delegate(bool is_success)
			{
				GameSection.ResumeEvent(is_success);
			});
			MonoBehaviourSingleton<FriendManager>.I.SetNoReadMessageNum(MonoBehaviourSingleton<FriendManager>.I.noReadMessageNum - messageUserInfo.noReadNum);
			messageUserInfo.noReadNum = 0;
		}
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

	protected override bool IsHideSwitchInfoButton()
	{
		if (recvdata != null)
		{
			FriendMessageUserListModel.MessageUserInfo[] array = recvdata.ToArray();
			return array == null || array.Length == 0;
		}
		return (recvdata == null) ? true : false;
	}

	protected override void OnQuery_JOIN_FRIEND()
	{
		int num = (int)GameSection.GetEventData();
		if (recvdata == null || recvdata.Count <= num || num < 0)
		{
			return;
		}
		FriendCharaInfo.JoinInfo joinStatus = recvdata[num].joinStatus;
		if (joinStatus == null)
		{
			return;
		}
		GameSection.StayEvent();
		switch (joinStatus.joinType)
		{
		case 3:
			if (MonoBehaviourSingleton<PartyManager>.IsValid())
			{
				MonoBehaviourSingleton<PartyManager>.I.SendApply(joinStatus.conditionParam, delegate(bool isSucceed, Error error)
				{
					if (isSucceed)
					{
						MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("QuestAccept", "QuestAcceptRoom");
					}
					GameSection.ResumeEvent(is_resume: true);
				}, joinStatus.targetParam);
			}
			break;
		case 2:
			JoinLounge(joinStatus);
			break;
		case 4:
		{
			int toUserId = int.Parse(joinStatus.conditionParam);
			JoinField(joinStatus.targetParam, toUserId, delegate(bool is_matching, bool is_connect, bool is_regist)
			{
				if (!is_matching)
				{
					GameSection.StopEvent();
				}
				else if (!is_connect)
				{
					GameSection.StopEvent();
				}
				else
				{
					GameSection.ResumeEvent(is_regist);
					if (is_regist)
					{
						MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("InGame");
					}
				}
			});
			break;
		}
		default:
			GameSection.ResumeEvent(is_resume: true);
			break;
		}
	}

	protected override void OnQuery_SORT()
	{
		UpdateSortType();
		GameSaveData.instance.SetMutualFollowerListSortType((int)m_currentSortType);
		Sort(recvdata);
		RefreshUI();
	}

	protected override void UpdateSortType()
	{
		m_currentSortType++;
		if (m_currentSortType >= USER_SORT_TYPE.MAX)
		{
			m_currentSortType = USER_SORT_TYPE.NAME;
		}
	}
}
