using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeMutualFollowerListUIController : ScrollItemListControllerBase
{
	public class InitParam : InitializeParameter
	{
		public HomeVariableMemberListController ListCtrl;
	}

	private static readonly string LIST_ITEM_PREFAB_NAME = "FollowListBaseItem";

	private static readonly string WINDOW_TITLE_TEXT = "Mutual Follower List";

	private HomeVariableMemberListController m_listCtrl;

	private List<FriendMessageUserListModel.MessageUserInfo> m_userDataList;

	public HomeMutualFollowerListUIController()
	{
	}

	public HomeMutualFollowerListUIController(InitParam _initParam)
		: base(_initParam)
	{
		m_listCtrl = _initParam.ListCtrl;
	}

	protected unsafe override IEnumerator RequestNextPageInfo(int _nextPageNum, Action<bool, int> _callback)
	{
		MonoBehaviourSingleton<FriendManager>.I.SendGetMessageUserList(_nextPageNum, true, new Action<bool, FriendMessageUserListModel.Param>((object)/*Error near IL_002d: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		yield return (object)null;
	}

	protected override void OnCallbackRequestPageInfo(bool _isSucceeded, int _nextPageNum)
	{
		if (_isSucceeded)
		{
			m_listCtrl.UpdateUI();
			m_listCtrl.SetCurrentPageNum((base.MaxPageNum >= 1) ? (base.CurrentPageNum + 1) : 0);
			m_listCtrl.SetMaxPageNum(base.MaxPageNum);
		}
		base.OnCallbackRequestPageInfo(_isSucceeded, _nextPageNum);
	}

	public override string GetItemPrefabName()
	{
		return LIST_ITEM_PREFAB_NAME;
	}

	public unsafe override void SetListItem(int i, Transform t, bool is_recycle)
	{
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Expected O, but got Unknown
		if (m_userDataList == null || m_userDataList.Count < 1 || m_userDataList.Count <= i || i < 0)
		{
			if (base.OnCompleteAllItemLoading != null)
			{
				base.OnCompleteAllItemLoading(base.ItemLoadCompleteCount);
			}
		}
		else
		{
			FriendMessageUserListModel.MessageUserInfo messageUserInfo = m_userDataList[i];
			HomeMutualFollowerListItem component = t.GetComponent<HomeMutualFollowerListItem>();
			if (component == null)
			{
				if (base.OnCompleteAllItemLoading != null)
				{
					base.OnCompleteAllItemLoading(base.ItemLoadCompleteCount);
				}
			}
			else
			{
				HomeMutualFollowerListItem.InitParam initParam = new HomeMutualFollowerListItem.InitParam();
				initParam.CharacterInfo = messageUserInfo;
				initParam.Index = i;
				initParam.IsFollower = messageUserInfo.follower;
				initParam.IsFollowing = messageUserInfo.following;
				initParam.NoReadMsgNum = messageUserInfo.noReadNum;
				initParam.IsPermittedMessage = messageUserInfo.isPermitted;
				initParam.OnClickItem = OnClickItem;
				initParam.IsUseRenderTextureCharaModel = (!FieldManager.IsValidInField() && !FieldManager.IsValidInGame() && !FieldManager.IsValidInTutorial());
				initParam.OnCompleteLoading = new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
				component.Initialize(initParam);
			}
		}
	}

	public override int GetItemListDataCount()
	{
		return (m_userDataList != null) ? m_userDataList.Count : 0;
	}

	public override string GetChatTitle()
	{
		return WINDOW_TITLE_TEXT;
	}

	protected void OnClickItem(int _itemIndex)
	{
		if (m_userDataList != null && _itemIndex >= 0 && m_userDataList.Count > _itemIndex)
		{
			if (!m_userDataList[_itemIndex].isPermitted)
			{
				MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, StringTable.GetErrorMessage(13022u), null, null, null, null), delegate
				{
				}, true, 13022);
			}
			else if (MonoBehaviourSingleton<FriendManager>.IsValid())
			{
				MonoBehaviourSingleton<FriendManager>.I.SendGetMessageDetailList(m_userDataList[_itemIndex].userId, 0, true, delegate(bool flag)
				{
					if (flag && m_listCtrl != null)
					{
						m_listCtrl.OnClickItem();
					}
				});
			}
		}
	}
}
