using Network;
using System;
using System.Collections;
using UnityEngine;

public class GuildSearchList : GameSection
{
	protected enum UI
	{
		GRD_GUILD,
		STR_NON_LIST,
		LBL_HOST_NAME,
		LBL_HOST_LV,
		LBL_GUILD_NAME,
		LBL_LABEL,
		LBL_STYLE,
		OBJ_SYMBOL,
		SPR_EMBLEM_LAYER_1,
		SPR_EMBLEM_LAYER_2,
		SPR_EMBLEM_LAYER_3,
		LBL_HOST_MEMBER_NUM
	}

	private GuildSearchModel.GuildSearchInfo[] guilds;

	public override void Initialize()
	{
		MonoBehaviourSingleton<GuildManager>.I.ResetGuildSearchRequest();
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		yield return this.StartCoroutine(Reload());
		base.Initialize();
	}

	public override void UpdateUI()
	{
		if (!GuildManager.IsValidNotEmptyGuildList())
		{
			SetActive((Enum)UI.GRD_GUILD, is_visible: false);
			SetActive((Enum)UI.STR_NON_LIST, is_visible: true);
			return;
		}
		guilds = MonoBehaviourSingleton<GuildManager>.I.guilds.ToArray();
		SetActive((Enum)UI.GRD_GUILD, is_visible: true);
		SetActive((Enum)UI.STR_NON_LIST, is_visible: false);
		SetGrid(UI.GRD_GUILD, "GuildSearchListItem", guilds.Length, reset: true, delegate(int i, Transform t, bool is_recycle)
		{
			GuildSearchModel.GuildSearchInfo guildSearchInfo = guilds[i];
			SetEvent(t, "INFO", guildSearchInfo.clanId);
			SetGuildData(guilds[i], t);
		});
		base.UpdateUI();
	}

	private void SetGuildData(GuildSearchModel.GuildSearchInfo guild, Transform t)
	{
		SetLabelText(t, UI.LBL_GUILD_NAME, string.Format(guild.name + " [{0}]", guild.tag));
		object label_enum = UI.LBL_LABEL;
		GuildManager.GUILD_TYPE privacy = (GuildManager.GUILD_TYPE)guild.privacy;
		SetLabelText(t, (Enum)label_enum, privacy.ToString());
		SetLabelText(t, UI.LBL_HOST_LV, guild.level);
		SetLabelText(t, UI.LBL_HOST_MEMBER_NUM, guild.currentMem + "/" + guild.memCap);
		if (guild.emblem != null && guild.emblem.Length >= 3)
		{
			SetSprite(t, UI.SPR_EMBLEM_LAYER_1, GuildItemManager.I.GetItemSprite(guild.emblem[0]));
			SetSprite(t, UI.SPR_EMBLEM_LAYER_2, GuildItemManager.I.GetItemSprite(guild.emblem[1]));
			SetSprite(t, UI.SPR_EMBLEM_LAYER_3, GuildItemManager.I.GetItemSprite(guild.emblem[2]));
		}
		else
		{
			SetSprite(t, UI.SPR_EMBLEM_LAYER_1, string.Empty);
			SetSprite(t, UI.SPR_EMBLEM_LAYER_2, string.Empty);
			SetSprite(t, UI.SPR_EMBLEM_LAYER_3, string.Empty);
		}
		SetLabelText(t, UI.LBL_HOST_NAME, guild.admin);
	}

	private void OnQuery_RELOAD()
	{
		MonoBehaviourSingleton<GuildManager>.I.mSearchKeywork = string.Empty;
		GameSection.StayEvent();
		this.StartCoroutine(Reload(delegate(bool b)
		{
			GameSection.ResumeEvent(b);
		}));
	}

	private void OnCloseDialog_GuildSearchSettings()
	{
		RefreshUI();
	}

	private void OnCloseDialog_GuildEntryPassRoom()
	{
		RefreshUI();
	}

	private IEnumerator Reload(Action<bool> cb = null)
	{
		bool is_recv = false;
		SendRequest(delegate
		{
			is_recv = true;
		}, cb);
		while (!is_recv)
		{
			yield return null;
		}
		SetDirty(UI.GRD_GUILD);
		RefreshUI();
	}

	private void SendRequest(Action onFinish, Action<bool> cb)
	{
		MonoBehaviourSingleton<GuildManager>.I.SendSearch(delegate(bool isSuccess, Error error)
		{
			onFinish();
			if (cb != null)
			{
				cb(isSuccess);
			}
		}, saveSettings: false);
	}
}
