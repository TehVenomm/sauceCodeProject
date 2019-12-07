using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MutualFollowFBInviteDialog : GameSection
{
	private enum UI
	{
		WRP_LIST,
		BTN_SELECTALL,
		BTN_INVITE,
		IPT_NAME,
		LBL_SELECTED,
		LBL_SELECTED_NUM,
		LBL_NAME,
		TEX_AVATAR,
		OBJ_SELECT
	}

	private List<FBManager.FriendData> selectedList = new List<FBManager.FriendData>();

	public override void Initialize()
	{
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		bool getFriendFinish = false;
		bool getInvitableFriendFinish = false;
		MonoBehaviourSingleton<FBManager>.I.GetInvitableFriends(delegate
		{
			getInvitableFriendFinish = true;
			MonoBehaviourSingleton<FBManager>.I.GetFriends(delegate
			{
				getFriendFinish = true;
			});
		});
		while (!getInvitableFriendFinish || !getFriendFinish)
		{
			yield return null;
		}
		SetInput(UI.IPT_NAME, string.Empty, 0, delegate
		{
			string inputValue = GetInputValue(UI.IPT_NAME);
			SetWrapContentFilterText(UI.WRP_LIST, inputValue);
		});
		base.Initialize();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		_UpdateListFriend(MonoBehaviourSingleton<FBManager>.I.invitableFriendInfo);
		_UpdateNumFriend(MonoBehaviourSingleton<FBManager>.I.invitableFriendInfo, MonoBehaviourSingleton<FBManager>.I.friendInfo);
		_UpdateSelected();
	}

	private void _UpdateListFriend(FBManager.InvitableFriendInfo invitable_friend_info)
	{
		List<FBManager.FriendData> friendList = invitable_friend_info.data;
		int j = 0;
		SetWrapContentFilter(UI.WRP_LIST, "MutualFollowFBInviteListItem", friendList.Count, reset: false, delegate(int i, Transform t, bool b)
		{
			FBManager.FriendData friendData2 = friendList[j++];
			SetLabelText(t, UI.LBL_NAME, friendData2.name);
			SetDownloadTexture(t, UI.TEX_AVATAR, friendData2.picture.data.url);
			SetEvent(t, "SELECT", i);
		}, delegate(int i, string s)
		{
			FBManager.FriendData friendData = friendList[i];
			return (friendData.name.ContainIgnoreCase(s) || s.ContainIgnoreCase(friendData.name)) ? true : false;
		});
	}

	private void _UpdateSelected()
	{
		SetLabelText(UI.LBL_SELECTED, string.Format(base.sectionData.GetText("FRIEND_SELECTED"), selectedList.Count));
	}

	private void _UpdateNumFriend(FBManager.InvitableFriendInfo invitable_friend_info, FBManager.FriendInfo friend_info)
	{
		_ = friend_info.data;
		SetLabelText(UI.LBL_SELECTED_NUM, string.Format(base.sectionData.GetText("FRIEND_NUM"), friend_info.data.Count, invitable_friend_info.data.Count));
	}

	private void OnQuery_SELECT()
	{
		int index = (int)GameSection.GetEventData();
		Transform child = GetCtrl(UI.WRP_LIST).GetChild(index);
		SetActive(child, UI.OBJ_SELECT, is_visible: true);
		FBManager.FriendData item = MonoBehaviourSingleton<FBManager>.I.invitableFriendInfo.data[index];
		if (selectedList.Contains(item))
		{
			selectedList.Remove(item);
			SetActive(child, UI.OBJ_SELECT, is_visible: false);
		}
		else
		{
			selectedList.Add(item);
			SetActive(child, UI.OBJ_SELECT, is_visible: true);
		}
		_UpdateSelected();
	}

	private void OnQuery_SELECTALL()
	{
		int count = selectedList.Count;
		selectedList.Clear();
		bool is_visible = false;
		if (count < MonoBehaviourSingleton<FBManager>.I.invitableFriendInfo.data.Count)
		{
			selectedList.AddRange(MonoBehaviourSingleton<FBManager>.I.invitableFriendInfo.data);
			is_visible = true;
		}
		foreach (Transform item in GetCtrl(UI.WRP_LIST))
		{
			SetActive(item, UI.OBJ_SELECT, is_visible);
		}
		_UpdateSelected();
	}

	private void OnQuery_INVITE()
	{
		List<string> list = selectedList.Select((FBManager.FriendData o) => o.id).ToList();
		if (list.Count > 0)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("amount", list.Count);
			MonoBehaviourSingleton<GoWrapManager>.I.trackEvent("invite_friend", "Social", dictionary);
			GameSection.ChangeEvent("INVITE_SUCCESS");
			GameSection.StayEvent();
			MonoBehaviourSingleton<FBManager>.I.AppRequest(base.sectionData.GetText("INVITE_MESSAGE"), list, "", "", delegate(bool req_success, FBManager.AppRequestResult ret)
			{
				if (req_success)
				{
					List<string> list2 = ret.to.Split(',').ToList();
					MonoBehaviourSingleton<AccountManager>.I.SendTrackInviteFacebook(MonoBehaviourSingleton<FBManager>.I.accessToken, list2, delegate(bool success)
					{
						if (!success)
						{
							GameSection.ChangeStayEvent("TRACK_FAIL");
						}
						GameSection.ResumeEvent(is_resume: true);
					});
				}
				else
				{
					GameSection.ChangeStayEvent("INVITE_FAIL");
					GameSection.ResumeEvent(is_resume: true);
				}
			});
		}
		else
		{
			GameSection.ChangeEvent("INVITE_NONE");
		}
	}
}
