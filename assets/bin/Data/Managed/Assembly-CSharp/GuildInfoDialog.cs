using Network;
using System;
using System.Collections;

public class GuildInfoDialog : GameSection
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
		LBL_LOCATION,
		LBL_DAYS,
		LBL_DONATE,
		LBL_GUILD_ID,
		BTN_JOIN,
		BTN_REQUEST,
		BTN_DETAIL,
		LBL_HUNTER_NUM
	}

	private GuildStatisticInfo _info;

	private int _clanId;

	public override void Initialize()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		_clanId = (int)GameSection.GetEventData();
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		bool finish_get_statistic = false;
		MonoBehaviourSingleton<GuildManager>.I.SendRequestStatistic(_clanId, delegate(bool success, GuildStatisticInfo info)
		{
			((_003CDoInitialize_003Ec__Iterator4B)/*Error near IL_0038: stateMachine*/)._003Cfinish_get_statistic_003E__0 = true;
			((_003CDoInitialize_003Ec__Iterator4B)/*Error near IL_0038: stateMachine*/)._003C_003Ef__this._info = info;
		});
		while (!finish_get_statistic)
		{
			yield return (object)null;
		}
		base.Initialize();
	}

	public override void UpdateUI()
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
		SetLabelText((Enum)UI.LBL_TAG, _info.tag);
		SetLabelText((Enum)UI.LBL_LEVEL, string.Format(base.sectionData.GetText("TEXT_LEVEL"), _info.level));
		SetLabelText((Enum)UI.LBL_MEM, $"{_info.currentMem}/{_info.memCap}");
		SetLabelText((Enum)UI.LBL_DESC, _info.description);
		TimeSpan timeSpan = DateTime.UtcNow - DateTime.Parse(_info.createAt);
		SetLabelText(UI.LBL_DAYS, timeSpan.Days);
		SetLabelText(UI.LBL_DONATE, _info.donate);
		SetLabelText((Enum)UI.LBL_GUILD_ID, $"{_clanId:D5}");
		SetLabelText((Enum)UI.LBL_HUNTER_NUM, $"{_info.currentMem}/{_info.memCap}");
		SetActive((Enum)UI.BTN_JOIN, _info.privacy == 0);
		SetActive((Enum)UI.BTN_REQUEST, _info.privacy == 1);
		SetButtonEnabled((Enum)UI.BTN_JOIN, _info.canJoin, true);
		SetButtonEnabled((Enum)UI.BTN_REQUEST, _info.canJoin, true);
	}

	private void OnQuery_JOIN()
	{
		GameSection.SetEventData($"Welcome to {_info.clanName}!");
		GameSection.StayEvent();
		MonoBehaviourSingleton<GuildManager>.I.SendRequestJoin(_clanId, -1, delegate(bool isSuccess, Error error)
		{
			GuildInfoDialog guildInfoDialog = this;
			DoWaitProtocolBusyFinish(delegate
			{
				if (!GuildManager.IsValidInGuild())
				{
					GameSection.ChangeStayEvent("REQUEST", null);
				}
				GameSection.ResumeEvent(isSuccess, null);
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

	private void OnQuery_REQUEST()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<GuildManager>.I.SendRequestJoin(_clanId, -1, delegate(bool isSuccess, Error error)
		{
			GuildInfoDialog guildInfoDialog = this;
			DoWaitProtocolBusyFinish(delegate
			{
				if (isSuccess)
				{
					guildInfoDialog.SetButtonEnabled((Enum)UI.BTN_REQUEST, false, true);
				}
				if (GuildManager.IsValidInGuild())
				{
					GameSection.ChangeStayEvent("JOIN", null);
				}
				GameSection.ResumeEvent(true, null);
			});
		});
	}

	private void OnQuery_MORE()
	{
		GameSection.SetEventData(_info.description);
	}
}
