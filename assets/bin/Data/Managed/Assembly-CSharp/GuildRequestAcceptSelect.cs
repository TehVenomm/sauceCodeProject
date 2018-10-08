using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GuildRequestAcceptSelect : QuestAcceptSelect
{
	public override void UpdateUI()
	{
		base.UpdateUI();
		SetActive(UI.BTN_PARTY, false);
		SetActive(UI.TWN_DIFFICULT_STAR, false);
		GetCtrl(UI.BTN_GUILD_REQUEST).localPosition = new Vector3(0f, 0f, 0f);
		SetLabelText(UI.LBL_LIMIT_TIME, "--:--");
		RARITY_TYPE rarity = questInfo.questData.tableData.rarity;
		SetActive(UI.LBL_GUILD_REQUEST_NEED_POINT, true);
		string format = StringTable.Get(STRING_CATEGORY.GUILD_REQUEST, 6u);
		string text = string.Format(format, MonoBehaviourSingleton<GuildRequestManager>.I.GetNeedPoint(rarity), MonoBehaviourSingleton<GuildRequestManager>.I.GetNeedTimeWithFormat(rarity));
		SetLabelText(UI.LBL_GUILD_REQUEST_NEED_POINT, text);
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

	protected virtual void OnQuery_GuildRequestSortieMessage_YES()
	{
		GuildRequestItem selectedItem = MonoBehaviourSingleton<GuildRequestManager>.I.GetSelectedItem();
		List<GameSectionHistory.HistoryData> historyList = MonoBehaviourSingleton<GameSceneManager>.I.GetHistoryList();
		bool flag = historyList.Any((GameSectionHistory.HistoryData h) => h.sectionName == "QuestAcceptChallengeCounter" || h.sectionName == "GuildRequestChallengeCounter");
		GameSection.StayEvent();
		MonoBehaviourSingleton<GuildRequestManager>.I.SendGuildRequestStart(questInfo, !flag, delegate(bool isSuccess)
		{
			GuildRequestAcceptSelect guildRequestAcceptSelect = this;
			SendGetChallengeInfo(delegate
			{
				GameSection.ResumeEvent(isSuccess, null);
			}, null);
		});
	}

	private void SendGetChallengeInfo(Action onFinish, Action<bool> cb)
	{
		MonoBehaviourSingleton<PartyManager>.I.SendGetChallengeInfo(delegate(bool is_success, Error err)
		{
			if (onFinish != null)
			{
				onFinish();
			}
			if (cb != null)
			{
				cb(is_success);
			}
		});
	}
}
