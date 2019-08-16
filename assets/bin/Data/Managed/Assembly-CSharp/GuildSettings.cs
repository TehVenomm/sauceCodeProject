using Network;
using System;
using System.Collections.Generic;
using System.Linq;

public class GuildSettings : GameSection
{
	private enum UI
	{
		LBL_GUILD_ID,
		BTN_LEAVE,
		BTN_DELETE,
		BTN_INVITE,
		BTN_SETTING,
		BTN_MEMBER,
		SPR_BADGE
	}

	public override void Initialize()
	{
		base.Initialize();
		if (MonoBehaviourSingleton<GuildManager>.I.guildData.clanMasterId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
		{
			SetActive((Enum)UI.BTN_SETTING, is_visible: true);
			SetActive((Enum)UI.BTN_DELETE, is_visible: true);
			SetActive((Enum)UI.BTN_LEAVE, is_visible: false);
		}
		else
		{
			SetActive((Enum)UI.BTN_SETTING, is_visible: false);
			SetActive((Enum)UI.BTN_DELETE, is_visible: false);
			SetActive((Enum)UI.BTN_LEAVE, is_visible: true);
		}
		if (MonoBehaviourSingleton<GuildManager>.I.guildData.privacy == 2)
		{
			if (MonoBehaviourSingleton<GuildManager>.I.guildData.clanMasterId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
			{
				SetActive((Enum)UI.BTN_INVITE, is_visible: true);
			}
			else
			{
				SetActive((Enum)UI.BTN_INVITE, is_visible: false);
			}
		}
		else
		{
			SetActive((Enum)UI.BTN_INVITE, is_visible: true);
		}
		SetActive((Enum)UI.SPR_BADGE, is_visible: false);
		UpdateBadge();
	}

	private void UpdateBadge()
	{
		if (MonoBehaviourSingleton<GuildManager>.I.guildData != null)
		{
			MonoBehaviourSingleton<GuildManager>.I.SendMemberList(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.clanId, delegate(bool success, GuildMemberListModel ret)
			{
				List<FriendCharaInfo> list = new List<FriendCharaInfo>(ret.result.requesters);
				list.Remove(list.FirstOrDefault((FriendCharaInfo o) => o.userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id));
				SetActive(FindCtrl(base._transform, UI.BTN_MEMBER), UI.SPR_BADGE, list.Count > 0);
			});
		}
	}

	public override void UpdateUI()
	{
		SetLabelText((Enum)UI.LBL_GUILD_ID, $"{MonoBehaviourSingleton<UserInfoManager>.I.userStatus.clanId:D5}");
	}

	private void OnQuery_MESSAGE()
	{
		MonoBehaviourSingleton<GuildManager>.I.EmptyTalkUser();
	}

	private void OnQuery_GuildDeleteConfirm_YES()
	{
		MonoBehaviourSingleton<GuildManager>.I.IsEnterGuild = false;
		GameSection.StayEvent();
		MonoBehaviourSingleton<GuildManager>.I.SendDelete(delegate(bool is_success, Error err)
		{
			if (is_success)
			{
				MonoBehaviourSingleton<ChatManager>.I.DestroyClanChat();
			}
			GameSection.ResumeEvent(is_success);
		});
	}

	private void OnQuery_GuildLeaveConfirm_YES()
	{
		MonoBehaviourSingleton<GuildManager>.I.IsEnterGuild = false;
		GameSection.StayEvent();
		MonoBehaviourSingleton<GuildManager>.I.SendLeave(delegate(bool is_success, Error err)
		{
			if (is_success)
			{
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(NOTIFY_FLAG.UPDATE_EQUIP_CHANGE);
				MonoBehaviourSingleton<ChatManager>.I.DestroyClanChat();
			}
			GameSection.ResumeEvent(is_success);
		});
	}

	private void OnQuery_EXIT()
	{
		MonoBehaviourSingleton<GuildManager>.I.IsEnterGuild = false;
	}

	private void OnQuery_LEAVE()
	{
		GameSection.SetEventData(new CommonDialog.Desc(CommonDialog.TYPE.DECLINE_COMFIRM, "message"));
	}

	private void OnQuery_DELETE()
	{
		GameSection.SetEventData(new CommonDialog.Desc(CommonDialog.TYPE.DECLINE_COMFIRM, "message"));
	}
}
