using System;
using System.Collections.Generic;
using UnityEngine;

public class GuildSearchFriend : FollowListBase
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
		SPR_ICON_FIRST_MET,
		LBL_MEMBER_NUM,
		LBL_CLAN_LV
	}

	private List<GuildSearchFollowerRoomModel.GuildFollowerModel> clanFollowers;

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
		MonoBehaviourSingleton<GuildManager>.I.SendSearchFollowerRoom(delegate(bool isSuccess, List<GuildSearchFollowerRoomModel.GuildFollowerModel> clans)
		{
			if (isSuccess)
			{
				nowPage = 1;
				pageNumMax = 1;
				clanFollowers = clans;
			}
			if (callback != null)
			{
				callback(isSuccess);
			}
		});
	}

	private void UpdateListUI()
	{
		if (clanFollowers == null || clanFollowers.Count == 0)
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
			SetDynamicList((Enum)UI.GRD_LIST, "GuildSearchFriendItem", clanFollowers.Count, false, (Func<int, bool>)null, (Func<int, Transform, Transform>)null, (Action<int, Transform, bool>)delegate(int i, Transform t, bool is_recycle)
			{
				GuildSearchFollowerRoomModel.GuildFollowerModel data = clanFollowers[i];
				SetupListItem(data, i, t);
			});
		}
	}

	private void SetupListItem(GuildSearchFollowerRoomModel.GuildFollowerModel data, int i, Transform t)
	{
		SetEvent(t, "GUILD_INFO", data.clanData.clanId);
		SetFollowerInfo(data, t);
		SetLoungeInfo(data, t);
	}

	private void SetFollowerInfo(GuildSearchFollowerRoomModel.GuildFollowerModel data, Transform t)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		SetRenderPlayerModel(t, UI.TEX_MODEL, PlayerLoadInfo.FromCharaInfo(data.charInfo, false, true, false, true), 99, new Vector3(0f, -1.536f, 1.87f), new Vector3(0f, 154f, 0f), true, null);
		SetLabelText(t, UI.LBL_NAME, data.charInfo.name);
		SetLabelText(t, UI.LBL_LEVEL, data.charInfo.level.ToString());
		DegreePlate component = FindCtrl(t, UI.OBJ_DEGREE_FRAME_ROOT).GetComponent<DegreePlate>();
		component.Initialize(data.charInfo.selectedDegrees, false, delegate
		{
			GetCtrl(UI.GRD_LIST).GetComponent<UIGrid>().Reposition();
		});
	}

	private void SetLoungeInfo(GuildSearchFollowerRoomModel.GuildFollowerModel data, Transform t)
	{
		SetLabelText(t, UI.LBL_LOUNGE_NAME, data.clanData.name);
		if (data.clanData.privacy == 0)
		{
			SetLabelText(t, UI.LBL_LABEL, "PUBLIC");
		}
		else if (data.clanData.privacy == 1)
		{
			SetLabelText(t, UI.LBL_LABEL, "PRIVATE");
		}
		else
		{
			SetLabelText(t, UI.LBL_LABEL, "CLOSE");
		}
		if (data.clanData.currentMem != data.clanData.memCap)
		{
			SetLabelText(t, UI.LBL_MEMBER_NUM, data.clanData.currentMem.ToString() + "/" + data.clanData.memCap.ToString());
		}
		else
		{
			SetLabelText(t, UI.LBL_MEMBER_NUM, "Full");
		}
		SetLabelText(t, UI.LBL_CLAN_LV, "Lv " + data.clanData.level.ToString());
	}
}
