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
			SetActive((Enum)UI.LBL_NON_LIST, true);
			SetActive((Enum)UI.GRD_LIST, false);
			SetButtonEnabled((Enum)UI.BTN_PAGE_PREV, false);
			SetButtonEnabled((Enum)UI.BTN_PAGE_NEXT, false);
			SetLabelText((Enum)UI.LBL_NOW, "0");
			SetLabelText((Enum)UI.LBL_MAX, "0");
		}
		else
		{
			SetLabelText((Enum)UI.LBL_NOW, (nowPage + 1).ToString());
			SetActive((Enum)UI.LBL_NON_LIST, false);
			SetActive((Enum)UI.GRD_LIST, true);
			SetButtonEnabled((Enum)UI.BTN_PAGE_PREV, nowPage > 0);
			SetButtonEnabled((Enum)UI.BTN_PAGE_NEXT, nowPage + 1 < pageNumMax);
			SetDynamicList((Enum)UI.GRD_LIST, "LoungeSearchFriendItem", loungeFollowers.Count, false, (Func<int, bool>)null, (Func<int, Transform, Transform>)null, (Action<int, Transform, bool>)delegate(int i, Transform t, bool is_recycle)
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
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		CharaInfo charaInfo = null;
		for (int i = 0; i < data.slotInfos.Count; i++)
		{
			if (data.slotInfos[i].userInfo != null && data.slotInfos[i].userInfo.userId == data.followerUserId)
			{
				charaInfo = data.slotInfos[i].userInfo;
			}
		}
		SetRenderPlayerModel(t, UI.TEX_MODEL, PlayerLoadInfo.FromCharaInfo(charaInfo, false, true, false, true), 99, new Vector3(0f, -1.536f, 1.87f), new Vector3(0f, 154f, 0f), true, null);
		SetLabelText(t, UI.LBL_NAME, charaInfo.name);
		SetLabelText(t, UI.LBL_LEVEL, charaInfo.level.ToString());
		DegreePlate component = FindCtrl(t, UI.OBJ_DEGREE_FRAME_ROOT).GetComponent<DegreePlate>();
		component.Initialize(charaInfo.selectedDegrees, false, delegate
		{
			GetCtrl(UI.GRD_LIST).GetComponent<UIGrid>().Reposition();
		});
		SetActive(t, UI.SPR_ICON_FIRST_MET, CheckFirstMet(charaInfo.userId));
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
		int num2 = data.slotInfos.Count((LoungeModel.SlotInfo slotInfo) => slotInfo != null && slotInfo.userInfo != null && slotInfo.userInfo.userId != data.ownerUserId);
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
			GameSection.ResumeEvent(isSuccess, null);
		});
	}
}
