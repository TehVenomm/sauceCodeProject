using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class InGamePlayerList : GameSection
{
	private enum UI
	{
		FRAME,
		GRD_LIST,
		STR_NON_LIST,
		SCR_LIST,
		LBL_NAME,
		LBL_LEVEL,
		LBL_HP,
		LBL_ATK,
		LBL_DEF,
		LBL_COMMENT,
		GRD_FOLLOW_ARROW,
		OBJ_FOLLOW,
		SPR_FOLLOW,
		SPR_FOLLOWER,
		SPR_BLACKLIST_ICON,
		SPR_SAME_CLAN_ICON,
		BTN_FOLLOW,
		BTN_BLACK_LIST,
		PORTRAIT_FRAME,
		PORTRAIT_LIST,
		LANDSCAPE_FRAME,
		LANDSCAPE_LIST
	}

	private List<FriendCharaInfo> infoList = new List<FriendCharaInfo>();

	public override void Initialize()
	{
		base.Initialize();
		UpdateCharaList();
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += OnScreenRotate;
			OnScreenRotate(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
		}
	}

	public override void Exit()
	{
		base.Exit();
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= OnScreenRotate;
		}
	}

	private void OnScreenRotate(bool is_portrait)
	{
		UIPanel panel = GetCtrl(UI.SCR_LIST).GetComponent<UIPanel>();
		Vector4 baseClipRegion = panel.baseClipRegion;
		if (is_portrait)
		{
			Vector3 localPosition = GetCtrl(UI.PORTRAIT_FRAME).localPosition;
			int height = GetHeight(UI.PORTRAIT_FRAME);
			GetCtrl(UI.FRAME).localPosition = localPosition;
			SetHeight(UI.FRAME, height);
			GetCtrl(UI.SCR_LIST).parent = GetCtrl(UI.PORTRAIT_LIST);
			baseClipRegion.w = GetHeight(UI.PORTRAIT_LIST);
		}
		else
		{
			Vector3 localPosition2 = GetCtrl(UI.LANDSCAPE_FRAME).localPosition;
			int height2 = GetHeight(UI.LANDSCAPE_FRAME);
			GetCtrl(UI.FRAME).localPosition = localPosition2;
			SetHeight(UI.FRAME, height2);
			GetCtrl(UI.SCR_LIST).parent = GetCtrl(UI.LANDSCAPE_LIST);
			baseClipRegion.w = GetHeight(UI.LANDSCAPE_LIST);
		}
		panel.baseClipRegion = baseClipRegion;
		panel.clipOffset = Vector2.zero;
		GetCtrl(UI.SCR_LIST).localPosition = Vector3.zero;
		ScrollViewResetPosition(UI.SCR_LIST);
		UpdateAnchors();
		AppMain i = MonoBehaviourSingleton<AppMain>.I;
		i.onDelayCall = (Action)Delegate.Combine(i.onDelayCall, (Action)delegate
		{
			RefreshUI();
			panel.Refresh();
		});
	}

	public override void UpdateUI()
	{
		int count = infoList.Count;
		if (count <= 0)
		{
			SetActive(UI.GRD_LIST, is_visible: false);
			SetActive(UI.STR_NON_LIST, is_visible: true);
		}
		else
		{
			SetGrid(UI.GRD_LIST, "InGamePlayerListItem", count, reset: false, delegate(int i, Transform t, bool b)
			{
				MonoBehaviourSingleton<StatusManager>.I.CalcUserStatusParam(infoList[i], out int _atk, out int _def, out int _hp);
				SetLabelText(t, UI.LBL_NAME, infoList[i].name);
				SetLabelText(t, UI.LBL_LEVEL, infoList[i].level.ToString());
				SetLabelText(t, UI.LBL_HP, _hp.ToString());
				SetLabelText(t, UI.LBL_ATK, _atk.ToString());
				SetLabelText(t, UI.LBL_DEF, _def.ToString());
				SetLabelText(t, UI.LBL_COMMENT, infoList[i].comment);
				if (infoList[i].following)
				{
					SetEvent(t, UI.BTN_FOLLOW, "UN_FOLLOW", i);
				}
				else
				{
					SetEvent(t, UI.BTN_FOLLOW, "FOLLOW", i);
				}
				SetButtonEnabled(t, UI.BTN_FOLLOW, !infoList[i].following);
				bool flag = MonoBehaviourSingleton<BlackListManager>.I.CheckBlackList(infoList[i].userId);
				if (flag)
				{
					SetEvent(t, UI.BTN_BLACK_LIST, "BLACK_LIST_OUT", i);
				}
				else
				{
					SetEvent(t, UI.BTN_BLACK_LIST, "BLACK_LIST_IN", i);
				}
				SetButtonEnabled(t, UI.BTN_BLACK_LIST, !flag);
				string clanId = (infoList[i].userClanData != null) ? infoList[i].userClanData.cId : "0";
				SetFollowStatus(t, infoList[i].userId, infoList[i].following, infoList[i].follower, clanId);
			});
			SetActive(UI.STR_NON_LIST, is_visible: false);
		}
	}

	protected void SetFollowStatus(Transform t, int user_id, bool following, bool follower, string clanId)
	{
		bool flag = MonoBehaviourSingleton<BlackListManager>.I.CheckBlackList(user_id);
		bool is_visible = false;
		if (MonoBehaviourSingleton<UserInfoManager>.I.userClan != null && MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsRegistered())
		{
			is_visible = (clanId == MonoBehaviourSingleton<UserInfoManager>.I.userClan.cId);
		}
		bool flag2 = !flag && (following | follower);
		SetActive(t, UI.SPR_BLACKLIST_ICON, flag);
		SetActive(t, UI.OBJ_FOLLOW, flag2);
		SetActive(t, UI.SPR_FOLLOW, flag2 && following);
		SetActive(t, UI.SPR_FOLLOWER, flag2 && follower);
		SetActive(t, UI.SPR_SAME_CLAN_ICON, is_visible);
		UIGrid component = GetComponent<UIGrid>(t, UI.GRD_FOLLOW_ARROW);
		if (component != null)
		{
			component.Reposition();
		}
	}

	private void UpdateCharaList()
	{
		infoList.Clear();
		if (CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected())
		{
			MonoBehaviourSingleton<FieldManager>.I.SendFieldCharaList(delegate(bool is_success, List<FriendCharaInfo> list)
			{
				if (is_success)
				{
					infoList = list;
					SetDirty(UI.GRD_LIST);
					RefreshUI();
				}
			});
		}
	}

	private void OnQuery_CHANGE_INFO()
	{
		UpdateCharaList();
	}

	private void OnQuery_FOLLOW()
	{
		int index = (int)GameSection.GetEventData();
		GameSection.SetEventData(new object[1]
		{
			infoList[index].name
		});
		List<int> list = new List<int>();
		list.Add(infoList[index].userId);
		GameSection.StayEvent();
		MonoBehaviourSingleton<FriendManager>.I.SendFollowUser(list, delegate(Error err, List<int> follow_list)
		{
			bool num = err == Error.None && follow_list.Count > 0;
			if (num)
			{
				infoList[index].following = !infoList[index].following;
			}
			if (MonoBehaviourSingleton<CoopApp>.IsValid())
			{
				CoopApp.UpdateField();
			}
			GameSection.ResumeEvent(num);
			RefreshUI();
		});
	}

	private void OnQuery_UN_FOLLOW()
	{
		int index = (int)GameSection.GetEventData();
		GameSection.SetEventData(new object[1]
		{
			infoList[index].name
		});
		GameSection.StayEvent();
		MonoBehaviourSingleton<FriendManager>.I.SendUnfollowUser(infoList[index].userId, delegate(bool is_success)
		{
			if (is_success)
			{
				infoList[index].following = !infoList[index].following;
			}
			GameSection.ResumeEvent(is_success);
			RefreshUI();
		});
	}

	private void OnQuery_BLACK_LIST_IN()
	{
		int index = (int)GameSection.GetEventData();
		GameSection.SetEventData(new object[1]
		{
			infoList[index].name
		});
		GameSection.StayEvent();
		MonoBehaviourSingleton<BlackListManager>.I.SendAdd(infoList[index].userId, delegate(bool is_success)
		{
			if (is_success)
			{
				infoList[index].following = false;
			}
			GameSection.ResumeEvent(is_success);
			RefreshUI();
		});
	}

	private void OnQuery_BLACK_LIST_OUT()
	{
		int index = (int)GameSection.GetEventData();
		GameSection.SetEventData(new object[1]
		{
			infoList[index].name
		});
		GameSection.StayEvent();
		MonoBehaviourSingleton<BlackListManager>.I.SendDelete(infoList[index].userId, delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success);
			RefreshUI();
		});
	}
}
