using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
		SetActive((Enum)UI.OBJ_GUILD_NUMBER_ROOT, is_visible: false);
		this.StartCoroutine(DoInitialize());
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (MonoBehaviourSingleton<ChatManager>.IsValid())
		{
		}
	}

	private IEnumerator DoInitialize()
	{
		bool is_finish = false;
		GetListItem(delegate
		{
			is_finish = true;
		});
		while (!is_finish)
		{
			yield return null;
		}
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetActive((Enum)UI.OBJ_GUILD_NUMBER_ROOT, is_visible: true);
		SetLabelText(UI.LBL_GUILD_NUMBER_MAX, allMember.Count);
		if (allMember == null || allMember.Count == 0)
		{
			SetActive((Enum)UI.STR_NON_LIST, is_visible: true);
			SetActive((Enum)UI.GRD_LIST, is_visible: false);
			return;
		}
		int online_count = 0;
		SetDynamicList((Enum)UI.GRD_LIST, "GuildMemberListItem", allMember.Count, reset: true, (Func<int, bool>)null, (Func<int, Transform, Transform>)null, (Action<int, Transform, bool>)delegate(int i, Transform t, bool is_recycle)
		{
			FriendCharaInfo member = allMember[i];
			bool flag = MonoBehaviourSingleton<BlackListManager>.I.CheckBlackList(member.userId);
			SetActive(t, UI.SPR_BLACKLIST_ICON, flag);
			SetActive(t, UI.SPR_FOLLOW, !flag && member.following);
			SetActive(t, UI.SPR_FOLLOWER, !flag && member.follower);
			SetActive(t, UI.SPR_ICON_FIRST_MET, is_visible: false);
			SetCharaInfo(member, i, t, is_recycle, 0 == member.userId);
			bool flag2 = status.Any((int st) => st == member.userId);
			if (flag2)
			{
				online_count++;
			}
			if (requestMembers.Contains(member))
			{
				SetActive(t, UI.OBJ_OFFLINE_MASK, is_visible: false);
			}
			else
			{
				SetActive(t, UI.OBJ_OFFLINE_MASK, !flag2);
			}
			SetActive(t, UI.OBJ_REQUEST_PENDING, requestMembers.Contains(member));
			SetListItem(i, t, ListItemEvent, member);
		});
		SetLabelText((Enum)UI.LBL_GUILD_NUMBER_NOW, online_count.ToString());
		SetActive((Enum)UI.STR_NON_LIST, is_visible: false);
		SetActive((Enum)UI.GRD_LIST, is_visible: true);
	}

	protected virtual void GetListItem(Action<bool, object> callback)
	{
		MonoBehaviourSingleton<GuildManager>.I.SendMemberList(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.clanId, delegate(bool success, GuildMemberListModel ret)
		{
			allMember = new List<FriendCharaInfo>();
			members = new List<FriendCharaInfo>(ret.result.list);
			members.Remove(members.FirstOrDefault((FriendCharaInfo o) => o.userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id));
			requestMembers = new List<FriendCharaInfo>(ret.result.requesters);
			requestMembers.Remove(requestMembers.FirstOrDefault((FriendCharaInfo o) => o.userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id));
			allMember.AddRange(requestMembers);
			allMember.AddRange(members);
			MonoBehaviourSingleton<GuildManager>.I.SendClanChatOnlineStatus(delegate(bool is_success, List<GuildMemberChatStatus> list)
			{
				status = (from o in list
				select o.id).ToList();
				callback(is_success, null);
			});
		});
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
			SetRenderNPCModel(t, UI.TEX_MODEL, 0, new Vector3(0f, -1.49f, 1.87f), new Vector3(0f, 154f, 0f), 10f);
		}
		else
		{
			SetRenderPlayerModel(t, UI.TEX_MODEL, PlayerLoadInfo.FromCharaInfo(data, need_weapon: false, need_helm: true, need_leg: false, is_priority_visual_equip: true), 99, new Vector3(0f, -1.536f, 1.87f), new Vector3(0f, 154f, 0f), is_priority_visual_equip: true);
		}
		SetLabelText(t, UI.LBL_NAME, data.name);
		SetLabelText(t, UI.LBL_LEVEL, data.level.ToString());
		SetLabelText(t, UI.LBL_COMMENT, data.comment);
		SetLabelText(t, UI.LBL_LAST_LOGIN, base.sectionData.GetText("LAST_LOGIN"));
		SetLabelText(t, UI.LBL_LAST_LOGIN_TIME, data.lastLogin);
		EquipSetCalculator otherEquipSetCalculator = MonoBehaviourSingleton<StatusManager>.I.GetOtherEquipSetCalculator(i + 4);
		otherEquipSetCalculator.SetEquipSet(data.equipSet);
		SimpleStatus finalStatus = otherEquipSetCalculator.GetFinalStatus(0, data.hp, data.atk, data.def);
		SetLabelText(t, UI.LBL_ATK, finalStatus.GetAttacksSum().ToString());
		SetLabelText(t, UI.LBL_DEF, finalStatus.defences[0].ToString());
		SetLabelText(t, UI.LBL_HP, finalStatus.hp.ToString());
		DegreePlate component = FindCtrl(t, UI.OBJ_DEGREE_FRAME_ROOT).GetComponent<DegreePlate>();
		component.Initialize(data.selectedDegrees, isButton: false, delegate
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
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
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
		SetActive(base._transform, UI.SPR_GUILD_INFO, is_visible: false);
	}

	protected void OnQuery_MEMBER_MESSAGE()
	{
		MonoBehaviourSingleton<GuildManager>.I.SetTalkUser(memberInfoData);
		SetActive(base._transform, UI.SPR_GUILD_INFO, is_visible: false);
	}

	private void OnQuery_MEMBER_BAN()
	{
		GameSection.SetEventData(memberInfoData);
		SetActive(base._transform, UI.SPR_GUILD_INFO, is_visible: false);
	}

	private void OnQuery_MEMBER_ACCEPT()
	{
		SetActive(base._transform, UI.SPR_GUILD_INFO, is_visible: false);
		GameSection.StayEvent();
		if (memberInfoData != null)
		{
			MonoBehaviourSingleton<GuildManager>.I.SendAdminJoin(memberInfoData.requestId, 1, delegate(bool is_success, Error err)
			{
				MonoBehaviourSingleton<GuildManager>.I.SendMemberList(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.clanId, delegate
				{
					GameSection.ResumeEvent(is_success);
				});
			});
		}
	}

	private void OnQuery_MEMBER_REJECT()
	{
		SetActive(base._transform, UI.SPR_GUILD_INFO, is_visible: false);
		GameSection.StayEvent();
		if (memberInfoData != null)
		{
			MonoBehaviourSingleton<GuildManager>.I.SendAdminJoin(memberInfoData.requestId, 0, delegate(bool is_success, Error err)
			{
				MonoBehaviourSingleton<GuildManager>.I.SendMemberList(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.clanId, delegate
				{
					GameSection.ResumeEvent(is_success);
				});
			});
		}
	}

	private void OnQuery_GuildKickMember_YES()
	{
		GameSection.SetEventData(memberInfoData);
	}

	private void OnQuery_BanReasonConfirm_YES()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<GuildManager>.I.SendKick(memberInfoData.userId, delegate(bool is_success, Error err)
		{
			GameSection.ResumeEvent(is_success);
		});
	}

	private void OnQuery_BanReasonConfirm_NO()
	{
	}
}
