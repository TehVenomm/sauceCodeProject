using Network;
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
			((_003CGetClanStatistic_003Ec__Iterator5A)/*Error near IL_003a: stateMachine*/)._003Cfinish_get_statistic_003E__0 = true;
			if (success)
			{
				((_003CGetClanStatistic_003Ec__Iterator5A)/*Error near IL_003a: stateMachine*/)._003C_info_003E__1 = info;
			}
		});
		while (!finish_get_statistic)
		{
			yield return (object)null;
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
		SetTouchAndRelease(UI.BTN_INFO, "TAG_INFO_SHOW", "TAG_INFO_HIDE", null);
		SetActive(UI.SPR_TAG, false);
		bool flag = MonoBehaviourSingleton<GuildManager>.I.guildData != null && MonoBehaviourSingleton<GuildManager>.I.guildData.clanId != -1;
		SetActive(UI.BTN_NEXT, !flag);
		SetActive(UI.BTN_NEXT_UPDATE, flag);
	}

	protected virtual void OnChangeGuildName()
	{
		if (!FindCtrl(base._transform, UI.LBL_INPUT).gameObject.activeInHierarchy)
		{
			SetActive(UI.LBL_INPUT, true);
		}
		string inputValue = GetInputValue(UI.IPT_NAME);
		if (string.IsNullOrEmpty(inputValue))
		{
			SetActive(UI.LBL_DEFAULT_INPUT_NAME, true);
			if (FindCtrl(base._transform, UI.IPT_NAME).gameObject.activeInHierarchy)
			{
				SetActive(UI.LBL_INPUT, false);
			}
		}
		else
		{
			SetActive(UI.LBL_DEFAULT_INPUT_NAME, false);
		}
		mCreateRequest.SetGuildName(inputValue);
	}

	protected virtual void OnChangeGuildTag()
	{
		string inputValue = GetInputValue(UI.IPT_TAG);
		inputValue = inputValue.ToUpper();
		SetLabelText(UI.LBL_INPUT_TAG, inputValue);
		mCreateRequest.SetGuildTag(inputValue);
	}

	protected virtual void OnChangeGuildDescribe()
	{
		string inputValue = GetInputValue(UI.IPT_DESCRIBE);
		if (string.IsNullOrEmpty(inputValue))
		{
			SetActive(UI.LBL_DEFAULT_INPUT_DESCRIBE, true);
		}
		else
		{
			SetActive(UI.LBL_DEFAULT_INPUT_DESCRIBE, false);
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
		SetSprite(UI.SPR_GUILD_EMBLEM_1, GuildItemManager.I.GetItemSprite(mCreateRequest.EmblemLayerIDs[0]));
		if (mCreateRequest.EmblemLayerIDs[1] == -1)
		{
			mCreateRequest.SetEmblemID(1, GuildManager.sDefaultEmblemIDLayer2);
		}
		SetSprite(UI.SPR_GUILD_EMBLEM_2, GuildItemManager.I.GetItemSprite(mCreateRequest.EmblemLayerIDs[1]));
		if (mCreateRequest.EmblemLayerIDs[2] == -1)
		{
			mCreateRequest.SetEmblemID(2, GuildManager.sDefaultEmblemIDLayer3);
		}
		SetSprite(UI.SPR_GUILD_EMBLEM_3, GuildItemManager.I.GetItemSprite(mCreateRequest.EmblemLayerIDs[2]));
	}

	private void OnQuery_TAG_INFO_SHOW()
	{
		SetActive(UI.SPR_TAG, true);
	}

	private void OnQuery_TAG_INFO_HIDE()
	{
		SetActive(UI.SPR_TAG, false);
	}

	private void OnQuery_RANDOM_EMBLEM()
	{
		int[] array = GuildItemManager.I.RandomEmblem(true);
		mCreateRequest.SetEmblemID(0, array[0]);
		mCreateRequest.SetEmblemID(1, array[1]);
		mCreateRequest.SetEmblemID(2, array[2]);
		UpdateEmblems();
	}

	private void OnQuery_CLOSE()
	{
		if (MonoBehaviourSingleton<GuildManager>.I.guildData != null && MonoBehaviourSingleton<GuildManager>.I.guildData.clanId != -1)
		{
			GameSection.ChangeEvent("CLOSE_UPDATE", null);
		}
	}

	private void OnQuery_STEP_1()
	{
		MonoBehaviourSingleton<GuildManager>.I.SetGuildChangeData(mCreateRequest);
		if (string.IsNullOrEmpty(mCreateRequest.GuildName))
		{
			MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, "Please enter your Clan name", null, null, null, null), delegate
			{
			}, false, 0);
			GameSection.StopEvent();
		}
		else if (string.IsNullOrEmpty(mCreateRequest.GuildTag))
		{
			MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, "Please enter your Clan tag. This will be added to your Hunter name as a prefix.", null, null, null, null), delegate
			{
			}, false, 0);
			GameSection.StopEvent();
		}
		else if (string.IsNullOrEmpty(mCreateRequest.GuildDescribe))
		{
			MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, " Please enter your Clan description. You can give an introduction, share your motto, or list rules.", null, null, null, null), delegate
			{
			}, false, 0);
			GameSection.StopEvent();
		}
		else
		{
			GameSection.StayEvent();
			MonoBehaviourSingleton<GuildManager>.I.SendCheckClanSetting((MonoBehaviourSingleton<GuildManager>.I.guildData != null) ? MonoBehaviourSingleton<GuildManager>.I.guildData.clanId : 0, mCreateRequest.GuildName, mCreateRequest.GuildTag, mCreateRequest.GuildDescribe, delegate(bool isSuccess, Error error)
			{
				GameSection.ResumeEvent(isSuccess, null);
			});
		}
	}

	private void OnCloseDialog_CustomEmblem()
	{
		UpdateEmblems();
	}
}
