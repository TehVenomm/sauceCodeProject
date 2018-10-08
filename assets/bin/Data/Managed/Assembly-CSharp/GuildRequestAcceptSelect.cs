using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GuildRequestAcceptSelect : QuestAcceptSelect
{
	public override void UpdateUI()
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		base.UpdateUI();
		SetActive((Enum)UI.BTN_PARTY, false);
		SetActive((Enum)UI.TWN_DIFFICULT_STAR, false);
		GetCtrl(UI.BTN_GUILD_REQUEST).set_localPosition(new Vector3(0f, 0f, 0f));
		SetLabelText((Enum)UI.LBL_LIMIT_TIME, "--:--");
		RARITY_TYPE rarity = questInfo.questData.tableData.rarity;
		SetActive((Enum)UI.LBL_GUILD_REQUEST_NEED_POINT, true);
		string format = StringTable.Get(STRING_CATEGORY.GUILD_REQUEST, 6u);
		string text = string.Format(format, MonoBehaviourSingleton<GuildRequestManager>.I.GetNeedPoint(rarity), MonoBehaviourSingleton<GuildRequestManager>.I.GetNeedTimeWithFormat(rarity));
		SetLabelText((Enum)UI.LBL_GUILD_REQUEST_NEED_POINT, text);
	}

	protected override void OnQuery_GUILD_REQUEST()
	{
		GuildRequestItem selectedItem = MonoBehaviourSingleton<GuildRequestManager>.I.GetSelectedItem();
		string arg = MonoBehaviourSingleton<GuildRequestManager>.I.GetNeedPoint(questInfo.questData.tableData.rarity).ToString();
		string needTimeWithFormat = MonoBehaviourSingleton<GuildRequestManager>.I.GetNeedTimeWithFormat(questInfo.questData.tableData.rarity);
		string houndRemainTimeWithFormat = selectedItem.GetHoundRemainTimeWithFormat();
		TimeSpan needTime = MonoBehaviourSingleton<GuildRequestManager>.I.GetNeedTime(questInfo.questData.tableData.rarity);
		TimeSpan houndRemainTime = selectedItem.GetHoundRemainTime();
		string eventData = (!(0.0 < houndRemainTime.TotalSeconds) || !(houndRemainTime < needTime)) ? string.Format(StringTable.Get(STRING_CATEGORY.GUILD_REQUEST, 0u), arg, needTimeWithFormat) : string.Format(StringTable.Get(STRING_CATEGORY.GUILD_REQUEST, 5u), arg, needTimeWithFormat, houndRemainTimeWithFormat);
		GameSection.SetEventData(eventData);
	}

	protected unsafe virtual void OnQuery_GuildRequestSortieMessage_YES()
	{
		GuildRequestItem selectedItem = MonoBehaviourSingleton<GuildRequestManager>.I.GetSelectedItem();
		List<GameSectionHistory.HistoryData> historyList = MonoBehaviourSingleton<GameSceneManager>.I.GetHistoryList();
		List<GameSectionHistory.HistoryData> source = historyList;
		if (_003C_003Ef__am_0024cache0 == null)
		{
			_003C_003Ef__am_0024cache0 = new Func<GameSectionHistory.HistoryData, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		bool flag = source.Any(_003C_003Ef__am_0024cache0);
		GameSection.StayEvent();
		MonoBehaviourSingleton<GuildRequestManager>.I.SendGuildRequestStart(questInfo, !flag, delegate
		{
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Expected O, but got Unknown
			GuildRequestAcceptSelect guildRequestAcceptSelect = this;
			_003COnQuery_GuildRequestSortieMessage_YES_003Ec__AnonStorey371 _003COnQuery_GuildRequestSortieMessage_YES_003Ec__AnonStorey;
			SendGetChallengeInfo(new Action((object)_003COnQuery_GuildRequestSortieMessage_YES_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), null);
		});
	}

	private unsafe void SendGetChallengeInfo(Action onFinish, Action<bool> cb)
	{
		_003CSendGetChallengeInfo_003Ec__AnonStorey372 _003CSendGetChallengeInfo_003Ec__AnonStorey;
		MonoBehaviourSingleton<PartyManager>.I.SendGetChallengeInfo(new Action<bool, Error>((object)_003CSendGetChallengeInfo_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}
}
