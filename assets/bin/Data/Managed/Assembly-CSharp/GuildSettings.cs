using Network;
using System;

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
			SetActive((Enum)UI.BTN_SETTING, true);
			SetActive((Enum)UI.BTN_DELETE, true);
			SetActive((Enum)UI.BTN_LEAVE, false);
		}
		else
		{
			SetActive((Enum)UI.BTN_SETTING, false);
			SetActive((Enum)UI.BTN_DELETE, false);
			SetActive((Enum)UI.BTN_LEAVE, true);
		}
		if (MonoBehaviourSingleton<GuildManager>.I.guildData.privacy == 2)
		{
			if (MonoBehaviourSingleton<GuildManager>.I.guildData.clanMasterId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
			{
				SetActive((Enum)UI.BTN_INVITE, true);
			}
			else
			{
				SetActive((Enum)UI.BTN_INVITE, false);
			}
		}
		else
		{
			SetActive((Enum)UI.BTN_INVITE, true);
		}
		SetActive((Enum)UI.SPR_BADGE, false);
		UpdateBadge();
	}

	private unsafe void UpdateBadge()
	{
		if (MonoBehaviourSingleton<GuildManager>.I.guildData != null)
		{
			MonoBehaviourSingleton<GuildManager>.I.SendMemberList(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.clanId, new Action<bool, GuildMemberListModel>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
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

	private unsafe void OnQuery_GuildDeleteConfirm_YES()
	{
		MonoBehaviourSingleton<GuildManager>.I.IsEnterGuild = false;
		GameSection.StayEvent();
		GuildManager i = MonoBehaviourSingleton<GuildManager>.I;
		if (_003C_003Ef__am_0024cache0 == null)
		{
			_003C_003Ef__am_0024cache0 = new Action<bool, Error>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		i.SendDelete(_003C_003Ef__am_0024cache0);
	}

	private unsafe void OnQuery_GuildLeaveConfirm_YES()
	{
		MonoBehaviourSingleton<GuildManager>.I.IsEnterGuild = false;
		GameSection.StayEvent();
		GuildManager i = MonoBehaviourSingleton<GuildManager>.I;
		if (_003C_003Ef__am_0024cache1 == null)
		{
			_003C_003Ef__am_0024cache1 = new Action<bool, Error>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		i.SendLeave(_003C_003Ef__am_0024cache1);
	}

	private void OnQuery_EXIT()
	{
		MonoBehaviourSingleton<GuildManager>.I.IsEnterGuild = false;
	}

	private void OnQuery_LEAVE()
	{
		GameSection.SetEventData(new CommonDialog.Desc(CommonDialog.TYPE.DECLINE_COMFIRM, "message", null, null, null, null));
	}

	private void OnQuery_DELETE()
	{
		GameSection.SetEventData(new CommonDialog.Desc(CommonDialog.TYPE.DECLINE_COMFIRM, "message", null, null, null, null));
	}
}
