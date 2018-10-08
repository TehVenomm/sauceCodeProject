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

	private unsafe IEnumerator GetClanStatistic(int clanID)
	{
		bool finish_get_statistic = false;
		GuildStatisticInfo _info = null;
		MonoBehaviourSingleton<GuildManager>.I.SendRequestStatistic(clanID, new Action<bool, GuildStatisticInfo>((object)/*Error near IL_003a: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
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
		SetInput((Enum)UI.IPT_NAME, mCreateRequest.GuildName, 30, (EventDelegate.Callback)OnChangeGuildName);
		if (string.IsNullOrEmpty(mCreateRequest.GuildTag))
		{
			mCreateRequest.SetGuildTag(string.Empty);
		}
		SetInput((Enum)UI.IPT_TAG, mCreateRequest.GuildTag, 4, (EventDelegate.Callback)OnChangeGuildTag);
		if (string.IsNullOrEmpty(mCreateRequest.GuildDescribe))
		{
			mCreateRequest.SetGuildDescribe(string.Empty);
		}
		SetInput((Enum)UI.IPT_DESCRIBE, mCreateRequest.GuildDescribe, 256, (EventDelegate.Callback)OnChangeGuildDescribe);
		UpdateEmblems();
		SetTouchAndRelease((Enum)UI.BTN_INFO, "TAG_INFO_SHOW", "TAG_INFO_HIDE", (object)null);
		SetActive((Enum)UI.SPR_TAG, false);
		bool flag = MonoBehaviourSingleton<GuildManager>.I.guildData != null && MonoBehaviourSingleton<GuildManager>.I.guildData.clanId != -1;
		SetActive((Enum)UI.BTN_NEXT, !flag);
		SetActive((Enum)UI.BTN_NEXT_UPDATE, flag);
	}

	protected virtual void OnChangeGuildName()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		if (!FindCtrl(base._transform, UI.LBL_INPUT).get_gameObject().get_activeInHierarchy())
		{
			SetActive((Enum)UI.LBL_INPUT, true);
		}
		string inputValue = GetInputValue((Enum)UI.IPT_NAME);
		if (string.IsNullOrEmpty(inputValue))
		{
			SetActive((Enum)UI.LBL_DEFAULT_INPUT_NAME, true);
			if (FindCtrl(base._transform, UI.IPT_NAME).get_gameObject().get_activeInHierarchy())
			{
				SetActive((Enum)UI.LBL_INPUT, false);
			}
		}
		else
		{
			SetActive((Enum)UI.LBL_DEFAULT_INPUT_NAME, false);
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
			SetActive((Enum)UI.LBL_DEFAULT_INPUT_DESCRIBE, true);
		}
		else
		{
			SetActive((Enum)UI.LBL_DEFAULT_INPUT_DESCRIBE, false);
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
		SetActive((Enum)UI.SPR_TAG, true);
	}

	private void OnQuery_TAG_INFO_HIDE()
	{
		SetActive((Enum)UI.SPR_TAG, false);
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

	private unsafe void OnQuery_STEP_1()
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
			GuildManager i = MonoBehaviourSingleton<GuildManager>.I;
			int clanId = (MonoBehaviourSingleton<GuildManager>.I.guildData != null) ? MonoBehaviourSingleton<GuildManager>.I.guildData.clanId : 0;
			string guildName = mCreateRequest.GuildName;
			string guildTag = mCreateRequest.GuildTag;
			string guildDescribe = mCreateRequest.GuildDescribe;
			if (_003C_003Ef__am_0024cache4 == null)
			{
				_003C_003Ef__am_0024cache4 = new Action<bool, Error>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			i.SendCheckClanSetting(clanId, guildName, guildTag, guildDescribe, _003C_003Ef__am_0024cache4);
		}
	}

	private void OnCloseDialog_CustomEmblem()
	{
		UpdateEmblems();
	}
}
