using Network;
using System;
using System.Collections;
using UnityEngine;

public class InGameGuildInvitedJoinDialog : GameSection
{
	private enum UI
	{
		LBL_GUILD_NAME,
		SPR_EMBLEM_LAYER_1,
		SPR_EMBLEM_LAYER_2,
		SPR_EMBLEM_LAYER_3,
		LBL_TAG,
		LBL_LEVEL,
		LBL_MEM,
		LBL_DESC,
		LBL_DAYS,
		LBL_DONATE,
		LBL_GUILD_ID,
		BTN_JOIN,
		BTN_REQUEST,
		BTN_DETAIL,
		LBL_HUNTER_NUM,
		BTN_REJECT_INVITE,
		BTN_ACCEPT_INVITE
	}

	private GuildStatisticInfo _info;

	private int _clanId;

	private GuildInvitedModel.GuildInvitedInfo guildInviteInfo;

	public override void Initialize()
	{
		guildInviteInfo = (GameSection.GetEventData() as GuildInvitedModel.GuildInvitedInfo);
		_clanId = guildInviteInfo.clanId;
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		bool finish_get_statistic = false;
		MonoBehaviourSingleton<GuildManager>.I.SendRequestStatistic(_clanId, delegate(bool success, GuildStatisticInfo info)
		{
			finish_get_statistic = true;
			_info = info;
		});
		while (!finish_get_statistic)
		{
			yield return null;
		}
		if (_info != null)
		{
			Transform ctrl = GetCtrl(UI.BTN_ACCEPT_INVITE);
			SetEvent(ctrl, "JOIN", null);
		}
		base.Initialize();
	}

	public override void StartSection()
	{
		if (_info == null)
		{
			MonoBehaviourSingleton<GuildManager>.I.guildInviteList.Remove(guildInviteInfo);
			GameSection.BackSection();
		}
	}

	public override void UpdateUI()
	{
		if (_info != null)
		{
			SetLabelText((Enum)UI.LBL_GUILD_NAME, _info.clanName);
			if (_info.emblem != null && _info.emblem.Length >= 3)
			{
				SetSprite((Enum)UI.SPR_EMBLEM_LAYER_1, GuildItemManager.I.GetItemSprite(_info.emblem[0]));
				SetSprite((Enum)UI.SPR_EMBLEM_LAYER_2, GuildItemManager.I.GetItemSprite(_info.emblem[1]));
				SetSprite((Enum)UI.SPR_EMBLEM_LAYER_3, GuildItemManager.I.GetItemSprite(_info.emblem[2]));
			}
			else
			{
				SetSprite((Enum)UI.SPR_EMBLEM_LAYER_1, string.Empty);
				SetSprite((Enum)UI.SPR_EMBLEM_LAYER_2, string.Empty);
				SetSprite((Enum)UI.SPR_EMBLEM_LAYER_3, string.Empty);
			}
			SetLabelText((Enum)UI.LBL_LEVEL, string.Format(base.sectionData.GetText("TEXT_LEVEL"), _info.level));
			SetLabelText((Enum)UI.LBL_MEM, $"{_info.currentMem}/{_info.memCap}");
			SetLabelText((Enum)UI.LBL_DESC, _info.description);
			SetLabelText((Enum)UI.LBL_TAG, _info.tag);
			TimeSpan timeSpan = DateTime.UtcNow - DateTime.Parse(_info.createAt);
			SetLabelText(UI.LBL_DAYS, timeSpan.Days);
			SetLabelText(UI.LBL_DONATE, _info.donate);
			SetLabelText((Enum)UI.LBL_HUNTER_NUM, $"{_info.currentMem}/{_info.memCap}");
		}
	}

	private void OnQuery_JOIN()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<GuildManager>.I.SendRequestJoin(_clanId, -1, delegate(bool isSuccess, Error error)
		{
			InGameGuildInvitedJoinDialog inGameGuildInvitedJoinDialog = this;
			DoWaitProtocolBusyFinish(delegate
			{
				if (!GuildManager.IsValidInGuild())
				{
					GameSection.ChangeStayEvent("REQUEST");
				}
				GameSection.ResumeEvent(isSuccess);
				if (GuildManager.IsValidInGuild())
				{
					MonoBehaviourSingleton<GuildManager>.I.guildInviteList.Clear();
					MonoBehaviourSingleton<UserInfoManager>.I.ClearPartyInvite();
					MonoBehaviourSingleton<UIManager>.I.invitationButton.Close();
					MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.Close();
					MonoBehaviourSingleton<UserInfoManager>.I.showJoinClanInGame = true;
					inGameGuildInvitedJoinDialog.BackToHome();
				}
			});
		});
	}

	private void OnQuery_REJECT()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<GuildManager>.I.SendRejectInviteClan(guildInviteInfo.requestId, delegate(bool isSuccess)
		{
			InGameGuildInvitedJoinDialog inGameGuildInvitedJoinDialog = this;
			DoWaitProtocolBusyFinish(delegate
			{
				GameSection.ResumeEvent(isSuccess);
				MonoBehaviourSingleton<GuildManager>.I.guildInviteList.Remove(inGameGuildInvitedJoinDialog.guildInviteInfo);
			});
		});
	}

	private void OnQuery_DETAIL()
	{
		GameSection.SetEventData(new object[2]
		{
			_info,
			_clanId
		});
	}

	private void OnQuery_MORE()
	{
		GameSection.SetEventData(_info.description);
	}

	private void BackToHome()
	{
		if (MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			if (QuestManager.IsValidInGame())
			{
				MonoBehaviourSingleton<InGameProgress>.I.InviteInQuest();
			}
			else
			{
				MonoBehaviourSingleton<InGameProgress>.I.FieldToHome();
			}
		}
	}
}
