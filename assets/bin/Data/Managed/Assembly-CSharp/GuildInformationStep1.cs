using Network;
using System;
using System.Collections;

public class GuildInformationStep1 : GameSection
{
	public enum UI
	{
		SPR_GUILD_EMBLEM_1,
		SPR_GUILD_EMBLEM_2,
		SPR_GUILD_EMBLEM_3,
		IPT_NAME,
		IPT_TAG,
		LBL_INPUT_TAG,
		IPT_DESCRIBE,
		BTN_INFO,
		SPR_TAG,
		LBL_DEFAULT_INPUT_DESCRIBE,
		LBL_DEFAULT_INPUT_NAME,
		BTN_NEXT,
		BTN_NEXT_UPDATE,
		LBL_INPUT
	}

	private GuildManager.CreateGuildRequestParam mCreateRequest;

	public override void Initialize()
	{
		base.Initialize();
		if (MonoBehaviourSingleton<GuildManager>.I.guildData != null && MonoBehaviourSingleton<GuildManager>.I.guildData.clanId != -1)
		{
			MonoBehaviourSingleton<GuildManager>.I.CreateAddedGuildRequestParam(MonoBehaviourSingleton<GuildManager>.I.guildStatData);
		}
		else
		{
			MonoBehaviourSingleton<GuildManager>.I.ClearCreateGuildRequestParam();
		}
		mCreateRequest = MonoBehaviourSingleton<GuildManager>.I.GetCreateGuildRequestParam();
		UpdateUIInfo();
	}

	private IEnumerator GetClanStatistic(int clanID)
	{
		bool finish_get_statistic = false;
		GuildStatisticInfo _info = null;
		MonoBehaviourSingleton<GuildManager>.I.SendRequestStatistic(clanID, delegate(bool success, GuildStatisticInfo info)
		{
			finish_get_statistic = true;
			if (success)
			{
				_info = info;
			}
		});
		while (!finish_get_statistic)
		{
			yield return null;
		}
		MonoBehaviourSingleton<GuildManager>.I.CreateAddedGuildRequestParam(_info);
		mCreateRequest = MonoBehaviourSingleton<GuildManager>.I.GetCreateGuildRequestParam();
		UpdateUIInfo();
	}

	private void UpdateUIInfo()
	{
		if (string.IsNullOrEmpty(mCreateRequest.GuildName))
		{
			mCreateRequest.SetGuildName(string.Empty);
		}
		SetInput(UI.IPT_NAME, mCreateRequest.GuildName, 30, OnChangeGuildName);
		if (string.IsNullOrEmpty(mCreateRequest.GuildTag))
		{
			mCreateRequest.SetGuildTag(string.Empty);
		}
		SetInput(UI.IPT_TAG, mCreateRequest.GuildTag, 4, OnChangeGuildTag);
		if (string.IsNullOrEmpty(mCreateRequest.GuildDescribe))
		{
			mCreateRequest.SetGuildDescribe(string.Empty);
		}
		SetInput(UI.IPT_DESCRIBE, mCreateRequest.GuildDescribe, 256, OnChangeGuildDescribe);
		UpdateEmblems();
		SetTouchAndRelease((Enum)UI.BTN_INFO, "TAG_INFO_SHOW", "TAG_INFO_HIDE", (object)null);
		SetActive((Enum)UI.SPR_TAG, is_visible: false);
		bool flag = MonoBehaviourSingleton<GuildManager>.I.guildData != null && MonoBehaviourSingleton<GuildManager>.I.guildData.clanId != -1;
		SetActive((Enum)UI.BTN_NEXT, !flag);
		SetActive((Enum)UI.BTN_NEXT_UPDATE, flag);
	}

	protected virtual void OnChangeGuildName()
	{
		if (!FindCtrl(base._transform, UI.LBL_INPUT).get_gameObject().get_activeInHierarchy())
		{
			SetActive((Enum)UI.LBL_INPUT, is_visible: true);
		}
		string inputValue = GetInputValue((Enum)UI.IPT_NAME);
		if (string.IsNullOrEmpty(inputValue))
		{
			SetActive((Enum)UI.LBL_DEFAULT_INPUT_NAME, is_visible: true);
			if (FindCtrl(base._transform, UI.IPT_NAME).get_gameObject().get_activeInHierarchy())
			{
				SetActive((Enum)UI.LBL_INPUT, is_visible: false);
			}
		}
		else
		{
			SetActive((Enum)UI.LBL_DEFAULT_INPUT_NAME, is_visible: false);
		}
		mCreateRequest.SetGuildName(inputValue);
	}

	protected virtual void OnChangeGuildTag()
	{
		string inputValue = GetInputValue((Enum)UI.IPT_TAG);
		inputValue = inputValue.ToUpper();
		SetLabelText((Enum)UI.LBL_INPUT_TAG, inputValue);
		mCreateRequest.SetGuildTag(inputValue);
	}

	protected virtual void OnChangeGuildDescribe()
	{
		string inputValue = GetInputValue((Enum)UI.IPT_DESCRIBE);
		if (string.IsNullOrEmpty(inputValue))
		{
			SetActive((Enum)UI.LBL_DEFAULT_INPUT_DESCRIBE, is_visible: true);
		}
		else
		{
			SetActive((Enum)UI.LBL_DEFAULT_INPUT_DESCRIBE, is_visible: false);
		}
		inputValue = inputValue.Replace("\n", "\\n");
		mCreateRequest.SetGuildDescribe(inputValue);
	}

	private void UpdateEmblems()
	{
		if (mCreateRequest.EmblemLayerIDs[0] == -1)
		{
			mCreateRequest.SetEmblemID(0, GuildManager.sDefaultEmblemIDLayer1);
		}
		SetSprite((Enum)UI.SPR_GUILD_EMBLEM_1, GuildItemManager.I.GetItemSprite(mCreateRequest.EmblemLayerIDs[0]));
		if (mCreateRequest.EmblemLayerIDs[1] == -1)
		{
			mCreateRequest.SetEmblemID(1, GuildManager.sDefaultEmblemIDLayer2);
		}
		SetSprite((Enum)UI.SPR_GUILD_EMBLEM_2, GuildItemManager.I.GetItemSprite(mCreateRequest.EmblemLayerIDs[1]));
		if (mCreateRequest.EmblemLayerIDs[2] == -1)
		{
			mCreateRequest.SetEmblemID(2, GuildManager.sDefaultEmblemIDLayer3);
		}
		SetSprite((Enum)UI.SPR_GUILD_EMBLEM_3, GuildItemManager.I.GetItemSprite(mCreateRequest.EmblemLayerIDs[2]));
	}

	private void OnQuery_TAG_INFO_SHOW()
	{
		SetActive((Enum)UI.SPR_TAG, is_visible: true);
	}

	private void OnQuery_TAG_INFO_HIDE()
	{
		SetActive((Enum)UI.SPR_TAG, is_visible: false);
	}

	private void OnQuery_RANDOM_EMBLEM()
	{
		int[] array = GuildItemManager.I.RandomEmblem(isFree: true);
		mCreateRequest.SetEmblemID(0, array[0]);
		mCreateRequest.SetEmblemID(1, array[1]);
		mCreateRequest.SetEmblemID(2, array[2]);
		UpdateEmblems();
	}

	private void OnQuery_CLOSE()
	{
		if (MonoBehaviourSingleton<GuildManager>.I.guildData != null && MonoBehaviourSingleton<GuildManager>.I.guildData.clanId != -1)
		{
			GameSection.ChangeEvent("CLOSE_UPDATE");
		}
	}

	private void OnQuery_STEP_1()
	{
		MonoBehaviourSingleton<GuildManager>.I.SetGuildChangeData(mCreateRequest);
		if (string.IsNullOrEmpty(mCreateRequest.GuildName))
		{
			MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, "Please enter your Clan name"), delegate
			{
			});
			GameSection.StopEvent();
		}
		else if (string.IsNullOrEmpty(mCreateRequest.GuildTag))
		{
			MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, "Please enter your Clan tag. This will be added to your Hunter name as a prefix."), delegate
			{
			});
			GameSection.StopEvent();
		}
		else if (string.IsNullOrEmpty(mCreateRequest.GuildDescribe))
		{
			MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, " Please enter your Clan description. You can give an introduction, share your motto, or list rules."), delegate
			{
			});
			GameSection.StopEvent();
		}
		else
		{
			GameSection.StayEvent();
			MonoBehaviourSingleton<GuildManager>.I.SendCheckClanSetting((MonoBehaviourSingleton<GuildManager>.I.guildData != null) ? MonoBehaviourSingleton<GuildManager>.I.guildData.clanId : 0, mCreateRequest.GuildName, mCreateRequest.GuildTag, mCreateRequest.GuildDescribe, delegate(bool isSuccess, Error error)
			{
				GameSection.ResumeEvent(isSuccess);
			});
		}
	}

	private void OnCloseDialog_CustomEmblem()
	{
		UpdateEmblems();
	}
}
