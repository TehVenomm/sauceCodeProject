using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildMemberList : GameSection
{
	protected enum UI
	{
		OBJ_GUILD_NUMBER_ROOT,
		LBL_GUILD_NUMBER_NOW,
		LBL_GUILD_NUMBER_MAX,
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
		LBL_NAME,
		LBL_ATK,
		LBL_DEF,
		LBL_HP,
		LBL_LEVEL,
		OBJ_DEGREE_FRAME_ROOT,
		SPR_ICON_FIRST_MET,
		OBJ_OFFLINE_MASK,
		OBJ_REQUEST_PENDING,
		OBJ_GUILD_INFO,
		SPR_GUILD_INFO,
		BTN_ACCEPT,
		BTN_REJECT,
		BTN_MESSAGE,
		BTN_BAN
	}

	protected List<FriendCharaInfo> members;

	protected List<FriendCharaInfo> requestMembers;

	protected List<FriendCharaInfo> allMember;

	protected FriendCharaInfo memberInfoData;

	protected List<int> status;

	public virtual string ListItemEvent => "GUILD_INFO";

	public override void Initialize()
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		SetActive((Enum)UI.OBJ_GUILD_NUMBER_ROOT, false);
		MonoBehaviourSingleton<ChatManager>.I.clanChat.onJoin += OnJoinClanChat;
		MonoBehaviourSingleton<ChatManager>.I.clanChat.onLeave += OnLeaveClanChat;
		this.StartCoroutine(DoInitialize());
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (MonoBehaviourSingleton<ChatManager>.IsValid())
		{
			MonoBehaviourSingleton<ChatManager>.I.clanChat.onJoin -= OnJoinClanChat;
			MonoBehaviourSingleton<ChatManager>.I.clanChat.onLeave -= OnLeaveClanChat;
		}
	}

	private unsafe IEnumerator DoInitialize()
	{
		bool is_finish = false;
		GetListItem(new Action<bool, object>((object)/*Error near IL_002e: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		while (!is_finish)
		{
			yield return (object)null;
		}
		base.Initialize();
	}

	public unsafe override void UpdateUI()
	{
		SetActive((Enum)UI.OBJ_GUILD_NUMBER_ROOT, true);
		SetLabelText(UI.LBL_GUILD_NUMBER_MAX, allMember.Count);
		if (allMember == null || allMember.Count == 0)
		{
			SetActive((Enum)UI.STR_NON_LIST, true);
			SetActive((Enum)UI.GRD_LIST, false);
		}
		else
		{
			int online_count = 0;
			_003CUpdateUI_003Ec__AnonStorey33A _003CUpdateUI_003Ec__AnonStorey33A;
			SetDynamicList((Enum)UI.GRD_LIST, "GuildMemberListItem", allMember.Count, true, null, null, new Action<int, Transform, bool>((object)_003CUpdateUI_003Ec__AnonStorey33A, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			SetLabelText((Enum)UI.LBL_GUILD_NUMBER_NOW, online_count.ToString());
			SetActive((Enum)UI.STR_NON_LIST, false);
			SetActive((Enum)UI.GRD_LIST, true);
		}
	}

	protected unsafe virtual void GetListItem(Action<bool, object> callback)
	{
		_003CGetListItem_003Ec__AnonStorey33B _003CGetListItem_003Ec__AnonStorey33B;
		MonoBehaviourSingleton<GuildManager>.I.SendMemberList(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.clanId, new Action<bool, GuildMemberListModel>((object)_003CGetListItem_003Ec__AnonStorey33B, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	protected virtual void SetListItem(int i, Transform t, string event_name, FriendCharaInfo member)
	{
		SetEvent(t, event_name, member);
	}

	protected void SetCharaInfo(FriendCharaInfo data, int i, Transform t, bool is_recycle, bool isGM)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		if (isGM)
		{
			SetRenderNPCModel(t, UI.TEX_MODEL, 0, new Vector3(0f, -1.49f, 1.87f), new Vector3(0f, 154f, 0f), 10f, null);
		}
		else
		{
			SetRenderPlayerModel(t, UI.TEX_MODEL, PlayerLoadInfo.FromCharaInfo(data, false, true, false, true), 99, new Vector3(0f, -1.536f, 1.87f), new Vector3(0f, 154f, 0f), true, null);
		}
		SetLabelText(t, UI.LBL_NAME, data.name);
		SetLabelText(t, UI.LBL_LEVEL, data.level.ToString());
		SetLabelText(t, UI.LBL_COMMENT, data.comment);
		SetLabelText(t, UI.LBL_LAST_LOGIN, base.sectionData.GetText("LAST_LOGIN"));
		SetLabelText(t, UI.LBL_LAST_LOGIN_TIME, data.lastLogin);
		EquipSetCalculator otherEquipSetCalculator = MonoBehaviourSingleton<StatusManager>.I.GetOtherEquipSetCalculator(i + 4);
		otherEquipSetCalculator.SetEquipSet(data.equipSet, false);
		SimpleStatus finalStatus = otherEquipSetCalculator.GetFinalStatus(0, data.hp, data.atk, data.def);
		SetLabelText(t, UI.LBL_ATK, finalStatus.GetAttacksSum().ToString());
		SetLabelText(t, UI.LBL_DEF, finalStatus.defences[0].ToString());
		SetLabelText(t, UI.LBL_HP, finalStatus.hp.ToString());
		DegreePlate component = FindCtrl(t, UI.OBJ_DEGREE_FRAME_ROOT).GetComponent<DegreePlate>();
		component.Initialize(data.selectedDegrees, false, delegate
		{
			GetCtrl(UI.GRD_LIST).GetComponent<UIGrid>().Reposition();
		});
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		if ((flags & NOTIFY_FLAG.UPDATE_GUILD_LIST) != (NOTIFY_FLAG)0L)
		{
			RefreshUI();
		}
		base.OnNotify(flags);
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_GUILD_LIST;
	}

	private void OnJoinClanChat(CHAT_ERROR_TYPE errorType, string userId)
	{
		int item = int.Parse(userId);
		if (!status.Contains(item))
		{
			status.Add(item);
			RefreshUI();
		}
	}

	private void OnLeaveClanChat(CHAT_ERROR_TYPE errorType, string userId)
	{
		int item = int.Parse(userId);
		if (status.Contains(item))
		{
			status.Remove(item);
			RefreshUI();
		}
	}

	protected void OnQuery_GUILD_INFO()
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Expected O, but got Unknown
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		memberInfoData = (FriendCharaInfo)GameSection.GetEventData();
		SetButton();
		GameObject sender = GameSceneEvent.current.sender;
		FindCtrl(base._transform, UI.SPR_GUILD_INFO).SetParent(FindCtrl(sender.get_transform(), UI.OBJ_GUILD_INFO));
		FindCtrl(base._transform, UI.SPR_GUILD_INFO).set_localPosition(Vector3.get_zero());
		FindCtrl(base._transform, UI.SPR_GUILD_INFO).set_localScale(Vector3.get_one());
		Transform val = FindCtrl(base._transform, UI.SPR_GUILD_INFO);
		SetActive(base._transform, UI.SPR_GUILD_INFO, !val.get_gameObject().get_activeSelf());
	}

	private void SetButton()
	{
		bool flag = requestMembers != null && requestMembers.Count > 0 && memberInfoData.requestId != 0;
		Transform root = FindCtrl(base._transform, UI.SPR_GUILD_INFO);
		SetActive(root, UI.BTN_ACCEPT, flag);
		SetActive(root, UI.BTN_REJECT, flag);
		SetActive(root, UI.BTN_BAN, !flag && MonoBehaviourSingleton<GuildManager>.I.guildData.clanMasterId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
		SetActive(root, UI.BTN_MESSAGE, !flag);
	}

	protected void OnQuery_MEMBER_PROFILE()
	{
		GameSection.SetEventData(memberInfoData);
		SetActive(base._transform, UI.SPR_GUILD_INFO, false);
	}

	protected void OnQuery_MEMBER_MESSAGE()
	{
		MonoBehaviourSingleton<GuildManager>.I.SetTalkUser(memberInfoData);
		SetActive(base._transform, UI.SPR_GUILD_INFO, false);
	}

	private void OnQuery_MEMBER_BAN()
	{
		GameSection.SetEventData(memberInfoData);
		SetActive(base._transform, UI.SPR_GUILD_INFO, false);
	}

	private unsafe void OnQuery_MEMBER_ACCEPT()
	{
		SetActive(base._transform, UI.SPR_GUILD_INFO, false);
		GameSection.StayEvent();
		if (memberInfoData != null)
		{
			GuildManager i = MonoBehaviourSingleton<GuildManager>.I;
			int requestId = memberInfoData.requestId;
			if (_003C_003Ef__am_0024cache5 == null)
			{
				_003C_003Ef__am_0024cache5 = new Action<bool, Error>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			i.SendAdminJoin(requestId, 1, _003C_003Ef__am_0024cache5);
		}
	}

	private unsafe void OnQuery_MEMBER_REJECT()
	{
		SetActive(base._transform, UI.SPR_GUILD_INFO, false);
		GameSection.StayEvent();
		if (memberInfoData != null)
		{
			GuildManager i = MonoBehaviourSingleton<GuildManager>.I;
			int requestId = memberInfoData.requestId;
			if (_003C_003Ef__am_0024cache6 == null)
			{
				_003C_003Ef__am_0024cache6 = new Action<bool, Error>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			i.SendAdminJoin(requestId, 0, _003C_003Ef__am_0024cache6);
		}
	}

	private void OnQuery_GuildKickMember_YES()
	{
		GameSection.SetEventData(memberInfoData);
	}

	private unsafe void OnQuery_BanReasonConfirm_YES()
	{
		GameSection.StayEvent();
		GuildManager i = MonoBehaviourSingleton<GuildManager>.I;
		int userId = memberInfoData.userId;
		if (_003C_003Ef__am_0024cache7 == null)
		{
			_003C_003Ef__am_0024cache7 = new Action<bool, Error>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		i.SendKick(userId, _003C_003Ef__am_0024cache7);
	}

	private void OnQuery_BanReasonConfirm_NO()
	{
	}
}
