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
		STR_TITLE_REFLECT,
		OBJ_DEGREE_FRAME_ROOT
	}

	private List<FriendMessageUserListModel.MessageUserInfo> recvdata;

	public override void Initialize()
	{
		isInitializeSend = true;
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
		FriendMessageUserListModel.MessageUserInfo[] array = recvdata.ToArray();
		if (array == null || array.Length == 0)
		{
			SetActive((Enum)UI.STR_NON_LIST, true);
			SetActive((Enum)UI.GRD_LIST, false);
			SetActive((Enum)UI.OBJ_ACTIVE_ROOT, false);
			SetActive((Enum)UI.OBJ_INACTIVE_ROOT, true);
			SetLabelText((Enum)UI.LBL_NOW, "0");
			SetLabelText((Enum)UI.LBL_MAX, "0");
		}
		else
		{
			SetPageNumText((Enum)UI.LBL_NOW, nowPage + 1);
			SetPageNumText((Enum)UI.LBL_MAX, pageNumMax);
			SetActive((Enum)UI.STR_NON_LIST, false);
			SetActive((Enum)UI.GRD_LIST, true);
			SetActive((Enum)UI.OBJ_ACTIVE_ROOT, pageNumMax != 1);
			SetActive((Enum)UI.OBJ_INACTIVE_ROOT, pageNumMax == 1);
			UpdateDynamicList();
		}
	}

	protected override void UpdateDynamicList()
	{
		FriendMessageUserListModel.MessageUserInfo[] info = recvdata.ToArray();
		int item_num = (info != null) ? info.Length : 0;
		if (GameDefine.ACTIVE_DEGREE)
		{
			UIGrid component = GetCtrl(UI.GRD_LIST).GetComponent<UIGrid>();
			component.cellHeight = (float)GameDefine.DEGREE_FRIEND_LIST_HEIGHT;
		}
		SetDynamicList((Enum)UI.GRD_LIST, "FollowListBaseItem", item_num, false, (Func<int, bool>)null, (Func<int, Transform, Transform>)null, (Action<int, Transform, bool>)delegate(int i, Transform t, bool is_recycle)
		{
			FriendMessageUserListModel.MessageUserInfo messageUserInfo = info[i];
			SetFollowStatus(t, messageUserInfo.userId, true, true);
			SetCharaInfo(messageUserInfo, i, t, is_recycle, 0 == messageUserInfo.userId);
			base.SetEnabled<UISprite>(t, (Enum)UI.OBJ_DISABLE_USER_MASK, !messageUserInfo.isPermitted);
			if (0 < messageUserInfo.noReadNum)
			{
				SetBadge(t, messageUserInfo.noReadNum, 3, -10, -6, false);
			}
		});
	}

	protected override void SendGetList(int page, Action<bool> callback = null)
	{
		MonoBehaviourSingleton<FriendManager>.I.SendGetMessageUserList(page, delegate(bool is_success, FriendMessageUserListModel.Param recv_data)
		{
			if (is_success)
			{
				recvdata = recv_data.messageUser;
				nowPage = page;
				pageNumMax = recv_data.pageNumMax;
			}
			if (callback != null)
			{
				callback(is_success);
			}
		});
	}

	public override void OnQuery_FOLLOW_INFO()
	{
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Expected O, but got Unknown
		int index = (int)GameSection.GetEventData();
		FriendMessageUserListModel.MessageUserInfo messageUserInfo = recvdata[index];
		if (messageUserInfo == null)
		{
			GameSection.StopEvent();
		}
		else if (!messageUserInfo.isPermitted)
		{
			GameSection.ChangeEvent("NOT_PERMITTED", null);
		}
		else
		{
			GameSection.ChangeEvent("TO_MESSAGE", null);
			GameSection.StayEvent();
			MonoBehaviourSingleton<FriendManager>.I.SendGetMessageDetailList(messageUserInfo.userId, 0, delegate(bool is_success)
			{
				GameSection.ResumeEvent(is_success, null);
			});
			MonoBehaviourSingleton<FriendManager>.I.SetNoReadMessageNum(MonoBehaviourSingleton<FriendManager>.I.noReadMessageNum - messageUserInfo.noReadNum);
			messageUserInfo.noReadNum = 0;
			Transform t = GetCtrl(UI.GRD_LIST).FindChild(index.ToString());
			SetBadge(t, messageUserInfo.noReadNum, 3, -10, -6, false);
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
}
