using Network;
using System;
using System.Collections;
using UnityEngine;

public class GuildInvitedJoinDialog : GameSection
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
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		guildInviteInfo = (GameSection.GetEventData() as GuildInvitedModel.GuildInvitedInfo);
		_clanId = guildInviteInfo.clanId;
		this.StartCoroutine(DoInitialize());
	}

	private unsafe IEnumerator DoInitialize()
	{
		bool finish_get_statistic = false;
		MonoBehaviourSingleton<GuildManager>.I.SendRequestStatistic(_clanId, new Action<bool, GuildStatisticInfo>((object)/*Error near IL_0038: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		while (!finish_get_statistic)
		{
			yield return (object)null;
		}
		if (_info != null)
		{
			Transform t = GetCtrl(UI.BTN_ACCEPT_INVITE);
			SetEvent(t, "JOIN", null);
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

	private unsafe void OnQuery_JOIN()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<GuildManager>.I.SendRequestJoin(_clanId, -1, new Action<bool, Error>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	private unsafe void OnQuery_REJECT()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<GuildManager>.I.SendRejectInviteClan(guildInviteInfo.requestId, delegate
		{
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Expected O, but got Unknown
			GuildInvitedJoinDialog guildInvitedJoinDialog = this;
			_003COnQuery_REJECT_003Ec__AnonStorey338 _003COnQuery_REJECT_003Ec__AnonStorey;
			DoWaitProtocolBusyFinish(new Action((object)_003COnQuery_REJECT_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
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
