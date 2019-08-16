using System.Collections.Generic;
using UnityEngine;

public class ProfilePlayData : GameSection
{
	private enum UI
	{
		GRD_LIST,
		LBL_NAME,
		LBL_NUM
	}

	private const int NUM_BASE = 1000;

	public override IEnumerable<string> requireDataTable
	{
		get
		{
			yield return "PlayDataTable";
		}
	}

	public override void Initialize()
	{
		base.Initialize();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		PlayDataTable.PlayData[] nameList = Singleton<PlayDataTable>.I.GetSortedPlayData(MonoBehaviourSingleton<AchievementManager>.I.GetAchievementCounterList().ToArray());
		SetGrid(UI.GRD_LIST, "ProfilePlaydataListItem", nameList.Length, reset: false, delegate(int i, Transform t, bool is_recycle)
		{
			PlayDataTable.PlayData playData = nameList[i];
			SetLabelText(t, UI.LBL_NAME, playData.name);
			SetLabelText(t, UI.LBL_NUM, string.Format(playData.format, playData.count));
		});
	}
}
