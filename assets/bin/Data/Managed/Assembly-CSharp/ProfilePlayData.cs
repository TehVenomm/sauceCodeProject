using System;
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

	public unsafe override void UpdateUI()
	{
		base.UpdateUI();
		PlayDataTable.PlayData[] nameList = Singleton<PlayDataTable>.I.GetSortedPlayData(MonoBehaviourSingleton<AchievementManager>.I.GetAchievementCounterList().ToArray());
		_003CUpdateUI_003Ec__AnonStorey3EC _003CUpdateUI_003Ec__AnonStorey3EC;
		SetGrid(UI.GRD_LIST, "ProfilePlaydataListItem", nameList.Length, false, new Action<int, Transform, bool>((object)_003CUpdateUI_003Ec__AnonStorey3EC, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}
}
