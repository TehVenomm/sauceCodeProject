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
		SPR_FOLLOW,
		SPR_FOLLOWER,
		SPR_BLACKLIST_ICON,
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

	private unsafe void OnScreenRotate(bool is_portrait)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Expected O, but got Unknown
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Expected O, but got Unknown
		UIPanel panel = GetCtrl(UI.SCR_LIST).GetComponent<UIPanel>();
		Vector4 baseClipRegion = panel.baseClipRegion;
		if (is_portrait)
		{
			Vector3 localPosition = GetCtrl(UI.PORTRAIT_FRAME).get_localPosition();
			int height = GetHeight(UI.PORTRAIT_FRAME);
			GetCtrl(UI.FRAME).set_localPosition(localPosition);
			SetHeight((Enum)UI.FRAME, height);
			GetCtrl(UI.SCR_LIST).set_parent(GetCtrl(UI.PORTRAIT_LIST));
			baseClipRegion.w = (float)GetHeight(UI.PORTRAIT_LIST);
		}
		else
		{
			Vector3 localPosition2 = GetCtrl(UI.LANDSCAPE_FRAME).get_localPosition();
			int height2 = GetHeight(UI.LANDSCAPE_FRAME);
			GetCtrl(UI.FRAME).set_localPosition(localPosition2);
			SetHeight((Enum)UI.FRAME, height2);
			GetCtrl(UI.SCR_LIST).set_parent(GetCtrl(UI.LANDSCAPE_LIST));
			baseClipRegion.w = (float)GetHeight(UI.LANDSCAPE_LIST);
		}
		panel.baseClipRegion = baseClipRegion;
		panel.clipOffset = Vector2.get_zero();
		GetCtrl(UI.SCR_LIST).set_localPosition(Vector3.get_zero());
		ScrollViewResetPosition((Enum)UI.SCR_LIST);
		UpdateAnchors();
		AppMain i = MonoBehaviourSingleton<AppMain>.I;
		_003COnScreenRotate_003Ec__AnonStorey38A _003COnScreenRotate_003Ec__AnonStorey38A;
		i.onDelayCall = Delegate.Combine((Delegate)i.onDelayCall, (Delegate)new Action((object)_003COnScreenRotate_003Ec__AnonStorey38A, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	public unsafe override void UpdateUI()
	{
		int count = infoList.Count;
		if (count <= 0)
		{
			SetActive((Enum)UI.GRD_LIST, false);
			SetActive((Enum)UI.STR_NON_LIST, true);
		}
		else
		{
			SetGrid(UI.GRD_LIST, "InGamePlayerListItem", count, false, new Action<int, Transform, bool>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			SetActive((Enum)UI.STR_NON_LIST, false);
		}
	}

	private unsafe void UpdateCharaList()
	{
		infoList.Clear();
		if (CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected())
		{
			MonoBehaviourSingleton<FieldManager>.I.SendFieldCharaList(new Action<bool, List<FriendCharaInfo>>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}

	private void OnQuery_CHANGE_INFO()
	{
		UpdateCharaList();
	}

	private unsafe void OnQuery_FOLLOW()
	{
		int index = (int)GameSection.GetEventData();
		GameSection.SetEventData(new object[1]
		{
			infoList[index].name
		});
		List<int> list = new List<int>();
		list.Add(infoList[index].userId);
		GameSection.StayEvent();
		_003COnQuery_FOLLOW_003Ec__AnonStorey38B _003COnQuery_FOLLOW_003Ec__AnonStorey38B;
		MonoBehaviourSingleton<FriendManager>.I.SendFollowUser(list, new Action<Error, List<int>>((object)_003COnQuery_FOLLOW_003Ec__AnonStorey38B, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
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
			GameSection.ResumeEvent(is_success, null);
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
			GameSection.ResumeEvent(is_success, null);
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
			GameSection.ResumeEvent(is_success, null);
			RefreshUI();
		});
	}
}
