using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoungeSearchFriend : FollowListBase
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
		LBL_LOUNGE_NAME,
		LBL_LABEL,
		TGL_MEMBER_1,
		TGL_MEMBER_2,
		TGL_MEMBER_3,
		TGL_MEMBER_4,
		TGL_MEMBER_5,
		TGL_MEMBER_6,
		TGL_MEMBER_7,
		LBL_NON_LIST,
		SPR_ICON_FIRST_MET
	}

	private List<LoungeSearchFollowerRoomModel.LoungeFollowerModel> loungeFollowers;

	private UI[] members = new UI[7]
	{
		UI.TGL_MEMBER_1,
		UI.TGL_MEMBER_2,
		UI.TGL_MEMBER_3,
		UI.TGL_MEMBER_4,
		UI.TGL_MEMBER_5,
		UI.TGL_MEMBER_6,
		UI.TGL_MEMBER_7
	};

	private List<int> firstMetUserIds;

	public override void Initialize()
	{
		base.Initialize();
	}

	public override void UpdateUI()
	{
		UpdateListUI();
	}

	protected override void SendGetList(int page, Action<bool> callback)
	{
		MonoBehaviourSingleton<LoungeMatchingManager>.I.SendSearchFollowerRoom(delegate(bool isSuccess, List<LoungeSearchFollowerRoomModel.LoungeFollowerModel> lounges, List<int> firstMetUserIds)
		{
			if (isSuccess)
			{
				nowPage = 1;
				pageNumMax = 1;
				loungeFollowers = lounges;
				this.firstMetUserIds = firstMetUserIds;
			}
			if (callback != null)
			{
				callback(isSuccess);
			}
		});
	}

	private void UpdateListUI()
	{
		if (loungeFollowers == null || loungeFollowers.Count == 0)
		{
			SetActive(UI.LBL_NON_LIST, is_visible: true);
			SetActive(UI.GRD_LIST, is_visible: false);
			SetButtonEnabled(UI.BTN_PAGE_PREV, is_enabled: false);
			SetButtonEnabled(UI.BTN_PAGE_NEXT, is_enabled: false);
			SetLabelText(UI.LBL_NOW, "0");
			SetLabelText(UI.LBL_MAX, "0");
		}
		else
		{
			SetLabelText(UI.LBL_NOW, (nowPage + 1).ToString());
			SetActive(UI.LBL_NON_LIST, is_visible: false);
			SetActive(UI.GRD_LIST, is_visible: true);
			SetButtonEnabled(UI.BTN_PAGE_PREV, nowPage > 0);
			SetButtonEnabled(UI.BTN_PAGE_NEXT, nowPage + 1 < pageNumMax);
			SetDynamicList(UI.GRD_LIST, "LoungeSearchFriendItem", loungeFollowers.Count, reset: false, null, null, delegate(int i, Transform t, bool is_recycle)
			{
				LoungeSearchFollowerRoomModel.LoungeFollowerModel data = loungeFollowers[i];
				SetupListItem(data, i, t);
			});
		}
	}

	private void SetupListItem(LoungeSearchFollowerRoomModel.LoungeFollowerModel data, int i, Transform t)
	{
		SetEvent(t, "JOIN", i);
		SetFollowerInfo(data, t);
		SetLoungeInfo(data, t);
	}

	private void SetFollowerInfo(LoungeSearchFollowerRoomModel.LoungeFollowerModel data, Transform t)
	{
		CharaInfo charaInfo = null;
		for (int i = 0; i < data.slotInfos.Count; i++)
		{
			if (data.slotInfos[i].userInfo != null && data.slotInfos[i].userInfo.userId == data.followerUserId)
			{
				charaInfo = data.slotInfos[i].userInfo;
			}
		}
		SetRenderPlayerModel(t, UI.TEX_MODEL, PlayerLoadInfo.FromCharaInfo(charaInfo, need_weapon: false, need_helm: true, need_leg: false, is_priority_visual_equip: true), 99, new Vector3(0f, -1.536f, 1.87f), new Vector3(0f, 154f, 0f), is_priority_visual_equip: true);
		SetLabelText(t, UI.LBL_NAME, charaInfo.name);
		SetLabelText(t, UI.LBL_LEVEL, charaInfo.level.ToString());
		FindCtrl(t, UI.OBJ_DEGREE_FRAME_ROOT).GetComponent<DegreePlate>().Initialize(charaInfo.selectedDegrees, isButton: false, delegate
		{
			GetCtrl(UI.GRD_LIST).GetComponent<UIGrid>().Reposition();
		});
		SetActive(t, UI.SPR_ICON_FIRST_MET, CheckFirstMet(charaInfo.userId));
		SetFollowStatus(t, charaInfo.userId, following: true, follower: true, charaInfo.userClanData.cId);
	}

	private bool CheckFirstMet(int userId)
	{
		int i = 0;
		for (int count = firstMetUserIds.Count; i < count; i++)
		{
			if (userId == firstMetUserIds[i])
			{
				return true;
			}
		}
		return false;
	}

	private void SetLoungeInfo(LoungeSearchFollowerRoomModel.LoungeFollowerModel data, Transform t)
	{
		SetLabelText(t, UI.LBL_LOUNGE_NAME, data.name);
		string text = StringTable.Get(STRING_CATEGORY.LOUNGE_LABEL, (uint)data.label);
		SetLabelText(t, UI.LBL_LABEL, text);
		int num = data.num + 1;
		int num2 = data.slotInfos.Count((PartyModel.SlotInfo slotInfo) => slotInfo != null && slotInfo.userInfo != null && slotInfo.userInfo.userId != data.ownerUserId);
		for (int i = 0; i < 7; i++)
		{
			bool is_visible = i < num - 1;
			SetActive(t, members[i], is_visible);
			SetToggle(t, members[i], i < num2);
		}
	}

	private void OnQuery_JOIN()
	{
		int index = (int)GameSection.GetEventData();
		LoungeSearchFollowerRoomModel.LoungeFollowerModel loungeFollowerModel = loungeFollowers[index];
		GameSection.StayEvent();
		MonoBehaviourSingleton<LoungeMatchingManager>.I.SendEntry(loungeFollowerModel.id, delegate(bool isSuccess)
		{
			GameSection.ResumeEvent(isSuccess);
		});
	}
}
